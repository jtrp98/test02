

//SETTING UP OUR POPUP
//0 means disabled(ซ่อน); 1 means enabled(แสดง);
var popupStatus = 0;

//loading popup with jQuery magic!
function loadPopup(backgroundPopup,PopUpID){
	//loads popup only if it is disabled
	if(popupStatus==0){
		jQuery("#"+backgroundPopup).css({
			"opacity": "0.7","height":"100%","width":"100%","right":"0"

		});
		jQuery("#"+backgroundPopup).fadeIn("def");
		jQuery("#"+PopUpID).fadeIn("def");
		popupStatus = 1;
	}
}

//disabling popup with jQuery magic!
function disablePopup(backgroundPopup,PopUpID){
	//disables popup only if it is enabled
	if(popupStatus==1){
		jQuery("#"+backgroundPopup).fadeOut("def");
		jQuery("#"+PopUpID).fadeOut("def");
		popupStatus = 0;
	}
}

//centering popup
function centerPopup(backgroundPopup,PopUpID){
	//request data for centering
	var windowWidth = document.documentElement.clientWidth;
	var windowHeight = document.documentElement.clientHeight;
	var popupHeight = jQuery("#"+PopUpID).height();
	var popupWidth = jQuery("#"+PopUpID).width();
	//centering
	jQuery("#"+PopUpID).css({
		"position": "absolute",
		"top": windowHeight/2-popupHeight/2,
		"left": windowWidth/2-popupWidth/2
	});
	//only need force for IE6
	
	jQuery("#"+backgroundPopup).css({
		"height": windowHeight
	});
	
}

//check ข้อความใน txtdata ก่อน ว่ามีมั้ย ถ้ามีใส่เครื่องหมายถูก ถ้าไม่มี ไม่ต้องใส่เครื่องหมาย
function checkData(Ctrl_PicID,Ctrl_ValueID,Crlt_DelID){
	if(document.getElementById( Ctrl_ValueID ).value == '')
    {   document.getElementById(Ctrl_PicID ).innerHTML ='<img src=../images/ic_check0.gif  width=19 height=19 style=vertical-align:middle title=ยังไม่มีการบันทึกข้อมูล >'; 
        //document.getElementById(Ctrl_PicID ).title='ยังไม่มีการบันทึกข้อมูล';document.getElementById(Ctrl_PicID ).tipsy({gravity: 's'});
        document.getElementById(Crlt_DelID ).style.visibility = 'hidden';
    }
    else
    {
        document.getElementById(Ctrl_PicID).innerHTML ='<img src=../images/ic_check1.gif  width=19 height=19 style=vertical-align:middle title=มีการบันทึกข้อมูลแล้ว>';
        //document.getElementById(Ctrl_PicID ).title='มีการบันทึกข้อมูลแล้ว';document.getElementById(Ctrl_PicID ).tipsy({gravity: 's'});
        document.getElementById(Crlt_DelID).style.visibility = 'visible';
    }
}

//ส่วนการช่วยแสดง pop up โดย จะแสดงแต่ตัวที่มีไอดีเท่ากับยที่ส่งมาเท่าน่ะ
function DisplayDIV(main_DIVID,DIVID){
    var divname='';
	var inputs = document.getElementById(main_DIVID).getElementsByTagName('div');

			for(k = 0; k < inputs.length; k++){
			
				if(inputs[k].id!='') 
				{
				    if(inputs[k].id == DIVID) 
				    {
				    
    				    document.getElementById(inputs[k].id).style.display ='';
    				    //alert(inputs[k].id+'=='+DIVID);
				    }
				    else
				    {
				      document.getElementById(inputs[k].id).style.display ='none'; 
				      //alert(inputs[k].id+'!='+DIVID); 
				    }
				}
			}

}
