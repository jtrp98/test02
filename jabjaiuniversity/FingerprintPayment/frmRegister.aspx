<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmRegister.aspx.cs" Inherits="frmRegister" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Fingerprints</title>
    <link rel="icon" type="image/png" href="/images/favicon-32x32.png" sizes="32x32">

    <!-- Include to new file  -->
    <script src="Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <link href="bootstrap/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <script src="bootstrap/js/bootstrap.js" type="text/javascript"></script>
    <script type="text/javascript">
            var tmpFuncSuccess = null;
            var tmpFuncCancel = null;

            function clearTmpFunc() {
                console.log("call clear");
                tmpFuncSuccess = null;
                tmpFuncCancel = null;
            }
            function closeModalOnKey(e) {
                if (e.which == 27 || e.which == 13) {
                    if (!$('#modalAlert').hasClass('in'))
                        return;
                    e.preventDefault();
                    e.stopPropagation();
                    document.removeEventListener("keydown", closeModalOnKey);
                    if (typeof tmpFuncCancel !== "function") {
                        tmpFuncSuccess();
                    }
                    else {
                        tmpFuncCancel();
                    }
                    clearTmpFunc();
                    $('#modalAlert').modal('hide');
                }
            }

            $(document).ready(function () {
                $('#modal-confirm').on('click', function (e) {
                    if (typeof tmpFuncSuccess === "function")
                        tmpFuncSuccess();
                    clearTmpFunc();
                    $('#modalAlert').modal('hide');
                });
                $('#modalAlert #modal-cancel').on('click', function (e) {
                    if (typeof tmpFuncCancel !== "function") {
                        tmpFuncSuccess();
                    }
                    else {
                        tmpFuncCancel();
                    }
                    clearTmpFunc();
                    $('#modalAlert').modal('hide');
                });
                $('#modalAlert #modal-close').on('click', function (e) {
                    if (typeof tmpFuncCancel !== "function") {
                        tmpFuncSuccess();
                    }
                    else {
                        tmpFuncCancel();
                    }
                    clearTmpFunc();
                    $('#modalAlert').modal('hide');
                });
            });
            var emptFunc = function () { };
            function showModal() {
                var header = arguments[0];
                var msg = arguments[1];
                if (typeof arguments[2] === "boolean") {
                    //show confirm footer
                    tmpFuncSuccess = arguments[3];
                    document.getElementsByClassName("modal-footer")[0].style.display = "block";
                    if (typeof arguments[4] === "function") {

                        tmpFuncCancel = arguments[4];
                    } else {
                        tmpFuncCancel = emptFunc;
                    }
                }
                else if (typeof arguments[2] === "function") {
                    document.getElementsByClassName("modal-footer")[0].style.display = "none";
                    tmpFuncSuccess = arguments[2];
                }
                else {
                    document.getElementsByClassName("modal-footer")[0].style.display = "none";
                }

                $('#modalAlert #modal-header').text(header);
                $('#modalAlert #modal-content').html(msg);
                document.addEventListener("keydown", closeModalOnKey);
                $('#modalAlert').modal('show');
            }


    </script>
    <!-- Close Include to new file  -->



    <script type="text/javascript">
<!--
    function OnClose() {
        if (window.opener != null && !window.opener.closed) {
            //                    window.opener.HideModalDiv();
            //                    var txtName = window.opener.getElementById("txtUserFinger1");
            //                    txtName.value = "test";
            //                    var form = window.opener.document.getElementsByTagName("aspnetform");
            //                    var txtName = GetElement(form, "span", "txtUserFinger1");
            //                    txtName.innerHTML = "test";
        }
    }
    window.onunload = OnClose;
    //-->
    </script>
    <script lang="javascript">
