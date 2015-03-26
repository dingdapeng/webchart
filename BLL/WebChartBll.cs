using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BLL
{
    public class WebChartBll
    {


        /// <summary>
        /// 添加预约用户
        /// </summary>
        /// <param name="FromUserName">用户微信标识</param>
        /// <param name="Phone">手机号</param>
        /// <param name="UserName">用户名</param>
        /// <returns></returns>
        public static bool AddUser(string FromUserName, string Phone, string UserName, string District)
        {
            //select count(*) from webchat where FromUserName=@FromUserName and Phone=@Phone


            #region MyRegion
            SqlParameter[] arr = new SqlParameter[]{
            new SqlParameter("@FromUserName",FromUserName),
            new SqlParameter("@Phone",Phone),
            new SqlParameter("@username",UserName),
            new SqlParameter("@District",District)
            };

            object o = SqlHelper.ExecuteScalar("select count(*) from webchat where FromUserName=@FromUserName or Phone=@Phone", arr);
            if (Convert.ToInt32(o) < 1)
            {

                #region MyRegion
                SqlHelper.ExecuteNonQuery("insert into webchat(FromUserName,Phone,username,District) values(@FromUserName,@Phone,@username,@District) select @@identity;", arr);
                return true;
                #endregion
            }
            else
            {
                return false;
            }

            #endregion
        }

        /// <summary>
        /// 更新用户地区
        /// </summary>
        /// <param name="id"></param>
        /// <param name="district"></param>
        /// <returns></returns>
        public static bool UpdateUserD(string id, string district)
        {
            string sql = "update webchat set district=@district where id=@id";

            SqlParameter[] arr = new SqlParameter[] { 
            new SqlParameter("@district",district),
            new SqlParameter("@id",id),
            };
            return SqlHelper.ExecuteNonQuery(sql, arr) > 0;
        }

        /// <summary>
        /// 添加新的推送消息
        /// </summary>
        /// <param name="newid">唯一标识</param>
        /// <param name="content">类容</param>
        /// <param name="mobile">手机</param>
        /// <param name="statge">阶段</param>
        /// <returns></returns>
        public static bool AddMessage(string newid, string content, string phone, string stage, string remark)
        {
            string sql = "insert into webchatmessage(id,content,phone,stage,remark) values (@id,@content,@phone,@stage,@remark)";
            SqlParameter[] arr = new SqlParameter[]{
            new SqlParameter("@id",newid),
            new SqlParameter("@content",content),
            new SqlParameter("@phone",phone),
            new SqlParameter("@stage",stage),
             new SqlParameter("@remark",remark)
            };
            return SqlHelper.ExecuteNonQuery(sql, arr) > 0;
        }

        /// <summary>
        /// 更新为已发送状态
        /// </summary>
        /// <param name="newid"></param>
        /// <returns></returns>
        public static bool UpMessageOK(string newid)
        {
            string sql = "update webchatmessage set isend='1' where id=@id";
            SqlParameter[] arr = new SqlParameter[]{
            new SqlParameter("@id",newid)   
            };
            return SqlHelper.ExecuteNonQuery(sql, arr) > 0;
        }


        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="newid"></param>
        /// <returns></returns>
        public static bool UpMessage(string content, string id, string stage, string remark)
        {
            string sql = "update webchatmessage set stage=@stage,content=@content, remark=@remark where id=@id";
            SqlParameter[] arr = new SqlParameter[]{
            new SqlParameter("@id",id)   ,

            new SqlParameter("@content",content),

            new SqlParameter("@stage",stage),

            new SqlParameter("@remark",remark)
            };
            return SqlHelper.ExecuteNonQuery(sql, arr) > 0;
        }
        /// <summary>
        /// 删除指定消息
        /// </summary>
        /// <param name="newid"></param>
        /// <returns></returns>
        public static bool DeleteMessage(string newid)
        {
            string sql = "delete from webchatmessage where id=@id";
            SqlParameter[] arr = new SqlParameter[]{
            new SqlParameter("@id",newid)   
            };
            return SqlHelper.ExecuteNonQuery(sql, arr) > 0;
        }

        /// <summary>
        /// 某一个具体的消息
        /// </summary>
        /// <param name="newid"></param>
        /// <returns></returns>
        public static DataTable GetMessage(string newid)
        {
            string sql = " select * from   webchatmessage where id=@id   ";
            SqlParameter[] arr = new SqlParameter[]{
            new SqlParameter("@id",newid)   
            };
            return SqlHelper.ExecuteDataTable(sql, arr);

            //有你的幸福
        }


        /// <summary>
        /// 某一个用户的所有消息
        /// </summary>
        /// <param name="newid"></param>
        /// <returns></returns>
        public static DataTable GetMessageByPhone(string phone)
        {
            string sql = " select webchatmessage.createtime as t, webchatmessage.id as ids, * from webchat left join webchatmessage on webchat.Phone=webchatmessage.phone where webchat.phone=@phone  order by webchatmessage.createtime desc; ";
            SqlParameter[] arr = new SqlParameter[]{
            new SqlParameter("@phone",phone)   
            };
            return SqlHelper.ExecuteDataTable(sql, arr);
        }

        /// <summary>
        /// 某一个用户的所有消息
        /// </summary>
        /// <param name="newid"></param>
        /// <returns></returns>
        public static DataTable GetMessageByFromUserName(string FromUserName)
        {
            string sql = " select * from webchat left join webchatmessage on webchat.Phone=webchatmessage.phone where webchat.FromUserName=@FromUserName; ";
            SqlParameter[] arr = new SqlParameter[]{
            new SqlParameter("@FromUserName",FromUserName)   
            };
            return SqlHelper.ExecuteDataTable(sql, arr);
        }

        /// <summary>
        /// 某一个用户的所有消息
        /// </summary>
        /// <param name="newid"></param>
        /// <returns></returns>
        public static string GetMessageByFromUserNameExt(string FromUserName)
        {
            string sql = "   select webchatmessage.createtime, webchatmessage.id,webchatmessage.stage from webchat left join webchatmessage on webchat.Phone=webchatmessage.phone where webchat.FromUserName=@FromUserName and isend='1'; ";
            SqlParameter[] arr = new SqlParameter[]{
            new SqlParameter("@FromUserName",FromUserName)   
            };
            DataTable dt = SqlHelper.ExecuteDataTable(sql, arr);
            //"2015-1-30 <a href='http://mp.weixin.qq.com/s?__biz=MjM5MDA4MTUyMg==&mid=203167435&idx=1&sn=72b4898364451476f0931739820ca9ac#rd'>漆作施工记录</a>"
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["createtime"]!=null)
                {
                    sb.Append(Convert.ToDateTime(dt.Rows[i]["createtime"]).ToString("yy-MM-dd") + "<a href='http://img.mj100.com/weixin/ueditor/readhtml.aspx?key=" + dt.Rows[i]["id"].ToString() + "'>" + dt.Rows[i]["stage"].ToString() + "</a> \n\n"); 
                }
                if (sb.Length==0)
                {
                    sb.Append("尊敬的客户你好，目前管家没有您的相关推送记录，您可以输入 体验管家 进行体验，谢谢您的支持。");
                }
            }
            return sb.ToString();

        }

        public static string GetMessageByFromUserNameExt1(string FromUserName)
        {
            string sql = "   select webchatmessage.createtime, webchatmessage.id,webchatmessage.stage from webchat left join webchatmessage on webchat.Phone=webchatmessage.phone where webchat.FromUserName=@FromUserName and isend='1'; ";
            SqlParameter[] arr = new SqlParameter[]{
            new SqlParameter("@FromUserName",FromUserName)   
            };
            DataTable dt = SqlHelper.ExecuteDataTable(sql, arr);
            //"2015-1-30 <a href='http://mp.weixin.qq.com/s?__biz=MjM5MDA4MTUyMg==&mid=203167435&idx=1&sn=72b4898364451476f0931739820ca9ac#rd'>漆作施工记录</a>"
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["createtime"] != null)
                {
                    sb.Append(Convert.ToDateTime(dt.Rows[i]["createtime"]).ToString("yy-MM-dd") + "<a href='http://img.mj100.com/weixin/ueditor/readhtml.aspx?key=" + dt.Rows[i]["id"].ToString() + "'>" + dt.Rows[i]["stage"].ToString() + "</a> \n\n");
                }
                if (sb.Length == 0)
                {
                    sb.Append("请点击预约量房获取您的专属设计方案");
                }
            }
            return sb.ToString();

        }
        public static string GetMessageByFromUserNameExt0()
        {
            string sql = "   select webchatmessage.createtime, webchatmessage.id,webchatmessage.stage from webchat left join webchatmessage on webchat.Phone=webchatmessage.phone where webchat.FromUserName=@FromUserName and isend='1'; ";
            SqlParameter[] arr = new SqlParameter[]{
            new SqlParameter("@FromUserName","o8r91jv006I_SCONLoJw6eACZEVM")   
            };
            DataTable dt = SqlHelper.ExecuteDataTable(sql, arr);
            //"2015-1-30 <a href='http://mp.weixin.qq.com/s?__biz=MjM5MDA4MTUyMg==&mid=203167435&idx=1&sn=72b4898364451476f0931739820ca9ac#rd'>漆作施工记录</a>"
            StringBuilder sb = new StringBuilder();
            sb.Append("以下是极客美家推送案例体验\n\n");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["createtime"] != null)
                {
                    sb.Append(Convert.ToDateTime(dt.Rows[i]["createtime"]).ToString("yy-MM-dd") + "<a href='http://img.mj100.com/weixin/ueditor/readhtml.aspx?key=" + dt.Rows[i]["id"].ToString() + "'>" + dt.Rows[i]["stage"].ToString() + "</a> \n\n");
                }

            }
            return sb.ToString();

        }
        /// <summary>
        /// 得到所有用户
        /// </summary>
        /// <returns></returns>
        public static DataTable GetUser()
        {

            #region sql语句
            string sql = @" select * from ( 
   select *, ROW_NUMBER() over(order by createtime desc) as num from webchat ) b   
   where b.num>(@pageindex-1)*@pagecount and b.num<=@pageindex*@pagecount 
