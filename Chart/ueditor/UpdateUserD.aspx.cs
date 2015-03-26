using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chart.ueditor
{
    public partial class UpdateUserD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.WebChartBll.UpdateUserD(Request["id"], Request["district"]);
        }
    }
}