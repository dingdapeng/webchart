using Commen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace Chart.ueditor
{
    public partial class SendMsg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


//            {{first.DATA}}
//服务编号：{{keyword1.DATA}}
//服务类型：{{keyword2.DATA}}
//服务内容：{{keyword3.DATA}}
//进度情况：{{keyword4.DATA}}
//{{remark.DATA}}

//在发送时，需要将内容中的参数（{{.DATA}}内为参数）赋值替换为需要的信息
//内容示例
//尊敬的用户您好，您的店铺“XXX”发起的服务申请最新进度如下
//服务编号：201410155283
//服务类型：公共区域维修
//服务内容：楼梯间照明设施维修
//进度情况：已派工
//请点击详细查看，谢谢！

            string FromUserName = Request["FromUserName"];

            string Url = "http://img.mj100.com/weixin/ueditor/readhtml.aspx?key=" + Request["key"];

           System.Data.DataTable dt=  BLL.WebChartBll.GetMessage(Request["key"].ToString());

           string s = WeixinServer.PostMa("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + WeixinServer.Get_Access_token(), new Template.Notice3(FromUserName, Url, "#FF0000", "尊敬的用户您好，你的房屋装修服务最新进度如下", "201410155283", "极客美家装修", "装修", dt.Rows[0]["stage"].ToString(), "点击查看详情").ToString(), Encoding.UTF8);

            bool sensuccuess = Regex.IsMatch(s, "^{\"errcode\":0,\"errmsg\":\"ok\",\"msgid\":\\d+}$");
            if (sensuccuess)
            {
                BLL.WebChartBll.UpMessageOK(Request["key"]);
                Response.Write("<h3 style='color:green'>发送成功</h3><br><br><a style='' href='http://img.mj100.com/weixin/ueditor/MessageList.aspx?phone="+dt.Rows[0]["phone"]+"'>点击返回用户详情页</a>");
            }
            else
            {
                Response.Write("<h3 style='color:green'>发送失败</h3><br><br><a style='' href='http://img.mj100.com/weixin/ueditor/MessageList.aspx?phone=" + dt.Rows[0]["phone"] + "'>点击返回用户详情页</a>");
            }

        }
    }
}