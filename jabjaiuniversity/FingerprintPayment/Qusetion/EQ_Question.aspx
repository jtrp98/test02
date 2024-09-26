<%@ Page Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="EQ_Question.aspx.cs" Inherits="FingerprintPayment.Qusetion.EqQuestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1" />
    <link href="//netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.min.css" rel="stylesheet" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
        label.btn span {
            font-size: 2.0em;
            font-weight: bold;
        }

        h1 {
            color: #000000;
        }

        h2 {
            color: #000000;
        }

        label input[type="radio"] ~ i.fa.fa-circle-o {
            color: #000000;
            display: inline;
        }

        label input[type="radio"] ~ i.fa.fa-dot-circle-o {
            display: none;
        }

        label input[type="radio"]:checked ~ i.fa.fa-circle-o {
            display: none;
        }

        label input[type="radio"]:checked ~ i.fa.fa-dot-circle-o {
            color: #ff1a1a;
            display: inline;
        }

        div[data-toggle="buttons"] label {
            display: inline-block;
            padding: 6px 12px;
            margin-bottom: 0;
            font-size: 14px;
            font-weight: normal;
            line-height: 2em;
            text-align: left;
            white-space: nowrap;
            vertical-align: top;
            cursor: pointer;
            background-color: none;
            border: 0px solid #808080;
            border-radius: 3px;
            color: #000000;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            -o-user-select: none;
            user-select: none;
        }

            div[data-toggle="buttons"] label:active, div[data-toggle="buttons"] label.active {
                -webkit-box-shadow: none;
                box-shadow: none;
            }
    </style>

    <div class="container-fluid full-card box-content">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>

        <div class="row">
            <h1 class="text-center"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132972") %> </strong></h1>

            <div class="form-group row">
                <h2 class="text-center col-sm-2 "><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %> : </strong></h2>
                <div class="col-sm-3">
                    <h3>
                        <input class="form-control text-center" runat="server" id="txt_Font_Name" readonly="True" /></h3>
                </div>
                <h2 class="text-center col-sm-1"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103187") %> : </strong></h2>
                <div class="col-sm-2">
                    <h3>
                        <input class="form-control text-center" runat="server" id="txt_Font_Class" readonly="True" />
                    </h3>
                </div>
                <h2 class="text-center col-sm-1 "><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %> : </strong></h2>
                <div class="col-sm-2">
                    <h3>
                        <input class="form-control text-center" runat="server" id="txt_Font_Number" readonly="True" />
                    </h3>
                </div>
            </div>

            <div class="form-group row">
                <h2 class="text-center col-sm-2 "><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104025") %></strong></h2>
                <div class="col-sm-3">
                    <h3>
                        <input class="form-control text-center" runat="server" id="txt_Bday" readonly="True" /></h3>
                </div>

                <h2 class="text-center col-sm-1"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101061") %></strong></h2>
                <asp:RadioButtonList ID="radioGender" CssClass="radioButtonList" runat="server" RepeatDirection="Horizontal" Enabled="False">
                    <asp:ListItem Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %>" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %>&nbsp;" Selected="True"><h2 class="text-center"><strong>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %>&nbsp;&nbsp;&nbsp;&nbsp;</strong></h2></asp:ListItem>
                    <asp:ListItem Value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %>"><h2><strong>&nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %>&nbsp;</strong></h2></asp:ListItem>
                </asp:RadioButtonList>
            </div>

            <h2 class="text-center">
                <strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133125") %> <i class="fa fa-dot-circle-o fa-1x"></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133126") %></strong>
            </h2>

        </div>


        <div class="row">
            <table class="table table-bordered table-hover">
                <thead>
                    <tr>
                        <th class="text-center">
                            <h2><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203033") %></strong></h2>
                        </th>
                        <th>
                            <h2><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132974") %></strong></h2>
                        </th>
                        <th class="text-center">
                            <h2><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132975") %></strong></h2>
                        </th>
                        <th class="text-center">
                            <h2><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132976") %></strong></h2>
                        </th>
                        <th class="text-center">
                            <h2><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132977") %></strong></h2>
                        </th>
                        <th class="text-center">
                            <h2><strong>จริงมาก</strong></h2>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (var data in eQquestions)
                        { %>
                    <tr>
                        <td class="text-center"><strong><%= data.TB_EQ_Question_Id %></strong></td>
                        <td class="text-left"><strong><%= data.TB_EQ_Question_Des %></strong></td>

                        <% foreach (var data2 in data.TB_EQ_Points)
                            { %>
                        <td class="text-center">
                            <div class="btn-group btn-group-vertical" data-toggle="buttons">
                                <label class="btn active">
                                    <input type="radio" tb_eq_question_id="<%= data.TB_EQ_Question_Id %>" name="check_<%= data.TB_EQ_Question_Id %>" id="check_<%= data.TB_EQ_Question_Id %>"
                                        value="<%= data2.TB_EQ_Point_Point %>" eqquestion-group="<%= data.TB_EQ_Question_Group %>" />
                                    <i class="fa fa-circle-o fa-2x"></i><i class="fa fa-dot-circle-o fa-2x"></i>
                                </label>
                            </div>
                        </td>
                        <% } %>
                    </tr>
                    <% } %>
                </tbody>
            </table>
        </div>

        
        <div class="full-card">
            <div>
                <div class="col-sm-3">

                </div>
                <div class="col-sm-6 text-center">
                    <a class="btn btn-success" id="sNext" href="EQindex.aspx"><h3><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %></strong></h3></a>
                    <a class="btn btn-danger" href="EQindex.aspx"><h3><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></strong></h3></a>
                </div>
                <div class="col-sm-3">

                </div>
            </div>
        </div>


