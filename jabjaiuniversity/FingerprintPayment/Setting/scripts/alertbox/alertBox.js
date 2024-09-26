function BoxOverlayCSS()
{
        $('#BoxOverlay').css({
                height          : $(document).height(),
		        width           : $(document).width()
			}).hide();
}
function j_alert(header,msg) {
    showModal(header, msg);
	//Sexy.alert("<br/><h1>&nbsp;&nbsp;"+header+"</h1><br/><p>&nbsp;&nbsp;&nbsp;"+msg+"</p>"); 
}
function j_info(header,msg)
{
    showModal(header,msg);
}
function j_err(header,msg) {
    showModal(header, msg);
	//Sexy.error("<br/><h1>&nbsp;&nbsp;"+header+"</h1><br/><p>&nbsp;&nbsp;&nbsp;"+msg+"</p>");
}
function j_infoSess(header,msg,hreftxt)
{
//	Sexy.info("<br/><h1>&nbsp;&nbsp;"+header+"</h1><br/><p>&nbsp;&nbsp;&nbsp;"+msg+"</p>",
//    { onComplete:
//			function(returnvalue){
//				if(returnvalue)
//				{
//					window.location=hreftxt;
//				}
//			}
//});
    showModal(header,msg, function () {
        window.location = hreftxt;
    });
    //window.location=hreftxt;
}
function j_confirm(header,msg,controlName)
{
    BoxOverlayCSS();
	Sexy.confirm("<br/><h1>&nbsp;&nbsp;"+header+"</h1><br/><p>&nbsp;&nbsp;&nbsp;"+msg+"</p>", 
		{ onComplete:
			function(returnvalue){
				if(returnvalue)
				{
					__doPostBack(controlName,'');//**
				}
			}
		});

}
function j_confirm_CancelDue(header,msg,controlName,sID,sUnit,txtID,txtUnit)
{
    BoxOverlayCSS();
	Sexy.confirm("<br/><h1>&nbsp;&nbsp;"+header+"</h1><br/><p>&nbsp;&nbsp;&nbsp;"+msg+"</p>", 
		{ onComplete:
			function(returnvalue){
				if(returnvalue)
				{
                    document.getElementById(txtID).value=sID;
                    document.getElementById(txtUnit).value=sUnit;
					__doPostBack(controlName,'');//**
				}
			}
		});
}
function j_confirm_ResDue(header,msg,controlName,sID,nItem,txtID,txtnItem)
{
    BoxOverlayCSS();
	Sexy.confirm("<br/><h1>&nbsp;&nbsp;"+header+"</h1><br/><p>&nbsp;&nbsp;&nbsp;"+msg+"</p>", 
		{ onComplete:
			function(returnvalue){
				if(returnvalue)
				{
                    document.getElementById(txtID).value=sID;
                    document.getElementById(txtnItem).value=nItem;
					__doPostBack(controlName,'');//**
				}
			}
		});
}
function j_confirmx(header,msg,chk)
{
	Sexy.confirm("<br/><h1>&nbsp;&nbsp;"+header+"</h1><br/><p>&nbsp;&nbsp;&nbsp;"+msg+"</p>", 
		{ onComplete:
			function(returnvalue){
				if(returnvalue)
				{
					document.getElementById(chk).checked=true;//**
				}
				else
				{
				    document.getElementById(chk).checked=false;
				}
			}
		});

}
function j_confirmDelContent(header,msg,hreftxt)
{
	Sexy.confirm("<br/><h1>&nbsp;&nbsp;"+header+"</h1><br/><p>&nbsp;&nbsp;&nbsp;"+msg+"</p>", 
		{ onComplete:
			function(returnvalue){
				if(returnvalue)
				{
					window.location.href=hreftxt;//**
				}
			}
		});
}
function j_confirmRedrirect(header,msg,hrefIsTrue,hrefIsFalse)
{
	Sexy.confirm("<br/><h1>&nbsp;&nbsp;"+header+"</h1><br/><p>&nbsp;&nbsp;&nbsp;"+msg+"</p>", 
		{ onComplete:
			function(returnvalue){
				if(returnvalue)
				{
					window.location.href=hrefIsTrue;//**
				}
				else
				{
				    window.location.href=hrefIsFalse;
				}
			}
		});
}
function j_promt(header,msg)
{
	//to do...
}

// headerDel = หัวข้อความลบ
// headerErr = หัวข้อความerr
// msgDel = ข้อความ  ลบ
// msgErr = ข้อความ  err
// controlID = control table id
// controlName = control del button *(.Unique)
function j_OnConfirmDel(headerDel,headerErr,msgDel,msgErr,controlID,controlName)
{
	//old code from jscommon.js
	var isOneChecked = false;
	var table = document.getElementById(controlID);
	var rows = table.getElementsByTagName('tr');
	for(i = 1; i < rows.length; i++){
		var cols = rows[i].getElementsByTagName('td');
		for(j = 0; j < cols.length; j++){
			var inputs = cols[j].getElementsByTagName('input');
			for(k = 0; k < inputs.length; k++){
				switch (inputs[k].type) {
					case 'checkbox': 
						if(inputs[k].checked) {isOneChecked = true; break;}
					break;
					case 'text': 						
					break;
				}
			}
		}
	}
	//add code
	if(isOneChecked) //ถ้า ติกถูก
    {
        j_confirm(headerDel,msgDel,controlName);
    } 
    else 
    {
        j_err(headerErr,msgErr);
    }
}

function j_OnConfirmSub()
{
    //to do....
}

//mix : 27/10/2010

function j_CheckedInput(headerErr,msgErr,controlID,controlName)
{
	//old code from jscommon.js
	var isOneChecked = false;
	var table = document.getElementById(controlID);
	var rows = table.getElementsByTagName('tr');
	for(i = 1; i < rows.length; i++){
		var cols = rows[i].getElementsByTagName('td');
		for(j = 0; j < cols.length; j++){
			var inputs = cols[j].getElementsByTagName('input');
			for(k = 0; k < inputs.length; k++){
				switch (inputs[k].type) {
					case 'checkbox': 
						if(inputs[k].checked) {isOneChecked = true; break;}
					break;
					case 'text': 						
					break;
				}
			}
		}
	}
	//add code
	if(!isOneChecked) //ถ้า ติกถูก
    {
        j_err(headerErr,msgErr);
    }
}

function j_confirm_del(header,msg)
{
    Sexy.confirm("<br/><h1>&nbsp;&nbsp;ยืนยันการลบข้อมูล</h1><br/><p>&nbsp;&nbsp;&nbsp;คุณต้องการที่จะลบข้อมูลนี้ใช่หรือไม่ ?</p>", 
    { onComplete:
        function(returnvalue){
            return returnvalue;
        }
    });//return false;

}
function j_OnConfirm_Calendar(header,headerErr,msg,msgErr,controlID,controlName)
{
	var isOneChecked = false;
	var inputs = document.getElementById(controlID).getElementsByTagName('input');
	for(k = 0; k < inputs.length; k++){
		switch (inputs[k].type) {
			case 'checkbox': 
				if(inputs[k].checked) {isOneChecked = true; break;}
			break;
			case 'text': 						
			break;
		}
	}
	//add code
	if(isOneChecked) //ถ้า ติกถูก
    {
        j_confirm(header,msg,controlName);
    } 
    else 
    {
        j_err(headerErr,msgErr);
    }
}

