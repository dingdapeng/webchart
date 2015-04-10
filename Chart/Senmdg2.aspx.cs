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
    public partial class Senmdg2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string chartid = Request["chartid"];
            string phone = Request["phone"];
            string name = Request["name"];

            string s = WeixinServer.PostMa("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + WeixinServer.Get_Access_token(), new Template.Notice4("o8r91jv006I_SCONLoJw6eACZEVM", "http://img.mj100.com/admin", "#FF0000", "666活动链接客户申请", name, phone, "666活动", "上门量房时间待定，请及时联系客户", "无", "暂时没时间做后台无法查看详情，请直接记录用户手机号和用户名").ToString(), Encoding.UTF8);
        }
    }
}