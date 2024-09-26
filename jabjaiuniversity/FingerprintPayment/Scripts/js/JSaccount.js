$(document).ready(function(){
    $("input[id$='txtsPassword2']").change(function() {
        var _value1 = $("input[id$='txtsPassword']").attr("value");
        var _value2 = $("input[id$='txtsPassword2']").attr("value");
       if(_value2 != _value1)
       {
            alert('คุณระบุ password ไม่ตรงกัน กรุณาตรวจสอบและระบุใหม่ !');
            $("input[id$='txtsPassword2']").attr("value","");
            return false;
       }

    });
    $("select[id$='ddlProvice2']").change(function() {
        $("select[id$='ddlTumbon2']").html("");
        $("select[id$='ddlAumpur2']").html("");
        var _id = $("select[id$='ddlProvice2']").attr("value");
        _id = (_id != '') ? _id : "";
        $("select[id$='ddlTumbon2']").fadeOut();
        $("select[id$='ddlAumpur2']").fadeOut();
        $('#loadingTumbon2').fadeIn();
        $('#loadingAumpur2').fadeIn();
        var str =xor_encrypt(_id,7);
        $.ajax({
                            type: "POST",
                            url: "JsonTtumbon.ashx",
                            cache : false,
                            dataType: "json",
                            data: {strQueryString : encodeURI(str)},
                            success:  function(response) {
                                         $.each(response, function() {                        
                                            $("select[id$='ddlTumbon2']").append($("<option></option>").val(this['stumbonid']).html(this['stumbonname']));
                                         });
                                         $('#loadingTumbon2').fadeOut();
                                         $("select[id$='ddlTumbon2']").fadeIn();
                                         $.ajax({
                                            type: "POST",
                                            url: "JsonTAmphur.ashx",
                                            cache : false,
                                            dataType: "json",
                                            data: {strQueryString : encodeURI(str)},
                                            success:  function(response) {
                                                         $.each(response, function() {                        
                                                            $("select[id$='ddlAumpur2']").append($("<option></option>").val(this['svalue']).html(this['stxt']));
                                                         });
                                                         $('#loadingAumpur2').fadeOut();
                                                         $("select[id$='ddlAumpur2']").fadeIn();
                                                      }
                                        });   
                                      }
               });   

    });
    $("select[id$='ddlsRegion2']").change(function() {
        $("select[id$='ddlProvice2']").html("");
        var _id = $("select[id$='ddlsRegion2']").attr("value");
        _id = (_id != '') ? _id : "";
        $("select[id$='ddlProvice2']").fadeOut();
        $('#loadingProvice2').fadeIn();
        var str =xor_encrypt(_id,7);
        $.ajax({
                            type: "POST",
                            url: "JsonTProvince.ashx",
                            cache : false,
                            dataType: "json",
                            data: {strQueryString : encodeURI(str)},
                            success:  function(response) {
                                         $.each(response, function() {                        
                                            $("select[id$='ddlProvice2']").append($("<option></option>").val(this['svalue']).html(this['stxt']));
                                         });
                                         $('#loadingProvice2').fadeOut();
                                         $("select[id$='ddlProvice2']").fadeIn();
                                      }
               });   

    });
    $("select[id$='ddlprovince']").change(function() {
        $("select[id$='ddlstumbon']").html("");
        var _id = $("select[id$='ddlprovince']").attr("value");
        _id = (_id != '') ? _id : "";
        $("select[id$='ddlstumbon']").fadeOut();
        $('#loadingTumbon').fadeIn();
        var str =xor_encrypt(_id,7);
        $.ajax({
                            type: "POST",
                            url: "JsonTtumbon.ashx",
                            cache : false,
                            dataType: "json",
                            data: {strQueryString : encodeURI(str)},
                            success:  function(response) {
                                         $.each(response, function() {                        
                                            $("select[id$='ddlstumbon']").append($("<option></option>").val(this['stumbonid']).html(this['stumbonname']));
                                         });
                                         $('#loadingTumbon').fadeOut();
                                         $("select[id$='ddlstumbon']").fadeIn();
                                         $("span[id$='lbmsgSystem']").html("");
                                      }
               });   

    });
    $("select[id$='ddlstumbon']").change(function() {
        var _id = $("select[id$='ddlstumbon']").attr("value");
        _id = (_id != '') ? _id : "";
        $("select[id$='ddlstumbon']").fadeOut();
        $("span[id$='lbmsgSystem']").html("กรุณารอสักครู่ ระบบกำลังตรวจสอบข้อมูล..");
        $("span[id$='lbmsgSystem']").fadeIn();
        $('#loadingTumbon').fadeIn();
        var str =xor_encrypt(_id,7);
        $.ajax({
                            type: "POST",
                            url: "AjaxGetAccounTUsers.ashx",
                            cache : false,
                            dataType: "html",
                            data: {strQueryString : encodeURI(str)},
                            success:  function(response) {
                                         $("span[id$='lbmsgSystem']").html(response);
                                         $("span[id$='lbmsgSystem']").fadeIn();
                                         $('#loadingTumbon').fadeOut();
                                         $("select[id$='ddlstumbon']").fadeIn();
                                         if(response != '')
                                            $("select[id$='ddlstumbon']").attr("value","");
                                      }
               });   

    });
    $("input[id$='txtsUsername']").change(function() {
        var _id = $("input[id$='txtsUsername']").attr("value");
        _id = (_id != '') ? _id : '';
        $("input[id$='txtsUsername']").attr("value","");
        $("span[id$='lbmsgCheckUser']").html("กรุณารอสักครู่ ระบบกำลังตรวจสอบข้อมูล..");
        $("span[id$='lbmsgCheckUser']").fadeIn();
        $('#loadingCheckUser').fadeIn();
        var str =xor_encrypt(_id,7);
        $.ajax({
                            type: "POST",
                            url: "AjaxCheckAccounTUsers.ashx",
                            cache : false,
                            dataType: "html",
                            data: {strQueryString : encodeURI(str)},
                            success:  function(response) {
                                         if(response == '0')
                                         {
                                            $("input[id$='txtsUsername']").attr("value",_id);
                                            $("span[id$='lbmsgCheckUser']").html("<img src = 'images/002_29.gif' border=0 \> Username สามารถใช้งานได้");
                                         }
                                         else
                                         {
                                            $("input[id$='txtsUsername']").attr("value","");
                                            $("span[id$='lbmsgCheckUser']").html("<img src = 'images/002_30.gif' border=0 \> Username: \""+_id+"\" ไม่สามารถใช้งานได้ กรุณาระบุใหม่");
                                         }
                                         $('#loadingCheckUser').fadeOut();
                                         $("span[id$='lbmsgCheckUser']").fadeIn();
                                      }
               });   

    });
    $("a[id$='lbtResetPassword']").click(function() {
        var _id = $("span[id$='lbsUserID']").html();
        var _email = $("input[id$='txtsEmail']").attr("value");
        var _susername = $("input[id$='txtsUsername']").attr("value");
        if(_email == '' || _susername == ''){alert('กรุณากรอกข้อมูลอีเมล์และusername ก่อนที่จะกดปุ่ม reset password');return false;}
        if(_id == null) {alert('ไม่สามารถ Reset Password ได้ \nเนื่องจาก ข้อมูลเจ้าหน้าที่บริหารข้อมูลดังกล่าว ยังไม่ถูกสร้างไว้ในระบบ\nกรุณาสร้างผู้ใช้งานนี้ก่อน');return false;}
        if(confirm('คุณแน่ใจที่จะ Reset Password ใช่หรือไม่'))
        {
            $('#loadingResetPassword').fadeIn();
            var str =xor_encrypt(_id+'&'+_email+'&'+_susername,7);
            $.ajax({
                            type: "POST",
                            url: "AjaxResetPassword.ashx",
                            cache : false,
                            dataType: "html",
                            data: {strQueryString : encodeURI(str)},
                            success:  function(response) {
                                         if(response != '')
                                         {
                                            $("input[id$='txtsPassword']").attr("value",response);
                                            $("input[id$='txtsPassword2']").attr("value",response);
                                            alert('ระบบได้ทำการส่ง UserName และ Password ใหม่กลับไปให้ทางอีเมล์เรียบร้อย!');
                                            $('#loadingResetPassword').fadeOut();
                                         }
                                      }
            });   
            
        }
        return false;
    });
});