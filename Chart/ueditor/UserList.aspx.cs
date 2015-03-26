using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Chart.ueditor
{
    public partial class UserList : System.Web.UI.Page
    {
        protected DataTable dtuserlist = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            dtuserlist = BLL.WebChartBll.GetUser();
        }
    }
}