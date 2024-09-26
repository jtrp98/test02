<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="invoices.aspx.cs" Inherits="FingerprintPayment.import.invoices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../Scripts/tableExport/tableExport.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jquery.base64.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/sprintf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/jspdf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/base64.js"></script>

    <script src="/bootstrap SB2/bower_components/flot/excanvas.min.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.pie.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.resize.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.time.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot.tooltip/js/jquery.flot.tooltip.min.js" type="text/javascript"></script>
    <link href="../Styles/SettingDialog.css" rel="stylesheet" />
    <script src="../javascript/jquery-number/jquery.number.js" type="text/javascript"></script>
    <script src="../Scripts/FileSaver.js" type="text/javascript"></script>
    <link href="../../Content/jquery-confirm.css" rel="stylesheet" />
    <script src="/Scripts/jquery-confirm.js"></script>

    <script type="text/javascript">

        $(function () {

            $("select[id*=ddlyear]").change(function () {
                getListTrem();
            });

            getListTrem();
            LoadTermSubLevel2($("#sltLevel").val(), '#sltClass');

            $("#sltLevel").change(function () {
                LoadTermSubLevel2($(this).val(), '#sltClass');
            });

            $("#btnImport").click(function () {
                if (document.getElementById("upload").files.length > 0) {
                    let nTerm = $('select[id*=semister]').val();
                    let nTSubLevel = $('select[id*=sltLevel]').val();
                    let nTermSubLevel2 = $('select[id*=sltClass]').val();

                    var formData = new FormData();
                    var objFiles = $('input[type="file"]').get(0);
                    var files = objFiles.files;
                    for (var i = 0; i < files.length; i++) {
                        formData.append('_pr_' + i, files[i]);
                    }

                    $("body").mLoading();
                    $.ajax({
                        url: '/import/invoices_Import.ashx?nTerm=' + nTerm + "&nTSubLevel=" + nTSubLevel + "&nTermSubLevel2=" + nTermSubLevel2,
                        data: formData,
                        dataType: 'json',
                        type: 'POST',
                        contentType: false,
                        processData: false,
                        success: function (result) {
                            //do some tasks after upload
                            $("body").mLoading('hide')
                            console.log(result);
                            $("#upload").val('');
                            if (result.resultCode == 200) {
                                $.confirm({
                                    title: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101319") %></h3>',
                                    content: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132305") %></h3>',
                                    theme: 'bootstrap',
                                    buttons: {
                                        "<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": function () {
                                        }
                                    }
                                });
                            } else {
                                $.confirm({
                                    title: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101319") %></h3>',
                                    content: "<h3>" + result.message + "</h3>",
                                    theme: 'bootstrap',
                                    buttons: {
                                        "<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": function () {
                                        }
                                    }
                                });
                            }
                        },
                        error: function (response) {
                            $("body").mLoading('hide')
                            console.log(response);
                            $("#upload").val('');
                        }
                    });
                }
                else {
                    $.confirm({
                        title: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101319") %></h3>',
                        content: "<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132306") %></h3>",
                        theme: 'bootstrap',
                        buttons: {
                            "<span style=\"font-size: 20px;\"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></span>": function () {
                            }
                        }
                    });
                }
            });


        });

        function getListTrem() {
            var YearID = $('#ctl00_MainContent_ddlyear option:selected').val();
            var YearNumber = $('#ctl00_MainContent_ddlyear option:selected').text();
            $("#ctl00_MainContent_semister option").remove();
            if (YearID) {
                $.ajax({
                    url: "/App_Logic/dataGeneric.ashx?mode=listterm&id=" + YearID,
                    success: function (msg) {
                        trem = msg;
                        $.each(msg, function (index) {
                            $('select[id*=semister]')
                                .append($("<option></option>")
                                    .attr("value", msg[index].nTerm)
                                    .text(msg[index].sTerm));
                        });
                        //setMaxMinDate();
                    }
                });
            }
        }

        function export_excel() {
            //$("body").mLoading();
            var xhr;

            //this.RenderHtml_Detail('table_exports', true);
            xhr = new XMLHttpRequest();
            xhr.open("POST", "/import/invoices.aspx/ExportExcl", true);
            xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
            xhr.responseType = 'blob';
            xhr.onload = function () {
                saveAs(xhr.response, "ImportInvoice.xlsx");
                //$("body").mLoading('hide');
            };
            xhr.send();
        }

        function LoadTermSubLevel2(subLevelID, objResult) {
            if (subLevelID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "/StudentInfo/StudentList.aspx/LoadTermSubLevel2",
                    data: '{subLevelID: ' + subLevelID + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var subLevel2 = response.d;

                        $(objResult).empty();

                        if (subLevel2.length > 0) {

                            var options = '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103022") %></option>';
                            $(subLevel2).each(function () {

                                options += '<option value="' + this.id + '">' + this.name + '</option>';

                            });

                            $(objResult).html(options);
                        }
                    },
                    failure: function (response) {
                        console.log(response.d);
                    },
                    error: function (response) {
                        console.log(response.d);
                    }
                });
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card box-content userlist-container col-xs-12" style="background-color: white;">
        <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" />

        <asp:HiddenField ID="hdfsid" runat="server" />
        <div id="loading" class="loadstatus hidden"></div>
        <div class="col-xs-12 lefttext">
            <h2 class="lefttext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501043") %>
            </h2>
        </div>
        <div class="col-xs-12">
            <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206103") %> : <a href="/import/downloadList.aspx" class="" style=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206104") %></a></h3>
        </div>

        <div class="col-xs-12">
            <div class="col-xs-12 pad0">
                <div class="col-xs-3 pad0">
                    <h3 class="lh5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501045") %></h3>
                </div>
                <div class="col-xs-3">
                    <asp:DropDownList ID="ddlyear" runat="server" class="form-control">
                        <asp:ListItem Text="2558" Value="2557" Selected="True" />
                        <asp:ListItem Text="2559" Value="2557" Selected="False" />
                        <asp:ListItem Text="2560" Value="2557" Selected="False" />
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="col-xs-12">

            <div class="col-xs-12 pad0">
                <div class="col-xs-3 pad0">
                    <h3 class="lh5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501046") %></h3>
                </div>
                <div class="col-xs-3">
                    <asp:DropDownList ID="semister" runat="server" class="form-control">
                        <asp:ListItem Text="1" Value="1" Selected="True" />
                        <asp:ListItem Text="2" Value="2" />
                    </asp:DropDownList>
                </div>
            </div>
        </div>


        <div class="col-xs-12">

            <div class="col-xs-12 pad0">
                <div class="col-xs-3 pad0">
                    <h3 class="lh5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02766") %></h3>
                </div>
                <div class="col-xs-3">
                    <select id="sltLevel" name="sltLevel[]"
                        class="form-control">
                        <asp:Literal ID="ltrLevel" runat="server" />
                    </select>
                </div>
            </div>
        </div>

        <div class="col-xs-12">

            <div class="col-xs-12 pad0">
                <div class="col-xs-3 pad0">
                    <h3 class="lh5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501047") %></h3>
                </div>
                <div class="col-xs-3">
                    <select id="sltClass" name="sltClass[]"
                        class="form-control">
                        <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103022") %></option>
                    </select>
                </div>
            </div>
        </div>

        <div class="col-xs-12 hidden">
            <asp:TextBox ID="Textbox4" runat="server" CssClass="ddlselect" Width="80%"></asp:TextBox>
            <asp:TextBox ID="Textbox8" runat="server" CssClass="" Width="80%"></asp:TextBox>
        </div>

        <div class="col-xs-12 uploadbutton">
            <div class="col-xs-12 pad0">
                <div class="col-xs-3 pad0">
                    <h3 class="lh5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101283") %></h3>
                </div>
                <div class="col-xs-3">
                    <input id="upload" type="file" name="files[]" />
                </div>
            </div>
        </div>

        <div class="col-xs-12">
            <div class="col-xs-12 pad0">
                <div class="col-xs-3 pad0">
                </div>
                <div class="col-xs-3">
                    <div class="btn btn-info" id="btnImport">Import Invoice</div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>
