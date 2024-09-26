<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="SummaryDetail.aspx.cs" Inherits="FingerprintPayment.VisitHousePage.Web.Report.SummaryDetail" %>

<%@ Register Src="~/UserControls/YTLCFilter.ascx" TagPrefix="uc1" TagName="YTLCFilter" %>
<%@ Register Src="~/UserControls/StudentAutocomplete.ascx" TagPrefix="uc1" TagName="StudentAutocomplete" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <link href="//cdn.jsdelivr.net/npm/sweetalert2@10.12.4/dist/sweetalert2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />

    <style>
        label.error {
            color: red;
        }

        .table-bordered,
        .table-bordered td,
        .table-bordered th {
            border: 1px solid #000 !important;
        }

        table.table-bordered {
            margin-bottom: 1rem;
        }

            table.table-bordered th {
                font-weight: bold;
            }

        p.font-weight-bold {
            font-weight: 600 !important;
        }

        span.page-number{
            display:none;
        }
    </style>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script type="text/javascript" language="javascript" src="//cdn.datatables.net/buttons/2.0.1/js/dataTables.buttons.min.js"></script>
    <%--    <script type="text/javascript" language="javascript" src="//cdn.datatables.net/buttons/1.6.4/js/buttons.flash.min.js"></script>--%>
    <script type="text/javascript" language="javascript" src="//cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script type="text/javascript" language="javascript" src="//cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script type="text/javascript" language="javascript" src="//cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <script type="text/javascript" language="javascript" src="//cdn.datatables.net/buttons/2.0.1/js/buttons.html5.min.js"></script>
    <%-- <script src="Js/buttons.html5.js"></script>--%>
    <script type="text/javascript" language="javascript" src="//cdn.datatables.net/buttons/2.0.1/js/buttons.print.min.js"></script>

    <script>

        function SearchData(t) {
            if (!$("#aspnetForm").valid()) {
                return;
            }

            if (t == 'data') {
                $("#result-wrapper").html('');

                $("body").mLoading();         
                $.ajax(
                    {
                        type: "POST",
                        url: "/VisitHousePage/Web/Report/SummaryDetail.aspx/LoadData",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({
                                'term': YTLCF.GetTermID(),
                                'level1': YTLCF.GetLevelID(),
                                'level2': YTLCF.GetClassID(),
                            }),
                        success: function (response) {
                            $("#result-wrapper").html(response.d.html);
                            if (YTLCF.GetLevelID() == '') {
                                $('.--all').show();
                            }
                            else {
                                $('.--all').hide();
                            }

                            $("body").mLoading("hide");
                        }
                    });

            }
            else if (t == 'report') {

                window.open('/VisitHousePage/Web/Report/SummaryDetailPrint.aspx?term=' + YTLCF.GetTermID() + '&level1=' + YTLCF.GetLevelID() + '&level2=' + YTLCF.GetClassID(), '_blank');

                //return;
                //var json = JSON.stringify({
                //    'term': YTF.GetTermID(),
                //    'termno': YTF.GetTermText(),
                //    'year': YTF.GetYearNo(),
                //});
                //var dt = new Date();
                //var file_name = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104068") %>_' + dt.format("ddMMyyyyHHmmssss") + '.xls';

                //xhr = new XMLHttpRequest();

                //xhr.open("POST", "/Report/NewStudent.aspx/ExportExcel1", true);
                //xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
                //xhr.responseType = 'blob';
                //xhr.onload = function () {
                //    //aa = xhr.getResponseHeader("filename");
                //    saveAs(xhr.response, file_name);
                //    //$("body").mLoading('hide');
                //};
                //xhr.send(json);
                ////$('.dataTables_wrapper .buttons-excel').click();
                ////var url = "Handles/DataEmpTraining_Handler.ashx?c=report&emp=" + $("#txtid").val() + "&type=" + $("#type").val() + "&dstart=" + dStart + "&dend=" + dEnd;
                ////window.open(url);
            }
        }

        $(function () {

            if (jQuery.validator) {//.messages

                jQuery.extend(jQuery.validator.messages, {
                    required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105028") %>",
                });

                $("#aspnetForm").validate({  // initialize the plugin

                    errorPlacement: function (error, element) {
                        let _class = element.attr('class');

                        if (_class.includes('--req-append-last')) {
                            error.insertAfter(element.parent());
                        }
                        else {
                            error.insertAfter(element);
                        }

                    }

                });
            }
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00887") %> > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305012") %>
            </p>
        </div>
    </div>

    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release"></asp:ScriptManager>

        <div class="row">
            <div class="col-md-12">

                <div class="card ">
                    <div class="card-header card-header-info card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">search</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></h4>
                    </div>
                    <div class="card-body ">

                        <uc1:YTLCFilter runat="server" ID="YTLCFilter"  />

                        <div class="row mt-2">
                            <div class="col-md-12 text-center">
                                <button type="button" class="btn btn-success" onclick="SearchData('data');">
                                    <span class="btn-label">
                                        <i class="material-icons">search</i>
                                    </span>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>
                                </button>
                                  <button class="btn btn-success pull-rightx" id="exportfile" onclick="SearchData('report')">
                                    <span class="btn-label">
                                        <span class="material-icons">receipt_long
                                        </span>
                                    </span>
                                    Export
                                </button>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="card ">
                    <div class="card-header card-header-primary card-header-icon">
                        <div class="card-icon">
                            <span class="material-icons">list_alt</span>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305012") %></h4>
                    </div>
                    <div class="card-body ">
                        <div id="result-wrapper" class="row ">
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </form>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">

</asp:Content>
