<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StdProfile.aspx.cs" Inherits="FingerprintPayment.StudentInfo.StdProfile" %>

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
            <div class="stdInfoForm">
                <form id="stdInfoForm" class="form-padding">
                    <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101029") %></p>
                    <div class="row div-row-padding" style="margin-top: 30px;">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> :</label>
                        </div>
                        <div class="col-md-10 checkbox-radios">
                            <asp:Literal ID="ltrTerm" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="iptStudentID">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %> :
                                <p style="height: 0px; margin: -2px 7px 2px 0px; font-size: 0.9em;">(Username)</p>
                            </label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input" style="white-space: nowrap;">
                            <input type="text" class="form-control" id="iptStudentID" name="iptStudentID"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %>" maxlength="20" style="display: inline-block;" />
                            <a href="#" data-toggle="tooltip" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101049") %>" class="text-danger" style="font-size: 20px;">
                                <i class="fa fa-question-circle" aria-hidden="true"></i>
                            </a>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label for="sltStudentStatus"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltStudentStatus" name="sltStudentStatus" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103002") %>">
                                <option selected="selected" value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101031") %></option>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101033") %></option>
                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101032") %></option>
                                <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101034") %></option>
                                <option value="4" disabled><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101030") %></option>
                                <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101035") %></option>
                                <option value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101036") %></option>
                                <option value="7"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101037") %></option>
                            </select>
                            <div class="col-md-12 checkbox-radios drop-out" style="padding: 0px 0px 0px 0px; margin-bottom: -10px; display: none;">
                                <div class="form-check form-check-inline" data-toggle="tooltip" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101046") %>">
                                    <label class="form-check-label" for="rdoDropOutType1">
                                        <input class="form-check-input" type="radio" name="rdoDropOutType" id="rdoDropOutType1" value="1" />
                                        <span class="label-text" style="display: contents;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101045") %></span>
                                        <span class="circle">
                                            <span class="check"></span>
                                        </span>
                                    </label>
                                </div>
                                <div class="form-check form-check-inline" data-toggle="tooltip" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101048") %>">
                                    <label class="form-check-label" for="rdoDropOutType2">
                                        <input class="form-check-input" type="radio" name="rdoDropOutType" id="rdoDropOutType2" value="2" />
                                        <span class="label-text" style="display: contents;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101047") %></span>
                                        <span class="circle">
                                            <span class="check"></span>
                                        </span>
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="iptStudentMoveInDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101050") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3">
                            <div class="form-group div-datepicker">
                                <input id="iptStudentMoveInDate" name="iptStudentMoveInDate" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label id="spnStudentDayQuit" for="iptStudentDayQuit"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102097") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3">
                            <div class="form-group div-datepicker">
                                <input id="iptStudentDayQuit" name="iptStudentDayQuit" type="text" class="form-control datepicker" disabled />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="iptStudentNumber"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101051") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptStudentNumber" name="iptStudentNumber"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101051") %>" maxlength="3" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label for="iptStudentNote"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <div class="input-group" style="display: flex">
                                <input type="text" class="form-control" id="iptStudentNote" name="iptStudentNote" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %>" maxlength="250" disabled />
                                <div id="divStudentNote" class="input-group-btn" style="display: flex; width: auto;">
                                    <button type="button" class="btn btn-round btn-info dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" disabled>
                                        <i class="material-icons">list</i>
                                        <span class="caret"></span>
                                        <span class="sr-only">Toggle Dropdown</span>
                                    </button>
                                    <ul class="dropdown-menu dropdown-menu-right" style="font-size: 26px;">
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="sltStudentClass"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltStudentClass" name="sltStudentClass" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101009") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101009") %></option>
                                <asp:Literal ID="ltrStudentClass" runat="server"></asp:Literal>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label for="sltStudentClassRoom"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltStudentClassRoom" name="sltStudentClassRoom" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101011") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101011") %></option>
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding change-room" style="display: none;">
                        <label class="col-md-2 mb-2 col-form-label text-right"></label>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label for="iptChangeRoomDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111042") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3">
                            <div class="form-group div-datepicker">
                                <input id="iptChangeRoomDate" name="iptChangeRoomDate" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="sltStudentGender"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101061") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltStudentGender" name="sltStudentGender" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101062") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101062") %></option>
                                <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %></option>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %></option>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label for="sltStudentTitle"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltStudentTitle" name="sltStudentTitle" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101150") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101150") %></option>
                                <asp:Literal ID="ltrStudentTitle" runat="server"></asp:Literal>
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="iptStudentFirstNameTh"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101065") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptStudentFirstNameTh" name="iptStudentFirstNameTh"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101065") %>" maxlength="256" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label for="iptStudentLastNameTh"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101066") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptStudentLastNameTh" name="iptStudentLastNameTh"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101066") %>" maxlength="256" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="iptStudentFirstNameEn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101067") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptStudentFirstNameEn" name="iptStudentFirstNameEn"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101068") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label for="iptStudentLastNameEn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101069") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptStudentLastNameEn" name="iptStudentLastNameEn"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101070") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="iptStudentFirstNameOther"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101071") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptStudentFirstNameOther" name="iptStudentFirstNameOther"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101071") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label for="iptStudentLastNameOther"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101072") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptStudentLastNameOther" name="iptStudentLastNameOther"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101072") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="iptStudentNickNameTh"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101073") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptStudentNickNameTh" name="iptStudentNickNameTh"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101073") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label for="iptStudentNickNameEn"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101074") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptStudentNickNameEn" name="iptStudentNickNameEn"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101074") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="iptStudentBirthday"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3">
                            <div class="form-group div-datepicker">
                                <input id="iptStudentBirthday" name="iptStudentBirthday" type="text" class="form-control datepicker" />
                                <span class="form-control-feedback" style="color: #000; opacity: 1;">
                                    <i class="material-icons">event</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label for="iptStudentIdentification"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptStudentIdentification" name="iptStudentIdentification"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %>" maxlength="13" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="sltStudentRace"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltStudentRace" name="sltStudentRace" data-live-search="true" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101078") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101078") %></option>
                                <asp:Literal ID="ltrStudentRace" runat="server" />
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label for="sltStudentNation"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltStudentNation" name="sltStudentNation" data-live-search="true" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101080") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101080") %></option>
                                <asp:Literal ID="ltrStudentNation" runat="server" />
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="sltStudentReligion"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltStudentReligion" name="sltStudentReligion" data-live-search="true" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101082") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101082") %></option>
                                <asp:Literal ID="ltrStudentReligion" runat="server" />
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label></label>
                        </div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
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
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="sltDisabilityCode"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101086") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltDisabilityCode" name="sltDisabilityCode" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111012") %>">
                                <asp:Literal ID="ltrDisabilityCode" runat="server" />
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label for="sltDisadvantageCode"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101097") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltDisadvantageCode" name="sltDisadvantageCode" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111014") %>">
                                <asp:Literal ID="ltrDisadvantageCode" runat="server" />
                            </select>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="iptStudentPhone"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptStudentPhone" name="iptStudentPhone"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %>" maxlength="50" />
                            <span class="text-success d-none phone-verify-message"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00951") %></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label for="iptStudentEmail"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptStudentEmail" name="iptStudentEmail"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %>" maxlength="256" />
                            <span class="text-success d-none email-verify-message"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02327") %></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="sltStudentSonTotal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101111") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltStudentSonTotal" name="sltStudentSonTotal" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101112") %>">
                                <option selected="selected" value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101112") %></option>
                                <option value="1">1</option>
                                <option value="2">2</option>
                                <option value="3">3</option>
                                <option value="4">4</option>
                                <option value="5">5</option>
                                <option value="6">6</option>
                                <option value="7">7</option>
                                <option value="8">8</option>
                                <option value="9">9</option>
                                <option value="10">10</option>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label for="iptStudentSonNumber"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101113") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptStudentSonNumber" name="iptStudentSonNumber"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101113") %>" maxlength="2" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="sltStudentBrethrenStudyHere"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101114") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltStudentBrethrenStudyHere" name="sltStudentBrethrenStudyHere" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101115") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101115") %></option>
                                <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %></option>
                                <option value="1">1 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></option>
                                <option value="2">2 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></option>
                                <option value="3">3 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></option>
                                <option value="4">4 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></option>
                                <option value="5">5 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></option>
                                <option value="6">6 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></option>
                                <option value="7">7 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></option>
                                <option value="8">8 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></option>
                                <option value="9">9 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></option>
                                <option value="10">10 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></option>
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
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="iptnMax"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101119") %>:</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptnMax" name="iptnMax"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101119") %>" maxlength="11" />
                            <span style="color: red;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101120") %></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label for="btnDelFinger">
                                <asp:Literal ID="ltrFinger" runat="server"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111043") %> :</asp:Literal>
                            </label>
                        </div>
                        <div class="col-md-3 mb-3">
                            <asp:Literal ID="ltrPassword" runat="server"></asp:Literal>
                            <asp:Literal ID="ltrDelFinger" runat="server">
                            <button id="btnDelFinger" onclick="ShowFingerPopup(); return false;"
                                class="btn btn-primary" data-toggle="modal"
                                data-target="#modalFingerPopup" ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111044") %></button></asp:Literal>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="iptJourneyType1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101121") %>:</label>
                        </div>
                        <div class="col-md-3 checkbox-radios" style="padding: 0px 0px 0px 0px; margin-left: 15px;">
                            <div class="form-check form-check-inline">
                                <label class="form-check-label" for="iptJourneyType1">
                                    <input class="form-check-input" type="radio" name="iptJourneyType" id="iptJourneyType1" value="1" />
                                    <span class="label-text" style="display: contents;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101122") %></span>
                                    <span class="circle">
                                        <span class="check"></span>
                                    </span>
                                </label>
                            </div>
                            <div class="form-check form-check-inline">
                                <label class="form-check-label" for="iptJourneyType2">
                                    <input class="form-check-input" type="radio" name="iptJourneyType" id="iptJourneyType2" value="2" />
                                    <span class="label-text" style="display: contents;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101123") %></span>
                                    <span class="circle">
                                        <span class="check"></span>
                                    </span>
                                </label>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"></div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div id="divDormitory" class="row div-row-padding" style="display: none;">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="iptDormitoryName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111045") %>:</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptDormitoryName" name="iptDormitoryName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111045") %>" maxlength="150" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"></div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label for="iptNote2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101124") %>:</label>
                        </div>
                        <div class="col-md-3 mb-3">
                            <textarea class="form-control" rows="3" id="iptNote2"></textarea>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right" for="">
                            <label></label>
                        </div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                    </div>
                    <div class="row text-center" style="margin-top: 7px; margin-bottom: 7px;">
                        <button id="saveStudent" type="submit" class="btn btn-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                        <button id="saveStudentAndNext" type="submit" class="btn btn-info save-next"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101126") %></button>
                        <button id="btnCancelStdProfile" type="button" class="btn btn-danger btn-cancel" data-dismiss="modal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
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
                                <label class="btn btn-primary" onclick="changeFinger();" style="width: 122.7px !important;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111046") %></label>
                                <button id="modalClose" type="button" class="btn btn-danger" data-dismiss="modal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <script type="text/javascript">

                var stdInfoForm = {
                    ClickChangeStudentStatus: false,
                    MemStudentStatus: '',
                    GetItem: function (stdID, termID) {
                        $.ajax({
                            type: "POST",
                            url: "StdProfile.aspx/GetItem",
                            data: '{stdID: ' + stdID + ', termID: \'' + termID + '\'}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdInfoForm.OnSuccessGet,
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
                            xStdKey.fmlid = "0";

                            // Set default
                            $('#iptnMax').val('0');

                            $('#studentTabs li.nav-item:gt(0)').addClass("disabled");
                            $('#studentTabs li.nav-item:gt(0)').find('a').removeAttr('data-toggle');

                        } else {
                            var xmlDoc = $.parseXML(response.d);
                            var xml = $(xmlDoc);
                            var infors = xml.find("Table1");

                            $.each(infors, function (index) {
                                var infor = $(this);

                                if (index == 0) {
                                    xStdKey.fmlid = $(this).find("F0").text();

                                    $("#iptStudentID").val($(this).find("F1").text());
                                    //$("#sltStudentStatus").val($(this).find("F2").text());
                                    $("#sltStudentStatus").selectpicker('val', $(this).find("F2").text());
                                    stdInfoForm.MemStudentStatus = $("#sltStudentStatus").val();

                                    if ($("#sltStudentStatus").val() != "0") {
                                        $("#spnStudentDayQuit").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>" + $("#sltStudentStatus option:selected").text() + " :");
                                    } else {
                                        $("#spnStudentDayQuit").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> :");
                                    }

                                    // 1: <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101033") %>, 2: <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101032") %>, 3: <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101034") %>, 5: <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101035") %>, 6: <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101036") %>, 7: <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101037") %>
                                    if ($.inArray($("#sltStudentStatus").val(), ["0", "4"]) == -1) {
                                        $('.drop-out').show();

                                        if ($.inArray($("#sltStudentStatus").val(), ["2", "3", "6", "7"]) != -1) {
                                            $('#divStudentNote ul').empty().html(`
                                        <li><a class="note-studentstatus-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101056") %></a></li>
                                        <li><a class="note-studentstatus-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101057") %></a></li>
                                        <li><a class="note-studentstatus-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101058") %></a></li>
                                        <li><a class="note-studentstatus-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101059") %></a></li>
                                        <li><a class="note-other"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></a></li>`);
                                        }
                                        else if ($.inArray($("#sltStudentStatus").val(), ["1", "5"]) != -1) {
                                            $('#divStudentNote ul').empty().html(`
                                        <li><a class="note-studentstatus-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101053") %></a></li>
                                        <li><a class="note-studentstatus-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101054") %></a></li>
                                        <li><a class="note-studentstatus-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101055") %></a></li>
                                        <li><a class="note-other"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></a></li>`);
                                        }
                                    }
                                    else {
                                        $('.drop-out').hide();
                                    }

                                    $("#iptStudentMoveInDate").val($(this).find("F3").text());
                                    $("#iptStudentDayQuit").val($(this).find("F4").text());
                                    $('#iptStudentNumber').val($(this).find("F5").text());
                                    $("#iptStudentNote").val($(this).find("F6").text());
                                    switch ($("#iptStudentNote").val()) {
                                        case '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101056") %>':
                                        case '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101057") %>':
                                        case '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101058") %>':
                                        case '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101059") %>':
                                        case '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101053") %>':
                                        case '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101054") %>':
                                        case '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101055") %>':
                                            $("#iptStudentNote").prop('disabled', true);
                                            break
                                        default:
                                            $("#iptStudentNote").prop('disabled', false);
                                            break
                                    }

                                    $("#sltStudentClass").selectpicker('val', $(this).find("F7").text());
                                    LoadTermSubLevel2($("#sltStudentClass").val(), '#sltStudentClassRoom');

                                    $("#sltStudentClassRoom").attr('data-classId', $(this).find("F8").text()).selectpicker('val', $(this).find("F8").text());
                                    $("#sltStudentGender").selectpicker('val', $(this).find("F9").text());
                                    $("#sltStudentTitle").selectpicker('val', $(this).find("F10").text());
                                    $("#iptStudentFirstNameTh").val($(this).find("F11").text());
                                    $("#iptStudentLastNameTh").val($(this).find("F12").text());
                                    $("#iptStudentFirstNameEn").val($(this).find("F13").text());
                                    $("#iptStudentLastNameEn").val($(this).find("F14").text());
                                    $("#iptStudentFirstNameOther").val($(this).find("F15").text());
                                    $("#iptStudentLastNameOther").val($(this).find("F16").text());
                                    $("#iptStudentNickNameTh").val($(this).find("F17").text());
                                    $("#iptStudentNickNameEn").val($(this).find("F18").text());
                                    $("#iptStudentBirthday").val($(this).find("F19").text());
                                    $("#iptStudentIdentification").val($(this).find("F20").text());
                                    $('#sltStudentRace').selectpicker('val', $(this).find("F21").text());
                                    $('#sltStudentNation').selectpicker('val', $(this).find("F22").text());
                                    $('#sltStudentReligion').selectpicker('val', $(this).find("F23").text());
                                    $("#iptPassportNumber").val($(this).find("F41").text());
                                    $("#iptPassportCountry").val($(this).find("F42").text());
                                    $("#iptPassportExpirationDate").val($(this).find("F43").text());
                                    $("#iptStudentPhone").val($(this).find("F24").text());
                                    $("#iptStudentEmail").val($(this).find("F25").text());
                                    $("#sltStudentSonTotal").selectpicker('val', $(this).find("F26").text());
                                    $("#iptStudentSonNumber").val($(this).find("F27").text());
                                    $("#sltStudentBrethrenStudyHere").selectpicker('val', $(this).find("F28").text());

                                    if (!$.isBlank($(this).find("F29").text())) {
                                        let divProfileImage = $(this).find("F29").text();

                                        if ($(this).find("F29").text().indexOf("?x-image-process=image/resize,m_fill,h_300,w_270") != -1) {
                                            divProfileImage += '&v=' + (new Date()).getTime() * 1e4
                                        } else {
                                            divProfileImage += '?v=' + (new Date()).getTime() * 1e4
                                        }

                                        $("#divProfileImage .img-photo").attr("src", divProfileImage);
                                        $("#divProfileImage .img-photo-original").attr("src", $(this).find("F40").text());

                                        $(".stdProfile #divProfileImage .div-picture-remove").show();
                                        //$(".stdProfile #divProfileImage .div-crop").show();
                                    }

                                    $('#iptUseBiometric').bootstrapToggle((/true/i).test($(this).find("F30").text()) ? 'on' : 'off');
                                    $("#iptNote2").val($(this).find("F31").text());

                                    $("#sltDisabilityCode").selectpicker('val', $(this).find("F33").text());
                                    $("#sltDisadvantageCode").selectpicker('val', $(this).find("F34").text());
                                    if (!$.isBlank($(this).find("F35").text())) {
                                        $("#iptnMax").val($(this).find("F35").text());
                                    }
                                    else {
                                        $("#iptnMax").val('0');
                                    }

                                    if ($(this).find("F36").text()) $('#iptJourneyType' + $(this).find("F36").text()).click();
                                    $("#iptDormitoryName").val($(this).find("F37").text());

                                    if ($(this).find("F38").text()) $('#rdoDropOutType' + $(this).find("F38").text()).click();
                                }
                                else {
                                    // Disable verify field
                                    if (!!$(this).find("F25").text()) {
                                        $('#iptStudentEmail').prop('disabled', true);
                                        $('.email-verify-message').removeClass('d-none');
                                    }
                                    if (!!$(this).find("F24").text()) {
                                        $('#iptStudentPhone').prop('disabled', true);
                                        $('.phone-verify-message').removeClass('d-none');
                                    }
                                }
                            });

                            // 
                            $("#stdInfoForm").validate().resetForm();
                        }
                    },
                    SaveItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "StdProfile.aspx/SaveItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdInfoForm.OnSuccessSave,
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
                    OnSuccessSave: function (response) {
                        var title = "";
                        var body = "";

                        var flag = response.d.split('-');
                        switch (flag[0]) {
                            case "complete":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';

                                xStdKey.fmlid = flag[2];
                                xStdKey.tid = flag[3];

                                stdInfoForm.UploadFile2(flag[1], function () {
                                    $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {
                                        // Redirect to student list
                                        if ('<%=Request.QueryString["sid"]%>' == '0') {
                                            window.location.replace("StudentDetail.aspx?v=form&sid=" + flag[1] + "&tid=" + xStdKey.tid);
                                        }
                                    });
                                });

                                // hide change room section
                                $('.change-room').hide();
                                $('#iptChangeRoomDate').val('');
                                $("#sltStudentClassRoom").attr('data-classId', $("#sltStudentClassRoom").val());

                                break;
                            case "warning":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                                body = flag[1];

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
                    SaveAndNextItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "StdProfile.aspx/SaveItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdInfoForm.OnSuccessSaveAndNext,
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
                    OnSuccessSaveAndNext: function (response) {
                        var title = "";
                        var body = "";

                        var flag = response.d.split('-');
                        switch (flag[0]) {
                            case "complete":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';

                                xStdKey.fmlid = flag[2];
                                xStdKey.tid = flag[3];

                                stdInfoForm.UploadFile2(flag[1], function () {
                                    $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {
                                        // Redirect to student list
                                        window.location.replace("StudentDetail.aspx?v=form&sid=" + StudentDetail_NextSID + "&tid=" + xStdKey.tid);
                                    });
                                });

                                break;
                            case "warning":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111026") %> [{0}]'.format(flag[1]);

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
                            url: "StdProfile.aspx/ClearSessionID",
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
                    UploadFile: function (stdID) {

                        var formData = new FormData();
                        var objFiles = $('.stdProfile #divProfileImage input[type="file"]').get(0);
                        var files = objFiles.files;
                        for (var i = 0; i < files.length; i++) {
                            formData.append('_pr_' + i, files[i]);
                        }

                        $.ajax({
                            url: '/StudentInfo/Ashx/UploadImageProfileHandler.ashx?stdID=' + stdID,
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
                    },
                    UploadFile2: function (stdID, callback) {

                        var formData = new FormData();
                        var objFiles = $('.stdProfile #divProfileImage input[type="file"]').get(0);
                        var files = objFiles.files;
                        for (var i = 0; i < files.length; i++) {
                            formData.append('_pr_' + i, files[i]);
                        }

                        var coordinates = $('#divProfileImage img.img-cropped').data('coordinates');
                        var originalSize = $('#divProfileImage img.img-cropped').data('original-size');
                        var crop = { coordinates: coordinates, originalSize: originalSize, base64: $('#divProfileImage img.img-cropped').attr('src') };
                        formData.append('_crop_', JSON.stringify(crop));

                        $.ajax({
                            url: '/StudentInfo/Ashx/UploadImageProfileHandler.ashx?stdID=' + stdID,
                            data: formData,
                            dataType: 'json',
                            type: 'POST',
                            contentType: false,
                            processData: false,
                            success: function (result) {
                                //do some tasks after upload
                                console.log(result);
                                callback();
                            },
                            error: function (response) {
                                console.log(response);
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>' + response);
                            }
                        });
                    },
                    CheckScoreEntered: function (sId, nTerm, nTermSubLevelId, newRoomnTermSubLevelId) {
                        $('#tab1').prepend('<div class="loader"></div>');
                        $.ajax({
                            type: "POST",
                            url: "StdProfile.aspx/CheckScoreEntered",
                            data: '{stdID: ' + sId + ', termID: \'' + nTerm + '\', nTermSubLevelId: ' + nTermSubLevelId + ', newRoomnTermSubLevelId: ' + newRoomnTermSubLevelId + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                console.log(response.d);
                                if (response.d.length > 0) {
                                    $('#tab1 .loader').remove();

                                    var message = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101329") %></br>" +
                                        "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101330") %></br>" +
                                        "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111047") %> <br><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101331") %>";
                                    var courseCode = "";
                                    $(response.d).each(function () {

                                        courseCode += "</br>" + this;

                                    });

                                    message = message + "</br>" + courseCode;
                                    $.confirm({
                                        title: '<h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101328") %></h>',
                                        content: '<h2>' + message + '</h>',
                                        boxWidth: '500px',
                                        useBootstrap: false,
                                        buttons: {
                                            cancel: {
                                                label: '<i class="fa fa-times"></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>',
                                                action: function () {
                                                    $("#sltStudentClassRoom").selectpicker('val', nTermSubLevelId);
                                                }
                                            },
                                            confirm: {
                                                label: '<i class="fa fa-check"></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M201067") %>',
                                                action: function () {
                                                    $('.change-room').show();
                                                }
                                            }
                                        },
                                    });

                                }
                                else {
                                    $('#tab1 .loader').remove();
                                    $('.change-room').show();
                                }

                            },
                            failure: function (response) {

                                $('.change-room').show();
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    GetLastStudentNumberInClassroom: function (classroomID) {
                        $.ajax({
                            async: false,
                            type: "POST",
                            url: 'StdProfile.aspx/GetLastStudentNumberInClassroom',
                            data: JSON.stringify({ classroomID: classroomID }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (result) {
                                var r = JSON.parse(result.d);
                                if (r.success) {
                                    $('#iptStudentNumber').val(r.newStudentNumber);
                                }
                                else {
                                    $('#iptStudentNumber').val(0);
                                }
                            },
                            error: function (xhr, errorType, exception) {
                                var responseText;
                                try {
                                    responseText = jQuery.parseJSON(xhr.responseText);
                                    var errorMessage = "[" + errorType + ", " + exception + "] Exception:" + responseText.ExceptionType + ", StackTrace:" + responseText.StackTrace + ", Message:" + responseText.Message;
                                    console.log(errorMessage);
                                } catch (e) {
                                    responseText = xhr.responseText;
                                    console.log(responseText);
                                }
                            }
                        });
                    }
                }

                var getUrlParameter = function getUrlParameter(sParam) {
                    var sPageURL = window.location.search.substring(1),
                        sURLVariables = sPageURL.split('&'),
                        sParameterName,
                        i;

                    for (i = 0; i < sURLVariables.length; i++) {
                        sParameterName = sURLVariables[i].split('=');

                        if (sParameterName[0] === sParam) {
                            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
                        }
                    }
                    return false;
                };

                function changeFinger() {
                    $userId = getUrlParameter("sid");
                    $.ajax("/App_Logic/deleteDataJSON.ashx?mode=delfinger&userid=" + $userId + "&type=0", function (Result) {
                    }).done(function (result) {
                        $("#modalpopupdata-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111048") %> " + result);
                        $("#modalFingerPopup .modal-footer").addClass("hidden");
                        $('#btnDelFinger').addClass("disabled");
                    });
                }

                function ShowFingerPopup() {
                    var name = $("#iptStudentFirstNameTh").val() + " " + $("#iptStudentLastNameTh").val();
                    $("#modalpopupdata-content").html("ท่านต้องการลบลายนี้มือของ " + name + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603024") %> <br/>เมื่อท่านทำการลบลายนี้วมือแล้วจะไม่สามารถทำรายการได้ ");
                    $("#modalFingerPopup .modal-footer").removeClass("hidden")
                }

                function LoadTermSubLevel2(subLevelID, objResult) {
                    if (subLevelID) {
                        $.ajax({
                            async: false,
                            type: "POST",
                            url: "StdProfile.aspx/LoadTermSubLevel2",
                            data: '{subLevelID: ' + subLevelID + ' }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                var subLevel2 = response.d;

                                $(objResult).empty();

                                if (subLevel2.length > 0) {

                                    var options = '<option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101011") %></option>';
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

                $(document).ready(function () {

                    // Validate rule for stdInfoForm
                    $("#stdInfoForm").validate({
                        rules: {
                            chkTerm: {
                                required: function (element) {
                                    var boxes = $('.checkbox');
                                    if (boxes.filter(':checked').length == 0) {
                                        return true;
                                    }
                                    return false;
                                },
                                minlength: 1
                            },
                            iptStudentID: {
                                required: true,
                                code: true
                            },
                            sltStudentClass: "required",
                            sltStudentClassRoom: "required",
                            sltStudentGender: "required",
                            sltStudentTitle: "required",
                            iptStudentFirstNameTh: "required",
                            iptStudentLastNameTh: "required",
                            iptStudentBirthday: {
                                required: true,
                                thaiDate: true
                            },
                            iptStudentIdentification: {
                                required: false,
                                maxlength: 13,
                                minlength: 13,
                            },
                            iptStudentPhone: {
                                required: false,
                                number: true
                            },
                            iptStudentMoveInDate: {
                                required: false,
                                thaiDate: true
                            },
                            iptStudentDayQuit: {
                                required: function (element) {
                                    return $("#sltStudentStatus").val() != '0' && !$(element).val();
                                },
                                thaiDate: true
                            },
                            iptStudentNote: {
                                required: function (element) {
                                    return $("#sltStudentStatus").val() != '0' && !$(element).val();
                                }
                            },
                            iptStudentEmail: {
                                required: false,
                                email2: true
                            },
                            iptStudentSonNumber: {
                                required: false,
                                number: true
                            },
                            iptChangeRoomDate: {
                                required: function (element) {
                                    return $("#sltStudentClassRoom").val() != $("#sltStudentClassRoom").attr('data-classId') && xStdKey.sid != '0' && !$(element).val();
                                },
                                thaiDate: true
                            },
                            iptnMax: {
                                required: true,
                                number: true
                            },
                            rdoDropOutType: {
                                required: true,
                            },
                            iptPassportExpirationDate: {
                                required: false,
                                thaiDate: true
                            }
                        },
                        messages: {
                            chkTerm: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111049") %>",
                            iptStudentID: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            sltStudentClass: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            sltStudentClassRoom: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            sltStudentGender: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            sltStudentTitle: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptStudentFirstNameTh: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptStudentLastNameTh: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptStudentBirthday: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            iptStudentIdentification: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                                maxlength: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111041") %>",
                                minlength: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111041") %>",
                            },
                            iptStudentPhone: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                                number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                            },
                            iptStudentMoveInDate: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            iptStudentDayQuit: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            iptStudentNote: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            iptStudentEmail: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            iptStudentSonNumber: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                                number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                            },
                            iptChangeRoomDate: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            },
                            iptnMax: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                                number: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111028") %>"
                            },
                            rdoDropOutType: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111050") %>",
                            },
                            iptPassportExpirationDate: {
                                required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                            }
                        },
                        focusInvalid: false,
                        invalidHandler: function () {
                            $(this).find(":input.error:first").focus();
                        },
                        errorPlacement: function (error, element) {
                            switch (element.attr("name")) {
                                case "iptStudentBirthday":
                                case "iptStudentMoveInDate":
                                case "iptStudentDayQuit":
                                case "iptChangeRoomDate": error.insertAfter(element); break;
                                case "rdoDropOutType": error.insertAfter($('.drop-out')); break;
                                case "iptStudentID": error.insertAfter(element.parent().parent().find('.text-danger')); break;
                                case "sltStudentClass":
                                case "sltStudentClassRoom":
                                case "sltStudentGender":
                                case "sltStudentTitle": error.insertAfter(element.parent()); break;
                                case "chkTerm": error.css('display', 'block').insertAfter(element.parent().parent().parent().find('[data-toggle="tooltip"]')); break;
                                case "iptStudentNote": error.insertAfter(element.parent()); break;
                                default: error.insertAfter(element); break;
                            }
                        }
                    });

                    function GetStudentDataFromInput() {

                        var studentData = new Array();

                        studentData[0] = xStdKey.sid + "-" + xStdKey.tid;
                        studentData[1] = $("#iptStudentID").val();
                        studentData[2] = $("#sltStudentStatus").val();
                        studentData[3] = $("#iptStudentMoveInDate").val();
                        studentData[4] = $("#iptStudentDayQuit").val();
                        studentData[5] = $("#iptStudentNumber").val();
                        studentData[6] = $("#iptStudentNote").val();
                        studentData[7] = $("#sltStudentClass").val();
                        studentData[8] = $("#sltStudentClassRoom").val();
                        studentData[9] = $("#sltStudentGender").val();
                        studentData[10] = $("#sltStudentTitle").val();
                        studentData[11] = $("#iptStudentFirstNameTh").val();
                        studentData[12] = $("#iptStudentLastNameTh").val();
                        studentData[13] = $("#iptStudentFirstNameEn").val();
                        studentData[14] = $("#iptStudentLastNameEn").val();
                        studentData[15] = $("#iptStudentFirstNameOther").val();
                        studentData[16] = $("#iptStudentLastNameOther").val();
                        studentData[17] = $("#iptStudentNickNameTh").val();
                        studentData[18] = $("#iptStudentNickNameEn").val();
                        studentData[19] = $("#iptStudentBirthday").val();
                        studentData[20] = $("#iptStudentIdentification").val();
                        studentData[21] = $("#sltStudentRace").val();
                        studentData[22] = $("#sltStudentNation").val();
                        studentData[23] = $("#sltStudentReligion").val();
                        studentData[24] = $("#iptStudentPhone").val();
                        studentData[25] = $("#iptStudentEmail").val();
                        studentData[26] = $("#sltStudentSonTotal").val();
                        studentData[27] = $("#iptStudentSonNumber").val();
                        studentData[28] = $("#sltStudentBrethrenStudyHere").val();

                        studentData[29] = $("#divProfileImage").attr("data-remove-old-picture");

                        studentData[30] = $('#iptUseBiometric').prop('checked');
                        studentData[31] = $('#iptNote2').val();
                        studentData[32] = $("#iptChangeRoomDate").val();

                        studentData[33] = $("#sltDisabilityCode").val();
                        studentData[34] = $("#sltDisadvantageCode").val();
                        studentData[35] = $("#iptnMax").val();

                        studentData[36] = typeof $('input[name=iptJourneyType]:checked').val() === "undefined" ? '' : $('input[name=iptJourneyType]:checked').val();
                        studentData[37] = $("#iptDormitoryName").val();

                        studentData[38] = typeof $('input[name=rdoDropOutType]:checked').val() === "undefined" ? '' : $('input[name=rdoDropOutType]:checked').val();

                        var termData = [];
                        $('.choose-term').each(function () {
                            termData.push({ termID: $(this).attr('data-id'), termCheck: $(this).prop('checked'), flag: $(this).attr('data-flag'), current: $(this).attr('data-current'), currentYear: $(this).attr('data-current-year') });
                        });
                        studentData[39] = JSON.stringify(termData);

                        studentData[40] = $("#iptPassportNumber").val();
                        studentData[41] = $("#iptPassportCountry").val();
                        studentData[42] = $("#iptPassportExpirationDate").val();

                        return studentData;
                    }

                    $(".stdInfoForm #saveStudent").bind({
                        click: function () {

                            var canSave = true;

                            if (($("#sltStudentStatus").val() == "0" && stdInfoForm.MemStudentStatus != '0' && '<%=Request.QueryString["sid"]%>' != '0') || '<%=Request.QueryString["sid"]%>' == '0') {
                                $.ajax({
                                    async: false,
                                    type: "POST",
                                    url: "/App_Logic/StudentLimitInContact.ashx",
                                    data: '{}',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (response) {
                                        // console.log(response);
                                        if (response.success) {
                                            if (response.data.limitInContact > 0 && response.data.remainingNumber > 0) {

                                            }
                                            else {
                                                systemMessage.LimitInContact(response);

                                                canSave = false;
                                            }
                                        }
                                        else {
                                            console.log(response);
                                        }
                                    },
                                    failure: function (response) {
                                        console.log(response);
                                    },
                                    error: function (response) {
                                        console.log(response);
                                    }
                                });
                            }
                            //

                            if (canSave) {
                                if ($('#stdInfoForm').valid()) {

                                    $('#modalNotifyConfirmSave').modal('show');

                                    // Modal Section
                                    $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                                        $('#modalNotifyConfirmSave').modal('hide');

                                        $("#modalWaitDialog").modal('show');

                                        // Save command
                                        var data = GetStudentDataFromInput();

                                        stdInfoForm.SaveItem(data);

                                    });
                                }
                            }

                            return false;
                        }
                    });

                    $(".stdInfoForm #saveStudentAndNext").bind({
                        click: function () {

                            var canSave = true;

                            if (($("#sltStudentStatus").val() == "0" && stdInfoForm.MemStudentStatus != '0' && '<%=Request.QueryString["sid"]%>' != '0') || '<%=Request.QueryString["sid"]%>' == '0') {
                                $.ajax({
                                    async: false,
                                    type: "POST",
                                    url: "/App_Logic/StudentLimitInContact.ashx",
                                    data: '{}',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (response) {
                                        // console.log(response);
                                        if (response.success) {
                                            if (response.data.limitInContact > 0 && response.data.remainingNumber > 0) {

                                            }
                                            else {
                                                systemMessage.LimitInContact(response);

                                                canSave = false;
                                            }
                                        }
                                        else {
                                            console.log(response);
                                        }
                                    },
                                    failure: function (response) {
                                        console.log(response);
                                    },
                                    error: function (response) {
                                        console.log(response);
                                    }
                                });
                            }
                            //

                            if (canSave) {
                                if ($('#stdInfoForm').valid()) {

                                    $('#modalNotifyConfirmSave').modal('show');

                                    // Modal Section
                                    $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                                        $('#modalNotifyConfirmSave').modal('hide');

                                        $("#modalWaitDialog").modal('show');

                                        // Save command
                                        var data = GetStudentDataFromInput();

                                        stdInfoForm.SaveAndNextItem(data);

                                    });
                                }
                            }

                            return false;
                        }
                    });

                    $(".stdInfoForm .btn-cancel").bind({
                        click: function () {

                            // Redirect to employee list
                            stdInfoForm.ClearSession(function () {
                                window.location.replace("StudentList.aspx");
                            });

                            return false;
                        }
                    });

                    // Modal Section
                    $('#modalNotifyConfirmSave').off().on('show.bs.modal', function (e) {
                        $(this).find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101166") %>');
                        $(this).find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101167") %>');
                    });

                    $("#sltStudentClass").change(function () {

                        LoadTermSubLevel2($("#sltStudentClass").val(), '#sltStudentClassRoom');

                    });

                    $("#sltStudentClassRoom").change(function () {
                        if ($(this).val() != null && $(this).val() != $(this).attr('data-classId') && xStdKey.sid != '0') {
                            stdInfoForm.CheckScoreEntered(<%=Request.QueryString["sid"]%>, '<%=Request.QueryString["tid"]%>', $(this).attr('data-classId'), $(this).val());
                        }
                        else {
                            $('.change-room').hide();
                        }

                        // Get last student number in classroom
                        if (xStdKey.sid == '0') {
                            stdInfoForm.GetLastStudentNumberInClassroom($(this).val());
                        }
                    });

                    $("#sltStudentStatus").on('show.bs.select', function (e, clickedIndex) {
                        $("#sltStudentStatus").data('choice', $("#sltStudentStatus").val());
                        stdInfoForm.ClickChangeStudentStatus = true;
                    }).on('hide.bs.select', function (e, clickedIndex) {
                        stdInfoForm.ClickChangeStudentStatus = false;
                    }).change(function () {
                        // Check student limit in contact
                        if ($(this).val() == "0" && '<%=Request.QueryString["sid"]%>' != '0' && stdInfoForm.ClickChangeStudentStatus && stdInfoForm.MemStudentStatus != '0') {
                            $.ajax({
                                async: false,
                                type: "POST",
                                url: "/App_Logic/StudentLimitInContact.ashx",
                                data: '{}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (response) {
                                    // console.log(response);
                                    if (response.success) {
                                        if (response.data.limitInContact > 0 && response.data.remainingNumber > 0) {
                                            // ..
                                        }
                                        else {
                                            systemMessage.LimitInContact(response);
                                            $("#sltStudentStatus").selectpicker('val', $("#sltStudentStatus").data('choice'));
                                            return false;
                                        }
                                    }
                                    else {
                                        console.log(response);
                                    }
                                },
                                failure: function (response) {
                                    console.log(response);
                                },
                                error: function (response) {
                                    console.log(response);
                                }
                            });
                        }
                        //

                        if ($(this).val() != "0") {

                            $("#spnStudentDayQuit").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>" + $("#sltStudentStatus option:selected").text() + " :");
                            $("#iptStudentDayQuit").attr("placeholder", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>" + $("#sltStudentStatus option:selected").text());

                            $("#iptStudentDayQuit").prop('disabled', false);
                            $("#iptStudentNote").prop('disabled', false);
                            $("#divStudentNote button").prop('disabled', false);

                        } else {

                            $("#spnStudentDayQuit").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> :");
                            $("#iptStudentDayQuit").attr("placeholder", "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>");

                            $('#iptStudentDayQuit').data("DateTimePicker").clear();

                            $("#iptStudentDayQuit").prop('disabled', true);
                            $("#iptStudentNote").prop('disabled', true);
                            $("#iptStudentNote").val('');
                            $("#divStudentNote button").prop('disabled', true);
                        }

                        // 1: <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101033") %>, 2: <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101032") %>, 3: <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101034") %>, 5: <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101035") %>, 6: <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101036") %>, 7: <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101037") %>
                        if ($.inArray($(this).val(), ["0", "4"]) == -1) {
                            $('.drop-out').show();

                            if ($.inArray($("#sltStudentStatus").val(), ["2", "3", "6", "7"]) != -1) {
                                $('#divStudentNote ul').empty().html(`
                                        <li><a class="note-studentstatus-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101056") %></a></li>
                                        <li><a class="note-studentstatus-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101057") %></a></li>
                                        <li><a class="note-studentstatus-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101058") %></a></li>
                                        <li><a class="note-studentstatus-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101059") %></a></li>
                                        <li><a class="note-other"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></a></li>`);
                            }
                            else if ($.inArray($("#sltStudentStatus").val(), ["1", "5"]) != -1) {
                                $('#divStudentNote ul').empty().html(`
                                        <li><a class="note-studentstatus-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101053") %></a></li>
                                        <li><a class="note-studentstatus-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101054") %></a></li>
                                        <li><a class="note-studentstatus-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101055") %></a></li>
                                        <li><a class="note-other"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></a></li>`);
                            }
                            $('#iptStudentNote').val('');
                        }
                        else {
                            $('.drop-out').hide();

                            $('#stdInfoForm').valid();
                        }

                    });

                    $('#divStudentNote').on('click', 'ul a', function () {

                        if ($(this).attr('class') == 'note-other') {
                            $("#iptStudentNote").prop('disabled', false);
                            $('#iptStudentNote').val('').focus();

                        }
                        else {
                            $('#iptStudentNote').val($(this).text());
                            $("#iptStudentNote").prop('disabled', true);
                        }

                        $("#divStudentNote .dropdown-toggle").dropdown('toggle');

                        return false;
                    });

                    $("#divProfileImage .img-photo-original").on("load", function () {
                        var image = $(this).get(0);
                        if (image.height >= 1280 && image.width >= 720) {
                            $(".stdProfile #divProfileImage .div-crop").show();
                        }
                    });

                    // Initial data

                    $('#iptStudentSonNumber').number(true, 0);

                    //$('#divStudentMoveInDate, #divStudentDayQuit, #divStudentBirthday, #divChangeRoomDate').datetimepicker({
                    //    format: 'DD/MM/YYYY-BE',
                    //    locale: 'th'
                    //});
                    $('.stdInfoForm .datepicker').datetimepicker({
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

                    $(".stdInfoForm .datepicker").attr('maxlength', '10');

                    $('#iptUseBiometric').bootstrapToggle();

                    $('#iptnMax').number(true, 2);

                    activateBootstrapSelect('.stdInfoForm .selectpicker');

                    $('[data-toggle="tooltip"]').tooltip({
                        container: 'body'
                    });

                    $(document).on('change', 'input:radio[id^="iptJourneyType"]', function (event) {
                        switch ($(this).val()) {
                            case '2': $('#divDormitory').show(); break;
                            default: $('#divDormitory').hide(); break;
                        }
                    });

                    // Load info command
                    stdInfoForm.GetItem('<%=Request.QueryString["sid"]%>', '<%=Request.QueryString["tid"]%>');

                });

            </script>
        </asp:View>
        <asp:View ID="ViewContent" runat="server">
            <div class="stdInfoView view-form">
                <form id="stdInfoView" class="form-padding">
                    <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101029") %></p>
                    <div class="row div-row-padding" style="margin-top: 30px;">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> :</label>
                        </div>
                        <div class="col-md-10 mb-10 checkbox-radios" style="white-space: nowrap;">
                            <asp:Literal ID="ltrTermView" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span" style="white-space: nowrap;">
                            <span class="span-data" id="spStudentID"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101021") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentStatus"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101050") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentMoveInDate"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label id="spStudentDayQuitView"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102097") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentDayQuit"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101051") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentNumber"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102220") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentNote"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentClass"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentClassRoom"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding change-room" style="display: none;">
                        <div class="col-md-2 mb-2 col-form-label text-right"></div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111042") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spChangeRoomDate"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101061") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentGender"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101017") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentTitle"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101065") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentFirstNameTh"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101066") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentLastNameTh"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101067") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentFirstNameEn"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101069") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentLastNameEn"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101071") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentFirstNameOther"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101072") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentLastNameOther"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101073") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentNickNameTh"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101074") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentNickNameEn"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentBirthday"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101076") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentIdentification"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101077") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentRace"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101079") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentNation"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101081") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentReligion"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label></label>
                        </div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101086") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spDisabilityCode"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101097") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spDisadvantageCode"></span>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentPhone"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101110") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentEmail"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101111") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentSonTotal"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101113") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentSonNumber"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101114") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spStudentBrethrenStudyHere"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101116") %> :</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spUseBiometric"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101119") %>:</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spnMax"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right">
                            <label>
                                <asp:Literal ID="ltrFingerView" runat="server"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111043") %> :</asp:Literal>
                            </label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <asp:Literal ID="ltrPasswordView" runat="server"></asp:Literal>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101121") %>:</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spJourneyType"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"></div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div id="divDormitoryView" class="row div-row-padding" style="display: none;">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111045") %>:</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spDormitoryName"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"></div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101124") %>:</label>
                        </div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spNote2"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right" for="">
                            <label></label>
                        </div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                    </div>
                    <div class="row text-center" style="margin-top: 7px; margin-bottom: 7px;">
                        <button type="button"
                            class="btn btn-danger btn-cancel" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </form>
            </div>

            <script type="text/javascript">

                var stdInfoView = {
                    GetItem: function (stdID, termID) {
                        $.ajax({
                            type: "POST",
                            url: "StdProfile.aspx/GetItemView",
                            data: '{stdID: ' + stdID + ', termID: \'' + termID + '\'}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdInfoView.OnSuccessGet,
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
                            xStdKey.fmlid = "0";

                            $('#studentTabs li.nav-item:gt(0)').addClass("disabled");
                            $('#studentTabs li.nav-item:gt(0)').find('a').removeAttr('data-toggle');

                        } else {
                            var xmlDoc = $.parseXML(response.d);
                            var xml = $(xmlDoc);
                            var infors = xml.find("Table1");

                            $.each(infors, function (index) {
                                var infor = $(this);

                                xStdKey.fmlid = $(this).find("F0").text();

                                $("#spStudentID").text($(this).find("F1").text());
                                $("#spStudentStatus").text($(this).find("F2").text());

                                if ($("#spStudentStatus").text() != "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101031") %>") {
                                    $("#spStudentDayQuitView").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>" + $("#spStudentStatus").text() + " :");
                                } else {
                                    $("#spStudentDayQuitView").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> :");
                                }

                                $("#spStudentMoveInDate").text($(this).find("F3").text());
                                $("#spStudentDayQuit").text($(this).find("F4").text());
                                $('#spStudentNumber').text($(this).find("F5").text());
                                $("#spStudentNote").text($(this).find("F6").text());
                                $("#spStudentClass").text($(this).find("F7").text());
                                $("#spStudentClassRoom").text($(this).find("F8").text());
                                $("#spStudentGender").text($(this).find("F9").text());
                                $("#spStudentTitle").text($(this).find("F10").text());
                                $("#spStudentFirstNameTh").text($(this).find("F11").text());
                                $("#spStudentLastNameTh").text($(this).find("F12").text());
                                $("#spStudentFirstNameEn").text($(this).find("F13").text());
                                $("#spStudentLastNameEn").text($(this).find("F14").text());
                                $("#spStudentFirstNameOther").text($(this).find("F15").text());
                                $("#spStudentLastNameOther").text($(this).find("F16").text());
                                $("#spStudentNickNameTh").text($(this).find("F17").text());
                                $("#spStudentNickNameEn").text($(this).find("F18").text());
                                $("#spStudentBirthday").text($(this).find("F19").text());
                                $("#spStudentIdentification").text($(this).find("F20").text());
                                $("#spStudentRace").text($(this).find("F21").text());
                                $("#spStudentNation").text($(this).find("F22").text());
                                $("#spStudentReligion").text($(this).find("F23").text());
                                $("#spStudentPhone").text($(this).find("F24").text());
                                $("#spStudentEmail").text($(this).find("F25").text());
                                $("#spStudentSonTotal").text($(this).find("F26").text());
                                $("#spStudentSonNumber").text($(this).find("F27").text());
                                $("#spStudentBrethrenStudyHere").text($(this).find("F28").text());

                                if (!$.isBlank($(this).find("F29").text())) {
                                    let divProfileImage = $(this).find("F29").text();

                                    if ($(this).find("F29").text().indexOf("?x-image-process=image/resize,m_fill,h_300,w_270") != -1) {
                                        divProfileImage += '&v=' + (new Date()).getTime() * 1e4
                                    } else {
                                        divProfileImage += '?v=' + (new Date()).getTime() * 1e4
                                    }

                                    $("#divProfileImage .img-photo").attr("src", divProfileImage);
                                }

                                $('#spUseBiometric').text($(this).find("F30").text());
                                $("#spNote2").text($(this).find("F31").text());

                                $("#spDisabilityCode").text($(this).find("F33").text());
                                $("#spDisadvantageCode").text($(this).find("F34").text());
                                $("#spnMax").text($(this).find("F35").text());

                                $("#spJourneyType").text($(this).find("F36").text());
                                if ($(this).find("F36").text() == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101123") %>") {
                                    $('#divDormitoryView').show();
                                }
                                $("#spDormitoryName").text($(this).find("F37").text());
                            });

                        }
                    }
                }

                $(document).ready(function () {

                    $(".stdInfoView .btn-cancel").bind({
                        click: function () {

                            window.close();

                            return false;
                        }
                    });

                    $('[data-toggle="tooltip"]').tooltip({
                        container: 'body'
                    });

                    // Load info command
                    stdInfoView.GetItem('<%=Request.QueryString["sid"]%>', '<%=Request.QueryString["tid"]%>');

                });

            </script>

        </asp:View>
    </asp:MultiView></body>
</html>
