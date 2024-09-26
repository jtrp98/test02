<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="reportjobscanning.aspx.cs" Inherits="FingerprintPayment.Modules.TimeAttendance.reportjobscanning" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div>
<div class="panel panel-warning">
 <div class="panel-heading"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132653") %></div>
  <div class="panel-body">
  <div class="col-md-12">
    <div class="col-md-3">  <div><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603001") %> : </div>
        <select class="form-control" id="ddl_report">
        <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104055") %></option>
        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132646") %></option>
        <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132647") %></option>
        <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203054") %></option>
        </select>
    </div>
     <asp:UpdatePanel ID="update2" runat="server">
    <ContentTemplate>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
  
     <div class="col-md-3 divreport hide" id="userdiv">  <div><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132639") %> : </div>
     <div>   <asp:TextBox ID="txtSearch" runat="server" AutoCompleteType="Disabled" AutoPostBack="false" OnTextChanged="txtSearch_TextChanged" class="form-control"></asp:TextBox> </div>
    </div>
               
           
<cc1:AutoCompleteExtender ServiceMethod="SearchCustomers"
    MinimumPrefixLength="2"
    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
    TargetControlID="txtSearch"
    ID="AutoCompleteExtender2" runat="server" FirstRowSelected = "false">
</cc1:AutoCompleteExtender>
    </ContentTemplate>
    </asp:UpdatePanel>

   <div class="col-md-2 divreport hide" id="yeardiv">  <div><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101312") %> : </div>
        <select class="form-control" id="ddl_year">
        </select>
    </div>

       <div class="col-md-3  divreport hide" id="termdiv">  <div><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101011") %> : </div>
        <select class="form-control" id="ddl_temrsublv">
        <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
        </select>
    </div>

</div>

 <div class="col-md-12" style="margin-top:20px;">
 <div class="col-md-10"></div>
 <div class="col-md-2">
    <button class="btn btn-primary btnreport" style="font-size:14px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132640") %></button>
 </div>
  </div>

    <hr/>
    </div>
    </div>
</div>
    <div class="panel panel-warning">
 
  <div class="panel-body">
    <div id="container" style="width:100%; height:auto; margin: 0 auto"></div>
    <div id="container2" style="width:100%; height:auto; margin: 0 auto"></div>
  </div>