<!--
    var aaa = 0;
    var found = false;

    var param1var = getQueryVariable("page");
    var param2var = getQueryVariable("Firstname");
    var param3var = getQueryVariable("Lastname");
    var param4var = getQueryVariable("IDCARD");

    function getQueryVariable(variable) {
        var query = window.location.search.substring(1);
        var vars = query.split("&");
        for (var i = 0; i < vars.length; i++) {
            var pair = vars[i].split("=");
            if (pair[0] == variable) {
                return pair[1];
            }
        }
        showModal('Error Message', 'Query Variable ' + variable + ' not found');
    }

    function fnOpenDevice() {
        for (i = 0; i < 2; i++) {
            document.getElementById("frmmain").objFP[i].DeviceID = document.getElementById("frmmain").inc.value;
            document.getElementById("frmmain").objFP[i].CodeName = document.getElementById("frmmain").dev.value;
            document.getElementById("frmmain").objFP[i].MinutiaeMode = document.getElementById("frmmain").templateFormat.value;
            document.getElementById("frmmain").objFP[i].SecurityLevel = document.getElementById("frmmain").sec.value;
        }

        // template format of objVerify should be the same to those of objFP[]
        objVerify.MinutiaeMode = document.getElementById("frmmain").templateFormat.value;

        return;
    }
    setInterval(function () { fnCapture(0); }, 1000);
    function fnCapture(idx) {
        if (document.getElementById("frmmain").min[0].value != "") {
            idx = 1;
        }
        document.getElementById("frmmain").objFP[idx].Capture();
        var result = document.getElementById("frmmain").objFP[idx].ErrorCode;
        if (result == 0) {
            //var strimg1 = objFP.ImageTextData;

            var strmin = document.getElementById("frmmain").objFP[idx].MinTextData;
            document.getElementById("frmmain").min[idx].value = strmin;

            if (document.getElementById("frmmain").min[0].value != "" && document.getElementById("frmmain").min[1].value != "") {
                fnVerify();
                document.getElementById("frmmain").min[0].value = ""
                document.getElementById("frmmain").min[1].value = ""
            }
        }
        else
            //alert('failed - ' + result);

            return;
    }

    function fnRegister() {
        CheckFinger(document.getElementById("frmmain").txtCode1.value);
        if (found == true) {
            showModal('Message Info', 'ข้อมูลนิ้วมือนิ้วใดนิ้วหนึ่งซ้ำในฐานข้อมูล     กรุณาลองใหม่อีกครั้ง', function () {
                ClearData();
            });
        } else {
            // window.opener.document.getElementById("aspnetForm").ctl00$MainContent$txtUserFinger1.value = document.getElementById("frmmain").txtCode1.value;
            //window.opener.document.getElementById("aspnetForm").ctl00$MainContent$txtUserFinger2.value = document.getElementById("frmmain").txtCode2.value;
            window.dialogArguments.document.getElementById("aspnetForm").ctl00$MainContent$txtUserFinger1.value = document.getElementById("frmmain").txtCode1.value;
            window.dialogArguments.document.getElementById("aspnetForm").ctl00$MainContent$txtUserFinger2.value = document.getElementById("frmmain").txtCode2.value;
            showModal('Message Info', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131070") %>', function () {
                window.close();
            });
        }

        //            }
        //            else {
        //                alert('failed - ' + objVerify.ErrorCode);
        //            }

        //return;
    }

    function reloadParent() {
        var testData = document.getElementById("frmmain").txtCode1.value;
        var objPassedParentWindow = window.dialogArguments;
        objPassedParentWindow.returnedData(testData, document.getElementById("frmmain").txtCode2.value);
    }

    function CheckFinger(Finger) {
        var url = "";
        if (param1var != 'users') {
            "/App_Logic/dataGeneric.ashx?ID=" + param2var + "&mode=getuser&param3var=" + param3var + "&param4var=" + param4var;
        }
        else {
            "/App_Logic/dataGeneric.ashx?ID=" + param2var + "&mode=getemp&param3var=" + param3var + "&param4var=" + param4var;
        }
        $.ajax({
            type: "POST",
            url: url,
            cache: false,
            contentType: 'application/json;',
            success: function (obj) {
                $.each(obj, function (index) {
                    if (objVerify.VerifyForText(Finger, obj[index].sFinger) && objVerify.ErrorCode == 0) {
                        found = true;
                        return;
                    }
                    if (rs.fields(1) != "") {
                        if (objVerify.VerifyForText(Finger, obj[index].sFinger2) && objVerify.ErrorCode == 0) {
                            found = true;
                        }
                    }
                })

                found = false;
            }
        })
        <%--var conStr = '<%= JabJaiTutor.App_Code.fcommon.strConfigSqlConnection(JabJaiTutor.App_Code.fcommon.GetCookies()) %>';
        var connection = '<%= JabJaiTutor.App_Code.fcommon.strConfigSqlConnection(JabJaiTutor.App_Code.fcommon.GetCookies()) %>';
        var connectionstring = conStr;

        connection.Open();
        var rs = new ActiveXObject("ADODB.Recordset");

        if (param1var != 'users') {
            if (param2var == '') {
                rs.Open("SELECT sFinger,sFinger2 FROM TEmployees WHERE cDel IS NULL", connection);
            } else {
                rs.Open("SELECT sFinger,sFinger2 FROM TEmployees WHERE cDel IS NULL"
               + " AND ( sName = '" + param2var + "' OR sLastname = '" + param3var + "' OR sIdentification = '" + param4var + "')", connection);
            }

        } else {
            if (param2var == '') {
                rs.Open("SELECT sFinger,sFinger2 FROM TUser  WHERE cDel IS NULL", connection);
            } else {
                rs.Open("SELECT sFinger,sFinger2 FROM TUser  WHERE cDel IS NULL"
               + " AND ( sName = '" + param2var + "' OR sLastname = '" + param3var + "' OR sIdentification = '" + param4var + "')", connection);
            }
        }

        var i = rs.eof;
        //alert(i);
        if (i == false) {
            rs.MoveFirst
            while (!rs.eof) {
                if (rs.fields(0) != "") {
                    if (objVerify.VerifyForText(Finger, rs.fields(0)) && objVerify.ErrorCode == 0) {
                        found = true;
                        return;
                    }
                }
                if (rs.fields(1) != "") {
                    if (objVerify.VerifyForText(Finger, rs.fields(1)) && objVerify.ErrorCode == 0) {
                        found = true;
                    }

                }

                rs.movenext;
            }


            rs.close;
            connection.close;

            found = false;
            return;
        } else {
            found = false;
        }--%>

    }

        function fnVerifyEx() {
            var strmin1 = document.getElementById("frmmain").min[0].value;
            var strmin2 = document.getElementById("frmmain").min[1].value;
            var strmin3 = document.getElementById("frmmain").min[2].value;

            objVerify.SecurityLevel = document.getElementById("frmmain").sec.value;

            if (objVerify.VerifyExForText(strmin1, strmin2, strmin3) && objVerify.ErrorCode == 0)
                showModal('Message Info', 'Success - matched');
            else
                showModal('Message Info', 'Failed - ' + objVerify.ErrorCode);

            return;
        }

        function ChangeFinger() {
            document.getElementById("frmmain").txtFin.value = 2;
            ClearFinger();
        }

        function fnVerify() {
            var strmin1 = document.getElementById("frmmain").min[0].value;
            var strmin2 = document.getElementById("frmmain").min[1].value;

            objVerify.SecurityLevel = document.getElementById("frmmain").sec.value;

            if (document.getElementById("frmmain").txtFin.value != "2") {
                if (objVerify.VerifyForText(strmin1, strmin2) && objVerify.ErrorCode == 0) {
                    if (objVerify.VerifyForText(strmin1, strmin2) && objVerify.ErrorCode == 0) {
                        document.getElementById("frmmain").txtFinger1.value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103024") %>";
                        document.getElementById("frmmain").txtCode1.value = strmin1;

                        showModal("Message Info", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131071") %>", true, function () {
                            ChangeFinger();
                        }, function () { ClearData() });

                    } else {
                        document.getElementById("frmmain").txtFinger1.value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103025") %>";
                    }
                }
            } else {
                if (objVerify.VerifyForText(strmin1, strmin2) && objVerify.ErrorCode == 0) {
                    if (objVerify.VerifyForText(strmin1, strmin2) && objVerify.ErrorCode == 0) {
                        document.getElementById("frmmain").txtFinger2.value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103024") %>";
                        document.getElementById("frmmain").txtCode2.value = strmin1;
                        fnRegister();
                    } else {
                        document.getElementById("frmmain").txtFinger2.value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103025") %>";
                    }
                }
            }


            //            if (document.getElementById("frmmain").txtFinger1.value == "") {
            //                if (strmin1 != "" && strmin2 != "") {
            //                    if (objVerify.VerifyForText(strmin1, strmin2) && objVerify.ErrorCode == 0) {
            //                        //alert('Success - matched');
            //                        document.getElementById("frmmain").txtFinger1.value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103024") %>";
            //                        strmin1 = "";
            //                        strmin2 = "";


            //                        fnOpenDevice();

            //                    } else {
            //                        //alert('Failed - ' + objVerify.ErrorCode);
            //                        document.getElementById("frmmain").txtFinger1.value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103025") %>";
            //                    }
            //                } else {

            //                    if (document.getElementById("frmmain").txtFinger1.value != "") {
            //                        fnOpenDevice();

            //                        strmin1 = "";
            //                        strmin2 = "";
            //                    }
            //                }

            //            } else {
            //            if (strmin1 != "" && strmin2 != "") {
            //                if (objVerify.VerifyForText(strmin1, strmin2) && objVerify.ErrorCode == 0) {
            //                    //alert('Success - matched');
            //                    document.getElementById("frmmain").txtFinger2.value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103024") %>";

            //                    fnOpenDevice();

            //                    strmin1 = "";
            //                    strmin2 = "";
            //                } else {
            //                    //alert('Failed - ' + objVerify.ErrorCode);
            //                    document.getElementById("frmmain").txtFinger2.value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103025") %>";
            //                }
            //            } else {

            //                if (strmin1 != "" && strmin2 != "") {
            //                    fnOpenDevice();

            //                    strmin1 = "";
            //                    strmin2 = "";
            //                }
            //            }
            //            }

            strmin1 = "";
            strmin2 = "";


            //fnOpenDevice();


            return;
        }

        function fnSetimage() {
            document.getElementById("frmmain").objFP.ImageTextData = document.getElementById("frmmain").img1.value;

            return;
        }
        function ClearData() {
            document.getElementById("frmmain").txtFinger1.value = "";
            document.getElementById("frmmain").txtFinger2.value = "";

            document.getElementById("frmmain").txtCode1.value = "";
            document.getElementById("frmmain").txtCode2.value = "";

            document.getElementById("frmmain").min[0].value = "";
            document.getElementById("frmmain").min[1].value = "";

            document.getElementById("frmmain").txtFin.value = "1";
            //            document.getElementById("frmmain").min[2].value = "";


            document.getElementById("frmmain").objFP[0].Clear();
            document.getElementById("frmmain").objFP[1].Clear();

            //fnOpenDevice();
        }

        function ClearFinger() {
            //fnOpenDevice();
            document.getElementById("frmmain").objFP[0].Clear();
            document.getElementById("frmmain").objFP[1].Clear();
        }

        // -->

    </script>
    <link href="bootstrap/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link rel="stylesheet" href="Styles/style.css" />
