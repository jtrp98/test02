﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DiplomaPrint.aspx.cs" Inherits="FingerprintPayment.Diploma.DiplomaPrint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>

    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>

    <script src="JSDiplomaPrint.js?v=<%=DateTime.Now.Ticks%>"></script>

    <link href="/bootstrap SB2/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />


    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">

    <style>
        /*.w3-image {
            max-width: 100%;
            height: auto
        }*/

        img {
            vertical-align: middle
        }

        /*a {
            color: inherit
        }*/

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
            word-break: normal;
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

        .pad {
            padding-top: 2px;
            padding-bottom: 2px;
        }

        .noborder {
            border-style: none;
            text-decoration: none;
            text-shadow: none !important;
            box-shadow: inset 0px 0px 0px 0px white;
            width: 100%;
            height: 21px;
        }

        .centertext {
            text-align: center;
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

        .h16 {
            height: 16px;
        }

        .h24 {
            height: 24px;
        }

        .h48 {
            height: 48px;
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

        .lrborder {
            border-left: 1px solid black !important;
            border-right: 1px solid black !important;
            border-top: 0px !important;
            border-bottom: 0px !important;
        }

        .lrbborder {
            border-left: 1px solid black;
            border-right: 1px solid black;
            border-bottom: 1px solid black;
            border-top: 0px !important;
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

        .sarabun {
            font-family: "THSarabun";
            font-size: 140%;
        }


        .break {
            word-break: normal;
            word-wrap: anywhere;
        }

        .subheader {
            border: 1px solid #000000;
            text-align: center;
            height: 20.8px;
        }

        .padr0 {
            padding-right: 0px;
            height: 20px;
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
            min-height: 148mm;
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

        .subpage {
            padding: 0.5cm;
            border: 5px;
            height: 160mm;
            outline: 2cm;
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

        @page {
            size: A4;
            margin: 4mm;
        }

        @media print {
            html, body {
                width: 210mm;
                height: 148mm;
            }

            .page {
                margin: 0;
            }

            .no-print, .no-print * {
                display: none !important;
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
    <style type="text/css" media="print">
        .pagecut {
            page-break-after: always;
            page-break-inside: avoid;
        }
    </style>

        <style type="text/css">
       label{
           font-weight: lighter;
       }
    </style>
    <script type="text/javascript">


        function clickToggleText() {

             var paperText = document.getElementsByClassName('paperText');
          
            if ($("#btnToggleText").html() == '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01818") %>') {
                $("#btnToggleText").html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02181") %>');
                //alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132081") %>');
               

                for (var i = 0; i < paperText.length; i++) {
                    paperText[i].style.visibility = "hidden";
                }

            }
            else if ($("#btnToggleText").html() == '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02181") %>') {
                $("#btnToggleText").html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01818") %>');
                //alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132082") %>');

                 for (var i = 0; i < paperText.length; i++) {
                    paperText[i].style.visibility = "visible";
                }
            }

            
        }
    </script>

</head>
<body>

        

    <form id="form1" runat="server">
        <asp:TextBox runat="server" ID="stdId" hidden="hidden"></asp:TextBox>
        <div>
             <span class="example-screen button-checkbox">
        <button id="btnToggleText" onclick="clickToggleText()" type="button" style="position: fixed; top: 10%; right: 10px; z-index: 4;" class="btn" data-color="primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01818") %></button>
      </span>
            <div class="example-screen w3-button w3-blue w3-right glyphicon glyphicon-print modeall" style="position: fixed; top: 20%; right: 10px; z-index: 4; border: 1px solid black;" onclick="window.print()">
                <p>
                    <br>
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %>
                </p>
            </div>
        </div>

        <!-- Page Content -->

        <div class="book page1">
            <div class="page printableArea pagecut sarabun">
                <div class="row text-right">
                    <div class="col-xs-offset-9 col-xs-2" style="font-size:18px">
                        <label class="paperText"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132083") %></label>
                    </div>
                </div>
                <div class="row text-right">
                    <div class="col-xs-offset-9 col-xs-2" style="font-size:18px">
                        
                        <label class="paperText"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101015") %> </label><label id="lbDiplomaCode" class="paperText"></label>
                    </div>
                </div>
                <div class="row text-center" style="font-size:36px">
                    <div class="col-xs-12">
                        <label class="paperText"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132084") %></label>
                    </div>
                </div>
                <div class="row text-center" style="font-size:32px">
                    <div class="col-xs-12">       
                        <label class="paperText"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132085") %></label>
                    </div>
                </div>
                 <div class="row text-center">
                    <div class="col-xs-12">
                        <label class="" id="stdName"></label>
                    </div>
                </div>
                <div class="row text-left">
                    <div class="col-xs-offset-3 col-xs-1 pad0">
                        <label class="paperText"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211008") %></label>
                    </div>
                    <div class="col-xs-1 text-center">
                        <div class="col-xs-12">
                        <label class="" id="stdBd"></label>
                    </div>
                    </div>
                    <div class="col-xs-1">
                        <label class="paperText"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %></label>
                    </div>
                    <div class="col-xs-2 text-center">
                        <label class="" id="stdBm"></label>
                    </div>
                    <div class="col-xs-1 text-left">
                        <label class="paperText"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211009") %></label>
                    </div>
                    <div class="col-xs-1 text-left pad0">
                        <label class="text-center" id="stdBy"></label>
                    </div> 
                </div>
                <div class="row text-center">
                    <div class="col-xs-12">
                        <label class="paperText"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132086") %></label>
                    </div>
                </div>
                 <div class="row text-left">
                    <div class="col-xs-offset-3 col-xs-1 pad0">
                        <label class="paperText"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204050") %></label>
                    </div>
                    <div class="col-xs-4 text-center">
                        <label class="" id="schoolName"></label>
                    </div>
                   
                </div>
                <div class="row text-left">
                    <div class="col-xs-offset-3 col-xs-3 pad0">
                        <label class="paperText"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132087") %> </label>
                        <label class="" id="schoolDistrict"></label>
                    </div>
                    <div class="col-xs-3 text-left pad0">
                        <label class="paperText"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105083") %> </label>
                         <label class="" id="schoolProvince"></label>
                    </div>  
                </div>
                <div class="row text-left">
                    <div class="col-xs-offset-3 col-xs-1 pad0">
                        <label class="paperText"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M503060") %> </label>
                    </div>
                    <div class="col-xs-1 text-center">
                        <label class="" id="dayGrad"></label>
                    </div>
                    <div class="col-xs-1">
                         <label class="paperText"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %> </label>
                    </div>
                    <div class="col-xs-2 text-center">
                        <label class="" id="monthGrad"></label>
                    </div>
                    <div class="col-xs-1 text-left">
                         <label class="paperText"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211009") %> </label>
                    </div>
                    <div class="col-xs-1 text-left pad0">
                        <label class="" id="yearGrad"></label>
                    </div>
                    
                </div>
                <div class="row text-center">
                    <div class="col-xs-12">
                        <label class="paperText"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132077") %></label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 text-center">
                        <label class="paperText">............................................................</label>
                    </div>
                    <div class="col-xs-offset-2 col-xs-5 text-center">
                        <label class="paperText">............................................................</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 text-center">
                        <label class="paperText"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132078") %></label>
                    </div>
                    <div class="col-xs-offset-2 col-xs-5 text-center">
                        <label class="paperText"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132079") %></label>
                    </div>
                </div>

            </div>
        </div>

        <div class="book page2 hidden">
            <div class="page printableArea pagecut sarabun">
                <div class="row text-center" style="margin-top: 120px;">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132088") %>
                </div>
                <div class="row">
                    <div class="col-xs-offset-7 col-xs-3">
                       &nbsp;
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-offset-1 col-xs-3">
                        ........................................
                    </div>
                    <div class="col-xs-2">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132089") %>
                    </div>
                    <div class="col-xs-offset-1 col-xs-3">
                        ........................................
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-offset-1 col-xs-3">
                        ........................................
                    </div>
                    <div class="col-xs-2">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132090") %>
                    </div>
                    <div class="col-xs-offset-1 col-xs-3">
                        (......................................)
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-offset-1 col-xs-3">
                        ........................................
                    </div>
                    <div class="col-xs-2">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132091") %>
                    </div>
                    <div class="col-xs-offset-1 col-xs-3 text-center">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206511") %>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-offset-7 col-xs-3">
                       &nbsp;
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-offset-7 col-xs-3">
                        ........................................
                    </div>
                </div>
                 <div class="row">
                    <div class="col-xs-offset-7 col-xs-3">
                        (......................................)
                    </div>
                </div>
                 <div class="row">
                    <div class="col-xs-offset-7 col-xs-3 text-center">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132080") %>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-offset-6 col-xs-5 text-center">
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %> <label>30</label>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %> <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107051") %></label>
                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211009") %> <label>3000</label>
                    </div>
                </div>
            </div>
        </div>

    </form>
</body>
</html>

