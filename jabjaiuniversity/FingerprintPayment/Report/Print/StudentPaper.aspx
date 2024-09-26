<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="StudentPaper.aspx.cs" Inherits="FingerprintPayment.Report.Print.StudentPaper" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>

    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="/PreRegister/assets/js/text-fit.js" type="text/javascript"></script>

    <link href="/bootstrap SB2/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />

    <script src="/bootstrap SB2/dist/js/sb-admin-2.js" type="text/javascript"></script>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.5.0/css/all.css" integrity="sha384-B4dIYHKNBt8Bc12p+WXckhzcICo0wtJAoU8YZTY5qE0Id1GSseTk6S+L3BlXeVIU" crossorigin="anonymous">
    <script src="../../TeacherCard/Script/jquery.quickfit.js?v=3"></script>

    <style>
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

        .w3-image {
            max-width: 100%;
            height: auto;
        }

        .margin10 {
            margin-top: 10px !important;
            margin-bottom: 10px !important;
            border-color: black;
        }

        img {
            vertical-align: middle;
        }

        a {
            color: inherit;
        }

        .squared-letters p {
            display: inline-block;
            border: 1px solid darkgray;
            width: 13.5px;
            text-align: center;
            height: 80%;
            padding-top: 2px;
        }

        .font130 {
            font-size: 130%;
        }

        .checkbox {
            border: 1px solid black;
            width: 20px;
            height: 20px;
            display: inline-block;
            margin-right: 4px;
        }

        .circle {
            padding: 0px;
            font-size: 70%;
            width: 20px;
        }

        .righttext {
            position: relative;
            text-align: right;
            white-space: nowrap;
        }

        .leftim {
            text-align: left !important;
        }

        .lefttext {
            position: relative;
            text-align: left;
            white-space: nowrap;
        }

        .bigtxt {
            font-size: 22px;
            font-family: "THSarabun";
            font-weight: bold;
        }

        .bold {
            font-weight: bold;
        }

        .bigtxt2 {
            font-size: 18px;
            font-family: "THSarabun";
        }

        .bigtxt3 {
            font-size: 21px;
            font-family: "THSarabun";
        }


        .pad {
            padding-top: 2px;
            padding-bottom: 2px;
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
            border: none;
        }

        .ddd {
            padding: 0;
            width: 50px;
            text-align: center;
        }

        .righttext2 {
            background-color: #337AB7;
            position: relative;
            text-align: right;
            color: white;
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

        .tdtr {
            border-left: 1px solid #949191;
            border-right: 1px solid #949191;
            border-bottom: 1px solid #949191;
            border-top: 1px solid #949191;
            text-align: center;
            padding-right: 0.2px;
            padding-left: 0.1px;
        }

        u {
            border-bottom: 1px dotted #000;
            text-decoration: none;
        }

        .pad0 {
            padding-left: 0px;
            padding-right: 0px;
            font-family: THSarabun;
            font-size: 95%;
        }

        .pad97 {
            padding-left: 0px;
            padding-right: 0px;
            font-family: THSarabun;
            font-size: 97%;
            height: 22px;
        }

        .know31 {
            height: 31px;
            line-height: 31px;
        }

        .f60 {
            font-size: 60%;
        }

        .f80 {
            font-size: 80%;
        }

        .pad1 {
            padding-left: 0px;
            padding-right: 0px;
            font-size: 100%;
            font-family: THSarabun;
        }

        .pad2 {
            padding-left: 0px;
            padding-right: 0px;
            font-size: 125%;
            font-family: THSarabun;
        }

        .allborder {
            border: 2px solid #000000;
            text-align: center;
        }

        .allborder2 {
            border: 2px solid #000000;
            text-align: center;
            width: 50px;
        }

        .weeknum {
            border: 2px solid #000000;
            text-align: center;
            height: 20.8px;
        }

        .subheader {
            border: 1px solid #000000;
            text-align: center;
            height: 20.8px;
        }

        .rborder {
            border-left: 1px solid #949191;
            border-right: 2px solid #000000;
            border-bottom: 1px solid #949191;
            border-top: 1px solid #949191;
            text-align: center;
            padding-right: 0.2px;
            padding-left: 0.1px;
        }

        .smol {
            border-top: 0px;
            border-left: 0px;
            border-right: 0px;
            border-bottom: 0px;
            width: 11px;
            height: 20.8px;
            padding: 0px;
        }

        .smol2 {
            border-top: 0px;
            border-left: 0px;
            border-right: 0px;
            border-bottom: 0px;
            width: 11px;
            height: 27.9px;
            padding: 0px;
        }

        .lborder {
            border-left: 2px solid #000000;
            border-right: 1px solid #949191;
            border-bottom: 1px solid #949191;
            border-top: 1px solid #949191;
            height: 20.8px;
            text-align: center;
            padding-right: 0.2px;
            padding-left: 0.1px;
        }

        .ltbborder {
            border-left: 2px solid #000000;
            border-right: 1px solid #949191;
            border-bottom: 2px solid #000000;
            border-top: 2px solid #000000;
            height: 20.8px;
            text-align: center;
            padding-right: 0.2px;
            padding-left: 0.1px;
        }

        .lrborder {
            border-left: 2px solid #000000;
            border-right: 2px solid #000000;
            border-bottom: 1px solid #949191;
            border-top: 1px solid #949191;
            height: 20.8px;
            text-align: center;
            padding-right: 0.2px;
            padding-left: 0.1px;
        }

        .tbborder {
            border-left: 1px solid #949191;
            border-right: 1px solid #949191;
            border-bottom: 2px solid #000000;
            border-top: 2px solid #000000;
            height: 20.8px;
            text-align: center;
            padding-right: 0.2px;
            padding-left: 0.1px;
        }

        .tbrborder {
            border-left: 1px solid #949191;
            border-right: 2px solid #000000;
            border-bottom: 2px solid #000000;
            border-top: 2px solid #000000;
            height: 20.8px;
            text-align: center;
            padding-right: 0.2px;
            padding-left: 0.1px;
        }

        .lrbborder {
            border-left: 2px solid #000000;
            border-right: 2px solid #000000;
            border-bottom: 2px solid #000000;
            border-top: 1px solid #949191;
            height: 20.8px;
            text-align: center;
            padding-right: 0.2px;
            padding-left: 0.1px;
        }

        .lbborder {
            border-left: 2px solid #000000;
            border-right: 1px solid #949191;
            border-bottom: 2px solid #000000;
            border-top: 1px solid #949191;
            height: 20.8px;
            text-align: center;
            padding-right: 0.2px;
            padding-left: 0.1px;
        }

        .thinborder {
            border-left: 1px solid #949191;
            border-right: 1px solid #949191;
            border-bottom: 1px solid #949191;
            border-top: 1px solid #949191;
            height: 20.8px;
            text-align: center;
            padding-right: 0.2px;
            padding-left: 0.1px;
        }

        .sidbox {
            width: 80px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
        }

        ใ
        .attendancebox {
            width: 90px;
            height: 18.5px;
            font-size: 90%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
        }

        .attendancebox2 {
            width: 290px;
            height: 18.5px;
            font-size: 90%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
        }

        .attendancebox3 {
            width: 200px;
            height: 18.5px;
            font-size: 90%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
        }

        .attendancebox4 {
            width: 30px;
            height: 18.5px;
            font-size: 90%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
        }

        .setdatebox {
            width: 10px;
            height: 18.5px;
            font-size: 60%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
        }

        .setnumberbox {
            width: 10px;
            height: 18.5px;
            font-size: 60%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
        }

        .paper4box {
            width: 50px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
        }

        .paper5box {
            width: 21px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
        }

        .f130 {
            font-size: 130%;
        }

        .type2paper5box {
            width: 28.7px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
        }

        .paper5boxlarge {
            width: 32px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
        }

        .paper5boxmax {
            width: 21px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
        }

        .gradenamebox {
            width: 150px;
            padding-top: 3px;
            padding-bottom: 3px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: left;
        }

        .paper5box2 {
            width: 95px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: left;
        }

        .paper5box3 {
            width: 175px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: left;
        }

        .T2paper5box2 {
            width: 95px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: left;
        }

        .T2paper5box3 {
            width: 175px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: left;
        }

        .T3paper5box2 {
            width: 95px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: left;
        }

        .T3paper5box3 {
            width: 175px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: left;
        }

        .paper11box {
            width: 21px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
        }

        .paper11boxmax {
            width: 21px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
        }

        .grade11namebox {
            width: 180px;
            padding-top: 3px;
            padding-bottom: 3px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: left;
        }

        .paper11box2 {
            width: 75px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
        }

        .paper11box3 {
            width: 125px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
        }

        .paper4box2 {
            width: 90px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
        }

        .paper4box3 {
            width: 240px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
        }

        .paper6box {
            width: 25px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
        }

        .paper6box2 {
            width: 75px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
        }

        .paper6box3 {
            width: 125px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
        }

        .paper7box {
            width: 25px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
        }

        .paper8box {
            width: 30px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
        }

        .paper9box {
            width: 50px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
        }

        .paper9box2 {
            width: 85px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
        }

        .paper9box3 {
            width: 175px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
        }

        .paper10box {
            width: 100%;
            font-size: 140%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            margin-top: 2px;
            margin-bottom: 2px;
            font-family: "THSarabun";
        }

        .paper10box2 {
            width: 85px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
        }

        .paper10box3 {
            width: 175px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
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

        .namebox {
            width: 155px;
            height: 18.5px;
            font-size: 70%;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
        }

        .rbborder {
            border-left: 1px solid #949191;
            border-right: 2px solid #000000;
            border-bottom: 2px solid #000000;
            border-top: 1px solid #949191;
            height: 20.8px;
            text-align: center;
            padding-right: 0.2px;
            padding-left: 0.1px;
        }

        .bborder {
            border-left: 1px solid #949191;
            border-right: 1px solid #949191;
            border-bottom: 2px solid #000000;
            border-top: 1px solid #949191;
            height: 20.8px;
            text-align: center;
            padding-right: 0.2px;
            padding-left: 0.1px;
            width: 11.45px;
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

        .pageT2 {
            width: 210mm;
            min-height: 297mm;
            padding: 10mm;
            margin: 10mm auto;
            border: 1px;
            border-radius: 5px;
            background: white;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
        }

        .cycle {
            font-size: 70%;
            border-radius: 100%;
            border: solid black 1px;
            padding-right: 0px;
            padding-left: 0px;
            padding-top: 0px;
            padding-bottom: 0px;
            height: 13px;
            text-align: center;
        }

        .pad30L {
            padding-left: 30px;
            padding-right: 0px;
        }

        .lh50 {
            line-height: 50%;
            height: 12px;
            text-align: center;
        }

        .h25 {
            height: 23px;
            font-size: 130%;
            margin-bottom: 0px;
        }

        .pad30 {
            padding-right: 30px;
            padding-left: 30px;
        }

        .pad15 {
            padding-right: 15px;
            padding-left: 15px;
        }

        .lefttext2 {
            text-align: left !important;
        }

        .width20 {
            width: 20%;
        }

        .width15 {
            width: 15%;
        }

        .width35 {
            width: 35%;
        }

        .width100 {
            width: 100%;
        }

        .page {
            width: 210mm;
            min-height: 300mm;
            padding: 10mm;
            margin: 2mm auto;
            border: 1px;
            border-radius: 5px;
            background: white;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
        }

        .subpage {
            padding: 0.5cm;
            border: 5px;
            height: 300mm;
            min-height: 300mm;
            outline: 2cm;
        }

        @page {
            size: A4;
            margin: 0mm;
        }

        @media print {
            /*html, body {
                padding: 0;
                margin: 0;
                width: 210mm;
                height: 297mm;
            }

            .page {
                margin: 0;
                border: initial;
                border-radius: initial;
                width: initial;
                min-height: initial;
                box-shadow: initial;
                background: initial;
                page-break-after: always;
            }*/

            .no-print, .no-print * {
                display: none !important;
            }

            .example-screen {
                display: none;
            }

            .example-print {
                display: block;
            }
        }

        .example-print {
            display: none;
        }

        .wordwrap {
            word-wrap: break-word;
            text-align: left;
            font-weight: normal;
            width: 240px;
            padding-left: 10px;
            padding-right: 10px;
            font-size: 90%;
            font-weight: normal;
        }

        .wordwrap2 {
            width: 50px;
            height: 18.5px;
            font-size: 90%;
            padding: 0px;
            border: none;
            color: black;
            text-align: center;
            font-weight: normal;
        }

        .wordwrap3 {
            width: 450px;
            height: 18.5px;
            font-size: 90%;
            padding: 0px;
            border: none;
            color: black;
            text-align: center;
            font-weight: normal;
        }

        .nopad100 {
            width: 220px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
        }

        .nopad2 {
            width: 110px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
        }

        .botdot {
            border-bottom: 1px dotted black;
        }

        .page8, .page9 {
            font-family: THSarabun;
            font-size: 21px;
        }

            .page8 .padding-0, .page9 .padding-0 {
                padding: 0px;
            }

            .page8 .padding-lr-10, .page9 .padding-lr-10 {
                padding-left: 10px;
                padding-right: 10px;
            }

            .page8 .padding-l-20, .page9 .padding-l-20 {
                padding-left: 20px;
            }

            .page8 .padding-t-10, .page9 .padding-t-10 {
                padding-top: 10px;
            }

            .page8 .padding-t-130, .page9 .padding-t-130 {
                padding-top: 130px;
            }

            .page8 .padding-b-10, .page9 .padding-b-10 {
                padding-bottom: 10px;
            }

            .page8 .font16, .page9 .font16 {
                font-size: 16px;
            }

            .page8 .font18, .page9 .font18 {
                font-size: 18px;
            }

            .page8 .font10, .page9 .font10 {
                font-size: 10px;
            }

            .page8 .font20, .page9 .font20 {
                font-size: 20px;
            }

            .page8 .font22, .page9 .font22 {
                font-size: 22px;
            }

            .page8 .font25, .page9 .font25 {
                font-size: 25px;
            }

            .page8 .border, .page9 .border {
                border: 1px solid #000;
            }

            .page8 i.fa-circle, .page9 i.fa-circle, .page8 i.fa-check-circle, .page9 i.fa-check-circle {
                font-size: 14px;
                margin-top: 5px;
                margin-right: 5px;
            }

            .page8 p, .page9 p {
                margin: 0 0 3px;
            }

                .page8 p.bold, .page9 p.bold {
                    font-weight: bold;
                }

                .page8 p.flex, .page9 p.flex {
                    display: flex;
                }

                .page8 span.underline, .page9 span.underline, .page8 p.underline, .page9 p.underline {
                    text-decoration: underline;
                    font-weight: bold;
                }

            .page8 div.underline, .page9 div.underline {
                text-decoration: underline;
                font-weight: bold;
            }

            .page8 .input-row, .page9 .input-row {
                display: flex;
                white-space: nowrap;
            }

                .page8 .input-row .input-value, .page9 .input-row .input-value {
                    border-bottom: 1px dotted black;
                    height: 23px;
                    text-align: center;
                    margin: 0 5px 0 5px;
                }

            .page8 p.margin-b-n5, .page9 p.margin-b-n5 {
                margin: 0 0 -5px;
            }

            .page8 .padding-l-24, .page9 .padding-l-24 {
                padding-left: 24px;
            }
    </style>

    <style type="text/css" media="print">
        .pagecut {
            page-break-after: always;
            page-break-inside: avoid;
        }
    </style>

    <style type="text/css" media="print">
        .pagecut {
            /*page-break-after: always;
            page-break-inside: avoid;*/
        }
    </style>
    <script type="text/javascript" language="javascript">

        window.onload = startup;

        function start() {

            var tolat = document.getElementsByClassName("tolat");
            var tolon = document.getElementsByClassName("tolon");
            var fromlat = document.getElementsByClassName("fromlat");
            var fromlon = document.getElementsByClassName("fromlon");
            var stdmap = document.getElementsByClassName("stdmap");

            //var timetxt = document.getElementsByClassName("timetxt");
            var time = document.getElementsByClassName("time");
            var branch = document.getElementsByClassName("branch");
            //var branchtxt = document.getElementsByClassName("branchtxt");

            //if (timetxt[0].textContent.length < 1)
            //    time[0].classList.add('hid');
            //if (branchtxt[0].textContent.length < 7)
            //    branch[0].classList.add('hid');

            var pageheader = document.getElementsByClassName("pageheader");
            var page4 = document.getElementsByClassName("page4");
            var hideop = document.getElementsByClassName("hideop");
            var ddlchoice2 = document.getElementsByClassName("ddlchoice2");
            var ddlchoice3 = document.getElementsByClassName("ddlchoice3");

            isPaused = true;
            var full = window.location.href;
            var half = full.split('?');
            var split = half[1].split('&');

            var mode = split[2];
            var mode2 = mode.split('=');

            if (mode2[1] == 1) {
                pageheader[0].textContent = "ใบมอบตัว";
                //page4[0].classList.remove('hidden');
                hideop[0].classList.remove('hidden');
            }
            else if (mode2[1] == 2) {
                ddlchoice2[0].classList.add('hidden');
                ddlchoice3[0].textContent = "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105044") %>";
            }

        }

        function startup() {
            start();

            textfit();
        }

        function changeform() {

            // Hidden all
            $('.page1').addClass('hidden');
            $('.page4').addClass('hidden');
            $('.page5').addClass('hidden');
            $('.page6').addClass('hidden');
            $('.page7').addClass('hidden');
            $('.page8').addClass('hidden');
            $('.page9').addClass('hidden');

            switch ($('.changeform').val()) {
                case '1':
                    $('.page1').removeClass('hidden');
                    $('.page4').removeClass('hidden');
                    break;
                case '2':
                    $('.page5').removeClass('hidden');
                    break;
                case '3':
                    $('.page6').removeClass('hidden');
                    break;
                case '4':
                    $('.page7').removeClass('hidden');
                    break;
                case '5':
                    $('.page8').removeClass('hidden');
                    $('.page9').removeClass('hidden');
                    break;
            }
        }

        $(function () {
            $(".--quickfit").quickfit({ max: 15, min: 12 });
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="w3-overlay w3-animate-opacity" onclick="w3_close()" style="cursor: pointer" id="myOverlay"></div>
        <div class="w3-sidebar w3-bar-block w3-card w3-animate-right full-card no-print" style="display: none; right: 0; z-index: 5; width: 450px; top: 0px; height: 100%; font-size: 20px; overflow-y: hidden" id="mySidebar">
            <div class="col-xs-12 ">
                <div class="col-xs-12 hid">
                    <label>hidden</label>
                </div>
                <div class="centertext" style="font-size: 25px; font-family: THSarabun">
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132881") %></label>
                </div>
            </div>
            <div class="col-xs-12">
                <div class="col-xs-3 righttext" style="padding: 0px; margin-top: 7px; font-family: THSarabun; font-size: 130%;">
                    &nbsp;<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106065") %>
                </div>
                <div class="col-xs-9" style="padding: 5px; font-family: THSarabun; font-size: 130%">

                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="changeform pull-right form-control" Width="90%" AutoPostBack="false" onchange="changeform();">
                        <asp:ListItem Enabled="true" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105043") %>" Value="1"></asp:ListItem>
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105044") %>" class="ddlchoice2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132513") %>" class="ddlchoice3" Value="3"></asp:ListItem>
                        <asp:ListItem Selected="True" Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132882") %>" class="ddlchoice4" Value="4"></asp:ListItem>
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132883") %>" class="ddlchoice5" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-xs-12 hidden">
            </div>
            <div class="col-xs-12 hid">
                <label>hidden</label>
            </div>
        </div>
        <div>
            <div class="w3-button w3-teal w3-xlarge w3-right no-print hideop" style="display: none; position: fixed; top: 20%; right: 10px; z-index: 4; border: 1px solid black; font-family: THSarabun; width: 84px;" onclick="w3_open()"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M303007") %></div>
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
        <div>
            <div class="example-screen w3-button w3-blue w3-right glyphicon glyphicon-print hideop" style="position: fixed; top: 40%; right: 10px; z-index: 4; border: 1px solid black; width: 84px;" onclick="window.print(); return false;">
                <p>
                    <br>
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %>
                </p>
            </div>
        </div>

        <div class="book page7 ">


            <% foreach (var user in PageModel.Users)
                {%>
            <div class="page printableArea pagecut" style="padding: 0px;">
                <div class="subpage">
                    <div class="col-xs-12 pad0">
                        <div class="col-xs-2" style="padding-top: 10px;">
                            <img id="Img3" src="<%= PageModel.Img3 %>" alt="" class="avatar img-responsive centertext" style="margin: 0 auto; display: block; width: 90px;" />
                        </div>
                        <div class="col-xs-8" style="padding: 12px 17px 0px 17px; width: 460px;">
                            <div class="col-xs-12 centertext">
                                <span class="bigtxt3" style="font-size: 30px; font-weight: bold;">
                                    <%= PageModel.Label33 %>
                                </span>
                            </div>
                            <div class="col-xs-12 -righttext text-center" style="margin-top: 7px; padding-right: 5px;">
                                <span class="bigtxt3" style="font-size: 26px; font-weight: bold;">


                                    <%= PageModel.Label13 %>
                                </span>
                            </div>

                        </div>
                        <div class="col-xs-2 pad0" style="width: 168px; padding-left: 55px;">

                            <div class="col-xs-10 pad0" style="margin-top: 21px; /*margin-left: 8px; */position: relative; right: -10px;">
                                <div class="col-xs-8" style="padding: 0px; height: 2.7cm; width: 2.3cm; border: 1px; border-color: #ccc; border-style: dashed; line-height: 2.5cm; text-align: center; font-size: 1.3em; color: #999; bottom: 12px;">
                                    <label style="font-weight: normal; /*line-height: 18px; margin-top: 31px; */">
                                        <img src="<%= user.Image %>" alt="" style="width: 85px; height: 102px" />
                                    </label>
                                </div>
                            </div>
                            <span style="position: absolute; font-size: 17px; bottom: -12px; right: 14px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132034") %> : <%= user.studentID %></span>
                        </div>
                        <div class="col-xs-12 lefttext" style="margin-bottom: 5px;">
                            <hr class="margin10" />
                        </div>
                        <div class="col-xs-12 h25 pad15">
                            <div class="col-xs-2 pad0 lh50 lefttext2" style="">
                                <%= user.page7line1_1 %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad30">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 120px;">
                                <%= user.page7line1_2a %>
                            </div>
                            <div class="col-xs-4 pad0 lh50 botdot" style="width: 240px;">
                                <%= user.page7line1_2b %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 45px;">
                                <%= user.page7line1_2c %>
                            </div>
                            <div class="col-xs-1 pad0 lh50 botdot" style="width: 80px">
                                <%= user.page7line1_2d %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 40px;">
                                <%= user.page7line1_2g %>
                            </div>
                            <div class="col-xs-1 pad0 lh50 botdot" style="width: 75px">
                                <%= user.page7line1_2h %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="">
                                <%= user.page7line1_2e %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 37px">
                                <%= user.page7line1_2f %>
                            </div>
                        </div>
                        <div class="col-xs-12 pad30" style="height: 18px; font-size: 130%;">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 95px;">
                                <%= user.page7line1_3a %>
                            </div>
                            <div class="col-xs-5 pad0 lh50 botdot" style="width: 275px;">
                                <%= user.page7line1_3b %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 80px;">
                                <%= user.page7line1_3c %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 88px;">
                                <%= user.page7line1_3d %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="">
                                <%= user.page7line1_3e %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 99px;">
                                <%= user.page7line1_3f %>
                            </div>
                        </div>
                        <div class="col-xs-12 pad30" style="height: 32px; font-size: 130%;">
                            <div class="col-xs-1 pad0 leftim" style="width: 120px;">
                                <%= user.page7line1_4a %>
                            </div>
                            <div class="col-xs-1 pad0" style="width: 266px; line-height: 75%; margin-top: 3px;">

                                <div class="squared-letters">
                                    <p>
                                        <%= user.page7line1_4txta1 %>
                                    </p>
                                    - 
                                    <p>
                                        <%= user.page7line1_4txta2 %>
                                    </p>
                                    <p>
                                        <%= user.page7line1_4txta3 %>
                                    </p>
                                    <p>
                                        <%= user.page7line1_4txta4 %>
                                    </p>
                                    <p>
                                        <%= user.page7line1_4txta5 %>
                                    </p>
                                    - 
                                    <p>
                                        <%= user.page7line1_4txta6 %>
                                    </p>
                                    <p>
                                        <%= user.page7line1_4txta7 %>
                                    </p>
                                    <p>
                                        <%= user.page7line1_4txta8 %>
                                    </p>
                                    <p>
                                        <%= user.page7line1_4txta9 %>
                                    </p>
                                    <p>
                                        <%= user.page7line1_4txta10 %>
                                    </p>
                                    - 
                                    <p>
                                        <%= user.page7line1_4txta11 %>
                                    </p>
                                    <p>
                                        <%= user.page7line1_4txta12 %>
                                    </p>
                                    - 
                                    <p>
                                        <%= user.page7line1_4txta13 %>
                                    </p>
                                </div>
                            </div>
                            <div class="col-xs-1 pad0" style="width: 100px;">
                                <%= user.page7line1_4b %>
                            </div>
                            <div class="col-xs-1 pad0" style="width: 207px; line-height: 75%; margin-top: 3px;">

                                <div class="squared-letters">

                                    <p>
                                        <%= user.page7line1_4txtb1 %>
                                    </p>
                                    <p>
                                        <%= user.page7line1_4txtb2 %>
                                    </p>
                                    <p>
                                        <%= user.page7line1_4txtb3 %>
                                    </p>
                                    <p>
                                        <%= user.page7line1_4txtb4 %>
                                    </p>
                                    - 
                                    <p>
                                        <%= user.page7line1_4txtb5 %>
                                    </p>
                                    <p>
                                        <%= user.page7line1_4txtb6 %>
                                    </p>
                                    <p>
                                        <%= user.page7line1_4txtb7 %>
                                    </p>
                                    <p>
                                        <%= user.page7line1_4txtb8 %>
                                    </p>
                                    <p>
                                        <%= user.page7line1_4txtb9 %>
                                    </p>
                                    <p>
                                        <%= user.page7line1_4txtb10 %>
                                    </p>
                                    - 
                                    <p>
                                        <%= user.page7line1_4txtb11 %>
                                    </p>
                                </div>

                            </div>
                        </div>
                        <div class="col-xs-6 h25" style="padding-left: 30px; padding-right: 0px;">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 45px;">
                                <%= user.page7line1_5a %>
                            </div>
                            <div class="col-xs-5 pad0 lh50 botdot" style="width: 50px;">
                                <%= user.page7line1_5b %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 35px;">
                                <%= user.page7line1_5c %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 90px">
                                <%= user.page7line1_5d %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="">
                                <%= user.page7line1_5e %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 53px">
                                <%= user.page7line1_5f %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 43px; text-align: right !important;">
                                <%= user.page7line1_5g %>
                            </div>
                        </div>
                        <div class="col-xs-6 h25 pad0">

                            <div class="col-xs-5 pad0 lh50 botdot textfitxx" data-textfit-adjust="1" style="width: 45px;">
                                <%= user.page7line1_5h %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 45px;">
                                <%= user.page7line1_5i %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 45px">
                                <%= user.page7line1_5j %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 45px">
                                <%= user.page7line1_5k %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 45px">
                                <%= user.page7line1_5l %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 25px">
                                <%= user.page7line1_5m %>
                            </div>
                            <div class="col-xs-2 pad0 lh50" style="width: 40px">
                                <%= user.page7line1_5n %>
                            </div>
                            <div class="col-xs-1 pad0 lh50 botdot" style="width: 35px;">
                                <%= user.page7line1_5o %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="">
                                <%= user.page7line1_5p %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad30">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 170px;">
                                <%= user.page7line1_6a %>
                            </div>
                            <div class="col-xs-5 pad0 lh50 botdot" style="width: 68px;">
                                <%= user.page7line1_6b %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 30px">
                                <%= user.page7line1_6c %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 150px">
                                <%= user.page7line1_6d %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 68px">
                                <%= user.page7line1_6e %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 30px;">
                                <%= user.page7line1_6f %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 110px">
                                <%= user.page7line1_6g %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 69px">
                                <%= user.page7line1_6h %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad30">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 45px;">
                                <%= user.page7line1_7a %>
                            </div>
                            <div id="studentCategory1" class="col-xs-1 far  lh50 circle  <%= string.IsNullOrEmpty(user.studentCategory1) ? "fa-circle" : user.studentCategory1%>" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 45px;">
                                <%= user.page7line1_7b %>
                            </div>
                            <div id="studentCategory2" class="col-xs-1 far  lh50 circle <%= string.IsNullOrEmpty(user.studentCategory2) ? "fa-circle" : user.studentCategory2%>" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 70px;">
                                <%= user.page7line1_7c %>
                            </div>
                            <div class="col-xs-1 far fa-circle lh50 circle" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 92px;">
                                <%= user.page7line1_7d %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 154px">
                                <%= user.page7line1_7e %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 75px;">
                                <%= user.page7line1_7f %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 154px">
                                <%= user.page7line1_7g %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad15">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 80px;">
                                <%= user.page7line2_1a %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="">
                                <%= user.page7line2_1b %>
                            </div>
                            <div class="col-xs-5 pad0 lh50 botdot" style="width: 60px;">
                                <%= user.page7line2_1c %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 30px;">
                                <%= user.page7line2_1d %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 30px">
                                <%= user.page7line2_1e %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 30px;">
                                <%= user.page7line2_1f %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 116px">
                                <%= user.page7line2_1g %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 30px;">
                                <%= user.page7line2_1h %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 116px">
                                <%= user.page7line2_1i %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 40px">
                                <%= user.page7line2_1j %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 117px">
                                <%= user.page7line2_1k %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad30">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 35px;">
                                <%= user.page7line2_2a %>
                            </div>
                            <div class="col-xs-5 pad0 lh50 botdot" style="width: 145px;">
                                <%= user.page7line2_2b %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 45px;">
                                <%= user.page7line2_2c %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 145px">
                                <%= user.page7line2_2d %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 75px;">
                                <%= user.page7line2_2e %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 100px">
                                <%= user.page7line2_2f %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 50px;">
                                <%= user.page7line2_2g %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 100px">
                                <%= user.page7line2_2h %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad30">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 75px">
                                <%= user.page7line2_3a %>
                            </div>
                            <div class="col-xs-5 pad0 lh50 botdot" style="width: 200px;">
                                <%= user.page7line2_3b %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 130px;">
                                <%= user.page7line2_3c %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 120px;">
                                <%= user.page7line2_3d %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 50px;">
                                <%= user.page7line2_3e %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 120px;">
                                <%= user.page7line2_3f %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad30">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 130px">
                                <%= user.page7line2_4a %>
                            </div>
                            <div class="col-xs-1 far circle lh50 <%= string.IsNullOrEmpty(user.hometype13) ? "fa-circle" : user.hometype13 %>" id="hometype13" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 85px;">
                                <%= user.page7line2_4b %>
                            </div>
                            <div class="col-xs-1 far circle lh50 <%= string.IsNullOrEmpty(user.hometype23) ? "fa-circle" : user.hometype23 %>" id="hometype23" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="">
                                <%= user.page7line2_4c %>
                            </div>
                            <div class="col-xs-1 far circle lh50 <%= string.IsNullOrEmpty(user.hometype33) ? "fa-circle" : user.hometype33 %>" id="hometype33" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="">
                                <%= user.page7line2_4d %>
                            </div>
                            <div class="col-xs-1 far circle lh50 <%= string.IsNullOrEmpty(user.hometype43) ? "fa-circle" : user.hometype43 %>" id="hometype43" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 90px;">
                                <%= user.page7line2_4e %>
                            </div>
                            <div class="col-xs-1 far circle lh50 <%= string.IsNullOrEmpty(user.hometype53) ? "fa-circle" : user.hometype53 %>" id="hometype53" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 80px;">
                                <%= user.page7line2_4f %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad30">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 73px;">
                                <%= user.page7line2_5a %>
                            </div>
                            <div class="col-xs-5 pad0 lh50 botdot" style="width: 202px;">
                                <%= user.page7line2_5b %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 30px;">
                                <%= user.page7line2_5c %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 152.5px">
                                <%= user.page7line2_5d %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 85px;">
                                <%= user.page7line2_5e %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 152.5px">
                                <%= user.page7line2_5f %>
                            </div>
                        </div>

                        <div class="col-xs-12 h25 pad15">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 135px">
                                <%= user.page7line3_1a %>
                            </div>

                            <div class="col-xs-1 pad0 lh50" style="">
                                <%= user.page7line3_1c %>
                            </div>
                            <div class="col-xs-5 pad0 lh50 botdot" style="width: 80px;">
                                <%= user.page7line3_1d %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 30px">
                                <%= user.page7line3_1e %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 50px">
                                <%= user.page7line3_1f %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 35px">
                                <%= user.page7line3_1g %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 136px">
                                <%= user.page7line3_1h %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 35px;">
                                <%= user.page7line3_1i %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 136px;">
                                <%= user.page7line3_1j %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad30">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 33px;">
                                <%= user.page7line3_2a %>
                            </div>
                            <div class="col-xs-1 pad0 lh50 botdot" style="width: 103px;">
                                <%= user.page7line3_2b %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 38px;">
                                <%= user.page7line3_2c %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 103px">
                                <%= user.page7line3_2d %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 40px">
                                <%= user.page7line3_2e %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 108px">
                                <%= user.page7line3_2f %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 70px;">
                                <%= user.page7line3_2g %>
                            </div>
                            <div class="col-xs-1 pad0 lh50 botdot" style="width: 60px;">
                                <%= user.page7line3_2h %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 50px;">
                                <%= user.page7line3_2i %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 90px;">
                                <%= user.page7line3_2j %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad30" style="display: none;">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 113px;">
                                <%= user.page7line3_3a %>
                            </div>
                            <div class="col-xs-1 pad0 lh50 botdot" style="width: 132px;">
                                <%= user.page7line3_3b %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 40px;">
                                <%= user.page7line3_3c %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 110px">
                                <%= user.page7line3_3d %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 40px;">
                                <%= user.page7line3_3e %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 110px">
                                <%= user.page7line3_3f %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 40px;">
                                <%= user.page7line3_3g %>
                            </div>
                            <div class="col-xs-1 pad0 lh50 botdot" style="width: 110px;">
                                <%= user.page7line3_3h %>
                            </div>

                        </div>

                        <div class="col-xs-12 h25 pad15">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 120px;">
                                <%= user.page7line4_1a %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad30">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 90px;">
                                <%= user.page7line4_2a %>
                            </div>
                            <div class="col-xs-5 pad0 lh50 botdot" style="width: 230px;">
                                <%= user.page7line4_2b %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 65px">
                                <%= user.page7line4_2c %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 184px">
                                <%= user.page7line4_2d %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 35px;">
                                <%= user.page7line4_2e %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 75px">
                                <%= user.page7line4_2f %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 15px;">
                                <%= user.page7line4_2g %>
                            </div>

                        </div>
                        <div class="col-xs-12 h25 pad30" style="height: 18px; font-size: 130%;">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 115px">
                                <%= user.page7line4_3a %>
                            </div>
                            <div class="col-xs-5 pad0 lh50 botdot" style="width: 206px;">
                                <%= user.page7line4_3b %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 45px;">
                                <%= user.page7line4_3c %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 79px">
                                <%= user.page7line4_3d %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 45px;">
                                <%= user.page7line4_3e %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 79px">
                                <%= user.page7line4_3f %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 45px;">
                                <%= user.page7line4_3g %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 81px">
                                <%= user.page7line4_3h %>
                            </div>
                        </div>
                        <div class="col-xs-12 pad30" style="height: 32px; font-size: 130%;">
                            <div class="col-xs-1 pad0 leftim" style="width: 120px;">
                                <%= user.page7line4_4a %>
                            </div>
                            <div class="col-xs-1 pad0" style="width: 266px; line-height: 75%; margin-top: 3px;">

                                <div class="squared-letters">
                                    <p>
                                        <%= user.page7line4_4_1 %>
                                    </p>
                                    - 
                                    <p>
                                        <%= user.page7line4_4_2 %>
                                    </p>
                                    <p>
                                        <%= user.page7line4_4_3 %>
                                    </p>
                                    <p>
                                        <%= user.page7line4_4_4 %>
                                    </p>
                                    <p>
                                        <%= user.page7line4_4_5 %>
                                    </p>
                                    - 
                                    <p>
                                        <%= user.page7line4_4_6 %>
                                    </p>
                                    <p>
                                        <%= user.page7line4_4_7 %>
                                    </p>
                                    <p>
                                        <%= user.page7line4_4_8 %>
                                    </p>
                                    <p>
                                        <%= user.page7line4_4_9 %>
                                    </p>
                                    <p>
                                        <%= user.page7line4_4_10 %>
                                    </p>
                                    - 
                                    <p>
                                        <%= user.page7line4_4_11 %>
                                    </p>
                                    <p>
                                        <%= user.page7line4_4_12 %>
                                    </p>
                                    - 
                                    <p>
                                        <%= user.page7line4_4_13 %>
                                    </p>
                                </div>

                            </div>
                            <div class="col-xs-1 pad0" style="width: 35px;">
                                <%= user.page7line4_4b %>
                            </div>
                            <div class="col-xs-2 pad0  botdot centertext" style="width: 124px; height: 20px; text-align: center;">
                                <%= user.page7line4_4c %>
                            </div>
                            <div class="col-xs-1 pad0" style="width: 80px; text-align: center;">
                                <%= user.page7line4_4d %>
                            </div>
                            <div class="col-xs-2 pad0 botdot centertext" style="width: 70px; height: 20px;">
                                <%= user.page7line4_4e %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad30">
                            <div class="col-xs-1 pad0 lh50 leftim" style="">
                                <%= user.page7line4_5a %>
                            </div>
                            <div class="col-xs-1 far  lh50 circle  <%= string.IsNullOrEmpty(user.fatherincome13) ? "fa-circle" : user.fatherincome13 %>" id="fatherincome13" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 120px;">
                                <%= user.page7line4_5b %>
                            </div>
                            <div class="col-xs-1 far lh50 circle <%= string.IsNullOrEmpty(user.fatherincome23) ? "fa-circle" : user.fatherincome23 %>" id="fatherincome23" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 140px;">
                                <%= user.page7line4_5c %>
                            </div>
                            <div class="col-xs-1 far lh50 circle <%= string.IsNullOrEmpty(user.fatherincome33) ? "fa-circle" : user.fatherincome33 %>" id="fatherincome33" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 120px;">
                                <%= user.page7line4_5d %>
                            </div>
                            <div class="col-xs-1 far lh50 circle <%= string.IsNullOrEmpty(user.fatherincome43) ? "fa-circle" : user.fatherincome43 %>" id="fatherincome43" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="">
                                <%= user.page7line4_5e %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad30">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 80px;">
                                <%= user.page7line4_6a %>
                            </div>
                            <div class="col-xs-5 pad0 lh50 botdot --quickfit <%= user.educate13 %>" id="educate13" style="width: 82px;"><%= user.page7line4_6b  %></div>
                            <div class="col-xs-1 pad0 lh50" style="width: 80px;">
                                <%= user.page7line4_6c %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 120px; font-size: 80% !important">
                                <%= user.page7line4_6d %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 93px;">
                                <%= user.page7line4_6e %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 80px">
                                <%= user.page7line4_6f %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 80px;">
                                <%= user.page7line4_6g %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 80px">
                                <%= user.page7line4_6h %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad30">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 100px;">
                                <%= user.page7line4_7a %>
                            </div>
                            <div class="col-xs-5 pad0 lh50 botdot" style="width: 225px;">
                                <%= user.page7line4_7b %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="">
                                <%= user.page7line4_7c %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 185px">
                                <%= user.page7line4_7d %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 35px;">
                                <%= user.page7line4_7e %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 75px">
                                <%= user.page7line4_7f %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 15px;">
                                <%= user.page7line4_7g %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad30">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 125px;">
                                <%= user.page7line4_8a %>
                            </div>
                            <div class="col-xs-5 pad0 lh50 botdot" style="width: 200px;">
                                <%= user.page7line4_8b %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 45px;">
                                <%= user.page7line4_8c %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 75px">
                                <%= user.page7line4_8d %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 45px;">
                                <%= user.page7line4_8e %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 80px">
                                <%= user.page7line4_8f %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 45px;">
                                <%= user.page7line4_8g %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 80px">
                                <%= user.page7line4_8h %>
                            </div>
                        </div>
                        <div class="col-xs-12 pad30" style="height: 32px; font-size: 130%;">
                            <div class="col-xs-1 pad0 leftim" style="width: 120px;">
                                <%= user.page7line4_9a %>
                            </div>
                            <div class="col-xs-1 pad0" style="width: 266px; line-height: 75%; margin-top: 3px;">

                                <div class="squared-letters">
                                    <p>
                                        <%= user.page7line4_9_1 %>
                                    </p>
                                    - 
                                    <p>
                                        <%= user.page7line4_9_2 %>
                                    </p>
                                    <p>
                                        <%= user.page7line4_9_3 %>
                                    </p>
                                    <p>
                                        <%= user.page7line4_9_4 %>
                                    </p>
                                    <p>
                                        <%= user.page7line4_9_5 %>
                                    </p>
                                    - 
                                    <p>
                                        <%= user.page7line4_9_6 %>
                                    </p>
                                    <p>
                                        <%= user.page7line4_9_7 %>
                                    </p>
                                    <p>
                                        <%= user.page7line4_9_8 %>
                                    </p>
                                    <p>
                                        <%= user.page7line4_9_9 %>
                                    </p>
                                    <p>
                                        <%= user.page7line4_9_10 %>
                                    </p>
                                    - 
                                    <p>
                                        <%= user.page7line4_9_11 %>
                                    </p>
                                    <p>
                                        <%= user.page7line4_9_12 %>
                                    </p>
                                    - 
                                    <p>
                                        <%= user.page7line4_9_13 %>
                                    </p>
                                </div>
                            </div>
                            <div class="col-xs-1 pad0" style="width: 35px;">
                                <%= user.page7line4_9b %>
                            </div>
                            <div class="col-xs-2 pad0 botdot centertext" style="width: 124px; height: 20px; text-align: center;">
                                <%= user.page7line4_9c %>
                            </div>
                            <div class="col-xs-1 pad0" style="width: 80px; text-align: center;">
                                <%= user.page7line4_9d %>
                            </div>
                            <div class="col-xs-2 pad0 botdot centertext" style="width: 70px; height: 20px;">
                                <%= user.page7line4_9e %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad30">
                            <div class="col-xs-1 pad0 lh50 leftim" style="">
                                <%= user.page7line4_10a %>
                            </div>
                            <div class="col-xs-1 far <%= string.IsNullOrEmpty(user.motherincome13) ? "fa-circle" : user.motherincome13 %> lh50 circle" id="motherincome13" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 120px;">
                                <%= user.page7line4_10b %>
                            </div>
                            <div class="col-xs-1 far <%= string.IsNullOrEmpty(user.motherincome23) ? "fa-circle" : user.motherincome23 %> lh50 circle" id="motherincome23" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 140px;">
                                <%= user.page7line4_10c %>
                            </div>
                            <div class="col-xs-1 far <%= string.IsNullOrEmpty(user.motherincome33) ? "fa-circle" : user.motherincome33 %> lh50 circle" id="motherincome33" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 120px;">
                                <%= user.page7line4_10d %>
                            </div>
                            <div class="col-xs-1 far <%= string.IsNullOrEmpty(user.motherincome43) ? "fa-circle" : user.motherincome43 %> lh50 circle" id="motherincome43" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="">
                                <%= user.page7line4_10e %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad30">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 80px;">
                                <%= user.page7line4_11a %>
                            </div>
                            <div class="col-xs-5 pad0 lh50 botdot  --quickfit <%= user.educate23 %>" id="educate23" style="width: 82px;">
                                <%= user.page7line4_11b %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 80px;">
                                <%= user.page7line4_11c %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 120px; font-size: 80% !important">
                                <%= user.page7line4_11d %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 93px;">
                                <%= user.page7line4_11e %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 80px">
                                <%= user.page7line4_11f %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 80px;">
                                <%= user.page7line4_11g %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 80px">
                                <%= user.page7line4_11h %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad15">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 105px">
                                <%= user.page7line5_1a %>
                            </div>
                            <div class="col-xs-1 far  lh50 circle <%= string.IsNullOrEmpty(user.famstatus13) ? "fa-circle" : user.famstatus13 %>" id="famstatus13" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 120px;">
                                <%= user.page7line5_1b %>
                            </div>
                            <div class="col-xs-1 far lh50 circle <%= string.IsNullOrEmpty(user.famstatus23) ? "fa-circle" : user.famstatus23 %>" id="famstatus23" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 120px;">
                                <%= user.page7line5_1c %>
                            </div>
                            <div class="col-xs-1 far lh50 circle <%= string.IsNullOrEmpty(user.famstatus33) ? "fa-circle" : user.famstatus33 %>" id="famstatus33" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 120px;">
                                <%= user.page7line5_1d %>
                            </div>
                            <div class="col-xs-1 far lh50 circle <%= string.IsNullOrEmpty(user.famstatus43) ? "fa-circle" : user.famstatus43 %> " id="famstatus43" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 90px;">
                                <%= user.page7line5_1e %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad30" style="margin-left: 93px">
                            <div class="col-xs-1 far  lh50 leftim circle  <%= string.IsNullOrEmpty(user.famstatus53) ? "fa-circle" : user.famstatus53 %>" id="famstatus53" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 100px;">
                                <%= user.page7line5_2a %>
                            </div>
                            <div class="col-xs-1 far  lh50 circle  <%= string.IsNullOrEmpty(user.famstatus63) ? "fa-circle" : user.famstatus63 %>" id="famstatus63" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 130px;">
                                <%= user.page7line5_2b %>
                            </div>
                            <div class="col-xs-1 far lh50 circle <%= string.IsNullOrEmpty(user.famstatus73) ? "fa-circle" : user.famstatus73 %>" id="famstatus73" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 120px;">
                                <%= user.page7line5_2c %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad15">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 135px">
                                <%= user.page7line6_1a %>
                            </div>
                            <div class="col-xs-1 far  lh50 circle <%= string.IsNullOrEmpty(user.famrelate13) ? "fa-circle" : user.famrelate13 %>" id="famrelate13" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 30px">
                                <%= user.page7line6_1c %>
                            </div>
                            <div class="col-xs-1 far  lh50 circle <%= string.IsNullOrEmpty(user.famrelate23) ? "fa-circle" : user.famrelate23 %>" id="famrelate23" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 40px">
                                <%= user.page7line6_1d %>
                            </div>
                            <div class="col-xs-1 far lh50 circle <%= string.IsNullOrEmpty(user.famrelate33) ? "fa-circle" : user.famrelate33 %>" id="famrelate33" style="">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 40px">
                                <%= user.page7line6_1e %>
                            </div>
                            <div class="col-xs-5 pad0 lh50 " style="width: 30px;">
                                <%= user.page7line6_1f %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 100px; display: none;">
                                <%= user.page7line6_1g %>
                            </div>
                            <div class="col-xs-1 far lh50 circle <%= string.IsNullOrEmpty(user.requestmoney13) ? "fa-circle" : user.requestmoney13 %>" id="requestmoney13" style="display: none;">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 40px; display: none;">
                                <%= user.page7line6_1h %>
                            </div>
                            <div class="col-xs-1 far lh50 circle <%= string.IsNullOrEmpty(user.requestmoney23) ? "fa-circle" : user.requestmoney23 %>" id="requestmoney23" style="display: none;">
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 55px; display: none;">
                                <%= user.page7line6_1i %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad30">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 115px">
                                <%= user.page7line6_2a %>
                            </div>
                            <div class="col-xs-5 pad0 lh50 botdot" style="width: 210px;">
                                <%= user.page7line6_2b %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 65px">
                                <%= user.page7line6_2c %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 185px">
                                <%= user.page7line6_2d %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 35px">
                                <%= user.page7line6_2e %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 70px">
                                <%= user.page7line6_2f %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 15px">
                                <%= user.page7line6_2g %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad30" style="height: 18px; font-size: 130%;">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 140px; display: none;">
                                <%= user.page7line6_3a %>
                            </div>
                            <div class="col-xs-5 pad0 lh50 botdot" style="width: 186px; display: none;">
                                <%= user.page7line6_3b %>
                            </div>
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 45px">
                                <%= user.page7line6_3c %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 75px">
                                <%= user.page7line6_3d %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 45px">
                                <%= user.page7line6_3e %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 80px">
                                <%= user.page7line6_3f %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 45px">
                                <%= user.page7line6_3g %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 79px">
                                <%= user.page7line6_3h %>
                            </div>
                        </div>
                        <div class="col-xs-12 pad30" style="height: 32px; font-size: 130%;">
                            <div class="col-xs-1 pad0 leftim" style="width: 120px;">
                                <%= user.page7line6_4a %>
                            </div>
                            <div class="col-xs-1 pad0" style="width: 266px; line-height: 75%; margin-top: 3px;">

                                <div class="squared-letters">
                                    <p>
                                        <%= user.page7line6_4_1 %>
                                    </p>
                                    - 
                                    <p>
                                        <%= user.page7line6_4_2 %>
                                    </p>
                                    <p>
                                        <%= user.page7line6_4_3 %>
                                    </p>
                                    <p>
                                        <%= user.page7line6_4_4 %>
                                    </p>
                                    <p>
                                        <%= user.page7line6_4_5 %>
                                    </p>
                                    - 
                                    <p>
                                        <%= user.page7line6_4_6 %>
                                    </p>
                                    <p>
                                        <%= user.page7line6_4_7 %>
                                    </p>
                                    <p>
                                        <%= user.page7line6_4_8 %>
                                    </p>
                                    <p>
                                        <%= user.page7line6_4_9 %>
                                    </p>
                                    <p>
                                        <%= user.page7line6_4_10 %>
                                    </p>
                                    - 
                                    <p>
                                        <%= user.page7line6_4_11 %>
                                    </p>
                                    <p>
                                        <%= user.page7line6_4_12 %>
                                    </p>
                                    - 
                                    <p>
                                        <%= user.page7line6_4_13 %>
                                    </p>
                                </div>
                            </div>
                            <div class="col-xs-1 pad0" style="width: 35px;">
                                <%= user.page7line6_4b %>
                            </div>
                            <div class="col-xs-2 pad0 botdot centertext" style="width: 124px; height: 20px; text-align: center;">
                                <%= user.page7line6_4c %>
                            </div>
                            <div class="col-xs-1 pad0" style="width: 80px; text-align: center;">
                                <%= user.page7line6_4d %>
                            </div>
                            <div class="col-xs-2 pad0 botdot centertext" style="width: 70px; height: 20px;">
                                <%= user.page7line6_4e %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad30">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 80px">
                                <%= user.page7line6_5a %>
                            </div>
                            <div class="col-xs-5 pad0 lh50 botdot <%=  user.educate33 %>" id="educate33" style="width: 82px;">
                                <%= user.page7line6_5b %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 80px;">
                                <%=user.page7line6_5c %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 120px">
                                <%=user.page7line6_5d %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 93px;">
                                <%=user.page7line6_5e %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 80px">
                                <%=user.page7line6_5f %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 80px">
                                <%=user.page7line6_5g %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 80px">
                                <%=user.page7line6_5h %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad15">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 170px">
                                <%=user.page7line7_1a %>
                            </div>
                            <div class="col-xs-5 pad0 lh50 botdot" style="width: 209px;">
                                <%=user.page7line7_1b %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 40px;">
                                <%=user.page7line7_1c %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 125px">
                                <%=user.page7line7_1d %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 40px;">
                                <%=user.page7line7_1e %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 126px">
                                <%=user.page7line7_1f %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad30">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 40px">
                                <%=user.page7line7_2a %>
                            </div>
                            <div class="col-xs-5 pad0 lh50 botdot" style="width: 120px;">
                                <%=user.page7line7_2b %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 95px;">
                                <%=user.page7line7_2c %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 125px">
                                <%=user.page7line7_2d %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 70px;">
                                <%=user.page7line7_2e %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 55px">
                                <%=user.page7line7_2f %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="">
                                <%=user.page7line7_2g %>
                            </div>
                            <div class="col-xs-2 pad0 lh50 botdot" style="width: 132px">
                                <%=user.page7line7_2h %>
                            </div>
                        </div>
                        <div class="col-xs-6 h25 pad30  <%= (mode != "1" ? "hidden" : "") %>" style="margin-top: 14px; padding-left: 90px;">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 30px;">
                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211049") %></span>
                            </div>
                            <div class="col-xs-1 pad0 lh50 botdot" style="width: 167px;">
                                <span></span>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 60px;">
                                <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %></span>
                            </div>
                        </div>
                        <div class="col-xs-6 h25 pad30 <%= (mode != "1" ? "hidden" : "") %>" style="margin-top: 14px; padding-left: 45px;">
                            <div class="col-xs-1 pad0 lh50 leftim" style="width: 30px;">
                                <%=user.page7foot1_1a %>
                            </div>
                            <div class="col-xs-1 pad0 lh50 botdot" style="width: 167px;">
                                <%=user.page7foot1_1b %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 60px;">
                                <%=user.page7foot1_1c %>
                            </div>
                        </div>
                        <div class="col-xs-6 h25 pad30 hidden " style="padding-left: 94px;">
                            <div class="col-xs-1 pad0 lh50" style="width: 25px;">
                                <%=user.page7foot1s_2a %>
                            </div>
                            <div class="col-xs-1 pad0 lh50 botdot" style="width: 40px;">
                                <%=user.page7foot1s_2b %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 30px">
                                <%=user.page7foot1s_2c %>
                            </div>
                            <div class="col-xs-1 pad0 lh50 botdot" style="width: 80px;">
                                <%=user.page7foot1s_2d %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 25px">
                                <%=user.page7foot1s_2e %>
                            </div>
                            <div class="col-xs-1 pad0 lh50 botdot" style="width: 50px;">
                                <%=user.page7foot1s_2f %>
                            </div>
                        </div>
                        <div class="col-xs-6 h25 pad30 hidden" style="padding-left: 45px;">
                            <div class="col-xs-1 pad0 lh50" style="width: 25px;">
                                <%=user.page7foot1_2a %>
                            </div>
                            <div class="col-xs-1 pad0 lh50 botdot" style="width: 40px;">
                                <%=user.page7foot1_2b %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 30px">
                                <%=user.page7foot1_2c %>
                            </div>
                            <div class="col-xs-1 pad0 lh50 botdot" style="width: 80px;">
                                <%=user.page7foot1_2d %>
                            </div>
                            <div class="col-xs-1 pad0 lh50" style="width: 25px">
                                <%=user.page7foot1_2e %>
                            </div>
                            <div class="col-xs-1 pad0 lh50 botdot" style="width: 50px;">
                                <%=user.page7foot1_2f %>
                            </div>
                        </div>
                        <div class="col-xs-12 h25 pad0  <%= (mode != "1" ? "hidden" : "") %>" style="padding: 0px 30px; font-size: 120%">
                            <div class="row" style="border: 1px solid black; padding: 4px 0px;">
                                <div class="col-xs-12 text-center" style="margin-bottom: -5px;">
                                    <b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132892") %></b>
                                </div>
                                <div class="col-xs-6 ">
                                    <span class="col-xs-12" style="margin-bottom: 4px;">1. งานทะเบียน</span>
                                    <div class="col-xs-1 far fa-circle lh50 leftim circle" style="margin-left: 26px;"></div>
                                    <span class="col-xs-3 pad0 lh50 leftim"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132893") %></span>
                                    <div class="col-xs-1 far fa-circle lh50 leftim circle" style=""></div>
                                    <span class="col-xs-4 pad0 lh50 leftim" style="width: 94px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132894") %></span>
                                    <div class="col-xs-2 pad0 lh50 botdot leftim" style="width: 85px; font-size: 80% !important"></div>
                                    <span class="col-xs-3 pad0 lh50 " style="text-align: right; margin-top: 6px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211049") %></span>
                                    <span class="col-xs-6 pad0 lh50 botdot leftim" style="margin-top: 6px;"></span>
                                    <span class="col-xs-12 text-center" style="margin-top: 0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132895") %></span>
                                </div>
                                <div class="col-xs-6">
                                    <span class="col-xs-12" style="margin-bottom: 4px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132896") %></span>
                                    <div class="col-xs-1 far fa-circle lh50 leftim circle" style="margin-left: 26px;"></div>
                                    <span class="col-xs-3 pad0 lh50 leftim"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132897") %></span>
                                    <div class="col-xs-1 far fa-circle lh50 leftim circle" style=""></div>
                                    <span class="col-xs-4 pad0 lh50 leftim" style="width: 80px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132898") %></span>
                                    <div class="col-xs-2 pad0 lh50 botdot leftim" style="width: 85px; font-size: 80% !important"></div>
                                    <span class="col-xs-3 pad0 lh50 " style="text-align: right; margin-top: 6px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211049") %></span>
                                    <span class="col-xs-6 pad0 lh50 botdot leftim" style="margin-top: 6px;"></span>
                                    <span class="col-xs-12 text-center" style="margin-top: 0px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132866") %></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%    } %>
        </div>


    </form>
</body>
</html>

