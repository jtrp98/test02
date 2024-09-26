<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="privacypolicy.aspx.cs" Inherits="FingerprintPayment.privacypolicy" %>

<!DOCTYPE html>


<head>
    <meta http-equiv="Content-Type" content="text/html; charset=windows-874">
    <meta name="ProgId" content="Word.Document">
    <meta name="Generator" content="Microsoft Word 15">
    <meta name="Originator" content="Microsoft Word 15">
    <style>
        <!--
        /* Font Definitions */
        @font-face {
            font-family: "MS Mincho";
            panose-1: 2 2 6 9 4 2 5 8 3 4;
            mso-font-alt: "\FF2D\FF33 \660E\671D";
            mso-font-charset: 128;
            mso-generic-font-family: modern;
            mso-font-pitch: fixed;
            mso-font-signature: -536870145 1791491579 134217746 0 131231 0;
        }

        @font-face {
            font-family: SimSun;
            panose-1: 2 1 6 0 3 1 1 1 1 1;
            mso-font-alt: \5B8B\4F53;
            mso-font-charset: 134;
            mso-generic-font-family: auto;
            mso-font-pitch: variable;
            mso-font-signature: 3 680460288 22 0 262145 0;
        }

        @font-face {
            font-family: "Cordia New";
            panose-1: 2 11 3 4 2 2 2 2 2 4;
            mso-font-charset: 0;
            mso-generic-font-family: swiss;
            mso-font-pitch: variable;
            mso-font-signature: -2130706429 0 0 0 65537 0;
        }

        @font-face {
            font-family: "Cambria Math";
            panose-1: 2 4 5 3 5 4 6 3 2 4;
            mso-font-charset: 0;
            mso-generic-font-family: roman;
            mso-font-pitch: variable;
            mso-font-signature: 3 0 0 0 1 0;
        }

        @font-face {
            font-family: Calibri;
            panose-1: 2 15 5 2 2 2 4 3 2 4;
            mso-font-charset: 0;
            mso-generic-font-family: swiss;
            mso-font-pitch: variable;
            mso-font-signature: -469750017 -1073732485 9 0 511 0;
        }

        @font-face {
            font-family: Meiryo;
            panose-1: 2 11 6 4 3 5 4 4 2 4;
            mso-font-alt: Meiryo;
            mso-font-charset: 128;
            mso-generic-font-family: swiss;
            mso-font-pitch: variable;
            mso-font-signature: -520027393 -355991553 65554 0 131231 0;
        }

        @font-face {
            font-family: "\@Meiryo";
            mso-font-charset: 128;
            mso-generic-font-family: swiss;
            mso-font-pitch: variable;
            mso-font-signature: -520027393 -355991553 65554 0 131231 0;
        }

        @font-face {
            font-family: "\@SimSun";
            panose-1: 2 1 6 0 3 1 1 1 1 1;
            mso-font-charset: 134;
            mso-generic-font-family: auto;
            mso-font-pitch: variable;
            mso-font-signature: 3 680460288 22 0 262145 0;
        }

        @font-face {
            font-family: "\@MS Mincho";
            panose-1: 2 2 6 9 4 2 5 8 3 4;
            mso-font-charset: 128;
            mso-generic-font-family: modern;
            mso-font-pitch: fixed;
            mso-font-signature: -536870145 1791491579 134217746 0 131231 0;
        }
        /* Style Definitions */
        p.MsoNormal, li.MsoNormal, div.MsoNormal {
            mso-style-unhide: no;
            mso-style-qformat: yes;
            mso-style-parent: "";
            margin-top: 0in;
            margin-right: 0in;
            margin-bottom: 10.0pt;
            margin-left: 0in;
            line-height: 115%;
            mso-pagination: widow-orphan;
            font-size: 11.0pt;
            mso-bidi-font-size: 14.0pt;
            font-family: "Calibri",sans-serif;
            mso-ascii-font-family: Calibri;
            mso-ascii-theme-font: minor-latin;
            mso-fareast-font-family: "MS Mincho";
            mso-fareast-theme-font: minor-fareast;
            mso-hansi-font-family: Calibri;
            mso-hansi-theme-font: minor-latin;
            mso-bidi-font-family: "Cordia New";
            mso-bidi-theme-font: minor-bidi;
            mso-fareast-language: JA;
        }

        h3 {
            mso-style-priority: 9;
            mso-style-unhide: no;
            mso-style-qformat: yes;
            mso-style-link: "À—«‡√◊ËÕß 3 Õ—°¢√–";
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            mso-outline-level: 3;
            font-size: 13.5pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
            font-weight: bold;
        }

        h4 {
            mso-style-priority: 9;
            mso-style-unhide: no;
            mso-style-qformat: yes;
            mso-style-link: "À—«‡√◊ËÕß 4 Õ—°¢√–";
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            mso-outline-level: 4;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
            font-weight: bold;
        }

        a:link, span.MsoHyperlink {
            mso-style-priority: 99;
            color: blue;
            text-decoration: underline;
            text-underline: single;
        }

        a:visited, span.MsoHyperlinkFollowed {
            mso-style-noshow: yes;
            mso-style-priority: 99;
            color: purple;
            mso-themecolor: followedhyperlink;
            text-decoration: underline;
            text-underline: single;
        }

        p {
            mso-style-noshow: yes;
            mso-style-priority: 99;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        span.3 {
            mso-style-name: "À—«‡√◊ËÕß 3 Õ—°¢√–";
            mso-style-priority: 9;
            mso-style-unhide: no;
            mso-style-locked: yes;
            mso-style-link: "À—«‡√◊ËÕß 3";
            mso-ansi-font-size: 13.5pt;
            mso-bidi-font-size: 13.5pt;
            font-family: "Times New Roman",serif;
            mso-ascii-font-family: "Times New Roman";
            mso-fareast-font-family: "Times New Roman";
            mso-hansi-font-family: "Times New Roman";
            mso-bidi-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
            font-weight: bold;
        }

        span.4 {
            mso-style-name: "À—«‡√◊ËÕß 4 Õ—°¢√–";
            mso-style-priority: 9;
            mso-style-unhide: no;
            mso-style-locked: yes;
            mso-style-link: "À—«‡√◊ËÕß 4";
            mso-ansi-font-size: 12.0pt;
            mso-bidi-font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-ascii-font-family: "Times New Roman";
            mso-fareast-font-family: "Times New Roman";
            mso-hansi-font-family: "Times New Roman";
            mso-bidi-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
            font-weight: bold;
        }

        p.p2, li.p2, div.p2 {
            mso-style-name: p2;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p3, li.p3, div.p3 {
            mso-style-name: p3;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p4, li.p4, div.p4 {
            mso-style-name: p4;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p5, li.p5, div.p5 {
            mso-style-name: p5;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        span.apple-converted-space {
            mso-style-name: apple-converted-space;
            mso-style-unhide: no;
        }

        span.ng-scope {
            mso-style-name: ng-scope;
            mso-style-unhide: no;
        }

        p.p6, li.p6, div.p6 {
            mso-style-name: p6;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p7, li.p7, div.p7 {
            mso-style-name: p7;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p8, li.p8, div.p8 {
            mso-style-name: p8;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p9, li.p9, div.p9 {
            mso-style-name: p9;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p10, li.p10, div.p10 {
            mso-style-name: p10;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p11, li.p11, div.p11 {
            mso-style-name: p11;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p12, li.p12, div.p12 {
            mso-style-name: p12;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p13, li.p13, div.p13 {
            mso-style-name: p13;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        span.ft1 {
            mso-style-name: ft1;
            mso-style-unhide: no;
        }

        p.p14, li.p14, div.p14 {
            mso-style-name: p14;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p1, li.p1, div.p1 {
            mso-style-name: p1;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p15, li.p15, div.p15 {
            mso-style-name: p15;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        span.ft14 {
            mso-style-name: ft14;
            mso-style-unhide: no;
        }

        p.p16, li.p16, div.p16 {
            mso-style-name: p16;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p17, li.p17, div.p17 {
            mso-style-name: p17;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        span.ft15 {
            mso-style-name: ft15;
            mso-style-unhide: no;
        }

        p.p18, li.p18, div.p18 {
            mso-style-name: p18;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p19, li.p19, div.p19 {
            mso-style-name: p19;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        span.ft17 {
            mso-style-name: ft17;
            mso-style-unhide: no;
        }

        p.p20, li.p20, div.p20 {
            mso-style-name: p20;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p21, li.p21, div.p21 {
            mso-style-name: p21;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p22, li.p22, div.p22 {
            mso-style-name: p22;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p23, li.p23, div.p23 {
            mso-style-name: p23;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p24, li.p24, div.p24 {
            mso-style-name: p24;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        span.ft3 {
            mso-style-name: ft3;
            mso-style-unhide: no;
        }

        p.p26, li.p26, div.p26 {
            mso-style-name: p26;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p27, li.p27, div.p27 {
            mso-style-name: p27;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p28, li.p28, div.p28 {
            mso-style-name: p28;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p29, li.p29, div.p29 {
            mso-style-name: p29;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p30, li.p30, div.p30 {
            mso-style-name: p30;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        span.ft23 {
            mso-style-name: ft23;
            mso-style-unhide: no;
        }

        p.p31, li.p31, div.p31 {
            mso-style-name: p31;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        span.ft24 {
            mso-style-name: ft24;
            mso-style-unhide: no;
        }

        p.p32, li.p32, div.p32 {
            mso-style-name: p32;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        span.ft25 {
            mso-style-name: ft25;
            mso-style-unhide: no;
        }

        p.p33, li.p33, div.p33 {
            mso-style-name: p33;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p35, li.p35, div.p35 {
            mso-style-name: p35;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p36, li.p36, div.p36 {
            mso-style-name: p36;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p37, li.p37, div.p37 {
            mso-style-name: p37;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p38, li.p38, div.p38 {
            mso-style-name: p38;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p39, li.p39, div.p39 {
            mso-style-name: p39;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.list-item, li.list-item, div.list-item {
            mso-style-name: list-item;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.ng-scope1, li.ng-scope1, div.ng-scope1 {
            mso-style-name: ng-scope1;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p41, li.p41, div.p41 {
            mso-style-name: p41;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        span.ft27 {
            mso-style-name: ft27;
            mso-style-unhide: no;
        }

        p.p42, li.p42, div.p42 {
            mso-style-name: p42;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p43, li.p43, div.p43 {
            mso-style-name: p43;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p44, li.p44, div.p44 {
            mso-style-name: p44;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p45, li.p45, div.p45 {
            mso-style-name: p45;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p46, li.p46, div.p46 {
            mso-style-name: p46;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p47, li.p47, div.p47 {
            mso-style-name: p47;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p48, li.p48, div.p48 {
            mso-style-name: p48;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p51, li.p51, div.p51 {
            mso-style-name: p51;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p52, li.p52, div.p52 {
            mso-style-name: p52;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p53, li.p53, div.p53 {
            mso-style-name: p53;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        p.p54, li.p54, div.p54 {
            mso-style-name: p54;
            mso-style-unhide: no;
            mso-margin-top-alt: auto;
            margin-right: 0in;
            mso-margin-bottom-alt: auto;
            margin-left: 0in;
            mso-pagination: widow-orphan;
            font-size: 12.0pt;
            font-family: "Times New Roman",serif;
            mso-fareast-font-family: "Times New Roman";
            mso-fareast-language: ZH-CN;
        }

        span.SpellE {
            mso-style-name: "";
            mso-spl-e: yes;
        }

        span.GramE {
            mso-style-name: "";
            mso-gram-e: yes;
        }

        .MsoChpDefault {
            mso-style-type: export-only;
            mso-default-props: yes;
            font-family: "Calibri",sans-serif;
            mso-ascii-font-family: Calibri;
            mso-ascii-theme-font: minor-latin;
            mso-fareast-font-family: "MS Mincho";
            mso-fareast-theme-font: minor-fareast;
            mso-hansi-font-family: Calibri;
            mso-hansi-theme-font: minor-latin;
            mso-bidi-font-family: "Cordia New";
            mso-bidi-theme-font: minor-bidi;
            mso-fareast-language: JA;
        }

        .MsoPapDefault {
            mso-style-type: export-only;
            margin-bottom: 10.0pt;
            line-height: 115%;
        }

        @page WordSection1 {
            size: 8.5in 11.0in;
            margin: 1.0in 1.0in 1.0in 1.0in;
            mso-header-margin: .5in;
            mso-footer-margin: .5in;
            mso-paper-source: 0;
        }

        div.WordSection1 {
            page: WordSection1;
        }
        -->
    </style>
    <!--[if gte mso 10]>
<style>
 /* Style Definitions */
 table.MsoNormalTable
	{mso-style-name:µ“√“ßª°µ‘;
	mso-tstyle-rowband-size:0;
	mso-tstyle-colband-size:0;
	mso-style-noshow:yes;
	mso-style-priority:99;
	mso-style-parent:"";
	mso-padding-alt:0in 5.4pt 0in 5.4pt;
	mso-para-margin-top:0in;
	mso-para-margin-right:0in;
	mso-para-margin-bottom:10.0pt;
	mso-para-margin-left:0in;
	line-height:115%;
	mso-pagination:widow-orphan;
	font-size:11.0pt;
	mso-bidi-font-size:14.0pt;
	font-family:"Calibri",sans-serif;
	mso-ascii-font-family:Calibri;
	mso-ascii-theme-font:minor-latin;
	mso-hansi-font-family:Calibri;
	mso-hansi-theme-font:minor-latin;
	mso-bidi-font-family:"Cordia New";
	mso-bidi-theme-font:minor-bidi;
	mso-fareast-language:JA;}
</style>
<![endif]-->
    <!--[if gte mso 9]><xml>
 <o:shapedefaults v:ext="edit" spidmax="1026"/>
</xml><![endif]-->
    <!--[if gte mso 9]><xml>
 <o:shapelayout v:ext="edit">
  <o:idmap v:ext="edit" data="1"/>
 </o:shapelayout></xml><![endif]-->
</head>

<body lang="EN-US" link="blue" vlink="purple" style='tab-interval: .5in; word-wrap: break-word'>

    <div class="WordSection1">

        <p class="MsoNormal" style='margin-bottom: 12.0pt; line-height: normal; mso-outline-level: 3; background: white'>
            <b><span style='font-size: 9.0pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; text-transform: uppercase; letter-spacing: 1.5pt; mso-fareast-language: ZH-CN'>JABJAI
CORPORATION CO., LTD. PRIVACY POLICY</span></b><b><span style='font-size: 9.0pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; text-transform: uppercase; letter-spacing: 1.5pt; mso-fareast-language: ZH-CN'><o:p></o:p></span></b>
        </p>

        <p class="MsoNormal" style='mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; line-height: normal; background: white'>
            <b><span style='font-size: 9.0pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>Data Policy</span></b><b><span
                style='font-size: 9.0pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span></b>
        </p>

        <p class="MsoNormal" style='mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>This policy describes the
information we process to support School Bright, School Bright Grade, School
Bright Shop, SB Exam and other product features offered by JABJAI CORPORATION
CO., LTD. (<span lang="ZH-CN">ì</span>School Bright<span lang="ZH-CN">î</span></span><span
    style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-fareast-font-family: SimSun; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>,</span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'> ìDeveloperî, <span
        lang="ZH-CN">ì</span>we<span lang="ZH-CN">î</span></span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-fareast-font-family: SimSun; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>,</span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'> and <span lang="ZH-CN">ì</span>us<span
            lang="ZH-CN">î</span>). We respect the privacy of its users (<span lang="ZH-CN">ì</span>you<span
                lang="ZH-CN">î</span>) and has developed this Privacy Policy to demonstrate its
commitment to protecting your privacy. This Privacy Policy describes the
information we collect, how that information may be used, with whom it may be
shared, and your choices about such uses and disclosures. We encourage you to
read this Privacy Policy carefully when using our application or services or
transacting business with us. By using our website, application or other online
services (our <span lang="ZH-CN">ì</span>Service<span lang="ZH-CN">î</span>), you
are accepting the practices described in this Privacy Policy.</span><span
    style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>If you have any questions
about our privacy practices, please refer to the end of this Privacy Policy for
information on how to contact us.</span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; line-height: normal; background: white'>
            <b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>Information we
collect about you</span></b><b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span></b>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; line-height: normal; background: white'>
            <b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>In General.</span></b><span
                style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>&nbsp;We may collect Personal Information, including Sensitive Data, and
other information. <span lang="ZH-CN">ì</span>Personal Information<span
    lang="ZH-CN">î</span> means individually identifiable information that would
allow us to determine the actual identity of, and contact, a specific living
person. Sensitive Data includes information, comments or content (<span
    class="GramE">e.g.</span> photographs, video, profile, lifestyle) that you
optionally provide that may reveal your ethnic origin, nationality, religion
and/or sexual orientation. By providing Sensitive Data to us, you consent to
the collection, use and disclosure of Sensitive Data as permitted by applicable
privacy laws. We may also collect your geolocation information with your
consent. We may collect this information through a website, mobile application,
or other online services. By using the Service, you are authorizing us to
gather, parse and retain data related to the provision of the Service. When you
provide personal information through our Service, the information may be sent
to servers located in the United States and countries around the world.</span><span
    style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; margin-left: 24.0pt; line-height: normal; background: white'>
            <b><span
                style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>Information you provide.</span></b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>&nbsp;In order
to register as a user with School Bright, you will be asked to sign in using
your school login ID. If you do so, you authorize us to access certain account
information, such as your name, school, student ID, academic records, your
email address, gender, birthday, education history, current city, photos,
personal description. In addition, we may collect and store any personal
information you provide while using our Service or in some other manner. This
may include identifying information, such as your name, address, email address
and telephone number, and, if you transact business with us, financial
information. You may also provide us photos, a personal description and
information about your gender and preferences for recommendations, such as
search distance, age range and gender. If you chat with other School Bright
users, you provide us the content of your chats, and if you contact us with a
customer service or other inquiry, you provide us with the content of that
communication.</span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; margin-left: 24.0pt; line-height: normal; background: white'>
            <b><span
                style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>Use of technologies to collect information.</span></b><span
                    style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>&nbsp;We use various technologies to collect information from your
device and about your activities on our Service.</span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; margin-left: 24.0pt; line-height: normal; background: white'>
            <b><span
                style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>Information collected automatically.</span></b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>&nbsp;We
automatically collect information from your browser or device when you visit
our Service. This information could include your IP address, device ID and
type, your browser type and language, the operating system used by your device,
access times, your mobile device<span lang="ZH-CN">í</span>s geographic location
while our application is actively running, and the referring website address.</span><span
    style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; margin-left: 24.0pt; line-height: normal; background: white'>
            <b><span
                style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>Cookies and use of cookie and similar data.</span></b><span
                    style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>&nbsp;When you visit our Service, we may assign your device one or more
cookies or other technologies that facilitate personalization to facilitate
access to our Service and to personalize your experience. Through the use of a
cookie, we also may automatically collect information about your activity on
our Service, such as the pages you visit, the time and date of your visits and
the links you click. If we advertise, we (or third parties) may use certain
data collected on our Service to show you School Bright advertisements on other
sites or applications.</span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; margin-left: 24.0pt; line-height: normal; background: white'>
            <b><span
                style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>Pixel tags.</span></b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>&nbsp;We embed pixel tags (also called web beacons
or clear GIFs) on web pages, ads, and emails. These tiny, invisible graphics
are used to access cookies and track user activities (such as how many times a
page is viewed). We use pixel tags to measure the popularity of our features
and services. Ad companies also use pixel tags to measure the number of ads
displayed and their performance (such as how many people clicked on an ad).</span><span
    style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; margin-left: 24.0pt; line-height: normal; background: white'>
            <b><span
                style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>Mobile device IDs.</span></b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>&nbsp;If you're using our
app, we use mobile device IDs (the unique identifier assigned to a device by
the manufacturer), or Advertising IDs (for iOS 6 and later), instead of
cookies, to recognize you. We do this to store your preferences and track your
use of our app. Unlike cookies, device IDs cannot be deleted, but Advertising
IDs can be reset in <span lang="ZH-CN">ì</span>Settings<span lang="ZH-CN">î</span>
                    on your iPhone. Ad companies also use device IDs or Advertising IDs to track
your use of the app, track the number of ads displayed, measure ad performance
and display ads that are more relevant to you. Analytics companies use device
IDs to track information about app usage.</span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; line-height: normal; background: white'>
            <b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>How we use the
information we collect</span></b><b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span></b>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; margin-right: 0in; margin-bottom: 0in; margin-left: 0in; line-height: normal; background: white'>
            <b><span
                style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>In General.</span></b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>&nbsp;We may use information that we collect about
you to:</span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; margin-left: 24.0pt; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>deliver and
improve our products and services, and manage our business;</span><span
    style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; margin-left: 24.0pt; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>manage your
account and provide you with customer support;</span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; margin-left: 24.0pt; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>perform
research and analysis about your use of, or interest in, our or others<span
    lang="ZH-CN">í</span> products, services, or content;</span><span
        style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; margin-left: 24.0pt; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>communicate
with you by email, postal mail, telephone and/or mobile devices about products
or services that may be of interest to you either from us or other third
parties;</span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; margin-left: 24.0pt; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>develop,
display, and track content and advertising tailored to your interests on our
Service and other sites, including providing our advertisements to you when you
visit other sites;</span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; margin-left: 24.0pt; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>website or
mobile application analytics;</span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; margin-left: 24.0pt; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>verify your
eligibility and deliver prizes in connection with contests and sweepstakes;</span><span
    style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; margin-left: 24.0pt; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>enforce or
exercise any rights in our&nbsp;</span><span style='color: black; mso-color-alt: windowtext'><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN; text-decoration: none; text-underline: none'>Terms of Use</span></span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>; and</span><span
    style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; margin-left: 24.0pt; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>perform
functions or services as otherwise described to you at the time of collection.</span><span
    style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; line-height: normal; background: white'>
            <b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>With whom we
share your information</span></b><b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span></b>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; margin-right: 0in; margin-bottom: 0in; margin-left: 0in; line-height: normal; background: white'>
            <span
                style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>Information Shared with Other <span class="SpellE">Users.When</span> you
register as a user of School Bright, your School Bright profile will be
viewable by other users of the Service. Other users (and in the case of any
sharing features available on School Bright, the individuals or apps with whom
a School Bright user may choose to share you with) will be able to view
information you have provided to us directly or through Facebook, such as your
Facebook photos, any additional photos you upload, your first name, your age,
approximate number of miles away, your personal description, and information you
have in common with the person viewing your profile, such as common Facebook
friends and likes</span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext'>.</span><span
    style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; line-height: normal; background: white'>
            <b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>Personal
information.</span></b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>&nbsp;We do not share your personal information
with others except as indicated in this Privacy Policy or when we inform you
and give you an opportunity to opt out of having your personal information
shared. </span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'>
    <o:p></o:p>
</span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; margin-left: 24.0pt; line-height: normal; background: white'>
            <b><span
                style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>Other Situations.</span></b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>&nbsp;We may disclose your
information, including personal information:</span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; margin-left: 48.0pt; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>In response to
a subpoena or similar investigative demand, a court order, or a request for cooperation
from a law enforcement or other government agency; to establish or exercise our
legal rights; to defend against legal claims; or as otherwise required by law.
In such cases, we may raise or waive any legal objection or right available to
us.</span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; margin-left: 48.0pt; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>When we
believe disclosure is appropriate in connection with efforts to investigate,
prevent, or take other action regarding illegal activity, suspected fraud or
other wrongdoing; to protect and defend the rights, property or safety of our
company, our users, our employees, or others; to comply with applicable law or
cooperate with law enforcement; or to enforce our&nbsp;</span><span
    style='color: black; mso-color-alt: windowtext'><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN; text-decoration: none; text-underline: none'>Terms of Use</span></span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>&nbsp;or other
agreements or policies.</span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; margin-left: 48.0pt; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>In connection
with a substantial corporate transaction, such as the sale of our business, a
divestiture, merger, consolidation, or asset sale, or in the unlikely event of
bankruptcy.</span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; margin-right: 0in; margin-bottom: 0in; margin-left: 0in; line-height: normal; background: white'>
            <b><span
                style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>Aggregated and/or non-personal information.</span></b><span
                    style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>&nbsp;We may use and share non-personal information we collect under any
of the above circumstances. We may also share it with third parties to develop
and deliver targeted advertising on our Service and on websites or applications
of third parties, and to analyze and report on advertising you see. We may
combine non-personal information we collect with additional non-personal
information collected from other sources. We also may share aggregated,
non-personal information, or personal information in hashed, non-human readable
form, with third parties, including advisors, advertisers and investors, for
the purpose of conducting general business analysis, advertising, marketing, or
other business purposes. For example, we may engage a data provider who may
collect web log data from you (including IP address and information about your
browser or operating system), or place or recognize a unique cookie on your
browser to enable you to receive customized ads or content. The cookies may re<span
    lang="ZH-CN">&#64258;</span><span class="SpellE">ect</span> de-identified demographic or
other data linked to data you voluntarily have submitted to us (such as your
email address), that we may share with a data provider solely in hashed,
non-human readable form. We may also share your geolocation information in
de-identified form with third parties for the above purposes. </span><span
    style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'>
    <o:p></o:p>
</span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; line-height: normal; background: white'>
            <b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>How you can
access and correct your information</span></b><b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span></b>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>If you have a School
Bright account, you have the ability to review and update your personal
information within the Service by opening your account and going to
settings.&nbsp;Applicable privacy laws may allow you the right to access and/or
request the correction of errors or omissions in your personal information that
is in our custody or under our control. Our Privacy Officer will assist you
with the access request. This includes:</span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; margin-left: 24.0pt; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>Identification
of personal information under our custody or control; and</span><span
    style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; margin-left: 24.0pt; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>Information
about how personal information under our control may be or has been used by us.</span><span
    style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>We will respond to
requests within the time allowed by all applicable privacy laws and will make
every effort to respond as accurately and completely as possible. Any
corrections made to personal information will be promptly sent to any
organization to which it was disclosed.</span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; margin-right: 0in; margin-bottom: 0in; margin-left: 0in; line-height: normal; background: white'>
            <span
                style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>In certain exceptional circumstances, we may not be able to provide
access to certain personal information we hold. For security purposes, not all
personal information is accessible and amendable by the Privacy Officer. If
access or corrections cannot be provided, we will notify the individual making
the request within 30 days, in writing, of the reasons for the refusal.</span><span
    style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; line-height: normal; background: white'>
            <b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>Data retention</span></b><b><span
                style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span></b>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>We keep your information
only as long as we need it for legitimate business purposes and as permitted by
applicable legal requirements. If you close your account, we will retain
certain data for analytical purposes and recordkeeping integrity, as well as to
prevent fraud, enforce our&nbsp;</span><span style='color: black; mso-color-alt: windowtext'><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN; text-decoration: none; text-underline: none'>Terms of Use</span></span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>, take actions
we deem necessary to protect the integrity of our Service or our users, or take
other actions otherwise permitted by law. In addition, if certain information
has already been provided to third parties as described in this Privacy Policy,
retention of that information will be subject to those third parties<span
    lang="ZH-CN">í</span> policies.</span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; line-height: normal; background: white'>
            <b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>Your choices
about collection and use of your information</span></b><b><span
    style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span></b>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; margin-left: 24.0pt; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>You can choose
not to provide us with certain information, but that may result in you being
unable to use certain features of our Service because such information may be
required in order for you to register as user; purchase products or services;
participate in a contest, promotion, survey, or sweepstakes; ask a question; or
initiate other transactions.</span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; margin-left: 24.0pt; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>You can also
control information collected by cookies. You can delete or decline cookies by
changing your browser settings. Click <span lang="ZH-CN">ì</span>help<span
    lang="ZH-CN">î</span> in the toolbar of most browsers for instructions.</span><span
        style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; line-height: normal; background: white'>
            <b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>How we protect
your personal information</span></b><b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span></b>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>We take security measures
to help safeguard your personal information from unauthorized access and
disclosure. However, no system can be completely secure. Therefore, although we
take steps to secure your information, we do not promise, and you should not
expect, that your personal information, chats, or other communications will
always remain secure. Users should also take care with how they handle and
disclose their personal information and should avoid sending personal
information through insecure email. Please refer to the Federal Trade
Commission's website at&nbsp;</span><span style='color: black; mso-color-alt: windowtext'><a href="http://www.ftc.gov/bcp/menus/consumer/data.shtm"><span
    style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN; text-decoration: none; text-underline: none'>http://www.ftc.gov/bcp/menus/consumer/data.shtm</span></a></span><span
        style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>&nbsp;for information about how to protect yourself against identity
theft.</span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>You agree that we may
communicate with you electronically regarding security, privacy, and
administrative issues, such as security breaches. We may post a notice on our
Service if a security breach occurs. We may also send an email to you at the
email address you have provided to us. You may have a legal right to receive
this notice in writing. </span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'>
    <o:p></o:p>
</span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; line-height: normal; background: white'>
            <b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>Information
you provide about yourself while using our Service</span></b><b><span
    style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span></b>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>We provide areas on our
Service where you can post information about yourself and others and
communicate with others. Such postings are governed by our&nbsp;</span><span
    style='color: black; mso-color-alt: windowtext'><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN; text-decoration: none; text-underline: none'>Terms of Use</span></span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>. Also,
whenever you voluntarily disclose personal information on publicly-viewable
pages, that information will be publicly available and can be collected and
used by others. For example, if you post your email address, you may receive
unsolicited messages. We cannot control who reads your posting or what other
users may do with the information you voluntarily post, so we encourage you to
exercise discretion and caution with respect to your personal information.</span><span
    style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; line-height: normal; background: white'>
            <b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>No Rights of
Third Parties</span></b><b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span></b>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>This Privacy Policy does
not create rights enforceable by third parties or require disclosure of any
personal information relating to users of the website.</span><span
    style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; line-height: normal; background: white'>
            <b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>Changes to
this Privacy Policy</span></b><b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span></b>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>We will occasionally
update this Privacy Policy to reflect changes in the law, our data collection
and use practices, the features of our Service, or advances in technology. If
we make any material changes to this Privacy Policy, we will notify you of the
changes by reasonable means, which could include notifications through the
Services or via email. Please review the changes carefully. Your continued use
of the Services following the posting of changes to this policy will mean you
consent to and accept those changes. If you do not consent to such changes you
can delete your account by following the instructions under Settings.</span><span
    style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; line-height: normal; background: white'>
            <b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>How to contact
us</span></b><b><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span></b>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; mso-margin-bottom-alt: auto; line-height: normal; background: white'>
            <span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>If you have any questions
about this Privacy Policy, please contact us by email or postal mail as
follows:</span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal" style='margin-top: 12.0pt; margin-right: 0in; margin-bottom: 0in; margin-left: 0in; line-height: normal; background: white'>
            <span
                style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'>Privacy Officer&nbsp;<br>
                JABJAI CORPORATION CO., LTD.<br>
            </span><span style='color: black; mso-color-alt: windowtext'><a
                href="mailto:narin@schoolbright.co"><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'>narin@schoolbright.co</span></a></span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; color: black; mso-color-alt: windowtext; mso-fareast-language: ZH-CN'><span
                    style='mso-tab-count: 1'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>&nbsp;<br>
                    812/36 Soi Somchai, Wat <span class="SpellE">Tha</span> <span class="SpellE">Phra</span>,
Bangkok <span class="SpellE">Yai</span>, Bangkok 10600<br style='mso-special-character: line-break'>
                    <![if !supportLineBreakNewLine]><br style='mso-special-character: line-break'>
                    <![endif]></span><span style='font-size: 10.5pt; font-family: "Meiryo",sans-serif; mso-bidi-font-family: "Times New Roman"; mso-fareast-language: ZH-CN'><o:p></o:p></span>
        </p>

        <p class="MsoNormal">
            <span style='font-size: 8.0pt; mso-bidi-font-size: 11.0pt; line-height: 115%'>
                <o:p>&nbsp;</o:p></span>
        </p>

    </div>

</body>

</html>
