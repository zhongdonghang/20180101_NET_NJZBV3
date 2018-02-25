<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookStudyRoom.aspx.cs"
    Inherits="SchoolPocketBookOnlineV2.StudyRoom.BookStudyRoom" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="../Styles/jquery.mobile.min.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.7.2.min.js"></script>
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
        function setPageValue(cmd) {
            $("#subCmd").val(cmd);
        }

        function subLoginOut(cmd) {
            document.getElementById("subCmd").value = cmd;
            form1.submit();
        }
        function DateOnfocus() {
            $(function () {
                var curr = new Date().getFullYear();
                var opt = {
                }
                opt.date = { preset: 'date' };
                opt.datetime = { preset: 'date', minDate: new Date(2012, 3, 10, 9, 22), maxDate: new Date(2020, 30, 15, 44), stepMinute: 5 };

                opt.time = { preset: 'time' };
                $('#hidBookDate').val($('#txtBookDate').val());
                $('#txtBookDate').val($('#hidBookDate').val()).scroller('destroy').scroller($.extend(opt['date'], { theme: 'default', mode: 'scroller', display: 'modal', dayText: '日', monthText: '月', yearText: '年', setText: '确定', cancelText: '取消', dateFormat: 'yy-mm-dd', dateOrder: 'yymmdd', startYear: 2000, endYear: 2100 }));
            });
        }
        function checkDei(eID) {

            if (document.getElementById(eID).checked) {
                document.getElementById("derlist").value += document.getElementById(eID).value + ';';
            }
            else {
                var ls = document.getElementById("derlist").value.split(";");
                document.getElementById("derlist").value = "";
                for (var i = 0; i < ls.length; i++) {
                    if (ls[i] == document.getElementById(eID).value) {
                        continue;
                    }
                    if (ls[i] == "") {
                        continue;
                    }
                    document.getElementById("derlist").value += ls[i] + ";";
                }
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" name="subCmd" id="subCmd" />
    <input type="hidden" name="subBookNo" id="subBookNo" />
    <input type="hidden" name="derlist" id="derlist" runat="server" value="" />
    <asp:HiddenField ID="hidBookDate" runat="server" />
    <div data-role="page" id="page1">
        <div data-theme="d" data-role="header">
            <a data-role="button" onclick="javascript:history.go(-1);" class="ui-btn-left">返回
            </a><a data-role="button" onclick="location.href='../MainFunctionPage.aspx'" class="ui-btn-right">
                功能界面 </a>
            <h3>
                研习间申请
            </h3>
        </div>
        <div>
            <div data-role="content">
                <h4>
                    申请人信息
                </h4>
                <div class="ui-grid-a">
                    <div class="ui-block-a" style="width: 30%; height: 40px; text-align: right; line-height: 0px">
                        <span>
                            <h5>
                                申请人姓名：</h5>
                        </span>
                    </div>
                    <div class="ui-block-b" style="height: 40px; line-height: 0px; width: 70%;">
                        <input id="txtApplicantName" type="text" runat="server" style="height: 12px; min-height: 12px" />
                    </div>
                    <div class="ui-block-a" style="width: 30%; height: 40px; text-align: right; line-height: 0px">
                        <span>
                            <h5>
                                申请人类别：</h5>
                        </span>
                    </div>
                    <div class="ui-block-b" style="height: 40px; line-height: 0px; width: 70%;">
                        <input id="txtApplicantType" type="text" runat="server" style="height: 12px; min-height: 12px" />
                    </div>
                    <div class="ui-block-a" style="width: 30%; height: 40px; text-align: right; line-height: 0px">
                        <span>
                            <h5>
                                证件号：</h5>
                        </span>
                    </div>
                    <div class="ui-block-b" style="height: 40px; line-height: 0px; width: 70%;">
                        <input id="txtApplicantCardNo" type="text" runat="server" style="height: 12px; min-height: 12px" />
                    </div>
                    <div class="ui-block-a" style="width: 30%; height: 40px; text-align: right; line-height: 0px">
                        <span>
                            <h5>
                                申请人单位：</h5>
                        </span>
                    </div>
                    <div class="ui-block-b" style="height: 40px; line-height: 0px; width: 70%;">
                        <input id="txtApplicantDept" type="text" runat="server" style="height: 12px; min-height: 12px" />
                    </div>
                    <div class="ui-block-a" style="width: 30%; height: 40px; text-align: right; line-height: 0px">
                        <span>
                            <h5>
                                联系电话：</h5>
                        </span>
                    </div>
                    <div class="ui-block-b" style="height: 40px; line-height: 0px; width: 70%;">
                        <input id="txtApplicantPhoneNum" type="text" runat="server" style="height: 12px;
                            min-height: 12px" />
                    </div>
                </div>
                <h4>
                    负责人信息
                </h4>
                <div class="ui-grid-a">
                    <div class="ui-block-a" style="width: 30%; height: 40px; text-align: right; line-height: 0px">
                        <span>
                            <h5>
                                负责人姓名：</h5>
                        </span>
                    </div>
                    <div class="ui-block-b" style="height: 40px; line-height: 0px; width: 70%;">
                        <input id="txtHeadPerson" type="text" runat="server" style="height: 12px; min-height: 12px" />
                    </div>
                    <div class="ui-block-a" style="width: 30%; height: 40px; text-align: right; line-height: 0px">
                        <span>
                            <h5>
                                负责人类别：</h5>
                        </span>
                    </div>
                    <div class="ui-block-b" style="height: 40px; line-height: 0px; width: 70%;">
                        <input id="txtHeadPersonType" type="text" runat="server" style="height: 12px; min-height: 12px" />
                    </div>
                    <div class="ui-block-a" style="width: 30%; height: 40px; text-align: right; line-height: 0px">
                        <span>
                            <h5>
                                联系电话：</h5>
                        </span>
                    </div>
                    <div class="ui-block-b" style="height: 40px; line-height: 0px; width: 70%;">
                        <input id="txtHeadPersonPhoneNum" type="text" runat="server" style="height: 12px;
                            min-height: 12px" />
                    </div>
                </div>
                <h4>
                    申请内容
                </h4>
                <div class="ui-grid-a">
                    <div class="ui-block-a" style="width: 30%; height: 40px; text-align: right; line-height: 0px">
                        <span>
                            <h5>
                                会议名称：</h5>
                        </span>
                    </div>
                    <div class="ui-block-b" style="height: 40px; line-height: 0px; width: 70%;">
                        <input id="txtMeetingName" type="text" runat="server" style="height: 12px; min-height: 12px" />
                    </div>
                    <div class="ui-block-a" style="width: 30%; height: 40px; text-align: right; line-height: 0px">
                        <span>
                            <h5>
                                参加人数：</h5>
                        </span>
                    </div>
                    <div class="ui-block-b" style="height: 40px; line-height: 0px; width: 70%;">
                        <input id="txtMembersCount" type="text" runat="server" style="height: 12px; min-height: 12px" />
                    </div>
                    <div class="ui-block-a" style="width: 30%; height: 40px; text-align: right; line-height: 0px">
                        <span>
                            <h5>
                                需求设备：</h5>
                        </span>
                    </div>
                    <div class="ui-block-b" style="width: 70%;">
                        <table id="td">
                            <%=deList %>
                        </table>
                    </div>
                </div>
                <h4>
                    使用时间
                </h4>
                <div class="ui-grid-a">
                    <div class="ui-block-a" style="width: 30%; height: 40px; text-align: right; line-height: 0px">
                        <span>
                            <h5>
                                预约日期：</h5>
                        </span>
                    </div>
                    <div class="ui-block-b" style="height: 40px; line-height: 0px; width: 70%;">
                        <input data-mini="true" name="txtBookDate" id="txtBookDate" type="text" runat="server"
                            style="height: 12px; min-height: 12px" onfocus="DateOnfocus()" />
                    </div>
                    <div class="ui-block-a" style="width: 30%; height: 40px; text-align: right; line-height: 0px">
                        <span>
                            <h5>
                                预约时间：</h5>
                        </span>
                    </div>
                    <div class="ui-block-b" style="height: 40px; line-height: 15px; width: 70%;">
                        <select data-mini="true" id="slbookTime" runat="server" style="height: 30px; min-height: 30px">
                        </select>
                    </div>
                    <div class="ui-block-a" style="width: 30%; height: 40px; text-align: right; line-height: 0px">
                        <span>
                            <h5>
                                使用时长：</h5>
                        </span>
                    </div>
                    <div class="ui-block-b" style="height: 40px; line-height: 0px; width: 70%;">
                        <input id="txtUseTime" type="text" runat="server" style="height: 12px; min-height: 12px" />
                    </div>
                    <div class="ui-block-a" style="width: 30%; height: 40px; text-align: right; line-height: 0px">
                        <span>
                            <h5>
                                通知邮箱：</h5>
                        </span>
                    </div>
                    <div class="ui-block-b" style="height: 40px; line-height: 0px; width: 70%;">
                        <input id="txtEmailAddress" type="text" runat="server" style="height: 12px; min-height: 12px" />
                    </div>
                </div>
                <div class="ui-block-a" style="width: 50%; text-align: center">
                    <input id="btnSubmit" data-inline="true" value="提交" data-mini="true" type="button"
                        onclick="subQuery('query')" runat="server" />
                </div>
                <div class="ui-block-b" style="width: 50%; text-align: center">
                    <input data-inline="true" value="返回" data-mini="true" type="button" onclick="javascript:history.go(-1);" />
                </div>
                <span id="spanWarmInfo" name="spanWarmInfo" runat="server" style="color: Red"></span>
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
