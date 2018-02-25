//透明层


var divTop;
var divleft;
function ThumbnailClick(objThunbnail, event, scaleX, scaleY, moveX, moveY) {
    //event.stopEvent();

    //半透明div移动的距离
    var divTransparent = document.getElementById("divTransparent");
    divTop = getY(objThunbnail, event) - divTransparent.offsetHeight / 2;
    divleft = getX(objThunbnail, event) - divTransparent.offsetWidth / 2;
    divTransparent.style.top = divTop + 'px';
    divTransparent.style.left = divleft + 'px';

    var seatLayoutTop, seatLayoutLeft;
    seatLayoutTop = -((divTop - moveY) * scaleY) + "px";
    seatLayoutLeft = -((divleft - moveX) * scaleX) + "px";
    $("#divSeatLayout").css("top", seatLayoutTop);
    $("#divSeatLayout").css("left", seatLayoutLeft);
}
//座位管理处理窗口
function seatClick(urlParameters) {
    if (urlParameters == "" || urlParameters == NaN) {
        return;
    }
    X("seatHandleWindow").box_show("../SeatMonitor/SeatHandle.aspx" + urlParameters, '座位操作');
}
//座位预约确认窗口
function BespeakSeatClick(urlParameters) {
    if (urlParameters == "" || urlParameters == NaN) {
        return;
    }
    X("seatBespeakWindow").box_show("../SeatBespeak/BespeakSubmitWindow.aspx?parameters=" + urlParameters, '座位预约');
}
//座位预约确认窗口(当天)
function BespeakSeatNowDayClick(urlParameters) {
    if (urlParameters == "" || urlParameters == NaN) {
        return;
    }
    X("bespeakHandleWindow").box_show("../SeatBespeak/BespeakNowDayHandle.aspx?parameters=" + urlParameters, '座位预约');
}
//预约座位设置窗口
function BespeakSeatSettingClick(seatNo, urlParameters) {
    if (urlParameters == "" || urlParameters == NaN) {
        return;
    }
    X("seatBespeakSettingWindow").box_show("BespeakSeatSettingWindow.aspx?" + urlParameters, '预约座位设置');
    //var seatStatus = $("#seatStatus").val();
    //nobook表示座位未设置预约，canbook表示当前座位已设置预约
//    if (seatStatus == "nobook" || seatStatus == "" || seatStatus == NaN) {
//        $("#subCmd").val('setBook');
//        $("#seatNo").val(seatNo);
//        form1.submit();
//    }
//    else {
//        $("#subCmd").val('setNoBook');
//        $("#seatNo").val(seatNo);
//        form1.submit();
//    }
}

