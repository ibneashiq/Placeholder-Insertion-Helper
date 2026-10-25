<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form2
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.pnlColumns = New System.Windows.Forms.Panel()
        Me.lblColumns = New System.Windows.Forms.Label()
        Me.rdo1Col = New System.Windows.Forms.RadioButton()
        Me.rdo2Col = New System.Windows.Forms.RadioButton()
        Me.pnlInsertType = New System.Windows.Forms.Panel()
        Me.lblInsert = New System.Windows.Forms.Label()
        Me.rdoSyntax = New System.Windows.Forms.RadioButton()
        Me.rdoControl = New System.Windows.Forms.RadioButton()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.pnlTop.SuspendLayout()
        Me.pnlColumns.SuspendLayout()
        Me.pnlInsertType.SuspendLayout()
        Me.SuspendLayout()
        '
        ' pnlTop (Header container docked to top)
        '
        Me.pnlTop.BackColor = System.Drawing.SystemColors.Control
        Me.pnlTop.Controls.Add(Me.btnBrowse)
        Me.pnlTop.Controls.Add(Me.pnlColumns)
        Me.pnlTop.Controls.Add(Me.pnlInsertType)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(284, 116)
        Me.pnlTop.TabIndex = 0
        '
        ' btnBrowse
        '
        Me.btnBrowse.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBrowse.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        Me.btnBrowse.Location = New System.Drawing.Point(8, 8)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(268, 34)
        Me.btnBrowse.TabIndex = 0
        Me.btnBrowse.Text = "Browse for Placeholder List..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        ' pnlColumns (Radio group 1: column layout)
        '
        Me.pnlColumns.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlColumns.Controls.Add(Me.lblColumns)
        Me.pnlColumns.Controls.Add(Me.rdo1Col)
        Me.pnlColumns.Controls.Add(Me.rdo2Col)
        Me.pnlColumns.Location = New System.Drawing.Point(8, 48)
        Me.pnlColumns.Name = "pnlColumns"
        Me.pnlColumns.Size = New System.Drawing.Size(268, 28)
        Me.pnlColumns.TabIndex = 1
        '
        ' lblColumns
        '
        Me.lblColumns.AutoSize = True
        Me.lblColumns.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))
        Me.lblColumns.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblColumns.Location = New System.Drawing.Point(2, 6)
        Me.lblColumns.Name = "lblColumns"
        Me.lblColumns.Size = New System.Drawing.Size(48, 15)
        Me.lblColumns.TabIndex = 0
        Me.lblColumns.Text = "Layout:"
        '
        ' rdo1Col
        '
        Me.rdo1Col.AutoSize = True
        Me.rdo1Col.Checked = True
        Me.rdo1Col.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        Me.rdo1Col.Location = New System.Drawing.Point(62, 3)
        Me.rdo1Col.Name = "rdo1Col"
        Me.rdo1Col.Size = New System.Drawing.Size(46, 21)
        Me.rdo1Col.TabIndex = 1
        Me.rdo1Col.TabStop = True
        Me.rdo1Col.Text = "1x1"
        Me.rdo1Col.UseVisualStyleBackColor = True
        '
        ' rdo2Col
        '
        Me.rdo2Col.AutoSize = True
        Me.rdo2Col.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        Me.rdo2Col.Location = New System.Drawing.Point(124, 3)
        Me.rdo2Col.Name = "rdo2Col"
        Me.rdo2Col.Size = New System.Drawing.Size(46, 21)
        Me.rdo2Col.TabIndex = 2
        Me.rdo2Col.Text = "1x2"
        Me.rdo2Col.UseVisualStyleBackColor = True
        '
        ' pnlInsertType (Radio group 2: insertion mode)
        '
        Me.pnlInsertType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlInsertType.Controls.Add(Me.lblInsert)
        Me.pnlInsertType.Controls.Add(Me.rdoSyntax)
        Me.pnlInsertType.Controls.Add(Me.rdoControl)
        Me.pnlInsertType.Location = New System.Drawing.Point(8, 78)
        Me.pnlInsertType.Name = "pnlInsertType"
        Me.pnlInsertType.Size = New System.Drawing.Size(268, 28)
        Me.pnlInsertType.TabIndex = 2
        '
        ' lblInsert
        '
        Me.lblInsert.AutoSize = True
        Me.lblInsert.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))
        Me.lblInsert.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblInsert.Location = New System.Drawing.Point(2, 6)
        Me.lblInsert.Name = "lblInsert"
        Me.lblInsert.Size = New System.Drawing.Size(42, 15)
        Me.lblInsert.TabIndex = 0
        Me.lblInsert.Text = "Mode:"
        '
        ' rdoSyntax
        '
        Me.rdoSyntax.AutoSize = True
        Me.rdoSyntax.Checked = True
        Me.rdoSyntax.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        Me.rdoSyntax.Location = New System.Drawing.Point(62, 3)
        Me.rdoSyntax.Name = "rdoSyntax"
        Me.rdoSyntax.Size = New System.Drawing.Size(64, 21)
        Me.rdoSyntax.TabIndex = 1
        Me.rdoSyntax.TabStop = True
        Me.rdoSyntax.Text = "Syntax"
        Me.rdoSyntax.UseVisualStyleBackColor = True
        '
        ' rdoControl
        '
        Me.rdoControl.AutoSize = True
        Me.rdoControl.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        Me.rdoControl.Location = New System.Drawing.Point(140, 3)
        Me.rdoControl.Name = "rdoControl"
        Me.rdoControl.Size = New System.Drawing.Size(70, 21)
        Me.rdoControl.TabIndex = 2
        Me.rdoControl.Text = "Control"
        Me.rdoControl.UseVisualStyleBackColor = True
        '
        ' FlowLayoutPanel1 (Fills remaining client area)
        '
        Me.FlowLayoutPanel1.AutoScroll = True
        Me.FlowLayoutPanel1.BackColor = System.Drawing.SystemColors.Window
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 116)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Padding = New System.Windows.Forms.Padding(6, 4, 6, 6)
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(284, 464)
        Me.FlowLayoutPanel1.TabIndex = 1
        '
        ' Form2
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(284, 580)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MinimumSize = New System.Drawing.Size(240, 360)
        Me.Name = "Form2"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Placeholder Manager"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlColumns.ResumeLayout(False)
        Me.pnlColumns.PerformLayout()
        Me.pnlInsertType.ResumeLayout(False)
        Me.pnlInsertType.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents pnlColumns As System.Windows.Forms.Panel
    Friend WithEvents lblColumns As System.Windows.Forms.Label
    Friend WithEvents rdo1Col As System.Windows.Forms.RadioButton
    Friend WithEvents rdo2Col As System.Windows.Forms.RadioButton
    Friend WithEvents pnlInsertType As System.Windows.Forms.Panel
    Friend WithEvents lblInsert As System.Windows.Forms.Label
    Friend WithEvents rdoSyntax As System.Windows.Forms.RadioButton
    Friend WithEvents rdoControl As System.Windows.Forms.RadioButton
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel

End Class

