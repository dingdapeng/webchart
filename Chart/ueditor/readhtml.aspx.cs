using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
namespace Chart.ueditor
{
    public partial class readhtml : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string key=Request["key"];

            DataTable dtmessage=  BLL.WebChartBll.GetMessage(key);


            using (FileStream fs=new FileStream (Server.MapPath("dp.html"),FileMode.Open))
            {
                byte[] buffer =  new byte[fs.Length];

                fs.Read(buffer,0,buffer.Length);

                string s = System.Text.Encoding.UTF8.GetString(buffer,0,buffer.Length).Replace("$time",Convert.ToDateTime(dtmessage.Rows[0]["createtime"]).ToString("yy-MM-dd"));
                if (dtmessage.Rows[0]["remark"]!=null)
                {
                    s = s.Replace("$biaoti", dtmessage.Rows[0]["remark"].ToString());
                }
                else
                {
                    s = s.Replace("$biaoti","极客美家");
                }
              
                Response.Write(s.Replace("$", dtmessage.Rows[0]["content"].ToString()));

                Response.End();
            }
        }
    }
}