function loadSeatLayout() {
    var roomNum = $("#hiddenRoomNum").val()
    $.ajax({ //一个Ajax过程 
        type: "post", //使用get方法访问后台
        dataType: "html", //返回json格式的数据
        // dataType: "text",
        url: "SeatLayout.ashx", //要访问的后台地址
        data: { "roomNum": roomNum, "divTransparentTop": divTop, "divTransparentLeft": divleft }, //要发送的数据

        // complete: function () { $("#load").hide(); }, //AJAX请求完成时隐藏loading提示
        success: function (msg) {//msg为返回的数据，在这里做数据绑定
            $("#divSeatGraphMain").html(msg);

        },
        error: function () {
            //alert("error");
        }
    });
}
function loadBespeakSeatLayout() {
    var roomNum = $("#hiddenRoomNum").val();
    var bespeakDate = $("#hiddenDate").val();

    $.ajax({ //一个Ajax过程 
        type: "post", //使用get方法访问后台
        dataType: "html", //返回json格式的数据 
        url: "SeatLayoutHandle.ashx", //要访问的后台地址
        data: { "roomNum": roomNum, "date": bespeakDate, "divTransparentTop": divTop, "divTransparentLeft": divleft }, //要发送的数据

        // complete: function () { $("#load").hide(); }, //AJAX请求完成时隐藏loading提示
        success: function (msg) {//msg为返回的数据，在这里做数据绑定
            $("#divSeatGraphMain").html(msg);

        },
        error: function () {
            //alert("error");
        }
    });

}
function loadBespeakSeatNowDayLayout() {
    var roomNum = $("#hiddenRoomNum").val();

    $.ajax({ //一个Ajax过程 
        type: "post", //使用get方法访问后台
        dataType: "html", //返回json格式的数据 
        url: "NowBespeakSeatLayout.ashx", //要访问的后台地址
        data: { "roomNum": roomNum, "divTransparentTop": divTop, "divTransparentLeft": divleft }, //要发送的数据

        // complete: function () { $("#load").hide(); }, //AJAX请求完成时隐藏loading提示
        success: function (msg) {//msg为返回的数据，在这里做数据绑定
            $("#divSeatGraphMain").html(msg);

        },
        error: function () {
            //alert("error");
        }
    });

}
function loadBespeakSeatSettingLayout() {
    var roomNum = $("#hiddenRoomNum").val();
    $.ajax({ //一个Ajax过程 
        type: "post", //使用get方法访问后台
        dataType: "html", //返回json格式的数据 
        url: "BespeakSeatGraph.ashx", //要访问的后台地址
        data: { "roomNum": roomNum, "divTransparentTop": divTop, "divTransparentLeft": divleft }, //要发送的数据

        // complete: function () { $("#load").hide(); }, //AJAX请求完成时隐藏loading提示
        success: function (msg) {//msg为返回的数据，在这里做数据绑定
            $("#divSeatGraphMain").html(msg);

        },
        error: function () {
            //alert("error");
        }
    });

}
//获取鼠标点击时先对于点击对象的X坐标
function getX(obj, event) {
    var newleft = $(obj).offset().left;
    newleft = (event.clientX - newleft - 8 + document.body.scrollLeft);
    return newleft;
}
//获取鼠标点击时相对于点击对象的Y坐标
function getY(obj, event) {
    var newtop = $(obj).offset().top;
    newtop = (event.clientY - newtop + document.body.scrollLeft);
    return newtop;
}

function tipShow(object, tipContent) {

    var actualLeft = $(object).offset().left;
    var actualTop = $(object).offset().top;

    actualTop = actualTop + 42;
    actualLeft = actualLeft + 10;
    $("#bub_box").css("top", actualTop + "px");
    $("#bub_box").css("left", actualLeft + "px");
    $("#bub_box").css("display", "block");
    $("#bub_Content").html(tipContent);
}


function tipShowPad(object, tipContent) {

    var actualLeft = $(object).offset().left;
    var actualTop = $(object).offset().top;

    actualTop = actualTop + 42;
    if (($(window).width() - actualLeft) < 150) {

        if (object.className != "RealSeatFree") {
            actualLeft = actualLeft - 80;
            $("#bub_JanTou").css("left", "120px");
        }
        else {
            actualLeft = actualLeft + 10;
            $("#bub_JanTou").css("left", "15px");
        }
    }
    else {
        actualLeft = actualLeft + 10;
        $("#bub_JanTou").css("left", "15px");
    }
    $("#bub_box").css("top", actualTop + "px");
    $("#bub_box").css("left", actualLeft + "px");
    $("#bub_box").css("display", "block");
    $("#bub_Content").html(tipContent);
}
function tipHidden(clickObject) {
    $("#bub_box").css("display", "none");
}

/*
* 座位操作
*/
function showDiv(showDiv, hiddenDiv) {
    $("#" + showDiv).css("display", "block");
    $("#" + hiddenDiv).css("display", "none");
    return false;
}

function hiddenAll(showDiv, hiddenDiv) {
    $("#" + showDiv).css("display", "none");
    $("#" + hiddenDiv).css("display", "none");
    return false;
}