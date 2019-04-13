<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.文件FToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.打开数据文件 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.保存报告 = New System.Windows.Forms.ToolStripMenuItem()
        Me.保存图形 = New System.Windows.Forms.ToolStripMenuItem()
        Me.输出DXF文件 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.退出 = New System.Windows.Forms.ToolStripMenuItem()
        Me.计算 = New System.Windows.Forms.ToolStripMenuItem()
        Me.查看CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.示意图 = New System.Windows.Forms.ToolStripMenuItem()
        Me.报告 = New System.Windows.Forms.ToolStripMenuItem()
        Me.刷新ZToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.帮助HToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.open = New System.Windows.Forms.ToolStripButton()
        Me.save = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.count = New System.Windows.Forms.ToolStripButton()
        Me.grid = New System.Windows.Forms.ToolStripButton()
        Me.pic = New System.Windows.Forms.ToolStripButton()
        Me.rep = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.big = New System.Windows.Forms.ToolStripButton()
        Me.small = New System.Windows.Forms.ToolStripButton()
        Me.refush = New System.Windows.Forms.ToolStripButton()
        Me.help = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.TextBox2 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.clos = New System.Windows.Forms.ToolStripButton()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.DGV = New System.Windows.Forms.DataGridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.MenuStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.文件FToolStripMenuItem, Me.计算, Me.查看CToolStripMenuItem, Me.刷新ZToolStripMenuItem, Me.帮助HToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1012, 30)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        '文件FToolStripMenuItem
        '
        Me.文件FToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.打开数据文件, Me.ToolStripSeparator1, Me.保存报告, Me.保存图形, Me.输出DXF文件, Me.ToolStripSeparator2, Me.退出})
        Me.文件FToolStripMenuItem.Image = CType(resources.GetObject("文件FToolStripMenuItem.Image"), System.Drawing.Image)
        Me.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem"
        Me.文件FToolStripMenuItem.Size = New System.Drawing.Size(125, 26)
        Me.文件FToolStripMenuItem.Text = "文件（&F）"
        '
        '打开数据文件
        '
        Me.打开数据文件.Image = CType(resources.GetObject("打开数据文件.Image"), System.Drawing.Image)
        Me.打开数据文件.Name = "打开数据文件"
        Me.打开数据文件.Size = New System.Drawing.Size(244, 26)
        Me.打开数据文件.Text = "打开数据文件（&O）"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(241, 6)
        '
        '保存报告
        '
        Me.保存报告.Image = CType(resources.GetObject("保存报告.Image"), System.Drawing.Image)
        Me.保存报告.Name = "保存报告"
        Me.保存报告.Size = New System.Drawing.Size(244, 26)
        Me.保存报告.Text = "保存报告（&I）"
        '
        '保存图形
        '
        Me.保存图形.Image = CType(resources.GetObject("保存图形.Image"), System.Drawing.Image)
        Me.保存图形.Name = "保存图形"
        Me.保存图形.Size = New System.Drawing.Size(244, 26)
        Me.保存图形.Text = "保存图形（&P）"
        '
        '输出DXF文件
        '
        Me.输出DXF文件.Image = CType(resources.GetObject("输出DXF文件.Image"), System.Drawing.Image)
        Me.输出DXF文件.Name = "输出DXF文件"
        Me.输出DXF文件.Size = New System.Drawing.Size(244, 26)
        Me.输出DXF文件.Text = "输出DXF文件（&D）"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(241, 6)
        '
        '退出
        '
        Me.退出.Image = CType(resources.GetObject("退出.Image"), System.Drawing.Image)
        Me.退出.Name = "退出"
        Me.退出.Size = New System.Drawing.Size(244, 26)
        Me.退出.Text = "退出（&E）"
        '
        '计算
        '
        Me.计算.Image = CType(resources.GetObject("计算.Image"), System.Drawing.Image)
        Me.计算.Name = "计算"
        Me.计算.Size = New System.Drawing.Size(127, 26)
        Me.计算.Text = "计算（&C）"
        '
        '查看CToolStripMenuItem
        '
        Me.查看CToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.示意图, Me.报告})
        Me.查看CToolStripMenuItem.Image = CType(resources.GetObject("查看CToolStripMenuItem.Image"), System.Drawing.Image)
        Me.查看CToolStripMenuItem.Name = "查看CToolStripMenuItem"
        Me.查看CToolStripMenuItem.Size = New System.Drawing.Size(126, 26)
        Me.查看CToolStripMenuItem.Text = "查看（&S）"
        '
        '示意图
        '
        Me.示意图.Image = CType(resources.GetObject("示意图.Image"), System.Drawing.Image)
        Me.示意图.Name = "示意图"
        Me.示意图.Size = New System.Drawing.Size(140, 26)
        Me.示意图.Text = "示意图"
        '
        '报告
        '
        Me.报告.Image = CType(resources.GetObject("报告.Image"), System.Drawing.Image)
        Me.报告.Name = "报告"
        Me.报告.Size = New System.Drawing.Size(140, 26)
        Me.报告.Text = "报告"
        '
        '刷新ZToolStripMenuItem
        '
        Me.刷新ZToolStripMenuItem.Image = CType(resources.GetObject("刷新ZToolStripMenuItem.Image"), System.Drawing.Image)
        Me.刷新ZToolStripMenuItem.Name = "刷新ZToolStripMenuItem"
        Me.刷新ZToolStripMenuItem.Size = New System.Drawing.Size(125, 26)
        Me.刷新ZToolStripMenuItem.Text = "刷新（&Z）"
        '
        '帮助HToolStripMenuItem
        '
        Me.帮助HToolStripMenuItem.Image = CType(resources.GetObject("帮助HToolStripMenuItem.Image"), System.Drawing.Image)
        Me.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem"
        Me.帮助HToolStripMenuItem.Size = New System.Drawing.Size(127, 26)
        Me.帮助HToolStripMenuItem.Text = "帮助（&H）"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.open, Me.save, Me.ToolStripSeparator3, Me.count, Me.grid, Me.pic, Me.rep, Me.ToolStripSeparator4, Me.big, Me.small, Me.refush, Me.help, Me.ToolStripSeparator5, Me.ToolStripLabel1, Me.TextBox2, Me.ToolStripSeparator6, Me.clos})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 30)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1012, 29)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'open
        '
        Me.open.Image = CType(resources.GetObject("open.Image"), System.Drawing.Image)
        Me.open.Name = "open"
        Me.open.Size = New System.Drawing.Size(70, 26)
        Me.open.Text = "打开"
        '
        'save
        '
        Me.save.Image = CType(resources.GetObject("save.Image"), System.Drawing.Image)
        Me.save.Name = "save"
        Me.save.Size = New System.Drawing.Size(70, 26)
        Me.save.Text = "保存"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 29)
        '
        'count
        '
        Me.count.Image = CType(resources.GetObject("count.Image"), System.Drawing.Image)
        Me.count.Name = "count"
        Me.count.Size = New System.Drawing.Size(70, 26)
        Me.count.Text = "计算"
        '
        'grid
        '
        Me.grid.Image = CType(resources.GetObject("grid.Image"), System.Drawing.Image)
        Me.grid.Name = "grid"
        Me.grid.Size = New System.Drawing.Size(70, 26)
        Me.grid.Text = "表格"
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(70, 26)
        Me.pic.Text = "图形"
        '
        'rep
        '
        Me.rep.Image = CType(resources.GetObject("rep.Image"), System.Drawing.Image)
        Me.rep.Name = "rep"
        Me.rep.Size = New System.Drawing.Size(70, 26)
        Me.rep.Text = "报告"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 29)
        '
        'big
        '
        Me.big.Image = CType(resources.GetObject("big.Image"), System.Drawing.Image)
        Me.big.Name = "big"
        Me.big.Size = New System.Drawing.Size(70, 26)
        Me.big.Text = "放大"
        '
        'small
        '
        Me.small.Image = CType(resources.GetObject("small.Image"), System.Drawing.Image)
        Me.small.Name = "small"
        Me.small.Size = New System.Drawing.Size(70, 26)
        Me.small.Text = "缩小"
        '
        'refush
        '
        Me.refush.Image = CType(resources.GetObject("refush.Image"), System.Drawing.Image)
        Me.refush.Name = "refush"
        Me.refush.Size = New System.Drawing.Size(70, 26)
        Me.refush.Text = "刷新"
        '
        'help
        '
        Me.help.Image = CType(resources.GetObject("help.Image"), System.Drawing.Image)
        Me.help.Name = "help"
        Me.help.Size = New System.Drawing.Size(70, 26)
        Me.help.Text = "帮助"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 29)
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(82, 26)
        Me.ToolStripLabel1.Text = "定桩间隔"
        '
        'TextBox2
        '
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(80, 29)
        Me.TextBox2.Text = "10"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 29)
        '
        'clos
        '
        Me.clos.Image = CType(resources.GetObject("clos.Image"), System.Drawing.Image)
        Me.clos.Name = "clos"
        Me.clos.Size = New System.Drawing.Size(70, 26)
        Me.clos.Text = "关闭"
        '
        'TabControl1
        '
        Me.TabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.ImageList = Me.ImageList1
        Me.TabControl1.Location = New System.Drawing.Point(0, 56)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(989, 402)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.DGV)
        Me.TabPage1.ImageIndex = 4
        Me.TabPage1.Location = New System.Drawing.Point(4, 4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(981, 371)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "表格"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'DGV
        '
        Me.DGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGV.Location = New System.Drawing.Point(3, 3)
        Me.DGV.Name = "DGV"
        Me.DGV.RowHeadersVisible = False
        Me.DGV.RowTemplate.Height = 23
        Me.DGV.Size = New System.Drawing.Size(975, 365)
        Me.DGV.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Chart1)
        Me.TabPage2.ImageIndex = 7
        Me.TabPage2.Location = New System.Drawing.Point(4, 4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(981, 371)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "图形"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Chart1
        '
        ChartArea2.Name = "ChartArea1"
        ChartArea2.Position.Auto = False
        ChartArea2.Position.Height = 100.0!
        ChartArea2.Position.Width = 32.0!
        ChartArea2.Position.X = 3.0!
        Me.Chart1.ChartAreas.Add(ChartArea2)
        Legend2.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend2)
        Me.Chart1.Location = New System.Drawing.Point(159, 9)
        Me.Chart1.Name = "Chart1"
        Series2.ChartArea = "ChartArea1"
        Series2.Legend = "Legend1"
        Series2.Name = "Series1"
        Me.Chart1.Series.Add(Series2)
        Me.Chart1.Size = New System.Drawing.Size(400, 400)
        Me.Chart1.TabIndex = 0
        Me.Chart1.Text = "Chart1"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.TextBox1)
        Me.TabPage3.ImageKey = "报告.png"
        Me.TabPage3.Location = New System.Drawing.Point(4, 4)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(981, 371)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "报告"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox1.Location = New System.Drawing.Point(3, 3)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox1.Size = New System.Drawing.Size(975, 365)
        Me.TextBox1.TabIndex = 0
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "DXF1.png")
        Me.ImageList1.Images.SetKeyName(1, "帮助.png")
        Me.ImageList1.Images.SetKeyName(2, "保存.png")
        Me.ImageList1.Images.SetKeyName(3, "报告.png")
        Me.ImageList1.Images.SetKeyName(4, "表格.png")
        Me.ImageList1.Images.SetKeyName(5, "查看.png")
        Me.ImageList1.Images.SetKeyName(6, "打开文件.png")
        Me.ImageList1.Images.SetKeyName(7, "道路.png")
        Me.ImageList1.Images.SetKeyName(8, "放大.png")
        Me.ImageList1.Images.SetKeyName(9, "关闭.png")
        Me.ImageList1.Images.SetKeyName(10, "计算1.png")
        Me.ImageList1.Images.SetKeyName(11, "批处理.png")
        Me.ImageList1.Images.SetKeyName(12, "数据 (1).png")
        Me.ImageList1.Images.SetKeyName(13, "数据.png")
        Me.ImageList1.Images.SetKeyName(14, "缩小.png")
        Me.ImageList1.Images.SetKeyName(15, "图片.png")
        Me.ImageList1.Images.SetKeyName(16, "图形.png")
        Me.ImageList1.Images.SetKeyName(17, "文件.png")
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1012, 471)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("黑体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "道路曲线要素计算V1.0"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents 文件FToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 打开数据文件 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents 保存报告 As ToolStripMenuItem
    Friend WithEvents 保存图形 As ToolStripMenuItem
    Friend WithEvents 输出DXF文件 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents 退出 As ToolStripMenuItem
    Friend WithEvents 计算 As ToolStripMenuItem
    Friend WithEvents 查看CToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 示意图 As ToolStripMenuItem
    Friend WithEvents 报告 As ToolStripMenuItem
    Friend WithEvents 帮助HToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DGV As DataGridView
    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents 刷新ZToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents open As ToolStripButton
    Friend WithEvents save As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents count As ToolStripButton
    Friend WithEvents grid As ToolStripButton
    Friend WithEvents pic As ToolStripButton
    Friend WithEvents rep As ToolStripButton
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents big As ToolStripButton
    Friend WithEvents small As ToolStripButton
    Friend WithEvents refush As ToolStripButton
    Friend WithEvents help As ToolStripButton
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents clos As ToolStripButton
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents TextBox2 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
End Class
