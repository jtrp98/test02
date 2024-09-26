<%@ Page Language="C#" MasterPageFile="~/mp_notFrom.Master" AutoEventWireup="true" CodeBehind="DiplomaMain.aspx.cs" Inherits="FingerprintPayment.Diploma.DiplomaMain" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .text-responsive {
            font-weight: normal;
            font-size: calc(40% + 1vw + 1vh);
        }

        .marginHandle {
            margin-top: 20px;
            margin-bottom: 20px;
        }

        th {
            font-size: 26px;
            color: white;
        }

        td {
            font-size: 26px;
        }

        .text-handle {
            /*font-family: "THSarabun";*/
            font-weight: lighter;
            font-size: 26px;
        }

        table.dataTable thead .sorting_asc_disabled:after,
        table.dataTable thead .sorting_desc_disabled:after {
            color: #eee;
        }

        table.dataTable thead .sorting:after {
            /*opacity: 0.2;
  content: "\e150";*/
            color: #337AB7;
            /* sort */
        }

        table.dataTable thead .sorting_asc:after {
            /*content: "\e155";*/
            color: #337AB7;
            /* sort-by-attributes */
        }

        table.dataTable thead .sorting_desc:after {
            /*content: "\e156";*/
            color: #337AB7;
            /* sort-by-attributes-alt */
        }


        .modal-title {
            font-size: 36px;
        }

        .modal-body {
            font-size: 26px;
        }
    </style>

    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.css" />

    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.js"></script>

    <%--<link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"/>--%>

    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/buttons/1.5.2/js/dataTables.buttons.min.js"></script>

    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/select/1.2.6/js/dataTables.select.min.js"></script>

    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/responsive/2.2.3/css/responsive.dataTables.min.css" />

    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/responsive/2.2.3/js/dataTables.responsive.js"></script>

    <%--fontawesome icon--%>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.1/css/all.css" integrity="sha384-50oBUHEmvpQ+1lW4y57PTFmhCaXp0ML5d60M1M7uH2+nqUivzIebhndOJK28anvf" crossorigin="anonymous" />

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12 content-container" style="background: #ffffff; padding: 30px 30px 10px 10px; border-radius: 5px 5px 0 0">

            <div class="row text-center  text-handle">
                <div class="col-md-2 col-xs-12">
                    <label class="text-handle"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                </div>
                <div class="col-md-3 col-xs-12">

                    <select class="form-control" id="ddlYear">
                        <%-- <option>1</option>--%>
                    </select>
                </div>
                <div class="col-md-1 col-xs-12">
                </div>
                <div class="col-md-2 col-xs-12">
                </div>
                <div class="col-md-3 col-xs-12">
                </div>
                <div class="col-md-1 col-xs-12">
                </div>
            </div>

            <div class="row text-center  text-handle">
                <div class="col-md-2 col-xs-12">
                    <label class="text-handle"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %></label>
                </div>
                <div class="col-md-3 col-xs-12">
                    <select class="form-control" id="ddlSubLV">
                        <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                    </select>
                </div>
                <div class="col-md-1 col-xs-12">
                </div>
                <div class="col-md-2 col-xs-12">
                    <label class="text-handle"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                </div>
                <div class="col-md-3 col-xs-12">
                    <select class="form-control" id="ddlClassroom">
                        <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                    </select>
                </div>
                <div class="col-md-1 col-xs-12">
                </div>
            </div>

            <div class="row text-center  text-handle">
                <div class="col-md-2 col-xs-12">
                    <label class="text-handle"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                </div>
                <div class="col-md-3 col-xs-12">
                    <input class="form-control" id="txtname" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206395") %>" />
                </div>
                <div class="col-md-3 col-xs-12 hidden">
                    <input class="form-control" id="txtId" />
                </div>
                <div class="col-md-3 col-xs-12 hidden">
                    <input class="form-control" id="txtCode" />
                </div>
                <div class="col-md-1 col-xs-12">
                </div>
                <div class="col-md-2 col-xs-12">
                </div>
                <div class="col-md-3 col-xs-12">
                </div>
                <div class="col-md-1 col-xs-12">
                </div>
            </div>
            <div class="row" style="margin-top: 20px; margin-bottom: 20px">
                <div class="col-md-12 text-center">
                    <button type="button" class="btn btn-info" style="width: 100px" onclick="ClickSearch()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></button>
                    <button id="btnDiplomaReport" type="button" class="btn btn-warning" style="float: right;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206396") %></button>
                </div>
            </div>
            <form target="_blank" runat="server" hidden="hideen">
                <asp:TextBox runat="server" ID="stdId"></asp:TextBox>
                <asp:TextBox runat="server" ID="classroomId"></asp:TextBox>
                <asp:Button runat="server" ID="btnPrint" OnClick="btnPrint_Click" Text="sendValue" />

            </form>

            <table id="stdDetail" class="table-striped text-nowrap " style="width: 100%;">
                <thead style="background-color: #337AB7">
                    <tr>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                    </tr>
                </thead>
                <tbody></tbody>
            </table>

        </div>
    </div>

    <div id="modalDiplomaCode" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
        aria-hidden="true" style="margin: 0 auto; top: 25%;">
        <div class="modal-dialog global-modal">
            <div class="modal-content" style="max-width: 900px;">

                <div class="modal-header center" style="padding: 0px; top: 25%;">
                    <h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206383") %></h1>
                </div>
                <div class="modal-body">
                    <div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="col-xs-4">
                                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %></label>
                                </div>
                                <div class="col-xs-8">
                                    <input id="iptCode" type="text" class="form-control" data-sid="" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <button type="button" id="btnSaveDiplomaCode" class="btn btn-success global-btn">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                    <button type="button" class="btn btn-danger global-btn" data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                </div>

            </div>
        </div>
    </div>
    <!-- /.modal -->

    <% Response.WriteFile("~/Employees/modal-emp.inc"); %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">

    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>

    <link href="/Content/jquery-confirm.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-confirm.js" type="text/javascript"></script>

    <script type="text/javascript" src="JSDiplomaMain.js?v=<%=DateTime.Now.Ticks%>"></script>
</asp:Content>
