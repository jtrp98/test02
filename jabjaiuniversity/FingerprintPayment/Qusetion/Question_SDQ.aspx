<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Question_SDQ.aspx.cs" MasterPageFile="~/mp.Master" Inherits="FingerprintPayment.Qusetion.Question_SDQ" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- DataTables -->
    <link rel="stylesheet" href="/assets/plugins/datatables/dataTables.bootstrap.css" />


    <link rel="stylesheet" href="/styles/style-std.css" />


    <style type="text/css">
        input[type="radio"]:checked + .label-text:before {
            content: "\f192";
            color: #FFB600;
            animation: effect 250ms ease-in;
        }

        h3 {
            margin-top: 7px;
            margin-bottom: 9px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>


    <div class="full-card box-content">
        <div class="QusetionSDQ">

            <div class="row">
                <div class="col-lg-12 center">
                    <label class="col-md-12"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133072") %></label>
                </div>

                <table id="tableData1" class="mm table table-bordered table-hover" style="width: 100%;">
                    <thead>
                        <tr>
                            <th style="width: 10%" class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203033") %></th>
                            <th style="width: 30%" class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133070") %></th>
                            <th style="width: 10%" class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132975") %></th>
                            <th style="width: 10%" class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132977") %></th>
                            <th style="width: 10%" class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133071") %></th>
                        </tr>
                    </thead>

                    <tbody>
                        <tr>
                            <td class="center">1.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133073") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question1" value="0" group="5" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question1" value="1" group="5" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question1" value="2" group="5" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">2.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133074") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question2" value="0" group="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question2" value="1" group="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question2" value="2" group="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">3.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133075") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question3" value="0" group="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question3" value="1" group="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question3" value="2" group="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">4.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133076") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question4" value="0" group="5" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question4" value="1" group="5" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question4" value="2" group="5" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">5.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133077") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question5" value="0" group="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question5" value="1" group="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question5" value="2" group="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">6.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133078") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question6" value="0" group="4" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question6" value="1" group="4" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question6" value="2" group="4" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">7.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133079") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question7" value="2" group="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question7" value="1" group="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question7" value="0" group="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">8.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133080") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question8" value="0" group="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question8" value="1" group="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question8" value="2" group="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">9.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133081") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question9" value="0" group="5" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question9" value="1" group="5" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question9" value="2" group="5" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">10.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133082") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question10" value="0" group="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question10" value="1" group="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question10" value="2" group="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">11.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133083") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question11" value="2" group="4" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question11" value="1" group="4" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question11" value="0" group="4" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">12.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133084") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question12" value="0" group="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question12" value="1" group="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question12" value="2" group="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">13.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133085") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question13" value="0" group="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question13" value="1" group="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question13" value="2" group="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">14.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133086") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question14" value="2" group="4" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question14" value="1" group="4" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question14" value="0" group="4" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">15.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133087") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question15" value="0" group="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question15" value="1" group="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question15" value="2" group="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">16.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133088") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question16" value="0" group="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question16" value="1" group="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question16" value="2" group="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">17.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133089") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question17" value="0" group="5" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question17" value="1" group="5" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline7">
                                    <label>
                                        <input type="radio" name="Question17" value="2" group="5" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">18.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133090") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question18" value="0" group="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question18" value="1" group="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question18" value="2" group="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">19.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133091") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question19" value="0" group="4" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question19" value="1" group="4" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question19" value="2" group="4" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">20.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133092") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question20" value="0" group="5" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question20" value="1" group="5" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question20" value="2" group="5" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">21.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133093") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question21" value="2" group="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question21" value="1" group="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question21" value="0" group="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">22.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133094") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question22" value="0" group="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question22" value="1" group="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question22" value="2" group="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">23.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133095") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question23" value="0" group="4" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question23" value="1" group="4" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question23" value="2" group="4" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">24.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133096") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question24" value="0" group="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question24" value="1" group="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question24" value="2" group="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">25.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133097") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question25" value="2" group="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question25" value="1" group="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question25" value="0" group="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>


                <div class="col-lg-12" style="padding: 0px; border: 2px solid #FFB600; text-align: left;">

                    <div class="panel-heading" style="border-bottom: solid 2px #FFB600; background-color: #FAF69F;">
                        <label style="margin: 0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133098") %></label>
                    </div>

                    <div class="panel-body">

                        <div class="panel-default" style="margin-bottom: 13px;">
                            <div class="panel-body" style="border: solid 1px;">
                                <h3 style="font-weight: bolder;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133099") %></h3>

                                <div class="col-sm-3 form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="t1" value="0" />
                                        <span class="label-text" style="font-weight: normal;" onclick="fnc_Hide()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01355") %></span>
                                    </label>
                                </div>
                                <div class="col-sm-3 form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="t1" value="0" />
                                        <span class="label-text" style="font-weight: normal;" onclick="fnc_Show()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133100") %></span>
                                    </label>
                                </div>
                                <div class="col-sm-3 form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="t1" value="0" />
                                        <span class="label-text" style="font-weight: normal;" onclick="fnc_Show()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133101") %></span>
                                    </label>
                                </div>
                                <div class="col-sm-3 form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="t1" value="0" />
                                        <span class="label-text" style="font-weight: normal;" onclick="fnc_Show()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133102") %></span>
                                    </label>
                                </div>
                            </div>
                        </div>

                        <div class="panel-default" style="margin-bottom: 13px;" id="p1">
                            <div class="panel-body" style="border: solid 1px;">
                                <h3 style="font-weight: bolder;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133103") %></h3>
                                <div class="col-sm-3 form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="t2" value="0" />
                                        <span class="label-text" style="font-weight: normal;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133104") %></span>
                                    </label>
                                </div>
                                <div class="col-sm-3 form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="t2" value="0" />
                                        <span class="label-text" style="font-weight: normal;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133105") %></span>
                                    </label>
                                </div>
                                <div class="col-sm-3 form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="t2" value="0" />
                                        <span class="label-text" style="font-weight: normal;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133106") %></span>
                                    </label>
                                </div>
                                <div class="col-sm-3 form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="t2" value="0" />
                                        <span class="label-text" style="font-weight: normal;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133107") %></span>
                                    </label>
                                </div>
                            </div>
                        </div>

                        <div class="panel-default" style="margin-bottom: 13px;" id="p2">
                            <div class="panel-body" style="border: solid 1px;">
                                <h3 style="font-weight: bolder;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133108") %></h3>
                                <div class="col-sm-3 form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="t3" value="0" />
                                        <span class="label-text" style="font-weight: normal;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133109") %></span>
                                    </label>
                                </div>
                                <div class="col-sm-3 form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="t3" value="0" />
                                        <span class="label-text" style="font-weight: normal;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133110") %></span>
                                    </label>
                                </div>
                                <div class="col-sm-3 form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="t3" value="0" />
                                        <span class="label-text" style="font-weight: normal;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133111") %></span>
                                    </label>
                                </div>
                                <div class="col-sm-3 form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="t3" value="0" />
                                        <span class="label-text" style="font-weight: normal;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305104") %></span>
                                    </label>
                                </div>
                            </div>
                        </div>

                        <div class="panel panel-default" id="p3">
                            <div class="panel-heading">
                                <label style="font-size: x-large;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133112") %></label>
                            </div>

                            <div class="panel-body">
                                <table id="tableData2" class="mm table table-bordered table-hover" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203033") %></th>
                                            <th class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133120") %></th>
                                            <th class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133109") %></th>
                                            <th class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133110") %></th>
                                            <th class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133111") %></th>
                                            <th class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305104") %></th>
                                        </tr>
                                    </thead>

                                    <tbody>
                                        <tr>
                                            <td class="center">1.</td>
                                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133113") %></td>
                                            <td class="center">
                                                <div class="form-check form-check-inline">
                                                    <label>
                                                        <input type="radio" name="Question26" value="0" group="6" /><span class="label-text"></span></label>
                                                </div>
                                            </td>
                                            <td class="center">
                                                <div class="form-check form-check-inline">
                                                    <label>
                                                        <input type="radio" name="Question26" value="0" group="6" /><span class="label-text"></span></label>
                                                </div>
                                            </td>
                                            <td class="center">
                                                <div class="form-check form-check-inline">
                                                    <label>
                                                        <input type="radio" name="Question26" value="1" group="6" /><span class="label-text"></span></label>
                                                </div>
                                            </td>
                                            <td class="center">
                                                <div class="form-check form-check-inline">
                                                    <label>
                                                        <input type="radio" name="Question26" value="2" group="6" /><span class="label-text"></span></label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center">2.</td>
                                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133114") %></td>
                                            <td class="center">
                                                <div class="form-check form-check-inline">
                                                    <label>
                                                        <input type="radio" name="Question27" value="0" group="6" /><span class="label-text"></span></label>
                                                </div>
                                            </td>
                                            <td class="center">
                                                <div class="form-check form-check-inline">
                                                    <label>
                                                        <input type="radio" name="Question27" value="0" group="6" /><span class="label-text"></span></label>
                                                </div>
                                            </td>
                                            <td class="center">
                                                <div class="form-check form-check-inline">
                                                    <label>
                                                        <input type="radio" name="Question27" value="1" group="6" /><span class="label-text"></span></label>
                                                </div>
                                            </td>
                                            <td class="center">
                                                <div class="form-check form-check-inline">
                                                    <label>
                                                        <input type="radio" name="Question27" value="2" group="6" /><span class="label-text"></span></label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center">3.</td>
                                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133115") %></td>
                                            <td class="center">
                                                <div class="form-check form-check-inline">
                                                    <label>
                                                        <input type="radio" name="Question28" value="0" group="6" /><span class="label-text"></span></label>
                                                </div>
                                            </td>
                                            <td class="center">
                                                <div class="form-check form-check-inline">
                                                    <label>
                                                        <input type="radio" name="Question28" value="0" group="6" /><span class="label-text"></span></label>
                                                </div>
                                            </td>
                                            <td class="center">
                                                <div class="form-check form-check-inline">
                                                    <label>
                                                        <input type="radio" name="Question28" value="1" group="6" /><span class="label-text"></span></label>
                                                </div>
                                            </td>
                                            <td class="center">
                                                <div class="form-check form-check-inline">
                                                    <label>
                                                        <input type="radio" name="Question28" value="2" group="6" /><span class="label-text"></span></label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="center">4.</td>
                                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133116") %></td>
                                            <td class="center">
                                                <div class="form-check form-check-inline">
                                                    <label>
                                                        <input type="radio" name="Question29" value="0" group="6" /><span class="label-text"></span></label>
                                                </div>
                                            </td>
                                            <td class="center">
                                                <div class="form-check form-check-inline">
                                                    <label>
                                                        <input type="radio" name="Question29" value="0" group="6" /><span class="label-text"></span></label>
                                                </div>
                                            </td>
                                            <td class="center">
                                                <div class="form-check form-check-inline">
                                                    <label>
                                                        <input type="radio" name="Question29" value="1" group="6" /><span class="label-text"></span></label>
                                                </div>
                                            </td>
                                            <td class="center">
                                                <div class="form-check form-check-inline">
                                                    <label>
                                                        <input type="radio" name="Question29" value="2" group="6" /><span class="label-text"></span></label>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>

                        <div class="panel-default" style="margin-bottom: 13px;" id="p4">
                            <div class="panel-body" style="border: solid 1px;">
                                <h3 style="font-weight: bolder;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133117") %></h3>
                                <div class="col-sm-3 form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="t4" value="0" />
                                        <span class="label-text" style="font-weight: normal;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133109") %></span>
                                    </label>
                                </div>
                                <div class="col-sm-3 form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="t4" value="0" />
                                        <span class="label-text" style="font-weight: normal;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133110") %></span>
                                    </label>
                                </div>
                                <div class="col-sm-3 form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="t4" value="0" />
                                        <span class="label-text" style="font-weight: normal;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133111") %></span>
                                    </label>
                                </div>
                                <div class="col-sm-3 form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="t4" value="0" />
                                        <span class="label-text" style="font-weight: normal;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305104") %></span>
                                    </label>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

                <div class="col-lg-12 center" style="padding: 15px;">
                    <button type="button" class="btn btn-success" id="saveQuestion" style="width: 11%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                    <button type="button" class="btn btn-danger" id="cancelQuestion" style="width: 11%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00070") %></button>
                </div>

            </div>
        </div>
    </div>


    <!-- ModelError -->
    <div class="modal fade" id="ModelError">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                </div>
                <div class="modal-body center">
                    <h1 id="errorAlert">errorAlert</h1>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>


    <!-- ModalLoading -->
    <div id="modalWaitDialog" class="modal fade" tabindex="-1" role="dialog" data-keyboard="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00106") %></h3>
                </div>
                <div class="modal-body">
                    <div style="text-align: center;">
                        <img src="../Assets/images/wait.gif" style="width: 75px; height: 75px;" />
                    </div>
                </div>
                <div class="modal-footer" style="text-align: center;">
                </div>
            </div>
        </div>
    </div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">

    <script type="text/javascript" src="../PreRegister/assets/js/plugins/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../PreRegister/assets/js/plugins/additional-methods.min.js"></script>

    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.2/Chart.min.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {

            $(".QusetionSDQ #saveQuestion").bind({
                click: function () {
                    var data = GetQusetionDataFromInput();
                    QusetionSDQ.SaveItem(data);
                    return false;
                }
            });

            $(".QusetionSDQ #cancelQuestion").bind({
                click: function () {
                    QusetionSDQ.Back();
                    return false;
                }
            });

        });


        var QusetionSDQ = {
            SaveItem: function (data) {
                console.log(data);
                if (data != undefined) {
                    $("#modalWaitDialog").modal('show');
                    PageMethods.SaveItem('<%=Request.QueryString["sid"]%>', data, function (result) {
                        console.log(result);
                        QusetionSDQ.OnSuccessSave('<%=Request.QueryString["sid"]%>');
                    });
                }
            },
            OnSuccessSave: function (sid) {
                $("#modalWaitDialog").modal('hide');
                window.location.replace("Summary_SDQ.aspx?sid=" + sid + "&term=" + '<%=Request.QueryString["term"]%>');
            },
            Back: function () {
                window.location.replace("Index_SDQ.aspx");
            }
        }




        function GetQusetionDataFromInput() {

            var valid = true;
            var count = "";

            //Total Article 29
            //Article < 25 dont check 
            for (var i = 1; i <= 25; i++) {
                count = document.getElementsByName("Question" + i + "");

                for (var check = 0; check < count.length; check++) {
                    if (count[check].checked)
                        break;
                }
                if (check === count.length) {
                    $('#ModelError').modal('show');
                    document.getElementById("errorAlert").innerHTML = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133069") %> " + i + "";
                    valid = false;
                    break;
                }
            }

            //Article < 25 dont checked value default = 0
            if (valid != false) {
                var data = [];
                for (var i = 1; i <= 29; i++) {

                    var QuestionScore = typeof $('input[name=Question' + i + ']:checked').val() === 'undefined' ? "0" : $('input[name=Question' + i + ']:checked').val();
                    var QuestionDes = $("input[name=Question" + i + "]").attr("name");
                    var QuestionGroup = $("input[name=Question" + i + "]").attr("group");

                    data.push({
                        QuestionScore: QuestionScore,
                        QuestionDes: QuestionDes,
                        QuestionGroup: QuestionGroup
                    });

                    //$.each($("input[name=Question" + i + "]:checked"), function (index, value) {
                    //    data.push({
                    //        QuestionScore: $(this).val(),
                    //        QuestionDes: $(value).attr("name"),
                    //        QuestionGroup: $(value).attr("group")
                    //    })
                    //});

                }
                return data;
            }

        }


        function fnc_Hide() {
            document.getElementById("p1").style.display = "none";
            document.getElementById("p2").style.display = "none";
            document.getElementById("p3").style.display = "none";
            document.getElementById("p4").style.display = "none";
        }

        function fnc_Show() {
            document.getElementById("p1").style.display = "";
            document.getElementById("p2").style.display = "";
            document.getElementById("p3").style.display = "";
            document.getElementById("p4").style.display = "";
        }






    </script>



</asp:Content>
