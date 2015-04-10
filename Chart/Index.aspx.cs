using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Commen;
using System.IO;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace Chart
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region   处理请求
            if (Request.HttpMethod.ToUpper() == "GET")//GET
            {
                // 微信加密签名  
                string signature = Request.QueryString["signature"];
                // 时间戳  
                string timestamp = Request.QueryString["timestamp"];
                // 随机数  
                string nonce = Request.QueryString["nonce"];
                // 随机字符串  
                string echostr = Request.QueryString["echostr"];
                if (WeixinServer.CheckSignature(signature, timestamp, nonce))
                {
                    Response.Write(echostr);
                    Response.End();
                }

            }
            else if (Request.HttpMethod.ToUpper() == "POST")
            {
                Stream s = System.Web.HttpContext.Current.Request.InputStream;
                byte[] b = new byte[s.Length];
                s.Read(b, 0, (int)s.Length);
                string postStr = Encoding.UTF8.GetString(b);
                processRequest(postStr);
            }
            #endregion
        }


        /// <summary>
        /// 处理微信发来的请求 
        /// </summary>
        /// <param name="xml"></param>
        public void processRequest(String xml)
        {
            try
            {
                #region 初始化接受到的xml   初始化消息父类
                // xml请求解析  
                Hashtable requestHT = WeixinServer.ParseXml(xml);
                // 发送方帐号（open_id）  
                string fromUserName = (string)requestHT["FromUserName"];
                // 公众帐号  
                string toUserName = (string)requestHT["ToUserName"];
                // 消息类型  
                string msgType = (string)requestHT["MsgType"];

                BaseMessage info = new BaseMessage();
                info.FromUserName = fromUserName;
                info.ToUserName = toUserName;
                info.MsgType = msgType;
                #endregion

                //文字消息
                if (msgType == ReqMsgType.Text)
                {
                    string s = ResponseByText(requestHT["Content"].ToString(), info);
                    Response.Write(s);
                    Response.End();

                    #region 描述

                    //用户在点击按钮后的输入以后   session[用户id]!=null
                    //如果session["用户id"]=="自助服务菜单下"  那么用户所有的输入都是按照   自助服务菜单下做回复  用户切换菜单后session[用户id]="新的用户菜单"
                    //主界面的用户输入最好不要和自定义菜单的输入重复; 这样就可以实现不同菜单下用户输入相同的数字 服务器做出不同的响应 
                    #endregion
                }
                //事件推送
                else if (msgType == ReqMsgType.Event)
                {
                    // 事件类型  
                    String eventType = (string)requestHT["Event"];

                    #region 订阅取消订阅部分
                    // 订阅  
                    if (eventType.ToLower() == ReqEventType.Subscribe)
                    {   //qrscene_123123
                        if (requestHT["EventKey"].ToString().Contains("qrscene_"))//用户未关注情况下
                        {
                            #region MyRegion

                            //List<ArticleEntity> lis = new List<ArticleEntity> { new ArticleEntity { Description = "感谢您关注极客美家服务号！极客美家是集设计、装修、建材、家居领域为一体，提供优质家装配套服务的云装修平台。点击屏幕下方功能按钮，您可以预约量房、查看装修工程进度，还可以申请装修贷款。云中之家，触手可得，极客美家与您一起开启新居工程。了解更多装修知识和优惠活动欢迎关注极客美家订阅号mj100pb。", Title = "极客美家", PicUrl = "http://img.mj100.com/weixin/img/jkmj-1.jpg", Url = "http://www.mj100.com/" } };

                            //  List<ArticleEntity> lis = new List<ArticleEntity> { new ArticleEntity { Description = "极客美家推出698装修套餐活动，设计、施工、主材全包，施工零增项，无任何附加和隐蔽费用。使用十大一线品牌主料和欧洲标准辅材，施工过程中您可以随时随地获得施工现场的图文信息推送。另外您还可以通过美家易贷获得装修贷款。极客美家提供10年维保，保障装修质量。", Title = "活动详情", PicUrl = "http://img.mj100.com/weixin/ueditor/net/upload/image/20150317/6356219888539987504148743.jpg", Url = "http://img.mj100.com/weixin/ueditor/readhtml.aspx?key=48514002-dd7a-461e-9235-33fd6aa843b7" } };
                            // Response.Write(GetresponseNews(info, lis)); 
                            #endregion

                            Response.Write(GetresponseText(info, @"感谢您关注极客美家服务号！极客美家是集设计、装修、建材、家居领域为一体，提供优质家装配套服务的云装修平台。点击屏幕下方功能按钮，您可以预约量房、查看装修工程进度，还可以申请装修贷款。
【中铁建小区用户】请回复中铁建＋您的姓名+手机号  
【参与美家计划698活动】请回复698+您的姓名+手机号+新居所在城市
设计师会在第一时间与您取得联系"));
                            Response.End();


                        }
                        else
                        {
                            #region MyRegion
                            //  List<ArticleEntity> lis = new List<ArticleEntity> { new ArticleEntity { Description = "极客美家推出698装修套餐活动，设计、施工、主材全包，施工零增项，无任何附加和隐蔽费用。使用十大一线品牌主料和欧洲标准辅材，施工过程中您可以随时随地获得施工现场的图文信息推送。另外您还可以通过美家易贷获得装修贷款。极客美家提供10年维保，保障装修质量。", Title = "活动详情", PicUrl = "http://img.mj100.com/weixin/ueditor/net/upload/image/20150317/6356219888539987504148743.jpg", Url = "http://img.mj100.com/weixin/ueditor/readhtml.aspx?key=48514002-dd7a-461e-9235-33fd6aa843b7" } };
                            // Response.Write(GetresponseNews(info, lis)); 
                            #endregion


                            Response.Write(GetresponseText(info, @"感谢您关注极客美家服务号！极客美家是集设计、装修、建材、家居领域为一体，提供优质家装配套服务的云装修平台。点击屏幕下方功能按钮，您可以预约量房、查看装修工程进度，还可以申请装修贷款。
【中铁建小区用户】请回复中铁建＋您的姓名+手机号  
【参与美家计划698活动】请回复698+您的姓名+手机号+新居所在城市
设计师会在第一时间与您取得联系"));
                            Response.End();

                        }

                    }
                    // 取消订阅  
                    else if (eventType.ToLower() == ReqEventType.Unsubscribe)
                    {
                        // TODO 取消订阅后用户再收不到公众号发送的消息，因此不需要回复消息  
                    }
                    #endregion

                    //自定义菜单点击事件  
                    else if (eventType.ToLower() == ReqEventType.CLICK)
                    {
                        #region 描述
                        //这里可能要记录每次用户的点击 用用户的appid做key  EventKey作为值
                        //根据 EventKey　判断是点的哪个按钮　，　然后返回什么样的数据 ，　比如说用户点的是查询装修进度 ，　返回请输入进度用户名 ，　则可返回
                        //多少个key值全列出来 每个值下面做不同的处理 
                        /* {
     "button":[
     {	
         
          "name":"美家计划",
         "sub_button":[
            {
               "type":"click",
               "name":"积分商城",
               "key":"jfsc"
            },
            {
               "type":"click",
               "name":"建材折扣",
               "key":"jczk"
            },
            {
               "type":"click",
               "name":"最新活动",
               "key":"wqhg"
            }
            ]
      },
      {
           "name":"自助服务",
          "sub_button":[
            {
               "type":"click",
               "name":"小美装家",
               "key":"jfcx"
            },
            {
               "type":"click",
               "name":"云装修平台",
               "key":"sgys"
            },
            {
               "type":"click",
               "name":"我的订单",
               "key":"wddd"
            },
            {
               "type":"click",
               "name":"施工管理",
               "key":"sgppp"
            },
            {
               "type":"click",
               "name":"实时验收",
               "key":"sgppp"
            }
            ]
      },
      {
           "name":"关于我们",
           "sub_button":[
            {
               "type":"click",
               "name":"美家易贷",
               "key":"gywm"
            },
            {
               "type":"click",
               "name":"小美管家",
               "key":"fqf"
            },
            {
               "type":"click",
               "name":"极客美家",
               "key":"zxb"
            }
            ]
       }]
 } */
                        #endregion
                        Response.Write(ResponseByClick((string)requestHT["EventKey"], info));
                        Response.End();
                    }
                    else if (eventType.ToLower() == "scan")//用户关注过后扫描二维码事件
                    {
                        Response.Write(ResponseByClick((string)requestHT["EventKey"], info));
                        Response.End();
                    }
                    else if (eventType.ToLower() == ReqEventType.view)
                    {

                        Response.Write(GetresponseText(info, "你要跳转的链接是：" + (string)requestHT["EventKey"]));
                        Response.End();
                    }
                }
                //位置消息
                else if (msgType.ToLower() == ReqMsgType.Location)
                {

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 响应文本信息
        /// </summary>
        /// <param name="info">基类</param>
        /// <param name="content">返回的内容</param>
        /// <returns></returns>
        public string GetresponseText(BaseMessage info, string content)
        {

            ResponseText model = new ResponseText(info);
            model.Content = content;

            return model.ToXml();
        }


        /// <summary>
        /// 转接到客服
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string GotoKefu(BaseMessage info)
        {
            string to = info.ToUserName;
            string fr = info.FromUserName;

            info.ToUserName = fr;
            info.FromUserName = to;

            info.MsgType = "transfer_customer_service";

            return info.ToXml();

        }


        /// <summary>
        /// 响应图文信息
        /// </summary>
        /// <param name="info">基类</param>
        /// <param name="list">返回的图文注意不能超过十个</param>
        /// <returns></returns>
        public string GetresponseNews(BaseMessage info, List<ArticleEntity> list)
        {

            ResponseNews model = new ResponseNews(info);
            model.Articles.AddRange(list);
            return model.ToXml();
        }

        #region 一个根据需求出文本  一个根据需求出articles集合  暂时没用到
        /// <summary>
        /// 返回图文根据不同的信息返回图文
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        public List<ArticleEntity> GetArticleEntitys(Hashtable ht)
        {
            return null;
        }

        /// <summary>
        /// 返回图文根据不同的信息返回文本信息
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        public string GetresponseText(Hashtable ht)
        {
            return null;
        }
        #endregion


        /// <summary>
        /// 根据不同的EventKey做出不同的响应
        /// </summary>
        /// <param name="EventKey">自定义事件key</param>
        /// <returns></returns>
        public string ResponseByClick(string EventKey, BaseMessage info)
        {

            string res = "";
            switch (EventKey)
            {
                #region MyRegion
                case "zxb":// 关于我们 极客美家点击
                    List<ArticleEntity> lis = new List<ArticleEntity> { new ArticleEntity { Description = "极客美家是专门为您提供家装服务本地化解决方案的电子商务平台，在我们打造的云装修世界里：我们是您的设计师，是您的建材商，是您的监理工，为您的家装尽我的全力。家装无忧，在这里成为可能。", Title = "极客美家", PicUrl = "http://223.4.236.5/weixin/img/mj.png", Url = "http://www.mj100.com/" } };
                    res = GetresponseNews(info, lis);
                    break;
                case "wqhg":
                    res = GetresponseText(info, "现在没有最新活动哦");
                    break;
                case "kffw":
                    res = GotoKefu(info);
                    break;
                case "1":
                    res = GetresponseText(info, "您好，感谢您一直以来对小美家的关注，“有奖问卷”活动开始啦，只需几分钟，轻松赢话费，<a href='http://www.wenjuan.com/s/vm6jmy'>我要参加</a>");
                    break;
                case "888":
                    res = GetresponseText(info, "您好，感谢您一直以来对小美家的关注，“有奖问卷”活动开始啦，只需几分钟，轻松赢话费，<a href='http://www.wenjuan.com/s/UfmuIz'>我要参加</a>");
                    break;
                case "qrscene_888":
                    res = GetresponseText(info, "您好，欢迎来到小美家做客，只需几分钟，轻松赢话费，<a href='http://www.wenjuan.com/s/UfmuIz'>我要参加</a>");
                    break;
                case "qrscene_1":
                    res = GetresponseText(info, "您好，欢迎来到小美家做客，只需几分钟，轻松赢话费，<a href='http://www.wenjuan.com/s/vm6jmy'>我要参加</a>");
                    break;
                case "2":
                    res = GetresponseText(info, "您好，感谢您一直以来对小美家的关注，“有奖问卷”活动开始啦，只需几分钟，轻松赢话费，<a href='http://www.wenjuan.com/s/j6ZvMz'>我要参加</a>");
                    break;
                case "qrscene_2":
                    res = GetresponseText(info, "您好，欢迎来到小美家做客，只需几分钟，轻松赢话费，<a href='http://www.wenjuan.com/s/j6ZvMz'>我要参加</a>");
                    break;
                case "zxhd":
                    res = GetresponseText(info, "1.百名天使用户征集活动开始啦,想免费装修吗，快来参与吧\n\n活动网址：http://ap.mj100.com/  \n\n<a href='https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx2c2f2e7b5b62daa1&redirect_uri=http%3a%2f%2fmobile.mj100.com%2fLuckdraw%2fauthorize.aspx&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect'>也可以点此参与</a> ");
                    break;
                case "hdyg":
                    res = GetresponseText(info, "1.百名天使用户征集活动开始啦,想免费装修吗，快来参与吧\n\n活动网址：http://ap.mj100.com/  \n\n<a href='https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx2c2f2e7b5b62daa1&redirect_uri=http%3a%2f%2fmobile.mj100.com%2fLuckdraw%2fauthorize.aspx&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect'>也可以点此参与</a> ");
                    break;
                case "xyx":
                    res = GetresponseText(info, "<a href='http://mobile.mj100.com/demo/demo1/se.htm'>1.看看我有多色</a>\n\n<a href='http://mobile.mj100.com/demo/demo1/0.htm'>2.看看我前世是什么</a>\n\n<a href='http://mobile.mj100.com/demo/demo1/2048.htm'>3.挑战2048</a>");
                    break;
                #endregion

                #region 按钮点击
                case "yylf":
                    res = GetresponseText(info, "回复：姓名+手机号+所在城市 预约量房，我们会致电与您确定量房时间和地点。");
                    break;
                case "gj":
                    res = GetresponseText(info, BLL.WebChartBll.GetMessageByFromUserNameExt(info.FromUserName));
                    break;
                case "wdfa":
                    #region 我的方案
                    //List<ArticleEntity> lis2 = new List<ArticleEntity> { new ArticleEntity { Description = "点击可查看方案图文详情", Title = "方案图文详情", PicUrl = "http://img.mj100.com/weixin/img/%E7%8E%8B%E5%BB%B7%E4%BA%AE%E8%AE%BE%E8%AE%A1.jpg", Url = "http://img.mj100.com/weixin/test.html" } };
                    //res = GetresponseNews(info, lis2); 
                    res = GetresponseText(info, BLL.WebChartBll.GetMessageByFromUserNameExt1(info.FromUserName));

                    #endregion
                    break;
                case "ghxj":
                    res = GetresponseText(info, "新居规划功能开发中，如有问题，回复 客服 即可联系客服。\n\n也可以点击\n www.mj100.com \n进入官网规划新居");
                    break;
                case "sjs":
                    res = GetresponseText(info, @"Q：你们是谁（极客美家是什么）
A：我们是极客美家，是一家提供设计方案、建材选购、工程施工、项目监理、装修贷款等全部装修服务的本地化电子商务网站。
在极客美家您可以享受到价格透明、工程优质的配套装修服务。    
<a href='http://img.mj100.com/weixin/ueditor/readhtml.aspx?key=7b0e2393-202d-447e-ab73-d33793baa8f9'>点击查看更多常见问题</a>
没有你想要的问题？回复 客服 二字，即可联系客服解答
 ");
                    break;
                case "jqhd":
                    List<ArticleEntity> lishd = new List<ArticleEntity> { new ArticleEntity { Description = "极客美家推出698装修套餐活动，设计、施工、主材全包，施工零增项，无任何附加和隐蔽费用。使用十大一线品牌主料和欧洲标准辅材，施工过程中您可以随时随地获得施工现场的图文信息推送。另外您还可以通过美家易贷获得装修贷款。极客美家提供10年维保，保障装修质量。", Title = "活动详情", PicUrl = "http://img.mj100.com/weixin/ueditor/net/upload/image/20150317/6356219888539987504148743.jpg", Url = "http://img.mj100.com/weixin/ueditor/readhtml.aspx?key=48514002-dd7a-461e-9235-33fd6aa843b7" } };
                    res = GetresponseNews(info, lishd);
                    break;

                case "cxyd":
                    #region 查询易贷
                    List<ArticleEntity> lisyd = new List<ArticleEntity> { new ArticleEntity { Description = "极客美家联合各大银行推出家装贷款产品，额度高达50万、固定利率10.8%无任何手续费、还款周期长、最快两天放款，真正满足您的装修资金需求。回复工资流水单、社保证明及公积金证明的清晰照片即可申请家装贷款。为了您能快速申请到款项，请确保照片内容的真实性，您的个人信息仅作贷款申请使用，不会以任何形式泄露给其他人员和机构。", Title = "美家易贷", PicUrl = "http://img.mj100.com/weixin/img/yd.jpg", Url = "http://img.mj100.com/weixin/ueditor/readhtml.aspx?key=52f98347-6f86-42e1-8165-19b6c4ad4828" } };
                    res = GetresponseNews(info, lisyd);
                    #endregion
                    break;
                case "ydjsq":
                    res = GetresponseText(info, " <a href='http://img.mj100.com/weixin/minimj/jsq.aspx'>点击进入易贷计算器</a>");
                    break;

                default:
                    res = GotoKefu(info);
                    break;
                #endregion
            }
            return res;
        }


        /// <summary>
        /// 根据不同的文本消息做出不同的响应
        /// </summary>
        /// <param name="EventKey">自定义事件key</param>
        /// <returns></returns>
        public string ResponseByText(string Text, BaseMessage info)
        {

            //【中铁建小区用户】请回复中铁建＋您的姓名+手机号
            //【参与美家计划698活动】请回复698+您的姓名+手机号
            //设计师会在第一时间与您取得联系


            string res = "";

            #region 易贷
            Regex ydre = new Regex("易贷\\d{11}");
            if (ydre.IsMatch(Text))
            {
                res = GotoKefu(info);
                return res;
            } 
            #endregion

            #region 预约的情况

            Regex ztj = new Regex("中铁建(.*)(\\d{11})");
            if (ztj.IsMatch(Text))
            {
                Match m = ztj.Match(Text);

                bool addb = BLL.WebChartBll.AddUser(info.FromUserName, m.Groups[2].Value, m.Groups[1].Value, "中铁建");
                if (addb)
                {
                    res = GetresponseText(info, "我们已收到您的量房申请，稍后会致电您预留的电话" + m.Groups[2].Value + "，与您确定量房时间和地点,请您保持电话畅通。");

                    string s = WeixinServer.PostMa("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + WeixinServer.Get_Access_token(), new Template.Notice4("o8r91jv006I_SCONLoJw6eACZEVM", "http://img.mj100.com/weixin/ueditor/userlist.aspx", "#FF0000", "Tony,你好,有一个中铁建的顾客预约量房，请注意安排时间", m.Groups[1].Value, m.Groups[2].Value, "中铁建上门量房", "上门量房时间待定，请及时联系中铁建客户", "无", "点击查看详情").ToString(), Encoding.UTF8);

                    return res;
                }
            }

            Regex ljb = new Regex("^698(.+)(\\d{11})(.+)?$");
            if (ljb.IsMatch(Text) && !Text.Contains("易贷"))
            {
                Match m = ljb.Match(Text);

                bool addb = BLL.WebChartBll.AddUser(info.FromUserName, m.Groups[2].Value, m.Groups[1].Value,"698活动："+ m.Groups[3].Value);
                if (addb)
                {
                    res = GetresponseText(info, "我们已收到您的量房申请，稍后会致电您预留的电话" + m.Groups[2].Value + "，与您确定量房时间和地点,请您保持电话畅通。");

                    string s = WeixinServer.PostMa("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + WeixinServer.Get_Access_token(), new Template.Notice4("o8r91jv006I_SCONLoJw6eACZEVM", "http://img.mj100.com/weixin/ueditor/userlist.aspx", "#FF0000", "Tony,你好,有一个顾客预约量房，参与698活动，请注意安排时间", m.Groups[1].Value, m.Groups[2].Value, "698活动上门量房", "上门量房时间待定，请及时联系客户", "无", "点击查看详情").ToString(), Encoding.UTF8);

                    return res;
                }

            }


            Regex reyy = new Regex(@"^(.+)(\d{11})(.+)?$", RegexOptions.Singleline);
            if (reyy.IsMatch(Text) && !Text.Contains("易贷"))
            {
                Match m = reyy.Match(Text);

                bool addb = BLL.WebChartBll.AddUser(info.FromUserName, m.Groups[2].Value, m.Groups[1].Value, m.Groups[3].Value);
                if (addb)
                {
                    res = GetresponseText(info, "我们已收到您的量房申请，稍后会致电您预留的电话" + m.Groups[2].Value + "，与您确定量房时间和地点,请您保持电话畅通。");

                    string s = WeixinServer.PostMa("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + WeixinServer.Get_Access_token(), new Template.Notice4("o8r91jv006I_SCONLoJw6eACZEVM", "http://img.mj100.com/weixin/ueditor/userlist.aspx", "#FF0000", "Tony,你好,有一个顾客预约量房，请注意安排时间", m.Groups[1].Value, m.Groups[2].Value, "上门量房", "上门量房时间待定，请及时联系客户", "无", "点击查看详情").ToString(), Encoding.UTF8);

                    return res;
                }

            }

            #endregion



            switch (Text)
            {
                #region MyRegion
                case "2":
                    res = GetresponseText(info, "小美为了鞭策自己减肥，将每日体重做了Excel表格，生成走势图。今天，同事经过小美座位。只见他走过去，又若有所思地倒了回来，悄悄问：“那个，能不能透露一下，你买的哪支股票啊？走势这么好啊……”当时小美泪奔了;");
                    break;
                case "1":
                    res = GetresponseText(info, "家装小妙招：一些门窗由于制作和安装质量不良，缝隙不严，冷天透风，夏天穿热，门窗装上“密封条”，节能又隔音;");
                    break;
                case "3":
                    res = GetresponseText(info, "<a href='http://mobile.mj100.com/demo/demo1/se.htm'>1.看看我有多色</a>\n\n<a href='http://mobile.mj100.com/demo/demo1/0.htm'>2.看看我前世是什么</a>\n\n<a href='http://mobile.mj100.com/demo/demo1/2048.htm'>3.挑战2048</a>");
                    break;
                case "客服":
                    res = GotoKefu(info);
                    break;
                case "体验管家":
                    List<ArticleEntity> lishd = new List<ArticleEntity> { new ArticleEntity { Description = @"工程进度：油工中期
客户名称：X先生
项目地址： 海淀区XXX A-B-XXXX
2015.1.30您家的油工中期完毕了~

1月31日将进行热水器安装，2月1日将进行铝扣板安装。您家新居的漆作阶段将于2月1日完成。如有任何疑问，请语音/文字回复或联系您的小美管家（季建生先生，电话：13146109935）。 
期待已久？小美马上上图~~", Title = "2015 01 30 您的漆作工程施工节点推送", PicUrl = "http://img.mj100.com/weixin/ueditor/net/upload/image/20150313/6356186523821237508619902.jpg", Url = "http://img.mj100.com/weixin/ueditor/readhtml.aspx?key=6e1066ff-21fc-4f35-83b2-76a597af6d68" } };
                    res = GetresponseNews(info, lishd);

                    break;
                case "dingdapeng":
                    string s = WeixinServer.PostMa("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + WeixinServer.Get_Access_token(), new Template.Notice1(info.FromUserName, "http://www.mj100.com", "#FF0000", "尊敬的用户，您好", "您的预约服务进度如下", "极客美家装修服务", "装修进度提醒", "这是一个测试信息").ToString(), Encoding.UTF8);
                    res = GetresponseText(info, s);
                    break;
                case "易贷":
                    res = GetresponseText(info, @"Q：什么是美家易贷？
A：美家易贷是极客美家与多家银行强强联手为极客美家用户量身定制的家装消费贷款分期产品。利率低、门槛低、效率高，30分钟内完成审核，无需抵押和担保，通过率远高于银行等金融机构。。    
<a href='http://img.mj100.com/weixin/ueditor/readhtml.aspx?key=ed344ddb-8ac2-4ca4-b657-7004962b3288'>点击查看更多易贷常见问题</a>
没有你想要的问答？回复 客服 二字，即可联系客服解答
 ");
                    break;
                #endregion
                default:
                    //res = GetresponseText(info, "查看家装小妙招，请戳1，查看小美糗事，请戳2，玩小游戏请戳3 \n\n实时查询装修动态请点击自助服务下的[实时验收]菜单\n\n");

                    res = GotoKefu(info);
                    break;
            }
            return res;
        }
    }



}