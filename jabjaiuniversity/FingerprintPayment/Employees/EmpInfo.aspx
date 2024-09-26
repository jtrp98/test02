<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpInfo.aspx.cs" Inherits="FingerprintPayment.Employees.EmpInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:MultiView ID="MvContent" runat="server">
        <asp:View ID="ListContent" runat="server">
            List Content
        </asp:View>
        <asp:View ID="FormContent" runat="server">
            <div class="empInfoForm">
                <form id="empInfoForm" class="form-padding" style="padding: 0px;">
                    <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101029") %></p>
                    <div class="row div-row-padding" style="margin-top: 14px;">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltEmpType"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102004") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltEmpType" name="sltEmpType" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102004") %>">
                                <asp:Literal ID="ltrEmpType" runat="server"></asp:Literal>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptCode"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401083") %> : <p style="height: 0px; margin: -2px 10px 2px 0px; font-size: 0.9em;">(Username)</p></label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptCode" name="iptCode"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401083") %>" maxlength="20" />
                        </div>
                        <div class="col-md-1 mb-1" style="height: 40px; padding: 0px;">
                            <a href="#" data-toggle="tooltip" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102028") %>" class="text-danger" style="margin: 0px 0px 0px -10px; position: absolute; top: 50%; -ms-transform: translateY(-50%); transform: translateY(-50%); font-size: 20px;">
                                <i class="fa fa-question-circle" aria-hidden="true"></i>
                            </a>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltJob"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102133") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltJob" name="sltJob" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102012") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102012") %></option>
                                <asp:Literal ID="ltrJob" runat="server"></asp:Literal>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltDepartment"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102013") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltDepartment" name="sltDepartment" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102014") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102014") %></option>
                                <asp:Literal ID="ltrDepartment" runat="server"></asp:Literal>
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="Gender"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101061") %> :</label></div>
                        <div class="col-md-3 checkbox-radios">
                                <div class="form-check form-check-inline">
                                    <label class="form-check-label" for="rdoGender1">
                                        <input class="form-check-input" type="radio" name="Gender" id="rdoGender1" value="0" checked />
                                        <span class="label-text" style="display: contents;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %></span>
                                        <span class="circle">
                                            <span class="check"></span>
                                        </span>
                                    </label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <label class="form-check-label" for="rdoGender2">
                                        <input class="form-check-input" type="radio" name="Gender" id="rdoGender2" value="1" />
                                        <span class="label-text" style="display: contents;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %></span>
                                        <span class="circle">
                                            <span class="check"></span>
                                        </span>
                                    </label>
                                </div>
                            </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltTitle"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102015") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltTitle" name="sltTitle" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102016") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102016") %></option>
                                <asp:Literal ID="ltrTitle" runat="server"></asp:Literal>
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptFirstName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptFirstName" name="iptFirstName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %>" maxlength="256" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptLastName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptLastName" name="iptLastName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %>" maxlength="256" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptFirstNameEn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101068") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptFirstNameEn" name="iptFirstNameEn"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101068") %>" maxlength="256" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptLastNameEn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101070") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptLastNameEn" name="iptLastNameEn"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101070") %>" maxlength="256" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptIDCardNumber"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101284") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptIDCardNumber" name="iptIDCardNumber"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101284") %>" maxlength="13" />
                        </div>
                        <div class="col-md-3 mb-3"></div>
                        <div class="col-md-3 mb-3"></div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <%-- Employee Info --%>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="iptPassportNumber"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101083") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptPassportNumber"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101083") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptPassportCountry"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101084") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptPassportCountry"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101084") %>" maxlength="100" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptPassportExpirationDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101085") %> :</label></div>
                        <div class="col-md-3 mb-3">
                            <div class="form-group div-datepicker">
                                <input id="iptPassportExpirationDate" name="iptPassportExpirationDate" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3"></div>
                        <div class="col-md-3 mb-3"></div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptVisaNo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102017") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptVisaNo"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102017") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptVisaExpirationDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102018") %> :</label></div>
                        <div class="col-md-3 mb-3">
                            <div class="form-group div-datepicker">
                                <input id="iptVisaExpirationDate" name="iptVisaExpirationDate" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptWorkPermitNo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102019") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptWorkPermitNo"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102019") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptWorkPermitExpirationDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102020") %> :</label></div>
                        <div class="col-md-3 mb-3">
                            <div class="form-group div-datepicker">
                                <input id="iptWorkPermitExpirationDate" name="iptWorkPermitExpirationDate" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <%------%>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptBirthday"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %> :</label></div>
                        <div class="col-md-3 mb-3">
                            <div class="form-group div-datepicker">
                                <input id="iptBirthday" name="iptBirthday" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltBloodType"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101217") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltBloodType" name="sltBloodType" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101218") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101218") %></option>
                                <option value="A">A</option>
                                <option value="B">B</option>
                                <option value="AB">AB</option>
                                <option value="O">O</option>
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltNationality"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <%--<input type="text" class="form-control" id="inpNationality"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %>" maxlength="50">--%>
                            <select id="sltNationality" name="sltNationality" data-live-search="true" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101080") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101080") %></option>
                                <asp:Literal ID="ltrNationality" runat="server" />
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltEthnicity"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <%--<input type="text" class="form-control" id="inpEthnicity"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %>" maxlength="50">--%>
                            <select id="sltEthnicity" name="sltEthnicity" data-live-search="true" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101078") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101078") %></option>
                                <asp:Literal ID="ltrEthnicity" runat="server" />
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltReligion"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <%--<input type="text" class="form-control" id="inpReligion"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %>" maxlength="50">--%>
                            <select id="sltReligion" name="sltReligion" data-live-search="true" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101082") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101082") %></option>
                                <asp:Literal ID="ltrReligion" runat="server" />
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltPersonalStatus"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102022") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltPersonalStatus" name="sltPersonalStatus" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102021") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102021") %></option>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102038") %></option>
                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121008") %></option>
                                <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102039") %></option>
                                <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102040") %></option>
                                <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102041") %></option>
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="inpSpouseFirstName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102023") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="inpSpouseFirstName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102023") %>" maxlength="30" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="inpSpouseLastName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102024") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="inpSpouseLastName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102024") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptPhone"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102025") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptPhone" name="iptPhone"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102025") %>" maxlength="20" />
                            <span class="text-success d-none phone-verify-message"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121015") %></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="inpEmail"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="inpEmail" name="inpEmail"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %>" maxlength="256" />
                            <span class="text-success d-none email-verify-message"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02327") %></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltTimeType"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105033") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltTimeType" name="sltTimeType" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102027") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102027") %></option>
                                <asp:Literal ID="ltrTimeType" runat="server"></asp:Literal>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label for="iptUseBiometric"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101116") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3">
                            <input id="iptUseBiometric" type="checkbox" checked data-on="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101117") %>" data-off="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %>" data-toggle="toggle" data-onstyle="success" data-height="40" data-width="100" data-style="toggle-switch" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2">
                        </div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label for="btnDelFinger">
                                <asp:Literal ID="ltrFinger" runat="server"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111043") %> :</asp:Literal>
                            </label>
                        </div>
                        <div class="col-md-3 mb-3" style="padding-top: 15px;">
                            <asp:Literal ID="ltrPassword" runat="server"></asp:Literal>
                            <asp:Literal ID="ltrDelFinger" runat="server">
                            <button id="btnDelFinger" onclick="ShowFingerPopup(); return false;"
                                class="btn btn-primary" data-toggle="modal"
                                data-target="#modalFingerPopup" style="margin-top: -8px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111044") %></button></asp:Literal>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>

                    <div class="row text-center" style="margin-bottom: 7px;">
                        <button id="save1" type="submit" class="btn btn-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                        <button type="button"
                            class="btn btn-danger btn-cancel" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </form>
                <form id="empInfoAddressForm" class="form-padding" style="padding: 0px;">
                    <%--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101148") %>--%>
                    <p class="bg-primary" style="margin-top: 10px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101148") %></p>
                    <div class="row div-row-padding" style="margin-top: 14px;">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptNo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptNo" name="iptNo"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptVillageNo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102031") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptVillageNo" name="iptVillageNo"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102031") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptVillage"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102030") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptVillage" name="iptVillage"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102030") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptAlley"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptAlley" name="iptAlley"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptBuilding"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102032") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptBuilding" name="iptBuilding"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102032") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptRoad"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptRoad" name="iptRoad"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltProvince"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltProvince" name="sltProvince" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %></option>
                                <asp:Literal ID="ltrProvince" runat="server"></asp:Literal>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltAmphur"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltAmphur" name="sltAmphur" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %>">
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltDistrict"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltDistrict" name="sltDistrict" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %>">
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptPostalCode"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptPostalCode" name="iptPostalCode"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %>" maxlength="10" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>

                    <div class="row text-center" style="margin-bottom: 7px;">
                        <button id="save2" type="submit" class="btn btn-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                        <button type="button"
                            class="btn btn-danger btn-cancel" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </form>
                <form id="empAddressForm" class="form-padding" style="padding: 0px;">
                    <%--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102029") %>--%>
                    <p class="bg-primary" style="margin-top: 10px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102029") %></p>
                    <div class="row div-row-padding" style="margin-top: 14px;">
                        <div class="col-md-2 mb-2 text-right">
                        </div>
                        <div class="col-md-3 mb-3 div-check-input">
                            <div class="form-check form-check-inline">
                                <label class="form-check-label" style="font-weight: bold;">
                                    <input id="UseHouseAddress" name="UseHouseAddress" class="form-check-input" type="checkbox" />
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101148") %>
                                    <span class="form-check-sign">
                                        <span class="check"></span>
                                    </span>
                                </label>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 text-right">
                        </div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptNo2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptNo2" name="iptNo2"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %>" maxlength="10" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptVillageNo2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102031") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptVillageNo2" name="iptVillageNo2"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102031") %>" maxlength="10" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptVillage2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102030") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptVillage2" name="iptVillage2"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102030") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptAlley2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptAlley2" name="iptAlley2"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %>" maxlength="40" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptBuilding2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102032") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptBuilding2" name="iptBuilding2"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102032") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptRoad2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptRoad2" name="iptRoad2"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %>" maxlength="40" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltProvince2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltProvince2" name="sltProvince2" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %></option>
                                <asp:Literal ID="ltrProvince2" runat="server"></asp:Literal>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltAmphur2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltAmphur2" name="sltAmphur2" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %>">
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltDistrict2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltDistrict2" name="sltDistrict2" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %>">
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptPostalCode2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptPostalCode2" name="iptPostalCode2"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %>" maxlength="10" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>

                    <div class="row text-center" style="margin-bottom: 7px;">
                        <button id="save3" type="submit" class="btn btn-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                        <button type="button"
                            class="btn btn-danger btn-cancel" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </form>
                <div id="modalFingerPopup" class="modal fade alertBoxInfo" tabindex="-1" role="dialog"
                    aria-hidden="true">
                    <div class="modal-dialog maclist-modal" style="top: 50px;">
                        <div class="modal-content">
                            <div class="modal-header">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111044") %>
                            </div>
                            <div class="modal-body" id="modalpopupdata-content">
                            </div>
                            <div class="modal-footer" style="display: block; text-align: center;">
                                <label class="btn btn-primary" onclick="changeFinger();" style="width: 122px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111046") %></label>
                                <button id="modalClose" type="button" class="btn btn-danger" data-dismiss="modal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script type="text/javascript">

                var empInfoForm = {
                    GetItem: function (empID) {
                        $.ajax({
                            type: "POST",
                            url: "EmpInfo.aspx/GetItem",
                            data: '{empID: ' + empID + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empInfoForm.OnSuccessGet,
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    OnSuccessGet: function (response) {
                        if (response.d == "error") {

                            $("#modalNotifyOnlyClose").find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>');
                            $("#modalNotifyOnlyClose").find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111025") %>');
                            $("#modalNotifyOnlyClose").modal('show');

                        } else if (response.d == "new") {

                            $('#employeeTabs li.nav-item:gt(0)').addClass("disabled");
                            $('#employeeTabs li.nav-item:gt(0)').find('a').removeAttr('data-toggle');

                        } else {
                            var xmlDoc = $.parseXML(response.d);
                            var xml = $(xmlDoc);
                            var infors = xml.find("Table1");

                            $.each(infors, function (index) {
                                var infor = $(this);

                                if (index == 0) {
                                    $("#sltEmpType").selectpicker('val', $(this).find("F1").text());
                                    $("#iptCode").val($(this).find("F2").text()); 
                                    $("#sltJob").selectpicker('val', $(this).find("F3").text());
                                    $("#sltDepartment").selectpicker('val', $(this).find("F4").text());
                                    $('input:radio[name=Gender][value=' + $(this).find("F5").text() + ']').click();
                                    $("#sltTitle").selectpicker('val', $(this).find("F6").text());
                                    $("#iptFirstName").val($(this).find("F7").text());
                                    $("#iptLastName").val($(this).find("F8").text());
                                    $("#iptFirstNameEn").val($(this).find("F9").text()); 
                                    $("#iptLastNameEn").val($(this).find("F10").text()); 
                                    $("#iptIDCardNumber").val($(this).find("F11").text());
                                    $("#iptPassportNumber").val($(this).find("F12").text()); 
                                    $("#iptPassportCountry").val($(this).find("F13").text()); 
                                    $("#iptPassportExpirationDate").val($(this).find("F49").text()); //+
                                    $("#iptVisaNo").val($(this).find("F50").text()); //+
                                    $("#iptVisaExpirationDate").val($(this).find("F51").text()); //+
                                    $("#iptWorkPermitNo").val($(this).find("F52").text()); //+
                                    $("#iptWorkPermitExpirationDate").val($(this).find("F53").text()); //+

                                    $("#iptBirthday").val($(this).find("F14").text());
                                    $("#sltBloodType").selectpicker('val', $(this).find("F15").text()); 
                                    $('#sltNationality').selectpicker('val', $(this).find("F16").text());
                                    $('#sltEthnicity').selectpicker('val', $(this).find("F17").text());
                                    $('#sltReligion').selectpicker('val', $(this).find("F18").text());
                                    $("#sltPersonalStatus").selectpicker('val', $(this).find("F19").text()); 
                                    $("#inpSpouseFirstName").val($(this).find("F20").text()); 
                                    $("#inpSpouseLastName").val($(this).find("F21").text()); 
                                    $("#iptPhone").val($(this).find("F22").text());
                                    $("#inpEmail").val($(this).find("F23").text());
                                    $("#sltTimeType").selectpicker('val', $(this).find("F24").text());

                                    if (!$.isBlank($(this).find("F25").text())) {
                                        let divProfileImage = $(this).find("F25").text();

                                        if ($(this).find("F25").text().indexOf("?x-image-process=image/resize,m_fill,h_300,w_270") != -1) {
                                            divProfileImage += '&v=' + (new Date()).getTime() * 1e4
                                        } else {
                                            divProfileImage += '?v=' + (new Date()).getTime() * 1e4
                                        }

                                        $(".empProfile #divProfileImage .img-photo").attr("src", divProfileImage);
                                        $(".empProfile #divProfileImage .img-photo-original").attr("src", $(this).find("F48").text());

                                        $(".empProfile #divProfileImage .div-picture-remove").show();
                                        //$(".empProfile #divProfileImage .div-crop").show();
                                    }

                                    $('#iptUseBiometric').bootstrapToggle((/true/i).test($(this).find("F47").text()) ? 'on' : 'off');

                                    $("#iptNo").val($(this).find("F26").text());
                                    $("#iptVillageNo").val($(this).find("F27").text());
                                    $("#iptVillage").val($(this).find("F28").text());
                                    $("#iptAlley").val($(this).find("F29").text());
                                    $("#iptBuilding").val($(this).find("F30").text());
                                    $("#iptRoad").val($(this).find("F31").text());
                                    $("#sltProvince").selectpicker('val', $(this).find("F32").text());
                                    LoadDistrict('#sltAmphur', $("#sltProvince").val());
                                    $("#sltAmphur").selectpicker('val', $(this).find("F33").text());
                                    LoadSubDistrict('#sltDistrict', $("#sltAmphur").val());
                                    $("#sltDistrict").selectpicker('val', $(this).find("F34").text());
                                    $("#iptPostalCode").val($(this).find("F35").text());

                                    $('input[name=UseHouseAddress]').prop("checked", ($(this).find("F36").text() == "1"));
                                    $("#iptNo2").val($(this).find("F37").text());
                                    $("#iptVillageNo2").val($(this).find("F38").text());
                                    $("#iptVillage2").val($(this).find("F39").text());
                                    $("#iptAlley2").val($(this).find("F40").text());
                                    $("#iptBuilding2").val($(this).find("F41").text());
                                    $("#iptRoad2").val($(this).find("F42").text());
                                    $("#sltProvince2").selectpicker('val', $(this).find("F43").text());
                                    LoadDistrict('#sltAmphur2', $("#sltProvince2").val());
                                    $("#sltAmphur2").selectpicker('val', $(this).find("F44").text());
                                    LoadSubDistrict('#sltDistrict2', $("#sltAmphur2").val());
                                    $("#sltDistrict2").selectpicker('val', $(this).find("F45").text());
                                    $("#iptPostalCode2").val($(this).find("F46").text());
                                }
                                else {
                                    // Disable verify field
                                    if (!!$(this).find("F23").text()) {
                                        $('#inpEmail').prop('disabled', true);
                                        $('.email-verify-message').removeClass('d-none');
                                    }
                                    if (!!$(this).find("F22").text()) {
                                        $('#iptPhone').prop('disabled', true);
                                        $('.phone-verify-message').removeClass('d-none');
                                    }
                                }
                            });

                        }
                    },
                    SaveItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "EmpInfo.aspx/SaveItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: empInfoForm.OnSuccessSave,
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (xhr, status, error) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>[' + xhr + ']');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    OnSuccessSave: function (response) {
                        var title = "";
                        var body = "";

                        var flag = response.d.split('-');
                        switch (flag[0]) {
                            case "complete":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';

                                empInfoForm.UploadFile(flag[1]);

                                $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {

                                    // Redirect to employee list : if page name = EmployeeNew.aspx
                                    if ('<%=Request.QueryString["eid"]%>' == '0') {
                                        window.location.replace("EmployeeDetail.aspx?v=view&eid=" + flag[1]);
                                    }

                                });

                                break;
                            case "warning":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121014") %> [{0}]'.format(flag[1]);

                                break;
                            case "error":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111027") %> [{0}]'.format(flag[1]);

                                break;
                            default: break;

                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);
                        $("#modalNotifyOnlyClose").modal('show');
                    },
                    ClearSession: function (callbackRedirect) {
                        $.ajax({
                            type: "POST",
                            url: "EmpInfo.aspx/ClearSessionID",
                            data: '{}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                callbackRedirect();
                            },
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    UploadFile: function (empID) {

                        var formData = new FormData();
                        var objFiles = $('.empProfile #divProfileImage input[type="file"]').get(0);
                        var files = objFiles.files;
                        for (var i = 0; i < files.length; i++) {
                            formData.append('_pr_' + i, files[i]);
                        }

                        var coordinates = $('#divProfileImage img.img-cropped').data('coordinates');
                        var originalSize = $('#divProfileImage img.img-cropped').data('original-size');
                        var crop = { coordinates: coordinates, originalSize: originalSize, base64: $('#divProfileImage img.img-cropped').attr('src') };
                        formData.append('_crop_', JSON.stringify(crop));

                        $.ajax({
                            url: '/Employees/Ashx/UploadImageProfileHandler.ashx?empID=' + empID,
                            data: formData,
                            dataType: 'json',
                            type: 'POST',
                            contentType: false,
                            processData: false,
                            success: function (result) {
                                //do some tasks after upload
                                console.log(result);
                            },
                            error: function (response) {
                                console.log(response);
                            }
                        });

                    }
                }

                function changeFinger() {
                    $.ajax("/App_Logic/deleteDataJSON.ashx?mode=delfinger&userid=<%=Request.QueryString["eid"]%>&type=1", function (Result) {
                    }).done(function (result) {
                        $("#modalpopupdata-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111048") %> " + result);
                        $("#modalFingerPopup .modal-footer").addClass("hidden");
                        $('#btnDelFinger').addClass("disabled");
                    });

                    return false;
                }

                function ShowFingerPopup() {
                    var name = $("#iptFirstName").val() + " " + $("#iptLastName").val();
                    $("#modalpopupdata-content").html("ท่านต้องการลบลายนี้มือของ " + name + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603024") %> <br/>เมื่อท่านทำการลบลายนี้วมือแล้วจะไม่สามารถทำรายการได้ ");
                    $("#modalFingerPopup .modal-footer").removeClass("hidden")
                }

                $(document).ready(function () {

                    // Validate rule for empInfoForm
                    $("#empInfoForm").validate({
                        rules: {
                            iptCode: {
                                required: true,
                                code: true
                            },
                            sltTitle: "required",
                            iptFirstName: "required",
                            iptLastName: "required",
                            iptBirthday: {
                                required: true,
                                thaiDate: true
                            },
                            iptPhone: {
                                required: true,
                                number: true
                            },
                            iptIDCardNumber: {
                                required: false,
                                maxlength: 13,
                                minlength: 13,
                            },
                            inpEmail: {
                                required: false,
                                email2: true
                            },
                            iptPassportExpirationDate: {
                                required: false,
                                thaiDate: true
                            },
                            iptVisaExpirationDate: {
                                required: false,
                                thaiDate: true
                            },
                            iptWorkPermitExpirationDate: {
                                required: false,
                                thaiDate: true
                            }
                        },
                        messages: {
                            iptCode: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            sltTitle: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptFirstName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptLastName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptBirthday: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            iptPhone: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                                number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                            },
                            iptIDCardNumber: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                                maxlength: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111041") %>",
                                minlength: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111041") %>",
                            },
                            inpEmail: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            iptPassportExpirationDate: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            iptVisaExpirationDate: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            iptWorkPermitExpirationDate: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            }
                        },
                        focusInvalid: false,
                        invalidHandler: function () {
                            $(this).find(":input.error:first").focus();
                        },
                        errorPlacement: function (error, element) {
                            switch (element.attr("name")) {
                                case "iptCode":
                                case "iptFirstName":
                                case "iptLastName":
                                case "iptBirthday": 
                                case "iptPhone": error.insertAfter(element); break;
                                case "sltTitle": error.insertAfter(element.parent()); break;
                                default: error.insertAfter(element); break;
                            }
                        }
                    });
                    // Validate rule for empInfoAddressForm
                    $("#empInfoAddressForm").validate({
                        rules: {
                            iptNo: "required",
                            sltProvince: "required",
                            sltAmphur: "required",
                            sltDistrict: "required",
                            iptPostalCode: "required"
                        },
                        messages: {
                            iptNo: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            sltProvince: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            sltAmphur: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            sltDistrict: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptPostalCode: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                        },
                        focusInvalid: false,
                        invalidHandler: function () {
                            $(this).find(":input.error:first").focus();
                        },
                        errorPlacement: function (error, element) {
                            switch (element.attr("name")) {
                                case "iptNo":
                                case "iptPostalCode": error.insertAfter(element); break;
                                case "sltProvince":
                                case "sltAmphur":
                                case "sltDistrict": error.insertAfter(element.parent()); break;
                                default: error.insertAfter(element); break;
                            }
                        }
                    });
                    // Validate rule for empAddressForm
                    $("#empAddressForm").validate({
                        rules: {
                            iptNo2: "required",
                            sltProvince2: "required",
                            sltAmphur2: "required",
                            sltDistrict2: "required",
                            iptPostalCode2: "required"
                        },
                        messages: {
                            iptNo2: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            sltProvince2: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            sltAmphur2: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            sltDistrict2: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptPostalCode2: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                        },
                        focusInvalid: false,
                        invalidHandler: function () {
                            $(this).find(":input.error:first").focus();
                        },
                        errorPlacement: function (error, element) {
                            switch (element.attr("name")) {
                                case "iptNo2":
                                case "iptPostalCode2": error.insertAfter(element); break;
                                case "sltProvince2":
                                case "sltAmphur2":
                                case "sltDistrict2": error.insertAfter(element.parent()); break;
                                default: error.insertAfter(element); break;
                            }
                        }
                    });


                    $(".empInfoForm #save1").bind({
                        click: function () {

                            if ($('#empInfoForm').valid()) {

                                $('#modalNotifyConfirmSave').modal('show');

                                // Modal Section
                                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                                    $('#modalNotifyConfirmSave').modal('hide');

                                    $("#modalWaitDialog").modal('show');

                                    // Save command
                                    var data = new Array();
                                    data[0] = "1-" + xEmpKey.eid; // Section 1 + EmpID

                                    data[1] = $("#sltEmpType").val();
                                    data[2] = $("#iptCode").val();
                                    data[3] = $("#sltJob").val();
                                    data[4] = $("#sltDepartment").val();
                                    data[5] = $("input[name='Gender']:checked").val();
                                    data[6] = $("#sltTitle").val();
                                    data[7] = $("#iptFirstName").val();
                                    data[8] = $("#iptLastName").val();
                                    data[9] = $("#iptFirstNameEn").val();
                                    data[10] = $("#iptLastNameEn").val();
                                    data[11] = $("#iptIDCardNumber").val();
                                    data[12] = $("#iptPassportNumber").val();
                                    data[13] = $("#iptPassportCountry").val();
                                    data[49] = $("#iptPassportExpirationDate").val(); //+
                                    data[50] = $("#iptVisaNo").val(); //+
                                    data[51] = $("#iptVisaExpirationDate").val(); //+
                                    data[52] = $("#iptWorkPermitNo").val(); //+
                                    data[53] = $("#iptWorkPermitExpirationDate").val(); //+

                                    data[14] = $("#iptBirthday").val();
                                    data[15] = $("#sltBloodType").val();
                                    data[16] = $("#sltNationality").val();
                                    data[17] = $("#sltEthnicity").val();
                                    data[18] = $("#sltReligion").val();
                                    data[19] = $("#sltPersonalStatus").val();
                                    data[20] = $("#inpSpouseFirstName").val();
                                    data[21] = $("#inpSpouseLastName").val();
                                    data[22] = $("#iptPhone").val();
                                    data[23] = $("#inpEmail").val();
                                    data[24] = $("#sltTimeType").val();

                                    data[25] = $("#divProfileImage").attr("data-remove-old-picture"); //$("#divProfileImage").attr("data-filename");

                                    data[47] = $('#iptUseBiometric').prop('checked');

                                    empInfoForm.SaveItem(data);

                                });
                            }

                            return false;
                        }
                    });

                    $(".empInfoForm #save2").bind({
                        click: function () {

                            if ($('#empInfoAddressForm').valid()) {

                                $('#modalNotifyConfirmSave').modal('show');

                                // Modal Section
                                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                                    $('#modalNotifyConfirmSave').modal('hide');

                                    $("#modalWaitDialog").modal('show');

                                    // Save command
                                    var data = new Array();
                                    data[0] = "2-" + xEmpKey.eid; // Section 2 + EmpID

                                    data[26] = $("#iptNo").val();
                                    data[27] = $("#iptVillageNo").val();
                                    data[28] = $("#iptVillage").val();
                                    data[29] = $("#iptAlley").val();
                                    data[30] = $("#iptBuilding").val();
                                    data[31] = $("#iptRoad").val();
                                    data[32] = $("#sltProvince").val();
                                    data[33] = $("#sltAmphur").val();
                                    data[34] = $("#sltDistrict").val();
                                    data[35] = $("#iptPostalCode").val();

                                    empInfoForm.SaveItem(data);

                                });
                            }

                            return false;
                        }
                    });

                    $(".empInfoForm #save3").bind({
                        click: function () {

                            if ($('#empAddressForm').valid()) {

                                $('#modalNotifyConfirmSave').modal('show');

                                // Modal Section
                                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                                    $('#modalNotifyConfirmSave').modal('hide');

                                    $("#modalWaitDialog").modal('show');

                                    // Save command
                                    var data = new Array();
                                    data[0] = "3-" + xEmpKey.eid; // Section 3 + EmpID

                                    data[36] = $('input[name=UseHouseAddress]').prop("checked") ? "1" : "0";
                                    data[37] = $("#iptNo2").val();
                                    data[38] = $("#iptVillageNo2").val();
                                    data[39] = $("#iptVillage2").val();
                                    data[40] = $("#iptAlley2").val();
                                    data[41] = $("#iptBuilding2").val();
                                    data[42] = $("#iptRoad2").val();
                                    data[43] = $("#sltProvince2").val();
                                    data[44] = $("#sltAmphur2").val();
                                    data[45] = $("#sltDistrict2").val();
                                    data[46] = $("#iptPostalCode2").val();

                                    empInfoForm.SaveItem(data);

                                });
                            }

                            return false;
                        }
                    });

                    $(".empInfoForm .btn-cancel").bind({
                        click: function () {

                            // Redirect to employee list
                            empInfoForm.ClearSession(function () {
                                window.location.replace("EmployeeList.aspx");
                            });

                            return false;
                        }
                    });

                    // Modal Section
                    $('#modalNotifyConfirmSave').off().on('show.bs.modal', function (e) {
                        $(this).find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101166") %>');
                        $(this).find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101167") %>');
                    });

                    $("#divProfileImage .img-photo-original").on("load", function () {
                        var image = $(this).get(0);
                        if (image.height >= 1280 && image.width >= 720) {
                            $(".empProfile #divProfileImage .div-crop").show();
                        }
                    });

                    // Initial data

                    //$('#divBirthday').datetimepicker({
                    //    format: 'DD/MM/YYYY-BE',
                    //    locale: 'th'
                    //});
                    $('.empInfoForm .datepicker').datetimepicker({
                        keepOpen: false,
                        debug: false,
                        format: 'DD/MM/YYYY-BE',
                        locale: 'th',
                        icons: {
                            time: "fa fa-clock-o",
                            date: "fa fa-calendar",
                            up: "fa fa-chevron-up",
                            down: "fa fa-chevron-down",
                            previous: 'fa fa-chevron-left',
                            next: 'fa fa-chevron-right',
                            today: 'fa fa-screenshot',
                            clear: 'fa fa-trash',
                            close: 'fa fa-remove'
                        }
                    });

                    $(".empInfoForm .datepicker").attr('maxlength', '10');

                    activateBootstrapSelect('.empInfoForm .selectpicker');

                    // Address section
                    // Initial data

                    $("#sltProvince").change(function () {

                        LoadDistrict('#sltAmphur', $("#sltProvince").val());

                    });

                    $("#sltAmphur").change(function () {

                        LoadSubDistrict('#sltDistrict', $("#sltAmphur").val());

                    });

                    $("#sltProvince2").change(function () {

                        LoadDistrict('#sltAmphur2', $("#sltProvince2").val());

                    });

                    $("#sltAmphur2").change(function () {

                        LoadSubDistrict('#sltDistrict2', $("#sltAmphur2").val());

                    });

                    $('#UseHouseAddress').change(function () {
                        if ($(this).prop('checked')) {
                            $("#iptNo2").val($("#iptNo").val());
                            $("#iptVillageNo2").val($("#iptVillageNo").val());
                            $("#iptVillage2").val($("#iptVillage").val());
                            $("#iptAlley2").val($("#iptAlley").val());
                            $("#iptBuilding2").val($("#iptBuilding").val());
                            $("#iptRoad2").val($("#iptRoad").val());
                            $("#sltProvince2").selectpicker('val', $("#sltProvince").val());
                            var $options1 = $("#sltAmphur > option").clone();
                            $('#sltAmphur2').empty().append($options1);
                            $("#sltAmphur2").selectpicker('val', $("#sltAmphur").val());
                            var $options2 = $("#sltDistrict > option").clone();
                            $('#sltDistrict2').empty().append($options2);
                            $("#sltDistrict2").selectpicker('val', $("#sltDistrict").val());
                            $("#iptPostalCode2").val($("#iptPostalCode").val());
                        }
                    })

                    $('#iptUseBiometric').bootstrapToggle();

                    // Load info command
                    empInfoForm.GetItem(<%=Request.QueryString["eid"]%>);

                    if ('<%=Request.QueryString["eid"]%>' == '0') {
                        $('.empInfoForm #save2, .empInfoForm #save3').addClass("disabled");
                    }

                    $('[data-toggle="tooltip"]').tooltip();


                });

            </script>
        </asp:View>
        <asp:View ID="ViewContent" runat="server">
            View Content
        </asp:View>
    </asp:MultiView>
</body>
</html>