;";
            #endregion
            SqlParameter[] arr = new SqlParameter[]{
            new SqlParameter("@pageindex",1),
             new SqlParameter("@pagecount",100) 
            };


            return SqlHelper.ExecuteDataTable(sql, arr);
        }

        /// <summary>
        /// 预约量房
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="phone"></param>
        /// <param name="time"></param>
        /// <param name="name"></param>
        public static void MakeAnAppointment(string phone, string name)
        {
            string sql = "insert into Tentent(UserId,Extension1,Extension3,Extension4) values(@UserId,@phone,@time,@name);";
            string userid = "";
            object o = SqlHelper.ExecuteScalar("select UserId from Users where LoginName=@phone or UserMPhone=@phone;", new SqlParameter("@phone", phone));
            if (o != null)
            {
                userid = o.ToString();
            }
            else
            {
                o = SqlHelper.ExecuteScalar("insert into Users(LoginName,UserMPhone)values(@phone,@phone) select @@IDENTITY;", new SqlParameter("@phone", phone));
                userid = o.ToString();
            }
            SqlParameter[] arr = new SqlParameter[] { 
            new SqlParameter("@UserId",userid),
            new SqlParameter("@phone",phone),
            new SqlParameter("@time",DateTime.Now.ToString("yy-MM-dd")),
            new SqlParameter("@name",name)
            };
            SqlHelper.ExecuteNonQuery(sql, arr);

        }


    }
}
