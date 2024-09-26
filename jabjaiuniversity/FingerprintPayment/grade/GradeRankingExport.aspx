<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="GradeRankingExport.aspx.cs" Inherits="FingerprintPayment.grade.GradeRankingExport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="full-card planelist-container" style="width: 100%; padding: 0px;">

        <%--        <link rel="stylesheet" href="//min.gitcdn.xyz/repo/wintercounter/Protip/master/protip.min.css">
        <script src="//min.gitcdn.xyz/repo/wintercounter/Protip/master/protip.min.js"></script>--%>
        <link href="/Styles/protip.min.css" rel="stylesheet" />
        <script src="/Scripts/protip.min.js"></script>
        <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
        <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>
        <style>
            /* W3.CSS 4.10 February 2018 by Jan Egil and Borge Refsnes */

            /* End extract */

            .w3-image {
                max-width: 100%;
                height: auto
            }

            img {
                vertical-align: middle
            }

            a {
                color: inherit
            }

            .w3-table, .w3-table-all {
                border-collapse: collapse;
                border-spacing: 0;
                width: 100%;
                display: table
            }

            .w3-table-all {
                border: 1px solid #ccc
            }

                .w3-bordered tr, .w3-table-all tr {
                    border-bottom: 1px solid #ddd
                }

            .w3-striped tbody tr:nth-child(even) {
                background-color: #f1f1f1
            }

            .w3-table-all tr:nth-child(odd) {
                background-color: #fff
            }

            .w3-table-all tr:nth-child(even) {
                background-color: #f1f1f1
            }

            .w3-hoverable tbody tr:hover, .w3-ul.w3-hoverable li:hover {
                background-color: #ccc
            }

            .w3-centered tr th, .w3-centered tr td {
                text-align: center
            }

            .w3-table td, .w3-table th, .w3-table-all td, .w3-table-all th {
                padding: 8px 8px;
                display: table-cell;
                text-align: left;
                vertical-align: top
            }

                .w3-table th:first-child, .w3-table td:first-child, .w3-table-all th:first-child, .w3-table-all td:first-child {
                    padding-left: 16px
                }

            .w3-btn, .w3-button {
                border: none;
                display: inline-block;
                padding: 8px 16px;
                vertical-align: middle;
                overflow: hidden;
                text-decoration: none;
                color: inherit;
                background-color: inherit;
                text-align: center;
                cursor: pointer;
                white-space: nowrap
            }

                .w3-btn:hover {
                    box-shadow: 0 8px 16px 0 rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19)
                }

            .w3-btn, .w3-button {
                -webkit-touch-callout: none;
                -webkit-user-select: none;
                -khtml-user-select: none;
                -moz-user-select: none;
                -ms-user-select: none;
                user-select: none
            }

                .w3-disabled, .w3-btn:disabled, .w3-button:disabled {
                    cursor: not-allowed;
                    opacity: 0.3
                }

                    .w3-disabled *, :disabled * {
                        pointer-events: none
                    }

                    .w3-btn.w3-disabled:hover, .w3-btn:disabled:hover {
                        box-shadow: none
                    }

            .w3-badge, .w3-tag {
                background-color: #000;
                color: #fff;
                display: inline-block;
                padding-left: 8px;
                padding-right: 8px;
                text-align: center
            }

            .w3-badge {
                border-radius: 50%
            }

            .w3-ul {
                list-style-type: none;
                padding: 0;
                margin: 0
            }

                .w3-ul li {
                    padding: 8px 16px;
                    border-bottom: 1px solid #ddd
                }

                    .w3-ul li:last-child {
                        border-bottom: none
                    }

            .w3-tooltip, .w3-display-container {
                position: relative
            }

                .w3-tooltip .w3-text {
                    display: none
                }

                .w3-tooltip:hover .w3-text {
                    display: inline-block
                }

            .w3-ripple:active {
                opacity: 0.5
            }

            .w3-ripple {
                transition: opacity 0s
            }

            .w3-input {
                padding: 8px;
                display: block;
                border: none;
                border-bottom: 1px solid #ccc;
                width: 100%
            }

            .w3-select {
                padding: 9px 0;
                width: 100%;
                border: none;
                border-bottom: 1px solid #ccc
            }

            .w3-dropdown-click, .w3-dropdown-hover {
                position: relative;
                display: inline-block;
                cursor: pointer
            }

                .w3-dropdown-hover:hover .w3-dropdown-content {
                    display: block
                }

                .w3-dropdown-hover:first-child, .w3-dropdown-click:hover {
                    background-color: #ccc;
                    color: #000
                }

                    .w3-dropdown-hover:hover > .w3-button:first-child, .w3-dropdown-click:hover > .w3-button:first-child {
                        background-color: #ccc;
                        color: #000
                    }

            .w3-dropdown-content {
                cursor: auto;
                color: #000;
                background-color: #fff;
                display: none;
                position: absolute;
                min-width: 160px;
                margin: 0;
                padding: 0;
                z-index: 1
            }

            .w3-check, .w3-radio {
                width: 24px;
                height: 24px;
                position: relative;
                top: 6px
            }

            .w3-sidebar {
                height: 100%;
                width: 200px;
                background-color: #fff;
                position: fixed !important;
                z-index: 1;
                overflow: auto
            }

            .w3-bar-block .w3-dropdown-hover, .w3-bar-block .w3-dropdown-click {
                width: 100%
            }

                .w3-bar-block .w3-dropdown-hover .w3-dropdown-content, .w3-bar-block .w3-dropdown-click .w3-dropdown-content {
                    min-width: 100%
                }

                .w3-bar-block .w3-dropdown-hover .w3-button, .w3-bar-block .w3-dropdown-click .w3-button {
                    width: 100%;
                    text-align: left;
                    padding: 8px 16px
                }

            .w3-main, #main {
                transition: margin-left .4s
            }

            .w3-modal {
                z-index: 3;
                display: none;
                padding-top: 100px;
                position: fixed;
                left: 0;
                top: 0;
                width: 100%;
                height: 100%;
                overflow: auto;
                background-color: rgb(0,0,0);
                background-color: rgba(0,0,0,0.4)
            }

            .w3-modal-content {
                margin: auto;
                background-color: #fff;
                position: relative;
                padding: 0;
                outline: 0;
                width: 600px
            }

            .w3-bar {
                width: 100%;
                overflow: hidden
            }

            .w3-center .w3-bar {
                display: inline-block;
                width: auto
            }

            .w3-bar .w3-bar-item {
                padding: 8px 16px;
                float: left;
                width: auto;
                border: none;
                display: block;
                outline: 0
            }

            .w3-bar .w3-dropdown-hover, .w3-bar .w3-dropdown-click {
                position: static;
                float: left
            }

            .w3-bar .w3-button {
                white-space: normal
            }

            .w3-bar-block .w3-bar-item {
                width: 100%;
                display: block;
                padding: 8px 16px;
                text-align: left;
                border: none;
                white-space: normal;
                float: none;
                outline: 0
            }

            .w3-bar-block.w3-center .w3-bar-item {
                text-align: center
            }

            .w3-block {
                display: block;
                width: 100%
            }

            .w3-responsive {
                display: block;
                overflow-x: auto
            }

            .w3-container:after, .w3-container:before, .w3-panel:after, .w3-panel:before, .w3-row:after, .w3-row:before, .w3-row-padding:after, .w3-row-padding:before,
            .w3-cell-row:before, .w3-cell-row:after, .w3-clear:after, .w3-clear:before, .w3-bar:before, .w3-bar:after {
                content: "";
                display: table;
                clear: both
            }

            .w3-col, .w3-half, .w3-third, .w3-twothird, .w3-threequarter, .w3-quarter {
                float: left;
                width: 100%
            }

                .w3-col.s1 {
                    width: 8.33333%
                }

                .w3-col.s2 {
                    width: 16.66666%
                }

                .w3-col.s3 {
                    width: 24.99999%
                }

                .w3-col.s4 {
                    width: 33.33333%
                }

                .w3-col.s5 {
                    width: 41.66666%
                }

                .w3-col.s6 {
                    width: 49.99999%
                }

                .w3-col.s7 {
                    width: 58.33333%
                }

                .w3-col.s8 {
                    width: 66.66666%
                }

                .w3-col.s9 {
                    width: 74.99999%
                }

                .w3-col.s10 {
                    width: 83.33333%
                }

                .w3-col.s11 {
                    width: 91.66666%
                }

                .w3-col.s12 {
                    width: 99.99999%
                }

            @media (min-width:601px) {
                .w3-col.m1 {
                    width: 8.33333%
                }

                .w3-col.m2 {
                    width: 16.66666%
                }

                .w3-col.m3, .w3-quarter {
                    width: 24.99999%
                }

                .w3-col.m4, .w3-third {
                    width: 33.33333%
                }

                .w3-col.m5 {
                    width: 41.66666%
                }

                .w3-col.m6, .w3-half {
                    width: 49.99999%
                }

                .w3-col.m7 {
                    width: 58.33333%
                }

                .w3-col.m8, .w3-twothird {
                    width: 66.66666%
                }

                .w3-col.m9, .w3-threequarter {
                    width: 74.99999%
                }

                .w3-col.m10 {
                    width: 83.33333%
                }

                .w3-col.m11 {
                    width: 91.66666%
                }

                .w3-col.m12 {
                    width: 99.99999%
                }
            }

            @media (min-width:993px) {
                .w3-col.l1 {
                    width: 8.33333%
                }

                .w3-col.l2 {
                    width: 16.66666%
                }

                .w3-col.l3 {
                    width: 24.99999%
                }

                .w3-col.l4 {
                    width: 33.33333%
                }

                .w3-col.l5 {
                    width: 41.66666%
                }

                .w3-col.l6 {
                    width: 49.99999%
                }

                .w3-col.l7 {
                    width: 58.33333%
                }

                .w3-col.l8 {
                    width: 66.66666%
                }

                .w3-col.l9 {
                    width: 74.99999%
                }

                .w3-col.l10 {
                    width: 83.33333%
                }

                .w3-col.l11 {
                    width: 91.66666%
                }

                .w3-col.l12 {
                    width: 99.99999%
                }
            }

            .w3-content {
                max-width: 980px;
                margin: auto
            }

            .w3-rest {
                overflow: hidden
            }

            .w3-cell-row {
                display: table;
                width: 100%
            }

            .w3-cell {
                display: table-cell
            }

            .w3-cell-top {
                vertical-align: top
            }

            .w3-cell-middle {
                vertical-align: middle
            }

            .w3-cell-bottom {
                vertical-align: bottom
            }

            .w3-hide {
                display: none !important
            }

            .w3-show-block, .w3-show {
                display: block !important
            }

            .w3-show-inline-block {
                display: inline-block !important
            }

            @media (max-width:600px) {
                .w3-modal-content {
                    margin: 0 10px;
                    width: auto !important
                }

                .w3-modal {
                    padding-top: 30px
                }

                .w3-dropdown-hover.w3-mobile .w3-dropdown-content, .w3-dropdown-click.w3-mobile .w3-dropdown-content {
                    position: relative
                }

                .w3-hide-small {
                    display: none !important
                }

                .w3-mobile {
                    display: block;
                    width: 100% !important
                }

                .w3-bar-item.w3-mobile, .w3-dropdown-hover.w3-mobile, .w3-dropdown-click.w3-mobile {
                    text-align: center
                }

                    .w3-dropdown-hover.w3-mobile, .w3-dropdown-hover.w3-mobile .w3-btn, .w3-dropdown-hover.w3-mobile .w3-button, .w3-dropdown-click.w3-mobile, .w3-dropdown-click.w3-mobile .w3-btn, .w3-dropdown-click.w3-mobile .w3-button {
                        width: 100%
                    }
            }

            @media (max-width:768px) {
                .w3-modal-content {
                    width: 500px
                }

                .w3-modal {
                    padding-top: 50px
                }
            }

            @media (min-width:993px) {
                .w3-modal-content {
                    width: 900px
                }

                .w3-hide-large {
                    display: none !important
                }

                .w3-sidebar.w3-collapse {
                    display: block !important
                }
            }

            @media (max-width:992px) and (min-width:601px) {
                .w3-hide-medium {
                    display: none !important
                }
            }

            @media (max-width:992px) {
                .w3-sidebar.w3-collapse {
                    display: none
                }

                .w3-main {
                    margin-left: 0 !important;
                    margin-right: 0 !important
                }
            }

            .w3-top, .w3-bottom {
                position: fixed;
                width: 100%;
                z-index: 1
            }

            .w3-top {
                top: 0
            }

            .w3-bottom {
                bottom: 0
            }

            .w3-overlay {
                position: fixed;
                display: none;
                width: 100%;
                height: 100%;
                top: 0;
                left: 0;
                right: 0;
                bottom: 0;
                background-color: rgba(0,0,0,0.5);
                z-index: 2
            }

            .w3-display-topleft {
                position: absolute;
                left: 0;
                top: 0
            }

            .w3-display-topright {
                position: absolute;
                right: 0;
                top: 0
            }

            .w3-display-bottomleft {
                position: absolute;
                left: 0;
                bottom: 0
            }

            .w3-display-bottomright {
                position: absolute;
                right: 0;
                bottom: 0
            }

            .w3-display-middle {
                position: absolute;
                top: 50%;
                left: 50%;
                transform: translate(-50%,-50%);
                -ms-transform: translate(-50%,-50%)
            }

            .w3-display-left {
                position: absolute;
                top: 50%;
                left: 0%;
                transform: translate(0%,-50%);
                -ms-transform: translate(-0%,-50%)
            }

            .w3-display-right {
                position: absolute;
                top: 50%;
                right: 0%;
                transform: translate(0%,-50%);
                -ms-transform: translate(0%,-50%)
            }

            .w3-display-topmiddle {
                position: absolute;
                left: 50%;
                top: 0;
                transform: translate(-50%,0%);
                -ms-transform: translate(-50%,0%)
            }

            .w3-display-bottommiddle {
                position: absolute;
                left: 50%;
                bottom: 0;
                transform: translate(-50%,0%);
                -ms-transform: translate(-50%,0%)
            }

            .w3-display-container:hover .w3-display-hover {
                display: block
            }

            .w3-display-container:hover span.w3-display-hover {
                display: inline-block
            }

            .w3-display-hover {
                display: none
            }

            .w3-display-position {
                position: absolute
            }

            .w3-circle {
                border-radius: 50%
            }

            .w3-round-small {
                border-radius: 2px
            }

            .w3-round, .w3-round-medium {
                border-radius: 4px
            }

            .w3-round-large {
                border-radius: 8px
            }

            .w3-round-xlarge {
                border-radius: 16px
            }

            .w3-round-xxlarge {
                border-radius: 32px
            }

            .w3-row-padding, .w3-row-padding > .w3-half, .w3-row-padding > .w3-third, .w3-row-padding > .w3-twothird, .w3-row-padding > .w3-threequarter, .w3-row-padding > .w3-quarter, .w3-row-padding > .w3-col {
                padding: 0 8px
            }

            .w3-container, .w3-panel {
                padding: 0.01em 16px
            }

            .w3-panel {
                margin-top: 16px;
                margin-bottom: 16px
            }

            .w3-code, .w3-codespan {
                font-family: Consolas,"courier new";
                font-size: 16px
            }

            .w3-code {
                width: auto;
                background-color: #fff;
                padding: 8px 12px;
                border-left: 4px solid #4CAF50;
                word-wrap: break-word
            }

            .w3-codespan {
                color: crimson;
                background-color: #f1f1f1;
                padding-left: 4px;
                padding-right: 4px;
                font-size: 110%
            }

            .w3-card, .w3-card-2 {
                box-shadow: 0 2px 5px 0 rgba(0,0,0,0.16),0 2px 10px 0 rgba(0,0,0,0.12)
            }

            .w3-card-4, .w3-hover-shadow:hover {
                box-shadow: 0 4px 10px 0 rgba(0,0,0,0.2),0 4px 20px 0 rgba(0,0,0,0.19)
            }

            .w3-spin {
                animation: w3-spin 2s infinite linear
            }

            @keyframes w3-spin {
                0% {
                    transform: rotate(0deg)
                }

                100% {
                    transform: rotate(359deg)
                }
            }

            .w3-animate-fading {
                animation: fading 10s infinite
            }

            @keyframes fading {
                0% {
                    opacity: 0
                }

                50% {
                    opacity: 1
                }

                100% {
                    opacity: 0
                }
            }

            .w3-animate-opacity {
                animation: opac 0.8s
            }

            @keyframes opac {
                from {
                    opacity: 0
                }

                to {
                    opacity: 1
                }
            }

            .w3-animate-top {
                position: relative;
                animation: animatetop 0.4s
            }

            @keyframes animatetop {
                from {
                    top: -300px;
                    opacity: 0
                }

                to {
                    top: 0;
                    opacity: 1
                }
            }

            .w3-animate-left {
                position: relative;
                animation: animateleft 0.4s
            }

            @keyframes animateleft {
                from {
                    left: -300px;
                    opacity: 0
                }

                to {
                    left: 0;
                    opacity: 1
                }
            }

            .w3-animate-right {
                position: relative;
                animation: animateright 0.4s
            }

            @keyframes animateright {
                from {
                    right: -300px;
                    opacity: 0
                }

                to {
                    right: 0;
                    opacity: 1
                }
            }

            .w3-animate-bottom {
                position: relative;
                animation: animatebottom 0.4s
            }

            @keyframes animatebottom {
                from {
                    bottom: -300px;
                    opacity: 0
                }

                to {
                    bottom: 0;
                    opacity: 1
                }
            }

            .w3-animate-zoom {
                animation: animatezoom 0.6s
            }

            @keyframes animatezoom {
                from {
                    transform: scale(0)
                }

                to {
                    transform: scale(1)
                }
            }

            .w3-animate-input {
                transition: width 0.4s ease-in-out
            }

                .w3-animate-input:focus {
                    width: 100% !important
                }

            .w3-opacity, .w3-hover-opacity:hover {
                opacity: 0.60
            }

            .w3-opacity-off, .w3-hover-opacity-off:hover {
                opacity: 1
            }

            .w3-opacity-max {
                opacity: 0.25
            }

            .w3-opacity-min {
                opacity: 0.75
            }

            .w3-greyscale-max, .w3-grayscale-max, .w3-hover-greyscale:hover, .w3-hover-grayscale:hover {
                filter: grayscale(100%)
            }

            .w3-greyscale, .w3-grayscale {
                filter: grayscale(75%)
            }

            .w3-greyscale-min, .w3-grayscale-min {
                filter: grayscale(50%)
            }

            .w3-sepia {
                filter: sepia(75%)
            }

            .w3-sepia-max, .w3-hover-sepia:hover {
                filter: sepia(100%)
            }

            .w3-sepia-min {
                filter: sepia(50%)
            }

            .w3-tiny {
                font-size: 10px !important
            }

            .w3-small {
                font-size: 12px !important
            }

            .w3-medium {
                font-size: 15px !important
            }

            .w3-large {
                font-size: 18px !important
            }

            .w3-xlarge {
                font-size: 24px !important
            }

            .w3-xxlarge {
                font-size: 36px !important
            }

            .w3-xxxlarge {
                font-size: 48px !important
            }

            .w3-jumbo {
                font-size: 64px !important
            }

            .w3-left-align {
                text-align: left !important
            }

            .w3-right-align {
                text-align: right !important
            }

            .w3-justify {
                text-align: justify !important
            }

            .w3-center {
                text-align: center !important
            }

            .w3-border-0 {
                border: 0 !important
            }

            .w3-border {
                border: 1px solid #ccc !important
            }

            .w3-border-top {
                border-top: 1px solid #ccc !important
            }

            .w3-border-bottom {
                border-bottom: 1px solid #ccc !important
            }

            .w3-border-left {
                border-left: 1px solid #ccc !important
            }

            .w3-border-right {
                border-right: 1px solid #ccc !important
            }

            .w3-topbar {
                border-top: 6px solid #ccc !important
            }

            .w3-bottombar {
                border-bottom: 6px solid #ccc !important
            }

            .w3-leftbar {
                border-left: 6px solid #ccc !important
            }

            .w3-rightbar {
                border-right: 6px solid #ccc !important
            }

            .w3-section, .w3-code {
                margin-top: 16px !important;
                margin-bottom: 16px !important
            }

            .w3-margin {
                margin: 16px !important
            }

            .w3-margin-top {
                margin-top: 16px !important
            }

            .w3-margin-bottom {
                margin-bottom: 16px !important
            }

            .w3-margin-left {
                margin-left: 16px !important
            }

            .w3-margin-right {
                margin-right: 16px !important
            }

            .w3-padding-small {
                padding: 4px 8px !important
            }

            .w3-padding {
                padding: 8px 16px !important
            }

            .w3-padding-large {
                padding: 12px 24px !important
            }

            .w3-padding-16 {
                padding-top: 16px !important;
                padding-bottom: 16px !important
            }

            .w3-padding-24 {
                padding-top: 24px !important;
                padding-bottom: 24px !important
            }

            .w3-padding-32 {
                padding-top: 32px !important;
                padding-bottom: 32px !important
            }

            .w3-padding-48 {
                padding-top: 48px !important;
                padding-bottom: 48px !important
            }

            .w3-padding-64 {
                padding-top: 64px !important;
                padding-bottom: 64px !important
            }

            .w3-left {
                float: left !important
            }

            .w3-right {
                float: right !important
            }

            .w3-button:hover {
                color: #000 !important;
                background-color: #ccc !important
            }

            .w3-transparent, .w3-hover-none:hover {
                background-color: transparent !important
            }

            .w3-hover-none:hover {
                box-shadow: none !important
            }
            /* Colors */
            .w3-amber, .w3-hover-amber:hover {
                color: #000 !important;
                background-color: #ffc107 !important
            }

            .w3-aqua, .w3-hover-aqua:hover {
                color: #000 !important;
                background-color: #00ffff !important
            }

            .w3-blue, .w3-hover-blue:hover {
                color: #fff !important;
                background-color: #2196F3 !important
            }

            .w3-light-blue, .w3-hover-light-blue:hover {
                color: #000 !important;
                background-color: #87CEEB !important
            }

            .w3-brown, .w3-hover-brown:hover {
                color: #fff !important;
                background-color: #795548 !important
            }

            .w3-cyan, .w3-hover-cyan:hover {
                color: #000 !important;
                background-color: #00bcd4 !important
            }

            .w3-blue-grey, .w3-hover-blue-grey:hover, .w3-blue-gray, .w3-hover-blue-gray:hover {
                color: #fff !important;
                background-color: #607d8b !important
            }

            .w3-green, .w3-hover-green:hover {
                color: #fff !important;
                background-color: #4CAF50 !important
            }

            .w3-light-green, .w3-hover-light-green:hover {
                color: #000 !important;
                background-color: #8bc34a !important
            }

            .w3-indigo, .w3-hover-indigo:hover {
                color: #fff !important;
                background-color: #3f51b5 !important
            }

            .w3-khaki, .w3-hover-khaki:hover {
                color: #000 !important;
                background-color: #f0e68c !important
            }

            .w3-lime, .w3-hover-lime:hover {
                color: #000 !important;
                background-color: #cddc39 !important
            }

            .w3-orange, .w3-hover-orange:hover {
                color: #000 !important;
                background-color: #ff9800 !important
            }

            .w3-deep-orange, .w3-hover-deep-orange:hover {
                color: #fff !important;
                background-color: #ff5722 !important
            }

            .w3-pink, .w3-hover-pink:hover {
                color: #fff !important;
                background-color: #e91e63 !important
            }

            .w3-purple, .w3-hover-purple:hover {
                color: #fff !important;
                background-color: #9c27b0 !important
            }

            .w3-deep-purple, .w3-hover-deep-purple:hover {
                color: #fff !important;
                background-color: #673ab7 !important
            }

            .w3-red, .w3-hover-red:hover {
                color: #fff !important;
                background-color: #f44336 !important
            }

            .w3-sand, .w3-hover-sand:hover {
                color: #000 !important;
                background-color: #fdf5e6 !important
            }

            .w3-teal, .w3-hover-teal:hover {
                color: #fff !important;
                background-color: #009688 !important
            }

            .w3-yellow, .w3-hover-yellow:hover {
                color: #000 !important;
                background-color: #ffeb3b !important
            }

            .w3-white, .w3-hover-white:hover {
                color: #000 !important;
                background-color: #fff !important
            }

            .w3-black, .w3-hover-black:hover {
                color: #fff !important;
                background-color: #000 !important
            }

            .w3-grey, .w3-hover-grey:hover, .w3-gray, .w3-hover-gray:hover {
                color: #000 !important;
                background-color: #9e9e9e !important
            }

            .w3-light-grey, .w3-hover-light-grey:hover, .w3-light-gray, .w3-hover-light-gray:hover {
                color: #000 !important;
                background-color: #f1f1f1 !important
            }

            .w3-dark-grey, .w3-hover-dark-grey:hover, .w3-dark-gray, .w3-hover-dark-gray:hover {
                color: #fff !important;
                background-color: #616161 !important
            }

            .w3-pale-red, .w3-hover-pale-red:hover {
                color: #000 !important;
                background-color: #ffdddd !important
            }

            .w3-pale-green, .w3-hover-pale-green:hover {
                color: #000 !important;
                background-color: #ddffdd !important
            }

            .w3-pale-yellow, .w3-hover-pale-yellow:hover {
                color: #000 !important;
                background-color: #ffffcc !important
            }

            .w3-pale-blue, .w3-hover-pale-blue:hover {
                color: #000 !important;
                background-color: #ddffff !important
            }

            .btn2 {
                border-radius: 0;
                border: 0;
                border-bottom: 4px solid #CCCCCC;
                margin: 0;
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
                color: white;
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

                    .dropdown-content a:hover {
                        background-color: #ddd;
                    }

            .dropdown:hover .dropdown-content {
                display: block;
            }

            .dropdown:hover .dropbtn {
                background-color: #3e8e41;
            }

            .btn2-pressure {
                position: relative;
                margin-bottom: 0;
            }

                .btn2-pressure:focus {
                    -moz-outline-style: none;
                    outline: medium none;
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

            .rotate2 {
                transform: rotate(90deg);
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

                #myImg:hover {
                    opacity: 0.7;
                }

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
                from {
                    -webkit-transform: scale(0)
                }

                to {
                    -webkit-transform: scale(1)
                }
            }

            @keyframes zoom {
                from {
                    transform: scale(0)
                }

                to {
                    transform: scale(1)
                }
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
            @media only screen and (max-width: 700px) {
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

            .centertext2 {
                text-align: center;
                color: white;
            }

            .hid {
                visibility: hidden;
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
                display: none;
            }

            .bold {
                font-weight: bold;
            }

            .noborder {
                outline: none;
            }

            .hid4 {
                white-space: nowrap;
                background-color: #337AB7;
                background: #337AB7;
                position: absolute;
                left: 9999px;
            }

            .hid3 {
                opacity: 0;
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

            .cen {
                padding-left: 0px;
                padding-right: 0px;
                padding-top: 3.5px;
                padding-bottom: 3.5px;
                border: 1px solid white;
            }

            .cen2 {
                padding-top: 3.5px;
                padding-bottom: 3.5px;
                width: 9.5%;
            }

            .cen2alt {
                padding-top: 3.5px;
                padding-bottom: 3.5px;
                width: 9.5%;
            }

            .cen3 {
                padding-top: 3.5px;
                padding-bottom: 3.5px;
                width: 12%;
            }

            .cen4 {
                padding-left: 0px;
                width: 15%;
            }

            .cen5 {
                padding-left: 0px;
                padding-right: 5px;
                width: 12%;
            }

            .cen4alt {
                padding-left: 0px;
                padding-right: 5px;
                width: 15%;
            }

            .nopad100Tooltip {
                background: #333;
                color: #fff;
            }

            .txtglow {
                box-shadow: 0px 0px 7px red;
                border-color: none;
                background-color: pink;
            }

            .txtglow2 {
                box-shadow: 0px 0px 7px orange;
                border-color: none;
                background-color: yellow;
            }

            .name {
                overflow: hidden;
                white-space: nowrap;
                table-layout: fixed;
                padding-left: 5px;
            }

            .tdtr {
                border: 1px solid #000000;
                text-align: center;
                padding: 0px;
            }

            .centertext {
            }

            .tdtrx {
                border: 1px solid #000000;
                border-left: none;
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
                border-left: none;
                text-align: center;
                border-right: none;
                padding: 0px;
            }

            .tdtr22 {
                border: 1px solid #000000;
                border-left: none;
                text-align: center;
                padding: 0px;
            }

            .tdtr4 {
                border: 1px solid #000000;
                border-bottom: none;
                text-align: center;
                padding: 0px;
            }

            .tdtr5 {
                border: 1px solid #000000;
                border-bottom: none;
                border-top: none;
                border-left: none;
                text-align: center;
                padding: 0px;
            }

            .borderline {
                border-right: none;
                border-bottom: 1px solid #a3b7c8;
                border-top: none;
                border-left: none;
            }

            .borderline2 {
                border-right: none;
                border-bottom: 1px solid #a3b7c8;
                border-top: none;
                border-left: 1px solid #a3b7c8;
            }

            .notdtr {
                border: 1px solid #000000;
                border-bottom: none;
                border-top: none;
                border-left: none;
                text-align: center;
                padding: 0px;
            }

            .textAlignVer {
                display: block;
                filter: flipv fliph;
                -webkit-transform: rotate(-90deg);
                -moz-transform: rotate(-90deg);
                transform: rotate(-90deg);
                white-space: nowrap;
                background-color: #337AB7;
                height: 130px;
                color: #000000;
                padding: 0px;
            }

            }


            .nopad {
                padding: 0px;
                width: 70px;
            }

            .maxtest {
                font-size: 80%;
            }

            .maxtestcw {
                font-size: 80%;
            }

            .maxmidscore {
                font-size: 90%;
            }

            .maxlatescore {
                font-size: 90%;
            }

            .nopad180 {
                padding: 0px;
                width: 19.5%;
            }

            .nopad40 {
                padding: 0px;
                width: 90%;
            }

            .nopad100 {
                font-size: 72%;
                width: 130px;
                height: 40px;
                background: rgba(0,0,0,0);
                border: none;
                color: white;
                background-color: #337AB7;
                font-weight: normal;
            }

            .nopad10 {
                padding-right: 10px;
                width: 43px;
            }

            .disable {
                pointer-events: none;
                background-color: #f0f0f0;
                border: 1px solid #AFAFAF;
            }

            input[type=checkbox] {
                transform: scale(1.3);
            }

            .active .a {
                background-color: chocolate;
            }

            .nav-tabs > li.active > a, .nav-tabs > li.active > a:hover, .nav-tabs > li.active > a:focus {
                background-color: #337AB7;
                border-top: 1px solid black;
                border-right: 1px solid black;
                border-left: 1px solid black;
                color: white;
            }

            .nav-tabs > li > a {
                border-left: 1px solid #949191;
                border-right: 1px solid #949191;
                border-bottom: 1px solid #949191;
                border-top: 1px solid #949191;
                height: 30px;
                padding-top: 0px;
                font-size: 73%;
            }

            .nopadd {
                padding: 0px;
            }

            .hidden5 {
                visibility: hidden;
                padding: 0px;
            }

            .table2 {
                border-collapse: collapse;
                width: 100%;
            }

            .table3 {
                border-collapse: collapse;
                width: 100%;
                table-layout: fixed;
            }

            .table4 {
                border-collapse: collapse;
                width: 100%;
            }

            .vrt-header {
                writing-mode: vertical-lr;
                min-width: 30px; /* for firefox */
            }

            .smolfont {
                font-size: 90%;
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
                text-align: center;
            }

            .headerCell2 {
                height: 65px;
            }

            .font90 {
                font-size: 90%;
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

            .wrapper1 {
                overflow-x: scroll;
                overflow-y: hidden;
            }

            .wrapper2 {
                overflow-x: hidden;
                overflow-y: hidden;
            }

            .wrapper1 {
                height: 20px;
            }

            .div1 {
                height: 20px;
            }

            .div2 {
                overflow: none;
            }

            .tooltipCss {
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
                $("body").mLoading();
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
            $('#image').click(function () {
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

                if (array.length > 1) {
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
                else {
                    maxEl = array[0];
                }

                return maxEl;
            }



            function focusbox(id, row) {
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

                if (page[0].value == 10 || page[0].value == 12) {
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
                var loadstatus = document.getElementsByClassName("loadstatus");

                loadstatus[0].classList.remove('hidden');
                CompareDates(99999);
                autobehave();
                loadstatus[0].classList.add('hidden');
                var txtglow = document.getElementsByClassName("txtglow");


                if (txtglow.length == 0) {
                    clickButton.click();
                    loadstatus[0].classList.remove('hidden');
                }
                else {
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
                            if (result == true) {
                                clickButton.click();
                                loadstatus[0].classList.remove('hidden');
                            }
                        }
                    });
                }
            }

            function print(id) {
                $("body").mLoading();
               <%-- var clickButton1 = document.getElementById("<%= btnExport.ClientID %>");
                var clickButton2 = document.getElementById("<%= btnchewut.ClientID %>");
                var clickButton3 = document.getElementById("<%= btnbehavior.ClientID %>");
                var clickButton4 = document.getElementById("<%= btnExport4.ClientID %>");
                var clickButton5 = document.getElementById("<%= btnExportOriginalScore.ClientID %>");

                var clickButton6 = document.getElementById("<%= btnReadWirte.ClientID %>");
                var clickButton7 = document.getElementById("<%= btnSamattana.ClientID %>");

                var clickButton8 = document.getElementById("<%= btnExportOriginalScorePDF.ClientID %>");

                var clickButton9 = document.getElementById("<%= btnExportPDF.ClientID %>");--%>

                var clickButton10 = document.getElementById("<%= btnExportPDF2.ClientID %>");

                //if (id == "1")
                //    clickButton1.click();
                //if (id == "2")
                //    clickButton2.click();
                //if (id == "3")
                //    clickButton3.click();
                //if (id == "4")
                //    clickButton4.click();
                //if (id == "5")
                //    clickButton5.click();
                //if (id == "6")
                //    clickButton6.click();
                //if (id == "7")
                //    clickButton7.click();
                //if (id == "8")
                //    clickButton8.click();
                //if (id == "9")
                //    clickButton9.click();
                if (id == "10")
                    clickButton10.click();

            }



            function start() {
                $("body").mLoading();
                //var full = window.location.href;
                //var half = full.split('?');
                //var split = half[1].split('&');
                //var year = split[0];
                //var idlv = split[1];
                //var idlv2 = split[2];
                //var term = split[3];
                //var id2 = split[4];
                //var mode = split[5].split('=');
                //var index = split[6].split('=');
                //console.log("start");
                console.log(getUrlParameter("print"));
                print(getUrlParameter("print"));

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

                    window.location = ("Webform2.aspx?" + year + "&" + idlv + "&" + idlv2 + "&" + term + "&" + id + "&mode=EN");
                }
                if (value == 2) {

                    window.location = ("Webform2.aspx?" + year + "&" + idlv + "&" + idlv2 + "&" + term + "&" + id + "&mode=1");
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
            <div class="w3-sidebar w3-bar-block w3-card w3-animate-right full-card" style="display: none; right: 0; z-index: 5; width: 700px; top: 50px; height: 550px;" id="mySidebar">



                <div class="col-xs-12 ">
                    <hr />
                    <div class="centertext">
                        <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M603033") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202030") %> </label>
                    </div>
                </div>
                <div class="col-xs-12 ">
                    <asp:DropDownList ID="DropDownList1" runat="server" Style="width: 100%;" AutoPostBack="false">
                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106065") %>" Value="30" class="grey hidden"></asp:ListItem>
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00307") %>" Value="1" class="grey"></asp:ListItem>
                        <asp:ListItem Text="3 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02861") %>" Value="2" class="grey"></asp:ListItem>
                    </asp:DropDownList>

                </div>

                <div class="col-xs-12 hid">
                    <label>hidden</label>

                </div>

            </div>

            <!-- Page Content -->
            <div class="w3-overlay w3-animate-opacity" onclick="w3_close()" style="cursor: pointer" id="myOverlay"></div>

            <div>
                <div class="w3-button w3-teal w3-xlarge w3-right " style="position: fixed; top: 90px; right: 10px; z-index: 4; border: 1px solid black;" onclick="w3_open()">
                    <asp:Label ID="config" CssClass="" runat="server"> </asp:Label>
                </div>

            </div>

            <script>
                function w3_open() {
                    document.getElementById("mySidebar").style.display = "block";
                    document.getElementById("myOverlay").style.display = "block";
                }
                function w3_close() {
                    document.getElementById("mySidebar").style.display = "none";
                    document.getElementById("myOverlay").style.display = "none";
                }
            </script>


            <div class="row mini--space__top ">
                <div class="col-xs-12">
                    <div class="wrapper-table">
                        
                    </div>
                   <%-- <asp:Button ID="btnExport" CausesValidation="false" class="btn btn-success" runat="server" Text="Export To Excel" />
                    <asp:Button ID="btnExpor--%>tPDF" CausesValidation="false" class="btn btn-success" runat="server" Text="Export To PDF" />
                    <asp:Button ID="btnExportPDF2" CausesValidation="false" class="btn btn-success" runat="server" Text="Export To PDF" />
                </div>
            </div>
          

           

           

          



           

           



        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>



