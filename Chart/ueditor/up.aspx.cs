using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chart.ueditor
{
    public partial class up : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string input = Request["content"];
            Regex re = new Regex("<img src=\"(.*?)\".*?>", RegexOptions.Singleline);

            MatchCollection col = re.Matches(input);
            foreach (Match item in col)
            {
                string key = item.Groups[0].Value;
                string s = Regex.Replace(key, "<img src=\"(.*?)\".*?>", "<a href='$1'><img dp src='$1'/></a>");
                input = input.Replace(key, s);
            }
            BLL.WebChartBll.UpMessage( input, Request["id"], Request["stage"], Request["remark"]);
        }
    }
}