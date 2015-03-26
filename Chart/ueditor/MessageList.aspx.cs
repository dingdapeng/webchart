using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Chart.ueditor
{
    public partial class MessageList : System.Web.UI.Page
    {
        protected DataTable dtmessagelist = null;
        
        protected void Page_Load(object sender, EventArgs e)
        {
           
            string phone=Request["phone"];
            
            dtmessagelist = BLL.WebChartBll.GetMessageByPhone(phone);

            //if (dtmessagelist.Rows[0]["ids"]==null||dtmessagelist.Rows[0]["ids"].ToString().Length<1)
            //{
                
            //}

        }
    }
}