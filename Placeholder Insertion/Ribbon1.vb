Imports Microsoft.Office.Tools.Ribbon
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

' ================================================================
'  Ribbon1  -  Placeholder Insertion Helper
'  Word VSTO Add-in
'
'  Adds "Insert Placeholder" to the Add-Ins tab in Word.
'  Opens Form2 docked on the RIGHT-HAND SIDE of the Word window
'  positioned like a side pane / task pane.
' ================================================================
Public Class Ribbon1

    <StructLayout(LayoutKind.Sequential)>
    Private Structure RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer
    End Structure

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function GetWindowRect(ByVal hWnd As IntPtr, ByRef lpRect As RECT) As Boolean
    End Function

    Private Sub Ribbon1_Load(ByVal sender As System.Object,
                             ByVal e As RibbonUIEventArgs) Handles MyBase.Load
    End Sub

    ' ----------------------------------------------------------------
    '  Opens Placeholder Manager docked on the right side of Word
    ' ----------------------------------------------------------------
    Private Sub btnOpenPlaceholderForm_Click(sender As Object,
                                             e As RibbonControlEventArgs) _
                                             Handles btnOpenPlaceholderForm.Click
        Dim frm As New Form2()
        frm.TopMost       = True
        frm.StartPosition = FormStartPosition.Manual

        Try
            ' Retrieve Word's main window handle and exact screen pixels
            Dim wordHwnd As IntPtr = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle
            Dim wordRect As RECT
            Dim hasRect  As Boolean = False

            If wordHwnd <> IntPtr.Zero Then
                hasRect = GetWindowRect(wordHwnd, wordRect)
            End If

            If hasRect Then
                Dim screenArea As System.Drawing.Rectangle = Screen.FromHandle(wordHwnd).WorkingArea

                Dim formW As Integer = frm.Width
                ' Top starts slightly below Word's title/ribbon area (~65px)
                Dim formT As Integer = wordRect.Top + 65
                ' Height spans the document editing area
                Dim formH As Integer = Math.Max(400, (wordRect.Bottom - wordRect.Top) - 75)
                ' Placed directly on the right edge of Word's window
                Dim formL As Integer = wordRect.Right - formW - 8

                ' Keep inside screen bounds
                formL = Math.Max(screenArea.Left, Math.Min(formL, screenArea.Right - formW))
                formT = Math.Max(screenArea.Top,  Math.Min(formT, screenArea.Bottom - formH))
                formH = Math.Min(formH, screenArea.Height)

                frm.SetBounds(formL, formT, formW, formH)
            Else
                ' Fallback: dock at right of primary screen
                Dim workArea As System.Drawing.Rectangle = Screen.PrimaryScreen.WorkingArea
                frm.SetBounds(workArea.Right - frm.Width - 10, workArea.Top + 60, frm.Width, workArea.Height - 120)
            End If

        Catch
            ' If positioning fails, open at center
            frm.StartPosition = FormStartPosition.WindowsDefaultLocation
        End Try

        frm.Show()
    End Sub

End Class

