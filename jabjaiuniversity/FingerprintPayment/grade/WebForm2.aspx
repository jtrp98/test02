<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="FingerprintPayment.grade.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="full-card planelist-container" style="width: 100%; padding:0px;">
         
        <link rel="stylesheet" href="//min.gitcdn.xyz/repo/wintercounter/Protip/master/protip.min.css">
        <script src="//min.gitcdn.xyz/repo/wintercounter/Protip/master/protip.min.js"></script>
        <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
<script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>
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
#loading2 {
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
.rotate3 {
  /* FF3.5+ */
  -moz-transform: rotate(-90.0deg);
  /* Opera 10.5 */
  -o-transform: rotate(-90.0deg);
  /* Saf3.1+, Chrome */
  -webkit-transform: rotate(-90.0deg);
  
  /* Standard */
  transform: rotate(-90.0deg);
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
                padding-left:0px;
                padding-right:0px;
                padding-top:3.5px;
                padding-bottom:3.5px;
                border:1px solid white;
            }
            .cen2{
                
                padding-top:3.5px;
                padding-bottom:3.5px;   
                width:9.5%;             
            }
            .cen2alt{                
                padding-top:3.5px;
                padding-bottom:3.5px;   
                width:9.5%;             
            }
            .cen3{
                
                padding-top:3.5px;
                padding-bottom:3.5px;  
                width:12%;
            }
            .cen4{
                padding-left:0px;
                width:15%;
                           
            }
            .cen5{
                padding-left:0px;
                padding-right:5px;
                width:12%;                           
            }
            .cen4alt{
                padding-left:0px;
                padding-right:5px;
                width:15%;
                           
            }
            .wid50{
                width:50px !important;
            }
            .wid1{
                width:39px !important;
                margin-right:2px !important;
            }
            .wid2{
                width:39px !important;
            }
            .wid3{
                width:39px !important;
            }
            .wid4{
                width:39px !important;
            }
            .wid5{
                width:39px !important;
            }
            .wid6{
                width:39px !important;
            }
            .wid39{
                width:39px !important;
            }
            
            .head1{
                width:55.8px !important;
            }
            .head2{
                width:55.8px !important;
                padding-left:3px !important;
            }
            .head3{
                width:55.8px !important;
                padding-left:4px !important;
            }
            .head4{
                width:55.8px !important;
            }
            .head5{
                width:55.8px !important;
                
            }
            .head6{
                width:55.8px !important;
            }
            .head7{
                width:55.8px !important;
                
            }
            .head8{
                width:55.8px !important;
               padding-left:3px !important;
            }
            .head9{
                width:56px !important;
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
            .hidden2{
                display:none !important;
            }
            .hidden3{
                display:none !important;
            }
            .centertext{
               
            }
            .tdtrx {
    border: 1px solid #000000;
    border-left:none;
    text-align: center;
    padding: 0px;
}
            .rmargin2{
                margin-right:2.5px !important;
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
             #upload { display: none; }
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
            .disable2{
                pointer-events: none;                
            }
            .disable3{
                border:none !important;               
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

.headerCell4 {
        background-color: #337AB7;
        color: #000000;
    }
.wrapper1 {  overflow-x: scroll; overflow-y: hidden; }
.wrapper2 {  overflow-x: hidden; overflow-y: hidden; }
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
<script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.8.0/jszip.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.8.0/xlsx.js"></script>



        <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.css" />

    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.js"></script>

    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/buttons/1.5.2/js/dataTables.buttons.min.js"></script>

    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/select/1.2.6/js/dataTables.select.min.js"></script>

    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/responsive/2.2.3/css/responsive.dataTables.min.css" />

    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/responsive/2.2.3/js/dataTables.responsive.js"></script>

        <script type="text/javascript" charset="utf8" src="https://cdn.rawgit.com/ashl1/datatables-rowsgroup/fbd569b8768155c7a9a62568e66a64115887d7d0/dataTables.rowsGroup.js"></script>

    <%--fontawesome icon--%>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.1/css/all.css" integrity="sha384-50oBUHEmvpQ+1lW4y57PTFmhCaXp0ML5d60M1M7uH2+nqUivzIebhndOJK28anvf" crossorigin="anonymous" />

        <script src="setGroupExam.js"></script>

<script>
    function hasClass(element, cls) {
        return (' ' + element.className + ' ').indexOf(' ' + cls + ' ') > -1;
    }

    function autosave(target, score, sid) {
        var autosavedata = document.getElementsByClassName("autosavedata");
        var autotext = document.getElementsByClassName("autotext");
        var gradeid = autosavedata[0].value;        
        var target1 = target.split('_');
        
        //alert("/App_Logic/gradeAutosave.ashx?gradeid=" + gradeid + "&target=" + target1[4] + "&sid=" + sid + "&score=" + score);
        if(gradeid != "")
        {
            $.ajax({
                url: "/App_Logic/gradeAutosave.ashx?gradeid="+gradeid+ "&target=" + target1[4] + "&sid=" + sid + "&score=" +score,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
            });
            var d = new Date();
            var n = d.getHours();
            var n2 = d.getMinutes();
            n2 = (d.getMinutes()<10?'0':'') + d.getMinutes();
            n = (d.getHours()<10?'0':'') + d.getHours();

            var target2 = "";
            
            switch (target1[4]) {
                case "txtGrade1":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 1";
                    break;
                case "txtGrade2":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 2";
                    break;
                case "txtGrade3":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 3";
                    break;
                case "txtGrade4":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 4";
                    break;
                case "txtGrade5":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 5";
                    break;
                case "txtGrade6":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 6";
                    break;
                case "txtGrade7":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 7";
                    break;
                case "txtGrade8":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 8";
                    break;
                case "txtGrade9":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 9";
                    break;
                case "txtGrade10":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 10";
                    break;
                case "txtGrade11":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 11";
                    break;
                case "txtGrade12":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 12";
                    break;
                case "txtGrade13":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 13";
                    break;
                case "txtGrade14":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 14";
                    break;
                case "txtGrade15":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 15";
                    break;
                case "txtGrade16":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 16";
                    break;
                case "txtGrade17":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 17";
                    break;
                case "txtGrade18":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 18";
                    break;
                case "txtGrade19":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 19";
                    break;
                case "txtGrade20":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 20";
                    break;
                case "chewat1":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 21";
                    break;
                case "chewat2":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 22";
                    break;
                case "chewat3":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 23";
                    break;
                case "chewat4":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 24";
                    break;
                case "chewat5":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 25";
                    break;
                case "chewat6":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 26";
                    break;
                case "chewat7":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 27";
                    break;
                case "chewat8":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 28";
                    break;
                case "chewat9":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 29";
                    break;
                case "chewat10":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 30";
                    break;
                case "chewat11":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 31";
                    break;
                case "chewat12":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 32";
                    break;
                case "chewat13":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 33";
                    break;
                case "chewat14":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 34";
                    break;
                case "chewat15":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 35";
                    break;
                case "chewat16":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 36";
                    break;
                case "chewat17":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 37";
                    break;
                case "chewat18":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 38";
                    break;
                case "chewat19":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 39";
                    break;
                case "chewat20":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %> 40";
                    break;
                case "midscore1":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00306") %> 1";
                    break;
                case "midscore2":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00306") %> 2";
                    break;
                case "midscore3":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00306") %> 3";
                    break;
                case "midscore4":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00306") %> 4";
                    break;
                case "midscore5":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00306") %> 5";
                    break;
                case "midscore6":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00306") %> 6";
                    break;
                case "midscore7":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00306") %> 7";
                    break;
                case "midscore8":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00306") %> 8";
                    break;
                case "midscore9":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00306") %> 9";
                    break;
                case "midscore10":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00306") %> 10";
                    break;
                case "finalscore1":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00320") %> 1";
                    break;
                case "finalscore2":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00320") %> 2";
                    break;
                case "finalscore3":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00320") %> 3";
                    break;
                case "finalscore4":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00320") %> 4";
                    break;
                case "finalscore5":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00320") %> 5";
                    break;
                case "finalscore6":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00320") %> 6";
                    break;
                case "finalscore7":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00320") %> 7";
                    break;
                case "finalscore8":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00320") %> 8";
                    break;
                case "finalscore9":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00320") %> 9";
                    break;
                case "finalscore10":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00320") %> 10";
                    break;
                case "behave1":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132253") %> 1";
                    break;
                case "behave2":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132253") %> 2";
                    break;
                case "behave3":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132253") %> 3";
                    break;
                case "behave4":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132253") %> 4";
                    break;
                case "behave5":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132253") %> 5";
                    break;
                case "behave6":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132253") %> 6";
                    break;
                case "behave7":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132253") %> 7";
                    break;
                case "behave8":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132253") %> 8";
                    break;
                case "behave9":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132253") %> 9";
                    break;
                case "behave10":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132253") %> 10";
                    break;
                case "txtMidScore":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00306") %>";
                    break;
                case "txtLateScore":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132254") %>";
                    break;
                case "txtGoodBehavior":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132253") %>";
                    break;
                case "txtGoodReading":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132255") %>";
                    break;
                case "txtSamattana":
                    target = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132256") %>";
                    break;
            }

            autotext[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132257") %>" + target + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132258") %> " + n + ":" + n2;
        }
    }

    var ExcelToJSON = function() {
        var maxtest = document.getElementsByClassName("maxtest");
        var maxtestcw = document.getElementsByClassName("maxtestcw");
        var maxtestb1 = document.getElementsByClassName("maxtestb1");
        var maxmidcw = document.getElementsByClassName("maxmidcw");
        var maxfinalcw = document.getElementsByClassName("maxfinalcw");
        var setnameb1 = document.getElementsByClassName("setnameb1");
        var setnameb2 = document.getElementsByClassName("setnameb2");
        var setnameb3 = document.getElementsByClassName("setnameb3");
        var setnameb4 = document.getElementsByClassName("setnameb4");
        var setnameb5 = document.getElementsByClassName("setnameb5");
        var setnameb6 = document.getElementsByClassName("setnameb6");
        var setnameb7 = document.getElementsByClassName("setnameb7");
        var setnameb8 = document.getElementsByClassName("setnameb8");
        var setnameb9 = document.getElementsByClassName("setnameb9");
        var setnameb10 = document.getElementsByClassName("setnameb10");
        var set1name = document.getElementsByClassName("set1name");
        var set2name = document.getElementsByClassName("set2name");
        var set3name = document.getElementsByClassName("set3name");
        var set4name = document.getElementsByClassName("set4name");
        var set5name = document.getElementsByClassName("set5name");
        var set6name = document.getElementsByClassName("set6name");
        var set7name = document.getElementsByClassName("set7name");
        var set8name = document.getElementsByClassName("set8name");
        var set9name = document.getElementsByClassName("set9name");
        var set10name = document.getElementsByClassName("set10name");
        var set11name = document.getElementsByClassName("set11name");
        var set12name = document.getElementsByClassName("set12name");
        var set13name = document.getElementsByClassName("set13name");
        var set14name = document.getElementsByClassName("set14name");
        var set15name = document.getElementsByClassName("set15name");
        var set16name = document.getElementsByClassName("set16name");
        var set17name = document.getElementsByClassName("set17name");
        var set18name = document.getElementsByClassName("set18name");
        var set19name = document.getElementsByClassName("set19name");
        var set20name = document.getElementsByClassName("set20name");
        var set1namecw = document.getElementsByClassName("set1namecw");
        var set2namecw = document.getElementsByClassName("set2namecw");
        var set3namecw = document.getElementsByClassName("set3namecw");
        var set4namecw = document.getElementsByClassName("set4namecw");
        var set5namecw = document.getElementsByClassName("set5namecw");
        var set6namecw = document.getElementsByClassName("set6namecw");
        var set7namecw = document.getElementsByClassName("set7namecw");
        var set8namecw = document.getElementsByClassName("set8namecw");
        var set9namecw = document.getElementsByClassName("set9namecw");
        var set10namecw = document.getElementsByClassName("set10namecw");
        var set11namecw = document.getElementsByClassName("set11namecw");
        var set12namecw = document.getElementsByClassName("set12namecw");
        var set13namecw = document.getElementsByClassName("set13namecw");
        var set14namecw = document.getElementsByClassName("set14namecw");
        var set15namecw = document.getElementsByClassName("set15namecw");
        var set16namecw = document.getElementsByClassName("set16namecw");
        var set17namecw = document.getElementsByClassName("set17namecw");
        var set18namecw = document.getElementsByClassName("set18namecw");
        var set19namecw = document.getElementsByClassName("set19namecw");
        var set20namecw = document.getElementsByClassName("set20namecw");
        var set1midcw = document.getElementsByClassName("set1midcw");
        var set2midcw = document.getElementsByClassName("set2midcw");
        var set3midcw = document.getElementsByClassName("set3midcw");
        var set4midcw = document.getElementsByClassName("set4midcw");
        var set5midcw = document.getElementsByClassName("set5midcw");
        var set6midcw = document.getElementsByClassName("set6midcw");
        var set7midcw = document.getElementsByClassName("set7midcw");
        var set8midcw = document.getElementsByClassName("set8midcw");
        var set9midcw = document.getElementsByClassName("set9midcw");
        var set10midcw = document.getElementsByClassName("set10midcw");
        var set1finalcw = document.getElementsByClassName("set1finalcw");
        var set2finalcw = document.getElementsByClassName("set2finalcw");
        var set3finalcw = document.getElementsByClassName("set3finalcw");
        var set4finalcw = document.getElementsByClassName("set4finalcw");
        var set5finalcw = document.getElementsByClassName("set5finalcw");
        var set6finalcw = document.getElementsByClassName("set6finalcw");
        var set7finalcw = document.getElementsByClassName("set7finalcw");
        var set8finalcw = document.getElementsByClassName("set8finalcw");
        var set9finalcw = document.getElementsByClassName("set9finalcw");
        var set10finalcw = document.getElementsByClassName("set10finalcw");
        var maxmidscore = document.getElementsByClassName("maxmidscore");
        var maxlatescore = document.getElementsByClassName("maxlatescore");

        var stdscore1 = document.getElementsByClassName("lockg1");
        var stdscore2 = document.getElementsByClassName("lockg2");
        var stdscore3 = document.getElementsByClassName("lockg3");
        var stdscore4 = document.getElementsByClassName("lockg4");
        var stdscore5 = document.getElementsByClassName("lockg5");
        var stdscore6 = document.getElementsByClassName("lockg6");
        var stdscore7 = document.getElementsByClassName("lockg7");
        var stdscore8 = document.getElementsByClassName("lockg8");
        var stdscore9 = document.getElementsByClassName("lockg9");
        var stdscore10 = document.getElementsByClassName("lockg10");
        var stdscore11 = document.getElementsByClassName("lockg11");
        var stdscore12 = document.getElementsByClassName("lockg12");
        var stdscore13 = document.getElementsByClassName("lockg13");
        var stdscore14 = document.getElementsByClassName("lockg14");
        var stdscore15 = document.getElementsByClassName("lockg15");
        var stdscore16 = document.getElementsByClassName("lockg16");
        var stdscore17 = document.getElementsByClassName("lockg17");
        var stdscore18 = document.getElementsByClassName("lockg18");
        var stdscore19 = document.getElementsByClassName("lockg19");
        var stdscore20 = document.getElementsByClassName("lockg20");

        var stdchewat1 = document.getElementsByClassName("lockcw1");
        var stdchewat2 = document.getElementsByClassName("lockcw2");
        var stdchewat3 = document.getElementsByClassName("lockcw3");
        var stdchewat4 = document.getElementsByClassName("lockcw4");
        var stdchewat5 = document.getElementsByClassName("lockcw5");
        var stdchewat6 = document.getElementsByClassName("lockcw6");
        var stdchewat7 = document.getElementsByClassName("lockcw7");
        var stdchewat8 = document.getElementsByClassName("lockcw8");
        var stdchewat9 = document.getElementsByClassName("lockcw9");
        var stdchewat10 = document.getElementsByClassName("lockcw10");
        var stdchewat11 = document.getElementsByClassName("lockcw11");
        var stdchewat12 = document.getElementsByClassName("lockcw12");
        var stdchewat13 = document.getElementsByClassName("lockcw13");
        var stdchewat14 = document.getElementsByClassName("lockcw14");
        var stdchewat15 = document.getElementsByClassName("lockcw15");
        var stdchewat16 = document.getElementsByClassName("lockcw16");
        var stdchewat17 = document.getElementsByClassName("lockcw17");
        var stdchewat18 = document.getElementsByClassName("lockcw18");
        var stdchewat19 = document.getElementsByClassName("lockcw19");
        var stdchewat20 = document.getElementsByClassName("lockcw20");

        var stdbehave1 = document.getElementsByClassName("behavetxt1");
        var stdbehave2 = document.getElementsByClassName("behavetxt2");
        var stdbehave3 = document.getElementsByClassName("behavetxt3");
        var stdbehave4 = document.getElementsByClassName("behavetxt4");
        var stdbehave5 = document.getElementsByClassName("behavetxt5");
        var stdbehave6 = document.getElementsByClassName("behavetxt6");
        var stdbehave7 = document.getElementsByClassName("behavetxt7");
        var stdbehave8 = document.getElementsByClassName("behavetxt8");
        var stdbehave9 = document.getElementsByClassName("behavetxt9");
        var stdbehave10 = document.getElementsByClassName("behavetxt10");

        var stdmid1 = document.getElementsByClassName("lockm1");
        var stdmid2 = document.getElementsByClassName("lockm2");
        var stdmid3 = document.getElementsByClassName("lockm3");
        var stdmid4 = document.getElementsByClassName("lockm4");
        var stdmid5 = document.getElementsByClassName("lockm5");
        var stdmid6 = document.getElementsByClassName("lockm6");
        var stdmid7 = document.getElementsByClassName("lockm7");
        var stdmid8 = document.getElementsByClassName("lockm8");
        var stdmid9 = document.getElementsByClassName("lockm9");
        var stdmid10 = document.getElementsByClassName("lockm10");
        var stdfinal1 = document.getElementsByClassName("lockf1");
        var stdfinal2 = document.getElementsByClassName("lockf2");
        var stdfinal3 = document.getElementsByClassName("lockf3");
        var stdfinal4 = document.getElementsByClassName("lockf4");
        var stdfinal5 = document.getElementsByClassName("lockf5");
        var stdfinal6 = document.getElementsByClassName("lockf6");
        var stdfinal7 = document.getElementsByClassName("lockf7");
        var stdfinal8 = document.getElementsByClassName("lockf8");
        var stdfinal9 = document.getElementsByClassName("lockf9");
        var stdfinal10 = document.getElementsByClassName("lockf10");
        var stdmidtotal = document.getElementsByClassName("lockmidterm");
        var stdfinaltotal = document.getElementsByClassName("lockfinalterm");
        var stdsamatanatotal = document.getElementsByClassName("samatscore");
        var stdreadwritetotal = document.getElementsByClassName("readscore");
        var stdbehavetotal = document.getElementsByClassName("goodbe");
        var stdSID = document.getElementsByClassName("stdSID");
        var stdidlist = document.getElementsByClassName("stdidlist");
        var loadstatus = document.getElementsByClassName("loadstatus");
        loadstatus[0].classList.remove('hidden');

        var idlist = [];
        for (var x = 0; x < stdidlist.length; x++)
            idlist.push(stdidlist[x].value);
        
        

      this.parseExcel = function(file) {
        var reader = new FileReader();

        reader.onload = function(e) {
          var data = e.target.result;
          var workbook = XLSX.read(data, {
            type: 'binary'
          });
          var check = 0;
          workbook.SheetNames.forEach(function(sheetName) {
              // Here is your object
              //alert(sheetName);
            var XL_row_object = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[sheetName]);
            var json_object = JSON.stringify(XL_row_object);
            console.log(JSON.parse(json_object));
            var data = JSON.parse(json_object);
            var yyy = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101321") %> " + data.length;
            
            if (sheetName == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132277") %>")
            {
                check = 1;
                if (data[0].fRatioQuiz%5 == 0)
                    document.getElementById('ctl00_MainContent_scoreRatio').value = data[0].fRatioQuiz;
                if (data[0].fRatioMidTerm % 5 == 0)
                    document.getElementById('ctl00_MainContent_midRatio').value = data[0].fRatioMidTerm;
                if (data[0].fRatioLateTerm % 5 == 0)
                    document.getElementById('ctl00_MainContent_lastRatio').value = data[0].fRatioLateTerm;
                if (data[0].fRatioQuizPass % 5 == 0)
                document.getElementById('ctl00_MainContent_passRatio').value = data[0].fRatioQuizPass;
                
                
                if (hasClass(maxmidscore[0], 'disable') == false) { maxmidscore[0].value = data[0].maxmidtotal; }
                if (hasClass(maxlatescore[0], 'disable') == false) { maxlatescore[0].value = data[0].maxfinaltotal; }

                if (hasClass(maxtest[0], 'disable') == false) { maxtest[0].value = data[0].maxGrade1; }
                if (hasClass(maxtest[1], 'disable') == false) { maxtest[1].value = data[0].maxGrade2; }
                if (hasClass(maxtest[2], 'disable') == false) { maxtest[2].value = data[0].maxGrade3; }
                if (hasClass(maxtest[3], 'disable') == false) { maxtest[3].value = data[0].maxGrade4; }
                if (hasClass(maxtest[4], 'disable') == false) { maxtest[4].value = data[0].maxGrade5; }
                if (hasClass(maxtest[5], 'disable') == false) { maxtest[5].value = data[0].maxGrade6; }
                if (hasClass(maxtest[6], 'disable') == false) { maxtest[6].value = data[0].maxGrade7; }
                if (hasClass(maxtest[7], 'disable') == false) { maxtest[7].value = data[0].maxGrade8; }
                if (hasClass(maxtest[8], 'disable') == false) { maxtest[8].value = data[0].maxGrade9; }
                if (hasClass(maxtest[9], 'disable') == false) { maxtest[9].value = data[0].maxGrade10; }
                if (hasClass(maxtest[10], 'disable') == false) { maxtest[10].value = data[0].maxGrade11; }
                if (hasClass(maxtest[11], 'disable') == false) { maxtest[11].value = data[0].maxGrade12; }
                if (hasClass(maxtest[12], 'disable') == false) { maxtest[12].value = data[0].maxGrade13; }
                if (hasClass(maxtest[13], 'disable') == false) { maxtest[13].value = data[0].maxGrade14; }
                if (hasClass(maxtest[14], 'disable') == false) { maxtest[14].value = data[0].maxGrade15; }
                if (hasClass(maxtest[15], 'disable') == false) { maxtest[15].value = data[0].maxGrade16; }
                if (hasClass(maxtest[16], 'disable') == false) { maxtest[16].value = data[0].maxGrade17; }
                if (hasClass(maxtest[17], 'disable') == false) { maxtest[17].value = data[0].maxGrade18; }
                if (hasClass(maxtest[18], 'disable') == false) { maxtest[18].value = data[0].maxGrade19; }
                if (hasClass(maxtest[19], 'disable') == false) { maxtest[19].value = data[0].maxGrade20; }

                if (hasClass(maxtestcw[0], 'disable') == false) { maxtestcw[0].value = data[0].maxCheewat1; }
                if (hasClass(maxtestcw[1], 'disable') == false) { maxtestcw[1].value = data[0].maxCheewat2; }
                if (hasClass(maxtestcw[2], 'disable') == false) { maxtestcw[2].value = data[0].maxCheewat3; }
                if (hasClass(maxtestcw[3], 'disable') == false) { maxtestcw[3].value = data[0].maxCheewat4; }
                if (hasClass(maxtestcw[4], 'disable') == false) { maxtestcw[4].value = data[0].maxCheewat5; }
                if (hasClass(maxtestcw[5], 'disable') == false) { maxtestcw[5].value = data[0].maxCheewat6; }
                if (hasClass(maxtestcw[6], 'disable') == false) { maxtestcw[6].value = data[0].maxCheewat7; }
                if (hasClass(maxtestcw[7], 'disable') == false) { maxtestcw[7].value = data[0].maxCheewat8; }
                if (hasClass(maxtestcw[8], 'disable') == false) { maxtestcw[8].value = data[0].maxCheewat9; }
                if (hasClass(maxtestcw[9], 'disable') == false) { maxtestcw[9].value = data[0].maxCheewat10; }
                if (hasClass(maxtestcw[10], 'disable') == false) { maxtestcw[10].value = data[0].maxCheewat11; }
                if (hasClass(maxtestcw[11], 'disable') == false) { maxtestcw[11].value = data[0].maxCheewat12; }
                if (hasClass(maxtestcw[12], 'disable') == false) { maxtestcw[12].value = data[0].maxCheewat13; }
                if (hasClass(maxtestcw[13], 'disable') == false) { maxtestcw[13].value = data[0].maxCheewat14; }
                if (hasClass(maxtestcw[14], 'disable') == false) { maxtestcw[14].value = data[0].maxCheewat15; }
                if (hasClass(maxtestcw[15], 'disable') == false) { maxtestcw[15].value = data[0].maxCheewat16; }
                if (hasClass(maxtestcw[16], 'disable') == false) { maxtestcw[16].value = data[0].maxCheewat17; }
                if (hasClass(maxtestcw[17], 'disable') == false) { maxtestcw[17].value = data[0].maxCheewat18; }
                if (hasClass(maxtestcw[18], 'disable') == false) { maxtestcw[18].value = data[0].maxCheewat19; }
                if (hasClass(maxtestcw[19], 'disable') == false) { maxtestcw[19].value = data[0].maxCheewat20; }

                if (hasClass(set1name[0], 'disable') == false) set1name[0].value = data[0].nameGrade1;
                if (hasClass(set2name[0], 'disable') == false) set2name[0].value = data[0].nameGrade2;
                if (hasClass(set3name[0], 'disable') == false) set3name[0].value = data[0].nameGrade3;
                if (hasClass(set4name[0], 'disable') == false) set4name[0].value = data[0].nameGrade4;
                if (hasClass(set5name[0], 'disable') == false) set5name[0].value = data[0].nameGrade5;
                if (hasClass(set6name[0], 'disable') == false) set6name[0].value = data[0].nameGrade6;
                if (hasClass(set7name[0], 'disable') == false) set7name[0].value = data[0].nameGrade7;
                if (hasClass(set8name[0], 'disable') == false) set8name[0].value = data[0].nameGrade8;
                if (hasClass(set9name[0], 'disable') == false) set9name[0].value = data[0].nameGrade9;
                if (hasClass(set10name[0], 'disable') == false) set10name[0].value = data[0].nameGrade10;
                if (hasClass(set11name[0], 'disable') == false) set11name[0].value = data[0].nameGrade11;
                if (hasClass(set12name[0], 'disable') == false) set12name[0].value = data[0].nameGrade12;
                if (hasClass(set13name[0], 'disable') == false) set13name[0].value = data[0].nameGrade13;
                if (hasClass(set14name[0], 'disable') == false) set14name[0].value = data[0].nameGrade14;
                if (hasClass(set15name[0], 'disable') == false) set15name[0].value = data[0].nameGrade15;
                if (hasClass(set16name[0], 'disable') == false) set16name[0].value = data[0].nameGrade16;
                if (hasClass(set17name[0], 'disable') == false) set17name[0].value = data[0].nameGrade17;
                if (hasClass(set18name[0], 'disable') == false) set18name[0].value = data[0].nameGrade18;
                if (hasClass(set19name[0], 'disable') == false) set19name[0].value = data[0].nameGrade19;
                if (hasClass(set20name[0], 'disable') == false) set20name[0].value = data[0].nameGrade20;

                if (hasClass(set1namecw[0], 'disable') == false) set1namecw[0].value = data[0].nameCheewat1;
                if (hasClass(set2namecw[0], 'disable') == false) set2namecw[0].value = data[0].nameCheewat2;
                if (hasClass(set3namecw[0], 'disable') == false) set3namecw[0].value = data[0].nameCheewat3;
                if (hasClass(set4namecw[0], 'disable') == false) set4namecw[0].value = data[0].nameCheewat4;
                if (hasClass(set5namecw[0], 'disable') == false) set5namecw[0].value = data[0].nameCheewat5;
                if (hasClass(set6namecw[0], 'disable') == false) set6namecw[0].value = data[0].nameCheewat6;
                if (hasClass(set7namecw[0], 'disable') == false) set7namecw[0].value = data[0].nameCheewat7;
                if (hasClass(set8namecw[0], 'disable') == false) set8namecw[0].value = data[0].nameCheewat8;
                if (hasClass(set9namecw[0], 'disable') == false) set9namecw[0].value = data[0].nameCheewat9;
                if (hasClass(set10namecw[0], 'disable') == false) set10namecw[0].value = data[0].nameCheewat10;
                if (hasClass(set11namecw[0], 'disable') == false) set11namecw[0].value = data[0].nameCheewat11;
                if (hasClass(set12namecw[0], 'disable') == false) set12namecw[0].value = data[0].nameCheewat12;
                if (hasClass(set13namecw[0], 'disable') == false) set13namecw[0].value = data[0].nameCheewat13;
                if (hasClass(set14namecw[0], 'disable') == false) set14namecw[0].value = data[0].nameCheewat14;
                if (hasClass(set15namecw[0], 'disable') == false) set15namecw[0].value = data[0].nameCheewat15;
                if (hasClass(set16namecw[0], 'disable') == false) set16namecw[0].value = data[0].nameCheewat16;
                if (hasClass(set17namecw[0], 'disable') == false) set17namecw[0].value = data[0].nameCheewat17;
                if (hasClass(set18namecw[0], 'disable') == false) set18namecw[0].value = data[0].nameCheewat18;
                if (hasClass(set19namecw[0], 'disable') == false) set19namecw[0].value = data[0].nameCheewat19;
                if (hasClass(set20namecw[0], 'disable') == false) set20namecw[0].value = data[0].nameCheewat20;


                if (hasClass(maxtestb1[0], 'disable') == false) maxtestb1[0].value = data[0].maxBehavior1;
                if (hasClass(maxtestb1[1], 'disable') == false) maxtestb1[1].value = data[0].maxBehavior2;
                if (hasClass(maxtestb1[2], 'disable') == false) maxtestb1[2].value = data[0].maxBehavior3;
                if (hasClass(maxtestb1[3], 'disable') == false) maxtestb1[3].value = data[0].maxBehavior4;
                if (hasClass(maxtestb1[4], 'disable') == false) maxtestb1[4].value = data[0].maxBehavior5;
                if (hasClass(maxtestb1[5], 'disable') == false) maxtestb1[5].value = data[0].maxBehavior6;
                if (hasClass(maxtestb1[6], 'disable') == false) maxtestb1[6].value = data[0].maxBehavior7;
                if (hasClass(maxtestb1[7], 'disable') == false) maxtestb1[7].value = data[0].maxBehavior8;
                if (hasClass(maxtestb1[8], 'disable') == false) maxtestb1[8].value = data[0].maxBehavior9;
                if (hasClass(maxtestb1[9], 'disable') == false) maxtestb1[9].value = data[0].maxBehavior10;

                if (hasClass(setnameb1[0], 'disable') == false) setnameb1[0].value = data[0].nameBehavior1;
                if (hasClass(setnameb2[0], 'disable') == false) setnameb2[0].value = data[0].nameBehavior2;
                if (hasClass(setnameb3[0], 'disable') == false) setnameb3[0].value = data[0].nameBehavior3;
                if (hasClass(setnameb4[0], 'disable') == false) setnameb4[0].value = data[0].nameBehavior4;
                if (hasClass(setnameb5[0], 'disable') == false) setnameb5[0].value = data[0].nameBehavior5;
                if (hasClass(setnameb6[0], 'disable') == false) setnameb6[0].value = data[0].nameBehavior6;
                if (hasClass(setnameb7[0], 'disable') == false) setnameb7[0].value = data[0].nameBehavior7;
                if (hasClass(setnameb8[0], 'disable') == false) setnameb8[0].value = data[0].nameBehavior8;
                if (hasClass(setnameb9[0], 'disable') == false) setnameb9[0].value = data[0].nameBehavior9;
                if (hasClass(setnameb10[0], 'disable') == false) setnameb10[0].value = data[0].nameBehavior10;

                if (hasClass(maxmidcw[0], 'disable') == false) maxmidcw[0].value = data[0].maxMid1;
                if (hasClass(maxmidcw[1], 'disable') == false) maxmidcw[1].value = data[0].maxMid2;
                if (hasClass(maxmidcw[2], 'disable') == false) maxmidcw[2].value = data[0].maxMid3;
                if (hasClass(maxmidcw[3], 'disable') == false) maxmidcw[3].value = data[0].maxMid4;
                if (hasClass(maxmidcw[4], 'disable') == false) maxmidcw[4].value = data[0].maxMid5;
                if (hasClass(maxmidcw[5], 'disable') == false) maxmidcw[5].value = data[0].maxMid6;
                if (hasClass(maxmidcw[6], 'disable') == false) maxmidcw[6].value = data[0].maxMid7;
                if (hasClass(maxmidcw[7], 'disable') == false) maxmidcw[7].value = data[0].maxMid8;
                if (hasClass(maxmidcw[8], 'disable') == false) maxmidcw[8].value = data[0].maxMid9;
                if (hasClass(maxmidcw[9], 'disable') == false) maxmidcw[9].value = data[0].maxMid10;

                if (hasClass(set1midcw[0], 'disable') == false) set1midcw[0].value = data[0].nameMid1;
                if (hasClass(set2midcw[0], 'disable') == false) set2midcw[0].value = data[0].nameMid2;
                if (hasClass(set3midcw[0], 'disable') == false) set3midcw[0].value = data[0].nameMid3;
                if (hasClass(set4midcw[0], 'disable') == false) set4midcw[0].value = data[0].nameMid4;
                if (hasClass(set5midcw[0], 'disable') == false) set5midcw[0].value = data[0].nameMid5;
                if (hasClass(set6midcw[0], 'disable') == false) set6midcw[0].value = data[0].nameMid6;
                if (hasClass(set7midcw[0], 'disable') == false) set7midcw[0].value = data[0].nameMid7;
                if (hasClass(set8midcw[0], 'disable') == false) set8midcw[0].value = data[0].nameMid8;
                if (hasClass(set9midcw[0], 'disable') == false) set9midcw[0].value = data[0].nameMid9;
                if (hasClass(set10midcw[0], 'disable') == false) set10midcw[0].value = data[0].nameMid10;

                if (hasClass(maxfinalcw[0], 'disable') == false) maxfinalcw[0].value = data[0].maxFinal1;
                if (hasClass(maxfinalcw[1], 'disable') == false) maxfinalcw[1].value = data[0].maxFinal2;
                if (hasClass(maxfinalcw[2], 'disable') == false) maxfinalcw[2].value = data[0].maxFinal3;
                if (hasClass(maxfinalcw[3], 'disable') == false) maxfinalcw[3].value = data[0].maxFinal4;
                if (hasClass(maxfinalcw[4], 'disable') == false) maxfinalcw[4].value = data[0].maxFinal5;
                if (hasClass(maxfinalcw[5], 'disable') == false) maxfinalcw[5].value = data[0].maxFinal6;
                if (hasClass(maxfinalcw[6], 'disable') == false) maxfinalcw[6].value = data[0].maxFinal7;
                if (hasClass(maxfinalcw[7], 'disable') == false) maxfinalcw[7].value = data[0].maxFinal8;
                if (hasClass(maxfinalcw[8], 'disable') == false) maxfinalcw[8].value = data[0].maxFinal9;
                if (hasClass(maxfinalcw[9], 'disable') == false) maxfinalcw[9].value = data[0].maxFinal10;

                if (hasClass(set1finalcw[0], 'disable') == false) set1finalcw[0].value = data[0].nameFinal1;
                if (hasClass(set2finalcw[0], 'disable') == false) set2finalcw[0].value = data[0].nameFinal2;
                if (hasClass(set3finalcw[0], 'disable') == false) set3finalcw[0].value = data[0].nameFinal3;
                if (hasClass(set4finalcw[0], 'disable') == false) set4finalcw[0].value = data[0].nameFinal4;
                if (hasClass(set5finalcw[0], 'disable') == false) set5finalcw[0].value = data[0].nameFinal5;
                if (hasClass(set6finalcw[0], 'disable') == false) set6finalcw[0].value = data[0].nameFinal6;
                if (hasClass(set7finalcw[0], 'disable') == false) set7finalcw[0].value = data[0].nameFinal7;
                if (hasClass(set8finalcw[0], 'disable') == false) set8finalcw[0].value = data[0].nameFinal8;
                if (hasClass(set9finalcw[0], 'disable') == false) set9finalcw[0].value = data[0].nameFinal9;
                if (hasClass(set10finalcw[0], 'disable') == false) set10finalcw[0].value = data[0].nameFinal10;
                
                for(var x=0;x<10;x++)
                {                   
                    if (maxfinalcw[x].value == 'undefined') maxfinalcw[x].value = "";
                    if (maxtestb1[x].value == 'undefined') maxtestb1[x].value = "";
                    if (maxmidcw[x].value == 'undefined') maxmidcw[x].value = "";                    
                }

                for (var y = 0; y < 20; y++) {                    
                    if (maxtest[x].value == 'undefined') maxtest[x].value = "";
                                     
                }

                if (set1name[0].value == 'undefined') set1name[0].value = "";
                if (set2name[0].value == 'undefined') set2name[0].value = "";
                if (set3name[0].value == 'undefined') set3name[0].value = "";
                if (set4name[0].value == 'undefined') set4name[0].value = "";
                if (set5name[0].value == 'undefined') set5name[0].value = "";
                if (set6name[0].value == 'undefined') set6name[0].value = "";
                if (set7name[0].value == 'undefined') set7name[0].value = "";
                if (set8name[0].value == 'undefined') set8name[0].value = "";
                if (set9name[0].value == 'undefined') set9name[0].value = "";
                if (set10name[0].value == 'undefined') set10name[0].value = "";
                if (set11name[0].value == 'undefined') set11name[0].value = "";
                if (set12name[0].value == 'undefined') set12name[0].value = "";
                if (set13name[0].value == 'undefined') set13name[0].value = "";
                if (set14name[0].value == 'undefined') set14name[0].value = "";
                if (set15name[0].value == 'undefined') set15name[0].value = "";
                if (set16name[0].value == 'undefined') set16name[0].value = "";
                if (set17name[0].value == 'undefined') set17name[0].value = "";
                if (set18name[0].value == 'undefined') set18name[0].value = "";
                if (set19name[0].value == 'undefined') set19name[0].value = "";
                if (set20name[0].value == 'undefined') set20name[0].value = "";

              

                if (setnameb1[0].value == 'undefined') setnameb1[0].value = "";
                if (setnameb2[0].value == 'undefined') setnameb2[0].value = "";
                if (setnameb3[0].value == 'undefined') setnameb3[0].value = "";
                if (setnameb4[0].value == 'undefined') setnameb4[0].value = "";
                if (setnameb5[0].value == 'undefined') setnameb5[0].value = "";
                if (setnameb6[0].value == 'undefined') setnameb6[0].value = "";
                if (setnameb7[0].value == 'undefined') setnameb7[0].value = "";
                if (setnameb8[0].value == 'undefined') setnameb8[0].value = "";
                if (setnameb9[0].value == 'undefined') setnameb9[0].value = "";
                if (setnameb10[0].value == 'undefined') setnameb10[0].value = "";                

                if (set1midcw[0].value == 'undefined') set1midcw[0].value = "";
                if (set2midcw[0].value == 'undefined') set2midcw[0].value = "";
                if (set3midcw[0].value == 'undefined') set3midcw[0].value = "";
                if (set4midcw[0].value == 'undefined') set4midcw[0].value = "";
                if (set5midcw[0].value == 'undefined') set5midcw[0].value = "";
                if (set6midcw[0].value == 'undefined') set6midcw[0].value = "";
                if (set7midcw[0].value == 'undefined') set7midcw[0].value = "";
                if (set8midcw[0].value == 'undefined') set8midcw[0].value = "";
                if (set9midcw[0].value == 'undefined') set9midcw[0].value = "";
                if (set10midcw[0].value == 'undefined') set10midcw[0].value = "";
                
                if (set1finalcw[0].value == 'undefined') set1finalcw[0].value = "";
                if (set2finalcw[0].value == 'undefined') set2finalcw[0].value = "";
                if (set3finalcw[0].value == 'undefined') set3finalcw[0].value = "";
                if (set4finalcw[0].value == 'undefined') set4finalcw[0].value = "";
                if (set5finalcw[0].value == 'undefined') set5finalcw[0].value = "";
                if (set6finalcw[0].value == 'undefined') set6finalcw[0].value = "";
                if (set7finalcw[0].value == 'undefined') set7finalcw[0].value = "";
                if (set8finalcw[0].value == 'undefined') set8finalcw[0].value = "";
                if (set9finalcw[0].value == 'undefined') set9finalcw[0].value = "";
                if (set10finalcw[0].value == 'undefined') set10finalcw[0].value = "";
            }
            if (sheetName == "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132278") %>")
            {
                var message1 = '<h2><p class="centertext">';                
                var count = 0;
                var notfound = 0;
                var midcount = 0;
                var finalcount = 0;
                var behavecount = 0;
                var cwcount = 0;
                var readcount = 0;
                
                for (var x = 0; x < data.length; x++) {
                    if (data[x].stdSID != 0)
                    {
                        var sss = data[x].stdSID;
                        var xx = idlist.indexOf(sss);

                        
                        if (xx != -1) {
                            count = count + 1;
                            
                            if (hasClass(stdscore1[xx], 'disable') == false) { if (data[x].scoreGrade1 != null) stdscore1[xx].value = data[x].scoreGrade1; else stdscore1[xx].value = ""; }
                            if (hasClass(stdscore2[xx], 'disable') == false) { if (data[x].scoreGrade2 != null) stdscore2[xx].value = data[x].scoreGrade2; else stdscore2[xx].value = ""; }
                            if (hasClass(stdscore3[xx], 'disable') == false) { if (data[x].scoreGrade3 != null) stdscore3[xx].value = data[x].scoreGrade3; else stdscore3[xx].value = ""; }
                            if (hasClass(stdscore4[xx], 'disable') == false) { if (data[x].scoreGrade4 != null) stdscore4[xx].value = data[x].scoreGrade4; else stdscore4[xx].value = ""; }
                            if (hasClass(stdscore5[xx], 'disable') == false) { if (data[x].scoreGrade5 != null) stdscore5[xx].value = data[x].scoreGrade5; else stdscore5[xx].value = ""; }
                            if (hasClass(stdscore6[xx], 'disable') == false) { if (data[x].scoreGrade6 != null) stdscore6[xx].value = data[x].scoreGrade6; else stdscore6[xx].value = ""; }
                            if (hasClass(stdscore7[xx], 'disable') == false) { if (data[x].scoreGrade7 != null) stdscore7[xx].value = data[x].scoreGrade7; else stdscore7[xx].value = ""; }
                            if (hasClass(stdscore8[xx], 'disable') == false) { if (data[x].scoreGrade8 != null) stdscore8[xx].value = data[x].scoreGrade8; else stdscore8[xx].value = ""; }
                            if (hasClass(stdscore9[xx], 'disable') == false) { if (data[x].scoreGrade9 != null) stdscore9[xx].value = data[x].scoreGrade9; else stdscore9[xx].value = ""; }
                            if (hasClass(stdscore10[xx], 'disable') == false) { if (data[x].scoreGrade10 != null) stdscore10[xx].value = data[x].scoreGrade10; else stdscore10[xx].value = ""; }
                            if (hasClass(stdscore11[xx], 'disable') == false) { if (data[x].scoreGrade11 != null) stdscore11[xx].value = data[x].scoreGrade11; else stdscore11[xx].value = ""; }
                            if (hasClass(stdscore12[xx], 'disable') == false) { if (data[x].scoreGrade12 != null) stdscore12[xx].value = data[x].scoreGrade12; else stdscore12[xx].value = ""; }
                            if (hasClass(stdscore13[xx], 'disable') == false) { if (data[x].scoreGrade13 != null) stdscore13[xx].value = data[x].scoreGrade13; else stdscore13[xx].value = ""; }
                            if (hasClass(stdscore14[xx], 'disable') == false) { if (data[x].scoreGrade14 != null) stdscore14[xx].value = data[x].scoreGrade14; else stdscore14[xx].value = ""; }
                            if (hasClass(stdscore15[xx], 'disable') == false) { if (data[x].scoreGrade15 != null) stdscore15[xx].value = data[x].scoreGrade15; else stdscore15[xx].value = ""; }
                            if (hasClass(stdscore16[xx], 'disable') == false) { if (data[x].scoreGrade16 != null) stdscore16[xx].value = data[x].scoreGrade16; else stdscore16[xx].value = ""; }
                            if (hasClass(stdscore17[xx], 'disable') == false) { if (data[x].scoreGrade17 != null) stdscore17[xx].value = data[x].scoreGrade17; else stdscore17[xx].value = ""; }
                            if (hasClass(stdscore18[xx], 'disable') == false) { if (data[x].scoreGrade18 != null) stdscore18[xx].value = data[x].scoreGrade18; else stdscore18[xx].value = ""; }
                            if (hasClass(stdscore19[xx], 'disable') == false) { if (data[x].scoreGrade19 != null) stdscore19[xx].value = data[x].scoreGrade19; else stdscore19[xx].value = ""; }
                            if (hasClass(stdscore20[xx], 'disable') == false) { if (data[x].scoreGrade20 != null) stdscore20[xx].value = data[x].scoreGrade20; else stdscore20[xx].value = ""; }

                            if (hasClass(stdchewat1[xx], 'disable') == false) { if (data[x].scoreCheewat1 != null) { stdchewat1[xx].value = data[x].scoreCheewat1; if (Number(data[x].scoreCheewat1) > 0) cwcount++;} else stdchewat1[xx].value = ""; }
                            if (hasClass(stdchewat2[xx], 'disable') == false) { if (data[x].scoreCheewat2 != null) stdchewat2[xx].value = data[x].scoreCheewat2; else stdchewat2[xx].value = ""; }
                            if (hasClass(stdchewat3[xx], 'disable') == false) { if (data[x].scoreCheewat3 != null) stdchewat3[xx].value = data[x].scoreCheewat3; else stdchewat3[xx].value = ""; }
                            if (hasClass(stdchewat4[xx], 'disable') == false) { if (data[x].scoreCheewat4 != null) stdchewat4[xx].value = data[x].scoreCheewat4; else stdchewat4[xx].value = ""; }
                            if (hasClass(stdchewat5[xx], 'disable') == false) { if (data[x].scoreCheewat5 != null) stdchewat5[xx].value = data[x].scoreCheewat5; else stdchewat5[xx].value = ""; }
                            if (hasClass(stdchewat6[xx], 'disable') == false) { if (data[x].scoreCheewat6 != null) stdchewat6[xx].value = data[x].scoreCheewat6; else stdchewat6[xx].value = ""; }
                            if (hasClass(stdchewat7[xx], 'disable') == false) { if (data[x].scoreCheewat7 != null) stdchewat7[xx].value = data[x].scoreCheewat7; else stdchewat7[xx].value = ""; }
                            if (hasClass(stdchewat8[xx], 'disable') == false) { if (data[x].scoreCheewat8 != null) stdchewat8[xx].value = data[x].scoreCheewat8; else stdchewat8[xx].value = ""; }
                            if (hasClass(stdchewat9[xx], 'disable') == false) { if (data[x].scoreCheewat9 != null) stdchewat9[xx].value = data[x].scoreCheewat9; else stdchewat9[xx].value = ""; }
                            if (hasClass(stdchewat10[xx], 'disable') == false) { if (data[x].scoreCheewat10 != null) stdchewat10[xx].value = data[x].scoreCheewat10; else stdchewat10[xx].value = ""; }
                            if (hasClass(stdchewat11[xx], 'disable') == false) { if (data[x].scoreCheewat11 != null) stdchewat11[xx].value = data[x].scoreCheewat11; else stdchewat11[xx].value = ""; }
                            if (hasClass(stdchewat12[xx], 'disable') == false) { if (data[x].scoreCheewat12 != null) stdchewat12[xx].value = data[x].scoreCheewat12; else stdchewat12[xx].value = ""; }
                            if (hasClass(stdchewat13[xx], 'disable') == false) { if (data[x].scoreCheewat13 != null) stdchewat13[xx].value = data[x].scoreCheewat13; else stdchewat13[xx].value = ""; }
                            if (hasClass(stdchewat14[xx], 'disable') == false) { if (data[x].scoreCheewat14 != null) stdchewat14[xx].value = data[x].scoreCheewat14; else stdchewat14[xx].value = ""; }
                            if (hasClass(stdchewat15[xx], 'disable') == false) { if (data[x].scoreCheewat15 != null) stdchewat15[xx].value = data[x].scoreCheewat15; else stdchewat15[xx].value = ""; }
                            if (hasClass(stdchewat16[xx], 'disable') == false) { if (data[x].scoreCheewat16 != null) stdchewat16[xx].value = data[x].scoreCheewat16; else stdchewat16[xx].value = ""; }
                            if (hasClass(stdchewat17[xx], 'disable') == false) { if (data[x].scoreCheewat17 != null) stdchewat17[xx].value = data[x].scoreCheewat17; else stdchewat17[xx].value = ""; }
                            if (hasClass(stdchewat18[xx], 'disable') == false) { if (data[x].scoreCheewat18 != null) stdchewat18[xx].value = data[x].scoreCheewat18; else stdchewat18[xx].value = ""; }
                            if (hasClass(stdchewat19[xx], 'disable') == false) { if (data[x].scoreCheewat19 != null) stdchewat19[xx].value = data[x].scoreCheewat19; else stdchewat19[xx].value = ""; }
                            if (hasClass(stdchewat20[xx], 'disable') == false) { if (data[x].scoreCheewat20 != null) stdchewat20[xx].value = data[x].scoreCheewat20; else stdchewat20[xx].value = ""; }

                            if (hasClass(stdbehave1[xx], 'disable') == false) { if (data[x].scoreBehavior1 != null) { stdbehave1[xx].value = data[x].scoreBehavior1; behavecount++; } else stdbehave1[xx].value = ""; }
                            if (hasClass(stdbehave2[xx], 'disable') == false) { if (data[x].scoreBehavior2 != null) { stdbehave2[xx].value = data[x].scoreBehavior2; behavecount++; } else stdbehave2[xx].value = ""; }
                            if (hasClass(stdbehave3[xx], 'disable') == false) { if (data[x].scoreBehavior3 != null) { stdbehave3[xx].value = data[x].scoreBehavior3; behavecount++; } else stdbehave3[xx].value = ""; }
                            if (hasClass(stdbehave4[xx], 'disable') == false) { if (data[x].scoreBehavior4 != null) { stdbehave4[xx].value = data[x].scoreBehavior4; behavecount++; } else stdbehave4[xx].value = ""; }
                            if (hasClass(stdbehave5[xx], 'disable') == false) { if (data[x].scoreBehavior5 != null) { stdbehave5[xx].value = data[x].scoreBehavior5; behavecount++; } else stdbehave5[xx].value = ""; }
                            if (hasClass(stdbehave6[xx], 'disable') == false) { if (data[x].scoreBehavior6 != null) { stdbehave6[xx].value = data[x].scoreBehavior6; behavecount++; } else stdbehave6[xx].value = ""; }
                            if (hasClass(stdbehave7[xx], 'disable') == false) { if (data[x].scoreBehavior7 != null) { stdbehave7[xx].value = data[x].scoreBehavior7; behavecount++; } else stdbehave7[xx].value = ""; }
                            if (hasClass(stdbehave8[xx], 'disable') == false) { if (data[x].scoreBehavior8 != null) { stdbehave8[xx].value = data[x].scoreBehavior8; behavecount++; } else stdbehave8[xx].value = ""; }
                            if (hasClass(stdbehave9[xx], 'disable') == false) { if (data[x].scoreBehavior9 != null) { stdbehave9[xx].value = data[x].scoreBehavior9; behavecount++; } else stdbehave9[xx].value = ""; }
                            if (hasClass(stdbehave10[xx], 'disable') == false) { if (data[x].scoreBehavior10 != null) { stdbehave10[xx].value = data[x].scoreBehavior10; behavecount++; } else stdbehave10[xx].value = ""; }

                            if (hasClass(stdmid1[xx], 'disable') == false) { if  (data[x].scoreMid1 != null) { stdmid1[xx].value = data[x].scoreMid1; midcount++;} else stdmid1[xx].value = "";}
                            if (hasClass(stdmid2[xx], 'disable') == false) { if  (data[x].scoreMid2 != null) { stdmid2[xx].value = data[x].scoreMid2; midcount++;} else stdmid2[xx].value = "";}
                            if (hasClass(stdmid3[xx], 'disable') == false) { if  (data[x].scoreMid3 != null) { stdmid3[xx].value = data[x].scoreMid3; midcount++;} else stdmid3[xx].value = "";}
                            if (hasClass(stdmid4[xx], 'disable') == false) { if  (data[x].scoreMid4 != null) { stdmid4[xx].value = data[x].scoreMid4; midcount++;} else stdmid4[xx].value = "";}
                            if (hasClass(stdmid5[xx], 'disable') == false) { if  (data[x].scoreMid5 != null) { stdmid5[xx].value = data[x].scoreMid5; midcount++;} else stdmid5[xx].value = "";}
                            if (hasClass(stdmid6[xx], 'disable') == false) { if  (data[x].scoreMid6 != null) { stdmid6[xx].value = data[x].scoreMid6; midcount++;} else stdmid6[xx].value = "";}
                            if (hasClass(stdmid7[xx], 'disable') == false) { if  (data[x].scoreMid7 != null) { stdmid7[xx].value = data[x].scoreMid7; midcount++;} else stdmid7[xx].value = "";}
                            if (hasClass(stdmid8[xx], 'disable') == false) { if  (data[x].scoreMid8 != null) { stdmid8[xx].value = data[x].scoreMid8; midcount++;} else stdmid8[xx].value = "";}
                            if (hasClass(stdmid9[xx], 'disable') == false) { if  (data[x].scoreMid9 != null) { stdmid9[xx].value = data[x].scoreMid9; midcount++;} else stdmid9[xx].value = "";}
                            if (hasClass(stdmid10[xx], 'disable') == false) { if (data[x].scoreMid10 != null) { stdmid10[xx].value = data[x].scoreMid10; midcount++; } else stdmid10[xx].value = ""; }

                            if (hasClass(stdfinal1[xx], 'disable') == false) { if (data[x].scoreFinal1 != null) { stdfinal1[xx].value = data[x].scoreFinal1; finalcount++; } else stdfinal1[xx].value = ""; }
                            if (hasClass(stdfinal2[xx], 'disable') == false) { if (data[x].scoreFinal2 != null) { stdfinal2[xx].value = data[x].scoreFinal2; finalcount++; } else stdfinal2[xx].value = ""; }
                            if (hasClass(stdfinal3[xx], 'disable') == false) { if (data[x].scoreFinal3 != null) { stdfinal3[xx].value = data[x].scoreFinal3; finalcount++; } else stdfinal3[xx].value = ""; }
                            if (hasClass(stdfinal4[xx], 'disable') == false) { if (data[x].scoreFinal4 != null) { stdfinal4[xx].value = data[x].scoreFinal4; finalcount++; } else stdfinal4[xx].value = ""; }
                            if (hasClass(stdfinal5[xx], 'disable') == false) { if (data[x].scoreFinal5 != null) { stdfinal5[xx].value = data[x].scoreFinal5; finalcount++; } else stdfinal5[xx].value = ""; }
                            if (hasClass(stdfinal6[xx], 'disable') == false) { if (data[x].scoreFinal6 != null) { stdfinal6[xx].value = data[x].scoreFinal6; finalcount++; } else stdfinal6[xx].value = ""; }
                            if (hasClass(stdfinal7[xx], 'disable') == false) { if (data[x].scoreFinal7 != null) { stdfinal7[xx].value = data[x].scoreFinal7; finalcount++; } else stdfinal7[xx].value = ""; }
                            if (hasClass(stdfinal8[xx], 'disable') == false) { if (data[x].scoreFinal8 != null) { stdfinal8[xx].value = data[x].scoreFinal8; finalcount++; } else stdfinal8[xx].value = ""; }
                            if (hasClass(stdfinal9[xx], 'disable') == false) { if (data[x].scoreFinal9 != null) { stdfinal9[xx].value = data[x].scoreFinal9; finalcount++; } else stdfinal9[xx].value = ""; }
                            if (hasClass(stdfinal10[xx], 'disable') == false) { if (data[x].scoreFinal10 != null) { stdfinal10[xx].value = data[x].scoreFinal10; finalcount++; } else stdfinal10[xx].value = ""; }

                            if (hasClass(stdmidtotal[xx], 'disable') == false) { if (data[x].scoreMidTermSUM != null) stdmidtotal[xx].value = data[x].scoreMidTermSUM; else stdmidtotal[xx].value = ""; }
                            if (hasClass(stdfinaltotal[xx], 'disable') == false) { if (data[x].scoreFinalTermSUM != null) stdfinaltotal[xx].value = data[x].scoreFinalTermSUM; else stdfinaltotal[xx].value = ""; }
                            if (hasClass(stdsamatanatotal[xx], 'disable') == false) { if (data[x].scoreSamatana != null) stdsamatanatotal[xx].value = data[x].scoreSamatana; else stdsamatanatotal[xx].value = ""; }
                            if (data[x].getReadWrite != null) { stdreadwritetotal[xx].value = data[x].getReadWrite; readcount++;} else stdreadwritetotal[xx].value = "";
                            if (hasClass(stdbehavetotal[xx], 'disable') == false) { if (data[x].scoreBahaviorSUM != null) stdbehavetotal[xx].value = data[x].scoreBahaviorSUM; else stdbehavetotal[xx].value = ""; }
                            stdmidtotal[xx].value =
                                Number(stdmid1[xx].value) +
                                Number(stdmid2[xx].value) +
                                Number(stdmid3[xx].value) +
                                Number(stdmid4[xx].value) +
                                Number(stdmid5[xx].value) +
                                Number(stdmid6[xx].value) +
                                Number(stdmid7[xx].value) +
                                Number(stdmid8[xx].value) +
                                Number(stdmid9[xx].value) +
                                Number(stdmid10[xx].value);
                            stdfinaltotal[xx].value =
                                Number(stdfinal1[xx].value) +
                                Number(stdfinal2[xx].value) +
                                Number(stdfinal3[xx].value) +
                                Number(stdfinal4[xx].value) +
                                Number(stdfinal5[xx].value) +
                                Number(stdfinal6[xx].value) +
                                Number(stdfinal7[xx].value) +
                                Number(stdfinal8[xx].value) +
                                Number(stdfinal9[xx].value) +
                                Number(stdfinal10[xx].value);
                        }
                        else {
                            message1 = message1 + sss + '<br>';
                            notfound = notfound + 1;
                        }
                    }                    
                }
                
                if (readcount > 0)
                    document.getElementById("check3").checked = true;
                if (cwcount > 0)
                    document.getElementById("ctl00_MainContent_check6").checked = true;
                if (midcount > 0)
                    document.getElementById("ctl00_MainContent_check11").checked = true;
                if (finalcount > 0)
                    document.getElementById("ctl00_MainContent_check12").checked = true;
                if (behavecount > 0) {
                    document.getElementById("ctl00_MainContent_check4").checked = true;
                    document.getElementById("ctl00_MainContent_check5").checked = true;
                }
                message1 = message1 + "</p></h>";
                var error1 = '<h2><p class="centertext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02351") %></p></h>';
                var done1 = '<h2><p class="centertext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02349") %><br><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02350") %> : ' +count + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> </p>';
                if (notfound == 0)
                    error1 = "";
                if (count == 0)
                    done1 = "<h2>";
                
                setTimeout(function () {
                    auto();
                    CompareDates(99999);
                    changename();
                    calmaxmid();
                    calmaxfinal();
                    setTimeout(function () {
                        loadstatus[0].classList.add('hidden');
                        bootbox.alert({
                            message: done1 + error1 + message1,
                            backdrop: true
                        });
                    }, 1000);
                }, 2000);

                
                
            }

              
            
          })
          if (check == 0)
          {
              bootbox.alert({
                  message: '<h2><p class="centertext"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206065") %></p></h>',
                  backdrop: true
              });
              loadstatus[0].classList.add('hidden');
          }              
        };
       
        
        reader.onerror = function(ex) {
          console.log(ex);
        };
        
        reader.readAsBinaryString(file);
      };
  };

    function endfunction() {

        var files = evt.target.files; // FileList object
        var xl2json = new ExcelToJSON();
        xl2json.parseExcel(files[0]);
    }

  function handleFileSelect(evt) {
    
    var files = evt.target.files; // FileList object
    var xl2json = new ExcelToJSON();
    xl2json.parseExcel(files[0]);
  }


 
</script>
        <script type="text/javascript" language="javascript">
           
            $(document).ready(function () {
                $('.js-example-basic-multiple4').select2();
            });

            function addword4() {
                var from = document.getElementsByClassName("classchoose");
                var data = $('.js-example-basic-multiple4').select2('data')

                var mName = document.getElementsByClassName("modalName");
                var multiClass = document.getElementsByClassName("editmulti4");
                var mStart = document.getElementsByClassName("modalStart");
                var mEnd = document.getElementsByClassName("modalEnd");
                var mType = document.getElementsByClassName("modalType");
                var mColor = document.getElementsByClassName("modalColor");
                var mWho = document.getElementsByClassName("modalWho");
                var x;
                //alert(data.length);
                multiClass[0].value = "";
                for (x = 0; x < Number(data.length) ; x++) {
                    multiClass[0].value = multiClass[0].value + "/" + data[x].id;
                }

            }

            function editPillbox() {

                var editmulti = document.getElementsByClassName("editmulti");
                var check8 = document.getElementsByClassName("check8");
                var data = $('.js-example-basic-multiple4').select2('data');
                var xxxx = [];
                var fields = editmulti[0].value.split('/');
                if (editmulti[0].value != "")
                    document.getElementById("ctl00_MainContent_check8").checked = true;
                for (var x = 0; x < fields.length-1; x++) {
                    xxxx.push(fields[x]);
                }
                $('.js-example-basic-multiple4').val(xxxx).trigger("change");
            }

            $(document).ready(function () {
                $.protip();
                $("#content-overlay").show();   

                GetddlListGroup();
                ShowGroup();

                $('#mainDiv').keypress(function (event) {
                    document.getElementById("btnOpenGroupSidebar").disabled = true;


                });

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

            function checkmax(namemax,valuemax,length,lockvalue,popupvalue)
            {                
                var lockvalue = document.getElementsByClassName(lockvalue);
                var namemax = document.getElementsByClassName(namemax);
                var popupnomax = document.getElementsByClassName("popupnomax");

                var nomax = 0;
                if (Number(namemax[valuemax].value) != 0) {
                    
                    var z = 0;
                    for (var y = 0; y < length; y++) {
                        
                        if (lockvalue[y].value == "")
                            z++;
                        if (z == length) {                            
                            nomax++;
                        }
                    }
                }

                return nomax;
            }

            function nextpage(id) {
                
                var quizratio = document.getElementsByClassName("quizratio");
                var midratio = document.getElementsByClassName("midratio");
                var lateratio = document.getElementsByClassName("lateratio");
                var ddl4 = document.getElementsByClassName("ddl4");

                var checkdll = 1;
                if (quizratio[0].value == "-1") checkdll = 0;
                if (midratio[0].value == "-1") checkdll = 0;
                if (lateratio[0].value == "-1") checkdll = 0;
                if (ddl4[0].value == "-1") checkdll = 0;

                if (checkdll == 0)
                {
                    bootbox.alert({
                        message: '<h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132279") %></h>',
                        backdrop: true
                    });
                    return;
                }
                
                var loadstatus = document.getElementsByClassName("loadstatus");
                var periodnow = document.getElementsByClassName("periodnow");
                var maxtest = document.getElementsByClassName("maxtest");
                var maxtestcw = document.getElementsByClassName("maxtestcw");
                var periodsubmit = document.getElementsByClassName("periodsubmit");
                var lock1b = document.getElementsByClassName("lock1b");
                var lock2b = document.getElementsByClassName("lock2b");
                var lock3b = document.getElementsByClassName("lock3b");
                var lock4b = document.getElementsByClassName("lock4b");
                var lockg1 = document.getElementsByClassName("lockg1");
                var lockg2 = document.getElementsByClassName("lockg2");
                var lockg3 = document.getElementsByClassName("lockg3");
                var lockg4 = document.getElementsByClassName("lockg4");
                var lockg5 = document.getElementsByClassName("lockg5");
                var lockg6 = document.getElementsByClassName("lockg6");
                var lockg7 = document.getElementsByClassName("lockg7");
                var lockg8 = document.getElementsByClassName("lockg8");
                var lockg9 = document.getElementsByClassName("lockg9");
                var lockg10 = document.getElementsByClassName("lockg10");
                var lockg11 = document.getElementsByClassName("lockg11");
                var lockg12 = document.getElementsByClassName("lockg12");
                var lockg13 = document.getElementsByClassName("lockg13");
                var lockg14 = document.getElementsByClassName("lockg14");
                var lockg15 = document.getElementsByClassName("lockg15");
                var lockg16 = document.getElementsByClassName("lockg16");
                var lockg17 = document.getElementsByClassName("lockg17");
                var lockg18 = document.getElementsByClassName("lockg18");
                var lockg19 = document.getElementsByClassName("lockg19");
                var lockg20 = document.getElementsByClassName("lockg20");
                var lockcw1 = document.getElementsByClassName("lockcw1");
                var lockcw2 = document.getElementsByClassName("lockcw2");
                var lockcw3 = document.getElementsByClassName("lockcw3");
                var lockcw4 = document.getElementsByClassName("lockcw4");
                var lockcw5 = document.getElementsByClassName("lockcw5");
                var lockcw6 = document.getElementsByClassName("lockcw6");
                var lockcw7 = document.getElementsByClassName("lockcw7");
                var lockcw8 = document.getElementsByClassName("lockcw8");
                var lockcw9 = document.getElementsByClassName("lockcw9");
                var lockcw10 = document.getElementsByClassName("lockcw10");
                var lockcw11 = document.getElementsByClassName("lockcw11");
                var lockcw12 = document.getElementsByClassName("lockcw12");
                var lockcw13 = document.getElementsByClassName("lockcw13");
                var lockcw14 = document.getElementsByClassName("lockcw14");
                var lockcw15 = document.getElementsByClassName("lockcw15");
                var lockcw16 = document.getElementsByClassName("lockcw16");
                var lockcw17 = document.getElementsByClassName("lockcw17");
                var lockcw18 = document.getElementsByClassName("lockcw18");
                var lockcw19 = document.getElementsByClassName("lockcw19");
                var lockcw20 = document.getElementsByClassName("lockcw20");

                loadstatus[0].classList.remove('hidden');
                
                autobehave();
                loadstatus[0].classList.add('hidden');
                var clickButton = document.getElementById("<%= btnSave.ClientID %>");
                var txtglow = document.getElementsByClassName("txtglow");
                
                var btnok = document.getElementsByClassName("btnok");
                var btnerror = document.getElementsByClassName("btnerror");
                var viplogin = document.getElementsByClassName("viplogin");
                if (viplogin[0].value == "2") {
                    //btnok[0].classList.add('hidden');
                    //btnerror[0].classList.add('hidden');
                }

                periodsubmit[0].value = periodnow[0].value;
                if (id == 2)
                    periodsubmit[0].value = -1;
                if (txtglow.length == 0)
                {                                       

                    lock1b[0].value = checkmax('maxtest',0, lockg1.length, 'lockg1',0);
                    lock1b[1].value = checkmax('maxtest',1, lockg1.length, 'lockg2',0);
                    lock1b[2].value = checkmax('maxtest',2, lockg1.length, 'lockg3',0);
                    lock1b[3].value = checkmax('maxtest',3, lockg1.length, 'lockg4',0);
                    lock1b[4].value = checkmax('maxtest',4, lockg1.length, 'lockg5',0);
                    lock1b[5].value = checkmax('maxtest',5, lockg1.length, 'lockg6',0);
                    lock1b[6].value = checkmax('maxtest',6, lockg1.length, 'lockg7',0);
                    lock1b[7].value = checkmax('maxtest',7, lockg1.length, 'lockg8',0);
                    lock1b[8].value = checkmax('maxtest',8, lockg1.length, 'lockg9',0);
                    lock1b[9].value = checkmax('maxtest',9, lockg1.length, 'lockg10',0);
                    lock1b[10].value = checkmax('maxtest',10, lockg1.length, 'lockg11',0);
                    lock1b[11].value = checkmax('maxtest',11, lockg1.length, 'lockg12',0);
                    lock1b[12].value = checkmax('maxtest',12, lockg1.length, 'lockg13',0);
                    lock1b[13].value = checkmax('maxtest',13, lockg1.length, 'lockg14',0);
                    lock1b[14].value = checkmax('maxtest',14, lockg1.length, 'lockg15',0);
                    lock1b[15].value = checkmax('maxtest',15, lockg1.length, 'lockg16',0);
                    lock1b[16].value = checkmax('maxtest',16, lockg1.length, 'lockg17',0);
                    lock1b[17].value = checkmax('maxtest',17, lockg1.length, 'lockg18',0);
                    lock1b[18].value = checkmax('maxtest',18, lockg1.length, 'lockg19',0);
                    lock1b[19].value = checkmax('maxtest',19, lockg1.length, 'lockg20',0);
                    lock2b[0].value = checkmax('maxtestcw',0, lockg1.length, 'lockcw1',0);
                    lock2b[1].value = checkmax('maxtestcw',1, lockg1.length, 'lockcw2',0);
                    lock2b[2].value = checkmax('maxtestcw',2, lockg1.length, 'lockcw3',0);
                    lock2b[3].value = checkmax('maxtestcw',3, lockg1.length, 'lockcw4',0);
                    lock2b[4].value = checkmax('maxtestcw',4, lockg1.length, 'lockcw5',0);
                    lock2b[5].value = checkmax('maxtestcw',5, lockg1.length, 'lockcw6',0);
                    lock2b[6].value = checkmax('maxtestcw',6, lockg1.length, 'lockcw7',0);
                    lock2b[7].value = checkmax('maxtestcw',7, lockg1.length, 'lockcw8',0);
                    lock2b[8].value = checkmax('maxtestcw',8, lockg1.length, 'lockcw9',0);
                    lock2b[9].value = checkmax('maxtestcw',9, lockg1.length, 'lockcw10',0);
                    lock2b[10].value = checkmax('maxtestcw',10, lockg1.length, 'lockcw11',0);
                    lock2b[11].value = checkmax('maxtestcw',11, lockg1.length, 'lockcw12',0);
                    lock2b[12].value = checkmax('maxtestcw',12, lockg1.length, 'lockcw13',0);
                    lock2b[13].value = checkmax('maxtestcw',13, lockg1.length, 'lockcw14',0);
                    lock2b[14].value = checkmax('maxtestcw',14, lockg1.length, 'lockcw15',0);
                    lock2b[15].value = checkmax('maxtestcw',15, lockg1.length, 'lockcw16',0);
                    lock2b[16].value = checkmax('maxtestcw',16, lockg1.length, 'lockcw17',0);
                    lock2b[17].value = checkmax('maxtestcw',17, lockg1.length, 'lockcw18',0);
                    lock2b[18].value = checkmax('maxtestcw',18, lockg1.length, 'lockcw19',0);
                    lock2b[19].value = checkmax('maxtestcw',19, lockg1.length, 'lockcw20',0);
   
                    

                    if (id == 2)
                    {
                        bootbox.confirm({
                            title: '<h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101328") %></h>',
                            message: '<h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206090") %></h>',
                            buttons: {
                                cancel: {
                                    label: '<i class="fa fa-times"></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>'
                                },
                                confirm: {
                                    label: '<i class="fa fa-check"></i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>'
                                }
                            },
                            callback: function (result) {
                                if (result == true) {
                                    clickButton.click();
                                    loadstatus[0].classList.remove('hidden');
                                }
                            }
                        });
                    }
                    else {
                        clickButton.click();
                        loadstatus[0].classList.remove('hidden');
                    }
                }
                else 
                {

                    bootbox.alert({
                        message: '<h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132259") %></h>',
                        backdrop: true
                    });
                   
                }                
            }

            function nomax() {
                var popupnomax = document.getElementsByClassName("popupnomax");
                
                if (popupnomax[0].value != "1")
                {
                    popupnomax[0].value = "1";
                    bootbox.alert({
                        message: '<h2><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132280") %></h>',
                        backdrop: true
                    });
                }
                setTimeout(function () {
                    popupnomax[0].value = "0";
                }, 5000);
            }

            function calllock(id) {
                var txt = 0;
                for (var x = 0; (x < id.length) && (txt == 0) ; x++) {                    
                    if (id[x].value != "")
                        txt = 1;
                }
                return txt;
            }

             function lockpage() {
                var periodnow = document.getElementsByClassName("periodnow");
                var periodsubmit = document.getElementsByClassName("periodsubmit");
                var lock1 = document.getElementsByClassName("lock1");
                var lock2 = document.getElementsByClassName("lock2");
                var lock3 = document.getElementsByClassName("lock3");
                var lock4 = document.getElementsByClassName("lock4");
                var lockmid = document.getElementsByClassName("lockmid");
                var lockfinal = document.getElementsByClassName("lockfinal");
                var lockg1 = document.getElementsByClassName("lockg1");
                var lockg2 = document.getElementsByClassName("lockg2");
                var lockg3 = document.getElementsByClassName("lockg3");
                var lockg4 = document.getElementsByClassName("lockg4");
                var lockg5 = document.getElementsByClassName("lockg5");
                var lockg6 = document.getElementsByClassName("lockg6");
                var lockg7 = document.getElementsByClassName("lockg7");
                var lockg8 = document.getElementsByClassName("lockg8");
                var lockg9 = document.getElementsByClassName("lockg9");
                var lockg10 = document.getElementsByClassName("lockg10");
                var lockg11 = document.getElementsByClassName("lockg11");
                var lockg12 = document.getElementsByClassName("lockg12");
                var lockg13 = document.getElementsByClassName("lockg13");
                var lockg14 = document.getElementsByClassName("lockg14");
                var lockg15 = document.getElementsByClassName("lockg15");
                var lockg16 = document.getElementsByClassName("lockg16");
                var lockg17 = document.getElementsByClassName("lockg17");
                var lockg18 = document.getElementsByClassName("lockg18");
                var lockg19 = document.getElementsByClassName("lockg19");
                var lockg20 = document.getElementsByClassName("lockg20");
                var lockcw1 = document.getElementsByClassName("lockcw1");
                var lockcw2 = document.getElementsByClassName("lockcw2");
                var lockcw3 = document.getElementsByClassName("lockcw3");
                var lockcw4 = document.getElementsByClassName("lockcw4");
                var lockcw5 = document.getElementsByClassName("lockcw5");
                var lockcw6 = document.getElementsByClassName("lockcw6");
                var lockcw7 = document.getElementsByClassName("lockcw7");
                var lockcw8 = document.getElementsByClassName("lockcw8");
                var lockcw9 = document.getElementsByClassName("lockcw9");
                var lockcw10 = document.getElementsByClassName("lockcw10");
                var lockcw11 = document.getElementsByClassName("lockcw11");
                var lockcw12 = document.getElementsByClassName("lockcw12");
                var lockcw13 = document.getElementsByClassName("lockcw13");
                var lockcw14 = document.getElementsByClassName("lockcw14");
                var lockcw15 = document.getElementsByClassName("lockcw15");
                var lockcw16 = document.getElementsByClassName("lockcw16");
                var lockcw17 = document.getElementsByClassName("lockcw17");
                var lockcw18 = document.getElementsByClassName("lockcw18");
                var lockcw19 = document.getElementsByClassName("lockcw19");
                var lockcw20 = document.getElementsByClassName("lockcw20");
                var lockm1 = document.getElementsByClassName("lockm1");
                var lockm2 = document.getElementsByClassName("lockm2");
                var lockm3 = document.getElementsByClassName("lockm3");
                var lockm4 = document.getElementsByClassName("lockm4");
                var lockm5 = document.getElementsByClassName("lockm5");
                var lockm6 = document.getElementsByClassName("lockm6");
                var lockm7 = document.getElementsByClassName("lockm7");
                var lockm8 = document.getElementsByClassName("lockm8");
                var lockm9 = document.getElementsByClassName("lockm9");
                var lockm10 = document.getElementsByClassName("lockm10");
                var lockf1 = document.getElementsByClassName("lockf1");
                var lockf2 = document.getElementsByClassName("lockf2");
                var lockf3 = document.getElementsByClassName("lockf3");
                var lockf4 = document.getElementsByClassName("lockf4");
                var lockf5 = document.getElementsByClassName("lockf5");
                var lockf6 = document.getElementsByClassName("lockf6");
                var lockf7 = document.getElementsByClassName("lockf7");
                var lockf8 = document.getElementsByClassName("lockf8");
                var lockf9 = document.getElementsByClassName("lockf9");
                var lockf10 = document.getElementsByClassName("lockf10");
                var lockmidterm = document.getElementsByClassName("lockmidterm");
                var lockfinalterm = document.getElementsByClassName("lockfinalterm");
                var maxtest = document.getElementsByClassName("maxtest");
                var maxtestcw = document.getElementsByClassName("maxtestcw");
                var maxmidcw = document.getElementsByClassName("maxmidcw");
                var maxfinalcw = document.getElementsByClassName("maxfinalcw");
                var maxmidscore = document.getElementsByClassName("maxmidscore");
                var maxlatescore = document.getElementsByClassName("maxlatescore");
                var set1name = document.getElementsByClassName("set1name");
                var set2name = document.getElementsByClassName("set2name");
                var set3name = document.getElementsByClassName("set3name");
                var set4name = document.getElementsByClassName("set4name");
                var set5name = document.getElementsByClassName("set5name");
                var set6name = document.getElementsByClassName("set6name");
                var set7name = document.getElementsByClassName("set7name");
                var set8name = document.getElementsByClassName("set8name");
                var set9name = document.getElementsByClassName("set9name");
                var set10name = document.getElementsByClassName("set10name");
                var set11name = document.getElementsByClassName("set11name");
                var set12name = document.getElementsByClassName("set12name");
                var set13name = document.getElementsByClassName("set13name");
                var set14name = document.getElementsByClassName("set14name");
                var set15name = document.getElementsByClassName("set15name");
                var set16name = document.getElementsByClassName("set16name");
                var set17name = document.getElementsByClassName("set17name");
                var set18name = document.getElementsByClassName("set18name");
                var set19name = document.getElementsByClassName("set19name");
                var set20name = document.getElementsByClassName("set20name");
                var set1namecw= document.getElementsByClassName("set1namecw");
                var set2namecw= document.getElementsByClassName("set2namecw");
                var set3namecw= document.getElementsByClassName("set3namecw");
                var set4namecw= document.getElementsByClassName("set4namecw");
                var set5namecw= document.getElementsByClassName("set5namecw");
                var set6namecw= document.getElementsByClassName("set6namecw");
                var set7namecw= document.getElementsByClassName("set7namecw");
                var set8namecw= document.getElementsByClassName("set8namecw");
                var set9namecw= document.getElementsByClassName("set9namecw");
                var set10namecw= document.getElementsByClassName("set10namecw");
                var set11namecw= document.getElementsByClassName("set11namecw");
                var set12namecw= document.getElementsByClassName("set12namecw");
                var set13namecw= document.getElementsByClassName("set13namecw");
                var set14namecw= document.getElementsByClassName("set14namecw");
                var set15namecw= document.getElementsByClassName("set15namecw");
                var set16namecw= document.getElementsByClassName("set16namecw");
                var set17namecw= document.getElementsByClassName("set17namecw");
                var set18namecw= document.getElementsByClassName("set18namecw");
                var set19namecw= document.getElementsByClassName("set19namecw");
                var set20namecw= document.getElementsByClassName("set20namecw");
                var set1midcw = document.getElementsByClassName("set1midcw");
                var set2midcw = document.getElementsByClassName("set2midcw");
                var set3midcw = document.getElementsByClassName("set3midcw");
                var set4midcw = document.getElementsByClassName("set4midcw");
                var set5midcw = document.getElementsByClassName("set5midcw");
                var set6midcw = document.getElementsByClassName("set6midcw");
                var set7midcw = document.getElementsByClassName("set7midcw");
                var set8midcw = document.getElementsByClassName("set8midcw");
                var set9midcw = document.getElementsByClassName("set9midcw");
                var set10midcw = document.getElementsByClassName("set10midcw");
                var set1finalcw = document.getElementsByClassName("set1finalcw");
                var set2finalcw = document.getElementsByClassName("set2finalcw");
                var set3finalcw = document.getElementsByClassName("set3finalcw");
                var set4finalcw = document.getElementsByClassName("set4finalcw");
                var set5finalcw = document.getElementsByClassName("set5finalcw");
                var set6finalcw = document.getElementsByClassName("set6finalcw");
                var set7finalcw = document.getElementsByClassName("set7finalcw");
                var set8finalcw = document.getElementsByClassName("set8finalcw");
                var set9finalcw = document.getElementsByClassName("set9finalcw");
                var set10finalcw = document.getElementsByClassName("set10finalcw");
               
                var disablemid = document.getElementsByClassName("disablemid");
                var disablefinal = document.getElementsByClassName("disablefinal");
                var disableg1 = document.getElementsByClassName("disableg1");
                var disableg2 = document.getElementsByClassName("disableg2");
                var disableg3 = document.getElementsByClassName("disableg3");
                var disableg4 = document.getElementsByClassName("disableg4");
                var disableg5 = document.getElementsByClassName("disableg5");
                var disableg6 = document.getElementsByClassName("disableg6");
                var disableg7 = document.getElementsByClassName("disableg7");
                var disableg8 = document.getElementsByClassName("disableg8");
                var disableg9 = document.getElementsByClassName("disableg9");
                var disableg10 = document.getElementsByClassName("disableg10");
                var disableg11 = document.getElementsByClassName("disableg11");
                var disableg12 = document.getElementsByClassName("disableg12");
                var disableg13 = document.getElementsByClassName("disableg13");
                var disableg14 = document.getElementsByClassName("disableg14");
                var disableg15 = document.getElementsByClassName("disableg15");
                var disableg16 = document.getElementsByClassName("disableg16");
                var disableg17 = document.getElementsByClassName("disableg17");
                var disableg18 = document.getElementsByClassName("disableg18");
                var disableg19 = document.getElementsByClassName("disableg19");
                var disableg20 = document.getElementsByClassName("disableg20");
                var disablecw1 = document.getElementsByClassName("disablecw1");
                var disablecw2 = document.getElementsByClassName("disablecw2");
                var disablecw3 = document.getElementsByClassName("disablecw3");
                var disablecw4 = document.getElementsByClassName("disablecw4");
                var disablecw5 = document.getElementsByClassName("disablecw5");
                var disablecw6 = document.getElementsByClassName("disablecw6");
                var disablecw7 = document.getElementsByClassName("disablecw7");
                var disablecw8 = document.getElementsByClassName("disablecw8");
                var disablecw9 = document.getElementsByClassName("disablecw9");
                var disablecw10 = document.getElementsByClassName("disablecw10");
                var disablecw11 = document.getElementsByClassName("disablecw11");
                var disablecw12 = document.getElementsByClassName("disablecw12");
                var disablecw13 = document.getElementsByClassName("disablecw13");
                var disablecw14 = document.getElementsByClassName("disablecw14");
                var disablecw15 = document.getElementsByClassName("disablecw15");
                var disablecw16 = document.getElementsByClassName("disablecw16");
                var disablecw17 = document.getElementsByClassName("disablecw17");
                var disablecw18 = document.getElementsByClassName("disablecw18");
                var disablecw19 = document.getElementsByClassName("disablecw19");
                var disablecw20 = document.getElementsByClassName("disablecw20");
                var disablemid1 = document.getElementsByClassName("disablemid1");
                var disablemid2 = document.getElementsByClassName("disablemid2");
                var disablemid3 = document.getElementsByClassName("disablemid3");
                var disablemid4 = document.getElementsByClassName("disablemid4");
                var disablemid5 = document.getElementsByClassName("disablemid5");
                var disablemid6 = document.getElementsByClassName("disablemid6");
                var disablemid7 = document.getElementsByClassName("disablemid7");
                var disablemid8 = document.getElementsByClassName("disablemid8");
                var disablemid9 = document.getElementsByClassName("disablemid9");
                var disablemid10 = document.getElementsByClassName("disablemid10");
                var disablefinal1 = document.getElementsByClassName("disablefinal1");
                var disablefinal2 = document.getElementsByClassName("disablefinal2");
                var disablefinal3 = document.getElementsByClassName("disablefinal3");
                var disablefinal4 = document.getElementsByClassName("disablefinal4");
                var disablefinal5 = document.getElementsByClassName("disablefinal5");
                var disablefinal6 = document.getElementsByClassName("disablefinal6");
                var disablefinal7 = document.getElementsByClassName("disablefinal7");
                var disablefinal8 = document.getElementsByClassName("disablefinal8");
                var disablefinal9 = document.getElementsByClassName("disablefinal9");
                var disablefinal10 = document.getElementsByClassName("disablefinal10");

                if (Number(lock1[0].value) < Number(periodnow[0].value) && lock1[0].value != "")
                {
                    var check = calllock(lockg1);
                    if (check == 1 && lock1[0].value == "-1") {
                        for (var x = 0; x < lockg1.length; x++) {                            
                                lockg1[x].classList.add('disable');
                                disableg1[x].classList.add('disable2');                                                    
                        }
                        maxtest[0].classList.add('disable');
                        set1name[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock1[1].value) < Number(periodnow[0].value) && lock1[1].value != "") {
                    var check = calllock(lockg2);
                    if (check == 1 && lock1[1].value == "-1")
                    {
                        for (var x = 0; x < lockg2.length; x++) {                            
                                lockg2[x].classList.add('disable');
                                disableg2[x].classList.add('disable2');                            
                        }
                        maxtest[1].classList.add('disable');
                        set2name[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock1[2].value) < Number(periodnow[0].value) && lock1[2].value != "") {
                    var check = calllock(lockg3);
                    if (check == 1 && lock1[2].value == "-1") {
                        for (var x = 0; x < lockg3.length; x++) {                            
                                lockg3[x].classList.add('disable');
                                disableg3[x].classList.add('disable2');                            
                        }
                        maxtest[2].classList.add('disable');
                        set3name[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock1[3].value) < Number(periodnow[0].value) && lock1[3].value != "") {
                    var check = calllock(lockg4);
                    if (check == 1 && lock1[3].value == "-1") {
                        for (var x = 0; x < lockg4.length; x++) {                            
                                lockg4[x].classList.add('disable');
                                disableg4[x].classList.add('disable2');                            
                        }
                        maxtest[3].classList.add('disable');
                        set4name[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock1[4].value) < Number(periodnow[0].value) && lock1[4].value != "") {
                    var check = calllock(lockg5);
                    if (check == 1 && lock1[4].value == "-1") {
                        for (var x = 0; x < lockg5.length; x++) {
                            lockg5[x].classList.add('disable');
                            disableg5[x].classList.add('disable2');
                        }
                        maxtest[4].classList.add('disable');
                        set5name[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock1[5].value) < Number(periodnow[0].value) && lock1[5].value != "") {
                    var check = calllock(lockg6);
                    if (check == 1 && lock1[5].value == "-1") {
                        for (var x = 0; x < lockg6.length; x++) {
                            lockg6[x].classList.add('disable');
                            disableg6[x].classList.add('disable2');
                        }
                        maxtest[5].classList.add('disable');
                        set6name[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock1[6].value) < Number(periodnow[0].value) && lock1[6].value != "") {
                    var check = calllock(lockg7);
                    if (check == 1 && lock1[6].value == "-1") {
                        for (var x = 0; x < lockg7.length; x++) {
                            lockg7[x].classList.add('disable');
                            disableg7[x].classList.add('disable2');
                        }
                        maxtest[6].classList.add('disable');
                        set7name[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock1[7].value) < Number(periodnow[0].value) && lock1[7].value != "") {
                    var check = calllock(lockg8);
                    if (check == 1 && lock1[7].value == "-1") {
                        for (var x = 0; x < lockg8.length; x++) {
                            lockg8[x].classList.add('disable');
                            disableg8[x].classList.add('disable2');
                        }
                        maxtest[7].classList.add('disable');
                        set8name[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock1[8].value) < Number(periodnow[0].value) && lock1[8].value != "") {
                    var check = calllock(lockg9);
                    if (check == 1 && lock1[8].value == "-1") {
                        for (var x = 0; x < lockg9.length; x++) {
                            lockg9[x].classList.add('disable');
                            disableg9[x].classList.add('disable2');
                        }
                        maxtest[8].classList.add('disable');
                        set9name[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock1[9].value) < Number(periodnow[0].value) && lock1[9].value != "") {
                    var check = calllock(lockg10);
                    if (check == 1 && lock1[9].value == "-1") {
                        for (var x = 0; x < lockg10.length; x++) {
                            lockg10[x].classList.add('disable');
                            disableg10[x].classList.add('disable2');
                        }
                        maxtest[9].classList.add('disable');
                        set10name[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock1[10].value) < Number(periodnow[0].value) && lock1[10].value != "") {
                    var check = calllock(lockg11);
                    if (check == 1 && lock1[10].value == "-1") {
                        for (var x = 0; x < lockg11.length; x++) {
                            lockg11[x].classList.add('disable');
                            disableg11[x].classList.add('disable2');
                        }
                        maxtest[10].classList.add('disable');
                        set11name[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock1[11].value) < Number(periodnow[0].value) && lock1[11].value != "") {
                    var check = calllock(lockg12);
                    if (check == 1 && lock1[11].value == "-1") {
                        for (var x = 0; x < lockg12.length; x++) {
                            lockg12[x].classList.add('disable');
                            disableg12[x].classList.add('disable2');
                        }
                        maxtest[11].classList.add('disable');
                        set12name[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock1[12].value) < Number(periodnow[0].value) && lock1[12].value != "") {
                    var check = calllock(lockg13);
                    if (check == 1 && lock1[12].value == "-1") {
                        for (var x = 0; x < lockg13.length; x++) {
                            lockg13[x].classList.add('disable');
                            disableg13[x].classList.add('disable2');
                        }
                        maxtest[12].classList.add('disable');
                        set13name[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock1[13].value) < Number(periodnow[0].value) && lock1[13].value != "") {
                    var check = calllock(lockg14);
                    if (check == 1 && lock1[13].value == "-1") {
                        for (var x = 0; x < lockg14.length; x++) {                            
                                lockg14[x].classList.add('disable');
                                disableg14[x].classList.add('disable2');                            
                        }
                        maxtest[13].classList.add('disable');
                        set14name[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock1[14].value) < Number(periodnow[0].value) && lock1[14].value != "") {
                    var check = calllock(lockg15);
                    if (check == 1 && lock1[14].value == "-1") {
                        for (var x = 0; x < lockg15.length; x++) {
                                lockg15[x].classList.add('disable');
                                disableg15[x].classList.add('disable2');                            
                        }
                        maxtest[14].classList.add('disable');
                        set15name[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock1[15].value) < Number(periodnow[0].value) && lock1[15].value != "") {
                    var check = calllock(lockg16);
                    if (check == 1 && lock1[15].value == "-1") {
                        for (var x = 0; x < lockg16.length; x++) {
                                lockg16[x].classList.add('disable');
                                disableg16[x].classList.add('disable2');                            
                        }
                        maxtest[15].classList.add('disable');
                        set16name[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock1[16].value) < Number(periodnow[0].value) && lock1[16].value != "") {
                    var check = calllock(lockg17);
                    if (check == 1 && lock1[16].value == "-1") {
                        for (var x = 0; x < lockg17.length; x++) {
                                lockg17[x].classList.add('disable');
                                disableg17[x].classList.add('disable2');                            
                        }
                        maxtest[16].classList.add('disable');
                        set17name[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock1[17].value) < Number(periodnow[0].value) && lock1[17].value != "") {
                    var check = calllock(lockg18);
                    if (check == 1 && lock1[17].value == "-1") {
                        for (var x = 0; x < lockg18.length; x++) {
                            lockg18[x].classList.add('disable');
                            disableg18[x].classList.add('disable2');
                        }
                        maxtest[17].classList.add('disable');
                        set18name[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock1[18].value) < Number(periodnow[0].value) && lock1[18].value != "") {
                    var check = calllock(lockg19);
                    if (check == 1 && lock1[18].value == "-1") {
                        for (var x = 0; x < lockg19.length; x++) {
                            lockg19[x].classList.add('disable');
                            disableg19[x].classList.add('disable2');
                        }
                        maxtest[18].classList.add('disable');
                        set19name[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock1[19].value) < Number(periodnow[0].value) && lock1[19].value != "") {
                    var check = calllock(lockg20);
                    if (check == 1 && lock1[19].value == "-1") {
                        for (var x = 0; x < lockg20.length; x++) {
                            lockg20[x].classList.add('disable');
                            disableg20[x].classList.add('disable2');
                        }
                        maxtest[19].classList.add('disable');
                        set20name[0].classList.add('disable');
                    }
                    
                }
                
                
                if (Number(lock2[0].value) < Number(periodnow[0].value) && lock2[0].value != "") {
                    var check = calllock(lockcw1);
                    if (check == 1 && lock2[0].value == "-1") {
                        for (var x = 0; x < lockcw1.length; x++) {
                            lockcw1[x].classList.add('disable');
                            disablecw1[x].classList.add('disable2');
                        }
                        maxtestcw[0].classList.add('disable');
                        set1namecw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock2[1].value) < Number(periodnow[0].value) && lock2[1].value != "") {
                    var check = calllock(lockcw2);
                    if (check == 1 && lock2[1].value == "-1") {
                        for (var x = 0; x < lockcw2.length; x++) {
                            lockcw2[x].classList.add('disable');
                            disablecw2[x].classList.add('disable2');
                        }
                        maxtestcw[1].classList.add('disable');
                        set2namecw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock2[2].value) < Number(periodnow[0].value) && lock2[2].value != "") {
                    var check = calllock(lockcw3);
                    if (check == 1 && lock2[2].value == "-1") {
                        for (var x = 0; x < lockcw3.length; x++) {
                            lockcw3[x].classList.add('disable');
                            disablecw3[x].classList.add('disable2');
                        }
                        maxtestcw[2].classList.add('disable');
                        set3namecw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock2[3].value) < Number(periodnow[0].value) && lock2[3].value != "") {
                    var check = calllock(lockcw4);
                    if (check == 1 && lock2[3].value == "-1") {
                        for (var x = 0; x < lockcw4.length; x++) {
                            lockcw4[x].classList.add('disable');
                            disablecw4[x].classList.add('disable2');
                        }
                        maxtestcw[3].classList.add('disable');
                        set4namecw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock2[4].value) < Number(periodnow[0].value) && lock2[4].value != "") {
                    var check = calllock(lockcw5);
                    if (check == 1 && lock2[4].value == "-1") {
                        for (var x = 0; x < lockcw5.length; x++) {
                            lockcw5[x].classList.add('disable');
                            disablecw5[x].classList.add('disable2');
                        }
                        maxtestcw[4].classList.add('disable');
                        set5namecw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock2[5].value) < Number(periodnow[0].value) && lock2[5].value != "") {
                    var check = calllock(lockcw6);
                    if (check == 1 && lock2[5].value == "-1") {
                        for (var x = 0; x < lockcw6.length; x++) {                            
                                lockcw6[x].classList.add('disable');
                                disablecw6[x].classList.add('disable2');                            
                        }
                        maxtestcw[5].classList.add('disable');
                        set6namecw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock2[6].value) < Number(periodnow[0].value) && lock2[6].value != "") {
                    var check = calllock(lockcw7);
                    if (check == 1 && lock2[6].value == "-1") {
                        for (var x = 0; x < lockcw7.length; x++) {                           
                                lockcw7[x].classList.add('disable');
                                disablecw7[x].classList.add('disable2');                            
                        }
                        maxtestcw[6].classList.add('disable');
                        set7namecw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock2[7].value) < Number(periodnow[0].value) && lock2[7].value != "") {
                    var check = calllock(lockcw8);
                    if (check == 1 && lock2[7].value == "-1") {
                        for (var x = 0; x < lockcw8.length; x++) {
                                lockcw8[x].classList.add('disable');
                                disablecw8[x].classList.add('disable2');                            
                        }
                        maxtestcw[7].classList.add('disable');
                        set8namecw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock2[8].value) < Number(periodnow[0].value) && lock2[8].value != "") {
                    var check = calllock(lockcw9);
                    if (check == 1 && lock2[8].value == "-1") {
                        for (var x = 0; x < lockcw9.length; x++) {
                                lockcw9[x].classList.add('disable');
                                disablecw9[x].classList.add('disable2');                            
                        }
                        maxtestcw[8].classList.add('disable');
                        set9namecw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock2[9].value) < Number(periodnow[0].value) && lock2[9].value != "") {
                    var check = calllock(lockcw10);
                    if (check == 1 && lock2[9].value == "-1") {
                        for (var x = 0; x < lockcw10.length; x++) {
                                lockcw10[x].classList.add('disable');
                                disablecw10[x].classList.add('disable2');                            
                        }
                        maxtestcw[9].classList.add('disable');
                        set10namecw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock2[10].value) < Number(periodnow[0].value) && lock2[10].value != "") {
                    var check = calllock(lockcw11);
                    if (check == 1 && lock2[10].value == "-1") {
                        for (var x = 0; x < lockcw11.length; x++) {
                            lockcw11[x].classList.add('disable');
                            disablecw11[x].classList.add('disable2');
                        }
                        maxtestcw[10].classList.add('disable');
                        set11namecw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock2[11].value) < Number(periodnow[0].value) && lock2[11].value != "") {
                    var check = calllock(lockcw12);
                    if (check == 1 && lock2[11].value == "-1") {
                        for (var x = 0; x < lockcw12.length; x++) {
                            lockcw12[x].classList.add('disable');
                            disablecw12[x].classList.add('disable2');
                        }
                        maxtestcw[11].classList.add('disable');
                        set12namecw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock2[12].value) < Number(periodnow[0].value) && lock2[12].value != "") {
                    var check = calllock(lockcw13);
                    if (check == 1 && lock2[12].value == "-1") {
                        for (var x = 0; x < lockcw13.length; x++) {
                            lockcw13[x].classList.add('disable');
                            disablecw13[x].classList.add('disable2');
                        }
                        maxtestcw[12].classList.add('disable');
                        set13namecw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock2[13].value) < Number(periodnow[0].value) && lock2[13].value != "") {
                    var check = calllock(lockcw14);
                    if (check == 1 && lock2[13].value == "-1") {
                        for (var x = 0; x < lockcw14.length; x++) {
                            lockcw14[x].classList.add('disable');
                            disablecw14[x].classList.add('disable2');
                        }
                        maxtestcw[13].classList.add('disable');
                        set14namecw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock2[14].value) < Number(periodnow[0].value) && lock2[14].value != "") {
                    var check = calllock(lockcw15);
                    if (check == 1 && lock2[14].value == "-1") {
                        for (var x = 0; x < lockcw15.length; x++) {
                            lockcw15[x].classList.add('disable');
                            disablecw15[x].classList.add('disable2');
                        }
                        maxtestcw[14].classList.add('disable');
                        set15namecw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock2[15].value) < Number(periodnow[0].value) && lock2[15].value != "") {
                    var check = calllock(lockcw16);
                    if (check == 1 && lock2[15].value == "-1") {
                        for (var x = 0; x < lockcw16.length; x++) {
                            lockcw16[x].classList.add('disable');
                            disablecw16[x].classList.add('disable2');
                        }
                        maxtestcw[15].classList.add('disable');
                        set16namecw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock2[16].value) < Number(periodnow[0].value) && lock2[16].value != "") {
                    var check = calllock(lockcw17);
                    if (check == 1 && lock2[16].value == "-1") {
                        for (var x = 0; x < lockcw17.length; x++) {
                            lockcw17[x].classList.add('disable');
                            disablecw17[x].classList.add('disable2');
                        }
                        maxtestcw[16].classList.add('disable');
                        set17namecw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock2[17].value) < Number(periodnow[0].value) && lock2[17].value != "") {
                    var check = calllock(lockcw18);
                    if (check == 1 && lock2[17].value == "-1") {
                        for (var x = 0; x < lockcw18.length; x++) {
                            lockcw18[x].classList.add('disable');
                            disablecw18[x].classList.add('disable2');
                        }
                        maxtestcw[17].classList.add('disable');
                        set18namecw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock2[18].value) < Number(periodnow[0].value) && lock2[18].value != "") {
                    var check = calllock(lockcw19);
                    if (check == 1 && lock2[18].value == "-1") {
                        for (var x = 0; x < lockcw19.length; x++) {
                            lockcw19[x].classList.add('disable');
                            disablecw19[x].classList.add('disable2');
                        }
                        maxtestcw[18].classList.add('disable');
                        set19namecw[0].classList.add('disable');
                    }
                    
                }
                
                if (Number(lock2[19].value) < Number(periodnow[0].value) && lock2[19].value != "") {
                    var check = calllock(lockcw20);
                    if (check == 1 && lock2[19].value == "-1") {
                        for (var x = 0; x < lockcw20.length; x++) {
                            lockcw20[x].classList.add('disable');
                            disablecw20[x].classList.add('disable2');
                        }
                        maxtestcw[19].classList.add('disable');
                        set20namecw[0].classList.add('disable');
                    }
                    
                }

                if (Number(lock3[0].value) < Number(periodnow[0].value) && lock3[0].value != "") {
                    var check = calllock(lockm1);
                    if (check == 1 && lock3[0].value == "-1") {
                        for (var x = 0; x < lockm1.length; x++) {
                            lockm1[x].classList.add('disable');
                            disablemid1[x].classList.add('disable2');
                        }
                        maxmidcw[0].classList.add('disable');
                        set1midcw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock3[1].value) < Number(periodnow[0].value) && lock3[1].value != "") {
                    var check = calllock(lockm2);
                    if (check == 1 && lock3[1].value == "-1") {
                        for (var x = 0; x < lockm2.length; x++) {
                            lockm2[x].classList.add('disable');
                            disablemid2[x].classList.add('disable2');
                        }
                        maxmidcw[1].classList.add('disable');
                        set2midcw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock3[2].value) < Number(periodnow[0].value) && lock3[2].value != "") {
                    var check = calllock(lockm3);
                    if (check == 1 && lock3[2].value == "-1") {
                        for (var x = 0; x < lockm3.length; x++) {
                            lockm3[x].classList.add('disable');
                            disablemid3[x].classList.add('disable2');
                        }
                        maxmidcw[2].classList.add('disable');
                        set3midcw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock3[3].value) < Number(periodnow[0].value) && lock3[3].value != "") {
                    var check = calllock(lockm4);
                    if (check == 1 && lock3[3].value == "-1") {
                        for (var x = 0; x < lockm4.length; x++) {
                            lockm4[x].classList.add('disable');
                            disablemid4[x].classList.add('disable2');
                        }
                        maxmidcw[3].classList.add('disable');
                        set4midcw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock3[4].value) < Number(periodnow[0].value) && lock3[4].value != "") {
                    var check = calllock(lockm5);
                    if (check == 1 && lock3[4].value == "-1") {
                        for (var x = 0; x < lockm5.length; x++) {
                            lockm5[x].classList.add('disable');
                            disablemid5[x].classList.add('disable2');
                        }
                        maxmidcw[4].classList.add('disable');
                        set5midcw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock3[5].value) < Number(periodnow[0].value) && lock3[5].value != "") {
                    var check = calllock(lockm6);
                    if (check == 1 && lock3[5].value == "-1") {
                        for (var x = 0; x < lockm6.length; x++) {
                            lockm6[x].classList.add('disable');
                            disablemid6[x].classList.add('disable2');
                        }
                        maxmidcw[5].classList.add('disable');
                        set6midcw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock3[6].value) < Number(periodnow[0].value) && lock3[6].value != "") {
                    var check = calllock(lockm7);
                    if (check == 1 && lock3[6].value == "-1") {
                        for (var x = 0; x < lockm7.length; x++) {
                            lockm7[x].classList.add('disable');
                            disablemid7[x].classList.add('disable2');
                        }
                        maxmidcw[6].classList.add('disable');
                        set7midcw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock3[7].value) < Number(periodnow[0].value) && lock3[7].value != "") {
                    var check = calllock(lockm8);
                    if (check == 1 && lock3[7].value == "-1") {
                        for (var x = 0; x < lockm8.length; x++) {
                            lockm8[x].classList.add('disable');
                            disablemid8[x].classList.add('disable2');
                        }
                        maxmidcw[7].classList.add('disable');
                        set8midcw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock3[8].value) < Number(periodnow[0].value) && lock3[8].value != "") {
                    var check = calllock(lockm9);
                    if (check == 1 && lock3[8].value == "-1") {
                        for (var x = 0; x < lockm9.length; x++) {
                            lockm9[x].classList.add('disable');
                            disablemid9[x].classList.add('disable2');
                        }
                        maxmidcw[8].classList.add('disable');
                        set9midcw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock3[9].value) < Number(periodnow[0].value) && lock3[9].value != "") {
                    var check = calllock(lockm10);
                    if (check == 1 && lock3[9].value == "-1") {
                        for (var x = 0; x < lockm10.length; x++) {
                            lockm10[x].classList.add('disable');
                            disablemid10[x].classList.add('disable2');
                        }
                        maxmidcw[9].classList.add('disable');
                        set10midcw[0].classList.add('disable');
                    }
                    
                }

                if (Number(lock4[0].value) < Number(periodnow[0].value) && lock4[0].value != "") {
                    var check = calllock(lockf1);
                    if (check == 1 && lock4[0].value == "-1") {
                        for (var x = 0; x < lockf1.length; x++) {
                            lockf1[x].classList.add('disable');
                            disablefinal1[x].classList.add('disable2');
                        }
                        maxfinalcw[0].classList.add('disable');
                        set1finalcw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock4[1].value) < Number(periodnow[0].value) && lock4[1].value != "") {
                    var check = calllock(lockf2);
                    if (check == 1 && lock4[1].value == "-1") {
                        for (var x = 0; x < lockf2.length; x++) {
                            lockf2[x].classList.add('disable');
                            disablefinal2[x].classList.add('disable2');
                        }
                        maxfinalcw[1].classList.add('disable');
                        set2finalcw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock4[2].value) < Number(periodnow[0].value) && lock4[2].value != "") {
                    var check = calllock(lockf3);
                    if (check == 1 && lock4[2].value == "-1") {
                        for (var x = 0; x < lockf3.length; x++) {
                            lockf3[x].classList.add('disable');
                            disablefinal3[x].classList.add('disable2');
                        }
                        maxfinalcw[2].classList.add('disable');
                        set3finalcw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock4[3].value) < Number(periodnow[0].value) && lock4[3].value != "") {
                    var check = calllock(lockf4);
                    if (check == 1 && lock4[3].value == "-1") {
                        for (var x = 0; x < lockf4.length; x++) {
                            lockf4[x].classList.add('disable');
                            disablefinal4[x].classList.add('disable2');
                        }
                        maxfinalcw[3].classList.add('disable');
                        set4finalcw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock4[4].value) < Number(periodnow[0].value) && lock4[4].value != "") {
                    var check = calllock(lockf5);
                    if (check == 1 && lock4[4].value == "-1") {
                        for (var x = 0; x < lockf5.length; x++) {
                            lockf5[x].classList.add('disable');
                            disablefinal5[x].classList.add('disable2');
                        }
                        maxfinalcw[4].classList.add('disable');
                        set5finalcw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock4[5].value) < Number(periodnow[0].value) && lock4[5].value != "") {
                    var check = calllock(lockf6);
                    if (check == 1 && lock4[5].value == "-1") {
                        for (var x = 0; x < lockf6.length; x++) {
                            lockf6[x].classList.add('disable');
                            disablefinal6[x].classList.add('disable2');
                        }
                        maxfinalcw[5].classList.add('disable');
                        set6finalcw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock4[6].value) < Number(periodnow[0].value) && lock4[6].value != "") {
                    var check = calllock(lockf7);
                    if (check == 1 && lock4[6].value == "-1") {
                        for (var x = 0; x < lockf7.length; x++) {
                            lockf7[x].classList.add('disable');
                            disablefinal7[x].classList.add('disable2');
                        }
                        maxfinalcw[6].classList.add('disable');
                        set7finalcw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock4[7].value) < Number(periodnow[0].value) && lock4[7].value != "") {
                    var check = calllock(lockf8);
                    if (check == 1 && lock4[7].value == "-1") {
                        for (var x = 0; x < lockf8.length; x++) {
                            lockf8[x].classList.add('disable');
                            disablefinal8[x].classList.add('disable2');
                        }
                        maxfinalcw[7].classList.add('disable');
                        set8finalcw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock4[8].value) < Number(periodnow[0].value) && lock4[8].value != "") {
                    var check = calllock(lockf9);
                    if (check == 1 && lock4[8].value == "-1") {
                        for (var x = 0; x < lockf9.length; x++) {
                            lockf9[x].classList.add('disable');
                            disablefinal9[x].classList.add('disable2');
                        }
                        maxfinalcw[8].classList.add('disable');
                        set9finalcw[0].classList.add('disable');
                    }
                    
                }
                if (Number(lock4[9].value) < Number(periodnow[0].value) && lock4[9].value != "") {
                    var check = calllock(lockf10);
                    if (check == 1 && lock4[9].value == "-1") {
                        for (var x = 0; x < lockf10.length; x++) {
                            lockf10[x].classList.add('disable');
                            disablefinal10[x].classList.add('disable2');
                        }
                        maxfinalcw[9].classList.add('disable');
                        set10finalcw[0].classList.add('disable');
                    }
                    
                }

                if (Number(lockmid[0].value) < Number(periodnow[0].value) && lockmid[0].value != "") {
                    var check = calllock(lockmidterm);
                    if (check == 1 && lockmid[0].value == "-1") {
                        for (var x = 0; x < lockmidterm.length; x++) {
                            lockmidterm[x].classList.add('disable');
                            disablemid[x].classList.add('disable2');
                        }
                        maxmidscore[0].classList.add('disable');
                    }
                    
                }
                if (Number(lockfinal[0].value) < Number(periodnow[0].value) && lockfinal[0].value != "") {
                    var check = calllock(lockfinalterm);
                    if (check == 1 && lockfinal[0].value == "-1") {
                        for (var x = 0; x < lockfinalterm.length; x++) {
                            lockfinalterm[x].classList.add('disable');
                            disablefinal[x].classList.add('disable2');
                        }
                        maxlatescore[0].classList.add('disable');
                    }
                    
                }
                              
            }

            function print(id) {
               
                
                var full = window.location.href;
                var half = full.split('?');
                var url = full.split('.aspx');
                var split = half[1].split('&');
                var year = split[0].split('=');
                var idlv = split[1];
                var idlv2 = split[2];
                var term = split[3].split('=');
                var id2 = split[4].split('=');
                var mode = split[5].split('=');
                
                var loc = "";

                if (id == "1")
                {
                    loc = url[0] + "iFrame.aspx?" + half[1] + "&print=1";
                    document.getElementById('list1').src = loc;
                }
                if (id == "2")
                {
                    loc = url[0] + "iFrame.aspx?" + half[1] + "&print=2";
                    document.getElementById('list2').src = loc;
                }
                if (id == "3")
                {
                    loc = url[0] + "iFrame.aspx?" + half[1] + "&print=3";
                    document.getElementById('list3').src = loc;
                }
                if (id == "4")
                {
                    loc = url[0] + "iFrame.aspx?" + half[1] + "&print=4";
                    document.getElementById('list4').src = loc;
                }
                if (id == "5")
                {
                    loc = url[0] + "iFrame.aspx?" + half[1] + "&print=5";
                    document.getElementById('list5').src = loc;
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
                var viplogin = document.getElementsByClassName("viplogin");

                if (viplogin[0].value != "2")
                {
                    if (id == "9999") {
                        btnok[0].classList.remove('hidden');
                        btnerror[0].classList.add('hidden');
                    }
                    else {
                        //btnok[0].classList.add('hidden');
                        //btnerror[0].classList.remove('hidden');
                    }
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
                var behavesid = document.getElementsByClassName("behavesid");
                var samatscore = document.getElementsByClassName("samatscore");
                if(id == "1")
                {
                    for (var i = 0; i < goodbe.length; i++) {
                        goodbe[i].value = "";                        
                    }
                    for (var i = 0; i < behavesid.length; i++) {
                        goodbe[i].value = "";
                        behavetxt1[i].value = "";
                        behavetxt2[i].value = "";
                        behavetxt3[i].value = "";
                        behavetxt4[i].value = "";
                        behavetxt5[i].value = "";
                        behavetxt6[i].value = "";
                        behavetxt7[i].value = "";
                        behavetxt8[i].value = "";
                        behavetxt9[i].value = "";
                        behavetxt10[i].value = "";
                    }
                }
                else if (id == "2")
                {
                    for (var i = 0; i < readscore.length; i++) {
                        readscore[i].value = "";
                    }
                }
                else if(id == "3")
                {
                    for (var i = 0; i < readscore.length; i++) {
                        samatscore[i].value = "";
                    }
                }
                

            }

            function calmaxmid() {

                var maxmidcw = document.getElementsByClassName("maxmidcw");
                var maxmidscore = document.getElementsByClassName("maxmidscore");
                
                maxmidscore[0].value = 
                    Number(maxmidcw[0].value) + Number(maxmidcw[1].value) +
                    Number(maxmidcw[2].value) + Number(maxmidcw[3].value) +
                    Number(maxmidcw[4].value) + Number(maxmidcw[5].value) +
                    Number(maxmidcw[6].value) + Number(maxmidcw[7].value) +
                    Number(maxmidcw[8].value) + Number(maxmidcw[9].value);


            }

            function calmaxfinal() {

                var maxfinalcw = document.getElementsByClassName("maxfinalcw");
                var maxfinalscore = document.getElementsByClassName("maxlatescore");

                maxfinalscore[0].value =
                    Number(maxfinalcw[0].value) + Number(maxfinalcw[1].value) +
                    Number(maxfinalcw[2].value) + Number(maxfinalcw[3].value) +
                    Number(maxfinalcw[4].value) + Number(maxfinalcw[5].value) +
                    Number(maxfinalcw[6].value) + Number(maxfinalcw[7].value) +
                    Number(maxfinalcw[8].value) + Number(maxfinalcw[9].value);
            }

            function calmidscore(index) {

                index = index - 1;
                
                var midscore1 = document.getElementsByClassName("midscore1");
                var midscore2 = document.getElementsByClassName("midscore2");
                var midscore3 = document.getElementsByClassName("midscore3");
                var midscore4 = document.getElementsByClassName("midscore4");
                var midscore5 = document.getElementsByClassName("midscore5");
                var midscore6 = document.getElementsByClassName("midscore6");
                var midscore7 = document.getElementsByClassName("midscore7");
                var midscore8 = document.getElementsByClassName("midscore8");
                var midscore9 = document.getElementsByClassName("midscore9");
                var midscore10 = document.getElementsByClassName("midscore10");
                var summidbox = document.getElementsByClassName("summidbox");

                summidbox[index].value =
                    Number(midscore1[index].value) + Number(midscore6[index].value) +
                    Number(midscore2[index].value) + Number(midscore7[index].value) +
                    Number(midscore3[index].value) + Number(midscore8[index].value) +
                    Number(midscore4[index].value) + Number(midscore9[index].value) +
                    Number(midscore5[index].value) + Number(midscore10[index].value);
                index = index + 1;
                CompareDates(index);
            }

            function calfinalscore(index) {

                index = index - 1;

                var finalscore1 = document.getElementsByClassName("finalscore1");
                var finalscore2 = document.getElementsByClassName("finalscore2");
                var finalscore3 = document.getElementsByClassName("finalscore3");
                var finalscore4 = document.getElementsByClassName("finalscore4");
                var finalscore5 = document.getElementsByClassName("finalscore5");
                var finalscore6 = document.getElementsByClassName("finalscore6");
                var finalscore7 = document.getElementsByClassName("finalscore7");
                var finalscore8 = document.getElementsByClassName("finalscore8");
                var finalscore9 = document.getElementsByClassName("finalscore9");
                var finalscore10 = document.getElementsByClassName("finalscore10");
                var sumfinalbox = document.getElementsByClassName("sumfinalbox");

                sumfinalbox[index].value =
                    Number(finalscore1[index].value) + Number(finalscore6[index].value) +
                    Number(finalscore2[index].value) + Number(finalscore7[index].value) +
                    Number(finalscore3[index].value) + Number(finalscore8[index].value) +
                    Number(finalscore4[index].value) + Number(finalscore9[index].value) +
                    Number(finalscore5[index].value) + Number(finalscore10[index].value);
                index = index + 1;
                CompareDates(index);
            }

            function nextbutton(id) {

                var left1 = document.getElementsByClassName("left1");
                var left2 = document.getElementsByClassName("left2");
                var left3 = document.getElementsByClassName("left3");
                var left4 = document.getElementsByClassName("left4");
                var left5 = document.getElementsByClassName("left5");
                var left6 = document.getElementsByClassName("left6");
                var left7 = document.getElementsByClassName("left7");
                var left8 = document.getElementsByClassName("left8");
                var left9 = document.getElementsByClassName("left9");
                var right1 = document.getElementsByClassName("right1");
                var right2 = document.getElementsByClassName("right2");
                var right3 = document.getElementsByClassName("right3");
                var right4 = document.getElementsByClassName("right4");
                var right5 = document.getElementsByClassName("right5");
                var right6 = document.getElementsByClassName("right6");
                var right7 = document.getElementsByClassName("right7");
                var right8 = document.getElementsByClassName("right8");
                var right9 = document.getElementsByClassName("right9");
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
                left3[0].classList.remove('active');
                left4[0].classList.remove('active');
                left5[0].classList.remove('active');
                left6[0].classList.remove('active');
                left7[0].classList.remove('active');
                left8[0].classList.remove('active');
                left9[0].classList.remove('active');
                right1[0].classList.remove('active');
                right2[0].classList.remove('active');
                right3[0].classList.remove('active');
                right4[0].classList.remove('active');
                right5[0].classList.remove('active');
                right6[0].classList.remove('active');
                right7[0].classList.remove('active');
                right8[0].classList.remove('active');
                right9[0].classList.remove('active');

                left1[0].classList.add('hidden');
                left2[0].classList.add('hidden');
                left3[0].classList.add('hidden');
                left4[0].classList.add('hidden');
                left5[0].classList.add('hidden');
                left6[0].classList.add('hidden');
                left7[0].classList.add('hidden');
                left8[0].classList.add('hidden');
                left9[0].classList.add('hidden');
                right1[0].classList.add('hidden');
                right2[0].classList.add('hidden');
                right3[0].classList.add('hidden');
                right4[0].classList.add('hidden');
                right5[0].classList.add('hidden');
                right6[0].classList.add('hidden');
                right7[0].classList.add('hidden');
                right8[0].classList.add('hidden');
                right9[0].classList.add('hidden');
                
                
                if(id == "1")
                {
                    
                    if (mode[1] != "EN")
                        scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132261") %>";
                    else scorebox[0].value = "Exercise 1-5";
                    right1[0].classList.remove('hidden');                                 
                }
                else if(id == "2")
                {
                    if (mode[1] != "EN")
                        scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132262") %>";
                    else scorebox[0].value = "Exercise 6-10";
                    
                    left1[0].classList.remove('hidden');                    
                    right2[0].classList.remove('hidden');                                       
                }
                else if (id == "3")
                {
                    if (mode[1] != "EN")
                        scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132263") %>";
                    else scorebox[0].value = "Exercise 11-15";
                                       
                    left2[0].classList.remove('hidden');                    
                    right1[0].classList.remove('hidden');                                   
                }
                else if (id == "4")
                {
                    if (mode[1] != "EN")
                        scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132262") %>";
                    else scorebox[0].value = "Exercise 6-10";
                    
                    left1[0].classList.remove('hidden');                    
                    right2[0].classList.remove('hidden');                                     
                }
                else if (id == "5")
                {
                    if (mode[1] != "EN")
                        scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132263") %>";
                    else scorebox[0].value = "Exercise 11-15";
                                    
                    left2[0].classList.remove('hidden');                    
                    right3[0].classList.remove('hidden');                    
                }
                else if (id== "6")
                {
                    if (mode[1] != "EN")
                        scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132264") %>";
                    else scorebox[0].value = "Exercise 16-20";
                                        
                    left3[0].classList.remove('hidden');                    
                }
                else if(id == "7")
                {
                    if (mode[1] != "EN")
                        scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132281") %>";
                    else scorebox[0].value = "Behavior 1-5";
                    
                    right4[0].classList.remove('hidden');
                }
                else if (id == "8")
                {
                    if (mode[1] != "EN")
                        scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132282") %>";
                    else scorebox[0].value = "Behavior 6-10";
                    
                    left4[0].classList.remove('hidden');
                }
                else if (id == "9") {
                    if (mode[1] != "EN")
                        scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132265") %>";
                    else scorebox[0].value = "Exercise 21-25";
                    
                    right5[0].classList.remove('hidden');
                }
                else if (id == "10") {
                    if (mode[1] != "EN")
                        scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132266") %>";
                    else scorebox[0].value = "Exercise 26-30";
                    
                    left5[0].classList.remove('hidden');
                    right6[0].classList.remove('hidden');
                }
                else if (id == "11") {
                    if (mode[1] != "EN")
                        scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132267") %>";
                    else scorebox[0].value = "Exercise 31-35";
                    
                    left6[0].classList.remove('hidden');
                    right5[0].classList.remove('hidden');
                }
                else if (id == "12") {
                    if (mode[1] != "EN")
                        scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132266") %>";
                    else scorebox[0].value = "Exercise 26-30";
                    
                    left5[0].classList.remove('hidden');
                    right6[0].classList.remove('hidden');
                }
                else if (id == "13") {
                    if (mode[1] != "EN")
                        scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132267") %>";
                    else scorebox[0].value = "Exercise 31-35";
                    
                    left6[0].classList.remove('hidden');
                    right7[0].classList.remove('hidden');
                }
                else if (id == "14") {
                    if (mode[1] != "EN")
                        scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132268") %>";
                    else scorebox[0].value = "Exercise 36-40";
                    
                    left7[0].classList.remove('hidden');
                }
                else if (id == "21") {
                    if (mode[1] != "EN")
                        scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132283") %>";
                    else scorebox[0].value = "Mid Term 1-5";

                    right8[0].classList.remove('hidden');
                }
                else if (id == "22") {
                    if (mode[1] != "EN")
                        scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132285") %>";
                    else scorebox[0].value = "Final Term 1-5";

                    right9[0].classList.remove('hidden');
                }
                else if (id == "23") {
                    if (mode[1] != "EN")
                        scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132284") %>";
                    else scorebox[0].value = "Mid Term 6-10";

                    left8[0].classList.remove('hidden');
                }
                else if (id == "24") {
                    if (mode[1] != "EN")
                        scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132286") %>";
                    else scorebox[0].value = "Final Term 6-10";

                    left9[0].classList.remove('hidden');
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
                var midname1 = document.getElementsByClassName("testmid1name");
                var midname2 = document.getElementsByClassName("testmid2name");
                var midname3 = document.getElementsByClassName("testmid3name");
                var midname4 = document.getElementsByClassName("testmid4name");
                var midname5 = document.getElementsByClassName("testmid5name");
                var midname6 = document.getElementsByClassName("testmid6name");
                var midname7 = document.getElementsByClassName("testmid7name");
                var midname8 = document.getElementsByClassName("testmid8name");
                var midname9 = document.getElementsByClassName("testmid9name");
                var midname10 = document.getElementsByClassName("testmid10name");
                var finalname1 = document.getElementsByClassName("testfinal1name");
                var finalname2 = document.getElementsByClassName("testfinal2name");
                var finalname3 = document.getElementsByClassName("testfinal3name");
                var finalname4 = document.getElementsByClassName("testfinal4name");
                var finalname5 = document.getElementsByClassName("testfinal5name");
                var finalname6 = document.getElementsByClassName("testfinal6name");
                var finalname7 = document.getElementsByClassName("testfinal7name");
                var finalname8 = document.getElementsByClassName("testfinal8name");
                var finalname9 = document.getElementsByClassName("testfinal9name");
                var finalname10 = document.getElementsByClassName("testfinal10name");
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
                var setmid1 = document.getElementsByClassName("set1midcw");
                var setmid2 = document.getElementsByClassName("set2midcw");
                var setmid3 = document.getElementsByClassName("set3midcw");
                var setmid4 = document.getElementsByClassName("set4midcw");
                var setmid5 = document.getElementsByClassName("set5midcw");
                var setmid6 = document.getElementsByClassName("set6midcw");
                var setmid7 = document.getElementsByClassName("set7midcw");
                var setmid8 = document.getElementsByClassName("set8midcw");
                var setmid9 = document.getElementsByClassName("set9midcw");
                var setmid10 = document.getElementsByClassName("set10midcw");
                var setfinal1 = document.getElementsByClassName("set1finalcw");
                var setfinal2 = document.getElementsByClassName("set2finalcw");
                var setfinal3 = document.getElementsByClassName("set3finalcw");
                var setfinal4 = document.getElementsByClassName("set4finalcw");
                var setfinal5 = document.getElementsByClassName("set5finalcw");
                var setfinal6 = document.getElementsByClassName("set6finalcw");
                var setfinal7 = document.getElementsByClassName("set7finalcw");
                var setfinal8 = document.getElementsByClassName("set8finalcw");
                var setfinal9 = document.getElementsByClassName("set9finalcw");
                var setfinal10 = document.getElementsByClassName("set10finalcw");
                
                midname1[0].value = setmid1[0].value;
                midname2[0].value = setmid2[0].value;
                midname3[0].value = setmid3[0].value;
                midname4[0].value = setmid4[0].value;
                midname5[0].value = setmid5[0].value;
                midname6[0].value = setmid6[0].value;
                midname7[0].value = setmid7[0].value;
                midname8[0].value = setmid8[0].value;
                midname9[0].value = setmid9[0].value;
                midname10[0].value = setmid10[0].value;
                finalname1[0].value = setfinal1[0].value;
                finalname2[0].value = setfinal2[0].value;
                finalname3[0].value = setfinal3[0].value;
                finalname4[0].value = setfinal4[0].value;
                finalname5[0].value = setfinal5[0].value;
                finalname6[0].value = setfinal6[0].value;
                finalname7[0].value = setfinal7[0].value;
                finalname8[0].value = setfinal8[0].value;
                finalname9[0].value = setfinal9[0].value;
                finalname10[0].value = setfinal10[0].value;

                nameb1[0].value = setb1[0].value;
                nameb2[0].value = setb2[0].value;
                nameb3[0].value = setb3[0].value;
                nameb4[0].value = setb4[0].value;
                nameb5[0].value = setb5[0].value;
                nameb6[0].value = setb6[0].value;
                nameb7[0].value = setb7[0].value;
                nameb8[0].value = setb8[0].value;
                nameb9[0].value = setb9[0].value;
                nameb10[0].value = setb10[0].value;
                
                name1[0].value = set1[0].value;
                name2[0].value = set2[0].value;
                name3[0].value = set3[0].value;
                name4[0].value = set4[0].value;
                name5[0].value = set5[0].value;
                name6[0].value = set6[0].value;
                name7[0].value = set7[0].value;
                name8[0].value = set8[0].value;
                name9[0].value = set9[0].value;
                name10[0].value = set10[0].value;
                name11[0].value = set11[0].value;
                name12[0].value = set12[0].value;
                name13[0].value = set13[0].value;
                name14[0].value = set14[0].value;
                name15[0].value = set15[0].value;
                name16[0].value = set16[0].value;
                name17[0].value = set17[0].value;
                name18[0].value = set18[0].value;
                name19[0].value = set19[0].value;
                name20[0].value = set20[0].value;

                namecw1[0].value = setcw1[0].value;
                namecw2[0].value = setcw2[0].value;
                namecw3[0].value = setcw3[0].value;
                namecw4[0].value = setcw4[0].value;
                namecw5[0].value = setcw5[0].value;
                namecw6[0].value = setcw6[0].value;
                namecw7[0].value = setcw7[0].value;
                namecw8[0].value = setcw8[0].value;
                namecw9[0].value = setcw9[0].value;
                namecw10[0].value = setcw10[0].value;
                namecw11[0].value = setcw11[0].value;
                namecw12[0].value = setcw12[0].value;
                namecw13[0].value = setcw13[0].value;
                namecw14[0].value = setcw14[0].value;
                namecw15[0].value = setcw15[0].value;
                namecw16[0].value = setcw16[0].value;
                namecw17[0].value = setcw17[0].value;
                namecw18[0].value = setcw18[0].value;
                namecw19[0].value = setcw19[0].value;
                namecw20[0].value = setcw20[0].value;
                
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
               
                var open = document.getElementsByClassName("protip-show");
                
                // Shows tooltip with title: "My new title"
                if (id == 1) {
                    el1.protipShow({
                        title: set1[0].value,
                        trigger: 'hover'
                    });
                    open[0].classList.remove('protip-show');
                }

                else if (id == 2) {
                    el2.protipShow({
                        title: set2[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 3) {
                    el3.protipShow({
                        title: set3[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 4) {
                    el4.protipShow({
                        title: set4[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 5) {
                    el5.protipShow({
                        title: set5[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 6) {
                    el6.protipShow({
                        title: set6[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 7) {
                    el7.protipShow({
                        title: set7[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 8) {
                    el8.protipShow({
                        title: set8[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 9) {
                    el9.protipShow({
                        title: set9[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 10) {
                    el10.protipShow({
                        title: set10[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 11) {
                    el11.protipShow({
                        title: set11[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 12) {
                    el12.protipShow({
                        title: set12[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 13) {
                    el13.protipShow({
                        title: set13[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 14) {
                    el14.protipShow({
                        title: set14[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 15) {
                    el15.protipShow({
                        title: set15[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 16) {
                    el16.protipShow({
                        title: set16[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 17) {
                    el17.protipShow({
                        title: set17[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 18) {
                    el18.protipShow({
                        title: set18[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 19) {
                    el19.protipShow({
                        title: set19[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 20) {
                    el20.protipShow({
                        title: set20[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 41) {
                    elb1.protipShow({
                        title: setb1[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 42) {
                    elb2.protipShow({
                        title: setb2[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 43) {
                    elb3.protipShow({
                        title: setb3[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 44) {
                    elb4.protipShow({
                        title: setb4[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 45) {
                    elb5.protipShow({
                        title: setb5[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 46) {
                    elb6.protipShow({
                        title: setb6[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 47) {
                    elb7.protipShow({
                        title: setb7[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 48) {
                    elb8.protipShow({
                        title: setb8[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 49) {
                    elb9.protipShow({
                        title: setb9[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 50) {
                    elb10.protipShow({
                        title: setb10[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 21) {
                    elc1.protipShow({
                        title: setcw1[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 22) {
                    elc2.protipShow({
                        title: setcw2[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 23) {
                    elc3.protipShow({
                        title: setcw3[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 24) {
                    elc4.protipShow({
                        title: setcw4[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 25) {
                    elc5.protipShow({
                        title: setcw5[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 26) {
                    elc6.protipShow({
                        title: setcw6[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 27) {
                    elc7.protipShow({
                        title: setcw7[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 28) {
                    elc8.protipShow({
                        title: setcw8[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 29) {
                    elc9.protipShow({
                        title: setcw9[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 30) {
                    elc10.protipShow({
                        title: setcw10[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 31) {
                    elc11.protipShow({
                        title: setcw11[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 32) {
                    elc12.protipShow({
                        title: setcw12[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 33) {
                    elc13.protipShow({
                        title: setcw13[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 34) {
                    elc14.protipShow({
                        title: setcw14[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 35) {
                    elc15.protipShow({
                        title: setcw15[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 36) {
                    elc16.protipShow({
                        title: setcw16[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 37) {
                    elc17.protipShow({
                        title: setcw17[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 38) {
                    elc18.protipShow({
                        title: setcw18[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 39) {
                    elc19.protipShow({
                        title: setcw19[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }
                else if (id == 40) {
                    elc20.protipShow({
                        title: setcw20[0].value,
                        trigger: 'hover'
                    }); open[0].classList.remove('protip-show');
                }

                
            }

            function changeddl() {

                var ddl1 = document.getElementsByClassName("ddl1");
                var ddl2 = document.getElementsByClassName("ddl2");
                var ddl3 = document.getElementsByClassName("ddl3");
                var ddl4 = document.getElementsByClassName("ddl4");
                var ddl1set = document.getElementsByClassName("ddl1set");
                var ddl2set = document.getElementsByClassName("ddl2set");
                var ddl3set = document.getElementsByClassName("ddl3set");
                var ddl4set = document.getElementsByClassName("ddl4set");
                
                
                if (Number(ddl1set[0].value) == 0) ddl1[0].selectedIndex = 1;
                if (Number(ddl1set[0].value) == 5) ddl1[0].selectedIndex = 2;
                if (Number(ddl1set[0].value) == 10) ddl1[0].selectedIndex = 3;
                if (Number(ddl1set[0].value) == 15) ddl1[0].selectedIndex = 4;
                if (Number(ddl1set[0].value) == 20) ddl1[0].selectedIndex = 5;
                if (Number(ddl1set[0].value) == 25) ddl1[0].selectedIndex = 6;
                if (Number(ddl1set[0].value) == 30) ddl1[0].selectedIndex = 7;
                if (Number(ddl1set[0].value) == 35) ddl1[0].selectedIndex = 8;
                if (Number(ddl1set[0].value) == 40) ddl1[0].selectedIndex = 9;
                if (Number(ddl1set[0].value) == 45) ddl1[0].selectedIndex = 10;
                if (Number(ddl1set[0].value) == 50) ddl1[0].selectedIndex = 11;
                if (Number(ddl1set[0].value) == 55) ddl1[0].selectedIndex = 12;
                if (Number(ddl1set[0].value) == 60) ddl1[0].selectedIndex = 13;
                if (Number(ddl1set[0].value) == 65) ddl1[0].selectedIndex = 14;
                if (Number(ddl1set[0].value) == 70) ddl1[0].selectedIndex = 15;
                if (Number(ddl1set[0].value) == 75) ddl1[0].selectedIndex = 16;
                if (Number(ddl1set[0].value) == 80) ddl1[0].selectedIndex = 17;
                if (Number(ddl1set[0].value) == 85) ddl1[0].selectedIndex = 18;
                if (Number(ddl1set[0].value) == 90) ddl1[0].selectedIndex = 19;
                if (Number(ddl1set[0].value) == 95) ddl1[0].selectedIndex = 20;
                if (Number(ddl1set[0].value) == 100) ddl1[0].selectedIndex = 21;
           
                
                if (Number(ddl2set[0].value) == 0) ddl2[0].selectedIndex = 1;
                if (Number(ddl2set[0].value) == 5) ddl2[0].selectedIndex = 2;
                if (Number(ddl2set[0].value) == 10) ddl2[0].selectedIndex = 3;
                if (Number(ddl2set[0].value) == 15) ddl2[0].selectedIndex = 4;
                if (Number(ddl2set[0].value) == 20) ddl2[0].selectedIndex = 5;
                if (Number(ddl2set[0].value) == 25) ddl2[0].selectedIndex = 6;
                if (Number(ddl2set[0].value) == 30) ddl2[0].selectedIndex = 7;
                if (Number(ddl2set[0].value) == 35) ddl2[0].selectedIndex = 8;
                if (Number(ddl2set[0].value) == 40) ddl2[0].selectedIndex = 9;
                if (Number(ddl2set[0].value) == 45) ddl2[0].selectedIndex = 10;
                if (Number(ddl2set[0].value) == 50) ddl2[0].selectedIndex = 11;
                if (Number(ddl2set[0].value) == 55) ddl2[0].selectedIndex = 12;
                if (Number(ddl2set[0].value) == 60) ddl2[0].selectedIndex = 13;
                if (Number(ddl2set[0].value) == 65) ddl2[0].selectedIndex = 14;
                if (Number(ddl2set[0].value) == 70) ddl2[0].selectedIndex = 15;
                if (Number(ddl2set[0].value) == 75) ddl2[0].selectedIndex = 16;
                if (Number(ddl2set[0].value) == 80) ddl2[0].selectedIndex = 17;
                if (Number(ddl2set[0].value) == 85) ddl2[0].selectedIndex = 18;
                if (Number(ddl2set[0].value) == 90) ddl2[0].selectedIndex = 19;
                if (Number(ddl2set[0].value) == 95) ddl2[0].selectedIndex = 20;
                if (Number(ddl2set[0].value) == 100) ddl2[0].selectedIndex = 21;

               
                if (Number(ddl3set[0].value) == 0) ddl3[0].selectedIndex = 1;
                if (Number(ddl3set[0].value) == 5) ddl3[0].selectedIndex = 2;
                if (Number(ddl3set[0].value) == 10) ddl3[0].selectedIndex = 3;
                if (Number(ddl3set[0].value) == 15) ddl3[0].selectedIndex = 4;
                if (Number(ddl3set[0].value) == 20) ddl3[0].selectedIndex = 5;
                if (Number(ddl3set[0].value) == 25) ddl3[0].selectedIndex = 6;
                if (Number(ddl3set[0].value) == 30) ddl3[0].selectedIndex = 7;
                if (Number(ddl3set[0].value) == 35) ddl3[0].selectedIndex = 8;
                if (Number(ddl3set[0].value) == 40) ddl3[0].selectedIndex = 9;
                if (Number(ddl3set[0].value) == 45) ddl3[0].selectedIndex = 10;
                if (Number(ddl3set[0].value) == 50) ddl3[0].selectedIndex = 11;
                if (Number(ddl3set[0].value) == 55) ddl3[0].selectedIndex = 12;
                if (Number(ddl3set[0].value) == 60) ddl3[0].selectedIndex = 13;
                if (Number(ddl3set[0].value) == 65) ddl3[0].selectedIndex = 14;
                if (Number(ddl3set[0].value) == 70) ddl3[0].selectedIndex = 15;
                if (Number(ddl3set[0].value) == 75) ddl3[0].selectedIndex = 16;
                if (Number(ddl3set[0].value) == 80) ddl3[0].selectedIndex = 17;
                if (Number(ddl3set[0].value) == 85) ddl3[0].selectedIndex = 18;
                if (Number(ddl3set[0].value) == 90) ddl3[0].selectedIndex = 19;
                if (Number(ddl3set[0].value) == 95) ddl3[0].selectedIndex = 20;
                if (Number(ddl3set[0].value) == 100) ddl3[0].selectedIndex = 21;

                if (Number(ddl4set[0].value) == 0) ddl4[0].selectedIndex = 1;
                if (Number(ddl4set[0].value) == 5) ddl4[0].selectedIndex = 2;
                if (Number(ddl4set[0].value) == 10) ddl4[0].selectedIndex = 3;
                if (Number(ddl4set[0].value) == 15) ddl4[0].selectedIndex = 4;
                if (Number(ddl4set[0].value) == 20) ddl4[0].selectedIndex = 5;
                if (Number(ddl4set[0].value) == 25) ddl4[0].selectedIndex = 6;
                if (Number(ddl4set[0].value) == 30) ddl4[0].selectedIndex = 7;
                if (Number(ddl4set[0].value) == 35) ddl4[0].selectedIndex = 8;
                if (Number(ddl4set[0].value) == 40) ddl4[0].selectedIndex = 9;
                if (Number(ddl4set[0].value) == 45) ddl4[0].selectedIndex = 10;
                if (Number(ddl4set[0].value) == 50) ddl4[0].selectedIndex = 11;
                if (Number(ddl4set[0].value) == 55) ddl4[0].selectedIndex = 12;
                if (Number(ddl4set[0].value) == 60) ddl4[0].selectedIndex = 13;
                if (Number(ddl4set[0].value) == 65) ddl4[0].selectedIndex = 14;
                if (Number(ddl4set[0].value) == 70) ddl4[0].selectedIndex = 15;
                if (Number(ddl4set[0].value) == 75) ddl4[0].selectedIndex = 16;
                if (Number(ddl4set[0].value) == 80) ddl4[0].selectedIndex = 17;
                if (Number(ddl4set[0].value) == 85) ddl4[0].selectedIndex = 18;
                if (Number(ddl4set[0].value) == 90) ddl4[0].selectedIndex = 19;
                if (Number(ddl4set[0].value) == 95) ddl4[0].selectedIndex = 20;
                if (Number(ddl4set[0].value) == 100) ddl4[0].selectedIndex = 21;
            }

            function setupddl() {

                var setup1 = document.getElementsByClassName("setup1");
                var setup2 = document.getElementsByClassName("setup2");
                var setup3 = document.getElementsByClassName("setup3");
                var setup4 = document.getElementsByClassName("setup4");
                var setup5 = document.getElementsByClassName("setup5");
                var setup6 = document.getElementsByClassName("setup6");
                var setup7 = document.getElementsByClassName("setup7");
                var setup8 = document.getElementsByClassName("setup8");
                var setup9 = document.getElementsByClassName("setup9");
                var setup11 = document.getElementsByClassName("setup11");
                var setup12 = document.getElementsByClassName("setup12");

                var autoddl1 = document.getElementsByClassName("autoddl1");
                var autoddl2 = document.getElementsByClassName("autoddl2");
                var autoddl3 = document.getElementsByClassName("autoddl3");
                var autoddl4 = document.getElementsByClassName("autoddl4");
                var autoddl5 = document.getElementsByClassName("autoddl5");
                var autoddl6 = document.getElementsByClassName("autoddl6");
                var autoddl7 = document.getElementsByClassName("autoddl7");
                var autoddl8 = document.getElementsByClassName("autoddl8");
                var autoddl9 = document.getElementsByClassName("autoddl9");
                var autoddl11 = document.getElementsByClassName("autoddl11");
                var autoddl12 = document.getElementsByClassName("autoddl12");

                var check2 = document.getElementsByClassName("check2");
                var check3 = document.getElementsByClassName("check3");
                var check4 = document.getElementsByClassName("check4");
                var check5 = document.getElementsByClassName("check5");
                var check6 = document.getElementsByClassName("check6");
                var check7 = document.getElementsByClassName("check7");
                var check8 = document.getElementsByClassName("check8");
                var check9 = document.getElementsByClassName("check9");
                var check10 = document.getElementsByClassName("check10");
                
                if (setup1[0].value == "0")
                    document.getElementById("check2").checked = false;
                else if (setup1[0].value == "1")
                    document.getElementById("check2").checked = true;

                if (setup2[0].value == "0")
                    document.getElementById("check3").checked = false;
                else if (setup2[0].value == "1")
                    document.getElementById("check3").checked = true;

                if (setup3[0].value == "0")
                    document.getElementById("ctl00_MainContent_check4").checked = false;
                else if (setup3[0].value == "1")
                    document.getElementById("ctl00_MainContent_check4").checked = true;

                if (setup4[0].value == "0")
                    document.getElementById("ctl00_MainContent_check5").checked = false;
                else if (setup4[0].value == "1")
                    document.getElementById("ctl00_MainContent_check5").checked = true;

                if (setup5[0].value == "0")
                    document.getElementById("ctl00_MainContent_check6").checked = false;
                else if (setup5[0].value == "1")
                    document.getElementById("ctl00_MainContent_check6").checked = true;

                if (setup6[0].value == "0")
                    document.getElementById("check7").checked = false;
                else if (setup6[0].value == "1")
                    document.getElementById("check7").checked = true;

                
                
                if (setup8[0].value == "0")
                    document.getElementById("check9").checked = false;
                else if (setup8[0].value == "1")
                    document.getElementById("check9").checked = true;

                if (setup9[0].value == "0")
                    document.getElementById("check10").checked = false;
                else if (setup9[0].value == "1")
                    document.getElementById("check10").checked = true;

                if (setup11[0].value == "0")
                    document.getElementById("ctl00_MainContent_check11").checked = false;
                else if (setup11[0].value == "1")
                    document.getElementById("ctl00_MainContent_check11").checked = true;

                if (setup12[0].value == "0")
                    document.getElementById("ctl00_MainContent_check12").checked = false;
                else if (setup12[0].value == "1")
                    document.getElementById("ctl00_MainContent_check12").checked = true;
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
                var editform3 = document.getElementsByClassName("editform3");
                var editgrade = document.getElementsByClassName("editgrade");
                var cen2 = document.getElementsByClassName("cen2");
                var cen3 = document.getElementsByClassName("cen3");
                var cen4 = document.getElementsByClassName("cen4");
                var nobehave = document.getElementsByClassName("nobehave");
                var gradetxt2 = document.getElementsByClassName("gradetxt2");
                var samatscore = document.getElementsByClassName("samatscore");
                var cen5 = document.getElementsByClassName("cen5");
                var w40 = document.getElementsByClassName("w40");

                var ddl1 = document.getElementsByClassName("autoddl1");
                var ddl2 = document.getElementsByClassName("autoddl2");
                var ddl3 = document.getElementsByClassName("autoddl3");
                var ddl4 = document.getElementsByClassName("autoddl4");
                var ddl5 = document.getElementsByClassName("autoddl5");
                var ddl6 = document.getElementsByClassName("autoddl6");
                var ddl7 = document.getElementsByClassName("autoddl7");
                var ddl8 = document.getElementsByClassName("autoddl8");
                var ddl9 = document.getElementsByClassName("autoddl9");
                var ddl11 = document.getElementsByClassName("autoddl11");
                var ddl12 = document.getElementsByClassName("autoddl12");
                
                var check3 = document.getElementsByClassName("check3");
                var check4 = document.getElementsByClassName("check4");
                var check5 = document.getElementsByClassName("check5");
                var check6 = document.getElementsByClassName("check6");
                var check7 = document.getElementsByClassName("check7");
                var check8 = document.getElementsByClassName("check8");
                var check9 = document.getElementsByClassName("check9");
                var check10 = document.getElementsByClassName("check10");
                var check11 = document.getElementsByClassName("check11");
                var check12 = document.getElementsByClassName("check12");

                var samatscore = document.getElementsByClassName("samatscore");
                
                var maxmidscore = document.getElementsByClassName("maxmidscore");
                var summidbox = document.getElementsByClassName("summidbox");
                var maxlatescore = document.getElementsByClassName("maxlatescore");
                var sumfinalbox = document.getElementsByClassName("sumfinalbox");

                if (check5[0].checked == true) {
                    ddl4[0].value = "1";                   
                }
                else if (check5[0].checked == false) {
                    ddl4[0].value = "0";                   
                }

                if (id == "8")
                {                    
                    var elm2 = document.getElementById("ctl00_MainContent_check4");
                    var elm = document.getElementById("ctl00_MainContent_check5");
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
                    blue[4].classList.remove('hidden');
                    //subcheckbox[0].classList.remove('hidden');
                }
                else if (check4[0].checked == false) {
                    ddl3[0].value = "0";
                    blue[4].classList.add('hidden');
                    //subcheckbox[0].classList.add('hidden');
                }

                if (check5[0].checked == false) {
                    behavedisable[0].value = "0";                   
                    for (var i = 0; i < goodbe.length; i++) {
                        goodbe[i].classList.remove('disable');
                    }
                }
                if (check5[0].checked == true) {
                    behavedisable[0].value = "1";                    
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

                if (check11[0].checked == true) {
                    ddl11[0].value = "1";
                    maxmidscore[0].classList.add('disable');
                    for (var i = 0; i < read.length; i++) {
                        summidbox[i].classList.add('disable');
                    }
                    blue[2].classList.remove('hidden');
                }
                else if (check11[0].checked == false) {
                    ddl11[0].value = "0";
                    maxmidscore[0].classList.remove('disable');
                    for (var i = 0; i < read.length; i++) {
                        summidbox[i].classList.remove('disable');
                    }
                    blue[2].classList.add('hidden');
                }

                if (check12[0].checked == true) {
                    ddl12[0].value = "1";
                    maxlatescore[0].classList.add('disable');
                    for (var i = 0; i < read.length; i++) {
                        sumfinalbox[i].classList.add('disable');
                    }
                    blue[3].classList.remove('hidden');
                }
                else if (check12[0].checked == false) {
                    ddl12[0].value = "0";
                    maxlatescore[0].classList.remove('disable');
                    for (var i = 0; i < read.length; i++) {
                        sumfinalbox[i].classList.remove('disable');
                    }
                    blue[3].classList.add('hidden');
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
                        //gradetxt2[i].value = "";
                    }
                }
                else if (check9[0].checked == false) {
                    ddl8[0].value = "0";
                    editform2[0].classList.remove('hidden');
                    for (var i = 0; i < w40.length; i++) {
                        editgrade[i].classList.remove('hidden');
                    }
                }

                if (check10[0].checked == true) {
                    ddl9[0].value = "1";
                    editform3[0].classList.add('hidden');
                    for (var i = 0; i < w40.length; i++) {
                        cen5[i].classList.add('hidden');
                        samatscore[i].value = "";
                    }
                }
                else if (check10[0].checked == false) {
                    ddl9[0].value = "0";
                    editform3[0].classList.remove('hidden');
                    for (var i = 0; i < w40.length; i++) {
                        cen5[i].classList.remove('hidden');
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

            function ddlshare() {

                var editmulti = document.getElementsByClassName("editmulti");                                
                var check8 = document.getElementsByClassName("check8");
                
                var classchoose2 = document.getElementsByClassName("classchoose2");

                var maxmidscore = document.getElementsByClassName("maxmidscore");
                var summidbox = document.getElementsByClassName("summidbox");
                var maxlatescore = document.getElementsByClassName("maxlatescore");
                var sumfinalbox = document.getElementsByClassName("sumfinalbox");


                if (check8[0].checked == true) {
                    classchoose2[0].classList.remove('hidden');
                }
                else if (check8[0].checked == false) {
                    classchoose2[0].classList.add('hidden');                    
                    
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
                var idlv2 = split[2].split('=');
                var term = split[3].split('=');
                var id = split[4].split('=');
                                                
                $.get("/App_Logic/bp5GradeRegister.ashx?term=" + term[1] + "&year=" + year[1] + "&id=" + id[1] + "&idlv2=" + idlv2[1], function (Result) {
                    $.each(Result, function (index) {
                                                
                        for (var x = 0; x < siduser.length; x++)
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
                                else if (Result[index].ddlother == "10")
                                    special[x].selectedIndex = 10;
                                else if (Result[index].ddlother == "11")
                                    special[x].selectedIndex = 11;
                                else if (Result[index].ddlother == "12")
                                    special[x].selectedIndex = 12;
                                else if (Result[index].ddlother == "13")
                                    special[x].selectedIndex = 13;
                                else special[x].selectedIndex = 0;
                            }
                        }
                        

                       
                    });
                });
            }

            function ddlcopy(id) {

                var copy1 = document.getElementsByClassName("copy1");
                var copy2 = document.getElementsByClassName("copy2");
                var copy3 = document.getElementsByClassName("copy3");
                var ddlcopy1 = document.getElementsByClassName("ddlcopy1");
                var ddlcopy2 = document.getElementsByClassName("ddlcopy2");
                var ddlcopy3 = document.getElementsByClassName("ddlcopy3");
                var special = document.getElementsByClassName("special");
                var maxtestb1 = document.getElementsByClassName("maxtestb1");
                
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
                var behavesid = document.getElementsByClassName("behavesid");

                var full = window.location.href;
                var half = full.split('?');
                var split = half[1].split('&');
                var year = split[0].split('=');
                var idlv = split[1];
                var idlv2 = split[2].split('=');
                var term = split[3].split('=');                               
               
                var useryear = year[1];
                var userterm = term[1];
                var useridlv2 = idlv2[1];
                var samatscore = document.getElementsByClassName("samatscore");
                               
                copy1[0].value = ddlcopy1[0].value;
                copy2[0].value = ddlcopy2[0].value;
                copy3[0].value = ddlcopy3[0].value;
                
                if (id == 3)
                {
                    $.get("/App_Logic/bp5ImportScore.ashx?mode=1&term=" + userterm + "&year=" + useryear + "&idlv2=" + useridlv2 + "&id=" + copy1[0].value, function (Result) {
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
                            
                            setb1[0].value = Result[index].behave1name;
                            setb2[0].value = Result[index].behave2name;
                            setb3[0].value = Result[index].behave3name;
                            setb4[0].value = Result[index].behave4name;
                            setb5[0].value = Result[index].behave5name;
                            setb6[0].value = Result[index].behave6name;
                            setb7[0].value = Result[index].behave7name;
                            setb8[0].value = Result[index].behave8name;
                            setb9[0].value = Result[index].behave9name;
                            setb10[0].value = Result[index].behave10name;
                        });
                    });
                    
                    $.get("/App_Logic/bp5ImportScore.ashx?mode=2&term=" + userterm + "&year=" + useryear + "&idlv2=" + useridlv2 + "&id=" + copy1[0].value, function (Result) {
                        $.each(Result, function (index) {
                            for (var x = 0; x < behavesid.length; x++)
                            {
                                if (Result[index].sID == behavesid[x].value)
                                {
                                    behavetxt1[x].value = Result[index].behave1;
                                    behavetxt2[x].value = Result[index].behave2;
                                    behavetxt3[x].value = Result[index].behave3;
                                    behavetxt4[x].value = Result[index].behave4;
                                    behavetxt5[x].value = Result[index].behave5;
                                    behavetxt6[x].value = Result[index].behave6;
                                    behavetxt7[x].value = Result[index].behave7;
                                    behavetxt8[x].value = Result[index].behave8;
                                    behavetxt9[x].value = Result[index].behave9;
                                    behavetxt10[x].value = Result[index].behave10;
                                    goodbe[x].value = Result[index].behaveTotal;
                                }                                
                            }

                            
                        });
                    });
                }

                if (id == 4) {
                    $.get("/App_Logic/bp5ImportScore.ashx?mode=3&term=" + userterm + "&year=" + useryear + "&idlv2=" + useridlv2 + "&id=" + copy2[0].value, function (Result) {
                        $.each(Result, function (index) {
                            for (var x = 0; x < behavesid.length; x++) {
                                if (Result[index].sID == behavesid[x].value) {
                                    readscore[x].value = Result[index].readingTotal;                                    
                                }
                            }
                                                      
                        });
                    });                    
                }
               
                if (id == 5) {
                    
                    $.get("/App_Logic/bp5ImportScore.ashx?mode=4&term=" + userterm + "&year=" + useryear + "&idlv2=" + useridlv2 + "&id=" + copy3[0].value, function (Result) {
                        $.each(Result, function (index) {                            
                            for (var x = 0; x < behavesid.length; x++) {
                                if (Result[index].sID == behavesid[x].value) {
                                    samatscore[x].value = Result[index].samattanaTotal;
                                }
                            }

                        });
                    });
                }

                setTimeout(function () {
                    changename();
                }, 1000);
                
            }

            function isNumeric(n) {
                if (n == "")
                    return true;
                return !isNaN(parseFloat(n)) && isFinite(n);
            }

            function calallscore(index) {
                
                var textBoxesg1 = document.getElementsByClassName("AutoCompleteTextBox");
                var textBoxesg2 = document.getElementsByClassName("AutoCompleteTextBoxg2");
                var textBoxesg3 = document.getElementsByClassName("AutoCompleteTextBoxg3");
                var textBoxesg4 = document.getElementsByClassName("AutoCompleteTextBoxg4");
                var textBoxes2 = document.getElementsByClassName("AutoCompleteTextBox2");
                var chewut1 = document.getElementsByClassName("chewut1");
                var chewut2 = document.getElementsByClassName("chewut2");
                var chewut3 = document.getElementsByClassName("chewut3");
                var chewut4 = document.getElementsByClassName("chewut4");

                var mid = document.getElementsByClassName("midratio");
                var late = document.getElementsByClassName("lateratio");
                var gradebox = document.getElementsByClassName("gradetxt");
                var special = document.getElementsByClassName("special");
                var maxtest = document.getElementsByClassName("maxtest");
                var maxtestcw = document.getElementsByClassName("maxtestcw");
                var reading = document.getElementsByClassName("readscore");
                var check2 = document.getElementsByClassName("check2");                
               
                var maxscoreall = document.getElementsByClassName("maxscoreall");
                var scoresumall = document.getElementsByClassName("scoresumall");
                var maxmidscore = document.getElementsByClassName("maxmidscore");
                var maxlatescore = document.getElementsByClassName("maxlatescore");

                var lockg1 = document.getElementsByClassName("lockg1");
                var lockg2 = document.getElementsByClassName("lockg2");
                var lockg3 = document.getElementsByClassName("lockg3");
                var lockg4 = document.getElementsByClassName("lockg4");
                var lockg5 = document.getElementsByClassName("lockg5");
                var lockg6 = document.getElementsByClassName("lockg6");
                var lockg7 = document.getElementsByClassName("lockg7");
                var lockg8 = document.getElementsByClassName("lockg8");
                var lockg9 = document.getElementsByClassName("lockg9");
                var lockg10 = document.getElementsByClassName("lockg10");
                var lockg11 = document.getElementsByClassName("lockg11");
                var lockg12 = document.getElementsByClassName("lockg12");
                var lockg13 = document.getElementsByClassName("lockg13");
                var lockg14 = document.getElementsByClassName("lockg14");
                var lockg15 = document.getElementsByClassName("lockg15");
                var lockg16 = document.getElementsByClassName("lockg16");
                var lockg17 = document.getElementsByClassName("lockg17");
                var lockg18 = document.getElementsByClassName("lockg18");
                var lockg19 = document.getElementsByClassName("lockg19");
                var lockg20 = document.getElementsByClassName("lockg20");
                var lockcw1 = document.getElementsByClassName("lockcw1");
                var lockcw2 = document.getElementsByClassName("lockcw2");
                var lockcw3 = document.getElementsByClassName("lockcw3");
                var lockcw4 = document.getElementsByClassName("lockcw4");
                var lockcw5 = document.getElementsByClassName("lockcw5");
                var lockcw6 = document.getElementsByClassName("lockcw6");
                var lockcw7 = document.getElementsByClassName("lockcw7");
                var lockcw8 = document.getElementsByClassName("lockcw8");
                var lockcw9 = document.getElementsByClassName("lockcw9");
                var lockcw10 = document.getElementsByClassName("lockcw10");
                var lockcw11 = document.getElementsByClassName("lockcw11");
                var lockcw12 = document.getElementsByClassName("lockcw12");
                var lockcw13 = document.getElementsByClassName("lockcw13");
                var lockcw14 = document.getElementsByClassName("lockcw14");
                var lockcw15 = document.getElementsByClassName("lockcw15");
                var lockcw16 = document.getElementsByClassName("lockcw16");
                var lockcw17 = document.getElementsByClassName("lockcw17");
                var lockcw18 = document.getElementsByClassName("lockcw18");
                var lockcw19 = document.getElementsByClassName("lockcw19");
                var lockcw20 = document.getElementsByClassName("lockcw20");
                var lockm1 = document.getElementsByClassName("lockm1");
                var lockm2 = document.getElementsByClassName("lockm2");
                var lockm3 = document.getElementsByClassName("lockm3");
                var lockm4 = document.getElementsByClassName("lockm4");
                var lockm5 = document.getElementsByClassName("lockm5");
                var lockm6 = document.getElementsByClassName("lockm6");
                var lockm7 = document.getElementsByClassName("lockm7");
                var lockm8 = document.getElementsByClassName("lockm8");
                var lockm9 = document.getElementsByClassName("lockm9");
                var lockm10 = document.getElementsByClassName("lockm10");
                var lockf1 = document.getElementsByClassName("lockf1");
                var lockf2 = document.getElementsByClassName("lockf2");
                var lockf3 = document.getElementsByClassName("lockf3");
                var lockf4 = document.getElementsByClassName("lockf4");
                var lockf5 = document.getElementsByClassName("lockf5");
                var lockf6 = document.getElementsByClassName("lockf6");
                var lockf7 = document.getElementsByClassName("lockf7");
                var lockf8 = document.getElementsByClassName("lockf8");
                var lockf9 = document.getElementsByClassName("lockf9");
                var lockf10 = document.getElementsByClassName("lockf10");
                var lockmidterm = document.getElementsByClassName("lockmidterm");
                var lockfinalterm = document.getElementsByClassName("lockfinalterm");

                
                if(index == 99999 || index == null)
                {
                    var maxscore = 0;
                    for (var x = 0; x < 20; x++) {
                        maxscore = maxscore + Number(maxtest[x].value);
                        maxscore = maxscore + Number(maxtestcw[x].value);
                    }
                    maxscore = maxscore + Number(maxlatescore[0].value);
                    maxscore = maxscore + Number(maxmidscore[0].value);
                    maxscoreall[0].value = maxscore;
                }
                
                if(index == 99999)
                {
                    for (var x = 0; x < scoresumall.length; x++)
                    {
                        var y = 0;
                        y = y + Number(lockg1[x].value);
                        y = y + Number(lockg2[x].value);
                        y = y + Number(lockg3[x].value);
                        y = y + Number(lockg4[x].value);
                        y = y + Number(lockg5[x].value);
                        y = y + Number(lockg6[x].value);
                        y = y + Number(lockg7[x].value);
                        y = y + Number(lockg8[x].value);
                        y = y + Number(lockg9[x].value);
                        y = y + Number(lockg10[x].value);
                        y = y + Number(lockg11[x].value);
                        y = y + Number(lockg12[x].value);
                        y = y + Number(lockg13[x].value);
                        y = y + Number(lockg14[x].value);
                        y = y + Number(lockg15[x].value);
                        y = y + Number(lockg16[x].value);
                        y = y + Number(lockg17[x].value);
                        y = y + Number(lockg18[x].value);
                        y = y + Number(lockg19[x].value);
                        y = y + Number(lockg20[x].value);
                        y = y + Number(lockcw1[x].value);
                        y = y + Number(lockcw2[x].value);
                        y = y + Number(lockcw3[x].value);
                        y = y + Number(lockcw4[x].value);
                        y = y + Number(lockcw5[x].value);
                        y = y + Number(lockcw6[x].value);
                        y = y + Number(lockcw7[x].value);
                        y = y + Number(lockcw8[x].value);
                        y = y + Number(lockcw9[x].value);
                        y = y + Number(lockcw10[x].value);
                        y = y + Number(lockcw11[x].value);
                        y = y + Number(lockcw12[x].value);
                        y = y + Number(lockcw13[x].value);
                        y = y + Number(lockcw14[x].value);
                        y = y + Number(lockcw15[x].value);
                        y = y + Number(lockcw16[x].value);
                        y = y + Number(lockcw17[x].value);
                        y = y + Number(lockcw18[x].value);
                        y = y + Number(lockcw19[x].value);
                        y = y + Number(lockcw20[x].value);
                        y = y + Number(lockmidterm[x].value);
                        y = y + Number(lockfinalterm[x].value);
                        
                        scoresumall[x].value = y;
                    }
                }
                else if (index != null)
                {
                    var x = index - 1;
                    var y = 0;
                    y = y + Number(lockg1[x].value);
                    y = y + Number(lockg2[x].value);
                    y = y + Number(lockg3[x].value);
                    y = y + Number(lockg4[x].value);
                    y = y + Number(lockg5[x].value);
                    y = y + Number(lockg6[x].value);
                    y = y + Number(lockg7[x].value);
                    y = y + Number(lockg8[x].value);
                    y = y + Number(lockg9[x].value);
                    y = y + Number(lockg10[x].value);
                    y = y + Number(lockg11[x].value);
                    y = y + Number(lockg12[x].value);
                    y = y + Number(lockg13[x].value);
                    y = y + Number(lockg14[x].value);
                    y = y + Number(lockg15[x].value);
                    y = y + Number(lockg16[x].value);
                    y = y + Number(lockg17[x].value);
                    y = y + Number(lockg18[x].value);
                    y = y + Number(lockg19[x].value);
                    y = y + Number(lockg20[x].value);
                    y = y + Number(lockcw1[x].value);
                    y = y + Number(lockcw2[x].value);
                    y = y + Number(lockcw3[x].value);
                    y = y + Number(lockcw4[x].value);
                    y = y + Number(lockcw5[x].value);
                    y = y + Number(lockcw6[x].value);
                    y = y + Number(lockcw7[x].value);
                    y = y + Number(lockcw8[x].value);
                    y = y + Number(lockcw9[x].value);
                    y = y + Number(lockcw10[x].value);
                    y = y + Number(lockcw11[x].value);
                    y = y + Number(lockcw12[x].value);
                    y = y + Number(lockcw13[x].value);
                    y = y + Number(lockcw14[x].value);
                    y = y + Number(lockcw15[x].value);
                    y = y + Number(lockcw16[x].value);
                    y = y + Number(lockcw17[x].value);
                    y = y + Number(lockcw18[x].value);
                    y = y + Number(lockcw19[x].value);
                    y = y + Number(lockcw20[x].value);
                    y = y + Number(lockmidterm[x].value);
                    y = y + Number(lockfinalterm[x].value);
                    
                    scoresumall[x].value = y;
                }

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
                var ddl9 = document.getElementsByClassName("autoddl9");
                var ddl11 = document.getElementsByClassName("autoddl11");
                var ddl12 = document.getElementsByClassName("autoddl12");

                var cog1 = document.getElementsByClassName("cog1");
                var cog2 = document.getElementsByClassName("cog2");
                var behavedisable = document.getElementsByClassName("behavedisable");
                
                
                var mid = document.getElementsByClassName("midratio");
                var late = document.getElementsByClassName("lateratio");
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

                var midratio = document.getElementById('<%=midRatio.ClientID%>');
                var quizratio = document.getElementById('<%=scoreRatio.ClientID%>');
                var lateratio = document.getElementById('<%=lastRatio.ClientID%>');
               

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

                
                var check3 = document.getElementsByClassName("check3");                
                var check5 = document.getElementsByClassName("check5");
                

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

                
                var ratiomid = midratio.value;
                var ratioquiz = quizratio.value;
                var ratiolate = lateratio.value;
                
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
                            if (Number(maxtest1) == 0) nomax();

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
                            if (Number(maxtest6) == 0) nomax();

                            textBoxesg2[i].classList.add('txtglow');

                        }
                        if (Number(textBoxesg2[i].value) <= Number(maxtest6))
                        {
                            textBoxesg2[i].classList.remove('txtglow');
                        }
                           
                        if (Number(textBoxesg3[i].value) > Number(maxtest11))
                        {
                            changebutton(i);
                            if (Number(maxtest11) == 0) nomax();

                            textBoxesg3[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg3[i].value) <= Number(maxtest11)) {                            
                            textBoxesg3[i].classList.remove('txtglow');
                        }
                            
                        if (Number(textBoxesg4[i].value) > Number(maxtest16))
                        {
                            changebutton(i);
                            if (Number(maxtest16) == 0) nomax();

                            textBoxesg4[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg4[i].value) <= Number(maxtest16)) {
                            textBoxesg4[i].classList.remove('txtglow');
                        }

                        if (Number(chewut1[i].value) > Number(maxtestcw1))
                        {
                            changebutton(i);
                            if (Number(maxtestcw1) == 0) nomax();

                            chewut1[i].classList.add('txtglow');
                        }
                        if (Number(chewut1[i].value) <= Number(maxtestcw1)) {
                            chewut1[i].classList.remove('txtglow');
                        }
                            
                        if (Number(chewut2[i].value) > Number(maxtestcw6))
                        {
                            changebutton(i);
                            if (Number(maxtestcw6) == 0) nomax();

                            chewut2[i].classList.add('txtglow');
                        }
                        if (Number(chewut2[i].value) <= Number(maxtestcw6)) {                            
                            chewut2[i].classList.remove('txtglow');
                        }
                            
                        if (Number(chewut3[i].value) > Number(maxtestcw11))
                        {
                            changebutton(i);
                            if (Number(maxtestcw11) == 0) nomax();

                            chewut3[i].classList.add('txtglow');
                        }
                        if (Number(chewut3[i].value) <= Number(maxtestcw11)) {                           
                            chewut3[i].classList.remove('txtglow');
                        }
                            
                        if (Number(chewut4[i].value) > Number(maxtestcw16))
                        {
                            changebutton(i);
                            if (Number(maxtestcw16) == 0) nomax();

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
                            if (Number(maxtest2) == 0) nomax();

                            textBoxesg1[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg1[i].value) <= Number(maxtest2)) {
                            textBoxesg1[i].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg2[i].value) > Number(maxtest7)) {
                            changebutton(i);
                            if (Number(maxtest7) == 0) nomax();

                            textBoxesg2[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg2[i].value) <= Number(maxtest7)) {
                            textBoxesg2[i].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg3[i].value) > Number(maxtest12)) {
                            changebutton(i);
                            if (Number(maxtest12) == 0) nomax();

                            textBoxesg3[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg3[i].value) <= Number(maxtest12)) {
                            textBoxesg3[i].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg4[i].value) > Number(maxtest17)) {
                            changebutton(i);
                            if (Number(maxtest17) == 0) nomax();

                            textBoxesg4[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg4[i].value) <= Number(maxtest17)) {
                            textBoxesg4[i].classList.remove('txtglow');
                        }

                        if (Number(chewut1[i].value) > Number(maxtestcw2)) {
                            changebutton(i);
                            if (Number(maxtestcw2) == 0) nomax();

                            chewut1[i].classList.add('txtglow');
                        }
                        if (Number(chewut1[i].value) <= Number(maxtestcw2)) {
                            chewut1[i].classList.remove('txtglow');
                        }

                        if (Number(chewut2[i].value) > Number(maxtestcw7)) {
                            changebutton(i);
                            if (Number(maxtestcw7) == 0) nomax();

                            chewut2[i].classList.add('txtglow');
                        }
                        if (Number(chewut2[i].value) <= Number(maxtestcw7)) {
                            chewut2[i].classList.remove('txtglow');
                        }

                        if (Number(chewut3[i].value) > Number(maxtestcw12)) {
                            changebutton(i);
                            if (Number(maxtestcw12) == 0) nomax();

                            chewut3[i].classList.add('txtglow');
                        }
                        if (Number(chewut3[i].value) <= Number(maxtestcw12)) {
                            chewut3[i].classList.remove('txtglow');
                        }

                        if (Number(chewut4[i].value) > Number(maxtestcw17)) {
                            changebutton(i);
                            if (Number(maxtestcw17) == 0) nomax();

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
                            if (Number(maxtest3) == 0) nomax();

                            textBoxesg1[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg1[i].value) <= Number(maxtest3)) {
                            textBoxesg1[i].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg2[i].value) > Number(maxtest8)) {
                            changebutton(i);
                            if (Number(maxtest8) == 0) nomax();

                            textBoxesg2[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg2[i].value) <= Number(maxtest8)) {
                            textBoxesg2[i].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg3[i].value) > Number(maxtest13)) {
                            changebutton(i);
                            if (Number(maxtest13) == 0) nomax();

                            textBoxesg3[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg3[i].value) <= Number(maxtest13)) {
                            textBoxesg3[i].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg4[i].value) > Number(maxtest18)) {
                            changebutton(i);
                            if (Number(maxtest18) == 0) nomax();

                            textBoxesg4[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg4[i].value) <= Number(maxtest18)) {
                            textBoxesg4[i].classList.remove('txtglow');
                        }

                        if (Number(chewut1[i].value) > Number(maxtestcw3)) {
                            changebutton(i);
                            if (Number(maxtestcw3) == 0) nomax();

                            chewut1[i].classList.add('txtglow');
                        }
                        if (Number(chewut1[i].value) <= Number(maxtestcw3)) {
                            chewut1[i].classList.remove('txtglow');
                        }

                        if (Number(chewut2[i].value) > Number(maxtestcw8)) {
                            changebutton(i);
                            if (Number(maxtestcw8) == 0) nomax();

                            chewut2[i].classList.add('txtglow');
                        }
                        if (Number(chewut2[i].value) <= Number(maxtestcw8)) {
                            chewut2[i].classList.remove('txtglow');
                        }

                        if (Number(chewut3[i].value) > Number(maxtestcw13)) {
                            changebutton(i);
                            if (Number(maxtestcw13) == 0) nomax();

                            chewut3[i].classList.add('txtglow');
                        }
                        if (Number(chewut3[i].value) <= Number(maxtestcw13)) {
                            chewut3[i].classList.remove('txtglow');
                        }

                        if (Number(chewut4[i].value) > Number(maxtestcw18)) {
                            changebutton(i);
                            if (Number(maxtestcw18) == 0) nomax();

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
                            if (Number(maxtest4) == 0) nomax();

                            textBoxesg1[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg1[i].value) <= Number(maxtest4)) {
                            textBoxesg1[i].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg2[i].value) > Number(maxtest9)) {
                            changebutton(i);
                            if (Number(maxtest9) == 0) nomax();

                            textBoxesg2[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg2[i].value) <= Number(maxtest9)) {
                            textBoxesg2[i].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg3[i].value) > Number(maxtest14)) {
                            changebutton(i);
                            if (Number(maxtest14) == 0) nomax();

                            textBoxesg3[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg3[i].value) <= Number(maxtest14)) {
                            textBoxesg3[i].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg4[i].value) > Number(maxtest19)) {
                            changebutton(i);
                            if (Number(maxtest19) == 0) nomax();

                            textBoxesg4[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg4[i].value) <= Number(maxtest19)) {
                            textBoxesg4[i].classList.remove('txtglow');
                        }

                        if (Number(chewut1[i].value) > Number(maxtestcw4)) {
                            changebutton(i);
                            if (Number(maxtestcw4) == 0) nomax();

                            chewut1[i].classList.add('txtglow');
                        }
                        if (Number(chewut1[i].value) <= Number(maxtestcw4)) {
                            chewut1[i].classList.remove('txtglow');
                        }

                        if (Number(chewut2[i].value) > Number(maxtestcw9)) {
                            changebutton(i);
                            if (Number(maxtestcw9) == 0) nomax();

                            chewut2[i].classList.add('txtglow');
                        }
                        if (Number(chewut2[i].value) <= Number(maxtestcw9)) {
                            chewut2[i].classList.remove('txtglow');
                        }

                        if (Number(chewut3[i].value) > Number(maxtestcw14)) {
                            changebutton(i);
                            if (Number(maxtestcw14) == 0) nomax();

                            chewut3[i].classList.add('txtglow');
                        }
                        if (Number(chewut3[i].value) <= Number(maxtestcw14)) {
                            chewut3[i].classList.remove('txtglow');
                        }

                        if (Number(chewut4[i].value) > Number(maxtestcw19)) {
                            changebutton(i);
                            if (Number(maxtestcw19) == 0) nomax();

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

                        if (ddl2[0].value == 0)
                        {
                            if (check3[0].checked == false) {
                                if (Number(totalpercent) >= 80) { readingsore = "3"; }                                
                                else if (Number(totalpercent) >= 60) { readingsore = "2"; }
                                else if (Number(totalpercent) >= 50) { readingsore = "1"; }
                                
                                else { readingsore = "0"; }
                                reading[(y - 1)].value = readingsore;
                            }
                            
                        }
                        
                        gradebox[(y * 2) - 2].value = "";
                        gradebox[(y * 2) - 1].value = "";                        
                        gradebox[(y * 2) - 2].value = totalpercent;                            
                       

                        //if (ddl8[0].value != "1") {                         
                            gradebox[(y * 2) - 1].value = totalgrade;                            
                        //}
                             
                        var g = isFinite(gradebox[(y * 2) - 2].value);
                        if (g == false)
                            alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132287") %>");
                        
                        if (Number(getmid) > Number(maxmid))
                           changebutton(i); 

                        if (Number(getlate) > Number(maxlate))
                           changebutton(i); 


                        if (Number(textBoxes2[(y * 2) - 2].value) > Number(maxmid)) {
                            changebutton(i);
                            if (Number(maxmid) == 0) nomax();

                            textBoxes2[(y * 2) - 2].classList.add('txtglow');
                        }
                        if (Number(textBoxes2[(y * 2) - 2].value) <= Number(maxmid)) {
                            textBoxes2[(y * 2) - 2].classList.remove('txtglow');
                        }

                        if (Number(textBoxes2[(y * 2) - 1].value) > Number(maxlate)) {
                            changebutton(i);
                            if (Number(maxlate) == 0) nomax();

                            textBoxes2[(y * 2) - 1].classList.add('txtglow');
                        }
                        if (Number(textBoxes2[(y * 2) - 1].value) <= Number(maxlate)) {
                            textBoxes2[(y * 2) - 1].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg1[i].value) > Number(maxtest5)) {
                            changebutton(i);
                            if (Number(maxtest5) == 0) nomax();

                            textBoxesg1[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg1[i].value) <= Number(maxtest5)) {
                            textBoxesg1[i].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg2[i].value) > Number(maxtest10)) {
                            changebutton(i);
                            if (Number(maxtest10) == 0) nomax();

                            textBoxesg2[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg2[i].value) <= Number(maxtest10)) {
                            textBoxesg2[i].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg3[i].value) > Number(maxtest15)) {
                            changebutton(i);
                            if (Number(maxtest15) == 0) nomax();

                            textBoxesg3[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg3[i].value) <= Number(maxtest15)) {
                            textBoxesg3[i].classList.remove('txtglow');
                        }

                        if (Number(textBoxesg4[i].value) > Number(maxtest20)) {
                            changebutton(i);
                            if (Number(maxtest20) == 0) nomax();

                            textBoxesg4[i].classList.add('txtglow');
                        }
                        if (Number(textBoxesg4[i].value) <= Number(maxtest20)) {
                            textBoxesg4[i].classList.remove('txtglow');
                        }

                        if (Number(chewut1[i].value) > Number(maxtestcw5)) {
                            changebutton(i);
                            if (Number(maxtestcw5) == 0) nomax();

                            chewut1[i].classList.add('txtglow');
                        }
                        if (Number(chewut1[i].value) <= Number(maxtestcw5)) {
                            chewut1[i].classList.remove('txtglow');
                        }

                        if (Number(chewut2[i].value) > Number(maxtestcw10)) {
                            changebutton(i);
                            if (Number(maxtestcw10) == 0) nomax();

                            chewut2[i].classList.add('txtglow');
                        }
                        if (Number(chewut2[i].value) <= Number(maxtestcw10)) {
                            chewut2[i].classList.remove('txtglow');
                        }

                        if (Number(chewut3[i].value) > Number(maxtestcw15)) {
                            changebutton(i);
                            if (Number(maxtestcw15) == 0) nomax();

                            chewut3[i].classList.add('txtglow');
                        }
                        if (Number(chewut3[i].value) <= Number(maxtestcw15)) {
                            chewut3[i].classList.remove('txtglow');
                        }

                        if (Number(chewut4[i].value) > Number(maxtestcw20)) {
                            changebutton(i);
                            if (Number(maxtestcw20) == 0) nomax();

                            chewut4[i].classList.add('txtglow');
                        }
                        if (Number(chewut4[i].value) <= Number(maxtestcw20)) {
                            chewut4[i].classList.remove('txtglow');
                        }
                        y = y + 1;

                    }
                }

                calallscore(id);
            }

            function checkstatus() {

                var stdout1 = document.getElementsByClassName("stdout1");
                var stdout2 = document.getElementsByClassName("stdout2");
                var stdout3 = document.getElementsByClassName("stdout3");
                var stdout4 = document.getElementsByClassName("stdout4");
                var stdout5 = document.getElementsByClassName("stdout5");
                var stdout6 = document.getElementsByClassName("stdout6");
                var stdout7 = document.getElementsByClassName("stdout7");
                var stdout8 = document.getElementsByClassName("stdout8");
                var stdout9 = document.getElementsByClassName("stdout9");
                var stdout10 = document.getElementsByClassName("stdout10");
                var stdout11 = document.getElementsByClassName("stdout11");
                var stdout12 = document.getElementsByClassName("stdout12");
                var stdout13 = document.getElementsByClassName("stdout13");
                var stdout14 = document.getElementsByClassName("stdout14");
                var stdout15 = document.getElementsByClassName("stdout15");
                var stdout16 = document.getElementsByClassName("stdout16");
                var stdout17 = document.getElementsByClassName("stdout17");
                var stdout18 = document.getElementsByClassName("stdout18");
                var stdout19 = document.getElementsByClassName("stdout19");
                var stdout20 = document.getElementsByClassName("stdout20");
                var stdout21 = document.getElementsByClassName("stdout21");
                var stdout22 = document.getElementsByClassName("stdout22");
                var stdout23 = document.getElementsByClassName("stdout23");
                var stdout24 = document.getElementsByClassName("stdout24");
                var stdout25 = document.getElementsByClassName("stdout25");
                var stdout26 = document.getElementsByClassName("stdout26");
                var stdout27 = document.getElementsByClassName("stdout27");
                var stdout28 = document.getElementsByClassName("stdout28");
                var stdout29 = document.getElementsByClassName("stdout29");
                var stdout30 = document.getElementsByClassName("stdout30");
                var stdout31 = document.getElementsByClassName("stdout31");
                var stdout32 = document.getElementsByClassName("stdout32");
                var stdout33 = document.getElementsByClassName("stdout33");
                var stdout34 = document.getElementsByClassName("stdout34");
                var stdout35 = document.getElementsByClassName("stdout35");
                var stdout36 = document.getElementsByClassName("stdout36");
                var stdout37 = document.getElementsByClassName("stdout37");
                var stdout38 = document.getElementsByClassName("stdout38");
                var stdout39 = document.getElementsByClassName("stdout39");
                var stdout40 = document.getElementsByClassName("stdout40");
                var stdout41 = document.getElementsByClassName("stdout41");
                var stdout42 = document.getElementsByClassName("stdout42");
                var stdout43 = document.getElementsByClassName("stdout43");
                var stdout44 = document.getElementsByClassName("stdout44");
                var stdout45 = document.getElementsByClassName("stdout45");
                var stdout46 = document.getElementsByClassName("stdout46");
                var stdout47 = document.getElementsByClassName("stdout47");
                var stdout48 = document.getElementsByClassName("stdout48");
                var stdout49 = document.getElementsByClassName("stdout49");
                var stdout50 = document.getElementsByClassName("stdout50");
                var stdout51 = document.getElementsByClassName("stdout51");
                var stdout52 = document.getElementsByClassName("stdout52");
                var stdout53 = document.getElementsByClassName("stdout53");
                var stdout54 = document.getElementsByClassName("stdout54");
                var stdout55 = document.getElementsByClassName("stdout55");
                var stdout56 = document.getElementsByClassName("stdout56");
                var stdout57 = document.getElementsByClassName("stdout57");
                var stdout58 = document.getElementsByClassName("stdout58");
                var stdout59 = document.getElementsByClassName("stdout59");
                var stdout60 = document.getElementsByClassName("stdout60");
                var stdout61 = document.getElementsByClassName("stdout61");
                var stdout62 = document.getElementsByClassName("stdout62");
                var stdout63 = document.getElementsByClassName("stdout63");
                var stdout64 = document.getElementsByClassName("stdout64");
                var stdout65 = document.getElementsByClassName("stdout65");
                var stdout66 = document.getElementsByClassName("stdout66");
                var stdout67 = document.getElementsByClassName("stdout67");
                var stdout68 = document.getElementsByClassName("stdout68");
                var stdout69 = document.getElementsByClassName("stdout69");
                var stdout70 = document.getElementsByClassName("stdout70");
                var stdout71 = document.getElementsByClassName("stdout71");
                var stdout72 = document.getElementsByClassName("stdout72");
                var stdout73 = document.getElementsByClassName("stdout73");
                var stdout74 = document.getElementsByClassName("stdout74");
                var stdout75 = document.getElementsByClassName("stdout75");
                var stdout76 = document.getElementsByClassName("stdout76");
                var stdout77 = document.getElementsByClassName("stdout77");
                var stdout78 = document.getElementsByClassName("stdout78");
                var stdout79 = document.getElementsByClassName("stdout79");
                var stdout80 = document.getElementsByClassName("stdout80");
                var stdout81 = document.getElementsByClassName("stdout81");
                var stdidlist = document.getElementsByClassName("stdidlist");
                var stdstatus = document.getElementsByClassName("stdstatus");
                
                var stdscore1 = document.getElementsByClassName("lockg1");
                var stdscore2 = document.getElementsByClassName("lockg2");
                var stdscore3 = document.getElementsByClassName("lockg3");
                var stdscore4 = document.getElementsByClassName("lockg4");
                var stdscore5 = document.getElementsByClassName("lockg5");
                var stdscore6 = document.getElementsByClassName("lockg6");
                var stdscore7 = document.getElementsByClassName("lockg7");
                var stdscore8 = document.getElementsByClassName("lockg8");
                var stdscore9 = document.getElementsByClassName("lockg9");
                var stdscore10 = document.getElementsByClassName("lockg10");
                var stdscore11 = document.getElementsByClassName("lockg11");
                var stdscore12 = document.getElementsByClassName("lockg12");
                var stdscore13 = document.getElementsByClassName("lockg13");
                var stdscore14 = document.getElementsByClassName("lockg14");
                var stdscore15 = document.getElementsByClassName("lockg15");
                var stdscore16 = document.getElementsByClassName("lockg16");
                var stdscore17 = document.getElementsByClassName("lockg17");
                var stdscore18 = document.getElementsByClassName("lockg18");
                var stdscore19 = document.getElementsByClassName("lockg19");
                var stdscore20 = document.getElementsByClassName("lockg20");

                var stdchewat1 = document.getElementsByClassName("lockcw1");
                var stdchewat2 = document.getElementsByClassName("lockcw2");
                var stdchewat3 = document.getElementsByClassName("lockcw3");
                var stdchewat4 = document.getElementsByClassName("lockcw4");
                var stdchewat5 = document.getElementsByClassName("lockcw5");
                var stdchewat6 = document.getElementsByClassName("lockcw6");
                var stdchewat7 = document.getElementsByClassName("lockcw7");
                var stdchewat8 = document.getElementsByClassName("lockcw8");
                var stdchewat9 = document.getElementsByClassName("lockcw9");
                var stdchewat10 = document.getElementsByClassName("lockcw10");
                var stdchewat11 = document.getElementsByClassName("lockcw11");
                var stdchewat12 = document.getElementsByClassName("lockcw12");
                var stdchewat13 = document.getElementsByClassName("lockcw13");
                var stdchewat14 = document.getElementsByClassName("lockcw14");
                var stdchewat15 = document.getElementsByClassName("lockcw15");
                var stdchewat16 = document.getElementsByClassName("lockcw16");
                var stdchewat17 = document.getElementsByClassName("lockcw17");
                var stdchewat18 = document.getElementsByClassName("lockcw18");
                var stdchewat19 = document.getElementsByClassName("lockcw19");
                var stdchewat20 = document.getElementsByClassName("lockcw20");

                var stdbehave1 = document.getElementsByClassName("behavetxt1");
                var stdbehave2 = document.getElementsByClassName("behavetxt2");
                var stdbehave3 = document.getElementsByClassName("behavetxt3");
                var stdbehave4 = document.getElementsByClassName("behavetxt4");
                var stdbehave5 = document.getElementsByClassName("behavetxt5");
                var stdbehave6 = document.getElementsByClassName("behavetxt6");
                var stdbehave7 = document.getElementsByClassName("behavetxt7");
                var stdbehave8 = document.getElementsByClassName("behavetxt8");
                var stdbehave9 = document.getElementsByClassName("behavetxt9");
                var stdbehave10 = document.getElementsByClassName("behavetxt10");

                var stdmid1 = document.getElementsByClassName("lockm1");
                var stdmid2 = document.getElementsByClassName("lockm2");
                var stdmid3 = document.getElementsByClassName("lockm3");
                var stdmid4 = document.getElementsByClassName("lockm4");
                var stdmid5 = document.getElementsByClassName("lockm5");
                var stdmid6 = document.getElementsByClassName("lockm6");
                var stdmid7 = document.getElementsByClassName("lockm7");
                var stdmid8 = document.getElementsByClassName("lockm8");
                var stdmid9 = document.getElementsByClassName("lockm9");
                var stdmid10 = document.getElementsByClassName("lockm10");
                var stdfinal1 = document.getElementsByClassName("lockf1");
                var stdfinal2 = document.getElementsByClassName("lockf2");
                var stdfinal3 = document.getElementsByClassName("lockf3");
                var stdfinal4 = document.getElementsByClassName("lockf4");
                var stdfinal5 = document.getElementsByClassName("lockf5");
                var stdfinal6 = document.getElementsByClassName("lockf6");
                var stdfinal7 = document.getElementsByClassName("lockf7");
                var stdfinal8 = document.getElementsByClassName("lockf8");
                var stdfinal9 = document.getElementsByClassName("lockf9");
                var stdfinal10 = document.getElementsByClassName("lockf10");
                var stdmidtotal = document.getElementsByClassName("lockmidterm");
                var stdfinaltotal = document.getElementsByClassName("lockfinalterm");
                var stdsamatanatotal = document.getElementsByClassName("samatscore");
                var stdreadwritetotal = document.getElementsByClassName("readscore");
                var stdbehavetotal = document.getElementsByClassName("goodbe");
                var special = document.getElementsByClassName("special");
                
                for (var x = 0; x < stdidlist.length; x++)
                {
                    if(stdstatus[x].value == 1 || stdstatus[x].value == 2 || stdstatus[x].value == 3 || stdstatus[x].value == 5)
                    {
                        stdout1[x].classList.add('disable');
                        stdout1[x].classList.add('disable3');
                        stdout2[x].classList.add('disable');
                        stdout2[x].classList.add('disable3');
                        stdout3[x].classList.add('disable');
                        stdout3[x].classList.add('disable3');
                        stdout4[x].classList.add('disable');
                        stdout4[x].classList.add('disable3');
                        stdout5[x].classList.add('disable');
                        stdout5[x].classList.add('disable3');
                        stdout6[x].classList.add('disable');
                        stdout6[x].classList.add('disable3');
                        stdout7[x].classList.add('disable');
                        stdout7[x].classList.add('disable3');
                        stdout8[x].classList.add('disable');
                        stdout8[x].classList.add('disable3');
                        stdout9[x].classList.add('disable');
                        stdout9[x].classList.add('disable3');
                        stdout10[x].classList.add('disable');
                        stdout10[x].classList.add('disable3');
                        stdout11[x].classList.add('disable');
                        stdout11[x].classList.add('disable3');
                        stdout12[x].classList.add('disable');
                        stdout12[x].classList.add('disable3');
                        stdout13[x].classList.add('disable');
                        stdout13[x].classList.add('disable3');
                        stdout14[x].classList.add('disable');
                        stdout14[x].classList.add('disable3');
                        stdout15[x].classList.add('disable');
                        stdout15[x].classList.add('disable3');
                        stdout16[x].classList.add('disable');
                        stdout16[x].classList.add('disable3');
                        stdout17[x].classList.add('disable');
                        stdout17[x].classList.add('disable3');
                        stdout18[x].classList.add('disable');
                        stdout18[x].classList.add('disable3');
                        stdout19[x].classList.add('disable');
                        stdout19[x].classList.add('disable3');
                        stdout20[x].classList.add('disable');
                        stdout20[x].classList.add('disable3');
                        stdout21[x].classList.add('disable');
                        stdout21[x].classList.add('disable3');
                        stdout22[x].classList.add('disable');
                        stdout22[x].classList.add('disable3');
                        stdout23[x].classList.add('disable');
                        stdout23[x].classList.add('disable3');
                        stdout24[x].classList.add('disable');
                        stdout24[x].classList.add('disable3');
                        stdout25[x].classList.add('disable');
                        stdout25[x].classList.add('disable3');
                        stdout26[x].classList.add('disable');
                        stdout26[x].classList.add('disable3');
                        stdout27[x].classList.add('disable');
                        stdout27[x].classList.add('disable3');
                        stdout28[x].classList.add('disable');
                        stdout28[x].classList.add('disable3');
                        stdout29[x].classList.add('disable');
                        stdout29[x].classList.add('disable3');
                        stdout30[x].classList.add('disable');
                        stdout30[x].classList.add('disable3');
                        stdout31[x].classList.add('disable');
                        stdout31[x].classList.add('disable3');
                        stdout32[x].classList.add('disable');
                        stdout32[x].classList.add('disable3');
                        stdout33[x].classList.add('disable');
                        stdout33[x].classList.add('disable3');
                        stdout34[x].classList.add('disable');
                        stdout34[x].classList.add('disable3');
                        stdout35[x].classList.add('disable');
                        stdout35[x].classList.add('disable3');
                        stdout36[x].classList.add('disable');
                        stdout36[x].classList.add('disable3');
                        stdout37[x].classList.add('disable');
                        stdout37[x].classList.add('disable3');
                        stdout38[x].classList.add('disable');
                        stdout38[x].classList.add('disable3');
                        stdout39[x].classList.add('disable');
                        stdout39[x].classList.add('disable3');
                        stdout40[x].classList.add('disable');
                        stdout40[x].classList.add('disable3');
                        stdout41[x].classList.add('disable');
                        stdout41[x].classList.add('disable3');
                        stdout42[x].classList.add('disable');
                        stdout42[x].classList.add('disable3');
                        stdout43[x].classList.add('disable');
                        stdout43[x].classList.add('disable3');
                        stdout44[x].classList.add('disable');
                        stdout44[x].classList.add('disable3');
                        stdout45[x].classList.add('disable');
                        stdout45[x].classList.add('disable3');
                        stdout46[x].classList.add('disable');
                        stdout46[x].classList.add('disable3');
                        stdout47[x].classList.add('disable');
                        stdout47[x].classList.add('disable3');
                        stdout48[x].classList.add('disable');
                        stdout48[x].classList.add('disable3');
                        stdout49[x].classList.add('disable');
                        stdout49[x].classList.add('disable3');
                        stdout50[x].classList.add('disable');
                        stdout50[x].classList.add('disable3');
                        stdout51[x].classList.add('disable');
                        stdout51[x].classList.add('disable3');
                        stdout52[x].classList.add('disable');
                        stdout52[x].classList.add('disable3');
                        stdout53[x].classList.add('disable');
                        stdout53[x].classList.add('disable3');
                        stdout54[x].classList.add('disable');
                        stdout54[x].classList.add('disable3');
                        stdout55[x].classList.add('disable');
                        stdout55[x].classList.add('disable3');
                        stdout56[x].classList.add('disable');
                        stdout56[x].classList.add('disable3');
                        stdout57[x].classList.add('disable');
                        stdout57[x].classList.add('disable3');
                        stdout58[x].classList.add('disable');
                        stdout58[x].classList.add('disable3');
                        stdout59[x].classList.add('disable');
                        stdout59[x].classList.add('disable3');
                        stdout60[x].classList.add('disable');
                        stdout60[x].classList.add('disable3');
                        stdout61[x].classList.add('disable');
                        stdout61[x].classList.add('disable3');
                        stdout62[x].classList.add('disable');
                        stdout62[x].classList.add('disable3');
                        stdout63[x].classList.add('disable');
                        stdout63[x].classList.add('disable3');
                        stdout64[x].classList.add('disable');
                        stdout64[x].classList.add('disable3');
                        stdout65[x].classList.add('disable');
                        stdout65[x].classList.add('disable3');
                        stdout66[x].classList.add('disable');
                        stdout66[x].classList.add('disable3');
                        stdout67[x].classList.add('disable');
                        stdout67[x].classList.add('disable3');
                        stdout68[x].classList.add('disable');
                        stdout68[x].classList.add('disable3');
                        stdout69[x].classList.add('disable');
                        stdout69[x].classList.add('disable3');
                        stdout70[x].classList.add('disable');
                        stdout70[x].classList.add('disable3');
                        stdout71[x].classList.add('disable');
                        stdout71[x].classList.add('disable3');
                        stdout72[x].classList.add('disable');
                        stdout72[x].classList.add('disable3');
                        stdout73[x].classList.add('disable');
                        stdout73[x].classList.add('disable3');
                        stdout74[x].classList.add('disable');
                        stdout74[x].classList.add('disable3');
                        stdout75[x].classList.add('disable');
                        stdout75[x].classList.add('disable3');
                        stdout76[x].classList.add('disable');
                        stdout76[x].classList.add('disable3');
                        stdout77[x].classList.add('disable');
                        stdout77[x].classList.add('disable3');
                        stdout78[x].classList.add('disable');
                        stdout78[x].classList.add('disable3');
                        stdout79[x].classList.add('disable');
                        stdout79[x].classList.add('disable3');
                        stdout80[x].classList.add('disable');
                        stdout80[x].classList.add('disable3');
                        stdout81[x].classList.add('disable');
                        stdout81[x].classList.add('disable3');

                        stdscore1[x].classList.add('disable');
                        stdscore2[x].classList.add('disable');
                        stdscore3[x].classList.add('disable');
                        stdscore4[x].classList.add('disable');
                        stdscore5[x].classList.add('disable');
                        stdscore6[x].classList.add('disable');
                        stdscore7[x].classList.add('disable');
                        stdscore8[x].classList.add('disable');
                        stdscore9[x].classList.add('disable');
                        stdscore10[x].classList.add('disable');
                        stdscore11[x].classList.add('disable');
                        stdscore12[x].classList.add('disable');
                        stdscore13[x].classList.add('disable');
                        stdscore14[x].classList.add('disable');
                        stdscore15[x].classList.add('disable');
                        stdscore16[x].classList.add('disable');
                        stdscore17[x].classList.add('disable');
                        stdscore18[x].classList.add('disable');
                        stdscore19[x].classList.add('disable');
                        stdscore20[x].classList.add('disable');

                        stdchewat1[x].classList.add('disable');
                        stdchewat2[x].classList.add('disable');
                        stdchewat3[x].classList.add('disable');
                        stdchewat4[x].classList.add('disable');
                        stdchewat5[x].classList.add('disable');
                        stdchewat6[x].classList.add('disable');
                        stdchewat7[x].classList.add('disable');
                        stdchewat8[x].classList.add('disable');
                        stdchewat9[x].classList.add('disable');
                        stdchewat10[x].classList.add('disable');
                        stdchewat11[x].classList.add('disable');
                        stdchewat12[x].classList.add('disable');
                        stdchewat13[x].classList.add('disable');
                        stdchewat14[x].classList.add('disable');
                        stdchewat15[x].classList.add('disable');
                        stdchewat16[x].classList.add('disable');
                        stdchewat17[x].classList.add('disable');
                        stdchewat18[x].classList.add('disable');
                        stdchewat19[x].classList.add('disable');
                        stdchewat20[x].classList.add('disable');

                        stdbehave1[x].classList.add('disable');
                        stdbehave2[x].classList.add('disable');
                        stdbehave3[x].classList.add('disable');
                        stdbehave4[x].classList.add('disable');
                        stdbehave5[x].classList.add('disable');
                        stdbehave6[x].classList.add('disable');
                        stdbehave7[x].classList.add('disable');
                        stdbehave8[x].classList.add('disable');
                        stdbehave9[x].classList.add('disable');
                        stdbehave10[x].classList.add('disable');

                        stdmid1[x].classList.add('disable');
                        stdmid2[x].classList.add('disable');
                        stdmid3[x].classList.add('disable');
                        stdmid4[x].classList.add('disable');
                        stdmid5[x].classList.add('disable');
                        stdmid6[x].classList.add('disable');
                        stdmid7[x].classList.add('disable');
                        stdmid8[x].classList.add('disable');
                        stdmid9[x].classList.add('disable');
                        stdmid10[x].classList.add('disable');

                        stdfinal1[x].classList.add('disable');
                        stdfinal2[x].classList.add('disable');
                        stdfinal3[x].classList.add('disable');
                        stdfinal4[x].classList.add('disable');
                        stdfinal5[x].classList.add('disable');
                        stdfinal6[x].classList.add('disable');
                        stdfinal7[x].classList.add('disable');
                        stdfinal8[x].classList.add('disable');
                        stdfinal9[x].classList.add('disable');
                        stdfinal10[x].classList.add('disable');
                        
                        stdmidtotal[x].classList.add('disable');
                        stdfinaltotal[x].classList.add('disable');
                        stdsamatanatotal[x].classList.add('disable');
                        stdreadwritetotal[x].classList.add('disable');
                        stdbehavetotal[x].classList.add('disable');
                        special[x].classList.add('disable');
                        
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
                
                //var t0 = performance.now();
                setupddl();
                changeddl();
                changename();
                editddl();
                autobehave();
                CompareDates(99999);
                auto();
                lockpage();
                checkstatus();
                editPillbox();

                
                //var t1 = performance.now();
                //console.log("Call to doSomething took " + (t1 - t0) + " milliseconds.");
                var cog1 = document.getElementsByClassName("cog1");
                var cog2 = document.getElementsByClassName("cog2");
                var check5 = document.getElementsByClassName("check5");
                var scorebox = document.getElementsByClassName("scorebox");
                var btnok = document.getElementsByClassName("btnok");
                var btnerror = document.getElementsByClassName("btnerror");
                var btnback = document.getElementsByClassName("btnback");
                var lastbtn = document.getElementsByClassName("lastbtn");
               
                var periodnow = document.getElementsByClassName("periodnow");
                var periodsubmit = document.getElementsByClassName("periodsubmit");
                periodsubmit[0].value = periodnow[0].value;

                var periodclass = document.getElementsByClassName("periodclass");
                var period = document.getElementsByClassName("period");
                if(period[0].textContent.length <5)
                    periodclass[0].classList.add('hidden');

                var viplogin = document.getElementsByClassName("viplogin");
                if (viplogin[0].value == "2") {
                    //btnok[0].classList.add('hidden');
                    //btnerror[0].classList.add('hidden');
                }

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
                
                if (mode[1] == "3") {
                    scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132261") %>";
                    btnok[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132269") %>";
                    btnerror[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132269") %>";
                    btnback[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>";
                    lastbtn[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206088") %>";
                    nextpage();
                }
                else if (mode[1] != "EN")
                {
                    scorebox[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132261") %>";
                    btnok[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601083") %>";
                    btnerror[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601083") %>";
                    btnback[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %>";
                    lastbtn[0].value = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206088") %>";
                }                
                else {
                    scorebox[0].value = "Exercise 1-5";
                    btnok[0].value = "Submit";
                    btnerror[0].value = "Submit";
                    btnback[0].value = "Back";
                    lastbtn[0].value = "Last Submit";
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
                    trigger: 'hover',
                    scheme: 'black'
                });
                el2.protipShow({
                    title: set2[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                el3.protipShow({
                    title: set3[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                el4.protipShow({
                    title: set4[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                el5.protipShow({
                    title: set5[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                el6.protipShow({
                    title: set6[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                el7.protipShow({
                    title: set7[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                el8.protipShow({
                    title: set8[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                el9.protipShow({
                    title: set9[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                el10.protipShow({
                    title: set10[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                el11.protipShow({
                    title: set11[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                el12.protipShow({
                    title: set12[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                el13.protipShow({
                    title: set13[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                el14.protipShow({
                    title: set14[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                el15.protipShow({
                    title: set15[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                el16.protipShow({
                    title: set16[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                el17.protipShow({
                    title: set17[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                el18.protipShow({
                    title: set18[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                el19.protipShow({
                    title: set19[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                el20.protipShow({
                    title: set20[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elb1.protipShow({
                    title: setb1[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elb2.protipShow({
                    title: setb2[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elb3.protipShow({
                    title: setb3[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elb4.protipShow({
                    title: setb4[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elb5.protipShow({
                    title: setb5[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elb6.protipShow({
                    title: setb6[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elb7.protipShow({
                    title: setb7[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elb8.protipShow({
                    title: setb8[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elb9.protipShow({
                    title: setb9[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elb10.protipShow({
                    title: setb10[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elc1.protipShow({
                    title: setcw1[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elc2.protipShow({
                    title: setcw2[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elc3.protipShow({
                    title: setcw3[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elc4.protipShow({
                    title: setcw4[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elc5.protipShow({
                    title: setcw5[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elc6.protipShow({
                    title: setcw6[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elc7.protipShow({
                    title: setcw7[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elc8.protipShow({
                    title: setcw8[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elc9.protipShow({
                    title: setcw9[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elc10.protipShow({
                    title: setcw10[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elc11.protipShow({
                    title: setcw11[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elc12.protipShow({
                    title: setcw12[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elc13.protipShow({
                    title: setcw13[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elc14.protipShow({
                    title: setcw14[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elc15.protipShow({
                    title: setcw15[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elc16.protipShow({
                    title: setcw16[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elc17.protipShow({
                    title: setcw17[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elc18.protipShow({
                    title: setcw18[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elc19.protipShow({
                    title: setcw19[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                elc20.protipShow({
                    title: setcw20[0].value,
                    trigger: 'hover',
                    scheme: 'black'
                });
                var protipshow = document.getElementsByClassName("protip-show");

                while (protipshow.length != 0) {
                    protipshow[0].classList.remove('protip-show');
                }

                setTimeout(function () {
                    removeprotip();
                }, 1);

                if (mode[1] != "3") {
                    $('#loading').hide();
                }
                    
            }

            function removeprotip() {
                var protipshow = document.getElementsByClassName("protip-show");
                
                while (protipshow.length != 0) {
                    protipshow[0].classList.remove('protip-show');
                }
                
            }

            $(window).on('load', function () {
                start();
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
        <div class="full-card box-content" id="mainDiv" style="padding-left:10px;padding-right:20px; padding-top:10px;">
           <!-- Sidebar -->

       
<!-- Page Content -->
<div class="w3-overlay w3-animate-opacity" onclick="w3_close()" style="cursor:pointer" id="myOverlay"></div>

<div>
  <div class="w3-button w3-teal w3-xlarge w3-right " style="position:fixed;top:90px;right:10px; z-index:4; border:1px solid black;" onclick="w3_open()"><asp:Label ID="config" cssclass="" runat="server" > </asp:Label></div>
  <%--<div class="w3-button w3-deep-purple w3-xlarge w3-right " style="position:fixed;top:150px;right:10px; z-index:4; border:1px solid black;" id="btnOpenGroupSidebar" onclick="OpenGroupSidebar();LogicOpenGroupSidebar();"><asp:Label ID="Label6" cssclass="" runat="server" ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132270") %></asp:Label></div>--%>
     <button type="button" class="w3-button w3-deep-purple w3-xlarge w3-right " style="position:fixed;top:150px;right:10px; z-index:4; border:1px solid black;" id="btnOpenGroupSidebar" onclick="OpenGroupSidebar();"
         data-original-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132271") %>  <br> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132272") %> <br> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132273") %>  <br>  <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132274") %>" data-html="true" data-placement="bottom" data-toggle="tooltip"><asp:Label ID="Label8" cssclass="" runat="server" ><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132270") %></asp:Label>
     </button>

</div>


     
<script>
    function w3_open() {
        document.getElementById("mySidebar").style.display = "block";
        document.getElementById("myOverlay").style.display = "block";
    }

    function OpenGroupSidebar(){
         document.getElementById("groupSidebar").style.display = "block";
        document.getElementById("myOverlay").style.display = "block";
    }
    function w3_close() {

        document.getElementById("mySidebar").style.display = "none";
        document.getElementById("groupSidebar").style.display = "none";
        document.getElementById("myOverlay").style.display = "none";
}
</script>
               
            <div class="col-xs-12" style="padding:0px">
                <asp:Textbox ID="Label5" cssclass="autotext" disabled="true" runat="server" style="width: 100%;font-size: 60%;border: none;color:red;background-color:white;" > </asp:Textbox>
                </div>
            <div class="col-xs-12" style="padding:0px">
                <div class="col-xs-1 righttext" style="padding:0px; font-size:98%">                            
                    <asp:Label ID="headertxtclass" cssclass="bold" runat="server" > </asp:Label>
                </div>

                <div class="col-xs-3">
                    <asp:Label ID="txtclass" cssclass="userplan"                                                                
                               runat="server">                                    
                    </asp:Label>                           
                </div>
                <div class="col-xs-1 righttext" style="padding:0px;font-size:98%">                            
                    <asp:Label ID="headertxtyear" cssclass="bold" runat="server" > </asp:Label>
                </div>
                <div class="col-xs-3" style="padding-right:0px;">
                    <asp:Label ID="Year"  cssclass="useryear"                                                                                    
                               runat="server">                                    
                    </asp:Label>                            
                </div> 
                <div class="col-xs-2" style="padding-right:0px">
                    <label class="btn btn-success" style="height:37px; text-align: center;font-size:70%" for="upload">
  <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206076") %>
</label> 
                    </div>
                <div class="col-xs-2" style="padding:0px;">
                    <div class="dropdown">
  <button class="btn btn-success" style="pointer-events:none;height:37px; text-align: center; line-height: 1px;">Export to Excel</button>
  <div class="dropdown-content">
    <a href="#" onclick="print(1)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00323") %> 1</a>
    <a href="#" onclick="print(4)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206080") %></a>
      <a href="#" onclick="print(5)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206081") %></a>
    <a href="#" onclick="print(2)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206084") %></a>
    <a href="#" onclick="print(3)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206085") %></a>
    
  </div>
</div>
                    </div>  
                             
            </div>

            <div class="col-xs-12" style="padding:0px;">                

<!-- The Modal -->
<div id="myModal" class="modal2" onmousedown="mdown()">
  <span class="close2" onclick="popup2()">&times;</span>
  <img class="modal2-content" id="img01">
  <div id="caption"></div>
</div>
                <div id="loading"></div>
                <div id="loading2" class="loadstatus hidden"></div>
                <div class="col-xs-12" style="padding:0px;">
                <div class="col-xs-12" style="height:0.1px;">
                <iframe id="list1" height="0.1px" width="0.1px" src="" frameBorder="0" name="iframe_a"></iframe>
                    <iframe id="list2" height="0.1px" width="0.1px" src="" frameBorder="0" name="iframe_a"></iframe>
                    <iframe id="list3" height="0.1px" width="0.1px" src="" frameBorder="0" name="iframe_a"></iframe>
                    <iframe id="list4" height="0.1px" width="0.1px" src="" frameBorder="0" name="iframe_a"></iframe>
                    <iframe id="list5" height="0.1px" width="0.1px" src="" frameBorder="0" name="iframe_a"></iframe>
                </div>
            </div>
                <div class="col-xs-1 righttext" style="padding:0px;font-size:98%;">   
                            
                    <asp:Label ID="headerteacher1" cssclass="bold font90"  runat="server" > </asp:Label>
                </div>

                <div class="col-xs-3">
                    <asp:Label ID="teacher1" cssclass="userplan"                                                                
                               runat="server">                                    
                    </asp:Label>                           
                </div>
                <div class="col-xs-1 righttext" style="padding:0px;font-size:98%">                            
                    <asp:Label ID="headerplanname" cssclass="bold" runat="server" > </asp:Label>
                </div>
                <div class="col-xs-6">
                    <asp:Label ID="planname"                                                                                     
                               runat="server">                                    
                    </asp:Label>
                            
                </div>
                
            </div>
           
            <div class="col-xs-12" style="padding:0px">
                <div class="col-xs-1 righttext" style="padding:0px;font-size:98%">                            
                    <asp:Label ID="headerteacher2" cssclass="bold" runat="server" > </asp:Label>
                </div>
                <div class="col-xs-11">
                    <asp:Label ID="teacher2"                                                                                     
                               runat="server">                                    
                    </asp:Label>
                            
                </div>
                
                
            </div>
            
                <div class="col-xs-3" style="padding:0px">
                    <form enctype="multipart/form-data">
    <input id="upload" class="btn btn-success pull-right" type=file  name="files[]">
</form>
                    
   

    <script>
        document.getElementById('upload').addEventListener('change', handleFileSelect, false);

    </script>
                    </div>
            <div class="col-xs-12 periodclass">
                <div class="col-xs-4 righttext" style="padding:0px;">                            
                    <asp:Label ID="submitPeriodheader" cssclass="bold" runat="server" > </asp:Label>
                </div>
                <div class="col-xs-8">
                    <asp:Label ID="submitPeriod" cssclass="period"                                                                                      
                               runat="server">                                    
                    </asp:Label>
                            
                </div>                                
            </div>
            <div class="col-xs-12 hidden">
                <asp:Textbox ID="gradeidtxt" cssclass="autosavedata" runat="server"> </asp:Textbox>
                <asp:Textbox ID="idlv2txt" cssclass="autosavedata" runat="server"> </asp:Textbox>
                <asp:Textbox ID="planidtxt" cssclass="autosavedata" runat="server"> </asp:Textbox>
                <asp:Textbox ID="termtxt" cssclass="autosavedata" runat="server"> </asp:Textbox>
                <asp:Textbox ID="yeartxt" cssclass="autosavedata" runat="server"> </asp:Textbox>
                </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-2 righttext" style="padding:0px;">                            
<label>period now </label>                </div>
                <div class="col-xs-10">
                    <asp:Textbox ID="periodNow" cssclass="periodnow"                                                                                      
                               runat="server">                                    
                    </asp:Textbox>                            
                </div>                                
            </div>

            <div class="col-xs-12 hidden">
                <div class="col-xs-2 righttext" style="padding:0px;">                            
<label>period submit </label>                </div>
                <div class="col-xs-10">
                    <asp:Textbox ID="periodSubmit" cssclass="periodsubmit"                                                                                      
                               runat="server">                                    
                    </asp:Textbox>                            
                </div>                                
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-2 righttext" style="padding:0px;">                            
<label>vip login</label>                </div>
                <div class="col-xs-10">
                    <asp:Textbox ID="viplogin" cssclass="viplogin"                                                                                      
                               runat="server">                                    
                    </asp:Textbox>                            
                </div>                                
            </div>

             <div class="col-xs-12 hidden">
                <div class="col-xs-1 righttext">                            
                    <label>popupnomax</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="Textbox22" runat="server" CssClass="popupnomax2" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g1</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkg1" runat="server" CssClass="lock1b" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g2</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkg2" runat="server" CssClass="lock1b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g3</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkg3" runat="server" CssClass="lock1b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g4</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkg4" runat="server" CssClass="lock1b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g5</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkg5" runat="server" CssClass="lock1b" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-1 righttext">                            
                    <label>g6</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkg6" runat="server" CssClass="lock1b" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g7</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkg7" runat="server" CssClass="lock1b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g8</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkg8" runat="server" CssClass="lock1b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g9</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkg9" runat="server" CssClass="lock1b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g10</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkg10" runat="server" CssClass="lock1b" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-1 righttext">                            
                    <label>g11</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkg11" runat="server" CssClass="lock1b" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g12</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkg12" runat="server" CssClass="lock1b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g13</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkg13" runat="server" CssClass="lock1b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g14</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkg14" runat="server" CssClass="lock1b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g15</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkg15" runat="server" CssClass="lock1b" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-1 righttext">                            
                    <label>g16</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkg16" runat="server" CssClass="lock1b" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g17</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkg17" runat="server" CssClass="lock1b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g18</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkg18" runat="server" CssClass="lock1b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g19</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkg19" runat="server" CssClass="lock1b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g20</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkg20" runat="server" CssClass="lock1b" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-1 righttext">                            
                    <label>cw1</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkcw1" runat="server" CssClass="lock2b" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw2</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkcw2" runat="server" CssClass="lock2b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw3</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkcw3" runat="server" CssClass="lock2b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw4</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkcw4" runat="server" CssClass="lock2b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw5</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkcw5" runat="server" CssClass="lock2b" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-1 righttext">                            
                    <label>cw6</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkcw6" runat="server" CssClass="lock2b" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw7</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkcw7" runat="server" CssClass="lock2b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw8</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkcw8" runat="server" CssClass="lock2b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw9</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkcw9" runat="server" CssClass="lock2b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw10</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkcw10" runat="server" CssClass="lock2b" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-1 righttext">                            
                    <label>cw11</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkcw11" runat="server" CssClass="lock2b" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw12</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkcw12" runat="server" CssClass="lock2b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw13</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkcw13" runat="server" CssClass="lock2b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw14</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkcw14" runat="server" CssClass="lock2b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw15</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkcw15" runat="server" CssClass="lock2b" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-1 righttext">                            
                    <label>cw16</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkcw16" runat="server" CssClass="lock2b" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw17</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkcw17" runat="server" CssClass="lock2b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw18</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkcw18" runat="server" CssClass="lock2b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw19</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkcw19" runat="server" CssClass="lock2b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw20</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkcw20" runat="server" CssClass="lock2b" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-1 righttext">                            
                    <label>m1</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkm1" runat="server" CssClass="lock3b" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>m2</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkm2" runat="server" CssClass="lock3b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>m3</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkm3" runat="server" CssClass="lock3b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>m4</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkm4" runat="server" CssClass="lock3b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>m5</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkm5" runat="server" CssClass="lock3b" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-1 righttext">                            
                    <label>m6</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkm6" runat="server" CssClass="lock3b" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>m7</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkm7" runat="server" CssClass="lock3b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>m8</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkm8" runat="server" CssClass="lock3b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>m9</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkm9" runat="server" CssClass="lock3b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>m10</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkm10" runat="server" CssClass="lock3b" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-1 righttext">                            
                    <label>f1</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkf1" runat="server" CssClass="lock4b" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>f2</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkf2" runat="server" CssClass="lock4b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>f3</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkf3" runat="server" CssClass="lock4b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>f4</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkf4" runat="server" CssClass="lock4b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>f5</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkf5" runat="server" CssClass="lock4b" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-1 righttext">                            
                    <label>f6</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkf6" runat="server" CssClass="lock4b" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>f7</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkf7" runat="server" CssClass="lock4b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>f8</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkf8" runat="server" CssClass="lock4b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>f9</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkf9" runat="server" CssClass="lock4b" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>f10</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="checkf10" runat="server" CssClass="lock4b" width="50%" > </asp:Textbox>                           
                </div>
            </div>

            <div class="col-xs-12 hidden">
                <div class="col-xs-1 righttext">                            
                    <label>popupnomax</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="nomax" runat="server" CssClass="popupnomax" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g1</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockg1" runat="server" CssClass="lock1" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g2</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockg2" runat="server" CssClass="lock1" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g3</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockg3" runat="server" CssClass="lock1" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g4</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockg4" runat="server" CssClass="lock1" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g5</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockg5" runat="server" CssClass="lock1" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-1 righttext">                            
                    <label>g6</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockg6" runat="server" CssClass="lock1" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g7</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockg7" runat="server" CssClass="lock1" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g8</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockg8" runat="server" CssClass="lock1" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g9</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockg9" runat="server" CssClass="lock1" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g10</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockg10" runat="server" CssClass="lock1" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-1 righttext">                            
                    <label>g11</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockg11" runat="server" CssClass="lock1" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g12</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockg12" runat="server" CssClass="lock1" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g13</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockg13" runat="server" CssClass="lock1" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g14</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockg14" runat="server" CssClass="lock1" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g15</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockg15" runat="server" CssClass="lock1" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-1 righttext">                            
                    <label>g16</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockg16" runat="server" CssClass="lock1" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g17</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockg17" runat="server" CssClass="lock1" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g18</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockg18" runat="server" CssClass="lock1" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g19</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockg19" runat="server" CssClass="lock1" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>g20</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockg20" runat="server" CssClass="lock1" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-1 righttext">                            
                    <label>cw1</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockcw1" runat="server" CssClass="lock2" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw2</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockcw2" runat="server" CssClass="lock2" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw3</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockcw3" runat="server" CssClass="lock2" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw4</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockcw4" runat="server" CssClass="lock2" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw5</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockcw5" runat="server" CssClass="lock2" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-1 righttext">                            
                    <label>cw6</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockcw6" runat="server" CssClass="lock2" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw7</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockcw7" runat="server" CssClass="lock2" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw8</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockcw8" runat="server" CssClass="lock2" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw9</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockcw9" runat="server" CssClass="lock2" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw10</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockcw10" runat="server" CssClass="lock2" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-1 righttext">                            
                    <label>cw11</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockcw11" runat="server" CssClass="lock2" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw12</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockcw12" runat="server" CssClass="lock2" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw13</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockcw13" runat="server" CssClass="lock2" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw14</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockcw14" runat="server" CssClass="lock2" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw15</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockcw15" runat="server" CssClass="lock2" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-1 righttext">                            
                    <label>cw16</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockcw16" runat="server" CssClass="lock2" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw17</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockcw17" runat="server" CssClass="lock2" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw18</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockcw18" runat="server" CssClass="lock2" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw19</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockcw19" runat="server" CssClass="lock2" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>cw20</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockcw20" runat="server" CssClass="lock2" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-1 righttext">                            
                    <label>m1</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockm1" runat="server" CssClass="lock3" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>m2</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockm2" runat="server" CssClass="lock3" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>m3</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockm3" runat="server" CssClass="lock3" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>m4</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockm4" runat="server" CssClass="lock3" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>m5</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockm5" runat="server" CssClass="lock3" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-1 righttext">                            
                    <label>m6</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockm6" runat="server" CssClass="lock3" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>m7</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockm7" runat="server" CssClass="lock3" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>m8</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockm8" runat="server" CssClass="lock3" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>m9</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockm9" runat="server" CssClass="lock3" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>m10</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockm10" runat="server" CssClass="lock3" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-1 righttext">                            
                    <label>f1</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockf1" runat="server" CssClass="lock4" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>f2</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockf2" runat="server" CssClass="lock4" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>f3</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockf3" runat="server" CssClass="lock4" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>f4</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockf4" runat="server" CssClass="lock4" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>f5</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockf5" runat="server" CssClass="lock4" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-1 righttext">                            
                    <label>f6</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockf6" runat="server" CssClass="lock4" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>f7</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockf7" runat="server" CssClass="lock4" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>f8</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockf8" runat="server" CssClass="lock4" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>f9</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockf9" runat="server" CssClass="lock4" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>f10</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockf10" runat="server" CssClass="lock4" width="50%" > </asp:Textbox>                           
                </div>
            </div>
            <div class="col-xs-12 hidden">
                <div class="col-xs-1 righttext">                            
                    <label>midterm</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockmid" runat="server" CssClass="lockmid" width="50%" > </asp:Textbox>                          
                </div>
                <div class="col-xs-1 righttext">                            
                    <label>finalterm</label>
                </div>
                <div class="col-xs-1">
                    <asp:Textbox ID="lockfinal" runat="server" CssClass="lockfinal" width="50%" > </asp:Textbox>                           
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
                <div class="col-xs-2 righttext">                            
                    <label>copy3</label>
                </div>
                <div class="col-xs-4">
                    <asp:Textbox ID="Textbox18" runat="server" CssClass="copy3" width="50%" > </asp:Textbox>                           
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
                <div class="col-xs-2 righttext">                            
                    <label>ddl9</label>
                </div>
                <div class="col-xs-4">
                    <asp:Textbox ID="ddl9" runat="server" CssClass="autoddl9" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-2 righttext">                            
                    <label>ddl11</label>
                </div>
                <div class="col-xs-4">
                    <asp:Textbox ID="ddl11" runat="server" CssClass="autoddl11" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-2 righttext">                            
                    <label>ddl12</label>
                </div>
                <div class="col-xs-4">
                    <asp:Textbox ID="ddl12" runat="server" CssClass="autoddl12" width="50%" > </asp:Textbox>                           
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
                <div class="col-xs-2 righttext">                            
                    <label>setupddl9</label>
                </div>
                <div class="col-xs-4">
                    <asp:Textbox ID="setup9" runat="server" CssClass="setup9" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-2 righttext">                            
                    <label>setupddl11</label>
                </div>
                <div class="col-xs-4">
                    <asp:Textbox ID="setup11" runat="server" CssClass="setup11" width="50%" > </asp:Textbox>                           
                </div>
                <div class="col-xs-2 righttext">                            
                    <label>setupddl12</label>
                </div>
                <div class="col-xs-4">
                    <asp:Textbox ID="setup12" runat="server" CssClass="setup12" width="50%" > </asp:Textbox>                           
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

                     <!-- mid1 -->
                     <div class="modal fade" id="myModalmid1" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="midmodalname1" runat="server" CssClass="set1midcw" onkeyup="changename(81)" > </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
                     <!-- mid2 -->
                     <div class="modal fade" id="myModalmid2" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="midmodalname2" runat="server" CssClass="set2midcw" onkeyup="changename(82)" > </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
                     <!-- mid1 -->
                     <div class="modal fade" id="myModalmid3" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="midmodalname3" runat="server" CssClass="set3midcw" onkeyup="changename(83)" > </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
                     <!-- mid1 -->
                     <div class="modal fade" id="myModalmid4" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="midmodalname4" runat="server" CssClass="set4midcw" onkeyup="changename(84)" > </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
                     <!-- mid1 -->
                     <div class="modal fade" id="myModalmid5" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="midmodalname5" runat="server" CssClass="set5midcw" onkeyup="changename(85)" > </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
<!-- mid1 -->
                     <div class="modal fade" id="myModalmid6" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="midmodalname6" runat="server" CssClass="set6midcw" onkeyup="changename(86)" > </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
                     <!-- mid1 -->
                     <div class="modal fade" id="myModalmid7" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="midmodalname7" runat="server" CssClass="set7midcw" onkeyup="changename(87)" > </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
                     <!-- mid1 -->
                     <div class="modal fade" id="myModalmid8" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="midmodalname8" runat="server" CssClass="set8midcw" onkeyup="changename(88)" > </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
                     <!-- mid1 -->
                     <div class="modal fade" id="myModalmid9" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="midmodalname9" runat="server" CssClass="set9midcw" onkeyup="changename(89)" > </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
                     <!-- mid1 -->
                     <div class="modal fade" id="myModalmid10" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="midmodalname10" runat="server" CssClass="set10midcw" onkeyup="changename(90)" > </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
<!-- final -->
                     <div class="modal fade" id="myModalfinal1" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="finalmodalname1" runat="server" CssClass="set1finalcw" onkeyup="changename(91)" > </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
                     <!-- mid1 -->
                     <div class="modal fade" id="myModalfinal2" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="finalmodalname2" runat="server" CssClass="set2finalcw" onkeyup="changename(92)" > </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
                     <!-- mid1 -->
                     <div class="modal fade" id="myModalfinal3" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="finalmodalname3" runat="server" CssClass="set3finalcw" onkeyup="changename(93)" > </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
                     <!-- mid1 -->
                     <div class="modal fade" id="myModalfinal4" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="finalmodalname4" runat="server" CssClass="set4finalcw" onkeyup="changename(94)" > </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
                     <!-- mid1 -->
                     <div class="modal fade" id="myModalfinal5" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="finalmodalname5" runat="server" CssClass="set5finalcw" onkeyup="changename(95)" > </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
<!-- mid1 -->
                     <div class="modal fade" id="myModalfinal6" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="finalmodalname6" runat="server" CssClass="set6finalcw" onkeyup="changename(96)" > </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
                     <!-- mid1 -->
                     <div class="modal fade" id="myModalfinal7" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="finalmodalname7" runat="server" CssClass="set7finalcw" onkeyup="changename(97)" > </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
                     <!-- mid1 -->
                     <div class="modal fade" id="myModalfinal8" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="finalmodalname8" runat="server" CssClass="set8finalcw" onkeyup="changename(98)" > </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
                     <!-- mid1 -->
                     <div class="modal fade" id="myModalfinal9" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="finalmodalname9" runat="server" CssClass="set9finalcw" onkeyup="changename(99)" > </asp:Textbox>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
                     <!-- mid1 -->
                     <div class="modal fade" id="myModalfinal10" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00150") %></h4>
        </div>
        <div class="modal-body">
            <asp:Textbox ID="finalmodalname10" runat="server" CssClass="set10finalcw" onkeyup="changename(100)" > </asp:Textbox>
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
            <asp:DropDownList ID="ddlcopy1"  runat="server" CssClass="ddlcopy1" Width="100%">                  
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
            <asp:DropDownList ID="ddlcopy2"  runat="server" CssClass="ddlcopy2" Width="100%">                  
                                 <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206051") %>" Value="-1" class="grey hidden"></asp:ListItem>                                
                            </asp:DropDownList>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-success" data-dismiss="modal" onclick="ddlcopy(4)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %></button>
        </div>
      </div>
    </div>
  </div>         


                     <div class="modal fade" id="myModalcopy3" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132289") %></h4>
        </div>
        <div class="modal-body">
            <asp:DropDownList ID="ddlcopy3"  runat="server" CssClass="ddlcopy3" Width="100%">                  
                                 <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206051") %>" Value="-1" class="grey hidden"></asp:ListItem>                                
                            </asp:DropDownList>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-success" data-dismiss="modal" onclick="ddlcopy(5)"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %></button>
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
            <li class="bluebutton active"><a href="#one" style="font-size:80%;padding-left:11px;padding-right:10px;" onclick="nextbutton(1)" data-target="#one, #dgdone" data-toggle="tab"><asp:Label ID="tabscore" cssclass="" runat="server" > </asp:Label></a></li>
            <li class="bluebutton hidden"> <a href="#seven" style="font-size:80%;padding-left:11px;padding-right:11px;"  onclick="nextbutton(9)" data-target="#seven, #dgdseven" data-toggle="tab"><asp:Label ID="tabchewat" cssclass="" runat="server" > </asp:Label></a></li>
       <li class="bluebutton hidden"> <a href="#mid1" style="font-size:80%;padding-left:11px;padding-right:10px;"  onclick="nextbutton(21)" data-target="#mid1, #dgdmid1" data-toggle="tab"><asp:Label ID="tabmid" cssclass="" runat="server" > </asp:Label></a></li>
       <li class="bluebutton hidden"> <a href="#final1" style="font-size:80%;padding-left:11px;padding-right:10px;"  onclick="nextbutton(22)" data-target="#final1, #dgdfinal1" data-toggle="tab"><asp:Label ID="tabfinal" cssclass="" runat="server" > </asp:Label></a></li>
            <li class="bluebutton hidden"> <a href="#five" style="font-size:80%;padding-left:11px;padding-right:10px;"  onclick="nextbutton(7)" data-target="#five, #dgdfive" data-toggle="tab"><asp:Label ID="tabbehaveior" cssclass="" runat="server" > </asp:Label></a></li>                   
       <li class="pull-right"> <a href="#wrapper" style="font-size:80%;padding-left:11px;padding-right:10px;"  onclick="popup()" ><asp:Label ID="popuptxt" cssclass="" runat="server" > </asp:Label></a></li>
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
             <div class="col-xs-3" style="padding:0px; background-color:white;" >
                    <table class="table3" style="width:105%; height:70%; padding:0px;">
  <tr class="tdtr2">
     
    <th  class ="tdtr2 headerCell" style="border-left:1px; height:26px;">
        <div class="col-xs-12" style="padding:0px">
            <div class="col-xs-1" style="padding:0px">
                <div class="navigation">
   <ul id="myTab4" class="nav">
            <li class="left1 hidden"><a href="#one"style="padding:0px;" onclick="nextbutton(1)" data-target="#one, #dgdone" data-toggle="tab"><span class="glyphicon glyphicon-chevron-left" style="font-size:80%"></span></a></li>
            <li class="left2 hidden"><a href="#two"style="padding:0px;"  onclick="nextbutton(2)" data-target="#two, #dgdtwo" data-toggle="tab"><span class="glyphicon glyphicon-chevron-left" style="font-size:80%"></span></a></li>
            <li class="left3 hidden"><a href="#three" style="padding:0px"  onclick="nextbutton(3)" data-target="#three, #dgdthree" data-toggle="tab"><span class="glyphicon glyphicon-chevron-left" style="font-size:80%"></span></a></li>
            <li class="left4 hidden"><a href="#five"style="padding:0px;" onclick="nextbutton(7)" data-target="#five, #dgdfive" data-toggle="tab"><span class="glyphicon glyphicon-chevron-left" style="font-size:80%"></span></a></li>
            <li class="left5 hidden"><a href="#seven"style="padding:0px;" onclick="nextbutton(9)" data-target="#seven, #dgdseven" data-toggle="tab"><span class="glyphicon glyphicon-chevron-left" style="font-size:80%"></span></a></li>
            <li class="left6 hidden"><a href="#eight"style="padding:0px;"  onclick="nextbutton(10)" data-target="#eight, #dgdeight" data-toggle="tab"><span class="glyphicon glyphicon-chevron-left" style="font-size:80%"></span></a></li>
            <li class="left7 hidden"><a href="#nine" style="padding:0px"  onclick="nextbutton(11)" data-target="#nine, #dgdnine" data-toggle="tab"><span class="glyphicon glyphicon-chevron-left" style="font-size:80%"></span></a></li>
            <li class="left8 hidden"><a href="#mid1" style="padding:0px"  onclick="nextbutton(21)" data-target="#mid1, #dgdmid1" data-toggle="tab"><span class="glyphicon glyphicon-chevron-left" style="font-size:80%"></span></a></li>
            <li class="left9 hidden"><a href="#final1" style="padding:0px"  onclick="nextbutton(22)" data-target="#final1, #dgdfinal1" data-toggle="tab"><span class="glyphicon glyphicon-chevron-left" style="font-size:80%"></span></a></li>
        </ul>
                </div>
            </div>
            <div class="col-xs-9" style="height:25px;padding:0px;">
                <input style="height:80%; width:105%; color:white; background-color:#337AB7; border:none;" type="text" class="headertxt centertext scorebox" >
            </div>
            <div class="col-xs-1" style="padding-left:10px">
                <div class="navigation">
   <ul id="myTab3" class="nav">
            <li class="right1"><a href="#two"style="padding:0px;"  onclick="nextbutton(4)" data-target="#two, #dgdtwo" data-toggle="tab"><span class="glyphicon glyphicon-chevron-right" style="font-size:80%"></span></a></li>
            <li class="right2 hidden"><a href="#three" style="padding:0px"  onclick="nextbutton(5)" data-target="#three, #dgdthree" data-toggle="tab"><span class="glyphicon glyphicon-chevron-right" style="font-size:80%"></span></a></li>            
            <li class="right3 hidden"><a href="#four"style="padding:0px"  onclick="nextbutton(6)" data-target="#four, #dgdfour" data-toggle="tab"><span class="glyphicon glyphicon-chevron-right" style="font-size:80%"></span></a></li>
            <li class="right4 hidden"><a href="#six" style="padding:0px"  onclick="nextbutton(8)" data-target="#six, #dgdsix" data-toggle="tab"><span class="glyphicon glyphicon-chevron-right" style="font-size:80%"></span></a></li>            
            <li class="right5 hidden"><a href="#eight"style="padding:0px"  onclick="nextbutton(12)" data-target="#eight, #dgdeight" data-toggle="tab"><span class="glyphicon glyphicon-chevron-right" style="font-size:80%"></span></a></li>
            <li class="right6 hidden"><a href="#nine" style="padding:0px"  onclick="nextbutton(13)" data-target="#nine, #dgdnine" data-toggle="tab"><span class="glyphicon glyphicon-chevron-right" style="font-size:80%"></span></a></li>            
            <li class="right7 hidden"><a href="#ten"style="padding:0px"  onclick="nextbutton(14)" data-target="#ten, #dgdten" data-toggle="tab"><span class="glyphicon glyphicon-chevron-right" style="font-size:80%"></span></a></li>
            <li class="right8 hidden"><a href="#mid2"style="padding:0px"  onclick="nextbutton(23)" data-target="#mid2, #dgdmid2" data-toggle="tab"><span class="glyphicon glyphicon-chevron-right" style="font-size:80%"></span></a></li>
            <li class="right9 hidden"><a href="#final2"style="padding:0px"  onclick="nextbutton(24)" data-target="#final2, #dgdfinal2" data-toggle="tab"><span class="glyphicon glyphicon-chevron-right" style="font-size:80%"></span></a></li>
           
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

        <th class="rotate tdtr5"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal1"></span><asp:Textbox ID="testname1" runat="server" CssClass="nopad100 test1name" data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal2"></span><asp:Textbox ID="testname2" runat="server" CssClass="nopad100 test2name" data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal3"></span><asp:Textbox ID="testname3" runat="server" CssClass="nopad100 test3name" data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal4"></span><asp:Textbox ID="testname4" runat="server" CssClass="nopad100 test4name" data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal5"></span><asp:Textbox ID="testname5" runat="server" CssClass="nopad100 test5name" data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        
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
    
  </tr>
  
  
</table>
                                            
            </div>
        <div class="tab-pane " id="two" style="background: white;  ">
                 <table class="" style="width:100%; background-color:#337AB7;">
  <thead>
    <tr>
      
      <!-- Following headers are rotated -->
                <th class="" style="height:103px; width:1px;"><div><span></span></div></th>

        <th class="rotate tdtr5"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal6"></span><asp:Textbox ID="testname6" runat="server" CssClass="nopad100 test6name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal7"></span><asp:Textbox ID="testname7" runat="server" CssClass="nopad100 test7name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal8"></span><asp:Textbox ID="testname8" runat="server" CssClass="nopad100 test8name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal9"></span><asp:Textbox ID="testname9" runat="server" CssClass="nopad100 test9name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal10"></span><asp:Textbox ID="testname10" runat="server" CssClass="nopad100 test10name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        
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
    
  </tr>
  
  
</table>
            </div>
        <div class="tab-pane " id="three" style="background: white;  ">
                 <table class="" style="width:100%; background-color:#337AB7;">
  <thead>
    <tr>
      
      <!-- Following headers are rotated -->
                <th class="" style="height:103px; width:1px;"><div><span></span></div></th>

        <th class="rotate tdtr5"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal11"></span><asp:Textbox ID="testname11" runat="server" CssClass="nopad100 test11name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal12"></span><asp:Textbox ID="testname12" runat="server" CssClass="nopad100 test12name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal13"></span><asp:Textbox ID="testname13" runat="server" CssClass="nopad100 test13name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal14"></span><asp:Textbox ID="testname14" runat="server" CssClass="nopad100 test14name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal15"></span><asp:Textbox ID="testname15" runat="server" CssClass="nopad100 test15name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        
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
    
  </tr>
  
  
</table>
            </div>
        <div class="tab-pane" id="four" style="background: white;  ">
                 <table class="" style="width:100%; background-color:#337AB7;">
  <thead>
    <tr>
      
      <!-- Following headers are rotated -->
                <th class="" style="height:103px; width:1px;"><div><span></span></div></th>

        <th class="rotate tdtr5"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal16"></span><asp:Textbox ID="testname16" runat="server" CssClass="nopad100 test16name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal17"></span><asp:Textbox ID="testname17" runat="server" CssClass="nopad100 test17name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal18"></span><asp:Textbox ID="testname18" runat="server" CssClass="nopad100 test18name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal19"></span><asp:Textbox ID="testname19" runat="server" CssClass="nopad100 test19name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModal20"></span><asp:Textbox ID="testname20" runat="server" CssClass="nopad100 test20name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        
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
    
  </tr>
  
  
</table>
            </div>
        <div class="tab-pane" id="five" style="background: white;  ">
                 <table class="" style="width:100%; background-color:#337AB7;">
  <thead>
    <tr>
      
      <!-- Following headers are rotated -->
                <th class="" style="height:103px; width:1px;"><div><span></span></div></th>

        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalB1"></span><asp:Textbox ID="behavior1" runat="server" CssClass="nopad100 testb1name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalB2"></span><asp:Textbox ID="behavior2" runat="server" CssClass="nopad100 testb2name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalB3"></span><asp:Textbox ID="behavior3" runat="server" CssClass="nopad100 testb3name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalB4"></span><asp:Textbox ID="behavior4" runat="server" CssClass="nopad100 testb4name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalB5"></span><asp:Textbox ID="behavior5" runat="server" CssClass="nopad100 testb5name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        
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
    
  </tr>
  
  
</table>
            </div>
        <div class="tab-pane" id="six" style="background: white;  ">
                 <table class="" style="width:100%; background-color:#337AB7;">
  <thead>
    <tr>
      
      <!-- Following headers are rotated -->
                <th class="" style="height:103px; width:1px;"><div><span></span></div></th>

        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalB6"></span><asp:Textbox ID="behavior6" runat="server" CssClass="nopad100 testb6name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalB7"></span><asp:Textbox ID="behavior7" runat="server" CssClass="nopad100 testb7name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalB8"></span><asp:Textbox ID="behavior8" runat="server" CssClass="nopad100 testb8name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalB9"></span><asp:Textbox ID="behavior9" runat="server" CssClass="nopad100 testb9name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalB10"></span><asp:Textbox ID="behavior10" runat="server" CssClass="nopad100 testb10name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        
    </tr> 
  </thead>
  
</table>
        <table class="table3" style="width:100%">
  
                <tr class ="">
    <td class =" " style="padding:0px; width:0.001%; border:0px;"></td>
    <td class ="tdtrx headerCell2" style="padding:3px;"><asp:Textbox ID="maxb6" runat="server"  CssClass="nopad40 maxtestb1" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="maxb7" runat="server" CssClass="nopad40 maxtestb1" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="maxb8" runat="server" CssClass="nopad40 maxtestb1" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="maxb9" runat="server" CssClass="nopad40 maxtestb1" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>    
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="maxb10" runat="server" CssClass="nopad40 maxtestb1" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>    
    
  </tr>
  
  
</table>
            </div>
        <div class="tab-pane" id="seven" style="background: white;  ">
                 <table class="" style="width:100%; background-color:#337AB7;">
  <thead>
    <tr>
      
      <!-- Following headers are rotated -->
                <th class="" style="height:103px; width:1px;"><div><span></span></div></th>

        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalcw1"></span><asp:Textbox ID="cwname1" runat="server" CssClass="nopad100 testcw1name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalcw2"></span><asp:Textbox ID="cwname2" runat="server" CssClass="nopad100 testcw2name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalcw3"></span><asp:Textbox ID="cwname3" runat="server" CssClass="nopad100 testcw3name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalcw4"></span><asp:Textbox ID="cwname4" runat="server" CssClass="nopad100 testcw4name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalcw5"></span><asp:Textbox ID="cwname5" runat="server" CssClass="nopad100 testcw5name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        
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
    
  </tr>
  
  
</table>
            </div>
        <div class="tab-pane" id="eight" style="background: white;  ">
                 <table class="" style="width:100%; background-color:#337AB7;">
  <thead>
    <tr>
      
      <!-- Following headers are rotated -->
                <th class="" style="height:103px; width:1px;"><div><span></span></div></th>

        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalcw6"></span><asp:Textbox ID="cwname6" runat="server" CssClass="nopad100 testcw6name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalcw7"></span><asp:Textbox ID="cwname7" runat="server" CssClass="nopad100 testcw7name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalcw8"></span><asp:Textbox ID="cwname8" runat="server" CssClass="nopad100 testcw8name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalcw9"></span><asp:Textbox ID="cwname9" runat="server" CssClass="nopad100 testcw9name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalcw10"></span><asp:Textbox ID="cwname10" runat="server" CssClass="nopad100 testcw10name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        
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
    
  </tr>
  
  
</table>
            </div>
        <div class="tab-pane" id="nine" style="background: white;  ">
                 <table class="" style="width:100%; background-color:#337AB7;">
  <thead>
    <tr>
      
      <!-- Following headers are rotated -->
                <th class="" style="height:103px; width:1px;"><div><span></span></div></th>

        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalcw11"></span><asp:Textbox ID="cwname11" runat="server" CssClass="nopad100 testcw11name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalcw12"></span><asp:Textbox ID="cwname12" runat="server" CssClass="nopad100 testcw12name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalcw13"></span><asp:Textbox ID="cwname13" runat="server" CssClass="nopad100 testcw13name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalcw14"></span><asp:Textbox ID="cwname14" runat="server" CssClass="nopad100 testcw14name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalcw15"></span><asp:Textbox ID="cwname15" runat="server" CssClass="nopad100 testcw15name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        
    </tr> 
  </thead>
  
</table>
        <table class="table3" style="width:100%">
  
                <tr class ="">
    <td class =" " style="padding:0px; width:0.001%; border:0px;"></td>
    <td class ="tdtrx headerCell2" style="padding:3px;"><asp:Textbox ID="maxCW11" runat="server"  CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="maxCW12" runat="server" CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="maxCW13" runat="server" CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="maxCW14" runat="server" CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>    
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="maxCW15" runat="server" CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>    
    
  </tr>
  
  
</table>
            </div>
        <div class="tab-pane" id="ten" style="background: white;  ">
                 <table class="" style="width:100%; background-color:#337AB7;">
  <thead>
    <tr>
      
      <!-- Following headers are rotated -->
                <th class="" style="height:103px; width:1px;"><div><span></span></div></th>

        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalcw16"></span><asp:Textbox ID="cwname16" runat="server" CssClass="nopad100 testcw16name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalcw17"></span><asp:Textbox ID="cwname17" runat="server" CssClass="nopad100 testcw17name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalcw18"></span><asp:Textbox ID="cwname18" runat="server" CssClass="nopad100 testcw18name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalcw19"></span><asp:Textbox ID="cwname19" runat="server" CssClass="nopad100 testcw19name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalcw20"></span><asp:Textbox ID="cwname20" runat="server" CssClass="nopad100 testcw20name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        
    </tr> 
  </thead>
  
</table>
        <table class="table3" style="width:100%">
  
                <tr class ="">
    <td class =" " style="padding:0px; width:0.001%; border:0px;"></td>
    <td class ="tdtrx headerCell2" style="padding:3px;"><asp:Textbox ID="maxCW16" runat="server"  CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="maxCW17" runat="server" CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="maxCW18" runat="server" CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="maxCW19" runat="server" CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>    
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="maxCW20" runat="server" CssClass="nopad40 maxtestcw" Width="80%" onkeyup="CompareDates()"> </asp:Textbox></td>    
    
  </tr>
  
  
</table>
            </div>
        <div class="tab-pane" id="mid1" style="background: white;  ">
                 <table class="" style="width:100%; background-color:#337AB7;">
  <thead>
    <tr>
      
      <!-- Following headers are rotated -->
                <th class="" style="height:103px; width:1px;"><div><span></span></div></th>

        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalmid1"></span><asp:Textbox ID="midname1" runat="server" CssClass="nopad100 testmid1name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalmid2"></span><asp:Textbox ID="midname2" runat="server" CssClass="nopad100 testmid2name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalmid3"></span><asp:Textbox ID="midname3" runat="server" CssClass="nopad100 testmid3name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalmid4"></span><asp:Textbox ID="midname4" runat="server" CssClass="nopad100 testmid4name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalmid5"></span><asp:Textbox ID="midname5" runat="server" CssClass="nopad100 testmid5name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        
    </tr> 
  </thead>
  
</table>
        <table class="table3" style="width:100%">
  
                <tr class ="">
    <td class =" " style="padding:0px; width:0.001%; border:0px;"></td>
    <td class ="tdtrx headerCell2" style="padding:3px;"><asp:Textbox ID="midmax1" runat="server"  CssClass="nopad40 maxmidcw" Width="80%" onkeyup="calmaxmid();CompareDates()" > </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="midmax2" runat="server" CssClass="nopad40 maxmidcw" Width="80%" onkeyup="calmaxmid();CompareDates()" > </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="midmax3" runat="server" CssClass="nopad40 maxmidcw" Width="80%" onkeyup="calmaxmid();CompareDates()" > </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="midmax4" runat="server" CssClass="nopad40 maxmidcw" Width="80%" onkeyup="calmaxmid();CompareDates()" > </asp:Textbox></td>    
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="midmax5" runat="server" CssClass="nopad40 maxmidcw" Width="80%" onkeyup="calmaxmid();CompareDates()" > </asp:Textbox></td>    
    
  </tr>
  
  
</table>
            </div>
        <div class="tab-pane" id="mid2" style="background: white;  ">
                 <table class="" style="width:100%; background-color:#337AB7;">
  <thead>
    <tr>
      
      <!-- Following headers are rotated -->
                <th class="" style="height:103px; width:1px;"><div><span></span></div></th>

        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalmid6"></span><asp:Textbox ID="midname6" runat="server" CssClass="nopad100 testmid6name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalmid7"></span><asp:Textbox ID="midname7" runat="server" CssClass="nopad100 testmid7name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalmid8"></span><asp:Textbox ID="midname8" runat="server" CssClass="nopad100 testmid8name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalmid9"></span><asp:Textbox ID="midname9" runat="server" CssClass="nopad100 testmid9name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalmid10"></span><asp:Textbox ID="midname10" runat="server" CssClass="nopad100 testmid10name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        
    </tr> 
  </thead>
  
</table>
        <table class="table3" style="width:100%">
  
                <tr class ="">
    <td class =" " style="padding:0px; width:0.001%; border:0px;"></td>
    <td class ="tdtrx headerCell2" style="padding:3px;"><asp:Textbox ID="midmax6" runat="server"  CssClass="nopad40 maxmidcw" Width="80%" onkeyup="calmaxmid();CompareDates()" > </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="midmax7" runat="server" CssClass="nopad40 maxmidcw" Width="80%" onkeyup="calmaxmid();CompareDates()" > </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="midmax8" runat="server" CssClass="nopad40 maxmidcw" Width="80%" onkeyup="calmaxmid();CompareDates()" > </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="midmax9" runat="server" CssClass="nopad40 maxmidcw" Width="80%" onkeyup="calmaxmid();CompareDates()" > </asp:Textbox></td>    
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="midmax10" runat="server" CssClass="nopad40 maxmidcw" Width="80%" onkeyup="calmaxmid();CompareDates()" > </asp:Textbox></td>    
    
  </tr>
  
  
</table>
            </div>
        <div class="tab-pane" id="final1" style="background: white;  ">
                 <table class="" style="width:100%; background-color:#337AB7;">
  <thead>
    <tr>
      
      <!-- Following headers are rotated -->
                <th class="" style="height:103px; width:1px;"><div><span></span></div></th>

        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalfinal1"></span><asp:Textbox ID="finalname1" runat="server" CssClass="nopad100 testfinal1name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalfinal2"></span><asp:Textbox ID="finalname2" runat="server" CssClass="nopad100 testfinal2name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalfinal3"></span><asp:Textbox ID="finalname3" runat="server" CssClass="nopad100 testfinal3name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalfinal4"></span><asp:Textbox ID="finalname4" runat="server" CssClass="nopad100 testfinal4name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalfinal5"></span><asp:Textbox ID="finalname5" runat="server" CssClass="nopad100 testfinal5name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        
    </tr> 
  </thead>
  
</table>
        <table class="table3" style="width:100%">
  
                <tr class ="">
    <td class =" " style="padding:0px; width:0.001%; border:0px;"></td>
    <td class ="tdtrx headerCell2" style="padding:3px;"><asp:Textbox ID="finalmax1" runat="server"  CssClass="nopad40 maxfinalcw" Width="80%" onkeyup="calmaxfinal();CompareDates()" > </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="finalmax2" runat="server" CssClass="nopad40 maxfinalcw" Width="80%" onkeyup="calmaxfinal();CompareDates()" > </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="finalmax3" runat="server" CssClass="nopad40 maxfinalcw" Width="80%" onkeyup="calmaxfinal();CompareDates()" > </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="finalmax4" runat="server" CssClass="nopad40 maxfinalcw" Width="80%" onkeyup="calmaxfinal();CompareDates()" > </asp:Textbox></td>    
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="finalmax5" runat="server" CssClass="nopad40 maxfinalcw" Width="80%" onkeyup="calmaxfinal();CompareDates()" > </asp:Textbox></td>    
    
  </tr>
  
  
</table>
            </div>
        <div class="tab-pane" id="final2" style="background: white;  ">
                 <table class="" style="width:100%; background-color:#337AB7;">
  <thead>
    <tr>
      
      <!-- Following headers are rotated -->
                <th class="" style="height:103px; width:1px;"><div><span></span></div></th>

        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalfinal6"></span><asp:Textbox ID="finalname6" runat="server" CssClass="nopad100 testfinal6name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalfinal7"></span><asp:Textbox ID="finalname7" runat="server" CssClass="nopad100 testfinal7name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalfinal8"></span><asp:Textbox ID="finalname8" runat="server" CssClass="nopad100 testfinal8name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalfinal9"></span><asp:Textbox ID="finalname9" runat="server" CssClass="nopad100 testfinal9name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        <th class="rotate tdtr5" style="padding-left:6px; width:20%;"><div><span class="fa fa-edit gly-rotate-90" style="cursor:pointer; font-size:70%; color:white;"  data-toggle="modal" data-target="#myModalfinal10"></span><asp:Textbox ID="finalname10" runat="server" CssClass="nopad100 testfinal10name " data-pt-gravity="top -45 50" Enabled="false"> </asp:Textbox></div></th>        
        
    </tr> 
  </thead>
  
</table>
        <table class="table3" style="width:100%">
  
                <tr class ="">
    <td class =" " style="padding:0px; width:0.001%; border:0px;"></td>
    <td class ="tdtrx headerCell2" style="padding:3px;"><asp:Textbox ID="finalmax6" runat="server"  CssClass="nopad40 maxfinalcw" Width="80%" onkeyup="calmaxfinal();CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="finalmax7" runat="server" CssClass="nopad40 maxfinalcw" Width="80%" onkeyup="calmaxfinal();CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr" style="padding:1px"><asp:Textbox ID="finalmax8" runat="server" CssClass="nopad40 maxfinalcw" Width="80%" onkeyup="calmaxfinal();CompareDates()" > </asp:Textbox></td>
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="finalmax9" runat="server" CssClass="nopad40 maxfinalcw" Width="80%" onkeyup="calmaxfinal();CompareDates()"> </asp:Textbox></td>    
    <td class ="tdtr" style="padding:2px"><asp:Textbox ID="finalmax10" runat="server" CssClass="nopad40 maxfinalcw" Width="80%" onkeyup="calmaxfinal();CompareDates()" > </asp:Textbox></td>    
    
  </tr>
  
  
</table>
            </div>
    </div>
</div>
                 
                    
                 </div>
            <div class="col-xs-6" style="padding:0px;">
                    <table class="table3 table_legenda" style="width:100%">
  <tr class="tdtr">
    <th colspan="2" class ="tdtr headerCell" style="width:100px !important; padding:0px; table-layout:fixed;" ><asp:Label ID="tablescore" cssclass="bold" runat="server" > </asp:Label></th>
      <th rowspan="2" class="tdtr headerCell" style="width:50px !important; padding:0px; font-size:80%; table-layout:fixed;" ><asp:Label ID="tablescoresum" cssclass="bold" runat="server" > </asp:Label></th>
    <th rowspan="3" class="tdtr headerCell" style="width:50px !important; padding:0px; table-layout:fixed;" ><asp:Label ID="tablescore100" cssclass="bold" runat="server" > </asp:Label><br/><i class="fa fa-question-circle" style="font-size: 18px;" data-original-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206032") %> <br> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206033") %>" data-placement="top" data-html="true" data-toggle="tooltip"></i></th>
    <th rowspan="3" class="tdtr headerCell editform2" style="width:50px !important; padding:0px; table-layout:fixed;" ><asp:Label ID="tablegrade" cssclass="bold" runat="server" > </asp:Label><br/><i class="fa fa-question-circle" style="font-size: 18px;" data-original-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206042") %> <br> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206043") %> <br> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206044") %> <br>  <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206045") %> <br>  <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206046") %> <br>  <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206047") %> <br>  <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206048") %> <br>  <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132290") %> <br>  <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132291") %> <br>" data-html="true" data-placement="top" data-toggle="tooltip"></i></th>
    <th rowspan="3" class="tdtr headerCell editform1" style="width:50px !important; padding:0px; font-size:80%; table-layout:fixed;" ><asp:Label ID="tablebehavior" cssclass="bold" runat="server" > </asp:Label> <br/>
        <span class="glyphicon glyphicon-cog greencog hidden" style="cursor:pointer; font-size:70%; color:lightgreen;" onclick="auto(1)" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132275") %>"></span> 
        <span class="glyphicon glyphicon-cog redcog hidden" style="cursor:pointer; font-size:70%; color:coral;" onclick="auto(2)" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132275") %>"></span>
        <span class="glyphicon glyphicon-cog yellowcog hidden" style="cursor:pointer; font-size:70%; color:yellow;" onclick="auto(7)" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132275") %>"></span>         
        <span class="glyphicon glyphicon-save-file autosave" style="cursor:pointer; font-size:70%; color:white;" onclick="auto(3)" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206040") %>" data-toggle="modal" data-target="#myModalcopy1"></span>
        <span class="glyphicon glyphicon-trash" style="cursor:pointer; font-size:70%; color:white;" onclick="cleartext(1)" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206041") %>" ></span>
    <br/><i class="fa fa-question-circle" style="font-size: 18px;" data-original-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206049") %>" data-placement="top" data-toggle="tooltip"></i></th>
       
    <th rowspan="3" class="tdtr headerCell editform1" style="width:50px !important; padding:0px; font-size:80%; table-layout:fixed;" ><asp:Label ID="tablereading" cssclass="bold" runat="server" > </asp:Label><br/>
        <span class="glyphicon glyphicon-cog greencog hidden" style="cursor:pointer; font-size:70%; color:lightgreen;" onclick="auto(4)" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132275") %>"></span> 
        <span class="glyphicon glyphicon-cog redcog hidden" style="cursor:pointer; font-size:70%; color:coral;" onclick="auto(5)" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132275") %>"></span> 
        <span class="glyphicon glyphicon-save-file autosave" style="cursor:pointer; font-size:70%; color:white;" onclick="auto(6)" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206040") %>" data-toggle="modal" data-target="#myModalcopy2"></span>
        <span class="glyphicon glyphicon-trash" style="cursor:pointer; font-size:70%; color:white;" onclick="cleartext(2)" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206041") %>" ></span>
    <br/><i class="fa fa-question-circle" style="font-size: 18px;" data-original-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206035") %> <br> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206036") %>  <br> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206037") %>  <br> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206038") %>  <br>  <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206039") %>  <br>" data-html="true" data-placement="top" data-toggle="tooltip"></i></th>
       <th rowspan="3" class="tdtr headerCell editform3" style="width:50px !important; padding:0px; font-size:80%; table-layout:fixed;" ><asp:Label ID="tabSamattana" cssclass="bold" runat="server" > </asp:Label><br/>
        <span class="glyphicon glyphicon-cog greencog hidden" style="cursor:pointer; font-size:70%; color:lightgreen;" onclick="auto(4)" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132275") %>"></span> 
        <span class="glyphicon glyphicon-cog redcog hidden" style="cursor:pointer; font-size:70%; color:coral;" onclick="auto(5)" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132275") %>"></span> 
        <span class="glyphicon glyphicon-save-file autosave" style="cursor:pointer; font-size:70%; color:white;" onclick="auto(6)" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206040") %>" data-toggle="modal" data-target="#myModalcopy3"></span>
        <span class="glyphicon glyphicon-trash" style="cursor:pointer; font-size:70%; color:white;" onclick="cleartext(3)" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206041") %>" ></span>
    </th>
    <th rowspan="3" class="tdtr headerCell" style="width:50px !important; padding:0px; font-size:80%; table-layout:fixed;" ><asp:Label ID="tableother" cssclass="bold" runat="server" > </asp:Label></th>
  </tr>
  
                
   <tr class ="tdtr">
    <th  class ="tdtr headerCell3" style="width:50px !important; padding:0px;"><asp:Label ID="tablemid" cssclass="bold" runat="server" > </asp:Label></th>
    <th  class ="tdtr headerCell3" style="width:50px !important; padding:0px;"><asp:Label ID="tablefinal" cssclass="bold" runat="server" > </asp:Label></th>  
  </tr>

    <tr class ="tdtr">
    <td class ="tdtr  headerCell2" style="padding:0px"><asp:Textbox ID="maxSmid" runat="server"  CssClass="nopad40 maxmidscore wid39" Width="79%" onkeyup="CompareDates()"> </asp:Textbox></td>
    <td class ="tdtr " style="padding:0px"><asp:Textbox ID="maxSlate" runat="server"  CssClass="nopad40 maxlatescore wid39" Width="79%" onkeyup="CompareDates()"> </asp:Textbox></td>   
        <td class ="tdtr " style="padding:0px"><asp:Textbox ID="Textbox20" runat="server"  CssClass="nopad40 maxscoreall disable wid39" Width="79%" > </asp:Textbox></td>   
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
                    <asp:TemplateField ItemStyle-CssClass="centertext cen borderline2 w40 stdout1" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
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
                     
                    <asp:TemplateField ItemStyle-CssClass="name cen borderline stdout2" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
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
                    <asp:TemplateField ItemStyle-CssClass="centertext cen hid" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="20%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox19"   runat="server" Width="80%" Height="42.5px"  CssClass="stdidlist" Text='<%# Eval("studentid") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen hid" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="20%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox21"   runat="server" Width="80%" Height="42.5px"  CssClass="stdstatus" Text='<%# Eval("studentstatus") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                             Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />
               
            </asp:GridView>
                </div>
                
                <div class="col-xs-3" style="padding:0px; background-color:white;" >
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
                    
                    
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disableg1 stdout3" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="20%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade1" tabindex="1" onfocus='<%# "focusbox(0," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBox lockg1" Text='<%# Eval("txtg1") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disableg2 stdout4" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="20%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade2" tabindex="2" onfocus='<%# "focusbox(1," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBox lockg2" Text='<%# Eval("txtg2") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disableg3 stdout5" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="20%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade3" tabindex="3" onfocus='<%# "focusbox(2," +Eval("number") + " );" %>'  onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBox lockg3" Text='<%# Eval("txtg3") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disableg4 stdout6" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="20%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade4" tabindex="4" onfocus='<%# "focusbox(3," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBox lockg4" Text='<%# Eval("txtg4") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disableg5 stdout7" HeaderStyle-CssClass="centertext"  HeaderText="5"> 
                        <HeaderStyle Width="20%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade5" tabindex="5" onfocus='<%# "focusbox(4," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBox lockg5" Text='<%# Eval("txtg5") %>'></asp:TextBox>
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
                    
                    
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disableg6 stdout8" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade6" tabindex="6" onfocus='<%# "focusbox(5," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg2 lockg6" Text='<%# Eval("txtg6") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disableg7 stdout9" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade7" tabindex="7" onfocus='<%# "focusbox(6," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg2 lockg7" Text='<%# Eval("txtg7") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disableg8 stdout10" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade8" tabindex="8" onfocus='<%# "focusbox(7," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg2 lockg8" Text='<%# Eval("txtg8") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disableg9 stdout11" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade9" tabindex="9" onfocus='<%# "focusbox(8," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg2 lockg9" Text='<%# Eval("txtg9") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disableg10 stdout12" HeaderStyle-CssClass="centertext"  HeaderText="5"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade10" tabindex="10" onfocus='<%# "focusbox(9," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg2 lockg10" Text='<%# Eval("txtg10") %>'></asp:TextBox>
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
                    
                    
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disableg11 stdout13" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade11" tabindex="11" onfocus='<%# "focusbox(10," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg3 lockg11" Text='<%# Eval("txtg11") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disableg12 stdout14" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade12" tabindex="12" onfocus='<%# "focusbox(11," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg3 lockg12" Text='<%# Eval("txtg12") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disableg13 stdout15" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade13" tabindex="13" onfocus='<%# "focusbox(12," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg3 lockg13" Text='<%# Eval("txtg13") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disableg14 stdout16" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade14" tabindex="14" onfocus='<%# "focusbox(13," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg3 lockg14" Text='<%# Eval("txtg14") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disableg15 stdout17" HeaderStyle-CssClass="centertext"  HeaderText="5"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade15" tabindex="15" onfocus='<%# "focusbox(14," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg3 lockg15" Text='<%# Eval("txtg15") %>'></asp:TextBox>
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
                    
                    
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disableg16 stdout18" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade16" tabindex="16" onfocus='<%# "focusbox(15," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg4 lockg16" Text='<%# Eval("txtg16") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disableg17 stdout19" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade17" tabindex="17" onfocus='<%# "focusbox(16," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg4 lockg17" Text='<%# Eval("txtg17") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disableg18 stdout20" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade18" tabindex="18" onfocus='<%# "focusbox(17," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg4 lockg18" Text='<%# Eval("txtg18") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disableg19 stdout21" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade19" tabindex="19" onfocus='<%# "focusbox(18," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg4 lockg19" Text='<%# Eval("txtg19") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disableg20 stdout22" HeaderStyle-CssClass="centertext"  HeaderText="5"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtGrade20" tabindex="20" onfocus='<%# "focusbox(19," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBoxg4 lockg20" Text='<%# Eval("txtg20") %>'></asp:TextBox>
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
                    
                    
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablebh1 stdout23" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="behave1" onfocus='<%# "focusbox(20," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt1 behavepage1" Text='<%# Eval("txtb1") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablebh2 stdout24" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="behave2" onfocus='<%# "focusbox(21," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt2 behavepage1" Text='<%# Eval("txtb2") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablebh3 stdout25" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="behave3" onfocus='<%# "focusbox(22," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt3 behavepage1" Text='<%# Eval("txtb3") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablebh4 stdout26" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="behave4" onfocus='<%# "focusbox(23," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt4 behavepage1" Text='<%# Eval("txtb4") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablebh5 stdout27" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="behave5" onfocus='<%# "focusbox(24," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt5 behavepage1" Text='<%# Eval("txtb5") %>'></asp:TextBox>
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
                    
                    
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablebh6 stdout28" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="behave6" onfocus='<%# "focusbox(25," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt6 behavepage2" Text='<%# Eval("txtb6") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablebh7 stdout29" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="behave7" onfocus='<%# "focusbox(26," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt7 behavepage2" Text='<%# Eval("txtb7") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablebh8 stdout30" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="behave8" onfocus='<%# "focusbox(27," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt8 behavepage2" Text='<%# Eval("txtb8") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablebh9 stdout31" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="behave9" onfocus='<%# "focusbox(28," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt9 behavepage2" Text='<%# Eval("txtb9") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField ItemStyle-CssClass="centertext cen disablebh10 stdout32" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="behave10" onfocus='<%# "focusbox(29," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup="autobehave()" CssClass="behavetxt10 behavepage2" Text='<%# Eval("txtb10") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField ItemStyle-CssClass="hid2" HeaderStyle-CssClass="centertext"  HeaderText="20"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox14" CssClass="behavesid" runat="server" Width="40px" Text='<%# Eval("sID") %>'></asp:TextBox>
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
                    
                    
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablecw1 stdout33" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="chewat1" onfocus='<%# "focusbox(30," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut1 lockcw1" Text='<%# Eval("txtchewut1") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablecw2 stdout34" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="chewat2" onfocus='<%# "focusbox(31," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut1 lockcw2" Text='<%# Eval("txtchewut2") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablecw3 stdout35" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="chewat3" onfocus='<%# "focusbox(32," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut1 lockcw3" Text='<%# Eval("txtchewut3") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablecw4 stdout36" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="chewat4" onfocus='<%# "focusbox(33," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut1 lockcw4" Text='<%# Eval("txtchewut4") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablecw5 stdout37" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="chewat5" onfocus='<%# "focusbox(34," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut1 lockcw5" Text='<%# Eval("txtchewut5") %>'></asp:TextBox>
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
                        <div class="tab-pane" id="dgdeight" style="background: white;  ">
                 <asp:GridView ID="dgdp8" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="0" AllowCustomPaging="False"
                          GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                         ShowHeader="false"
                          Font-Strikeout="False" Font-Underline="False" PageSize="99" CssClass="cen"> 
                             
                <Columns>
                    
                    
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablecw6 stdout38" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="chewat6" onfocus='<%# "focusbox(35," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut2 lockcw6" Text='<%# Eval("txtchewut6") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablecw7 stdout39" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="chewat7" onfocus='<%# "focusbox(36," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut2 lockcw7" Text='<%# Eval("txtchewut7")%>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablecw8 stdout40" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="chewat8" onfocus='<%# "focusbox(37," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut2 lockcw8" Text='<%# Eval("txtchewut8") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablecw9 stdout41" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="chewat9" onfocus='<%# "focusbox(38," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut2 lockcw9" Text='<%# Eval("txtchewut9") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablecw10 stdout42" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="chewat10" onfocus='<%# "focusbox(39," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut2 lockcw10" Text='<%# Eval("txtchewut10") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                   
                    <asp:TemplateField ItemStyle-CssClass="hid2" HeaderStyle-CssClass="centertext"  HeaderText="20"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox17" runat="server" Width="40px" Text='<%# Eval("sID") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     
                     
                </Columns>

                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                             Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />
               
            </asp:GridView>
            </div>
                        <div class="tab-pane" id="dgdnine" style="background: white;  ">
                 <asp:GridView ID="dgdp9" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="0" AllowCustomPaging="False"
                          GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                         ShowHeader="false"
                          Font-Strikeout="False" Font-Underline="False" PageSize="99" CssClass="cen"> 
                             
                <Columns>
                    
                    
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablecw11 stdout43" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="chewat11" onfocus='<%# "focusbox(40," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut3 lockcw11" Text='<%# Eval("txtchewut11") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablecw12 stdout44" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="chewat12" onfocus='<%# "focusbox(41," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut3 lockcw12" Text='<%# Eval("txtchewut12") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablecw13 stdout45" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="chewat13" onfocus='<%# "focusbox(42," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut3 lockcw13" Text='<%# Eval("txtchewut13") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablecw14 stdout46" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="chewat14" onfocus='<%# "focusbox(43," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut3 lockcw14" Text='<%# Eval("txtchewut14") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablecw15 stdout47" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="chewat15" onfocus='<%# "focusbox(44," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut3 lockcw15" Text='<%# Eval("txtchewut15") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                   
                    <asp:TemplateField ItemStyle-CssClass="hid2" HeaderStyle-CssClass="centertext"  HeaderText="20"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox25" runat="server" Width="40px" Text='<%# Eval("sID") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     
                     
                </Columns>

                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                             Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />
               
            </asp:GridView>
            </div>
                        <div class="tab-pane" id="dgdten" style="background: white;  ">
                 <asp:GridView ID="dgdp10" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="0" AllowCustomPaging="False"
                          GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                         ShowHeader="false"
                          Font-Strikeout="False" Font-Underline="False" PageSize="99" CssClass="cen"> 
                             
                <Columns>
                    
                    
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablecw16 stdout48" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="chewat16" onfocus='<%# "focusbox(45," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut4 lockcw16" Text='<%# Eval("txtchewut16") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablecw17 stdout49" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="chewat17" onfocus='<%# "focusbox(46," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut4 lockcw17" Text='<%# Eval("txtchewut17") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablecw18 stdout50" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="chewat18" onfocus='<%# "focusbox(47," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut4 lockcw18" Text='<%# Eval("txtchewut18") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablecw19 stdout51" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="chewat19" onfocus='<%# "focusbox(48," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut4 lockcw19" Text='<%# Eval("txtchewut19") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablecw20 stdout52" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="chewat20" onfocus='<%# "focusbox(49," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="chewut4 lockcw20" Text='<%# Eval("txtchewut20") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                   
                    <asp:TemplateField ItemStyle-CssClass="hid2" HeaderStyle-CssClass="centertext"  HeaderText="20"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox31" runat="server" Width="40px" Text='<%# Eval("sID") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     
                     
                </Columns>

                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                             Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />
               
            </asp:GridView>
            </div>
                        <div class="tab-pane" id="dgdmid1" style="background: white;  ">
                 <asp:GridView ID="dgdmidterm1" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="0" AllowCustomPaging="False"
                          GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                         ShowHeader="false"
                          Font-Strikeout="False" Font-Underline="False" PageSize="99" CssClass="cen"> 
                             
                <Columns>
                    
                    
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablemid1 stdout53" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="midscore1" onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "calmidscore(" +Eval("number") + " );" %>' CssClass="midscore1 lockm1" Text='<%# Eval("txtmid1") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablemid2 stdout54" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="midscore2" onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "calmidscore(" +Eval("number") + " );" %>' CssClass="midscore2 lockm2" Text='<%# Eval("txtmid2") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablemid3 stdout55" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="midscore3" onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "calmidscore(" +Eval("number") + " );" %>' CssClass="midscore3 lockm3" Text='<%# Eval("txtmid3") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablemid4 stdout56" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="midscore4" onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "calmidscore(" +Eval("number") + " );" %>' CssClass="midscore4 lockm4" Text='<%# Eval("txtmid4") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablemid5 stdout57" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="midscore5" onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "calmidscore(" +Eval("number") + " );" %>' CssClass="midscore5 lockm5" Text='<%# Eval("txtmid5") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                   
                    <asp:TemplateField ItemStyle-CssClass="hid2" HeaderStyle-CssClass="centertext"  HeaderText="20"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox24" runat="server" Width="40px" Text='<%# Eval("sID") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     
                     
                </Columns>

                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                             Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />
               
            </asp:GridView>
            </div>
                        <div class="tab-pane" id="dgdmid2" style="background: white;  ">
                 <asp:GridView ID="dgdmidterm2" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="0" AllowCustomPaging="False"
                          GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                         ShowHeader="false"
                          Font-Strikeout="False" Font-Underline="False" PageSize="99" CssClass="cen"> 
                             
                <Columns>
                    
                    
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablemid6 stdout58" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="midscore6" onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "calmidscore(" +Eval("number") + " );" %>' CssClass="midscore6 lockm6" Text='<%# Eval("txtmid6") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablemid7 stdout59" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="midscore7" onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "calmidscore(" +Eval("number") + " );" %>' CssClass="midscore7 lockm7" Text='<%# Eval("txtmid7") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablemid8 stdout60" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="midscore8" onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "calmidscore(" +Eval("number") + " );" %>' CssClass="midscore8 lockm8" Text='<%# Eval("txtmid8") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablemid9 stdout61" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="midscore9" onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "calmidscore(" +Eval("number") + " );" %>' CssClass="midscore9 lockm9" Text='<%# Eval("txtmid9") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablemid10 stdout62" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="midscore10" onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "calmidscore(" +Eval("number") + " );" %>' CssClass="midscore10 lockm10" Text='<%# Eval("txtmid10") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                   
                    <asp:TemplateField ItemStyle-CssClass="hid2" HeaderStyle-CssClass="centertext"  HeaderText="20"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox30" runat="server" Width="40px" Text='<%# Eval("sID") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     
                     
                </Columns>

                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                             Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />
               
            </asp:GridView>
            </div>
                        <div class="tab-pane" id="dgdfinal1" style="background: white;  ">
                 <asp:GridView ID="dgdfinalterm1" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="0" AllowCustomPaging="False"
                          GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                         ShowHeader="false"
                          Font-Strikeout="False" Font-Underline="False" PageSize="99" CssClass="cen"> 
                             
                <Columns>
                    
                    
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablefinal1 stdout63" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="finalscore1" onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "calfinalscore(" +Eval("number") + " );" %>' CssClass="finalscore1 lockf1" Text='<%# Eval("txtfinal1") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablefinal2 stdout64" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="finalscore2" onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "calfinalscore(" +Eval("number") + " );" %>' CssClass="finalscore2 lockf2" Text='<%# Eval("txtfinal2") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablefinal3 stdout65" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="finalscore3" onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "calfinalscore(" +Eval("number") + " );" %>' CssClass="finalscore3 lockf3" Text='<%# Eval("txtfinal3") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablefinal4 stdout66" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="finalscore4" onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "calfinalscore(" +Eval("number") + " );" %>' CssClass="finalscore4 lockf4" Text='<%# Eval("txtfinal4") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablefinal5 stdout67" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="finalscore5" onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "calfinalscore(" +Eval("number") + " );" %>' CssClass="finalscore5 lockf5" Text='<%# Eval("txtfinal5") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                   
                    <asp:TemplateField ItemStyle-CssClass="hid2" HeaderStyle-CssClass="centertext"  HeaderText="20"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox37" runat="server" Width="40px" Text='<%# Eval("sID") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     
                     
                </Columns>

                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                             Font-Underline="False" CssClass="headerCell" BackColor="#337AB7" />
               
            </asp:GridView>
            </div>
                        <div class="tab-pane" id="dgdfinal2" style="background: white;  ">
                 <asp:GridView ID="dgdfinalterm2" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="0" AllowCustomPaging="False"
                          GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                         ShowHeader="false"
                          Font-Strikeout="False" Font-Underline="False" PageSize="99" CssClass="cen"> 
                             
                <Columns>
                    
                    
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablefinal6 stdout68" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="finalscore6" onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "calfinalscore(" +Eval("number") + " );" %>' CssClass="finalscore6 lockf6" Text='<%# Eval("txtfinal6") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablefinal7 stdout69" HeaderStyle-CssClass="centertext"  HeaderText="1"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="finalscore7" onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "calfinalscore(" +Eval("number") + " );" %>' CssClass="finalscore7 lockf7" Text='<%# Eval("txtfinal7") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablefinal8 stdout70" HeaderStyle-CssClass="centertext"  HeaderText="2"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="finalscore8" onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "calfinalscore(" +Eval("number") + " );" %>' CssClass="finalscore8 lockf8" Text='<%# Eval("txtfinal8") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablefinal9 stdout71" HeaderStyle-CssClass="centertext"  HeaderText="3"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="finalscore9" onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "calfinalscore(" +Eval("number") + " );" %>' CssClass="finalscore9 lockf9" Text='<%# Eval("txtfinal9") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen disablefinal10 stdout72" HeaderStyle-CssClass="centertext"  HeaderText="4"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="finalscore10" onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" onkeyup='<%# "calfinalscore(" +Eval("number") + " );" %>' CssClass="finalscore10 lockf10" Text='<%# Eval("txtfinal10") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                   
                    <asp:TemplateField ItemStyle-CssClass="hid2" HeaderStyle-CssClass="centertext"  HeaderText="20"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="TextBox43" runat="server" Width="40px" Text='<%# Eval("sID") %>'></asp:TextBox>
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
                <div class="col-xs-6" style="padding:0px;">
                    
                    <asp:GridView ID="dgd3" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" AllowCustomPaging="False"
                          GridLines="None" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                         ShowHeader="false"
                          Font-Strikeout="False" Font-Underline="False" PageSize="99" CssClass="cen"> 
                             
                <Columns>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen2 cen head1 disablemid stdout73" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106099") %>"> 
                        <HeaderStyle Width="13%"></HeaderStyle>                                           
                        <ItemTemplate>
                            <asp:TextBox  ID="txtMidScore" onfocus='<%# "focusbox(50," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" tabindex="21"  onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBox2 summidbox lockmidterm wid39" Text='<%# Eval("txtgmid") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen2 cen head2 disablefinal stdout74" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106100") %>">  
                        <HeaderStyle Width="13%"></HeaderStyle>                                             
                        <ItemTemplate>
                            <asp:TextBox  ID="txtLateScore" onfocus='<%# "focusbox(51," +Eval("number") + " );" %>' onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" tabindex="22" onkeyup='<%# "CompareDates(" +Eval("number") + " );" %>' CssClass="AutoCompleteTextBox2 sumfinalbox rmargin2 wid39 lockfinalterm" Text='<%# Eval("txtglate") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen4 cen head3 stdout75" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132235") %>">  
                        <HeaderStyle Width="13%"></HeaderStyle>                                             
                        <ItemTemplate>                                      
                                            <asp:TextBox  ID="txtScoreSum" runat="server" Width="80%" Height="42.5px" CssClass="wid1 scoresumall disable" Text="" ></asp:TextBox>                                                                                                                                                             
                                </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen4 cen head4 stdout76" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132235") %>">  
                        <HeaderStyle Width="10%"></HeaderStyle>                                             
                        <ItemTemplate>                                      
                                            <asp:TextBox  ID="txtTotalScore" runat="server" Width="80%" Height="42.5px" CssClass="wid2 gradetxt disable" Text="" ></asp:TextBox>                                                                                                                                                             
                                </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen4 editgrade cen head5 stdout77" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206414") %>">  
                        <HeaderStyle Width="10%"></HeaderStyle>                                             
                        <ItemTemplate>
                                        
                                            <asp:TextBox  ID="txtTotalGrade" runat="server" Width="80%" Height="42.5px" CssClass="wid3 gradetxt disable gradetxt2" Text="" ></asp:TextBox>                                                               
                                                                
                               
                                </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField ItemStyle-CssClass="centertext cen3 cen head6 stdout78" HeaderStyle-CssClass="centertext"  >   
                        <HeaderStyle Width="10%" Font-Size="70%"></HeaderStyle>
                        <ItemTemplate>
                                      
                                            <asp:TextBox  ID="txtGoodBehavior" onfocus='<%# "focusbox(60," +Eval("number") + " );" %>' CssClass="wid4 goodbe page12"  tabindex="23" onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" Text='<%# Eval("txtGoodBehavior") %>'></asp:TextBox>                                                               
                                                                  
                               
                                </ItemTemplate>                                   
                       
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="centertext cen3 cen head7 stdout79" HeaderStyle-CssClass="centertext"   HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206427") %>">   
                        <HeaderStyle Width="10%" Font-Size="70%"></HeaderStyle>                                   
                        <ItemTemplate>
                                       
                                            <asp:TextBox  ID="txtGoodReading" onfocus='<%# "focusbox(61," +Eval("number") + " );" %>' CssClass="wid5 readscore page12 disable" Enabled="" tabindex="24" onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" Text='<%# Eval("txtGoodReading") %>'></asp:TextBox>                                                                 
                                        
                        </ItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="centertext samat cen5 head8 stdout80" HeaderStyle-CssClass="centertext"   HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132256") %>">   
                        <HeaderStyle Width="10%" Font-Size="70%"></HeaderStyle>                                   
                        <ItemTemplate>
                                       
                                            <asp:TextBox  ID="txtSamattana" CssClass="wid6 samatscore" Enabled=""  onfocusout='<%# "autosave(this.id,this.value," +Eval("sID") + " );" %>' runat="server" Width="80%" Height="42.5px" Text='<%# Eval("txtSamattana") %>'></asp:TextBox>                                                                 
                                        
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="nopad cen4 cen head9 stdout81" HeaderStyle-CssClass="centertext"  HeaderText="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206071") %>">   
                        <HeaderStyle Width="32%" Font-Size="70%"></HeaderStyle>                                   
                        <ItemTemplate>
                                       
                                            <asp:DropDownList ID="ddlOther"  runat="server" CssClass="special wid50" Width="80%" Height="42.5px" AutoPostBack="false" onchange="CompareDates();">                  
                                 <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %>" Value="-1" class="grey "></asp:ListItem>
                                <asp:ListItem  Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>" Value="1" ></asp:ListItem>
                                <asp:ListItem  Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206416") %>" Value="2" ></asp:ListItem>
                                <asp:ListItem  Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206417") %>" Value="3" ></asp:ListItem>
                                <asp:ListItem  Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206418") %>" Value="4"></asp:ListItem>
                                <asp:ListItem  Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206419") %>" Value="5" ></asp:ListItem>
                                <asp:ListItem  Text="ขร" Value="7" ></asp:ListItem>
                                <asp:ListItem  Text="ขส" Value="8"></asp:ListItem>
                                <asp:ListItem  Text="ท" Value="9" ></asp:ListItem>
                                <asp:ListItem  Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %>" Value="6"></asp:ListItem>
                                <asp:ListItem  Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206541") %>" Value="10" ></asp:ListItem>
                                <asp:ListItem  Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206542") %>" Value="11"></asp:ListItem>
                                <asp:ListItem  Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206543") %>" Value="12" ></asp:ListItem>
                                <asp:ListItem  Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206308") %>" Value="13"></asp:ListItem>
                                
                            </asp:DropDownList>
                                            
                                                                                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="hid2" HeaderStyle-CssClass="centertext"  HeaderText="20"> 
                        <HeaderStyle Width="3%"></HeaderStyle>                                           
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
                    <input type="button" id="btnSearch2" onclick="nextpage(1)" class="btn btn-success search-btn hidden btnok" style="width:200px;"  />
                    <input type="button" id="btnSearch1" onclick="nextpage(0)" class="btn btn-success search-btn btnerror" style="width:200px"  />
                    <input type="button" id="btnSearch3" onclick="nextpage(2)" class="btn btn-primary global-btn pull-right lastbtn" style="width:200px"  />
                    
                    <asp:Button CssClass="btn btn-primary global-btn hidden" ID="btnSave" runat="server" Style="width: 200px;"
                                Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132205") %>" />
                    <asp:Button CssClass="btn btn-warning global-btn btnback" ID="btnCancle" runat="server" Style="width: 100px;"
                                />
                    
                </div>
            </div>

          
        </div>
          <div class="w3-sidebar w3-bar-block w3-card w3-animate-right full-card" style="display:none;right:0;z-index:5;width:700px;top:0px; height:100%;" id="mySidebar">
   
 <div class="col-xs-12 ">
                <div class="centertext">   
                    <asp:Label ID="configratio" cssclass="bold" runat="server" > </asp:Label>
                    </div>

            </div>
    
                <div class="col-xs-4">
                    <div class="col-xs-7 righttext" style="padding:5px;">
                        <asp:Label ID="configaccumulate" cssclass="bold" runat="server" > </asp:Label> 
                    </div>
                    <div class="col-xs-5 " style="padding-left:20px;">
                        <asp:DropDownList ID="scoreRatio"   runat="server" CssClass="quizratio ddl1" Style="width: 70px;" AutoPostBack="false" onchange="CompareDates();">                  
                    </asp:DropDownList>  
                    </div>
                                                     
                   
                </div>

    <div class="col-xs-4">
        <div class="col-xs-7 righttext" style="padding:5px;">
                        <asp:Label ID="configmid" cssclass="bold" runat="server" > </asp:Label> 
                    </div>
                    <div class="col-xs-5 " style="padding-left:20px;">
                         <asp:DropDownList ID="midRatio"   runat="server" CssClass="midratio ddl2" Style="width: 70px;" AutoPostBack="false" onchange="CompareDates();">                  
                        </asp:DropDownList>                        
                    </div>
                </div>

    <div class="col-xs-4">
         <div class="col-xs-7 righttext" style="padding:5px;">
                        <asp:Label ID="configfinal" cssclass="bold" runat="server" > </asp:Label> 
                    </div>
                    <div class="col-xs-5 " style="padding-left:20px;">
                        <asp:DropDownList ID="lastRatio"   runat="server" CssClass="lateratio ddl3" Style="width: 70px;" AutoPostBack="false" onchange="CompareDates();">                  
                        </asp:DropDownList>  
                   
                    </div>
                    
                </div>
    <div class="col-xs-12">
         <div class="col-xs-7 righttext" style="padding:5px;">
                        <asp:Label ID="configpasspersent" cssclass="bold" runat="server" > </asp:Label> 
                    </div>
                    <div class="col-xs-5 " style="padding:0px;">
                        <asp:DropDownList ID="passRatio"   runat="server" CssClass="ddl4" Style="width: 70px;" AutoPostBack="false" onchange="CompareDates();">                  
                        </asp:DropDownList>  
                   
                    </div>
                    
                </div>
                
    <div class="col-xs-12 " style="padding:0px;">
      <hr />
                <div class="col-xs-6 righttext">   
                    <asp:Label ID="configformedit" cssclass="bold" runat="server" > </asp:Label> 
                    </div>
        <div class="col-xs-4 righttext" style="padding:0px; left:5%;">
                        <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206235") %></h4>                     
                    </div>         
        <div class="col-xs-2 righttext" style="padding:0px;">
            <a href="#">
  <img src="https://i.imgur.com/RiBPPSJ.png" onclick="changepage(1)" style="max-width:30px; border:0;">
</a> 
            <a href="#">
  <img src="https://i.imgur.com/pOJuWZJ.png" onclick="changepage(2)" style="max-width:30px; border:0;">
</a> 
        </div>
      </div>
     
    <div class="col-xs-12">         
                    
    </div> 

    <div class="col-xs-12">
         <div class="col-xs-1 righttext" style="padding:0px; margin-top:7px">
                        <input id="check2" class="check2" onclick="CompareDates()" type="checkbox" name="vehicle" value="1">
                    </div>
                    <div class="col-xs-10" style="padding-left:7px;">
                        <asp:Label ID="configdicimal" cssclass="" runat="server" > </asp:Label>                     
                    </div>                    
                </div>
    <div class="col-xs-12">
         <div class="col-xs-1 righttext" style="padding:0px; margin-top:7px">
                        <input id="check3" class="check3" onclick="auto()" type="checkbox" name="vehicle2" value="2"> 
                    </div>
                    <div class="col-xs-10" style="padding-left:7px;">
                        <asp:Label ID="configautoread" cssclass="" runat="server" > </asp:Label>                     
                    </div>                    
                </div>
    
    <div class="col-xs-12">
         <div class="col-xs-1 righttext" style="padding:0px; margin-top:7px">
                        <input id="check4" class="check4" onclick="auto(8)" type="checkbox" name="vehicle3" value="3" runat="server"> 
                    </div>
                    <div class="col-xs-10" style="padding-left:7px;">
                        <asp:Label ID="configaddbehavior" cssclass="" runat="server" > </asp:Label>                   
                    </div>                    
                </div>
    <div class="col-xs-12 subcheckbox hidden">
         <div class="col-xs-2 righttext" style="padding:0px; margin-top:7px">
                        <input id="check5" runat="server" class="check5" onclick="auto()" type="checkbox" name="vehicle2" value="5"> 
                    </div>
                    <div class="col-xs-10" style="padding-left:7px;">
                        <asp:Label ID="configautobehavior" cssclass="" runat="server" > </asp:Label>                   
                    </div>                    
                </div>
    <div class="col-xs-12">
         <div class="col-xs-1 righttext" style="padding:0px; margin-top:7px">
                        <input id="check6" runat="server" class="check6" onclick="auto(9)" type="checkbox" name="vehicle3" value="3"> 
                    </div>
                    <div class="col-xs-10" style="padding-left:7px;">
                        <asp:Label ID="configaddchewat" cssclass="" runat="server" > </asp:Label>                    
                    </div>                    
                </div>
    <div class="col-xs-12">
         <div class="col-xs-1 righttext" style="padding:0px; margin-top:7px">
                        <input id="check11" runat="server" class="check11" onclick="auto()" type="checkbox" name="vehicle3" value="3"> 
                    </div>
                    <div class="col-xs-10" style="padding-left:7px;">
                        <asp:Label ID="configaddmid" cssclass="" runat="server" > </asp:Label>                    
                    </div>                    
                </div>
    <div class="col-xs-12">
         <div class="col-xs-1 righttext" style="padding:0px; margin-top:7px">
                        <input id="check12" runat="server" class="check12" onclick="auto()" type="checkbox" name="vehicle3" value="3"> 
                    </div>
                    <div class="col-xs-10" style="padding-left:7px;">
                        <asp:Label ID="configaddfinal" cssclass="" runat="server" > </asp:Label>                    
                    </div>                    
                </div>
    <div class="col-xs-12">
         <div class="col-xs-1 righttext" style="padding:0px; margin-top:7px">
                        <input id="check7" class="check7" onclick="auto()" type="checkbox" name="vehicle3" value="3"> 
                    </div>
                    <div class="col-xs-10" style="padding-left:7px;">
                        <asp:Label ID="configlosetwotab" cssclass="" runat="server" > </asp:Label>                    
                    </div>                    
                </div>
    <div class="col-xs-12">
         <div class="col-xs-1 righttext" style="padding:0px; margin-top:7px">
                        <input id="check9" class="check9" onclick="auto()" type="checkbox" name="vehicle3" value="3"> 
                    </div>
                    <div class="col-xs-10" style="padding-left:7px;">
                        <asp:Label ID="configclosegrade" cssclass="" runat="server" > </asp:Label>                    
                    </div>                    
                </div>
    <div class="col-xs-12">
         <div class="col-xs-1 righttext" style="padding:0px; margin-top:7px">
                        <input id="check10" class="check10" onclick="auto()" type="checkbox" name="vehicle3" value="3"> 
                    </div>
                    <div class="col-xs-10" style="padding-left:7px;">
                        <asp:Label ID="configclosesamattana" cssclass="" runat="server" > </asp:Label>                    
                    </div>                    
                </div>
    <div class="col-xs-12">
         <div class="col-xs-1 righttext" style="padding:0px; margin-top:7px">
                        <input id="check8" class="check8" onclick="ddlshare()" type="checkbox" name="vehicle3" value="3" runat="server"> 
                    </div>
                    <div class="col-xs-10" style="padding-left:7px;">
                        <asp:Label ID="configdisableshare" cssclass="" runat="server" > </asp:Label>
                    </div>                    
                </div>
    <div id="shareheader" runat="server" class="col-xs-12 classchoose2" style="padding:5px;">
                <div class="col-xs-4 righttext">
                    <asp:Label ID="Label2" cssclass="" runat="server" > </asp:Label>
                    </div>
                <div class="col-xs-7">
                   <asp:DropDownList ID="modalClass2" onchange="addword4()"  runat="server" width="100%" CssClass="js-example-basic-multiple4" name="classchoice4[]" multiple="multiple" >        
                   </asp:DropDownList>
                </div>
            </div>
    <div class="col-xs-12 classchoose2" style="padding:5px;">
                <div class="col-xs-5 righttext">
                    <asp:Label ID="Label3" cssclass="sharedetail1" runat="server" > </asp:Label>
                    </div>
                <div class="col-xs-7">
                   <asp:Label ID="Label4" cssclass="sharedetail2" runat="server" > </asp:Label>
                </div>
            </div>

            <div class="col-xs-12 hidden" style="padding:5px;">
                <div class="col-xs-4 righttext">
                    <label>xxxxxx</label>
                    </div>
                <div class="col-xs-7">
                   <asp:Textbox ID="editmulticlass" class="form-control editmulti"  runat="server" width="80%" > </asp:Textbox>
                </div>
            </div>

          

            <div class="col-xs-12 hidden" style="padding:5px;">
                <div class="col-xs-4 righttext">
                    <label>xxxxxx4</label>
                    </div>
                <div class="col-xs-7">
                   <asp:Textbox ID="editmulticlass4" class="form-control editmulti4"  runat="server" width="80%" > </asp:Textbox>
                </div>
            </div>
    

  <div class="col-xs-12 hidden">
      <hr />
                <div class="centertext">   
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603033") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202030") %> </label> 
                    </div>
      </div>
      <div class="col-xs-12 hidden">
    <asp:DropDownList ID="DropDownList1"   runat="server"  Style="width: 100%;" AutoPostBack="false">                  
        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106065") %>" Value="30" class="grey hidden"></asp:ListItem>
        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %>" value="1"  class="grey"></asp:ListItem>
        <asp:ListItem Text="3 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02861") %>" value="2" class="grey"></asp:ListItem>
    </asp:DropDownList>  
                   
  </div> 

    <div class="col-xs-12 hid">
    <label>hidden</label> 
                   
  </div> 
  
</div>

            <div class="w3-sidebar w3-bar-block w3-card w3-animate-right full-card" style="display: none; right: 0; z-index: 5; width: 800px; top: 0px; height: 100%;" id="groupSidebar">
                <div class="col-xs-12 ">
                    <div class="centertext">
                        <asp:Label ID="Label7" CssClass="bold" runat="server">จัดกลุ่ม</asp:Label>
                    </div>
                </div>

                <style>
                    .px0{
                        padding-left:0px;
                        padding-right:0px;
                    }
                    .mx0{
                        margin-left:0px;
                        margin-right:0px;
                    }
                    .my8{
                        margin-top:8px;
                        margin-bottom:8px;
                    }
                </style>
                <div id="detail" class="col-xs-12 ">
                    <%-- ############################### groupDetail ############################### --%>
                    <div class="panel-group">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse999" style="font-size: 24px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132276") %></a>
                                    <asp:Label ID="headingGroupName_detail" CssClass="" runat="server"></asp:Label>
                                </h4>
                            </div>
                            <div id="collapse999" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <table id="groupDetail" class="cell-border text-nowrap " style="width: 100%;">
                                                <thead style="background-color: #337AB7">
                                                    <tr style="color:#fff">
                                                        <th></th>
                                                        <th></th>
                                                        <th></th>
                                                        <th>button</th>

                                                    </tr>
                                                </thead>
                                                <tbody></tbody>
                                            </table>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <input  id="hiddenGroupId" type="hidden"/>


                <div id="boxCoverGroup" class="col-xs-12 ">
                   <script>
                       scriptGenCollapseGroup(1);
                       //scriptGenCollapseGroup(2);
                       //scriptGenCollapseGroup(3);
                       //scriptGenCollapseGroup(4);

                   </script>
                   

                    <%-- ############################### group1 ############################### --%>
<%--                    <div class="panel-group">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" href="#collapse1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M504009") %>1</a> <asp:Label ID="headingGroupName_1" cssclass="" runat="server" ></asp:Label>
                                </h4>
                            </div>
                            <div id="collapse1" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="row my8">
                                                <div class="col-xs-4 text-right">
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401075") %>
                                                    </div>
                                                <div class="col-xs-6">
                                                    <input name="groupName" id="txtGroupName_1" class="form-control" />
                                                </div>
                                            </div>
                                            <div class="row my8 px0">
                                                <div class="col-xs-5 col-xs-offset-1 px0">
                                                    <select name="ddlListGroup" id="select_1" class="form-control" multiple="multiple" style="width: 150px;margin:auto">
                                                        <option value="AAAAAAAAA">AAAAAAAAA</option>
                                                        <option value="BBBBBBBBB">BBBBBBBBB</option>
                                                        <option value="CCCCCCCCC">CCCCCCCCC</option>
                                                        <option value="DDDDDDDDD">DDDDDDDDD</option>
                                                        <option value="EEEEEEEEE">EEEEEEEEE</option>
                                                        <option value="FFFFFFFFF">FFFFFFFFF</option>
                                                    </select>
                                                </div>
                                                <div class="col-xs-1 px0">
                                                    <br />
                                                        <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601019") %> =></label>
                                                    </div>
                                                <div class="col-xs-4 px0">
                                                    <select name="ddlSelectedGroup" id="add_1" class="form-control" multiple="multiple" style="width: 150px;margin:auto">
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                    <%--<div class="col-xs-offset-10 col-xs-2">
                        <button type="button" class="btn btn-success" id="btnGroupCreate" onclick="clickSaveGroup()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401074") %></button>
                    </div>
                    <div class="col-xs-offset-9 col-xs-3 right" style="padding-right:0px">
                        <button type="button" class="btn btn-success" id="btnGroupEdit" onclick="clickSaveEditGroup()" style="display: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105042") %></button>
                    </div>--%>
                </div>


                <div class="col-xs-12 hid">
                    <label>hidden</label>
                </div>
            </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
   


    <div class="modal fade" id="moadlCheckDelete" role="dialog">
    <div class="modal-dialog" style="margin:200px auto">
    
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal" onclick="ClickDeleteCancle()" >&times;</button>
          <h3 class="modal-title center"><label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601037") %></label></h3>
        </div>
        <div class="modal-body center">
            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M504009") %> <label id="modalGroupname"></label> จะถูกลบ และการจัดกลุ่มจะถูกยกเลิก <br />
          <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802056") %>
           
        </div>
        <div class="modal-footer">
            <div class="row">
                <div class="col-xs-12 center">
                    <button type="button" class="btn btn-primary" id="btnOKforDelete" onclick="ClickDeleteOK()" data-dismiss="modal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101165") %></button>
                     <button type="button" class="btn btn-danger" id="btnCancleDelete" onclick="ClickDeleteCancle()" data-dismiss="modal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                </div>
            </div>
        </div>
      </div>
      
    </div>
  </div>



</asp:Content>

