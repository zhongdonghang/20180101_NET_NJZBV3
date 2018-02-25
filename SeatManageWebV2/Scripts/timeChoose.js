  var d=new Date();
  var h=d.getHours();
  var m=d.getMinutes();
 var hzu=h+"时";
var mzu=m+"分";
 var objtt;
document.writeln('<iframe id=setDateLayer frameborder=0  style="height:28px;width:140px;position: absolute;  z-index: 9998; display: none"></iframe>');
		var str_table="";
		str_table=str_table+"<table id='set_shijian' style=\"height:28px;width:140px;z-index:9999;border:#CEEAFD 1px solid;position: absolute; left:0; top:0;\" cellspacing=1 cellpadding=0>"+
			"<tr bgcolor=\"#63A3E9\">"+
				"<td id=\"hour\" onclick=\"parent.SelectHour(this.innerHTML)\" style=\"cursor:hand;width:40px;font-size:12px;\" onmouseover=\"style.backgroundColor='#aaccf3'\" onmouseout=\"style.backgroundColor='#63A3E9'\" align=\"center\">"+h+"时</td>"+
				"<td id=\"min\" onclick=\"parent.SelectMin(this.innerHTML)\" style=\"cursor:hand;width:40px;font-size:12px;\" onmouseover=\"style.backgroundColor='#aaccf3'\" onmouseout=\"style.backgroundColor='#63A3E9'\" align=\"center\">"+m+"分</td>"+
				"<td style=\"cursor:hand;font-size:12px;width:30px;\" onmouseover=\"style.backgroundColor='#aaccf3'\" onmouseout=\"style.backgroundColor='#63A3E9'\"align=\"center\" onclick=\"parent.dangqian()\"> 当前</td>"+
				"<td style=\"cursor:hand;font-size:12px;width:30px;\" onmouseover=\"style.backgroundColor='#aaccf3'\" onmouseout=\"style.backgroundColor='#63A3E9'\"align=\"center\" onclick=\"parent.queding()\"> 确定</td>"+
			"</tr>"+
		"</table>";

window.frames.setDateLayer.document.writeln(str_table);
window.frames.setDateLayer.document.close();      
 
 var fobj = window.frames["setDateLayer"];
 var dads = document.all.setDateLayer.style;
	function setTime(tt){
	objtt=tt;
   		 var th = tt;
   		 var ttop = tt.offsetTop;   

   		 var thei = tt.clientHeight;    
    		var tleft = tt.offsetLeft;    

   		 var ttyp = tt.type;   

   		 while (tt = tt.offsetParent){ttop+=tt.offsetTop; tleft+=tt.offsetLeft;}
    		dads.top = (ttyp=="image") ? ttop+thei : ttop+thei+4;
		
    		dads.left = tleft;
		dads.display = '';
	}
	function queding(){
	var str="";
	var sh;
	var sm;
	var hour=fobj.document.getElementById("hour").innerHTML;
	var min=fobj.document.getElementById("min").innerHTML;
	if(hour != hzu){
		sh=fobj.document.getElementById("SelectHour").value;
		if(sh < 10){
			sh="0"+sh;	
		}
	}
	if(min !=mzu){
		sm=fobj.document.getElementById("SelectMin").value;
		if(sm < 10){
			sm="0"+sm;	
		}
	}
	if(h < 10){
		h="0"+h;
	}
	if(m < 10){
		m="0"+m;
	}
	if(hour == hzu && min == mzu){
		str=str+h+":"+m;
	}else if(hour != hzu && min == mzu){
		str=str+sh+":"+m;
	}else if(hour == hzu && min != mzu){
		str=str+h+":"+sm;
	}else{
		str=str+sh+":"+sm;
	}
	
	objtt.value=str;
	dads.display ='none';
	fobj.document.getElementById("hour").innerHTML=hzu;
	fobj.document.getElementById("min").innerHTML=mzu;
}
	function dangqian(){
		var str="";
		fobj.document.getElementById("hour").innerHTML=hzu;
		fobj.document.getElementById("min").innerHTML=mzu;
		if(h < 10){
			h="0"+h;
		}
		if(m < 10){
			m="0"+m;
		}
		str=str+h+":"+m;
		objtt.value=str;
		dads.display ='none';
}
function document.onclick() 
{ 
    with(window.event)
    {
        if (srcElement != objtt && srcElement != objtt)
         dads.display ='none';
    }
}
function document.onkeyup()        
{
    if (window.event.keyCode==27){
        if(objtt)objtt.blur();
        dads.display ='none';
    }
    else if(document.activeElement)
    {
        if(document.activeElement != objtt && document.activeElement != objtt)
        {
            dads.display ='none';
        }
    }
}

/***** 增加 小时、分钟 ***/
function SelectHour(val) 
{
  
    if(val == hzu){
    	 var selectInnerHTML = "<select id='SelectHour' style='font-size: 12px'>";
    	for (var i = 0; i < 24; i++){
      		selectInnerHTML += "<option value='"+i+"'>"+i+"</option>\n";
    	}
   	selectInnerHTML += "</select>";
    	fobj.document.getElementById("hour").innerHTML=selectInnerHTML;	
    }else{
    	return false;
    }	
}

function SelectMin(val) 
{
    
    if(val == mzu){
    	var selectInnerHTML = "<select id='SelectMin' style='font-size:12px'>";
    	for (var i = 0; i < 60; i++)
    	{
      	selectInnerHTML += "<option value='"+i+"'>"+i+"</option>\n";
    	}
    	selectInnerHTML += "</select>";
    	fobj.document.getElementById("min").innerHTML=selectInnerHTML;      
    }else{
    	return false;
    }
}