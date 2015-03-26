<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="miniLogin.aspx.cs" Inherits="Chart.minimj.miniLogin" %>

<!DOCTYPE html>
<html>
<head>
    <title>极客美家</title>
    <meta name="viewport" id="viewport" content="width=device-width,height=device-height,target-densitydpi=medium-dpi,initial-scale=1.0, maximum-scale=1.0,minimum-scale=1.0,user-scalable=no" />
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.3.2/jquery.mobile-1.3.2.min.css">
    <script src="http://code.jquery.com/jquery-1.8.3.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.3.2/jquery.mobile-1.3.2.min.js"></script>
</head>
<body>

    <div data-role="page" id="pageone" data-transition="slide"> 
        <div data-role="header">
            <div data-role="navbar">
                <ul>
                    <li><a href="#pageone" data-icon="arrow-r" data-theme="e">绑定并登陆</a></li>
                    <li><a href="#pagetwo" data-icon="arrow-r">注册并绑定</a></li>
                    <li><a href="#pagethree" data-icon="arrow-r">忘记密码</a></li>
                </ul>
            </div>
        </div>
        <div data-role="content">
            <form method="post">
                <label for="fname">用户名：</label>
                <input type="text" name="fname" placeholder="请输入用户名" id="fname">
                <label for="fname">密码：</label>
                <input type="password" name="fname" placeholder="请输入密码"  id="Text1">

                <input type="button" id="bddl" data-inline="true" value="绑定并登陆">
            </form>
        </div>
      <%--  <div data-role="footer" data-fullscreen="true" data-position="fixed">
            www.mj100.com 美家无线2012-2014
        </div>--%>
    </div>

    <div data-role="page" id="pagetwo" data-transition="slide">
        <div data-role="header">
            <div data-role="navbar">
                <ul>
                    <li><a href="#pageone" data-icon="arrow-r">绑定并登陆</a></li>
                    <li><a href="#pagetwo" data-icon="arrow-r" data-theme="e">注册并绑定</a></li>
                    <li><a href="#pagethree" data-icon="arrow-r">忘记密码</a></li>
                </ul>
            </div>
        </div>
        <div data-role="content">
            <form method="post">
                <label for="fname">用户名：</label>
                <input type="text" name="fname" id="Text3">
                <label for="fname">密码：</label>
                <input type="text" name="fname" id="Text4">
                <label for="fname">验证码：</label>
                <input type="text" name="fname" id="Text5">
                <img src="yzm.png" alt="点击更新验证码" />
                <input type="button" data-inline="true" value="注册并绑定">
            </form>
        </div>
     <%--   <div data-role="footer" data-fullscreen="true" data-position="fixed">
            www.mj100.com 美家无线2012-2014
        </div>--%>
    </div>

    <div data-role="page" id="pagethree" data-transition="slide">
        <div data-role="header">
            <div data-role="navbar">
                <ul>
                    <li><a href="#pageone" data-icon="arrow-r">绑定并登陆</a></li>
                    <li><a href="#pagetwo" data-icon="arrow-r">注册并绑定</a></li>
                    <li><a href="#pagethree" data-icon="arrow-r" data-theme="e">忘记密码</a></li>
                </ul>
            </div>
        </div>
        <div data-role="content">
            <form method="post">
                <label for="fname">邮箱或手机号：</label>
                <input type="text" name="fname" id="Text6">
                <input type="button" value="发送验证">
                <label for="fname">验证码：</label>
                <input type="text" name="fname" id="Text8">
                <input type="button" value="确定">
            </form>
        </div>
       <%-- <div data-role="footer" data-fullscreen="true" data-position="fixed">
            www.mj100.com 美家无线2012-2014
        </div>--%>
    </div>
</body>
</html>
<script>
    $(function () {
        $("#bddl").click(function () {
            window.location.href = "test.html";
        });
    });
</script>