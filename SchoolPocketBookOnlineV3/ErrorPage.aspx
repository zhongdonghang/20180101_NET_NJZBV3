<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="SchoolPocketBookWeb.ErrorPage" %>

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
    </script>
</head>
<body>
    <!-- Home -->
    <div data-role="page" id="page1" style="text-align: center">
        <div data-theme="d" data-role="header">
            <h3>
                提示:系统出现异常
            </h3>
        </div>
        <div data-role="content" style="text-align: center">
            <h5>
                对不起，系统暂时不能完成您的操作。
            </h5>
            <h5>
                任何问题请与管理员联系！
            </h5>
            <div style="width: 150px; height: 150px;">
                <img src="Images/smiley.png" alt="image" style="position: absolute; width: 150px;
                    height: 150px" />
            </div>
        </div>
        <div data-theme="d" data-role="footer" data-position="fixed">
            <h3>智佰闻欣图书馆座位在线
            </h3>
        </div>
    </div>
</body>
</html>
