using Commen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chart
{
    public partial class SenMsg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string chartid = Request["chartid"];
            string phone=Request["phone"];
            //o8r91jv0dXNAa5fbQYKKPQcJ0IhI  万妮
            //o8r91jnVTVThw7TN1jfiqhYuOUiY  王清
            string s = WeixinServer.PostMa("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + WeixinServer.Get_Access_token(), new Template.Notice4("o8r91jv0dXNAa5fbQYKKPQcJ0IhI", "http://img.mj100.com/admin", "#FF0000", "万妮,你好,有一个顾客预约量房，请注意安排时间", "姓名未知", phone, "上门量房", "上门量房时间待定，请及时联系客户", "无", "点击查看详情").ToString(), Encoding.UTF8);
            WeixinServer.PostMa("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + WeixinServer.Get_Access_token(), new Template.Notice4("o8r91jnVTVThw7TN1jfiqhYuOUiY", "http://img.mj100.com/admin", "#FF0000", "王清,你好,有一个顾客预约量房，请注意安排时间", "姓名未知", phone, "上门量房", "上门量房时间待定，请及时联系客户", "无", "点击查看详情").ToString(), Encoding.UTF8);
            Response.Write(s);
        }
    }
}