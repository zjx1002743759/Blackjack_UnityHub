namespace Blackjack_UnityHub.Helper
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    public class HttpHelper
    {
        public static string HttpGet(string url)
        {
            HttpWebRequest request = null;
            request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

            string retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            myResponseStream.Close();
            myResponseStream.Dispose();
            request.Abort();
            return retString;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            // 总是接受
            return true;
        }

        public static string HttpPost(string url, string postDataStr)
        {
            return HttpPost(url, postDataStr, "txt", string.Empty, "utf-8");
        }

        public static string HttpPostByJson(string url, string postDataStr)
        {
            return HttpPost(url, postDataStr, "json", string.Empty, "utf-8");
        }

        public static string HttpPost(string url, string postDataStr, string postType, string cacert, string chartset)
        {
            // 调用系统回收垃圾
            System.GC.Collect();
            if (url.Contains("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 1000 * 20;
            request.Method = "POST";
            request.Proxy = null;
            request.AllowAutoRedirect = true;
            if (postType == "txt")
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }
            else if (postType == "json")
            {
                request.ContentType = "application/json";
            }
            else if (postType == "html")
            {
                request.ContentType = "text/html";
            }
            else if (postType == "textjson")
            {
                request.ContentType = "text/json";
            }

            if (cacert != string.Empty)
            {
                X509Certificate cert = new System.Security.Cryptography.X509Certificates.X509Certificate(cacert, string.Empty);

                // 设定验证回调(总是同意)
                request.ImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;

                // 把证书添加进http请求中
                request.ClientCertificates.Add(cert);
            }

            try
            {
                byte[] payload = System.Text.Encoding.UTF8.GetBytes(postDataStr);
                request.ContentLength = payload.Length;
                request.ServicePoint.Expect100Continue = false;
                request.GetRequestStream().Write(payload, 0, payload.Length);

                // 响应接收
                HttpWebResponse response;
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                {
                    response = (HttpWebResponse)ex.Response;
                }

                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding(chartset));
                string retString = myStreamReader.ReadToEnd();
                response.Close();
                myStreamReader.Close();
                myResponseStream.Close();
                myResponseStream.Dispose();
                response = null;
                myStreamReader = null;
                myResponseStream = null;
                request.Abort();
                request = null;

                return retString;
            }
            catch (Exception ex)
            {
                request.Abort();
                request = null;
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// Post方式请求接口.
        /// </summary>
        /// <param name="action">请求的方法名.</param>
        /// <param name="dic">请求发送的数据.</param>
        /// <returns>返回字符串.</returns>
        public static string HttpPost(string action, Dictionary<string, string> dic)
        {
            // 此处换为自己的请求url
            string url = action;
            string result = string.Empty;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                {
                    builder.Append("&");
                }

                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }

            byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();

            // 获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }

        public static string PostDataNew(string url, string infor)
        {
            string result = string.Empty;
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "POST";
                request.KeepAlive = true;
                request.AllowAutoRedirect = false;
                request.ContentType = "application/x-www-form-urlencoded";
                byte[] postdatabtyes = Encoding.UTF8.GetBytes(infor);
                request.ContentLength = postdatabtyes.Length;

                // 这个在Post的时候，一定要加上，如果服务器返回错误，他还会继续再去请求，不会使用之前的错误数据，做返回数据
                request.ServicePoint.Expect100Continue = false;
                Stream requeststream = request.GetRequestStream();
                requeststream.Write(postdatabtyes, 0, postdatabtyes.Length);
                requeststream.Close();
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader sr2 = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    string respsr = sr2.ReadToEnd();
                    result = respsr;
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        public static string RequestData(string postURL, string postData)
        {
            // 发送请求的数据
            WebRequest myHttpWebRequest = WebRequest.Create(postURL);
            myHttpWebRequest.Method = "POST";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] byte1 = encoding.GetBytes(postData);
            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            myHttpWebRequest.ContentLength = byte1.Length;

            Stream newStream = myHttpWebRequest.GetRequestStream();
            newStream.Write(byte1, 0, byte1.Length);
            newStream.Close();

            // 发送成功后接收返回的XML信息
            HttpWebResponse response = (HttpWebResponse)myHttpWebRequest.GetResponse();
            Encoding enc = Encoding.GetEncoding("UTF-8");
            Stream stream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream, enc);
            string lcHtml = streamReader.ReadToEnd();
            return lcHtml;
        }
    }
}
