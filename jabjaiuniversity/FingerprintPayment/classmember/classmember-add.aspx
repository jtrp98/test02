<%@ Page Title="" Language="C#" MasterPageFile="~/mppopup.Master" AutoEventWireup="true" CodeBehind="classmember-add.aspx.cs" Inherits="FingerprintPayment.classmember_add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
 <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <script type="text/javascript">
        $(document).ready(function () {
            $(function () {
                $("#startDay").datepicker();
            });
        });
    </script>
<script type="text/javascript">
   
    $(document).ready(function () {
        $(function () {
            var head = document.getElementById("<%=head.ClientID%>").value;
            var help1 = document.getElementById("<%=help1.ClientID%>").value;
            var help2 = document.getElementById("<%=help2.ClientID%>").value;
            document.getElementById("<%=txtSearch.ClientID %>").value = head;
            document.getElementById("<%=txtSearch2.ClientID %>").value = help1;
            document.getElementById("<%=txtSearch3.ClientID %>").value = help2;
        });
    });
</script>
    
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('input[id*=btnDel]').click(function () {
                showModalConfirm("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %>", "<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601038") %></p>", $(this).attr('name')); return false;
            });
            //$("input[id*=txtSearch]").keypress(function (e) {
            //    if (e.keyCode == 13) {
            //        $("input[id*=btnSearch]").click();
            //    }
            //});
           
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

            $('#ctl00_MainContent_txtSearch').autocomplete({
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
                    $("#ctl00_MainContent_txtSearch").val(ui.item.label);
                },
                focus: function (event, ui) {
                    event.preventDefault();
                }
            });

            $('#ctl00_MainContent_txtSearch').val(getUrlParameter("name"));
            $("#type").val(getUrlParameter("type"));
            $("#btnsearch").click(function () {
                window.location.href = "employeeslist.aspx?name=" + $('#ctl00_MainContent_txtSearch').val() + "&type=" + $("#type").val();
            });

            $('#ctl00_MainContent_txtSearch2').autocomplete({
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
                    $("#ctl00_MainContent_txtSearch2").val(ui.item.label);
                },
                focus: function (event, ui) {
                    event.preventDefault();
                }
            });

            $('#ctl00_MainContent_txtSearch2').val(getUrlParameter("name"));
            $("#type").val(getUrlParameter("type"));
            $("#btnsearch").click(function () {
                window.location.href = "employeeslist.aspx?name=" + $('#ctl00_MainContent_txtSearch2').val() + "&type=" + $("#type").val();
            });

            $('#ctl00_MainContent_txtSearch3').autocomplete({
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
                    $("#ctl00_MainContent_txtSearch3").val(ui.item.label);
                },
                focus: function (event, ui) {
                    event.preventDefault();
                }
            });

            $('#ctl00_MainContent_txtSearch3').val(getUrlParameter("name"));
            $("#type").val(getUrlParameter("type"));
            $("#btnsearch").click(function () {
                window.location.href = "employeeslist.aspx?name=" + $('#ctl00_MainContent_txtSearch3').val() + "&type=" + $("#type").val();
            });
            
        });


        function changeFinger() {
            $.ajax("/Api/change/?userid=" + $("#ctl00_MainContent_hdfsid").val() + "&type=1", function (Result) {
            }).done(function (Result) {
                $("#modalpopupdata-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111048") %> " + Result);
                $("#modalpopupdatamac .modal-footer").addClass("hidden");
            });
        }
        function ShowPopUP(userid, name) {
            $("#modalpopupdata-content").html("ท่านต้องการลบลายนี้มือของ " + name + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603024") %> <br/>" +
                "เมื่อท่านทำการลบลายนี้วมือแล้วจะไม่สามารถทำรายการได้ ");
            $("#ctl00_MainContent_hdfsid").val(userid);
            $("#modalpopupdatamac .modal-footer").removeClass("hidden")
        }
    </script>
    <style>
        .completionList {
            border: solid 1px #444444;
            background-color: White;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
        }
        .cover {
    
    text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
}
        .bigfont {
    
    font-size: 200%;
}
        .smol {
    
    font-size: 85%;
}
        .listItem {
            color: blue;
            background-color: White;
        }
        .hid {
             visibility: hidden;
         }
        .hid2 {
             visibility: hidden;
             display: none;
         }
        
        .width10 {
            margin: 0 auto;
            width: 10%;
        }
        .centertext {
            text-align: center;
        }
        .bord {
            border-left:    1px solid #ffffff;
        }
        .itemHighlighted {
            background-color: #ffc0c0;
        }
        label {
            font-weight: normal;
            font-size: 26px;
        }
        .gvbutton  {
     font-size: 25px;
     
        }
        .nounder a:hover{
    text-decoration: none;
        

}
        .button-custom {
                font-size: 26px;
                padding-left: 30px;
                padding-right: 30px;
                width: 100%;
            }
        .shadowblack{
    text-decoration: none;
        text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
}
        .ui-autocomplete {
            position: absolute;
            cursor: default;
            z-index: 1060 !important;
        }
        .boxhead a {
    color: #FFFFFF;
    text-decoration: none;
}
        a.imjusttext{ color: #ffffff; text-decoration: none; }
a.imjusttext:hover { color: aquamarine; }
        .centerText {
            text-align: center;
        }
        .btn-red {
  background: red; /* use your color here */
}
        

        .nowrap {
            max-width:100%;
            white-space:nowrap;
        }
         .width100 {
            margin: 0 auto;
            width: 100%;
        }
         body { margin-left: 0; margin-right: 0 }
hr {
    margin-left: -30px;
  margin-right: -30px;
}
        .namemangin {
            margin-left: 5px;
            padding-left: 35px;
            border-left: 10px
        }
        .setmin-width0 {
            min-width: 0px !important;
        }

        .setfont-size14 {
            font-size: 14px !important;
        }
        .tab {border-collapse:collapse;margin-left: 6px; margin-right: 6px; border-bottom:3px solid #337AB7; border-left:3px solid #337AB7;border-right: 3px solid #337AB7; border-top:3px solid #337AB7;box-shadow: inset 0 1px 0 #337AB7;}

    </style>
    <style>
        .leftText {
            text-align: left;
        }
    </style>
    <div class="full-card text-center planeadd-container">
        <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
        </asp:ScriptManager>
        <asp:HiddenField ID="hfdsClassID" runat="server" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                
<h1 class="leftText"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803006") %></h1>
                    <hr />
                <div class="form-group row" id="row-name">
                    <div class="col-xs-12">
                        <div class="col-xs-3">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210005") %></label>
                        </div>
                        <div class="col-xs-7" >
                            <input type="hidden" runat="server" id="head" />  
                            <input type="hidden" runat="server" id="help1" /> 
                            <input type="hidden" runat="server" id="help2" />                     
                    <asp:TextBox ID="txtSearch"   runat="server" CssClass='form-control' Style="width: 100%;"></asp:TextBox>                    
                        </div>
                    </div>
                </div>
                <div class="form-group row" id="row-name">
                    <div class="col-xs-12">
                        <div class="col-xs-3">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303021") %></label>
                        </div>
                        <div class="col-xs-7" >
                    <asp:TextBox ID="txtSearch2"  runat="server" CssClass='form-control' Style="width: 100%;"></asp:TextBox>                    
                        </div>
                    </div>
                </div>
                <div class="form-group row" id="row-name">
                    <div class="col-xs-12">
                        <div class="col-xs-3">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303022") %></label>
                        </div>
                        <div class="col-xs-7" >
                    <asp:TextBox ID="txtSearch3" runat="server" CssClass='form-control' Style="width: 100%;"></asp:TextBox>                    
                        </div>
                    </div>
                </div>
                
                           <hr />
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
