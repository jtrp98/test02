<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="costdetail.aspx.cs" Inherits="FingerprintPayment.costdetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css" integrity="sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb" crossorigin="anonymous">
    <link href="https://gitcdn.github.io/bootstrap-toggle/2.2.2/css/bootstrap-toggle.min.css" rel="stylesheet">
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.3/umd/popper.min.js" integrity="sha384-vFJXuSJphROIrBnz7yo7oB41mKfc8JzQZiCq4NCceLEaO4IHwicKwpJf9c9IpFgh" crossorigin="anonymous"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/js/bootstrap.min.js" integrity="sha384-alpBpkh1PFOepccYVYDB4do5UnbKysX5WZXm3XxPqe5iKTfUKjNkCk9SaVuEZflJ" crossorigin="anonymous"></script>
<script src="https://gitcdn.github.io/bootstrap-toggle/2.2.2/js/bootstrap-toggle.min.js"></script>
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
        .lefttext {
            text-align: left;
        }
        .righttext {
            text-align: right;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full-card box-content employeeslist-container">
        <div class="row form-group">
            <div class="col-md-6 col-sm-12">
                
                <div class="col-lg-12 col-md-12 col-sm-12 adjust-col-padding col-space centertext " style="font-style: inherit">
                    <label style="font-size: 150%">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131064") %></label>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 adjust-col-padding col-space lefttext" style="margin-top: 0px; margin-bottom: 0px; padding-top: 0px; padding-bottom: 0px;">
                    
                    <div class="col-lg-12 col-md-12 col-sm-12" style="margin-top: -3px; margin-bottom: -3px; padding-top: -3px; padding-bottom: -3px;"><hr/></div>
                </div>
                
               
                <div class="col-lg-12 col-md-12 col-sm-12 adjust-col-padding col-space centertext " >
                    <label style="font-size: 130%;">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131065") %></label>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12" style="margin-top: -3px; margin-bottom: -3px; padding-top: -3px; padding-bottom: -3px;"><hr/></div>
                <div class="col-lg-12 col-md-12 col-sm-12 adjust-col-padding col-space lefttext " style="margin-top: -3px; margin-bottom: -3px; padding-top: -3px; padding-bottom: -3px;">
                    <div class="col-lg-4 col-md-4 col-sm-4 adjust-col-padding col-space lefttext ">
                        <b>
                        Order No.</b>
                        </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 adjust-col-padding col-space righttext ">
                        <label>
                        2939483204938490238409238</label>
                        </div>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12" style="margin-top: -3px; margin-bottom: -3px; padding-top: -3px; padding-bottom: -3px;"><hr/></div>
                <div class="col-lg-12 col-md-12 col-sm-12 adjust-col-padding col-space lefttext " style="margin-top: -3px; margin-bottom: -3px; padding-top: -3px; padding-bottom: -3px;">
                    <div class="col-lg-4 col-md-4 col-sm-4 adjust-col-padding col-space lefttext ">
                        <b>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %></b>
                        </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 adjust-col-padding col-space righttext ">
                        <label>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131066") %></label>
                        </div>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12" style="margin-top: -3px; margin-bottom: -3px; padding-top: -3px; padding-bottom: -3px;"><hr/></div>
                <div class="col-lg-12 col-md-12 col-sm-12 adjust-col-padding col-space lefttext " style="margin-top: -3px; margin-bottom: -3px; padding-top: -3px; padding-bottom: -3px;">
                    <div class="col-lg-4 col-md-4 col-sm-4 adjust-col-padding col-space lefttext ">
                        <b>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131067") %></b>
                        </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 adjust-col-padding col-space righttext ">
                        <label>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131068") %></label>
                        </div>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12" style="margin-top: -3px; margin-bottom: -3px; padding-top: -3px; padding-bottom: -3px;"><hr/></div>
                <div class="col-lg-12 col-md-12 col-sm-12 hid" >
                    <label> a
                         </label>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 adjust-col-padding col-space centertext " style="font-style: inherit">
                    <button type="button" class="btn btn-primary btn-lg" style="font-size: 110%">Payment</button>   
                </div>

                </div>
            </div>
            

        </div>
        <div class="row form-group">
            <div class="col-sm-12 text-center">
                         
            </div>
        </div>
        <div class="row mini--space__top">
            
        </div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
