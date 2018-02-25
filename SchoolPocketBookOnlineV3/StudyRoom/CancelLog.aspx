<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CancelLog.aspx.cs" Inherits="SchoolPocketBookWeb.StudyRoom.CancelLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="../Styles/jquery.mobile.min.css" />
    <script type="text/javascript" src="./Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.mobile.min.js"></script>
    <!--Includes-->
    <link href="../Styles/mobiscroll.custom-2.4.4.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/mobiscroll.custom-2.4.4.min.js" type="text/javascript"></script>
    <script defer="defer">
        try {

            $(function () {

            });

        } catch (error) {
            console.error("Your javascript has an error: " + error);
        }
        function subQuery(cmd) {
            document.getElementById("subCmd").value = cmd;
            form1.submit();
        }
      
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" name="subCmd" id="subCmd" />
    <asp:HiddenField ID="hidBookDate" runat="server" />
    <div data-role="page" id="page1">
        <div data-theme="d" data-role="header">
            <a data-role="button" onclick="javascript:history.go(-1);" class="ui-btn-left">返回
            </a><a data-role="button" onclick="location.href='../MainFunctionPage.aspx'" class="ui-btn-right">
                功能界面 </a>
            <h3>
                取消申请
            </h3>
        </div>
        <div>
            <div data-role="content">
                <div class="ui-grid-a">
                    <div class="ui-block-a" style="width: 30%; height: 100px; text-align: right; line-height: 0px">
                        <span>
                            <h5>
                                取消原因：</h5>
                        </span>
                    </div>
                    <div class="ui-block-b" style="height: 100px; line-height: 0px; width: 70%;">
                        <textarea cols="20" rows="4" id="txtRemark" type="text" runat="server" style="height: 80px;"></textarea>
                    </div>
                </div>
                <div class="ui-block-a" style="width: 100%; text-align: center">
                    <div class="ui-block-a" style="width: 50%; text-align: center">
                        <input id="btnSubmit" data-inline="true" value="提交" data-mini="true" type="button"
                            onclick="subQuery('cancel')" runat="server" />
                    </div>
                    <div class="ui-block-b" style="width: 50%; text-align: center">
                        <input data-inline="true" value="返回" data-mini="true" type="button" onclick="javascript:history.go(-1);" />
                    </div>
                </div>
            </div>
            <div data-theme="d" data-role="footer" data-position="fixed">
                <h5>智佰闻欣图书馆座位在线
                </h5>
                <%--<a data-role="button" class="ui-btn-right" onclick="subQuery('LoginOut')">注销</a>--%>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
