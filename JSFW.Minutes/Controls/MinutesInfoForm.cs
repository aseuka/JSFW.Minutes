using Greenshot;
using Greenshot.Destinations;
using Greenshot.Helpers;
using Greenshot.IniFile;
using GreenshotPlugin.Controls;
using GreenshotPlugin.Core;
using PopupControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSFW.Minutes.Controls
{
    public partial class MinutesInfoForm : Form, IVisit
    {
        public bool IsDirty { get; set; }

        public bool HasRaiseViewEvent { get; set; }

        public string UnitID { get; set; }
          
        public string UnitName
        {
            get { return txtSubject.Text; }
            set
            {
                txtSubject.Text = value; IsDirty = false;
            }
        }
         
        public bool IsNew { get; set; }

        bool IsRun_Clock = false;


        // 시작시각
        public string StartTime { get; private set; } = "";

        public string EndTime { get; private set; } = "";
          
        ChattingForm chatt = new ChattingForm();

        Popup pop = null;

        public MinutesInfoForm()
        {
            InitializeComponent(); 

            IniConfig.Init();
            MainForm._conf = IniConfig.GetIniSection<CoreConfiguration>();
            MainForm._conf.Language = "ko-kr";
            Language.CurrentLanguage = "ko-kr";

            MainForm._conf.OutputDestinations.Clear();
            // MainForm._conf.OutputDestinations.Add(EditorDestination.DESIGNATION);
            MainForm._conf.OutputDestinations.Add(FileDestination.DESIGNATION);
            MainForm._conf.OutputDestinations.Add(ClipboardDestination.DESIGNATION);
             
            MainForm._conf.IECapture = true;
            MainForm._conf.CaptureDelay = 600;

            string LogFileLocation; LogFileLocation = LogHelper.InitializeLog4Net();

            MainForm._conf.OutputFilePath = StaticConst.JSFW_NPT_DIR + "TEMP\\";
            if (Directory.Exists(MainForm._conf.OutputFilePath) == false)
            {
                Directory.CreateDirectory(MainForm._conf.OutputFilePath);
            }

            IniConfig.Save();

            bool ignoreFailedRegistration = false;
            StringBuilder failedKeys = new StringBuilder();


            HotkeyControl.RegisterHotkeyHwnd(Handle);

            if (!RegisterWrapper(Keys.Alt, Keys.PrintScreen, this.Handle, CaptureWin, ignoreFailedRegistration))
            {

            }

            //Ctrl + Shift + PrintScreen
            if (!RegisterWrapper(Keys.Control | Keys.Shift, Keys.PrintScreen, this.Handle, CaptureIE, ignoreFailedRegistration))
            {

            } 

            pop = new Popup(chatt);
            pop.AutoClose = true;
            pop.Opened += Pop_Opened;
            pop.Closing += Pop_Closing;
            chatt.Commit += Chatt_Commit;

            this.Disposed += MinutesInfoForm_Disposed;
             
            //Windows 10 일때 Full!  
            Screen sc = Screen.FromControl(this);
            this.MaximumSize = new Size(sc.WorkingArea.Width + 38, sc.WorkingArea.Height + 16);
        }
         
        public MinutesUnit Unit { get; internal set; }
        public MinutesInfo Info { get; internal set; }

        internal void CreateUnit()
        {
            Unit = new MinutesUnit();
            Info = Unit.GetMinutesInfo(); 
            DataBind();
        }

        public void SetUnit(MinutesUnit unit)
        {
            Unit = unit;
            if (Unit != null)
            {
                Info = Unit.GetMinutesInfo();
            }
            DataBind();
        }

        bool IsDataBinding = false;
        private void DataBind()
        {
            try
            {
                IsDataBinding = true; 

                DataClear();

                hasPlayTime = false;

                if (Info != null)
                {
                    try
                    {
                        txtSubject.Text = Info.Subject;
                        txtPlace.Text = Info.Place;
                        subjectViewControl1.SetSubjectList(Info.Topics ?? "");

                        rtxAttachFiles.Links.AddRange(Info.AttatchFileLinks.ToArray());
                        rtxAttachFiles.Rtf = Info.AttatchFilsRTF?.Replace("\n", Environment.NewLine);
                        rtxAttachFiles.RestoreHyperLinks();
                        txbBigo.Text = Info.Bigo;

                        txtPlace.Text = Info.Place;
                        dtDay.Value = Info.Day?.ToDateTime("yyyy-MM-dd") ?? DateTime.Now;

                        cboHour.Text = Info.Time ?? "시각";

                        StartTime = Info.StartTime;
                        EndTime = Info.EndTime;

                        chkStartAndStop.Checked = false;
                        chkStartAndStop.Enabled = string.IsNullOrWhiteSpace(EndTime);
                        btnEnd.Enabled = string.IsNullOrWhiteSpace(EndTime);

                        btnPlay.Enabled = false;
                        btnStop.Enabled = false;

                        if (string.IsNullOrWhiteSpace(StartTime) == false && string.IsNullOrWhiteSpace(EndTime) == false)
                        {
                            btnPlay.Enabled = true;
                            btnStop.Enabled = false;

                            ReadOnly(true);
                        }
                        trackBar1.Visible = btnPlay.Enabled; 
                        btnChattLogRefresh.Enabled = hasPlayTime;

                        rtxChattings.Links.AddRange(Info.ChattLinks.ToArray());
                        rtxChattings.Rtf = Info.ChattRTF?.Replace("\n", Environment.NewLine);
                        rtxChattings.RestoreHyperLinks(); 
                         
                        // 저장. 
                        string[] players = Info.Players?.Split(Environment.NewLine.ToArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (players != null && 0 < players.Length)
                        {
                            foreach (var player in players)
                            {
                                PlayerItemControl playerControl = new PlayerItemControl();
                                playerControl.Set(player);
                                flpPlayers.Controls.Add(playerControl);
                                playerControl.Width = flpPlayers.Width - 6;
                                playerControl.ItemDbClick += PlayerControl_ItemDbClick;
                                playerControl.ItemSelected += PlayerControl_ItemSelected;
                                playerControl.DataUpdate += Player_DataUpdate;
                            }
                        }

                        foreach (var data in Info.PlaceInPlayers)
                        {
                            PlayerViewControl player = null;
                            foreach (PlayerViewControl playerview in pnlPlacePanel.Controls)
                            {
                                if (playerview.SEQ == data.SEQ)
                                {
                                    player = playerview;
                                    break;
                                }
                            }

                            if (player == null)
                            {
                                player = new PlayerViewControl();
                                player.SetData(data);
                                pnlPlacePanel.Controls.Add(player);

                                player.Left = data.Left;
                                player.Top = data.Top;
                                player.Width = data.Width;
                                player.Height = data.Height;

                                player.MouseDown += Player_MouseDown;
                                player.MouseDoubleClick += MinutesInfoForm_MouseDoubleClick;
                                player.ContextMenuStrip = contextMenuStrip1;
                            }
                        }

                        Records.AddRange(Info.Records.ToArray());

                        txtLogView.Text = Info.Log?.Replace("\n", Environment.NewLine);
                        txtLogView.SelectionStart = txtLogView.Text.Length;
                        txtLogView.SelectionLength = 0;
                        txtLogView.ScrollToCaret();
                    }
                    finally
                    {
                        IsDirty = false;
                    }
                }
            }
            finally
            {
                SetPlayerViewControl(null);
                SetSelectNode(null);
                SetSelectPlayer(null);

                IsDataBinding = false;
            }
        }

        private void DataClear()
        {
            try
            {
                hasPlayTime = false;

                ReadOnly(false);

                SetPlayerViewControl(null);
                SetSelectNode(null);
                SetSelectPlayer(null); 

                txtSubject.ResetText();
                txtPlace.ResetText();
                subjectViewControl1.SetSubjectList("");
                rtxAttachFiles.Links.Clear();
                rtxAttachFiles.ResetText();
                rtxAttachFiles.Rtf = "";
                txbBigo.ResetText();

                txtPlace.ResetText();
                dtDay.Value = DateTime.Now;
                cboHour.Text = "시각";

                using (pictureBox1.Image) {
                    pictureBox1.Image = null;
                }
                txtComment.ResetText();

               // pnlPlacePanel

                for (int loop = flpPlayers.Controls.Count - 1; loop >= 0; loop--)
                {
                    PlayerItemControl item = flpPlayers.Controls[loop] as PlayerItemControl;
                    if( item != null)
                    {
                        using (item)
                        {
                            item.ItemDbClick -= PlayerControl_ItemDbClick;
                            item.ItemSelected -= PlayerControl_ItemSelected;
                            item.DataUpdate -= Player_DataUpdate; 
                            flpPlayers.Controls.Remove(item);
                        }
                    }
                }
                flpPlayers.Controls.Clear();

                for (int loop = pnlPlacePanel.Controls.Count - 1; loop >= 0; loop--)
                {
                    PlayerViewControl item = pnlPlacePanel.Controls[loop] as PlayerViewControl;
                    if (item != null)
                    {
                        using (item)
                        {
                            item.MouseDown -= Player_MouseDown;
                            item.MouseDoubleClick -= MinutesInfoForm_MouseDoubleClick;
                            item.ContextMenuStrip = null; 
                            pnlPlacePanel.Controls.Remove(item);
                        }
                    }
                }
                pnlPlacePanel.Controls.Clear();
                 
                Records.Clear();

                StartTime = "";
                EndTime = "";

                chkStartAndStop.Checked = false;
                chkStartAndStop.Enabled = true;
                btnEnd.Enabled = true;
                btnPlay.Enabled = false;
                btnStop.Enabled = false;

                trackBar1.Visible = false; 
                btnChattLogRefresh.Enabled = false;
            }
            finally
            {
                IsDirty = false;
            }


        }

        private void MinutesInfoForm_Disposed(object sender, EventArgs e)
        {
            chatt = null;
            pop = null;
             
            IsRun_Clock = false;
            SelectPlayerControl = null;
            SelectedPlayerViewControl = null;
            SelectedNode = null;
            ThreadClock = null;

            Unit = null;
            Info = null;
        }
        
        Action ThreadClock = null;
        protected override void OnLoad(EventArgs e)
        {
            cboHour.Items.Clear();
            for (int loop = 0; loop < 24; loop++)
            {
                cboHour.Items.Add(string.Format("{0:D2}:{1:D2}", loop, 0));
                cboHour.Items.Add(string.Format("{0:D2}:{1:D2}", loop, 30));
            }
            cboHour.DropDown += CboHour_DropDown;

            base.OnLoad(e); 

            if (IsNew)
            {
                UnitID = MinutesManager.CreateMinutesID();  
            }
         
         //   chkOpenPlace.Checked = false;

            try
            {
                IsRun_Clock = true;
                ThreadClock = new Action(StartClock);
                ThreadClock.BeginInvoke(ir => ThreadClock?.EndInvoke(ir), null);
            }
            catch {
                // 닫을때? 에러강??? 
            }
            finally
            {
                btnEnd.Enabled = false;
            } 
        }

        private void CboHour_DropDown(object sender, EventArgs e)
        {
            if (cboHour.Text == "시각")
            {
                cboHour.Text = "09:00";
            }
        }

        private void StartClock()
        {
            while (IsRun_Clock)
            {
                this.DoAsync( c => lbClock.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                System.Threading.Thread.Sleep(100);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            using (pictureBox1.Image)
            {
                pictureBox1.Image = null;
            }

            SavingPlacePlayer();
            SavingChattAndAttatchFiles(); 

            base.OnFormClosing(e);

            SetPlayerViewControl(null);
            SetSelectNode(null);
            SetSelectPlayer(null);

            using (chatt)
            {
                if (chatt != null)
                {
                    chatt.Commit -= Chatt_Commit;
                }
            }

            using (pop)
            {
                if (pop != null)
                {
                    pop.Opened -= Pop_Opened;
                    pop.Closing -= Pop_Closing;
                }
            }
             
            IsRun_Clock = e.Cancel;

            if (!e.Cancel) {
                IsPlayRecord = false; // 플레이 정지.
            }
            Application.DoEvents();
            System.Threading.Thread.Sleep(110);
            ThreadClock = null;
        }

        private void SavingChattAndAttatchFiles()
        {
            //닫을때 챗창!! 
            if (IsDirty && !hasPlayTime)
            {
                Info.AttatchFileLinks.Clear();
                Info.AttatchFileLinks.AddRange(rtxAttachFiles.Links.ToArray());
                Info.AttatchFilsRTF = rtxAttachFiles.Rtf;

                Info.ChattLinks.Clear();
                Info.ChattLinks.AddRange(rtxChattings.Links.ToArray());
                Info.ChattRTF = rtxChattings.Rtf;
            }
        }
         
        private void SavingPlacePlayer()
        {
            Info.PlaceInPlayers.Clear();
            foreach (PlayerViewControl view in pnlPlacePanel.Controls)
            {
                Info.PlaceInPlayers.Add(new PlayerDragDropData()
                {
                    SEQ = view.SEQ,
                    PlayerName = view.PlayerName,
                    Left = view.Left,
                    Top = view.Top,
                    Width = view.Width,
                    Height = view.Height,
                });
            }
        }

        private void txtSubject_TextChanged(object sender, EventArgs e)
        {
            if (IsDataBinding) return;

            IsDirty = true; 
        }

        private void txtSubject_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               
            }
        }

        int oldPanel2Width = 0;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //panel2 = 985, 763
            //   this.MaximumSize = new Size(sc.WorkingArea.Width + 38, sc.WorkingArea.Height + 16);
            FormWindowState backWindowState = WindowState;
            try
            {
                this.SuspendLayout();
                this.MaximumSize = Size.Empty;
                WindowState = FormWindowState.Normal; 
                if (chkOpenPlace.Checked)
                {
                    Screen sc = Screen.FromControl(this);

                    panel2.Width = oldPanel2Width;
                    this.Width += panel2.Width;
                    chkOpenPlace.Text = "장소 기록 닫기";
                    //최대값일때? 누르면... 이상해지넹? 
                    this.MaximumSize = new Size(this.Width, sc.WorkingArea.Height + 8); // 가로 폭 고정하고 세로로만 늘어나게...  
                    this.Top = 0;
                    this.Height = sc.WorkingArea.Height + 8;
                }
                else
                {
                    oldPanel2Width = panel2.Width;
                    this.Width -= oldPanel2Width;
                    chkOpenPlace.Text = "장소 기록 보기";
                    this.MaximumSize = this.Size;
                }
                panel2.Visible = chkOpenPlace.Checked;

                chkStartAndStop.Visible = chkOpenPlace.Checked;
                btnEnd.Visible = chkOpenPlace.Checked;

                tabControl1.SelectedIndex = 0;
            }
            finally
            {
                WindowState = backWindowState; 
                this.ResumeLayout(false);
                this.PerformLayout();
            }
        } 

        private void btnTopicEdit_Click(object sender, EventArgs e)
        {
            // 토픽 등록 
            /*
                >> 토픽으로 트리형태로 구분할수 있게 만듬.
                >> 이걸 통으로 저장!
                # 안건1
                ##  항목1-1
                # 안건2
                ## 항목2-1
                ### 항목2-1-1
            */

            string ASIS = @"# 안건1
##  항목1
# 안건2
## 항목21
### 항목211
### 항목212
#### 항목2121
# 안건3
## 항목31
### 항목31
## 항목32
## 항목33";
             
            string TOBE = @"1. 안건1
1.1.  항목1
2. 안건2
2.1. 항목21
2.1.1. 항목211
2.1.2. 항목212
2.1.2.1. 항목2121
3. 안건3
3.1. 항목31
3.1.1 항목311
3.2. 항목32
3.3. 항목33";

            using (InputBoxForm bf = new InputBoxForm())
            {
                bf.ASIS = ASIS;
                bf.TOBE = TOBE;

                bf.Title = "안건 등록";
                bf.SetContent( subjectViewControl1.SubjectText.Replace("\r", "").Replace("\n", Environment.NewLine) ); 
                if (bf.ShowDialog() == DialogResult.OK)
                {
                    subjectViewControl1.PreFix = SubjectViewControl.PreFixType.NUMBER;
                    subjectViewControl1.SetSubjectList(bf.Content);

                    IsDirty = true;
                    Info.Topics = bf.Content;
                    Log("안건 편집", bf.Content);
                }
            }
        }
         
        private void chkStartAndStop_CheckedChanged(object sender, EventArgs e)
        {
            if (IsDataBinding) return;

            // 시작 > 스탑!
            if (chkStartAndStop.Checked)
            {
                // 시작
                chkStartAndStop.Text = "휴식";

                if (string.IsNullOrWhiteSpace(StartTime))
                {
                    //시작 전 !!
                    if (string.IsNullOrWhiteSpace(txtSubject.Text))
                    {
                        "주제를 입력해야함.".AlertWarning();
                        IsDataBinding = true;
                        chkStartAndStop.Checked = false;
                        IsDataBinding = false;
                        chkStartAndStop.Text = "시작";
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtPlace.Text))
                    {
                        "장소 입력이 필요함.".AlertWarning();
                        IsDataBinding = true;
                        chkStartAndStop.Checked = false;
                        IsDataBinding = false;
                        chkStartAndStop.Text = "시작";
                        return;
                    }

                    if (subjectViewControl1.Nodes.Count <= 0)
                    {
                        "안건 입력이 필요함.".AlertWarning();
                        IsDataBinding = true;
                        chkStartAndStop.Checked = false;
                        IsDataBinding = false;
                        chkStartAndStop.Text = "시작";
                        return;
                    }
                     
                    if (string.IsNullOrWhiteSpace(Info.Day))
                    {
                        "일자 확인이 필요함.".AlertWarning();
                        IsDataBinding = true;
                        chkStartAndStop.Checked = false;
                        IsDataBinding = false;
                        chkStartAndStop.Text = "시작";
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(Info.Time))
                    {
                        "시각 설정이 필요함.".AlertWarning();
                        IsDataBinding = true;
                        chkStartAndStop.Checked = false;
                        IsDataBinding = false;
                        chkStartAndStop.Text = "시작";
                        return;
                    }
                     
                    if (flpPlayers.Controls.Count <= 0)
                    {
                        "참가자 입력이 필요함.".AlertWarning();
                        IsDataBinding = true;
                        chkStartAndStop.Checked = false;
                        IsDataBinding = false;
                        chkStartAndStop.Text = "시작";
                        return;
                    } 

                    StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    EndTime = "";

                    Info.StartTime = StartTime;
                    Info.EndTime = EndTime;
                }

                ChangeREC(true);

                Record rec = new Record()
                {
                    Command = "INPUT",
                    TypeName = "COMMONMSG",
                    TargetID = "",                    
                    Data = "회의 시작",
                    Tick = DateTime.Now.Ticks
                };
                Records.Add(rec);
                Info.Records.Add(rec);
                IsDirty = true;
                rtxChattings.Write(rec);
               
                Log("시작");
            }
            else
            {
                // 휴식
                chkStartAndStop.Text = "시작";

                if (string.IsNullOrWhiteSpace(EndTime)) // 종료시 종료 레코딩 후 휴식 레코드가 타므로... 
                { 
                    Record rec = new Record()
                    {
                        Command = "INPUT",
                        TypeName = "COMMONMSG",
                        TargetID = "",
                        Data = "회의 휴식",
                        Tick = DateTime.Now.Ticks
                    };
                    Records.Add(rec);
                    Info.Records.Add(rec);
                    IsDirty = true;
                    rtxChattings.Write(rec);
                } 
                ChangeREC(false);

                Log("휴식");
            }
             
            btnEnd.Enabled = true;
        }

        private void ChangeREC(bool turnON = false)
        {
            if (turnON)
            {
                lbREC.ForeColor = Color.Red;
                rdoREC.PointColor = Color.Red;
            }
            else
            {
                lbREC.ForeColor = Color.DimGray;
                rdoREC.PointColor = Color.DimGray;
            }
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            // 종료.
            if ("정말 종료?".Confirm() == DialogResult.Yes)
            {
                Record rec = new Record()
                {
                    Command = "INPUT",
                    TypeName = "COMMONMSG",
                    TargetID = "",
                    Data = "회의 종료",
                    Tick = DateTime.Now.Ticks
                };
                Records.Add(rec);
                Info.Records.Add(rec);
                IsDirty = true;
                rtxChattings.Write(rec);

                ChangeREC(false);

                if (string.IsNullOrWhiteSpace(EndTime))
                {
                    EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); 
                    Info.EndTime = EndTime;
                }

                chkStartAndStop.Checked = false; // 휴식 레코드가 실행되니까... 아예 휴식 안찍히도록 해당 컨트롤 이벤트에서 패스처리.
                chkStartAndStop.Enabled = false;
                 
                btnPlay.Enabled = false;
                btnStop.Enabled = false;

                if (string.IsNullOrWhiteSpace(StartTime) == false && string.IsNullOrWhiteSpace(EndTime) == false)
                { 
                    btnPlay.Enabled = true;
                    btnStop.Enabled = true;
                }
                trackBar1.Visible = btnPlay.Enabled;

                btnEnd.Enabled = false;

                ReadOnly(true);

                SavingChattAndAttatchFiles();

                SetPlayerViewControl(null);
                SetSelectNode(null);
                SetSelectPlayer(null);

                Log("종료");
            }
        }

        private void ReadOnly(bool isReadOnly)
        { 
            bool isEnabled = !isReadOnly;


            txtSubject.ReadOnly = isReadOnly;
            btnTopicEdit.Enabled = isEnabled;
            txbBigo.ReadOnly = isReadOnly;
            txtPlace.ReadOnly = isReadOnly;
            dtDay.Enabled = isEnabled;
            cboHour.Enabled = isEnabled;
            panel8.Enabled = isEnabled;
            pnlPlacePanel.Enabled = isEnabled;
            btnPlayerEdit.Enabled = isEnabled;
            btnAddPlayer.Enabled = isEnabled;
            btnDelPlayer.Enabled = isEnabled;
            btnDelPlayerOK.Enabled = isEnabled;
            btnDelPlayerCancel.Enabled = isEnabled;
            flpPlayers.Enabled = isEnabled;
            subjectViewControl1.Enabled = isEnabled;
        }

        private void btnPlayerEdit_Click(object sender, EventArgs e)
        {
            //참가자 편집
            //{직급 이름}:::{SEQ}

            if (0 < pnlPlacePanel.Controls.Count)
            {
                "장소에 배치된 인력이 있음.".AlertWarning();
                return;
            }

            string ASIS = @"과장 홍길동
대리 고길동
PM 부장 아무개
PL 차장 고무개
개발자 A씨";

            string TOBE = @"과장 홍길동:::762ea7899e764daf803e55393146f282
대리 고길동:::480012ce9ca8469391a332fb0fc6678e
PM 부장 아무개:::11c9eaeefb1f4845be1ea1e5b41cb859
PL 차장 고무개:::a15a0a17122848b7b22f7edcd26caa5c
개발자 A씨:::7ad737cebd2244668652a082b007a08c";
             
            using (InputBoxForm bf = new InputBoxForm())
            {
                bf.Title = "참여자 등록";
                bf.ASIS = ASIS;
                bf.TOBE = TOBE;

                bf.SetContent(SavingPlayer());
                if (bf.ShowDialog() == DialogResult.OK)
                {
                    // 모두 삭제.
                    for (int loop = flpPlayers.Controls.Count - 1; loop >= 0; loop--)
                    {
                        PlayerItemControl remove = flpPlayers.Controls[loop] as PlayerItemControl;
                        if (remove != null)
                        {
                            using (remove)
                            {
                                remove.ItemDbClick -= PlayerControl_ItemDbClick;
                                remove.ItemSelected -= PlayerControl_ItemSelected;
                                remove.DataUpdate -= Player_DataUpdate;
                            }
                        }
                    }

                    string[] players = bf.Content.Split(Environment.NewLine.ToArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (players != null && 0 < players.Length)
                    {
                        foreach (var player in players)
                        {
                            PlayerItemControl playerControl = new PlayerItemControl();
                            playerControl.Set(player);
                            flpPlayers.Controls.Add(playerControl);
                            playerControl.Width = flpPlayers.Width - 6;
                            playerControl.ItemDbClick += PlayerControl_ItemDbClick;
                            playerControl.ItemSelected += PlayerControl_ItemSelected;
                            playerControl.DataUpdate += Player_DataUpdate;
                        }
                    }

                    Info.Players = SavingPlayer(); 
                    IsDirty = true;
                    Log("참석자 변경", bf.Content);
                }
            }
        }
         
        PlayerItemControl SelectPlayerControl { get; set; } 
        void SetSelectPlayer(PlayerItemControl player) {

            if (SelectPlayerControl != null)
            {
                SelectPlayerControl.CloseEdit();

                SelectPlayerControl.BackColor = BackColor;
                SelectPlayerControl.ForeColor = ForeColor;
            }

            SelectPlayerControl = player;

            if (SelectPlayerControl != null)
            {
                SelectPlayerControl.BackColor = Color.DodgerBlue;
                SelectPlayerControl.ForeColor = Color.White;
            }
        }

        private void Player_DataUpdate(object sender, ItemSelectedEventArgs<PlayerItemControl> e)
        { 
            foreach (PlayerViewControl player in pnlPlacePanel.Controls)
            {
                if (player.SEQ == e.Item.SEQ)
                {
                    Log("참석자 변경", player.PlayerName + " -> " + e.Item.PlayerName); 
                    PlayerDragDropData data = new PlayerDragDropData() { SEQ = player.SEQ, PlayerName = e.Item.PlayerName };
                    player.SetData(data);
                    IsDirty = true;
                    Info.Players = SavingPlayer(); 
                    break;
                }
            }
        }

        private void PlayerControl_ItemSelected(object sender, ItemSelectedEventArgs<PlayerItemControl> e)
        {
            SetSelectPlayer(e.Item);
        }

        private void PlayerControl_ItemDbClick(object sender, ItemSelectedEventArgs<PlayerItemControl> e)
        {
            // 수정!
            
        }

        void Log(string header, object msg = null)
        {
            txtLogView.AppendText(("" + header).Trim()  + "\t[" +  DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]" + Environment.NewLine);
            if( string.IsNullOrWhiteSpace("" + msg) == false)
                txtLogView.AppendText(("" + msg).Trim() + Environment.NewLine);
            txtLogView.AppendText(Environment.NewLine);

            Info.Log = txtLogView.Text;
        }
         
        private void button1_Click(object sender, EventArgs e)
        {
            // 추후에 캡쳐 기능으로 바꾸어 주어야 할 것... 
            // 사용자 정의 캡쳐!
            using (pictureBox1.Image)
            { 
                pictureBox1.Image = null;
            }
            txtComment.Text = "";

            try
            {
                CaptureHelper.Install(Handle);
                CaptureHelper.CaptureRegion(true);
            }
            finally
            {
                CaptureHelper.UnInstall();

                string dir = Unit.GetFileFolderName();
                string fileName = Path.GetFileName(MainForm._conf.OutputFileAsFullpath);
                 
                string imageFilePath = dir + fileName;

                //파일 카피
                if (File.Exists(MainForm._conf.OutputFileAsFullpath))
                {
                    File.Copy(MainForm._conf.OutputFileAsFullpath, imageFilePath, true);

                    //string[] imageFiles = new string[] { @"C:\Users\aseuk\Pictures\11.jpg", @"C:\Users\aseuk\Pictures\vex.PNG" };

                    using (Image img = Bitmap.FromFile(imageFilePath))
                    {
                        pictureBox1.Image = img.Clone() as Image;
                    }

                    Record rec = new Record()
                    {
                        Command = "CHAGE",
                        TypeName = "IMAGE",
                        TargetID = "",
                        Data = imageFilePath,
                        Tick = DateTime.Now.Ticks
                    };

                    if (string.IsNullOrWhiteSpace(EndTime))
                    {
                        RecordView.Write(rtxChattings, rec);

                        if (rtxAttachFiles.Links.Any(l => l.Link == imageFilePath.Replace("\\", "\\\\")) == false)
                        {
                            //링크가 없는 것만 추가함... 
                            rtxAttachFiles.InsertLink(Path.GetFileName(imageFilePath), imageFilePath.Replace("\\", "\\\\"));
                            rtxAttachFiles.AppendText(Environment.NewLine);                            
                        }
                    }

                    if (IsRunnigTime())
                    {
                        Records.Add(rec); IsDirty = true;
                        Info.Records.Add(rec);
                    }
                     
                    Log("자료 변경", imageFilePath); 
                    IsDirty = true;
                    this.Activate();
                }
            } 
        }

        private bool IsRunnigTime()
        {
            // 현재 회의가 진행중인가?
            return string.IsNullOrWhiteSpace(StartTime) == false && string.IsNullOrWhiteSpace(EndTime);
        }

        #region 참가자 목록 관리 
        private void btnAddPlayer_Click(object sender, EventArgs e)
        {
            // 추가!
            PlayerItemControl player = new PlayerItemControl();
            player.Set("");
            flpPlayers.Controls.Add(player);
            player.Width = flpPlayers.Width - 6;
            SetSelectPlayer(player);
            player.ItemDbClick += PlayerControl_ItemDbClick;
            player.ItemSelected += PlayerControl_ItemSelected;
            player.DataUpdate += Player_DataUpdate;
            player.Edit();
             
            Info.Players = SavingPlayer();
            IsDirty = true;
            Log("참석자 추가", "");
        }

        private string SavingPlayer()
        {
            List<string> lst = new List<string>();
            foreach (PlayerItemControl player in flpPlayers.Controls)
            {
                lst.Add($"{player.PlayerName}:::{player.SEQ}");
            }
            return string.Join(Environment.NewLine, lst.ToArray());
        }

        private void btnDelPlayer_Click(object sender, EventArgs e)
        {
            foreach (PlayerItemControl item in flpPlayers.Controls)
            {
                item.ShowDeleteCheckBox();
            }

            btnPlayerEdit.Enabled = false;

            btnDelPlayerOK.BringToFront();
            btnDelPlayerCancel.BringToFront();
            Log("참석자 삭제", "목록");
        }

        private void btnDelPlayerOK_Click(object sender, EventArgs e)
        {
            List<PlayerItemControl> dels = new List<PlayerItemControl>();
            List<PlayerViewControl> delveiws = new List<PlayerViewControl>();

            foreach (PlayerItemControl item in flpPlayers.Controls)
            {
                if (item.IsDeleteSelected)
                    dels.Add(item);
            }

            foreach (PlayerViewControl item in pnlPlacePanel.Controls)
            {
                if (dels.Any(a => a.SEQ == item.SEQ)){
                    delveiws.Add(item);
                }
            }

            for (int loop = dels.Count - 1; loop >= 0; loop--)
            {
                using (dels[loop])
                {
                    dels[loop].ItemDbClick -= PlayerControl_ItemDbClick;
                    dels[loop].ItemSelected -= PlayerControl_ItemSelected;
                    dels[loop].DataUpdate -= Player_DataUpdate; 
                    flpPlayers.Controls.Remove(dels[loop]);
                }
            }

            for (int loop = delveiws.Count - 1; loop >= 0; loop--)
            {
                using (delveiws[loop])
                {
                    delveiws[loop].MouseDown -= Player_MouseDown;
                    delveiws[loop].MouseDoubleClick -= MinutesInfoForm_MouseDoubleClick;
                    pnlPlacePanel.Controls.Remove(delveiws[loop]);
                    IsDirty = true;
                    Log("참석자 삭제", ""+ delveiws[loop].PlayerName );
                }
            }

            // 저장.
            Info.Players = SavingPlayer(); 
            foreach (PlayerItemControl item in flpPlayers.Controls)
            {
                item.HideDeleteCheckBox();
            }

            btnPlayerEdit.Enabled = true;

            btnAddPlayer.BringToFront();
            btnDelPlayer.BringToFront();
        } 

        private void btnDelPlayerCancel_Click(object sender, EventArgs e)
        {
            foreach (PlayerItemControl item in flpPlayers.Controls)
            {
                item.HideDeleteCheckBox();
            }

            btnPlayerEdit.Enabled = true;
            btnAddPlayer.BringToFront();
            btnDelPlayer.BringToFront();

            Log("참석자 삭제 취소", "");
        }
        #endregion
         
        List<Record> Records = new List<Record>(); 

        public void Accept(IAccept acc)
        {
            // 레코드.. 
            // 회의 내용을 플레이 하도록 만듬. 
            // 계속 1개씩 비지터가 넘겨받아서 던져주면 받아서 플레이 해줌. 
            switch (acc.Record.TypeName)
            {
                case "SUBJECT":
                    if (acc.Record.Command == "SELECT")
                    {
                        SelectNode("" + acc.Record.Data);
                    }
                    break;
                case "CHAT":
                    if (acc.Record.Command == "INPUT")
                    {
                        DisplayTalk(acc.Record);
                    }
                    break;
                case "IMAGE":
                    if (acc.Record.Command == "CHAGE")
                    {
                        DrawImage(acc.Record);
                    }
                    else if( acc.Record.Command == "COMMENT" )
                    {
                        txtComment.Text = "" + acc.Record.Data;
                    }
                    break;
                case "COMMONMSG":
                    //if (acc.Record.Command == "INPUT")
                    //{
                    //    DisplayTalk(acc.Record);
                    //}
                    break;
            }
        }
        
        private void DrawImage(Record rec)
        {
            using (pictureBox1.Image)
            {
                pictureBox1.Image = null;
            }
            txtComment.Text = "";

            string imageFilePath =  ""+ rec.Data; 

            if (File.Exists(imageFilePath))
            {
                using (Image img = Bitmap.FromFile(imageFilePath))
                {
                    pictureBox1.Image = img.Clone() as Image;
                }
            }
        }

        private void DisplayTalk(Record rec)
        {
            string seq = rec.TargetID;

            PlayerViewControl pv = null;
            foreach (PlayerViewControl player in pnlPlacePanel.Controls)
            {
                if (player.SEQ == seq)
                {
                    pv = player;
                    break;
                }
            }

            if (pv != null)
            {
                toolTip1.RemoveAll();
                toolTip1.SetToolTip(pv, new DateTime(rec.Tick).ToShortTimeString());                
                toolTip1.Show("" + rec.Data, pv, 2500);
            }
        }

        private void SelectNode(string fullPath)
        {
            TreeNode findingNode = null;
            foreach (TreeNode tn in subjectViewControl1.Nodes)
            {
                if (tn.FullPath == fullPath)
                {
                    findingNode = tn;
                }
                else
                {
                    findingNode = FindFullPathNode(tn, fullPath);
                }

                if (findingNode != null)
                {
                    break;
                }
            }
            subjectViewControl1.SelectedNode = findingNode;
        }

        private TreeNode FindFullPathNode(TreeNode tn, string fullPath)
        {
            TreeNode findingNode = null;
            foreach (TreeNode cn in tn.Nodes)
            {
                if (cn.FullPath == fullPath)
                {
                    findingNode = cn;
                    break;
                }
                else
                {
                    findingNode = FindFullPathNode(cn, fullPath);
                }
            }
            return findingNode;
        }

        private void subjectViewControl1_AfterSelect(object sender, TreeViewEventArgs e)
        { 
            if (IsPlayRecord)
            {
                return;                
            }
            var rec = new Record().SetData("SELECT", "SUBJECT", subjectViewControl1.Name, e.Node.FullPath);
            if (IsRunnigTime())
            {
                Records.Add(rec);
                Info.Records.Add(rec);
                IsDirty = true;
            }
            rtxChattings.Write(rec);
            Log("안건 선택", e.Node.FullPath);
        }

        TreeNode SelectedNode = null;
        private void SetSelectNode(TreeNode node)
        {
            if (SelectedNode != null)
            {
                SelectedNode.BackColor = subjectViewControl1.BackColor;
                SelectedNode.ForeColor = subjectViewControl1.ForeColor;
            }

            SelectedNode = node;

            if (SelectedNode != null)
            {
                SelectedNode.BackColor = SystemColors.Highlight;
                SelectedNode.ForeColor = SystemColors.HighlightText; 
            }
        }

        /// <summary>
        /// 한번이라도 재생을 한거라면 기존 내역을 유지하기 위해... 변경된 내용들을 저장하지 않음.
        /// </summary>
        bool hasPlayTime = false;

        int RecordINDEX = 0;
        bool IsPlayRecord = false;
        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (Records.Count <= 0)
            {
                "기록 내용이 없음.".Alert();
                return;
            }

            // 플레이 버튼 
            // 녹화된 내용을 재생하는 버튼. 
            // 플레이 <----> 일시정지   
            IsPlayRecord = !IsPlayRecord;
             
            if (IsPlayRecord == false)
            {
                btnPlay.Text = "재시작";
                return;
            }
            else
            {
                Log("재생 " + btnPlay.Text, "");

                trackBar1.Maximum = Records.Count - 1; 
                if (RecordINDEX == 0)
                {
                    SetPlayerViewControl(null);
                    SetSelectNode(null);
                    SetSelectPlayer(null);

                    using (pictureBox1.Image)
                    {
                        pictureBox1.Image = null;
                    }
                    txtComment.Text = "";

                    rtxChattings.Clear();
                }

                btnPlay.Text = "일시정지";
                btnStop.Enabled = true;

                subjectViewControl1.PlayMode = true;

                System.Func<int, int> AsyncPlay = new System.Func<int, int>((rIndex) =>
                {
                    hasPlayTime = true;
                    int sumSleeptime = 0;
                    for (; IsPlayRecord && rIndex < Records.Count; rIndex++)
                    {
                        int sleepMaxTime = 500;                        
                        Record rec = Records[rIndex];
                        this.DoAsync(c => tabControl1.SelectedIndex = 0);
                        this.DoAsync(c => rec.Acceptor(this));
                        this.DoAsync(c => rtxChattings.Write(rec));
                        this.DoAsync(c => trackBar1.Value = rIndex);
                        Application.DoEvents();   // 다른 컨트롤에 포커스를 주려면...  
                        
                        if (rec.TypeName == "CHAT") // 챗창!!
                        {
                            sleepMaxTime = 3000;
                        }

                        while (IsPlayRecord && sumSleeptime < sleepMaxTime)
                        {
                            System.Threading.Thread.Sleep(100);
                            sumSleeptime += 100;
                        }                      
                        sumSleeptime = 0;
                    }
                    return rIndex;
                });

                AsyncPlay.BeginInvoke( RecordINDEX, ir =>
                {
                    RecordINDEX = AsyncPlay.EndInvoke(ir);
                    subjectViewControl1.PlayMode = false;

                    // 플레이가 끝난것이면!
                    if (RecordINDEX == Records.Count)
                    {
                        IsPlayRecord = false;
                        RecordINDEX = 0;
                        btnPlay.DoAsync( b => b.Text = "시작"); 
                        btnChattLogRefresh.DoAsync( b => b.Enabled = hasPlayTime);                       
                    }

                    this.DoAsync(c =>
                    {
                        trackBar1.Value = RecordINDEX;
                        SetPlayerViewControl(null);
                        SetSelectNode(null);
                        SetSelectPlayer(null);
                    });
                }, null);
            } 
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            // 플레이 정지
            // 녹화내용 플레이 정지! 
            if (hasPlayTime == false) {
                SavingChattAndAttatchFiles();
            }
            hasPlayTime = true;
            IsPlayRecord = false;
            while (subjectViewControl1.PlayMode)
            {
                System.Threading.Thread.Sleep(100); // 비동기 처리가 끝날때까지 대기
            }
            RecordINDEX = 0;
            this.DoAsync(c => trackBar1.Value = RecordINDEX);
            Log("재생 " + btnPlay.Text, ""); 
            btnPlay.Text = "시작";
            btnChattLogRefresh.Enabled = hasPlayTime;
        } 
         
        /*    참가자 드랍  */

        PlayerViewControl SelectedPlayerViewControl = null;
        void SetPlayerViewControl(PlayerViewControl view)
        {
            if (SelectedPlayerViewControl != null)
            {
                SelectedPlayerViewControl.BackColor = Color.MintCream; 
            }

            SelectedPlayerViewControl = view;

            if (SelectedPlayerViewControl != null)
            {
                SelectedPlayerViewControl.BackColor = Color.LightCyan; 
            }
        }
        
        private void pnlPlacePanel_DragDrop(object sender, DragEventArgs e)
        {
            if (e.AllowedEffect == DragDropEffects.Link)
            { 
                PlayerDragDropData data = e.Data.GetData(typeof(PlayerDragDropData).FullName) as PlayerDragDropData;
                if (data != null)
                {
                    PlayerViewControl player = null;

                    foreach (PlayerViewControl playerview in pnlPlacePanel.Controls)
                    {
                        if (playerview.SEQ == data.SEQ)
                        {
                            player = playerview;
                            break;
                        }
                    }

                    if (player == null)
                    {
                        player = new PlayerViewControl();
                        player.SetData(data);
                        pnlPlacePanel.Controls.Add(player);
                        player.Location = pnlPlacePanel.PointToClient(new Point(e.X, e.Y));
                        player.MouseDown += Player_MouseDown;
                        player.MouseDoubleClick += MinutesInfoForm_MouseDoubleClick;
                        player.ContextMenuStrip = contextMenuStrip1;
                          
                        Log("참석자 배치", $"{player.PlayerName}:::{player.SEQ}");
                    }
                    SetPlayerViewControl(player);
                }
            }
        } 

        private void pnlPlacePanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.AllowedEffect == DragDropEffects.Link)
            {
                PlayerDragDropData data = e.Data.GetData(typeof(PlayerDragDropData).FullName) as PlayerDragDropData;
                if (data != null)
                {
                    bool hasPlayer = false;
                    foreach (PlayerViewControl playerview in pnlPlacePanel.Controls)
                    {
                        if (playerview.SEQ == data.SEQ)
                        {
                            hasPlayer = true;
                            break;
                        }
                    }
                    if (!hasPlayer)
                    {
                        e.Effect = DragDropEffects.Link;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Player_MouseDown(object sender, MouseEventArgs e)
        {
            SetPlayerViewControl(sender as PlayerViewControl);
        }

        private void MinutesInfoForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 챗창.. 연결 ]
            if (string.IsNullOrWhiteSpace(EndTime) == false) return; // 종료된 후엔.. 변경 X

            if (SelectedPlayerViewControl != null)
            {
                pop.Show(sender as Control);
            }
        }
         
        private void Chatt_Commit(object sender, EventArgs e)
        {
            string onOff = "";
            if (chkStartAndStop.Checked == false)
            {
                onOff = "[OFF]";
            }

            Record rec = new Record()
            {
                Command = "INPUT",
                TypeName = "CHAT",
                TargetID = chatt.SEQ,
                Player = chatt.PlayerName,
                Data = onOff + chatt.Chatt,
                Tick = DateTime.Now.Ticks
            };

            if (IsRunnigTime())
            {
                Records.Add(rec); IsDirty = true; 
                Info.Records.Add(rec);
            }
            rtxChattings.Write(rec);

            IsDirty = true;

            Log("CHAT", chatt.Chatt);
            Action async = new Action(chatt.ClearText);
            async.BeginInvoke(ir => async.EndInvoke(ir), null);
            pop.Close();
        }

        private void Pop_Opened(object sender, EventArgs e)
        {
            //챗창이 열릴때 > 아무개 설정.    
            if (SelectedPlayerViewControl != null)
            {
                chatt.SEQ = SelectedPlayerViewControl.SEQ;
                chatt.PlayerName = SelectedPlayerViewControl.PlayerName;
            }
        }

        private void Pop_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {

        }

        private void txtSubject_Leave(object sender, EventArgs e)
        {
            if (txtSubject.Modified)
            {
                Info.Subject = txtSubject.Text;

                Log("주제변경", txtSubject.Text);
                txtSubject.Modified = false;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (txtPlace.Modified)
            {
                Info.Place = txtPlace.Text;
                Log("장소변경", txtPlace.Text);
                txtPlace.Modified = false;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (IsDataBinding) return;

            IsDirty = true;
            Info.Day = dtDay.Value.ToShortDateString();
            Info.Time = cboHour.Text;
            Log("일자변경", dtDay.Value);
        }

        private void cboHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsDataBinding) return;

            IsDirty = true;
            Info.Day = dtDay.Value.ToShortDateString();
            Info.Time = cboHour.Text;
            Log("시각변경", cboHour.Text);
        }

        private void txtComment_Leave(object sender, EventArgs e)
        {
            if (txtComment.Modified)
            { 
                Record rec = new Record()
                {
                    Command = "COMMENT",
                    TypeName = "IMAGE",
                    TargetID = "",
                    Data = txtComment.Text,
                    Tick = DateTime.Now.Ticks
                }; 
                RecordView.Write(rtxChattings, rec);
                if (IsRunnigTime())
                {
                    Records.Add(rec);
                    Info.Records.Add(rec);
                    IsDirty = true;
                }
                Log("자료 코멘트", txtComment.Text);
                txtComment.Modified = false;
            }
        }

        private void txtComment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //SendKeys.Send("{TAB}");
                pnlPlacePanel.Focus();
            }
        }

        private void richTextBoxEx1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            // 이미지 링크
            string[] link = rtxChattings.ParseLink(e.LinkText);
            if (1 < link.Length) {
                System.Diagnostics.Process.Start(link[1]);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void 삭제ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 플레이어 삭제
            if (SelectedPlayerViewControl != null)
            {
                SelectedPlayerViewControl.MouseDown -= Player_MouseDown;
                SelectedPlayerViewControl.MouseDoubleClick -= MinutesInfoForm_MouseDoubleClick;
                pnlPlacePanel.Controls.Remove(SelectedPlayerViewControl);
                using (SelectedPlayerViewControl) {
                    SelectedPlayerViewControl.ContextMenuStrip = null;
                } 
                SelectedPlayerViewControl = null;
            }
        }

        private void rtxAttachFiles_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            // 첨부파일 링크
            string[] link = rtxAttachFiles.ParseLink(e.LinkText);
            if (1 < link.Length)
            {
                System.Diagnostics.Process.Start(link[1]);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void txtComment_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            
        }

        private void txbBigo_Leave(object sender, EventArgs e)
        {
            if (txbBigo.Modified)
            {
                Info.Bigo = txbBigo.Text;
                txbBigo.Modified = false;
            }
        }

        private void txbBigo_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void btnChattLogRefresh_Click(object sender, EventArgs e)
        {
            if (hasPlayTime == false)
            {
                "원본 로그임.".Alert();
                return;
            }

            if (hasPlayTime)
            {
                rtxAttachFiles.Links.Clear();
                rtxAttachFiles.Links.AddRange(Info.AttatchFileLinks.ToArray());
                rtxAttachFiles.Rtf = Info.AttatchFilsRTF;
                rtxAttachFiles.RestoreHyperLinks();

                rtxChattings.Links.Clear();
                rtxChattings.Links.AddRange(Info.ChattLinks.ToArray());
                rtxChattings.Rtf = Info.ChattRTF;
                rtxChattings.RestoreHyperLinks();
            }
        }

        private void subjectViewControl1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
