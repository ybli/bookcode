<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_Calcu
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_Calcu))
        Me.TextAX = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextAY = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBX = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBY = New System.Windows.Forms.TextBox()
        Me.TextCX = New System.Windows.Forms.TextBox()
        Me.TextCY = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GrpBxKwPt = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Textα = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.项目ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.前方交会ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.后方交会ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.距离交会ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.数据文件ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KnowDataMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SurvDataMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.操作ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CalcuMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OutputMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DrawMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EndMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GrpBxAngl = New System.Windows.Forms.GroupBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Textγ = New System.Windows.Forms.TextBox()
        Me.Textβ = New System.Windows.Forms.TextBox()
        Me.TextDsa = New System.Windows.Forms.TextBox()
        Me.TextDsb = New System.Windows.Forms.TextBox()
        Me.OutTxtBx = New System.Windows.Forms.TextBox()
        Me.GrpBxDist = New System.Windows.Forms.GroupBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.IputDataRadioBtn = New System.Windows.Forms.RadioButton()
        Me.FileDataRadioBtn = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GrpBxKwPt.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.GrpBxAngl.SuspendLayout()
        Me.GrpBxDist.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextAX
        '
        Me.TextAX.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextAX.Location = New System.Drawing.Point(75, 59)
        Me.TextAX.Name = "TextAX"
        Me.TextAX.Size = New System.Drawing.Size(121, 28)
        Me.TextAX.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(122, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(24, 24)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "X"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(265, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(22, 24)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Y"
        '
        'TextAY
        '
        Me.TextAY.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextAY.Location = New System.Drawing.Point(218, 59)
        Me.TextAY.Name = "TextAY"
        Me.TextAY.Size = New System.Drawing.Size(121, 28)
        Me.TextAY.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 24)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "PA："
        '
        'TextBX
        '
        Me.TextBX.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBX.Location = New System.Drawing.Point(75, 98)
        Me.TextBX.Name = "TextBX"
        Me.TextBX.Size = New System.Drawing.Size(121, 28)
        Me.TextBX.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 101)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 24)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "PB："
        '
        'TextBY
        '
        Me.TextBY.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBY.Location = New System.Drawing.Point(218, 98)
        Me.TextBY.Name = "TextBY"
        Me.TextBY.Size = New System.Drawing.Size(121, 28)
        Me.TextBY.TabIndex = 6
        '
        'TextCX
        '
        Me.TextCX.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextCX.Location = New System.Drawing.Point(75, 135)
        Me.TextCX.Name = "TextCX"
        Me.TextCX.Size = New System.Drawing.Size(121, 28)
        Me.TextCX.TabIndex = 9
        '
        'TextCY
        '
        Me.TextCY.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextCY.Location = New System.Drawing.Point(218, 136)
        Me.TextCY.Name = "TextCY"
        Me.TextCY.Size = New System.Drawing.Size(121, 28)
        Me.TextCY.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Times New Roman", 15.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(16, 56)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 30)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "α："
        '
        'GrpBxKwPt
        '
        Me.GrpBxKwPt.Controls.Add(Me.Label7)
        Me.GrpBxKwPt.Controls.Add(Me.TextCY)
        Me.GrpBxKwPt.Controls.Add(Me.TextCX)
        Me.GrpBxKwPt.Controls.Add(Me.TextAX)
        Me.GrpBxKwPt.Controls.Add(Me.TextAY)
        Me.GrpBxKwPt.Controls.Add(Me.TextBX)
        Me.GrpBxKwPt.Controls.Add(Me.Label4)
        Me.GrpBxKwPt.Controls.Add(Me.Label2)
        Me.GrpBxKwPt.Controls.Add(Me.TextBY)
        Me.GrpBxKwPt.Controls.Add(Me.Label3)
        Me.GrpBxKwPt.Controls.Add(Me.Label1)
        Me.GrpBxKwPt.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpBxKwPt.Location = New System.Drawing.Point(232, 47)
        Me.GrpBxKwPt.Name = "GrpBxKwPt"
        Me.GrpBxKwPt.Size = New System.Drawing.Size(353, 179)
        Me.GrpBxKwPt.TabIndex = 13
        Me.GrpBxKwPt.TabStop = False
        Me.GrpBxKwPt.Text = "已知点数据"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(15, 136)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(54, 24)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "PC："
        '
        'PictureBox1
        '
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(232, 239)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(350, 280)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 15
        Me.PictureBox1.TabStop = False
        '
        'Textα
        '
        Me.Textα.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Textα.Location = New System.Drawing.Point(76, 59)
        Me.Textα.Name = "Textα"
        Me.Textα.Size = New System.Drawing.Size(121, 28)
        Me.Textα.TabIndex = 16
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(93, 539)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(187, 36)
        Me.Button1.TabIndex = 17
        Me.Button1.Text = "计  算"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.项目ToolStripMenuItem, Me.数据文件ToolStripMenuItem, Me.操作ToolStripMenuItem, Me.EndMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1150, 28)
        Me.MenuStrip1.TabIndex = 18
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        '项目ToolStripMenuItem
        '
        Me.项目ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.前方交会ToolStripMenuItem, Me.后方交会ToolStripMenuItem, Me.距离交会ToolStripMenuItem})
        Me.项目ToolStripMenuItem.Name = "项目ToolStripMenuItem"
        Me.项目ToolStripMenuItem.Size = New System.Drawing.Size(51, 24)
        Me.项目ToolStripMenuItem.Text = "项目"
        '
        '前方交会ToolStripMenuItem
        '
        Me.前方交会ToolStripMenuItem.Name = "前方交会ToolStripMenuItem"
        Me.前方交会ToolStripMenuItem.Size = New System.Drawing.Size(144, 26)
        Me.前方交会ToolStripMenuItem.Text = "前方交会"
        '
        '后方交会ToolStripMenuItem
        '
        Me.后方交会ToolStripMenuItem.Name = "后方交会ToolStripMenuItem"
        Me.后方交会ToolStripMenuItem.Size = New System.Drawing.Size(144, 26)
        Me.后方交会ToolStripMenuItem.Text = "后方交会"
        '
        '距离交会ToolStripMenuItem
        '
        Me.距离交会ToolStripMenuItem.Name = "距离交会ToolStripMenuItem"
        Me.距离交会ToolStripMenuItem.Size = New System.Drawing.Size(144, 26)
        Me.距离交会ToolStripMenuItem.Text = "距离交会"
        '
        '数据文件ToolStripMenuItem
        '
        Me.数据文件ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.KnowDataMenuItem, Me.SurvDataMenuItem})
        Me.数据文件ToolStripMenuItem.Name = "数据文件ToolStripMenuItem"
        Me.数据文件ToolStripMenuItem.Size = New System.Drawing.Size(51, 24)
        Me.数据文件ToolStripMenuItem.Text = "文件"
        '
        'KnowDataMenuItem
        '
        Me.KnowDataMenuItem.Name = "KnowDataMenuItem"
        Me.KnowDataMenuItem.Size = New System.Drawing.Size(174, 26)
        Me.KnowDataMenuItem.Text = "导入已知数据"
        '
        'SurvDataMenuItem
        '
        Me.SurvDataMenuItem.Name = "SurvDataMenuItem"
        Me.SurvDataMenuItem.Size = New System.Drawing.Size(174, 26)
        Me.SurvDataMenuItem.Text = "导入观测数据"
        '
        '操作ToolStripMenuItem
        '
        Me.操作ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CalcuMenuItem, Me.OutputMenuItem, Me.DrawMenuItem})
        Me.操作ToolStripMenuItem.Name = "操作ToolStripMenuItem"
        Me.操作ToolStripMenuItem.Size = New System.Drawing.Size(51, 24)
        Me.操作ToolStripMenuItem.Text = "操作"
        '
        'CalcuMenuItem
        '
        Me.CalcuMenuItem.Name = "CalcuMenuItem"
        Me.CalcuMenuItem.Size = New System.Drawing.Size(144, 26)
        Me.CalcuMenuItem.Text = "执行计算"
        '
        'OutputMenuItem
        '
        Me.OutputMenuItem.Name = "OutputMenuItem"
        Me.OutputMenuItem.Size = New System.Drawing.Size(144, 26)
        Me.OutputMenuItem.Text = "输出结果"
        '
        'DrawMenuItem
        '
        Me.DrawMenuItem.Name = "DrawMenuItem"
        Me.DrawMenuItem.Size = New System.Drawing.Size(144, 26)
        Me.DrawMenuItem.Text = "绘图"
        '
        'EndMenuItem
        '
        Me.EndMenuItem.Name = "EndMenuItem"
        Me.EndMenuItem.Size = New System.Drawing.Size(51, 24)
        Me.EndMenuItem.Text = "退出"
        '
        'GrpBxAngl
        '
        Me.GrpBxAngl.Controls.Add(Me.Label14)
        Me.GrpBxAngl.Controls.Add(Me.Label13)
        Me.GrpBxAngl.Controls.Add(Me.Label12)
        Me.GrpBxAngl.Controls.Add(Me.Textα)
        Me.GrpBxAngl.Controls.Add(Me.Label6)
        Me.GrpBxAngl.Controls.Add(Me.Textγ)
        Me.GrpBxAngl.Controls.Add(Me.Textβ)
        Me.GrpBxAngl.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpBxAngl.Location = New System.Drawing.Point(18, 191)
        Me.GrpBxAngl.Name = "GrpBxAngl"
        Me.GrpBxAngl.Size = New System.Drawing.Size(208, 179)
        Me.GrpBxAngl.TabIndex = 20
        Me.GrpBxAngl.TabStop = False
        Me.GrpBxAngl.Text = "观测角数据"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Times New Roman", 15.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(20, 130)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(50, 30)
        Me.Label14.TabIndex = 22
        Me.Label14.Text = "γ："
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Times New Roman", 15.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(17, 95)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(53, 30)
        Me.Label13.TabIndex = 21
        Me.Label13.Text = "β："
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(75, 34)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(78, 24)
        Me.Label12.TabIndex = 1
        Me.Label12.Text = "D.mmss"
        '
        'Textγ
        '
        Me.Textγ.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Textγ.Location = New System.Drawing.Point(75, 135)
        Me.Textγ.Name = "Textγ"
        Me.Textγ.Size = New System.Drawing.Size(121, 28)
        Me.Textγ.TabIndex = 9
        '
        'Textβ
        '
        Me.Textβ.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Textβ.Location = New System.Drawing.Point(76, 98)
        Me.Textβ.Name = "Textβ"
        Me.Textβ.Size = New System.Drawing.Size(121, 28)
        Me.Textβ.TabIndex = 11
        '
        'TextDsa
        '
        Me.TextDsa.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextDsa.Location = New System.Drawing.Point(76, 55)
        Me.TextDsa.Name = "TextDsa"
        Me.TextDsa.Size = New System.Drawing.Size(120, 28)
        Me.TextDsa.TabIndex = 0
        '
        'TextDsb
        '
        Me.TextDsb.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextDsb.Location = New System.Drawing.Point(75, 94)
        Me.TextDsb.Name = "TextDsb"
        Me.TextDsb.Size = New System.Drawing.Size(121, 28)
        Me.TextDsb.TabIndex = 2
        '
        'OutTxtBx
        '
        Me.OutTxtBx.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OutTxtBx.Location = New System.Drawing.Point(588, 47)
        Me.OutTxtBx.Multiline = True
        Me.OutTxtBx.Name = "OutTxtBx"
        Me.OutTxtBx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.OutTxtBx.Size = New System.Drawing.Size(547, 541)
        Me.OutTxtBx.TabIndex = 4
        '
        'GrpBxDist
        '
        Me.GrpBxDist.Controls.Add(Me.Label16)
        Me.GrpBxDist.Controls.Add(Me.Label17)
        Me.GrpBxDist.Controls.Add(Me.Label18)
        Me.GrpBxDist.Controls.Add(Me.TextDsa)
        Me.GrpBxDist.Controls.Add(Me.TextDsb)
        Me.GrpBxDist.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpBxDist.Location = New System.Drawing.Point(18, 381)
        Me.GrpBxDist.Name = "GrpBxDist"
        Me.GrpBxDist.Size = New System.Drawing.Size(208, 138)
        Me.GrpBxDist.TabIndex = 21
        Me.GrpBxDist.TabStop = False
        Me.GrpBxDist.Text = "观测边数据"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Times New Roman", 15.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(22, 91)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(53, 30)
        Me.Label16.TabIndex = 21
        Me.Label16.Text = "b："
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(75, 29)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(26, 24)
        Me.Label17.TabIndex = 1
        Me.Label17.Text = "m"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Times New Roman", 15.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(22, 52)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(53, 30)
        Me.Label18.TabIndex = 12
        Me.Label18.Text = "a："
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(332, 539)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(187, 36)
        Me.Button2.TabIndex = 22
        Me.Button2.Text = "清  空"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'IputDataRadioBtn
        '
        Me.IputDataRadioBtn.AutoSize = True
        Me.IputDataRadioBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IputDataRadioBtn.Location = New System.Drawing.Point(27, 78)
        Me.IputDataRadioBtn.Name = "IputDataRadioBtn"
        Me.IputDataRadioBtn.Size = New System.Drawing.Size(153, 29)
        Me.IputDataRadioBtn.TabIndex = 23
        Me.IputDataRadioBtn.TabStop = True
        Me.IputDataRadioBtn.Text = "手工输入数据"
        Me.IputDataRadioBtn.UseVisualStyleBackColor = True
        '
        'FileDataRadioBtn
        '
        Me.FileDataRadioBtn.AutoSize = True
        Me.FileDataRadioBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FileDataRadioBtn.Location = New System.Drawing.Point(27, 43)
        Me.FileDataRadioBtn.Name = "FileDataRadioBtn"
        Me.FileDataRadioBtn.Size = New System.Drawing.Size(153, 29)
        Me.FileDataRadioBtn.TabIndex = 24
        Me.FileDataRadioBtn.TabStop = True
        Me.FileDataRadioBtn.Text = "文件导入数据"
        Me.FileDataRadioBtn.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.FileDataRadioBtn)
        Me.GroupBox1.Controls.Add(Me.IputDataRadioBtn)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(18, 47)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(208, 129)
        Me.GroupBox1.TabIndex = 25
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "数据来源"
        '
        'Form_Calcu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1150, 602)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.GrpBxDist)
        Me.Controls.Add(Me.GrpBxAngl)
        Me.Controls.Add(Me.OutTxtBx)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.GrpBxKwPt)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form_Calcu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.GrpBxKwPt.ResumeLayout(False)
        Me.GrpBxKwPt.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GrpBxAngl.ResumeLayout(False)
        Me.GrpBxAngl.PerformLayout()
        Me.GrpBxDist.ResumeLayout(False)
        Me.GrpBxDist.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextAX As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextAY As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBX As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBY As System.Windows.Forms.TextBox
    Friend WithEvents TextCX As System.Windows.Forms.TextBox
    Friend WithEvents TextCY As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GrpBxKwPt As System.Windows.Forms.GroupBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Textα As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents 项目ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 前方交会ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 后方交会ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 距离交会ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 数据文件ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents KnowDataMenuItem As ToolStripMenuItem
    Friend WithEvents SurvDataMenuItem As ToolStripMenuItem
    Friend WithEvents EndMenuItem As ToolStripMenuItem
    Friend WithEvents Label7 As Label
    Friend WithEvents GrpBxAngl As GroupBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Textγ As TextBox
    Friend WithEvents Textβ As TextBox
    Friend WithEvents TextDsa As TextBox
    Friend WithEvents TextDsb As TextBox
    Friend WithEvents OutTxtBx As TextBox
    Friend WithEvents GrpBxDist As GroupBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents IputDataRadioBtn As RadioButton
    Friend WithEvents 操作ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CalcuMenuItem As ToolStripMenuItem
    Friend WithEvents OutputMenuItem As ToolStripMenuItem
    Friend WithEvents FileDataRadioBtn As RadioButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents DrawMenuItem As ToolStripMenuItem
End Class
