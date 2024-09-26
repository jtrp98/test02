<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Preview.aspx.cs" Inherits="FingerprintPayment.Card.PermissionCard.Preview" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8">
    <title><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %></title>
    <%--<link rel="stylesheet" href="/styles/style-visithouse-print.css" />--%>
    <%-- <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">--%>
    <%-- <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />--%>
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/css/all.min.css" />
    <link href="//cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="/Content/Material/layout.css?v=<%=DateTime.Now.Ticks%>" rel="stylesheet" />
    <style>
        .print-btn {
            position: fixed;
            top: 50%;
            right: 10px;
            z-index: 4;
            border: 1px solid black;
            padding: 10px 15px;
            color: #fff !important;
            background-color: #2196F3 !important;
            text-align: center;
            cursor: pointer;
        }

        .photo {
            padding-top: 2px;
        }

        .input-with-dot {
            /* text-decoration:underline;
            text-decoration-style:dotted;*/
            border: 0px;
            border-bottom: 1px dotted;
            text-align: center;
        }

        @media print {
            .print-btn {
                display: none !important;
            }

            @page {
                size: auto;
                margin: 0mm;
            }
        }

        .sub-page {
            width: 21cm !important;
            height: 7.5cm !important;
        }

        .fs1 {
            font-size: 14px;
        }

        .fs2 {
            font-size: 12px;
        }

        .fs3 {
            font-size: 11px;
        }

        .part2 > div {
            margin-bottom: 8px;
        }

        input {
            width: -webkit-fill-available;
            /*  width: 50%;*/
            border: 0px solid #fff;
            border-bottom: 1px solid #bbb;
            margin-left: 6px;
            margin-right: 6px;
        }

            input.--dotted {
                border-bottom: 1px dotted #000 !important;
            }

            input.--none {
                border-bottom: 0px solid #000 !important;
            }

        .--stuudent {
            border-right: 2px dashed #ccc;
        }

        .--stuudent, .--school {
            padding: 4px !important;
        }
        /*.part1 , .part2{
                margin-right:4px;
            }*/
    </style>

