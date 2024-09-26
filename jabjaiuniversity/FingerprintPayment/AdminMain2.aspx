<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="AdminMain2.aspx.cs" Inherits="FingerprintPayment.AdminMain2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <style>
        /*! CSS Used from: https://dev.schoolbright.co/Styles/style.css */
        .center {
            text-align: center;
        }

        .right {
            text-align: right;
        }

        @media (max-width: 1023px) {
            .full-card {
                width: 100%;
                font-family: THSarabun;
                font-size: 16px;
                color: #555;
                padding: 30px 30px;
            }
        }

        @media (min-width: 1024px) and (max-width: 1199px) {
            .full-card {
                font-family: THSarabun;
                font-size: 18px;
                color: #555;
                padding: 30px 30px;
            }
        }

        @media (min-width: 1200px) {
            .full-card {
                font-family: THSarabun;
                font-size: 26px;
                color: #555;
                padding: 30px 30px;
            }
        }
        /*! CSS Used from: https://dev.schoolbright.co/bootstrap%20SB2/bower_components/bootstrap/dist/css/bootstrap.css */
        canvas {
            display: inline-block;
            vertical-align: baseline;
        }

        h1 {
            margin: .67em 0;
            font-size: 2em;
        }

        small {
            font-size: 80%;
        }

        img {
            border: 0;
        }

        hr {
            height: 0;
            -webkit-box-sizing: content-box;
            -moz-box-sizing: content-box;
            box-sizing: content-box;
        }

        table {
            border-spacing: 0;
            border-collapse: collapse;
        }

        td, th {
            padding: 0;
        }

        @media print {
            *, *:before, *:after {
                color: #000 !important;
                text-shadow: none !important;
                background: transparent !important;
                -webkit-box-shadow: none !important;
                box-shadow: none !important;
            }

            a, a:visited {
                text-decoration: underline;
            }

                a[href]:after {
                    content: " (" attr(href) ")";
                }

                a[href^="#"]:after {
                    content: "";
                }

            thead {
                display: table-header-group;
            }

            tr, img {
                page-break-inside: avoid;
            }

            img {
                max-width: 100% !important;
            }

            p {
                orphans: 3;
                widows: 3;
            }

            .btn > .caret {
                border-top-color: #000 !important;
            }

            .table {
                border-collapse: collapse !important;
            }

                .table th {
                    background-color: #fff !important;
                }
        }

        .glyphicon {
            position: relative;
            top: 1px;
            display: inline-block;
            font-family: 'Glyphicons Halflings';
            font-style: normal;
            font-weight: normal;
            line-height: 1;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
        }

        .glyphicon-stats:before {
            content: "\e185";
        }

        img {
            vertical-align: middle;
        }

        hr {
            margin-top: 20px;
            margin-bottom: 20px;
            border: 0;
            border-top: 1px solid #eee;
        }

        h1, h4, .h2 {
            font-family: inherit;
            font-weight: 500;
            line-height: 1.1;
            color: inherit;
        }

        h1, .h2 {
            margin-top: 20px;
            margin-bottom: 10px;
        }

        h4 {
            margin-top: 10px;
            margin-bottom: 10px;
        }

        h1 {
            font-size: 36px;
        }

        .h2 {
            font-size: 30px;
        }

        h4 {
            font-size: 18px;
        }

        p {
            margin: 0 0 10px;
        }

        small, .small {
            font-size: 85%;
        }

        .text-right {
            text-align: right;
        }

        .text-muted {
            color: #777;
        }

        ul {
            margin-top: 0;
            margin-bottom: 10px;
        }

            ul ul {
                margin-bottom: 0;
            }

        .row {
            margin-right: -15px;
            margin-left: -15px;
        }

        .col-lg-1, .col-lg-2, .col-xs-3, .col-sm-3, .col-md-3, .col-lg-3, .col-sm-4, .col-md-4, .col-lg-4, .col-sm-6, .col-md-6, .col-lg-6, .col-lg-8, .col-xs-9, .col-sm-9, .col-md-9, .col-lg-9, .col-lg-11, .col-sm-12, .col-md-12, .col-lg-12 {
            position: relative;
            min-height: 1px;
            padding-right: 15px;
            padding-left: 15px;
        }

        .col-xs-3, .col-xs-9 {
            float: left;
        }

        .col-xs-9 {
            width: 75%;
        }

        .col-xs-3 {
            width: 25%;
        }

        @media (min-width: 768px) {
            .col-sm-3, .col-sm-4, .col-sm-6, .col-sm-9, .col-sm-12 {
                float: left;
            }

            .col-sm-12 {
                width: 100%;
            }

            .col-sm-9 {
                width: 75%;
            }

            .col-sm-6 {
                width: 50%;
            }

            .col-sm-4 {
                width: 33.33333333%;
            }

            .col-sm-3 {
                width: 25%;
            }
        }

        @media (min-width: 992px) {
            .col-md-3, .col-md-4, .col-md-6, .col-md-9, .col-md-12 {
                float: left;
            }

            .col-md-12 {
                width: 100%;
            }

            .col-md-9 {
                width: 75%;
            }

            .col-md-6 {
                width: 50%;
            }

            .col-md-4 {
                width: 33.33333333%;
            }

            .col-md-3 {
                width: 25%;
            }
        }

        @media (min-width: 1200px) {
            .col-lg-1, .col-lg-2, .col-lg-3, .col-lg-4, .col-lg-6, .col-lg-8, .col-lg-9, .col-lg-11, .col-lg-12 {
                float: left;
            }

            .col-lg-12 {
                width: 100%;
            }

            .col-lg-11 {
                width: 91.66666667%;
            }

            .col-lg-9 {
                width: 75%;
            }

            .col-lg-8 {
                width: 66.66666667%;
            }

            .col-lg-6 {
                width: 50%;
            }

            .col-lg-4 {
                width: 33.33333333%;
            }

            .col-lg-3 {
                width: 25%;
            }

            .col-lg-2 {
                width: 16.66666667%;
            }

            .col-lg-1 {
                width: 8.33333333%;
            }
        }

        table {
            background-color: transparent;
        }

        th {
            text-align: left;
        }

        .table {
            width: 100%;
            max-width: 100%;
            margin-bottom: 20px;
        }

            .table > thead > tr > th {
                padding: 8px;
                line-height: 1.42857143;
                vertical-align: top;
                border-top: 1px solid #ddd;
            }

            .table > thead > tr > th {
                vertical-align: bottom;
                border-bottom: 2px solid #ddd;
            }

            .table > thead:first-child > tr:first-child > th {
                border-top: 0;
            }


        .btn-default {
            color: #333;
            background-color: #fff;
            border-color: #ccc;
        }

            .btn-default:hover, .btn-default:focus, .btn-default:active {
                color: #333;
                background-color: #e6e6e6;
                border-color: #adadad;
            }

            .btn-default:active {
                background-image: none;
            }

        .btn-primary {
            color: #fff;
            background-color: #337ab7;
            border-color: #2e6da4;
        }

            .btn-primary:hover, .btn-primary:focus, .btn-primary:active {
                color: #fff;
                background-color: #286090;
                border-color: #204d74;
            }

            .btn-primary:active {
                background-image: none;
            }

        .btn-link {
            font-weight: normal;
            color: #337ab7;
            border-radius: 0;
        }

            .btn-link, .btn-link:active {
                background-color: transparent;
                -webkit-box-shadow: none;
                box-shadow: none;
            }

                .btn-link, .btn-link:hover, .btn-link:focus, .btn-link:active {
                    border-color: transparent;
                }

                    .btn-link:hover, .btn-link:focus {
                        color: #23527c;
                        text-decoration: underline;
                        background-color: transparent;
                    }

        .btn-sm {
            padding: 5px 10px;
            font-size: 12px;
            line-height: 1.5;
            border-radius: 3px;
        }

        .fade {
            opacity: 0;
            -webkit-transition: opacity .15s linear;
            -o-transition: opacity .15s linear;
            transition: opacity .15s linear;
        }

        .caret {
            display: inline-block;
            width: 0;
            height: 0;
            margin-left: 2px;
            vertical-align: middle;
            border-top: 4px dashed;
            border-right: 4px solid transparent;
            border-left: 4px solid transparent;
        }

        .dropdown-toggle:focus {
            outline: 0;
        }

        .dropdown-menu {
            position: absolute;
            top: 100%;
            left: 0;
            z-index: 1000;
            display: none;
            float: left;
            min-width: 160px;
            padding: 5px 0;
            margin: 2px 0 0;
            font-size: 14px;
            text-align: left;
            list-style: none;
            background-color: #fff;
            -webkit-background-clip: padding-box;
            background-clip: padding-box;
            border: 1px solid #ccc;
            border: 1px solid rgba(0, 0, 0, .15);
            border-radius: 4px;
            -webkit-box-shadow: 0 6px 12px rgba(0, 0, 0, .175);
            box-shadow: 0 6px 12px rgba(0, 0, 0, .175);
        }

            .dropdown-menu .divider {
                height: 1px;
                margin: 9px 0;
                overflow: hidden;
                background-color: #e5e5e5;
            }

            .dropdown-menu > li > a {
                display: block;
                padding: 3px 20px;
                clear: both;
                font-weight: normal;
                line-height: 1.42857143;
                color: #333;
                white-space: nowrap;
            }

                .dropdown-menu > li > a:hover, .dropdown-menu > li > a:focus {
                    color: #262626;
                    text-decoration: none;
                    background-color: #f5f5f5;
                }

        .btn-group {
            position: relative;
            display: inline-block;
            vertical-align: middle;
        }

            .btn-group > .btn {
                position: relative;
                float: left;
            }

                .btn-group > .btn:hover, .btn-group > .btn:focus, .btn-group > .btn:active {
                    z-index: 2;
                }

                .btn-group > .btn:first-child {
                    margin-left: 0;
                }

            .btn-group .dropdown-toggle:active {
                outline: 0;
            }

        .btn .caret {
            margin-left: 0;
        }

        .list-group {
            padding-left: 0;
            margin-bottom: 20px;
            font-size: 18px;
        }

        .panel {
            margin-bottom: 20px;
            background-color: #fff;
            border: 1px solid transparent;
            border-radius: 4px;
            -webkit-box-shadow: 0 1px 1px rgba(0, 0, 0, .05);
            box-shadow: 0 1px 1px rgba(0, 0, 0, .05);
        }

        .panel-body {
            padding: 15px;
        }

        .panel-heading {
            padding: 10px 15px;
            border-bottom: 1px solid transparent;
            border-top-left-radius: 3px;
            border-top-right-radius: 3px;
        }

            .panel-heading .material-icons {
                vertical-align: bottom;
            }

        .panel-default {
            border-color: #ddd;
        }

            .panel-default > .panel-heading {
                color: #333;
                background-color: #f5f5f5;
                border-color: #ddd;
                font-size: 20px;
            }

        .panel-primary {
            border-color: #337ab7;
        }

            .panel-primary > .panel-heading {
                color: #fff;
                background-color: #337ab7;
                border-color: #337ab7;
            }

        .close {
            float: right;
            font-size: 21px;
            font-weight: bold;
            line-height: 1;
            color: #000;
            text-shadow: 0 1px 0 #fff;
            filter: alpha(opacity=20);
            opacity: .2;
        }

            .close:hover, .close:focus {
                color: #000;
                text-decoration: none;
                cursor: pointer;
                filter: alpha(opacity=50);
                opacity: .5;
            }

        button.close {
            -webkit-appearance: none;
            padding: 0;
            cursor: pointer;
            background: transparent;
            border: 0;
        }

        .modal {
            position: fixed;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            z-index: 1050;
            display: none;
            overflow: hidden;
            -webkit-overflow-scrolling: touch;
            outline: 0;
        }

            .modal.fade .modal-dialog {
                -webkit-transition: -webkit-transform .3s ease-out;
                -o-transition: -o-transform .3s ease-out;
                transition: transform .3s ease-out;
                -webkit-transform: translate(0, -25%);
                -ms-transform: translate(0, -25%);
                -o-transform: translate(0, -25%);
                transform: translate(0, -25%);
            }

        .modal-dialog {
            position: relative;
            width: auto;
            margin: 10px;
        }

        .modal-content {
            position: relative;
            background-color: #fff;
            -webkit-background-clip: padding-box;
            background-clip: padding-box;
            border: 1px solid #999;
            border: 1px solid rgba(0, 0, 0, .2);
            border-radius: 6px;
            outline: 0;
            -webkit-box-shadow: 0 3px 9px rgba(0, 0, 0, .5);
            box-shadow: 0 3px 9px rgba(0, 0, 0, .5);
        }

        .modal-header {
            min-height: 16.42857143px;
            padding: 15px;
            border-bottom: 1px solid #e5e5e5;
        }

            .modal-header .close {
                margin-top: -2px;
            }

        .modal-title {
            margin: 0;
            line-height: 1.42857143;
        }

        .modal-body {
            position: relative;
            padding: 15px;
        }

        .modal-footer {
            padding: 15px;
            text-align: right;
            border-top: 1px solid #e5e5e5;
        }

        @media (min-width: 768px) {
            .modal-dialog {
                width: 600px;
                margin: 30px auto;
            }

            .modal-content {
                -webkit-box-shadow: 0 5px 15px rgba(0, 0, 0, .5);
                box-shadow: 0 5px 15px rgba(0, 0, 0, .5);
            }
        }

        .row:before, .row:after, .panel-body:before, .panel-body:after, .modal-footer:before, .modal-footer:after {
            display: table;
            content: " ";
        }

        .row:after, .panel-body:after, .modal-footer:after {
            clear: both;
        }

        .hidden {
            display: none !important;
        }
        /*! CSS Used from: https://dev.schoolbright.co/bootstrap%20SB2/dist/css/timeline.css */
        .timeline {
            position: relative;
            padding: 20px 0 20px;
            list-style: none;
        }

            .timeline:before {
                content: " ";
                position: absolute;
                top: 0;
                bottom: 0;
                left: 50%;
                width: 3px;
                margin-left: -1.5px;
                background-color: #eeeeee;
            }

            .timeline > li {
                position: relative;
                margin-bottom: 20px;
            }

                .timeline > li:before, .timeline > li:after {
                    content: " ";
                    display: table;
                }

                .timeline > li:after {
                    clear: both;
                }

                .timeline > li:before, .timeline > li:after {
                    content: " ";
                    display: table;
                }

                .timeline > li:after {
                    clear: both;
                }

                .timeline > li > .timeline-panel {
                    float: left;
                    position: relative;
                    width: 46%;
                    padding: 20px;
                    border: 1px solid #d4d4d4;
                    border-radius: 2px;
                    -webkit-box-shadow: 0 1px 6px rgba(0,0,0,0.175);
                    box-shadow: 0 1px 6px rgba(0,0,0,0.175);
                }

                    .timeline > li > .timeline-panel:before {
                        content: " ";
                        display: inline-block;
                        position: absolute;
                        top: 26px;
                        right: -15px;
                        border-top: 15px solid transparent;
                        border-right: 0 solid #ccc;
                        border-bottom: 15px solid transparent;
                        border-left: 15px solid #ccc;
                    }

                    .timeline > li > .timeline-panel:after {
                        content: " ";
                        display: inline-block;
                        position: absolute;
                        top: 27px;
                        right: -14px;
                        border-top: 14px solid transparent;
                        border-right: 0 solid #fff;
                        border-bottom: 14px solid transparent;
                        border-left: 14px solid #fff;
                    }

                .timeline > li > .timeline-badge {
                    z-index: 100;
                    position: absolute;
                    top: 16px;
                    left: 50%;
                    width: 50px;
                    height: 50px;
                    margin-left: -25px;
                    border-radius: 50% 50% 50% 50%;
                    text-align: center;
                    font-size: 1.4em;
                    line-height: 50px;
                    color: #fff;
                    background-color: #999999;
                    font-size: 25px;
                    padding-top: 10px;
                }

                .timeline > li.timeline-inverted > .timeline-panel {
                    float: right;
                }

                    .timeline > li.timeline-inverted > .timeline-panel:before {
                        right: auto;
                        left: -15px;
                        border-right-width: 15px;
                        border-left-width: 0;
                    }

                    .timeline > li.timeline-inverted > .timeline-panel:after {
                        right: auto;
                        left: -14px;
                        border-right-width: 14px;
                        border-left-width: 0;
                    }

        .timeline-badge.success {
            background-color: #3f903f !important;
        }

        .timeline-badge.warning {
            background-color: #f0ad4e !important;
        }

        .timeline-badge.danger {
            background-color: #d9534f !important;
        }

        .timeline-badge.info {
            background-color: #5bc0de !important;
        }

        .timeline-title {
            margin-top: 0;
            color: inherit;
        }

        .timeline-body > p {
            margin-bottom: 0;
        }

            .timeline-body > p + p {
                margin-top: 5px;
            }

        @media (max-width:767px) {
            ul.timeline:before {
                left: 40px;
            }

            ul.timeline > li > .timeline-panel {
                width: calc(100% - 90px);
                width: -moz-calc(100% - 90px);
                width: -webkit-calc(100% - 90px);
            }

            ul.timeline > li > .timeline-badge {
                top: 16px;
                left: 15px;
                margin-left: 0;
            }

            ul.timeline > li > .timeline-panel {
                float: right;
            }

                ul.timeline > li > .timeline-panel:before {
                    right: auto;
                    left: -15px;
                    border-right-width: 15px;
                    border-left-width: 0;
                }

                ul.timeline > li > .timeline-panel:after {
                    right: auto;
                    left: -14px;
                    border-right-width: 14px;
                    border-left-width: 0;
                }
        }
        /*! CSS Used from: https://dev.schoolbright.co/bootstrap%20SB2/dist/css/sb-admin-2.css */
        #page-wrapper {
            padding: 0 15px;
            min-height: 568px;
            background-color: #fff;
            padding-bottom: 30px;
        }

        @media (min-width:768px) {
            #page-wrapper {
                position: inherit;
                margin: 0 0 0 250px;
                padding: 0 30px;
                border-left: 1px solid #e7e7e7;
                background-color: #eee;
                padding-top: 30px;
                padding-bottom: 30px;
                min-height: 1000px !important;
            }
        }

        .flot-chart {
            display: block;
            height: 400px;
        }

        .flot-chart-content {
            width: 100%;
            font-size: 20px;
            height: 100%;
        }

        .huge {
            font-size: 40px;
        }

        .panel-green {
            border-color: #5cb85c;
        }

            .panel-green .panel-heading {
                border-color: #5cb85c;
                color: #fff;
                background-color: #5cb85c;
            }

        .panel-yellow {
            border-color: #f0ad4e;
        }

            .panel-yellow .panel-heading {
                border-color: #f0ad4e;
                color: #fff;
                background-color: #f0ad4e;
            }

        @media (max-width: 1023px) {
            .legendLabel {
                font-size: 16px;
                padding-left: 5px;
                padding-right: 5px;
                width: 100%;
                vertical-align: middle;
            }
        }

        @media (min-width: 1024px) and (max-width: 1199px) {
            .legendLabel {
                font-size: 16px;
                padding-left: 5px;
                padding-right: 0px;
                width: 100px;
                vertical-align: middle;
            }
        }

        @media (min-width: 1200px) and (max-width: 1359px) {
            .legendLabel {
                font-size: 16px;
                padding-left: 5px;
                padding-right: 0px;
                width: 100px;
                vertical-align: middle;
            }
        }

        @media (min-width: 1360px) {
            .legendLabel {
                font-size: 18px;
                padding-left: 5px;
                padding-right: 0px;
                width: 100px;
                vertical-align: middle;
            }
        }

        .legendColorBox {
            vertical-align: middle;
        }
        /*! CSS Used from: https://dev.schoolbright.co/Content/font-awesome.css */
        .fa {
            display: inline-block;
            font: normal normal normal 14px/1 FontAwesome;
            font-size: inherit;
            text-rendering: auto;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
        }

        .fa-5x {
            font-size: 5em;
        }

        .fa-fw {
            width: 1.28571429em;
            text-align: center;
        }

        .fa-star:before {
            content: "\f005";
        }

        .fa-check:before {
            content: "\f00c";
        }

        .fa-gear:before {
            content: "\f013";
        }

        .fa-clock-o:before {
            content: "\f017";
        }

        .fa-bar-chart-o:before {
            content: "\f080";
        }

        .fa-credit-card:before {
            content: "\f09d";
        }

        .fa-bell:before {
            content: "\f0f3";
        }

        .fa-save:before {
            content: "\f0c7";
        }

        .fa-university:before {
            content: "\f19c";
        }

        .fa-graduation-cap:before {
            content: "\f19d";
        }

        .fa-bomb:before {
            content: "\f1e2";
        }
        /*! CSS Used from: https://dev.schoolbright.co/Styles/customize-css/admin_main_style.css */
        .card-summary {
            font-size: 26px;
        }

        span.graph-label {
            font-size: 23px;
        }

        @media (max-width: 999px) {
            .panel-summary {
                min-height: 120px;
                min-width: 140px;
            }

                .panel-summary .panel-heading {
                    height: 120px;
                }

            .card-summary {
                font-size: 16px;
            }

                .card-summary .huge {
                    font-size: 32px;
                }

                .card-summary .card-summary-icon {
                    font-size: 3em;
                }
        }

        @media (min-width: 1000px) and (max-width: 1099px) {
            .card-summary {
                font-size: 16px;
            }

                .card-summary .huge {
                    font-size: 36px;
                }

                .card-summary .card-summary-icon {
                    font-size: 3em;
                }
        }

        @media (min-width: 1100px) and (max-width: 1199px) {
            .card-summary {
                font-size: 18px;
            }

                .card-summary .huge {
                    font-size: 36px;
                }

                .card-summary .card-summary-icon {
                    font-size: 3.5em;
                }
        }

        @media (min-width: 1200px) and (max-width:1299px) {
            .card-summary {
                font-size: 22px;
            }

                .card-summary .huge {
                    font-size: 36px;
                }

                .card-summary .card-summary-icon {
                    font-size: 4em;
                }
        }

        @media (min-width: 1300px) {
            .card-summary {
                font-size: 22px;
            }

                .card-summary .huge {
                    font-size: 50px;
                }

                .card-summary .card-summary-icon {
                    font-size: 4.5em;
                }
        }
        /*! CSS Used from: https://use.fontawesome.com/releases/v5.8.2/css/all.css */
        .fa, .far, .fas {
            -moz-osx-font-smoothing: grayscale;
            -webkit-font-smoothing: antialiased;
            display: inline-block;
            font-style: normal;
            font-variant: normal;
            text-rendering: auto;
            line-height: 1;
        }

        .fa-5x {
            font-size: 5em;
        }

        .fa-fw {
            text-align: center;
            width: 1.25em;
        }

        .fa-bell:before {
            content: "\f0f3";
        }

        .fa-bomb:before {
            content: "\f1e2";
        }

        .fa-calendar-alt:before {
            content: "\f073";
        }

        .fa-check:before {
            content: "\f00c";
        }

        .fa-credit-card:before {
            content: "\f09d";
        }

        .fa-graduation-cap:before {
            content: "\f19d";
        }

        .fa-save:before {
            content: "\f0c7";
        }

        .fa-star:before {
            content: "\f005";
        }

        .fa-university:before {
            content: "\f19c";
        }

        .far {
            font-weight: 400;
        }

        .fa, .far, .fas {
            font-family: "Font Awesome 5 Free";
        }

        .fa, .fas {
            font-weight: 900;
        }
        /*! CSS Used from: Embedded */
        .textInDiv {
            font-size: 26px;
        }

        .col-lg-1.left {
            padding: 0px;
        }

        .word-wrap {
            white-space: pre-wrap;
            white-space: -moz-pre-wrap;
            white-space: -pre-wrap;
            white-space: -o-pre-wrap;
            word-wrap: break-word;
        }
        /*! CSS Used from: Embedded */
        #calendar {
            max-width: 900px;
            margin: 0 auto;
            font-size: 1.2em;
        }
        /*! CSS Used from: Embedded */
        .notification-li .dropdown-item {
            display: block;
            width: 100%;
            padding: .25rem 1.5rem .25rem 0.8rem;
            clear: both;
            font-weight: 400;
            color: #3a3b45;
            white-space: nowrap;
            background-color: transparent;
            border: 0;
            text-decoration: none;
            position: relative;
        }

            .notification-li .dropdown-item .dropdown-list-image {
                position: absolute;
                top: 5px;
            }

        .notification-li .align-items-center {
            align-items: center !important;
        }

        .notification-li .d-flex {
            display: flex !important;
        }

        .notification-li .align-items-center {
            align-items: center !important;
        }

        .notification-li .mr-3 {
            margin-right: 1rem !important;
            margin-left: -3px;
        }

        .notification-li .font-weight-bold {
            font-weight: 700 !important;
        }

        .notification-li .text-truncate {
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
            max-width: 34rem;
        }
    </style>

    <style type="text/css">
        .textInDiv {
            font-size: 18px;
        }

            .textInDiv .row div {
                padding: 5px 15px 5px 15px;
            }

        .col-lg-1.left {
            padding: 0px;
        }

        .word-wrap {
            white-space: pre-wrap; /* CSS3 */
            white-space: -moz-pre-wrap; /* Firefox */
            white-space: -pre-wrap; /* Opera <7 */
            white-space: -o-pre-wrap; /* Opera 7 */
            word-wrap: break-word; /* IE */
        }
    </style>

    <style type="text/css">
        #calendar {
            max-width: 900px;
            margin: 0 auto;
            font-size: 1.2em;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Custom Theme JavaScript -->
    <div class="full-card">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12" style="margin-top: -57px; margin-bottom: 10px;">
                <h1>
                    <asp:Literal ID="ltrSchoolName" runat="server" />
                </h1>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-4 col-md-4 col-sm-4">
                <div class="panel panel-primary panel-summary">
                    <div class="panel-heading">
                        <div class="row card-item card-summary">
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                                <i class="material-icons card-summary-icon">school</i>
                            </div>
                            <div class="col-xs-9 col-sm-9 col-md-9 col-lg-9 text-right">
                                <div class="huge status">
                                    <span class='text-large' id="status01" runat="server">0</span>
                                </div>
                                <div><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101321") %></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4">
                <div class="panel panel-green panel-summary">
                    <div class="panel-heading">
                        <div class="row card-item card-summary">
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                                <i class="material-icons card-summary-icon">account_balance</i>
                            </div>
                            <div class="col-xs-9 col-sm-9 col-md-9 col-lg-9 text-right">
                                <div class="huge status">
                                    <span class='text-large' id="status02" runat="server">0</span>
                                </div>
                                <div><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00451") %></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4">
                <div class="panel panel-yellow panel-summary">
                    <div class="panel-heading">
                        <div class="row card-item card-summary">
                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                                <i class="material-icons card-summary-icon">equalizer</i>
                            </div>
                            <div class="col-xs-9 col-sm-9 col-md-9 col-lg-9 text-right">
                                <div class="huge status">
                                    <span class='text-large' id="valuescore">A+</span>
                                </div>
                                <div><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131030") %></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <i class="material-icons">star</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01309") %>
                </div>
                <!-- /.panel-heading -->
                <div class="row">
                    <div class="col-lg-11">

                        <div class="panel-body h2 textInDiv">
                            <%--<div class="row">
                        <div class="col-lg-3 right">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701029") %> :
                        </div>
                        <div class="col-lg-2 left">
                            
                        </div>
                        <div class="col-lg-1">
                            
                        </div>
                        <div class="col-lg-3 right">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701017") %> :
                        </div>
                        <div class="col-lg-2 left">
                            <asp:Label runat="server" ID="txtMaxBehavior"></asp:Label>   
                        </div>
                        <div class="col-lg-1">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %>
                        </div>
                    </div>--%>
                            <div class="row">
                                <div class="col-lg-3 right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02128") %> :
                                </div>
                                <div class="col-lg-2 left">
                                    9:30
                                </div>
                                <div class="col-lg-1">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131031") %>
                                </div>
                                <div class="col-lg-3 right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M803039") %> :
                                </div>
                                <div class="col-lg-2 left">
                                    10:30
                                </div>
                                <div class="col-lg-1">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131031") %>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-3 right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01070") %> :
                                </div>
                                <div class="col-lg-2 left">
                                    <asp:Label runat="server" ID="schoolYear"></asp:Label>
                                </div>
                                <div class="col-lg-1">
                                </div>
                                <div class="col-lg-3 right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01301") %> :
                                </div>
                                <div class="col-lg-2 left">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210456") %>
                                    <asp:Label runat="server" ID="schoolTerm"></asp:Label>
                                </div>
                                <div class="col-lg-1">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-3 right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01990") %> :
                                </div>
                                <div class="col-lg-2 left">
                                    <asp:Label runat="server" ID="dateTermStart"></asp:Label>
                                </div>
                                <div class="col-lg-1">
                                </div>
                                <div class="col-lg-3 right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01994") %> :
                                </div>
                                <div class="col-lg-2 left">
                                    <asp:Label runat="server" ID="dateTermEnd"></asp:Label>
                                </div>
                                <div class="col-lg-1">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-3 right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01998") %> :
                                </div>
                                <div class="col-lg-2 left">
                                    <asp:Label runat="server" ID="countAllHoliday"></asp:Label>
                                </div>
                                <div class="col-lg-1">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %>
                                </div>
                                <div class="col-lg-3 right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00313") %> :
                                </div>
                                <div class="col-lg-2 left">
                                    <asp:Label runat="server" ID="txtMaxBehavior"></asp:Label>
                                </div>
                                <div class="col-lg-1">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>


    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <i class="material-icons">insert_chart_outlined</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02096") %>
                </div>
                <!-- /.panel-heading -->
                <div class="row">
                    <div class="col-lg-11">

                        <div class="panel-body h2 textInDiv">
                            <div class="row">
                                <div class="col-lg-3 right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101031") %> :
                                </div>
                                <div class="col-lg-1 left">
                                </div>
                                <div class="col-lg-1 left">
                                    <%= StudentStatus.Where(w=>w.Status == 0).Sum(s=> s.Count) %>
                                </div>
                                <div class="col-lg-1">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>
                                </div>
                                <div class="col-lg-3 right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01849") %> :
                                </div>
                                <div class="col-lg-1 left">
                                </div>
                                <div class="col-lg-1 left">
                                    <%= StudentStatus.Where(w=>w.Status == 3).Sum(s=> s.Count) %>
                                </div>
                                <div class="col-lg-1">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-3 right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101033") %> :
                                </div>
                                <div class="col-lg-1 left">
                                </div>
                                <div class="col-lg-1 left">
                                    <%= StudentStatus.Where(w=>w.Status == 1).Sum(s=> s.Count) %>
                                </div>
                                <div class="col-lg-1">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>
                                </div>
                                <div class="col-lg-3 right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101035") %> :
                                </div>
                                <div class="col-lg-1 left">
                                </div>
                                <div class="col-lg-1 left">
                                    <%= StudentStatus.Where(w=>w.Status == 5).Sum(s=> s.Count) %>
                                </div>
                                <div class="col-lg-1">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-3 right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101032") %> :
                                </div>
                                <div class="col-lg-1 left">
                                </div>
                                <div class="col-lg-1 left">
                                    <%= StudentStatus.Where(w=>w.Status == 2).Sum(s=> s.Count) %>
                                </div>
                                <div class="col-lg-1">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>
                                </div>
                                <div class="col-lg-3 right">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01166") %> :
                                </div>
                                <div class="col-lg-1 left">
                                </div>
                                <div class="col-lg-1 left">
                                    <%= StudentStatus.Where(w=>w.Status == 6).Sum(s=> s.Count) %>
                                </div>
                                <div class="col-lg-1">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %>
                                </div>
                            </div>
                        </div>


                    </div>
                </div>




            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-8">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <i class="material-icons">date_range</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00990") %>
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body">
                    <div id='calendar'></div>

                    <div id="myModal" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title" style="font-size: 180%"><span id="eventTitle"></span></h4>
                                </div>
                                <div class="modal-body" style="font-size: 180%">
                                    <p id="pDetails"></p>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101118") %></button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->

            <div class="panel panel-default">
                <div class="panel-heading">
                    <i class="material-icons">fact_check</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132129") %>
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body">
                    <div id="morris-area-bar">
                        <table style="/*position: absolute; */ top: 5px; right: 5px; font-size: smaller; color: #545454">
                            <tbody>
                                <tr>
                                    <td class="legendColorBox">
                                        <div style="border: 1px solid #ccc; padding: 1px">
                                            <div style="width: 4px; height: 0; border: 5px solid rgb(13,199,66); overflow: hidden"></div>
                                        </div>
                                    </td>
                                    <td class="legendLabel"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %></td>
                                    <td class="legendColorBox">
                                        <div style="border: 1px solid #ccc; padding: 1px">
                                            <div style="width: 4px; height: 0; border: 5px solid rgb(3,147,199); overflow: hidden"></div>
                                        </div>
                                    </td>
                                    <td class="legendLabel"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %></td>
                                    <td class="legendColorBox">
                                        <div style="border: 1px solid #ccc; padding: 1px">
                                            <div style="width: 4px; height: 0; border: 5px solid rgb(228,146,47); overflow: hidden"></div>
                                        </div>
                                    </td>
                                    <td class="legendLabel"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %></td>
                                    <td class="legendColorBox">
                                        <div style="border: 1px solid #ccc; padding: 1px">
                                            <div style="width: 4px; height: 0; border: 5px solid rgb(255,0,254); overflow: hidden"></div>
                                        </div>
                                    </td>
                                    <td class="legendLabel"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %></td>
                                    <td class="legendColorBox">
                                        <div style="border: 1px solid #ccc; padding: 1px">
                                            <div style="width: 4px; height: 0; border: 5px solid rgb(152,80,224); overflow: hidden"></div>
                                        </div>
                                    </td>
                                    <td class="legendLabel"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %></td>
                                    <td class="legendColorBox">
                                        <div style="border: 1px solid #ccc; padding: 1px">
                                            <div style="width: 4px; height: 0; border: 5px solid rgb(255,57,66); overflow: hidden"></div>

                                        </div>
                                    </td>
                                    <td class="legendLabel"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %></td>
                                    <td class="legendColorBox">
                                        <div style="border: 1px solid #ccc; padding: 1px">
                                            <div style="width: 4px; height: 0; border: 5px solid #e8e8e8; overflow: hidden"></div>
                                        </div>
                                    </td>
                                    <td class="legendLabel"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01365") %></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 center">
                        <div class="flot-chart">
                            <div class="flot-chart-content" id="flot-pie-chart"></div>
                        </div>
                        <span class="graph-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405014") %></span>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 center">
                        <div class="flot-chart">
                            <div class="flot-chart-content" id="flot-pie-chart2"></div>
                        </div>
                        <span class="graph-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %></span>
                    </div>
                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->

            <div class="panel panel-default">
                <div class="panel-heading">
                    <i class="material-icons">price_check</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02095") %>
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body">
                    <div id="morris-area-chart">
                    </div>
                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->

            <div class="panel panel-default">
                <div class="panel-heading">
                    <i class="material-icons">checklist</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701017") %>
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body">
                    <table class="table table-section" style="font-size: 24px;">
                        <thead>
                            <tr>
                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %> -<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></th>
                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></th>
                                <th><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203034") %></th>
                            </tr>
                        </thead>
                        <tbody>
                            <% var q_data = topScores();
                                foreach (var data in q_data)
                                {%>
                            <tr>
                                <td><%= data._name %></td>
                                <td><%= data._class %></td>
                                <td><%= data.Score %></td>
                            </tr>
                            <%}
                            %>
                        </tbody>
                    </table>
                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->

            <div class="panel panel-default hidden">
                <div class="panel-heading">
                    <i class="fa fa-clock-o fa-fw"></i>Responsive Timeline
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body">
                    <ul class="timeline">
                        <li>
                            <div class="timeline-badge">
                                <i class="fa fa-check"></i>
                            </div>
                            <div class="timeline-panel">
                                <div class="timeline-heading">
                                    <h4 class="timeline-title">Lorem ipsum dolor</h4>
                                    <p>
                                        <small class="text-muted"><i class="fa fa-clock-o"></i>11 hours ago via Twitter</small>
                                    </p>
                                </div>
                                <div class="timeline-body">
                                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Libero laboriosam dolor perspiciatis omnis exercitationem. Beatae, officia pariatur? Est cum veniam excepturi. Maiores praesentium, porro voluptas suscipit facere rem dicta, debitis.</p>
                                </div>
                            </div>
                        </li>
                        <li class="timeline-inverted">
                            <div class="timeline-badge warning">
                                <i class="fa fa-credit-card"></i>
                            </div>
                            <div class="timeline-panel">
                                <div class="timeline-heading">
                                    <h4 class="timeline-title">Lorem ipsum dolor</h4>
                                </div>
                                <div class="timeline-body">
                                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Autem dolorem quibusdam, tenetur commodi provident cumque magni voluptatem libero, quis rerum. Fugiat esse debitis optio, tempore. Animi officiis alias, officia repellendus.</p>
                                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Laudantium maiores odit qui est tempora eos, nostrum provident explicabo dignissimos debitis vel! Adipisci eius voluptates, ad aut recusandae minus eaque facere.</p>
                                </div>
                            </div>
                        </li>
                        <li>
                            <div class="timeline-badge danger">
                                <i class="fa fa-bomb"></i>
                            </div>
                            <div class="timeline-panel">
                                <div class="timeline-heading">
                                    <h4 class="timeline-title">Lorem ipsum dolor</h4>
                                </div>
                                <div class="timeline-body">
                                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Repellendus numquam facilis enim eaque, tenetur nam id qui vel velit similique nihil iure molestias aliquam, voluptatem totam quaerat, magni commodi quisquam.</p>
                                </div>
                            </div>
                        </li>
                        <li class="timeline-inverted">
                            <div class="timeline-panel">
                                <div class="timeline-heading">
                                    <h4 class="timeline-title">Lorem ipsum dolor</h4>
                                </div>
                                <div class="timeline-body">
                                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptates est quaerat asperiores sapiente, eligendi, nihil. Itaque quos, alias sapiente rerum quas odit! Aperiam officiis quidem delectus libero, omnis ut debitis!</p>
                                </div>
                            </div>
                        </li>
                        <li>
                            <div class="timeline-badge info">
                                <i class="fa fa-save"></i>
                            </div>
                            <div class="timeline-panel">
                                <div class="timeline-heading">
                                    <h4 class="timeline-title">Lorem ipsum dolor</h4>
                                </div>
                                <div class="timeline-body">
                                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Nobis minus modi quam ipsum alias at est molestiae excepturi delectus nesciunt, quibusdam debitis amet, beatae consequuntur impedit nulla qui! Laborum, atque.</p>
                                    <hr>
                                    <div class="btn-group">
                                        <button type="button" class="btn btn-primary btn-sm dropdown-toggle" data-toggle="dropdown">
                                            <i class="fa fa-gear"></i><span class="caret"></span>
                                        </button>
                                        <ul class="dropdown-menu" role="menu">
                                            <li>
                                                <a href="#">Action</a>
                                            </li>
                                            <li>
                                                <a href="#">Another action</a>
                                            </li>
                                            <li>
                                                <a href="#">Something else here</a>
                                            </li>
                                            <li class="divider"></li>
                                            <li>
                                                <a href="#">Separated link</a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </li>
                        <li>
                            <div class="timeline-panel">
                                <div class="timeline-heading">
                                    <h4 class="timeline-title">Lorem ipsum dolor</h4>
                                </div>
                                <div class="timeline-body">
                                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Sequi fuga odio quibusdam. Iure expedita, incidunt unde quis nam! Quod, quisquam. Officia quam qui adipisci quas consequuntur nostrum sequi. Consequuntur, commodi.</p>
                                </div>
                            </div>
                        </li>
                        <li class="timeline-inverted">
                            <div class="timeline-badge success">
                                <i class="fa fa-graduation-cap"></i>
                            </div>
                            <div class="timeline-panel">
                                <div class="timeline-heading">
                                    <h4 class="timeline-title">Lorem ipsum dolor</h4>
                                </div>
                                <div class="timeline-body">
                                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Deserunt obcaecati, quaerat tempore officia voluptas debitis consectetur culpa amet, accusamus dolorum fugiat, animi dicta aperiam, enim incidunt quisquam maxime neque eaque.</p>
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->
        </div>
        <!-- /.col-lg-8 -->
        <div class="col-lg-4">
            <div class="panel panel-default notificastion-panel">
                <div class="panel-heading">
                    <i class="material-icons">notifications</i> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00231") %>
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body" style="height: 691px; overflow-y: auto; margin-bottom: 10px;">
                    <div class="list-group notification-li">
                        <asp:Literal ID="ltrMessageSystem" runat="server"></asp:Literal>
                    </div>
                    <!-- /.list-group -->
                </div>
                <div style="text-align: center;">
                    <a href="UpdateLog.aspx" class="btn btn-link" style="text-decoration: none"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00648") %></a>
                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->
        </div>
        <!-- /.col-lg-4 -->
    </div>
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true">
    </asp:ScriptManager>--%>

    <div style="text-align: center; margin-left: 5px; display: none;">
        <asp:Label ID="ltrLink" runat="server"></asp:Label>
    </div>
    <br />
    <canvas id="myChart2"></canvas>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <link href="/Scripts/FullCalendar/fullcalendar.min.css" rel='stylesheet' />
    <link href="/Scripts/FullCalendar/fullcalendar.print.min.css" rel='stylesheet' media='print' />
    <script src="/Scripts/FullCalendar/moment.min.js" type="text/javascript"></script>
    <script src="/Scripts/FullCalendar/fullcalendar.js" type="text/javascript"></script>

    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.pie.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.resize.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot/jquery.flot.time.js" type="text/javascript"></script>
    <script src="/bootstrap SB2/bower_components/flot.tooltip/js/jquery.flot.tooltip.min.js" type="text/javascript"></script>

    <script src="/Scripts/less-1.5.1.js" type="text/javascript"></script>
    <script src="/javascript/highcharts.js" type="text/javascript"></script>
    <script src="/javascript/exporting.js" type="text/javascript"></script>

    <script src="/Scripts/Chart.js" type="text/javascript"></script>
    <script lang="javascript" type="text/javascript">

        function GenerateCalender(events) {
            $('#calendar').fullCalendar({
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,basicWeek'
                },
                buttonText: {
                    today: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131028") %>',
                    month: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105011") %>',
                    week: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206330") %>'
                },
                locale: 'th',
                defaultDate: new Date(),
                navLinks: true, // can click day/week names to navigate views
                editable: true,
                eventLimit: true, // allow "more" link when too many events
                events: events,
                eventClick: function (calEvent, jsEvent, view) {
                    $('#myModal #eventTitle').text(calEvent.title);

                    var $description = $('<div/>');
                    $description.append($('<p/>').html('<b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131025") %>: </b>' + calEvent.txtStart));
                    if (calEvent.end != null) {
                        $description.append($('<p/>').html('<b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132130") %>: </b>' + calEvent.txtEnd));
                    }
                    $description.append($('<p/>').html('<b><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132131") %>: </b>' + calEvent.description));
                    $('#myModal #pDetails').empty().html($description);

                    $('#myModal').modal();
                }
            });
        };

        $(document).ready(function () {
            //ShowDisplyLed("0 2"); 

            chart();

            //var ctx = document.getElementById("myChart2").getContext('2d');
            //var myChart = new Chart(ctx, {
            //    type: 'pie',
            //    data: {
            //        labels: ["M", "T", "W", "T", "F", "S", "S"],
            //        datasets: [{
            //            backgroundColor: [
            //                "#2ecc71",
            //                "#3498db",
            //                "#95a5a6",
            //                "#9b59b6",
            //                "#f1c40f",
            //                "#e74c3c",
            //                "#34495e"
            //            ],
            //            data: [12, 19, 3, 17, 28, 24, 7]
            //        }]
            //    }
            //});

            $.ajax({
                type: "POST",
                url: "AdminMain.aspx/GetEvents",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (doc) {

                    var events = [];
                    var obj = $.parseJSON(doc.d);  //.net returns json wrapped in "d"
                    for (var i = 0; i < obj.length; i++) {

                        events.push({
                            title: obj[i]['Subject'],
                            description: obj[i]['Description'],
                            txtStart: obj[i]['txtStart'],
                            txtEnd: obj[i]['txtEnd'],
                            start: moment(obj[i]['start']),
                            end: obj[i]['end'] != null ? moment(obj[i]['end']) : null,
                            color: obj[i]['ThemeColor'],
                            allDay: true
                        });
                    }

                    GenerateCalender(events);
                },
                error: function (error) {
                    alert('failed');
                }
            });

            $.getJSON('https://api.ipify.org/?format=json', function (data) {
                console.log(data.ip);
            });

        });

        const mask = () => {
        }
        function chart() {

            var data = [];

        <% 
        string StatusDesc = "Online", Message = "";
        int StatusCode = 200;
        StatusDesc = tb_Server.Status ?? true ? "Online" : "Offline";
        Message = tb_Server.Message;
        StatusCode = tb_Server.Status ?? true ? 200 : 400;

        string Status = "{" + "\"Status\" : \"" + StatusDesc + "\", \"StatusCode\" : " + StatusCode + ", \"Message\" : `" + Message + "` " + "}";
        %>

            let dataServer = <%= Status %>;
            if (dataServer.StatusCode !== 200) {
                $("#modalpopupsystem").modal('show');
                $("#modalpopupsystem #modalpopupdata-content").html(dataServer.Message);
            }

            $.get("/App_Logic/Report/ReportCome2Schoolteacher.ashx?mode=reportscome2school01teacher", "",
                function (Obj) {
                }).done(function (Obj) {
                    $.each(Obj, function (index) {
                        data = [{
                            label: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>",
                            data: Obj[index].status_0,
                            color: "#0dc742"
                        }, {
                            label: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %>",
                            data: 0,
                            color: "#0393c7"
                        }, {
                            label: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %>",
                            data: Obj[index].status_1,
                            color: "#e4922f"
                        }, {
                            label: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %>",
                            data: 0,
                            //color: "#9850e0"
                            color: "#ff00fe"
                        }, {
                            label: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %>",
                            data: 0,
                            color: "#9850e0"
                        }, {
                            label: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %>",
                            data: Obj[index].status_2,
                            color: "#ff3942"
                        }];
                    });

                    $.plot($("#flot-pie-chart"), data, {
                        series: {
                            pie: {
                                show: true,
                                radius: 8 / 10,
                                label: {
                                    show: false,
                                    radius: 3 / 4,
                                    background: {
                                        opacity: 0.5
                                    }
                                }
                            }
                        },
                        bars: { show: false },
                        grid: {
                            hoverable: true,
                            labelMargin: 10
                        },
                        legend: { show: false },
                        tooltip: true,
                        tooltipOpts: {
                            content: "%p.2%, %s", // show percentages, rounding to 2 decimal places
                            shifts: {
                                x: 20,
                                y: 0
                            },
                            style: "font-size:30px;",
                            defaultTheme: false
                        }
                    });
                });

            <% var q1 = report02UsersView01().FirstOrDefault();%>

            var _f_0 = <%=q1 ==null?0: q1.female_status_0 + q1.female_status_1 +q1.female_status_5 %>, _f_1 =  <%= q1 ==null?0:q1.female_status_2+q1.female_status_3+q1.female_status_4 %>;
            var _m_0 =  <%=q1 ==null?0: q1.male_status_0 + q1.male_status_1+q1.male_status_5 %>, _m_1 =  <%= q1 ==null?0:q1.male_status_2+q1.male_status_3+q1.male_status_4 %>;
            var _m =  <%= q1 ==null?0:q1.male_status_0 + q1.male_status_1 + q1.male_status_2+q1.male_status_3+q1.male_status_4+q1.male_status_5 %> ;
            var _f =  <%= q1 ==null?0: q1.female_status_0 + q1.female_status_1 + q1.female_status_2+q1.female_status_3+q1.female_status_4+q1.female_status_5 %>;

            data = [
                {
                    label: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301016") %>",
                    data: <%=q1 == null?0: q1.female_status_0 + q1.male_status_0  %>,
                    color: "#0dc742"
                }, {
                    label: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701033") %>",
                    data: <%=q1 == null?0: q1.female_status_1 + q1.male_status_1  %>,
                    color: "#0393c7"
                }, {
                    label: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102211") %>",
                    data: <%=q1 == null?0: q1.female_status_2 + q1.male_status_2  %>,
                    color: "#e4922f"
                }, {
                    label: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102254") %>",
                    data: <%=q1 == null?0: q1.female_status_3 + q1.male_status_3  %>,
                    //color: "#9850e0"
                    color: "#ff00fe"
                }, {
                    label: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102255") %>",
                    data: <%=q1 == null?0: q1.female_status_4 + q1.male_status_4  %>,
                    color: "#9850e0"
                }, {
                    label: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105015") %>",
                    data: <%=q1 == null?0: q1.female_status_5 + q1.male_status_5  %>,
                    color: "#ff3942"
                }, {
                    label: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01365") %>",
                    data: <%=q1 == null?0: q1.female_status_6 + q1.male_status_6  %>,
                    color: "#e8e8e8"
                }];

            $.plot($("#flot-pie-chart2"), data, {
                series: {
                    pie: {
                        show: true,
                        radius: 8 / 10,
                        label: {
                            show: false,
                            radius: 3 / 4,
                            background: {
                                opacity: 0.5
                            }
                        }
                    }
                },
                bars: { show: false },
                grid: {
                    hoverable: true,
                    labelMargin: 10
                },
                legend: { show: false },
                tooltip: true,
                tooltipOpts: {
                    content: "%p.2%, %s", // show percentages, rounding to 2 decimal places
                    shifts: {
                        x: 20,
                        y: 0
                    },
                    style: "font-size:30px;",
                    defaultTheme: false
                }
            });

            //$.ajax({
            //    url: "/App_Logic/dataReportGeneric.ashx?mode=GetReportChart01", // getchart.php
            //    dataType: 'JSON',
            //    type: 'POST',
            //    data: { get_values: true },
            //    success: function (response) {
            //        Morris.Line({
            //            element: 'morris-area-chart',
            //            data: response,
            //            xkey: 'thedate',
            //            ykeys: ['nSumSell', 'nSumAdd'],
            //            labels: ['ยอดการขาย', 'ยอดการเติม'],
            //            xLabelFormat: function (date) {
            //                return date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();
            //            },
            //            lineWidth: 2,
            //            dateFormat: function (date) {
            //                d = new Date(date);
            //                return d.getDate() + '/' + (d.getMonth() + 1) + '/' + d.getFullYear();
            //            }
            //        });
            //    }
            //});
        }

        var status01 = 0;
        var status02 = 0;
        var status03 = 0;
        var statusE = 0;
        var statusU = 0;
        var TotalAll = 0;

        $.ajax({
            type: "POST",
            url: "/App_Logic/dataReportGeneric.ashx?mode=GetReportmobileall",
            cache: false,
            contentType: 'application/json;',
            success: function (obj) {

                $.each(obj, function (index) {
                    status01 += obj[index].ScanStatusE0 + obj[index].ScanStatusU0;
                    status02 += obj[index].ScanStatusE1 + obj[index].ScanStatusU1;
                    status03 += obj[index].ScanStatusE2 + obj[index].ScanStatusU2;
                    TotalAll += obj[index].TotallAllUser + obj[index].TotallAllEmp
                });

                $("#status02").html(statusE);
                $("#status01").html(statusU);
                if (status01 == null) status01 = 0;
                if (status02 == null) status02 = 0;
                if (status03 == null) status03 = 0;

                _numberpage = status01 + status02 + status03;

                var score = 0;
                score += status01 * 3;
                score += status02 * 1;

                var per = score / (TotalAll * 3);
                per = per * 100;

                if (per < 51) {
                    $("#valuescore").html("F")
                }
                else if (per < 50) {
                    $("#valuescore").html("D");
                } else if (per < 70) {
                    $("#valuescore").html("D+");
                } else if (per <= 75) {
                    $("#valuescore").html("C");
                } else if (per <= 80) {
                    $("#valuescore").html("C+");
                } else if (per <= 85) {
                    $("#valuescore").html("B");
                } else if (per <= 90) {
                    $("#valuescore").html("B+");
                } else if (per <= 95) {
                    $("#valuescore").html("A");
                }
                else {
                    $("#valuescore").html("A+");
                }
            }
        });

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
