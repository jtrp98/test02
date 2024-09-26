<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default2.aspx.cs" Inherits="FingerprintPayment.Default2" Async="true" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">

        window.onerror = function (msg, url, line, col, error) {
            // Note that col & error are new to the HTML 5 spec and may not be 
            // supported in every browser.  It worked for me in Chrome.
            var extra = !col ? '' : '\ncolumn: ' + col;
            extra += !error ? '' : '\nerror: ' + error;

            // You can view the information in an alert to see things working like this:
            alert("Error: " + msg + "\nurl: " + url + "\nline: " + line + extra);

            // TODO: Report this error via ajax so you can keep track
            //       of what pages have JS issues

            var suppressErrorAlert = true;
            // If you return true, then error alerts (like in older versions of 
            // Internet Explorer) will be suppressed.
            return suppressErrorAlert;
        };

        function getParameterByName(name, url) {
            url = url || window.location.href;
            name = name.replace(/[\[\]]/g, '\\$&');
            var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, ' '));
        }

        $(function () {
            $("input[id*=txtspass]").keydown(function (e) {
                if (e.keyCode == 13) {
                    Login();
                }
            });

            $("#btnLogin").click(function (e) {
                e.preventDefault();
                Login();
            });
        })
        function Login() {
            //$("#modal-content").html("<h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M111104') %>ที่เคา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %><br/><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701067") %> School Bright จะ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206010") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801046") %>ทุกส่วนงาน<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133177") %> 12-14 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107044") %> 2564 เ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ื่อให้<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106043") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ะสิทธิภา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ที่<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206542") %>ยิ่ง<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>ึ้น<br/><br/>ด้วยความเคา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %><br/>" +
            //    "บ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ิษัท จับจ่าย คอ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>์<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %>อเ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ชั่น จำกัด<br/><br/>" +
            //    "Dear Users<br/><br/>" +
            //    "Please be adviced that School Bright will be conducting system maintenance from 12-14 April 2020. All services will not be available.<br/><br/>" +
            //    "We apologize for any inconvenience caused.<br/><br/>" +
            //    "Jabjai Corporation Co., Ltd." +
            //    "</h1>");
            //$("#modalAlert").modal();
            //return;
            var user_id = $("input[id*=txtsid]").val();
            var password = $("input[id*=txtspass]").val();
            if (user_id == "") {
                $("#modal-header").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M03000') %>");
                $("#modal-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M03003') %>");
                $("#modalAlert").modal();
            }
            else if (password == "") {
                $("#modal-header").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M03000') %>");
                $("#modal-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M03002') %>");
                $("#modalAlert").modal();
            }
            else {
                $("input[id*=txtsid]").prop("disabled", "disabled");
                $("input[id*=txtspass]").prop("disabled", "disabled");
                //$("#btnLogin").addClass("disabled");
                //$("#btnLogin").html("<i class=\"fa fa-circle-o-notch fa-spin\" aria-hidden=\"true\"></i> Loading");
                $("body").mLoading();
                //PageMethods.Login(user_id, password);
                PageMethods.Login(user_id, password, OnSuccess, OnError);
            }
        }

        function OnSuccess(response) {
            switch (response.StatusCode) {
                case "200":
                    var returnUrl = getParameterByName('returnUrl');
                    window.location.href = returnUrl || "/StudentCall/main.aspx";
                    //if (returnUrl) {
                    //    window.location.href = returnUrl;
                    //}
                    //else {
                    //    window.location.href = "/AdminMain.aspx";
                    //}
                    break;
                case "501":
                    $("#modal-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M03004') %><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M03005') %>");
                    $("#modalAlert").modal();
                    $("input[id*=txtspass]").val("");
                    //$("#btnLogin").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00237") %>");
                    //$("#btnLogin").removeClass("disabled");
                    $("input[id*=txtsid]").prop("disabled", "");
                    $("input[id*=txtspass]").prop("disabled", "");
                    $("body").mLoading('hide');
                    break;
                case "404":
                    $("#modal-content").html("ท่านไม่มีสิทธิในการเข้าใช้ระบบ กรุณาติดต่อผู้ดูแลระบบโรงเรียนท่านเพื่อทำการเปิดสิทธิ");
                    $("#modalAlert").modal();
                    //$("input[id*=txtspass]").val("");
                    //$("#btnLogin").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00237") %>");
                    $("#btnLogin").removeClass("disabled");
                    $("input[id*=txtsid]").prop("disabled", "");
                    $("input[id*=txtspass]").prop("disabled", "");
                    $("body").mLoading('hide');
                    break;
                case "405":
                    $("#modal-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M111103') %><br/>Oops! Your school account has been temporarily suspended, please contact your school administrator to resolve the issue.");
                    $("#modalAlert").modal();
                    //$("input[id*=txtspass]").val("");
                    //$("#btnLogin").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00237") %>");
                    $("#btnLogin").removeClass("disabled");
                    $("input[id*=txtsid]").prop("disabled", "");
                    $("input[id*=txtspass]").prop("disabled", "");
                    $("body").mLoading('hide');
                    break;
                default:
                    $("#modal-content").html("<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M111104') %><br/>" +
                        "<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M111105') %>" +

                        "<br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M111106') %></p>");
                    $("#modalAlert").modal();
                    $("input[id*=txtspass]").val("");
                    //$("#btnLogin").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00237") %>");
                    //$("#btnLogin").removeClass("disabled");
                    $("input[id*=txtsid]").prop("disabled", "");
                    $("input[id*=txtspass]").prop("disabled", "");
                    $("body").mLoading('hide');
                    break;
            }
        }
        function OnError(error) {
            $("#modal-content").html(error);
            $("#modalAlert").modal();
            $("input[id*=txtspass]").val("");
            //$("#btnLogin").html("Login");
            //$("#btnLogin").removeClass("disabled");
            $("input[id*=txtsid]").prop("disabled", "");
            $("input[id*=txtspass]").prop("disabled", "");
            $("body").mLoading('hide');
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:TextBox ID="txtCheckFinger" runat="server" Style="display: none;" AutoPostBack="true"
        OnTextChanged="txtCheckFinger_TextChanged" />
    <asp:TextBox ID="txtUserFinger" runat="server" Style="display: none;" />
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true"></asp:ScriptManager>
</asp:Content>
