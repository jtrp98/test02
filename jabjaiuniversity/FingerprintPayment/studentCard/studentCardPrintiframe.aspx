<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="studentCardPrintiframe.aspx.cs" Inherits="FingerprintPayment.studentCard.studentCardPrintiframe" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <%--    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>--%>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <link href="/bootstrap SB2/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
    <script src="/bootstrap SB2/dist/js/sb-admin-2.js" type="text/javascript"></script>

    <%--    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>--%>

    <script src="../TeacherCard/Script/jquery.quickfit.js?v=3"></script>
    <style type="text/css">
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

        .w3-image {
            max-width: 100%;
            height: auto;
        }

        .breakall {
            word-break: break-all
        }

        img {
            vertical-align: middle;
        }

        a {
            color: inherit;
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

        .righttext {
            position: relative;
            text-align: right;
        }

        .lefttext {
            position: relative;
            text-align: left;
        }

        .bigtxt {
            font-size: 20px;
        }

        .wid100 {
            width: 100%;
        }

        .box100left {
            width: 100%;
            text-align: left;
            padding-left: 6px;
            font-size: 130%;
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
            text-align: center !important;
        }

        .wsnormal {
            white-space: normal;
        }

        .wsnowrap {
            white-space: nowrap;
        }

        .pad3 {
            padding-left: 3px;
            padding-right: 3px;
        }

        .pad0 {
            padding: 0px !important;
        }

        .h24 {
            height: 20px;
        }

        .h40 {
            height: 30px;
        }

        .hid {
            visibility: hidden;
            border: none;
        }

        .bold {
            font-weight: bold;
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

        .allborder {
            border: 2px solid #000000;
            text-align: center;
        }

        .box100 {
            width: 100%;
            text-align: center;
            font-size: 130%;
        }

        .sarabun {
            font-family: "THSarabun" !important;
            font-size: 100%;
        }

        .txtarea {
            line-height: 30px;
            padding-top: 10px !important;
            border: solid black 1px;
            padding-left: 5px !important;
            padding-right: 5px !important;
            overflow-y: hidden;
        }

        .allborder2 {
            border: 2px solid #000000;
            text-align: center;
            width: 50px;
        }

        .smallbox1 {
            width: 70px !important;
            text-align: center !important;
            font-size: 130%;
            height: 21px;
        }

        .smallbox2 {
            width: 290px !important;
            text-align: left;
            font-size: 130%;
            padding-left: 6px;
            height: 21px;
        }

        .smallbox3 {
            width: 100%;
            text-align: center !important;
            font-size: 130%;
            height: 21px;
        }

        .smallbox4 {
            width: 595px !important;
            text-align: center !important;
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

        .namebox {
            width: 260px;
            height: 18.5px;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: left;
            white-space: pre !important;
        }

        .gradebox {
            width: 28px;
            height: 18.5px;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
        }

        .hiddenbox {
            height: 21px;
        }

        .width90 {
            width: 90%;
        }

        .width85 {
            width: 85%;
        }

        .width93 {
            width: 93%;
        }

        .creditbox {
            width: 28px;
            height: 18.5px;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
        }

        .scorebox {
            width: 28px;
            height: 18.5px;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
        }

        .namebox2 {
            width: 260px;
            height: 18.5px;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
            font-weight: bold;
            white-space: pre !important;
        }

        .gradebox2 {
            width: 28px;
            height: 18.5px;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
            font-weight: bold;
        }

        .creditbox2 {
            width: 28px;
            height: 18.5px;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
            font-weight: bold;
        }

        .scorebox2 {
            width: 28px;
            height: 18.5px;
            padding: 0px;
            background: rgba(0,0,0,0);
            border: none;
            color: black;
            background-color: white;
            text-align: center;
            font-weight: bold;
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

        .planName2 {
            font-weight: bold;
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

        .page {
            width: 8.5cm;
            min-height: 5.4cm;
            padding: 10mm;
            border: 1px;
            border-radius: 5px;
            background: white;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
        }

        .cycle {
            font-size: 90%;
            border-radius: 100%;
            border: solid red 1px;
            padding-right: 0px;
            padding-left: 0px;
            padding-top: 0px;
            padding-bottom: 0px;
            height: 13px;
            text-align: center;
            color: red;
        }

        .subpage {
            padding: 0px;
            border: 5px;
            height: 5.4cm;
            outline: 2cm;
            width: 8.5cm;
        }

        .bgimage {
            height: 5.5cm !important;
            width: 8.6cm !important;
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

        /*@page {
            width: 8.5cm;
            min-height: 5.4cm;
        }*/


        @media print {
            html, body {
                width: 8.5cm;
                min-height: 5.4cm;
            }

            .no-print, .no-print * {
                display: none !important;
            }

            .page {
                width: 8.5cm;
                min-height: 5.4cm;
                size: landscape
            }

            .example-screen {
                display: none;
            }

            .example-print {
                display: block;
            }

            .FwhiteOnly12 span {
                color: #fff !important;
                -webkit-print-color-adjust: exact;
            }

            .FblackOnly12 span {
                color: #000 !important;
                -webkit-print-color-adjust: exact;
            }

            .FblueOnly12 span {
                color: #004DAA !important;
                -webkit-print-color-adjust: exact;
            }

            @media print and (-webkit-min-device-pixel-ratio:0) {
                .Fwhite {
                    color: #fff !important;
                    -webkit-print-color-adjust: exact;
                }

                .Fblack {
                    color: #000 !important;
                    -webkit-print-color-adjust: exact;
                }

                .Fblue {
                    color: #004DAA !important;
                    -webkit-print-color-adjust: exact;
                }


                .FwhiteOnly12 span {
                    color: #fff !important;
                    -webkit-print-color-adjust: exact;
                }

                .FblackOnly12 span {
                    color: #000 !important;
                    -webkit-print-color-adjust: exact;
                }

                .FblueOnly12 span {
                    color: #004DAA !important;
                    -webkit-print-color-adjust: exact;
                }
            }
        }

        .example-print {
            display: none;
        }

        .Fwhite {
            color: #fff !important;
        }

        .Fblack {
            color: #000 !important;
        }

        .Fblue {
            color: #004DAA !important;
        }


        .FwhiteOnly12 span {
            color: #fff !important;
        }

        .FblackOnly12 span {
            color: #000 !important;
        }

        .FblueOnly12 span {
            color: #004DAA !important;
        }



        .PositionRelative {
            position: relative;
        }

        .PositionInConner {
            position: absolute;
            font-family: "THSarabun" !important;
            font-weight: bold;
        }

        .resizeableFont {
            white-space: nowrap;
        }

        .--fix-pl1 {
            padding-left: 8px !important;
        }

        .--fix-pl2 {
            padding-left: 2px !important;
        }

        .--fix-pl3 {
            padding-left: 40px !important;
        }

        .--fix-pl4 {
            padding-left: 50px !important;
        }

        .--fix-pl5 {
            padding-left: 10px !important;
        }

        .--fix-font10 {
            font-size: 10px !important;
        }

        .--fix-bottom {
            /*  position: absolute;
            bottom: 2px;*/
            /* vertical-align: middle;*/
            /* display: table-cell;
  vertical-align: bottom;*/
            display: flex;
            flex-wrap: wrap;
            align-items: flex-end;
            height: 18px;
        }

        #meassure {
            display: none;
        }
    </style>

    <style type="text/css" media="print">
        .pagecut {
            page-break-after: always;
            page-break-inside: avoid;
            border: 0px solid #FFF !important;
        }
    </style>

    <% if (schoolID == 1145)
        {%>
    <style>
        #bottomcard2 {
            margin-top: 40px !important;
        }
    </style>

    <%--<style>
        #mainPage1 #Img1 {
            /*   margin-left: -1mm !important;
            margin-top: -1mm !important;*/
            width: 2.25cm !important;
            height: 3cm !important;
        }

        #mainPage1 #Label2 {
            margin-left: 4mm !important;
            margin-top: 0mm !important;
        }

        #mainPage1 #Label3 {
            margin-left: 7mm !important;
            margin-top: 0mm !important;
        }

        #mainPage1 #Class {
            margin-left: 4mm !important;
            margin-top: 0mm !important;
        }

        #mainPage1 #DateNow {
            margin-left: 4mm !important;
            margin-top: 0mm !important;
        }

        #mainPage1 #ExpDate {
            margin-left: 4mm !important;
            margin-top: 0mm !important;
        }

        #mainPage1 #BarCode {
            margin-left: 14mm !important;
            margin-top: 5mm !important;
        } 
        
        #mainPage1 #bottomcard2 {
            margin-top: 10mm !important;
        }
    </style>--%>
    <% }%>
