<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageList.aspx.cs" Inherits="Chart.ueditor.MessageList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0,maximum-scale=1.0,user-scalable=0"/>
    <title></title>
    <link rel="stylesheet" href="http://cdn.bootcss.com/bootstrap/3.3.0/css/bootstrap.min.css" />
    <link rel="stylesheet" href="http://cdn.bootcss.com/bootstrap/3.3.0/js/bootstrap.min.js" />
</head>
<body>
    <h3 style="margin-left:50px"><%=dtmessagelist.Rows[0]["username"] %>&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;手机号为：<%=dtmessagelist.Rows[0]["phone"] %></h3>
    <a class="btn btn-primary" style="margin: 50px" href="bj.aspx?phone=<%=Request["phone"] %>">新增推送消息</a>
    
    <table class="table" style="margin-top: 100px">
        <thead>
            <tr>
                <td>编号</td>
                <td>阶段</td>
                <td></td>
                <td></td>
            </tr>
        </thead>
        <%for (int i = 0; i < dtmessagelist.Rows.Count; i++)
          {
              bool flag = dtmessagelist.Rows[i]["isend"].ToString() == "0" ? true : false;
        %>

        <tr <%=dtmessagelist.Rows[i]["ids"]==null||dtmessagelist.Rows[i]["ids"].ToString().Length==0?"style='display:none'":"" %>>
            <td><%=i+1 %></td>
            <td><%=dtmessagelist.Rows[i]["stage"] %></td>
            <td></td>
            <td>
                <%if (flag)
                  { %>
                <a href='SendMsg.aspx?FromUserName=<%=dtmessagelist.Rows[i]["FromUserName"] %>&key=<%=dtmessagelist.Rows[i]["ids"] %>'>发送</a>
                <%}
                  else
                  {%>
                已发送
                <%} %>
            </td>
            <td><a href='readhtml.aspx?key=<%=dtmessagelist.Rows[i]["ids"] %>'>预览</a>  &nbsp;&nbsp; <a target="_blank" href='img.aspx?key=<%=dtmessagelist.Rows[i]["ids"] %>'> 二维码预览</a></td>
            <td><a href='UpdateMessage.aspx?key=<%=dtmessagelist.Rows[i]["ids"] %>'>修改</a></td>
        </tr>


        <% } %>
    </table>
</body>
</html>
