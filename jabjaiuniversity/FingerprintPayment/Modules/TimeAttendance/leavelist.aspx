<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="leavelist.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.leavelist" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .font-size-14 {
            font-size: 14px !important;
        }

        .completionList {
            border: solid 1px #444444;
            background-color: White;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
        }

        .modal {
            z-index: 1;
        }

        .modal-backdrop {
            z-index: 0;
        }

        .listItem {
            color: blue;
            background-color: White;
        }

        .itemHighlighted {
            background-color: #ffc0c0;
        }

        .dropdown-menu {
            z-index: 1100 !important;
        }

    </style>
    <script type="text/javascript" language="javascript">
        var availableValueplane = [];
        $(document).ready(function () {
            $('#ctl00_MainContent_ddlsublevel').val(getUrlParameter("idlv"));

            funtionListSubLV2("ctl00_MainContent_ddlsublevel", "ddlSubLV2", getUrlParameter("idlv2"));
            availableValueplane = functionListstudent("ctl00_MainContent_ddlsublevel", "ddlSubLV2");

            $('input[id*=txtSearch]').val(getUrlParameter("sname"));

            $('#ctl00_MainContent_ddlsublevel').change(function () {
                funtionListSubLV2("ctl00_MainContent_ddlsublevel", "ddlSubLV2");
                availableValueplane = functionListstudent("ctl00_MainContent_ddlsublevel", "ddlSubLV2");
            });

            $('select[id*=ddlSubLV2]').change(function () {
                availableValueplane = functionListstudent("ctl00_MainContent_ddlsublevel", "ddlSubLV2");
            });


            $('input[id*=btnSearch]').click(function () {
                var param1var = $('#ctl00_MainContent_ddlsublevel option:selected').val();
                var param2var = $('select[id*=ddlSubLV2] option:selected').val();
                var param3var = $('input[id*=txtSearch]').val();

                if ($("#type").val() == "E") {
                    window.location.href = "leavelist.aspx?sname=" + $('#ctl00_MainContent_txtsname').val()
                + "&type=E";
                }
                else {
                    window.location.href = "leavelist.aspx?idlv=" + param1var + "&idlv2="
                        + param2var + "&sname=" + param3var + "&type=U";
                }
            });

            $('input[id*=txtSearch]').autocomplete({
                width: 300,
                max: 10,
                delay: 100,
                minLength: 1,
                maxLength: 10,
                autoFocus: true,
                cacheLength: 1,
                scroll: true,
                highlight: false,
                source: function (request, response) {
                    var results = $.ui.autocomplete.filter(availableValueplane, request.term);
                    response(results.slice(0, 10));
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $("input[id*=txtSearch]").val(ui.item.label);
                    //setTimeout('__doPostBack(\'ctl00$MainContent$txtSearch\',\'\')', 0);
                    //                    $("#txtid").val(ui.item.value);
                },
                focus: function (event, ui) {
                    event.preventDefault();
                    //                    $("#txtid").val("");
                }
            });

            var availableValueEmployees = [];
            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=teacher",
                dataType: "json",
                success: function (objjson) {
                    $.each(objjson, function (index) {
                        var newObject = {
                            label: objjson[index].sName + ' ' + objjson[index].sLastname,
                            value: objjson[index].sEmp
                        };
                        availableValueEmployees[index] = newObject;
                    });
                }
            });

            $('#ctl00_MainContent_txtsname').autocomplete({
                width: 300,
                max: 10,
                delay: 100,
                minLength: 1,
                autoFocus: true,
                cacheLength: 1,
                scroll: true,
                highlight: false,
                source: function (request, response) {
                    results = $.ui.autocomplete.filter(availableValueEmployees, request.term);
                    response(results.slice(0, 10));
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $("#ctl00_MainContent_txtsname").val(ui.item.label);
                    //$("#txtid").val(ui.item.value);
                },
                focus: function (event, ui) {
                    event.preventDefault();
                    //$("#txtid").val("");
                }
            });

            $('#stype').change(function () {
                if ($(this).val() == "E") {
                    $('.student').css("display", "none");
                    $('.emp').css("display", "");
                }
                else {
                    $('.student').css("display", "");
                    $('.emp').css("display", "none");
                }
            });
            var paraurl1 = getUrlParameter("type");
            var paraurl2 = getUrlParameter("sname");
            var paraurl3 = getUrlParameter("idlv");
            var paraurl4 = getUrlParameter("idlv2");

            if (paraurl1 == "E") {
                $('#ctl00_MainContent_txtsname').val(paraurl2);
                $("#stype").val(paraurl1);
                $('.student').css("display", "none");
                $('.emp').css("display", "");
            }
            if (paraurl1 == "U") {
                $('#ctl00_MainContent_txtsname').val(paraurl2);
                $("#stype").val(paraurl1);
                $('.student').css("display", "");
                $('.emp').css("display", "none");
            }
            else {
                $('#ctl00_MainContent_txtsname').val(paraurl2);
                $("#stype").val();
                $("#ctl00_MainContent_ddlsublevel option:selected").val(paraurl3);
            }
        });
    </script>
    <%--  <asp:UpdatePanel ID="update2" runat="server">
        <ContentTemplate>--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtSearch"
                CompletionInterval="1000" CompletionSetCount="20" UseContextKey="false" MinimumPrefixLength="1"
                ServiceMethod="getAutoListTUser" ServicePath="AutoCompleteService.asmx" EnableCaching="true"
                FirstRowSelected="true" CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
                CompletionListItemCssClass="listItem">
            </ajaxToolkit:AutoCompleteExtender>--%>
    <div class="full-card box-content leavelist-container">
        <div class="row">
            <div class="form-group col-md-6 col-sm-12">
                <label class="col-md-5 col-sm-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></label>
                <div class="col-md-7 col-sm-8">
                    <select id="stype" class="form-control">
                        <option value="" selected="selected"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                        <option value="U"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %></option>
                        <option value="E"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102165") %>/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102166") %></option>
                    </select>
                </div>
            </div>
            <div class="form-group col-md-6 col-sm-12 emp" style="display: none;">
                <label class="col-md-5 col-sm-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></label>
                <div class="col-md-7 col-sm-8">
                    <asp:TextBox ID="txtsname" runat="server" CssClass='form-control' />
                </div>
            </div>
        </div>
        <div class="row student">
            <div class="form-group col-md-6 col-sm-12">
                <label class="col-md-5 col-sm-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                <div class="col-md-7 col-sm-8">
                    <asp:DropDownList ID="ddlsublevel" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group col-md-6 col-sm-12">
                <label class="col-md-4 col-sm-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                <div class="col-md-7 col-sm-8">
                    <select id="ddlSubLV2" class="form-control">
                    </select>
                </div>
            </div>
        </div>
        <div class="row student">
            <div class="form-group col-md-6 col-sm-12">
                <label class="col-md-5 col-sm-4 control-label">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></label>
                <div class="col-md-7 col-sm-8">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass='form-control' class="input--mid"></asp:TextBox>
                    <input type="text" class='form-control' id="txtid" style="display: none;" />
                </div>
            </div>
            <div class="form-group col-sm-6">
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 text-center button-section">
                <input type="button" id="btnSearch" class='btn btn-primary global-btn' value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" />
            </div>
        </div>
        <div class="row mini--space__top">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <asp:DataGrid ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2"
                        GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" PageSize="20" CssClass="cool-table">
                        <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" CssClass="alternateCell" />
                        <Columns>
                            <asp:BoundColumn DataField="sID" HeaderStyle-Width="20%" HeaderText="sID" Visible="false">
                                <HeaderStyle Width="20%"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="sName" HeaderStyle-Width="20%" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>">
                                <HeaderStyle Width="20%"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="sLastName" HeaderStyle-Width="20%" HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>">
                                <HeaderStyle Width="20%"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206327") %>" HeaderStyle-Width="30%" DataField="sIdentification">
                                <HeaderStyle></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" CssClass="btn btn-success glyphicon glyphicon-plus "
                                        runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132600") %>" CommandName="Edit" />
                                    <asp:LinkButton ID="btnData" CssClass="btn btn-default glyphicon glyphicon-zoom-in "
                                        runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102227") %>" CommandName="Edit" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" CssClass="headerCell" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" CssClass="itemCell" />
                        <PagerStyle HorizontalAlign="Left" Mode="NumericPages" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" CssClass="pagerCell" />
                        <SelectedItemStyle ForeColor="GhostWhite" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" />
                    </asp:DataGrid>
                </div>
            </div>
        </div>
    </div>
    <%--      </ContentTemplate>
    </asp:UpdatePanel>--%>
    <div class="modal fade" id="modalLeave" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content leavelist-modal-container">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h3 class="modal-title" id="myModalLabel"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132600") %></h3>
                </div>
                <div class="modal-body modalinsert" style="max-height: 470px; overflow-y: scroll;">
                    <div class="row mini--space__top">
                        <div class="col-xs-12">
                            <div class="wrapper-table">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success global-btn btnMoreLeave">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132600") %></button>
                    <button type="button" class="btn btn-danger global-btn" data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalLeaveData" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content leavelist-modal-container">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102227") %> [<b id="lblName"></b>]</h4>
                </div>
                <div class="modal-body modallist" style="max-height: 470px; overflow-y: scroll;">
                    <div class="row mini--space__top">
                        <div class="col-xs-12">
                            <div class="wrapper-table">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger global-btn" data-dismiss="modal">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                </div>
            </div>
        </div>
    </div>
    <script>
        var setHTML = "";
        setHTML += '<div class="row"><div class="col-xs-3"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105057") %></label></div>';
        setHTML += '<div class="col-xs-3"><select id="leave_time" class="form-control"><option value="0">ลาวันเดียว</option></select></div></div>';

        setHTML += '<div class="row" >';
        setHTML += '<div class="col-xs-3"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %></label></div><div class="col-xs-9"> ';
        setHTML += '<div class="input-group" style="float:left;"><input id="dateLeave" class="form-control datepicker" data-date-format="dd/mm/yyyy" aria-describedby="basic-addon2" runat="server" />';
        setHTML += '<span class="input-group-addon" id="basic-addon2"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></span>';
        setHTML += '</div>';

        setHTML += '<div style="float: left;">&nbsp; - &nbsp;</div><div class="input-group" style="float:left;"> <input id="dateendLeave" class="form-control datepicker" data-date-format="dd/mm/yyyy" aria-describedby="basic-addon2" runat="server" />';
        setHTML += '<span class="input-group-addon" id="basic-addon2" ><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span></span></div>';
        setHTML += '</div>';
        setHTML += '</div>';

        setHTML += '<div class="row"><div class="col-xs-3"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></label></div>';
        setHTML += '<div class="col-xs-9"><select id="leave_type" class="form-control"><option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %></option><option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %></option></select></div></div>';

        //setHTML += '<div class="col-xs-12" style="margin-top:10px" id="divtlable"><div class="col-xs-2"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503004") %> :</label></div>';
        //setHTML += '<div class="col-xs-10"><label id="tvalue">06:00 - 11:59</label></div></div>';

        setHTML += '<div class="row" id="divtvalue"><div class="col-xs-3"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503004") %><b style="color:red;">*</b></label></div><div class="col-xs-9">'
        setHTML += '<div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">'
        setHTML += '<input type="text" class="form-control mon" id="tstart" runat="server" />'
        setHTML += '<span class="input-group-addon"><span class="glyphicon glyphicon-time"></span></span>'
        setHTML += '</div>'
        setHTML += '<div style="float: left;">'
        setHTML += '&nbsp; - &nbsp;'
        setHTML += '</div>'
        setHTML += '<div class="input-group clockpicker" data-placement="left"'
        setHTML += 'data-align="top" data-autoclose="true">'
        setHTML += '<input type="text" class="form-control mon" id="tend" runat="server" />'
        setHTML += '<span class="input-group-addon"><span class="glyphicon glyphicon-time"></span></span>'
        setHTML += '</div>'
        setHTML += '<div class="overlay-mon" id="overlaymon" runat="server" style="position: relative; z-index: 100; min-height: 40px;">'
        setHTML += '&nbsp;'
        setHTML += '</div></div></div>';

        setHTML += '<div class="row"><div class="col-xs-3"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132602") %><b style="color:red;">*</b></label></div><div class="col-xs-9"> <textarea class="form-control" id="detailLeave"></textarea></div>';
        setHTML += '</div>';

        function modalLeave(stdID) {
            $(".modalinsert").html('<div style="text-align:center"><img src="/images/loading.gif" /></div>');

            $(".modalinsert").html(setHTML);
            $('.datepicker').datepicker({ dateFormat: 'dd/mm/yy' });
            $("#ctl00_MainContent_dateendLeave").datepicker("destroy");
            $("#ctl00_MainContent_dateendLeave").attr('disabled', 'disabled');
            $('.clockpicker').clockpicker();

            $('#leave_time').change(function () {
                switch ($(this).val()) {
                    case "0":
                        $("#ctl00_MainContent_dateendLeave").datepicker("destroy");
                        $("#ctl00_MainContent_dateendLeave").attr('disabled', 'disabled');
                        break;
                    case "1":
                        $("#ctl00_MainContent_dateendLeave").datepicker();
                        $("#ctl00_MainContent_dateendLeave").removeAttr("disabled");
                        break;
                }
            })
            $('.btnMoreLeave').click(function () {

                if ($("#detailLeave").val() != "" && $("#ctl00_MainContent_dateLeave").val() != "") {
                    var valueDate = "" + $("#ctl00_MainContent_dateLeave").val() + "";
                    var date = valueDate.split("/");
                    var d = parseInt(date[0], 10),
                          m = parseInt(date[1], 10),
                          y = parseInt(date[2], 10);

                    var dateLeave = new Date(y, m - 1, d);
                    var dateNow = new Date();

                    if (dateLeave < dateNow) {
                        alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132605") %>");
                    }
                    else {
                        $.ajax({
                            url: "/App_Logic/insertJSON.aspx?mode=leaver&stdid=" + stdID,
                            type: "POST",
                            data: "dateLeave=" + $("#ctl00_MainContent_dateLeave").val() + "&detailLeave=" + $("#detailLeave").val()
                                + "&dateendLeave=" + $("#ctl00_MainContent_dateendLeave").val()
                                    + "&typeLeave=" + $("#leave_type").val() + "&typeTime=" + $("#leave_time").val()
                                    + "&tstart=" + $("input[id*=tstart]").val() + "&tend=" + $("input[id*=tend]").val(),
                            success: function (resp) {
                                if (resp == "success") {
                                    modalLeave(stdID);
                                }
                                else {
                                    alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132564") %>");
                                }
                            }
                        }).done(function () {
                            $('#modalLeave').modal('hide');
                        });
                    }
                }
                else {
                    alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132606") %>");
                }
            });
        }


        function modalLeaveData(stdID, stdName) {
            $("#lblName").text(stdName);
            $(".modallist").html('<div style="text-align:center"><img src="/images/loading.gif" /></div>');
            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=leaverlist&stdID=" + stdID,
                success: function (resp) {
                    console.log(resp);
                    var objjson = $.parseJSON(resp);
                    var strHtml = '<div style="text-align:center;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102035") %></div>';
                    if (objjson != []) {
                        $.each(objjson, function (index) {
                            if (index == 0) {
                                strHtml = "";
                                strHtml += '<table class="table table-striped"> <thead><tr><th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132603") %></th><th>วันสิ้นสุดการลา</th><th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %></th><th></th></tr></thead><tbody>';
                            }

                            var parsedDate = new Date(parseInt(objjson[index].dLeaveStart.substr(6)));
                            var parsedDate2 = new Date(parseInt(objjson[index].dLeaveEnd.substr(6)));
                            var jsDate = new Date(parsedDate);
                            var jsDate2 = new Date(parsedDate2);
                            strHtml += '<tr trleavedata="' + objjson[index].nLeave + '">'
                            strHtml += '<td>' + jsDate.format("dd/MM/yyyy HH:mm") + '</td>'
                            strHtml += '<td>' + jsDate2.format("dd/MM/yyyy HH:mm") + '</td>'
                            strHtml += '<td>' + typeLeave(objjson[index].cTypeLeave) + '</td>';
                            strHtml += '<td>' + buttonCancle(objjson[index].nLeave) + '</td></tr>';
                        });
                    }

                    $(".modallist").html(strHtml);
                    $(".btnCancle").click(function () {
                        var leaveID = $(this).attr("leaveid");
                        if (window.confirm('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132604") %>')) {
                            $.ajax({
                                url: "/App_Logic/deleteJSON.aspx?mode=leaver&leaveid=" + leaveID,
                                success: function (boolcancle) {
                                    if (boolcancle == "success") {
                                        $("tr[trleavedata='" + leaveID + "']").remove();
                                    }
                                    else {
                                        alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132564") %>");
                                    }
                                }
                            });
                        }
                        return false;
                    });
                }
            });
        }

        function buttonCancle(nLeave) {
            return "<button leaveid='" + nLeave + "' class='btn btn-danger font-size-14 btnCancle'><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>";
        }

        function typeLeave(typeData) {
            var typeName = "";
            switch (typeData) {
                case "0": typeName = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %>"; break;
                case "1": typeName = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %>"; break;
            }
            return typeName;
        }

        function typeTime(typeData) {
            var typeName = "";
            switch (typeData) {
                case "0": typeName = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M406041") %>"; break;
                case "1": typeName = "ช่วงบ่าย"; break;
                case "2": typeName = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132607") %>"; break;
            }
            return typeName;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
