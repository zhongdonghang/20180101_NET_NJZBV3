function TimeVer(startH, startM, endH, endM) {
    var error = "";
    if (startH != "" || startM != "" || endH != "" || endM != "") {
        if (startH == "" || startM == "" || endH == "" || endM == "") {
            error = "时间输入不完整！";
        }
        else {
            if (isNaN(startH) || isNaN(startH) || isNaN(startH) || isNaN(startH)) {
                error = "请输入数字！"
            }
            else {
                if (parseInt(startH) > 23 || parseInt(startH) < 0 || parseInt(startM) > 59 || parseInt(startM) < 0 || parseInt(endH) > 23 || parseInt(endH) < 0 || parseInt(endM) > 59 || parseInt(endM) < 0) {
                    error = "输入的时间超出范围！";
                }
                else {
                    if (parseInt(startH) > parseInt(endH) || (parseInt(startH) == parseInt(endH) && parseInt(startM) >= parseInt(endM))) {
                        error = "起始时间不能大于结束时间！";
                    }
                }
            }
        }
    }
    return error;
}

function LeaveTimeVer(startH, startM, endH, endM, leaveTime) {
    var error = "";
    if (startH != "" || startM != "" || endH != "" || endM != "" || leaveTime != "") {
        if (startH == "" || startM == "" || endH == "" || endM == "") {
            error = "时间输入不完整！";
        }
        else {
            if (isNaN(startH) || isNaN(startH) || isNaN(startH) || isNaN(startH)) {
                error = "请输入数字！"
            }
            else {
                if (parseInt(startH) > 23 || parseInt(startH) < 0 || parseInt(startM) > 59 || parseInt(startM) < 0 || parseInt(endH) > 23 || parseInt(endH) < 0 || parseInt(endM) > 59 || parseInt(endM) < 0) {
                    error = "输入的时间超出范围！";
                }
                else {
                    if (parseInt(startH) > parseInt(endH) || (parseInt(startH) == parseInt(endH) && parseInt(startM) >= parseInt(endM))) {
                        error = "起始时间不能大于结束时间！";
                    }
                }
            }
        }
        error = error + NumberVer(leaveTime, 1);
    }
    return error;
}

function OCTimeVer(startH, startM, beforeTime) {
    var error = "";
    if (startH == "" || startM == "" || beforeTime == "") {
        error = "时间输入不完整！";
    }
    else {
        if (isNaN(startH) || isNaN(startM)) {
            error = "请输入数字！"
        }
        else {
            if (parseInt(startH) > 23 || parseInt(startH) < 0 || parseInt(startM) > 59 || parseInt(startM) < 0) {
                error = "输入的时间超出范围！";
            }
        }
        error = error + NumberVer(beforeTime, 0);
    }
    return error;
}

function OnlyTimeVer(startH, startM) {
    var error = "";
    if (startH != "" || startM != "")
    {
        if (startH == "" || startM == "") {
            error = "时间输入不完整！";
        }
        else {
            if (isNaN(startH) || isNaN(startM)) {
                error = "请输入数字！"
            }
            else {
                if (parseInt(startH) > 23 || parseInt(startH) < 0 || parseInt(startM) > 59 || parseInt(startM) < 0) {
                    error = "输入的时间超出范围！";
                }
            }
        }
    }
    return error;
}

