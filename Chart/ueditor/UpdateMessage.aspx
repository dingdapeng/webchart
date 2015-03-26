<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateMessage.aspx.cs" Inherits="Chart.ueditor.UpdateMessage" %>
 <!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN"
        "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
    <title>完整demo</title>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8"/>
    <script type="text/javascript" charset="utf-8" src="ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="ueditor.all.min.js"> </script>
    <script src="../js/jquery-1.8.3.min.js"></script>
    <!--建议手动加在语言，避免在ie下有时因为加载语言失败导致编辑器加载失败-->
    <!--这里加载的语言文件会覆盖你在配置项目里添加的语言类型，比如你在配置项目里配置的是英文，这里加载的中文，那最后就是中文-->
    <script type="text/javascript" charset="utf-8" src="lang/zh-cn/zh-cn.js"></script>

    <style type="text/css">
        div{
            width:100%;
        }
    </style>
    <link rel="stylesheet" href="http://cdn.bootcss.com/bootstrap/3.3.0/css/bootstrap.min.css">
   
     
</head>
<body>
    <br>
<h1 style=" margin:0 auto; text-align:center" class="text-center text-success"><b>图文消息编辑后台</b></h1>
<hr size="3px">
<div style="margin:10px;">
    <div class="container-fluid">
	<form id="form" role="form" class="form-horizontal">
	    <div class="form-group">
	        <div class="row">
	            <div class="col-md-1"></div>
	            <div class="col-md-9">    
	            	<h4 for="title">文章标题</h4>
	    			<input type="text" id="remark" class="form-control" value='<%=dtmessage.Rows[0]["remark"] %>' style="width:100%"   name="title" placeholder="输入文章标题">
	    		</div>
	            <div class="col-md-2"></div>
	        </div>
             <div class="row">
	            <div class="col-md-1"></div>
	            <div class="col-md-9">    
	            	<h4 for="title">装修阶段（32个阶段）</h4>
	    			<input type="text" id="jieduan" class="form-control" style="width:100%" value='<%=dtmessage.Rows[0]["stage"] %>'  name="title" placeholder="请输入装修阶段">
	    		</div>
	            <div class="col-md-2"></div>
	        </div>
	        <div class="row">
	            <div class="col-md-1"></div>

	            <div class="col-md-9">    
                    <h4>正文  </h4><h5><a>  (请勿发布违反国家政策法规的内容，一经发现立即举报！)</a></h5>
	   				<script id="editor" type="text/plain" style="width:100%;height:480px"></script>	 
	    		</div>
	            <div class="col-md-2"></div>
	        </div>
            
            <div class="row">
            <div class="col-md-1"></div>
            <div class="col-md-4">   
                <br>
                <div class=" form-inline">
                   <%-- <input type="text" class="form-control" style="WIDTH: auto; DISPLAY: inline-block" id="username" name="username" placeholder="文章作者">
                    <input type="email" class="form-control" style="WIDTH: auto; DISPLAY: inline-block" id="email" name="email" placeholder="Email(可不填)">--%>
                </div>
            </div>
            <div class="col-md-7"> </div>
            </div>
		</div>
	 
	<div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-1"><br><a id="cmt" class="btn btn-primary ">提交</a> <%--<button id="preview" class="btn btn-primary ">预览</button>--%></div>
        <div class="col-md-3">
            <div id="pop1" class="alert alert-danger " role="alert" style="display: none;">
                  <p id="msg1"></p>
            </div>
        </div>
        <div class="col-md-7">
    </div>
</div>
<br><br>
   
</div></div>
 <div id="Con" style="display:none"><%=dtmessage.Rows[0]["content"] %></div>
