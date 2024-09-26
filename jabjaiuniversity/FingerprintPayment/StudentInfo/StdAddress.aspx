<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StdAddress.aspx.cs" Inherits="FingerprintPayment.StudentInfo.StdAddress" %>

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
            <div class="stdAddressForm">
                <form id="stdAddressForm" class="form-padding">
                    <%--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101148") %>--%>
                    <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101129") %></p>
                    <div class="row div-row-padding" style="margin-top: 30px;">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptRegisterHomeCode"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101130") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptRegisterHomeCode" name="iptRegisterHomeCode"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101130") %>" maxlength="20" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptRegisterHomeNo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptRegisterHomeNo" name="iptRegisterHomeNo"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptRegisterHomeSoi"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptRegisterHomeSoi" name="iptRegisterHomeSoi"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptRegisterHomeMoo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptRegisterHomeMoo" name="iptRegisterHomeMoo"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptRegisterHomeRoad"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptRegisterHomeRoad" name="iptRegisterHomeRoad"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltRegisterHomeProvince"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltRegisterHomeProvince" name="sltRegisterHomeProvince" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %></option>
                                <asp:Literal ID="ltrRegisterHomeProvince" runat="server"></asp:Literal>
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltRegisterHomeAmphoe"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltRegisterHomeAmphoe" name="sltRegisterHomeAmphoe" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %></option>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltRegisterHomeTombon"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltRegisterHomeTombon" name="sltRegisterHomeTombon" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %></option>
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptRegisterHomePostalCode"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptRegisterHomePostalCode" name="iptRegisterHomePostalCode"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %>" maxlength="10" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptRegisterHomePhone"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101142") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptRegisterHomePhone" name="iptRegisterHomePhone"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101142") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptBornFrom"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101143") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptBornFrom" name="iptBornFrom"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101143") %>" maxlength="100" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltBornFromProvince"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101144") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltBornFromProvince" name="sltBornFromProvince" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01933") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101144") %></option>
                                <asp:Literal ID="ltrBornFromProvince" runat="server"></asp:Literal>
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltBornFromAmphoe"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101145") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltBornFromAmphoe" name="sltBornFromAmphoe" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01931") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101145") %></option>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltBornFromTombon"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101146") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltBornFromTombon" name="sltBornFromTombon" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01932") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101146") %></option>
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                    </div>
                    <div class="row text-center">
                        <button id="saveRegisterHome" type="submit" class="btn btn-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                        <button id="saveRegisterHomeAndNext" type="submit" class="btn btn-info save-next"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101126") %></button>
                        <button id="btnCancelStdRegisterHome" type="button" class="btn btn-danger btn-cancel" data-dismiss="modal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </form>
                <form id="stdContactAddressForm" class="form-padding">
                    <%--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102029") %>--%>
                    <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101147") %></p>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label></label></div>
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
                        <div class="col-md-3 mb-3 col-form-label text-right"><label></label></div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptHomeNo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptHomeNo" name="iptHomeNo"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label></label></div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptHomeSoi"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptHomeSoi" name="iptHomeSoi"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptHomeMoo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptHomeMoo" name="iptHomeMoo"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102031") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptHomeRoad"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptHomeRoad" name="iptHomeRoad"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltHomeProvince"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltHomeProvince" name="sltHomeProvince" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101136") %></option>
                                <asp:Literal ID="ltrHomeProvince" runat="server"></asp:Literal>
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltHomeAmphoe"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltHomeAmphoe" name="sltHomeAmphoe" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %></option>
                            </select>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltHomeTombon"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltHomeTombon" name="sltHomeTombon" class="selectpicker col-sm-12" data-live-search="true" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %></option>
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptHomePostalCode"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptHomePostalCode" name="iptHomePostalCode"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %>" maxlength="10" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptHomePhone"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101142") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptHomePhone" name="iptHomePhone"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101142") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="sltHomeStayWithTitle"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101149") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltHomeStayWithTitle" name="sltHomeStayWithTitle" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111013") %>">
                                <option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101150") %></option>
                                <asp:Literal ID="ltrHomeStayWithTitle" runat="server"></asp:Literal>
                            </select>
                        </div>
                        <label class="col-md-3 mb-3 col-form-label text-right"><label></label></label>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptHomeStayWithName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101151") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptHomeStayWithName" name="iptHomeStayWithName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101151") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptHomeStayWithLast"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101152") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptHomeStayWithLast" name="iptHomeStayWithLast"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101152") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptHomeStayWithEmergencyCall"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101153") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptHomeStayWithEmergencyCall" name="iptHomeStayWithEmergencyCall"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101153") %>" maxlength="20" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptHomeStayWithEmergencyEmail"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101154") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptHomeStayWithEmergencyEmail" name="iptHomeStayWithEmergencyEmail"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101154") %>" maxlength="100" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptHomeFriendName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101155") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptHomeFriendName" name="iptHomeFriendName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101155") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="iptHomeFriendLastName"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101156") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptHomeFriendLastName" name="iptHomeFriendLastName"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101156") %>" maxlength="50" />
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label for="iptHomeFriendPhone"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101157") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-input">
                            <input type="text" class="form-control" id="iptHomeFriendPhone" name="iptHomeFriendPhone"
                                placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101157") %>" maxlength="50" />
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label for="sltHomeHomeType"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101158") %> :</label></div>
                        <div class="col-md-3 mb-3 div-select-input">
                            <select id="sltHomeHomeType" name="sltHomeHomeType" class="selectpicker col-sm-12" data-style="select-with-transition" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01927") %>">
                                <option selected="selected" value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101158") %></option>
                                <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101159") %></option>
                                <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101160") %></option>
                                <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101161") %></option>
                                <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101162") %></option>
                                <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101163") %></option>
                            </select>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                    </div>
                    <div class="row text-center" style="margin-bottom: 7px;">
                        <button id="saveHome" type="submit" class="btn btn-success"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                        <button id="saveHomeAndNext" type="submit" class="btn btn-info save-next"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101126") %></button>
                        <button id="btnCancelHome" type="button" class="btn btn-danger btn-cancel" data-dismiss="modal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </form>
            </div>
            <script type="text/javascript">

                var stdAddressForm = {
                    GetAddressItem: function (stdID) {
                        $.ajax({
                            type: "POST",
                            url: "StdAddress.aspx/GetAddressItem",
                            data: '{stdID: ' + stdID + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdAddressForm.OnSuccessGetAddress,
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
                    OnSuccessGetAddress: function (response) {
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
                                $("#iptRegisterHomeCode").val($(this).find("F1").text());
                                $("#iptRegisterHomeNo").val($(this).find("F2").text());
                                $("#iptRegisterHomeSoi").val($(this).find("F3").text());
                                $("#iptRegisterHomeMoo").val($(this).find("F4").text());
                                $("#iptRegisterHomeRoad").val($(this).find("F5").text());
                                $("#sltRegisterHomeProvince").selectpicker('val', $(this).find("F6").text());
                                LoadDistrict('#sltRegisterHomeAmphoe', $("#sltRegisterHomeProvince").val());
                                $("#sltRegisterHomeAmphoe").selectpicker('val', $(this).find("F7").text());
                                LoadSubDistrict('#sltRegisterHomeTombon', $("#sltRegisterHomeAmphoe").val());
                                $("#sltRegisterHomeTombon").selectpicker('val', $(this).find("F8").text());
                                $("#iptRegisterHomePostalCode").val($(this).find("F9").text());
                                $("#iptRegisterHomePhone").val($(this).find("F10").text());
                                $("#iptBornFrom").val($(this).find("F11").text());

                                $("#sltBornFromProvince").selectpicker('val', $(this).find("F12").text());
                                LoadDistrict('#sltBornFromAmphoe', $("#sltBornFromProvince").val());
                                $("#sltBornFromAmphoe").selectpicker('val', $(this).find("F13").text());
                                LoadSubDistrict('#sltBornFromTombon', $("#sltBornFromAmphoe").val());
                                $("#sltBornFromTombon").selectpicker('val', $(this).find("F14").text());

                                CheckedSameAddress();
                            });

                        }
                    },
                    SaveAddressItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "StdAddress.aspx/SaveAddressItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdAddressForm.OnSuccessSaveAddress,
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
                    OnSuccessSaveAddress: function (response) {
                        var title = "";
                        var body = "";

                        var flag = response.d.split('-');
                        switch (flag[0]) {
                            case "complete":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';

                                xStdKey.fmlid = flag[1];

                                $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {

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
                    SaveAndNextAddressItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "StdAddress.aspx/SaveAddressItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdAddressForm.OnSuccessSaveAndNextAddress,
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
                    OnSuccessSaveAndNextAddress: function (response) {
                        var title = "";
                        var body = "";

                        var flag = response.d.split('-');
                        switch (flag[0]) {
                            case "complete":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';

                                $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {

                                    // Redirect to student list
                                    window.location.replace("StudentDetail.aspx?v=form&sid=" + StudentDetail_NextSID + "&tid=" + xStdKey.tid);

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
                    GetContactAddressItem: function (stdID) {
                        $.ajax({
                            type: "POST",
                            url: "StdAddress.aspx/GetContactAddressItem",
                            data: '{stdID: ' + stdID + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdAddressForm.OnSuccessGetContactAddress,
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
                    OnSuccessGetContactAddress: function (response) {
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
                                $("#iptHomeNo").val($(this).find("F1").text());
                                $("#iptHomeSoi").val($(this).find("F2").text());
                                $("#iptHomeMoo").val($(this).find("F3").text());
                                $("#iptHomeRoad").val($(this).find("F4").text());
                                $("#sltHomeProvince").selectpicker('val', $(this).find("F5").text());
                                LoadDistrict('#sltHomeAmphoe', $("#sltHomeProvince").val());
                                $("#sltHomeAmphoe").selectpicker('val', $(this).find("F6").text());
                                LoadSubDistrict('#sltHomeTombon', $("#sltHomeAmphoe").val());
                                $("#sltHomeTombon").selectpicker('val', $(this).find("F7").text());
                                $("#iptHomePostalCode").val($(this).find("F8").text());
                                $("#iptHomePhone").val($(this).find("F9").text());

                                $("#sltHomeStayWithTitle").selectpicker('val', $(this).find("F10").text());
                                $("#iptHomeStayWithName").val($(this).find("F11").text());
                                $("#iptHomeStayWithLast").val($(this).find("F12").text());
                                $("#iptHomeStayWithEmergencyCall").val($(this).find("F13").text());
                                $("#iptHomeStayWithEmergencyEmail").val($(this).find("F14").text());
                                $("#iptHomeFriendName").val($(this).find("F15").text());
                                $("#iptHomeFriendLastName").val($(this).find("F16").text());
                                $("#iptHomeFriendPhone").val($(this).find("F17").text());
                                $("#sltHomeHomeType").selectpicker('val', $(this).find("F18").text());

                                CheckedSameAddress();
                            });

                        }
                    },
                    SaveContactAddressItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "StdAddress.aspx/SaveContactAddressItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdAddressForm.OnSuccessSaveContactAddress,
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
                    OnSuccessSaveContactAddress: function (response) {
                        var title = "";
                        var body = "";

                        var flag = response.d.split('-');
                        switch (flag[0]) {
                            case "complete":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';

                                xStdKey.fmlid = flag[1];

                                $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {

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
                    SaveAndNextContactAddressItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "StdAddress.aspx/SaveContactAddressItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdAddressForm.OnSuccessSaveAndNextContactAddress,
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
                    OnSuccessSaveAndNextContactAddress: function (response) {
                        var title = "";
                        var body = "";

                        var flag = response.d.split('-');
                        switch (flag[0]) {
                            case "complete":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';

                                $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').off().on('click', function () {

                                    // Redirect to student list
                                    window.location.replace("StudentDetail.aspx?v=form&sid=" + StudentDetail_NextSID + "&tid=" + xStdKey.tid);

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
                            url: "StdAddress.aspx/ClearSessionID",
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

                function CheckedSameAddress() {
                    var isSame = (($("#iptHomeNo").val() == $("#iptRegisterHomeNo").val()) && !!$("#iptRegisterHomeNo").val()
                        && ($("#iptHomeSoi").val() == $("#iptRegisterHomeSoi").val()) && !!$("#iptRegisterHomeSoi").val()
                        && ($("#iptHomeMoo").val() == $("#iptRegisterHomeMoo").val()) && !!$("#iptRegisterHomeMoo").val()
                        && ($("#iptHomeRoad").val() == $("#iptRegisterHomeRoad").val()) && !!$("#iptRegisterHomeRoad").val()
                        && ($("#sltHomeProvince").val() == $("#sltRegisterHomeProvince").val()) && !!$("#sltRegisterHomeProvince").val()
                        && ($("#sltHomeAmphoe").val() == $("#sltRegisterHomeAmphoe").val()) && !!$("#sltRegisterHomeAmphoe").val()
                        && ($("#sltHomeTombon").val() == $("#sltRegisterHomeTombon").val()) && !!$("#sltRegisterHomeTombon").val()
                        && ($("#iptHomePostalCode").val() == $("#iptRegisterHomePostalCode").val()) && !!$("#iptRegisterHomePostalCode").val()
                        && ($("#iptHomePhone").val() == $("#iptRegisterHomePhone").val()) && !!$("#iptRegisterHomePhone").val());
                    $('input[name=UseHouseAddress]').prop("checked", isSame);
                }

                $(document).ready(function () {

                    // Validate rule for stdAddressForm
                    $("#stdAddressForm").validate({
                        rules: {
                            iptRegisterHomeNo: "required",
                            sltRegisterHomeProvince: "required",
                            sltRegisterHomeAmphoe: "required",
                            sltRegisterHomeTombon: "required",
                            iptRegisterHomePostalCode: "required"
                        },
                        messages: {
                            iptRegisterHomeNo: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            sltRegisterHomeProvince: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            sltRegisterHomeAmphoe: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            sltRegisterHomeTombon: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptRegisterHomePostalCode: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                        },
                        focusInvalid: false,
                        invalidHandler: function () {
                            $(this).find(":input.error:first").focus();
                        },
                        errorPlacement: function (error, element) {
                            switch (element.attr("name")) {
                                case "iptRegisterHomeNo":
                                case "iptRegisterHomePostalCode": error.insertAfter(element); break;
                                case "sltRegisterHomeProvince":
                                case "sltRegisterHomeAmphoe":
                                case "sltRegisterHomeTombon": error.insertAfter(element.parent()); break;
                                default: error.insertAfter(element); break;
                            }
                        }
                    });

                    // Validate rule for stdContactAddressForm
                    $("#stdContactAddressForm").validate({
                        rules: {
                            iptHomeNo: "required",
                            sltHomeProvince: "required",
                            sltHomeAmphoe: "required",
                            sltHomeTombon: "required",
                            iptHomePostalCode: "required"
                        },
                        messages: {
                            iptHomeNo: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            sltHomeProvince: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            sltHomeAmphoe: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            sltHomeTombon: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>",
                            iptHomePostalCode: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101044") %>"
                        },
                        focusInvalid: false,
                        invalidHandler: function () {
                            $(this).find(":input.error:first").focus();
                        },
                        errorPlacement: function (error, element) {
                            switch (element.attr("name")) {
                                case "iptHomeNo":
                                case "iptHomePostalCode": error.insertAfter(element); break;
                                case "sltHomeProvince":
                                case "sltHomeAmphoe":
                                case "sltHomeTombon": error.insertAfter(element.parent()); break;
                                default: error.insertAfter(element); break;
                            }
                        }
                    });

                    function GetRegisterHomeDataFromInput() {

                        var registerHomeData = new Array();

                        registerHomeData[0] = xStdKey.sid + "-" + xStdKey.fmlid;
                        registerHomeData[1] = $("#iptRegisterHomeCode").val();
                        registerHomeData[2] = $("#iptRegisterHomeNo").val();
                        registerHomeData[3] = $("#iptRegisterHomeSoi").val();
                        registerHomeData[4] = $("#iptRegisterHomeMoo").val();
                        registerHomeData[5] = $("#iptRegisterHomeRoad").val();
                        registerHomeData[6] = $("#sltRegisterHomeProvince").val();
                        registerHomeData[7] = $("#sltRegisterHomeAmphoe").val();
                        registerHomeData[8] = $("#sltRegisterHomeTombon").val();
                        registerHomeData[9] = $("#iptRegisterHomePostalCode").val();
                        registerHomeData[10] = $("#iptRegisterHomePhone").val();
                        registerHomeData[11] = $("#iptBornFrom").val();
                        registerHomeData[12] = $("#sltBornFromProvince").val();
                        registerHomeData[13] = $("#sltBornFromAmphoe").val();
                        registerHomeData[14] = $("#sltBornFromTombon").val();

                        return registerHomeData;
                    }

                    $(".stdAddressForm #saveRegisterHome").bind({
                        click: function () {

                            if ($('#stdAddressForm').valid()) {

                                $('#modalNotifyConfirmSave').modal('show');

                                // Modal Section
                                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                                    $('#modalNotifyConfirmSave').modal('hide');

                                    $("#modalWaitDialog").modal('show');

                                    // Save command
                                    var data = GetRegisterHomeDataFromInput();

                                    stdAddressForm.SaveAddressItem(data);

                                });
                            }

                            return false;
                        }
                    });

                    $(".stdAddressForm #saveRegisterHomeAndNext").bind({
                        click: function () {

                            if ($('#stdAddressForm').valid()) {

                                $('#modalNotifyConfirmSave').modal('show');

                                // Modal Section
                                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                                    $('#modalNotifyConfirmSave').modal('hide');

                                    $("#modalWaitDialog").modal('show');

                                    // Save command
                                    var data = GetRegisterHomeDataFromInput();

                                    stdAddressForm.SaveAndNextAddressItem(data);

                                });
                            }

                            return false;
                        }
                    });

                    function GetHomeDataFromInput() {

                        var homeData = new Array();

                        homeData[0] = xStdKey.sid + "-" + xStdKey.fmlid;
                        homeData[1] = $("#iptHomeNo").val();
                        homeData[2] = $("#iptHomeSoi").val();
                        homeData[3] = $("#iptHomeMoo").val();
                        homeData[4] = $("#iptHomeRoad").val();
                        homeData[5] = $("#sltHomeProvince").val();
                        homeData[6] = $("#sltHomeAmphoe").val();
                        homeData[7] = $("#sltHomeTombon").val();
                        homeData[8] = $("#iptHomePostalCode").val();
                        homeData[9] = $("#iptHomePhone").val();
                        homeData[10] = $("#sltHomeStayWithTitle").val();
                        homeData[11] = $("#iptHomeStayWithName").val();
                        homeData[12] = $("#iptHomeStayWithLast").val();
                        homeData[13] = $("#iptHomeStayWithEmergencyCall").val();
                        homeData[14] = $("#iptHomeStayWithEmergencyEmail").val();
                        homeData[15] = $("#iptHomeFriendName").val();
                        homeData[16] = $("#iptHomeFriendLastName").val();
                        homeData[17] = $("#iptHomeFriendPhone").val();
                        homeData[18] = $("#sltHomeHomeType").val();

                        return homeData;
                    }

                    $(".stdAddressForm #saveHome").bind({
                        click: function () {

                            if ($('#stdContactAddressForm').valid()) {

                                $('#modalNotifyConfirmSave').modal('show');

                                // Modal Section
                                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                                    $('#modalNotifyConfirmSave').modal('hide');

                                    $("#modalWaitDialog").modal('show');

                                    // Save command
                                    var data = GetHomeDataFromInput();

                                    stdAddressForm.SaveContactAddressItem(data);

                                });
                            }

                            return false;
                        }
                    });

                    $(".stdAddressForm #saveHomeAndNext").bind({
                        click: function () {

                            if ($('#stdContactAddressForm').valid()) {

                                $('#modalNotifyConfirmSave').modal('show');

                                // Modal Section
                                $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').off().on('click', function () {
                                    $('#modalNotifyConfirmSave').modal('hide');

                                    $("#modalWaitDialog").modal('show');

                                    // Save command
                                    var data = GetHomeDataFromInput();

                                    stdAddressForm.SaveAndNextContactAddressItem(data);

                                });
                            }

                            return false;
                        }
                    });

                    $(".stdAddressForm .btn-cancel").bind({
                        click: function () {

                            // Redirect to employee list
                            stdAddressForm.ClearSession(function () {
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


                    // Initial data

                    activateBootstrapSelect('.stdAddressForm .selectpicker');

                    // Address - Register Home
                    $("#sltRegisterHomeProvince").change(function () {

                        LoadDistrict('#sltRegisterHomeAmphoe', $("#sltRegisterHomeProvince").val());

                    });
                    $("#sltRegisterHomeAmphoe").change(function () {

                        LoadSubDistrict('#sltRegisterHomeTombon', $("#sltRegisterHomeAmphoe").val());

                    });
                    $("#sltBornFromProvince").change(function () {

                        LoadDistrict('#sltBornFromAmphoe', $("#sltBornFromProvince").val());

                    });
                    $("#sltBornFromAmphoe").change(function () {

                        LoadSubDistrict('#sltBornFromTombon', $("#sltBornFromAmphoe").val());

                    });

                    // ContactAddress - Home
                    $("#sltHomeProvince").change(function () {

                        LoadDistrict('#sltHomeAmphoe', $("#sltHomeProvince").val());

                    });
                    $("#sltHomeAmphoe").change(function () {

                        LoadSubDistrict('#sltHomeTombon', $("#sltHomeAmphoe").val());

                    });

                    $('#UseHouseAddress').change(function () {
                        if ($(this).prop('checked')) {
                            $("#iptHomeNo").val($("#iptRegisterHomeNo").val());
                            $("#iptHomeSoi").val($("#iptRegisterHomeSoi").val());
                            $("#iptHomeMoo").val($("#iptRegisterHomeMoo").val());
                            $("#iptHomeRoad").val($("#iptRegisterHomeRoad").val());
                            $("#sltHomeProvince").selectpicker('val', $("#sltRegisterHomeProvince").val());
                            $('#sltHomeProvince').selectpicker('refresh');

                            var $options1 = $("#sltRegisterHomeAmphoe > option:not(.bs-title-option)").clone();
                            $('#sltHomeAmphoe').empty().append($options1);
                            $('#sltHomeAmphoe').selectpicker('refresh');
                            $("#sltHomeAmphoe").selectpicker('val', $("#sltRegisterHomeAmphoe").val());

                            var $options2 = $("#sltRegisterHomeTombon > option:not(.bs-title-option)").clone();
                            $('#sltHomeTombon').empty().append($options2);
                            $('#sltHomeTombon').selectpicker('refresh');
                            $("#sltHomeTombon").selectpicker('val', $("#sltRegisterHomeTombon").val());

                            $("#iptHomePostalCode").val($("#iptRegisterHomePostalCode").val());
                            $("#iptHomePhone").val($("#iptRegisterHomePhone").val());
                        }
                    })

                    // Load info command
                    stdAddressForm.GetAddressItem(<%=Request.QueryString["sid"]%>);
                    stdAddressForm.GetContactAddressItem(<%=Request.QueryString["sid"]%>);

                    if ('<%=Request.QueryString["sid"]%>' == '0') {
                        $('.stdAddressForm #saveRegisterHome, .stdAddressForm #saveHome, .stdAddressForm #saveRegisterHomeAndNext, .stdAddressForm #saveHomeAndNext, .stdAddressForm .btn-cancel').addClass("disabled");
                    }

                });

            </script>
        </asp:View>
        <asp:View ID="ViewContent" runat="server">
            <div class="stdAddressView view-form">
                <form id="stdAddressView" class="form-padding">
                    <%--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101148") %>--%>
                    <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101129") %></p>
                    <div class="row div-row-padding" style="margin-top: 30px;">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101130") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spRegisterHomeCode"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spRegisterHomeNo"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spRegisterHomeSoi"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spRegisterHomeMoo"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spRegisterHomeRoad"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spRegisterHomeProvince"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spRegisterHomeAmphoe"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spRegisterHomeTombon"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spRegisterHomePostalCode"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101142") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spRegisterHomePhone"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101143") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spBornFrom"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101144") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spBornFromProvince"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101145") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spBornFromAmphoe"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101146") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spBornFromTombon"></span>
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
                <form id="stdContactAddressView" class="form-padding">
                    <%--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102029") %>--%>
                    <p class="bg-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101147") %></p>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label></label></div>
                        <div class="col-md-3 mb-3 div-check-input">
                            <div class="form-check disabled form-check-inline">
                                <label class="form-check-label" style="font-weight: bold;">
                                    <input id="spUseHouseAddress" name="spUseHouseAddress" class="form-check-input" type="checkbox" />
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101148") %>
                                    <span class="form-check-sign">
                                        <span class="check"></span>
                                    </span>
                                </label>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><span></span></div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105079") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spHomeNo"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label></label></div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101132") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spHomeSoi"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101133") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spHomeMoo"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105080") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spHomeRoad"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spHomeProvince"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101137") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spHomeAmphoe"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101139") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spHomeTombon"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101141") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spHomePostalCode"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101142") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spHomePhone"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101149") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spHomeStayWithTitle"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label></label></div>
                        <div class="col-md-3 mb-3">
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101151") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spHomeStayWithName"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101152") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spHomeStayWithLast"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101153") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spHomeStayWithEmergencyCall"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101154") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spHomeStayWithEmergencyEmail"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101155") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spHomeFriendName"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101156") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spHomeFriendLastName"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-2 mb-2 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101157") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spHomeFriendPhone"></span>
                        </div>
                        <div class="col-md-3 mb-3 col-form-label text-right"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101158") %> :</label></div>
                        <div class="col-md-3 mb-3 div-text-span">
                            <span class="span-data" id="spHomeHomeType"></span>
                        </div>
                        <div class="col-md-1 mb-1"></div>
                    </div>
                    <div class="row div-row-padding">
                    </div>
                    <div class="row text-center" style="margin-bottom: 7px;">
                        <button type="button"
                            class="btn btn-danger btn-cancel" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                    </div>
                </form>
            </div>
            <script type="text/javascript">

                var stdAddressView = {
                    GetAddressItem: function (stdID) {
                        $.ajax({
                            type: "POST",
                            url: "StdAddress.aspx/GetAddressItemView",
                            data: '{stdID: ' + stdID + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdAddressView.OnSuccessGetAddress,
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
                    OnSuccessGetAddress: function (response) {
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
                                $("#spRegisterHomeCode").text($(this).find("F1").text());
                                $("#spRegisterHomeNo").text($(this).find("F2").text());
                                $("#spRegisterHomeSoi").text($(this).find("F3").text());
                                $("#spRegisterHomeMoo").text($(this).find("F4").text());
                                $("#spRegisterHomeRoad").text($(this).find("F5").text());
                                $("#spRegisterHomeProvince").text($(this).find("F6").text());
                                $("#spRegisterHomeAmphoe").text($(this).find("F7").text());
                                $("#spRegisterHomeTombon").text($(this).find("F8").text());
                                $("#spRegisterHomePostalCode").text($(this).find("F9").text());
                                $("#spRegisterHomePhone").text($(this).find("F10").text());
                                $("#spBornFrom").text($(this).find("F11").text());

                                $("#spBornFromProvince").text($(this).find("F12").text());
                                $("#spBornFromAmphoe").text($(this).find("F13").text());
                                $("#spBornFromTombon").text($(this).find("F14").text());

                                CheckedSameAddress();
                            });

                        }
                    },
                    GetContactAddressItem: function (stdID) {
                        $.ajax({
                            type: "POST",
                            url: "StdAddress.aspx/GetContactAddressItemView",
                            data: '{stdID: ' + stdID + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: stdAddressView.OnSuccessGetContactAddress,
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
                    OnSuccessGetContactAddress: function (response) {
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
                                $("#spHomeNo").text($(this).find("F1").text());
                                $("#spHomeSoi").text($(this).find("F2").text());
                                $("#spHomeMoo").text($(this).find("F3").text());
                                $("#spHomeRoad").text($(this).find("F4").text());
                                $("#spHomeProvince").text($(this).find("F5").text());
                                $("#spHomeAmphoe").text($(this).find("F6").text());
                                $("#spHomeTombon").text($(this).find("F7").text());
                                $("#spHomePostalCode").text($(this).find("F8").text());
                                $("#spHomePhone").text($(this).find("F9").text());

                                $("#spHomeStayWithTitle").text($(this).find("F10").text());
                                $("#spHomeStayWithName").text($(this).find("F11").text());
                                $("#spHomeStayWithLast").text($(this).find("F12").text());
                                $("#spHomeStayWithEmergencyCall").text($(this).find("F13").text());
                                $("#spHomeStayWithEmergencyEmail").text($(this).find("F14").text());
                                $("#spHomeFriendName").text($(this).find("F15").text());
                                $("#spHomeFriendLastName").text($(this).find("F16").text());
                                $("#spHomeFriendPhone").text($(this).find("F17").text());
                                $("#spHomeHomeType").text($(this).find("F18").text());

                                CheckedSameAddress();
                            });

                        }
                    }
                }

                function CheckedSameAddress() {
                    var isSame = (($("#spHomeNo").text() == $("#spRegisterHomeNo").text()) && !!$("#spRegisterHomeNo").text()
                        && ($("#spHomeSoi").text() == $("#spRegisterHomeSoi").text()) && !!$("#spRegisterHomeSoi").text()
                        && ($("#spHomeMoo").text() == $("#spRegisterHomeMoo").text()) && !!$("#spRegisterHomeMoo").text()
                        && ($("#spHomeRoad").text() == $("#spRegisterHomeRoad").text()) && !!$("#spRegisterHomeRoad").text()
                        && ($("#spHomeProvince").text() == $("#spRegisterHomeProvince").text()) && !!$("#spRegisterHomeProvince").text()
                        && ($("#spHomeAmphoe").text() == $("#spRegisterHomeAmphoe").text()) && !!$("#spRegisterHomeAmphoe").text()
                        && ($("#spHomeTombon").text() == $("#spRegisterHomeTombon").text()) && !!$("#spRegisterHomeTombon").text()
                        && ($("#spHomePostalCode").text() == $("#spRegisterHomePostalCode").text()) && !!$("#spRegisterHomePostalCode").text()
                        && ($("#spHomePhone").text() == $("#spRegisterHomePhone").text()) && !!$("#spRegisterHomePhone").text());

                    if (isSame) {
                        $("#spUseHouseAddress").html('<i class="fa fa-check-square-o" aria-hidden="true"></i>');
                    }
                    else {
                        $("#spUseHouseAddress").html('<i class="fa fa-square-o" aria-hidden="true"></i>');
                    }
                }

                $(document).ready(function () {

                    $(".stdAddressView .btn-cancel").bind({
                        click: function () {

                            window.close();

                            return false;
                        }
                    });

                    // Load info command
                    stdAddressView.GetAddressItem(<%=Request.QueryString["sid"]%>);
                    stdAddressView.GetContactAddressItem(<%=Request.QueryString["sid"]%>);

                });

            </script>
        </asp:View>
    </asp:MultiView>
</body>
</html>