</head>
<body>
    <div class="print-btn " onclick="window.print();">
        <i class="fas fa-print"></i>
        <br>
        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %>      
    </div>
    <div class="book">
        <div class="page">
            <div class="sub-page">
                <div class="row" style="border: 1px solid #ccc;">
                    <div class="col-6 --stuudent">
                        <div class=" part1 row ">
                            <div class="col-3 text-center p-0">
                                <img src="<%= FormData.Logo %>" style="width: 50px" />
                            </div>
                            <div class=" col-9  p-0">
                                <div class="row ">
                                    <div class="col-12  p-0 text-start fs1">
                                        <strong><%= FormData.School %></strong>
                                    </div>

                                    <div class="col-6  p-0 text-start fs2">
                                        <strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132048") %></strong>
                                    </div>
                                    <div class="col-6 p-0 text-end fs2">
                                        <strong>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132049") %>)</strong>
                                    </div>

                                    <div class="col-6  p-0 text-start fs3">
                                        <div class="row">
                                            <div class="col-2  p-0 text-start ">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>
                                            </div>
                                            <div class="col-8  p-0 text-start ">
                                                <input type="text" class="text-center" value="<%= FormData.Date %>" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-6  p-0 text-end fs3">
                                        <div class="row">
                                            <div class="col-7  p-0 text-start ">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107015") %>
                                            </div>
                                            <div class="col-5  p-0 text-start ">
                                                <input type="text" class="col-8 text-center" value="<%= FormData.RefNo %>" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="part2 row fs3" style="margin-top: 6px;">
                            <div class="col-2  p-0 text-end ">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105070") %> : 
                            </div>
                            <div class="col-10  p-0 text-start ">
                                <span style="margin-left: 4px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132046") %>
                                </span>
                            </div>
                            <div class="col-2  p-0 text-end ">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %> : 
                            </div>
                            <div class="col-10  p-0 text-start ">
                                <input class="col-6" value="<%= FormData.FullName %>" />
                            </div>
                            <div class="col-2  p-0 text-end ">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>ออนุญาต : 
                            </div>
                            <div class="col-10  p-0 text-start ">
                                <input class="col-6" value="<%= FormData.Type %>" />
                            </div>
                            <div class="col-2  p-0 text-end ">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105073") %> : 
                            </div>
                            <div class="col-10  p-0 text-start ">
                                <input class="col-6" value="<%= FormData.Cause %>" />
                            </div>

                            <div class="col-2  p-0 text-end ">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132050") %> : 
                            </div>
                            <div class="col-4  p-0 text-start ">
                                <input class="text-center" value="<%= FormData.Date1 %>" />
                            </div>
                            <div class="col-2  p-0 text-end ">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132047") %> : 
                            </div>
                            <div class="col-4  p-0 text-start ">
                                <input class="text-center" value="<%= FormData.Date2 %>" />
                            </div>

                            <div class="col-6  p-0 text-center " style="margin-top: 10px">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132051") %>
                            </div>
                            <div class="col-6  p-0 text-center " style="margin-top: 10px">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132052") %>
                            </div>
                            <div class="col-6  p-0 text-center d-inline-flex">
                                &nbsp;&nbsp;(<input class="text-center --none" value="<%= FormData.Teacher1 %>" />)&nbsp;&nbsp;
                            </div>
                            <div class="col-6  p-0 text-center d-inline-flex">
                                &nbsp;&nbsp;(<input class="text-center --none" value="<%= FormData.Teacher2 %>" />)&nbsp;&nbsp;
                            </div>
                        </div>
                    </div>
                    <div class="col-6 --school">
                        <div class=" part1 row ">
                            <div class="col-3 text-center p-0">
                                <img src="<%= FormData.Logo %>" style="width: 50px" />
                            </div>
                            <div class=" col-9  p-0">
                                <div class="row ">
                                    <div class="col-12  p-0 text-start fs1">
                                        <strong><%= FormData.School %></strong>
                                    </div>

                                    <div class="col-6  p-0 text-start fs2">
                                        <strong><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132048") %></strong>
                                    </div>
                                    <div class="col-6 p-0 text-end fs2">
                                        <strong>(<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132053") %>)</strong>
                                    </div>

                                    <div class="col-6  p-0 text-start fs3">
                                        <div class="row">
                                            <div class="col-2  p-0 text-start ">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>
                                            </div>
                                            <div class="col-8  p-0 text-start ">
                                                <input class="text-center" value="<%= FormData.Date %>" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-6  p-0 text-end fs3">
                                        <div class="row">
                                            <div class="col-7  p-0 text-start ">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107015") %>
                                            </div>
                                            <div class="col-5  p-0 text-start ">
                                                <input  class="col-8 text-center" value="<%= FormData.RefNo %>" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="part2 row fs3" style="margin-top: 6px;">
                            <div class="col-2  p-0 text-end ">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105070") %> : 
                            </div>
                            <div class="col-10  p-0 text-start ">
                                <span style="margin-left: 4px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132046") %>
                                </span>
                            </div>
                            <div class="col-2  p-0 text-end ">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107018") %> : 
                            </div>
                            <div class="col-10  p-0 text-start ">
                                <input class="col-6" value="<%= FormData.FullName %>" />
                            </div>
                            <div class="col-2  p-0 text-end ">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>ออนุญาต : 
                            </div>
                            <div class="col-10  p-0 text-start ">
                                <input class="col-6" value="<%= FormData.Type %>" />
                            </div>
                            <div class="col-2  p-0 text-end ">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105073") %> : 
                            </div>
                            <div class="col-10  p-0 text-start ">
                                <input class="col-6" value="<%= FormData.Cause %>" />
                            </div>

                            <div class="col-2  p-0 text-end ">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132050") %> : 
                            </div>
                            <div class="col-4  p-0 text-start ">
                                <input class="text-center" value="<%= FormData.Date1 %>" />
                            </div>
                            <div class="col-2  p-0 text-end ">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132047") %> : 
                            </div>
                            <div class="col-4  p-0 text-start ">
                                <input class="text-center" value="<%= FormData.Date2 %>" />
                            </div>

                            <div class="col-6  p-0 text-center " style="margin-top: 10px">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132051") %>
                            </div>
                            <div class="col-6  p-0 text-center " style="margin-top: 10px">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132052") %>
                            </div>
                            <div class="col-6  p-0 text-center d-inline-flex">
                                &nbsp;&nbsp;(<input class="text-center --none" value="<%= FormData.Teacher1 %>" />)&nbsp;&nbsp;
                            </div>
                            <div class="col-6  p-0 text-center d-inline-flex">
                                &nbsp;&nbsp;(<input class="text-center --none" value="<%= FormData.Teacher2 %>" />)&nbsp;&nbsp;
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

    </div>
</body>
</html>
