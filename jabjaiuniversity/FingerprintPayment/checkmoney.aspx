<%@ Page Title="" Language="C#" MasterPageFile="~/mp2.Master" AutoEventWireup="true"
    CodeBehind="checkmoney.aspx.cs" Inherits="FingerprintPayment.checkmoney" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript">
<!--
        shortcut.add("Ctrl+N", function () {
            $('input[id*=btnSavepage1]').click();
        });
        shortcut.add("Ctrl+B", function () {
            $('input[id*=btnCancel2]').click();
        });
        shortcut.add("ESC", function () {$('input[id*=btnCancel]').click();
        });
        shortcut.add("Ctrl+Shift", function () {
            $('input[id*=btnSave]').click();
        });
        shortcut.add("Ctrl+P", function () {
            fnCapture();
        });
        shortcut.add("Ctrl+C", function () {
            Clear();
        });
        function Clear() {
            $('input[id*=txtsID]').prop("disabled", false);
            $("input[id*=txtsID]").val('');
            $("input[id*=txtCheckFinger]").val('');
            $("input[id*=txtsName]").val('');
            $("input[id*=txtsLastName]").val('');
            $("input[id*=txtnBalance]").val('');
        }
        function j_infosell(msg) {
            //            Sexy.info("<br/><h1>&nbsp;&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %></h1><br/><p>&nbsp;&nbsp;&nbsp;" + msg + "</p>",
            //            { onComplete:
            //			        function (returnvalue) {
            //			            if (returnvalue) {
            //			                $("input[id*=txtsBarCode]").focus();
            //			            }
            //			        }
            //            });
            showModal("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", msg, function () {
                $("input[id*=txtsBarCode]").focus();
                $('input[id*=txtsBarCode]').select();
            });

            $('#modalAlert').on('hidden.bs.modal', function () {
                $("input[id*=txtsBarCode]").focus();
                $('input[id*=txtsBarCode]').select();
            });
        }

        function postbackReady() {
            $("input[id*=txtsBarCode]").focus();
        }
        $(document).ready(function () {
            $("input[id*=txtsBarCode]").keypress(function (e) {
                if ($("input[id*=txtsBarCode]").val() == "") {
                    $("input[id*=txtsBarCode]").focus();
                }
                var sID = $("input[id*=txtsBarCode]").val();
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
                }
            });
        });

    </script>
    <script language="Javascript" type="text/javascript">
        $(document).ready(function () {
            fnOpenDevice();
        });

        setInterval(function () { fnCapture(0, 'CheckMoney'); }, 1000);

        //setInterval(function () { fnCapture(0, 'CheckMoney'); }, 1000);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <object id="objSecuBSP" classid="CLSID:6283f7ea-608c-11dc-8314-0800200c9a66" height="0"
        name="objSecuBSP" style="left: 0px; top: 0px" viewastext="" width="0">
    </object>
    <div class="detail-card box-content">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="row" style="display: none;">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111043") %></label>
            </div>
            <div class="col-xs-8">
                <asp:Button ID="btnRegister" runat="server" OnClientClick="fnCapture();return false;"
                    class="btn btn-info" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132149") %>" UseSubmitBehavior="False" />
                <span id="div" style="clear: both; width: 100px;"></span>&nbsp;
                <asp:TextBox ID="txtCheckFinger" runat="server" Style="display: none;" Text="1" />
                <asp:TextBox ID="txtUserFinger" runat="server" Style="display: none;" Text="1" />
                <asp:RequiredFieldValidator ID="revtxtCheckFinger" runat="server" Display="None"
                    SetFocusOnError="true" ErrorMessage="<span style='font-size:12'><b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02719") %> !</b><br /><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111043") %><br>"
                    ControlToValidate="txtCheckFinger" ValidationGroup="add" />
                <cc1:ValidatorCalloutExtender runat="server" ID="vcetxtCheckFinger" TargetControlID="revtxtCheckFinger"
                    HighlightCssClass="validatorcallouthighlight" Width="200px" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105022") %></label>
            </div>
            <div class="col-xs-8">
                <asp:TextBox ID="txtsID" runat="server" MaxLength="4" class="input--mid" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></label>
            </div>
            <div class="col-xs-8">
                <asp:TextBox ID="txtsName" runat="server" MaxLength="512" ReadOnly="True" class="input--mid" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></label>
            </div>
            <div class="col-xs-8">
                <asp:TextBox ID="txtsLastName" runat="server" ReadOnly="True" class="input--mid" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106151") %></label>
            </div>
            <div class="col-xs-8">
                <asp:TextBox ID="txtnBalance" runat="server" MaxLength="13" ReadOnly="True" class="input--mid" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-4">
                <label class="pull-right">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106151") %></label>
            </div>
            <div class="col-xs-8">
                <asp:TextBox ID="txtnMoney" runat="server" class="input--mid" ReadOnly="True" />
            </div>
        </div>
        <div class="row mini--space__top">
            <div class="col-xs-12 center">
                <%--<asp:Button ID="btnSave" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>" ValidationGroup="add" class="btn btn-primary" />--%>
                &nbsp;<asp:Button ID="btnCancel" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %> (Ctrl+C)" class="btn btn-danger"
                    UseSubmitBehavior="False" />
            </div>
        </div>

           <ul class="nav nav-tabs">
                <li class="active"><a data-toggle="tab" href="#tabMenu1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131053") %></a></li>
                <li><a data-toggle="tab" href="#tabMenu2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102227") %></a></li>
                <li><a data-toggle="tab" href="#tabMenu3">ข้อมุลการขาด</a></li>
                <li><a data-toggle="tab" href="#tabMenu4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132132") %></a></li>
            </ul>

                      <div class="tab-content">
                <div id="tabMenu1" class="tab-pane fade in active">
                     <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131053") %></h3>
                     <p>xx tab1</p>
                </div>
                <div id="tabMenu2" class="tab-pane fade">
                     <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102227") %></h3>
                      <p>Some content in menu 1.</p>
                </div>
                <div id="tabMenu3" class="tab-pane fade">
                      <h3>ข้อมุลการขาด</h3>
                     <p>Some content in menu 2.</p>
                </div>
                 <div id="tabMenu4" class="tab-pane fade">
                      <div class="divReportData">
                      <div class="col-md-12" style="min-height:850px">
                      <div class="col-md-6"> <div id="container" style="width:270px; height:auto; margin: 0 auto"></div></div>
                      <div class="col-md-6"><div id="container2" style="width:270px; height:auto; margin: 0 auto"></div> </div>
                         </div>
                     <br/>
                     <br/>
                     <br/>
                     <br/>
                     <br/> <br/>
                     <br/>
                     <br/>
                     <br/>
                     <br/>
                       </div>
                </div>
            </div>
    </div>
    
    <script>
        $(function () {
            getReportByID("1111");
        });
        function getReportByID(userID) {
            $.ajax({
                url: "/App_Logic/dataJSON.aspx?mode=userreportbyid&years=2015&userid=" + userID,
                success: function (respjson) {
                    var strJSON = respjson.replace("\"", '"');
                    var arrSplit = strJSON.split("split");
                    var chartOBJ = $.parseJSON(arrSplit[0]);
                    var chartOBJ2 = $.parseJSON(arrSplit[1]);
                    var name1 = ["<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %>"];
                    var name2 = ["<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105017") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131055") %>"];
                    var userName = chartOBJ[0].Name;
                    if (userName == undefined) {
                        userName = " -";
                    }
                    LineChart(chartOBJ, "container", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131056") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131057") %>" + userName + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210455") %> ", name1);
                    userName = chartOBJ2[0].Name;
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
                    width: 300
                },
                title: {
                    text: title + "2558"
                },
                subtitle: {
                //   text: 'Source: WorldClimate.com'
            },
            xAxis: {
                categories:
             objs[0].Month
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
                color: "green"

            },
                    {
                        name: name[1],
                        data: objs[0].Count2,
                        color: "red"

                    },
                    {
                        name: name[2],
                        data: objs[0].Count3,
                        color: "gray"

                    }
                  ]
        });
    }
    </script>
</asp:Content>
