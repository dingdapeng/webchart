using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;//先引入这两个命名空间
using Newtonsoft.Json.Converters;
using System.Net;
namespace Chart
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpWebRequest request = HttpWebRequest.Create("") as HttpWebRequest;
            request.Method = "GET"; 

            




            //string s = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx2b7afab991637512&redirect_uri=http%3a%2f%2fmobile.mj100.com%2fauto.aspx&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect";

            //Response.Write(Server.UrlEncode("http://mobile.mj100.com/auto.aspx"));



            string value = SqlHelper.ExecuteScalar("select Msg from TestWeiXin where id=392").ToString();
            //  Regex reuserdeteils = new Regex("{\"\":\"(.+?)\",\"nickname\":\"(.+?)\",\"sex\":(\\d),\"language\":\"(.+?)\",\"city\":\"(.+?)\",\"province\":\"(.+?)\",\"country\":\"(.+?)\",\"headimgurl\":\"(.{0,}?)\",\"privilege\":\\[(.{0,}?)\\].+}");

            //  Regex reuserdeteils2 = new Regex("{\"openid\":\"(.+?)\",\"nickname\":\"(.+?)\",\"sex\":(\\d),\"language\":\"(.+?)\",\"city\":\"(.+?)\",\"province\":\"(.+?)\",\"country\":\"(.+?)\",\"headimgurl\":\"(.{0,}?)\"");


            object obj = JsonConvert.DeserializeObject(value);

            Newtonsoft.Json.Linq.JObject js = obj as Newtonsoft.Json.Linq.JObject;//把上面的obj转换为 Jobject对象
            string openid = js["openid"].ToString();//取Jtoken对象     通过Jobject的索引获得到
            string nickname = js["nickname"].ToString();//取Jtoken对象     通过Jobject的索引获得到
            string sex = js["sex"].ToString();//取Jtoken对象     通过Jobject的索引获得到
            string language = js["language"].ToString();//取Jtoken对象     通过Jobject的索引获得到
            string city = js["city"].ToString();//取Jtoken对象     通过Jobject的索引获得到
            string province = js["province"].ToString();//取Jtoken对象     通过Jobject的索引获得到
            string country = js["country"].ToString();//取Jtoken对象     通过Jobject的索引获得到
            string headimgurl = js["headimgurl"].ToString();//取Jtoken对象     通过Jobject的索引获得到
            string privilege = js["privilege"] == null ? "" : js["privilege"].ToString();

            Response.Write(openid + "<br/>");
            Response.Write(nickname + "<br/>");
            Response.Write(sex + "<br/>");
            Response.Write(language + "<br/>");
            Response.Write(city + "<br/>");
            Response.Write(province + "<br/>");
            Response.Write(country + "<br/>");
            Response.Write(headimgurl + "<br/>");
            Response.Write(privilege + "<br/>");







        }
    }
}