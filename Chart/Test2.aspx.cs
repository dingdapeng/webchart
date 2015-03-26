using Commen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;//先引入这两个命名空间
using Newtonsoft.Json.Converters;
using System.Data.SqlClient;

namespace Chart
{
    public partial class Test2 : System.Web.UI.Page
    {
        protected string a = WeixinServer.Get_Access_token();
        protected void Page_Load(object sender, EventArgs e)
        {
            //string s=    new Template.Notice1("123456", "http://www.mj100.com", "#FF0000", "尊敬的用户，您好", "您的预约服务进度如下", "海尔冰箱", "调配配件中", "这是一个测试信息").ToString();

            //Response.Write(s);



            //{"touser":"o8r91jjmQWUqO8zrq4rxL0QVTEYs","template_id":"nkLd6D0zQ1OKywLqjk8hnKUhKjo2MpUlqTBKi_n_l2M","url":"http://www.mj100.com","topcolor":"#FF0000","data":{"first":"尊敬的用户，您好","Content1":"您的预约服务进度如下","Good":"海尔冰箱","contentType":"调配配件中","remark":"这是一个测试信息"}}    FPtxn9Pie2u99OTmtAHAETPJ4hb9Bw7b-g6q7FNdjiBPjpC-KEFXOo015BRDRlvin5C_yIcvVVBZQmir4zV3c8JD0_1Awe1HVk5Bhkw-eSw

            //string s=   WeixinServer.PostMa("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + WeixinServer.Get_Access_token(), new Template.Notice2("o8r91jjmQWUqO8zrq4rxL0QVTEYs", "http://www.mj100.com", "#FF0000", "1", "2", "3", "4").ToString(), Encoding.UTF8);


            //https://api.weixin.qq.com/cgi-bin/user/get?access_token=ACCESS_TOKEN


            string s = GetResponseString("https://api.weixin.qq.com/cgi-bin/user/get?access_token="+a);

            object obj = JsonConvert.DeserializeObject(s);//obj   转换json格式的字符串为obj对象

            Newtonsoft.Json.Linq.JObject js = obj as Newtonsoft.Json.Linq.JObject;//把上面的obj转换为 Jobject对象

            Newtonsoft.Json.Linq.JToken model = js["data"];//取Jtoken对象     通过Jobject的索引获得到


            Newtonsoft.Json.Linq.JToken m2 = model["openid"];

            for (int i = 0; i < m2.Count(); i++)
            {

                string value = GetResponseString("https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + a + "&openid=" + m2[i].ToString() + "&lang=zh_CN");

              object obj2 = JsonConvert.DeserializeObject(value);

              Newtonsoft.Json.Linq.JObject js2 = obj2 as Newtonsoft.Json.Linq.JObject;

              string subscribe = js2["subscribe"].ToString();

              string openid = js2["openid"].ToString();

              string nickname = js2["nickname"].ToString();

              string sex = js2["sex"].ToString();

              string language = js2["language"].ToString();

              string city = js2["city"].ToString();

              string province = js2["province"].ToString();

              string country = js2["country"].ToString();

              string headimgurl = js2["headimgurl"].ToString();

              string subscribe_time = js2["subscribe_time"].ToString();
              string sql =@"insert webchatuser ( subscribe, openid, nickname, sex, language1, city, province, country, headimgurl, subscribe_time)
values(@subscribe, @openid, @nickname, @sex, @language1, @city, @province, @country, @headimgurl, @subscribe_time)";

              SqlParameter[] arr = new SqlParameter[] { 
              new SqlParameter("@subscribe",subscribe),
              new SqlParameter("@openid",openid),
              new SqlParameter("@nickname",nickname),
              new SqlParameter("@sex",sex),
              new SqlParameter("@language1",language),
              new SqlParameter("@city",city),
              new SqlParameter("@province",province),
              new SqlParameter("@country",country),
              new SqlParameter("@headimgurl",headimgurl),
              new SqlParameter("@subscribe_time",subscribe_time)
              }
                  
                  ;
              SqlHelper.ExecuteNonQuery(sql,arr);
 
            }
          
        }


        /// <summary>
        /// 售房网下载数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetResponseString(string url)
        {
            string _StrResponse = "";
            HttpWebRequest _WebRequest = (HttpWebRequest)WebRequest.Create(url);
            _WebRequest.UserAgent = "MOZILLA/4.0 (COMPATIBLE; MSIE 7.0; WINDOWS NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";

            _WebRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            _WebRequest.Method = "GET";
            WebResponse _WebResponse = _WebRequest.GetResponse();
            StreamReader _ResponseStream = new StreamReader(_WebResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            _StrResponse = _ResponseStream.ReadToEnd();
            _WebResponse.Close();
            _ResponseStream.Close();
            return _StrResponse;
        }
    }
}