</head>
<body>

    <form id="form1" autocomplete="off" runat="server">

        <div id="printbutton" runat="server" class="example-screen w3-button w3-blue w3-right glyphicon glyphicon-print modeall" style="position: fixed; top: 40%; right: 10px; z-index: 4; border: 1px solid black;"
            onclick="window.print()">
            <p>
                <br>
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %>
            </p>
        </div>

        <!-- Page Content -->
        <div id="mainPage1" runat="server" class="page printableArea pagecut" style="padding: 0px;">
            <div class="subpage">

                <div class="col-xs-12 pad0" style="height: 5.4cm;">

                    <img id="BG" runat="server" alt="" class="avatar img-responsive centertext pull-left" onerror="this.style.display='none'" />

                    <div style="height: 1.3cm;"></div>

                    <div class="col-xs-12 pad0" style="height: 91px; margin-top: 9px;">

                        <div class="col-xs-4 pad0">
                            <img id="Img1" runat="server" alt="" class="avatar img-responsive centertext pull-left" style="" />
                        </div>

                        <div class="col-xs-8 pad0">

                            <div class="col-xs-12 pad0" style="height: 14px;">
                                <div class="col-xs-2 pad0" style=""></div>
                                <div class="col-xs-10 pad0" stylexx="display: inline-flex;align-items: end;height: 14px;">
                                    <asp:Label ID="Label2" runat="server" CssClass="col-xs-12 sarabun resizeableFont --fix-pl1 --fix-bottom"></asp:Label>
                                </div>
                            </div>
                            <div class="col-xs-12 pad0" style="height: 15px;">
                                <div class="col-xs-2 pad0"></div>
                                <div class="col-xs-10 pad0">
                                    <asp:Label ID="LabelEngName" runat="server" CssClass="col-xs-12 sarabun resizeableFont2 " Style="padding-left: 16px !important;"></asp:Label>
                                </div>
                            </div>

                            <div class="col-xs-12 pad0" style="height: 25px;" runat="server" id="divPID">
                                <div class="col-xs-4 pad0"></div>
                                <div class="col-xs-8 pad0">
                                    <asp:Label ID="Label3" runat="server" CssClass="col-xs-12 sarabun  --fix-pl2"> </asp:Label>
                                </div>
                            </div>

                            <div class="col-xs-12 pad0" style="height: 28px;">
                                <div class="col-xs-10 pad0">
                                    <asp:Label ID="Class" runat="server" CssClass="col-xs-12 sarabun --fix-pl3"> </asp:Label>
                                </div>
                                <div class="col-xs-2 pad0">
                                    <asp:Label ID="Blood" runat="server" CssClass="col-xs-12 sarabun"> </asp:Label>
                                </div>
                            </div>

                            <div class="col-xs-12 pad0" style="height: 21px;">
                                <div class="col-xs-6 pad0">
                                    <asp:Label ID="DateNow" CssClass="col-xs-12 sarabun --fix-pl4" runat="server"> </asp:Label>
                                </div>
                                <div class="col-xs-6 pad0">
                                    <asp:Label ID="ExpDate" CssClass="col-xs-12 sarabun" runat="server"> </asp:Label>
                                </div>
                            </div>

                            <div class="col-xs-12 pad0" id="HideQRCode" runat="server">
                                <div class="col-xs-12 pad0">
                                    <div class="col-xs-12 pad0">
                                        <img id="BarCode" runat="server" alt="" class="avatar img-responsive centertext" />
                                    </div>
                                </div>

                            </div>

                            <%--  <div class="col-xs-12 pad0" id="dome" runat="server">
                                <div class="col-xs-12 pad0">
                                    <div class="col-xs-12 pad0">
                                        <img id="Img2" runat="server" alt="" class="avatar img-responsive centertext" />
                                    </div>
                                </div>
                            </div>--%>
                        </div>
                    </div>

                    <div class="col-xs-12 pad0">
                        <div class="col-xs-4 pad0" id="bottomcard2" style="width: 102px; font-size: 100%; font-weight: bold; margin-top: 33px; text-align: center;">
                            <asp:Label ID="studentID" runat="server"> </asp:Label>
                        </div>
                        <div class="col-xs-8 pad0"></div>
                    </div>

                </div>
            </div>
        </div>

        <div id="mainPageTC15" runat="server" class="page printableArea pagecut" style="padding: 0px;">
            <div class="subpage">

                <div class="col-xs-12 pad0" <%--style="height: 5.4cm;"--%>>

                    <img id="BG15" runat="server" alt="" class="bgimage  img-responsive centertext pull-left" onerror="this.style.display='none'" />

                    <div style="height: 1.3cm;"></div>

                    <div class="col-xs-12 pad0" style="height: 91px; margin-top: 9px;">

                        <div class="col-xs-4 pad0">
                            <img id="ImgProfile15" runat="server" alt="" class="avatar  img-responsive centertext pull-left" style="" />
                        </div>

                        <div class="col-xs-8 pad0">

                            <div class="col-xs-12 pad0" style="height: 14px;">
                                <div class="col-xs-2 pad0"></div>
                                <div class="col-xs-10 pad0">
                                    <asp:Label ID="LblName15" runat="server" CssClass="col-xs-12 sarabun resizeableFont --fix-pl1"></asp:Label>
                                </div>
                            </div>
                            <div class="col-xs-12 pad0" style="height: 15px;">
                                <div class="col-xs-2 pad0"></div>
                                <div class="col-xs-10 pad0">
                                    <asp:Label ID="LblEngname15" runat="server" CssClass="col-xs-12 sarabun resizeableFontx --fix-font10"></asp:Label>
                                </div>
                            </div>

                            <div class="col-xs-12 pad0" style="height: 25px;">
                                <div class="col-xs-4 pad0"></div>
                                <div class="col-xs-8 pad0">
                                    <asp:Label ID="LblStudentID15" runat="server" CssClass="col-xs-12 sarabun" Style="padding-left: 20px !important;"> </asp:Label>
                                </div>
                            </div>

                            <div class="col-xs-12 pad0" style="height: 26px;">
                                <div class="col-xs-12 pad0">
                                    <asp:Label ID="LblClass15" runat="server" CssClass="col-xs-12 sarabun --fix-pl3"> </asp:Label>
                                </div>
                                <div class="col-xs-12 pad0">
                                    <asp:Label ID="LblClassEng15" runat="server" CssClass="col-xs-12 sarabun   --fix-font10"> </asp:Label>
                                </div>
                            </div>

                            <div class="col-xs-12 pad0" id="Div2" runat="server">
                                <div class="col-xs-12 pad0">
                                    <img id="ImgBarcode15" runat="server" alt="" class="avatar img-responsive centertext" />
                                </div>
                                <div class="col-xs-12 pad0 text-center">
                                    <asp:Label ID="LblDorm15" runat="server" CssClass=" col-xs-12 sarabun resizeableFont2" Style="position: relative; top: 0px; height: 12px; color: black !important;"> </asp:Label>
                                </div>
                            </div>


                        </div>
                    </div>

                    <div class="col-xs-12 pad0">
                        <div class="col-xs-4 pad0" id="bottomcard2" style="width: 102px; font-size: 100%; font-weight: bold; margin-top: 34px; text-align: center;">
                            <asp:Label ID="Label10" runat="server"> </asp:Label>
                        </div>
                        <div class="col-xs-8 pad0"></div>
                    </div>

                </div>
            </div>
        </div>

        <!--type5-->
        <div id="mainPageTC5" runat="server" class="col-xs-12 pagecut" style="width: 82.5mm; height: 128.5mm; padding: 0px; margin: 0px; border-radius: 5px; border: 1px solid #ccc;">

            <div class="col-xs-12 pad0 PositionRelative">
                <img id="BG5" runat="server" alt="" style="width: 82.5mm; height: 128.5mm;" onerror="this.style.display='none'" />
            </div>

            <div class="col-xs-12 pad0 PositionInConner">
                <div class="col-xs-12 pad0" style="height: 84px;"></div>

                <div class="col-xs-12 pad0" style="height: 156px;">
                    <div id="MoveImgProfileTC5" class="col-xs-12 pad0" runat="server">
                        <div class="col-xs-12" style="text-align: center;">
                            <img id="ImgProfileTC5" runat="server" alt="" style="height: 146px;" />
                        </div>
                    </div>
                </div>

                <div class="col-xs-12 pad0" style="height: 99px;">
                    <div class="col-xs-12 pad0" id="MoveContentTC5" runat="server">
                        <div class="col-xs-12 sarabun" id="tagDivNameTC5" runat="server" style="height: 27px;">
                            <span class="col-xs-12 pad0 resizeableFont5" id="FullNameTC5" runat="server"></span>
                        </div>
                        <div class="col-xs-12 sarabun" id="tagDivClassTC5" runat="server" style="height: 28px;">
                            <span class="col-xs-12 pad0" id="ClassTC5" runat="server" style="font-size: 22px;"></span>
                        </div>
                        <div class="col-xs-12 sarabun" id="tagDivStudentIDTC5" runat="server" style="height: 28px;">
                            <span class="col-xs-12 pad0" id="MainidTC5" runat="server" style="font-size: 22px;"></span>
                        </div>
                    </div>
                </div>

                <div class="col-xs-12 pad0" style="height: 103px;">
                    <div class="col-xs-12 pad0" id="MoveBarcodeQRTC5" runat="server">
                        <div class="col-xs-12 sarabun pad0 centertext">
                            <img id="QR5" runat="server" style="height: 90px; object-fit: contain;" />
                        </div>
                    </div>
                </div>
                <div class="col-xs-12 sarabun pad0 centertext" style="height: 26px;">
                    <span class="col-xs-12" id="StudentidTC5" runat="server" style="font-size: 25px;"></span>
                </div>
            </div>
        </div>

        <!--type6-->
        <div id="mainPageTC6" runat="server" class="col-xs-12 pagecut" style="width: 82.5mm; height: 128.5mm; padding: 0px; margin: 0px; border-radius: 5px; border: 1px solid #ccc;">
            <div class="col-xs-12 pad0 PositionRelative">
                <img id="BG3" runat="server" alt="" style="width: 82.5mm; height: 128.5mm;" onerror="this.style.display='none'" />
            </div>

            <div class="col-xs-12 pad0 PositionInConner">

                <div class="col-xs-12 pad0" style="height: 54px;"></div>

                <div class="col-xs-12 pad0" style="height: 180px;">
                    <div class="col-xs-4 pad0"></div>
                    <div class="col-xs-8" style="padding-left: 6px; padding-right: 55px; text-align: center;">
                        <img id="ImgProfile3" runat="server" style="height: 177px; width: 143px;" />
                    </div>
                </div>

                <div class="col-xs-12 pad0" style="">
                    <div class="col-xs-12 pad0" style="height: 35px;">
                        <div class="col-xs-2 pad0"></div>
                        <div class="col-xs-10" style="padding-left: 5px; padding-right: 5px; text-align: center;">
                            <span id="studentID3" runat="server" style="font-size: 200%"></span>
                        </div>
                    </div>

                    <div class="col-xs-12 pad0" style="height: 26px;">
                        <div class="col-xs-2 pad0"></div>
                        <div class="col-xs-10" style="padding-left: 5px; padding-right: 5px; text-align: center;">
                            <span id="FullName3" runat="server"></span>
                        </div>
                    </div>

                    <div class="col-xs-12 pad0" style="height: 26px;">
                        <div class="col-xs-2 pad0"></div>
                        <div class="col-xs-10" style="padding-left: 5px; padding-right: 5px; text-align: center;">
                            <span id="FullNameEng3" runat="server"></span>
                        </div>
                    </div>

                    <div class="col-xs-12 pad0" style="height: 26px;">
                        <div class="col-xs-2 pad0"></div>
                        <div class="col-xs-10" style="padding-left: 5px; padding-right: 5px; text-align: center;">
                            <span id="MainId3" runat="server" style="font-size: 160%"></span>
                        </div>
                    </div>
                </div>

                <div class="col-xs-12 pad0" style="height: 26px;"></div>

                <div class="col-xs-12 pad0">
                    <div class="col-xs-2 pad0"></div>
                    <div class="col-xs-10" style="padding-left: 4px; padding-right: 5px;">
                        <img id="QR3" runat="server" style="height: 110px; width: 110px; object-fit: contain;" />
                    </div>
                </div>

            </div>
        </div>

        <!--type7-->
        <div id="mainPageTC7" runat="server" class="col-xs-12 pagecut" style="width: 82.5mm; height: 128.5mm; padding: 0px; margin: 0px; border-radius: 5px; border: 1px solid #ccc;">
            <div class="col-xs-12 pad0 PositionRelative">
                <img id="BG7" runat="server" alt="" style="width: 82.5mm; height: 128.5mm;" onerror="this.style.display='none'" />
            </div>

            <div class="col-xs-12 pad0 PositionInConner">

                <div class="col-xs-12 pad0" style="height: 94px;"></div>

                <div id="MoveImgProfileTC7" runat="server" class="col-xs-12 pad0">
                    <div class="col-xs-12 pad0" style="text-align: center;">
                        <img id="ImgProfileTC7" runat="server" style="height: 188px; width: 150px;" />
                    </div>
                </div>

                <div id="MoveConTentTC7" runat="server" class="col-xs-12 pad0">

                    <div class="col-xs-12" style="text-align: right; height: 28px;">
                        <span id="FullNameTC7" runat="server" class="resizeableFontType7_1"></span>
                    </div>
                    <div id="DivHidNameEng7" runat="server" class="col-xs-12" style="text-align: right; height: 28px;">
                        <span id="FullNameEngTC7" runat="server" class="resizeableFontType7_2"></span>
                    </div>
                    <div class="col-xs-12" style="text-align: right; height: 27px;">
                        <span id="ClassTC7" runat="server" style="font-size: 130%"></span>
                    </div>
                    <div class="col-xs-12" style="text-align: right; height: 27px;">
                        <span id="MainIdTC7" runat="server" style="font-size: 130%"></span>
                    </div>
                    <div class="col-xs-12" style="text-align: right; height: 27px;">
                        <span id="StudentIdTC7" runat="server" style="font-size: 130%"></span>
                    </div>

                </div>

            </div>
        </div>

        <!--type8-->
        <div id="mainPageTC8" runat="server" class="page printableArea pagecut" style="padding: 0px;">
            <div class="subpage">

                <div class="col-xs-12 pad0" style="height: 5.4cm;">

                    <img id="BG8" runat="server" alt="" class="avatar img-responsive centertext pull-left" onerror="this.style.display='none'" />

                    <div style="height: 1.3cm;"></div>

                    <div class="col-xs-12 pad0" style="height: 91px; margin-top: 9px;">
                        <div class="col-xs-4 pad0">
                            <img id="IMGProfile8" runat="server" alt="" class="avatar img-responsive centertext pull-left" style="" />
                        </div>
                        <div class="col-xs-8 pad0">
                            <div class="col-xs-12 pad0">
                                <div class="col-xs-2 pad0"></div>
                                <div class="col-xs-10 pad0">
                                    <asp:Label ID="NameTH8" runat="server" CssClass="pad0 col-xs-12 sarabun"></asp:Label>
                                </div>
                            </div>
                            <div class="col-xs-12 pad0">
                                <div class="col-xs-2 pad0"></div>
                                <div class="col-xs-10 pad0">
                                    <asp:Label ID="NameENG8" runat="server" CssClass="pad0 col-xs-12 sarabun"></asp:Label>
                                </div>
                            </div>
                            <div class="col-xs-12 pad0">
                                <div class="col-xs-2 pad0"></div>
                                <div class="col-xs-10 pad0">
                                    <asp:Label ID="LastNameTH8" runat="server" CssClass="col-xs-12 sarabun"></asp:Label>
                                </div>
                            </div>
                            <div class="col-xs-12 pad0">
                                <div class="col-xs-2 pad0"></div>
                                <div class="col-xs-10 pad0">
                                    <asp:Label ID="LastNameENG8" runat="server" CssClass="col-xs-12 sarabun"></asp:Label>
                                </div>
                            </div>
                            <div class="col-xs-12 pad0" style="height: 25px;">
                                <div class="col-xs-12 pad0">
                                    <asp:Label ID="StudentID8" runat="server" CssClass="sarabun"> </asp:Label>
                                </div>
                            </div>
                            <div class="col-xs-12 pad0" style="height: 26px;">
                                <div class="col-xs-12">
                                    <asp:Label ID="DateTimeNow8" CssClass="sarabun" runat="server"> </asp:Label>
                                </div>
                            </div>
                            <div class="col-xs-12 pad0">
                                <div class="col-xs-12 pad0">
                                    <img id="BarCode8" runat="server" alt="" class="avatar img-responsive centertext" />
                                </div>
                            </div>
                        </div>

                    </div>

                </div>

            </div>

        </div>

        <!--type9-->
        <div id="mainPageTC9" runat="server" class="col-xs-12 pagecut" style="width: 82.5mm; height: 128.5mm; padding: 0px; margin: 0px; border-radius: 5px; border: 1px solid #ccc;">

            <div class="col-xs-12 pad0 PositionRelative">
                <img id="BG9" runat="server" alt="" style="width: 82.5mm; height: 128.5mm;" onerror="this.style.display='none'" />
            </div>

            <div class="col-xs-12 pad0 PositionInConner">
                <div class="col-xs-12 pad0" style="height: 96px;"></div>

                <div class="col-xs-12 pad0" style="height: 186px;">
                    <div id="MoveImgProfileTC9" runat="server" class="col-xs-12 pad0">
                        <div class="col-xs-12 pad0" style="text-align: center;">
                            <img id="ImgProfileTC9" runat="server" style="height: 182px;" />
                        </div>
                    </div>
                </div>

                <div id="MoveConTentTC9" class="col-xs-12 pad0" style="top: 3px; margin-left: 0px;">

                    <div id="tagDivNameTC9" runat="server" class="col-xs-12" style="height: 28px; top: 2px">
                        <span class="col-xs-12 pad0 resizeableFont9" id="FullNameTC9" runat="server"></span>
                    </div>
                    <div id="tagDivNameENTC9" runat="server" class="col-xs-12" style="height: 28px; top: 2px">
                        <span class="col-xs-12 pad0 resizeableFont9" id="FullNameENTC9" runat="server"></span>
                    </div>
                    <div id="tagDivClassTC9" runat="server" class="col-xs-12" style="height: 27px; top: 2px;">
                        <span id="ClassTC9" runat="server" style="font-size: 130%"></span>
                    </div>
                    <div id="tagDivStudentIDTC9" runat="server" class="col-xs-12" style="height: 27px; top: 4px;">
                        <span id="StudentIdTC9" runat="server" style="font-size: 130%"></span>
                    </div>
                    <div class="col-xs-12 pad0" style="height: 79px;">
                        <div class="col-xs-12 pad0" id="MoveBarcodeTC9" runat="server">
                            <img id="BarCodeTC9" runat="server" alt="" class="avatar img-responsive" style="margin-left: 45px; margin-top: 7px; width: 220px;" />
                        </div>
                    </div>
                    <div class="col-xs-12" style="text-align: center; height: 27px;">
                        <span id="JournneyTypeTC9" runat="server" style="font-size: 130%"></span>
                    </div>

                </div>
            </div>

        </div>

        <!--type10-->
        <div id="mainPageTC10" runat="server" class="page printableArea pagecut" style="padding: 0px;">
            <div class="subpage">
                <div class="col-xs-12 pad0" style="height: 5.4cm;">
                    <img id="BG10" runat="server" alt="" class="avatar img-responsive centertext pull-left" onerror="this.style.display='none'" />

                    <div style="height: 14.7mm;"></div>

                    <div class="col-xs-12 pad0" style="height: 91px; margin-top: 9px;">

                        <div id="MoveContent10" runat="server" class="col-xs-12 pad0">
                            <div class="col-xs-12" style="height: 25px;">
                                <div class="col-xs-6" style="padding-left: 32px; padding-right: 0px;">
                                    <asp:Label ID="NameTH10" runat="server" CssClass="pad0 col-xs-12 sarabun"></asp:Label>
                                </div>
                                <div class="col-xs-6" style="padding-left: 32px; padding-right: 0px;">
                                    <asp:Label ID="LastNameTH10" runat="server" CssClass="pad0 col-xs-12 sarabun"></asp:Label>
                                </div>
                            </div>
                            <div class="col-xs-12" style="height: 26px;">
                                <div class="col-xs-12" style="padding-left: 49px;">
                                    <asp:Label ID="NickNameTH10" runat="server" CssClass="pad0 col-xs-12 sarabun"></asp:Label>
                                </div>
                            </div>
                            <div class="col-xs-12" style="height: 25px;">
                                <div class="col-xs-12" style="padding-left: 150px;">
                                    <asp:Label ID="MainId10" runat="server" CssClass="pad0 col-xs-12 sarabun"></asp:Label>
                                </div>
                            </div>
                            <div class="col-xs-12" style="height: 15px;">
                                <div class="col-xs-12" style="padding-left: 118px;">
                                    <asp:Label ID="StudentID10" runat="server" CssClass="pad0 col-xs-12 sarabun"></asp:Label>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>

        <!--type11-->
        <div id="mainPageTC11" runat="server" class="col-xs-12 pagecut" style="width: 82.5mm; height: 128.5mm; padding: 0px; margin: 0px; border-radius: 5px; border: 1px solid #ccc;">
            <div class="col-xs-12 pad0 PositionRelative">
                <img id="BG11" runat="server" alt="" style="width: 82.5mm; height: 128.5mm;" onerror="this.style.display='none'" />
            </div>

            <div class="col-xs-12 pad0 PositionInConner">

                <div class="col-xs-12 pad0" style="height: 100px;"></div>

                <div id="MoveImgProfileTC11" runat="server" class="col-xs-12 pad0">
                    <div class="col-xs-12 pad0" style="text-align: center;">
                        <img id="ImgProfileTC11" runat="server" style="width: 138px; height: 173.88px;" />
                    </div>
                </div>

                <div class="col-xs-12 pad0" style="height: 30px;"></div>

                <div id="MoveConTentTC11" runat="server" class="col-xs-12 pad0">

                    <div class="col-xs-12" style="text-align: center; height: 27px;">
                        <span id="FullNameTC11" runat="server"></span>
                    </div>
                    <div class="col-xs-12" style="text-align: center; height: 27px;">
                        <span id="ClassTC11" runat="server" style="font-size: 135%"></span>
                    </div>
                    <div class="col-xs-12" style="text-align: center; height: 27px;" runat="server" id="divPID11">
                        <span id="MainIdTC11" runat="server" style="font-size: 135%"></span>
                    </div>
                    <div class="col-xs-12" style="text-align: center; height: 27px;">
                        <span id="StudentIdTC11" runat="server" style="font-size: 135%"></span>
                    </div>

                </div>

            </div>
        </div>

        <!--type12-->
        <div id="mainPageTC12" runat="server" class="col-xs-12 pagecut" style="width: 82.5mm; height: 128.5mm; padding: 0px; margin: 0px; border-radius: 5px; border: 1px solid #ccc;">
            <div class="col-xs-12 pad0 PositionRelative">
                <img id="BG12" runat="server" alt="" style="width: 82.5mm; height: 128.5mm;" onerror="this.style.display='none'" />
            </div>

            <div class="col-xs-12 pad0 PositionInConner">

                <div class="col-xs-12 pad0" style="height: 53px;"></div>

                <div class="col-xs-12 pad0" style="height: 180px;">
                    <div class="col-xs-4 pad0"></div>
                    <div class="col-xs-8" style="padding-left: 3px;">
                        <img id="ImgProfile12" runat="server" style="height: 176px; width: 140px;" />
                    </div>
                </div>

                <div id="BodyT12" style="height: 126px;" runat="server">
                    <div class="col-xs-12 pad0" style="height: 35px;">
                        <div class="col-xs-2 pad0"></div>
                        <div class="col-xs-10" style="padding-left: 5px; padding-right: 5px; text-align: center;">
                            <span id="studentID12" runat="server" style="font-size: 200%"></span>
                        </div>
                    </div>

                    <div class="col-xs-12 pad0" style="height: 29px;">
                        <div class="col-xs-2 pad0"></div>
                        <div class="col-xs-10" style="padding-left: 5px; padding-right: 5px; text-align: center;">
                            <span id="FullNameTC12" runat="server"></span>
                        </div>
                    </div>

                    <div class="col-xs-12 pad0" style="height: 25px;">
                        <div class="col-xs-2 pad0"></div>
                        <div class="col-xs-10" style="padding-left: 5px; padding-right: 5px; text-align: center;">
                            <span id="ClassTC12" runat="server" style="font-size: 150%"></span>
                        </div>
                    </div>

                    <div class="col-xs-12 pad0">
                        <div class="col-xs-2 pad0"></div>
                        <div class="col-xs-10" style="padding-left: 5px; padding-right: 5px; text-align: center;">
                            <span id="MainId12" runat="server" style="font-size: 160%"></span>
                        </div>
                    </div>
                </div>

                <div class="col-xs-12 pad0">
                    <div class="col-xs-4 pad0"></div>
                    <div class="col-xs-8" style="padding-left: 19px;">
                        <img id="QR12" runat="server" style="height: 105px; width: 105px; object-fit: contain;" />
                    </div>
                </div>

            </div>
        </div>

        <!--type13-->
        <div id="mainPageTC13" runat="server" class="col-xs-12 pagecut" style="width: 82.5mm; height: 128.5mm; padding: 0px; margin: 0px; border-radius: 5px; border: 1px solid #ccc;">
            <div class="col-xs-12 pad0 PositionRelative">
                <img id="BG13" runat="server" alt="" style="width: 82.5mm; height: 128.5mm;" onerror="this.style.display='none'" />
            </div>

            <div class="col-xs-12 pad0 PositionInConner">
                <div class="col-xs-12 pad0" style="height: 84px;"></div>

                <div class="col-xs-12 pad0" style="height: 156px;">
                    <div class="col-xs-12 pad0" style="height: 156px;">
                        <div id="MoveImgProfileTC13" class="col-xs-12 pad0" runat="server">
                            <div class="col-xs-12" style="text-align: center;">
                                <img id="ImgProfileTC13" runat="server" alt="" style="height: 146px; width: 117px" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xs-12 pad0" style="height: 99px;">
                    <div class="col-xs-12 pad0" id="MoveContentTC13" runat="server">
                        <div class="col-xs-12 sarabun" style="text-align: center; height: 27px;">
                            <span class="col-xs-12 pad0" id="FullNameThTC13" runat="server"></span>
                        </div>
                        <div class="col-xs-12 sarabun" style="text-align: center; height: 28px;">
                            <span class="col-xs-12 pad0" id="FullNameEnTC13" runat="server" style="font-size: 22px;"></span>
                        </div>
                        <div class="col-xs-12 sarabun" style="text-align: center; height: 28px;">
                            <span class="col-xs-12 pad0" id="MainidTC13" runat="server" style="font-size: 22px;"></span>
                        </div>
                    </div>
                </div>

                <div class="col-xs-12 pad0" style="height: 103px;">
                    <div class="col-xs-12 pad0" id="MoveBarcodeQRTC13" runat="server">
                        <div class="col-xs-12 sarabun pad0 centertext">
                            <img id="QR13" runat="server" style="height: 90px; object-fit: contain;" />
                        </div>
                    </div>
                </div>
                <div class="col-xs-12 sarabun pad0 centertext" style="height: 26px;">
                    <span class="col-xs-12" id="StudentidTC13" runat="server" style="font-size: 25px;"></span>
                </div>
            </div>
        </div>

        <!--type16-->
        <div id="mainPageTC16" runat="server" class="col-xs-12 pagecut" style="width: 82.5mm; height: 128.5mm; padding: 0px; margin: 0px; border-radius: 5px; border: 1px solid #ccc;">
            <div class="col-xs-12 pad0 PositionRelative">
                <img id="bg16" runat="server" alt="" style="width: 82.5mm; height: 128.5mm;" onerror="this.style.display='none'" />
            </div>

            <div class="col-xs-12 pad0 PositionInConner">

                <div class="col-xs-12 pad0" style="height: 88px;"></div>

                <div class="col-xs-12 pad0 text-center" style="height: 147px;">
                    <img id="avatar16" runat="server" style="height: 147px;width: 118px;" />
                </div>

                <div id="Div3"  class="mb-5"  runat="server">
                    <div class="col-xs-12 pad0 text-center" style="height: 30px;">
                        <span id="nameTh16" runat="server" style="font-size: 180%"></span>
                    </div>

                    <div class="col-xs-12 pad0 text-center" style="height: 30px;">
                        <span id="nameEn16" runat="server" style="font-size: 150%"></span>
                    </div>

                    <div class="col-xs-12 pad0 text-center" style="height: 25px;">
                        <span id="code16" runat="server" style="font-size: 150%"></span>
                    </div>
                </div>

                <div class="col-xs-12 pad0 text-center " style="margin-top:12px;">
                    <img id="qrCode16" runat="server" style="height: 105px; width: 105px; object-fit: contain;" />
                </div>

                <div class="col-xs-12  position-relative " style="top: 24px;padding:0 4px;">
                    <div class="col-xs-6 pad0 text-left ml-1">
                        <span id="dateRelease16" runat="server" style="font-size: 120%;margin-left:70px"></span>
                    </div>
                    <div class="col-xs-6 pad0 text-right mr-1">
                        <span id="dateExpire16" runat="server" style="font-size: 120%;margin-right:12px"></span>
                    </div>
                </div>

            </div>
        </div>
        <script>
            $(function () {
                $(".resizeableFont").quickfit({ max: 15, min: 13, truncate: false, width: 220 });
                $(".resizeableFont2").quickfit({ max: 13, min: 9, truncate: false, width: 200 });
                $(".resizeableFont9").quickfit({ max: 22, min: 15, truncate: false, width: 260 });
                $(".resizeableFont5").quickfit({ max: 22, min: 15, truncate: false, width: 290 });

                $(".resizeableFontType7_1").quickfit({ max: 23, min: 15, truncate: false, width: 270 });
                $(".resizeableFontType7_2").quickfit({ max: 23, min: 17, truncate: false, width: 230 });
            });
        </script>
    </form>

</body>

</html>
