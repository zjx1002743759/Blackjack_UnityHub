
namespace Blackjack_UnityHub.GUI
{
    partial class MainUI
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainUI));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listView1 = new System.Windows.Forms.ListView();
            this.processLbl = new System.Windows.Forms.Label();
            this.tipsLbl = new System.Windows.Forms.Label();
            this.Btn_Downlaod = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.ucListView1 = new HZH_Controls.Controls.UCListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.下载ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.processLbl);
            this.splitContainer1.Panel2.Controls.Add(this.tipsLbl);
            this.splitContainer1.Panel2.Controls.Add(this.Btn_Downlaod);
            this.splitContainer1.Panel2.Controls.Add(this.progressBar);
            this.splitContainer1.Panel2.Controls.Add(this.ucListView1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 457);
            this.splitContainer1.SplitterDistance = 179;
            this.splitContainer1.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(179, 457);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // processLbl
            // 
            this.processLbl.AutoSize = true;
            this.processLbl.Location = new System.Drawing.Point(379, 387);
            this.processLbl.Name = "processLbl";
            this.processLbl.Size = new System.Drawing.Size(23, 12);
            this.processLbl.TabIndex = 6;
            this.processLbl.Text = "00%";
            // 
            // tipsLbl
            // 
            this.tipsLbl.AutoSize = true;
            this.tipsLbl.Location = new System.Drawing.Point(236, 387);
            this.tipsLbl.Name = "tipsLbl";
            this.tipsLbl.Size = new System.Drawing.Size(137, 12);
            this.tipsLbl.TabIndex = 5;
            this.tipsLbl.Text = "正在下载中，请耐心等候";
            // 
            // Btn_Downlaod
            // 
            this.Btn_Downlaod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Btn_Downlaod.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Btn_Downlaod.Font = new System.Drawing.Font("宋体", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_Downlaod.Location = new System.Drawing.Point(0, 416);
            this.Btn_Downlaod.Name = "Btn_Downlaod";
            this.Btn_Downlaod.Size = new System.Drawing.Size(617, 41);
            this.Btn_Downlaod.TabIndex = 4;
            this.Btn_Downlaod.Text = "Downlaod";
            this.Btn_Downlaod.UseVisualStyleBackColor = false;
            this.Btn_Downlaod.Click += new System.EventHandler(this.Btn_Downlaod_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(0, 405);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(617, 11);
            this.progressBar.TabIndex = 1;
            // 
            // ucListView1
            // 
            this.ucListView1.BackColor = System.Drawing.Color.White;
            this.ucListView1.ContextMenuStrip = this.contextMenuStrip1;
            this.ucListView1.DataSource = null;
            this.ucListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucListView1.IsMultiple = false;
            this.ucListView1.ItemType = typeof(HZH_Controls.Controls.UCListViewItem);
            this.ucListView1.Location = new System.Drawing.Point(0, 0);
            this.ucListView1.Margin = new System.Windows.Forms.Padding(0);
            this.ucListView1.Name = "ucListView1";
            this.ucListView1.Page = null;
            this.ucListView1.SelectedSource = ((System.Collections.Generic.List<object>)(resources.GetObject("ucListView1.SelectedSource")));
            this.ucListView1.Size = new System.Drawing.Size(617, 457);
            this.ucListView1.TabIndex = 0;
            this.ucListView1.SelectedItemEvent += new System.EventHandler(this.ucListView1_SelectedItemEvent);
            this.ucListView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ucListView1_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.下载ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // 下载ToolStripMenuItem
            // 
            this.下载ToolStripMenuItem.Name = "下载ToolStripMenuItem";
            this.下载ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.下载ToolStripMenuItem.Text = "下载";
            // 
            // MainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 457);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainUI";
            this.Text = "BlackJack Unity Hub 1.0.1";
            this.Load += new System.EventHandler(this.MainUI_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listView1;
        private HZH_Controls.Controls.UCListView ucListView1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 下载ToolStripMenuItem;
        private System.Windows.Forms.Button Btn_Downlaod;
        private System.Windows.Forms.Label tipsLbl;
        private System.Windows.Forms.Label processLbl;
    }
}