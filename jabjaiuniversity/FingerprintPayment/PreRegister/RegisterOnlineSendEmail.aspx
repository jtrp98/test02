<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="RegisterOnlineSendEmail.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterOnlineSendEmail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <style>
        .register-online-send-mail .row-input {
            border: 1px solid #ccc;
            border-radius: 3px;
            display: flex;
        }

            .register-online-send-mail .row-input .col-form-label {
                border: 2px solid #87ddeb;
                border-radius: 3px;
                margin: -1px;
                padding-bottom: 5px;
                width: 120px;
            }

        .register-online-send-mail .div-text-span {
            padding-left: 15px;
            width: 90%;
        }

        .register-online-send-mail #iptSenderLabel {
            width: 100%;
            border: none;
        }

        .register-online-send-mail #ttaMessage {
            width: inherit;
            border: 1px solid #ccc;
            border-radius: 3px;
            overflow-y: auto;
            font-size: 16px;
            line-height: 1.5 !important;
        }

        .register-online-send-mail #btnSend {
            width: 122px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="/styles/material-form.css?v=<%=DateTime.Now.Ticks%>" />

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101001") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103001") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103033") %>
            </p>
        </div>
    </div>

    <div class="material-form row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header card-header-info card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">mail_outline</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103033") %></h4>
                </div>
                <div class="card-body" style="padding-left: 7%; padding-right: 3%; padding-bottom: 3%;">
                    <div class="toolbar">
                        <!-- Here you can write extra buttons/actions for the toolbar -->
                    </div>
                    <form class="form-padding register-online-send-mail">
                        <div class="row div-row-padding" style="">
                            <div class="col-md-12 text-right">
                                <asp:Literal ID="ltrMailDate" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="row div-row-padding" style="margin-bottom: 5px;">
                            <div class="col-md-12">
                                <div class="row-input">
                                    <div class="col-form-label text-center">
                                        <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %></label>
                                    </div>
                                    <div class="div-text-span">
                                        <span class="span-data">
                                            <asp:Literal ID="ltrTo" runat="server"></asp:Literal>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row div-row-padding" style="margin-bottom: 5px;">
                            <div class="col-md-12">
                                <div class="row-input">
                                    <div class="col-form-label text-center">
                                        <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103146") %></label>
                                    </div>
                                    <div class="div-text-span">
                                        <span class="span-data">
                                            <input id="iptSenderLabel" name="iptSenderLabel" type="text" value="" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103148") %>" />
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row div-row-padding" style="margin-bottom: 5px;">
                            <div class="col-md-12">
                                <div class="row-input">
                                    <div class="col-form-label text-center">
                                        <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105069") %></label>
                                    </div>
                                    <div class="div-text-span">
                                        <span class="span-data">
                                            <asp:Literal ID="ltrTitle" runat="server"></asp:Literal>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row div-row-padding" style="margin-bottom: 5px;">
                            <div class="col-md-12">
                                <textarea id="ttaMessage" name="w3review" rows="15" cols="50" <%--onkeydown="keydownTabToIndent(this)"--%>>
                                    <asp:Literal ID="ltrMessage" runat="server"></asp:Literal>
                                </textarea>
                            </div>
                        </div>
                        <div class="row reply-input">
                            <div class="col-md-12 text-right">
                                <button id="btnCancel" type="submit" class="btn btn-danger" style="font-size: .95rem;"><i class="material-icons">close</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                                <button id="btnSend" type="submit" class="btn btn-success" style="font-size: .95rem;" <%=DISABLEDSENDEMAIL ? "disabled" : "" %>><i class="material-icons">send</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02041") %>&nbsp;&nbsp;&nbsp;</button>
                            </div>
                        </div>
                    </form>
                </div>
                <!-- end content-->
            </div>
            <!--  end card  -->
        </div>
        <!-- end col-md-12 -->
    </div>
    <!-- end row -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script type='text/javascript'>

        function keydownTabToIndent(obj) {
            if (event.keyCode === 9) {
                var v = obj.value, s = obj.selectionStart, e = obj.selectionEnd;
                obj.value = v.substring(0, s) + '\t' + v.substring(e);
                obj.selectionStart = obj.selectionEnd = s + 1;
                return false;
            }
        }

        function OnError(xhr, errorType, exception) {
            var responseText;
            try {
                responseText = jQuery.parseJSON(xhr.responseText);
                var errorMessage = "[" + errorType + ", " + exception + "] Exception:" + responseText.ExceptionType + ", StackTrace:" + responseText.StackTrace + ", Message:" + responseText.Message;

                Swal.fire({
                    title: 'Error!',
                    text: 'Error Message - ' + errorMessage,
                    type: 'error',
                    confirmButtonClass: "btn btn-danger",
                    buttonsStyling: false
                });
            } catch (e) {
                responseText = xhr.responseText;
                Swal.fire({
                    title: 'Error!',
                    text: 'Error Message - ' + responseText,
                    type: 'error',
                    confirmButtonClass: "btn btn-danger",
                    buttonsStyling: false
                });
            }
        }

        $(document).ready(function () {

            $("#btnCancel").bind({
                click: function () {

                    window.location.replace("/preRegister/preregisterlist2.aspx");

                    return false;
                }
            });

            $("#btnSend").bind({
                click: function () {

                    $.ajax({
                        async: false,
                        type: "POST",
                        url: 'RegisterOnlineSendEmail.aspx/SaveMail',
                        data: JSON.stringify({ registerId: '<%=Request.QueryString["id"]%>', mailFrom: $("#iptSenderLabel").val(), mailMessage: $("#ttaMessage").val() }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            var r = JSON.parse(result.d);
                            if (r.success) {
                                Swal.fire({
                                    title: 'Done!',
                                    text: r.message,
                                    type: 'success',
                                    confirmButtonClass: "btn btn-success",
                                    showConfirmButton: true,
                                    buttonsStyling: false
                                }).then(result => {
                                    if (result.value) {
                                        // handle Confirm button click
                                        // result.value will contain `true` or the input value
                                    } else {
                                        // handle dismissals
                                        // result.dismiss can be 'cancel', 'overlay', 'esc' or 'timer'
                                    }
                                    window.location.replace("/preRegister/preregisterlist2.aspx");
                                });
                            }
                            else {
                                Swal.fire({
                                    title: 'Warning!',
                                    text: 'Warning Message - ' + r.message,
                                    type: 'warning',
                                    confirmButtonClass: "btn btn-warning",
                                    buttonsStyling: false
                                });
                            }
                        },
                        error: OnError
                    });

                    return false;
                }
            });

        });

    </script>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
