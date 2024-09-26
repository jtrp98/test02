<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LCFilter.ascx.cs" Inherits="FingerprintPayment.UserControls.LCFilter" %>

<style>

</style>
<script defer>

    function LCFilter() {

        var _my = {};
       
        //_my.GetYearID = function () {
        //    return $("#sltYear").val();
        //};

        //_my.GetYearNo = function () {
        //    return $("#sltYear :selected").text();
        //};

        //_my.GetTermID = function () {
        //    return $("#sltTerm").val();
        //};

        _my.GetLevelID = function () {
            return $("#sltLevel").val();
        };

        _my.GetLevelText = function () {
            return $("#sltLevel option:selected").text();
        };
     
        _my.GetClassID = function () {
            return $("#sltClass").val();
        };

        _my.GetClassText = function () {
            return $("#sltClass option:selected").text();
        };

        return _my;
    }

   <%-- function LoadTerm(yearID, objResult) {
        if (yearID) {
            $.ajax({
                async: false,
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
    }--%>

    function LoadTermSubLevel2(subLevelID, objResult) {
        if (subLevelID) {
            $.ajax({
                async: true,
                type: "POST",
                url: "<%=Page.ResolveUrl("~/StudentInfo/StudentList.aspx/LoadTermSubLevel2")%>",
                data: '{subLevelID: ' + subLevelID + ' }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var subLevel2 = response.d;

                    $(objResult).empty();

                    if (subLevel2.length > 0) {

                        //var options = '<option value="">ทั้งหมด</option>';
                        var options = '<option value=""> <%= (IsRequired  ? "เลือกชั้นเรียน" : "ทั้งหมด") %></option>';
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
    }

    var LCF;
    $(function () {
        LCF = LCFilter();

        //// Search
        //$("#sltYear").change(function () {

        //    LoadTerm($(this).val(), '#sltTerm');

        //});

        $("#sltLevel").change(function () {

            LoadTermSubLevel2($(this).val(), '#sltClass');

        });
    });
</script>

<%--<div class="row">
    <div class="col-sm-1"></div>
    <label class="col-sm-1 col-form-label text-left">ปีการศึกษา</label>
    <div class="col-sm-3">
        <select id="sltYear" class="selectpicker col-sm-12" data-style="select-with-transition" title="เลือกปีการศึกษา" <%= (IsRequired ? "required" : "") %> >
            <asp:Literal ID="ltrYear" runat="server" />
        </select>
    </div>
    <div class="col-sm-1"></div>
    <label class="col-sm-1 col-form-label text-left">เทอม</label>
    <div class="col-sm-3">
        <select id="sltTerm" class="selectpicker col-sm-12" data-style="select-with-transition" title="เลือกเทอม" <%= (IsRequired ? "required" : "") %> >
            <asp:Literal ID="ltrTerm" runat="server" />
        </select>
    </div>
    <div class="col-sm-2"></div>
</div>--%>
<div class="row">
    <div class="col-sm-1"></div>
    <label class="col-sm-1 col-form-label text-left">ระดับชั้นเรียน/<br />Level Class</label>
    <div class="col-sm-3">
        <select id="sltLevel" name="sltLevel" class="selectpicker  --req-append-last" data-size="7"  data-width="100%" data-style="select-with-transition" title="เลือกระดับชั้นเรียน" <%= (IsRequired ? "required" : "") %> >
            <asp:Literal ID="ltrLevel" runat="server" />
        </select>
    </div>
    <div class="col-sm-1"></div>
    <label class="col-sm-1 col-form-label text-left">ชั้นเรียน/<br />Class</label>
    <div class="col-sm-3">
        <select id="sltClass" name="sltClass" class="selectpicker  --req-append-last" data-size="7"  data-width="100%" data-style="select-with-transition" title="เลือกชั้นเรียน" <%= (IsRequired ? "required" : "") %> >
          <%--  <option value="">ทั้งหมด</option>--%>
        </select>
    </div>
    <div class="col-sm-2"></div>
</div>
