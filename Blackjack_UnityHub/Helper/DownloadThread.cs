namespace Blackjack_UnityHub.Helper
{
    using System;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;

    internal class DownloadThread
    {
        public delegate void EventDownloadStart(long totalSize);

        public delegate void EventDownloadIng(long curDownloadSize);

        public delegate void EventCheckingMd5();

        public delegate void EventDownloadDone();

        public event EventDownloadStart EventDownloadStartEventHandler;

        public event EventDownloadIng EventDownloadIngEventHandler;

        public event EventCheckingMd5 EventCheckingMd5EventHandler;

        public event EventDownloadDone EventDownloadDoneEventHandler;

        public static string GetMD5FromFile(string filename)
        {
            try
            {
                FileStream fs = new FileStream(filename, FileMode.Open);
                MD5CryptoServiceProvider md5Helper = new MD5CryptoServiceProvider();
                byte[] data = md5Helper.ComputeHash(fs);
                fs.Close();
                StringBuilder sbr = new StringBuilder();
                for (int i = 0; i < data.Length; ++i)
                {
                    sbr.Append(data[i].ToString("X2"));
                }

                string md5Str = sbr.ToString();
                return md5Str;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RunMethod(string url, string filename, long totalSize, string md5)
        {
            try
            {
                Stream st;
                Stream so;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 10000;

                // 通知主界面,我开始了, response.ContentLength用来设置进度条的最大值
                this.EventDownloadStartEventHandler(totalSize);

                if (File.Exists(filename))
                {
                    // 续传
                    so = File.OpenWrite(filename);
                    if (so.Length < totalSize)
                    {
                        so.Seek(so.Length, SeekOrigin.Current);
                        request.AddRange((int)so.Length);
                    }
                    else
                    {
                        so.Close();
                        so = new FileStream(filename, FileMode.Create);
                    }
                }
                else
                {
                    so = new FileStream(filename, FileMode.Create);
                }

                long totalDownloadedByte = so.Length;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                st = response.GetResponseStream();

                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte += osize;
                    so.Write(by, 0, osize);
                    osize = st.Read(by, 0, by.Length);

                    // 通知主界面我正在执行,totalDownloadedByte表示进度条当前进度
                    // PS: 除以一千是因为文件太大int类型放不下
                    this.EventDownloadIngEventHandler(totalDownloadedByte / 1000);
                }

                so.Close();
                st.Close();

                this.EventCheckingMd5EventHandler();
                if (GetMD5FromFile(filename) != md5)
                {
                    // md5校验不通过，重新传
                    this.RunMethod(url, filename, totalSize, md5);
                }
                else
                {
                    // 通知主界面我已经完成了
                    this.EventDownloadDoneEventHandler();
                }
                this.EventDownloadDoneEventHandler();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
