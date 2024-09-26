<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YTLCFilter.ascx.cs" Inherits="FingerprintPayment.UserControls.YTLCFilter" %>

<style>

</style>
<script defer>

    function YTLCFilter() {

        var _my = {};

        _my.GetYearID = function () {
            return $("#sltYear").val();
        };

        _my.GetYearNo = function () {
            return $("#sltYear :selected").text();
        };

        _my.GetTermNo = function () {
            return $("#sltTerm :selected").text();
        };

        _my.GetTermID = function () {
            return $("#sltTerm").val();
        };

        _my.GetLevelID = function () {
            return $("#sltLevel").val();
        };

        _my.GetClassID = function () {
            return $("#sltClass").val();
        };

        return _my;
    }

    function LoadTerm(yearID, objResult) {
        if (yearID) {
            $.ajax({
                async: true,
                type: "POST",
                url: "<%=Page.ResolveUrl("~/StudentInfo/StudentList.aspx/LoadTerm")%>",
                data: '{yearID: ' + yearID + ' }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var yearData = response.d;

                    $(objResult).empty();

                    if (yearData.length > 0) {

                        var options = '';
                        $(yearData).each(function () {

                            options += '<option value="' + this.id + '">' + this.name + '</option>';

                        });

                        $(objResult).html(options);
                        $(objResult).selectpicker('refresh');
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

    function LoadTermSubLevel2(subLevelID, objResult) {
        var level = (subLevelID).toString();
        $.ajax({
            async: true,
            type: "POST",
            url: "<%=Page.ResolveUrl("~/StudentInfo/StudentList.aspx/LoadRoom")%>",
            data: '{subLevelID: "' + level + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var subLevel2 = response.d;

                $(objResult).empty();

                if (subLevel2)
                    if (subLevel2.length > 0) {

                        var options = '<option value=""> <%= (IsRequired || IsRoomRequired ? "เลือกชั้นเรียน" : "ทั้งหมด") %></option>';
                        $(subLevel2).each(function () {
                            options += '<option value="' + this.id + '">' + this.name + '</option>';
                        });

                        $(objResult).html(options);
                        $(objResult).selectpicker('refresh');
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

    var YTLCF;
    $(function () {
        YTLCF = YTLCFilter();

        // Search
        $("#sltYear").change(function () {

            LoadTerm($(this).val(), '#sltTerm');

        });

        $("#sltLevel").change(function () {

            LoadTermSubLevel2($(this).val(), '#sltClass');

        });
    });
</script>

<div class="row">
    <div class="col-sm-1"></div>
    <label class="col-sm-1 col-form-label text-left">
        ปีการศึกษา/<br />
        Academic Year</label>
    <div class="col-sm-3">
        <select id="sltYear" class="selectpicker " data-width="100%" data-style="select-with-transition" title="เลือกปีการศึกษา" <%= (IsRequired ? "required" : "") %>>
            <asp:Literal ID="ltrYear" runat="server" />
        </select>
    </div>
    <div class="col-sm-1"></div>
    <label class="col-sm-1 col-form-label text-left">
        เทอม/<br />
        Semester</label>
    <div class="col-sm-3">
        <select id="sltTerm" class="selectpicker " data-width="100%" data-style="select-with-transition" title="เลือกเทอม" <%= (IsRequired ? "required" : "") %>>
            <asp:Literal ID="ltrTerm" runat="server" />
        </select>
    </div>
    <div class="col-sm-2"></div>
</div>
<div class="row">
    <div class="col-sm-1 --class"></div>
    <label class="col-sm-1 col-form-label text-left --class">
        ระดับชั้นเรียน/<br />
        Level Class</label>
    <div class="col-sm-3 --class">
        <select id="sltLevel" name="sltLevel" <%=(IsLevelMultiSelect ? "multiple" : "") %> class="selectpicker --req-append-last" data-width="100%" data-style="select-with-transition" title="เลือกระดับชั้นเรียน" <%= (IsRequired || IsLevelRequired ? "required" : "") %>>
            <asp:Literal ID="ltrLevel" runat="server" />
        </select>
    </div>
    <div class="col-sm-1 --level"></div>
    <label class="col-sm-1 col-form-label text-left --level">
        ชั้นเรียน/<br />
        Class</label>
    <div class="col-sm-3 --level">
        <select id="sltClass" name="sltClass" class="selectpicker --req-append-last " data-width="100%" data-style="select-with-transition" title="<%= (IsRequired || IsRoomRequired ? "เลือกชั้นเรียน" : "ทุกชั้นเรียน") %>" <%= (IsRequired || IsRoomRequired ? "required" : "") %>>
            <%--          <option value="">เลือกชั้นเรียน</option>--%>
        </select>
    </div>
    <div class="col-sm-2 --level"></div>
</div>