</div>

		<script type="text/javascript">

		    var chart;
		    var nullYear=1;
		    $(document).ready(function () {



		        $.ajax({
		            url: "/App_Logic/modalJSON.aspx?mode=allyear",
		            success: function (resp) {
		                var obj = $.parseJSON(resp);


		                if (obj != []) {
		                    $.each(obj, function (index) {
		                        $("#ddl_year").append("<option value='" + obj[index].nYear + "'>" + obj[index].numberYear + "</option>");
		                    });

		                    $("#ddl_year").change(function () {
		                        var nYear = $(this).val();
		                        if (nYear != "0" && $("#ddl_report").val() == "2") {
		                            $.ajax({
		                                url: "/App_Logic/modalJSON.aspx?mode=allsublv2&nYear=" + nYear,
		                                success: function (resp2) {
		                                    var objLV2 = $.parseJSON(resp2);
		                                    $("#ddl_temrsublv").html('<option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>');
		                                    if (objLV2 != []) {
		                                        $.each(objLV2, function (index) {
		                                            $("#ddl_temrsublv").append('<option value="' + objLV2[index].SubLevelID + '">' + objLV2[index].SubLevelName + '/' + objLV2[index].SubLevelNameLV2 + '</option>');
		                                        });
		                                    }
		                                }
		                            });
		                        }
		                        else {
		                            $("#ddl_temrsublv").html('<option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>');
		                        }
		                    });

		                } else {
		                    nullYear = 0;
		                }
		            }
		        });


		        $("#ddl_report").change(function () {
		            switch ($(this).val()) {
		                case "0": $(".divreport").addClass("hide"); break;
		                case "1": $("#yeardiv").removeClass("hide");
		                    $("#termdiv").addClass("hide");
		                    $("#userdiv").addClass("hide");
		                    break;
		                case "2": $("#yeardiv").removeClass("hide");
		                    $("#termdiv").removeClass("hide");
		                    $("#userdiv").addClass("hide");
		                    var nYear = $("#ddl_year").val();
		                    if (nYear != "0" && nYear != "" && $("#ddl_report").val() == "2") {
		                        $.ajax({
		                            url: "/App_Logic/modalJSON.aspx?mode=allsublv2&nYear=" + nYear,
		                            success: function (resp2) {
		                                var objLV2 = $.parseJSON(resp2);
		                                $("#ddl_temrsublv").html('<option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>');
		                                if (objLV2 != []) {
		                                    $.each(objLV2, function (index) {
		                                        $("#ddl_temrsublv").append('<option value="' + objLV2[index].SubLevelID + '">' + objLV2[index].SubLevelName + '/' + objLV2[index].SubLevelNameLV2 + '</option>');
		                                    });
		                                }
		                            }
		                        });
		                    }
		                    else {
		                        $("#ddl_temrsublv").html('<option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132648") %></option>');
		                    }
		                    break;
		                case "3": $("#yeardiv").removeClass("hide");
		                    $("#termdiv").addClass("hide");
		                    $("#userdiv").removeClass("hide"); break;
		            }
		        });

		        $(".btnreport").click(function () {
		            if ($("#ddl_report").val() != "0") {
		                switch ($("#ddl_report").val()) {
		                    case "1": $.ajax({
		                        url: "/App_Logic/dataJSON.aspx?mode=reportuser&type=1&years=" + $("#ddl_year").val(),
		                        success: function (respjson) {
		                            var strJSON = respjson.replace("\"", '"');
		                            var arrSplit = strJSON.split("split");
		                            var chartOBJ = $.parseJSON(arrSplit[0]);
		                            var chartOBJ2 = $.parseJSON(arrSplit[1]);
		                            var name1 = ["<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %>"];
		                            var name2 = ["<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105017") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131055") %>"];
		                            LineChart(chartOBJ, "container", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131056") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132651") %> ", name1);
		                            LineChart(chartOBJ2, "container2", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131051") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132652") %> ", name2);
		                        }
		                    }); break;
		                    case "2":
		                        $.ajax({
		                            url: "/App_Logic/dataJSON.aspx?mode=reportuser&type=0&years=" + $("#ddl_year").val() + "&sublv2=" + $("#ddl_temrsublv").val(),
		                            success: function (respjson) {
		                                var strJSON = respjson.replace("\"", '"');
		                                var arrSplit = strJSON.split("split");
		                                var chartOBJ = $.parseJSON(arrSplit[0]);
		                                var chartOBJ2 = $.parseJSON(arrSplit[1]);
		                                var name1 = ["<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %>"];
		                                var name2 = ["<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105017") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131055") %>"];
		                                LineChart(chartOBJ, "container", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131056") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132649") %> ", name1);
		                                LineChart(chartOBJ2, "container2", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131051") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132650") %> ", name2);
		                            }
		                        });
		                        break;

		                    case "3":
		                        if ($("#ctl00_MainContent_txtSearch").val() != "") {
		                            $.ajax({
		                                url: "/App_Logic/dataJSON.aspx?mode=userreportbyid&years=" + $("#ddl_year").val() + "&userid=" + $("#ctl00_MainContent_txtSearch").val(),
		                                success: function (respjson) {
		                                    var strJSON = respjson.replace("\"", '"');
		                                    var arrSplit = strJSON.split("split");
		                                    var chartOBJ = $.parseJSON(arrSplit[0]);
		                                    var chartOBJ2 = $.parseJSON(arrSplit[1]);
		                                    var name1 = ["<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %>"];
		                                    var name2 = ["<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105017") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131055") %>"];
		                                    var userName = chartOBJ[0].Name;
		                                    if (userName == undefined) {
		                                        userName = " -";
		                                    }
		                                    LineChart(chartOBJ, "container", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131056") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131057") %>" + userName + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210455") %> ", name1);
		                                    userName = chartOBJ2[0].Name;
                                             if (userName == undefined) {
		                                        userName = " -";
		                                    }
		                                    LineChart(chartOBJ2, "container2", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131051") %>", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131052") %>" + userName + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210455") %> ", name2);
		                                }
		                            });
		                        } else {
		                            alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132645") %>");
		                        }
		                        break;
		                }
		            }
		            return false;
		        });

		    });

		    function LineChart(objs,elem,fileexp ,title,name) {

		        if (name == undefined) {
		            name = "-";
                }
		        $('#' + elem).highcharts({
		            chart: {
		                type: 'column'
		            },
		            exporting: {
		                url: '/HighchartsExport.axd',
		                filename: fileexp,
		                width: 1200
		            },
		            title: {
		                text: title + $("#ddl_year option:selected").text()
		            },
		            subtitle: {
		             //   text: 'Source: WorldClimate.com'
		            },
		            xAxis: {
		                categories: 
             objs[0].Month
            ,
		                crosshair: true
		            },
		            yAxis: {
		                min: 0,
		                title: {
		                    text: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %> (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %>)'
		                }
		            },
		            tooltip: {
		                headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
		                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.1f} <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102213") %></b></td></tr>',
		                footerFormat: '</table>',
		                shared: true,
		                useHTML: true
		            },
		            plotOptions: {
		                column: {
		                    pointPadding: 0.2,
		                    borderWidth: 0
		                }
		            },
		            series: [{
		                name: name[0],
		                data: objs[0].Count,
                        color: "green"

                    },
                    {
                        name: name[1],
                        data: objs[0].Count2,
                        color: "red"

                    },
                    {
                        name: name[2],
                        data: objs[0].Count3,
                        color: "gray"

                    }
                  ]
		        });
            }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
