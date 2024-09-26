<%@ Page Title="" Language="C#" MasterPageFile="~/mp_notFrom.Master" AutoEventWireup="true" CodeBehind="EditsGrade.aspx.cs" Inherits="FingerprintPayment.EditsGrade.EditsGrade" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .text-responsive {
            font-weight: normal;
            font-size: calc(40% + 1vw + 1vh);
        }

        .set-text {
            font-size: 24px;
        }

        .output {
            font-weight: normal;
        }

        .marginHandle {
            margin-top: 20px;
            margin-bottom: 20px;
        }

        .btn2 {
            width: 165px;
            margin-left: 10px;
            margin-right: 10px;
            margin-top: 10px;
            margin-bottom: 10px;
        }

        .btn-secondary {
            color: #fff;
            background-color: #6c757d;
        }

        #loading {
            display: block;
            position: fixed;
            top: 0;
            left: 0;
            z-index: 100;
            width: 100vw;
            height: 100vh;
            background-color: rgba(192, 192, 192, 0.5);
            background-image: url("https://i.imgur.com/CgViPo0.gif");
            background-repeat: no-repeat;
            background-position: center;
        }
    </style>


    <%--fontawesome icon--%>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.1/css/all.css" integrity="sha384-50oBUHEmvpQ+1lW4y57PTFmhCaXp0ML5d60M1M7uH2+nqUivzIebhndOJK28anvf" crossorigin="anonymous" />


      <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.css" />

    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.js"></script>

    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/buttons/1.5.2/js/dataTables.buttons.min.js"></script>

    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/select/1.2.6/js/dataTables.select.min.js"></script>

    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/responsive/2.2.3/css/responsive.dataTables.min.css" />

    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/responsive/2.2.3/js/dataTables.responsive.js"></script>

        <script type="text/javascript" charset="utf8" src="https://cdn.rawgit.com/ashl1/datatables-rowsgroup/fbd569b8768155c7a9a62568e66a64115887d7d0/dataTables.rowsGroup.js"></script>

    <%--fontawesome icon--%>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.1/css/all.css" integrity="sha384-50oBUHEmvpQ+1lW4y57PTFmhCaXp0ML5d60M1M7uH2+nqUivzIebhndOJK28anvf" crossorigin="anonymous" />

    <script src="JS_EditsGrade.js?d=<%= DateTime.Now.ToString("ddMMyyyyHHmmssss") %>"></script>

    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>
    <%--<script src="Script/updateEmpStatus.js?d=<%= DateTime.Now.ToString("ddMMyyyyHHmmss") %>" type="text/javascript"></script>--%>
    <script src="/bootstrap SB2/bower_components/flot/excanvas.min.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.pie.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.resize.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.time.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot.tooltip/js/jquery.flot.tooltip.min.js" type="text/javascript"></script>
    <link href="/Content/jquery-confirm.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-confirm.js" type="text/javascript"></script>

    <style>
        .AssessmentScoreBox {
            width: 81%;
            /*  margin: 3px;
            margin-left: 6px !important;*/
            border-radius: 1rem !important;
            border: 1px solid #dddddd;
            text-align: center;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<div id="loading"class="loading"></div>--%>
    <div class="row set-text">
        <div class="col-md-12 content-container" style="background: #ffffff; padding: 50px 50px 30px 50px">
            <div class="row" style="margin:10px 0">
                <div class="col-md-2 col-xs-4" style="margin-top:10px">
                    <h2 style="margin: 0 0; padding: 0 0;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></h2>
                </div>
                <div class="col-md-4 col-xs-6" style="margin-top:10px">
                    <input class="form-control" id="txtname" placeholder=" <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %>" autofocus />
                    <input class="form-control" id="txtId" placeholder=" id" style="display: none" />
                    <input class="form-control" id="txtCode" placeholder=" <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02940") %>" style="display: none" />
                </div>
                <div class="col-md-2 col-xs-4" style="margin-top:10px">
                    <h2 style="margin: 0 0; padding: 0 0;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></h2>
                </div>
                <div class="col-md-4 col-xs-6 " style="margin-top:10px">
                    <select class="form-control"  id="ddlYear" style="height: 37.78px;"> 
                         <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                    </select>
                </div>

            </div>
            <div class="row" style="margin:20px">
                <div class="col-xs-12 center">
                    <button class="btn btn-info"  style="width:150px" onclick="Search()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></button>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-12">
                    <table id="tb" class="cell-border text-nowrap " style="width: 100%;">
                        <thead style="background-color: #337AB7">
                            <tr style="color: #fff">
                                <th></th>
                                <th></th>
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

        </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">

    <div class="container text-responsive">
        <!-- Trigger the modal with a button -->
        

        <!-- Modal -->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" id="close">&times;</button>
                        <h2 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701008") %></h2>
                    </div>
                    <div class="modal-body">
                        <div class="row hidden">
                            <div class="col-xs-4" style="text-align:right">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206191") %>Year
                            </div>
                             <div class="col-xs-8">
                                 <label id="txtnumberYear"></label>
                             </div>
                        </div>
                        <div class="row hidden">
                            <div class="col-xs-4" style="text-align:right">
                                id
                            </div>
                             <div class="col-xs-8">
                                 <label id="txtGradeDetailId"></label>
                             </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-4" style="text-align:right">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203003") %>
                            </div>
                             <div class="col-xs-8">
                                 <label id="txtSubject" style="font-weight:lighter"></label>
                             </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5" style="text-align:right">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206097") %>
                            </div>
                             <div class="col-xs-7">
                                 <label id="txtGradeCal" style="font-weight:lighter"></label>
                             </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5" style="text-align:right">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206098") %>
                            </div>
                            <div class="col-xs-7">
                                <select class="form-control" id="gradeNew" style="height: 37.78px;">
                                    <option value="-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206100") %></option>
                                    <option value="0.00">0.00</option>
                                    <option value="1.00">1.00</option>
                                    <option value="1.50">1.50</option>
                                    <option value="2.00">2.00</option>
                                    <option value="2.50">2.50</option>
                                    <option value="3.00">3.00</option>
                                    <option value="3.50">3.50</option>
                                    <option value="4.00">4.00</option>
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5" style="text-align:right">                               
<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206099") %> </div>
                            <div class="col-xs-7">
                               <input type="text" oncopy="return false" class="AssessmentScoreBox" onpaste="return false" name="GetScore100" id="GetScore100" onfocus="(this.oldValue=this.value)"  onkeypress="return isNumberKey(event, this)" value="" />
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer " style="text-align: center">
                        <button type="button" class="btn btn-success"  onclick="EditsGrade()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                        <button type="button" class="btn btn-danger" data-dismiss="modal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </div>

            </div>
        </div>

    </div>

</asp:Content>
