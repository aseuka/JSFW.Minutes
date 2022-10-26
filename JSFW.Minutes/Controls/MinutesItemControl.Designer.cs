namespace JSFW.Minutes.Controls
{
    partial class MinutesItemControl
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lbID = new JSFW.Minutes.Controls.Label();
            this.lbText = new JSFW.Minutes.Controls.Label();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox1.Location = new System.Drawing.Point(5, 5);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.checkBox1.Size = new System.Drawing.Size(19, 46);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // lbID
            // 
            this.lbID.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbID.HasLINE = false;
            this.lbID.Location = new System.Drawing.Point(24, 5);
            this.lbID.Name = "lbID";
            this.lbID.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lbID.Size = new System.Drawing.Size(288, 23);
            this.lbID.TabIndex = 8;
            this.lbID.Text = "주제";
            this.lbID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbID.Click += new System.EventHandler(this.lbID_Click);
            this.lbID.DoubleClick += new System.EventHandler(this.lbID_DoubleClick);
            // 
            // lbText
            // 
            this.lbText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbText.HasLINE = false;
            this.lbText.Location = new System.Drawing.Point(24, 28);
            this.lbText.Name = "lbText";
            this.lbText.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lbText.Size = new System.Drawing.Size(288, 23);
            this.lbText.TabIndex = 9;
            this.lbText.Text = "장소 && 회의 일시";
            this.lbText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbText.Click += new System.EventHandler(this.lbText_Click);
            this.lbText.DoubleClick += new System.EventHandler(this.lbText_DoubleClick);
            // 
            // MinutesItemControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lbText);
            this.Controls.Add(this.lbID);
            this.Controls.Add(this.checkBox1);
            this.Name = "MinutesItemControl";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(317, 56);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private Label lbID;
        private Label lbText;
    }
}
