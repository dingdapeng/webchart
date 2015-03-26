using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Chart.ueditor
{
    /// <summary>
    /// save 的摘要说明
    /// </summary>
    public class save : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            Stream s = System.Web.HttpContext.Current.Request.InputStream;
            byte[] b = new byte[s.Length];
            s.Read(b, 0, (int)s.Length);
            string postStr = Encoding.UTF8.GetString(b);

            File.AppendAllText(@"D:\weixin\ueditor\HtmlPage1.html", context.Server.UrlDecode(postStr).Replace("src=\"/ueditor", "src=\"../ueditor"));

           //context.Response.Write(context.Server.UrlDecode(postStr).Replace("src=\"/ueditor", "src=\"../ueditor"));

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}