
//  Begin ---ปิดฟั่งก์ชั่นต่างๆ ของการสร้างแบบสอบถาม (แอดตัวเลือก 1 มิติ 2 มิติ แนบไฟล์ และข้อความ) ----------------------- 
var smode = '';
var idEdit = '';
var smodeType = '';
var squestid = '';
function ShowTableQuize(_id,_sname,_dstart,_dend,_ssremark)
{
	var shtml = '';
	squestid = _id;
	$("div[id$='v2']").show();
	$("div[id$='v1']").hide();
	$("input[id$='v2txtfaq']").val(_sname);
	$("input[id$='v2txtsdate']").val(_dstart); // กำหนดค่าเวันริ่มต้น
	$("input[id$='v2txtedate']").val(_dend); // กำหนดค่าเวันสิ้นสุด
	$$('.ctl00cphv2txtsdate').set("value",_dstart);
	$$('.ctl00cphv2txtedate').set("value",_dend);
	$("input[id$='txtData']").val(_ssremark);
	checkData($("span[id$='lbRemInform']").attr('id'),$("input[id$='txtData']").attr('id'),$("a[id$='lbtndel']").attr('id'));
	
	//ดึงรายชื่อผู้ตอบแบบสอบถาม
	$.ajax({
                            type: "POST",
                            url: "tquestuserGet.ashx",
                            cache : false,
                            dataType: "json",
                            data: {str : xor_encrypt(squestid,7)},
                            success:  function(Jdata) {
                                         $.each(Jdata, function() {     
											var sid = this['suserid'];
											var sname = this['sname'];
											if( $('#divUsersend > div').size() == 0)
											{ 
												$("#divUsersend").append("<div id='"+sid+"' title='"+sname+"' class='slide'>"+sname+"<a style='cursor:pointer; cursor:hand;' herf\"#\" onclick=\"RemoveDiv('"+sid+"')\"><img src='images/action_delete.png' border=0 ></a></div>");
											}
											else
											{
												var cstatus = '0';
												$("#divUsersend").find('div').each(function(){
													if($(this).attr("id") == sid)
													{
														cstatus = '1';
														return false;
													}
												});
												if(cstatus == '0')
												{
													$("#divUsersend").append("<div id='"+sid+"' title='"+sname+"' class='slide'>"+sname+"<a style='cursor:pointer; cursor:hand;' herf\"#\" onclick=\"RemoveDiv('"+sid+"')\"><img src='images/action_delete.png' border=0 ></a></div>");
												}
											}
                                        });
										//ดึงคำถามหัวข้อใหญ่มาแสดง
										$.ajax({
											type: "POST",
											url: "tquesttitleGet.ashx",
											cache : false,
											dataType: "json",
											data: {str : xor_encrypt(squestid,7)},
											success:  function(Jdata) {
												$.each(Jdata, function() {     
													$('#tableQuize').append('<tr class = \'trMainQuest\' ><td width = \'100%\'><span class = \'trQuestNo\'></span><span class = \'trQuestName\' >'+this['squesttitlename']+'</span><span class=\'trQuestMode\' ></span> </br> <div></div> </td></tr>');
													$('#tableQuize tr').eq(this['squesttitleid']).data('QuizeType',this['cdimension']);
												});
												TableQuizeListsup();
												showChoice();
											}
										});
                            }
			}); 
}
function showChoice()
{
	var shtml = '';
	$.ajax({
                            type: "POST",
                            url: "tquestchoiceGet.ashx",
                            cache : false,
                            dataType: "json",
                            data: {str : xor_encrypt(squestid,7)},
                            success:  function(JdataChoice) {
								$.ajax({
									type: "POST",
									url: "tquestchoice2dGet.ashx",
									cache : false,
									dataType: "json",
									data: {str : xor_encrypt(squestid,7)},
									success:  function(JdataChoice2d) {
										var _idexTr2d = 0;
										$('.trMainQuest').each(function () {
											if($(this).data('QuizeType') == '2d')
											{
												var _item = $('.trMainQuest').eq(_idexTr2d).attr("id");
												var shtmlRadio = '';
												var shtmlHead = '<td></td>';
												$.each(JdataChoice2d[_item], function() {
													shtmlHead += '<td nowrap=""><span class = \'spanIDX\'>'+this['squestchoice2did']+'</span>. <span class = \'spanNameX\'>'+this['schoice2dname']+'</span></td>';
													shtmlRadio += '<td nowrap=""> <input type="radio" disabled="disabled"></td>';
												});	
												$.each(JdataChoice[_item], function() {
													shtml += '<tr><td nowrap=""><span class = \'spanID\'>'+this['squestchoiceid']+'</span>. <span class = \'spanName\'>'+this['schoicename']+'</span></td>'+shtmlRadio+'</tr>';
												});
												if(shtml != '')
												{
													$('.trMainQuest').eq(_idexTr2d).find('div').html('<table width = \'100%\' ><tr class = \'strhead\'>'+shtmlHead+'</tr>'+shtml+'</table>');
													shtml='';
												}
											}
											_idexTr2d++;
										});
									}
								});
								var _idexTr = 0;
								$('.trMainQuest').each(function () {
									if($(this).data('QuizeType') == '1d')
									{
										var _item = $('.trMainQuest').eq(_idexTr).attr("id");
										$.each(JdataChoice[_item], function() {
											if(this['squestchoiceid'] != null && this['schoicename'] != null)
												shtml += '<tr><td nowrap=""> <input type="radio" disabled="disabled"><span class = \'spanID\' >'+this['squestchoiceid']+'</span>. <span class = \'spanName\'>'+this['schoicename']+'</span></td></tr>';
										});
									    if(shtml != '')
											{
													$('.trMainQuest').eq(_idexTr).find('div').html('<table width = \'100%\' >'+shtml+'</table>');
													shtml='';
											}
									}
									else if($(this).data('QuizeType') == 'file')
									{
										shtml= '<br><input type="file" name="datafile" size="40">';
										$(this).find('div').html(shtml);
									}												
									else if($(this).data('QuizeType') == 'text')
									{
										shtml= '<br><textarea name="comments" rows="3" cols="40"></textarea>';
										$(this).find('div').html(shtml);
									}		
									_idexTr++;
								});
							}
    });
}
function SaveTableQuize()
{
    var stype = '';
	var str = '';
	var i = 1;
    $('.trMainQuest').each(function () {
	        if($(this).data('QuizeType') == '1d')
				stype = '1';
			else if($(this).data('QuizeType') == '2d')
				stype = '2';
			else if($(this).data('QuizeType') == 'file')
				stype = '3';	
			else if($(this).data('QuizeType') == 'text')
				stype = '4';	
			else return false;
            str += squestid+'&'+i+'&'+$(this).find('.trQuestName').html()+'&'+i+'&1&'+stype+'|';
			i++;
    });
	$.ajax({
                            type: "POST",
                            url: "tquesttitleSave.ashx",
                            cache : false,
                            dataType: "text",
                            data: {strQueryString : encodeURI(xor_encrypt(str,7))},
                            success:  function(response) {
										 if($.trim(response) != '0')
										 {
											SaveTableY();
										 }
										 else
										 {
											alert('ไม่สามารถบันทึกข้อมูลได้ โปรดตรวจสอบอีกครั้ง!');
										 }
                                      }
                });
}
function SaveTableY()
{
    var stype = '';
	var str = '';
	var i = 1;
	var j = 1;
    $('.trMainQuest').each(function () {
		j=1;
		$(this).find('table tr').each(function () {
			if($(this).find('.spanName').html() != null)
			{
				str += squestid+'&'+i+'&'+j+'&'+$(this).find('.spanName').html()+'|';
				j++;
			}
        });  
		i++;
    });
	if(str != '')
	{
		$.ajax({
                            type: "POST",
                            url: "tquestchoiceSave.ashx",
                            cache : false,
                            dataType: "text",
                            data: {strQueryString : encodeURI(xor_encrypt(str,7))},
                            success:  function(response) {
										 if($.trim(response) != '0')
										 {
											SaveTableX();
										 }
										 else
										 {
											alert('ไม่สามารถบันทึกข้อมูลได้ โปรดตรวจสอบอีกครั้ง!');
										 }
                                      }
            });
	}
}
function SaveTableX()
{
    var stype = '';
	var str = '';
	var i = 1;
	var j = 1;
    $('.trMainQuest').each(function () {
		$(this).find('table tr').each(function () {
			j=1;
			$(this).find('td').each(function () {
				if($(this).find('.spanNameX').html() != null)
				{
					str += squestid+'&'+i+'&'+j+'&'+$(this).find('.spanNameX').html()+'|';
					j++;
				}
			});
        });  
		i++;
    });
	if(str != '')
	{
		$.ajax({
                            type: "POST",
                            url: "tquestchoice2dSave.ashx",
                            cache : false,
                            dataType: "text",
                            data: {strQueryString : encodeURI(xor_encrypt(str,7))},
                            success:  function(response) {
										 if($.trim(response) == '0')
										 {
											alert('ไม่สามารถบันทึกข้อมูลได้ โปรดตรวจสอบอีกครั้ง!');
										 }
                                      }
            });
	}

}
function AddTableY(trix,trixno) //แอดตัวเลือก แนว นอน
{
    if(trix == '') { alert('กรุณาระบุ ตัวเลือก (ปกติ/แกน Y)'); $("input[id$='v2txttrix']").focus(); return false;}
    if(trixno == '') trixno = 1;
    $('#tableY tr').eq(trixno-1).after('<tr><td width = \'10%\'></td><td width = \'5%\'></td><td width = \'85%\'>'+trix+'</td></tr>');//ใส่ตัวเลือก ลงแถวถัดไป
    TableYListsup();
    $("input[id$='v2txttrix']").val('');
    $("input[id$='v2txttrix']").focus();
    $("input[id$='v2txttrixno']").val($('#tableY tr').length);
}
function AddTableX(trix,trixno) //แอดตัวเลือก แนว ตั้ง กรณีเลือก 2 มิติ
{
    if(trix == '') { alert('กรุณาระบุ ตัวเลือก (2 มิติ/แกน X)'); $("input[id$='v2txttriy']").focus(); return false;}
    if(trixno == '') trixno = 1;
    $('#tableX tr').eq(trixno-1).after('<tr><td width = \'10%\'></td><td width = \'5%\'></td><td width = \'85%\'>'+trix+'</td></tr>');//ใส่ตัวเลือก ลงแถวถัดไป
    TableXListsup();
    $("input[id$='v2txttriy']").val('');
    $("input[id$='v2txttriy']").focus();
    $("input[id$='v2txttriyno']").val($('#tableX tr').length);
}
function TableXListsup()
{
    var i = 0;
    $('#tableX tr').each(function () {
        if(i != 0)
        {
            $(this).attr('id','trX'+i);
            $(this).find('td').eq(1).html(i+'. ');
            $(this).find('td').eq(0).html('<a href = \'javascript:void(0);\' onclick="if(confirm(\'คุณต้องการลบข้อมูลใช่หรือไม่ ?\')) DelTableX(\'trX'+i+'\');" ><img height="12" border="0" width="12" style="vertical-align: middle;" src="images/del.gif" alt="ลบข้อมูล" > ลบ</a>');
        }
        i++;
    });
}
function TableYListsup()
{
    var i = 0;
    $('#tableY tr').each(function () {
        if(i != 0)
        {
            $(this).attr('id','trY'+i);
            $(this).find('td').eq(1).html(i+'. ');
            $(this).find('td').eq(0).html('<a href = \'javascript:void(0);\' onclick="if(confirm(\'คุณต้องการลบข้อมูลใช่หรือไม่ ?\')) DelTableY(\'trY'+i+'\');" ><img height="12" border="0" width="12" style="vertical-align: middle;" src="images/del.gif" alt="ลบข้อมูล" > ลบ</a>');
        }
        i++;
    });
}
function TableQuizeListsup()
{
    var i = 0;
	$("#tableQuize").attr("algin","left");
    $('#tableQuize tr').each(function () {
        if($(this).attr('class') == 'trMainQuest')
        {
            i++;
			$(this).find('td').attr("algin","left");
            $(this).find('.trQuestNo').eq(0).html(i+'. ');
            $(this).attr('id','QuizeId'+i);
            $(this).find('.trQuestMode').html('');
            $(this).find('.trQuestMode').append(' <a href = \'javascript:void(0);\' onclick="EditTableQuize(\''+$(this).data('QuizeType')+'\',\'QuizeId'+i+'\');" >[แก้ไข]</a>');
            $(this).find('.trQuestMode').append(' <a href = \'javascript:void(0);\' onclick="if(confirm(\'คุณต้องการลบข้อมูลใช่หรือไม่ ?\')) DelTableQuize(\'QuizeId'+i+'\');" ><img height="12" border="0" width="12" style="vertical-align: middle;" src="images/del.gif" alt="ลบข้อมูล" ></a>');        }
    });
}
function EditTableQuize(_type,_id)
{
    if(_type == '1d')
    {
		$("div[id$='v1ymode']").show();
		$("div[id$='v2ymode']").hide();
        ClearTableY();
		$("input[id$='v2txtQuest']").val($('#'+_id).find('.trQuestName').html());
        $("input[id$='v2txtQuestNo']").val($.trim($('#'+_id).find('.trQuestNo').html().replace('.',''))).attr('disabled', 'disabled');
        $("input[id$='v2rbnormal']").attr("checked", "checked");
        $("*[name$='rbmode']").attr('disabled', 'disabled');
        $('#'+_id).find('table tr').each(function () {
            AddTableY($(this).find('.spanName').html(),$(this).find('.spanID').html());
        });
    }
	else if(_type == '2d')
    {
		$("div[id$='v1ymode']").show();
		$("div[id$='v2ymode']").show();
        ClearTableY();
		ClearTableX();
		$("input[id$='v2txtQuest']").val($('#'+_id).find('.trQuestName').html());
		$("input[id$='v2txtQuestNo']").val($.trim($('#'+_id).find('.trQuestNo').html().replace('.',''))).attr('disabled', 'disabled');
		$("input[id$='v2rb2mode']").attr("checked", "checked");
        $("*[name$='rbmode']").attr('disabled', 'disabled');
        $('#'+_id).find('table tr').each(function () {
			$(this).find('td').each(function () {
				AddTableX($(this).find('.spanNameX').html(),$(this).find('.spanIDX').html());
			});
            AddTableY($(this).find('.spanName').html(),$(this).find('.spanID').html());
        });
    }
	else if(_type == 'file')
    {
		$("div[id$='v1ymode']").hide();
		$("div[id$='v2ymode']").hide();
        $("input[id$='v2txtQuest']").val($('#'+_id).find('.trQuestName').html());
        $("input[id$='v2txtQuestNo']").val($.trim($('#'+_id).find('.trQuestNo').html().replace('.',''))).attr('disabled', 'disabled');
        $("input[id$='v2rb3mode']").attr("checked", "checked");
        $("*[name$='rbmode']").attr('disabled', 'disabled');
		$("input[id$='v2txtQuest']").focus();
    }
	else if(_type == 'text')
    {
		$("div[id$='v1ymode']").hide();
		$("div[id$='v2ymode']").hide();
        $("input[id$='v2txtQuest']").val($('#'+_id).find('.trQuestName').html());
        $("input[id$='v2txtQuestNo']").val($.trim($('#'+_id).find('.trQuestNo').html().replace('.',''))).attr('disabled', 'disabled');
        $("input[id$='v2rb4mode']").attr("checked", "checked");
        $("*[name$='rbmode']").attr('disabled', 'disabled');
		$("input[id$='v2txtQuest']").focus();

    }
	smode = 'edit';
    idEdit = _id;
	smodeType = _type;
}
function ClearTableY()
{
    var i = 0;
    $('#tableY tr').each(function () {
        if(i != 0)
        {
            $(this).remove();   
        }     
        i++;
    });
}
function ClearTableX()
{
    var i = 0;
    $('#tableX tr').each(function () {
        if(i != 0)
        {
            $(this).remove();   
        }     
        i++;
    });
}
function DelTableY(_index)
{
    $('#'+_index).remove();
    TableYListsup();
    $("input[id$='v2txttrixno']").val($('#tableY tr').length);
}
function DelTableX(_index)
{
    $('#'+_index).remove();
    TableXListsup();
    $("input[id$='v2txttriyno']").val($('#tableX tr').length);
}
function DelTableQuize(_index)
{
    $('#'+_index).remove();
    TableQuizeListsup();
}
function AddDivQuize()
{
    var shtml= '';
    var Quest = $("input[id$='v2txtQuest']").val();
    var QuestNo = $("input[id$='v2txtQuestNo']").val();
    if(QuestNo == '') QuestNo = 1;
    if(Quest == '') { alert('กรุณาระบุ หัวข้อคำถาม'); $("input[id$='v2txtQuest']").focus(); return false;}
    if(($('#tableY tr').length-1) <= 0)  { alert('กรุณาระบุ ระบุตัวเลือก'); $("input[id$='v2txttrix']").focus(); return false;}
    else
    {
        var i = 0;
        shtml = '<div ><table width = \'100%\' >';
        $('#tableY tr').each(function () {
            if(i != 0)
            {
                shtml += '<tr><td nowrap=""> <input type="radio" disabled="disabled"><span class = \'spanID\' >'+$(this).find('td').eq(1).html()+'</span>. <span class = \'spanName\'>'+$(this).find('td').eq(2).html()+'</span></td></tr>';
                $(this).remove();
            }
            i++;
        });
        shtml += '</table></div> ';
    
        if(($('#tableQuize tr').length-1) <= 0)
        {
            $('#tableQuize tr').eq(QuestNo-1).after('<tr class = \'trMainQuest\' ><td width = \'100%\'><span class = \'trQuestNo\'> </span><span class = \'trQuestName\' >'+Quest+'</span><span class=\'trQuestMode\' ></span> </br> '+shtml+' </td></tr>');
        }
        else
        {
            if(($('.trMainQuest').length+1) == QuestNo)
            {
               var limit = 0;
               $('.trMainQuest').each(function () {
                    limit++;
                    if( limit == (QuestNo-1))
                    {
                        $(this).after('<tr class = \'trMainQuest\' ><td width = \'100%\'><span class = \'trQuestNo\'> </span><span class = \'trQuestName\' >'+Quest+'</span><span class=\'trQuestMode\' ></span> </br> '+shtml+' </td></tr>');
                        return false;
                    }
               });
            }
            else
            {
                var j = 0;
                $('.trMainQuest').each(function () {
                    j++;
                    if( j == (QuestNo))
                    {
                        $(this).before('<tr class = \'trMainQuest\' ><td width = \'100%\'><span class = \'trQuestNo\'> </span><span class = \'trQuestName\' >'+Quest+'</span><span class=\'trQuestMode\' ></span> </br> '+shtml+' </td></tr>');
                        return false;
                    }
                });
            }
        }
        var sindex = 0;
        $('.trMainQuest').each(function () {
            sindex++;
            if( sindex == (QuestNo))
            {
                $(this).data('QuizeType', '1d')
                return false;
            }
        });
        $("input[id$='v2txtQuest']").val('').focus();
    }
    TableQuizeListsup();
    $("input[id$='v2txtQuestNo']").val($('.trMainQuest').length+1)
}
function AddDivQuizeFile()
{
    var shtml= '<br><input type="file" name="datafile" size="40">';
    var Quest = $("input[id$='v2txtQuest']").val();
    var QuestNo = $("input[id$='v2txtQuestNo']").val();
    if(QuestNo == '') QuestNo = 1;
    if(Quest == '') { alert('กรุณาระบุ หัวข้อคำถาม'); $("input[id$='v2txtQuest']").focus(); return false;}
   
    if(($('#tableQuize tr').length-1) <= 0)
    {
        $('#tableQuize tr').eq(QuestNo-1).after('<tr class = \'trMainQuest\' ><td width = \'100%\'><span class = \'trQuestNo\'> </span><span class = \'trQuestName\' >'+Quest+'</span><span class=\'trQuestMode\' ></span> </br> '+shtml+' </td></tr>');
    }
    else
    {
        if(($('.trMainQuest').length+1) == QuestNo)
        {
            var limit = 0;
            $('.trMainQuest').each(function () {
                limit++;
                if( limit == (QuestNo-1))
                {
                    $(this).after('<tr class = \'trMainQuest\' ><td width = \'100%\'><span class = \'trQuestNo\'> </span><span class = \'trQuestName\' >'+Quest+'</span><span class=\'trQuestMode\' ></span> </br> '+shtml+' </td></tr>');
                    return false;
                }
            });
        }
        else
        {
            var j = 0;
            $('.trMainQuest').each(function () {
                j++;
                if( j == (QuestNo))
                {
                    $(this).before('<tr class = \'trMainQuest\' ><td width = \'100%\'><span class = \'trQuestNo\'> </span><span class = \'trQuestName\' >'+Quest+'</span><span class=\'trQuestMode\' ></span> </br> '+shtml+' </td></tr>');
                    return false;
                }
            });
        }
    }
    var sindex = 0;
    $('.trMainQuest').each(function () {
		sindex++;
         if( sindex == (QuestNo))
        {
            $(this).data('QuizeType', 'file')
            return false;
        }
    });
	TableQuizeListsup();
	$("input[id$='v2txtQuest']").val('').focus();
    $("input[id$='v2txtQuestNo']").val($('.trMainQuest').length+1)
}
function AddDivQuizeText()
{
    var shtml= '<br><textarea name="comments" rows="3" cols="40"></textarea> ';
    var Quest = $("input[id$='v2txtQuest']").val();
    var QuestNo = $("input[id$='v2txtQuestNo']").val();
    if(QuestNo == '') QuestNo = 1;
    if(Quest == '') { alert('กรุณาระบุ หัวข้อคำถาม'); $("input[id$='v2txtQuest']").focus(); return false;}
   
    if(($('#tableQuize tr').length-1) <= 0)
    {
        $('#tableQuize tr').eq(QuestNo-1).after('<tr class = \'trMainQuest\' ><td width = \'100%\'><span class = \'trQuestNo\'> </span><span class = \'trQuestName\' >'+Quest+'</span><span class=\'trQuestMode\' ></span> </br> '+shtml+' </td></tr>');
    }
    else
    {
        if(($('.trMainQuest').length+1) == QuestNo)
        {
            var limit = 0;
            $('.trMainQuest').each(function () {
                limit++;
                if( limit == (QuestNo-1))
                {
                    $(this).after('<tr class = \'trMainQuest\' ><td width = \'100%\'><span class = \'trQuestNo\'> </span><span class = \'trQuestName\' >'+Quest+'</span><span class=\'trQuestMode\' ></span> </br> '+shtml+' </td></tr>');
                    return false;
                }
            });
        }
        else
        {
            var j = 0;
            $('.trMainQuest').each(function () {
                j++;
                if( j == (QuestNo))
                {
                    $(this).before('<tr class = \'trMainQuest\' ><td width = \'100%\'><span class = \'trQuestNo\'> </span><span class = \'trQuestName\' >'+Quest+'</span><span class=\'trQuestMode\' ></span> </br> '+shtml+' </td></tr>');
                    return false;
                }
            });
        }
    }
    var sindex = 0;
    $('.trMainQuest').each(function () {
		sindex++;
         if( sindex == (QuestNo))
        {
            $(this).data('QuizeType', 'text')
            return false;
        }
    });
	TableQuizeListsup();
	$("input[id$='v2txtQuest']").val('').focus();
    $("input[id$='v2txtQuestNo']").val($('.trMainQuest').length+1)
}
function AddDivQuize2D()
{
    var shtml= '';
	var shtmlRadio = '';
    var Quest = $("input[id$='v2txtQuest']").val();
    var QuestNo = $("input[id$='v2txtQuestNo']").val();
    if(QuestNo == '') QuestNo = 1;
    if(Quest == '') { alert('กรุณาระบุ หัวข้อคำถาม'); $("input[id$='v2txtQuest']").focus(); return false;}
    if(($('#tableY tr').length-1) <= 0)  { alert('กรุณาระบุ ระบุตัวเลือก'); $("input[id$='v2txttrix']").focus(); return false;}
    else
    {
        var i = 0;
		var j = 0;
        shtml = '<div ><table width = \'100%\' >';
        $('#tableY tr').each(function () { //วนลูปเพิ่มแถวข้อมูล (แนวนอน)
			if(i != 0)
            {
                shtml += '<tr><td nowrap=""><span class = \'spanID\'>'+$(this).find('td').eq(1).html()+'</span>. <span class = \'spanName\'>'+$(this).find('td').eq(2).html()+'</span></td>'+shtmlRadio+'</tr>';
                $(this).remove();
            }
			else
			{
				shtml += '<tr class = \'strhead\' ><td></td>';
				$('#tableX tr').each(function () { //วนลูปแถว แรก เพื่อใส่ ตัวเลือก แกน x
					if(j != 0)
					{
						shtml += '<td nowrap=""><span class = \'spanIDX\'>'+$(this).find('td').eq(1).html()+'</span>. <span class = \'spanNameX\'>'+$(this).find('td').eq(2).html()+'</span></td>';
						shtmlRadio += '<td nowrap=""> <input type="radio" disabled="disabled"></td>';
						$(this).remove();
					}
					j++;
				});
				shtml += '</tr>';
			}
            i++;
        });
        shtml += '</table></div> ';
    
        if(($('#tableQuize tr').length-1) <= 0)
        {
            $('#tableQuize tr').eq(QuestNo-1).after('<tr class = \'trMainQuest\' ><td width = \'100%\'><span class = \'trQuestNo\'> </span><span class = \'trQuestName\' >'+Quest+'</span><span class=\'trQuestMode\' ></span> </br> '+shtml+' </td></tr>');
        }
        else
        {
            if(($('.trMainQuest').length+1) == QuestNo)
            {
               var limit = 0;
               $('.trMainQuest').each(function () {
                    limit++;
                    if( limit == (QuestNo-1))
                    {
                        $(this).after('<tr class = \'trMainQuest\' ><td width = \'100%\'><span class = \'trQuestNo\'> </span><span class = \'trQuestName\' >'+Quest+'</span><span class=\'trQuestMode\' ></span> </br> '+shtml+' </td></tr>');
                        return false;
                    }
               });
            }
            else
            {
                var j = 0;
                $('.trMainQuest').each(function () {
                    j++;
                    if( j == (QuestNo))
                    {
                        $(this).before('<tr class = \'trMainQuest\' ><td width = \'100%\'><span class = \'trQuestNo\'> </span><span class = \'trQuestName\' >'+Quest+'</span><span class=\'trQuestMode\' ></span> </br> '+shtml+' </td></tr>');
                        return false;
                    }
                });
            }
        }
        var sindex = 0;
        $('.trMainQuest').each(function () {
            sindex++;
            if( sindex == (QuestNo))
            {
                $(this).data('QuizeType', '2d')
                return false;
            }
        });
        $("input[id$='v2txtQuest']").val('').focus();
    }
    TableQuizeListsup();
    $("input[id$='v2txtQuestNo']").val($('.trMainQuest').length+1)
}
function EditDivQuize()
{
    var shtml= '';
    var Quest = $("input[id$='v2txtQuest']").val();
    var QuestNo = $("input[id$='v2txtQuestNo']").val();
    if(QuestNo == '') QuestNo = 1;
    if(Quest == '') { alert('กรุณาระบุ หัวข้อคำถาม'); $("input[id$='v2txtQuest']").focus(); return false;}
    if(($('#tableY tr').length-1) <= 0)  { alert('กรุณาระบุ ระบุตัวเลือก'); $("input[id$='v2txttrix']").focus(); return false;}
    else
    {
        var i = 0;
        shtml = '<table width = \'100%\' >';
        $('#tableY tr').each(function () {
            if(i != 0)
            {
                shtml += '<tr><td nowrap=""> <input type="radio" disabled="disabled"><span class = \'spanID\' >'+$(this).find('td').eq(1).html()+'</span>. <span class = \'spanName\'>'+$(this).find('td').eq(2).html()+'</span></td></tr>';
                $(this).remove();
            }
            i++;
        });
        shtml += '</table>';
        $('#'+idEdit).find('.trQuestName').html(Quest);
        $('#'+idEdit).find('div').html(shtml);
        $("input[id$='v2txtQuest']").val('').focus();
    }
    TableQuizeListsup();
    $("input[id$='v2txtQuestNo']").val($('.trMainQuest').length+1).removeAttr('disabled');

}
function EditDivQuizeFile()
{
    var shtml= '<br><input type="file" name="datafile" size="40">';
    var Quest = $("input[id$='v2txtQuest']").val();
    var QuestNo = $("input[id$='v2txtQuestNo']").val();
    if(QuestNo == '') QuestNo = 1;
    if(Quest == '') { alert('กรุณาระบุ หัวข้อคำถาม'); $("input[id$='v2txtQuest']").focus(); return false;}        

    $('#'+idEdit).find('.trQuestName').html(Quest);
    $('#'+idEdit).find('div').html(shtml);
    $("input[id$='v2txtQuest']").val('').focus();

    TableQuizeListsup();
    $("input[id$='v2txtQuestNo']").val($('.trMainQuest').length+1).removeAttr('disabled');

}
function EditDivQuizeText()
{
    var shtml= '<br><textarea name="comments" rows="3" cols="40"></textarea> ';
    var Quest = $("input[id$='v2txtQuest']").val();
    var QuestNo = $("input[id$='v2txtQuestNo']").val();
    if(QuestNo == '') QuestNo = 1;
    if(Quest == '') { alert('กรุณาระบุ หัวข้อคำถาม'); $("input[id$='v2txtQuest']").focus(); return false;}        

    $('#'+idEdit).find('.trQuestName').html(Quest);
    $('#'+idEdit).find('div').html(shtml);
    $("input[id$='v2txtQuest']").val('').focus();

    TableQuizeListsup();
    $("input[id$='v2txtQuestNo']").val($('.trMainQuest').length+1).removeAttr('disabled');

}

