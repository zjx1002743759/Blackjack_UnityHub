using Blackjack_UnityHub.Helper;
using HZH_Controls.Controls;
using HZH_Controls.Forms;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blackjack_UnityHub.GUI
{
    public partial class MainUI : Form
    {
        public MainUI()
        {
            var remoteHelper = new RemoteFileHelper();
            string rootPath = "\\\\192.168.8.88\\Unity_Release";
            remoteHelper.connectState(rootPath, "UnityUser", "bbbb1234");
            this.RemoteFilesTree = remoteHelper.GetallDirectory(new List<FileNames>(), rootPath);
            InitializeComponent();
        }
        public List<FileNames> RemoteFilesTree { get; set; }
        public List<FileNames> ChildrenFilesTree { get; set; }
        public FileNames VersionFilesTree { get; set; }
        public bool IsSelected { get; set; }

        // 要下载的文件的url
        private  string MUrl = "http://192.168.8.88:8080/Files/Test.zip";
        private  long MTotalSize = 2195611938 / 1000;
        private  string MMd5 = "99EAD7E018B10564F07F131CBFC2C9B9";

        // 下载到本地的文件名
        private  string MSaveFile = "./Test.zip";

        // 解压目录
        private  string MUnzipFile = "./Test";
        private  string MExePath = "./Test/WindowsEditor/Unity.exe";

        private void MainUI_Load(object sender, EventArgs e)
        {
            ImageList image = new ImageList();
            image.ImageSize = new Size(1, 35);//设置每次点击view时以图片的形式
            ColumnHeader ch = new ColumnHeader();
            ch.Text = "版本";
            ch.Width = splitContainer1.Panel1.Width;
            ch.TextAlign = HorizontalAlignment.Center;
            listView1.Columns.Add(ch);//设置listview的列名，没啥用处

            //设置每个view的显示形式
            listView1.SmallImageList = image;

            // 加载一级栏目
            this.LoadList();
            listView1.Width = ch.Width;//设置每个view的宽度都一致
            //listView1.Items[0].BackColor = Color.LightGray;//设置主页的选中后的颜色
            //启动首先展示主页
            //splitContainer1.Panel2.Controls.Clear();//每次执行时清空panel2
        }

        /// <summary>
        /// 一级菜单
        /// </summary>
        private void LoadList()
        {
            listView1.Items.Clear();//清空菜单
            //添加菜单
            this.RemoteFilesTree.ForEach(item=> 
            {
                listView1.Items.Add(new ListViewItem($"     Unity {item.FileName}"));
            });
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            VersionFilesTree = null;
            IsSelected = false;
            List<object> lstSource = new List<object>();
            var selItem = sender as ListView;
            VersionFilesTree = this.RemoteFilesTree.Where(f => selItem.FocusedItem.Text.Contains(f.FileName)).SingleOrDefault();
            ChildrenFilesTree = VersionFilesTree.Children;
            ChildrenFilesTree.ForEach(item =>
            {
                lstSource.Add(item.FileName);
            });
            
           
            //不使用分页控件
            this.ucListView1.DataSource = lstSource;
        }

        private void ucListView1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void ucListView1_SelectedItemEvent(object sender, EventArgs e)
        {
            // 设置选项点击事件
            var selItem = sender as UCListViewItem;
            IsSelected = true;
            var children = ChildrenFilesTree.Where(f => f.FileName == selItem.DataSource).SingleOrDefault();

            this.MUrl = $"http://192.168.8.88:8080/Files/Unity_Release/{VersionFilesTree.FileName}/{children.FileName}";
            this.MTotalSize = children.FileBype / 1000;
            this.MSaveFile = $"./{children.FileName}";
            this.MUnzipFile = $"./{children.Name}";
            this.MExePath = $"./{children.Name}/WindowsEditor/Unity.exe";
    }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {

        }

        private void Btn_Downlaod_Click(object sender, EventArgs e)
        {
            if (!IsSelected || VersionFilesTree == null)
            {
                FrmDialog.ShowDialog(this, "请选择Unity版本", "提示！！！！");
            }
            else
            {
                this.StartDownload();
            }
        }
        private bool TryOpenUnityDemoExe()
        {
            //if (File.Exists(MSaveFile) && DownloadThread.GetMD5FromFile(MSaveFile) != MMd5)
            //{
            //    return false;
            //}

            if (File.Exists(MExePath))
            {
                WinExec(MExePath, 1);
                System.Environment.Exit(0);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 被委托调用,专门设置进度条最大值.
        /// </summary>
        /// <param name="maxValue"></param>
        public void UpdateProgressMaxValue(int maxValue)
        {
            this.progressBar.Maximum = maxValue;
            this.UpdateTipsLbl("正在下载，请耐心等待");
        }

        public void UpdateTipsLbl(string txt)
        {
            this.tipsLbl.Text = txt;
        }

        /// <summary>
        /// 被委托调用,专门设置进度条当前值.
        /// </summary>
        /// <param name="curValue"></param>
        private void UpdateProgressCurValue(int curValue)
        {
            this.progressBar.Value = curValue;
            string nowValueStr = string.Format("{0:F}", (float)curValue / this.progressBar.Maximum * 100);
            this.processLbl.Text = nowValueStr + "%";
        }

        private void StartDownload()
        {
            DownloadThread method = new DownloadThread();

            // 先订阅一下事件
            method.EventDownloadStartEventHandler += this.OnEventDownloadStart;
            method.EventDownloadIngEventHandler += this.OnEventDownloadIng;
            method.EventCheckingMd5EventHandler += this.OnEventCheckMd5;
            method.EventDownloadDoneEventHandler += this.OnEventDownloadDone;

            // 开启一个线程进行下载
            Task task = new Task(() => { method.RunMethod(MUrl, MSaveFile, MTotalSize, MMd5); });
            task.Start();
        }

        private void UnZipFile(string file)
        {
            using (ZipFile zip = new ZipFile(file))
            {
                zip.ExtractAll(MUnzipFile);
            }
        }

        // 线程开始事件,设置进度条最大值
        // 但是不能直接操作进度条,需要一个委托来替我完成
        private void OnEventDownloadStart(long totalSize)
        {
            this.Invoke(new Action<int>(this.UpdateProgressMaxValue), (int)totalSize);
        }

        // 线程执行中的事件,设置进度条当前进度
        // 但是不能直接操作进度条,需要一个委托来替我完成
        private void OnEventDownloadIng(long curDownloadSize)
        {
            this.Invoke(new Action<int>(this.UpdateProgressCurValue), (int)curDownloadSize);
        }

        private void OnEventCheckMd5()
        {
            this.Invoke(new Action<string>(this.UpdateTipsLbl), "正在校验文件，请稍等");
        }

        /// <summary>
        /// 线程完成事件.
        /// </summary>
        private void OnEventDownloadDone()
        {
            if (Directory.Exists(MUnzipFile))
            {
                DirectoryInfo di = new DirectoryInfo(MUnzipFile);
                di.Delete(true);
            }

            // 解压文件
            this.UnZipFile(MSaveFile);

            // 尝试打开下载的exe
            this.TryOpenUnityDemoExe();
        }

        [DllImport("kernel32.dll")]
        public static extern int WinExec(string exeName, int operType);
    }
}
