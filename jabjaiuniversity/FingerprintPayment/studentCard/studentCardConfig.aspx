<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="studentCardConfig.aspx.cs" Inherits="FingerprintPayment.studentCard.studentCardConfig" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />--%>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style>
        .ddd {
        padding:0;
        width:50px;
        text-align:center;
    }
        .ok {
    background: white; /* Border color */
    border-radius: 100px;
}

.ok > DIV {
    background: #fff; /* Background color */
    border-radius: 0px 0px 35px 0px; /* Radius of outer element minus border width */
    height: 52px; /* For illustration purposes */
    box-shadow:rgb(250, 188, 2) 0px 5px;
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
    .drawline:before{
content:"";
position:absolute;
border-bottom:1px solid black;
  width:23px;
  transform: rotate(-63deg);
  padding-top:13px;
  margin-left:-12px;
}
.table2 {
    border-collapse: collapse;
    width: 100%;
}

.tdtr {
    border: 1px solid #000000;
    text-align: center;
    padding: 8px;
    width:40px;
}
.tdtr2 {
    border: 1px solid #000000;
    text-align: center;
    padding: 0px;
    width:40px;
    font-size:80%;
    height:30px !important;
}
.tdtr3 {
    border: 1px solid #000000;
    text-align: left;
    padding-left: 3px;
    width:90px;
    font-size:90%;
    height:30px !important;
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
        .bigtxt{
            font-size:26px;
            font-weight:bold;
        }
        .pad{
            padding-top:2px;
            padding-bottom:2px;
        }
        .noborder {
            border-style: none;
            text-decoration: none;
            text-shadow: none !important;
            box-shadow: inset 0px 0px 0px 0px white;
        }
        .centertext {
        text-align: center;
    }
    
    .hid {
        visibility: hidden;
    }
    .style1{
        font-size:110%;
    }
    .style2{
        font-size:110%;
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

.tg-yw4l {
    border-left:1px solid #949191;
    border-right:1px solid #949191;
    border-bottom:1px solid #949191;
    border-top:1px solid #949191;
    text-align: center;
    padding-right:0.2px;
    padding-left:0.1px;
}
.allborder {
    border: 2px solid #000000;
    text-align: center;   
    font-family: "THSarabun"; 
}
.allborderim {
    border: 2px solid #000000 !important;
    text-align: center;   
    font-family: "THSarabun"; 
}
.allblur2 { border: solid 1px #555; border-radius: 0px 0px 35px 0px; background-color: #eed; box-shadow: 0px 10px rgba(0,0,0,0.6);  }
.allborder2 {
    border: 2px solid #000000;
    text-align: center;  
    width:50px; 
}
.weeknum{
    border:2px solid #000000;
    text-align:center;
    height:20.8px;
}
.subheader {
    border: 1px solid #000000;
    text-align: center;  
    height:20.8px; 
}
.pad0{
    padding:0px !important;
}
.rborder{
    border-left:1px solid #949191;
    border-right:2px solid #000000;
    border-bottom:1px solid #949191;
    border-top:1px solid #949191;
       
    text-align: center;
    padding-right:0.2px;
    padding-left:0.1px;
}
.smol{
    border-top:0px;
    border-left:0px;
    border-right:0px;
    border-bottom:0px;
    width:11px;
    height:20.8px;
    padding:0px;
}
.smol2{
    border-top:0px;
    border-left:0px;
    border-right:0px;
    border-bottom:0px;
    width:11px;
    height:27.9px;
    padding:0px;
}
.lborder{
    border-left:2px solid #000000;
    border-right:1px solid #949191;
    border-bottom:1px solid #949191;
    border-top:1px solid #949191;
    height:20.8px;
    text-align: center;
   padding-right:0.2px;
   padding-left:0.1px;
}
.lrborder{
    border-left:2px solid #000000;
    border-right:2px solid #000000;
    border-bottom:1px solid #949191;
    border-top:1px solid #949191;
    height:20.8px;
    text-align: center;
   font-family: "THSarabun"; 
   padding-right:0.2px;
   padding-left:0.1px;
}
.lrborder2{
    border-left:2px solid #000000;
    border-right:2px solid #000000;
    border-bottom:1px solid #949191;
    border-top:1px solid #949191;
    width:33px;
    height:20.8px;
    text-align: center;
   
   padding-right:0.2px;
   padding-left:0.1px;
}
.quitbox{
    width:100% !important;
    text-align:center;
}
.lrbborder{
    border-left:2px solid #000000;
    border-right:2px solid #000000;
    border-bottom:2px solid #000000;
    border-top:1px solid #949191;
    height:20.8px;
    text-align: center;
   padding-right:0.2px;
   padding-left:0.1px;
}
.lbborder{
    border-left:2px solid #000000;
    border-right:1px solid #949191;
    border-bottom:2px solid #000000;
    border-top:1px solid #949191;
    height:20.8px;
    text-align: center;
   padding-right:0.2px;
   padding-left:0.1px;
   font-family: "THSarabun"; 
}
.tlrborder{
    border-left:2px solid #000000;
    border-right:2px solid #000000;
    border-bottom:1px solid #949191;
    border-top:2px solid #000000;
    height:20.8px;
    text-align: center;
   padding-right:0.2px;
   padding-left:0.1px;
}
.sidbox{
    width:80px; height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                font-family: "THSarabun"; 
}
.h25{
    height:28px !important;
}
.f120{
    font-size:120% !important;
}
.f90{
    font-size:90% !important;
}
.h23{
    height:23px !important;
    padding:0px !important;
}
.blue1{
            background-color:#0247fe;
}
.blue2{
            background-color:#0391ce;
}
.green1{
            background-color:#66b032;
}
.green2{
            background-color:#d0ea2b;
}
.yellow1{
            background-color:#fefe33;
}
.yellow2{
            background-color:#fabc02;
}
.orange1{
            background-color:#fb9902;
}
.orange2{
            background-color:#fd5308;
}
.red1{
            background-color:#fe2712;
}
.red2{
            background-color:#a7194b;
}
.purple1{
            background-color:#8601af;
}
.purple2{
            background-color:#3d01a4;
}
.pink1{
            background-color:#f3b9d1;
}
.pink2{
            background-color:#f381b4;
}
.white{
            background-color:#ffffff;
}
.black{
            background-color:#222222;
}
.textshadow{
    color:white;
    text-shadow: 1px 1px 2px black;
}
.attendancebox{
    width:10px;height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                text-align:center;
}
.attendancebox2{
    width:20px;height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                text-align:center;
}
.setdatebox{
    width:10px;height:18.5px; font-size:60%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                text-align:center;
}
.setnumberbox{
    width:10px;height:18.5px; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                text-align:center;
}
.smol90{
    font-size:60%;
}
.smol60{
   font-size:40%;
}
.smol30{
    font-size:25%;
}
.setteacherbox{
    width:100%; font-size:100%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                
                background-color:white;
                text-align:left;
                
}
.paper4box{
    width:50px;height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                text-align:center;
}
.paper5box{
    width:21px;height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                text-align:center;
}
.paper5boxmax{
    width:21px;height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                text-align:center;
}
.gradenamebox{
    width:150px;  padding-top:3px; padding-bottom:3px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                text-align:left;
                font-family: "THSarabun"; 
}
.paper5box2{
    width:75px;height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                text-align:center;
                font-family: "THSarabun"; 
}
.paper5box3{
    width:125px;height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                font-family: "THSarabun"; 
}

.paper11box{
    width:21px;height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                text-align:center;
}
.paper11boxmax{
    width:21px;height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                text-align:center;
}
.grade11namebox{
    width:180px;  padding-top:3px; padding-bottom:3px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                text-align:left;
}
.paper11box2{
    width:75px;height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                text-align:center;
                font-family: "THSarabun"; 
}
.paper11box3{
    width:125px;height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                font-family: "THSarabun"; 
}
.paper4box2{
    width:90px;height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                text-align:center;
                font-family: "THSarabun"; 
}
.behave9hidden{
    display:none !important;
}
::-webkit-input-placeholder { /* Safari, Chrome and Opera */
  color: black;
}

:-moz-placeholder { /* Firefox */
  color: black;
}

:placeholder-shown { /* Default */
  color: black;
}
.behave10hidden{
    display:none !important;
}
.paper4box3{
    width:240px;height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                font-family: "THSarabun"; 
}
.paper6box{
    width:25px;height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                text-align:center;
}
.paper6box2{
    width:75px;height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                text-align:center;
}
.nullborderLeft{
    border-left:2px solid #000000 !important;
    border-right:1px solid #fff !important;
    border-bottom:1px solid #949191 !important;
    border-top:1px solid #949191 !important;
}
.nullborderRight{
    border-left:1px solid #fff !important;
    border-right:2px solid #000000;
    border-bottom:1px solid #949191 !important;
    border-top:1px solid #949191 !important;
}
.nullborderMiddle{
    border-left:1px solid #fff !important;
    border-right:1px solid #fff !important;
    border-bottom:1px solid #949191 !important;
    border-top:1px solid #949191 !important;
}
.nullborderTop{
    border-left:1px solid #949191 !important;
    border-right:1px solid #949191 !important;
    border-bottom:1px solid #fff !important;
    border-top:2px solid #000000 !important;
}
hr{
    padding:0px;
    border:1px solid black;
    margin-top:26px;
    margin-bottom:0px;
}
.nullborderBot{
    border-left:1px solid #949191 !important;
    border-right:1px solid #949191 !important;
    border-bottom:2px solid #000000 !important;
    border-top:1px solid #fff !important;
}
.nullborderCen{
    display:none !important;
}
.nullborderAll{
    border-left:1px solid #fff !important;
    border-right:1px solid #fff !important;
    border-bottom:1px solid #fff !important;
    border-top:1px solid #fff !important;
}
.paper6box3{
    width:125px;height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
}
.paper7box{
    width:25px;height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
}

.paper8box{
    width:30px;height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
}
.paper9box{
    width:50px;height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                text-align:center;
}
table           { border-collapse:collapse } /* Ensure no space between cells   */
tr.strikeout td { position:relative        } /* Setup a new coordinate system   */
tr.strikeout td:before {                     /* Create a new element that       */
  content: " ";                              /* …has no text content            */
  position: absolute;                        /* …is absolutely positioned       */
  left: 0; top: 50%; width: 100%;            /* …with the top across the middle */
  border-bottom: 1px solid red;             /* …and with a border on the top   */
}    
.h42{
    height:42px;
}
.paper9box2{
    width:85px;height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                text-align:center;
                font-family: "THSarabun"; 
}
.paper9box3{
    width:125px;height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                font-family: "THSarabun"; 
}
.paper10box{
    width:40px;height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                text-align:center;
                font-family: "THSarabun"; 
}
.paper10box2{
    width:85px;height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                text-align:center;
                font-family: "THSarabun"; 
}
.paper10box3{
    width:175px;height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
                font-family: "THSarabun"; 
}
#loading {
    display: block;
    position: fixed;
    top: 0;
    left: 0;
    z-index: 100;
    width: 100vw;
    height: 100vh;
    background-color: rgba(192, 192, 192, 0.5);
    background-image: url("https://i.imgur.com/CgViPo0.gif");
    background-repeat: no-repeat;
    background-position: center;
}

.namebox{
    width:155px;height:18.5px; font-size:90%; padding:0px;
    background:rgba(0,0,0,0);
                border:none;
                color:black;
                background-color:white;
}
.rbborder{
    border-left:1px solid #949191;
    border-right:2px solid #000000;
    border-bottom:2px solid #000000;
    border-top:1px solid #949191;
    height:20.8px;
    text-align: center;
   padding-right:0.2px;
   padding-left:0.1px;
   font-family: "THSarabun"; 
}
.bigdash{
    font-size:110%;
}
.bborder{
    border-left:1px solid #949191;
    border-right:1px solid #949191;
    border-bottom:2px solid #000000;
    border-top:1px solid #949191;
    height:20.8px;
    text-align: center;
   padding-right:0.2px;
   padding-left:0.1px;
   width:11.45px;
   font-family: "THSarabun"; 
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
            font-family: "THSarabun"; 
            font-size:22px;
        }
        
        

        * {
            box-sizing: border-box;
            -moz-box-sizing: border-box;
        }
        .page {
            width: 8.55cm;
            min-height: 5.4cm;
            padding: 10mm;
            margin-left: 20px;
            border: 1px;
            border-radius: 5px;
            background: white;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
        }
       
        .cycle{
            font-size:90%; border-radius:100%; border:solid red 1px;padding-right:0px; padding-left:0px; padding-top:0px; padding-bottom:0px;
            height:13px; text-align:center; color:red;
        }
        .subpage {
            padding: 0px;
            border: 5px;
            height: 5.4cm;
            outline: 2cm;
        }
        .width20{
            width:20%;
        }

        .pad3{
            padding:3px;
        }
        .width15{
            width:15%;
        }
        .width35{
            width:35%;
        }
        .width100{
            width:100%;
        }
        .font23{
            font-size:23px;
        }
        .font23b{
            font-size:23px;
            font-weight:bold;
        }
        .rotate {
     -moz-transform: rotate(-90.0deg);  /* FF3.5+ */
       -o-transform: rotate(-90.0deg);  /* Opera 10.5 */
  -webkit-transform: rotate(-90.0deg);  /* Saf3.1+, Chrome */
             filter:  progid:DXImageTransform.Microsoft.BasicImage(rotation=0.083);  /* IE6,IE7 */
         -ms-filter: "progid:DXImageTransform.Microsoft.BasicImage(rotation=0.083)"; /* IE8 */
         margin-left: -10em;
         margin-right: -10em;
         width:250px !important;
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
            .no-print, .no-print *
    {
        display: none !important;
    }
        }
        .example-print {
    display: none;
}
        body{
  -webkit-print-color-adjust:exact;
}
@media print {
   .example-screen {
       display: none;
    }
    .example-print {
       display: block;
    }
}

 .nopad100{
                width:40px;
                background:rgba(0,0,0,0);
                border:none;
                color:black;
            }
 .pagecut
      {
        page-break-after: always;
        page-break-inside: avoid;
      }

        label {
            font-weight: normal;
            font-size: 26px;
        }
        .disable{
            pointer-events:none;
            background-color:#f0f0f0;
        }
        .ui-autocomplete {
            position: absolute;
            cursor: default;
            z-index: 1060 !important;
        }
        .centertext {
            text-align: center;
        }
        .centerText {
            text-align: center;
        }

        .setmin-width0 {
            min-width: 0px !important;
        }

        .setfont-size14 {
            font-size: 14px !important;
        }
    </style>
    <style>
        .completionList {
            border: solid 1px #444444;
            background-color: White;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
        }
        #loading {
    display: block;
    position: fixed;
    top: 0;
    left: 0;
    z-index: 100;
    width: 100vw;
    height: 100vh;
    background-color: rgba(192, 192, 192, 0.5);
    background-image: url("https://i.imgur.com/CgViPo0.gif");
    background-repeat: no-repeat;
    background-position: center;
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
        #loading {
    display: block;
    position: fixed;
    top: 0;
    left: 0;
    z-index: 100;
    width: 100vw;
    height: 100vh;
    background-color: rgba(192, 192, 192, 0.5);
    background-image: url("https://i.imgur.com/CgViPo0.gif");
    background-repeat: no-repeat;
    background-position: center;
}
        .width10 {
            margin: 0 auto;
            width: 10%;
        }
        
        .itemHighlighted {
            background-color: #ffc0c0;
        }
       .extime{
           font-size:1% !important;
       }
        .gvbutton  {
     font-size: 25px;
     
        }
        .labelbox{
            pointer-events:none;
            border:0px;
        }
        .whitetxt{
            color:white;
        }
        .nounder a:hover{
    text-decoration: none;
        

}
        .shadowblack{
    text-decoration: none;
        text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
}
        
        .boxhead a {
    color: #FFFFFF;
    text-decoration: none;
}
        a.imjusttext{ color: #ffffff; text-decoration: none; }
a.imjusttext:hover { color: aquamarine; }
        
        .btn-red {
  background: red; /* use your color here */
}
        .lh5{
            line-height:19px;
        }
        .pad0{
            padding:0px;
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
        
        .tab {border-collapse:collapse;margin-left: 6px; margin-right: 6px; border-bottom:3px solid #337AB7; border-left:3px solid #337AB7;border-right: 3px solid #337AB7; border-top:3px solid #337AB7;box-shadow: inset 0 1px 0 #337AB7;}

        .lds-facebook {
  display: inline-block;
  position: relative;
  width: 64px;
  height: 64px;
}
.lds-facebook div {
  display: inline-block;
  position: absolute;
  left: 6px;
  width: 13px;
  background: #cef;
  animation: lds-facebook 1.2s cubic-bezier(0, 0.5, 0.5, 1) infinite;
}
.lds-facebook div:nth-child(1) {
  left: 6px;
  animation-delay: -0.24s;
}
.lds-facebook div:nth-child(2) {
  left: 26px;
  animation-delay: -0.12s;
}
.lds-facebook div:nth-child(3) {
  left: 45px;
  animation-delay: 0;
}
.buttonhidden1{
    display: none !important;
}
.buttonhidden2{
    display: none !important;
}
@keyframes lds-facebook {
  0% {
    top: 6px;
    height: 51px;
  }
  50%, 100% {
    top: 19px;
    height: 26px;
  }
}

    </style>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/4.4.0/bootbox.min.js"></script>
    
<script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.8.0/jszip.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.8.0/xlsx.js"></script>

    <script type="text/javascript" language="javascript">

        function changecolor1() {
            var modalColor = document.getElementsByClassName("modalColor");
            var color1 = document.getElementById("color1");            

            if (modalColor[0].value == "1") color1.className = "col-xs-11 pad0 white";
            else if (modalColor[0].value == "2") color1.className = "col-xs-11 pad0 blue1 textshadow";
            else if (modalColor[0].value == "3") color1.className = "col-xs-11 pad0 blue2 textshadow";
            else if (modalColor[0].value == "4") color1.className = "col-xs-11 pad0 green1 textshadow";
            else if (modalColor[0].value == "5") color1.className = "col-xs-11 pad0 green2 textshadow";
            else if (modalColor[0].value == "6") color1.className = "col-xs-11 pad0 yellow1 textshadow";
            else if (modalColor[0].value == "7") color1.className = "col-xs-11 pad0 yellow2 textshadow";
            else if (modalColor[0].value == "8") color1.className = "col-xs-11 pad0 orange1 textshadow";
            else if (modalColor[0].value == "9") color1.className = "col-xs-11 pad0 orange2 textshadow";
            else if (modalColor[0].value == "10") color1.className = "col-xs-11 pad0 red1 textshadow";
            else if (modalColor[0].value == "11") color1.className = "col-xs-11 pad0 red2 textshadow";
            else if (modalColor[0].value == "12") color1.className = "col-xs-11 pad0 purple1 textshadow";
            else if (modalColor[0].value == "13") color1.className = "col-xs-11 pad0 purple2 textshadow";
            else if (modalColor[0].value == "14") color1.className = "col-xs-11 pad0 black textshadow";
            else if (modalColor[0].value == "15") color1.className = "col-xs-11 pad0 pink1 textshadow";
            else if (modalColor[0].value == "16") color1.className = "col-xs-11 pad0 pink2 textshadow";
            else if (modalColor[0].value == "0") {
                color1.className = "col-xs-11 pad0";
            }
                
            
        }

        function changecolor2() {
            var modalColor = document.getElementsByClassName("modalColor");
            var color1 = document.getElementById("color1");

            if (modalColor[1].value == "1") color1.style.boxShadow = "0px 5px #ffffff";
            else if (modalColor[1].value == "2") color1.style.boxShadow = "0px 5px #0247fe";
            else if (modalColor[1].value == "3") color1.style.boxShadow = "0px 5px #0391ce";
            else if (modalColor[1].value == "4") color1.style.boxShadow = "0px 5px #66b032";
            else if (modalColor[1].value == "5") color1.style.boxShadow = "0px 5px #d0ea2b";
            else if (modalColor[1].value == "6") color1.style.boxShadow = "0px 5px #fefe33";
            else if (modalColor[1].value == "7") color1.style.boxShadow = "0px 5px #fabc02";
            else if (modalColor[1].value == "8") color1.style.boxShadow = "0px 5px #fb9902";
            else if (modalColor[1].value == "9") color1.style.boxShadow = "0px 5px #fd5308";
            else if (modalColor[1].value == "10") color1.style.boxShadow = "0px 5px #fe2712";
            else if (modalColor[1].value == "11") color1.style.boxShadow = "0px 5px #a7194b";
            else if (modalColor[1].value == "12") color1.style.boxShadow = "0px 5px #8601af";
            else if (modalColor[1].value == "13") color1.style.boxShadow = "0px 5px #3d01a4";
            else if (modalColor[1].value == "14") color1.style.boxShadow = "0px 5px #222222";
            else if (modalColor[1].value == "15") color1.style.boxShadow = "0px 5px #f3b9d1";
            else if (modalColor[1].value == "16") color1.style.boxShadow = "0px 5px #f381b4";
            else if (modalColor[1].value == "0") {
                color1.style.boxShadow = "";
            }


        }

        function start() {
            var h1 = document.getElementsByClassName("header1");
            var h2 = document.getElementsByClassName("header2");
            var body1 = document.getElementsByClassName("body1");
            var body2 = document.getElementsByClassName("body2");
            var body3 = document.getElementsByClassName("body3");
            var bottom1 = document.getElementsByClassName("bottom1");
            var bottom2 = document.getElementsByClassName("bottom2");
            
            h1[0].textContent = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133301") %>";
            h2[0].textContent = "NARIVITAYA SCHOOL";
            body1[0].textContent = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133302") %>";
            body2[0].textContent = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133303") %>";
            body3[0].textContent = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133304") %>";
            bottom1[0].textContent = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133305") %>";
            bottom2[0].textContent = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133306") %>";
            
        }


       

       
       

      

        function bootbox2() {
            bootbox.alert({
                message: '<h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206133") %></h3>',
                backdrop: true
            });
        }

       
        window.onload = start;
    </script>
    
    
    
    <div class="full-card box-content userlist-container col-xs-8" style="background-color:white;">
       
        <asp:HiddenField ID="hdfsid" runat="server" />
        <div id="loading" class="loadstatus hidden"></div>
    <div class="col-xs-12 lefttext">
        <h2 class="lefttext"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133307") %>
            </h2>
        </div>

         <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                    <ContentTemplate>

        <div class="col-xs-12">
            <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133308") %> :</h3>
            </div>
                
       <div class="col-xs-12">
            <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133309") %> :</h3>
            </div>
        
        
                                       
          <div class="col-xs-12">
            <div class="col-xs-12 pad0">
                <div class="col-xs-3 pad0">
                <h3 class="lh5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133310") %></h3>
                    </div>
                <div class="col-xs-5">
                    <asp:DropDownList ID="DropDownList1" runat="server" class="form-control modalColor" width="100%" onchange="changecolor1()">
                    <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133316") %>" Value="-1" class="hidden" ></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106037") %>" Value="0" class="" ></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106040") %>" value="1"  class="white"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133317") %>" Value="2" class="blue1 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133318") %>" value="3"  class="blue2 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133319") %>" value="4"  class="green1 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133320") %>" value="5"  class="green2 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133321") %>" Value="6" class="yellow1 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133322") %>" value="7"  class="yellow2 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133323") %>" value="8"  class="orange1 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133324") %>" value="9"  class="orange2 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133325") %>" value="15"  class="pink1 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133326") %>" value="16"  class="pink2 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133327") %>" Value="10" class="red1 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133328") %>" value="11"  class="red2 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133329") %>" value="12"  class="purple1 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133330") %>" value="13"  class="purple2 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106039") %>" Value="14" class="black textshadow"></asp:ListItem>                    
                </asp:DropDownList>
                    </div>
                </div>           
            </div>

         <div class="col-xs-12">
            <div class="col-xs-12 pad0">
                <div class="col-xs-3 pad0">
                <h3 class="lh5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133311") %></h3>
                    </div>
                <div class="col-xs-5">
                    <asp:DropDownList ID="DropDownList4" runat="server" class="form-control modalColor" width="100%" onchange="changecolor2()">
                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133316") %>" Value="-1" class="hidden" ></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106037") %>" Value="0" class="" ></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106040") %>" value="1"  class="white"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133317") %>" Value="2" class="blue1 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133318") %>" value="3"  class="blue2 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133319") %>" value="4"  class="green1 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133320") %>" value="5"  class="green2 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133321") %>" Value="6" class="yellow1 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133322") %>" value="7"  class="yellow2 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133323") %>" value="8"  class="orange1 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133324") %>" value="9"  class="orange2 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133325") %>" value="15"  class="pink1 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133326") %>" value="16"  class="pink2 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133327") %>" Value="10" class="red1 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133328") %>" value="11"  class="red2 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133329") %>" value="12"  class="purple1 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133330") %>" value="13"  class="purple2 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106039") %>" Value="14" class="black textshadow"></asp:ListItem>                    
                </asp:DropDownList>
                    </div>
                </div>           
            </div>
       

                                       

        <div class="col-xs-12">            
            
            <div class="col-xs-12 pad0">
                <div class="col-xs-3 pad0">
                <h3 class="lh5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133312") %></h3>
                    </div>
                <div class="col-xs-5">
                    <asp:DropDownList ID="DropDownList2" runat="server" class="form-control modalColor" width="100%" onchange="changecolor3()">
                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133316") %>" Value="-1" class="hidden" ></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106037") %>" Value="0" class="" ></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106040") %>" value="1"  class="white"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133317") %>" Value="2" class="blue1 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133318") %>" value="3"  class="blue2 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133319") %>" value="4"  class="green1 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133320") %>" value="5"  class="green2 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133321") %>" Value="6" class="yellow1 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133322") %>" value="7"  class="yellow2 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133323") %>" value="8"  class="orange1 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133324") %>" value="9"  class="orange2 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133325") %>" value="15"  class="pink1 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133326") %>" value="16"  class="pink2 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133327") %>" Value="10" class="red1 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133328") %>" value="11"  class="red2 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133329") %>" value="12"  class="purple1 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133330") %>" value="13"  class="purple2 textshadow"></asp:ListItem>
                    <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106039") %>" Value="14" class="black textshadow"></asp:ListItem>                    
                </asp:DropDownList>
                    </div>
                </div>           
            </div>
                                       

                                   <div class="col-xs-12 uploadbutton">
            <div class="col-xs-12 pad0">
                <div class="col-xs-3 pad0">
                <h3 class="lh5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133314") %></h3>
                    </div>
                <div class="col-xs-3">
                    <form enctype="multipart/form-data">
    <input id="upload" type=file  name="files[]">
