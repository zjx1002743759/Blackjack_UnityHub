using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;

namespace AutoUpdate
{
    public partial class UpdateForm : Form
    {
        public delegate void EventDownLoading(long curDownloadSize);
        public delegate void EventDownLoadStart();
        public delegate void EventDownLoadDone();
        public event EventHandler EventCmdDoneEventHandler;
        public event EventDownLoading EventDownloadIngEventHandler;
        public event EventDownLoadStart EventDownloadStartEventHandler;
        public event EventDownLoadDone EventDownloadDoneEventHandler;
        string downLoad;

        //文件远程地址
        string updateUrl = "http://192.168.5.101:8080/files/";
        private int MTotalSize;
        public delegate void UpdateState();
        private static string _ZipName = "Blackjack_UnityHub.zip";
        private string _temporaryFile = "UpdateSoft_temporary";
        private string Executable_Soft = "Blackjack_UnityHub";
        bool IsUpdate;

        /// <summary>
        /// 通过配置文件去获取要更新的程序的名字
        /// </summary>
        //public static string XMLGetValue() {
        //    string path = "E:\\C#-workspace\\AutoUpdateApp\\AutoUpdateApp\\Properties\\Updated.xml";
        //    XmlDocument xmldoc = new XmlDocument();
        //    xmldoc.Load(path);
        //    XmlNode listnode = xmldoc.SelectSingleNode("Update");
        //    foreach (XmlNode xmlNode in listnode) {
        //        if (xmlNode.Name == "Soft") {
        //            return xmlNode.InnerText;
        //        }
        //    }
        //    return ""; 
        //}
        public UpdateForm()
        {
            InitializeComponent();
            checkVersion();
        }
        /// <summary>
        /// 调用cmd命令行执行杀死进程的目录，且当进程被杀死之后回调更新方法
        /// </summary>
        /// <param name="command"></param>
        /// <param name="onExit"></param>
        /// <returns></returns>
        private Process CmdProcessStart(string command)
        {
            ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + command);
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.RedirectStandardError = true;
            procStartInfo.CreateNoWindow = true;
            procStartInfo.UseShellExecute = false;
            Process proc = new Process();
            proc.StartInfo = procStartInfo;
            proc.EnableRaisingEvents = true;
            proc.Exited += App_UpdateFinish;
            proc.Start();
            return proc;
        }


        /// <summary>
        /// 下载完成时回调
        /// </summary>
        public void OnEventDownloadDone()
        {
            //Task task = new Task(() => { App_UpdateFinish(); });
            //task.Start();
            Process proc = CmdProcessStart($"taskkill /F /T /IM " + Executable_Soft + ".exe");

            this.tipsLbl.Text = "下载完成";
            this.EventUpdateStartHandler += OnEventUpdateStart;
            this.EventUpdateLoadingHandler += OnEventUpdateIng;
            this.EventUpdateStartHandler?.Invoke();
        }
        /// <summary>
        /// 下载开始时回调
        /// </summary>
        private void OnEventDownloadStart()
        {
            this?.Invoke(new Action(this.UpdateProgressMaxValue));
        }
        /// <summary>
        /// 下载进行时回调
        /// </summary>
        /// <param name="curDownloadSize"></param>
        private void OnEventDownloadIng(long curDownloadSize)
        {
            this?.Invoke(new Action<int>(this.UpdateProgressCurValue), (int)curDownloadSize);
        }




        /// <summary>
        /// 下载开始时
        /// </summary>
        private void UpdateProgressMaxValue()
        {
            String url = updateUrl + _ZipName;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //请求方式为Get
            request.Method = "GET";
            // 接受数据格式为json，字符集为UTF-8
            request.ContentType = "application/json; charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            MTotalSize = (int)response.ContentLength;
            response.Close();
            this.progressBar.Maximum = MTotalSize / 100;
        }
        /// <summary>
        /// 加载进度条
        /// </summary>
        /// <param name="curValue"></param>
        private void UpdateProgressCurValue(int curValue)
        {
            this.progressBar.PerformStep();
            this.progressBar.Value = curValue;
            string nowValueStr = string.Format("{0:F}", (float)curValue / this.progressBar.Maximum * 100);
            this.processLbl.Text = nowValueStr + "%";
            this.processLbl.Refresh();
            this.progressBar.Refresh();
        }