<script type="text/javascript">

    //实例化编辑器
    //建议使用工厂方法getEditor创建和引用编辑器实例，如果在某个闭包下引用该编辑器，直接调用UE.getEditor('editor')就能拿到相关的实例
    var ue = UE.getEditor('editor', {

    });


    function isFocus(e) {
        alert(UE.getEditor('editor').isFocus());
        UE.dom.domUtils.preventDefault(e)
    }
    function setblur(e) {
        UE.getEditor('editor').blur();
        UE.dom.domUtils.preventDefault(e)
    }
    function insertHtml() {
        var value = prompt('插入html代码', '');
        UE.getEditor('editor').execCommand('insertHtml', value)
    }
    function createEditor() {
        enableBtn();
        UE.getEditor('editor');
    }
    function getAllHtml() {
        alert(UE.getEditor('editor').getAllHtml());
        document.write(UE.getEditor('editor').getAllHtml());


    }
    function getContent() {
        var arr = [];
        arr.push("使用editor.getContent()方法可以获得编辑器的内容");
        arr.push("内容为：");
        arr.push(UE.getEditor('editor').getContent());
        alert(arr.join("\n"));
    }
    function getPlainTxt() {
        var arr = [];
        arr.push("使用editor.getPlainTxt()方法可以获得编辑器的带格式的纯文本内容");
        arr.push("内容为：");
        arr.push(UE.getEditor('editor').getPlainTxt());
        alert(arr.join('\n'))
    }
    function setContent(isAppendTo) {
        var arr = [];
        arr.push("使用editor.setContent('欢迎使用ueditor')方法可以设置编辑器的内容");
        UE.getEditor('editor').setContent('欢迎使用ueditor', isAppendTo);
        alert(arr.join("\n"));
    }
    function setDisabled() {
        UE.getEditor('editor').setDisabled('fullscreen');
        disableBtn("enable");
    }

    function setEnabled() {
        UE.getEditor('editor').setEnabled();
        enableBtn();
    }

    function getText() {
        //当你点击按钮时编辑区域已经失去了焦点，如果直接用getText将不会得到内容，所以要在选回来，然后取得内容
        var range = UE.getEditor('editor').selection.getRange();
        range.select();
        var txt = UE.getEditor('editor').selection.getText();
        alert(txt)
    }

    function getContentTxt() {
        var arr = [];
        arr.push("使用editor.getContentTxt()方法可以获得编辑器的纯文本内容");
        arr.push("编辑器的纯文本内容为：");
        arr.push(UE.getEditor('editor').getContentTxt());
        alert(arr.join("\n"));
    }
    function hasContent() {
        var arr = [];
        arr.push("使用editor.hasContents()方法判断编辑器里是否有内容");
        arr.push("判断结果为：");
        arr.push(UE.getEditor('editor').hasContents());
        alert(arr.join("\n"));
    }
    function setFocus() {
        UE.getEditor('editor').focus();
    }
    function deleteEditor() {
        disableBtn();
        UE.getEditor('editor').destroy();
    }
    function disableBtn(str) {
        var div = document.getElementById('btns');
        var btns = UE.dom.domUtils.getElementsByTagName(div, "button");
        for (var i = 0, btn; btn = btns[i++];) {
            if (btn.id == str) {
                UE.dom.domUtils.removeAttributes(btn, ["disabled"]);
            } else {
                btn.setAttribute("disabled", "true");
            }
        }
    }
    function enableBtn() {
        var div = document.getElementById('btns');
        var btns = UE.dom.domUtils.getElementsByTagName(div, "button");
        for (var i = 0, btn; btn = btns[i++];) {
            UE.dom.domUtils.removeAttributes(btn, ["disabled"]);
        }
    }

    function getLocalData() {
        alert(UE.getEditor('editor').execCommand("getlocaldata"));
    }

    function clearLocalData() {
        UE.getEditor('editor').execCommand("clearlocaldata");
        alert("已清空草稿箱")
    }

</script>

    <script type="text/javascript">



        $(function () {

            ue.addListener("ready", function () {
                // editor准备好之后才可以使用
                ue.setContent($("#Con").html());

            });
            $("#cmt").click(function () {
                var content = UE.getEditor('editor').getContent();

                var stage = $("#jieduan").val();

                var remark = $("#remark").val();

                if (stage.length < 1) {
                    alert("装修阶段不能为空");
                    return false;
                }
                if (remark.length < 1) {
                    alert("标题不能为空");
                    return false;
                }


                $.post("up.aspx", { content: content, id: '<%=dtmessage.Rows[0]["id"] %>', stage: stage, remark: remark }, function (data) {

                    location.href = 'MessageList.aspx?phone=<%=dtmessage.Rows[0]["phone"] %>';
                })

                return false;
            });
        });
    </script>
</body>
</html>