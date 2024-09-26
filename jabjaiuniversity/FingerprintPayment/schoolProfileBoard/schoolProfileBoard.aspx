<%@ Page Language="C#" MasterPageFile="~/mp_notFrom.Master" AutoEventWireup="true" CodeBehind="schoolProfileBoard.aspx.cs" Inherits="FingerprintPayment.schoolProfileBoard.schoolProfileBoard" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        /* USER PROFILE PAGE */
        .txtDiv {
            border-radius: 5px;
            border: 1px solid #000000;
            width: fit-content;
            min-width: 200px;
            font-size: 18px;
            padding: 3px;
            margin: auto;
            margin-top: 10px;
            margin-bottom: 10px;
            -webkit-box-shadow: 2px 2px 2px rgba(0, 0, 0, 0.12);
            -moz-box-shadow: 2px 2px 2px rgba(0, 0, 0, 0.12);
            box-shadow: 2px 2px 2px rgba(0, 0, 0, 0.12);
        }

</style>
  </asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12 content-container" style="background: #ffffff; border-radius: 5px 5px 0 0">
           <h1 class="page-header text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133245") %></h1>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="text-center">
                        <img id="imgSchoolHead" runat="server" class="avatar img-thumbnail" alt="avatar" /><br />
                        <div class="txtDiv">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206510") %></label><br />
                            <%--<label id="nameSchoolHead"></label>--%>
                            <asp:label ID="schoolHeadName" runat="server" ></asp:label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 col-md-6">
                    <div class="text-center">
                        <img id="imgPersonnel" runat="server" class="avatar img-thumbnail" alt="avatar" /><br />
                        <div class="txtDiv">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801068") %></label><br />
                            <%--<label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133250") %></label>--%>
                            <asp:label ID="personnelName" runat="server" ></asp:label>
                        </div> 
                    </div>
                </div>
                <div class="col-sm-12 col-md-6">
                    <div class="text-center">
                        <img id="imgStudentDevelopmentDirector" runat="server" class="avatar img-thumbnail" alt="avatar" /><br />
                        <div class="txtDiv">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801069") %></label><br />
                            <%--<label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01524") %></label>--%>
                            <asp:label ID="studentDevelopmentDirectorName" runat="server" ></asp:label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 col-md-6">
                    <div class="text-center">
                        <img id="imgRegistraDirector" runat="server" class="avatar img-thumbnail" alt="avatar" /><br />
                        <div class="txtDiv">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133246") %></label><br />
                            <%--<label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133247") %></label>--%>
                            <asp:label ID="registraDirectorName" runat="server" ></asp:label>
                        </div>
                    </div>
                </div>
                <div class="col-sm-12 col-md-6">
                    <div class="text-center">
                        <img id="imgGM" runat="server" class="avatar img-thumbnail" alt="avatar" /><br />
                        <div class="txtDiv">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801070") %></label><br />
                            <%--<label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01522") %></label>--%>
                            <asp:label ID="gmName" runat="server" ></asp:label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 col-md-6">
                    <div class="text-center">
                        <img id="imgAcademicSubDirectorid" runat="server" class="avatar img-thumbnail" alt="avatar" /><br />
                        <div class="txtDiv">
                            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133249") %></label><br />
                            <%--<label>ชื่อ รองผู้อำนวยการบริหารงารวิชาการ</label>--%>
                            <asp:label ID="academicSubDirectoridName" runat="server" ></asp:label>
                        </div>
                    </div>
                </div>
                <div class="col-sm-12 col-md-6">
                    <div class="text-center">
                        
                    </div>
                </div>
            </div>

        </div>

    </div>
    </asp:Content>

<%--<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">

    33333333333
        </asp:Content>--%>
