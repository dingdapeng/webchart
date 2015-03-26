<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="img.aspx.cs" Inherits="Chart.ueditor.img" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
   
    <div style="width:300px;height:300px;margin:0 auto; margin-top:20%">
    <img src="qrcode.aspx?key=<%=Request["key"] %>" />
    </div>
    
</body>
</html>
