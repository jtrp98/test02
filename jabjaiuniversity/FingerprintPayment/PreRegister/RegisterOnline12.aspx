<%@ Page Title="" Language="C#" MasterPageFile="~/PreRegister/RegisterOnline.Master" AutoEventWireup="true" CodeBehind="RegisterOnline12.aspx.cs" Inherits="FingerprintPayment.PreRegister.RegisterOnline12" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphStyle" runat="server">
    <style>
        .card .card-body .col-form-label, .card .card-body .label-on-right {
            padding: 12px 5px 0 0;
        }

        .card .card-body .form-group {
            margin: 0px 0 0;
        }

        .col-form-label {
            padding: 0px;
            margin: 0px;
        }

            .col-form-label span {
                vertical-align: -webkit-baseline-middle;
                font-size: 1em;
                color: #707070;
                font-weight: bold;
            }

        /* Container Bag */
        .div-bag {
            width: fit-content;
            /*float: right;*/
            text-align: left;
        }

            .div-bag.div-bag-select {
                margin-bottom: -25px;
                color: #707070;
            }

            .div-bag.div-bag-select2 {
                margin-bottom: -13px;
                color: #707070;
            }

            .div-bag.div-bag-input {
                margin-bottom: -10px;
                color: #707070;
            }

        .label-bag {
            color: #000;
            font-weight: bold;
        }

        input[type="file"] {
            /*display: none;*/
            visibility: collapse;
            position: absolute;
            width: 0%;
        }

        .no-file {
            display: none;
        }

        .card .card-body .col-form-label {
            text-align: left;
        }

            .card .card-body .col-form-label.multi-file {
                padding: 5px 0px 0px 30px;
            }

        .col-form-label span.ready {
            display: inline-block;
            color: #23A818;
            font-size: 26px;
            margin-top: -19px;
        }

        .col-form-label span.open-file {
            color: orange;
            font-size: 30px;
            margin-top: -19px;
            cursor: pointer;
        }

        .col-form-label span.upload-text {
            vertical-align: bottom;
        }

        .error {
            color: red;
        }

        .table .thead-light th {
            font-weight: bold;
        }

        .table.table-striped tbody tr td, .table.table-striped tbody tr th {
            text-align: center;
        }

            .table.table-striped tbody tr td:nth-child(2) {
                text-align: left;
                font-weight: bold;
            }

            .table.table-striped tbody tr td .col-form-label {
                text-align: right;
                padding: 5px 5px 0 0;
            }

        .no-assumption-sriracha, .no-suankularb-nonthaburi, .row-fileinput {
            display: none;
        }

        .row-fileinput.enable {
            display: revert;
        }

        @media (min-width: 320px) and (max-width: 767px) {

            .col-form-label {
                text-align: left !important;
                margin-left: 10px;
            }

            .label-on-right {
                text-align: right !important;
            }

            .div-bag {
                width: fit-content;
                /*float: left;*/
                text-align: left;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <form id="form">
                <div class="card">
                    <div class="card-header card-header-rose card-header-icon">
                        <div class="card-icon text-center" style="border-radius: 12px; margin-left: 30px; margin-top: -30px;">
                            <h4 style="font-family: THSarabunNew; margin-bottom: 0px; padding: 3px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103095") %></h4>
                            <p class="h6 text-center">(Document information)</p>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <br>
                        </div>
                        <div class="row">
                            <div class="col-md-12 ml-auto mr-auto">
                                <table class="table table-striped">
                                    <thead class="thead-light">
                                        <tr>
                                            <th width="12%" class="text-center" scope="col"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %><p class="h6">No.</p>
                                            </th>
                                            <th width="68%" class="text-center" scope="col"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103123") %><p class="h6">list of document information</p>
                                            </th>
                                            <th width="15%" scope="col"></th>
                                            <th width="5%" scope="col"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr style="display: none;">
                                            <th scope="row">0</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132859") %> :
                                                <p class="h6" data-did="0" data-tid="1" data-vfiid="0">(Please also download the photos in the application form.)</p>
                                            </td>
                                            <td>
                                                <div class="col-form-label">
                                                    <input id="fileDocument00" name="fileDocument00" type="file" data-did="0" data-tid="1" data-vfiid="0" accept="application/pdf, image/*" />
                                                    <span class="material-icons no-file" data-did="0" data-tid="1" data-vfiid="0">check_circle</span>
                                                    <span class="material-icons open-file" data-did="0" data-tid="1" data-vfiid="0">folder</span>
                                                    <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr class="row-fileinput">
                                            <th scope="row">1</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103124") %><span class="red-star"></span>:
                                                <p class="h6" data-did="1" data-tid="1" data-vfiid="1">(One copy of the birth certificate or ID card, in case of foreign birth, a <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %> translation must be attached.)</p>
                                            </td>
                                            <td>
                                                <div class="col-form-label">
                                                    <input id="fileDocument01" name="fileDocument01" type="file" data-did="1" data-tid="1" data-vfiid="1" accept="application/pdf, image/*" />
                                                    <span class="material-icons no-file" data-did="1" data-tid="1" data-vfiid="1">check_circle</span>
                                                    <span class="material-icons open-file" data-did="1" data-tid="1" data-vfiid="1">folder</span>
                                                    <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr class="row-fileinput h-2">
                                            <th scope="row">2</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103125") %> <span class="red-star"></span>:
                                                <p class="h6">(A copy of the student’s house registration, father and mother)</p>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr class="row-fileinput">
                                            <th scope="row">2.1</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103126") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="2" data-tid="1" data-vfiid="3">(A copy of the student’s house registration 1 copy)</p>
                                            </td>
                                            <td>
                                                <div class="col-form-label">
                                                    <input id="fileDocument021" name="fileDocument021" type="file" data-did="2" data-tid="1" data-vfiid="3" accept="application/pdf, image/*" />
                                                    <span class="material-icons no-file" data-did="2" data-tid="1" data-vfiid="3">check_circle</span>
                                                    <span class="material-icons open-file" data-did="2" data-tid="1" data-vfiid="3">folder</span>
                                                    <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr class="row-fileinput">
                                            <th scope="row">2.2</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103127") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="2" data-tid="2" data-vfiid="4">(A copy of the father’s house registration 1 copy)</p>
                                            </td>
                                            <td>
                                                <div class="col-form-label">
                                                    <input id="fileDocument022" name="fileDocument022" type="file" data-did="2" data-tid="2" data-vfiid="4" accept="application/pdf, image/*" />
                                                    <span class="material-icons no-file" data-did="2" data-tid="2" data-vfiid="4">check_circle</span>
                                                    <span class="material-icons open-file" data-did="2" data-tid="2" data-vfiid="4">folder</span>
                                                    <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr class="row-fileinput">
                                            <th scope="row">2.3</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103128") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="2" data-tid="3" data-vfiid="5">(A copy of the mother’s house registration 1 copy)</p>
                                            </td>
                                            <td>
                                                <div class="col-form-label">
                                                    <input id="fileDocument023" name="fileDocument023" type="file" data-did="2" data-tid="3" data-vfiid="5" accept="application/pdf, image/*" />
                                                    <span class="material-icons no-file" data-did="2" data-tid="3" data-vfiid="5">check_circle</span>
                                                    <span class="material-icons open-file" data-did="2" data-tid="3" data-vfiid="5">folder</span>
                                                    <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr class="row-fileinput">
                                            <th scope="row">2.4</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103129") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="2" data-tid="4" data-vfiid="171">(A copy of the host or homeowner)</p>
                                            </td>
                                            <td>
                                                <div class="col-form-label">
                                                    <input id="fileDocument024" name="fileDocument024" type="file" data-did="2" data-tid="4" data-vfiid="171" accept="application/pdf, image/*" />
                                                    <span class="material-icons no-file" data-did="2" data-tid="4" data-vfiid="171">check_circle</span>
                                                    <span class="material-icons open-file" data-did="2" data-tid="4" data-vfiid="171">folder</span>
                                                    <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr class="row-fileinput h-3">
                                            <th scope="row">3</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103130") %> <span class="red-star"></span>:
                                                <p class="h6">(Copy of ID card of father and mother)</p>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr class="row-fileinput">
                                            <th scope="row">3.1</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103131") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="3" data-tid="1" data-vfiid="7">(A copy of father ID card 1 copy)</p>
                                            </td>
                                            <td>
                                                <div class="col-form-label">
                                                    <input id="fileDocument031" name="fileDocument031" type="file" data-did="3" data-tid="1" data-vfiid="7" accept="application/pdf, image/*" />
                                                    <span class="material-icons no-file" data-did="3" data-tid="1" data-vfiid="7">check_circle</span>
                                                    <span class="material-icons open-file" data-did="3" data-tid="1" data-vfiid="7">folder</span>
                                                    <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr class="row-fileinput">
                                            <th scope="row">3.2</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103132") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="3" data-tid="2" data-vfiid="8">(A copy of mother ID card 1 copy)</p>
                                            </td>
                                            <td>
                                                <div class="col-form-label">
                                                    <input id="fileDocument032" name="fileDocument032" type="file" data-did="3" data-tid="2" data-vfiid="8" accept="application/pdf, image/*" />
                                                    <span class="material-icons no-file" data-did="3" data-tid="2" data-vfiid="8">check_circle</span>
                                                    <span class="material-icons open-file" data-did="3" data-tid="2" data-vfiid="8">folder</span>
                                                    <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr class="row-fileinput h-4">
                                            <th scope="row">4</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103133") %> <span class="red-star"></span>:
                                                <p class="h6">(A copy of the Transcript)</p>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr class="row-fileinput">
                                            <th scope="row">4.1</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103134") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="4" data-tid="1" data-vfiid="9">(A copy of the Transcript, front document 1 copy)</p>
                                            </td>
                                            <td>
                                                <div class="col-form-label">
                                                    <input id="fileDocument041" name="fileDocument041" type="file" data-did="4" data-tid="1" data-vfiid="169" accept="application/pdf, image/*" />
                                                    <span class="material-icons no-file" data-did="4" data-tid="1" data-vfiid="169">check_circle</span>
                                                    <span class="material-icons open-file" data-did="4" data-tid="1" data-vfiid="169">folder</span>
                                                    <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr class="row-fileinput">
                                            <th scope="row">4.2</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103135") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="4" data-tid="1" data-vfiid="9">(A copy of the Transcript, back document 1 copy)</p>
                                            </td>
                                            <td>
                                                <div class="col-form-label">
                                                    <input id="fileDocument042" name="fileDocument042" type="file" data-did="4" data-tid="2" data-vfiid="170" accept="application/pdf, image/*" />
                                                    <span class="material-icons no-file" data-did="4" data-tid="2" data-vfiid="170">check_circle</span>
                                                    <span class="material-icons open-file" data-did="4" data-tid="2" data-vfiid="170">folder</span>
                                                    <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr class="row-fileinput h-5">
                                            <th scope="row">5</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103136") %> <span class="red-star"></span>:
                                                <p class="h6">(Copy of proof of name-surname change student, father and mother (if any))</p>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr class="row-fileinput">
                                            <th scope="row">5.1</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103137") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="5" data-tid="1" data-vfiid="11">(Copy of proof of name-surname change of student(if any))</p>
                                            </td>
                                            <td>
                                                <div class="col-form-label">
                                                    <input id="fileDocument051" name="fileDocument051" type="file" data-did="5" data-tid="1" data-vfiid="11" accept="application/pdf, image/*" />
                                                    <span class="material-icons no-file" data-did="5" data-tid="1" data-vfiid="11">check_circle</span>
                                                    <span class="material-icons open-file" data-did="5" data-tid="1" data-vfiid="11">folder</span>
                                                    <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr class="row-fileinput">
                                            <th scope="row">5.2</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103138") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="5" data-tid="2" data-vfiid="12">(Copy of proof of name-surname change of father (if any))</p>
                                            </td>
                                            <td>
                                                <div class="col-form-label">
                                                    <input id="fileDocument052" name="fileDocument052" type="file" data-did="5" data-tid="2" data-vfiid="12" accept="application/pdf, image/*" />
                                                    <span class="material-icons no-file" data-did="5" data-tid="2" data-vfiid="12">check_circle</span>
                                                    <span class="material-icons open-file" data-did="5" data-tid="2" data-vfiid="12">folder</span>
                                                    <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr class="row-fileinput">
                                            <th scope="row">5.3</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103139") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="5" data-tid="3" data-vfiid="13">(Copy of proof of name-surname change of mother (if any))</p>
                                            </td>
                                            <td>
                                                <div class="col-form-label">
                                                    <input id="fileDocument053" name="fileDocument053" type="file" data-did="5" data-tid="3" data-vfiid="13" accept="application/pdf, image/*" />
                                                    <span class="material-icons no-file" data-did="5" data-tid="3" data-vfiid="13">check_circle</span>
                                                    <span class="material-icons open-file" data-did="5" data-tid="3" data-vfiid="13">folder</span>
                                                    <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr class="row-fileinput <%=NoSuankularbNonthaburi%>">
                                            <th scope="row">6</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132777") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="12" data-tid="1" data-vfiid="166">(A copy of household registration of the homeowner or landlord)</p>
                                            </td>
                                            <td>
                                                <div class="col-form-label">
                                                    <input id="fileDocument12" name="fileDocument12" type="file" data-did="12" data-tid="1" data-vfiid="166" accept="application/pdf, image/*" />
                                                    <span class="material-icons no-file" data-did="12" data-tid="1" data-vfiid="166">check_circle</span>
                                                    <span class="material-icons open-file" data-did="12" data-tid="1" data-vfiid="166">folder</span>
                                                    <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr class="row-fileinput <%=NoSuankularbNonthaburi%>">
                                            <th scope="row">7</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132778") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="13" data-tid="1" data-vfiid="167">(A document of home ownership certificate)</p>
                                            </td>
                                            <td>
                                                <div class="col-form-label">
                                                    <input id="fileDocument13" name="fileDocument13" type="file" data-did="13" data-tid="1" data-vfiid="167" accept="application/pdf, image/*" />
                                                    <span class="material-icons no-file" data-did="13" data-tid="1" data-vfiid="167">check_circle</span>
                                                    <span class="material-icons open-file" data-did="13" data-tid="1" data-vfiid="167">folder</span>
                                                    <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr class="row-fileinput <%=NoSuankularbNonthaburi%>">
                                            <th scope="row">8</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132779") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="14" data-tid="1" data-vfiid="168">(A document of student special condition)</p>
                                            </td>
                                            <td>
                                                <div class="col-form-label">
                                                    <input id="fileDocument14" name="fileDocument14" type="file" data-did="14" data-tid="1" data-vfiid="168" accept="application/pdf, image/*" />
                                                    <span class="material-icons no-file" data-did="14" data-tid="1" data-vfiid="168">check_circle</span>
                                                    <span class="material-icons open-file" data-did="14" data-tid="1" data-vfiid="168">folder</span>
                                                    <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr class="row-fileinput">
                                            <th scope="row" class="re-order">6</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103140") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="6" data-tid="1" data-vfiid="14">(Portfolio, only for students in grades 1 and 4)</p>
                                            </td>
                                            <td>
                                                <div class="col-form-label">
                                                    <input id="fileDocument06" name="fileDocument06" type="file" data-did="6" data-tid="1" data-vfiid="14" accept="application/pdf, image/*" />
                                                    <span class="material-icons no-file" data-did="6" data-tid="1" data-vfiid="14">check_circle</span>
                                                    <span class="material-icons open-file" data-did="6" data-tid="1" data-vfiid="14">folder</span>
                                                    <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr class="row-fileinput">
                                            <th scope="row" class="re-order">7</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103141") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="7" data-tid="1" data-vfiid="15">(Copy of adoption registration certificate (In the case of adoption) 1 copy)</p>
                                            </td>
                                            <td>
                                                <div class="col-form-label">
                                                    <input id="fileDocument07" name="fileDocument07" type="file" data-did="7" data-tid="1" data-vfiid="15" accept="application/pdf, image/*" />
                                                    <span class="material-icons no-file" data-did="7" data-tid="1" data-vfiid="15">check_circle</span>
                                                    <span class="material-icons open-file" data-did="7" data-tid="1" data-vfiid="15">folder</span>
                                                    <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr class="row-fileinput">
                                            <th scope="row" class="re-order">8</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103142") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="8" data-tid="1" data-vfiid="16">(Medical certificate (Hospital or Clinic))</p>
                                            </td>
                                            <td>
                                                <div class="col-form-label">
                                                    <input id="fileDocument08" name="fileDocument08" type="file" data-did="8" data-tid="1" data-vfiid="16" accept="application/pdf, image/*" />
                                                    <span class="material-icons no-file" data-did="8" data-tid="1" data-vfiid="16">check_circle</span>
                                                    <span class="material-icons open-file" data-did="8" data-tid="1" data-vfiid="16">folder</span>
                                                    <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr class="row-fileinput">
                                            <th scope="row" class="re-order">9</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103143") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="9" data-tid="1" data-vfiid="17">(Proof of transfer of the application fee (transfer slip))</p>
                                            </td>
                                            <td>
                                                <div class="col-form-label">
                                                    <input id="fileDocument09" name="fileDocument09" type="file" data-did="9" data-tid="1" data-vfiid="17" accept="application/pdf, image/*" />
                                                    <span class="material-icons no-file" data-did="9" data-tid="1" data-vfiid="17">check_circle</span>
                                                    <span class="material-icons open-file" data-did="9" data-tid="1" data-vfiid="17">folder</span>
                                                    <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr class="row-fileinput <%=NoAssumptionSriracha%>">
                                            <th scope="row">10</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132780") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="10" data-tid="1" data-vfiid="18">(Copy of documents showing the father/mother’s being Assumption Sriracha alumni or alumni Current students, brothers/sisters (if any))</p>
                                            </td>
                                            <td>
                                                <div class="col-form-label">
                                                    <input id="fileDocument10" name="fileDocument10" type="file" data-did="10" data-tid="1" data-vfiid="18" accept="application/pdf, image/*" />
                                                    <span class="material-icons no-file" data-did="10" data-tid="1" data-vfiid="18">check_circle</span>
                                                    <span class="material-icons open-file" data-did="10" data-tid="1" data-vfiid="18">folder</span>
                                                    <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr class="row-fileinput <%=NoAssumptionSriracha%>">
                                            <th scope="row">11</th>
                                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132781") %> <span class="red-star"></span>:
                                                <p class="h6" data-did="11" data-tid="1" data-vfiid="19">(Copy of the baptismal receipt (For Catholic students) 1 issue)</p>
                                            </td>
                                            <td>
                                                <div class="col-form-label">
                                                    <input id="fileDocument11" name="fileDocument11" type="file" data-did="11" data-tid="1" data-vfiid="19" accept="application/pdf, image/*" />
                                                    <span class="material-icons no-file" data-did="11" data-tid="1" data-vfiid="19">check_circle</span>
                                                    <span class="material-icons open-file" data-did="11" data-tid="1" data-vfiid="19">folder</span>
                                                    <span class="upload-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103122") %></span>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="row">
                            <br>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <button id="btnBack" class="btn btn-warning btn-round col-md-2" style="font-size: 1.2em;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %><p class="h6" style="margin-bottom: 0px;">(Back)</p>
                                </button>
                                <button id="btnSaveDraft" class="btn btn-info btn-round col-md-2" style="font-size: 1.2em;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601083") %><p class="h6" style="margin-bottom: 0px;">(Save draft)</p>
                                </button>
                                <button id="btnSave" class="btn btn-success btn-round col-md-2" style="font-size: 1.2em;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %><p class="h6" style="margin-bottom: 0px;">(Save)</p>
                                </button>
                            </div>
                        </div>
                        <div class="row">
                            <br>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScript" runat="server">
    <script>

        var registerDocument = {
            maxFileSize: 2097152, // 2 MB
            msgFileSize: "2",
            FileData: [],
        }

        function LoadDataFromLocalStorage() {
            // Get data from localStorage
            if (ls.isBrowserSupport()) {
                // Code for localStorage
                preRegister = ls.getItem('preRegister');

                RetreivedFileBase64FromSession();
            } else {
                // No web storage Support.
            }
        }

        function SaveDataToLocalStorage(callbackFunction) {
            if ($("#form").valid()) {

                preRegister.Files = registerDocument.FileData;

                preRegister.Page12Saved = true;

                // Save data to localStorage
                if (ls.isBrowserSupport()) {
                    // Code for localStorage
                    ls.setItem('preRegister', preRegister);
                } else {
                    // No web storage Support.
                }

                ez.activePageComplete(12);

                callbackFunction();

            }
        }

        function AddRequiredRulesvalidation(obj) {
            $(obj).closest('tr').find('.red-star').html('<sup>*</sup>');
            $(obj).rules('add', { required: function (element) { return !($(element).val().length > 0 || $(element).data('selected')); } });
            //$(obj).rules('remove', 'required');
        }

        function EnableFileUploadDocument(obj) {
            $(obj).closest('.row-fileinput').addClass('enable');

            var hobj = $('.row-fileinput.h-' + $(obj).data('did'));
            if (!$(hobj).hasClass("enable")) {
                $(hobj).addClass('enable');
            }
        }

        function ReNoFileUploadDocument() {
            var no = 1;
            $('.row-fileinput.enable th').each(function (index) {
                if ($(this).text().indexOf(".") != -1) {
                    no--;
                    $(this).text(no + $(this).text().substring(1));
                }
                else {
                    $(this).text(no);
                }
                no++;
            });
        }

        function ReapplyTableStriping() {
            $("tr:visible").each(function (index) {
                $(this).css("background-color", !!(index & 1) ? "#ffffff" : "#f9f9f9");
            });
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
                    text: 'Error Message - c' + responseText,
                    type: 'error',
                    confirmButtonClass: "btn btn-danger",
                    buttonsStyling: false
                });
            }
        }

        function UploadFileBase64ToSession(documentFile) {
            $.ajax({
                async: false,
                type: "POST",
                url: 'RegisterOnline04.aspx/UploadFileBase64ToSession',
                data: JSON.stringify({ documentFile: documentFile }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {

                    var r = JSON.parse(result.d);
                    if (r.success) {

                        // Upload file base64 to session
                        // Find index of specific object using findIndex method.    
                        var objIndex = registerDocument.FileData.findIndex((obj => obj.docId == r.documentFile.docId && obj.typeId == r.documentFile.typeId));
                        if (objIndex == -1) {
                            //var newFileId = registerDocument.FileData.length == 0 ? 1 : Math.max.apply(Math, registerDocument.FileData.map(function (o) { return o.id; })) + 1;
                            registerDocument.FileData.push({ id: r.documentFile.id, fileName: r.documentFile.fileName, contentType: r.documentFile.contentType, docId: r.documentFile.docId, typeId: r.documentFile.typeId, vfiId: r.documentFile.vfiId, byteData: '' });

                            //objIndex = registerDocument.FileData.findIndex((obj => obj.id == newFileId));
                        }
                        else {
                            registerDocument.FileData[objIndex].fileName = r.documentFile.fileName;
                            registerDocument.FileData[objIndex].contentType = r.documentFile.contentType;
                            registerDocument.FileData[objIndex].byteData = '';
                        }

                        var docId = r.documentFile.docId, typeId = r.documentFile.typeId;
                        if (!$('.no-file[data-did=' + docId + '][data-tid=' + typeId + ']').hasClass('ready')) {
                            $('.no-file[data-did=' + docId + '][data-tid=' + typeId + ']').addClass('ready');
                            $('.no-file[data-did=' + docId + '][data-tid=' + typeId + ']').removeClass('no-file');
                        }
                    }
                    else {
                        Swal.fire({
                            title: 'Warning!',
                            text: 'Warning Message (File Upload) - ' + r.message,
                            type: 'warning',
                            confirmButtonClass: "btn btn-warning",
                            buttonsStyling: false
                        });
                    }
                },
                error: OnError
            });
        }

        function RetreivedFileBase64FromSession() {
            $.ajax({
                async: false,
                type: "POST",
                url: 'RegisterOnline04.aspx/ListFileBase64FromSession',
                data: JSON.stringify({}),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {

                    var r = JSON.parse(result.d);
                    if (r.success) {
                        // TODO: Retreived file base64 from session
                        if (r.documentFiles != null) {
                            if (preRegister.Page04Saved) {
                                const profilePicture = r.documentFiles.filter(function (obj) { return obj.docId == 0; });
                                $.each(profilePicture, function (i, obj) {
                                    var docId = obj.docId;
                                    var typeId = obj.typeId;
                                    $('input[type=file][data-did=' + docId + '][data-tid=' + typeId + ']').data('selected', true);
                                    if (!$('.no-file[data-did=' + docId + '][data-tid=' + typeId + ']').hasClass('ready')) {
                                        $('.no-file[data-did=' + docId + '][data-tid=' + typeId + ']').addClass('ready');
                                        $('.no-file[data-did=' + docId + '][data-tid=' + typeId + ']').removeClass('no-file');
                                    }

                                    obj.byteData = '';
                                    registerDocument.FileData.push(obj);
                                });
                            }

                            if (preRegister.Page12Saved) {
                                //registerDocument.FileData = preRegister.Files;
                                const documentFiles = r.documentFiles.filter(function (obj) { return obj.docId != 0; });
                                $.each(documentFiles, function (i, obj) {
                                    var docId = obj.docId;
                                    var typeId = obj.typeId;
                                    $('input[type=file][data-did=' + docId + '][data-tid=' + typeId + ']').data('selected', true);
                                    if (!$('.no-file[data-did=' + docId + '][data-tid=' + typeId + ']').hasClass('ready')) {
                                        $('.no-file[data-did=' + docId + '][data-tid=' + typeId + ']').addClass('ready');
                                        $('.no-file[data-did=' + docId + '][data-tid=' + typeId + ']').removeClass('no-file');
                                    }

                                    obj.byteData = '';
                                    registerDocument.FileData.push(obj);
                                });
                            }
                        }
                    }
                    else {
                        Swal.fire({
                            title: 'Warning!',
                            text: 'Warning Message (File Upload) - ' + r.message,
                            type: 'warning',
                            confirmButtonClass: "btn btn-warning",
                            buttonsStyling: false
                        });
                    }
                },
                error: OnError
            });
        }

        var preRegister = null;
        $(document).ready(function () {

            LoadDataFromLocalStorage();

            $(".open-file").bind({
                click: function () {

                    $('input[type=file][data-did=' + $(this).data('did') + '][data-tid=' + $(this).data('tid') + ']').trigger('click');

                    return false;
                }
            });

            // Upload File
            $('input[type=file]').change(function () {
                ImagesPreview($(this)[0].files, $(this).data('did'), $(this).data('tid'), $(this).data('vfiid'));
            });

            // Multiple images preview in browser
            var ImagesPreview = function (files, docId, typeId, vfiId) {
                if (files) {
                    var filesAmount = files.length;
                    for (i = 0; i < filesAmount; i++) {
                        var file = files[i];

                        // Check file size
                        if (file.size > registerDocument.maxFileSize) { $("#form").valid(); continue; }

                        var reader = new FileReader();
                        reader.onload = function (event) {

                            //Update object's byteData property.
                            var byteData = this.result.split(';')[1].replace("base64,", "");

                            var id = 0;
                            var objIndex = registerDocument.FileData.findIndex((obj => obj.docId == docId && obj.typeId == typeId));
                            if (objIndex == -1) {
                                id = registerDocument.FileData.length == 0 ? 1 : Math.max.apply(Math, registerDocument.FileData.map(function (o) { return o.id; })) + 1;
                            }
                            else {
                                id = registerDocument.FileData[objIndex].id;
                            }

                            UploadFileBase64ToSession({ id: id, fileName: file.name, contentType: file.type, docId: docId, typeId: typeId, vfiId: vfiId, byteData: byteData });
                        }
                        reader.readAsDataURL(files[i]);
                    }
                }
            };

            $.validator.addMethod('fileSize', function (value, element, arg) {
                if (element.files.length > 0) {
                    if (element.files[0].size <= arg) {
                        return true;
                    } else {
                        return false;
                    }
                } else {
                    return true;
                }
            });

            $("#form").validate({
                rules: {
                    fileDocument00: {
                        required: false,
                        fileSize: registerDocument.maxFileSize
                    },
                    fileDocument01: {
                        required: false,
                        fileSize: registerDocument.maxFileSize
                    },
                    fileDocument021: {
                        required: false,
                        fileSize: registerDocument.maxFileSize
                    },
                    fileDocument022: {
                        required: false,
                        fileSize: registerDocument.maxFileSize
                    },
                    fileDocument023: {
                        required: false,
                        fileSize: registerDocument.maxFileSize
                    },
                    fileDocument024: {
                        required: false,
                        fileSize: registerDocument.maxFileSize
                    },
                    fileDocument031: {
                        required: false,
                        fileSize: registerDocument.maxFileSize
                    },
                    fileDocument032: {
                        required: false,
                        fileSize: registerDocument.maxFileSize
                    },
                    fileDocument041: {
                        required: false,
                        fileSize: registerDocument.maxFileSize
                    },
                    fileDocument042: {
                        required: false,
                        fileSize: registerDocument.maxFileSize
                    },
                    fileDocument051: {
                        required: false,
                        fileSize: registerDocument.maxFileSize
                    },
                    fileDocument052: {
                        required: false,
                        fileSize: registerDocument.maxFileSize
                    },
                    fileDocument053: {
                        required: false,
                        fileSize: registerDocument.maxFileSize
                    },
                    fileDocument06: {
                        required: false,
                        fileSize: registerDocument.maxFileSize
                    },
                    fileDocument07: {
                        required: false,
                        fileSize: registerDocument.maxFileSize
                    },
                    fileDocument08: {
                        required: false,
                        fileSize: registerDocument.maxFileSize
                    },
                    fileDocument09: {
                        required: false,
                        fileSize: registerDocument.maxFileSize
                    },
                    fileDocument10: {
                        required: false,
                        fileSize: registerDocument.maxFileSize
                    },
                    fileDocument11: {
                        required: false,
                        fileSize: registerDocument.maxFileSize
                    },
                    fileDocument12: {
                        required: false,
                        fileSize: registerDocument.maxFileSize
                    },
                    fileDocument13: {
                        required: false,
                        fileSize: registerDocument.maxFileSize
                    },
                    fileDocument14: {
                        required: false,
                        fileSize: registerDocument.maxFileSize
                    }
                },
                messages: {
                    fileDocument00: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132860") %>",
                        fileSize: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132861") %> " + registerDocument.msgFileSize + " MB"
                    },
                    fileDocument01: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132860") %>",
                        fileSize: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132861") %> " + registerDocument.msgFileSize + " MB"
                    },
                    fileDocument021: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132860") %>",
                        fileSize: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132861") %> " + registerDocument.msgFileSize + " MB"
                    },
                    fileDocument022: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132860") %>",
                        fileSize: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132861") %> " + registerDocument.msgFileSize + " MB"
                    },
                    fileDocument023: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132860") %>",
                        fileSize: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132861") %> " + registerDocument.msgFileSize + " MB"
                    },
                    fileDocument024: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132860") %>",
                        fileSize: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132861") %> " + registerDocument.msgFileSize + " MB"
                    },
                    fileDocument031: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132860") %>",
                        fileSize: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132861") %> " + registerDocument.msgFileSize + " MB"
                    },
                    fileDocument032: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132860") %>",
                        fileSize: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132861") %> " + registerDocument.msgFileSize + " MB"
                    },
                    fileDocument041: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132860") %>",
                        fileSize: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132861") %> " + registerDocument.msgFileSize + " MB"
                    },
                    fileDocument042: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132860") %>",
                        fileSize: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132861") %> " + registerDocument.msgFileSize + " MB"
                    },
                    fileDocument051: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132860") %>",
                        fileSize: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132861") %> " + registerDocument.msgFileSize + " MB"
                    },
                    fileDocument052: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132860") %>",
                        fileSize: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132861") %> " + registerDocument.msgFileSize + " MB"
                    },
                    fileDocument053: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132860") %>",
                        fileSize: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132861") %> " + registerDocument.msgFileSize + " MB"
                    },
                    fileDocument06: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132860") %>",
                        fileSize: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132861") %> " + registerDocument.msgFileSize + " MB"
                    },
                    fileDocument07: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132860") %>",
                        fileSize: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132861") %> " + registerDocument.msgFileSize + " MB"
                    },
                    fileDocument08: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132860") %>",
                        fileSize: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132861") %> " + registerDocument.msgFileSize + " MB"
                    },
                    fileDocument09: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132860") %>",
                        fileSize: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132861") %> " + registerDocument.msgFileSize + " MB"
                    },
                    fileDocument10: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132860") %>",
                        fileSize: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132861") %> " + registerDocument.msgFileSize + " MB"
                    },
                    fileDocument11: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132860") %>",
                        fileSize: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132861") %> " + registerDocument.msgFileSize + " MB"
                    },
                    fileDocument12: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132860") %>",
                        fileSize: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132861") %> " + registerDocument.msgFileSize + " MB"
                    },
                    fileDocument13: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132860") %>",
                        fileSize: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132861") %> " + registerDocument.msgFileSize + " MB"
                    },
                    fileDocument14: {
                        required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132860") %>",
                        fileSize: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132861") %> " + registerDocument.msgFileSize + " MB"
                    }
                },
                errorPlacement: function (error, element) {
                    switch (element.attr("name")) {
                        case "fileDocument00":
                        case "fileDocument01":
                        case "fileDocument021":
                        case "fileDocument022":
                        case "fileDocument023":
                        case "fileDocument024":
                        case "fileDocument031":
                        case "fileDocument032":
                        case "fileDocument041":
                        case "fileDocument042":
                        case "fileDocument051":
                        case "fileDocument052":
                        case "fileDocument053":
                        case "fileDocument06":
                        case "fileDocument07":
                        case "fileDocument08":
                        case "fileDocument09":
                        case "fileDocument10":
                        case "fileDocument11":
                        case "fileDocument12":
                        case "fileDocument13":
                        case "fileDocument14":

                            var docId = $(element).data('did');
                            var typeId = $(element).data('tid');
                            error.insertAfter($('p.h6[data-did=' + docId + '][data-tid=' + typeId + ']'));

                            break;
                        default: error.insertAfter(element); break;
                    }
                },
                onfocusout: false,
                invalidHandler: function (form, validator) {
                    var errors = validator.numberOfInvalids();
                    if (errors) {
                        validator.errorList[0].element.focus();
                    }
                }
            });

            $("#btnBack").bind({
                click: function () {

                    window.location.href = "RegisterOnline11.aspx";

                    return false;
                }
            });

            $("#btnSaveDraft").bind({
                click: function () {

                    SaveDataToLocalStorage(function () {

                        ez.showNotification('top', 'right', 'success', 'done', '[Notification Message]<br/><br/>Save draft is complete.', 3000);

                    });

                    return false;
                }
            });

            $("#btnSave").bind({
                click: function () {

                    SaveDataToLocalStorage(function () {

                        window.location.href = "RegisterOnlinePreview.aspx";

                    });

                    return false;
                }
            });
        });
    </script>
    <asp:Literal ID="ltrScriptRequiredField" runat="server"></asp:Literal>
</asp:Content>
