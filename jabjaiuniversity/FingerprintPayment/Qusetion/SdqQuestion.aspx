<%@ Page Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="SdqQuestion.aspx.cs" Inherits="FingerprintPayment.Qusetion.SdqQuestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1" />
    <link href="//netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.min.css" rel="stylesheet" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>

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
        /*สี<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %>ุ่ม*/

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


    <%--content1--%>
    <div class="container-fluid full-card box-content" id="content1">
        
        <div class="row">
            <h1 class="text-center"><strong>ส่วนที่ 1</strong></h1>
            <h1 class="text-center"><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133124") %> </strong></h1>

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
            <br />
            <h2 class="text-center">
                <strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133125") %> <i class="fa fa-dot-circle-o fa-1x"></i><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133126") %></strong>
            </h2>
            <br />
        </div>

        <div class="box-content">
            <table class="table table-bordered table-striped table-hover">
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
                            <h2><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132977") %></strong></h2>
                        </th>
                        <th class="text-center">
                            <h2><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133071") %></strong></h2>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (var data in SDQ_DATA_QuestionA.Where(w => w.T_FSDQ_Question_Group <= 5))
                        { %>
                    <tr>
                        <td class="text-center"><strong><%= data.T_FSDQ_Question_Id %></strong></td>
                        <td class="text-left"><strong><%= data.T_FSDQ_Question_Des %></strong></td>

                        <% foreach (var data2 in data.T_FSDQ_Points)
                            { %>
                        <td class="text-center">
                            <div class="btn-group btn-group-vertical" data-toggle="buttons">
                                <label class="btn active">
                                    <input type="radio" t_fsdq_question_id="<%= data.T_FSDQ_Question_Id %>" name="check_<%= data.T_FSDQ_Question_Id %>" id="check_<%= data.T_FSDQ_Question_Id %>"
                                        value="<%= data2.T_FSDQ_Point_Point %>" fontquestion-group="<%= data.T_FSDQ_Question_Group %>" />
                                    <i class="fa fa-circle-o fa-2x"></i><i class="fa fa-dot-circle-o fa-2x"></i>
                                </label>
                            </div>
                        </td>
                        <%}%>
                    </tr>
                    <%}%>
                </tbody>
            </table>
        </div>

        <div class="row text-center">
            <a class="btn btn-success" onclick="hideContent1()">
                <h3><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %></strong></h3>
            </a>
        </div>


        <%--        <div class="row text-center">
            <div>
                <div class="col-sm-2">
                    <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304050") %></p>
                    <div id="data-ans-1"></div>
                </div>
                <div class="col-sm-2">
                    <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133127") %></p>
                    <div id="data-ans-2"></div>
                </div>
                <div class="col-sm-2">
                    <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133128") %></p>
                    <div id="data-ans-3"></div>
                </div>
                <div class="col-sm-2">
                    <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133129") %></p>
                    <div id="data-ans-4"></div>
                </div>
                <div class="col-sm-2">
                    <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133130") %></p>
                    <div id="data-ans-5"></div>
                </div>
                <div class="col-sm-2">
                    <div class="btn btn-success" id="ntbSum">
                        <h3><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132984") %></strong></h3>
                    </div>
                </div>
            </div>
        </div>--%>
    </div>
    <%--content1--%>

    

    <%--content2--%>
    <div class="container-fluid full-card box-content" id="content2" style="display:none">
        <div>
            <div>
                <div class="col-sm-4">
                </div>
                <div class="col-sm-5 text-center">
                    <h1><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133131") %> </strong></h1>
                </div>
                <div class="col-sm-3">
                </div>
            </div>
        </div>

        <div class="row">
            <div>
                <div class="col-sm-4">
                </div>
                <div class="col-sm-6 text-left">
                    <h1><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133132") %>
                        <br />
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133099") %> </strong></h1>
                </div>
            </div>
        </div>

        <div>
            <div class=" row">
                <div class="col-sm-4">
                </div>

                <div class="col-sm-2 text-left">
                    <div class="btn-group btn-group-vertical" data-toggle="buttons">
                        <label class="btn active" id="oneDotone" onclick="dissable();">
                            <input type="radio" name="promblem1" runat="server" value="1" /><i class="fa fa-circle-o fa-2x"></i><i class="fa fa-dot-circle-o fa-2x"></i>
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01355") %> </span>
                        </label>
                    </div>
                </div>

                <div class="col-sm-5 text-left">
                    <div class="btn-group btn-group-vertical" data-toggle="buttons">
                        <label class="btn active" id="oneDottow" onclick="enable();">
                            <input type="radio" name="promblem1" runat="server" value="0" /><i class="fa fa-circle-o fa-2x"></i><i class="fa fa-dot-circle-o fa-2x"></i>
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133100") %> </span>
                        </label>
                    </div>
                </div>
            </div>
            <div class=" row">
                <div class="col-sm-4">
                </div>
                <div class="col-sm-2 text-left">
                    <div class="btn-group btn-group-vertical" data-toggle="buttons">
                        <label class="btn active" id="oneDottree" onclick="enable();">
                            <input type="radio" name="promblem1" runat="server" value="0" /><i class="fa fa-circle-o fa-2x"></i><i class="fa fa-dot-circle-o fa-2x"></i>
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133101") %> </span>
                        </label>
                    </div>
                </div>
                <div class="col-sm-5 text-left">
                    <div class="btn-group btn-group-vertical" data-toggle="buttons">
                        <label class="btn active" id="oneDotfour" onclick="enable();">
                            <input type="radio" name="promblem1" runat="server" value="0" /><i class="fa fa-circle-o fa-2x"></i><i class="fa fa-dot-circle-o fa-2x"></i>
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133102") %> </span>
                        </label>
                    </div>
                </div>
            </div>
        </div>

        <div>
            <div class="row">
                <div class="col-sm-4">
                </div>
                <div class="col-sm-5 text-left">
                    <h3><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133133") %> "<u><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01355") %></u>" <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133134") %>
                        <br />
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133133") %> "<u><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201067") %></u>" <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133135") %> </strong></h3>
                </div>
                <div class="col-sm-3">
                </div>
            </div>
        </div>

        <div>
            <div class="row">
                <div class="col-sm-4">
                </div>
                <div class="col-sm-5 text-left">
                    <h2><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133136") %> </strong></h2>
                </div>
                <div class="col-sm-3">
                </div>
            </div>
        </div>

        <div>
            <div class=" row">
                <div class="col-sm-4">
                </div>
                <div class="col-sm-2 text-left">
                    <div class="btn-group btn-group-vertical" data-toggle="buttons">
                        <label class="btn" id="towDotone">
                            <input type="radio" name="promblem2" runat="server" />
                            <i class="fa fa-circle-o fa-2x"></i>
                            <i class="fa fa-dot-circle-o fa-2x"></i>
                            <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133104") %> </span>
                        </label>
                    </div>
                </div>
                <div class="col-sm-5 text-left">
                    <div class="btn-group btn-group-vertical" data-toggle="buttons">
                        <label class="btn" id="towDottow">
                            <input type="radio" name="promblem2" runat="server" /><i class="fa fa-circle-o fa-2x"></i><i class="fa fa-dot-circle-o fa-2x"></i><span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133105") %> </span>
                        </label>
                    </div>
                </div>
            </div>
            <div class=" row">
                <div class="col-sm-4">
                </div>
                <div class="col-sm-2 text-left">
                    <div class="btn-group btn-group-vertical" data-toggle="buttons">
                        <label class="btn" id="towDottree">
                            <input type="radio" name="promblem2" runat="server" /><i class="fa fa-circle-o fa-2x"></i><i class="fa fa-dot-circle-o fa-2x"></i><span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133106") %> </span>
                        </label>
                    </div>
                </div>
                <div class="col-sm-5 text-left">
                    <div class="btn-group btn-group-vertical" data-toggle="buttons">
                        <label class="btn" id="towDotfour">
                            <input type="radio" name="promblem2" runat="server" /><i class="fa fa-circle-o fa-2x"></i><i class="fa fa-dot-circle-o fa-2x"></i><span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133107") %> </span>
                        </label>
                    </div>
                </div>
            </div>
        </div>

        <div>
            <div class="row">
                <div class="col-sm-4">
                </div>
                <div class="col-sm-5 text-left">
                    <h2><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133137") %> </strong></h2>
                </div>
                <div class="col-sm-3">
                </div>
            </div>
        </div>

        <div>
            <div class=" row">
                <div class="col-sm-4">
                </div>
                <div class="col-sm-2 text-left">
                    <div class="btn-group btn-group-vertical" data-toggle="buttons">
                        <label class="btn active" id="treeDotone">
                            <input type="radio" name="promblem3" runat="server" /><i class="fa fa-circle-o fa-2x"></i><i class="fa fa-dot-circle-o fa-2x"></i><span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133109") %> </span>
                        </label>
                    </div>
                </div>
                <div class="col-sm-5 text-left">
                    <div class="btn-group btn-group-vertical" data-toggle="buttons">
                        <label class="btn active" id="treeDottow">
                            <input type="radio" name="promblem3" runat="server" /><i class="fa fa-circle-o fa-2x"></i><i class="fa fa-dot-circle-o fa-2x"></i><span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133110") %> </span>
                        </label>
                    </div>
                </div>
            </div>
            <div class=" row">
                <div class="col-sm-4">
                </div>
                <div class="col-sm-2 text-left">
                    <div class="btn-group btn-group-vertical" data-toggle="buttons">
                        <label class="btn active" id="treeDottree">
                            <input type="radio" name="promblem3" runat="server" /><i class="fa fa-circle-o fa-2x"></i><i class="fa fa-dot-circle-o fa-2x"></i><span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133111") %> </span>
                        </label>
                    </div>
                </div>
                <div class="col-sm-5 text-left">
                    <div class="btn-group btn-group-vertical" data-toggle="buttons">
                        <label class="btn active" id="treeDotfour">
                            <input type="radio" name="promblem3" runat="server" /><i class="fa fa-circle-o fa-2x"></i><i class="fa fa-dot-circle-o fa-2x"></i><span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305104") %> </span>
                        </label>
                    </div>
                </div>
            </div>
        </div>


        <div>
            <div class="full-card">
                <div class="col-sm-4 ">
                </div>
                <div class="col-sm-5 text-left">
                    <h2><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133138") %> </strong></h2>
                </div>
                <div class="col-sm-3">
                </div>
            </div>
        </div>

        <div class="container-fluid full-card">
            <div class="row">
                <div class="col-sm-4">
                </div>
                <div class="col-sm-5 text-center">
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th class="text-center">
                                    <h2><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %></strong></h2>
                                </th>
                                <th class="text-center">
                                    <h2><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01355") %></strong></h2>
                                </th>
                                <th class="text-center">
                                    <h2><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133110") %></strong></h2>
                                </th>
                                <th class="text-center">
                                    <h2><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133111") %></strong></h2>
                                </th>
                                <th class="text-center">
                                    <h2><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305104") %></strong></h2>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <% var GroupNumber_6 = SDQ_DATA_QuestionA.Where(w => w.T_FSDQ_Question_Group == 6);
                                foreach (var data in GroupNumber_6)
                                { %>
                            <tr>
                                <td class="text-center">
                                    <h2><strong><%= data.T_FSDQ_Question_Des %></strong></h2>
                                </td>

                                <% foreach (var data2 in data.T_FSDQ_Points)
                                    { %>
                                <td class="text-center">
                                    <div class="btn-group btn-group-vertical" data-toggle="buttons">
                                        <label class="btn-block btn active">
                                            <input type="radio" t_fsdq_question_id="<%= data.T_FSDQ_Question_Id %>" name="check_<%= data.T_FSDQ_Question_Id %>" id="check_<%=data.T_FSDQ_Question_Id %>" value="<%= data2.T_FSDQ_Point_Point %>" fontquestion-group="<%= data.T_FSDQ_Question_Group %>" />
                                            <i class="fa fa-circle-o fa-2x"></i><i class="fa fa-dot-circle-o fa-2x"></i>
                                        </label>
                                    </div>
                                </td>
                                <%} %>
                            </tr>
                            <% } %>
                        </tbody>
                    </table>
                </div>

                <div class="col-sm-3">
                </div>

            </div>
        </div>


        <div>
            <div class="row">
                <div class="col-sm-4">
                </div>
                <div class="col-sm-5 text-left">
                    <h2><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133139") %> </strong></h2>
                </div>
                <div class="col-sm-3">
                </div>
            </div>
        </div>

        <div>
            <div class=" row">
                <div class="col-sm-4">
                </div>
                <div class="col-sm-2 text-left">
                    <div class="btn-group btn-group-vertical" data-toggle="buttons">
                        <label class="btn active" id="fiveDotone">
                            <input type="radio" name="promblem4" runat="server" /><i class="fa fa-circle-o fa-2x"></i><i class="fa fa-dot-circle-o fa-2x"></i><span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133109") %> </span>
                        </label>
                    </div>
                </div>
                <div class="col-sm-5 text-left">
                    <div class="btn-group btn-group-vertical" data-toggle="buttons">
                        <label class="btn active" id="fiveDottow">
                            <input type="radio" name="promblem4" runat="server" /><i class="fa fa-circle-o fa-2x"></i><i class="fa fa-dot-circle-o fa-2x"></i><span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133110") %> </span>
                        </label>
                    </div>
                </div>
            </div>
            <div class=" row">
                <div class="col-sm-4">
                </div>
                <div class="col-sm-2 text-left">
                    <div class="btn-group btn-group-vertical" data-toggle="buttons">
                        <label class="btn active" id="fiveDottree">
                            <input type="radio" name="promblem4" runat="server" /><i class="fa fa-circle-o fa-2x"></i><i class="fa fa-dot-circle-o fa-2x"></i><span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133111") %> </span>
                        </label>
                    </div>
                </div>
                <div class="col-sm-5 text-left">
                    <div class="btn-group btn-group-vertical" data-toggle="buttons">
                        <label class="btn active" id="fiveDotfour">
                            <input type="radio" name="promblem4" runat="server" /><i class="fa fa-circle-o fa-2x"></i><i class="fa fa-dot-circle-o fa-2x"></i><span> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305104") %> </span>
                        </label>
                    </div>
                </div>
            </div>
        </div>

        <div class="row full-card">
            <div class="col-sm-4">
            </div>
            <div class="col-sm-4 text-center">
                <a class="btn btn-success" id="sNext" href="SDQIndex.aspx">
                    <h3><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></strong></h3>
                </a>
                <a class="btn btn-danger" id="Cancel" href="SDQIndex.aspx">
                    <h3><strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></strong></h3>
                </a>
            </div>
            <div class="col-sm-4">
            </div>
        </div>

    </div>
    <%--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133140") %>--%>

    <script>


        function hideContent1() {
            var content2 = document.getElementById("content2");
            var content1 = document.getElementById("content1");

            if (content2.style.display === "none")
            {
                content2.style.display = "block",
                    content1.style.display = "none";
                
            } else {
                content2.style.display = "none";
            }

        }



        $(function () {
            $("#sNext").click(function () {
                var data = []
                $.each($("[id*=check_]:checked"), function (e, s) {
                    data.push({ T_FSDQ_Question_Id: $(s).attr("T_FSDQ_Question_Id"), T_FSDQ_Point_Point: $(s).val() })
                    console.log($(s).attr("T_FSDQ_Question_Id") + ":" + $(s).val());
                });

                if ($("input[name*=promblem1]:checked").val() === "1") {
                    var MaxId =  <%= GroupNumber_6.Select(s => s.T_FSDQ_Question_Id).Max() %>;
                    var MinId =  <%= GroupNumber_6.Select(s => s.T_FSDQ_Question_Id).Min() %>;
                    for (i = MinId; MinId <= MaxId; MinId++) {
                        data.push({ T_FSDQ_Question_Id: MinId, T_FSDQ_Point_Point: "0" })
                    }
                }
                PageMethods.updateData(getUrlParameter("id"), data, function (e) {

                })


            });
        });



        $(function () {
            $("#ntbSum").click(function () {
                for (var i = 1; i <= 5; i++) {
                    var sumPiont = 0;
                    var show;
                    $("#data-ans-" + i).html("");
                    $.each($("[id*=check_][fontquestion-group=" + i + "]:checked"), function (e, s) {
                        console.log($(s).attr("T_FSDQ_Question_Id") + ":" + $(s).val());

                        $("#data-ans-" + i).append("<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206452") %> " + $(s).attr("T_FSDQ_Question_Id") + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204047") %> " + $(s).val() + " </p>");
                        sumPiont += parseInt($(s).val());
                    });
                    $("#data-ans-" + i).append("<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203056") %> " + sumPiont + "</p>")

                    var _ans = "";
                    switch (i) {
                        case 1:
                            if (sumPiont <= 5) _ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>"
                            else if (sumPiont = 6 && sumPiont < 7) _ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %>";
                            else _ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %>";
                            break;
                        case 2:
                            if (sumPiont <= 4) _ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>"
                            else if (sumPiont = 5 && sumPiont < 6) _ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %>";
                            else _ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %>";
                            break;
                        case 3:
                            if (sumPiont <= 5) _ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>"
                            else if (sumPiont = 6 && sumPiont < 7) _ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %>";
                            else _ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %>";
                            break;
                        case 4:
                            if (sumPiont <= 3) _ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103008") %>"
                            else if (sumPiont = 4 && sumPiont < 5) _ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %>";
                            else _ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %>";
                            break;
                        case 5:
                            if (sumPiont <= 2) _ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %>"
                            else if (sumPiont = 3 && sumPiont < 4) _ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304032") %>";
                            else _ans = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304033") %>";
                            break;
                    }
                    $("#data-ans-" + i).append("<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M304049") %> " + _ans + "</p>")
                }
            });
        });





        function dissable() {

            $("#towDotone").attr("disabled", true);
            $("#towDottow").attr("disabled", true);
            $("#towDottree").attr("disabled", true);
            $("#towDotfour").attr("disabled", true);
            $("#towDotone").removeAttr("active").removeAttr().find('[type=radio]').prop("checked", false);
            $("#towDottow").removeAttr("active").removeAttr().find('[type=radio]').prop("checked", false);
            $("#towDottree").removeAttr("active").removeAttr().find('[type=radio]').prop("checked", false);
            $("#towDotfour").removeAttr("active").removeAttr().find('[type=radio]').prop("checked", false);

            $("#treeDotone").attr("disabled", true);
            $("#treeDottow").attr("disabled", true);
            $("#treeDottree").attr("disabled", true);
            $("#treeDotfour").attr("disabled", true);
            $("#treeDotone").removeAttr("active").removeAttr().find('[type=radio]').prop("checked", false);
            $("#treeDottow").removeAttr("active").removeAttr().find('[type=radio]').prop("checked", false);
            $("#treeDottree").removeAttr("active").removeAttr().find('[type=radio]').prop("checked", false);
            $("#treeDotfour").removeAttr("active").removeAttr().find('[type=radio]').prop("checked", false);

            $(".btn-block").attr("disabled", true);
            $(".btn-block").removeAttr("active").removeAttr().find('[type=radio]').prop("checked", false);

            $("#fiveDotone").attr("disabled", true);
            $("#fiveDottow").attr("disabled", true);
            $("#fiveDottree").attr("disabled", true);
            $("#fiveDotfour").attr("disabled", true);
            $("#fiveDotone").removeAttr("active").removeAttr().find('[type=radio]').prop("checked", false);
            $("#fiveDottow").removeAttr("active").removeAttr().find('[type=radio]').prop("checked", false);
            $("#fiveDottree").removeAttr("active").removeAttr().find('[type=radio]').prop("checked", false);
            $("#fiveDottree").removeAttr("active").removeAttr().find('[type=radio]').prop("checked", false);

        }




        function enable() {
            $("#towDotone").attr("disabled", false);
            $("#towDottow").attr("disabled", false);
            $("#towDottree").attr("disabled", false);
            $("#towDotfour").attr("disabled", false);

            $("#treeDotone").attr("disabled", false);
            $("#treeDottow").attr("disabled", false);
            $("#treeDottree").attr("disabled", false);
            $("#treeDotfour").attr("disabled", false);

            $(".btn-block").attr("disabled", false);

            $("#fiveDotone").attr("disabled", false);
            $("#fiveDottow").attr("disabled", false);
            $("#fiveDottree").attr("disabled", false);
            $("#fiveDotfour").attr("disabled", false);

        }

    </script>


</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
