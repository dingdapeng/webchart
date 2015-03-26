using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chart.ueditor
{
    public partial class UpdateMessage : System.Web.UI.Page
    {
        protected DataTable dtmessage = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string key = Request["key"];

            dtmessage = BLL.WebChartBll.GetMessage(key);
        }
    }
}