function NumberVerOnlyNum(number) {
    var error = "";
    if (number == "") {
        error = "请不要输入空值!"
    }
    else {
        if (isNaN(number)) {
            error = "请输入数字！"
        }
    }
    return error;
}
function NumberVer(number, startNmum) {
    var error = "";
    if (number == "") {
        error = "请不要输入空值!"
    }
    else {
        if (isNaN(number)) {
            error = "请输入数字！"
        }
        else {
            if (parseInt(number) < parseInt(startNmum))
            { error = "输入的数字最小为" + startNmum }
        }
    }
    return error;
}
function NumberVerMax(number, EndNmum) {
    var error = "";
    if (number == "") {
        error = "请不要输入空值!"
    }
    else {
        if (isNaN(number)) {
            error = "请输入数字！"
        }
        else {
            if (parseInt(number) > parseInt(EndNmum))
            { error = "输入的数字最大为" + EndNmum }
        }
    }
    return error;
}
function SettingVerification() {
    var error = "";
    var result = true;
    for (var i = 0; i < 7; i++) {
        for (var j = 1; j < 4; j++) {
            error = TimeVer($("#SeatSelectAdModeDay" + i + "_Time" + j + "_StartH").val(),
            $("#SeatSelectAdModeDay" + i + "_Time" + j + "_StartM").val(),
            $("#SeatSelectAdModeDay" + i + "_Time" + j + "_EndH").val(),
            $("#SeatSelectAdModeDay" + i + "_Time" + j + "_EndM").val())
            $("#SeatSelectAdModeDay" + i + "_Time" + j + "_Error").attr("title", error);
            if (error != "") {
                $("#SeatSelectAdModeDay" + i + "_Time" + j + "_Error").attr("innerText", "输入错误！");
                result = false;
            }
            else {
                $("#SeatSelectAdModeDay" + i + "_Time" + j + "_Error").attr("innerText", "");
            }
        }
    }

    error = NumberVer($("#ShortLeaveDufaultTime").val(), 1);
    $("#ShortLeaveDufaultTime_Error").attr("title", error);
    if (error != "") {
        $("#ShortLeaveDufaultTime_Error").attr("innerText", "输入错误！");
        result = false;
    }
    else {
        $("#ShortLeaveDufaultTime_Errorr").attr("innerText", "");
    }

    for (var i = 1; i < 3; i++) {
        error = LeaveTimeVer($("#ShortLeaveAdMode_Time" + i + "_StartH").val(),
    $("#ShortLeaveAdMode_Time" + i + "_StartM").val(),
    $("#ShortLeaveAdMode_Time" + i + "_EndH").val(),
    $("#ShortLeaveAdMode_Time" + i + "_EndM").val(),
    $("#ShortLeaveAdMode_Time" + i + "_LeaveTime").val());
        $("#ShortLeaveAdMode_Time" + i + "_Error").attr("title", error);
        if (error != "") {
            $("#ShortLeaveAdMode_Time" + i + "_Error").attr("innerText", "输入错误！");
            result = false;
        }
        else {
            $("#ShortLeaveAdMode_Time" + i + "_Errorr").attr("innerText", "");
        }
    }

    error = NumberVer($("#ShortLeaveByAdmin_LeaveTime").val(), 1);
    $("#ShortLeaveByAdmin_LeaveTime").attr("title", error);
    if (error != "") {
        $("#ShortLeaveByAdmin_Error").attr("innerText", "输入错误！");
        result = false;
    }
    else {
        $("#ShortLeaveByAdmin_Error").attr("innerText", "");
    }

    error = OCTimeVer($("#ReadingRoomDufaultOpenTimeH").val(), $("#ReadingRoomDufaultOpenTimeM").val(), $("#ReadingRoomBeforeOpenTime").val());
    $("#ReadingRoomDufaultOpenTime_Error").attr("title", error);
    if (error != "") {
        $("#ReadingRoomDufaultOpenTime_Error").attr("innerText", "输入错误！");
        result = false;
    }
    else {
        $("#ReadingRoomDufaultOpenTime_Error").attr("innerText", "");
    }

    error = OCTimeVer($("#ReadingRoomDufaultCloseTimeH").val(), $("#ReadingRoomDufaultCloseTimeM").val(), $("#ReadingRoomBeforeCloseTime").val());
    $("#ReadingRoomDufaultCloseTime_Error").attr("title", error);
    if (error != "") {
        $("#ReadingRoomDufaultCloseTime_Error").attr("innerText", "输入错误！");
        result = false;
    }
    else {
        $("#ReadingRoomDufaultCloseTime_Error").attr("innerText", "");
    }

    for (var i = 0; i < 7; i++) {
        for (var j = 1; j < 4; j++) {
            error = TimeVer($("#ReadingRoomAdOpenTime_Day" + i + "_Time" + j + "_OpenH").val(),
            $("#ReadingRoomAdOpenTime_Day" + i + "_Time" + j + "_OpenM").val(),
            $("#ReadingRoomAdOpenTime_Day" + i + "_Time" + j + "_CloseH").val(),
            $("#ReadingRoomAdOpenTime_Day" + i + "_Time" + j + "_CloseM").val())
            $("#ReadingRoomAdOpenTime_Day" + i + "_Time" + j + "_Error").attr("title", error);
            if (error != "") {
                $("#ReadingRoomAdOpenTime_Day" + i + "_Time" + j + "_Error").attr("innerText", "输入错误！");
                result = false;
            }
            else {
                $("#ReadingRoomAdOpenTime_Day" + i + "_Time" + j + "_Error").attr("innerText", "");
            }
        }
    }

    error = NumberVer($("#SeatTime_SeatTime").val(), 1);
    $("#SeatTime_SeatTime_Error").attr("title", error);
    if (error != "") {
        $("#SeatTime_SeatTime_Error").attr("innerText", "输入错误！");
        result = false;
    }
    else {
        $("#SeatTime_SeatTime_Error").attr("innerText", "");
    }

    error = NumberVer($("#SeatTime_ContinueTime_Time").val(), 1);
    $("#SeatTime_ContinueTime_Time").attr("title", error);
    if (error != "") {
        $("#SeatTime_ContinueTime_Time_Error").attr("innerText", "输入错误！");
        result = false;
    }
    else {
        $("#SeatTime_ContinueTime_Time_Error").attr("innerText", "");
    }

    error = NumberVer($("#SeatTime_ContinueTime_ContinueCount").val(), 0);
    error = error += NumberVer($("#SeatTime_ContinueTime_BeforeTime").val(), 1);
    $("#SeatTime_ContinueTime_Error").attr("title", error);
    if (error != "") {
        $("#SeatTime_ContinueTime_Error").attr("innerText", "输入错误！");
        result = false;
    }
    else {
        $("#SeatTime_ContinueTime_Error").attr("innerText", "");
    }

    //for (var i = 0; i < 4; i++) {
    //    error = OnlyTimeVer($("#SeatTime_TimeH_" + i).val(),
    //$("#SeatTime_TimeM_" + i).val());
    //    $("#SeatTime_TimeSpan_" + i + "_Error").attr("title", error);
    //    if (error != "") {
    //        $("#SeatTime_TimeSpan_" + i + "_Error").attr("innerText", "输入错误！");
    //        result = false;
    //    }
    //    else {
    //        $("#SeatTime_TimeSpan_" + i + "_Error").attr("innerText", "");
    //    }
    //}

    error = NumberVer($("#SeatBook_BeforeBookDay").val(), 1);
    $("#SeatBook_BeforeBookDay_Error").attr("title", error);
    if (error != "") {
        $("#SeatBook_BeforeBookDay_Error").attr("innerText", "输入错误！");
        result = false;
    }
    else {
        $("#SeatBook_BeforeBookDay_Error").attr("innerText", "");
    }

    error = TimeVer($("#SeatBook_BookTime_StartH").val(),
            $("#SeatBook_BookTime_StartM").val(),
            $("#SeatBook_BookTime_EndH").val(),
            $("#SeatBook_BookTime_EndM").val())
    $("#SeatBook_BookTime_Error").attr("title", error);
    if (error != "") {
        $("#SeatBook_BookTime_Error").attr("innerText", "输入错误！");
        result = false;
    }
    else {
        $("#SeatBook_BookTime_Error").attr("innerText", "");
    }

    error = NumberVer($("#SeatBook_BespeakSeatCount").val(), 1);
    $("#SeatBook_BespeakSeatCount_Error").attr("title", error);
    if (error != "") {
        $("#SeatBook_BespeakSeatCount_Error").attr("innerText", "输入错误！");
        result = false;
    }
    else {
        $("#SeatBook_BespeakSeatCount_Error").attr("innerText", "");
    }

    error = TimeVer($("#SeatBook_BookTime_StartH").val(),
            $("#SeatBook_BookTime_StartM").val(),
            $("#SeatBook_BookTime_EndH").val(),
            $("#SeatBook_BookTime_EndM").val())
    $("#SeatBook_BookTime_Error").attr("title", error);
    if (error != "") {
        $("#SeatBook_BookTime_Error").attr("innerText", "输入错误！");
        result = false;
    }
    else {
        $("#SeatBook_BookTime_Error").attr("innerText", "");
    }

    error = NumberVer($("#SeatBook_SeatBookRadioPercent_Percent").val(), 1);
    $("#SeatBook_SeatBookRadioPercent_Percent_Error").attr("title", error);
    if (error != "") {
        $("#SeatBook_SeatBookRadioPercent_Percent_Error").attr("innerText", "输入错误！");
        result = false;
    }
    else {
        $("#SeatBook_SeatBookRadioPercent_Percent_Error").attr("innerText", "");
    }

    error = NumberVerOnlyNum($("#SeatBook_SubmitBeforeTime").val());
    error = error + NumberVerOnlyNum($("#SeatBook_SubmitLateTime").val());
    $("#SeatBook_SubmitTime_Error").attr("title", error);
    if (error != "") {
        $("#SeatBook_SubmitTime_Error").attr("innerText", "输入错误！");
        result = false;
    }
    else {
        $("#SeatBook_SubmitTime_Error").attr("innerText", "");
    }
    error = "";
    var datespan = $("#SeatBook_CanNotSeatBookDate").val().split(";");
    for (var i = 0; i < datespan.length; i++) {
        var date = datespan[i].split("~");
        if (date.length == 2) {
            var startdate = date[0].split("-");
            var enddate = date[1].split("-");
            if (startdate.length == 2 && enddate.length == 2) {
                if (parseInt(startdate[0]) < 0 || parseInt(startdate[0]) > 12 || parseInt(enddate[0]) < 0 || parseInt(enddate[0]) > 12 || parseInt(startdate[1]) < 0 || parseInt(startdate[1]) > 31 || parseInt(enddate[1]) < 0 || parseInt(enddate[1]) > 31) {
                    error = "日期格式错误！";
                }
            }
            else {
                error = "日期格式错误！";
            }
        }
        else {
            error = "日期格式错误！";
        }
    }
    $("#SeatBook_CanNotSeatBookDate_Error").attr("title", error);
    if (error != "") {
        $("#SeatBook_CanNotSeatBookDate_Error").attr("innerText", "输入错误！");
        result = false;
    }
    else {
        $("#SeatBook_CanNotSeatBookDate_Error").attr("innerText", "");
    }

    error = NumberVer($("#ShowSeatNumberCount").val(), 1);
    error = NumberVerMax($("#ShowSeatNumberCount").val(), 9);
    $("#ShowSeatNumberCount_Error").attr("title", error);
    if (error != "") {
        $("#ShowSeatNumberCount_Error").attr("innerText", "输入错误！");
        result = false;
    }
    else {
        $("#ShowSeatNumberCount_Error").attr("innerText", "");
    }

    error = NumberVer($("#NoManMode_WaitTime").val(), 1);
    $("#NoManMode_WaitTime_Error").attr("title", error);
    if (error != "") {
        $("#NoManMode_WaitTime_Error").attr("innerText", "输入错误！");
        result = false;
    }
    else {
        $("#NoManMode_WaitTime_Error").attr("innerText", "");
    }

    error = NumberVer($("#RecordViolateCount").val(), 1);
    $("#RecordViolateCount_Error").attr("title", error);
    if (error != "") {
        $("#RecordViolateCount_Error").attr("innerText", "输入错误！");
        result = false;
    }
    else {
        $("#RecordViolateCount_Error").attr("innerText", "");
    }

    error = NumberVer($("#LeaveBlackDays").val(), 1);
    $("#LeaveBlackDays_Error").attr("title", error);
    if (error != "") {
        $("#LeaveBlackDays_Error").attr("innerText", "输入错误！");
        result = false;
    }
    else {
        $("#LeaveBlackDays_Error").attr("innerText", "");
    }

    error = NumberVer($("#LeaveRecordViolateDays").val(), 1);
    $("#LeaveRecordViolateDays_Error").attr("title", error);
    if (error != "") {
        $("#LeaveRecordViolateDays_Error").attr("innerText", "输入错误！");
        result = false;
    }
    else {
        $("#LeaveRecordViolateDays_Error").attr("innerText", "");
    }

    error = NumberVer($("#SelectSeatPosTimes").val(), 1);
    $("#SelectSeatPos_error").attr("title", error);
    if (error != "") {
        $("#SelectSeatPos_error").attr("innerText", "输入错误！");
        result = false;
    }
    else {
        $("#SelectSeatPos_error").attr("innerText", "");
    }

    error = NumberVer($("#SelectSeatPosCount").val(), 1);
    $("#SelectSeatPos_error").attr("title", error);
    if (error != "") {
        $("#SelectSeatPos_error").attr("innerText", "输入错误！");
        result = false;
    }
    else {
        $("#SelectSeatPos_error").attr("innerText", "");
    }

    error = NumberVer($("#NowDayBookTime").val(), 1);
    $("#NowDayBookTime_Error").attr("title", error);
    if (error != "") {
        $("#NowDayBookTime_Error").attr("innerText", "输入错误！");
        result = false;
    }
    else {
        $("#NowDayBookTime_Error").attr("innerText", "");
    }

    if (!result) {
        alert("输入信息有误或格式不正确！");
    }
    return result;
}
