<%@ Page Title="" Language="C#" MasterPageFile="~/mp2.Master" AutoEventWireup="true"
    CodeBehind="profilescaning.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.profilescanning" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript">


        shortcut.add("Ctrl+N", function () {
            $('input[id*=btnSavepage1]').click();
        });

        shortcut.add("Ctrl+C", function () {
            Clear();
        });



        function j_infosell(msg) {

            showModal("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", msg, function () {
                $("input[id*=txtsBarCode]").focus();
                $('input[id*=txtsBarCode]').select();
                Clear();
            });

            $('#modalAlert').on('hidden.bs.modal', function () {
                $("input[id*=txtsBarCode]").focus();
                $('input[id*=txtsBarCode]').select();
                Clear();
            });
        }

        function hideResult() {

            $("#resultScan").hide();
            /* $('input[id*=txtsID]').prop("disabled", false);
            $('input[id*=txtsID]').val('');
            $("input[id*=txtsName]").val('');
            $("input[id*=txtsLastName]").val('');
            $("input[id*=txtnBalance]").val('');*/
            // Clear();
        }


        function hideScan(name, lname, money, nMax, idUser) {
            $("#resultScan").show();
            $("input[id*=txtsName]").val(name);
            $("input[id*=txtsLastName]").val(lname);
            $("input[id*=txtnBalance]").val(money);
            $("input[id*=txtnMax]").val(nMax);

        }

        function postbackReady() {
            $("input[id*=txtsBarCode]").focus();
        }


        $(document).ready(function () {
            fnOpenDevice();
            setInterval(function () { fnCapture(0, 'JobScan'); }, 1000);
            $("input[id*=txtsID]").focus();
            $('input[id*=txtsID]').keyup(function () {
                if ($('input[id*=txtsID]').val().length == 4) {
                    $('input[id*=txtsID]').prop("disabled", true);
                    // $('input[id*=btnSavepage1]').click();
                }
            });
            $("#spanText").text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132634") %>");
        });




        /*
        if (e.keyCode == 13) {
        $.ajax({
        type: "POST",
        url: "PriceProduct.ashx",
        cache: false,
        dataType: "html",
        data: { ID: encodeURIComponent(sID) },
        success: function (response) {
        var _str = response;
        if (_str != "") {
        $('span[id*=productname]').html(_str.split('|')[1]);
        $('span[id*=productprice]').html(_str.split('|')[0]);
        }
        else {
        $('span[id*=productname]').html("");
        $('span[id*=productprice]').html("");
        //                                fnCapture();
        }
        }
        });
        $("input[id*=txtnNumber]").val("1");
        $("input[id*=txtnNumber]").select();
        if ($("input[id*=txtsBarCode]").attr("readonly"))
        return true;
        else
        return false;
                        
        }*/

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableViewState="true" EnablePartialRendering="true"
        EnableScriptGlobalization="true">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <style>
                .detail-card
                {
                    font-family: thaifont;
                    color: #555;
                    padding: 30px 10px;
                    width: 100% !important;
                    margin: 0 auto;
                    background-color: white;
                    min-height: 600px;
                }
                .txt-font-20px
                {
                    font-size: 20px;
                    padding: 0px 0px 2px 3px;
                }
                .txt-font-25px
                {
                    font-size: 25px;
                    padding: 0px 0px 2px 3px;
                }
                .txt-right
                {
                    text-align: right;
                }
            </style>
            <object id="objSecuBSP" classid="CLSID:6283f7ea-608c-11dc-8314-0800200c9a66" height="0"
                name="objSecuBSP" style="left: 0px; top: 0px" viewastext="" width="0">
            </object>
            <div class="detail-card top-buffer div-check" style="margin-top: -70px;">
                <div class="col-md-12">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="phoneField" class="col-xs-3">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %> :</label>
                            <div class="col-xs-9">
                                <asp:TextBox ID="txtsID" runat="server" MaxLength="4" class="form-control txt-font-25px" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="name" class="col-xs-3">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %> :</label>
                            <div class="col-xs-9">
                                <asp:TextBox ID="txtsName" runat="server" class="form-control disabled txt-font-25px"
                                    disabled />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="lname" class="col-xs-4">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %> :</label>
                            <div class="col-xs-8">
                                <asp:TextBox ID="txtsLastName" runat="server" class="form-control disabled txt-font-25px"
                                    disabled />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-1">
                        <asp:Button ID="btnClear" runat="server" AccessKey="N" OnKeyPress="return false;"
                            OnClientClick="Clear(); " TabIndex="100" Text="Clear(Ctrl+C)" UseSubmitBehavior="False"
                            class="btn btn-primary" />
                        <div style="display: none;">
                            <asp:Button ID="btnRegister" runat="server" OnClientClick="fnCapture(); return false;"
                                class="btn btn-info" Text="ตรวจสอบรายนิ้วมือ(Ctrl+P)" />
                            <span id="div" style="clear: both; width: 100px;"></span>&nbsp;
                            <asp:TextBox ID="txtCheckFinger" runat="server" Style="display: none;" Text="1" />
                            <asp:TextBox ID="txtUserFinger" runat="server" Style="display: none;" Text="1" />
                            <asp:RequiredFieldValidator ID="revtxtCheckFinger" runat="server" Display="None"
                                SetFocusOnError="true" ErrorMessage="<span style='font-size:12'><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %> !</b><br /><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111043") %><br>"
                                ControlToValidate="txtCheckFinger" ValidationGroup="add" />
                            <cc1:ValidatorCalloutExtender runat="server" ID="vcetxtCheckFinger" TargetControlID="revtxtCheckFinger"
                                HighlightCssClass="validatorcallouthighlight" Width="200px" />
                            <asp:Button ID="btnSavepage1" runat="server" AccessKey="N" OnKeyPress="return false;"
                                TabIndex="100" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %>(Ctrl+N)" UseSubmitBehavior="False" class="btn btn-primary" />
                        </div>
                    </div>
                </div>
                <div class="col-md-12 top-buffer">
                    <div class="col-md-3">
                        <div>
                            <fieldset>
                                <legend>
                                    <h3>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131134") %></h3>
                                </legend>
                                <div class="row form-group">
                                    <label for="lname" class="col-xs-6" style="font-size: 20px;">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M602009") %></label>
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="txtnBalance" runat="server" align="right" class="form-control txt-font-25px disabled txt-right"
                                            disabled />
                                    </div>
                                </div>
                                <div class="row form-group">
                                    <label for="lname" class="col-xs-6" style="font-size: 20px;">
                                        วงเงินจำกัดต่อวัน :</label>
                                    <div class="col-xs-6">
                                        <input type="text" class="form-control txt-font-25px txt-right" align="right" id="txtnMax"
                                            disabled />
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div>
                            <fieldset>
                                <legend>
                                    <h3>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131137") %></h3>
                                </legend>
                                <div class="row form-group">
                                    <label for="lname" class="col-xs-7" style="font-size: 20px;">
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131138") %> :</label>
                                    <div class="col-xs-5">
                                        <b style="padding: 0px; font-size: 200px; margin-top: -105px; margin-left: -20px;
                                            position: absolute;">&nbsp;</b>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div class="col-md-9">
                        <fieldset>
                            <legend>
                                <h3>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131141") %></h3>
                            </legend>
                            <div id="container" class="col-md-6" style="background: #c0c0c0; height: auto; margin: 0 auto">
                                <div style="padding: 25% 30%">
                                    <b style="color: white; font-size: 40px;">Graph</b></div>
                            </div>
                            <div id="container2" class="col-md-6" style="background: #c0c0c0; height: auto; margin: 0 auto">
                                <div style="padding: 25% 30%">
                                    <b style="color: white; font-size: 40px;">Graph</b></div>
                            </div>
                            <%--<div id="container3" class="col-md-4"  style="background:gray; height:auto; margin: 0 auto"></div> --%>
                        </fieldset>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        $(function () {
            var ele_contanier = $(".div-check").closest(".container");
            $(ele_contanier).removeClass();

        });
        function getReportByID(userID, fullname) {

            $("#container").html('<div style="padding:25% 30%"><b style="color:white;font-size:40px;">Loading...</b></div>');
            $("#container2").html('<div style="padding:25% 30%"><b style="color:white;font-size:40px;">Loading...</b></div>');
            $.ajax({
                url: "/App_Logic/dataJSON.aspx?mode=userreportbyid&years=2015&userid=" + userID,
                success: function (respjson) {
                    var strJSON = respjson.replace("\"", '"');
                    var arrSplit = strJSON.split("split");
                    var chartOBJ = $.parseJSON(arrSplit[0]);
                    var chartOBJ2 = $.parseJSON(arrSplit[1]);
                    var name1 = ["<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %>"];
                    var name2 = ["<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105017") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131055") %>"];
                    var userName = fullname; //chartOBJ[0].Name;
                    if (userName == undefined) {
                        userName = " -";
                    }
                    $("#container").html('');
                    $("#container2").html('');
                    $("#container").css({ "background": "" });
                    $("#container2").css({ "background": "" });
                    LineChart(chartOBJ, "container", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131056") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131057") %>" + userName + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210455") %> ", name1);
                    userName = fullname;
                    if (userName == undefined) {
                        userName = " -";
                    }
                    LineChart(chartOBJ2, "container2", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131051") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131052") %>" + userName + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210455") %> ", name2);
                }
            });
        }

        function LineChart(objs, elem, fileexp, title, name) {

            if (name == undefined) {
                name = "-";
            }
            $('#' + elem).highcharts({
                chart: {
                    type: 'column'
                },
                exporting: {
                    url: '/HighchartsExport.axd',
                    filename: fileexp,
                    width: 500
                },
                title: {
                    text: title + "2558"
                },
                subtitle: {
                    //   text: 'Source: WorldClimate.com'
                },
                xAxis: {
                    categories: objs[0].Month
                    // categories: ['<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210010") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131013") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210012") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210013") %>', '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210014") %>','<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210015") %>','<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210016") %>','<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210017") %>','<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210018") %>','<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210019") %>','<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210020") %>','<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210021") %>']
            ,
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %> (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %>)'
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.1f} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %></b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true
                },
                plotOptions: {
                    column: {
                        pointPadding: 0.2,
                        borderWidth: 0
                    }
                },
                series: [{
                    name: name[0],
                    data: objs[0].Count,
                    // data:[1,2,3,4,5,6,7,8,9,10,11,12],
                    color: "green"

                },
                    {
                        name: name[1],
                        data: objs[0].Count2,
                        // data: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12],
                        color: "red"

                    },
                    {
                        name: name[2],
                        data: objs[0].Count3,
                        // data: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12],
                        color: "gray"

                    }
                  ]
            });
        }
    </script>
</asp:Content>