function EditDivQuize2D()
{
    var shtml= '';
	var shtmlRadio = '';
    var Quest = $("input[id$='v2txtQuest']").val();
    var QuestNo = $("input[id$='v2txtQuestNo']").val();
    if(QuestNo == '') QuestNo = 1;
    if(Quest == '') { alert('กรุณาระบุ หัวข้อคำถาม'); $("input[id$='v2txtQuest']").focus(); return false;}
    if(($('#tableY tr').length-1) <= 0)  { alert('กรุณาระบุ ระบุตัวเลือก'); $("input[id$='v2txttrix']").focus(); return false;}
    else
    {
        var i = 0;
		var j = 0;
        shtml = '<table width = \'100%\' >';
        $('#tableY tr').each(function () { //วนลูปเพิ่มแถวข้อมูล (แนวนอน)
			if(i != 0)
            {
                shtml += '<tr><td nowrap=""><span class = \'spanID\'>'+$(this).find('td').eq(1).html()+'</span>. <span class = \'spanName\'>'+$(this).find('td').eq(2).html()+'</span></td>'+shtmlRadio+'</tr>';
                $(this).remove();
            }
			else
			{
				shtml += '<tr class = \'strhead\' ><td></td>';
				$('#tableX tr').each(function () { //วนลูปแถว แรก เพื่อใส่ ตัวเลือก แกน x
					if(j != 0)
					{
						shtml += '<td nowrap=""><span class = \'spanIDX\'>'+$(this).find('td').eq(1).html()+'</span>. <span class = \'spanNameX\'>'+$(this).find('td').eq(2).html()+'</span></td>';
						shtmlRadio += '<td nowrap=""> <input type="radio" disabled="disabled"></td>';
						$(this).remove();
					}
					j++;
				});
				shtml += '</tr>';
			}
            i++;
        });
        shtml += '</table>';
        $('#'+idEdit).find('.trQuestName').html(Quest);
        $('#'+idEdit).find('div').html(shtml);
        $("input[id$='v2txtQuest']").val('').focus();
    }
    TableQuizeListsup();
    $("input[id$='v2txtQuestNo']").val($('.trMainQuest').length+1).removeAttr('disabled');

}
//  End ----ปิดฟั่งก์ชั่นต่างๆ ของการสร้างแบบสอบถาม (แอดตัวเลือก 1 มิติ 2 มิติ แนบไฟล์ และข้อความ) ----------------------- 

