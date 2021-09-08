using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_UnityHub.Helper
{
    public class RemoteFileHelper
    {
        public string connectState(string path, string userName, string passWord)
        {
            bool Flag = false;
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                //登录验证
                string dosLine = @"net use " + path + " " + passWord + " /User:domain\\" + userName;
                proc.StandardInput.WriteLine("net use * /del /y");
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (!proc.HasExited)
                {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();
                if (string.IsNullOrEmpty(errormsg))
                {
                    Flag = true;
                }
                else
                {
                    throw new Exception(errormsg);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw ex;
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return Flag.ToString();
        }

        //获得指定路径下所有文件名
        public List<FileNames> getFileName(List<FileNames> list, string filepath)
        {
            DirectoryInfo root = new DirectoryInfo(filepath);
            foreach (FileInfo f in root.GetFiles())
            {
                list.Add(new FileNames
                {
                    Name = f.Name.Substring(0,f.Name.LastIndexOf(".")),
                    FileName = f.Name,
                    LastesUpdatte=f.LastWriteTime,
                    FileBype = f.Length
                });
            }
            return list;
        }

        //获得指定路径下的所有子目录名
        // <param name="list">文件列表</param>
        // <param name="path">文件夹路径</param>
        public List<FileNames> GetallDirectory(List<FileNames> list, string path)
        {
            DirectoryInfo root = new DirectoryInfo(path);
            var dirs = root.GetDirectories();
            if (dirs.Count() != 0)
            {
                foreach (DirectoryInfo d in dirs)
                {
                    list.Add(new FileNames
                    {
                        FileName = d.Name,
                        LastesUpdatte = d.LastWriteTime,
                       
                        Children = GetallDirectory(new List<FileNames>(), d.FullName)
                    });
                }
            }
            list = getFileName(list, path);
            return list;
        }

    }
    public class FileNames
    {
        public string FileName { get; set; }
        public string Name { get; set; }
        public List<FileNames> Children { get; set; }
        public string IsFile { get; set; }
        public long FileBype { get; set; }
        public DateTime LastesUpdatte { get; set; }

    }
}
