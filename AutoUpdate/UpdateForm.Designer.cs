
namespace AutoUpdate
{
    partial class UpdateForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateForm));
            this.Btn_Download = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.tipsLbl = new System.Windows.Forms.Label();
            this.processLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CurrentVersion = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.newVersion = new System.Windows.Forms.Label();
            this.btn_Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_Download
            // 
            this.Btn_Download.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Btn_Download.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_Download.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Btn_Download.Location = new System.Drawing.Point(-1, 259);
            this.Btn_Download.Name = "Btn_Download";
            this.Btn_Download.Size = new System.Drawing.Size(254, 30);
            this.Btn_Download.TabIndex = 4;
            this.Btn_Download.Text = "立即更新";
            this.Btn_Download.UseVisualStyleBackColor = false;
            this.Btn_Download.Click += new System.EventHandler(this.Btn_Download_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 147);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(513, 23);
            this.progressBar.TabIndex = 1;
            // 
            // tipsLbl
            // 
            this.tipsLbl.AutoSize = true;
            this.tipsLbl.Location = new System.Drawing.Point(151, 132);
            this.tipsLbl.Name = "tipsLbl";
            this.tipsLbl.Size = new System.Drawing.Size(125, 12);
            this.tipsLbl.TabIndex = 5;
            this.tipsLbl.Text = "正在更新，请耐心等候";
            // 
            // processLbl
            // 
            this.processLbl.AutoSize = true;
            this.processLbl.Location = new System.Drawing.Point(282, 132);
            this.processLbl.Name = "processLbl";
            this.processLbl.Size = new System.Drawing.Size(23, 12);
            this.processLbl.TabIndex = 6;
            this.processLbl.Text = "00%";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "当前版本：";
            // 
            // CurrentVersion
            // 
            this.CurrentVersion.AutoSize = true;
            this.CurrentVersion.Location = new System.Drawing.Point(85, 13);
            this.CurrentVersion.Name = "CurrentVersion";
            this.CurrentVersion.Size = new System.Drawing.Size(0, 12);
            this.CurrentVersion.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "更新版本：";
            // 
            // newVersion
            // 
            this.newVersion.AutoSize = true;
            this.newVersion.Location = new System.Drawing.Point(87, 44);
            this.newVersion.Name = "newVersion";
            this.newVersion.Size = new System.Drawing.Size(0, 12);
            this.newVersion.TabIndex = 10;
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_Close.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Close.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Close.Location = new System.Drawing.Point(271, 259);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(268, 30);
            this.btn_Close.TabIndex = 11;
            this.btn_Close.Text = "暂不更新";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // UpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 301);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.newVersion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CurrentVersion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.processLbl);
            this.Controls.Add(this.tipsLbl);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.Btn_Download);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UpdateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UnityHubDownloader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_Download;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label tipsLbl;
        private System.Windows.Forms.Label processLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label CurrentVersion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label newVersion;
        private System.Windows.Forms.Button btn_Close;
    }
}

