namespace JSFW.Minutes.Controls
{
    partial class MinutesInfoForm
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
            this.components = new System.ComponentModel.Container();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.subjectViewControl1 = new JSFW.Minutes.Controls.SubjectViewControl();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rtxAttachFiles = new JSFW.Minutes.Controls.RichTextBoxEx();
            this.label8 = new JSFW.Minutes.Controls.Label();
            this.btnTopicEdit = new System.Windows.Forms.Button();
            this.txbBigo = new System.Windows.Forms.TextBox();
            this.label2 = new JSFW.Minutes.Controls.Label();
            this.chkOpenPlace = new System.Windows.Forms.CheckBox();
            this.label1 = new JSFW.Minutes.Controls.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pnlPlacePanel = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel9 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label10 = new JSFW.Minutes.Controls.Label();
            this.label7 = new JSFW.Minutes.Controls.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.rtxChattings = new JSFW.Minutes.Controls.RichTextBoxEx();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtLogView = new System.Windows.Forms.TextBox();
            this.label6 = new JSFW.Minutes.Controls.Label();
            this.lbClock = new JSFW.Minutes.Controls.Label();
            this.pnlTimeLine = new System.Windows.Forms.Panel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.btnChattLogRefresh = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.rdoREC = new JSFW.Minutes.Controls.Radio();
            this.lbREC = new JSFW.Minutes.Controls.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cboHour = new System.Windows.Forms.ComboBox();
            this.dtDay = new System.Windows.Forms.DateTimePicker();
            this.txtPlace = new System.Windows.Forms.TextBox();
            this.label5 = new JSFW.Minutes.Controls.Label();
            this.label4 = new JSFW.Minutes.Controls.Label();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.btnEnd = new System.Windows.Forms.Button();
            this.chkStartAndStop = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnDelPlayer = new System.Windows.Forms.Button();
            this.flpPlayers = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddPlayer = new System.Windows.Forms.Button();
            this.btnDelPlayerOK = new System.Windows.Forms.Button();
            this.btnDelPlayerCancel = new System.Windows.Forms.Button();
            this.btnPlayerEdit = new System.Windows.Forms.Button();
            this.label3 = new JSFW.Minutes.Controls.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.삭제ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel10.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.pnlTimeLine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel7.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSubject
            // 
            this.txtSubject.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSubject.Location = new System.Drawing.Point(3, 26);
            this.txtSubject.Multiline = true;
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(357, 30);
            this.txtSubject.TabIndex = 0;
            this.txtSubject.Text = "abc가나다123\r\n가나다";
            this.txtSubject.TextChanged += new System.EventHandler(this.txtSubject_TextChanged);
            this.txtSubject.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubject_KeyDown);
            this.txtSubject.Leave += new System.EventHandler(this.txtSubject_Leave);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.subjectViewControl1);
            this.panel1.Controls.Add(this.splitter3);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.btnTopicEdit);
            this.panel1.Controls.Add(this.txbBigo);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtSubject);
            this.panel1.Controls.Add(this.chkOpenPlace);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(363, 741);
            this.panel1.TabIndex = 14;
            // 
            // subjectViewControl1
            // 
            this.subjectViewControl1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.subjectViewControl1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.subjectViewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subjectViewControl1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.subjectViewControl1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.subjectViewControl1.FullRowSelect = true;
            this.subjectViewControl1.HideSelection = false;
            this.subjectViewControl1.Location = new System.Drawing.Point(3, 80);
            this.subjectViewControl1.Name = "subjectViewControl1";
            this.subjectViewControl1.PlayMode = false;
            this.subjectViewControl1.PreFix = JSFW.Minutes.Controls.SubjectViewControl.PreFixType.NUMBER;
            this.subjectViewControl1.ShowLines = false;
            this.subjectViewControl1.Size = new System.Drawing.Size(357, 480);
            this.subjectViewControl1.SubjectText = "";
            this.subjectViewControl1.TabIndex = 2;
            this.subjectViewControl1.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.subjectViewControl1_BeforeCollapse);
            this.subjectViewControl1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.subjectViewControl1_AfterSelect);
            // 
            // splitter3
            // 
            this.splitter3.BackColor = System.Drawing.Color.Gainsboro;
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter3.Location = new System.Drawing.Point(3, 560);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(357, 3);
            this.splitter3.TabIndex = 24;
            this.splitter3.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.rtxAttachFiles);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(3, 563);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(357, 132);
            this.panel4.TabIndex = 23;
            // 
            // rtxAttachFiles
            // 
            this.rtxAttachFiles.BackColor = System.Drawing.Color.White;
            this.rtxAttachFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxAttachFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxAttachFiles.Location = new System.Drawing.Point(0, 23);
            this.rtxAttachFiles.Name = "rtxAttachFiles";
            this.rtxAttachFiles.ReadOnly = true;
            this.rtxAttachFiles.Size = new System.Drawing.Size(357, 109);
            this.rtxAttachFiles.TabIndex = 25;
            this.rtxAttachFiles.TabStop = false;
            this.rtxAttachFiles.Text = "";
            this.rtxAttachFiles.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtxAttachFiles_LinkClicked);
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label8.HasLINE = true;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(357, 23);
            this.label8.TabIndex = 22;
            this.label8.Text = "첨부파일";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnTopicEdit
            // 
            this.btnTopicEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTopicEdit.Location = new System.Drawing.Point(305, 57);
            this.btnTopicEdit.Name = "btnTopicEdit";
            this.btnTopicEdit.Size = new System.Drawing.Size(55, 23);
            this.btnTopicEdit.TabIndex = 1;
            this.btnTopicEdit.Text = "편집";
            this.btnTopicEdit.UseVisualStyleBackColor = true;
            this.btnTopicEdit.Click += new System.EventHandler(this.btnTopicEdit_Click);
            // 
            // txbBigo
            // 
            this.txbBigo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txbBigo.Location = new System.Drawing.Point(3, 695);
            this.txbBigo.Multiline = true;
            this.txbBigo.Name = "txbBigo";
            this.txbBigo.Size = new System.Drawing.Size(357, 43);
            this.txbBigo.TabIndex = 3;
            this.txbBigo.TextChanged += new System.EventHandler(this.txbBigo_TextChanged);
            this.txbBigo.Leave += new System.EventHandler(this.txbBigo_Leave);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label2.HasLINE = true;
            this.label2.Location = new System.Drawing.Point(3, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(357, 24);
            this.label2.TabIndex = 16;
            this.label2.Text = "안건";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkOpenPlace
            // 
            this.chkOpenPlace.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkOpenPlace.Checked = true;
            this.chkOpenPlace.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOpenPlace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkOpenPlace.Location = new System.Drawing.Point(256, 3);
            this.chkOpenPlace.Name = "chkOpenPlace";
            this.chkOpenPlace.Size = new System.Drawing.Size(104, 23);
            this.chkOpenPlace.TabIndex = 14;
            this.chkOpenPlace.Text = "장소 기록 보기";
            this.chkOpenPlace.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkOpenPlace.UseVisualStyleBackColor = true;
            this.chkOpenPlace.Visible = false;
            this.chkOpenPlace.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label1.HasLINE = false;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(357, 23);
            this.label1.TabIndex = 13;
            this.label1.Text = "주제";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.lbClock);
            this.panel2.Controls.Add(this.pnlTimeLine);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.splitter2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(363, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(981, 741);
            this.panel2.TabIndex = 15;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.tabControl1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 56);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(978, 639);
            this.panel6.TabIndex = 18;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(978, 639);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pnlPlacePanel);
            this.tabPage1.Controls.Add(this.panel8);
            this.tabPage1.Controls.Add(this.panel10);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(970, 613);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "진행";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pnlPlacePanel
            // 
            this.pnlPlacePanel.AllowDrop = true;
            this.pnlPlacePanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlPlacePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPlacePanel.Location = new System.Drawing.Point(3, 234);
            this.pnlPlacePanel.Name = "pnlPlacePanel";
            this.pnlPlacePanel.Size = new System.Drawing.Size(665, 376);
            this.pnlPlacePanel.TabIndex = 0;
            this.pnlPlacePanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnlPlacePanel_DragDrop);
            this.pnlPlacePanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.pnlPlacePanel_DragEnter);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Gainsboro;
            this.panel8.Controls.Add(this.txtComment);
            this.panel8.Controls.Add(this.button1);
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Controls.Add(this.label10);
            this.panel8.Controls.Add(this.label7);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(3, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(665, 231);
            this.panel8.TabIndex = 2;
            // 
            // txtComment
            // 
            this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment.Location = new System.Drawing.Point(127, 207);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(461, 21);
            this.txtComment.TabIndex = 0;
            this.txtComment.TextChanged += new System.EventHandler(this.txtComment_TextChanged);
            this.txtComment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtComment_KeyDown);
            this.txtComment.Leave += new System.EventHandler(this.txtComment_Leave);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(4, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 30);
            this.button1.TabIndex = 2;
            this.button1.Text = "사용자 정의 캡쳐";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel9
            // 
            this.panel9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel9.BackColor = System.Drawing.Color.White;
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Controls.Add(this.pictureBox1);
            this.panel9.Location = new System.Drawing.Point(127, 3);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(461, 203);
            this.panel9.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(459, 201);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label10
            // 
            this.label10.HasLINE = false;
            this.label10.Location = new System.Drawing.Point(4, 207);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(118, 21);
            this.label10.TabIndex = 0;
            this.label10.Text = "자료 설명";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.HasLINE = false;
            this.label7.Location = new System.Drawing.Point(4, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 23);
            this.label7.TabIndex = 0;
            this.label7.Text = "자료";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.DarkGray;
            this.panel10.Controls.Add(this.rtxChattings);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel10.Location = new System.Drawing.Point(668, 3);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(299, 607);
            this.panel10.TabIndex = 21;
            // 
            // rtxChattings
            // 
            this.rtxChattings.BackColor = System.Drawing.Color.White;
            this.rtxChattings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxChattings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxChattings.Location = new System.Drawing.Point(0, 0);
            this.rtxChattings.Name = "rtxChattings";
            this.rtxChattings.ReadOnly = true;
            this.rtxChattings.Size = new System.Drawing.Size(299, 607);
            this.rtxChattings.TabIndex = 0;
            this.rtxChattings.TabStop = false;
            this.rtxChattings.Text = "";
            this.rtxChattings.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBoxEx1_LinkClicked);
            // 
            // tabPage2
            // 
            this.tabPage2.CausesValidation = false;
            this.tabPage2.Controls.Add(this.txtLogView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(970, 613);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "로그";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtLogView
            // 
            this.txtLogView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogView.Location = new System.Drawing.Point(3, 3);
            this.txtLogView.MaxLength = 2147483647;
            this.txtLogView.Multiline = true;
            this.txtLogView.Name = "txtLogView";
            this.txtLogView.ReadOnly = true;
            this.txtLogView.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLogView.Size = new System.Drawing.Size(964, 607);
            this.txtLogView.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label6.HasLINE = false;
            this.label6.Location = new System.Drawing.Point(755, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 18);
            this.label6.TabIndex = 6;
            this.label6.Text = "현재시간 :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lbClock
            // 
            this.lbClock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbClock.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold);
            this.lbClock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lbClock.HasLINE = false;
            this.lbClock.Location = new System.Drawing.Point(840, 6);
            this.lbClock.Name = "lbClock";
            this.lbClock.Size = new System.Drawing.Size(135, 18);
            this.lbClock.TabIndex = 6;
            this.lbClock.Text = "2018-03-08 10:14:22";
            this.lbClock.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // pnlTimeLine
            // 
            this.pnlTimeLine.Controls.Add(this.trackBar1);
            this.pnlTimeLine.Controls.Add(this.btnChattLogRefresh);
            this.pnlTimeLine.Controls.Add(this.btnStop);
            this.pnlTimeLine.Controls.Add(this.btnPlay);
            this.pnlTimeLine.Controls.Add(this.rdoREC);
            this.pnlTimeLine.Controls.Add(this.lbREC);
            this.pnlTimeLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTimeLine.Location = new System.Drawing.Point(3, 695);
            this.pnlTimeLine.Name = "pnlTimeLine";
            this.pnlTimeLine.Size = new System.Drawing.Size(978, 46);
            this.pnlTimeLine.TabIndex = 0;
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.Enabled = false;
            this.trackBar1.LargeChange = 1;
            this.trackBar1.Location = new System.Drawing.Point(233, 3);
            this.trackBar1.Maximum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(439, 40);
            this.trackBar1.TabIndex = 4;
            this.trackBar1.TabStop = false;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar1.Visible = false;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // btnChattLogRefresh
            // 
            this.btnChattLogRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChattLogRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChattLogRefresh.Location = new System.Drawing.Point(895, 2);
            this.btnChattLogRefresh.Name = "btnChattLogRefresh";
            this.btnChattLogRefresh.Size = new System.Drawing.Size(80, 41);
            this.btnChattLogRefresh.TabIndex = 2;
            this.btnChattLogRefresh.Text = "로그 복구";
            this.btnChattLogRefresh.UseVisualStyleBackColor = true;
            this.btnChattLogRefresh.Click += new System.EventHandler(this.btnChattLogRefresh_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Location = new System.Drawing.Point(166, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(61, 41);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "정지";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Enabled = false;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Location = new System.Drawing.Point(80, 3);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(80, 41);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "시작";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // rdoREC
            // 
            this.rdoREC.AutoSize = true;
            this.rdoREC.Location = new System.Drawing.Point(50, 10);
            this.rdoREC.MaximumSize = new System.Drawing.Size(24, 24);
            this.rdoREC.MinimumSize = new System.Drawing.Size(24, 24);
            this.rdoREC.Name = "rdoREC";
            this.rdoREC.PointColor = System.Drawing.Color.DimGray;
            this.rdoREC.Size = new System.Drawing.Size(24, 24);
            this.rdoREC.TabIndex = 1;
            this.rdoREC.TabStop = true;
            this.rdoREC.Text = "radio1";
            this.rdoREC.UseVisualStyleBackColor = true;
            // 
            // lbREC
            // 
            this.lbREC.Font = new System.Drawing.Font("굴림체", 16F, System.Drawing.FontStyle.Bold);
            this.lbREC.ForeColor = System.Drawing.Color.DimGray;
            this.lbREC.HasLINE = false;
            this.lbREC.Location = new System.Drawing.Point(7, 11);
            this.lbREC.Name = "lbREC";
            this.lbREC.Size = new System.Drawing.Size(67, 22);
            this.lbREC.TabIndex = 2;
            this.lbREC.Text = "REC";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.cboHour);
            this.panel5.Controls.Add(this.dtDay);
            this.panel5.Controls.Add(this.txtPlace);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(3, 27);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(978, 29);
            this.panel5.TabIndex = 16;
            // 
            // cboHour
            // 
            this.cboHour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboHour.FormattingEnabled = true;
            this.cboHour.Location = new System.Drawing.Point(895, 4);
            this.cboHour.Name = "cboHour";
            this.cboHour.Size = new System.Drawing.Size(79, 20);
            this.cboHour.TabIndex = 2;
            this.cboHour.Text = "시각";
            this.cboHour.SelectedIndexChanged += new System.EventHandler(this.cboHour_SelectedIndexChanged);
            // 
            // dtDay
            // 
            this.dtDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtDay.Location = new System.Drawing.Point(729, 4);
            this.dtDay.Name = "dtDay";
            this.dtDay.Size = new System.Drawing.Size(161, 21);
            this.dtDay.TabIndex = 1;
            this.dtDay.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // txtPlace
            // 
            this.txtPlace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlace.Location = new System.Drawing.Point(95, 4);
            this.txtPlace.Name = "txtPlace";
            this.txtPlace.Size = new System.Drawing.Size(628, 21);
            this.txtPlace.TabIndex = 0;
            this.txtPlace.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.txtPlace.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.txtPlace.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // label5
            // 
            this.label5.HasLINE = false;
            this.label5.Location = new System.Drawing.Point(2, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "장소 && 시간";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label4.HasLINE = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(978, 27);
            this.label4.TabIndex = 15;
            this.label4.Text = "회의장";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.Color.Gainsboro;
            this.splitter2.Enabled = false;
            this.splitter2.Location = new System.Drawing.Point(0, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 741);
            this.splitter2.TabIndex = 19;
            this.splitter2.TabStop = false;
            // 
            // btnEnd
            // 
            this.btnEnd.BackColor = System.Drawing.Color.Coral;
            this.btnEnd.Enabled = false;
            this.btnEnd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnd.ForeColor = System.Drawing.Color.White;
            this.btnEnd.Location = new System.Drawing.Point(95, 15);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(75, 35);
            this.btnEnd.TabIndex = 1;
            this.btnEnd.Text = "종료";
            this.btnEnd.UseVisualStyleBackColor = false;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // chkStartAndStop
            // 
            this.chkStartAndStop.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkStartAndStop.BackColor = System.Drawing.Color.Green;
            this.chkStartAndStop.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.chkStartAndStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkStartAndStop.ForeColor = System.Drawing.Color.White;
            this.chkStartAndStop.Location = new System.Drawing.Point(6, 15);
            this.chkStartAndStop.Name = "chkStartAndStop";
            this.chkStartAndStop.Size = new System.Drawing.Size(83, 35);
            this.chkStartAndStop.TabIndex = 0;
            this.chkStartAndStop.Text = "시작";
            this.chkStartAndStop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkStartAndStop.UseVisualStyleBackColor = false;
            this.chkStartAndStop.CheckedChanged += new System.EventHandler(this.chkStartAndStop_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnDelPlayer);
            this.panel3.Controls.Add(this.flpPlayers);
            this.panel3.Controls.Add(this.btnAddPlayer);
            this.panel3.Controls.Add(this.btnDelPlayerOK);
            this.panel3.Controls.Add(this.btnDelPlayerCancel);
            this.panel3.Controls.Add(this.btnPlayerEdit);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(1347, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3);
            this.panel3.Size = new System.Drawing.Size(182, 741);
            this.panel3.TabIndex = 16;
            // 
            // btnDelPlayer
            // 
            this.btnDelPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelPlayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelPlayer.Location = new System.Drawing.Point(138, 58);
            this.btnDelPlayer.Name = "btnDelPlayer";
            this.btnDelPlayer.Size = new System.Drawing.Size(42, 21);
            this.btnDelPlayer.TabIndex = 4;
            this.btnDelPlayer.Text = "-";
            this.btnDelPlayer.UseVisualStyleBackColor = true;
            this.btnDelPlayer.Click += new System.EventHandler(this.btnDelPlayer_Click);
            // 
            // flpPlayers
            // 
            this.flpPlayers.AutoScroll = true;
            this.flpPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpPlayers.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpPlayers.Location = new System.Drawing.Point(3, 80);
            this.flpPlayers.Name = "flpPlayers";
            this.flpPlayers.Size = new System.Drawing.Size(176, 658);
            this.flpPlayers.TabIndex = 5;
            this.flpPlayers.WrapContents = false;
            // 
            // btnAddPlayer
            // 
            this.btnAddPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddPlayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPlayer.Location = new System.Drawing.Point(95, 58);
            this.btnAddPlayer.Name = "btnAddPlayer";
            this.btnAddPlayer.Size = new System.Drawing.Size(42, 21);
            this.btnAddPlayer.TabIndex = 3;
            this.btnAddPlayer.Text = "+";
            this.btnAddPlayer.UseVisualStyleBackColor = true;
            this.btnAddPlayer.Click += new System.EventHandler(this.btnAddPlayer_Click);
            // 
            // btnDelPlayerOK
            // 
            this.btnDelPlayerOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelPlayerOK.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDelPlayerOK.Location = new System.Drawing.Point(95, 58);
            this.btnDelPlayerOK.Name = "btnDelPlayerOK";
            this.btnDelPlayerOK.Size = new System.Drawing.Size(42, 21);
            this.btnDelPlayerOK.TabIndex = 0;
            this.btnDelPlayerOK.Text = "확인";
            this.btnDelPlayerOK.UseVisualStyleBackColor = true;
            this.btnDelPlayerOK.Click += new System.EventHandler(this.btnDelPlayerOK_Click);
            // 
            // btnDelPlayerCancel
            // 
            this.btnDelPlayerCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelPlayerCancel.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDelPlayerCancel.Location = new System.Drawing.Point(138, 58);
            this.btnDelPlayerCancel.Name = "btnDelPlayerCancel";
            this.btnDelPlayerCancel.Size = new System.Drawing.Size(42, 21);
            this.btnDelPlayerCancel.TabIndex = 0;
            this.btnDelPlayerCancel.Text = "취소";
            this.btnDelPlayerCancel.UseVisualStyleBackColor = true;
            this.btnDelPlayerCancel.Click += new System.EventHandler(this.btnDelPlayerCancel_Click);
            // 
            // btnPlayerEdit
            // 
            this.btnPlayerEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlayerEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlayerEdit.Location = new System.Drawing.Point(52, 58);
            this.btnPlayerEdit.Name = "btnPlayerEdit";
            this.btnPlayerEdit.Size = new System.Drawing.Size(40, 21);
            this.btnPlayerEdit.TabIndex = 2;
            this.btnPlayerEdit.Text = "편집";
            this.btnPlayerEdit.UseVisualStyleBackColor = true;
            this.btnPlayerEdit.Click += new System.EventHandler(this.btnPlayerEdit_Click);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label3.HasLINE = true;
            this.label3.Location = new System.Drawing.Point(3, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 23);
            this.label3.TabIndex = 1;
            this.label3.Text = "참석자";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btnEnd);
            this.panel7.Controls.Add(this.chkStartAndStop);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(3, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(176, 54);
            this.panel7.TabIndex = 21;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.LightGray;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Enabled = false;
            this.splitter1.Location = new System.Drawing.Point(1344, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 741);
            this.splitter1.TabIndex = 17;
            this.splitter1.TabStop = false;
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.삭제ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(99, 26);
            this.contextMenuStrip1.Text = "삭제";
            // 
            // 삭제ToolStripMenuItem
            // 
            this.삭제ToolStripMenuItem.Name = "삭제ToolStripMenuItem";
            this.삭제ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.삭제ToolStripMenuItem.Text = "삭제";
            this.삭제ToolStripMenuItem.Click += new System.EventHandler(this.삭제ToolStripMenuItem_Click);
            // 
            // MinutesInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1529, 741);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimizeBox = false;
            this.Name = "MinutesInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "회의록 정보";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel10.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.pnlTimeLine.ResumeLayout(false);
            this.pnlTimeLine.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtSubject;
        private Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox chkOpenPlace;
        private Label label3;
        private System.Windows.Forms.TextBox txbBigo;
        private Label label2;
        private System.Windows.Forms.Button btnTopicEdit;
        private System.Windows.Forms.Splitter splitter1;
        private Label label4;
        private System.Windows.Forms.Panel panel5;
        private Label label5;
        private System.Windows.Forms.DateTimePicker dtDay;
        private System.Windows.Forms.TextBox txtPlace;
        private System.Windows.Forms.CheckBox chkStartAndStop;
        private System.Windows.Forms.Button btnEnd;
        private Label lbClock;
        private Label label6;
        private System.Windows.Forms.Panel pnlTimeLine;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Button btnPlayerEdit;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.FlowLayoutPanel flpPlayers;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtLogView;
        private System.Windows.Forms.ComboBox cboHour;
        private System.Windows.Forms.Panel panel8;
        private Label label7;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel pnlPlacePanel;
        private System.Windows.Forms.Button button1;
        private Radio rdoREC;
        private Label lbREC;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnDelPlayerCancel;
        private System.Windows.Forms.Button btnDelPlayerOK;
        private System.Windows.Forms.Button btnDelPlayer;
        private System.Windows.Forms.Button btnAddPlayer;
        private SubjectViewControl subjectViewControl1;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Panel panel4;
        private Label label8;
        private System.Windows.Forms.Panel panel10;
        private RichTextBoxEx rtxChattings;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtComment;
        private Label label10;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 삭제ToolStripMenuItem;
        private RichTextBoxEx rtxAttachFiles;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button btnChattLogRefresh;
    }
}