        /// <summary>
        /// 下载压缩包
        /// </summary>
        private void Update(long totalSize)
        {
            try
            {
                //目标文件路径
                Stream writeStream;
                string filename = Application.StartupPath + "\\" + _ZipName;
                string newUpdateUrl = updateUrl + _ZipName;
                WebClient wc = new WebClient();
                this.EventDownloadStartEventHandler?.Invoke();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(newUpdateUrl);
                request.Method = "Get";
                request.ContentType = "application/json; charset=UTF-8";
                request.Timeout = 10000;
                if (File.Exists(filename))
                {
                    // 续传
                    writeStream = File.OpenWrite(filename);
                    if (writeStream.Length < totalSize)
                    {
                        writeStream.Seek(writeStream.Length, SeekOrigin.Current);
                        request.AddRange((int)writeStream.Length);
                    }
                    else
                    {
                        writeStream.Close();
                        writeStream = new FileStream(filename, FileMode.Create);
                    }
                }
                else
                {
                    writeStream = new FileStream(filename, FileMode.OpenOrCreate);
                }
                long totalDownloadedByte = writeStream.Length;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream readStream = response.GetResponseStream();
                byte[] bt = new byte[1024];
                int currentsize = readStream.Read(bt, 0, bt.Length);
                while (currentsize > 0)
                {
                    totalDownloadedByte += currentsize;
                    writeStream.Write(bt, 0, currentsize);
                    currentsize = readStream.Read(bt, 0, bt.Length);
                    this.EventDownloadIngEventHandler?.Invoke(totalDownloadedByte / 100);
                }
                writeStream.Flush();
                writeStream.Close();
                response.Close();

                readStream.Close();

                this.EventDownloadDoneEventHandler?.Invoke();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        /// <summary>
        /// 更新正在进行时回调
        /// </summary>
        /// <param name="currentfiles"></param>
        private void OnEventUpdateIng()
        {
            this.Invoke(new Action<int>(this.UpdateProgressCurValue));
        }
        /// <summary>
        ///更新开始时回调
        /// </summary>
        /// <param name="fileSum"></param>
        private void OnEventUpdateStart()
        {
            this.progressBar.Style = ProgressBarStyle.Marquee;
        }

        /// <summary>
        /// 更新完成时调用
        /// </summary>
        private void OnEventUpdateDone(Object sender, EventArgs e)
        {
            this.Invoke(new Action(() => { this.progressBar.Value = 100; }));
        }
        /// <summary>
        /// 设置progressBar的最大值
        /// </summary>
        /// <param name="fileSum"></param>
        private void UpdateStrat(int fileSum)
        {
            this.progressBar.Value = fileSum;
        }



        private void Btn_Download_Click(object sender, EventArgs e)
        {

            try
            {
                if (!IsUpdate)
                {
                    MessageBox.Show("没有发现新的版本");
                    //return;
                }
                if (Directory.Exists(Application.StartupPath + "\\" + _temporaryFile))
                {
                    try
                    {
                        Directory.Delete(Application.StartupPath + "\\" + _temporaryFile, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                this.EventDownloadIngEventHandler += OnEventDownloadIng;
                this.EventDownloadStartEventHandler += OnEventDownloadStart;
                this.EventDownloadDoneEventHandler += OnEventDownloadDone;
                //如果版本需要更新就去下载压缩包
                // Task task = new Task(() => { Update(MTotalSize); });
                // task.Start();
                Update(MTotalSize);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public delegate void EventUpdateLoading();
        public delegate void EventUpdateStart();
        public delegate void EventUpdateDone();
        public event EventUpdateLoading EventUpdateLoadingHandler;
        public event EventUpdateStart EventUpdateStartHandler;
        public event EventUpdateDone EventUpdateDoneHandler;
        /// <summary>
        /// 复制更新文件，然后删除下载包
        /// </summary>
        public void App_UpdateFinish(object sender, EventArgs e)
        {
            //获取压缩包的路径
            string dirEcgPath = Application.StartupPath + "\\" + _temporaryFile;
            //如果文件里没有这个路径就创建它
            if (!Directory.Exists(dirEcgPath))
            {
                Directory.CreateDirectory(dirEcgPath);
            }

            if (File.Exists(Application.StartupPath + "\\" + _ZipName))
            {
                Console.WriteLine(Thread.CurrentThread.Name);
                //如果这个路径有这个压缩包,就解压到指定的路径
                ZipFile.ExtractToDirectory(Application.StartupPath + "\\" + _ZipName, dirEcgPath);
                //解压完成删除
                File.Delete(Application.StartupPath + "\\" + _ZipName);
            }

            try
            {
                //获取新的文件
                //DirectoryInfo directoryInfo = new DirectoryInfo(dirEcgPath);
                //int filessum = directoryInfo.GetFiles().Length;
                //this.EventUpdateStartHandler?.Invoke(filessum);
                //int startfile = 0;
                //FileInfo[] files = directoryInfo.GetFiles();
                //foreach (FileInfo fileInfo in files)
                //{
                //    startfile++;
                //    //复制解压的新的文件，复制到你当前的工作目录下                   
                //    File.Copy(fileInfo.FullName, Application.StartupPath + "\\" + fileInfo.Name, true);
                //    this.EventUpdateLoadingHandler?.Invoke(startfile);
                //}
                DirectoryInfo directoryInfo = new DirectoryInfo(dirEcgPath);
                int filessum = directoryInfo.GetFiles().Length;
                string cmd = "";
                if (filessum > 1)
                {
                    cmd = "xcopy /QEY " + dirEcgPath + " " + Application.StartupPath;
                }
                else
                {
                    cmd = "xcopy /QEY " + dirEcgPath + "\\Release " + Application.StartupPath;
                }
                ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + cmd);
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.RedirectStandardError = true;
                procStartInfo.CreateNoWindow = true;
                procStartInfo.UseShellExecute = false;
                Process proc = new Process();
                proc.StartInfo = procStartInfo;
                proc.Exited += OnEventUpdateDone;
                proc.EnableRaisingEvents = true;
                this.EventUpdateLoadingHandler?.Invoke();
                proc.Start();

                //删除原有的文件
                //Directory.Delete(dirEcgPath, true);
                this.EventUpdateDoneHandler?.Invoke();
                //File.Delete(path);
                //////覆盖完成

                ////控制进程的类


                ////用进程对象启动.exe文件
                //System.Diagnostics.FileVersionInfo fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(Application.StartupPath + "\\" + _softName);
                //System.Diagnostics.Process process = new System.Diagnostics.Process();
                //process.StartInfo.FileName = _softName;
                //process.StartInfo.WorkingDirectory = Application.StartupPath;
                //process.StartInfo.CreateNoWindow = true;
                //process.Start();
                //MessageBox.Show("更新成功，已重启程序");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);

            }

        }
        private string _newVerson;
        public void checkVersion()
        {
            try
            {
                // （提供用于将数据发送到由 URI 标识的资源及从这样的资源接收数据的常用方法）
                WebClient wc = new WebClient();

                // OpenRead返回读取资源的流对象Stream
                Stream stream = wc.OpenRead(updateUrl + "Config.xml");

                // 以xml文档的格式去解析
                XmlDocument xmldoc = new XmlDocument();

                // 加载这个流资源
                xmldoc.Load(updateUrl + "Config.xml");

                // 获取xml文档中的update节点数据
                XmlNode list = xmldoc.SelectSingleNode("Update");
                foreach (XmlNode node in list)
                {
                    // 从xml文档中获取Version的值，然后于程序集的版本号比较
                    if (node.Name == "Soft" && node.Attributes["Name"].Value.ToLower() == Executable_Soft.ToLower())
                    {
                        foreach (XmlNode xml in node)
                        {
                            if (xml.Name == "Version")
                            {
                                _newVerson = xml.InnerText;
                            }
                        }
                    }
                }

                System.Diagnostics.FileVersionInfo fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(Application.StartupPath + "\\" + Executable_Soft + ".exe");
                Version ver = new Version(_newVerson);
                Version version = new Version(fv.FileVersion);
                this.CurrentVersion.Text = version.ToString();
                this.newVersion.Text = ver.ToString();
                int tm = version.CompareTo(ver);
                if (tm >= 0)
                {
                    IsUpdate = false;
                }
                else
                {
                    IsUpdate = true;
                }

            }
            catch { }
        }

        public void btn_Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
