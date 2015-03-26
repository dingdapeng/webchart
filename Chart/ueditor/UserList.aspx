<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="Chart.ueditor.UserList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
     <meta name="viewport" content="width=device-width, initial-scale=1.0,maximum-scale=1.0,user-scalable=0"/>
    <title></title>
    <script src="http://lib.sinaapp.com/js/jquery/1.9.1/jquery-1.9.1.min.js"></script>
    <link rel="stylesheet" href="http://cdn.bootcss.com/bootstrap/3.3.0/css/bootstrap.min.css" />
    <link rel="stylesheet" href="http://cdn.bootcss.com/bootstrap/3.3.0/js/bootstrap.min.js" />
</head>
<body>
    <%--  <ul>

         <%for (int i = 0; i <dtuserlist.Rows.Count; i++)
           {%>

              <li><%=dtuserlist.Rows[i]["username"] %></li>  


          <% } %>
          </ul>--%>

    <table class="table" style="margin-top:100px">
        <thead><tr><td>用户名</td><td>手机号</td><td>地区</td></tr></thead>
        <%for (int i = 0; i <dtuserlist.Rows.Count; i++)
           {%>

              <tr><td><%=dtuserlist.Rows[i]["username"] %></td><td><%=dtuserlist.Rows[i]["phone"] %></td><td><input class="dis" typeid='<%=dtuserlist.Rows[i]["id"] %>'  value='<%=dtuserlist.Rows[i]["district"] %>'/></td><td><a href='MessageList.aspx?phone=<%=dtuserlist.Rows[i]["phone"] %>'>详细</a></td></tr>


          <% } %>
    </table>

</body>
</html>
<script>
    $(function () {

        $(".dis").blur(function () {

            var id = $(this).attr("typeid");

            var district = $(this).val();

            $.post("UpdateUserD.aspx", { id: id, district: district }, function (data) {

            });
            
        });


    });
</script>