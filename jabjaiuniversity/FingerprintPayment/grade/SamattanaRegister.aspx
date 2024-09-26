<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="SamattanaRegister.aspx.cs" Inherits="FingerprintPayment.grade.SamattanaRegister" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="full-card planelist-container" style="width: 100%; padding:0px;">
         
        <link rel="stylesheet" href="//min.gitcdn.xyz/repo/wintercounter/Protip/master/protip.min.css">
        <script src="//min.gitcdn.xyz/repo/wintercounter/Protip/master/protip.min.js"></script>
        <style>
            /* W3.CSS 4.10 February 2018 by Jan Egil and Borge Refsnes */
             
/* End extract */

.w3-image{max-width:100%;height:auto}img{vertical-align:middle}a{color:inherit}
.w3-table,.w3-table-all{border-collapse:collapse;border-spacing:0;width:100%;display:table}.w3-table-all{border:1px solid #ccc}
.w3-bordered tr,.w3-table-all tr{border-bottom:1px solid #ddd}.w3-striped tbody tr:nth-child(even){background-color:#f1f1f1}
.w3-table-all tr:nth-child(odd){background-color:#fff}.w3-table-all tr:nth-child(even){background-color:#f1f1f1}
.w3-hoverable tbody tr:hover,.w3-ul.w3-hoverable li:hover{background-color:#ccc}.w3-centered tr th,.w3-centered tr td{text-align:center}
.w3-table td,.w3-table th,.w3-table-all td,.w3-table-all th{padding:8px 8px;display:table-cell;text-align:left;vertical-align:top}
.w3-table th:first-child,.w3-table td:first-child,.w3-table-all th:first-child,.w3-table-all td:first-child{padding-left:16px}
.w3-btn,.w3-button{border:none;display:inline-block;padding:8px 16px;vertical-align:middle;overflow:hidden;text-decoration:none;color:inherit;background-color:inherit;text-align:center;cursor:pointer;white-space:nowrap}
.w3-btn:hover{box-shadow:0 8px 16px 0 rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19)}
.w3-btn,.w3-button{-webkit-touch-callout:none;-webkit-user-select:none;-khtml-user-select:none;-moz-user-select:none;-ms-user-select:none;user-select:none}   
.w3-disabled,.w3-btn:disabled,.w3-button:disabled{cursor:not-allowed;opacity:0.3}.w3-disabled *,:disabled *{pointer-events:none}
.w3-btn.w3-disabled:hover,.w3-btn:disabled:hover{box-shadow:none}
.w3-badge,.w3-tag{background-color:#000;color:#fff;display:inline-block;padding-left:8px;padding-right:8px;text-align:center}.w3-badge{border-radius:50%}
.w3-ul{list-style-type:none;padding:0;margin:0}.w3-ul li{padding:8px 16px;border-bottom:1px solid #ddd}.w3-ul li:last-child{border-bottom:none}
.w3-tooltip,.w3-display-container{position:relative}.w3-tooltip .w3-text{display:none}.w3-tooltip:hover .w3-text{display:inline-block}
.w3-ripple:active{opacity:0.5}.w3-ripple{transition:opacity 0s}
.w3-input{padding:8px;display:block;border:none;border-bottom:1px solid #ccc;width:100%}
.w3-select{padding:9px 0;width:100%;border:none;border-bottom:1px solid #ccc}
.w3-dropdown-click,.w3-dropdown-hover{position:relative;display:inline-block;cursor:pointer}
.w3-dropdown-hover:hover .w3-dropdown-content{display:block}
.w3-dropdown-hover:first-child,.w3-dropdown-click:hover{background-color:#ccc;color:#000}
.w3-dropdown-hover:hover > .w3-button:first-child,.w3-dropdown-click:hover > .w3-button:first-child{background-color:#ccc;color:#000}
.w3-dropdown-content{cursor:auto;color:#000;background-color:#fff;display:none;position:absolute;min-width:160px;margin:0;padding:0;z-index:1}
.w3-check,.w3-radio{width:24px;height:24px;position:relative;top:6px}
.w3-sidebar{height:100%;width:200px;background-color:#fff;position:fixed!important;z-index:1;overflow:auto}
.w3-bar-block .w3-dropdown-hover,.w3-bar-block .w3-dropdown-click{width:100%}
.w3-bar-block .w3-dropdown-hover .w3-dropdown-content,.w3-bar-block .w3-dropdown-click .w3-dropdown-content{min-width:100%}
.w3-bar-block .w3-dropdown-hover .w3-button,.w3-bar-block .w3-dropdown-click .w3-button{width:100%;text-align:left;padding:8px 16px}
.w3-main,#main{transition:margin-left .4s}
.w3-modal{z-index:3;display:none;padding-top:100px;position:fixed;left:0;top:0;width:100%;height:100%;overflow:auto;background-color:rgb(0,0,0);background-color:rgba(0,0,0,0.4)}
.w3-modal-content{margin:auto;background-color:#fff;position:relative;padding:0;outline:0;width:600px}
.w3-bar{width:100%;overflow:hidden}.w3-center .w3-bar{display:inline-block;width:auto}
.w3-bar .w3-bar-item{padding:8px 16px;float:left;width:auto;border:none;display:block;outline:0}
.w3-bar .w3-dropdown-hover,.w3-bar .w3-dropdown-click{position:static;float:left}
.w3-bar .w3-button{white-space:normal}
.w3-bar-block .w3-bar-item{width:100%;display:block;padding:8px 16px;text-align:left;border:none;white-space:normal;float:none;outline:0}
.w3-bar-block.w3-center .w3-bar-item{text-align:center}.w3-block{display:block;width:100%}
.w3-responsive{display:block;overflow-x:auto}
.w3-container:after,.w3-container:before,.w3-panel:after,.w3-panel:before,.w3-row:after,.w3-row:before,.w3-row-padding:after,.w3-row-padding:before,
.w3-cell-row:before,.w3-cell-row:after,.w3-clear:after,.w3-clear:before,.w3-bar:before,.w3-bar:after{content:"";display:table;clear:both}
.w3-col,.w3-half,.w3-third,.w3-twothird,.w3-threequarter,.w3-quarter{float:left;width:100%}
.w3-col.s1{width:8.33333%}.w3-col.s2{width:16.66666%}.w3-col.s3{width:24.99999%}.w3-col.s4{width:33.33333%}
.w3-col.s5{width:41.66666%}.w3-col.s6{width:49.99999%}.w3-col.s7{width:58.33333%}.w3-col.s8{width:66.66666%}
.w3-col.s9{width:74.99999%}.w3-col.s10{width:83.33333%}.w3-col.s11{width:91.66666%}.w3-col.s12{width:99.99999%}
@media (min-width:601px){.w3-col.m1{width:8.33333%}.w3-col.m2{width:16.66666%}.w3-col.m3,.w3-quarter{width:24.99999%}.w3-col.m4,.w3-third{width:33.33333%}
.w3-col.m5{width:41.66666%}.w3-col.m6,.w3-half{width:49.99999%}.w3-col.m7{width:58.33333%}.w3-col.m8,.w3-twothird{width:66.66666%}
.w3-col.m9,.w3-threequarter{width:74.99999%}.w3-col.m10{width:83.33333%}.w3-col.m11{width:91.66666%}.w3-col.m12{width:99.99999%}}
@media (min-width:993px){.w3-col.l1{width:8.33333%}.w3-col.l2{width:16.66666%}.w3-col.l3{width:24.99999%}.w3-col.l4{width:33.33333%}
.w3-col.l5{width:41.66666%}.w3-col.l6{width:49.99999%}.w3-col.l7{width:58.33333%}.w3-col.l8{width:66.66666%}
.w3-col.l9{width:74.99999%}.w3-col.l10{width:83.33333%}.w3-col.l11{width:91.66666%}.w3-col.l12{width:99.99999%}}
.w3-content{max-width:980px;margin:auto}.w3-rest{overflow:hidden}
.w3-cell-row{display:table;width:100%}.w3-cell{display:table-cell}
.w3-cell-top{vertical-align:top}.w3-cell-middle{vertical-align:middle}.w3-cell-bottom{vertical-align:bottom}
.w3-hide{display:none!important}.w3-show-block,.w3-show{display:block!important}.w3-show-inline-block{display:inline-block!important}
@media (max-width:600px){.w3-modal-content{margin:0 10px;width:auto!important}.w3-modal{padding-top:30px}
.w3-dropdown-hover.w3-mobile .w3-dropdown-content,.w3-dropdown-click.w3-mobile .w3-dropdown-content{position:relative}	
.w3-hide-small{display:none!important}.w3-mobile{display:block;width:100%!important}.w3-bar-item.w3-mobile,.w3-dropdown-hover.w3-mobile,.w3-dropdown-click.w3-mobile{text-align:center}
.w3-dropdown-hover.w3-mobile,.w3-dropdown-hover.w3-mobile .w3-btn,.w3-dropdown-hover.w3-mobile .w3-button,.w3-dropdown-click.w3-mobile,.w3-dropdown-click.w3-mobile .w3-btn,.w3-dropdown-click.w3-mobile .w3-button{width:100%}}
@media (max-width:768px){.w3-modal-content{width:500px}.w3-modal{padding-top:50px}}
@media (min-width:993px){.w3-modal-content{width:900px}.w3-hide-large{display:none!important}.w3-sidebar.w3-collapse{display:block!important}}
@media (max-width:992px) and (min-width:601px){.w3-hide-medium{display:none!important}}
@media (max-width:992px){.w3-sidebar.w3-collapse{display:none}.w3-main{margin-left:0!important;margin-right:0!important}}
.w3-top,.w3-bottom{position:fixed;width:100%;z-index:1}.w3-top{top:0}.w3-bottom{bottom:0}
.w3-overlay{position:fixed;display:none;width:100%;height:100%;top:0;left:0;right:0;bottom:0;background-color:rgba(0,0,0,0.5);z-index:2}
.w3-display-topleft{position:absolute;left:0;top:0}.w3-display-topright{position:absolute;right:0;top:0}
.w3-display-bottomleft{position:absolute;left:0;bottom:0}.w3-display-bottomright{position:absolute;right:0;bottom:0}
.w3-display-middle{position:absolute;top:50%;left:50%;transform:translate(-50%,-50%);-ms-transform:translate(-50%,-50%)}
.w3-display-left{position:absolute;top:50%;left:0%;transform:translate(0%,-50%);-ms-transform:translate(-0%,-50%)}
.w3-display-right{position:absolute;top:50%;right:0%;transform:translate(0%,-50%);-ms-transform:translate(0%,-50%)}
.w3-display-topmiddle{position:absolute;left:50%;top:0;transform:translate(-50%,0%);-ms-transform:translate(-50%,0%)}
.w3-display-bottommiddle{position:absolute;left:50%;bottom:0;transform:translate(-50%,0%);-ms-transform:translate(-50%,0%)}
.w3-display-container:hover .w3-display-hover{display:block}.w3-display-container:hover span.w3-display-hover{display:inline-block}.w3-display-hover{display:none}
.w3-display-position{position:absolute}
.w3-circle{border-radius:50%}
.w3-round-small{border-radius:2px}.w3-round,.w3-round-medium{border-radius:4px}.w3-round-large{border-radius:8px}.w3-round-xlarge{border-radius:16px}.w3-round-xxlarge{border-radius:32px}
.w3-row-padding,.w3-row-padding>.w3-half,.w3-row-padding>.w3-third,.w3-row-padding>.w3-twothird,.w3-row-padding>.w3-threequarter,.w3-row-padding>.w3-quarter,.w3-row-padding>.w3-col{padding:0 8px}
.w3-container,.w3-panel{padding:0.01em 16px}.w3-panel{margin-top:16px;margin-bottom:16px}
.w3-code,.w3-codespan{font-family:Consolas,"courier new";font-size:16px}
.w3-code{width:auto;background-color:#fff;padding:8px 12px;border-left:4px solid #4CAF50;word-wrap:break-word}
.w3-codespan{color:crimson;background-color:#f1f1f1;padding-left:4px;padding-right:4px;font-size:110%}
.w3-card,.w3-card-2{box-shadow:0 2px 5px 0 rgba(0,0,0,0.16),0 2px 10px 0 rgba(0,0,0,0.12)}
.w3-card-4,.w3-hover-shadow:hover{box-shadow:0 4px 10px 0 rgba(0,0,0,0.2),0 4px 20px 0 rgba(0,0,0,0.19)}
.w3-spin{animation:w3-spin 2s infinite linear}@keyframes w3-spin{0%{transform:rotate(0deg)}100%{transform:rotate(359deg)}}
.w3-animate-fading{animation:fading 10s infinite}@keyframes fading{0%{opacity:0}50%{opacity:1}100%{opacity:0}}
.w3-animate-opacity{animation:opac 0.8s}@keyframes opac{from{opacity:0} to{opacity:1}}
.w3-animate-top{position:relative;animation:animatetop 0.4s}@keyframes animatetop{from{top:-300px;opacity:0} to{top:0;opacity:1}}
.w3-animate-left{position:relative;animation:animateleft 0.4s}@keyframes animateleft{from{left:-300px;opacity:0} to{left:0;opacity:1}}
.w3-animate-right{position:relative;animation:animateright 0.4s}@keyframes animateright{from{right:-300px;opacity:0} to{right:0;opacity:1}}
.w3-animate-bottom{position:relative;animation:animatebottom 0.4s}@keyframes animatebottom{from{bottom:-300px;opacity:0} to{bottom:0;opacity:1}}
.w3-animate-zoom {animation:animatezoom 0.6s}@keyframes animatezoom{from{transform:scale(0)} to{transform:scale(1)}}
.w3-animate-input{transition:width 0.4s ease-in-out}.w3-animate-input:focus{width:100%!important}
.w3-opacity,.w3-hover-opacity:hover{opacity:0.60}.w3-opacity-off,.w3-hover-opacity-off:hover{opacity:1}
.w3-opacity-max{opacity:0.25}.w3-opacity-min{opacity:0.75}
.w3-greyscale-max,.w3-grayscale-max,.w3-hover-greyscale:hover,.w3-hover-grayscale:hover{filter:grayscale(100%)}
.w3-greyscale,.w3-grayscale{filter:grayscale(75%)}.w3-greyscale-min,.w3-grayscale-min{filter:grayscale(50%)}
.w3-sepia{filter:sepia(75%)}.w3-sepia-max,.w3-hover-sepia:hover{filter:sepia(100%)}.w3-sepia-min{filter:sepia(50%)}
.w3-tiny{font-size:10px!important}.w3-small{font-size:12px!important}.w3-medium{font-size:15px!important}.w3-large{font-size:18px!important}
.w3-xlarge{font-size:24px!important}.w3-xxlarge{font-size:36px!important}.w3-xxxlarge{font-size:48px!important}.w3-jumbo{font-size:64px!important}
.w3-left-align{text-align:left!important}.w3-right-align{text-align:right!important}.w3-justify{text-align:justify!important}.w3-center{text-align:center!important}
.w3-border-0{border:0!important}.w3-border{border:1px solid #ccc!important}
.w3-border-top{border-top:1px solid #ccc!important}.w3-border-bottom{border-bottom:1px solid #ccc!important}
.w3-border-left{border-left:1px solid #ccc!important}.w3-border-right{border-right:1px solid #ccc!important}
.w3-topbar{border-top:6px solid #ccc!important}.w3-bottombar{border-bottom:6px solid #ccc!important}
.w3-leftbar{border-left:6px solid #ccc!important}.w3-rightbar{border-right:6px solid #ccc!important}
.w3-section,.w3-code{margin-top:16px!important;margin-bottom:16px!important}
.w3-margin{margin:16px!important}.w3-margin-top{margin-top:16px!important}.w3-margin-bottom{margin-bottom:16px!important}
.w3-margin-left{margin-left:16px!important}.w3-margin-right{margin-right:16px!important}
.w3-padding-small{padding:4px 8px!important}.w3-padding{padding:8px 16px!important}.w3-padding-large{padding:12px 24px!important}
.w3-padding-16{padding-top:16px!important;padding-bottom:16px!important}.w3-padding-24{padding-top:24px!important;padding-bottom:24px!important}
.w3-padding-32{padding-top:32px!important;padding-bottom:32px!important}.w3-padding-48{padding-top:48px!important;padding-bottom:48px!important}
.w3-padding-64{padding-top:64px!important;padding-bottom:64px!important}
.w3-left{float:left!important}.w3-right{float:right!important}
.w3-button:hover{color:#000!important;background-color:#ccc!important}
.w3-transparent,.w3-hover-none:hover{background-color:transparent!important}
.w3-hover-none:hover{box-shadow:none!important}
/* Colors */
.w3-amber,.w3-hover-amber:hover{color:#000!important;background-color:#ffc107!important}
.w3-aqua,.w3-hover-aqua:hover{color:#000!important;background-color:#00ffff!important}
.w3-blue,.w3-hover-blue:hover{color:#fff!important;background-color:#2196F3!important}
.w3-light-blue,.w3-hover-light-blue:hover{color:#000!important;background-color:#87CEEB!important}
.w3-brown,.w3-hover-brown:hover{color:#fff!important;background-color:#795548!important}
.w3-cyan,.w3-hover-cyan:hover{color:#000!important;background-color:#00bcd4!important}
.w3-blue-grey,.w3-hover-blue-grey:hover,.w3-blue-gray,.w3-hover-blue-gray:hover{color:#fff!important;background-color:#607d8b!important}
.w3-green,.w3-hover-green:hover{color:#fff!important;background-color:#4CAF50!important}
.w3-light-green,.w3-hover-light-green:hover{color:#000!important;background-color:#8bc34a!important}
.w3-indigo,.w3-hover-indigo:hover{color:#fff!important;background-color:#3f51b5!important}
.w3-khaki,.w3-hover-khaki:hover{color:#000!important;background-color:#f0e68c!important}
.w3-lime,.w3-hover-lime:hover{color:#000!important;background-color:#cddc39!important}
.w3-orange,.w3-hover-orange:hover{color:#000!important;background-color:#ff9800!important}
.w3-deep-orange,.w3-hover-deep-orange:hover{color:#fff!important;background-color:#ff5722!important}
.w3-pink,.w3-hover-pink:hover{color:#fff!important;background-color:#e91e63!important}
.w3-purple,.w3-hover-purple:hover{color:#fff!important;background-color:#9c27b0!important}
.w3-deep-purple,.w3-hover-deep-purple:hover{color:#fff!important;background-color:#673ab7!important}
.w3-red,.w3-hover-red:hover{color:#fff!important;background-color:#f44336!important}
.w3-sand,.w3-hover-sand:hover{color:#000!important;background-color:#fdf5e6!important}
.w3-teal,.w3-hover-teal:hover{color:#fff!important;background-color:#009688!important}
.w3-yellow,.w3-hover-yellow:hover{color:#000!important;background-color:#ffeb3b!important}
.w3-white,.w3-hover-white:hover{color:#000!important;background-color:#fff!important}
.w3-black,.w3-hover-black:hover{color:#fff!important;background-color:#000!important}
.w3-grey,.w3-hover-grey:hover,.w3-gray,.w3-hover-gray:hover{color:#000!important;background-color:#9e9e9e!important}
.w3-light-grey,.w3-hover-light-grey:hover,.w3-light-gray,.w3-hover-light-gray:hover{color:#000!important;background-color:#f1f1f1!important}
.w3-dark-grey,.w3-hover-dark-grey:hover,.w3-dark-gray,.w3-hover-dark-gray:hover{color:#fff!important;background-color:#616161!important}
.w3-pale-red,.w3-hover-pale-red:hover{color:#000!important;background-color:#ffdddd!important}
.w3-pale-green,.w3-hover-pale-green:hover{color:#000!important;background-color:#ddffdd!important}
.w3-pale-yellow,.w3-hover-pale-yellow:hover{color:#000!important;background-color:#ffffcc!important}
.w3-pale-blue,.w3-hover-pale-blue:hover{color:#000!important;background-color:#ddffff!important}

            .btn2 {
    border-radius: 0;
    border: 0;
    border-bottom: 4px solid #CCCCCC;
    margin:0;
    -webkit-box-shadow: 0 5px 5px -6px rgba(0,0,0,.3);
       -moz-box-shadow: 0 5px 5px -6px rgba(0,0,0,.3);
            box-shadow: 0 5px 5px -6px rgba(0,0,0,.3);
}
.btn2 .btn2-block:active, .btn2 .btn2-lg:active {
    -webkit-box-shadow: inset 0 3px 3px -5px rgba(0,0,0,.3);
       -moz-box-shadow: inset 0 3px 3px -5px rgba(0,0,0,.3);
            box-shadow: inset 0 3px 3px -5px rgba(0,0,0,.3);
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

.btn2-danger {
    color:white;
    background-color: #d73814;
    border-color: #be0000;
    text-shadow: 1px 1px 0 #ac2925;
}
.btn2-danger:hover, .btn2-danger:focus {
    background-color: #cd3714;
    border-color: #aa0000;
}
.btn-primary {
    background-color: #4274d7;
    border-color: #4d5bbe;
    text-shadow: 1px 1px 0 #232bd5;
}
.btn-primary:hover, .btn-primary:focus {
    background-color: #426acd;
    border-color: #4f56aa;
}
.dropbtn {
    background-color: #4CAF50;
    color: white;
    padding: 16px;
    font-size: 16px;
    border: none;
}

.dropdown {
    position: relative;
    display: inline-block;
}

.dropdown-content {
    display: none;
    position: absolute;
    background-color: #f1f1f1;
    min-width: 200px;
    box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
    z-index: 1;
}

.dropdown-content a {
    color: black;
    padding: 12px 16px;
    text-decoration: none;
    display: block;
}

.dropdown-content a:hover {background-color: #ddd;}

.dropdown:hover .dropdown-content {display: block;}

.dropdown:hover .dropbtn {background-color: #3e8e41;}
.btn2-pressure {
    position: relative;
    margin-bottom: 0;
}
.btn2-pressure:focus {
    -moz-outline-style:none;
         outline:medium none;
}
.btn2-pressure:active, .btn2-pressure.active {
    top: 4px;
    border: 0;
    position: relative;
}
.btn2-sensitive:active, .btn2-sensitive.active {
    top: 1px;
    margin-top: 4px;
}
            th.rotate {
  /* Something you can count on */
 
  white-space: nowrap;
}
.rotate2{
    transform:rotate(90deg);
}
th.rotate > div {
  transform: 
    /* Magic Numbers */
    translate(5px, 30px)
    /* 45 is really 360 - 45 */
    rotate(270deg);
  width: 30px;
}
th.rotate > div > span {
 
}
#myImg {
    border-radius: 5px;
    cursor: pointer;
    transition: 0.3s;
}

#myImg:hover {opacity: 0.7;}

/* The Modal (background) */
.modal2 {
    display: none; /* Hidden by default */
    position: fixed; /* Stay in place */
    z-index: 11; /* Sit on top */
    padding-top: 100px; /* Location of the box */
    left: 0;
    top: 0;
    width: 100%; /* Full width */
    height: 100%; /* Full height */
    overflow: auto; /* Enable scroll if needed */
    background-color: rgb(0,0,0); /* Fallback color */
    background-color: rgba(0,0,0,0.9); /* Black w/ opacity */
}

/* Modal Content (image) */
.modal2-content {
    margin: auto;
    display: block;
    width: 90%;
    max-width: 900px;
}

/* Caption of Modal Image */
#caption {
    margin: auto;
    display: block;
    width: 80%;
    max-width: 700px;
    text-align: center;
    color: #ccc;
    padding: 10px 0;
    height: 150px;
}



@-webkit-keyframes zoom {
    from {-webkit-transform:scale(0)} 
    to {-webkit-transform:scale(1)}
}

@keyframes zoom {
    from {transform:scale(0)} 
    to {transform:scale(1)}
}

/* The Close Button */
.close2 {
    position: absolute;
    top: 15px;
    right: 35px;
    color: #f1f1f1;
    font-size: 40px;
    font-weight: bold;
    transition: 0.3s;
}

.close2:hover,
.close2:focus {
    color: #bbb;
    text-decoration: none;
    cursor: pointer;
}

/* 100% Image Width on Smaller Screens */
@media only screen and (max-width: 700px){
    .modal-content {
        width: 100%;
    }
}
.table_legenda th {
  word-wrap: break-word;
}
.gly-rotate-90 {
  filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=1);
  -webkit-transform: rotate(90deg);
  -moz-transform: rotate(90deg);
  -ms-transform: rotate(90deg);
  -o-transform: rotate(90deg);
  transform: rotate(90deg);
}
.gly-rotate-180 {
  filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=2);
  -webkit-transform: rotate(180deg);
  -moz-transform: rotate(180deg);
  -ms-transform: rotate(180deg);
  -o-transform: rotate(180deg);
  transform: rotate(180deg);
}
.gly-rotate-270 {
  filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=3);
  -webkit-transform: rotate(270deg);
  -moz-transform: rotate(270deg);
  -ms-transform: rotate(270deg);
  -o-transform: rotate(270deg);
  transform: rotate(270deg);
}
.gly-flip-horizontal {
  filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=0, mirror=1);
  -webkit-transform: scale(-1, 1);
  -moz-transform: scale(-1, 1);
  -ms-transform: scale(-1, 1);
  -o-transform: scale(-1, 1);
  transform: scale(-1, 1);
}
.gly-flip-vertical {
  filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=2, mirror=1);
  -webkit-transform: scale(1, -1);
  -moz-transform: scale(1, -1);
  -ms-transform: scale(1, -1);
  -o-transform: scale(1, -1);
  transform: scale(1, -1);
}
            .centertext {
                text-align: center;
            }
            .centertext2{
                text-align:center;
                color:white;
            }
            
            .hid {
                visibility:hidden;
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
            .hid2 { 
                display:none;
            }
            .bold{
                font-weight:bold;
            }
            .noborder{
                outline:none;

            }
            .hid4 { 
                white-space: nowrap;
                background-color: #337AB7;
                background :  #337AB7;
                position:absolute;
                left:9999px;
            }
            .hid3 { 
              opacity:0;
            }
            .righttext {
                position: relative;
                text-align: right;
            }
            .lefttext {
                position: relative;
                text-align: left;
            }
            .centertext {
                text-align: center;
            }
            .cen{
                padding-left:7px;
                padding-right:7px;
                padding-top:3.5px;
                padding-bottom:3.5px;
                border:1px solid white;
            }
            .cen2{
                padding-left:3px;
                padding-right:3px;
                padding-top:3.5px;
                padding-bottom:3.5px;   
                width:10.5%;             
            }
            .cen2alt{
                padding-left:3px;
                padding-right:3px;
                padding-top:3.5px;
                padding-bottom:3.5px;   
                width:15%;             
            }
            .cen3{
                padding-left:3px;
                padding-right:5px;
                padding-top:3.5px;
                padding-bottom:3.5px;  
                width:14%;
            }
            .cen4{
                padding-left:0px;
                padding-right:5px;
                width:17%;
                           
            }
            .cen4alt{
                padding-left:0px;
                padding-right:5px;
                width:25%;
                           
            }
            .nopad100Tooltip {
    background: #333;
    color: #fff;
}
            .txtglow{    
    box-shadow: 0px 0px 7px red;
    border-color: none;
    background-color:pink;
}
            .txtglow2{    
    box-shadow: 0px 0px 7px orange;
    border-color: none;
    background-color:yellow;
}
            .name{
                overflow:hidden;
                white-space:nowrap;
                table-layout:fixed;
                padding-left:5px;
            }
            .tdtr {
    border: 1px solid #000000;
    text-align: center;
    padding: 0px;
}
            .tdtrx {
    border: 1px solid #000000;
    border-left:none;
    text-align: center;
    padding: 0px;
}
            .tdtr3 {
    border: 1px solid #000000;
    text-align: left;
    padding: 0px;
}
            .tdtr2 {
    border: 1px solid #000000;
    border-left:none;
    text-align:center;
    border-right:none;
    padding: 0px;
}
             .tdtr22 {
    border: 1px solid #000000;
    border-left:none;
    text-align:center;
    padding: 0px;
}
             .tdtr4 {
    border: 1px solid #000000;
    border-bottom:none;
    text-align:center;
    padding: 0px;
}
             .tdtr5 {
    border: 1px solid #000000;
    border-bottom:none;
    border-top:none;
    border-left:none;
    text-align:center;
    padding: 0px;
}
             .borderline {
    border-right: none;
    border-bottom:1px solid #a3b7c8;
    border-top:none;
    border-left:none;
   
}
             .borderline2 {
    border-right: none;
    border-bottom:1px solid #a3b7c8;
    border-top:none;
    border-left:1px solid #a3b7c8;
   
}
             .notdtr{
                  border: 1px solid #000000;
    border-bottom:none;
    border-top:none;
    border-left:none;
    text-align:center;
    padding: 0px;
             }
             .textAlignVer{
    display:block;
    filter: flipv fliph;
    -webkit-transform: rotate(-90deg); 
    -moz-transform: rotate(-90deg); 
    transform: rotate(-90deg); 
    white-space:nowrap;
    background-color: #337AB7;
    height:130px;
        color: #000000;
        padding:0px;
}
             }


            .nopad{
                padding:0px;
                width:70px;
            }
            .maxtest{
                font-size:80%;
            }
            .maxtestcw{
                font-size:80%;
            }
            .maxmidscore{
                font-size:90%;
            }
            .maxlatescore{
                font-size:90%;
            }
            
            .nopad180{
                padding:0px;
                width:19.5%;
            }
            .nopad40{
                padding:0px;
                width:90%;
            }
            .nopad100{
                font-size:72%;
                width:130px;
                height:40px;
                background:rgba(0,0,0,0);
                border:none;
                color:white;
                background-color:#337AB7;
                font-weight:normal;
            }
            .nopad10{
                padding-right:10px;
                width:43px;
            }
            .disable{
                pointer-events: none; 
                background-color:#f0f0f0;
                border:1px solid #AFAFAF;
            }
            input[type=checkbox] {
  transform: scale(1.3);
}
            .active .a{
                background-color:chocolate;
            }
            .nav-tabs > li.active > a, .nav-tabs > li.active > a:hover, .nav-tabs > li.active > a:focus
            {
                background-color: #337AB7;
                border-top:1px solid black;
                border-right:1px solid black;
                border-left:1px solid black;
                color:white;
            }
            .nav-tabs > li > a {
                border-left:1px solid #949191;
    border-right:1px solid #949191;
    border-bottom:1px solid #949191;
    border-top:1px solid #949191;
    height:30px;
    padding-top:0px;
    font-size:73%;
            }
            .nopadd{
                padding:0px;
            }
            .hidden5{
                visibility:hidden;
                padding:0px;
            }
            .table2 {
    border-collapse: collapse;
    width: 100%;    
}
            .table3 {
    border-collapse: collapse;
    width: 100%;    
    table-layout:fixed;
}
            .table4 {
    border-collapse: collapse;
    width: 100%;    
}
            .vrt-header {
  writing-mode: vertical-lr;
  min-width: 30px; /* for firefox */
}
        .smolfont{
            font-size:90%;
        }
.headerCell {
        background-color: #337AB7;
        color: White;
        height: 65px;
        font-size: 70%;
        font-weight: bold;

    }
.HeaderCell9 {
        background-color: #337AB7;
        color: White;
        height: 65px;
        font-size: 70%;
        font-weight: bold;
        border: 1px solid black;
        text-align:center;
    }
.headerCell2 {
       
        height: 65px;        
    }
.font90{
    font-size:90%;
}
.headerCell3 {
        background-color: #337AB7;
        color: White;
        height: 65px;
        font-size: 60%;
        font-weight: bold;
        
    }
.AutoCompleteTextBox{
    padding:0px;
}
.headerCell4 {
        background-color: #337AB7;
        color: #000000;
    }
.wrapper1 {  overflow-x: scroll; overflow-y: hidden; }
.wrapper2 {  overflow-x: hidden; overflow-y: hidden; width:489px;}
.wrapper1 { height: 20px; }
.div1 { height: 20px; }
.div2 { overflow: none; }

.tooltipCss
   {
      position: absolute;
      border: 1px solid gray;
      margin: 1em;
      padding: 3px;
      background: #A4D162;
      font-family: Trebuchet MS;
      font-weight: normal;
      color: black;
      font-size: 11px;
   }
        </style>
        
        <script src="https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/4.4.0/bootbox.min.js"></script>
        <script type="text/javascript" language="javascript">
           
            $(document).ready(function () {
                $.protip();
            });

            //$(function () {
           //     $('[title]').tooltip({
            //        content: function () {
           //             var element = $(this);
           //             return element.attr('title')
           //         }
           //     });
           // });

            var i = 1;
            $('#image').click(function() {    
            });
            function myFunction() {
                document.getElementById(i).classList.remove('hidden');
                i = i + 1;
            }


            function modeString(array) {
                if (array.length == 0)
                    return null;

                var modeMap = {},
                    maxEl = array[0],
                    maxCount = 1;

                if (array.length > 1)
                {
                    for (var i = 0; i < array.length; i++) {
                        var el = array[i];

                        if (modeMap[el] == null)
                            modeMap[el] = 1;
                        else
                            modeMap[el]++;

                        if (modeMap[el] > maxCount) {
                            maxEl = el;
                            maxCount = modeMap[el];
                        }
                        else if (modeMap[el] == maxCount) {
                            if (Number(maxEl) < Number(el))
                                maxEl = el;
                            else maxEl = maxEl;
                            
                            maxCount = modeMap[el];
                        }
                    }
                }
                else
                {
                    maxEl = array[0];
                }
                
                return maxEl;
            }

            function maxerror() {
                var clickButton = document.getElementById("<%= btnCancle.ClientID %>");
                    clickButton.click();
                
            }

            function focusbox(id,row) {
                var focus1 = document.getElementsByClassName("focus1");
                var focus2 = document.getElementsByClassName("focus2");
                var xx = document.getElementsByClassName("xx");
                var yy = document.getElementsByClassName("yy");
                var up = document.getElementsByClassName("fup");
                var fnow = document.getElementsByClassName("fnow");
                var page = document.getElementsByClassName("fpage");
                var down = document.getElementsByClassName("fdown");
                var left = document.getElementsByClassName("fleft");
                var right = document.getElementsByClassName("fright");
                var AutoCompleteTextBox = document.getElementsByClassName("AutoCompleteTextBox");
                var AutoCompleteTextBoxg2 = document.getElementsByClassName("AutoCompleteTextBoxg2");
                var AutoCompleteTextBoxg3 = document.getElementsByClassName("AutoCompleteTextBoxg3");
                var AutoCompleteTextBoxg4 = document.getElementsByClassName("AutoCompleteTextBoxg4");
                var AutoCompleteTextBox2 = document.getElementsByClassName("AutoCompleteTextBox2");
                focus1[0].value = id;
                focus2[0].value = row;
                var x = id;
                var y = row;
                
                x = (x % 5);
                page[0].value = Math.floor(id / 5);
                xx[0].value = x;
                yy[0].value = y;
                fnow[0].value = (x * y) + (5 * (y - 1) - (x * (y - 1)));
                var z = fnow[0].value;
                
                up[0].value = Number(fnow[0].value) - 5;
                down[0].value = Number(fnow[0].value) + 5;
                left[0].value = Number(fnow[0].value) - 1;
                right[0].value = Number(fnow[0].value) + 1;

                var divide = Math.floor(y / x);
                var mod = y % x;
                
                if (page[0].value == 10 || page[0].value == 12)
                {
                    fnow[0].value = (x * y) + (2 * (y - 1) - (x * (y - 1)));
                    up[0].value = Number(fnow[0].value) - 2;
                    down[0].value = Number(fnow[0].value) + 2;
                    left[0].value = Number(fnow[0].value) - 1;
                    right[0].value = Number(fnow[0].value) + 1;
                }
            }


            $(document).ready(function () {
                $("input:text").focus(function () { $(this).select(); });
            });


            function nextpage(id) {
                //CompareDates(99999);
                var clickButton = document.getElementById("<%= btnSave.ClientID %>");
                var txtglow = document.getElementsByClassName("txtglow");
                

                if (txtglow.length == 0)
                {
                    clickButton.click();
                }
                else 
                {
                    bootbox.confirm({
                        title: '<h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101328") %></h>',
                        message: '<h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132215") %></h>',
                        buttons: {
                            cancel: {
                                label: '<i class="fa fa-times"></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>'
                            },
                            confirm: {
                                label: '<i class="fa fa-check"></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>'
                            }
                        },
                        callback: function (result) {
                            if(result == true)
                                clickButton.click();
                        }
                    });
                }                
            }

            
            

            function keyUpdate(keyEvent, down) {
                var textBoxesg1 = document.getElementsByClassName("AutoCompleteTextBox");
                var textBoxesg2 = document.getElementsByClassName("AutoCompleteTextBoxg2");
                var textBoxesg3 = document.getElementsByClassName("AutoCompleteTextBoxg3");
                var textBoxesg4 = document.getElementsByClassName("AutoCompleteTextBoxg4");
                var cw1 = document.getElementsByClassName("chewut1");
                var cw2 = document.getElementsByClassName("chewut2");
                var cw3 = document.getElementsByClassName("chewut3");
                var cw4 = document.getElementsByClassName("chewut4");
                var bh1 = document.getElementsByClassName("behavepage1");
                var bh2 = document.getElementsByClassName("behavepage2");
                var textBoxes2 = document.getElementsByClassName("AutoCompleteTextBox2");
                var up = document.getElementsByClassName("fup");
                var page = document.getElementsByClassName("fpage");
                var down = document.getElementsByClassName("fdown");
                var left = document.getElementsByClassName("fleft");
                var right = document.getElementsByClassName("fright");
                var page12 = document.getElementsByClassName("page12");

                var p = page[0].value;
                var up2 = up[0].value;
                var down2 = down[0].value;
                var left2 = left[0].value;
                var right2 = right[0].value;
                // down is a boolean, whether the key event is keydown (true) or keyup (false)
                keyEvent.preventDefault(); // prevent screen from going crazy while i press keys.
                console.log(keyEvent.keyCode)
                switch (keyEvent.keyCode) {
                    
                    case 38:  // up key.
                        if (p == 0) textBoxesg1[up2].focus();
                        else if (p == 1) textBoxesg2[up2].focus();
                        else if (p == 2) textBoxesg3[up2].focus();
                        else if (p == 3) textBoxesg4[up2].focus();
                        else if (p == 6) cw1[up2].focus();
                        else if (p == 7) cw2[up2].focus();
                        else if (p == 8) cw3[up2].focus();
                        else if (p == 9) cw4[up2].focus();
                        else if (p == 4) bh1[up2].focus();
                        else if (p == 5) bh2[up2].focus();
                        else if (p == 10) textBoxes2[up2].focus();
                        else if (p == 12) page12[up2].focus();
                        break;
                    
                    case 40: // down key
                        if (p == 0) textBoxesg1[down2].focus();
                        else if (p == 1) textBoxesg2[down2].focus();
                        else if (p == 2) textBoxesg3[down2].focus();
                        else if (p == 3) textBoxesg4[down2].focus();
                        else if (p == 6) cw1[down2].focus();
                        else if (p == 7) cw2[down2].focus();
                        else if (p == 8) cw3[down2].focus();
                        else if (p == 9) cw4[down2].focus();
                        else if (p == 4) bh1[down2].focus();
                        else if (p == 5) bh2[down2].focus();
                        else if (p == 10) textBoxes2[down2].focus();
                        else if (p == 12) page12[down2].focus();
                        break;
                   
                    case 37: // left arrow.
                        if (p == 0) textBoxesg1[left2].focus();
                        else if (p == 1) textBoxesg2[left2].focus();
                        else if (p == 2) textBoxesg3[left2].focus();
                        else if (p == 3) textBoxesg4[left2].focus();
                        else if (p == 6) cw1[left2].focus();
                        else if (p == 7) cw2[left2].focus();
                        else if (p == 8) cw3[left2].focus();
                        else if (p == 9) cw4[left2].focus();
                        else if (p == 4) bh1[left2].focus();
                        else if (p == 5) bh2[left2].focus();
                        else if (p == 10) textBoxes2[left2].focus();
                        else if (p == 12) page12[left2].focus();
                        break;
                    
                    case 39: // right arrow.
                        if (p == 0) textBoxesg1[right2].focus();
                        else if (p == 1) textBoxesg2[right2].focus();
                        else if (p == 2) textBoxesg3[right2].focus();
                        else if (p == 3) textBoxesg4[right2].focus();
                        else if (p == 6) cw1[right2].focus();
                        else if (p == 7) cw2[right2].focus();
                        else if (p == 8) cw3[right2].focus();
                        else if (p == 9) cw4[right2].focus();
                        else if (p == 4) bh1[right2].focus();
                        else if (p == 5) bh2[right2].focus();
                        else if (p == 10) textBoxes2[right2].focus();
                        else if (p == 12) page12[right2].focus();
                        break;
                    
                        
                }
                
            }

            document.addEventListener("keydown", function (event) {
                
                if(event.keyCode==37 || event.keyCode==38 || event.keyCode==39 || event.keyCode==40)
                keyUpdate(event, false);
            });


            function changebutton(id) {
                var btnok = document.getElementsByClassName("btnok");
                var btnerror = document.getElementsByClassName("btnerror");
                if(id == "9999")
                {
                    btnok[0].classList.remove('hidden');
                    btnerror[0].classList.add('hidden');
                }
                else {
                    btnok[0].classList.add('hidden');
                    btnerror[0].classList.remove('hidden');
                }
            }

            function autobehave() {
                changebutton(9999);
                var maxtestb1 = document.getElementsByClassName("maxtestb1");
                var behavetxt1 = document.getElementsByClassName("behavetxt1");
                var behavetxt2 = document.getElementsByClassName("behavetxt2");
                var behavetxt3 = document.getElementsByClassName("behavetxt3");
                var behavetxt4 = document.getElementsByClassName("behavetxt4");
                var behavetxt5 = document.getElementsByClassName("behavetxt5");
                var behavetxt6 = document.getElementsByClassName("behavetxt6");
                var behavetxt7 = document.getElementsByClassName("behavetxt7");
                var behavetxt8 = document.getElementsByClassName("behavetxt8");
                var behavetxt9 = document.getElementsByClassName("behavetxt9");
                var behavetxt10 = document.getElementsByClassName("behavetxt10");
                var goodbe = document.getElementsByClassName("goodbe");
                var behavedisable = document.getElementsByClassName("behavedisable");

                var maxb1 = maxtestb1[0].value;
                var maxb2 = maxtestb1[1].value;
                var maxb3 = maxtestb1[2].value;
                var maxb4 = maxtestb1[3].value;
                var maxb5 = maxtestb1[4].value;
                var maxb6 = maxtestb1[5].value;
                var maxb7 = maxtestb1[6].value;
                var maxb8 = maxtestb1[7].value;
                var maxb9 = maxtestb1[8].value;
                var maxb10 = maxtestb1[9].value;
                
                for (var i = 0; i < behavetxt1.length; i++) {
                    if (Number(behavetxt1[i].value) > Number(maxb1))
                    {
                        changebutton(i);
                        behavetxt1[i].classList.add('txtglow');
                    }
                    if (Number(behavetxt1[i].value) <= Number(maxb1))
                    {
                        behavetxt1[i].classList.remove('txtglow');
                    }                    
                    if (Number(behavetxt2[i].value) > Number(maxb2))
                    {
                        changebutton(i);
                        behavetxt2[i].classList.add('txtglow');
                    }
                    if (Number(behavetxt2[i].value) <= Number(maxb2)) {                       
                        behavetxt2[i].classList.remove('txtglow');
                    }
                    if (Number(behavetxt3[i].value) > Number(maxb3))
                    {
                        changebutton(i);
                        behavetxt3[i].classList.add('txtglow');
                    }
                    if (Number(behavetxt3[i].value) <= Number(maxb3)) {                       
                        behavetxt3[i].classList.remove('txtglow');
                    }
                    if (Number(behavetxt4[i].value) > Number(maxb4))
                    {
                        changebutton(i);
                        behavetxt4[i].classList.add('txtglow');
                    }
                    if (Number(behavetxt4[i].value) <= Number(maxb4)) {                        
                        behavetxt4[i].classList.remove('txtglow');
                    }
                    if (Number(behavetxt5[i].value) > Number(maxb5))
                    {
                        changebutton(i);
                        behavetxt5[i].classList.add('txtglow');
                    }
                    if (Number(behavetxt5[i].value) <= Number(maxb5)) {                        
                        behavetxt5[i].classList.remove('txtglow');
                    }
                    if (Number(behavetxt6[i].value) > Number(maxb6))
                    {
                        changebutton(i);
                        behavetxt6[i].classList.add('txtglow');
                    }
                    if (Number(behavetxt6[i].value) <= Number(maxb6)) {
                        behavetxt6[i].classList.remove('txtglow');
                    }
                    if (Number(behavetxt7[i].value) > Number(maxb7))
                    {
                        changebutton(i);
                        behavetxt7[i].classList.add('txtglow');
                    }
                    if (Number(behavetxt7[i].value) <= Number(maxb7)) {
                        behavetxt7[i].classList.remove('txtglow');
                    }
                    if (Number(behavetxt8[i].value) > Number(maxb8))
                    {
                        changebutton(i);
                        behavetxt8[i].classList.add('txtglow');
                    }
                    if (Number(behavetxt8[i].value) <= Number(maxb8)) {
                        behavetxt8[i].classList.remove('txtglow');
                    }
                    if (Number(behavetxt9[i].value) > Number(maxb9))
                    {
                        changebutton(i);
                        behavetxt9[i].classList.add('txtglow');
                    }
                    if (Number(behavetxt9[i].value) <= Number(maxb9)) {
                        behavetxt9[i].classList.remove('txtglow');
                    }
                    if (Number(behavetxt10[i].value) > Number(maxb10))
                    {
                        changebutton(i);
                        behavetxt10[i].classList.add('txtglow');
                    }
                    if (Number(behavetxt10[i].value) <= Number(maxb10)) {
                        behavetxt10[i].classList.remove('txtglow');
                    }
                }

                if (behavedisable[0].value == 1)
                {
                    for (var i = 0; i < behavetxt1.length; i++) {
                        var array = [];

                        if (behavetxt1[i].value != "") array.push(behavetxt1[i].value);
                        if (behavetxt2[i].value != "") array.push(behavetxt2[i].value);
                        if (behavetxt3[i].value != "") array.push(behavetxt3[i].value);
                        if (behavetxt4[i].value != "") array.push(behavetxt4[i].value);
                        if (behavetxt5[i].value != "") array.push(behavetxt5[i].value);
                        if (behavetxt6[i].value != "") array.push(behavetxt6[i].value);
                        if (behavetxt7[i].value != "") array.push(behavetxt7[i].value);
                        if (behavetxt8[i].value != "") array.push(behavetxt8[i].value);
                        if (behavetxt9[i].value != "") array.push(behavetxt9[i].value);
                        if (behavetxt10[i].value != "") array.push(behavetxt10[i].value);

                        if (array.length > 1)
                            goodbe[i].value = modeString(array);
                        else if (array.length == 1) goodbe[i].value = array[0];
                        else goodbe[i].value = "";

                    }
                }
                
            }

            function cleartext(id) {
                
                var goodbe = document.getElementsByClassName("goodbe");
                var readscore = document.getElementsByClassName("readscore");
                
                if(id == "1")
                {
                    for (var i = 0; i < goodbe.length; i++) {
                        goodbe[i].value = "";
                    }
                }
                else if (id == "2")
                {
                    for (var i = 0; i < readscore.length; i++) {
                        readscore[i].value = "";
                    }
                }
                

            }

            function nextbutton(id) {

                var left1 = document.getElementsByClassName("left1");
                var left2 = document.getElementsByClassName("left2");
                
                var right1 = document.getElementsByClassName("right1");
                var right2 = document.getElementsByClassName("right2");
                
                var scorebox = document.getElementsByClassName("scorebox");                
                
                var full = window.location.href;
                var half = full.split('?');
                var split = half[1].split('&');
                var year = split[0].split('=');
                var idlv = split[1];
                var idlv2 = split[2];
                var term = split[3].split('=');
                var id2 = split[4].split('=');
                var mode = split[5].split('=');

                

                left1[0].classList.remove('active');
                left2[0].classList.remove('active');
                
                right1[0].classList.remove('active');
                right2[0].classList.remove('active');
                

                left1[0].classList.add('hidden');
                left2[0].classList.add('hidden');
                
                right1[0].classList.add('hidden');
                right2[0].classList.add('hidden');
                
                if(id == "1")
                {
                    
                    if (mode[1] != "EN")
                        scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132236") %>";
                    else scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132236") %>";
                                                     
                }
                else if(id == "2")
                {
                    if (mode[1] != "EN")
                        scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132237") %>";
                    else scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132237") %>";
                                                                              
                }
                else if (id == "3")
                {
                    if (mode[1] != "EN")
                        scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132238") %>";
                    else scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132238") %>";
                                       
                                        
                    right1[0].classList.remove('hidden');                                   
                }
                else if (id == "4")
                {
                    if (mode[1] != "EN")
                        scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132239") %>";
                    else scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132239") %>";
                    
                    right2[0].classList.remove('hidden');                                     
                }
                else if (id == "5")
                {
                    if (mode[1] != "EN")
                        scorebox[0].value = "สมรรถนะที่ 5 ความสมารถในการใช้เทคโนโลยี";
                    else scorebox[0].value = "สมรรถนะที่ 5 ความสมารถในการใช้เทคโนโลยี";
                                                                         
                }
                else if (id== "6")
                {
                    if (mode[1] != "EN")
                        scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132238") %>";
                    else scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132238") %>";
                                        
                    left1[0].classList.remove('hidden');                    
                }
                else if(id == "7")
                {
                    if (mode[1] != "EN")
                        scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132239") %>";
                    else scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132239") %>";
                    
                    left2[0].classList.remove('hidden');
                }
                
                
            }

            function changename(id) {
                
                var name1 = document.getElementsByClassName("test1name");
                var name2 = document.getElementsByClassName("test2name");
                var name3 = document.getElementsByClassName("test3name");
                var name4 = document.getElementsByClassName("test4name");
                var name5 = document.getElementsByClassName("test5name");
                var name6 = document.getElementsByClassName("test6name");
                var name7 = document.getElementsByClassName("test7name");
                var name8 = document.getElementsByClassName("test8name");
                var name9 = document.getElementsByClassName("test9name");
                var name10 = document.getElementsByClassName("test10name");
                var name11 = document.getElementsByClassName("test11name");
                var name12 = document.getElementsByClassName("test12name");
                var name13 = document.getElementsByClassName("test13name");
                var name14 = document.getElementsByClassName("test14name");
                var name15 = document.getElementsByClassName("test15name");
                var name16 = document.getElementsByClassName("test16name");
                var name17 = document.getElementsByClassName("test17name");
                var name18 = document.getElementsByClassName("test18name");
                var name19 = document.getElementsByClassName("test19name");
                var name20 = document.getElementsByClassName("test20name");
                var set1 = document.getElementsByClassName("set1name");
                var set2 = document.getElementsByClassName("set2name");
                var set3 = document.getElementsByClassName("set3name");
                var set4 = document.getElementsByClassName("set4name");
                var set5 = document.getElementsByClassName("set5name");
                var set6 = document.getElementsByClassName("set6name");
                var set7 = document.getElementsByClassName("set7name");
                var set8 = document.getElementsByClassName("set8name");
                var set9 = document.getElementsByClassName("set9name");
                var set10 = document.getElementsByClassName("set10name");
                var set11 = document.getElementsByClassName("set11name");
                var set12 = document.getElementsByClassName("set12name");
                var set13 = document.getElementsByClassName("set13name");
                var set14 = document.getElementsByClassName("set14name");
                var set15 = document.getElementsByClassName("set15name");
                var set16 = document.getElementsByClassName("set16name");
                var set17 = document.getElementsByClassName("set17name");
                var set18 = document.getElementsByClassName("set18name");
                var set19 = document.getElementsByClassName("set19name");
                var set20 = document.getElementsByClassName("set20name");
                var setb1 = document.getElementsByClassName("setnameb1");
                var setb2 = document.getElementsByClassName("setnameb2");
                var setb3 = document.getElementsByClassName("setnameb3");
                var setb4 = document.getElementsByClassName("setnameb4");
                var setb5 = document.getElementsByClassName("setnameb5");
                var setb6 = document.getElementsByClassName("setnameb6");
                var setb7 = document.getElementsByClassName("setnameb7");
                var setb8 = document.getElementsByClassName("setnameb8");
                var setb9 = document.getElementsByClassName("setnameb9");
                var setb10 = document.getElementsByClassName("setnameb10");
                var nameb1 = document.getElementsByClassName("testb1name");
                var nameb2 = document.getElementsByClassName("testb2name");
                var nameb3 = document.getElementsByClassName("testb3name");
                var nameb4 = document.getElementsByClassName("testb4name");
                var nameb5 = document.getElementsByClassName("testb5name");
                var nameb6 = document.getElementsByClassName("testb6name");
                var nameb7 = document.getElementsByClassName("testb7name");
                var nameb8 = document.getElementsByClassName("testb8name");
                var nameb9 = document.getElementsByClassName("testb9name");
                var nameb10 = document.getElementsByClassName("testb10name");

                var namecw1 = document.getElementsByClassName("testcw1name");
                var namecw2 = document.getElementsByClassName("testcw2name");
                var namecw3 = document.getElementsByClassName("testcw3name");
                var namecw4 = document.getElementsByClassName("testcw4name");
                var namecw5 = document.getElementsByClassName("testcw5name");
                var namecw6 = document.getElementsByClassName("testcw6name");
                var namecw7 = document.getElementsByClassName("testcw7name");
                var namecw8 = document.getElementsByClassName("testcw8name");
                var namecw9 = document.getElementsByClassName("testcw9name");
                var namecw10 = document.getElementsByClassName("testcw10name");
                var namecw11 = document.getElementsByClassName("testcw11name");
                var namecw12 = document.getElementsByClassName("testcw12name");
                var namecw13 = document.getElementsByClassName("testcw13name");
                var namecw14 = document.getElementsByClassName("testcw14name");
                var namecw15 = document.getElementsByClassName("testcw15name");
                var namecw16 = document.getElementsByClassName("testcw16name");
                var namecw17 = document.getElementsByClassName("testcw17name");
                var namecw18 = document.getElementsByClassName("testcw18name");
                var namecw19 = document.getElementsByClassName("testcw19name");
                var namecw20 = document.getElementsByClassName("testcw20name");
                var setcw1 = document.getElementsByClassName("set1namecw");
                var setcw2 = document.getElementsByClassName("set2namecw");
                var setcw3 = document.getElementsByClassName("set3namecw");
                var setcw4 = document.getElementsByClassName("set4namecw");
                var setcw5 = document.getElementsByClassName("set5namecw");
                var setcw6 = document.getElementsByClassName("set6namecw");
                var setcw7 = document.getElementsByClassName("set7namecw");
                var setcw8 = document.getElementsByClassName("set8namecw");
                var setcw9 = document.getElementsByClassName("set9namecw");
                var setcw10 = document.getElementsByClassName("set10namecw");
                var setcw11 = document.getElementsByClassName("set11namecw");
                var setcw12 = document.getElementsByClassName("set12namecw");
                var setcw13 = document.getElementsByClassName("set13namecw");
                var setcw14 = document.getElementsByClassName("set14namecw");
                var setcw15 = document.getElementsByClassName("set15namecw");
                var setcw16 = document.getElementsByClassName("set16namecw");
                var setcw17 = document.getElementsByClassName("set17namecw");
                var setcw18 = document.getElementsByClassName("set18namecw");
                var setcw19 = document.getElementsByClassName("set19namecw");
                var setcw20 = document.getElementsByClassName("set20namecw");
                
                var name20 = document.getElementsByClassName("test20name");
                var name21 = document.getElementsByClassName("test21name");
                var name22 = document.getElementsByClassName("test22name");
                var name23 = document.getElementsByClassName("test23name");
                var name24 = document.getElementsByClassName("test24name");
                var name25 = document.getElementsByClassName("test25name");
                var name26 = document.getElementsByClassName("test26name");
                var name27 = document.getElementsByClassName("test27name");
                var name28 = document.getElementsByClassName("test28name");
                var name29 = document.getElementsByClassName("test29name");
                var name30 = document.getElementsByClassName("test30name");
                var name31 = document.getElementsByClassName("test31name");
                var name32 = document.getElementsByClassName("test32name");
                var name33 = document.getElementsByClassName("test33name");
                var name34 = document.getElementsByClassName("test34name");
                var name35 = document.getElementsByClassName("test35name");
                var name36 = document.getElementsByClassName("test36name");
                var name37 = document.getElementsByClassName("test37name");
                var name38 = document.getElementsByClassName("test38name");
                var name39 = document.getElementsByClassName("test39name");
                var name40 = document.getElementsByClassName("test40name");
                var name41 = document.getElementsByClassName("test41name");
                var name42 = document.getElementsByClassName("test42name");
                var name43 = document.getElementsByClassName("test43name");
                var name44 = document.getElementsByClassName("test44name");
                var name45 = document.getElementsByClassName("test45name");
                var name46 = document.getElementsByClassName("test46name");
                var name47 = document.getElementsByClassName("test47name");
                var name48 = document.getElementsByClassName("test48name");
                var name49 = document.getElementsByClassName("test49name");
                var name50 = document.getElementsByClassName("test50name");
                var name51 = document.getElementsByClassName("test51name");
                var name52 = document.getElementsByClassName("test52name");
                var name53 = document.getElementsByClassName("test53name");
                var name54 = document.getElementsByClassName("test54name");
                var name55 = document.getElementsByClassName("test55name");
                var name56 = document.getElementsByClassName("test56name");
                var name57 = document.getElementsByClassName("test57name");
                var name58 = document.getElementsByClassName("test58name");
                var name59 = document.getElementsByClassName("test59name");
                var name60 = document.getElementsByClassName("test60name");
                var name61 = document.getElementsByClassName("test61name");
                var name62 = document.getElementsByClassName("test62name");
                var name63 = document.getElementsByClassName("test63name");
                var name64 = document.getElementsByClassName("test64name");
                var name65 = document.getElementsByClassName("test65name");
                var name66 = document.getElementsByClassName("test66name");
                var name67 = document.getElementsByClassName("test67name");
                var name68 = document.getElementsByClassName("test68name");
                var name69 = document.getElementsByClassName("test69name");
                var name70 = document.getElementsByClassName("test70name");
                var name71 = document.getElementsByClassName("test71name");
                var name72 = document.getElementsByClassName("test72name");

                name1[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 1.1";
                name2[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 1.2";
                name3[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 1.3";
                name4[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 1.4";
                name5[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 2.1";
                name6[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 2.2";
                name7[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 3.1";
                name8[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 3.2";
                name9[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 4";
                
                name10[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 1.1";
                name11[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 1.2";
                name12[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 1.3";
                name13[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 2.1";
                name14[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 2.2";
                name15[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 2.3";
                name16[0].value = "";
                name17[0].value = "";
                name18[0].value = "";

                name19[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 1.1.1";
                name20[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 1.1.2";
                name21[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 1.1.3";
                name22[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 1.1.1.4.1";
                name23[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 1.1.1.4.2";
                name24[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 1.1.1.4.3";
                name25[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 1.1.5";
                name26[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 1.1.6";
                name27[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 1.2";

                name28[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 1.3.1";
                name29[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 1.3.2";
                name30[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 1.3.3";
                name31[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 1.4";
                name32[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 2";
                name33[0].value = "";
                name34[0].value = "";
                name35[0].value = "";
                name36[0].value = "";

                name37[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 1";
                name38[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 2.1";
                name39[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 2.2";
                name40[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 2.3";
                name41[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 3.1";
                name42[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 3.2";
                name43[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 3.3";
                name44[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 4";
                name45[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 5.1";

                name46[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 5.2";
                name47[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 6.1";
                name48[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 6.2";
                name49[0].value = "";
                name50[0].value = "";
                name51[0].value = "";
                name52[0].value = "";
                name53[0].value = "";
                name54[0].value = "";

                name55[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 1.1";
                name56[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 1.2";
                name57[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 1.3";
                name58[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 1.4";
                name59[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 2.1";
                name60[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 2.2";
                name61[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 2.3";
                name62[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132241") %> 2.4";
                name63[0].value = "";

                name64[0].value = "";
                name65[0].value = "";
                name66[0].value = "";
                name67[0].value = "";
                name68[0].value = "";
                name69[0].value = "";
                name70[0].value = "";
                name71[0].value = "";
                name72[0].value = "";
            }

            

            function auto(id) {
                
                var maxtestb1 = document.getElementsByClassName("maxtestb1");
                var goodbe = document.getElementsByClassName("goodbe");
                var read = document.getElementsByClassName("readscore");
                var greencog = document.getElementsByClassName("greencog");
                var yellowcog = document.getElementsByClassName("yellowcog");
                var redcog = document.getElementsByClassName("redcog");
                var cog1 = document.getElementsByClassName("cog1");
                var cog2 = document.getElementsByClassName("cog2");
                var blue3 = document.getElementsByClassName("bluebutton3");
                var blue = document.getElementsByClassName("bluebutton");
                var behavedisable = document.getElementsByClassName("behavedisable");
                var subcheckbox = document.getElementsByClassName("subcheckbox");
                var editform1 = document.getElementsByClassName("editform1");
                var editform2 = document.getElementsByClassName("editform2");
                var editgrade = document.getElementsByClassName("editgrade");
                var cen2 = document.getElementsByClassName("cen2");
                var cen3 = document.getElementsByClassName("cen3");
                var cen4 = document.getElementsByClassName("cen4");
                var nobehave = document.getElementsByClassName("nobehave");
                var gradetxt2 = document.getElementsByClassName("gradetxt2");
                var w40 = document.getElementsByClassName("w40");

                var ddl1 = document.getElementsByClassName("autoddl1");
                var ddl2 = document.getElementsByClassName("autoddl2");
                var ddl3 = document.getElementsByClassName("autoddl3");
                var ddl4 = document.getElementsByClassName("autoddl4");
                var ddl5 = document.getElementsByClassName("autoddl5");
                var ddl6 = document.getElementsByClassName("autoddl6");
                var ddl7 = document.getElementsByClassName("autoddl7");
                var ddl8 = document.getElementsByClassName("autoddl8");
                
                var check3 = document.getElementsByClassName("check3");
                var check4 = document.getElementsByClassName("check4");
                var check5 = document.getElementsByClassName("check5");
                var check6 = document.getElementsByClassName("check6");
                var check7 = document.getElementsByClassName("check7");
                var check8 = document.getElementsByClassName("check8");
                var check9 = document.getElementsByClassName("check9");
                
                if (check5[0].checked == true) {
                    ddl4[0].value = "1";                   
                }
                else if (check5[0].checked == false) {
                    ddl4[0].value = "0";                   
                }

                if (id == "8")
                {                    
                    var elm2 = document.getElementById('check4');
                    var elm = document.getElementById('check5');
                    if (elm2.checked == true)
                    {
                        maxtestb1[0].value = "3";
                        maxtestb1[1].value = "3";
                        maxtestb1[2].value = "3";
                        maxtestb1[3].value = "3";
                        maxtestb1[4].value = "3";
                        maxtestb1[5].value = "3";
                        maxtestb1[6].value = "3";
                        maxtestb1[7].value = "3";
                        if (elm.checked == false)
                            elm.checked = !elm.checked;
                    }
                    else 
                    {
                        maxtestb1[0].value = "";
                        maxtestb1[1].value = "";
                        maxtestb1[2].value = "";
                        maxtestb1[3].value = "";
                        maxtestb1[4].value = "";
                        maxtestb1[5].value = "";
                        maxtestb1[6].value = "";
                        maxtestb1[7].value = "";
                        if (elm.checked == true)
                            elm.checked = !elm.checked;
                    }
                                        
                }
                if (id == "9") {
                    //document.getElementById("check5").checked = false;
                }

                if (check4[0].checked == true) {
                    ddl3[0].value = "1";
                    blue[2].classList.remove('hidden');
                    subcheckbox[0].classList.remove('hidden');
                }
                else if (check4[0].checked == false) {
                    ddl3[0].value = "0";
                    blue[2].classList.add('hidden');
                    subcheckbox[0].classList.add('hidden');
                }

                if (check5[0].checked == false) {
                    behavedisable[0].value = "0";                   
                    for (var i = 0; i < goodbe.length; i++) {
                        goodbe[i].classList.remove('disable');
                    }
                }
                if (check5[0].checked == true) {
                    behavedisable[0].value = "1";
                    goodbe[3].classList.add('disable');
                    for (var i = 0; i < read.length; i++) {
                        goodbe[i].classList.add('disable');
                    }
                }

                if (check3[0].checked == true) {
                    ddl2[0].value = "1";
                    cog2[0].value = "0";                    
                    for (var i = 0; i < goodbe.length; i++) {
                        read[i].classList.remove('disable');
                    }
                }
                else if (check3[0].checked == false) {
                    ddl2[0].value = "0";
                    cog2[0].value = "1";                    
                    for (var i = 0; i < read.length; i++) {
                        read[i].classList.add('disable');
                    }
                }

                

                if (check6[0].checked == true) {
                    ddl5[0].value = "1";
                    blue[1].classList.remove('hidden');                    
                }
                else if (check6[0].checked == false) {
                    ddl5[0].value = "0";
                    blue[1].classList.add('hidden');                    
                }

                if (check8[0].checked == true) {
                    ddl7[0].value = "1";                    
                }
                else if (check8[0].checked == false) {
                    ddl7[0].value = "0";                   
                }

                
                if (check9[0].checked == true) {
                    ddl8[0].value = "1";
                    editform2[0].classList.add('hidden');
                    for (var i = 0; i < w40.length; i++) {
                        editgrade[i].classList.add('hidden');
                        gradetxt2[i].value = "";
                    }
                }
                else if (check9[0].checked == false) {
                    ddl8[0].value = "0";
                    editform2[0].classList.remove('hidden');
                    for (var i = 0; i < w40.length; i++) {
                        editgrade[i].classList.remove('hidden');
                    }
                }

                if (check7[0].checked == true) {
                    ddl6[0].value = "1";
                    editform1[0].classList.add('hidden');
                    editform1[1].classList.add('hidden');
                    nobehave[0].value = "1";
                    for (var i = 0; i < cen3.length; i++) {
                        cen3[i].classList.add('hidden');
                    }

                    for (var i = 0; i < cen2.length; i++) {
                        cen2[i].classList.add('cen2alt');                        
                    }
                    for (var i = 0; i < cen4.length; i++) {
                        cen4[i].classList.add('cen4alt');                        
                    }
                    var cen2alt = document.getElementsByClassName("cen2alt");
                    var cen4alt = document.getElementsByClassName("cen4alt");

                    for (var i = 0; i < cen2alt.length; i++) {
                        cen2alt[i].classList.remove('cen2');
                    }
                    for (var i = 0; i < cen4alt.length; i++) {
                        cen4alt[i].classList.remove('cen4');
                    }
                    
                }
                else if (check7[0].checked == false) {
                    ddl6[0].value = "0";
                    editform1[0].classList.remove('hidden');
                    editform1[1].classList.remove('hidden');
                    nobehave[0].value = "0";
                    for (var i = 0; i < cen3.length; i++) {
                        cen3[i].classList.remove('hidden');
                    }
                    
                    var cen2alt = document.getElementsByClassName("cen2alt");
                    var cen4alt = document.getElementsByClassName("cen4alt");

                    for (var i = 0; i < cen2alt.length; i++) {
                        cen2alt[i].classList.add('cen2');
                    }
                    for (var i = 0; i < cen4alt.length; i++) {
                        cen4alt[i].classList.add('cen4');
                    }

                    var cen2new = document.getElementsByClassName("cen2");
                    var cen4new = document.getElementsByClassName("cen4");

                    for (var i = 0; i < cen2new.length; i++) {
                        cen2new[i].classList.remove('cen2alt');
                    }
                    for (var i = 0; i < cen4new.length; i++) {
                        cen4new[i].classList.remove('cen4alt');
                    }
                    
                }

                if (id == 1)
                {
                    greencog[0].classList.add('hidden');
                    redcog[0].classList.remove('hidden');
                    blue3[0].classList.remove('hidden');
                    cog1[0].value = "1";
                    for (var i = 0; i < goodbe.length; i++) {
                        goodbe[i].classList.add('disable');                        
                    }
                }

                if (id == 2) {
                    yellowcog[0].classList.remove('hidden');                    
                    redcog[0].classList.add('hidden');
                    cog1[0].value = "0";
                    for (var i = 0; i < read.length; i++) {
                        goodbe[i].classList.remove('disable');
                    }
                }

                if (id == 7) {
                    greencog[0].classList.remove('hidden');
                    blue3[0].classList.add('hidden');
                    yellowcog[0].classList.add('hidden');
                    cog1[0].value = "0";
                    for (var i = 0; i < read.length; i++) {
                        goodbe[i].classList.remove('disable');
                    }
                }
                
                if (id == 4) {
                    cog2[0].value = "1";
                    greencog[1].classList.add('hidden');
                    redcog[1].classList.remove('hidden');
                    for (var i = 0; i < read.length; i++) {
                        read[i].classList.add('disable');
                    }
                }

                if (id == 5) {
                    cog2[0].value = "0";
                    greencog[1].classList.remove('hidden');
                    redcog[1].classList.add('hidden');
                    for (var i = 0; i < goodbe.length; i++) {
                        read[i].classList.remove('disable');
                    }
                }
            }

            function editddl() {

                var special = document.getElementsByClassName("special");
                var useryear = document.getElementsByClassName("useryear");
                var userterm = document.getElementsByClassName("userterm");
                var userplan = document.getElementsByClassName("userplan");
                var siduser = document.getElementsByClassName("siduser");

                var full = window.location.href;
                var half = full.split('?');
                var split = half[1].split('&');
                var year = split[0].split('=');
                var idlv = split[1];
                var idlv2 = split[2];
                var term = split[3].split('=');
                var id = split[4].split('=');
                                

                $.get("/App_Logic/bp5GradeRegister.ashx?term=" + term[1] +"&year="+year[1]+"&id="+id[1], function (Result) {
                    $.each(Result, function (index) {
                        
                        
                        for (var x = 0; x < Result.length; x++)
                        {                           
                            if (Result[index].ddlother != "-1" && siduser[x].value == Result[index].sID) {
                                if (Result[index].ddlother == "1")
                                    special[x].selectedIndex = Result[index].ddlother;
                                else if (Result[index].ddlother == "2")
                                    special[x].selectedIndex = Result[index].ddlother;
                                else if (Result[index].ddlother == "3")
                                    special[x].selectedIndex = Result[index].ddlother;
                                else if (Result[index].ddlother == "4")
                                    special[x].selectedIndex = Result[index].ddlother;
                                else if (Result[index].ddlother == "5")
                                    special[x].selectedIndex = Result[index].ddlother;
                                else if (Result[index].ddlother == "6")
                                    special[x].selectedIndex = 9;
                                else if (Result[index].ddlother == "7")
                                    special[x].selectedIndex = 6;
                                else if (Result[index].ddlother == "8")
                                    special[x].selectedIndex = 7;
                                else if (Result[index].ddlother == "9")
                                    special[x].selectedIndex = 8;
                                else special[x].selectedIndex = 0;
                            }
                        }
                        

                       
                    });
                });
            }

            function ddlcopy(id) {

                var copy1 = document.getElementsByClassName("copy1");
                var copy2 = document.getElementsByClassName("copy2");
                var ddlcopy1 = document.getElementsByClassName("ddlcopy1");
                var ddlcopy2 = document.getElementsByClassName("ddlcopy2");
                var special = document.getElementsByClassName("special");
                var maxtestb1 = document.getElementsByClassName("maxtestb1");
                
                
                
                var behavetxt1 = document.getElementsByClassName("behavetxt1");
                var behavetxt2 = document.getElementsByClassName("behavetxt2");
                var behavetxt3 = document.getElementsByClassName("behavetxt3");
                var behavetxt4 = document.getElementsByClassName("behavetxt4");
                var behavetxt5 = document.getElementsByClassName("behavetxt5");
                var behavetxt6 = document.getElementsByClassName("behavetxt6");
                var behavetxt7 = document.getElementsByClassName("behavetxt7");
                var behavetxt8 = document.getElementsByClassName("behavetxt8");
                var behavetxt9 = document.getElementsByClassName("behavetxt9");
                var behavetxt10 = document.getElementsByClassName("behavetxt10");
                var goodbe = document.getElementsByClassName("goodbe");
                var readscore = document.getElementsByClassName("readscore");

                var full = window.location.href;
                var half = full.split('?');
                var split = half[1].split('&');
                var year = split[0].split('=');
                var idlv = split[1];
                var idlv2 = split[2];
                var term = split[3].split('=');
                

               
                var useryear = year[1];
                var userterm = term[1];
                               
                copy1[0].value = ddlcopy1[0].value;
                copy2[0].value = ddlcopy2[0].value;
                
                if (id == 3)
                {
                    $.get("/App_Logic/bp5ImportScore.ashx?mode=1&term=" + userterm + "&year=" + useryear + "&id=" + copy1[0].value, function (Result) {
                        $.each(Result, function (index) {
                            
                            maxtestb1[0].value = Result[index].behave1max;
                            maxtestb1[1].value = Result[index].behave2max;
                            maxtestb1[2].value = Result[index].behave3max;
                            maxtestb1[3].value = Result[index].behave4max;
                            maxtestb1[4].value = Result[index].behave5max;
                            maxtestb1[5].value = Result[index].behave6max;
                            maxtestb1[6].value = Result[index].behave7max;
                            maxtestb1[7].value = Result[index].behave8max;
                            maxtestb1[8].value = Result[index].behave9max;
                            maxtestb1[9].value = Result[index].behave10max;
                            

                        });
                    });

                    $.get("/App_Logic/bp5ImportScore.ashx?mode=2&term=" + userterm + "&year=" + useryear + "&id=" + copy1[0].value, function (Result) {
                        $.each(Result, function (index) {
                            behavetxt1[index].value = Result[index].behave1;
                            behavetxt2[index].value = Result[index].behave2;
                            behavetxt3[index].value = Result[index].behave3;
                            behavetxt4[index].value = Result[index].behave4;
                            behavetxt5[index].value = Result[index].behave5;
                            behavetxt6[index].value = Result[index].behave6;
                            behavetxt7[index].value = Result[index].behave7;
                            behavetxt8[index].value = Result[index].behave8;
                            behavetxt9[index].value = Result[index].behave9;
                            behavetxt10[index].value = Result[index].behave10;
                            goodbe[index].value = Result[index].behaveTotal;
                        });
                    });
                }

                if (id == 4) {
                    $.get("/App_Logic/bp5ImportScore.ashx?mode=3&term=" + userterm + "&year=" + useryear + "&id=" + copy2[0].value, function (Result) {
                        $.each(Result, function (index) {
                            readscore[index].value = Result[index].readingTotal;
                            

                        });
                    });                    
                }
               
            }

            function isNumeric(n) {
                if (n == "")
                    return true;
                return !isNaN(parseFloat(n)) && isFinite(n);
            }

            function CompareDates(id) {
                changebutton(9999);
                var textBoxesg1 = document.getElementsByClassName("AutoCompleteTextBox");               
                var textBoxesg2 = document.getElementsByClassName("AutoCompleteTextBoxg2");
                var textBoxesg3 = document.getElementsByClassName("AutoCompleteTextBoxg3");
                var textBoxesg4 = document.getElementsByClassName("AutoCompleteTextBoxg4");
                var textBoxes2 = document.getElementsByClassName("AutoCompleteTextBox2");
                var chewut1 = document.getElementsByClassName("chewut1");
                var chewut2 = document.getElementsByClassName("chewut2");
                var chewut3 = document.getElementsByClassName("chewut3");
                var chewut4 = document.getElementsByClassName("chewut4");
                
                var ddl1 = document.getElementsByClassName("autoddl1");
                var ddl2 = document.getElementsByClassName("autoddl2");
                var ddl3 = document.getElementsByClassName("autoddl3");
                var ddl4 = document.getElementsByClassName("autoddl4");
                var ddl5 = document.getElementsByClassName("autoddl5");
                var ddl6 = document.getElementsByClassName("autoddl6");
                var ddl8 = document.getElementsByClassName("autoddl8");

                var cog1 = document.getElementsByClassName("cog1");
                var cog2 = document.getElementsByClassName("cog2");
                var behavedisable = document.getElementsByClassName("behavedisable");
                
                
                
                var gradebox = document.getElementsByClassName("gradetxt");
                var special = document.getElementsByClassName("special");
                var maxtest = document.getElementsByClassName("maxtest");
                var maxtestcw = document.getElementsByClassName("maxtestcw");
                var reading = document.getElementsByClassName("readscore");
                var check2 = document.getElementsByClassName("check2");
                var decimal = 0;
                var dicimal1 = document.getElementsByClassName("dicimal1");                

                if (check2[0].checked == true) {
                    ddl1[0].value = "1";
                    decimal = 2;
                    dicimal1[0].value = "0";
                }
                else if (check2[0].checked == false) {
                    ddl1[0].value = "0";
                    decimal = 0;
                    dicimal1[0].value = "1";
                }

               

                var maxtest1 = Number(maxtest[0].value);                
                var maxtest2 =Number(maxtest[1].value);
                var maxtest3 =Number(maxtest[2].value);
                var maxtest4 =Number(maxtest[3].value);
                var maxtest5 =Number(maxtest[4].value);
                var maxtest6 =Number(maxtest[5].value);
                var maxtest7 =Number(maxtest[6].value);
                var maxtest8 =Number(maxtest[7].value);
                var maxtest9 =Number(maxtest[8].value);
                var maxtest10 =Number(maxtest[9].value); 
                var maxtest11 =Number(maxtest[10].value);
                var maxtest12 =Number(maxtest[11].value);
                var maxtest13 =Number(maxtest[12].value);
                var maxtest14 =Number(maxtest[13].value);
                var maxtest15 =Number(maxtest[14].value);
                var maxtest16 =Number(maxtest[15].value);
                var maxtest17 =Number(maxtest[16].value);
                var maxtest18 =Number(maxtest[17].value);
                var maxtest19 =Number(maxtest[18].value);
                var maxtest20 = Number(maxtest[19].value);

                var maxtestcw1 = Number(maxtestcw[0].value);
                var maxtestcw2 = Number(maxtestcw[1].value);
                var maxtestcw3 = Number(maxtestcw[2].value);
                var maxtestcw4 = Number(maxtestcw[3].value);
                var maxtestcw5 = Number(maxtestcw[4].value);
                var maxtestcw6 = Number(maxtestcw[5].value);
                var maxtestcw7 = Number(maxtestcw[6].value);
                var maxtestcw8 = Number(maxtestcw[7].value);
                var maxtestcw9 = Number(maxtestcw[8].value);
                var maxtestcw10 = Number(maxtestcw[9].value);
                var maxtestcw11 = Number(maxtestcw[10].value);
                var maxtestcw12 = Number(maxtestcw[11].value);
                var maxtestcw13 = Number(maxtestcw[12].value);
                var maxtestcw14 = Number(maxtestcw[13].value);
                var maxtestcw15 = Number(maxtestcw[14].value);
                var maxtestcw16 = Number(maxtestcw[15].value);
                var maxtestcw17 = Number(maxtestcw[16].value);
                var maxtestcw18 = Number(maxtestcw[17].value);
                var maxtestcw19 = Number(maxtestcw[18].value);
                var maxtestcw20 = Number(maxtestcw[19].value);

                var maxmidscore = document.getElementsByClassName("maxmidscore");
                var maxlatescore = document.getElementsByClassName("maxlatescore");

                var maxmid = 0;
                var maxlate = 0;
                if (maxlatescore[0].value != "" && maxlatescore[0].value != "0")
                    maxlate = Number(maxlatescore[0].value);
                if (maxmidscore[0].value != "" && maxmidscore[0].value != "0")
                    maxmid = Number(maxmidscore[0].value);

                var maxquiz = maxtest1 + maxtest2 + maxtest3 + maxtest4 + maxtest5 + maxtest6 + maxtest7 + maxtest8 + maxtest9 + maxtest10 + maxtest11
                + maxtest12 + maxtest13 + maxtest14 + maxtest15 + maxtest16 + maxtest17 + maxtest18 + maxtest19 + maxtest20;

                var maxchewat = maxtestcw1 + maxtestcw2 + maxtestcw3 + maxtestcw4 + maxtestcw5 + maxtestcw6 + maxtestcw7 + maxtestcw8 + maxtestcw9 + maxtestcw10 + maxtestcw11
               + maxtestcw12 + maxtestcw13 + maxtestcw14 + maxtestcw15 + maxtestcw16 + maxtestcw17 + maxtestcw18 + maxtestcw19 + maxtestcw20;

                
                
                
                var totalquizscore = Number(maxquiz) + Number(maxchewat);
                //alert(totalquizscore);
                var y = 1;                
                var getmid = Number("0");
                var getlate = Number("0");
                var getquiz = Number("0");
                var getchewut = Number("0");
                
                var aa = 0;
                //alert(id);
                aa = 5 * (id - 1);
                var cc = aa + 5;
                if (id == 99999)
                {                    
                    aa = 0;
                    cc = Number(textBoxesg1.length);
                }
                
                for (var i = aa; i < cc; i++) {
                    if (id != 99999) y = id;
                    //find 1st textbox
                    if (i - (5 * y) + 5 == 0) {
                        
                        getmid = Number("0");
                        getlate = Number("0");
                        getquiz = Number("0");
                        getchewut = Number("0");
                        getquiz = getquiz + Number(textBoxesg1[i].value);
                        getquiz = getquiz + Number(textBoxesg2[i].value);
                        getquiz = getquiz + Number(textBoxesg3[i].value);
                        getquiz = getquiz + Number(textBoxesg4[i].value);
                        getchewut = getchewut + Number(chewut1[i].value);
                        getchewut = getchewut + Number(chewut2[i].value);
                        getchewut = getchewut + Number(chewut3[i].value);
                        getchewut = getchewut + Number(chewut4[i].value);

                        if (Number(textBoxesg1[i].value) > Number(maxtest1))
                        {
                            changebutton(i);
                            textBoxesg1[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg1[i].value) <= Number(maxtest1))
                        {
                            textBoxesg1[i].classList.remove('txtglow');
                        }
                        
                        if (isNumeric(textBoxesg1[i].value) == false) {                            
                            textBoxesg1[i].classList.add('txtglow2');
                        }
                        if (isNumeric(textBoxesg1[i].value) == true) {
                            textBoxesg1[i].classList.remove('txtglow2');
                        }
                           
                        if (Number(textBoxesg2[i].value) > Number(maxtest6))
                        {
                            changebutton(i);
                            textBoxesg2[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg2[i].value) <= Number(maxtest6))
                        {
                            textBoxesg2[i].classList.remove('txtglow');
                        }
                           
                        if (Number(textBoxesg3[i].value) > Number(maxtest11))
                        {
                            changebutton(i);
                            textBoxesg3[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg3[i].value) <= Number(maxtest11)) {                            
                            textBoxesg3[i].classList.remove('txtglow');
                        }
                            
                        if (Number(textBoxesg4[i].value) > Number(maxtest16))
                        {
                            changebutton(i);
                            textBoxesg4[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg4[i].value) <= Number(maxtest16)) {
                            textBoxesg4[i].classList.remove('txtglow');
                        }

                        if (Number(chewut1[i].value) > Number(maxtestcw1))
                        {
                            changebutton(i);
                            chewut1[i].classList.add('txtglow');
                        }
                        if (Number(chewut1[i].value) <= Number(maxtestcw1)) {
                            chewut1[i].classList.remove('txtglow');
                        }
                            
                        if (Number(chewut2[i].value) > Number(maxtestcw6))
                        {
                            changebutton(i);
                            chewut2[i].classList.add('txtglow');
                        }
                        if (Number(chewut2[i].value) <= Number(maxtestcw6)) {                            
                            chewut2[i].classList.remove('txtglow');
                        }
                            
                        if (Number(chewut3[i].value) > Number(maxtestcw11))
                        {
                            changebutton(i);
                            chewut3[i].classList.add('txtglow');
                        }
                        if (Number(chewut3[i].value) <= Number(maxtestcw11)) {                           
                            chewut3[i].classList.remove('txtglow');
                        }
                            
                        if (Number(chewut4[i].value) > Number(maxtestcw16))
                        {
                            changebutton(i);
                            chewut4[i].classList.add('txtglow');
                        }
                        if (Number(chewut4[i].value) <= Number(maxtestcw16)) {                            
                            chewut4[i].classList.remove('txtglow');
                        }
                            
                    }
                        // 2 box
                    else if (i - (5 * y) + 4 == 0) {
                        
                        getquiz = getquiz + Number(textBoxesg1[i].value);
                        getquiz = getquiz + Number(textBoxesg2[i].value);
                        getquiz = getquiz + Number(textBoxesg3[i].value);
                        getquiz = getquiz + Number(textBoxesg4[i].value);
                        getchewut = getchewut + Number(chewut1[i].value);
                        getchewut = getchewut + Number(chewut2[i].value);
                        getchewut = getchewut + Number(chewut3[i].value);
                        getchewut = getchewut + Number(chewut4[i].value);
                        

                        if (Number(textBoxesg1[i].value) > Number(maxtest2)) {
                            changebutton(i);
                            textBoxesg1[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg1[i].value) <= Number(maxtest2)) {
                            textBoxesg1[i].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg2[i].value) > Number(maxtest7)) {
                            changebutton(i);
                            textBoxesg2[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg2[i].value) <= Number(maxtest7)) {
                            textBoxesg2[i].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg3[i].value) > Number(maxtest12)) {
                            changebutton(i);
                            textBoxesg3[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg3[i].value) <= Number(maxtest12)) {
                            textBoxesg3[i].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg4[i].value) > Number(maxtest17)) {
                            changebutton(i);
                            textBoxesg4[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg4[i].value) <= Number(maxtest17)) {
                            textBoxesg4[i].classList.remove('txtglow');
                        }

                        if (Number(chewut1[i].value) > Number(maxtestcw2)) {
                            changebutton(i);
                            chewut1[i].classList.add('txtglow');
                        }
                        if (Number(chewut1[i].value) <= Number(maxtestcw2)) {
                            chewut1[i].classList.remove('txtglow');
                        }

                        if (Number(chewut2[i].value) > Number(maxtestcw7)) {
                            changebutton(i);
                            chewut2[i].classList.add('txtglow');
                        }
                        if (Number(chewut2[i].value) <= Number(maxtestcw7)) {
                            chewut2[i].classList.remove('txtglow');
                        }

                        if (Number(chewut3[i].value) > Number(maxtestcw12)) {
                            changebutton(i);
                            chewut3[i].classList.add('txtglow');
                        }
                        if (Number(chewut3[i].value) <= Number(maxtestcw12)) {
                            chewut3[i].classList.remove('txtglow');
                        }

                        if (Number(chewut4[i].value) > Number(maxtestcw17)) {
                            changebutton(i);
                            chewut4[i].classList.add('txtglow');
                        }
                        if (Number(chewut4[i].value) <= Number(maxtestcw17)) {
                            chewut4[i].classList.remove('txtglow');
                        }
                    }

                        //3 box
                    else if (i - (5 * y) + 3 == 0) {

                        getquiz = getquiz + Number(textBoxesg1[i].value);
                        getquiz = getquiz + Number(textBoxesg2[i].value);
                        getquiz = getquiz + Number(textBoxesg3[i].value);
                        getquiz = getquiz + Number(textBoxesg4[i].value);
                        getchewut = getchewut + Number(chewut1[i].value);
                        getchewut = getchewut + Number(chewut2[i].value);
                        getchewut = getchewut + Number(chewut3[i].value);
                        getchewut = getchewut + Number(chewut4[i].value);

                        

                        if (Number(textBoxesg1[i].value) > Number(maxtest3)) {
                            changebutton(i);
                            textBoxesg1[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg1[i].value) <= Number(maxtest3)) {
                            textBoxesg1[i].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg2[i].value) > Number(maxtest8)) {
                            changebutton(i);
                            textBoxesg2[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg2[i].value) <= Number(maxtest8)) {
                            textBoxesg2[i].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg3[i].value) > Number(maxtest13)) {
                            changebutton(i);
                            textBoxesg3[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg3[i].value) <= Number(maxtest13)) {
                            textBoxesg3[i].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg4[i].value) > Number(maxtest18)) {
                            changebutton(i);
                            textBoxesg4[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg4[i].value) <= Number(maxtest18)) {
                            textBoxesg4[i].classList.remove('txtglow');
                        }

                        if (Number(chewut1[i].value) > Number(maxtestcw3)) {
                            changebutton(i);
                            chewut1[i].classList.add('txtglow');
                        }
                        if (Number(chewut1[i].value) <= Number(maxtestcw3)) {
                            chewut1[i].classList.remove('txtglow');
                        }

                        if (Number(chewut2[i].value) > Number(maxtestcw8)) {
                            changebutton(i);
                            chewut2[i].classList.add('txtglow');
                        }
                        if (Number(chewut2[i].value) <= Number(maxtestcw8)) {
                            chewut2[i].classList.remove('txtglow');
                        }

                        if (Number(chewut3[i].value) > Number(maxtestcw13)) {
                            changebutton(i);
                            chewut3[i].classList.add('txtglow');
                        }
                        if (Number(chewut3[i].value) <= Number(maxtestcw13)) {
                            chewut3[i].classList.remove('txtglow');
                        }

                        if (Number(chewut4[i].value) > Number(maxtestcw18)) {
                            changebutton(i);
                            chewut4[i].classList.add('txtglow');
                        }
                        if (Number(chewut4[i].value) <= Number(maxtestcw18)) {
                            chewut4[i].classList.remove('txtglow');
                        }
                    }

                        // 4box
                    else if (i - (5 * y) + 2 == 0) {

                        getquiz = getquiz + Number(textBoxesg1[i].value);
                        getquiz = getquiz + Number(textBoxesg2[i].value);
                        getquiz = getquiz + Number(textBoxesg3[i].value);
                        getquiz = getquiz + Number(textBoxesg4[i].value);
                        getchewut = getchewut + Number(chewut1[i].value);
                        getchewut = getchewut + Number(chewut2[i].value);
                        getchewut = getchewut + Number(chewut3[i].value);
                        getchewut = getchewut + Number(chewut4[i].value);

                        

                        if (Number(textBoxesg1[i].value) > Number(maxtest4)) {
                            changebutton(i);
                            textBoxesg1[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg1[i].value) <= Number(maxtest4)) {
                            textBoxesg1[i].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg2[i].value) > Number(maxtest9)) {
                            changebutton(i);
                            textBoxesg2[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg2[i].value) <= Number(maxtest9)) {
                            textBoxesg2[i].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg3[i].value) > Number(maxtest14)) {
                            changebutton(i);
                            textBoxesg3[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg3[i].value) <= Number(maxtest14)) {
                            textBoxesg3[i].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg4[i].value) > Number(maxtest19)) {
                            changebutton(i);
                            textBoxesg4[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg4[i].value) <= Number(maxtest19)) {
                            textBoxesg4[i].classList.remove('txtglow');
                        }

                        if (Number(chewut1[i].value) > Number(maxtestcw4)) {
                            changebutton(i);
                            chewut1[i].classList.add('txtglow');
                        }
                        if (Number(chewut1[i].value) <= Number(maxtestcw4)) {
                            chewut1[i].classList.remove('txtglow');
                        }

                        if (Number(chewut2[i].value) > Number(maxtestcw9)) {
                            changebutton(i);
                            chewut2[i].classList.add('txtglow');
                        }
                        if (Number(chewut2[i].value) <= Number(maxtestcw9)) {
                            chewut2[i].classList.remove('txtglow');
                        }

                        if (Number(chewut3[i].value) > Number(maxtestcw14)) {
                            changebutton(i);
                            chewut3[i].classList.add('txtglow');
                        }
                        if (Number(chewut3[i].value) <= Number(maxtestcw14)) {
                            chewut3[i].classList.remove('txtglow');
                        }

                        if (Number(chewut4[i].value) > Number(maxtestcw19)) {
                            changebutton(i);
                            chewut4[i].classList.add('txtglow');
                        }
                        if (Number(chewut4[i].value) <= Number(maxtestcw19)) {
                            chewut4[i].classList.remove('txtglow');
                        }
                    }

                    
                    
                    //find last textbox
                    else if (i - (5 * y) + 1 == 0) {
                        getquiz = getquiz + Number(textBoxesg1[i].value);
                        getquiz = getquiz + Number(textBoxesg2[i].value);
                        getquiz = getquiz + Number(textBoxesg3[i].value);
                        getquiz = getquiz + Number(textBoxesg4[i].value);
                        getchewut = getchewut + Number(chewut1[i].value);
                        getchewut = getchewut + Number(chewut2[i].value);
                        getchewut = getchewut + Number(chewut3[i].value);
                        getchewut = getchewut + Number(chewut4[i].value);
                        
                        getmid = Number(textBoxes2[(y * 2) - 2].value);
                        getlate = Number(textBoxes2[(y * 2) - 1].value);

                        var quizpercent = 0;
                        if (Number(totalquizscore) != 0)
                             quizpercent = Number(((getquiz + getchewut) * ratioquiz) / totalquizscore).toFixed(decimal);
                        var midpercent = 0;
                        if (maxmid != 0 && maxmid != "")
                            midpercent = Number((getmid * ratiomid) / maxmid).toFixed(decimal);
                        var latepercent = 0;
                        if (maxlate != 0 && maxlate != "")
                            latepercent = Number((getlate * ratiolate) / maxlate).toFixed(decimal);
                        var totalpercent = Number(Number(quizpercent) + Number(midpercent) + Number(latepercent)).toFixed(decimal);

                        var totalgrade = "";
                        //var readingsore = "";

                        if (Number(totalpercent) > 79) { totalgrade = "4.0";}
                        else if (Number(totalpercent) > 74) { totalgrade = "3.5"; }
                        else if (Number(totalpercent) > 69) { totalgrade = "3.0";  }
                        else if (Number(totalpercent) > 64) { totalgrade = "2.5"; }
                        else if (Number(totalpercent) > 59) { totalgrade = "2.0";  }
                        else if (Number(totalpercent) > 54) { totalgrade = "1.5";  }
                        else if (Number(totalpercent) > 49) { totalgrade = "1.0";  }
                        else if (Number(totalpercent) > 39) { totalgrade = "0";  }
                        else { totalgrade = "0";  }

                        if (cog2[0].value == 1)
                        {
                            if (Number(totalpercent) > 79) {  readingsore = "3"; }
                            else if (Number(totalpercent) > 74) {  readingsore = "3"; }
                            else if (Number(totalpercent) > 69) {  readingsore = "3"; }
                            else if (Number(totalpercent) > 64) {  readingsore = "2"; }
                            else if (Number(totalpercent) > 59) {  readingsore = "2"; }
                            else if (Number(totalpercent) > 54) {  readingsore = "1"; }
                            else if (Number(totalpercent) > 49) {  readingsore = "1"; }
                            else if (Number(totalpercent) > 39) {  readingsore = "1"; }
                            else { readingsore = "0"; }
                            reading[(y - 1)].value = readingsore;
                        }
                        
                        gradebox[(y * 2) - 2].value = "";
                        gradebox[(y * 2) - 1].value = "";                        
                        gradebox[(y * 2) - 2].value = totalpercent;                            
                       

                        if (ddl8[0].value != "1") {                         
                            gradebox[(y * 2) - 1].value = totalgrade;                            
                        }
                             
                        var g = isFinite(gradebox[(y * 2) - 2].value);
                        if (g == false)
                            alert(gradebox[(y * 2) - 2].value);
                        
                        if (Number(getmid) > Number(maxmid))
                            changebutton(i);
                        if (Number(getlate) > Number(maxlate))
                            changebutton(i);

                        if (Number(textBoxes2[(y * 2) - 2].value) > Number(maxmid)) {
                            changebutton(i);
                            textBoxes2[(y * 2) - 2].classList.add('txtglow');
                        }
                        if (Number(textBoxes2[(y * 2) - 2].value) <= Number(maxmid)) {
                            textBoxes2[(y * 2) - 2].classList.remove('txtglow');
                        }

                        if (Number(textBoxes2[(y * 2) - 1].value) > Number(maxlate)) {
                            changebutton(i);
                            textBoxes2[(y * 2) - 1].classList.add('txtglow');
                        }
                        if (Number(textBoxes2[(y * 2) - 1].value) <= Number(maxlate)) {
                            textBoxes2[(y * 2) - 1].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg1[i].value) > Number(maxtest5)) {
                            changebutton(i);
                            textBoxesg1[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg1[i].value) <= Number(maxtest5)) {
                            textBoxesg1[i].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg2[i].value) > Number(maxtest10)) {
                            changebutton(i);
                            textBoxesg2[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg2[i].value) <= Number(maxtest10)) {
                            textBoxesg2[i].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg3[i].value) > Number(maxtest15)) {
                            changebutton(i);
                            textBoxesg3[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg3[i].value) <= Number(maxtest15)) {
                            textBoxesg3[i].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg4[i].value) > Number(maxtest20)) {
                            changebutton(i);
                            textBoxesg4[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg4[i].value) <= Number(maxtest20)) {
                            textBoxesg4[i].classList.remove('txtglow');
                        }

                        if (Number(chewut1[i].value) > Number(maxtestcw5)) {
                            changebutton(i);
                            chewut1[i].classList.add('txtglow');
                        }
                        if (Number(chewut1[i].value) <= Number(maxtestcw5)) {
                            chewut1[i].classList.remove('txtglow');
                        }

                        if (Number(chewut2[i].value) > Number(maxtestcw10)) {
                            changebutton(i);
                            chewut2[i].classList.add('txtglow');
                        }
                        if (Number(chewut2[i].value) <= Number(maxtestcw10)) {
                            chewut2[i].classList.remove('txtglow');
                        }

                        if (Number(chewut3[i].value) > Number(maxtestcw15)) {
                            changebutton(i);
                            chewut3[i].classList.add('txtglow');
                        }
                        if (Number(chewut3[i].value) <= Number(maxtestcw15)) {
                            chewut3[i].classList.remove('txtglow');
                        }

                        if (Number(chewut4[i].value) > Number(maxtestcw20)) {
                            changebutton(i);
                            chewut4[i].classList.add('txtglow');
                        }
                        if (Number(chewut4[i].value) <= Number(maxtestcw20)) {
                            chewut4[i].classList.remove('txtglow');
                        }
                        y = y + 1;

                    }
                    

                    
                }
            }
            function start() {
                var full = window.location.href;
                var half = full.split('?');
                var split = half[1].split('&');
                var year = split[0];
                var idlv = split[1];
                var idlv2 = split[2];
                var term = split[3];
                var id2 = split[4];
                var mode = split[5].split('=');
                if (mode[1] == "3")
                    nextpage();
                setupddl();
                changeddl();                
                changename();
                editddl();
                autobehave();
                CompareDates(99999);
                auto();
                //maxerror();
                var cog1 = document.getElementsByClassName("cog1");
                var cog2 = document.getElementsByClassName("cog2");
                var check5 = document.getElementsByClassName("check5");
                var scorebox = document.getElementsByClassName("scorebox");
                var btnok = document.getElementsByClassName("btnok");
                var btnerror = document.getElementsByClassName("btnerror");
                var btnback = document.getElementsByClassName("btnback");

                

                

                check5[0].checked == true;
                cog1[0].value = "0";
                cog2[0].value = "1";

                var full = window.location.href;
                var half = full.split('?');
                var split = half[1].split('&');
                var year = split[0];
                var idlv = split[1];
                var idlv2 = split[2];
                var term = split[3];
                var id2 = split[4];
                var mode = split[5].split('=');
                
                if (mode[1] != "EN")
                {
                    scorebox[0].value = "คะแนนเก็บหน่วยที่ 1-5";
                    btnok[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132205") %>";
                    btnerror[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132205") %>";
                    btnback[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>";
                }
                else {
                    scorebox[0].value = "Exercise 1-5";
                    btnok[0].value = "Submit";
                    btnerror[0].value = "Submit";
                    btnback[0].value = "Back";
                }               

                var name1 = document.getElementsByClassName("test1name");
                var name2 = document.getElementsByClassName("test2name");
                var name3 = document.getElementsByClassName("test3name");
                var name4 = document.getElementsByClassName("test4name");
                var name5 = document.getElementsByClassName("test5name");
                var name6 = document.getElementsByClassName("test6name");
                var name7 = document.getElementsByClassName("test7name");
                var name8 = document.getElementsByClassName("test8name");
                var name9 = document.getElementsByClassName("test9name");
                var name10 = document.getElementsByClassName("test10name");
                var name11 = document.getElementsByClassName("test11name");
                var name12 = document.getElementsByClassName("test12name");
                var name13 = document.getElementsByClassName("test13name");
                var name14 = document.getElementsByClassName("test14name");
                var name15 = document.getElementsByClassName("test15name");
                var name16 = document.getElementsByClassName("test16name");
                var name17 = document.getElementsByClassName("test17name");
                var name18 = document.getElementsByClassName("test18name");
                var name19 = document.getElementsByClassName("test19name");
                
                var set1 = document.getElementsByClassName("set1name");
                var set2 = document.getElementsByClassName("set2name");
                var set3 = document.getElementsByClassName("set3name");
                var set4 = document.getElementsByClassName("set4name");
                var set5 = document.getElementsByClassName("set5name");
                var set6 = document.getElementsByClassName("set6name");
                var set7 = document.getElementsByClassName("set7name");
                var set8 = document.getElementsByClassName("set8name");
                var set9 = document.getElementsByClassName("set9name");
                var set10 = document.getElementsByClassName("set10name");
                var set11 = document.getElementsByClassName("set11name");
                var set12 = document.getElementsByClassName("set12name");
                var set13 = document.getElementsByClassName("set13name");
                var set14 = document.getElementsByClassName("set14name");
                var set15 = document.getElementsByClassName("set15name");
                var set16 = document.getElementsByClassName("set16name");
                var set17 = document.getElementsByClassName("set17name");
                var set18 = document.getElementsByClassName("set18name");
                var set19 = document.getElementsByClassName("set19name");
                var set20 = document.getElementsByClassName("set20name");
                var setb1 = document.getElementsByClassName("setnameb1");
                var setb2 = document.getElementsByClassName("setnameb2");
                var setb3 = document.getElementsByClassName("setnameb3");
                var setb4 = document.getElementsByClassName("setnameb4");
                var setb5 = document.getElementsByClassName("setnameb5");
                var setb6 = document.getElementsByClassName("setnameb6");
                var setb7 = document.getElementsByClassName("setnameb7");
                var setb8 = document.getElementsByClassName("setnameb8");
                var setb9 = document.getElementsByClassName("setnameb9");
                var setb10 = document.getElementsByClassName("setnameb10");
                var nameb1 = document.getElementsByClassName("testb1name");
                var nameb2 = document.getElementsByClassName("testb2name");
                var nameb3 = document.getElementsByClassName("testb3name");
                var nameb4 = document.getElementsByClassName("testb4name");
                var nameb5 = document.getElementsByClassName("testb5name");
                var nameb6 = document.getElementsByClassName("testb6name");
                var nameb7 = document.getElementsByClassName("testb7name");
                var nameb8 = document.getElementsByClassName("testb8name");
                var nameb9 = document.getElementsByClassName("testb9name");
                var nameb10 = document.getElementsByClassName("testb10name");

                var namecw1 = document.getElementsByClassName("testcw1name");
                var namecw2 = document.getElementsByClassName("testcw2name");
                var namecw3 = document.getElementsByClassName("testcw3name");
                var namecw4 = document.getElementsByClassName("testcw4name");
                var namecw5 = document.getElementsByClassName("testcw5name");
                var namecw6 = document.getElementsByClassName("testcw6name");
                var namecw7 = document.getElementsByClassName("testcw7name");
                var namecw8 = document.getElementsByClassName("testcw8name");
                var namecw9 = document.getElementsByClassName("testcw9name");
                var namecw10 = document.getElementsByClassName("testcw10name");
                var namecw11 = document.getElementsByClassName("testcw11name");
                var namecw12 = document.getElementsByClassName("testcw12name");
                var namecw13 = document.getElementsByClassName("testcw13name");
                var namecw14 = document.getElementsByClassName("testcw14name");
                var namecw15 = document.getElementsByClassName("testcw15name");
                var namecw16 = document.getElementsByClassName("testcw16name");
                var namecw17 = document.getElementsByClassName("testcw17name");
                var namecw18 = document.getElementsByClassName("testcw18name");
                var namecw19 = document.getElementsByClassName("testcw19name");
                var namecw20 = document.getElementsByClassName("testcw20name");
                var setcw1 = document.getElementsByClassName("set1namecw");
                var setcw2 = document.getElementsByClassName("set2namecw");
                var setcw3 = document.getElementsByClassName("set3namecw");
                var setcw4 = document.getElementsByClassName("set4namecw");
                var setcw5 = document.getElementsByClassName("set5namecw");
                var setcw6 = document.getElementsByClassName("set6namecw");
                var setcw7 = document.getElementsByClassName("set7namecw");
                var setcw8 = document.getElementsByClassName("set8namecw");
                var setcw9 = document.getElementsByClassName("set9namecw");
                var setcw10 = document.getElementsByClassName("set10namecw");
                var setcw11 = document.getElementsByClassName("set11namecw");
                var setcw12 = document.getElementsByClassName("set12namecw");
                var setcw13 = document.getElementsByClassName("set13namecw");
                var setcw14 = document.getElementsByClassName("set14namecw");
                var setcw15 = document.getElementsByClassName("set15namecw");
                var setcw16 = document.getElementsByClassName("set16namecw");
                var setcw17 = document.getElementsByClassName("set17namecw");
                var setcw18 = document.getElementsByClassName("set18namecw");
                var setcw19 = document.getElementsByClassName("set19namecw");
                var setcw20 = document.getElementsByClassName("set20namecw");

                var el1 = $('.test1name');
                var el2 = $('.test2name');
                var el3 = $('.test3name');
                var el4 = $('.test4name');
                var el5 = $('.test5name');
                var el6 = $('.test6name');
                var el7 = $('.test7name');
                var el8 = $('.test8name');
                var el9 = $('.test9name');
                var el10 = $('.test10name');
                var el11 = $('.test11name');
                var el12 = $('.test12name');
                var el13 = $('.test13name');
                var el14 = $('.test14name');
                var el15 = $('.test15name');
                var el16 = $('.test16name');
                var el17 = $('.test17name');
                var el18 = $('.test18name');
                var el19 = $('.test19name');
                var el20 = $('.test20name');
                var elb1 = $('.testb1name');
                var elb2 = $('.testb2name');
                var elb3 = $('.testb3name');
                var elb4 = $('.testb4name');
                var elb5 = $('.testb5name');
                var elb6 = $('.testb6name');
                var elb7 = $('.testb7name');
                var elb8 = $('.testb8name');
                var elb9 = $('.testb9name');
                var elb10 = $('.testb10name');
                var elc1 = $('.testcw1name');
                var elc2 = $('.testcw2name');
                var elc3 = $('.testcw3name');
                var elc4 = $('.testcw4name');
                var elc5 = $('.testcw5name');
                var elc6 = $('.testcw6name');
                var elc7 = $('.testcw7name');
                var elc8 = $('.testcw8name');
                var elc9 = $('.testcw9name');
                var elc10 = $('.testcw10name');
                var elc11 = $('.testcw11name');
                var elc12 = $('.testcw12name');
                var elc13 = $('.testcw13name');
                var elc14 = $('.testcw14name');
                var elc15 = $('.testcw15name');
                var elc16 = $('.testcw16name');
                var elc17 = $('.testcw17name');
                var elc18 = $('.testcw18name');
                var elc19 = $('.testcw19name');
                var elc20 = $('.testcw20name');


                // Shows tooltip with title: "My new title"

                el1.protipShow({
                    title: set1[0].value,
                    trigger: 'hover'
                });
                el2.protipShow({
                    title: set2[0].value,
                    trigger: 'hover'
                });
                el3.protipShow({
                    title: set3[0].value,
                    trigger: 'hover'
                });
                el4.protipShow({
                    title: set4[0].value,
                    trigger: 'hover'
                });
                el5.protipShow({
                    title: set5[0].value,
                    trigger: 'hover'
                });
                el6.protipShow({
                    title: set6[0].value,
                    trigger: 'hover'
                });
                el7.protipShow({
                    title: set7[0].value,
                    trigger: 'hover'
                });
                el8.protipShow({
                    title: set8[0].value,
                    trigger: 'hover'
                });
                el9.protipShow({
                    title: set9[0].value,
                    trigger: 'hover'
                });
                el10.protipShow({
                    title: set10[0].value,
                    trigger: 'hover'
                });
                el11.protipShow({
                    title: set11[0].value,
                    trigger: 'hover'
                });
                el12.protipShow({
                    title: set12[0].value,
                    trigger: 'hover'
                });
                el13.protipShow({
                    title: set13[0].value,
                    trigger: 'hover'
                });
                el14.protipShow({
                    title: set14[0].value,
                    trigger: 'hover'
                });
                el15.protipShow({
                    title: set15[0].value,
                    trigger: 'hover'
                });
                el16.protipShow({
                    title: set16[0].value,
                    trigger: 'hover'
                });
                el17.protipShow({
                    title: set17[0].value,
                    trigger: 'hover'
                });
                el18.protipShow({
                    title: set18[0].value,
                    trigger: 'hover'
                });
                el19.protipShow({
                    title: set19[0].value,
                    trigger: 'hover'
                });
                el20.protipShow({
                    title: set20[0].value,
                    trigger: 'hover'
                });
                elb1.protipShow({
                    title: setb1[0].value,
                    trigger: 'hover'
                });
                elb2.protipShow({
                    title: setb2[0].value,
                    trigger: 'hover'
                });
                elb3.protipShow({
                    title: setb3[0].value,
                    trigger: 'hover'
                });
                elb4.protipShow({
                    title: setb4[0].value,
                    trigger: 'hover'
                });
                elb5.protipShow({
                    title: setb5[0].value,
                    trigger: 'hover'
                });
                elb6.protipShow({
                    title: setb6[0].value,
                    trigger: 'hover'
                });
                elb7.protipShow({
                    title: setb7[0].value,
                    trigger: 'hover'
                });
                elb8.protipShow({
                    title: setb8[0].value,
                    trigger: 'hover'
                });
                elb9.protipShow({
                    title: setb9[0].value,
                    trigger: 'hover'
                });
                elb10.protipShow({
                    title: setb10[0].value,
                    trigger: 'hover'
                });
                elc1.protipShow({
                    title: setcw1[0].value,
                    trigger: 'hover'
                });
                elc2.protipShow({
                    title: setcw2[0].value,
                    trigger: 'hover'
                });
                elc3.protipShow({
                    title: setcw3[0].value,
                    trigger: 'hover'
                });
                elc4.protipShow({
                    title: setcw4[0].value,
                    trigger: 'hover'
                });
                elc5.protipShow({
                    title: setcw5[0].value,
                    trigger: 'hover'
                });
                elc6.protipShow({
                    title: setcw6[0].value,
                    trigger: 'hover'
                });
                elc7.protipShow({
                    title: setcw7[0].value,
                    trigger: 'hover'
                });
                elc8.protipShow({
                    title: setcw8[0].value,
                    trigger: 'hover'
                });
                elc9.protipShow({
                    title: setcw9[0].value,
                    trigger: 'hover'
                });
                elc10.protipShow({
                    title: setcw10[0].value,
                    trigger: 'hover'
                });
                elc11.protipShow({
                    title: setcw11[0].value,
                    trigger: 'hover'
                });
                elc12.protipShow({
                    title: setcw12[0].value,
                    trigger: 'hover'
                });
                elc13.protipShow({
                    title: setcw13[0].value,
                    trigger: 'hover'
                });
                elc14.protipShow({
                    title: setcw14[0].value,
                    trigger: 'hover'
                });
                elc15.protipShow({
                    title: setcw15[0].value,
                    trigger: 'hover'
                });
                elc16.protipShow({
                    title: setcw16[0].value,
                    trigger: 'hover'
                });
                elc17.protipShow({
                    title: setcw17[0].value,
                    trigger: 'hover'
                });
                elc18.protipShow({
                    title: setcw18[0].value,
                    trigger: 'hover'
                });
                elc19.protipShow({
                    title: setcw19[0].value,
                    trigger: 'hover'
                });
                elc20.protipShow({
                    title: setcw20[0].value,
                    trigger: 'hover'
                });
                var protipshow = document.getElementsByClassName("protip-show");

                while (protipshow.length != 0) {
                    protipshow[0].classList.remove('protip-show');
                }

                setTimeout(function () {
                    removeprotip();
                }, 1);
                if (mode[1] != "3")
                    $('#loading').hide();
            }

            function removeprotip() {
                var protipshow = document.getElementsByClassName("protip-show");
                
                while (protipshow.length != 0) {
                    protipshow[0].classList.remove('protip-show');
                }
                
            }

            $(window).on('load', function () {
                var full = window.location.href;
                var half = full.split('?');
                var split = half[1].split('&');
                var year = split[0];
                var idlv = split[1];
                var idlv2 = split[2];
                var term = split[3];
                var id2 = split[4];
                var mode = split[5].split('=');

                var btnerror = document.getElementsByClassName("btnerror");
                var btnback = document.getElementsByClassName("btnback");
                

                if (mode[1] != "EN") {
                    //scorebox[0].value = "คะแนนเก็บหน่วยที่ 1-5";
                    //btnok[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132205") %>";
                    btnerror[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132205") %>";
                    btnback[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>";
                }
                
                changename();
                $('#loading').hide();
            });

        </script>
         <script>



            



            function changepage(value) {
                var full = window.location.href;
                var half = full.split('?');
                var split = half[1].split('&');
                var year = split[0];
                var idlv = split[1];
                var idlv2 = split[2];
                var term = split[3];
                var id = split[4];

                if (value == 1) {
                    
                    window.location = ("Webform2.aspx?" + year + "&" + idlv + "&" + idlv2 + "&" + term + "&" + id +"&mode=EN");
                }
                if (value == 2) {
                    
                    window.location = ("Webform2.aspx?" + year + "&" + idlv + "&" + idlv2 + "&" + term + "&" + id +"&mode=1");
                }

            }

            function popup() {
                
                // Get the modal
                var modal = document.getElementById('myModal');

                // Get the image and insert it inside the modal - use its "alt" text as a caption
                var img = document.getElementById('myImg');
                var modalImg = document.getElementById("img01");
                var captionText = document.getElementById("caption");
                modal.style.display = "block";
                modalImg.src = "https://i.imgur.com/4xpYhQI.png";
                captionText.innerHTML = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132216") %>";
            }

            
            function popup2() {
                
                // Get the <span> element that closes the modal
                var span = document.getElementsByClassName("close2")[0];
                var modal = document.getElementById('myModal');
                // When the user clicks on <span> (x), close the modal
                modal.style.display = "none";
            }

            function mdown() {                
                // Get the <span> element that closes the modal
                var span = document.getElementsByClassName("close2")[0];
                var modal = document.getElementById('myModal');
                // When the user clicks on <span> (x), close the modal
                modal.style.display = "none";
            }


</script>
        

        <%--        <a href="plans-term.aspx" class="btn btn-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131220") %></a> <a href="periodslist.aspx"
            class="btn btn-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131226") %></a>--%>
        <div class="full-card box-content ">
           <!-- Sidebar -->
               
            
            <div class="col-xs-12">
                <div class="col-xs-2 righttext" style="padding:0px;">                            
                    <asp:Label ID="headertxtclass" cssclass="bold" runat="server" > </asp:Label>
                </div>

                <div class="col-xs-3">
                    <asp:Label ID="txtclass" cssclass="userplan"                                                                
                               runat="server">                                    
                    </asp:Label>                           
                </div>
                <div class="col-xs-2 righttext">                            
                    <asp:Label ID="headertxtyear" cssclass="bold" runat="server" > </asp:Label>
                </div>
                <div class="col-xs-2">
                    <asp:Label ID="Year"  cssclass="useryear"                                                                                    
                               runat="server">                                    
                    </asp:Label>                            
                </div>  
                <div class="col-xs-3">
                   
                    </div>              
            </div>

            <div class="col-xs-12">                

<!-- The Modal -->
<div id="myModal" class="modal2" onmousedown="mdown()">
  <span class="close2" onclick="popup2()">&times;</span>
  <img class="modal2-content" id="img01">
  <div id="caption"></div>
</div>
                <div id="loading"></div>
                <div class="col-xs-2 righttext" style="padding:0px;">   
                            
                    <asp:Label ID="headerteacher1" cssclass="bold font90"  runat="server" > </asp:Label>
                </div>

                <div class="col-xs-3">
                    <asp:Label ID="teacher1" cssclass="userplan"                                                                
                               runat="server">                                    
                    </asp:Label>                           
                </div>
                
                
            </div>
           
            
            <div class="col-xs-12 hidden">
                <div class="col-xs-3 righttext">                            
                    <label>BehaviorPart</label>
                </div>
                <div class="col-xs-2">
                    <asp:Textbox ID="behavepart" runat="server" CssClass="cog1" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-2 righttext">                            
                    <label>disableReading</label>
                </div>
                <div class="col-xs-4">
                    <asp:Textbox ID="disreading" runat="server" CssClass="cog2"  width="50%"> </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-3 righttext">                            
                    <label>dicimal</label>
                </div>
                <div class="col-xs-2">
                    <asp:Textbox ID="dicimalCheck" runat="server" CssClass="dicimal1" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-2 righttext">                            
                    <label>disableBehavior</label>
                </div>
                <div class="col-xs-4">
                    <asp:Textbox ID="disbehave" runat="server" CssClass="behavedisable" width="50%" > </asp:Textbox>                           
                </div>
            </div>

            <div class="col-xs-12 hidden">
                <div class="col-xs-3 righttext">                            
                    <label>copy1</label>
                </div>
                <div class="col-xs-2">
                    <asp:Textbox ID="Textbox1" runat="server" CssClass="copy1" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-2 righttext">                            
                    <label>copy2</label>
                </div>
                <div class="col-xs-4">
                    <asp:Textbox ID="Textbox3" runat="server" CssClass="copy2" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-3 righttext">                            
                    <label>noBehave noRead</label>
                </div>
                <div class="col-xs-2">
                    <asp:Textbox ID="nobehaveCheck" runat="server" CssClass="nobehave" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-2 righttext">                            
                    <label>????</label>
                </div>
                <div class="col-xs-4">
                    <asp:Textbox ID="Textbox5" runat="server" CssClass="" width="50%" > </asp:Textbox>                           
                </div>
            </div>
             <div class="col-xs-12 hidden">
                <div class="col-xs-3 righttext">                            
                    <label>ddl1</label>
                </div>
                <div class="col-xs-2">
                    <asp:Textbox ID="ddl1" runat="server" CssClass="autoddl1" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-2 righttext">                            
                    <label>ddl2</label>
                </div>
                <div class="col-xs-4">
                    <asp:Textbox ID="ddl2" runat="server" CssClass="autoddl2"  width="50%"> </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-3 righttext">                            
                    <label>ddl3</label>
                </div>
                <div class="col-xs-2">
                    <asp:Textbox ID="ddl3" runat="server" CssClass="autoddl3" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-2 righttext">                            
                    <label>ddl4</label>
                </div>
                <div class="col-xs-4">
                    <asp:Textbox ID="ddl4" runat="server" CssClass="autoddl4" width="50%" > </asp:Textbox>                           
                </div>
            </div>

            <div class="col-xs-12 hidden">
                <div class="col-xs-3 righttext">                            
                    <label>ddl5</label>
                </div>
                <div class="col-xs-2">
                    <asp:Textbox ID="ddl5" runat="server" CssClass="autoddl5" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-2 righttext">                            
                    <label>ddl6</label>
                </div>
                <div class="col-xs-4">
                    <asp:Textbox ID="ddl6" runat="server" CssClass="autoddl6" width="50%" > </asp:Textbox>                           
                </div>
            </div>

            <div class="col-xs-12 hidden">
                <div class="col-xs-3 righttext">                            
                    <label>ddl7</label>
                </div>
                <div class="col-xs-2">
                    <asp:Textbox ID="ddl7" runat="server" CssClass="autoddl7" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-2 righttext">                            
                    <label>ddl8</label>
                </div>
                <div class="col-xs-4">
                    <asp:Textbox ID="ddl8" runat="server" CssClass="autoddl8" width="50%" > </asp:Textbox>                           
                </div>
            </div>

            <div class="col-xs-12 hidden">
                <div class="col-xs-3 righttext">                            
                    <label>setupddl1</label>
                </div>
                <div class="col-xs-2">
                    <asp:Textbox ID="setup1" runat="server" CssClass="setup1" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-2 righttext">                            
                    <label>setupddl2</label>
                </div>
                <div class="col-xs-4">
                    <asp:Textbox ID="setup2" runat="server" CssClass="setup2"  width="50%"> </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-3 righttext">                            
                    <label>setupddl3</label>
                </div>
                <div class="col-xs-2">
                    <asp:Textbox ID="setup3" runat="server" CssClass="setup3" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-2 righttext">                            
                    <label>setupddl4</label>
                </div>
                <div class="col-xs-4">
                    <asp:Textbox ID="setup4" runat="server" CssClass="setup4" width="50%" > </asp:Textbox>                           
                </div>
            </div>

            <div class="col-xs-12 hidden">
                <div class="col-xs-3 righttext">                            
                    <label>setupddl5</label>
                </div>
                <div class="col-xs-2">
                    <asp:Textbox ID="setup5" runat="server" CssClass="setup5" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-2 righttext">                            
                    <label>setupddl6</label>
                </div>
                <div class="col-xs-4">
                    <asp:Textbox ID="setup6" runat="server" CssClass="setup6" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-3 righttext">                            
                    <label>setupddl7</label>
                </div>
                <div class="col-xs-2">
                    <asp:Textbox ID="setup7" runat="server" CssClass="setup7" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-2 righttext">                            
                    <label>setupddl8</label>
                </div>
                <div class="col-xs-4">
                    <asp:Textbox ID="setup8" runat="server" CssClass="setup8" width="50%" > </asp:Textbox>                           
                </div>
            </div>

            <div class="col-xs-12 hidden">
                <div class="col-xs-3 righttext">                            
                    <label>focusnow</label>
                </div>
                <div class="col-xs-2">
                    <asp:Textbox ID="nowfocus" runat="server" CssClass="focus1" width="50%" > </asp:Textbox>                          
                </div>   
                <div class="col-xs-3 righttext">                            
                    <label>focusrow</label>
                </div>
                <div class="col-xs-2">
                    <asp:Textbox ID="Textbox9" runat="server" CssClass="focus2" width="50%" > </asp:Textbox>                          
                </div>              
            </div>
            
            <div class="col-xs-12 hidden">
                <div class="col-xs-3 righttext">                            
                    <label>now</label>
                </div>
                <div class="col-xs-2">
                    <asp:Textbox ID="Textbox11" runat="server" CssClass="fnow" width="50%" > </asp:Textbox>                          
                </div>   
                <div class="col-xs-3 righttext">                            
                    <label>page</label>
                </div>
                <div class="col-xs-2">
                    <asp:Textbox ID="Textbox15" runat="server" CssClass="fpage" width="50%" > </asp:Textbox>                          
                </div>              
            </div>

            <div class="col-xs-12 hidden">
                <div class="col-xs-3 righttext">                            
                    <label>xx</label>
                </div>
                <div class="col-xs-2">
                    <asp:Textbox ID="Textbox2" runat="server" CssClass="xx" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-2 righttext">                            
                    <label>yy</label>
                </div>
                <div class="col-xs-4">
                    <asp:Textbox ID="Textbox4" runat="server" CssClass="yy" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-3 righttext">                            
                    <label>up</label>
                </div>
                <div class="col-xs-2">
                    <asp:Textbox ID="Textbox12" runat="server" CssClass="fup" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-2 righttext">                            
                    <label>down</label>
                </div>
                <div class="col-xs-4">
                    <asp:Textbox ID="Textbox13" runat="server" CssClass="fdown" width="50%" > </asp:Textbox>                           
                </div>
            </div>

            <div class="col-xs-12 hidden">
                <div class="col-xs-3 righttext">                            
                    <label>left</label>
                </div>
                <div class="col-xs-2">
                    <asp:Textbox ID="Textbox6" runat="server" CssClass="fleft" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-2 righttext">                            
                    <label>right</label>
                </div>
                <div class="col-xs-4">
                    <asp:Textbox ID="Textbox7" runat="server" CssClass="fright" width="50%" > </asp:Textbox>                           
                </div>
            </div>
               
        
             <div class="col-xs-12" style="padding:0px;">
                 <div class="col-xs-3" style="padding:0px;">

                 </div>
                 <div class="col-xs-3" style="padding:0px;">
 <!-- Trigger the modal with a button -->
<div class="modal fade" id="myModalratio" role="dialog">
    <div class="modal-dialog modal-md">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132242") %></h4>
        </div>
         <div class="modal-body">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-6 righttext">                            
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132288") %> </label>
                </div>
                <div class="col-md-3">
                    
                </div>
                <div class="col-xs-1">                            
                    <label>%</label>
                </div>
                </div>
            </div>
             <div class="container-fluid">
                <div class="row">
                    <div class="col-md-6 righttext">                            
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132243") %> </label>
                </div>
                <div class="col-md-3">
                                                   
                </div>
                <div class="col-md-1">                            
                    <label>%</label>
                </div>
                </div>
            </div>
             <div class="container-fluid">
                <div class="row">
                    <div class="col-md-6 righttext">                            
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132244") %> </label>
                </div>
                <div class="col-md-3">
                                                       
                </div>
                <div class="col-md-1">                            
                    <label>%</label>
                </div>
                </div>
            </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
                     
<!-- Modal1 -->
<div class="modal fade" id="ratiomodal" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="ddlbox1" runat="server" CssClass="ddl1set" > </asp:Textbox>
            <asp:Textbox ID="ddlbox2" runat="server" CssClass="ddl2set" > </asp:Textbox>
            <asp:Textbox ID="ddlbox3" runat="server" CssClass="ddl3set" > </asp:Textbox>
            <asp:Textbox ID="ddlbox4" runat="server" CssClass="ddl4set" > </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<div class="modal fade" id="myModal1" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setname1" runat="server" CssClass="set1name" onkeyup="changename(1)" > </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>


<!-- Modal2 -->
<div class="modal fade" id="myModal2" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setname2" runat="server" CssClass="set2name" onkeyup="changename(2)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal3 -->
<div class="modal fade" id="myModal3" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setname3" runat="server" CssClass="set3name" onkeyup="changename(3)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal4 -->
<div class="modal fade" id="myModal4" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setname4" runat="server" CssClass="set4name" onkeyup="changename(4)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>


<!-- Modal5 -->
<div class="modal fade" id="myModal5" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setname5" runat="server" CssClass="set5name" onkeyup="changename(5)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal6 -->
<div class="modal fade" id="myModal6" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setname6" runat="server" CssClass="set6name" onkeyup="changename(6)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal7 -->
<div class="modal fade" id="myModal7" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setname7" runat="server" CssClass="set7name" onkeyup="changename(7)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal8 -->
<div class="modal fade" id="myModal8" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setname8" runat="server" CssClass="set8name" onkeyup="changename(8)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal9 -->
<div class="modal fade" id="myModal9" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setname9" runat="server" CssClass="set9name" onkeyup="changename(9)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal10 -->
<div class="modal fade" id="myModal10" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setname10" runat="server" CssClass="set10name" onkeyup="changename(10)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal11 -->
<div class="modal fade" id="myModal11" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setname11" runat="server" CssClass="set11name" onkeyup="changename(11)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal12 -->
<div class="modal fade" id="myModal12" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setname12" runat="server" CssClass="set12name" onkeyup="changename(12)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal13 -->
<div class="modal fade" id="myModal13" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setname13" runat="server" CssClass="set13name" onkeyup="changename(13)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>


<!-- Modal14 -->
<div class="modal fade" id="myModal14" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setname14" runat="server" CssClass="set14name" onkeyup="changename(14)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal15 -->
<div class="modal fade" id="myModal15" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setname15" runat="server" CssClass="set15name" onkeyup="changename(15)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal16 -->
<div class="modal fade" id="myModal16" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setname16" runat="server" CssClass="set16name" onkeyup="changename(16)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal17 -->
<div class="modal fade" id="myModal17" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setname17" runat="server" CssClass="set17name" onkeyup="changename(17)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal18 -->
<div class="modal fade" id="myModal18" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setname18" runat="server" CssClass="set18name" onkeyup="changename(18)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal19 -->
<div class="modal fade" id="myModal19" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setname19" runat="server" CssClass="set19name" onkeyup="changename(19)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal20 -->
<div class="modal fade" id="myModal20" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setname20" runat="server" CssClass="set20name" onkeyup="changename(20)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

                     <!-- chewut -->
                     <div class="modal fade" id="myModalcw1" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="chewatname1" runat="server" CssClass="set1namecw" onkeyup="changename(21)" > </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>


<!-- Modal2 -->
<div class="modal fade" id="myModalcw2" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="chewatname2" runat="server" CssClass="set2namecw" onkeyup="changename(22)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal3 -->
<div class="modal fade" id="myModalcw3" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="chewatname3" runat="server" CssClass="set3namecw" onkeyup="changename(23)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal4 -->
<div class="modal fade" id="myModalcw4" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="chewatname4" runat="server" CssClass="set4namecw" onkeyup="changename(24)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>


<!-- Modal5 -->
<div class="modal fade" id="myModalcw5" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="chewatname5" runat="server" CssClass="set5namecw" onkeyup="changename(25)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal6 -->
<div class="modal fade" id="myModalcw6" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="chewatname6" runat="server" CssClass="set6namecw" onkeyup="changename(26)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal7 -->
<div class="modal fade" id="myModalcw7" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="chewatname7" runat="server" CssClass="set7namecw" onkeyup="changename(27)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal8 -->
<div class="modal fade" id="myModalcw8" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="chewatname8" runat="server" CssClass="set8namecw" onkeyup="changename(28)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal9 -->
<div class="modal fade" id="myModalcw9" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="chewatname9" runat="server" CssClass="set9namecw" onkeyup="changename(29)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal10 -->
<div class="modal fade" id="myModalcw10" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="chewatname10" runat="server" CssClass="set10namecw" onkeyup="changename(30)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal11 -->
<div class="modal fade" id="myModalcw11" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="chewatname11" runat="server" CssClass="set11namecw" onkeyup="changename(31)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal12 -->
<div class="modal fade" id="myModalcw12" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="chewatname12" runat="server" CssClass="set12namecw" onkeyup="changename(32)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal13 -->
<div class="modal fade" id="myModalcw13" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="chewatname13" runat="server" CssClass="set13namecw" onkeyup="changename(33)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>


<!-- Modal14 -->
<div class="modal fade" id="myModalcw14" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="chewatname14" runat="server" CssClass="set14namecw" onkeyup="changename(34)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal15 -->
<div class="modal fade" id="myModalcw15" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="chewatname15" runat="server" CssClass="set15namecw" onkeyup="changename(35)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal16 -->
<div class="modal fade" id="myModalcw16" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="chewatname16" runat="server" CssClass="set16namecw" onkeyup="changename(36)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal17 -->
<div class="modal fade" id="myModalcw17" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="chewatname17" runat="server" CssClass="set17namecw" onkeyup="changename(37)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal18 -->
<div class="modal fade" id="myModalcw18" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="chewatname18" runat="server" CssClass="set18namecw" onkeyup="changename(38)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal19 -->
<div class="modal fade" id="myModalcw19" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="chewatname19" runat="server" CssClass="set19namecw" onkeyup="changename(39)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

<!-- Modal20 -->
<div class="modal fade" id="myModalcw20" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="chewatname20" runat="server" CssClass="set20namecw" onkeyup="changename(40)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

                     <!-- ModalB1 -->
<div class="modal fade" id="myModalB1" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setnameb1" runat="server" CssClass="setnameb1" onkeyup="changename(41)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>

           <!-- ModalB2 -->
<div class="modal fade" id="myModalB2" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setnameb2" runat="server" CssClass="setnameb2" onkeyup="changename(42)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
                     
                     <!-- ModalB3 -->
<div class="modal fade" id="myModalB3" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setnameb3" runat="server" CssClass="setnameb3" onkeyup="changename(43)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
                     
                     <!-- ModalB4 -->
<div class="modal fade" id="myModalB4" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setnameb4" runat="server" CssClass="setnameb4" onkeyup="changename(44)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
                     
                     <!-- ModalB5 -->
<div class="modal fade" id="myModalB5" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setnameb5" runat="server" CssClass="setnameb5" onkeyup="changename(45)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
                     
                     <!-- ModalB6 -->
<div class="modal fade" id="myModalB6" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setnameb6" runat="server" CssClass="setnameb6" onkeyup="changename(46)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
                     
                     <!-- ModalB7 -->
<div class="modal fade" id="myModalB7" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setnameb7" runat="server" CssClass="setnameb7" onkeyup="changename(47)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
                     
                     <!-- ModalB8 -->
<div class="modal fade" id="myModalB8" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setnameb8" runat="server" CssClass="setnameb8" onkeyup="changename(48)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
                     
                     <!-- ModalB9 -->
<div class="modal fade" id="myModalB9" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setnameb9" runat="server" CssClass="setnameb9" onkeyup="changename(49)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
                     
                     <!-- ModalB10 -->
<div class="modal fade" id="myModalB10" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="setnameb10" runat="server" CssClass="setnameb10" onkeyup="changename(50)"> </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div> 
                     <!-- Modalcopy1 -->
<div class="modal fade" id="myModalcopy1" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206050") %></h4>
        </div>
        <div class="modal-body">
            <asp:DropDownList ID="ddlcopy1"  runat="server" CssClass="ddlcopy1" Width="100%"  onchange="ddlcopy(1);">                  
                                 <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206051") %>" Value="-1" class="grey hidden"></asp:ListItem>                                                                
                            </asp:DropDownList>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-success" data-dismiss="modal" onclick="ddlcopy(3)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %></button>
        </div>
      </div>
    </div>
  </div>
                     <!-- Modalcopy2 -->
<div class="modal fade" id="myModalcopy2" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132234") %></h4>
        </div>
        <div class="modal-body">
            <asp:DropDownList ID="ddlcopy2"  runat="server" CssClass="ddlcopy2" Width="100%"   onchange="ddlcopy(2);">                  
                                 <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206051") %>" Value="-1" class="grey hidden"></asp:ListItem>                                
                            </asp:DropDownList>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-success" data-dismiss="modal" onclick="ddlcopy(4)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %></button>
        </div>
      </div>
    </div>
  </div>         



                     
                 </div>
                 <div class="col-xs-5" style="padding:0px;">

                 </div>
             </div>
<div class="col-xs-12" id="" style="padding:0px;">
            <div class="col-xs-3" style="padding:0px;">

                    
                </div>
             <div class="col-xs-9" style="padding:0px; background-color:white;" >
                    
                 <div class="navigation">
   <ul id="myTab" class="nav nav-tabs nav-tabs-title">
            <li class="bluebutton active"><a href="#one" style="font-size:80%;padding-left:11px;padding-right:10px;" onclick="nextbutton(1)" data-target="#one, #dgdone" data-toggle="tab"><asp:Label ID="tabOne" cssclass="" runat="server" > </asp:Label></a></li>
            <li class="bluebutton"> <a href="#two" style="font-size:80%;padding-left:11px;padding-right:11px;"  onclick="nextbutton(2)" data-target="#two, #dgdtwo" data-toggle="tab"><asp:Label ID="tabTwo" cssclass="" runat="server" > </asp:Label></a></li>
            <li class="bluebutton"> <a href="#three" style="font-size:80%;padding-left:11px;padding-right:10px;"  onclick="nextbutton(3)" data-target="#three, #dgdthree" data-toggle="tab"><asp:Label ID="tabThree" cssclass="" runat="server" > </asp:Label></a></li>     
            <li class="bluebutton"> <a href="#five" style="font-size:80%;padding-left:11px;padding-right:11px;"  onclick="nextbutton(4)" data-target="#five, #dgdfive" data-toggle="tab"><asp:Label ID="tabFour" cssclass="" runat="server" > </asp:Label></a></li>
            <li class="bluebutton"> <a href="#seven" style="font-size:80%;padding-left:11px;padding-right:10px;"  onclick="nextbutton(5)" data-target="#seven, #dgdseven" data-toggle="tab"><asp:Label ID="tabFive" cssclass="" runat="server" > </asp:Label></a></li>                   
        </ul>
                </div>

                 
                 </div>
    
            
            </div>
        <div class="col-xs-12 " style="padding:0px;">
            <div class="col-xs-3 container" style="padding:0px; background-color:black;">

                    <table class="table2" style="width:100%">
  <tr class="notdtr">
    <th  class="notdtr headerCell " style="width:0.001%; padding:0px;" ></th>
    <th rowspan="3" class="tdtr headerCell" style="width:40px; padding:0px;" ><asp:Label ID="tablenumber" cssclass="bold" runat="server" > </asp:Label></th>
    <th rowspan="2" style="border-right:1px;" class="tdtr headerCell "><asp:Label ID="tablestudentname" cssclass="bold" runat="server" > </asp:Label></th> 
   
  </tr>
  <tr class ="notdtr">
   
  <td class ="notdtr headerCell" style="border-right:0px;"> </td>
    
  </tr>

                <tr class ="tdtr22">
    <td class ="notdtr headerCell" style="border-right:1px;"> </td>
     <td class ="tdtr22 headerCell" style="border-right:1px;"> <asp:Label ID="tablefullscore" cssclass="bold" runat="server" > </asp:Label> </td>
  </tr>
  
  
</table>
                </div>
             <div class="col-xs-6" style="padding:0px; background-color:white;" >
                    <table class="table3" style="width:105%; height:70%; padding:0px;">
  <tr class="tdtr2">
     
    <th  class ="tdtr2 headerCell" style="border-left:1px; height:26px;">
        <div class="col-xs-12" style="padding:0px">
            <div class="col-xs-1" style="padding:0px">
                <div class="navigation">
   <ul id="myTab4" class="nav">
            <li class="left1 hidden"><a href="#three"style="padding:0px;" onclick="nextbutton(3)" data-target="#three, #dgdthree" data-toggle="tab"><span class="glyphicon glyphicon-chevron-left" style="font-size:80%"></span></a></li>
            <li class="left2 hidden"><a href="#five"style="padding:0px;"  onclick="nextbutton(4)" data-target="#five, #dgdfive" data-toggle="tab"><span class="glyphicon glyphicon-chevron-left" style="font-size:80%"></span></a></li>                        
        </ul>
                </div>
            </div>
            <div class="col-xs-9" style="height:25px">
                <input style="height:80%; width:105%; color:white; background-color:#337AB7; border:none;" type="text" class="headertxt centertext scorebox" >
            </div>
            <div class="col-xs-1" style="padding-left:10px">
                <div class="navigation">
   <ul id="myTab3" class="nav">
            <li class="right1 hidden"><a href="#four"style="padding:0px;"  onclick="nextbutton(6)" data-target="#four, #dgdfour" data-toggle="tab"><span class="glyphicon glyphicon-chevron-right" style="font-size:80%"></span></a></li>
            <li class="right2 hidden"><a href="#six" style="padding:0px"  onclick="nextbutton(7)" data-target="#six, #dgdsix" data-toggle="tab"><span class="glyphicon glyphicon-chevron-right" style="font-size:80%"></span></a></li>                                   
        </ul>
                </div>
            </div>
        </div>
        
        
        
    </th>    
  </tr>  
</table>
                 
<div class="wrapper2">
    <div class="tab-content">
        <div class="tab-pane in active" id="one" style="background: white; ">
                <table class="" style="width:100%; background-color:#337AB7;">
  <thead>
    <tr>
      
      <!-- Following headers are rotated -->
                <th class="" style="height:103px; width:1px;"><div><span></span></div></th>

        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal1"></span><asp:Textbox ID="testname1" runat="server" CssClass="nopad100 test1name" data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal2"></span><asp:Textbox ID="testname2" runat="server" CssClass="nopad100 test2name" data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal3"></span><asp:Textbox ID="testname3" runat="server" CssClass="nopad100 test3name" data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal4"></span><asp:Textbox ID="testname4" runat="server" CssClass="nopad100 test4name" data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal5"></span><asp:Textbox ID="testname5" runat="server" CssClass="nopad100 test5name" data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal6"></span><asp:Textbox ID="Textbox26" runat="server" CssClass="nopad100 test6name" data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal7"></span><asp:Textbox ID="Textbox27" runat="server" CssClass="nopad100 test7name" data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal8"></span><asp:Textbox ID="Textbox28" runat="server" CssClass="nopad100 test8name" data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal9"></span><asp:Textbox ID="Textbox29" runat="server" CssClass="nopad100 test9name" data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        
        
    </tr> 
  </thead>
  
</table>
        <table class="table3" style="width:100%">
  
                <tr class ="">
    <td class =" " style="padding:0px; width:0.001%; border:0px;"></td>
    <td class ="tdtrx headerCell2" style="padding:3px;"><asp:Textbox ID="maxS1" runat="server"  CssClass="nopad40 maxtest"  Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="maxS2" runat="server" CssClass="nopad40 maxtest"  Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="maxS3" runat="server" CssClass="nopad40 maxtest"  Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="maxS4" runat="server" CssClass="nopad40 maxtest"  Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="maxS5" runat="server" CssClass="nopad40 maxtest" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="Textbox30" runat="server" CssClass="nopad40 maxtest"  Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="Textbox32" runat="server" CssClass="nopad40 maxtest"  Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="Textbox33" runat="server" CssClass="nopad40 maxtest"  Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="Textbox34" runat="server" CssClass="nopad40 maxtest" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    
  </tr>
  
  
</table>
                                            
            </div>
        <div class="tab-pane " id="two" style="background: white;  ">
                 <table class="" style="width:100%; background-color:#337AB7;">
  <thead>
    <tr>
      
      <!-- Following headers are rotated -->
                <th class="" style="height:103px; width:1px;"><div><span></span></div></th>

        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal10"></span><asp:Textbox ID="testname10" runat="server" CssClass="nopad100 test10name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal11"></span><asp:Textbox ID="testname111" runat="server" CssClass="nopad100 test11name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal12"></span><asp:Textbox ID="testname121" runat="server" CssClass="nopad100 test12name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal13"></span><asp:Textbox ID="testname131" runat="server" CssClass="nopad100 test13name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal14"></span><asp:Textbox ID="testname141" runat="server" CssClass="nopad100 test14name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal15"></span><asp:Textbox ID="Textbox35" runat="server" CssClass="nopad100 test15name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal16"></span><asp:Textbox ID="Textbox36" runat="server" CssClass="nopad100 test16name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal17"></span><asp:Textbox ID="Textbox37" runat="server" CssClass="nopad100 test17name" data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal18"></span><asp:Textbox ID="Textbox38" runat="server" CssClass="nopad100 test18name" data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        
    </tr> 
  </thead>
  
</table>
        <table class="table3" style="width:100%">
  
                <tr class ="">
    <td class =" " style="padding:0px; width:0.001%; border:0px;"></td>
    <td class ="tdtrx headerCell2" style="padding:3px;"><asp:Textbox ID="maxS6" runat="server"  CssClass="nopad40 maxtest" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="maxS7" runat="server" CssClass="nopad40 maxtest" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="maxS8" runat="server" CssClass="nopad40 maxtest" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="maxS9" runat="server" CssClass="nopad40 maxtest" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="maxS10" runat="server" CssClass="nopad40 maxtest" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="Textbox39" runat="server" CssClass="nopad40 maxtest" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="Textbox40" runat="server" CssClass="nopad40 maxtest hid" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="Textbox65" runat="server" CssClass="nopad40 maxtest hid" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="Textbox66" runat="server" CssClass="nopad40 maxtest hid" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    
  </tr>
  
  
</table>
            </div>
        <div class="tab-pane " id="three" style="background: white;  ">
                 <table class="" style="width:100%; background-color:#337AB7;">
  <thead>
    <tr>
      
      <!-- Following headers are rotated -->
                <th class="" style="height:103px; width:1px;"><div><span></span></div></th>

        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal19"></span><asp:Textbox ID="testname11" runat="server" CssClass="nopad100 test19name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal20"></span><asp:Textbox ID="testname12" runat="server" CssClass="nopad100 test20name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal21"></span><asp:Textbox ID="testname13" runat="server" CssClass="nopad100 test21name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal22"></span><asp:Textbox ID="testname14" runat="server" CssClass="nopad100 test22name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal23"></span><asp:Textbox ID="testname15" runat="server" CssClass="nopad100 test23name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal24"></span><asp:Textbox ID="Textbox41" runat="server" CssClass="nopad100 test24name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal25"></span><asp:Textbox ID="Textbox42" runat="server" CssClass="nopad100 test25name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal26"></span><asp:Textbox ID="Textbox67" runat="server" CssClass="nopad100 test26name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal27"></span><asp:Textbox ID="Textbox68" runat="server" CssClass="nopad100 test27name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        
    </tr> 
  </thead>
  
</table>
        <table class="table3" style="width:100%">
  
                <tr class ="">
    <td class =" " style="padding:0px; width:0.001%; border:0px;"></td>
    <td class ="tdtrx headerCell2" style="padding:3px;"><asp:Textbox ID="maxS11" runat="server"  CssClass="nopad40 maxtest" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="maxS12" runat="server" CssClass="nopad40 maxtest" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="maxS13" runat="server" CssClass="nopad40 maxtest" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="maxS14" runat="server" CssClass="nopad40 maxtest" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="maxS15" runat="server" CssClass="nopad40 maxtest" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="Textbox43" runat="server" CssClass="nopad40 maxtest" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="Textbox44" runat="server" CssClass="nopad40 maxtest" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="Textbox45" runat="server" CssClass="nopad40 maxtest" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="Textbox46" runat="server" CssClass="nopad40 maxtest" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    
  </tr>
  
  
</table>
            </div>
        <div class="tab-pane" id="four" style="background: white;  ">
                 <table class="" style="width:100%; background-color:#337AB7;">
  <thead>
    <tr>
      
      <!-- Following headers are rotated -->
                <th class="" style="height:103px; width:1px;"><div><span></span></div></th>

        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal28"></span><asp:Textbox ID="testname16" runat="server" CssClass="nopad100 test28name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal29"></span><asp:Textbox ID="testname17" runat="server" CssClass="nopad100 test29name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal30"></span><asp:Textbox ID="testname18" runat="server" CssClass="nopad100 test30name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal31"></span><asp:Textbox ID="testname19" runat="server" CssClass="nopad100 test31name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal32"></span><asp:Textbox ID="testname20" runat="server" CssClass="nopad100 test32name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal33"></span><asp:Textbox ID="Textbox47" runat="server" CssClass="nopad100 test33name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal34"></span><asp:Textbox ID="Textbox48" runat="server" CssClass="nopad100 test34name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal35"></span><asp:Textbox ID="Textbox49" runat="server" CssClass="nopad100 test35name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal36"></span><asp:Textbox ID="Textbox50" runat="server" CssClass="nopad100 test36name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        
    </tr> 
  </thead>
  
</table>
        <table class="table3" style="width:100%">
  
                <tr class ="">
    <td class =" " style="padding:0px; width:0.001%; border:0px;"></td>
    <td class ="tdtrx headerCell2" style="padding:3px;"><asp:Textbox ID="maxS16" runat="server"  CssClass="nopad40 maxtest" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="maxS17" runat="server" CssClass="nopad40 maxtest" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="maxS18" runat="server" CssClass="nopad40 maxtest" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="maxS19" runat="server" CssClass="nopad40 maxtest" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="maxS20" runat="server" CssClass="nopad40 maxtest" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="Textbox69" runat="server" CssClass="nopad40 maxtest hid" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="Textbox70" runat="server" CssClass="nopad40 maxtest hid" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="Textbox71" runat="server" CssClass="nopad40 maxtest hid" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="Textbox72" runat="server" CssClass="nopad40 maxtest hid" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    
  </tr>
  
  
</table>
            </div>
        <div class="tab-pane" id="five" style="background: white;  ">
                 <table class="" style="width:100%; background-color:#337AB7;">
  <thead>
    <tr>
      
      <!-- Following headers are rotated -->
                <th class="" style="height:103px; width:1px;"><div><span></span></div></th>

        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal37"></span><asp:Textbox ID="behavior1" runat="server" CssClass="nopad100 test37name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal38"></span><asp:Textbox ID="behavior2" runat="server" CssClass="nopad100 test38name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal39"></span><asp:Textbox ID="behavior3" runat="server" CssClass="nopad100 test39name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal40"></span><asp:Textbox ID="behavior4" runat="server" CssClass="nopad100 test40name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal41"></span><asp:Textbox ID="behavior5" runat="server" CssClass="nopad100 test41name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal42"></span><asp:Textbox ID="Textbox73" runat="server" CssClass="nopad100 test42name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal43"></span><asp:Textbox ID="Textbox74" runat="server" CssClass="nopad100 test43name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal44"></span><asp:Textbox ID="Textbox75" runat="server" CssClass="nopad100 test44name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal45"></span><asp:Textbox ID="Textbox76" runat="server" CssClass="nopad100 test45name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        
    </tr> 
  </thead>
  
</table>
        <table class="table3" style="width:100%">
  
                <tr class ="">
    <td class =" " style="padding:0px; width:0.001%; border:0px;"></td>
    <td class ="tdtrx headerCell2" style="padding:3px;"><asp:Textbox ID="maxb1" runat="server"  CssClass="nopad40 maxtestb1" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="maxb2" runat="server" CssClass="nopad40 maxtestb1" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="maxb3" runat="server" CssClass="nopad40 maxtestb1" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="maxb4" runat="server" CssClass="nopad40 maxtestb1" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>    
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="maxb5" runat="server" CssClass="nopad40 maxtestb1" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>    
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="Textbox51" runat="server" CssClass="nopad40 maxtestb1" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="Textbox52" runat="server" CssClass="nopad40 maxtestb1" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="Textbox53" runat="server" CssClass="nopad40 maxtestb1" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>    
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="Textbox54" runat="server" CssClass="nopad40 maxtestb1" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>    
    
  </tr>
  
  
</table>
            </div>
        <div class="tab-pane" id="six" style="background: white;  ">
                 <table class="" style="width:100%; background-color:#337AB7;">
  <thead>
    <tr>
      
      <!-- Following headers are rotated -->
                <th class="" style="height:103px; width:1px;"><div><span></span></div></th>

        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal46"></span><asp:Textbox ID="behavior6" runat="server" CssClass="nopad100 test46name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal47"></span><asp:Textbox ID="behavior7" runat="server" CssClass="nopad100 test47name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal48"></span><asp:Textbox ID="behavior8" runat="server" CssClass="nopad100 test48name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal49"></span><asp:Textbox ID="behavior9" runat="server" CssClass="nopad100 test49name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal50"></span><asp:Textbox ID="behavior10" runat="server" CssClass="nopad100 test50name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal51"></span><asp:Textbox ID="Textbox55" runat="server" CssClass="nopad100 test51name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal52"></span><asp:Textbox ID="Textbox56" runat="server" CssClass="nopad100 test52name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal53"></span><asp:Textbox ID="Textbox57" runat="server" CssClass="nopad100 test53name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal54"></span><asp:Textbox ID="Textbox58" runat="server" CssClass="nopad100 test54name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        
    </tr> 
  </thead>
  
</table>
        <table class="table3" style="width:100%">
  
                <tr class ="">
    <td class =" " style="padding:0px; width:0.001%; border:0px;"></td>
    <td class ="tdtrx headerCell2" style="padding:3px;"><asp:Textbox ID="maxb6" runat="server"  CssClass="nopad40 maxtestb1" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="maxb7" runat="server" CssClass="nopad40 maxtestb1" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="maxb8" runat="server" CssClass="nopad40 maxtestb1" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="Textbox78" runat="server" CssClass="nopad40 maxtestb1 hid" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="Textbox79" runat="server" CssClass="nopad40 maxtestb1 hid" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="Textbox81" runat="server" CssClass="nopad40 maxtestb1 hid" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="Textbox82" runat="server" CssClass="nopad40 maxtestb1 hid" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="Textbox77" runat="server" CssClass="nopad40 maxtestb1 hid" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="Textbox80" runat="server" CssClass="nopad40 maxtestb1 hid" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    
    
  </tr>
  
  
</table>
            </div>
        <div class="tab-pane" id="seven" style="background: white;  ">
                 <table class="" style="width:100%; background-color:#337AB7;">
  <thead>
    <tr>
      
      <!-- Following headers are rotated -->
                <th class="" style="height:103px; width:1px;"><div><span></span></div></th>

        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal55"></span><asp:Textbox ID="cwname1" runat="server" CssClass="nopad100 test55name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal56"></span><asp:Textbox ID="cwname2" runat="server" CssClass="nopad100 test56name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal57"></span><asp:Textbox ID="cwname3" runat="server" CssClass="nopad100 test57name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal58"></span><asp:Textbox ID="Textbox83" runat="server" CssClass="nopad100 test58name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal59"></span><asp:Textbox ID="Textbox84" runat="server" CssClass="nopad100 test59name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal60"></span><asp:Textbox ID="Textbox85" runat="server" CssClass="nopad100 test60name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal61"></span><asp:Textbox ID="Textbox86" runat="server" CssClass="nopad100 test61name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal62"></span><asp:Textbox ID="Textbox87" runat="server" CssClass="nopad100 test62name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal63"></span><asp:Textbox ID="Textbox88" runat="server" CssClass="nopad100 test63name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        
        
    </tr> 
  </thead>
  
</table>
        <table class="table3" style="width:100%">
  
                <tr class ="">
    <td class =" " style="padding:0px; width:0.001%; border:0px;"></td>
    <td class ="tdtrx headerCell2" style="padding:3px;"><asp:Textbox ID="maxCW1" runat="server"  CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="maxCW2" runat="server" CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="maxCW3" runat="server" CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="maxCW4" runat="server" CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>    
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="maxCW5" runat="server" CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>    
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="Textbox59" runat="server" CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="Textbox60" runat="server" CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>    
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="Textbox61" runat="server" CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>    
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="Textbox89" runat="server" CssClass="nopad40 maxtestcw hid" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>    
    
  </tr>
  
  
</table>
            </div>
        <div class="tab-pane" id="eight" style="background: white;  ">
                 <table class="" style="width:100%; background-color:#337AB7;">
  <thead>
    <tr>
      
      <!-- Following headers are rotated -->
                <th class="" style="height:103px; width:1px;"><div><span></span></div></th>

        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal64"></span><asp:Textbox ID="cwname6" runat="server" CssClass="nopad100 test64name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal65"></span><asp:Textbox ID="cwname7" runat="server" CssClass="nopad100 test65name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal66"></span><asp:Textbox ID="cwname8" runat="server" CssClass="nopad100 test66name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal67"></span><asp:Textbox ID="cwname9" runat="server" CssClass="nopad100 test67name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal68"></span><asp:Textbox ID="cwname10" runat="server" CssClass="nopad100 test68name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal69"></span><asp:Textbox ID="Textbox62" runat="server" CssClass="nopad100 test69name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal70"></span><asp:Textbox ID="Textbox63" runat="server" CssClass="nopad100 test70name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal71"></span><asp:Textbox ID="Textbox64" runat="server" CssClass="nopad100 test71name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        <th class="rotate tdtr5" style=""><div><span class="" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal72"></span><asp:Textbox ID="Textbox90" runat="server" CssClass="nopad100 test72name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        
    </tr> 
  </thead>
  
</table>
        <table class="table3" style="width:100%">
  
                <tr class ="">
    <td class =" " style="padding:0px; width:0.001%; border:0px;"></td>
    <td class ="tdtrx headerCell2" style="padding:3px;"><asp:Textbox ID="maxCW6" runat="server"  CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="maxCW7" runat="server" CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="maxCW8" runat="server" CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="maxCW9" runat="server" CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>    
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="maxCW10" runat="server" CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>    
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="Textbox91" runat="server" CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="Textbox92" runat="server" CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="Textbox93" runat="server" CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>    
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="Textbox94" runat="server" CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>    
    
  </tr>
  
  
</table>
            </div>
       
    </div>
</div>
                 
                    
                 </div>
            <div class="col-xs-3" style="padding:0px;">
                    <table class="table3 table_legenda" style="width:100%">
  <tr class="notdtr">
    <th  class="notdtr headerCell " style="width:0.001%; padding:0px;" ></th>
    <th rowspan="3" class="tdtr headerCell" style="width:50%; padding:0px;" ><asp:Label ID="tablescore100" cssclass="bold" runat="server" > </asp:Label></th>
    <th rowspan="3" style="border-right:1px;" class="tdtr headerCell "><asp:Label ID="tablegrade" cssclass="bold" runat="server" > </asp:Label></th> 
   
  </tr>
  <tr class ="notdtr">
   
  <td class ="notdtr headerCell" style="border-right:0px;"> </td>
    
  </tr>

                <tr class ="tdtr22">
    <td class ="notdtr headerCell" style="border-right:1px;"> </td>
  </tr>
  
     
</table>
                </div>
            </div>

        <div class="full-card " style=" overflow-x:hidden;overflow-y:scroll; width:101.6%; max-height:750px; padding-left:1px; padding-right:1px; padding-top:0px;">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            
            <div class="col-xs-12" style="padding:0px;">
                <div class="col-xs-3" style="padding:0px;">

                    <asp:GridView ID="dgd2" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" AllowCustomPaging="False"
                          GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                         ShowHeader="false" 
                          Font-Strikeout="False" Font-Underline="False" PageSize="99" CssClass="cen"> 
                             
                <Columns>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen borderline2 w40" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:Label  ID="txtnum" runat="server" Width="40px"  CssClass="centertext" Text='<%# Eval("number") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 

                    <asp:TemplateField ItemStyle-CssClass="quizsum hidden cen" HeaderStyle-CssClass="hidden"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:Label  ID="Label1" runat="server" Width="0px"  CssClass="centertext" Text='<%# Eval("number") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     
                    <asp:TemplateField ItemStyle-CssClass="name cen borderline" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:Label  ID="txtname" runat="server" Width="600px" Height=""  CssClass="name" Text='<%# Eval("sName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField ItemStyle-CssClass="centertext cen hid" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="20%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox16"   runat="server" Width="80%" Height="42.5px"  CssClass="" Text='<%# Eval("number") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                             Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />
               
            </asp:GridView>
                </div>
                
                <div class="col-xs-6" style="padding:0px; background-color:white;" >
                    <table class="table3" style="width:100%; height:100%; padding:0px;">
  
  
</table>
                    <div class="wrapper2">
    <div class="div2">
        
                    <div class="tab-content">
        <div class="tab-pane in active" id="dgdone" style="background: white; ">
                <asp:GridView ID="dgd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="0" AllowCustomPaging="False"
                          GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                         ShowHeader="false"
                          Font-Strikeout="False" Font-Underline="False" PageSize="99" CssClass="cen"> 
                             
                <Columns>
                    
                    
                    <asp:TemplateField ItemStyle-CssClass="cen" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle ></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade1_1" tabindex="1" onfocus='<%# "focusbox(0," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBox" Text='<%# Eval("txtg1") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="cen" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle ></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade1_2" tabindex="2" onfocus='<%# "focusbox(1," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBox" Text='<%# Eval("txtg2") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="cen" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle ></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade1_3" tabindex="3" onfocus='<%# "focusbox(2," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBox" Text='<%# Eval("txtg3") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="cen" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle ></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade1_4" tabindex="4" onfocus='<%# "focusbox(3," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBox" Text='<%# Eval("txtg4") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="cen" HeaderStyle-CssClass="centertext"  HeaderText="5"> 
                        <HeaderStyle ></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade1_5" tabindex="5" onfocus='<%# "focusbox(4," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBox" Text='<%# Eval("txtg5") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField ItemStyle-CssClass="cen" HeaderStyle-CssClass="centertext"  HeaderText="5"> 
                        <HeaderStyle ></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade" tabindex="5" onfocus='<%# "focusbox(4," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBox" Text='<%# Eval("txtg5") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="cen" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle ></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade7" tabindex="7" onfocus='<%# "focusbox(6," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBox" Text='<%# Eval("txtg7") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="cen" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle ></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade8" tabindex="8" onfocus='<%# "focusbox(7," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBox" Text='<%# Eval("txtg8") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="cen" HeaderStyle-CssClass="centertext"  HeaderText="5"> 
                        <HeaderStyle ></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade9" tabindex="9" onfocus='<%# "focusbox(8," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBox" Text='<%# Eval("txtg9") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField ItemStyle-CssClass="hid2" HeaderStyle-CssClass="centertext"  HeaderText="20"> 
                        <HeaderStyle Width="1%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="oneSID" CssClass="siduser" runat="server" Width="40px" Text='<%# Eval("sID") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     
                     
                </Columns>

                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                             Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />
               
            </asp:GridView>
                                            
            </div>
        <div class="tab-pane " id="dgdtwo" style="background: white;  ">
                 <asp:GridView ID="dgdp2" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="0" AllowCustomPaging="False"
                          GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                         ShowHeader="false"
                          Font-Strikeout="False" Font-Underline="False" PageSize="99" CssClass="cen"> 
                             
                <Columns>
                    
                    
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="score21" tabindex="6" onfocus='<%# "focusbox(5," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg2" Text='<%# Eval("txtg6") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="score22" tabindex="7" onfocus='<%# "focusbox(6," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg2" Text='<%# Eval("txtg7") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="score23" tabindex="8" onfocus='<%# "focusbox(7," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg2" Text='<%# Eval("txtg8") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="scoer24" tabindex="9" onfocus='<%# "focusbox(8," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg2" Text='<%# Eval("txtg9") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="5"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="scoer25" tabindex="10" onfocus='<%# "focusbox(9," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg2" Text='<%# Eval("txtg10") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox24" tabindex="7" onfocus='<%# "focusbox(6," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg2" Text='<%# Eval("txtg7") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox95" tabindex="8" onfocus='<%# "focusbox(7," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg2 hid" Text='<%# Eval("txtg8") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox96" tabindex="9" onfocus='<%# "focusbox(8," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg2 hid" Text='<%# Eval("txtg9") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="5"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox97" tabindex="10" onfocus='<%# "focusbox(9," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg2 hid" Text='<%# Eval("txtg10") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField ItemStyle-CssClass="hid2" HeaderStyle-CssClass="centertext"  HeaderText="20"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="twoSID" runat="server" Width="40px" Text='<%# Eval("sID") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     
                     
                </Columns>

                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                             Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />
               
            </asp:GridView>
            </div>
        <div class="tab-pane " id="dgdthree" style="background: white;  ">
                 <asp:GridView ID="dgdp3" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="0" AllowCustomPaging="False"
                          GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                         ShowHeader="false"
                          Font-Strikeout="False" Font-Underline="False" PageSize="99" CssClass="cen"> 
                             
                <Columns>
                    
                    
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade11" tabindex="11" onfocus='<%# "focusbox(10," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg3" Text='<%# Eval("txtg11") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade12" tabindex="12" onfocus='<%# "focusbox(11," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg3" Text='<%# Eval("txtg12") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade13" tabindex="13" onfocus='<%# "focusbox(12," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg3" Text='<%# Eval("txtg13") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade14" tabindex="14" onfocus='<%# "focusbox(13," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg3" Text='<%# Eval("txtg14") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="5"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade15" tabindex="15" onfocus='<%# "focusbox(14," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg3" Text='<%# Eval("txtg15") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox98" tabindex="12" onfocus='<%# "focusbox(11," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg3" Text='<%# Eval("txtg12") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox99" tabindex="13" onfocus='<%# "focusbox(12," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg3" Text='<%# Eval("txtg13") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox100" tabindex="14" onfocus='<%# "focusbox(13," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg3" Text='<%# Eval("txtg14") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="5"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox101" tabindex="15" onfocus='<%# "focusbox(14," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg3" Text='<%# Eval("txtg15") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField ItemStyle-CssClass="hid2" HeaderStyle-CssClass="centertext"  HeaderText="20"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="threeSID" runat="server" Width="40px" Text='<%# Eval("sID") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     
                     
                </Columns>

                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                             Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />
               
            </asp:GridView>
            </div>
        <div class="tab-pane" id="dgdfour" style="background: white;  ">
                 <asp:GridView ID="dgdp4" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="0" AllowCustomPaging="False"
                          GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                         ShowHeader="false"
                          Font-Strikeout="False" Font-Underline="False" PageSize="99" CssClass="cen"> 
                             
                <Columns>
                    
                    
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade16" tabindex="16" onfocus='<%# "focusbox(15," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg4" Text='<%# Eval("txtg16") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade17" tabindex="17" onfocus='<%# "focusbox(16," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg4" Text='<%# Eval("txtg17") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade18" tabindex="18" onfocus='<%# "focusbox(17," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg4" Text='<%# Eval("txtg18") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade19" tabindex="19" onfocus='<%# "focusbox(18," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg4" Text='<%# Eval("txtg19") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="5"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade20" tabindex="20" onfocus='<%# "focusbox(19," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg4" Text='<%# Eval("txtg20") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox102" tabindex="17" onfocus='<%# "focusbox(16," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg4 hid" Text='<%# Eval("txtg17") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox103" tabindex="18" onfocus='<%# "focusbox(17," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg4 hid" Text='<%# Eval("txtg18") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox104" tabindex="19" onfocus='<%# "focusbox(18," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg4 hid" Text='<%# Eval("txtg19") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="5"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox105" tabindex="20" onfocus='<%# "focusbox(19," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg4 hid" Text='<%# Eval("txtg20") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField ItemStyle-CssClass="hid2" HeaderStyle-CssClass="centertext"  HeaderText="20"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="fourSID" runat="server" Width="40px" Text='<%# Eval("sID") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     
                     
                </Columns>

                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                             Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />
               
            </asp:GridView>
            </div>
                        <div class="tab-pane" id="dgdfive" style="background: white;  ">
                 <asp:GridView ID="dgdp5" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="0" AllowCustomPaging="False"
                          GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                         ShowHeader="false"
                          Font-Strikeout="False" Font-Underline="False" PageSize="99" CssClass="cen"> 
                             
                <Columns>
                    
                    
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="behave1" onfocus='<%# "focusbox(20," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt1 behavepage1" Text='<%# Eval("txtb1") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="behave2" onfocus='<%# "focusbox(21," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt2 behavepage1" Text='<%# Eval("txtb2") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="behave3" onfocus='<%# "focusbox(22," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt3 behavepage1" Text='<%# Eval("txtb3") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="behave4" onfocus='<%# "focusbox(23," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt4 behavepage1" Text='<%# Eval("txtb4") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="behave5" onfocus='<%# "focusbox(24," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt5 behavepage1" Text='<%# Eval("txtb5") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox106" onfocus='<%# "focusbox(21," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt2 behavepage1" Text='<%# Eval("txtb2") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox107" onfocus='<%# "focusbox(22," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt3 behavepage1" Text='<%# Eval("txtb3") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox108" onfocus='<%# "focusbox(23," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt4 behavepage1" Text='<%# Eval("txtb4") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox109" onfocus='<%# "focusbox(24," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt5 behavepage1" Text='<%# Eval("txtb5") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField ItemStyle-CssClass="hid2" HeaderStyle-CssClass="centertext"  HeaderText="20"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox8" runat="server" Width="40px" Text='<%# Eval("sID") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     
                     
                </Columns>

                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                             Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />
               
            </asp:GridView>
            </div>
                        <div class="tab-pane" id="dgdsix" style="background: white;  ">
                 <asp:GridView ID="dgdp6" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="0" AllowCustomPaging="False"
                          GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                         ShowHeader="false"
                          Font-Strikeout="False" Font-Underline="False" PageSize="99" CssClass="cen"> 
                             
                <Columns>
                    
                    
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="behave6" onfocus='<%# "focusbox(25," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt6 behavepage2" Text='<%# Eval("txtb6") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="behave7" onfocus='<%# "focusbox(26," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt7 behavepage2" Text='<%# Eval("txtb7") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="behave8" onfocus='<%# "focusbox(27," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt8 behavepage2" Text='<%# Eval("txtb8") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="behave9" onfocus='<%# "focusbox(28," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt9 behavepage2 hid" Text='<%# Eval("txtb9") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="behave10" onfocus='<%# "focusbox(29," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt10 behavepage2 hid" Text='<%# Eval("txtb10") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox110" onfocus='<%# "focusbox(26," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt7 behavepage2 hid" Text='<%# Eval("txtb7") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox111" onfocus='<%# "focusbox(27," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt8 behavepage2 hid" Text='<%# Eval("txtb8") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox112" onfocus='<%# "focusbox(28," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt9 behavepage2 hid" Text='<%# Eval("txtb9") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox113" onfocus='<%# "focusbox(29," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt10 behavepage2 hid" Text='<%# Eval("txtb10") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField ItemStyle-CssClass="hid2" HeaderStyle-CssClass="centertext"  HeaderText="20"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox14" runat="server" Width="40px" Text='<%# Eval("sID") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     
                     
                </Columns>

                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                             Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />
               
            </asp:GridView>
            </div>
                       
                <div class="tab-pane" id="dgdseven" style="background: white;  ">
                 <asp:GridView ID="dgdp7" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="0" AllowCustomPaging="False"
                          GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                         ShowHeader="false"
                          Font-Strikeout="False" Font-Underline="False" PageSize="99" CssClass="cen"> 
                             
                <Columns>
                    
                    
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="chewat1" onfocus='<%# "focusbox(30," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut1" Text='<%# Eval("txtchewut1") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="chewat2" onfocus='<%# "focusbox(31," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut1" Text='<%# Eval("txtchewut2") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="chewat3" onfocus='<%# "focusbox(32," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut1" Text='<%# Eval("txtchewut3") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="chewat4" onfocus='<%# "focusbox(33," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut1" Text='<%# Eval("txtchewut4") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="chewat5" onfocus='<%# "focusbox(34," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut1" Text='<%# Eval("txtchewut5") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox114" onfocus='<%# "focusbox(31," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut1" Text='<%# Eval("txtchewut2") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox115" onfocus='<%# "focusbox(32," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut1" Text='<%# Eval("txtchewut3") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox116" onfocus='<%# "focusbox(33," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut1" Text='<%# Eval("txtchewut4") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox117" onfocus='<%# "focusbox(34," +Eval("number") + " );" %>' runat="server" Width="38" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut1 hid" Text='<%# Eval("txtchewut5") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                   
                    <asp:TemplateField ItemStyle-CssClass="hid2" HeaderStyle-CssClass="centertext"  HeaderText="20"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox10" runat="server" Width="40px" Text='<%# Eval("sID") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     
                     
                </Columns>

                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                             Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />
               
            </asp:GridView>
            </div>
                       
    </div>
                   
                    
                </div>

                    </div>

    </div>
                <div class="col-xs-3" style="padding:0px;">
                    
                    <asp:GridView ID="dgd3" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" AllowCustomPaging="False"
                          GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                         ShowHeader="false"
                          Font-Strikeout="False" Font-Underline="False" PageSize="99" CssClass="cen"> 
                             
                <Columns>
                    
                    <asp:TemplateField ItemStyle-CssClass="centertext cen4 cen" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132235") %>">  
                        <HeaderStyle Width="50%"></HeaderStyle>                                             
                        <ItemTemplate>
                                      
                                            <asp:TextBox  ID="txtTotalScore" runat="server" Width="80%" Height="42.5px" CssClass="gradetxt disable" Text="" ></asp:TextBox>                                                               
                                                               
                               
                                </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen4 editgrade cen" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206414") %>">  
                        <HeaderStyle Width="50%"></HeaderStyle>                                             
                        <ItemTemplate>
                                        
                                            <asp:TextBox  ID="txtTotalGrade" runat="server" Width="80%" Height="42.5px" CssClass="gradetxt disable gradetxt2" Text="" ></asp:TextBox>                                                               
                                                                
                               
                                </ItemTemplate>
                    </asp:TemplateField>
                    
                    
                    
                    <asp:TemplateField ItemStyle-CssClass="hid2" HeaderStyle-CssClass="centertext"  HeaderText="20"> 
                        <HeaderStyle Width="1%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="sID" runat="server" Width="40px" Text='<%# Eval("sID") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     
                     
                </Columns>

                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                             Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />
               
            </asp:GridView>
                </div>
                </div>
            
            </div>
            <div class="row text-center planadd-row">
                <br/>

                <div class="col-xs-12 button-segment">
                    <input type="button" id="btnSearch2" onclick="nextpage(1)" class="btn btn-primary search-btn hidden btnok" style="width:200px;"  />
                    <input type="button" id="btnSearch1" onclick="nextpage(0)" class="btn btn-primary search-btn btnerror" style="width:200px"  />
                    <asp:Button CssClass="btn btn-primary global-btn hidden" ID="btnSave" runat="server" Style="width: 200px;"
                                Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132205") %>" />
                    <asp:Button CssClass="btn btn-warning global-btn btnback" ID="btnCancle" runat="server" Style="width: 100px;"
                                />
                    
                </div>
            </div>

       
        
        </div>
            
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
