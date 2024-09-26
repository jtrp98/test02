<%@ Page Title="" Language="C#" MasterPageFile="~/mppopup.Master" AutoEventWireup="true"
    CodeBehind="planeadd.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.planeadd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function modal_plane(msg) {
            showModal("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", msg, function () {
                $("input[id*=txtPlaneID]").focus();
            });
        }
    </script>
    <div class="full-card text-center planeadd-container">
        <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
        </asp:ScriptManager>
        <asp:HiddenField ID="hfdsClassID" runat="server" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row planadd-row">
                    <div class="col-xs-12">
                        <div class="col-xs-3">
                            <label>
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201003") %></label>
                        </div>
                        <div class="col-xs-8">
                            <input class="form-control" type="text" clientidmode="Static"
                                id="txtPlaneID" runat="server" />
                        </div>
                  
                    </div>
                </div>
                <div class="row planadd-row">
                    <div class="col-xs-12">
                        <div class="col-xs-3">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202017") %></label>
                        </div>
                        <div class="col-xs-8">
                            <input class="form-control" type="text" id="txtPlaneName" runat="server" />
                        </div>
                        
                    </div>
                </div>
                <%--       <table width="80%" style="padding-left: 10px;">
                    <tr>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <div id="divmain">
                            </div>
                            <div id="myTabContent" class="tab-content">
                            </div>
                        </td>
                    </tr>
                </table>--%>
                <div class="row text-center planadd-row">
                    <div class="col-xs-12 button-segment">
                        <asp:Button CssClass="btn btn-primary global-btn" ID="btnSave" runat="server"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>" />
                        <asp:Button CssClass="btn btn-danger global-btn" ID="btnCancle" runat="server"
                            Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>" />
                    </div>
                </div>
                <div class="row--space">
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:TextBox runat="server" ID="txtListtime" Style="display: none;" />
    </div>
    <script type="text/javascript">
        var dataUpdate;
        var _nhol = "";
        function ShowSubLevel(level) {
            if ($('table[id*=tdsublevel' + level + ']').css("display") == "none") {
                $('table[id*=tdsublevel' + level + ']').css("display", "");
                $('i[id=tdlevel' + level + ']').removeClass("glyphicon glyphicon-chevron-right");
                $('i[id=tdlevel' + level + ']').addClass("glyphicon glyphicon-chevron-down");
            } else {
                $('table[id*=tdsublevel' + level + ']').css("display", "none");
                $('i[id=tdlevel' + level + ']').removeClass("glyphicon glyphicon-chevron-down");
                $('i[id=tdlevel' + level + ']').addClass("glyphicon glyphicon-chevron-right");
            }
        }
        $(document).ready(function () {

            $('td[id*=tdlevel]').bind("DOMSubtreeModified", function (e, s) {
                alert($(this).attr('id'));
            });
            //tabtimetype('00', 'divmain', 'myTabContent');
        });

        function tabtimetype(nHol, divmain, divsub) {
            _nhol = nHol;
            $('input[id*=txtListtime]').val("");
            $('#divmain').html("");
            $('#myTabContent').html("");
            $('#modalSet').html("");
            $('#modalsub').html("");
            var strHtml = "";
            $.ajaxSetup({ cache: false });
            $.ajax({
                type: "POST",
                url: "/App_Logic/dataGeneric.ashx?ID=&mode=tabtimetype",
                cache: false,
                contentType: 'application/json;',
                success: function (objjson, status) {
                    strHtml = '<ul id="myTab" class="nav nav-tabs">';
                    var tabHTML = "";
                    $.each(objjson, function (index) {
                        strHtml += ' <li ';
                        if (index == 0) {
                            strHtml += ' class="active"';
                        }
                        else {
                        }
                        strHtml += '><a href="#tab' + objjson[index].nTimeType + '" data-toggle="tab">' + objjson[index].sTimeType + '</a></li>';
                        getlevel(objjson[index].nTimeType, index, divsub)
                    })
                    strHtml += '</ul>';
                    $('#' + divmain).html(strHtml);
                }
            });
        }

        function getlevel(sid, _index, divsub) {
            var strHtml = "";
            $.ajax({
                type: "POST",
                url: "/App_Logic/dataGeneric.ashx?ID=" + sid + "&mode=listlevel",
                cache: false,
                contentType: 'application/json;',
                success: function (objlevel, status) {
                    var tabHTML = "";
                    var checked = "checked";
                    if (_index == 0) {
                        tabHTML += '<div class="tab-pane fade in active" id="tab' + sid + '" style="background:white;">';
                    }
                    else {
                        tabHTML += '<div class="tab-pane fade" id="tab' + sid + '" style="background:white;">';
                    }
                    tabHTML += '<table class="table table-striped"><tr style="background:#286090;"><td style="width:70%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132570") %></td><td style="width:15%"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132031") %></td></tr>';
                    $.each(objlevel, function (index) {
                        var strsublevel = getsublevel(objlevel[index].nTLevel);
                        if (strsublevel == undefined) {
                            strsublevel = "";
                        }
                        tabHTML += '<tr><td id="tdlevel' + objlevel[index].nTLevel + '" >'
                        tabHTML += '<i id="tdlevel' + objlevel[index].nTLevel + '" class="glyphicon glyphicon-chevron-right" onclick="ShowSubLevel(' + objlevel[index].nTLevel + ')" >' + objlevel[index].LevelName + '</i><br/><br/>'
                        tabHTML += '</td><td><input type="checkbox" '
                        tabHTML += 'value="' + objlevel[index].nTLevel + '" termsub="yes" class="form-control termsub" id="txtlv' + objlevel[index].nTLevel + '" '
                        tabHTML += 'style="width:70px;height:39px;" ' + checked + ' onclick=Allsublevel(' + objlevel[index].nTLevel + ',$(this).is(":checked"));  />' + strsublevel
                        tabHTML += '</td>'
                        tabHTML += '</td></tr>';
                    })
                    tabHTML += "</table></div>";
                    $('#' + divsub).html($('#' + divsub).html() + tabHTML);
                }
            });
        }
        function getsublevel(sid) {
            var strHtml = "";
            $.ajax({
                type: "POST",
                url: "/App_Logic/dataGeneric.ashx?ID=" + sid + "&mode=listsublevel&nhol=" + _nhol,
                cache: false,
                contentType: 'application/json;',
                success: function (objlevel, status) {
                    var tabHTML = "";
                    var checked = false;
                    tabHTML += '<table class="table table-striped" style="display:none;" id="tdsublevel' + sid + '">';
                    $.each(objlevel, function (index) {
                        if (objlevel[index].nHoliday != null || objlevel[index].nAll == "1") {
                            checked = " checked";
                        }
                        else {
                            checked = "";
                            $('input[id*=txtlv' + sid + ']').attr("checked", false);
                        }
                        tabHTML += '<tr><td style="width:70%">' + objlevel[index].SubLevel + '</td>'
                        tabHTML += '<td><input nHol="' + objlevel[index].nTSubLevel + '" type="checkbox" '
                        tabHTML += 'value="' + objlevel[index].nTSubLevel + '" termsub="yes" class="form-control termsub" id="txtsublv' + sid + '_' + objlevel[index].nTSubLevel + '" '
                        tabHTML += 'style="width:70px;height:39px;" ' + checked + ' onclick=GetListtime($(this).val(),$(this).is(":checked"),' + sid + '); /></td>'
                        tabHTML += '</td></tr>';
                    })
                    tabHTML += "</table>";
                    $('#tdlevel' + sid).html($('#tdlevel' + sid).html() + tabHTML);
                }
            });
        }

        function Allsublevel(val, addtime) {
            var _replace = $('input[id*=txtListtime]').val();
            var _str = "";
            var _ch = true;
            $.each(_replace.split(','), function (e, s) {
                $('input[id*=txtsublv' + val + ']').each(function (e, subs) {
                    if (s != "" && $(subs).val() == s) {
                        _ch = false;
                    }
                });
                if (_ch && s != "") {
                    _str += s + ",";
                }
                _ch = true;
            });
            if (_str == NaN) _str = "";
            if (addtime) {
                $('input[id*=txtsublv' + val + ']').each(function (e, subs) {
                    _str += $(subs).val() + ",";
                });
            }
            $('input[id*=txtListtime]').val(_str);
            $('input[id*=txtsublv' + val + ']').attr("checked", addtime);
        }
        function GetListtime(val, addtime, lv) {
            if (addtime) {
                $('input[id*=txtListtime]').val($('input[id*=txtListtime]').val() + val + ",");
            }
            else {
                var _replace = $('input[id*=txtListtime]').val();
                var _str = "";
                $.each(_replace.split(','), function (e, s) {
                    if (s != "" && val != s) {
                        _str = _str + s + ",";
                    }
                    else {
                    }
                });
                $('input[id*=txtListtime]').val(_str);
            }
            var _ch = true;
            $('input[id*=txtsublv' + lv + ']').each(function (e, subs) {
                if ($(subs).is(":checked") == false) {
                    _ch = false;
                }
            });
            $('input[id*=txtlv' + lv + ']').attr("checked", _ch)
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
