<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Question_EQ.aspx.cs" MasterPageFile="~/mp.Master" Inherits="FingerprintPayment.Qusetion.Question_EQ" %>


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

        th {
            text-align: center;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>

    <div class="full-card box-content">
        <div class="QusetionEQ">
            <div class="row">

                <div class="col-lg-12 center">
                    <label class="col-md-12"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133017") %></label>
                </div>

                <table id="tableData1" class="table table-bordered table-hover" style="width: 100%;">
                    <thead>
                        <tr>
                            <th style="width: 5%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203033") %></th>
                            <th style="width: 30%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133016") %></th>
                            <th style="width: 8%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132975") %></th>
                            <th style="width: 8%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132976") %></th>
                            <th style="width: 8%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132977") %></th>
                            <th style="width: 8%;">จริงมาก</th>
                        </tr>
                    </thead>

                    <tbody>
                        <tr>
                            <td class="center">1.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133018") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question1" value="1" smallgroup="1" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question1" value="2" smallgroup="1" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question1" value="3" smallgroup="1" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question1" value="4" smallgroup="1" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">2.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133019") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question2" value="4" smallgroup="1" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question2" value="3" smallgroup="1" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question2" value="2" smallgroup="1" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question2" value="1" smallgroup="1" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">3.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133020") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question3" value="4" smallgroup="1" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question3" value="3" smallgroup="1" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question3" value="2" smallgroup="1" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question3" value="1" smallgroup="1" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">4.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133021") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question4" value="1" smallgroup="1" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question4" value="2" smallgroup="1" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question4" value="3" smallgroup="1" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question4" value="4" smallgroup="1" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">5.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133022") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question5" value="4" smallgroup="1" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question5" value="3" smallgroup="1" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question5" value="2" smallgroup="1" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question5" value="1" smallgroup="1" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">6.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133023") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question6" value="1" smallgroup="1" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question6" value="2" smallgroup="1" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question6" value="3" smallgroup="1" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question6" value="4" smallgroup="1" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">7.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133024") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question7" value="1" smallgroup="2" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question7" value="2" smallgroup="2" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question7" value="3" smallgroup="2" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question7" value="4" smallgroup="2" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">8.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133025") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question8" value="4" smallgroup="2" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question8" value="3" smallgroup="2" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question8" value="2" smallgroup="2" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question8" value="1" smallgroup="2" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">9.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133026") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question9" value="4" smallgroup="2" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question9" value="3" smallgroup="2" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question9" value="2" smallgroup="2" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question9" value="1" smallgroup="2" largegroup="" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">10.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133027") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question10" value="1" smallgroup="2" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question10" value="2" smallgroup="2" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question10" value="3" smallgroup="2" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question10" value="4" smallgroup="2" largegroup="" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">11.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133028") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question11" value="4" smallgroup="2" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question11" value="3" smallgroup="2" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question11" value="2" smallgroup="2" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question11" value="1" smallgroup="2" largegroup="" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">12.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133029") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question12" value="1" smallgroup="2" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question12" value="2" smallgroup="2" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question12" value="3" smallgroup="2" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question12" value="4" smallgroup="2" largegroup="" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">13.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133030") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question13" value="4" smallgroup="3" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question13" value="3" smallgroup="3" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question13" value="2" smallgroup="3" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question13" value="1" smallgroup="3" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">14.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133031") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question14" value="1" smallgroup="3" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question14" value="2" smallgroup="3" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question14" value="3" smallgroup="3" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question14" value="4" smallgroup="3" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">15.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133032") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question15" value="1" smallgroup="3" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question15" value="2" smallgroup="3" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question15" value="3" smallgroup="3" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question15" value="4" smallgroup="3" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">16.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133033") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question16" value="4" smallgroup="3" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question16" value="3" smallgroup="3" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question16" value="2" smallgroup="3" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question16" value="1" smallgroup="3" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">17.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133034") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question17" value="1" smallgroup="3" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question17" value="2" smallgroup="3" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question17" value="3" smallgroup="3" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question17" value="4" smallgroup="3" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">18.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133035") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question18" value="4" smallgroup="3" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question18" value="3" smallgroup="3" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question18" value="2" smallgroup="3" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question18" value="1" smallgroup="3" largegroup="1" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">19.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133036") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question19" value="4" smallgroup="4" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question19" value="3" smallgroup="4" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question19" value="2" smallgroup="4" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question19" value="1" smallgroup="4" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">20.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133037") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question20" value="1" smallgroup="4" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question20" value="2" smallgroup="4" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question20" value="3" smallgroup="4" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question20" value="4" smallgroup="4" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">21.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133038") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question21" value="4" smallgroup="4" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question21" value="3" smallgroup="4" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question21" value="2" smallgroup="4" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question21" value="1" smallgroup="4" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">22.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133039") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question22" value="1" smallgroup="4" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question22" value="2" smallgroup="4" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question22" value="3" smallgroup="4" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question22" value="4" smallgroup="4" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">23.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133040") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question23" value="1" smallgroup="4" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question23" value="2" smallgroup="4" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question23" value="3" smallgroup="4" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question23" value="4" smallgroup="4" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">24.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133041") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question24" value="4" smallgroup="4" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question24" value="3" smallgroup="4" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question24" value="2" smallgroup="4" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question24" value="1" smallgroup="4" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">25.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133042") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question25" value="1" smallgroup="5" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question25" value="2" smallgroup="5" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question25" value="3" smallgroup="5" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question25" value="4" smallgroup="5" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">26.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133043") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question26" value="4" smallgroup="5" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question26" value="3" smallgroup="5" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question26" value="2" smallgroup="5" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question26" value="1" smallgroup="5" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">27.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133044") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question27" value="4" smallgroup="5" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question27" value="3" smallgroup="5" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question27" value="2" smallgroup="5" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question27" value="1" smallgroup="5" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">28.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133045") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question28" value="1" smallgroup="5" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question28" value="2" smallgroup="5" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question28" value="3" smallgroup="5" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question28" value="4" smallgroup="5" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">29.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133046") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question29" value="4" smallgroup="5" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question29" value="3" smallgroup="5" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question29" value="2" smallgroup="5" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question29" value="1" smallgroup="5" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">30.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133047") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question30" value="4" smallgroup="5" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question30" value="3" smallgroup="5" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question30" value="2" smallgroup="5" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question30" value="1" smallgroup="5" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">31.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133048") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question31" value="1" smallgroup="6" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question31" value="2" smallgroup="6" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question31" value="3" smallgroup="6" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question31" value="4" smallgroup="6" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">32.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133049") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question32" value="1" smallgroup="6" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question32" value="2" smallgroup="6" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question32" value="3" smallgroup="6" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question32" value="4" smallgroup="6" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">33.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133050") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question33" value="4" smallgroup="6" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question33" value="3" smallgroup="6" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question33" value="2" smallgroup="6" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question33" value="1" smallgroup="6" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">34.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133051") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question34" value="1" smallgroup="6" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question34" value="2" smallgroup="6" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question34" value="3" smallgroup="6" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question34" value="4" smallgroup="6" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">35.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133052") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question35" value="4" smallgroup="6" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question35" value="3" smallgroup="6" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question35" value="2" smallgroup="6" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question35" value="1" smallgroup="6" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">36.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133053") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question36" value="1" smallgroup="6" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question36" value="2" smallgroup="6" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question36" value="3" smallgroup="6" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question36" value="4" smallgroup="6" largegroup="2" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">37.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133054") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question37" value="4" smallgroup="7" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question37" value="3" smallgroup="7" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question37" value="2" smallgroup="7" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question37" value="1" smallgroup="7" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">38.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133055") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question38" value="1" smallgroup="7" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question38" value="2" smallgroup="7" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question38" value="3" smallgroup="7" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question38" value="4" smallgroup="7" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">39.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133056") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question39" value="1" smallgroup="7" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question39" value="2" smallgroup="7" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question39" value="3" smallgroup="7" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question39" value="4" smallgroup="7" largegroup="" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">40.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133057") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question40" value="4" smallgroup="7" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question40" value="3" smallgroup="7" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question40" value="2" smallgroup="7" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question40" value="1" smallgroup="7" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">41.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133058") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question41" value="1" smallgroup="8" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question41" value="2" smallgroup="8" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question41" value="3" smallgroup="8" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question41" value="4" smallgroup="8" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">42.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133059") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question42" value="1" smallgroup="8" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question42" value="2" smallgroup="8" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question42" value="3" smallgroup="8" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question42" value="4" smallgroup="8" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">43.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133060") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question43" value="1" smallgroup="8" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question43" value="2" smallgroup="8" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question43" value="3" smallgroup="8" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question43" value="4" smallgroup="8" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">44.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133061") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question44" value="1" smallgroup="8" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question44" value="2" smallgroup="8" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question44" value="3" smallgroup="8" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question44" value="4" smallgroup="8" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">45.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133062") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question45" value="4" smallgroup="8" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question45" value="3" smallgroup="8" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question45" value="2" smallgroup="8" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question45" value="1" smallgroup="8" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">46.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133063") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question46" value="1" smallgroup="8" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question46" value="2" smallgroup="8" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question46" value="3" smallgroup="8" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question46" value="4" smallgroup="8" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">47.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133064") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question47" value="4" smallgroup="9" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question47" value="3" smallgroup="9" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question47" value="2" smallgroup="9" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question47" value="1" smallgroup="9" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">48.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133065") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question48" value="1" smallgroup="9" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question48" value="2" smallgroup="9" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question48" value="3" smallgroup="9" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question48" value="4" smallgroup="9" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">49.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133066") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question49" value="1" smallgroup="9" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question49" value="2" smallgroup="9" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question49" value="3" smallgroup="9" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question49" value="4" smallgroup="9" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">50.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133067") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question50" value="1" smallgroup="9" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question50" value="2" smallgroup="9" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question50" value="3" smallgroup="9" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question50" value="4" smallgroup="9" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">51.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133068") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question51" value="4" smallgroup="9" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question51" value="3" smallgroup="9" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question51" value="2" smallgroup="9" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question51" value="1" smallgroup="9" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="center">52.</td>
                            <td style="text-align: left;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133033") %></td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question52" value="4" smallgroup="9" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question52" value="3" smallgroup="9" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question52" value="2" smallgroup="9" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                            <td class="center">
                                <div class="form-check form-check-inline">
                                    <label>
                                        <input type="radio" name="Question52" value="1" smallgroup="9" largegroup="3" />
                                        <span class="label-text"></span>
                                    </label>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>

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

            $(".QusetionEQ #saveQuestion").bind({
                click: function () {
                    var data = GetQusetionDataFromInput();
                    QusetionEQ.SaveItem(data);
                    return false;
                }
            });

            $(".QusetionEQ #cancelQuestion").bind({
                click: function () {
                    QusetionEQ.Back();
                    return false;
                }
            });

        });


        var QusetionEQ = {
            SaveItem: function (data) {
                if (data != undefined) {
                    $("#modalWaitDialog").modal('show');
                    PageMethods.SaveItem('<%=Request.QueryString["sid"]%>', data, function (value) {
                        console.log(data);
                        console.log(value);
                        QusetionEQ.OnSuccessSave('<%=Request.QueryString["sid"]%>');
                    });
                }
            },
            OnSuccessSave: function (sid) {
                $("#modalWaitDialog").modal('hide');
                window.location.replace("Summary_EQ.aspx?sid=" + sid + "&term=" + '<%=Request.QueryString["term"]%>');
            },
            Back: function () {
                window.location.replace("Index_EQ.aspx");
            }
        }

        function GetQusetionDataFromInput() {

            var valid = true;
            var count;

            for (var i = 1; i < 52; i++) {
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

            var tableRows = document.getElementById("tableData1").rows.length;
            var tableLength = (tableRows - 1);

            if (valid != false) {
                var data = [];
                for (var i = 1; i <= tableLength; i++) {

                    var QuestionScore = typeof $('input[name=Question' + i + ']:checked').val() === 'undefined' ? "0" : $('input[name=Question' + i + ']:checked').val();
                    var QuestionDes = $("input[name=Question" + i + "]").attr("name");
                    var SmallGroup = $("input[name=Question" + i + "]").attr("smallgroup");
                    var LargeGroup = $("input[name=Question" + i + "]").attr("largegroup");

                    data.push({
                        QuestionScore: QuestionScore,
                        QuestionDes: QuestionDes,
                        QuestionSmallGroup: SmallGroup,
                        QuestionLargeGroup: LargeGroup
                    });
                }
                return data;
            }


        }


    </script>

</asp:Content>
