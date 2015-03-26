using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commen
{
    public class TempZJ
    {


        public static bool Add(string number,string phone,string webchartuserid)
        {

            string sql = "select COUNT(*) from TempZj where webchartid='"+webchartuserid+"'";


            if (Convert.ToInt32( SqlHelper.ExecuteScalar(sql))<1)
            {
                SqlHelper.ExecuteScalar("insert TempZj(usernumber, userphone,webchartid) values('"+number+"','"+phone+"','"+webchartuserid+"')");
            }
            else
            {
                //update TempZj set usernumber='' ,userphone='' where webchartid=''
                SqlHelper.ExecuteScalar("update TempZj set usernumber='"+number+"' ,userphone='"+phone+"' where webchartid='"+webchartuserid+"'");
            }
            return true;
        }


    }
}
