<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StdFamily.aspx.cs" Inherits="FingerprintPayment.StudentInfo.StdFamily" %>

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
            <div class="stdFamilyForm">
                <form id="stdFatherForm" class="form-padding">
                    <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101180") %></p>
                    <div class="row div-row-padding" style="margin-top: 30px;">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltFatherTitle"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltFatherTitle" name="sltFatherTitle" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101150") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101150") %></option>
                                <asp:Literal ID="ltrFatherTitle" runat="server"></asp:Literal>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label></label></div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptFatherName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101065") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptFatherName" name="iptFatherName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101065") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptFatherLastName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101066") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptFatherLastName" name="iptFatherLastName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101066") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptFatherNameEn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101067") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptFatherNameEn" name="iptFatherNameEn"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101067") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptFatherNameLastEn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101069") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptFatherNameLastEn" name="iptFatherNameLastEn"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101069") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptFatherBirthday"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %> :</label></div>
                        <div class="col-md-3 mb-3">
                            <div class="form-group div-datepicker">
                                <input id="iptFatherBirthday" name="iptFatherBirthday" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptFatherIdentification"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptFatherIdentification" name="iptFatherIdentification"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %>" maxlength="13" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltFatherRace"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltFatherRace" name="sltFatherRace" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101078") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101078") %></option>
                                <asp:Literal ID="ltrFatherRace" runat="server" />
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltFatherNation"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltFatherNation" name="sltFatherNation" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101080") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101080") %></option>
                                <asp:Literal ID="ltrFatherNation" runat="server" />
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltFatherReligion"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltFatherReligion" name="sltFatherReligion" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101082") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101082") %></option>
                                <asp:Literal ID="ltrFatherReligion" runat="server" />
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltFatherGraduated"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltFatherGraduated" name="sltFatherGraduated" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101177") %>">
                                <option selected="selected" value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101177") %></option>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00745") %></option>
                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102055") %></option>
                                <option value="7"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01315") %></option>
                                <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102056") %></option>
                                <option value="9"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103101") %></option>
                                <option value="10"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103102") %></option>
                                <option value="8"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103103") %></option>
                                <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103104") %></option>
                                <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102060") %></option>
                                <option value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102061") %></option>
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltCopyAddressDataFrom"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101181") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltCopyAddressDataFrom" name="sltCopyAddressDataFrom" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111030") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101182") %></option>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101183") %></option>
                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101184") %></option>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label></label></div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptFatherHomeNo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptFatherHomeNo" name="iptFatherHomeNo"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptFatherSoi"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptFatherSoi" name="iptFatherSoi"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptFatherMoo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptFatherMoo" name="iptFatherMoo"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptFatherRoad"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptFatherRoad" name="iptFatherRoad"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltFatherProvince"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltFatherProvince" name="sltFatherProvince" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %></option>
                                <asp:Literal ID="ltrFatherProvince" runat="server"></asp:Literal>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltFatherAmphoe"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltFatherAmphoe" name="sltFatherAmphoe" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %></option>
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltFatherTombon"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltFatherTombon" name="sltFatherTombon" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %></option>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptFatherPostalCode"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptFatherPostalCode" name="iptFatherPostalCode"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %>" maxlength="10" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptFatherJob"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptFatherJob" name="iptFatherJob"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %>" maxlength="100" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptFatherIncome"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptFatherIncome" name="iptFatherIncome"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %>" maxlength="10" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptFatherWorkPlace"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptFatherWorkPlace" name="iptFatherWorkPlace"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %>" maxlength="200" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptFatherPhone"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101188") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptFatherPhone" name="iptFatherPhone"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101188") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptFatherPhone2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101189") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptFatherPhone2" name="iptFatherPhone2"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101189") %>" maxlength="20" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptFatherPhone3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101190") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptFatherPhone3" name="iptFatherPhone3"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101190") %>" maxlength="20" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                    </div>
                    <div class="row text-center">
                        <button id="saveFather" type="submit" class="btn btn-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                        <button id="saveFatherAndNext" type="submit" class="btn btn-info save-next"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101126") %></button>
                        <button type="button"
                            class="btn btn-danger btn-cancel" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </form>
                <form id="stdMotherForm" class="form-padding">
                    <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00194") %></p>
                    <div class="row div-row-padding" style="margin-top: 30px;">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltMotherTitle"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltMotherTitle" name="sltMotherTitle" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101150") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101150") %></option>
                                <asp:Literal ID="ltrMotherTitle" runat="server"></asp:Literal>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label></label></div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptMotherName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101065") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptMotherName" name="iptMotherName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101065") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptMotherLastName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101066") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptMotherLastName" name="iptMotherLastName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101066") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptMotherNameEn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101067") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptMotherNameEn" name="iptMotherNameEn"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101067") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptMotherNameLastEn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101069") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptMotherNameLastEn" name="iptMotherNameLastEn"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101069") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptMotherBirthday"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %> :</label></div>
                        <div class="col-md-3 mb-3">
                            <div class="form-group div-datepicker">
                                <input id="iptMotherBirthday" name="iptMotherBirthday" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptMotherIdentification"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptMotherIdentification" name="iptMotherIdentification"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %>" maxlength="13" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltMotherRace"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltMotherRace" name="sltMotherRace" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101078") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101078") %></option>
                                <asp:Literal ID="ltrMotherRace" runat="server" />
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltMotherNation"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltMotherNation" name="sltMotherNation" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101080") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101080") %></option>
                                <asp:Literal ID="ltrMotherNation" runat="server" />
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltMotherReligion"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltMotherReligion" name="sltMotherReligion" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101082") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101082") %></option>
                                <asp:Literal ID="ltrMotherReligion" runat="server" />
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltMotherGraduated"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltMotherGraduated" name="sltMotherGraduated" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101177") %>">
                                <option selected="selected" value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101177") %></option>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00745") %></option>
                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102055") %></option>
                                <option value="7"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01315") %></option>
                                <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102056") %></option>
                                <option value="9"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103101") %></option>
                                <option value="10"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103102") %></option>
                                <option value="8"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103103") %></option>
                                <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103104") %></option>
                                <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102060") %></option>
                                <option value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102061") %></option>
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltCopyAddressDataFrom2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101181") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltCopyAddressDataFrom2" name="sltCopyAddressDataFrom2" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111030") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101182") %></option>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101183") %></option>
                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101184") %></option>
                                <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111029") %></option>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 text-right">
                        </div>
                        <div class="col-md-3 mb-3 text-left">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptMotherHomeNo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptMotherHomeNo" name="iptMotherHomeNo"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptMotherSoi"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptMotherSoi" name="iptMotherSoi"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptMotherMoo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptMotherMoo" name="iptMotherMoo"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptMotherRoad"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptMotherRoad" name="iptMotherRoad"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltMotherProvince"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltMotherProvince" name="sltMotherProvince" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %></option>
                                <asp:Literal ID="ltrMotherProvince" runat="server"></asp:Literal>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltMotherAmphoe"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltMotherAmphoe" name="sltMotherAmphoe" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %></option>
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltMotherTombon"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltMotherTombon" name="sltMotherTombon" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %></option>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptMotherPostalCode"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptMotherPostalCode" name="iptMotherPostalCode"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %>" maxlength="10" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptMotherJob"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptMotherJob" name="iptMotherJob"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %>" maxlength="100" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptMotherIncome"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptMotherIncome" name="iptMotherIncome"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %>" maxlength="10" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptMotherWorkPlace"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptMotherWorkPlace" name="iptMotherWorkPlace"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %>" maxlength="200" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptMotherPhone"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101188") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptMotherPhone" name="iptMotherPhone"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101188") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptMotherPhone2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101189") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptMotherPhone2" name="iptMotherPhone2"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101189") %>" maxlength="20" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptMotherPhone3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101190") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptMotherPhone3" name="iptMotherPhone3"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101190") %>" maxlength="20" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                    </div>
                    <div class="row text-center">
                        <button id="saveMother" type="submit" class="btn btn-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                        <button id="saveMotherAndNext" type="submit" class="btn btn-info save-next"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101126") %></button>
                        <button type="button"
                            class="btn btn-danger btn-cancel" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </form>
                <form id="stdParentForm" class="form-padding">
                    <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101179") %></p>
                    <div class="row div-row-padding" style="margin-top: 30px;">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltCopyDataFrom"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101191") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltCopyDataFrom" name="sltCopyDataFrom" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101182") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101182") %></option>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101180") %></option>
                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00194") %></option>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label></label></div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltParentTitle"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltParentTitle" name="sltParentTitle" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101150") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101150") %></option>
                                <asp:Literal ID="ltrParentTitle" runat="server"></asp:Literal>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label></label></div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptParentName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101065") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptParentName" name="iptParentName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101065") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptParentLastName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101066") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptParentLastName" name="iptParentLastName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101066") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptParentNameEn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101067") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptParentNameEn" name="iptParentNameEn"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101067") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptParentNameLastEn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101069") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptParentNameLastEn" name="iptParentNameLastEn"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101069") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptParentBirthday"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %> :</label></div>
                        <div class="col-md-3 mb-3">
                            <div class="form-group div-datepicker">
                                <input id="iptParentBirthday" name="iptParentBirthday" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptParentIdentification"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptParentIdentification" name="iptParentIdentification"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %>" maxlength="13" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltParentRace"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltParentRace" name="sltParentRace" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101078") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101078") %></option>
                                <asp:Literal ID="ltrParentRace" runat="server" />
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltParentNation"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltParentNation" name="sltParentNation" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101080") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101080") %></option>
                                <asp:Literal ID="ltrParentNation" runat="server" />
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltParentReligion"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltParentReligion" name="sltParentReligion" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101082") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101082") %></option>
                                <asp:Literal ID="ltrParentReligion" runat="server" />
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltParentGraduated"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltParentGraduated" name="sltParentGraduated" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101177") %>">
                                <option selected="selected" value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101177") %></option>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00745") %></option>
                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102055") %></option>
                                <option value="7"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01315") %></option>
                                <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102056") %></option>
                                <option value="9"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103101") %></option>
                                <option value="10"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103102") %></option>
                                <option value="8"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103103") %></option>
                                <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103104") %></option>
                                <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102060") %></option>
                                <option value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102061") %></option>
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptParentHomeNo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptParentHomeNo" name="iptParentHomeNo"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptParentSoi"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptParentSoi" name="iptParentSoi"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptParentMoo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptParentMoo" name="iptParentMoo"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptParentRoad"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptParentRoad" name="iptParentRoad"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltParentProvince"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <select id="sltParentProvince" name="sltParentProvince" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %></option>
                                <asp:Literal ID="ltrParentProvince" runat="server"></asp:Literal>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltParentAmphoe"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <select id="sltParentAmphoe" name="sltParentAmphoe" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %></option>
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltParentTombon"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <select id="sltParentTombon" name="sltParentTombon" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %></option>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptParentPostalCode"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptParentPostalCode" name="iptParentPostalCode"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %>" maxlength="10" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltParentRelate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101192") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltParentRelate" name="sltParentRelate" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101193") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101193") %></option>
                                <option value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %></option>
                                <option value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101195") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101195") %></option>
                                <option value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101196") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101196") %></option>
                                <option value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></option>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltParentRequestStudyMoney"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101197") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltParentRequestStudyMoney" name="sltParentRequestStudyMoney" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101198") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101198") %></option>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101199") %></option>
                                <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101200") %></option>
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltParentStatus"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101201") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltParentStatus" name="sltParentStatus" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101202") %>">
                                <option selected="selected" value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101202") %></option>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101203") %></option>
                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101204") %></option>
                                <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101205") %></option>
                                <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101206") %></option>
                                <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103105") %></option>
                                <option value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101207") %></option>
                                <option value="7"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101208") %></option>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label></label></div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptParentJob"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptParentJob" name="iptParentJob"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %>" maxlength="100" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptParentIncome"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptParentIncome" name="iptParentIncome"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %>" maxlength="10" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptParentWorkPlace"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptParentWorkPlace" name="iptParentWorkPlace"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %>" maxlength="200" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptParentPhone"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101188") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptParentPhone" name="iptParentPhone"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101188") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptParentPhone2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101189") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptParentPhone2" name="iptParentPhone2"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101189") %>" maxlength="20" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptParentPhone3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101190") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptParentPhone3" name="iptParentPhone3"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101190") %>" maxlength="20" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                    </div>
                    <div class="row text-center">
                        <button id="saveParent" type="submit" class="btn btn-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                        <button id="saveParentAndNext" type="submit" class="btn btn-info save-next"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101126") %></button>
                        <button type="button"
                            class="btn btn-danger btn-cancel" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </form>
            </div>
            <script type="text/javascript">

                var stdFamilyForm = {
                    GetFatherItem: function (stdID) {
                        $.ajax({
                            type: "POST",
                            url: "StdFamily.aspx/GetFatherItem",
                            data: '{stdID: ' + stdID + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdFamilyForm.OnSuccessGetFather,
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
                    OnSuccessGetFather: function (response) {
                        if (response.d == "error") {

                            $("#modalNotifyOnlyClose").find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>');
                            $("#modalNotifyOnlyClose").find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111025") %>');
                            $("#modalNotifyOnlyClose").modal('show');

                        } else if (response.d == "new") {
                            xStdKey.fmlid = "0";
                        } else {
                            var xmlDoc = $.parseXML(response.d);
                            var xml = $(xmlDoc);
                            var infors = xml.find("Table1");

                            $.each(infors, function (index) {
                                var infor = $(this);

                                xStdKey.fmlid = $(this).find("F0").text();
                                $("#sltFatherTitle").selectpicker('val', $(this).find("F1").text());
                                $("#iptFatherName").val($(this).find("F2").text());
                                $("#iptFatherLastName").val($(this).find("F3").text());
                                $("#iptFatherNameEn").val($(this).find("F4").text());
                                $("#iptFatherNameLastEn").val($(this).find("F5").text());
                                $("#iptFatherBirthday").val($(this).find("F6").text());
                                $("#iptFatherIdentification").val($(this).find("F7").text());
                                //$("#iptFatherRace").val($(this).find("F8").text());
                                $("#sltFatherRace").selectpicker('val', $(this).find("F8").text());
                                //$("#iptFatherNation").val($(this).find("F9").text());
                                $("#sltFatherNation").selectpicker('val', $(this).find("F9").text());
                                //$("#iptFatherReligion").val($(this).find("F10").text());
                                $("#sltFatherReligion").selectpicker('val', $(this).find("F10").text());
                                $("#sltFatherGraduated").selectpicker('val', $(this).find("F11").text());
                                $("#iptFatherHomeNo").val($(this).find("F12").text());
                                $("#iptFatherSoi").val($(this).find("F13").text());
                                $("#iptFatherMoo").val($(this).find("F14").text());
                                $("#iptFatherRoad").val($(this).find("F15").text());
                                $("#sltFatherProvince").selectpicker('val', $(this).find("F16").text());
                                LoadDistrict('#sltFatherAmphoe', $("#sltFatherProvince").val());

                                $("#sltFatherAmphoe").selectpicker('val', $(this).find("F17").text());
                                LoadSubDistrict('#sltFatherTombon', $("#sltFatherAmphoe").val());

                                $("#sltFatherTombon").selectpicker('val', $(this).find("F18").text());
                                $("#iptFatherPostalCode").val($(this).find("F19").text());
                                $("#iptFatherJob").val($(this).find("F20").text());
                                $("#iptFatherIncome").val($(this).find("F21").text());
                                $("#iptFatherWorkPlace").val($(this).find("F22").text());
                                $("#iptFatherPhone").val($(this).find("F23").text());
                                $("#iptFatherPhone2").val($(this).find("F24").text());
                                $("#iptFatherPhone3").val($(this).find("F25").text());

                            });

                        }
                    },
                    GetMotherItem: function (stdID) {
                        $.ajax({
                            type: "POST",
                            url: "StdFamily.aspx/GetMotherItem",
                            data: '{stdID: ' + stdID + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdFamilyForm.OnSuccessGetMother,
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
                    OnSuccessGetMother: function (response) {
                        if (response.d == "error") {

                            $("#modalNotifyOnlyClose").find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>');
                            $("#modalNotifyOnlyClose").find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111025") %>');
                            $("#modalNotifyOnlyClose").modal('show');

                        } else if (response.d == "new") {
                            xStdKey.fmlid = "0";
                        } else {
                            var xmlDoc = $.parseXML(response.d);
                            var xml = $(xmlDoc);
                            var infors = xml.find("Table1");

                            $.each(infors, function (index) {
                                var infor = $(this);

                                xStdKey.fmlid = $(this).find("F0").text();
                                $("#sltMotherTitle").selectpicker('val', $(this).find("F1").text());
                                $("#iptMotherName").val($(this).find("F2").text());
                                $("#iptMotherLastName").val($(this).find("F3").text());
                                $("#iptMotherNameEn").val($(this).find("F4").text());
                                $("#iptMotherNameLastEn").val($(this).find("F5").text());
                                $("#iptMotherBirthday").val($(this).find("F6").text());
                                $("#iptMotherIdentification").val($(this).find("F7").text());
                                //$("#iptMotherRace").val($(this).find("F8").text());
                                $("#sltMotherRace").selectpicker('val', $(this).find("F8").text());
                                //$("#iptMotherNation").val($(this).find("F9").text());
                                $("#sltMotherNation").selectpicker('val', $(this).find("F9").text());
                                //$("#iptMotherReligion").val($(this).find("F10").text());
                                $("#sltMotherReligion").selectpicker('val', $(this).find("F10").text());
                                $("#sltMotherGraduated").selectpicker('val', $(this).find("F11").text());
                                $("#iptMotherHomeNo").val($(this).find("F12").text());
                                $("#iptMotherSoi").val($(this).find("F13").text());
                                $("#iptMotherMoo").val($(this).find("F14").text());
                                $("#iptMotherRoad").val($(this).find("F15").text());
                                $("#sltMotherProvince").selectpicker('val', $(this).find("F16").text());
                                LoadDistrict('#sltMotherAmphoe', $("#sltMotherProvince").val());

                                $("#sltMotherAmphoe").selectpicker('val', $(this).find("F17").text());
                                LoadSubDistrict('#sltMotherTombon', $("#sltMotherAmphoe").val());

                                $("#sltMotherTombon").selectpicker('val', $(this).find("F18").text());
                                $("#iptMotherPostalCode").val($(this).find("F19").text());
                                $("#iptMotherJob").val($(this).find("F20").text());
                                $("#iptMotherIncome").val($(this).find("F21").text());
                                $("#iptMotherWorkPlace").val($(this).find("F22").text());
                                $("#iptMotherPhone").val($(this).find("F23").text());
                                $("#iptMotherPhone2").val($(this).find("F24").text());
                                $("#iptMotherPhone3").val($(this).find("F25").text());

                            });

                        }
                    },
                    GetParentItem: function (stdID) {
                        $.ajax({
                            type: "POST",
                            url: "StdFamily.aspx/GetParentItem",
                            data: '{stdID: ' + stdID + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdFamilyForm.OnSuccessGetParent,
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
                    OnSuccessGetParent: function (response) {
                        if (response.d == "error") {

                            $("#modalNotifyOnlyClose").find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>');
                            $("#modalNotifyOnlyClose").find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111025") %>');
                            $("#modalNotifyOnlyClose").modal('show');

                        } else if (response.d == "new") {
                            xStdKey.fmlid = "0";
                        } else {
                            var xmlDoc = $.parseXML(response.d);
                            var xml = $(xmlDoc);
                            var infors = xml.find("Table1");

                            $.each(infors, function (index) {
                                var infor = $(this);

                                xStdKey.fmlid = $(this).find("F0").text();
                                $("#sltParentTitle").selectpicker('val', $(this).find("F1").text());
                                $("#iptParentName").val($(this).find("F2").text());
                                $("#iptParentLastName").val($(this).find("F3").text());
                                $("#iptParentNameEn").val($(this).find("F4").text());
                                $("#iptParentNameLastEn").val($(this).find("F5").text());
                                $("#iptParentBirthday").val($(this).find("F6").text());
                                $("#iptParentIdentification").val($(this).find("F7").text());
                                //$("#iptParentRace").val($(this).find("F8").text());
                                $("#sltParentRace").selectpicker('val', $(this).find("F8").text());
                                //$("#iptParentNation").val($(this).find("F9").text());
                                $("#sltParentNation").selectpicker('val', $(this).find("F9").text());
                                //$("#iptParentReligion").val($(this).find("F10").text());
                                $("#sltParentReligion").selectpicker('val', $(this).find("F10").text());
                                $("#sltParentGraduated").selectpicker('val', $(this).find("F11").text());
                                $("#iptParentHomeNo").val($(this).find("F12").text());
                                $("#iptParentSoi").val($(this).find("F13").text());
                                $("#iptParentMoo").val($(this).find("F14").text());
                                $("#iptParentRoad").val($(this).find("F15").text());
                                $("#sltParentProvince").selectpicker('val', $(this).find("F16").text());
                                LoadDistrict('#sltParentAmphoe', $("#sltParentProvince").val());

                                $("#sltParentAmphoe").selectpicker('val', $(this).find("F17").text());
                                LoadSubDistrict('#sltParentTombon', $("#sltParentAmphoe").val());

                                $("#sltParentTombon").selectpicker('val', $(this).find("F18").text());
                                $("#iptParentPostalCode").val($(this).find("F19").text());

                                $("#sltParentRelate").selectpicker('val', $(this).find("F20").text());
                                $("#sltParentRequestStudyMoney").selectpicker('val', $(this).find("F21").text());
                                $("#sltParentStatus").selectpicker('val', $(this).find("F22").text());

                                $("#iptParentJob").val($(this).find("F23").text());
                                $("#iptParentIncome").val($(this).find("F24").text());
                                $("#iptParentWorkPlace").val($(this).find("F25").text());
                                $("#iptParentPhone").val($(this).find("F26").text());
                                $("#iptParentPhone2").val($(this).find("F27").text());
                                $("#iptParentPhone3").val($(this).find("F28").text());

                            });

                        }
                    },
                    SaveFatherItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "StdFamily.aspx/SaveFatherItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdFamilyForm.OnSuccessSaveFather,
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    OnSuccessSaveFather: function (response) {
                        var title = "";
                        var body = "";

                        var r = JSON.parse(response.d);
                        if (r.success) {
                            title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                            body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';

                            xStdKey.fmlid = r.fmlID;

                            $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {

                            });
                        }
                        else {
                            title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                            body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133343") %> [' + r.message + ']';
                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);

                        $('#modalNotifyOnlyClose').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                        $("#modalNotifyOnlyClose").modal('show');
                    },
                    SaveAndNextFatherItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "StdFamily.aspx/SaveFatherItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdFamilyForm.OnSuccessSaveAndNextFather,
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    OnSuccessSaveAndNextFather: function (response) {
                        var title = "";
                        var body = "";

                        var r = JSON.parse(response.d);
                        if (r.success) {
                            title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                            body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';

                            xStdKey.fmlid = r.fmlID;

                            $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {
                                // Redirect to student list
                                window.location.replace("StudentDetail.aspx?v=form&sid=" + StudentDetail_NextSID + "&tid=" + xStdKey.tid);
                            });
                        }
                        else {
                            title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                            body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133343") %> [' + r.message + ']';
                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);

                        $('#modalNotifyOnlyClose').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                        $("#modalNotifyOnlyClose").modal('show');
                    },
                    SaveMotherItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "StdFamily.aspx/SaveMotherItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdFamilyForm.OnSuccessSaveMother,
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    OnSuccessSaveMother: function (response) {
                        var title = "";
                        var body = "";

                        var r = JSON.parse(response.d);
                        if (r.success) {
                            title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                            body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';

                            xStdKey.fmlid = r.fmlID;

                            $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {

                            });
                        }
                        else {
                            title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                            body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133343") %> [' + r.message + ']';
                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);

                        $('#modalNotifyOnlyClose').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                        $("#modalNotifyOnlyClose").modal('show');
                    },
                    SaveAndNextMotherItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "StdFamily.aspx/SaveMotherItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdFamilyForm.OnSuccessSaveAndNextMother,
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    OnSuccessSaveAndNextMother: function (response) {
                        var title = "";
                        var body = "";

                        var r = JSON.parse(response.d);
                        if (r.success) {
                            title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                            body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';

                            xStdKey.fmlid = r.fmlID;

                            $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {
                                // Redirect to student list
                                window.location.replace("StudentDetail.aspx?v=form&sid=" + StudentDetail_NextSID + "&tid=" + xStdKey.tid);
                            });
                        }
                        else {
                            title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                            body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133343") %> [' + r.message + ']';
                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);

                        $('#modalNotifyOnlyClose').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                        $("#modalNotifyOnlyClose").modal('show');
                    },
                    SaveParentItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "StdFamily.aspx/SaveParentItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdFamilyForm.OnSuccessSaveParent,
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    OnSuccessSaveParent: function (response) {
                        var title = "";
                        var body = "";

                        var r = JSON.parse(response.d);
                        if (r.success) {
                            title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                            body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';

                            xStdKey.fmlid = r.fmlID;

                            $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {

                            });
                        }
                        else {
                            title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                            body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133343") %> [' + r.message + ']';
                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);

                        $('#modalNotifyOnlyClose').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                        $("#modalNotifyOnlyClose").modal('show');
                    },
                    SaveAndNextParentItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "StdFamily.aspx/SaveParentItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdFamilyForm.OnSuccessSaveAndNextParent,
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    OnSuccessSaveAndNextParent: function (response) {
                        var title = "";
                        var body = "";

                        var r = JSON.parse(response.d);
                        if (r.success) {
                            title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                            body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';

                            xStdKey.fmlid = r.fmlID;

                            $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {
                                // Redirect to student list
                                window.location.replace("StudentDetail.aspx?v=form&sid=" + StudentDetail_NextSID + "&tid=" + xStdKey.tid);
                            });
                        }
                        else {
                            title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                            body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133343") %> [' + r.message + ']';
                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);

                        $('#modalNotifyOnlyClose').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                        $("#modalNotifyOnlyClose").modal('show');
                    },
                    ClearSession: function (callbackRedirect) {
                        $.ajax({
                            type: "POST",
                            url: "StdFamily.aspx/ClearSessionID",
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
                    }
                }

                $(document).ready(function () {

                    $("#stdFatherForm").validate({
                        rules: {
                            /*sltFatherTitle: "required",*/
                            iptFatherName: "required",
                            iptFatherLastName: "required",
                            iptFatherPhone: "required",
                            iptFatherBirthday: {
                                required: false,
                                thaiDate: true
                            },
                            iptFatherIncome: {
                                required: false,
                                number: true
                            }
                        },
                        messages: {
                            /*sltFatherTitle: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",*/
                            iptFatherName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptFatherLastName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptFatherPhone: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptFatherBirthday: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            iptFatherIncome: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                                number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                            }
                        },
                        focusInvalid: false,
                        invalidHandler: function () {
                            $(this).find(":input.error:first").focus();
                        },
                        errorPlacement: function (error, element) {
                            switch (element.attr("name")) {
                                case "iptFatherName":
                                case "iptFatherLastName": 
                                case "iptFatherPhone": error.insertAfter(element); break;
                                default: error.insertAfter(element); break;
                            }
                        }
                    });

                    $("#stdMotherForm").validate({
                        rules: {
                            /*sltMotherTitle: "required",*/
                            iptMotherName: "required",
                            iptMotherLastName: "required",
                            iptMotherPhone: "required",
                            iptMotherBirthday: {
                                required: false,
                                thaiDate: true
                            },
                            iptMotherIncome: {
                                required: false,
                                number: true
                            }
                        },
                        messages: {
                            /*sltMotherTitle: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",*/
                            iptMotherName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptMotherLastName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptMotherPhone: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptMotherBirthday: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            iptMotherIncome: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                                number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                            }
                        },
                        focusInvalid: false,
                        invalidHandler: function () {
                            $(this).find(":input.error:first").focus();
                        },
                        errorPlacement: function (error, element) {
                            switch (element.attr("name")) {
                                case "iptMotherName":
                                case "iptMotherLastName":
                                case "iptMotherPhone": error.insertAfter(element); break;
                                default: error.insertAfter(element); break;
                            }
                        }
                    });

                    $("#stdParentForm").validate({
                        rules: {
                            /*sltParentTitle: "required",*/
                            iptParentName: "required",
                            iptParentLastName: "required",
                            sltParentRelate: "required",
                            sltParentStatus: "required",
                            iptParentPhone: "required",
                            iptParentBirthday: {
                                required: false,
                                thaiDate: true
                            },
                            iptParentIncome: {
                                required: false,
                                number: true
                            }
                        },
                        messages: {
                            /*sltParentTitle: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",*/
                            iptParentName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptParentLastName: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            sltParentRelate: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            sltParentStatus: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptParentPhone: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptParentBirthday: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            iptParentIncome: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                                number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                            }
                        },
                        focusInvalid: false,
                        invalidHandler: function () {
                            $(this).find(":input.error:first").focus();
                        },
                        errorPlacement: function (error, element) {
                            switch (element.attr("name")) {
                                case "iptParentName":
                                case "iptParentLastName":
                                case "iptParentPhone": error.insertAfter(element); break;
                                case "sltParentRelate":
                                case "sltParentStatus": error.insertAfter(element.parent()); break;
                                default: error.insertAfter(element); break;
                            }
                        }
                    });

                    function GetFatherDataFromInput() {

                        var fatherData = new Array();

                        fatherData[0] = xStdKey.sid + "-" + xStdKey.fmlid;
                        fatherData[1] = $("#sltFatherTitle").val();
                        fatherData[2] = $("#iptFatherName").val();
                        fatherData[3] = $("#iptFatherLastName").val();
                        fatherData[4] = $("#iptFatherNameEn").val();
                        fatherData[5] = $("#iptFatherNameLastEn").val();
                        fatherData[6] = $("#iptFatherBirthday").val();
                        fatherData[7] = $("#iptFatherIdentification").val();
                        //fatherData[8] = $("#iptFatherRace").val();
                        fatherData[8] = $("#sltFatherRace").val();
                        //fatherData[9] = $("#iptFatherNation").val();
                        fatherData[9] = $("#sltFatherNation").val();
                        //fatherData[10] = $("#iptFatherReligion").val();
                        fatherData[10] = $("#sltFatherReligion").val();
                        fatherData[11] = $("#sltFatherGraduated").val();
                        fatherData[12] = $("#iptFatherHomeNo").val();
                        fatherData[13] = $("#iptFatherSoi").val();
                        fatherData[14] = $("#iptFatherMoo").val();
                        fatherData[15] = $("#iptFatherRoad").val();
                        fatherData[16] = $("#sltFatherProvince").val();
                        fatherData[17] = $("#sltFatherAmphoe").val();
                        fatherData[18] = $("#sltFatherTombon").val();
                        fatherData[19] = $("#iptFatherPostalCode").val();
                        fatherData[20] = $("#iptFatherJob").val();
                        fatherData[21] = $("#iptFatherIncome").val();
                        fatherData[22] = $("#iptFatherWorkPlace").val();
                        fatherData[23] = $("#iptFatherPhone").val();
                        fatherData[24] = $("#iptFatherPhone2").val();
                        fatherData[25] = $("#iptFatherPhone3").val();

                        return fatherData;
                    }

                    $(".stdFamilyForm #saveFather").bind({
                        click: function () {

                            if ($('#stdFatherForm').valid()) {

                                $('#modalNotifyConfirmSave').css('z-index', parseInt($('#modalShowForm').css('z-index')) + 1);
                                $('#modalNotifyConfirmSave').modal('show');

                                // Modal Section
                                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                                    $('#modalNotifyConfirmSave').modal('hide');

                                    $('#modalWaitDialog').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                                    $("#modalWaitDialog").modal('show');

                                    // Save command
                                    var data = GetFatherDataFromInput();

                                    stdFamilyForm.SaveFatherItem(data);

                                });
                            }

                            return false;
                        }
                    });

                    $(".stdFamilyForm #saveFatherAndNext").bind({
                        click: function () {

                            if ($('#stdFatherForm').valid()) {

                                $('#modalNotifyConfirmSave').css('z-index', parseInt($('#modalShowForm').css('z-index')) + 1);
                                $('#modalNotifyConfirmSave').modal('show');

                                // Modal Section
                                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                                    $('#modalNotifyConfirmSave').modal('hide');

                                    $('#modalWaitDialog').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                                    $("#modalWaitDialog").modal('show');

                                    // Save command
                                    var data = GetFatherDataFromInput();

                                    stdFamilyForm.SaveAndNextFatherItem(data);

                                });
                            }

                            return false;
                        }
                    });

                    function GetMotherDataFromInput() {

                        var motherData = new Array();

                        motherData[0] = xStdKey.sid + "-" + xStdKey.fmlid;
                        motherData[1] = $("#sltMotherTitle").val();
                        motherData[2] = $("#iptMotherName").val();
                        motherData[3] = $("#iptMotherLastName").val();
                        motherData[4] = $("#iptMotherNameEn").val();
                        motherData[5] = $("#iptMotherNameLastEn").val();
                        motherData[6] = $("#iptMotherBirthday").val();
                        motherData[7] = $("#iptMotherIdentification").val();
                        //motherData[8] = $("#iptMotherRace").val();
                        motherData[8] = $("#sltMotherRace").val();
                        //motherData[9] = $("#iptMotherNation").val();
                        motherData[9] = $("#sltMotherNation").val();
                        //motherData[10] = $("#iptMotherReligion").val();
                        motherData[10] = $("#sltMotherReligion").val();
                        motherData[11] = $("#sltMotherGraduated").val();
                        motherData[12] = $("#iptMotherHomeNo").val();
                        motherData[13] = $("#iptMotherSoi").val();
                        motherData[14] = $("#iptMotherMoo").val();
                        motherData[15] = $("#iptMotherRoad").val();
                        motherData[16] = $("#sltMotherProvince").val();
                        motherData[17] = $("#sltMotherAmphoe").val();
                        motherData[18] = $("#sltMotherTombon").val();
                        motherData[19] = $("#iptMotherPostalCode").val();
                        motherData[20] = $("#iptMotherJob").val();
                        motherData[21] = $("#iptMotherIncome").val();
                        motherData[22] = $("#iptMotherWorkPlace").val();
                        motherData[23] = $("#iptMotherPhone").val();
                        motherData[24] = $("#iptMotherPhone2").val();
                        motherData[25] = $("#iptMotherPhone3").val();

                        return motherData;
                    }

                    $(".stdFamilyForm #saveMother").bind({
                        click: function () {

                            if ($('#stdMotherForm').valid()) {

                                $('#modalNotifyConfirmSave').css('z-index', parseInt($('#modalShowForm').css('z-index')) + 1);
                                $('#modalNotifyConfirmSave').modal('show');

                                // Modal Section
                                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                                    $('#modalNotifyConfirmSave').modal('hide');

                                    $('#modalWaitDialog').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                                    $("#modalWaitDialog").modal('show');

                                    // Save command
                                    var data = GetMotherDataFromInput();

                                    stdFamilyForm.SaveMotherItem(data);

                                });
                            }

                            return false;
                        }
                    });

                    $(".stdFamilyForm #saveMotherAndNext").bind({
                        click: function () {

                            if ($('#stdMotherForm').valid()) {

                                $('#modalNotifyConfirmSave').css('z-index', parseInt($('#modalShowForm').css('z-index')) + 1);
                                $('#modalNotifyConfirmSave').modal('show');

                                // Modal Section
                                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                                    $('#modalNotifyConfirmSave').modal('hide');

                                    $('#modalWaitDialog').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                                    $("#modalWaitDialog").modal('show');

                                    // Save command
                                    var data = GetMotherDataFromInput();

                                    stdFamilyForm.SaveAndNextMotherItem(data);

                                });
                            }

                            return false;
                        }
                    });

                    function GetParentDataFromInput() {

                        var parentData = new Array();

                        parentData[0] = xStdKey.sid + "-" + xStdKey.fmlid;
                        parentData[1] = $("#sltParentTitle").val();
                        parentData[2] = $("#iptParentName").val();
                        parentData[3] = $("#iptParentLastName").val();
                        parentData[4] = $("#iptParentNameEn").val();
                        parentData[5] = $("#iptParentNameLastEn").val();
                        parentData[6] = $("#iptParentBirthday").val();
                        parentData[7] = $("#iptParentIdentification").val();
                        //parentData[8] = $("#iptParentRace").val();
                        parentData[8] = $("#sltParentRace").val();
                        //parentData[9] = $("#iptParentNation").val();
                        parentData[9] = $("#sltParentNation").val();
                        //parentData[10] = $("#iptParentReligion").val();
                        parentData[10] = $("#sltParentReligion").val();
                        parentData[11] = $("#sltParentGraduated").val();
                        parentData[12] = $("#iptParentHomeNo").val();
                        parentData[13] = $("#iptParentSoi").val();
                        parentData[14] = $("#iptParentMoo").val();
                        parentData[15] = $("#iptParentRoad").val();
                        parentData[16] = $("#sltParentProvince").val();
                        parentData[17] = $("#sltParentAmphoe").val();
                        parentData[18] = $("#sltParentTombon").val();
                        parentData[19] = $("#iptParentPostalCode").val();

                        parentData[20] = $("#sltParentRelate").val();
                        parentData[21] = $("#sltParentRequestStudyMoney").val();
                        parentData[22] = $("#sltParentStatus").val();

                        parentData[23] = $("#iptParentJob").val();
                        parentData[24] = $("#iptParentIncome").val();
                        parentData[25] = $("#iptParentWorkPlace").val();
                        parentData[26] = $("#iptParentPhone").val();
                        parentData[27] = $("#iptParentPhone2").val();
                        parentData[28] = $("#iptParentPhone3").val();

                        return parentData;
                    }

                    $(".stdFamilyForm #saveParent").bind({
                        click: function () {

                            if ($('#stdParentForm').valid()) {

                                $('#modalNotifyConfirmSave').css('z-index', parseInt($('#modalShowForm').css('z-index')) + 1);
                                $('#modalNotifyConfirmSave').modal('show');

                                // Modal Section
                                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                                    $('#modalNotifyConfirmSave').modal('hide');

                                    $('#modalWaitDialog').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                                    $("#modalWaitDialog").modal('show');

                                    // Save command
                                    var data = GetParentDataFromInput();

                                    stdFamilyForm.SaveParentItem(data);

                                });
                            }

                            return false;
                        }
                    });

                    $(".stdFamilyForm #saveParentAndNext").bind({
                        click: function () {

                            if ($('#stdParentForm').valid()) {

                                $('#modalNotifyConfirmSave').css('z-index', parseInt($('#modalShowForm').css('z-index')) + 1);
                                $('#modalNotifyConfirmSave').modal('show');

                                // Modal Section
                                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                                    $('#modalNotifyConfirmSave').modal('hide');

                                    $('#modalWaitDialog').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                                    $("#modalWaitDialog").modal('show');

                                    // Save command
                                    var data = GetParentDataFromInput();

                                    stdFamilyForm.SaveAndNextParentItem(data);

                                });
                            }

                            return false;
                        }
                    });

                    $(".stdFamilyForm .btn-cancel").bind({
                        click: function () {

                            // Redirect to employee list
                            stdFamilyForm.ClearSession(function () {
                                window.location.replace("StudentList.aspx");
                            });

                            return false;
                        }
                    });

                    $("#sltFatherProvince").change(function () {

                        LoadDistrict('#sltFatherAmphoe', $("#sltFatherProvince").val());

                    });
                    $("#sltFatherAmphoe").change(function () {

                        LoadSubDistrict('#sltFatherTombon', $("#sltFatherAmphoe").val());

                    });

                    $("#sltMotherProvince").change(function () {

                        LoadDistrict('#sltMotherAmphoe', $("#sltMotherProvince").val());

                    });
                    $("#sltMotherAmphoe").change(function () {

                        LoadSubDistrict('#sltMotherTombon', $("#sltMotherAmphoe").val());

                    });

                    $("#sltParentProvince").change(function () {

                        LoadDistrict('#sltParentAmphoe', $("#sltParentProvince").val());

                    });
                    $("#sltParentAmphoe").change(function () {

                        LoadSubDistrict('#sltParentTombon', $("#sltParentAmphoe").val());

                    });

                    $('#sltCopyDataFrom').change(function () {
                        if ($(this).val() == "1") {
                            // Copy data from father
                            $("#sltParentTitle").selectpicker('val', $("#sltFatherTitle").val());
                            $("#iptParentName").val($("#iptFatherName").val());
                            $("#iptParentLastName").val($("#iptFatherLastName").val());
                            $("#iptParentNameEn").val($("#iptFatherNameEn").val());
                            $("#iptParentNameLastEn").val($("#iptFatherNameLastEn").val());
                            $("#iptParentBirthday").val($("#iptFatherBirthday").val());
                            $("#iptParentIdentification").val($("#iptFatherIdentification").val());
                            $("#sltParentRace").selectpicker('val', $("#sltFatherRace").val());
                            $("#sltParentNation").selectpicker('val', $("#sltFatherNation").val());
                            $("#sltParentReligion").selectpicker('val', $("#sltFatherReligion").val());
                            $("#sltParentGraduated").selectpicker('val', $("#sltFatherGraduated").val());
                            $("#iptParentHomeNo").val($("#iptFatherHomeNo").val());
                            $("#iptParentSoi").val($("#iptFatherSoi").val());
                            $("#iptParentMoo").val($("#iptFatherMoo").val());
                            $("#iptParentRoad").val($("#iptFatherRoad").val());
                            $("#sltParentProvince").selectpicker('val', $("#sltFatherProvince").val());
                            $('#sltParentProvince').selectpicker('refresh');

                            var $options1 = $("#sltFatherAmphoe > option").clone();
                            $('#sltParentAmphoe').empty().append($options1);
                            $('#sltParentAmphoe').selectpicker('refresh');
                            $("#sltParentAmphoe").selectpicker('val', $("#sltFatherAmphoe").val());

                            var $options2 = $("#sltFatherTombon > option").clone();
                            $('#sltParentTombon').empty().append($options2);
                            $('#sltParentTombon').selectpicker('refresh');
                            $("#sltParentTombon").selectpicker('val', $("#sltFatherTombon").val());

                            $("#iptParentPostalCode").val($("#iptFatherPostalCode").val());

                            $("#iptParentJob").val($("#iptFatherJob").val());
                            $("#iptParentIncome").val($("#iptFatherIncome").val());
                            $("#iptParentWorkPlace").val($("#iptFatherWorkPlace").val());
                            $("#iptParentPhone").val($("#iptFatherPhone").val());
                            $("#iptParentPhone2").val($("#iptFatherPhone2").val());
                            $("#iptParentPhone3").val($("#iptFatherPhone3").val());
                        }
                        else if ($(this).val() == "2") {
                            // Copy data from mother
                            $("#sltParentTitle").selectpicker('val', $("#sltMotherTitle").val());
                            $("#iptParentName").val($("#iptMotherName").val());
                            $("#iptParentLastName").val($("#iptMotherLastName").val());
                            $("#iptParentNameEn").val($("#iptMotherNameEn").val());
                            $("#iptParentNameLastEn").val($("#iptMotherNameLastEn").val());
                            $("#iptParentBirthday").val($("#iptMotherBirthday").val());
                            $("#iptParentIdentification").val($("#iptMotherIdentification").val());
                            $("#sltParentRace").selectpicker('val', $("#sltMotherRace").val());
                            $("#sltParentNation").selectpicker('val', $("#sltMotherNation").val());
                            $("#sltParentReligion").selectpicker('val', $("#sltMotherReligion").val());
                            $("#sltParentGraduated").selectpicker('val', $("#sltMotherGraduated").val());
                            $("#iptParentHomeNo").val($("#iptMotherHomeNo").val());
                            $("#iptParentSoi").val($("#iptMotherSoi").val());
                            $("#iptParentMoo").val($("#iptMotherMoo").val());
                            $("#iptParentRoad").val($("#iptMotherRoad").val());
                            $("#sltParentProvince").selectpicker('val', $("#sltMotherProvince").val());
                            $('#sltParentProvince').selectpicker('refresh');

                            var $options1 = $("#sltMotherAmphoe > option").clone();
                            $('#sltParentAmphoe').empty().append($options1);
                            $('#sltParentAmphoe').selectpicker('refresh');
                            $("#sltParentAmphoe").selectpicker('val', $("#sltMotherAmphoe").val());

                            var $options2 = $("#sltMotherTombon > option").clone();
                            $('#sltParentTombon').empty().append($options2);
                            $('#sltParentTombon').selectpicker('refresh');
                            $("#sltParentTombon").selectpicker('val', $("#sltMotherTombon").val());

                            $("#iptParentPostalCode").val($("#iptMotherPostalCode").val());

                            $("#iptParentJob").val($("#iptMotherJob").val());
                            $("#iptParentIncome").val($("#iptMotherIncome").val());
                            $("#iptParentWorkPlace").val($("#iptMotherWorkPlace").val());
                            $("#iptParentPhone").val($("#iptMotherPhone").val());
                            $("#iptParentPhone2").val($("#iptMotherPhone2").val());
                            $("#iptParentPhone3").val($("#iptMotherPhone3").val());
                        }
                    });

                    $('#sltCopyAddressDataFrom').change(function () {
                        if ($(this).val() == "1") {
                            // Copy data from <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101183") %>
                            $("#iptFatherHomeNo").val($("#iptHomeNo").val());
                            $("#iptFatherSoi").val($("#iptHomeSoi").val());
                            $("#iptFatherMoo").val($("#iptHomeMoo").val());
                            $("#iptFatherRoad").val($("#iptHomeRoad").val());
                            $("#sltFatherProvince").selectpicker('val', $("#sltHomeProvince").val());
                            $('#sltFatherProvince').selectpicker('refresh');

                            var $options1 = $("#sltHomeAmphoe > option").clone();
                            $('#sltFatherAmphoe').empty().append($options1);
                            $('#sltFatherAmphoe').selectpicker('refresh');
                            $("#sltFatherAmphoe").selectpicker('val', $("#sltHomeAmphoe").val());

                            var $options2 = $("#sltHomeTombon > option").clone();
                            $('#sltFatherTombon').empty().append($options2);
                            $('#sltFatherTombon').selectpicker('refresh');
                            $("#sltFatherTombon").selectpicker('val', $("#sltHomeTombon").val());

                            $("#iptFatherPostalCode").val($("#iptHomePostalCode").val());
                            $("#iptFatherPhone").val($("#iptHomePhone").val());
                        }
                        else if ($(this).val() == "2") {
                            // Copy data from <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101184") %>
                            $("#iptFatherHomeNo").val($("#iptRegisterHomeNo").val());
                            $("#iptFatherSoi").val($("#iptRegisterHomeSoi").val());
                            $("#iptFatherMoo").val($("#iptRegisterHomeMoo").val());
                            $("#iptFatherRoad").val($("#iptRegisterHomeRoad").val());
                            $("#sltFatherProvince").selectpicker('val', $("#sltRegisterHomeProvince").val());
                            $('#sltFatherProvince').selectpicker('refresh');

                            var $options1 = $("#sltRegisterHomeAmphoe > option").clone();
                            $('#sltFatherAmphoe').empty().append($options1);
                            $('#sltFatherAmphoe').selectpicker('refresh');
                            $("#sltFatherAmphoe").selectpicker('val', $("#sltRegisterHomeAmphoe").val());

                            var $options2 = $("#sltRegisterHomeTombon > option").clone();
                            $('#sltFatherTombon').empty().append($options2);
                            $('#sltFatherTombon').selectpicker('refresh');
                            $("#sltFatherTombon").selectpicker('val', $("#sltRegisterHomeTombon").val());

                            $("#iptFatherPostalCode").val($("#iptRegisterHomePostalCode").val());
                            $("#iptFatherPhone").val($("#iptRegisterHomePhone").val());
                        }
                    });

                    $('#sltCopyAddressDataFrom2').change(function () {
                        if ($(this).val() == "1") {
                            // Copy data from <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101183") %>
                            $("#iptMotherHomeNo").val($("#iptHomeNo").val());
                            $("#iptMotherSoi").val($("#iptHomeSoi").val());
                            $("#iptMotherMoo").val($("#iptHomeMoo").val());
                            $("#iptMotherRoad").val($("#iptHomeRoad").val());
                            $("#sltMotherProvince").selectpicker('val', $("#sltHomeProvince").val());
                            $('#sltMotherProvince').selectpicker('refresh');

                            var $options1 = $("#sltHomeAmphoe > option").clone();
                            $('#sltMotherAmphoe').empty().append($options1);
                            $('#sltMotherAmphoe').selectpicker('refresh');
                            $("#sltMotherAmphoe").selectpicker('val', $("#sltHomeAmphoe").val());

                            var $options2 = $("#sltHomeTombon > option").clone();
                            $('#sltMotherTombon').empty().append($options2);
                            $('#sltMotherTombon').selectpicker('refresh');
                            $("#sltMotherTombon").selectpicker('val', $("#sltHomeTombon").val());

                            $("#iptMotherPostalCode").val($("#iptHomePostalCode").val());
                            $("#iptMotherPhone").val($("#iptHomePhone").val());
                        }
                        else if ($(this).val() == "2") {
                            // Copy data from <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101184") %>
                            $("#iptMotherHomeNo").val($("#iptRegisterHomeNo").val());
                            $("#iptMotherSoi").val($("#iptRegisterHomeSoi").val());
                            $("#iptMotherMoo").val($("#iptRegisterHomeMoo").val());
                            $("#iptMotherRoad").val($("#iptRegisterHomeRoad").val());
                            $("#sltMotherProvince").selectpicker('val', $("#sltRegisterHomeProvince").val());
                            $('#sltMotherProvince').selectpicker('refresh');

                            var $options1 = $("#sltRegisterHomeAmphoe > option").clone();
                            $('#sltMotherAmphoe').empty().append($options1);
                            $('#sltMotherAmphoe').selectpicker('refresh');
                            $("#sltMotherAmphoe").selectpicker('val', $("#sltRegisterHomeAmphoe").val());

                            var $options2 = $("#sltRegisterHomeTombon > option").clone();
                            $('#sltMotherTombon').empty().append($options2);
                            $('#sltMotherTombon').selectpicker('refresh');
                            $("#sltMotherTombon").selectpicker('val', $("#sltRegisterHomeTombon").val());

                            $("#iptMotherPostalCode").val($("#iptRegisterHomePostalCode").val());
                            $("#iptMotherPhone").val($("#iptRegisterHomePhone").val());
                        }
                        else if ($(this).val() == "3") {
                            // Copy data from <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111029") %>
                            $("#iptMotherHomeNo").val($("#iptFatherHomeNo").val());
                            $("#iptMotherSoi").val($("#iptFatherSoi").val());
                            $("#iptMotherMoo").val($("#iptFatherMoo").val());
                            $("#iptMotherRoad").val($("#iptFatherRoad").val());
                            $("#sltMotherProvince").selectpicker('val', $("#sltFatherProvince").val());
                            $('#sltMotherProvince').selectpicker('refresh');

                            var $options1 = $("#sltFatherAmphoe > option").clone();
                            $('#sltMotherAmphoe').empty().append($options1);
                            $('#sltMotherAmphoe').selectpicker('refresh');
                            $("#sltMotherAmphoe").selectpicker('val', $("#sltFatherAmphoe").val());

                            var $options2 = $("#sltFatherTombon > option").clone();
                            $('#sltMotherTombon').empty().append($options2);
                            $('#sltMotherTombon').selectpicker('refresh');
                            $("#sltMotherTombon").selectpicker('val', $("#sltFatherTombon").val());

                            $("#iptMotherPostalCode").val($("#iptFatherPostalCode").val());
                            $("#iptMotherPhone").val($("#iptFatherPhone").val());
                        }
                    });

                    // Modal Section
                    $('#modalNotifyConfirmSave').off().on('show.bs.modal', function (e) {
                        $(this).find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101166") %>');
                        $(this).find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101167") %>');
                    });

                    // Initial data

                    activateBootstrapSelect('.stdFamilyForm .selectpicker');

                    $('#iptFatherIncome, #iptMotherIncome, #iptParentIncome').number(true, 2);

                    $('.stdFamilyForm .datepicker').datetimepicker({
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

                    $(".stdFamilyForm .datepicker").attr('maxlength', '10');

                    // Load info command
                    stdFamilyForm.GetFatherItem(<%=Request.QueryString["sid"]%>);
                    stdFamilyForm.GetMotherItem(<%=Request.QueryString["sid"]%>);
                    stdFamilyForm.GetParentItem(<%=Request.QueryString["sid"]%>);

                });

            </script>
        </asp:View>
        <asp:View ID="ViewContent" runat="server">
            <div class="stdFamilyView view-form">
                <form id="stdFatherView" class="form-padding">
                    <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101180") %></p>
                    <div class="row div-row-padding" style="margin-top: 30px;">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spFatherTitle"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label></label></div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101065") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spFatherName"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101066") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spFatherLastName"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101067") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spFatherNameEn"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101069") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spFatherNameLastEn"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spFatherBirthday"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spFatherIdentification"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spFatherRace"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spFatherNation"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spFatherReligion"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spFatherGraduated"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spFatherHomeNo"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spFatherSoi"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spFatherMoo"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spFatherRoad"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spFatherProvince"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spFatherAmphoe"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spFatherTombon"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spFatherPostalCode"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spFatherJob"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spFatherIncome"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spFatherWorkPlace"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101188") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spFatherPhone"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101189") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spFatherPhone2"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101190") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spFatherPhone3"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                    </div>
                    <div class="row text-center">
                        <button type="button"
                            class="btn btn-danger btn-cancel" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </form>
                <form id="stdMotherView" class="form-padding">
                    <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00194") %></p>
                    <div class="row div-row-padding" style="margin-top: 30px;">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spMotherTitle"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label></label></div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101065") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spMotherName"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101066") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spMotherLastName"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101067") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spMotherNameEn"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101069") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spMotherNameLastEn"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spMotherBirthday"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spMotherIdentification"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spMotherRace"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spMotherNation"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spMotherReligion"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spMotherGraduated"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spMotherHomeNo"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spMotherSoi"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spMotherMoo"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spMotherRoad"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spMotherProvince"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spMotherAmphoe"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spMotherTombon"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spMotherPostalCode"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spMotherJob"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spMotherIncome"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spMotherWorkPlace"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101188") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spMotherPhone"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101189") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spMotherPhone2"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101190") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spMotherPhone3"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                    </div>
                    <div class="row text-center">
                        <button type="button"
                            class="btn btn-danger btn-cancel" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </form>
                <form id="stdParentView" class="form-padding">
                    <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101179") %></p>
                    <div class="row div-row-padding" style="margin-top: 30px;">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentTitle"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label></label></div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101065") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentName"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101066") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentLastName"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101067") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentNameEn"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101069") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentNameLastEn"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentBirthday"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentIdentification"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentRace"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentNation"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentReligion"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103098") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentGraduated"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentHomeNo"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentSoi"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentMoo"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentRoad"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentProvince"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentAmphoe"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentTombon"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentPostalCode"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101192") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentRelate"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101197") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentRequestStudyMoney"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101201") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentStatus"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label></label></div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentJob"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101186") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentIncome"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101187") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentWorkPlace"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101188") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentPhone"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101189") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentPhone2"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101190") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spParentPhone3"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                    </div>
                    <div class="row text-center">
                        <button type="button"
                            class="btn btn-danger btn-cancel" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </form>
            </div>
            <script type="text/javascript">

                var stdFamilyView = {
                    GetFatherItem: function (stdID) {
                        $.ajax({
                            type: "POST",
                            url: "StdFamily.aspx/GetFatherItemView",
                            data: '{stdID: ' + stdID + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdFamilyView.OnSuccessGetFather,
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
                    OnSuccessGetFather: function (response) {
                        if (response.d == "error") {

                            $("#modalNotifyOnlyClose").find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>');
                            $("#modalNotifyOnlyClose").find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111025") %>');
                            $("#modalNotifyOnlyClose").modal('show');

                        } else if (response.d == "new") {
                            xStdKey.fmlid = "0";
                        } else {
                            var xmlDoc = $.parseXML(response.d);
                            var xml = $(xmlDoc);
                            var infors = xml.find("Table1");

                            $.each(infors, function (index) {
                                var infor = $(this);

                                xStdKey.fmlid = $(this).find("F0").text();
                                $("#spFatherTitle").text($(this).find("F1").text());
                                $("#spFatherName").text($(this).find("F2").text());
                                $("#spFatherLastName").text($(this).find("F3").text());
                                $("#spFatherNameEn").text($(this).find("F4").text());
                                $("#spFatherNameLastEn").text($(this).find("F5").text());
                                $("#spFatherBirthday").text($(this).find("F6").text());
                                $("#spFatherIdentification").text($(this).find("F7").text());
                                $("#spFatherRace").text($(this).find("F8").text());
                                $("#spFatherNation").text($(this).find("F9").text());
                                $("#spFatherReligion").text($(this).find("F10").text());
                                $("#spFatherGraduated").text($(this).find("F11").text());
                                $("#spFatherHomeNo").text($(this).find("F12").text());
                                $("#spFatherSoi").text($(this).find("F13").text());
                                $("#spFatherMoo").text($(this).find("F14").text());
                                $("#spFatherRoad").text($(this).find("F15").text());
                                $("#spFatherProvince").text($(this).find("F16").text());
                                $("#spFatherAmphoe").text($(this).find("F17").text());
                                $("#spFatherTombon").text($(this).find("F18").text());
                                $("#spFatherPostalCode").text($(this).find("F19").text());
                                $("#spFatherJob").text($(this).find("F20").text());
                                $("#spFatherIncome").text($(this).find("F21").text());
                                $("#spFatherWorkPlace").text($(this).find("F22").text());
                                $("#spFatherPhone").text($(this).find("F23").text());
                                $("#spFatherPhone2").text($(this).find("F24").text());
                                $("#spFatherPhone3").text($(this).find("F25").text());

                            });

                        }
                    },
                    GetMotherItem: function (stdID) {
                        $.ajax({
                            type: "POST",
                            url: "StdFamily.aspx/GetMotherItemView",
                            data: '{stdID: ' + stdID + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdFamilyView.OnSuccessGetMother,
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
                    OnSuccessGetMother: function (response) {
                        if (response.d == "error") {

                            $("#modalNotifyOnlyClose").find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>');
                            $("#modalNotifyOnlyClose").find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111025") %>');
                            $("#modalNotifyOnlyClose").modal('show');

                        } else if (response.d == "new") {
                            xStdKey.fmlid = "0";
                        } else {
                            var xmlDoc = $.parseXML(response.d);
                            var xml = $(xmlDoc);
                            var infors = xml.find("Table1");

                            $.each(infors, function (index) {
                                var infor = $(this);

                                xStdKey.fmlid = $(this).find("F0").text();
                                $("#spMotherTitle").text($(this).find("F1").text());
                                $("#spMotherName").text($(this).find("F2").text());
                                $("#spMotherLastName").text($(this).find("F3").text());
                                $("#spMotherNameEn").text($(this).find("F4").text());
                                $("#spMotherNameLastEn").text($(this).find("F5").text());
                                $("#spMotherBirthday").text($(this).find("F6").text());
                                $("#spMotherIdentification").text($(this).find("F7").text());
                                $("#spMotherRace").text($(this).find("F8").text());
                                $("#spMotherNation").text($(this).find("F9").text());
                                $("#spMotherReligion").text($(this).find("F10").text());
                                $("#spMotherGraduated").text($(this).find("F11").text());
                                $("#spMotherHomeNo").text($(this).find("F12").text());
                                $("#spMotherSoi").text($(this).find("F13").text());
                                $("#spMotherMoo").text($(this).find("F14").text());
                                $("#spMotherRoad").text($(this).find("F15").text());
                                $("#spMotherProvince").text($(this).find("F16").text());
                                $("#spMotherAmphoe").text($(this).find("F17").text());
                                $("#spMotherTombon").text($(this).find("F18").text());
                                $("#spMotherPostalCode").text($(this).find("F19").text());
                                $("#spMotherJob").text($(this).find("F20").text());
                                $("#spMotherIncome").text($(this).find("F21").text());
                                $("#spMotherWorkPlace").text($(this).find("F22").text());
                                $("#spMotherPhone").text($(this).find("F23").text());
                                $("#spMotherPhone2").text($(this).find("F24").text());
                                $("#spMotherPhone3").text($(this).find("F25").text());

                            });

                        }
                    },
                    GetParentItem: function (stdID) {
                        $.ajax({
                            type: "POST",
                            url: "StdFamily.aspx/GetParentItemView",
                            data: '{stdID: ' + stdID + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdFamilyView.OnSuccessGetParent,
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
                    OnSuccessGetParent: function (response) {
                        if (response.d == "error") {

                            $("#modalNotifyOnlyClose").find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>');
                            $("#modalNotifyOnlyClose").find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111025") %>');
                            $("#modalNotifyOnlyClose").modal('show');

                        } else if (response.d == "new") {
                            xStdKey.fmlid = "0";
                        } else {
                            var xmlDoc = $.parseXML(response.d);
                            var xml = $(xmlDoc);
                            var infors = xml.find("Table1");

                            $.each(infors, function (index) {
                                var infor = $(this);

                                xStdKey.fmlid = $(this).find("F0").text();
                                $("#spParentTitle").text($(this).find("F1").text());
                                $("#spParentName").text($(this).find("F2").text());
                                $("#spParentLastName").text($(this).find("F3").text());
                                $("#spParentNameEn").text($(this).find("F4").text());
                                $("#spParentNameLastEn").text($(this).find("F5").text());
                                $("#spParentBirthday").text($(this).find("F6").text());
                                $("#spParentIdentification").text($(this).find("F7").text());
                                $("#spParentRace").text($(this).find("F8").text());
                                $("#spParentNation").text($(this).find("F9").text());
                                $("#spParentReligion").text($(this).find("F10").text());
                                $("#spParentGraduated").text($(this).find("F11").text());
                                $("#spParentHomeNo").text($(this).find("F12").text());
                                $("#spParentSoi").text($(this).find("F13").text());
                                $("#spParentMoo").text($(this).find("F14").text());
                                $("#spParentRoad").text($(this).find("F15").text());
                                $("#spParentProvince").text($(this).find("F16").text());
                                $("#spParentAmphoe").text($(this).find("F17").text());
                                $("#spParentTombon").text($(this).find("F18").text());
                                $("#spParentPostalCode").text($(this).find("F19").text());
                                $("#spParentRelate").text($(this).find("F20").text());
                                $("#spParentRequestStudyMoney").text($(this).find("F21").text());
                                $("#spParentStatus").text($(this).find("F22").text());
                                $("#spParentJob").text($(this).find("F23").text());
                                $("#spParentIncome").text($(this).find("F24").text());
                                $("#spParentWorkPlace").text($(this).find("F25").text());
                                $("#spParentPhone").text($(this).find("F26").text());
                                $("#spParentPhone2").text($(this).find("F27").text());
                                $("#spParentPhone3").text($(this).find("F28").text());

                            });

                        }
                    }
                }

                $(document).ready(function () {

                    $(".stdFamilyView .btn-cancel").bind({
                        click: function () {

                            window.close();

                            return false;
                        }
                    });

                    // Load info command
                    stdFamilyView.GetFatherItem(<%=Request.QueryString["sid"]%>);
                    stdFamilyView.GetMotherItem(<%=Request.QueryString["sid"]%>);
                    stdFamilyView.GetParentItem(<%=Request.QueryString["sid"]%>);

                });

            </script>
        </asp:View>
    </asp:MultiView></body>
</html>