<%--ตัวทดสอบ--%>
        <%--<div class="container-fluid full-card">

            <div class="row">
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %> EQ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405031") %></th>
                            <th class="text-center">เกณคะแนน</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>ควบคุมอารม</td>
                            <td>
                                <div id="ans1_1"></div>
                            </td>
                        </tr>
                        <tr>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132979") %></td>
                            <td>
                                <div id="ans1_2"></div>
                            </td>
                        </tr>
                        <tr>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306033") %></td>
                            <td>
                                <div id="ans1_3"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="text-center"><strong>องประกอบดี</strong></td>
                            <td class="text-center">
                                <div id="ansResult1"></div><br />
                                <div id="sumString1"></div>
                            </td>
                        </tr>
                        <tr>
                            <td>มีแรงจฅูงใจ</td>
                            <td>
                                <div id="ans2_1"></div>
                            </td>
                        </tr>
                        <tr>
                            <td>ตัดสินใจ</td>
                            <td>
                                <div id="ans2_2"></div>
                            </td>
                        </tr>
                        <tr>
                            <td>สัมพันกับผู้อื่น</td>
                            <td>
                                <div id="ans2_3"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="text-center"><strong>องประกอบดี</strong></td>
                            <td class="text-center">
                                <div id="ansResult2"></div><br />
                                <div id="sumString2"></div>
                            </td>
                        </tr>
                        <tr>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132979") %></td>
                            <td>
                                <div id="ans3_1"></div>
                            </td>
                        </tr>
                        <tr>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132981") %></td>
                            <td>
                                <div id="ans3_2"></div>
                            </td>
                        </tr>
                        <tr>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306041") %></td>
                            <td>
                                <div id="ans3_3"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="text-center"><strong>องประกอบความสุข</strong></td>
                            <td class="text-center">
                                <div id="ansResult3"></div><br />
                                <div id="sumString3"></div>
                            </td>
                        </tr>
                        <tr>
                            <td><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %> EQ <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303008") %></strong></td>
                            <td>
                                <div id="ansResultINT"></div>
                            </td>
                        </tr>
                        <tr>
                            <td><strong>เกณ EQ</strong></td>
                            <td>
                                <div id="ansResultSTRING"></div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>

        </div>

        <div id="data-ans-1"></div>
        <div id="data-ans-2"></div>
        <div class="btn btn-success" id="ntbSum">
            <h3><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132984") %></strong></h3>
        </div>--%>



    </div> <%--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %>--%>


    <script type="text/javascript">

        $(function () {
            $("#sNext").click(function () {
                var data = []
                $.each($("[id*=check_]:checked"), function (e, s) {
                    data.push({
                        TB_EQ_Question_Id: $(s).attr("TB_EQ_Question_Id"),
                        TB_EQ_Point_Point: $(s).val()
                    })
                    console.log($(s).attr("TB_EQ_Question_Id") + ":" + $(s).val());
                });
                PageMethods.upDateDataEQ(getUrlParameter("id"), data, function (e) {

                })
            });
        });


        $(function () {
            $("#ntbSum").click(function () {
                var SumResult1 = 0, SumResult2 = 0, SumResult3 = 0;
                for (var q1 = 1; q1 <= 9; q1++) {
                    var sumPoint = 0;

                    $("#data-ans-" + q1).html("");
                    $("#ans1_" + q1).html("");
                    $("#ans2_" + q1).html("");
                    $("#ans3_" + q1).html("");

                    $.each($("[id*=check_][eqquestion-group=" + q1 + "]:checked"), function (e, s) {
                        console.log($(s).attr("TB_EQ_Question_Id") + ":" + $(s).val());

                        $("#data-ans-" + q1).append("<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206452") %> " + $(s).attr("TB_EQ_Question_Id") + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204047") %> " + $(s).val() + " </p>");
                        sumPoint += parseInt($(s).val());
                    });
                    if (q1 >= 1 && q1 <= 3) {
                        $("#ans1_" + q1).html("<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %>" + sumPoint + "</p>");
                        SumResult1 = SumResult1 + sumPoint; 
                    } else if (q1 >= 4 && q1 <= 6) {
                        $("#ans2_" + (q1 - 3)).html("<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %>" + sumPoint + "</p>")
                        SumResult2 += sumPoint;
                    }
                    else if (q1 >= 7 && q1 <= 9) {
                        $("#ans3_" + (q1 - 6)).html("<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %>" + sumPoint + "</p>")
                        SumResult3 += sumPoint;
                    }
                }

                $("#ansResult1").html(SumResult1);
                $("#ansResult2").html(SumResult2);
                $("#ansResult3").html(SumResult3);

                var sumString1 = "";
                if (SumResult1 < 48) {
                    sumString1 = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132971") %>";
                } else if (SumResult1 <= 48 && SumResult1 >= 58) {
                    sumString1 = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
                } else {
                    sumString1 = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132985") %>";
                }

                var sumString2 = "";
                if (SumResult2 < 45) {
                    sumString2 = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132971") %>";
                } else if (SumResult2 <= 44 && SumResult2 >= 57) {
                    sumString2 = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
                } else {
                    sumString2 = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132985") %>";
                }

                var sumString3 = "";
                if (SumResult3 <= 40) {
                    sumString3 = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132971") %>";
                } else if (SumResult3 <= 40 && SumResult3 >= 55) {
                    sumString3 = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>";
                } else {
                    sumString3 = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132985") %>";
                }

                $("#sumString1").html(sumString1);
                $("#sumString2").html(sumString2);
                $("#sumString3").html(sumString3);








                var resultINT = 0;
                $("#ansResultINT").html(resultINT += SumResult1 + SumResult2 + SumResult3);

                var resultSTRING = "";
                if (resultINT < 140)
                {
                    resultSTRING = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132986") %>";
                }
                else if (resultINT >= 140 && resultINT <= 170)
                {
                    resultSTRING = "EQ อยู่ในเกณปกติ";
                }
                else
                {
                    resultSTRING = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132987") %>";
                }

                $("#ansResultSTRING").html(resultSTRING)
            })
        });

    </script>


</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