// Begin ---เปิดฟังก์ชั่น จัดการตาราง ผู้ใช้งาน
function setPageforTable(tableID)
{
    var options = {
              currPage : 1, 
              optionsForRows : [5,10,20],
              rowsPerPage : 5,
              firstArrow : (new Image()).src="./images/Pagination/first.png",
              prevArrow : (new Image()).src="./images/Pagination/prev.png",
              lastArrow : (new Image()).src="./images/Pagination/last.png",
              nextArrow : (new Image()).src="./images/Pagination/next.png" }
    $('#'+tableID).tablePagination(options);
    $('#'+tableID).tablesorter({ headers: { 0: { sorter: false} } });
}
function jqueryCheckAll(id)
{
    var checkedStatus = id.checked;
    $('#mytable tr td:first-child input:checkbox').each(function() {
            this.checked = checkedStatus;
    });
}
function RemoveDiv(id)
{
    $('#divUsersend div').each(function() {
            if($(this).attr("id") == id)
               $(this).remove(); 
    });
}
// End ---เปิดฟังก์ชั่น จัดการตาราง ผู้ใช้งาน
$(document).ready(function(){

    //*** ถ้าเข้าหน้านี้ครั้งแรก ให้ซ่อน popup***
    //PopUp Remark
	$("#backgroundPopup").fadeOut("def");
	$("#popup").fadeOut("def");
	//
	//*** ***************************
	
//  Begin ----ปิดฟังก์ชั่นต่างๆ ของการสร้างแบบสอบถาม (แอดตัวเลือก 1 มิติ 2 มิติ แนบไฟล์ และข้อความ) ----------------------- 
    $("input[id$='v2btnYadd']").click(function(){
	       AddTableY($("input[id$='v2txttrix']").val(),$("input[id$='v2txttrixno']").val());
	       return false;
	});
    $("input[id$='v2btnXadd']").click(function(){
	       AddTableX($("input[id$='v2txttriy']").val(),$("input[id$='v2txttriyno']").val());
	       return false;
	});
    $("input[id$='v2btnAdd']").click(function(){
           if(smode == 'edit' && idEdit != '' && smodeType == '1d')
           {
				$("div[id$='v1ymode']").show();
				$("div[id$='v2ymode']").hide();
                EditDivQuize();
                idEdit= '';
                smode='';
				smodeType = '';
            }
		   else if(smode == 'edit' && idEdit != '' && smodeType == '2d')
           {
				$("div[id$='v2ymode']").show();
				$("div[id$='v1ymode']").show();
                EditDivQuize2D();
                idEdit= '';
                smode='';
				smodeType = '';
            }
		   else if(smode == 'edit' && idEdit != '' && smodeType == 'file')
           {
			    $("div[id$='v2ymode']").hide();
				$("div[id$='v1ymode']").hide();
                EditDivQuizeFile();
                idEdit= '';
                smode='';
				smodeType = '';
            }
		   else if(smode == 'edit' && idEdit != '' && smodeType == 'text')
           {
			    $("div[id$='v2ymode']").hide();
				$("div[id$='v1ymode']").hide();
                EditDivQuizeText();
                idEdit= '';
                smode='';
				smodeType = '';
            }
            else
			{
				if($("input[id$='v2rbnormal']").attr("checked"))
					AddDivQuize();
				else if($("input[id$='v2rb2mode']").attr("checked"))
				{
					AddDivQuize2D();
				}
				else if($("input[id$='v2rb3mode']").attr("checked"))
				{
					AddDivQuizeFile();
				}
				else if($("input[id$='v2rb4mode']").attr("checked"))
				{
					AddDivQuizeText();
				}
			}
	       $("*[name$='rbmode']").removeAttr('disabled');
	       $("input[id$='v2txttrixno']").val('1');
		   $("input[id$='v2txttriyno']").val('1')
	       return false;
	});
	$("input[id$='v2btnClear']").click(function(){
	       smode = '';
	       idEdit = '';
	       $("input[id$='v2txtQuest']").val('');
	       $("input[id$='v2txttrix']").val('');
	       $("input[id$='v2txttrixno']").val('1');
	       $("input[id$='v2txtQuestNo']").val($('.trMainQuest').length+1).removeAttr('disabled');
	       $("*[name$='rbmode']").removeAttr('disabled');
	       $("input[id$='v2txttriy']").val('');
	       $("input[id$='v2txttriyno']").val('1');
	       ClearTableY();
	       ClearTableX();
	       return false;
	});
	$("input[id$='v2rb2mode']").click(function(){
	       $("div[id$='v2ymode']").show();
	});
	$("input[id$='v2rbnormal']").click(function(){
	       $("div[id$='v2ymode']").hide();
		   $("div[id$='v1ymode']").show();
	});
	$("input[id$='v2rb3mode']").click(function(){
	       $("div[id$='v2ymode']").hide();
		   $("div[id$='v1ymode']").hide();

	});
	$("input[id$='v2rb4mode']").click(function(){
	       $("div[id$='v2ymode']").hide();
		   $("div[id$='v1ymode']").hide();

	});
//  End ----ปิดฟั่งก์ชั่นต่างๆ ของการสร้างแบบสอบถาม (แอดตัวเลือก 1 มิติ 2 มิติ แนบไฟล์ และข้อความ) ----------------------- 

//  Begin ----เปิดฟังก์ชั่นต่างๆ ของการแสดงคำอธิบายเพิ่มเติม ----------------------- 
	$("#popup").draggable(); //กำหนดให้สามารถลาก popup ได้
	checkData($("span[id$='lbRemInform']").attr('id'),$("input[id$='txtData']").attr('id'),$("a[id$='lbtndel']").attr('id'));
	checkData($("span[id$='lbuseranswer']").attr('id'),$("input[id$='v2txtuserid']").attr('id'),$("a[id$='lbtnusdel']").attr('id'));
	$("span[id$='lbShowpopup']").click(function(){ //เมื่อคลิก ให้แสดง popup
	    $("textarea[id$='mdltxtvcomment']").val($("input[id$='txtData']").val()); //<---ใส่ค่า เก็บลงไป
		//Switch Object
		DisplayDIV('popup','divPopRemark');
		//centering with css
		centerPopup('backgroundPopup','popup');
		//load popup
		loadPopup('backgroundPopup','popup');
	});
	//// Pop Up คำอธิบายเพิ่มเติม
    $("input[id$='submitvcomment']").click(function(){
		disablePopup('backgroundPopup','popup');
		$("input[id$='txtData']").val($("textarea[id$='mdltxtvcomment']").val());
		$("textarea[id$='mdltxtvcomment']").val('');
		//*************** check ข้อความใน txtdata ก่อน ว่ามีมั้ย ถ้ามีใส่เครื่องหมายถูก ถ้าไม่มี ไม่ต้องใส่เครื่องหมาย *******************//
        checkData($("span[id$='lbRemInform']").attr('id'),$("input[id$='txtData']").attr('id'),$("a[id$='lbtndel']").attr('id'));
        //***************************************************************************************************//
		return false; //<----- return false ใส่ไว้ เพื่อ ไม่ต้องการ ให้เกิดการ postback
	});
	//************** Click ถังขยะ event! **************//
	$("a[id$='lbtndel']").click(function(){ //เรียก linkbutton ใน masterpage
	    if(confirm('คุณต้องการยกเลิกข้อมูลหมายเหตุหรือไม่?')) {
			$("input[id$='txtData']").val(''); //<---ใส่ค่า เก็บลงไป
			//*************** check ข้อความใน txtdata ก่อน ว่ามีมั้ย ถ้ามีใส่เครื่องหมายถูก ถ้าไม่มี ไม่ต้องใส่เครื่องหมาย *******************//
			checkData($("span[id$='lbRemInform']").attr('id'),$("input[id$='txtData']").attr('id'),$("a[id$='lbtndel']").attr('id'));
			//***************************************************************************************************//
        }
	    return false;
	});
//  End ----เปิดฟังก์ชั่นต่างๆ ของการแสดงคำอธิบายเพิ่มเติม ----------------------- 

// Begin ---เปิดฟังกชั่น การ จัดการ popup -------------------
	//************** CLOSING POPUP **************//
	//Click cancel event!
	//// Pop Up คำอธิบายเพิ่มเติม
	$("input[id$='cancelvcomment']").click(function(){
		disablePopup('backgroundPopup','popup');
	    document.getElementById('divPopRemark').style.display ='none';
	    return false; //<----- return false ใส่ไว้ เพื่อ ไม่ต้องการ ให้เกิดการ postback 
	});
	//// Pop Up รายชื่อผู้กรอกแบบสอบถาม
	$("input[id$='cancelvuser']").click(function(){
		disablePopup('backgroundPopup','popup');
	    document.getElementById('divPopUser').style.display ='none';
	    return false; //<----- return false ใส่ไว้ เพื่อ ไม่ต้องการ ให้เกิดการ postback 
	});
	//// Pop Up รายชื่อผู้กรอกแบบสอบถาม new
	$("input[id$='btncanceluser']").click(function(){
		disablePopup('backgroundPopup','popup');
	    document.getElementById('divPopUserAns').style.display ='none';
	    return false; //<----- return false ใส่ไว้ เพื่อ ไม่ต้องการ ให้เกิดการ postback 
	});
	//************** END CLOSING POPUP **************//
	//************** Click out event! **************//
	$("#backgroundPopup").click(function(){
		disablePopup('backgroundPopup','popup');
		document.getElementById('divPopRemark').style.display ='none';
		document.getElementById('divPopUser').style.display ='none';
	});
	//************** END out event! **************//
	//************** Press Escape event! **************//
	$(document).keypress(function(e){
		if(e.keyCode==27 && popupStatus==1){
			disablePopup('backgroundPopup','popup'); 
	        document.getElementById('divPopRemark').style.display ='none';
	         document.getElementById('divPopUser').style.display ='none';
		}
	});
	//************** END Escape event! **************//
// End ---ปิดฟังก์ชั่น การจัดการ popup ----------------------

// Begin ---เปิดฟังกชั่น การดึงรายชื่อผู้ตอบแบบสอบถาม-------------------
	$("span[id$='lbshowuser']").click(function(){ //เรียก linkbutton ใน masterpage
	    $("#divTable").html("");
        $.get('ajaxCompleTUsers.ashx?search=&ddl=', function(response) {
             $("#divTable").html(response);
             setPageforTable('mytable');
        });
        //Switch Object
 		DisplayDIV('popup','divPopUserAns');
 		get('divTable').style.display='';
		//centering with css
		centerPopup('backgroundPopup','popup');
		//load popup
		loadPopup('backgroundPopup','popup');
		$("span[id$='lbshead']").html('รายชื่อผู้กรอกแบบสอบถาม');
	});
	$("input[id$='btnsubmituser']").click(function(){ // กรณีกดปุ่ม submit ให้วนลูป ดึงรหัสพนักงานไปเก็บไว้ใน textbox
	    $('#mytable tr').each(function() {
            if (this.rowIndex) {
                var chk = $(this).find("input");   //ค้นหา tag input ใน row
                if(chk.attr("type") == 'checkbox' && chk.attr("checked"))
                {
                    var sid = $(this).find("td").eq(3).html();
                   
                    var sname = $(this).find("td").eq(1).html();
                    if( $('#divUsersend > div').size() == 0)
                    { 
                        $("#divUsersend").append("<div id='"+$(this).find("td").eq(3).html()+"' title='"+$(this).find("td").eq(1).html()+"' class='slide'>"+$(this).find("td").eq(1).html()+"<a style='cursor:pointer; cursor:hand;' herf\"#\" onclick=\"RemoveDiv('"+$(this).find("td").eq(3).html()+"')\"><img src='images/action_delete.png' border=0 ></a></div>");
                    }
                    else
                    {
                        var cstatus = '0';
                        $("#divUsersend").find('div').each(function(){
                            if($(this).attr("id") == sid)
                            {
                                cstatus = '1';
                                return false;
                            }
                        });
                        if(cstatus == '0')
                        {
                            $("#divUsersend").append("<div id='"+$(this).find("td").eq(3).html()+"' title='"+$(this).find("td").eq(1).html()+"' class='slide'>"+$(this).find("td").eq(1).html()+"<a style='cursor:pointer; cursor:hand;' herf\"#\" onclick=\"RemoveDiv('"+$(this).find("td").eq(3).html()+"')\"><img src='images/action_delete.png' border=0 ></a></div>");
                        }
                    }
                }
             }
        });
        disablePopup('backgroundPopup','popup');
        return false;
	});
	$("input[id$='v1ibtnsearch']").click(function(){
        $("#divTable").html("");
        $.get('ajaxCompleTUsers.ashx?search=' + encodeURIComponent($("input[id$='ajaxtxtsearch']").val())+'&ddl='+$("select[id$='v1ddltgroupuser']").attr("value"), function(response) {
                        $("#divTable").html(response);
                        setPageforTable('mytable');
        });
        return false;
	});
// End ---ปิดฟังกชั่น การดึงรายชื่อผู้ตอบแบบสอบถาม----------------------

// Begin ---เปิดฟังก์ชั่นการบันทึกข้อมูล
	$("input[id$='v2btnsubmit']").click(function(){ /*กรณีกดปุ่ม submit ให้วนลูป ดึงรหัสพนักงานไปเก็บไว้ใน textbox*/
	    var sid = '';
        $('#divUsersend div').each(function() {
            sid += $(this).attr("id")+',';
        });
        if(sid == '')
        {
            alert('กรุณาระบุผู้รับเมล์');return false;
        }
        $("input[id$='v2txtuserid']").attr("value",sid);//ctl00_cph_
        var str =xor_encrypt(squestid+'&'+$("input[id$='v2txtfaq']").val()+'&'+$("input[id$='v2txtsdate']").val()+'&'+$("input[id$='v2txtedate']").val()+'&'+$("input[id$='txtData']").val()+'&'+$("input[id$='v2txtuserid']").val(),7);
		$.ajax({
                            type: "POST",
                            url: "Questionnaire_new.ashx",
                            cache : false,
                            dataType: "text",
                            data: {strQueryString : encodeURI(str)},
                            success:  function(response) {
										 if($.trim(response) != '0')
										 {
											squestid = response;
											SaveTableQuize();
										 }
										 else
										 {
											alert('ไม่สามารถบันทึกข้อมูลได้ โปรดตรวจสอบอีกครั้ง!');
										 }
                                      }
                });  
        return false;
	});
// End --- ปิดฟังก์ชั่นการบันทึกข้อมูล

});
