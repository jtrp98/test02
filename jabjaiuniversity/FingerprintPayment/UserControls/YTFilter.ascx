<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YTFilter.ascx.cs" Inherits="FingerprintPayment.UserControls.YTFilter" %>

<style>

</style>
<script defer>

    function YTFilter() {

        var _my = {};
       
        _my.GetYearID = function () {
            return $("#sltYear").val();
        };

        _my.GetYearNo = function () {
            return $("#sltYear :selected").text();
        };

        _my.GetTermID = function () {
            return $("#sltTerm").val();
        };

        _my.GetTermText = function () {
            return $("#sltTerm option:selected").text();
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
      
    

    var YTF;
    $(function () {
        YTF = YTFilter();

        // Search
        $("#sltYear").change(function () {
            LoadTerm($(this).val(), '#sltTerm');
        });

    });
</script>

<div class="row">
    <div class="col-sm-1"></div>
    <label class="col-sm-1 col-form-label text-left">ปีการศึกษา/<br />Academic Year</label>
    <div class="col-sm-3">
        <select id="sltYear" class="selectpicker " data-width="100%"  data-size="7" data-style="select-with-transition"  title="เลือกปีการศึกษา" <%= (IsRequired ? "required" : "") %> >
            <asp:Literal ID="ltrYear" runat="server" />
        </select>
    </div>
    <div class="col-sm-1"></div>
    <label class="col-sm-1 col-form-label text-left">เทอม/<br />Semester</label>
    <div class="col-sm-3">
        <select id="sltTerm" class="selectpicker " data-width="100%" data-size="7"  data-style="select-with-transition" title="เลือกเทอม" <%= (IsRequired ? "required" : "") %> >
            <asp:Literal ID="ltrTerm" runat="server" />
        </select>
    </div>
    <div class="col-sm-2"></div>
</div>

