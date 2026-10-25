Imports System.IO
Imports System.Data
Imports System.Drawing
Imports System.Data.OleDb
Imports System.Windows.Forms

' ================================================================
'  Form2  -  Placeholder Manager
'  Word VSTO Add-in: Placeholder Insertion Helper
'
'  Column layout (Layout):
'    rdo1Col  → 1x1 single-column  (narrow side-pane width: 284px)
'    rdo2Col  → 1x2 two-column     (wider side-pane width: 480px)
'
'  Insertion mode (Mode):
'    rdoSyntax   → inserts  {#Name}  as plain text at cursor
'    rdoControl  → inserts a Rich Text Content Control with
'                  Title and Tag set to the lowercased name
' ================================================================
Public Class Form2

    ' ── State ─────────────────────────────────────────────────────
    Private _placeholders    As New List(Of String)
    Private _lastFormSize    As System.Drawing.Size = System.Drawing.Size.Empty
    Private _isInternalWidth As Boolean = False

    ' ── Layout constants ──────────────────────────────────────────
    Private Const BTN_FONT_SIZE As Single  = 9.75!
    Private Const BTN_HEIGHT    As Integer = 34
    Private Const BTN_MARGIN    As Integer = 4

    ' Target widths for column modes
    Private Const WIDTH_1COL As Integer = 284
    Private Const WIDTH_2COL As Integer = 480

    ' ================================================================
    '  FORM LOAD
    ' ================================================================
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rdo1Col.Checked   = True
        rdoSyntax.Checked = True
        _lastFormSize     = Me.Size

        Dim savedPath As String = My.Settings.SavedListPath
        If Not String.IsNullOrWhiteSpace(savedPath) AndAlso File.Exists(savedPath) Then
            _placeholders = ParseFile(savedPath)
            RebuildButtons()
        End If
    End Sub

    ' ================================================================
    '  BROWSE BUTTON
    ' ================================================================
    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Using dlg As New OpenFileDialog()
            dlg.Title       = "Select Placeholder List File"
            dlg.Filter      = "Supported Files (*.txt;*.csv;*.xlsx)|*.txt;*.csv;*.xlsx" &
                              "|Text Files (*.txt)|*.txt" &
                              "|CSV Files (*.csv)|*.csv" &
                              "|Excel Files (*.xlsx)|*.xlsx"
            dlg.FilterIndex = 1

            If dlg.ShowDialog() = DialogResult.OK Then
                My.Settings.SavedListPath = dlg.FileName
                My.Settings.Save()
                _placeholders = ParseFile(dlg.FileName)
                RebuildButtons()
            End If
        End Using
    End Sub

    ' ================================================================
    '  COLUMN RADIOS  -  switch layout width & reflow buttons
    ' ================================================================
    Private Sub rdo1Col_CheckedChanged(sender As Object, e As EventArgs) Handles rdo1Col.CheckedChanged
        If rdo1Col.Checked Then
            ApplyColumnMode(WIDTH_1COL)
        End If
    End Sub

    Private Sub rdo2Col_CheckedChanged(sender As Object, e As EventArgs) Handles rdo2Col.CheckedChanged
        If rdo2Col.Checked Then
            ApplyColumnMode(WIDTH_2COL)
        End If
    End Sub

    Private Sub ApplyColumnMode(targetWidth As Integer)
        _isInternalWidth = True

        ' Keep the RIGHT edge pinned to the screen/Word border by extending LEFT
        Dim currentRight As Integer = Me.Left + Me.Width
        Dim newLeft      As Integer = currentRight - targetWidth

        ' Ensure the form does not extend beyond the left edge of the screen
        Dim screenArea As System.Drawing.Rectangle = Screen.FromControl(Me).WorkingArea
        If newLeft < screenArea.Left Then
            newLeft = screenArea.Left
        End If

        Me.SetBounds(newLeft, Me.Top, targetWidth, Me.Height)
        _lastFormSize    = Me.Size
        _isInternalWidth = False
        RebuildButtons()
    End Sub

    ' ================================================================
    '  PARSE FILE  ->  List(Of String)
    ' ================================================================
    Private Function ParseFile(filePath As String) As List(Of String)
        Dim results As New List(Of String)
        Try
            Dim ext As String = Path.GetExtension(filePath).ToLowerInvariant()

            If ext = ".txt" OrElse ext = ".csv" Then
                For Each line As String In File.ReadAllLines(filePath)
                    For Each part As String In line.Split(","c)
                        Dim cleaned As String = part.Trim().Trim(""""c).Trim("'"c).Trim()
                        If Not String.IsNullOrWhiteSpace(cleaned) Then results.Add(cleaned)
                    Next
                Next

            ElseIf ext = ".xlsx" Then
                ' HDR=NO ensures Row 1 is read as data (not skipped as a header row)
                ' IMEX=1 reads mixed data types as text
                Dim connStr As String =
                    "Provider=Microsoft.ACE.OLEDB.12.0;" &
                    "Data Source=" & filePath & ";" &
                    "Extended Properties=""Excel 12.0 Xml;HDR=NO;IMEX=1"""

                Using conn As New OleDbConnection(connStr)
                    conn.Open()
                    Dim schema As DataTable =
                        conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                    If schema Is Nothing OrElse schema.Rows.Count = 0 Then
                        Throw New InvalidOperationException("No sheets found in the Excel file.")
                    End If

                    ' Find the first actual worksheet (names end with $)
                    Dim sheetName As String = ""
                    For Each row As DataRow In schema.Rows
                        Dim tName As String = row("TABLE_NAME").ToString()
                        If tName.EndsWith("$") OrElse tName.EndsWith("$'") Then
                            sheetName = tName
                            Exit For
                        End If
                    Next
                    If String.IsNullOrEmpty(sheetName) Then
                        sheetName = schema.Rows(0)("TABLE_NAME").ToString()
                    End If

                    Using cmd As New OleDbCommand("SELECT * FROM [" & sheetName & "]", conn)
                        Using reader As OleDbDataReader = cmd.ExecuteReader()
                            While reader.Read()
                                Dim raw As Object = reader.GetValue(0)
                                If raw IsNot Nothing AndAlso raw IsNot DBNull.Value Then
                                    Dim cell As String = raw.ToString().Trim()
                                    If Not String.IsNullOrWhiteSpace(cell) Then results.Add(cell)
                                End If
                            End While
                        End Using
                    End Using
                End Using
            Else
                MessageBox.Show("Unsupported file type: " & ext,
                                "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show("Error reading file:" & Environment.NewLine & ex.Message,
                            "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return results
    End Function

    ' ================================================================
    '  REBUILD BUTTONS
    '  Reflows all placeholder buttons cleanly across 1 or 2 columns
    ' ================================================================
    Private Sub RebuildButtons()
        FlowLayoutPanel1.SuspendLayout()
        FlowLayoutPanel1.Controls.Clear()

        If _placeholders.Count = 0 Then
            FlowLayoutPanel1.ResumeLayout(performLayout:=True)
            Return
        End If

        Dim columns As Integer = If(rdo2Col.Checked, 2, 1)

        ' Compute usable width inside the panel
        ' FlowLayoutPanel1 has 6px left and 6px right padding = 12px
        Dim clientW As Integer = FlowLayoutPanel1.ClientSize.Width
        Dim sbW     As Integer = SystemInformation.VerticalScrollBarWidth
        Dim usableW As Integer = clientW - sbW - FlowLayoutPanel1.Padding.Horizontal - 2

        ' Split usable width evenly among columns
        Dim btnW As Integer = (usableW - (BTN_MARGIN * 2 * columns)) \ columns
        If btnW < 70 Then btnW = 70

        ' Configure flow direction
        If columns = 2 Then
            FlowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight
            FlowLayoutPanel1.WrapContents  = True
        Else
            FlowLayoutPanel1.FlowDirection = FlowDirection.TopDown
            FlowLayoutPanel1.WrapContents  = False
        End If

        For Each name As String In _placeholders
            Dim btn As New Button With {
                .Text = name,
                .Font = New System.Drawing.Font("Segoe UI", BTN_FONT_SIZE, System.Drawing.FontStyle.Regular),
                .Width = btnW,
                .Height = BTN_HEIGHT,
                .AutoSize = False,
                .Margin = New Padding(BTN_MARGIN),
                .TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                .UseVisualStyleBackColor = True
            }
            AddHandler btn.Click, AddressOf PlaceholderButton_Click
            FlowLayoutPanel1.Controls.Add(btn)
        Next

        FlowLayoutPanel1.ResumeLayout(performLayout:=True)
    End Sub

    ' ================================================================
    '  PLACEHOLDER BUTTON CLICK
    ' ================================================================
    Private Sub PlaceholderButton_Click(sender As Object, e As EventArgs)
        Dim name As String = DirectCast(sender, Button).Text
        If rdoControl.Checked Then
            InsertContentControl(name)
        Else
            InsertSyntax(name)
        End If
    End Sub

    ' ================================================================
    '  SYNTAX INSERTION  -  types {#Name} at cursor
    ' ================================================================
    Private Sub InsertSyntax(name As String)
        Try
            Globals.ThisAddIn.Application.Selection.TypeText("{#" & name & "}")
        Catch ex As Exception
            MessageBox.Show("Could not insert syntax placeholder:" &
                            Environment.NewLine & ex.Message,
                            "Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ================================================================
    '  CONTENT CONTROL INSERTION
    '  Inserts a wdContentControlRichText at the cursor.
    '  • Title and Tag = lowercased placeholder name
    ' ================================================================
    Private Sub InsertContentControl(name As String)
        Try
            Dim app     As Word.Application   = Globals.ThisAddIn.Application
            Dim doc     As Word.Document      = app.ActiveDocument
            Dim sel     As Word.Selection     = app.Selection
            Dim ccTitle As String             = name.ToLowerInvariant()

            ' Insert Rich Text content control at the current cursor range
            Dim cc As Word.ContentControl =
                doc.ContentControls.Add(
                    Word.WdContentControlType.wdContentControlRichText,
                    sel.Range)

            cc.Title = ccTitle   ' lowercased name for later lookup
            cc.Tag   = ccTitle   ' machine-readable key

            ' Collapse cursor to end of the new control so user can keep inserting
            cc.Range.Collapse(Word.WdCollapseDirection.wdCollapseEnd)
            cc.Range.Select()

        Catch ex As Exception
            MessageBox.Show("Could not insert Content Control:" &
                            Environment.NewLine & ex.Message,
                            "Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ================================================================
    '  FORM RESIZE END
    '  Only recalculates buttons when the user actually resized the
    '  dimensions, NEVER on pure move/drag.
    ' ================================================================
    Private Sub Form2_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        If _isInternalWidth Then Return

        ' If dimensions did not change (user merely moved the form), do nothing!
        If Me.Size <> _lastFormSize Then
            _lastFormSize = Me.Size
            RebuildButtons()
        End If
    End Sub

End Class

