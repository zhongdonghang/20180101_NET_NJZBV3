<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Feedback.aspx.cs" Inherits="WeiXinPocketBookOnline.AboutUs.Feedback" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="/Styles/jquery.mobile.min.css" />
    <script type="text/javascript" src="/Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.mobile.min.js"></script>
    <script>
        try {

            $(function () {

            });

        } catch (error) {
            console.error("Your javascript has an error: " + error);
        }
        //提交登录信息
        function subQuery(cmd) {
            document.getElementById("subCmd").value = cmd;
            form1.submit();
        }
        </script>
</head>
<body>
    <!-- Home -->
    <form id="form1" runat="server">
    <input type="hidden" name="subCmd" id="subCmd" />
    <div data-role="page" id="page1">
        <div data-theme="d" data-role="header">
            <a data-role="button" onclick="location.href='../MainFunctionPage.aspx'" class="ui-btn-right">
                功能界面</a><a data-role="button" class="ui-btn-left" onclick="javascript:history.go(-1);">返回</a>
            <h3>
                意见反馈箱
            </h3>
        </div>
        <div data-role="content">
            <h5>
                您好，欢迎您给我们提产品的使用感受和建议！
            </h5>
            <div data-role="fieldcontain">
                <fieldset data-role="controlgroup">
                    <label for="txtFeedback" style="font-size: 10pt">
                        <b>内容：</b>
                    </label>
                    <textarea name="" runat="server" id="txtFeedback" placeholder="" style="height: 200px">
                        </textarea>
                </fieldset>
            </div>
            <div style="text-align: right">
               <%-- <a data-role="button" data-inline="true" data-theme="d" onclick="subQuery('addFeedback')">
                    提交 </a>--%>
                    <a data-role="button" onclick="subQuery('addFeedback')" class="ui-btn-right">提交 </a>
            </div>
            <span id="spanWarmInfo" name="spanWarmInfo" runat="server" style="color: Red"></span>
        </div>
        <div data-theme="d" data-role="footer" data-position="fixed">
            <h3>
                智佰闻欣座位在预约
            </h3>
            <a data-role="button" onclick="subQuery('LoginOut')" class="ui-btn-right">注销 </a>
        </div>
    </div>
    </form>
</body>
</html>
