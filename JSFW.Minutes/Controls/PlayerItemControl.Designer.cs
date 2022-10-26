namespace JSFW.Minutes.Controls
{
    partial class PlayerItemControl
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbPlayer = new JSFW.Minutes.Controls.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtPlayerName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbPlayer
            // 
            this.lbPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPlayer.HasLINE = false;
            this.lbPlayer.Location = new System.Drawing.Point(20, 5);
            this.lbPlayer.Name = "lbPlayer";
            this.lbPlayer.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lbPlayer.Size = new System.Drawing.Size(153, 15);
            this.lbPlayer.TabIndex = 0;
            this.lbPlayer.Text = "직급 참석자명 :: SEQ";
            this.lbPlayer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbPlayer.Click += new System.EventHandler(this.lbPlayer_Click);
            this.lbPlayer.DoubleClick += new System.EventHandler(this.lbPlayer_DoubleClick);
            this.lbPlayer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbPlayer_MouseDown);
            this.lbPlayer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbPlayer_MouseMove);
            this.lbPlayer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbPlayer_MouseUp);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox1.Location = new System.Drawing.Point(5, 5);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 15);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // txtPlayerName
            // 
            this.txtPlayerName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlayerName.Location = new System.Drawing.Point(3, 2);
            this.txtPlayerName.Name = "txtPlayerName";
            this.txtPlayerName.Size = new System.Drawing.Size(172, 21);
            this.txtPlayerName.TabIndex = 2;
            this.txtPlayerName.Visible = false;
            this.txtPlayerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPlayerName_KeyDown);
            this.txtPlayerName.Leave += new System.EventHandler(this.txtPlayerName_Leave);
            // 
            // PlayerItemControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbPlayer);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.txtPlayerName);
            this.Name = "PlayerItemControl";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(178, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lbPlayer;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox txtPlayerName;
    }
}
