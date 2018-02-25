<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SyncReader.aspx.cs" Inherits="SeatManageWebV2.FunctionPages.RegulationRulesSetting.SyncReader" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
    <script type="text/javascript">
        function sync() {
            $("#div_load").css("display", "block");
            $("#btnSync").attr("disabled", "true");

            $.ajax({ //一个Ajax过程 
                type: "post", //使用get方法访问后台
                dataType: "text", //返回json格式的数据
                // dataType: "text",
                url: "SyncReader.aspx", //要访问的后台地址
                data: { "param": "Sync" }, //要发送的数据

                // complete: function () { $("#load").hide(); }, //AJAX请求完成时隐藏loading提示
                success: function (msg) {//msg为返回的数据，在这里做数据绑定
                    msgHandle(msg);

                },
                error: function () {
                    //alert("error");
                }
            }); 
        }

        function StopSync() {
            $.ajax({ //一个Ajax过程 
                type: "post", //使用get方法访问后台
                dataType: "text", //返回json格式的数据
                // dataType: "text",
                url: "SyncReader.aspx", //要访问的后台地址
                data: { "param": "Stop" }, //要发送的数据

                // complete: function () { $("#load").hide(); }, //AJAX请求完成时隐藏loading提示
                success: function (msg) {//msg为返回的数据，在这里做数据绑定 
                    msgHandle(msg);
                    //$("#lab_state").text(msg);
                },
                error: function () {
                    //alert("error");
                }
            }); 
        }

        function msgHandle(msg) {
            arr = msg.split(":");
            if (arr.length > 1) {
                //如果第一位为1，说明正在同步
                if (arr[0] == "1") {
                    $("#lab_state").text(arr[1]);
                }
                //第一位为2，说明同步结束
                else if (arr[0] == "2") {
                    clearInterval(interval);
                    $("#lab_state").text(arr[1]);
                     StopSync();
                    $("#imgLoad").css("display", "none");
                } else if (arr[0] == "3") {
                    clearInterval(interval);
                    $("#lab_state").text(arr[1]);
                     StopSync();
                    $("#imgLoad").css("display", "none");
                    $("#btnSync").attr("disabled", "false");
                }
            } else { 
                $("#lab_state").text(arr[0]);
            }

        }
        var interval;
        function intervalRun(cmd) {

            interval = setInterval(sync, '1000');
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <div id="div_load" style="display: none;">
        <table width="320" height="72" border="1" bordercolor="#cccccc" cellpadding="5" cellspacing="1"
            class="font" style="filter: Alpha(opacity=80); width: 320px; height: 72px">
            <tr>
                <td>
                    <p>
                        <img id="imgLoad" alt="请等待" src="../../Images/icon/clocks.gif" align="left">
                        <br>
                        <span id="lab_state"></span>
                    </p>
                </td>
            </tr>
        </table>
    </div>
    <input type="button" value="开始同步" id="btnSync" onclick="intervalRun()" enableviewstate="false" />
    <br>
    <asp:Label ID="lab_jg" runat="server"></asp:Label>
    </form>
</body>
</html>