</form>
                    </div>
                </div>           
            </div>            
                                                
                                         <div class="col-xs-12 uploadbutton">
            <div class="col-xs-12 pad0">
                <div class="col-xs-3 pad0">
                <h3 class="lh5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133315") %></h3>
                    </div>
                <div class="col-xs-3">
                    <form enctype="multipart/form-data">
    <input id="upload" type=file  name="files[]">
</form>
                    </div>
                </div>           
            </div>          
                                         <div class="col-xs-12">            
            
            <div class="col-xs-12 pad0">
                <div class="col-xs-4 pad0">
                <h3 class="lh5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133313") %></h3>
                    </div>
                <div class="col-xs-4">
                    <asp:DropDownList ID="DropDownList3" runat="server" onchange="ddlterm()" CssClass="ddl2 form-control disabletarget">
                        <asp:ListItem Value="0" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202002") %>" Selected="True"></asp:ListItem>                         
                            </asp:DropDownList>
                    </div>
                </div>           
            </div>                                                                      
                                      
                                    </ContentTemplate>
                                    <Triggers>                                        
                                        <asp:AsyncPostBackTrigger ControlID="DropDownList1" EventName="SelectedIndexChanged" />                                     
                                    </Triggers>
                </asp:UpdatePanel>
        
        
        

    <script>
        document.getElementById('upload').addEventListener('change', handleFileSelect, false);

    </script>

     
           
    
      
        <div class="uploadstatus">
            <div class="col-xs-12">
            <div class="col-xs-12 pad0">
                <div class="col-xs-10 pad0">
                
                    </div>
                <div class="col-xs-1 hidden">
                    <asp:Button ID="Button2" class="btn btn-danger global-btn" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206118") %>" />
                    </div>
                <div class="col-xs-2 savebutton1" onclick="showload()">
                    <asp:Button ID="Button1" class="btn btn-success global-btn" runat="server" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %>" ValidationGroup="add" />
                    </div>
                <div class="col-xs-2 savebutton2 hidden">
                    <div class="btn btn-success" onclick="bootbox2()"
                                > <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103144") %></div>  
                    </div>
                </div>           
            </div>

        
      </div>
            </div>
       
    <div class="col-xs-4 pad0" style="height:220px">
        <div class="" style="text-align:center">
            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133331") %></label>
            </div>
     <div class="book normalpage extrapage">
    <div class="page printableArea pagecut" style="padding:0px;">
        <div class="subpage">
          
     
          <div class="col-xs-12 pad0" style="background-color:blue;height:5.4cm;">
              <div id="color1" class="col-xs-11 pad0 white" style="height:1.3cm; border-radius: 0px 0px 35px 0px; box-shadow: 0px 5px rgba(0,0,0,0.6);"><div>
                  <div class="col-xs-12 pad0">
                  <div class="col-xs-4 pad0">
                      <img id="schoolpicture" runat="server"   alt=""  class="avatar img-responsive centertext" style="margin:0 auto; display:block; width:1.7cm; height:1.7cm;" />
                      </div>
                  <div class="col-xs-8 pad0">
                      <div class="col-xs-12 pad0" style="height:20px;">
                      <asp:Label ID="configratio" cssclass="header1" runat="server" style="height:20px;"> </asp:Label>
                          </div>

                      <hr align="left" style="width:85%"/>
                       <div class="col-xs-12 pad0" style="height:20px;">
                      <asp:Label ID="Label1" cssclass="header2" runat="server" style="line-height:22px;"> </asp:Label>
                          </div>
                      </div>
                      </div>
                  
        </div> </div>
            <div class="col-xs-12 pad0" style="height:91px;margin-top:20px;">
                  <div class="col-xs-4 pad0">
                      <img id="Img1" runat="server"   alt=""  class="avatar img-responsive centertext" style="margin:0 auto; display:block; width:78px; height:88px;" />
                      </div>
                  <div class="col-xs-8 pad0">
                      <div class="col-xs-12 pad0" style="height:25px; font-size:90%;">
                      <asp:Label ID="Label2" cssclass="body1" runat="server" style=""> </asp:Label>
                          </div>
                      <div class="col-xs-12 pad0" style="height:25px;font-size:90%;">
                      <asp:Label ID="Label3" cssclass="body2" runat="server" style=""> </asp:Label>
                          </div>
                      <div class="col-xs-12 pad0" style="height:25px;font-size:90%;">
                      <asp:Label ID="Label4" cssclass="body3" runat="server" style=""> </asp:Label>
                          </div>
                      <div class="col-xs-12 pad0" style="z-index:9;">
                    <img id="Img2" runat="server"   alt=""  class="avatar img-responsive centertext" style="margin:0 auto; display:block; width:95%; height:52px; margin-top:3px;" />
                    </div>
                      </div>
                      
                      </div>
              <div class="col-xs-12 pad0" style="background-color:pink;height:1.15cm;">
                <div class="col-xs-5 pad0">
                    <div class="col-xs-12 pad0" style="height:20px; font-size:75%;margin-left:5px;">
                      <asp:Label ID="Label5" cssclass="bottom1" runat="server" style=""> </asp:Label>
                          </div>
                    <div class="col-xs-12 pad0" style="height:20px; font-size:75%;margin-left:5px;">
                      <asp:Label ID="Label6" cssclass="bottom2" runat="server" style=""> </asp:Label>
                          </div>
                    </div>
                
         </div>
         </div>
            
        </div>    
    </div>
            
    </div>

        <div class="" style="text-align:center">
            <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133332") %></label>
            </div>
     <div class="book normalpage extrapage">
    <div class="page printableArea pagecut" style="padding:0px;">
        <div class="subpage">
          
     
      
        </div>    
    </div>
            
    </div>
        </div>
        
        
            </div>

     <div class="col-xs-4 pad0" style="height:220px">
        
        </div>
        
        
            </div>
        </div>
    </div>
    
</asp:Content>

