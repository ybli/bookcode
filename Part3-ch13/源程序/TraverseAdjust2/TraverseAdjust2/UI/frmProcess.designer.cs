namespace TraverseAdjust
{
    partial class frmProcess
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProcess));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbInput = new System.Windows.Forms.GroupBox();
            this.gbParam = new System.Windows.Forms.GroupBox();
            this.chkLeftAngle = new System.Windows.Forms.CheckBox();
            this.txtUnknowPtNum = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.chkIsConnecting = new System.Windows.Forms.CheckBox();
            this.btMannul = new System.Windows.Forms.Button();
            this.bgKnowPtInfo = new System.Windows.Forms.GroupBox();
            this.gbPt4 = new System.Windows.Forms.GroupBox();
            this.txtP4Name = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.txtX4 = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.txtY4 = new System.Windows.Forms.TextBox();
            this.gbPt3 = new System.Windows.Forms.GroupBox();
            this.txtP3Name = new System.Windows.Forms.TextBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.txtX3 = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.txtY3 = new System.Windows.Forms.TextBox();
            this.gbPt2 = new System.Windows.Forms.GroupBox();
            this.txtP2Name = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.txtX2 = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.txtY2 = new System.Windows.Forms.TextBox();
            this.gbPt1 = new System.Windows.Forms.GroupBox();
            this.txtP1Name = new System.Windows.Forms.TextBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.txtX1 = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtY1 = new System.Windows.Forms.TextBox();
            this.gbObsData = new System.Windows.Forms.GroupBox();
            this.gridObsData = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Angle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Distance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbOutput = new System.Windows.Forms.GroupBox();
            this.rDXF = new System.Windows.Forms.RadioButton();
            this.rXLS = new System.Windows.Forms.RadioButton();
            this.rTXT = new System.Windows.Forms.RadioButton();
            this.rMap = new System.Windows.Forms.RadioButton();
            this.btOutput = new System.Windows.Forms.Button();
            this.chkIsFileInput = new System.Windows.Forms.CheckBox();
            this.btFileInput = new System.Windows.Forms.Button();
            this.gbResult = new System.Windows.Forms.GroupBox();
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.btCalc = new System.Windows.Forms.Button();
            this.gbInput.SuspendLayout();
            this.gbParam.SuspendLayout();
            this.bgKnowPtInfo.SuspendLayout();
            this.gbPt4.SuspendLayout();
            this.gbPt3.SuspendLayout();
            this.gbPt2.SuspendLayout();
            this.gbPt1.SuspendLayout();
            this.gbObsData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridObsData)).BeginInit();
            this.gbOutput.SuspendLayout();
            this.gbResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbInput
            // 
            this.gbInput.Controls.Add(this.gbParam);
            this.gbInput.Controls.Add(this.btMannul);
            this.gbInput.Controls.Add(this.bgKnowPtInfo);
            this.gbInput.Controls.Add(this.gbObsData);
            this.gbInput.Location = new System.Drawing.Point(12, 40);
            this.gbInput.Name = "gbInput";
            this.gbInput.Size = new System.Drawing.Size(709, 426);
            this.gbInput.TabIndex = 9;
            this.gbInput.TabStop = false;
            this.gbInput.Text = "输入数据";
            // 
            // gbParam
            // 
            this.gbParam.Controls.Add(this.chkLeftAngle);
            this.gbParam.Controls.Add(this.txtUnknowPtNum);
            this.gbParam.Controls.Add(this.Label1);
            this.gbParam.Controls.Add(this.chkIsConnecting);
            this.gbParam.Location = new System.Drawing.Point(10, 20);
            this.gbParam.Name = "gbParam";
            this.gbParam.Size = new System.Drawing.Size(252, 70);
            this.gbParam.TabIndex = 4;
            this.gbParam.TabStop = false;
            this.gbParam.Text = "参数设置";
            // 
            // chkLeftAngle
            // 
            this.chkLeftAngle.AutoSize = true;
            this.chkLeftAngle.Checked = true;
            this.chkLeftAngle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLeftAngle.Location = new System.Drawing.Point(74, 32);
            this.chkLeftAngle.Name = "chkLeftAngle";
            this.chkLeftAngle.Size = new System.Drawing.Size(48, 16);
            this.chkLeftAngle.TabIndex = 42;
            this.chkLeftAngle.Text = "左角";
            this.chkLeftAngle.UseVisualStyleBackColor = true;
            // 
            // txtUnknowPtNum
            // 
            this.txtUnknowPtNum.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUnknowPtNum.ForeColor = System.Drawing.Color.Blue;
            this.txtUnknowPtNum.Location = new System.Drawing.Point(192, 29);
            this.txtUnknowPtNum.Name = "txtUnknowPtNum";
            this.txtUnknowPtNum.Size = new System.Drawing.Size(54, 21);
            this.txtUnknowPtNum.TabIndex = 40;
            this.txtUnknowPtNum.Text = "4";
            this.txtUnknowPtNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(133, 33);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(53, 12);
            this.Label1.TabIndex = 39;
            this.Label1.Text = "未知点数";
            // 
            // chkIsConnecting
            // 
            this.chkIsConnecting.AutoSize = true;
            this.chkIsConnecting.Checked = true;
            this.chkIsConnecting.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsConnecting.Location = new System.Drawing.Point(16, 32);
            this.chkIsConnecting.Name = "chkIsConnecting";
            this.chkIsConnecting.Size = new System.Drawing.Size(48, 16);
            this.chkIsConnecting.TabIndex = 38;
            this.chkIsConnecting.Text = "附和";
            this.chkIsConnecting.UseVisualStyleBackColor = true;
            this.chkIsConnecting.CheckedChanged += new System.EventHandler(this.chkIsConnecting_CheckedChanged);
            // 
            // btMannul
            // 
            this.btMannul.Image = ((System.Drawing.Image)(resources.GetObject("btMannul.Image")));
            this.btMannul.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btMannul.Location = new System.Drawing.Point(269, 47);
            this.btMannul.Name = "btMannul";
            this.btMannul.Size = new System.Drawing.Size(95, 24);
            this.btMannul.TabIndex = 41;
            this.btMannul.Text = "手工输入(&M)";
            this.btMannul.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btMannul.UseVisualStyleBackColor = true;
            this.btMannul.Click += new System.EventHandler(this.btMannul_Click);
            // 
            // bgKnowPtInfo
            // 
            this.bgKnowPtInfo.Controls.Add(this.gbPt4);
            this.bgKnowPtInfo.Controls.Add(this.gbPt3);
            this.bgKnowPtInfo.Controls.Add(this.gbPt2);
            this.bgKnowPtInfo.Controls.Add(this.gbPt1);
            this.bgKnowPtInfo.Location = new System.Drawing.Point(10, 103);
            this.bgKnowPtInfo.Name = "bgKnowPtInfo";
            this.bgKnowPtInfo.Size = new System.Drawing.Size(357, 312);
            this.bgKnowPtInfo.TabIndex = 5;
            this.bgKnowPtInfo.TabStop = false;
            this.bgKnowPtInfo.Text = "已知点坐标";
            // 
            // gbPt4
            // 
            this.gbPt4.Controls.Add(this.txtP4Name);
            this.gbPt4.Controls.Add(this.Label7);
            this.gbPt4.Controls.Add(this.txtX4);
            this.gbPt4.Controls.Add(this.Label8);
            this.gbPt4.Controls.Add(this.Label9);
            this.gbPt4.Controls.Add(this.txtY4);
            this.gbPt4.Location = new System.Drawing.Point(181, 162);
            this.gbPt4.Name = "gbPt4";
            this.gbPt4.Size = new System.Drawing.Size(162, 139);
            this.gbPt4.TabIndex = 9;
            this.gbPt4.TabStop = false;
            this.gbPt4.Text = "点4";
            // 
            // txtP4Name
            // 
            this.txtP4Name.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtP4Name.Location = new System.Drawing.Point(34, 24);
            this.txtP4Name.Name = "txtP4Name";
            this.txtP4Name.Size = new System.Drawing.Size(122, 21);
            this.txtP4Name.TabIndex = 1;
            this.txtP4Name.Text = "D";
            this.txtP4Name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(3, 30);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(35, 12);
            this.Label7.TabIndex = 8;
            this.Label7.Text = "点名:";
            // 
            // txtX4
            // 
            this.txtX4.Location = new System.Drawing.Point(34, 65);
            this.txtX4.Name = "txtX4";
            this.txtX4.Size = new System.Drawing.Size(122, 21);
            this.txtX4.TabIndex = 2;
            this.txtX4.Text = "0.000";
            this.txtX4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(7, 70);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(23, 12);
            this.Label8.TabIndex = 1;
            this.Label8.Text = "X4=";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(7, 102);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(23, 12);
            this.Label9.TabIndex = 3;
            this.Label9.Text = "Y4=";
            // 
            // txtY4
            // 
            this.txtY4.Location = new System.Drawing.Point(34, 98);
            this.txtY4.Name = "txtY4";
            this.txtY4.Size = new System.Drawing.Size(122, 21);
            this.txtY4.TabIndex = 3;
            this.txtY4.Text = "0.000";
            this.txtY4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // gbPt3
            // 
            this.gbPt3.Controls.Add(this.txtP3Name);
            this.gbPt3.Controls.Add(this.Label10);
            this.gbPt3.Controls.Add(this.txtX3);
            this.gbPt3.Controls.Add(this.Label11);
            this.gbPt3.Controls.Add(this.Label12);
            this.gbPt3.Controls.Add(this.txtY3);
            this.gbPt3.Location = new System.Drawing.Point(13, 162);
            this.gbPt3.Name = "gbPt3";
            this.gbPt3.Size = new System.Drawing.Size(162, 139);
            this.gbPt3.TabIndex = 8;
            this.gbPt3.TabStop = false;
            this.gbPt3.Text = "点3";
            // 
            // txtP3Name
            // 
            this.txtP3Name.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtP3Name.Location = new System.Drawing.Point(34, 24);
            this.txtP3Name.Name = "txtP3Name";
            this.txtP3Name.Size = new System.Drawing.Size(122, 21);
            this.txtP3Name.TabIndex = 1;
            this.txtP3Name.Text = "C";
            this.txtP3Name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(3, 30);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(35, 12);
            this.Label10.TabIndex = 8;
            this.Label10.Text = "点名:";
            // 
            // txtX3
            // 
            this.txtX3.Location = new System.Drawing.Point(34, 65);
            this.txtX3.Name = "txtX3";
            this.txtX3.Size = new System.Drawing.Size(122, 21);
            this.txtX3.TabIndex = 2;
            this.txtX3.Text = "0.000";
            this.txtX3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(7, 70);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(23, 12);
            this.Label11.TabIndex = 1;
            this.Label11.Text = "X3=";
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(7, 102);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(23, 12);
            this.Label12.TabIndex = 3;
            this.Label12.Text = "Y3=";
            // 
            // txtY3
            // 
            this.txtY3.Location = new System.Drawing.Point(34, 98);
            this.txtY3.Name = "txtY3";
            this.txtY3.Size = new System.Drawing.Size(122, 21);
            this.txtY3.TabIndex = 3;
            this.txtY3.Text = "0.000";
            this.txtY3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // gbPt2
            // 
            this.gbPt2.Controls.Add(this.txtP2Name);
            this.gbPt2.Controls.Add(this.Label4);
            this.gbPt2.Controls.Add(this.txtX2);
            this.gbPt2.Controls.Add(this.Label5);
            this.gbPt2.Controls.Add(this.Label6);
            this.gbPt2.Controls.Add(this.txtY2);
            this.gbPt2.Location = new System.Drawing.Point(181, 17);
            this.gbPt2.Name = "gbPt2";
            this.gbPt2.Size = new System.Drawing.Size(162, 139);
            this.gbPt2.TabIndex = 7;
            this.gbPt2.TabStop = false;
            this.gbPt2.Text = "点2";
            // 
            // txtP2Name
            // 
            this.txtP2Name.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtP2Name.Location = new System.Drawing.Point(34, 24);
            this.txtP2Name.Name = "txtP2Name";
            this.txtP2Name.Size = new System.Drawing.Size(122, 21);
            this.txtP2Name.TabIndex = 1;
            this.txtP2Name.Text = "B";
            this.txtP2Name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(3, 30);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(35, 12);
            this.Label4.TabIndex = 8;
            this.Label4.Text = "点名:";
            // 
            // txtX2
            // 
            this.txtX2.Location = new System.Drawing.Point(34, 65);
            this.txtX2.Name = "txtX2";
            this.txtX2.Size = new System.Drawing.Size(122, 21);
            this.txtX2.TabIndex = 2;
            this.txtX2.Text = "0.000";
            this.txtX2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(7, 70);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(23, 12);
            this.Label5.TabIndex = 1;
            this.Label5.Text = "X2=";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(7, 102);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(23, 12);
            this.Label6.TabIndex = 3;
            this.Label6.Text = "Y2=";
            // 
            // txtY2
            // 
            this.txtY2.Location = new System.Drawing.Point(34, 98);
            this.txtY2.Name = "txtY2";
            this.txtY2.Size = new System.Drawing.Size(122, 21);
            this.txtY2.TabIndex = 3;
            this.txtY2.Text = "0.000";
            this.txtY2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // gbPt1
            // 
            this.gbPt1.Controls.Add(this.txtP1Name);
            this.gbPt1.Controls.Add(this.Label13);
            this.gbPt1.Controls.Add(this.txtX1);
            this.gbPt1.Controls.Add(this.Label2);
            this.gbPt1.Controls.Add(this.Label3);
            this.gbPt1.Controls.Add(this.txtY1);
            this.gbPt1.Location = new System.Drawing.Point(13, 17);
            this.gbPt1.Name = "gbPt1";
            this.gbPt1.Size = new System.Drawing.Size(162, 139);
            this.gbPt1.TabIndex = 6;
            this.gbPt1.TabStop = false;
            this.gbPt1.Text = "点1";
            // 
            // txtP1Name
            // 
            this.txtP1Name.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtP1Name.Location = new System.Drawing.Point(34, 24);
            this.txtP1Name.Name = "txtP1Name";
            this.txtP1Name.Size = new System.Drawing.Size(122, 21);
            this.txtP1Name.TabIndex = 1;
            this.txtP1Name.Text = "A";
            this.txtP1Name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(3, 30);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(35, 12);
            this.Label13.TabIndex = 8;
            this.Label13.Text = "点名:";
            // 
            // txtX1
            // 
            this.txtX1.Location = new System.Drawing.Point(34, 65);
            this.txtX1.Name = "txtX1";
            this.txtX1.Size = new System.Drawing.Size(122, 21);
            this.txtX1.TabIndex = 2;
            this.txtX1.Text = "0.000";
            this.txtX1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(7, 70);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(23, 12);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "X1=";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(7, 102);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(23, 12);
            this.Label3.TabIndex = 3;
            this.Label3.Text = "Y1=";
            // 
            // txtY1
            // 
            this.txtY1.Location = new System.Drawing.Point(34, 98);
            this.txtY1.Name = "txtY1";
            this.txtY1.Size = new System.Drawing.Size(122, 21);
            this.txtY1.TabIndex = 3;
            this.txtY1.Text = "0.000";
            this.txtY1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // gbObsData
            // 
            this.gbObsData.Controls.Add(this.gridObsData);
            this.gbObsData.Location = new System.Drawing.Point(373, 20);
            this.gbObsData.Name = "gbObsData";
            this.gbObsData.Size = new System.Drawing.Size(330, 395);
            this.gbObsData.TabIndex = 37;
            this.gbObsData.TabStop = false;
            this.gbObsData.Text = "观测数据";
            // 
            // gridObsData
            // 
            this.gridObsData.AllowUserToAddRows = false;
            this.gridObsData.AllowUserToDeleteRows = false;
            this.gridObsData.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridObsData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridObsData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridObsData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.ptName,
            this.Angle,
            this.Distance});
            this.gridObsData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridObsData.Location = new System.Drawing.Point(3, 17);
            this.gridObsData.Name = "gridObsData";
            this.gridObsData.RowHeadersVisible = false;
            this.gridObsData.RowTemplate.Height = 23;
            this.gridObsData.Size = new System.Drawing.Size(324, 375);
            this.gridObsData.TabIndex = 0;
            // 
            // ID
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ID.DefaultCellStyle = dataGridViewCellStyle2;
            this.ID.HeaderText = "序号";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 40;
            // 
            // ptName
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ptName.DefaultCellStyle = dataGridViewCellStyle3;
            this.ptName.HeaderText = "点名";
            this.ptName.Name = "ptName";
            this.ptName.Width = 60;
            // 
            // Angle
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Angle.DefaultCellStyle = dataGridViewCellStyle4;
            this.Angle.HeaderText = "角度(dd.mmss)";
            this.Angle.Name = "Angle";
            this.Angle.Width = 120;
            // 
            // Distance
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Distance.DefaultCellStyle = dataGridViewCellStyle5;
            this.Distance.HeaderText = "距离(m)";
            this.Distance.Name = "Distance";
            this.Distance.Width = 80;
            // 
            // gbOutput
            // 
            this.gbOutput.Controls.Add(this.rDXF);
            this.gbOutput.Controls.Add(this.rXLS);
            this.gbOutput.Controls.Add(this.rTXT);
            this.gbOutput.Controls.Add(this.rMap);
            this.gbOutput.Controls.Add(this.btOutput);
            this.gbOutput.Location = new System.Drawing.Point(822, 390);
            this.gbOutput.Name = "gbOutput";
            this.gbOutput.Size = new System.Drawing.Size(308, 76);
            this.gbOutput.TabIndex = 12;
            this.gbOutput.TabStop = false;
            this.gbOutput.Text = "成果输出";
            // 
            // rDXF
            // 
            this.rDXF.AutoSize = true;
            this.rDXF.Location = new System.Drawing.Point(15, 51);
            this.rDXF.Name = "rDXF";
            this.rDXF.Size = new System.Drawing.Size(65, 16);
            this.rDXF.TabIndex = 5;
            this.rDXF.Text = "DXF文件";
            this.rDXF.UseVisualStyleBackColor = true;
            // 
            // rXLS
            // 
            this.rXLS.AutoSize = true;
            this.rXLS.Location = new System.Drawing.Point(112, 27);
            this.rXLS.Name = "rXLS";
            this.rXLS.Size = new System.Drawing.Size(65, 16);
            this.rXLS.TabIndex = 3;
            this.rXLS.Text = "XLS文件";
            this.rXLS.UseVisualStyleBackColor = true;
            // 
            // rTXT
            // 
            this.rTXT.AutoSize = true;
            this.rTXT.Location = new System.Drawing.Point(112, 49);
            this.rTXT.Name = "rTXT";
            this.rTXT.Size = new System.Drawing.Size(65, 16);
            this.rTXT.TabIndex = 2;
            this.rTXT.Text = "TXT文件";
            this.rTXT.UseVisualStyleBackColor = true;
            // 
            // rMap
            // 
            this.rMap.AutoSize = true;
            this.rMap.Checked = true;
            this.rMap.Location = new System.Drawing.Point(15, 29);
            this.rMap.Name = "rMap";
            this.rMap.Size = new System.Drawing.Size(71, 16);
            this.rMap.TabIndex = 1;
            this.rMap.TabStop = true;
            this.rMap.Text = "绘制图形";
            this.rMap.UseVisualStyleBackColor = true;
            // 
            // btOutput
            // 
            this.btOutput.Image = ((System.Drawing.Image)(resources.GetObject("btOutput.Image")));
            this.btOutput.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btOutput.Location = new System.Drawing.Point(185, 30);
            this.btOutput.Name = "btOutput";
            this.btOutput.Size = new System.Drawing.Size(111, 32);
            this.btOutput.TabIndex = 4;
            this.btOutput.Text = "输出(&O)...";
            this.btOutput.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btOutput.UseVisualStyleBackColor = true;
            this.btOutput.Click += new System.EventHandler(this.btOutput_Click);
            // 
            // chkIsFileInput
            // 
            this.chkIsFileInput.AutoSize = true;
            this.chkIsFileInput.Checked = true;
            this.chkIsFileInput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsFileInput.Location = new System.Drawing.Point(14, 16);
            this.chkIsFileInput.Name = "chkIsFileInput";
            this.chkIsFileInput.Size = new System.Drawing.Size(15, 14);
            this.chkIsFileInput.TabIndex = 7;
            this.chkIsFileInput.UseVisualStyleBackColor = true;
            this.chkIsFileInput.CheckedChanged += new System.EventHandler(this.chkIsFileInput_CheckedChanged);
            // 
            // btFileInput
            // 
            this.btFileInput.Image = ((System.Drawing.Image)(resources.GetObject("btFileInput.Image")));
            this.btFileInput.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btFileInput.Location = new System.Drawing.Point(35, 10);
            this.btFileInput.Name = "btFileInput";
            this.btFileInput.Size = new System.Drawing.Size(120, 24);
            this.btFileInput.TabIndex = 8;
            this.btFileInput.Text = "文件导入(&I)...";
            this.btFileInput.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btFileInput.UseVisualStyleBackColor = true;
            this.btFileInput.Click += new System.EventHandler(this.btInput_Click);
            // 
            // gbResult
            // 
            this.gbResult.Controls.Add(this.txtResult);
            this.gbResult.Location = new System.Drawing.Point(822, 40);
            this.gbResult.Name = "gbResult";
            this.gbResult.Size = new System.Drawing.Size(311, 347);
            this.gbResult.TabIndex = 11;
            this.gbResult.TabStop = false;
            this.gbResult.Text = "计算结果";
            // 
            // txtResult
            // 
            this.txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResult.Location = new System.Drawing.Point(3, 17);
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.txtResult.Size = new System.Drawing.Size(305, 327);
            this.txtResult.TabIndex = 1;
            this.txtResult.Text = "";
            // 
            // btCalc
            // 
            this.btCalc.Image = ((System.Drawing.Image)(resources.GetObject("btCalc.Image")));
            this.btCalc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btCalc.Location = new System.Drawing.Point(728, 262);
            this.btCalc.Name = "btCalc";
            this.btCalc.Size = new System.Drawing.Size(87, 24);
            this.btCalc.TabIndex = 10;
            this.btCalc.Text = "计算(&C)=>";
            this.btCalc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btCalc.UseVisualStyleBackColor = true;
            this.btCalc.Click += new System.EventHandler(this.btCalc_Click);
            // 
            // frmProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 477);
            this.Controls.Add(this.gbInput);
            this.Controls.Add(this.gbOutput);
            this.Controls.Add(this.chkIsFileInput);
            this.Controls.Add(this.btFileInput);
            this.Controls.Add(this.gbResult);
            this.Controls.Add(this.btCalc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProcess";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "平差处理";
            this.Load += new System.EventHandler(this.frmProcess_Load);
            this.gbInput.ResumeLayout(false);
            this.gbParam.ResumeLayout(false);
            this.gbParam.PerformLayout();
            this.bgKnowPtInfo.ResumeLayout(false);
            this.gbPt4.ResumeLayout(false);
            this.gbPt4.PerformLayout();
            this.gbPt3.ResumeLayout(false);
            this.gbPt3.PerformLayout();
            this.gbPt2.ResumeLayout(false);
            this.gbPt2.PerformLayout();
            this.gbPt1.ResumeLayout(false);
            this.gbPt1.PerformLayout();
            this.gbObsData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridObsData)).EndInit();
            this.gbOutput.ResumeLayout(false);
            this.gbOutput.PerformLayout();
            this.gbResult.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.GroupBox gbInput;
        internal System.Windows.Forms.GroupBox gbParam;
        internal System.Windows.Forms.CheckBox chkLeftAngle;
        internal System.Windows.Forms.TextBox txtUnknowPtNum;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.CheckBox chkIsConnecting;
        internal System.Windows.Forms.Button btMannul;
        internal System.Windows.Forms.GroupBox bgKnowPtInfo;
        internal System.Windows.Forms.GroupBox gbPt4;
        internal System.Windows.Forms.TextBox txtP4Name;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.TextBox txtX4;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.TextBox txtY4;
        internal System.Windows.Forms.GroupBox gbPt3;
        internal System.Windows.Forms.TextBox txtP3Name;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.TextBox txtX3;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.TextBox txtY3;
        internal System.Windows.Forms.GroupBox gbPt2;
        internal System.Windows.Forms.TextBox txtP2Name;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.TextBox txtX2;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.TextBox txtY2;
        internal System.Windows.Forms.GroupBox gbPt1;
        internal System.Windows.Forms.TextBox txtP1Name;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.TextBox txtX1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox txtY1;
        internal System.Windows.Forms.GroupBox gbObsData;
        internal System.Windows.Forms.DataGridView gridObsData;
        internal System.Windows.Forms.DataGridViewTextBoxColumn ID;
        internal System.Windows.Forms.DataGridViewTextBoxColumn ptName;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Angle;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Distance;
        internal System.Windows.Forms.GroupBox gbOutput;
        internal System.Windows.Forms.RadioButton rXLS;
        internal System.Windows.Forms.RadioButton rTXT;
        internal System.Windows.Forms.RadioButton rMap;
        internal System.Windows.Forms.Button btOutput;
        internal System.Windows.Forms.CheckBox chkIsFileInput;
        internal System.Windows.Forms.Button btFileInput;
        internal System.Windows.Forms.GroupBox gbResult;
        internal System.Windows.Forms.RichTextBox txtResult;
        internal System.Windows.Forms.Button btCalc;
        internal System.Windows.Forms.RadioButton rDXF;
    }
}