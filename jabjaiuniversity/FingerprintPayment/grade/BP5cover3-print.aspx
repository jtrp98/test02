<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BP5cover3-print.aspx.cs" Inherits="FingerprintPayment.grade.BP5cover3_print" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    
    <link href="/bootstrap SB2/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
   
    
    <script src="/bootstrap SB2/dist/js/sb-admin-2.js" type="text/javascript"></script>
    
    <style>
        
        .centertext {
        text-align: center;
    }
    
    .hid {
        visibility: hidden;
    }
    .ddd {
        padding:0;
        width:50px;
        text-align:center;
    }
    .righttext2 {
        background-color: #337AB7;
        position: relative;
        text-align: right;
        color: white
    } 
    .righttext3 {
        background-color: #337AB7;
        position: relative;
                
    }         
    
    .txt:hover {
        text-decoration: underline;
    }
    
.table2 {
    border-collapse: collapse;
    width: 100%;
}
.bigtxt{
    font-size:30px;   
}
.bigtxt2{
    font-size:25px;
}
.bigtxt3{
    font-size:20px;
}
.tdtr {
    border: 1px solid #000000;
    text-align: center;
    padding: 8px;
    width:40px;
}
         .righttext {
             position: relative;
             text-align: right;
             white-space: nowrap;
         }
        .lefttext {
            position: relative;
            text-align: left;
            white-space: nowrap;
        }
        
        .noborder {
            border-style: none;
            text-decoration: none;
            text-shadow: none !important;
            box-shadow: inset 0px 0px 0px 0px white;
        }
        .centerunderline {
            text-align: center;
        }

        .hid {
            visibility: hidden;
        }
        body {
            width: 100%;
            height: 100%;
            margin: 0;
            padding: 0;
            background-color: #FAFAFA;
            font: 12pt "Tahoma";
        }
        * {
            box-sizing: border-box;
            -moz-box-sizing: border-box;
        }
        .page {
            width: 210mm;
            min-height: 297mm;
            padding: 10mm;
            margin: 10mm auto;
            border: 1px;
            border-radius: 5px;
            background: white;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
        }
        .subpage {
            padding: 0.5cm;
            border: 5px;
            height: 257mm;
            outline: 2cm;
        }
    
        @page {
            size: A4;
            margin: 4mm;
        }
        @media print {
            html, body {
                width: 210mm;
                height: 297mm;        
            }
            .page {
                margin: 0;
            
            }
        }
        .example-print {
    display: none;
}
@media print {
   .example-screen {
       display: none;
    }
    .example-print {
       display: block;
    }
}
    </style>
    <title>Fingerprint Payment System</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="book">
    <div class="page printableArea">
        <div class="subpage">
            <div class="col-xs-12">
                <div class="col-xs-12 righttext">                            
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202030") %></label>
                </div>                 
            </div>
            <div class="col-xs-12">
                <div class="hid" >                                                
                    <p>hidden</p>                         
                </div>
                </div>

            <div class="col-md-12 col-sm-12 col-xs-12 ">
                        <div class="centertext">
                            <img id="schoolpicture" runat="server"  alt=""  class="avatar img-responsive centertext" style="margin:0 auto; display:block; width:250px" />
                            
                        </div>
                    </div>
    
    <div class="col-xs-12">
                <div class="col-xs-12 centertext">                                                
                    <asp:Label ID="txtschool" cssclass="bigtxt"                                                                       
                               runat="server">                                    
                    </asp:Label>                           
                </div>
                </div>
                
             <div class="col-xs-12">
                <div class="col-xs-12 centertext">                                                
                    <asp:Label ID="txtaumpher"    cssclass="bigtxt"                                                                                
                               runat="server">                                    
                    </asp:Label> 
                    <asp:Label ID="txtprovince"   cssclass="bigtxt"                                                                                 
                               runat="server">                                    
                    </asp:Label>                            
                </div>
                </div>
            <div class="col-xs-12">
                <div class="hid" >                                                
                    <p>hidden</p>                         
                </div>
                </div>
            <div class="col-xs-12">
                <div class="col-xs-12 centertext bigtxt2" >                                                
                    <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206413") %></p>                         
                </div>
                </div>
     <div class="col-xs-12">
                <div class="col-xs-12 centertext bigtxt2" >                                                
                    <p>(Student Quality Development Form)</p>                         
                </div>
                </div>

            
            <div class="col-xs-12">
                <div class="hid" >                                                
                    <p>hidden</p>                         
                </div>
                </div>
            <div class="col-xs-12">
                <div class="hid" >                                                
                    <p>hidden</p>                         
                </div>
                </div>
             <div class="col-xs-12">
                <div class="hid" >                                                
                    <p>hidden</p>                         
                </div>
                </div>

    <div class="col-xs-12">
                <div class="col-xs-8 lefttext bigtxt3">                            
                    <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132195") %></p>
                </div>
                <div class="col-xs-4">
                    <asp:Label ID="Label1"                                                                                    
                               runat="server">                                    
                    </asp:Label>                           
                </div>                   
            </div>

            <div class="col-xs-12">
                <div class="hid" >                                                
                    <p>hidden</p>                         
                </div>
                </div>

    <div class="col-xs-12">
                <div class="col-xs-8 lefttext bigtxt3">                            
                    <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132195") %></p>
                </div>
                <div class="col-xs-4">
                    <asp:Label ID="Label2"                                                                                    
                               runat="server">                                    
                    </asp:Label>                           
                </div>                   
            </div>

             <div class="col-xs-12">
                <div class="hid" >                                                
                    <p>hidden</p>                         
                </div>
                </div>

    <div class="col-xs-12">
                <div class="col-xs-8 lefttext bigtxt3">                            
                    <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132195") %></p>
                </div>
                <div class="col-xs-4">
                    <asp:Label ID="Label3"                                                                                    
                               runat="server">                                    
                    </asp:Label>                           
                </div>                   
            </div>

             <div class="col-xs-12">
                <div class="hid" >                                                
                    <p>hidden</p>                         
                </div>
                </div>

    <div class="col-xs-12">
                <div class="col-xs-8 lefttext bigtxt3">                            
                    <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132195") %></p>
                </div>
                <div class="col-xs-4">
                    <asp:Label ID="Label4"                                                                                    
                               runat="server">                                    
                    </asp:Label>                           
                </div>                   
            </div>

             <div class="col-xs-12">
                <div class="hid" >                                                
                    <p>hidden</p>                         
                </div>
                </div>

    <div class="col-xs-12">
                <div class="col-xs-8 lefttext bigtxt3">                            
                    <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132195") %></p>
                </div>
                <div class="col-xs-4">
                    <asp:Label ID="Label5"                                                                                    
                               runat="server">                                    
                    </asp:Label>                           
                </div>                   
            </div>

             <div class="col-xs-12">
                <div class="hid" >                                                
                    <p>hidden</p>                         
                </div>
                </div>

    <div class="col-xs-12">
                <div class="col-xs-8 lefttext bigtxt3">                            
                    <p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132195") %></p>
                </div>
                <div class="col-xs-4">
                    <asp:Label ID="Label6"                                                                                    
                               runat="server">                                    
                    </asp:Label>                           
                </div>                   
            </div>
           
            
        </div>    
    </div>

    </div>
        
        <div class="book">
    <div class="page printableArea">
        <div class="subpage">
            <div class="col-xs-12">
                <div class="col-xs-12 centertext">                            
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206425") %> (Evaluation Result)</label>
                </div>                 
            </div>

        </div>    
    </div>

    </div>
        <div class="book">
    <div class="page printableArea">
        <div class="subpage">
            <div class="col-xs-12">
                <div class="col-xs-12 centertext">                            
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132196") %></label>
                </div>                 
            </div>
            
           
            
        </div>    
    </div>

    </div>
        <div class="book">
    <div class="page printableArea">
        <div class="subpage">
            <div class="col-xs-12">
                <div class="col-xs-12 centertext">                            
                    <label>โครงการสอน / แนวทางการประเมินผล (Course Outline)</label>
                </div>                 
            </div>
         
            
        </div>    
    </div>

    </div>
    </form>
</body>
</html>
