<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jsq.aspx.cs" Inherits="Chart.minimj.jsq" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0,maximum-scale=1.0,user-scalable=0"/>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../js/jquery-1.8.3.min.js"></script>
    <link rel="stylesheet" href="http://cdn.bootcss.com/bootstrap/3.3.0/css/bootstrap.min.css" />
    <link rel="stylesheet" href="http://cdn.bootcss.com/bootstrap/3.3.0/js/bootstrap.min.js" />
    <style>
        ul {
            list-style:none;
            margin:0;
            padding:0;
            
            margin:0 auto;
            text-align:center

        }
            ul li {
                margin-top:15px
            }
                ul li select {
                    width:60%;
                    margin-left:10px
                }
    </style>
</head>
<body>
    <ul class="unstyled">
						<li class="o">			
							  <h3>信贷计算器</h3>
						</li>
       
						<li class="t tt">
							 您的月薪
							<select id="salily">
								<option value="">请选填</option>
								<option value="3-5">3k-5k</option>
								<option value="5-8">5k-8k</option>
								<option value="8-10">8k-10k</option>
								<option value="10-15">10k-15k</option>
								<option value="15-15">15k以上</option>
							</select>
						</li>
       
						<li class="t tt">
							 您的房产
							<select  id="slhouse">
								<option value="">请选填</option>
								<option value="50000">50万-100万</option>
								<option value="100000">100万-200万</option>
								<option value="150000">200万以上</option>
							</select>

						</li>
         
						<li class="t">
							 社保流水
							<select   id="slsocial">
								<option value="">请选填</option>
								<option value="2-4">2k-4k</option>
								<option value="4-6">4k-6k</option>
								<option value="6-8">6k-8k</option>
								<option value="8-10">8k-10k</option>
								<option value="10-12">10k-12k</option>
								<option value="12-14">12k-14k</option>
								<option value="14-16">14k-16k</option>
								<option value="16-18">16k-18k</option>
								<option value="18-20">18k-20k</option>
								<option value="20-20">20k以上</option>
							</select>
						</li>

						<li class="th">
							<button id="btcount" type="button" class="cur_hover">预估一下</button>
						</li>
                       <li class="o">			
							  <h3 id="show" style="color:green"></h3>
						</li>
					</ul>
   
</body>
</html>
<script>

    $(function () {
        $("#btcount").click(function () {
            var s1 = $("#salily").val();
            var s2 = $("#slhouse").val();
            var s3 = $("#slsocial").val();

            if (s1.length>0&&s2.length>0&&s3.length>0) {

                var t1 = parseInt(s1.split("-")[0]) * 1000;
              
                var t2 = parseInt(s2);

                var t3 = parseInt(s3.split("-")[1]) * 1000;

                var q1 = t1 + t2;

                var q2 = t2 + t3;

                if (q1<q2) {
                    $("#show").html("根据计算你可贷款额大约是：" + (t1 + t2) + "~" + (t2 + t3) + " 元");
                } else {
                    $("#show").html("根据计算你可贷款额大约是：" + q2 + "~" + q1 + " 元");
                }
                return false;
            }

            $("#show").html("");
        });
    });
</script>