</head>
<body onload="fnOpenDevice();" class="modal-registerfinger">
    <div style="margin: 15%; margin-top: 35px;">
        <object id="objVerify" style="left: 0px; top: 0px" height="0" width="0"
            classid="CLSID:8D613732-7D38-4664-A8B7-A24049B96117"
            name="objVerify" viewastext>
        </object>
        <form id="frmmain" name="frmmain" runat="server">
            <div>
                <div style="display: none;">
                    Device Type :
            <select name="dev">
                <option value="0">FDP02
            <option value="1">FDU02
            <option value="2">FDU03
            <option value="3">FDU04
            <option selected value="4">
                FDU05

            </select>

                    Instance No. :
            <select name="inc">
                <option value="-1">-1	
            <option value="0">0
            <option value="1">1
            <option value="2">2
            <option value="3">3
            <option value="4">4
            <option value="5">5
            <option value="6">6
            <option value="7">7
            <option value="8">8
            <option value="9">
                9
            </select>

                    <p>
                        Template Format:
            <select name="templateFormat">
                <option value="256">ANSI 378
            <option value="512">SG 400
            <option value="768">
                ISO 19794-2
            </select>

                        <input type="button" name="open" value='Open Device' onclick='fnOpenDevice();'>
                        <p>
                </div>
                <div style="display: none;">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102100") %> : </td>
                            <td>
                                <select name="sec">
                                    <option value="1">LOWEST	
                                    <option value="2">LOWER
                                    <option value="3">LOW
                                    <option value="4">BELOW_NORMAL
                                    <option selected value="5">NORMAL
                                    <option value="6">ABOVE_NORMAL
                                    <option value="7">HIGH
                                    <option value="8">HIGH
                                    <option value="9">
                                    HIGHEST
                                </select>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="container modal-registerfinger">
                    <div class="row">
                        <div class="col-xs-4">
                            <label class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102100") %>  :</label>
                        </div>
                        <div class="col-xs-8">
                            <select name="sec" class="mid-btn">
                                <option value="1">LOWEST	
                                    <option value="2">LOWER
                                    <option value="3">LOW
                                    <option value="4">BELOW_NORMAL
                                    <option value="5">NORMAL
                                    <option value="6">ABOVE_NORMAL
                                    <option value="7">HIGH
                                    <option value="8">HIGH
                                    <option selected value="9">
                                HIGHEST
                            </select>
                        </div>
                    </div>
                    <div class="row center" style="margin-top: 10px">
                        <label>Fingerprint Images</label>
                    </div>
                    <div class="row center">
                        <table border="1" style="margin-left: auto; margin-right: auto;">
                            <tr>
                                <td>
                                    <object id="OBJECT1" style="width: 149px; height: 182px;" height="182"
                                        width="149" classid="CLSID:D547FDD7-82F6-44e8-AFBA-7553ADCEE7C8" name="objFP" viewastext>
                                        <param name="CodeName" value="1">
                                    </object>
                                </td>
                                <td>

                                    <object id="objFP" style="width: 149px; height: 182px;" height="182"
                                        width="149" classid="CLSID:D547FDD7-82F6-44e8-AFBA-7553ADCEE7C8" name="objFP" viewastext>
                                        <param name="CodeName" value="1">
                                    </object>
                                </td>
                            </tr>
                        </table>

                        <input style="display: none" type="button" name="btnCapture1" value='Capture' onclick='fnCapture(0);'>
                        <input style="display: none" type="button" name="btnCapture2" value='Capture' onclick='fnCapture(1);'>
                    </div>
                    <div class="row" style="margin-top: 10px;">
                        <input id="txtFin" type="text" value="1" style="display: none;" />
                        <div class="col-xs-4">
                            <label class="pull-right">นิ้วที่  1 :</label>
                        </div>
                        <div class="col-xs-8">
                            <input id="txtFinger1" class="mid-btn" readonly="readonly" type="text" /><input id="txtCode1" type="text" style="display: none;" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4">
                            <label class="pull-right">นิ้วที่  2 :</label>
                        </div>
                        <div class="col-xs-8">
                            <input id="txtFinger2" class="mid-btn" readonly="readonly" type="text" /><input id="txtCode2" type="text" style="display: none;" />
                        </div>
                    </div>
                    <div class="row center" style="margin-top: 15px;">
                        <%-- <input style="" type=button name=btnNext value='Next Finger' OnClick='ChangeFinger();' class="long-btn btn btn-primary" ><br /><br />--%>
                        <input style="width: 100px;" type="button" name="btnRegister" value='Register' onclick='fnRegister();' class="btn-success">
                        <input style="width: 100px;" type="button" name="btnCancel" value='Clear' onclick='ClearData();' class="btn-danger">
                    </div>
                </div>
                <p>
                    <input type="text" name="min" style="display: none;">
                    <input type="text" name="min" style="display: none;">
                    <p>
                        &nbsp;<br>
                        <input type="button" name="btnVerify" value='VerifyEx' onclick='fnVerifyEx();' style='width: 200px; display: none;'>
                        <br>
                        <input type="button" name="btnVerify" value='Verify' onclick='fnVerify();' style='width: 200px; display: none;'>
            </div>
        </form>
    </div>

    <!-- Include to new file -->
    <div id="modalAlert" class="modal fade alertBoxInfo" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-md" style="top: 150px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="modal-close" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 style="font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 20px; text-align: center; font-weight: bold" class="modal-title" id="modal-header">Modal title</h4>
                </div>
                <div class="modal-body" id="modal-content" style="padding: 40px 10px; text-align: center; font-size: 19px; font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-weight: normal;">
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <button type="button" id="modal-cancel" class="btn btn-default" style="font-size: 16px !important" data-dismiss="modal">Cancel</button>
                    <button type="button" id="modal-confirm" class="btn btn-primary" style="font-size: 16px !important">Confirm</button>
                </div>
            </div>
        </div>
    </div>
    <!-- Close Include to new file -->
</body>
</html>
