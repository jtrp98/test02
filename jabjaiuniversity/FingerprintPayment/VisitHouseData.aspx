<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisitHouseData.aspx.cs" Inherits="FingerprintPayment.VisitHouseData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:MultiView ID="MvContent" runat="server">
        <asp:View ID="ListContent" runat="server">
            <div class="visitHouseList">
                <div class="row" style="padding-bottom: 10px;">
                    <div class="col-md-6">
                        <label class="col-md-5">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %></label>
                        <div class="col-md-7">
                            <select id="sltSearchYear" name="sltSearchYear[]"
                                class="form-control">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                <asp:Literal ID="ltrYear" runat="server" />
                            </select>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <label class="col-md-5">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %></label>
                        <div class="col-md-7">
                            <select id="sltSearchTerm" name="sltSearchTerm[]"
                                class="form-control">
                                <asp:Literal ID="ltrTerm" runat="server" />
                            </select>
                        </div>
                    </div>
                </div>
                <div class="row" style="padding-bottom: 10px;">
                    <div class="col-md-6">
                        <label class="col-md-5">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %></label>
                        <div class="col-md-7">
                            <select id="sltSearchLevel" name="sltSearchLevel[]"
                                class="form-control">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                                <asp:Literal ID="ltrLevel" runat="server" />
                            </select>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <label class="col-md-5">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %></label>
                        <div class="col-md-7">
                            <select id="sltSearchClass" name="sltSearchClass[]"
                                class="form-control">
                                <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="row" style="padding-bottom: 10px;">
                    <div class="col-md-6">
                        <label class="col-md-5">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                        <div class="col-md-7">
                            <input id="iptSearch" name="iptSearch" type="text" class="form-control" style="width: 100%;" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101340") %>" />
                        </div>
                    </div>
                    <div class="col-md-6" style="display: none;">
                    </div>
                </div>
                <div class="row">
                    <br />
                </div>
                <div class="row">
                    <div class="col-md-12 mb-12 text-center" style="padding-right: 2%;">
                        <button id="btnSearch" class="btn btn-info btn-round col-md-2" style="font-size: 24px; float: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></button>
                    </div>
                </div>
                <div class="row">
                    &nbsp;
                </div>
                <table id="tableData" class="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th style="width: 5%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %></th>
                            <th style="width: 25%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></th>
                            <th style="width: 25%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></th>
                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %></th>
                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %></th>
                            <th style="width: 10%" class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %></th>
                            <th style="width: 15%" class="text-center"></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                        </tr>
                    </thead>

                    <tfoot>
                        <tr>
                            <th colspan="11">
                                <div class="row">
                                    <div class="col-md-4 mb-4 text-left" style="padding-left: 2%;">
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102242") %>: </span>
                                        <select id="sltPageSize">
                                            <option selected="selected" value="20">20</option>
                                            <option value="50">50</option>
                                            <option value="100">100</option>
                                        </select>
                                    </div>
                                    <div class="col-md-4 mb-4 text-center">
                                        <a id="aPrevious" href="#" style="text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %></a>
                                        <select id="sltPageIndex">
                                        </select>
                                        <a id="aNext" href="#" style="text-decoration: none;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %></a>
                                    </div>
                                    <div class="col-md-4 mb-4 text-right" style="padding-right: 2%;">
                                        <span id="spnPageInfo"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132005") %></span>
                                    </div>
                                </div>
                            </th>
                        </tr>
                    </tfoot>
                </table>
            </div>

            <script type="text/javascript">

                var visitHouseList = {
                    PageIndex: 0,
                    PageSize: 20,
                    PageCount: 0,
                    LoadListData: function () {
                        var dt = $(".visitHouseList #tableData").DataTable({
                            "processing": true,
                            "serverSide": true,
                            "info": false,
                            "searching": false,
                            "paging": false,
                            "stateSave": true,
                            "ajax": {
                                "url": "Handles/VisitHouse/LoadVisitHouseList.ashx",
                                "type": "POST",
                                "contentType": "application/json; charset=utf-8",
                                "data": function (d) {
                                    d.search = $(".visitHouseList #iptSearch").val();
                                    d.year = $(".visitHouseList #sltSearchYear").children("option:selected").val();
                                    d.term = $(".visitHouseList #sltSearchTerm").children("option:selected").val();
                                    d.subLevel = $(".visitHouseList #sltSearchLevel").children("option:selected").val();
                                    d.termSubLevel = $(".visitHouseList #sltSearchClass").children("option:selected").val();
                                    d.page = visitHouseList.PageIndex;
                                    d.length = visitHouseList.PageSize;

                                    return JSON.stringify(d, function (key, value) { return (value === undefined) ? "" : value });
                                }
                            },
                            "columns": [
                                { "data": "no", "orderable": false },
                                { "data": "FirstName", "orderable": true },
                                { "data": "LastName", "orderable": true },
                                { "data": "StudentCode", "orderable": true },
                                { "data": "StampDate", "orderable": true },
                                { "data": "StampTime", "orderable": true },
                                { "data": "action", "orderable": false },
                                { "data": "year", "orderable": false },
                                { "data": "vid", "orderable": false },
                                { "data": "sid", "orderable": false },
                                { "data": "term", "orderable": false }
                            ],
                            "order": [[7, "desc"]],
                            "columnDefs": [
                                { className: "vertical-align-middle text-center", "targets": [0] },
                                { className: "text-center", "targets": [0, 3, 4, 5, 6] },
                                { "targets": [7, 8, 9, 10], "visible": false },
                                {
                                    "render": function (data, type, row) {
                                        return '<div class="btn-group">' +
                                            '<button type="button" class="btn btn-success record"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>' +
                                            '<button type="button" class="btn btn-success schedule" data-toggle="modal" data-target="#modalShowForm" form-name="VisitHouseData.aspx" form-action="form-schedule" form-title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131233") %>"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105033") %></button>' +
                                            '<button type="button" class="btn btn-success print"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %></button>' +
                                            '</div>';
                                    },
                                    "targets": 6
                                }
                            ],
                            "drawCallback": function (settings) {
                                var json = settings.json;

                                visitHouseList.PageCount = json.pageCount;

                                var options = '';
                                for (var pi = 0; pi < json.pageCount; pi++) {
                                    options += '<option ' + (visitHouseList.PageIndex == pi ? 'selected="selected"' : '') + ' value="' + pi + '">' + (pi + 1) + '</option>';
                                }
                                $('.visitHouseList #tableData #sltPageIndex').html(options);

                                $('.visitHouseList #tableData #spnPageInfo').html('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102240") %> ' + (visitHouseList.PageIndex + 1) + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102241") %> ' + visitHouseList.PageSize + ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %>');
                            }
                        });
                        // order.dt search.dt
                        dt.on('draw.dt', function () {
                            dt.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                cell.innerHTML = (visitHouseList.PageIndex * visitHouseList.PageSize) + (i + 1) + '.';
                            });
                            dt.column(6, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                                var year = dt.cells({ row: i, column: 7 }).data()[0];
                                var vid = dt.cells({ row: i, column: 8 }).data()[0];
                                var sid = dt.cells({ row: i, column: 9 }).data()[0];
                                var term = dt.cells({ row: i, column: 10 }).data()[0];
                                $(cell).find(".record").attr("url-data", "VisitHouseData.aspx?v=form&year=" + year + "&vid=" + vid + "&sid=" + sid + "&term=" + term);
                                $(cell).find(".schedule").attr("year", year).attr("vid", vid).attr("sid", sid).attr("term", term);
                                $(cell).find(".print").attr("url-data", "VisitHouseData.aspx?v=view&year=" + year + "&vid=" + vid);
                            });
                        });
                    },
                    ReloadListData: function () {
                        var dt = $('.visitHouseList #tableData').DataTable();
                        dt.draw();
                    }
                }

                function LoadTerm(yearID, objResult) {
                    if (yearID) {
                        $.ajax({
                            async: false,
                            type: "POST",
                            url: "VisitHouseData.aspx/LoadTerm",
                            data: '{yearID: ' + yearID + ' }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                var yearData = response.d;

                                $(objResult).empty();

                                if (yearData.length > 0) {

                                    var options = '';
                                    $(yearData).each(function () {

                                        options += '<option value="' + this.id + '">' + this.name + '</option>';

                                    });

                                    $(objResult).html(options);
                                }
                            },
                            failure: function (response) {
                                console.log(response.d);
                            },
                            error: function (response) {
                                console.log(response.d);
                            }
                        });
                    }
                }

                function LoadTermSubLevel2(subLevelID, objResult) {
                    if (subLevelID) {
                        $.ajax({
                            async: false,
                            type: "POST",
                            url: "VisitHouseData.aspx/LoadTermSubLevel2",
                            data: '{subLevelID: ' + subLevelID + ' }',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                var subLevel2 = response.d;

                                $(objResult).empty();

                                if (subLevel2.length > 0) {

                                    var options = '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>';
                                    $(subLevel2).each(function () {

                                        options += '<option value="' + this.id + '">' + this.name + '</option>';

                                    });

                                    $(objResult).html(options);
                                }
                            },
                            failure: function (response) {
                                console.log(response.d);
                            },
                            error: function (response) {
                                console.log(response.d);
                            }
                        });
                    }
                }

                $(document).ready(function () {

                    $(document).on('click', '.visitHouseList .record, .visitHouseList .print', function () {

                        $url = $(this).attr('url-data');

                        manageContent.redirect($url, {});

                    });

                    // Search
                    $("#sltSearchYear").change(function () {

                        LoadTerm($(this).val(), '#sltSearchTerm');

                    });

                    $("#sltSearchLevel").change(function () {

                        LoadTermSubLevel2($(this).val(), '#sltSearchClass');

                    });

                    // Searching, Pagination event 
                    $('.visitHouseList #btnSearch').click(function () {

                        visitHouseList.PageIndex = 0;

                        visitHouseList.ReloadListData();

                        return false;
                    });

                    $('.visitHouseList #tableData #sltPageSize').change(function () {

                        visitHouseList.PageSize = parseInt($(".visitHouseList #tableData #sltPageSize").children("option:selected").val());
                        visitHouseList.PageIndex = 0;

                        visitHouseList.ReloadListData();

                        return false;
                    });

                    $('.visitHouseList #tableData #sltPageIndex').change(function () {

                        visitHouseList.PageIndex = $(".visitHouseList #tableData #sltPageIndex").children("option:selected").val();

                        visitHouseList.ReloadListData();

                        return false;
                    });

                    $('.visitHouseList #tableData #aPrevious').click(function () {

                        if (visitHouseList.PageIndex > 0) {
                            visitHouseList.PageIndex--;
                            visitHouseList.ReloadListData();
                        }

                        return false;
                    });
                    $('.visitHouseList #tableData #aNext').click(function () {

                        if (visitHouseList.PageIndex < (visitHouseList.PageCount - 1)) {
                            visitHouseList.PageIndex++;
                            visitHouseList.ReloadListData();
                        }

                        return false;
                    });

                    $.ui.autocomplete.prototype._renderItem = function (ul, item) {
                        var t = String(item.value).replace(
                            new RegExp(this.term, "gi"),
                            "<strong>$&</strong>");
                        return $("<li></li>")
                            .data("item.autocomplete", item)
                            .append("<a>" + t + "</a>")
                            .appendTo(ul);
                    };

                    $(".visitHouseList #iptSearch").autocomplete({
                        source: function (request, response) {
                            var param = { keyword: $('.visitHouseList #iptSearch').val() };
                            $.ajax({
                                url: "VisitHouseData.aspx/GetStudentName",
                                data: JSON.stringify(param),
                                dataType: "json",
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                dataFilter: function (data) { return data; },
                                success: function (data) {
                                    response($.map(data.d, function (item) {
                                        return {
                                            value: item
                                        }
                                    }))
                                },
                                error: function (XMLHttpRequest, textStatus, errorThrown) {
                                    console.log(textStatus);
                                }
                            });
                        },
                        select: function (event, ui) {
                            // ui.item
                            // ui.item.value
                        },
                        minLength: 1
                    });

                    // Initial data
                    //initFunction.setDropdown('#sltSearchYear', 'Year', '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101312") %></option>');
                    //initFunction.setDropdown('#sltSearchTerm', 'Term', '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202002") %></option>');
                    //initFunction.setDropdown('#sltSearchLevel', 'SubLevel', '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101009") %></option>');
                    //initFunction.setDropdown('#sltSearchClass', 'TermSubLevel', '<option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101011") %></option>');

                    // Datatable Section
                    visitHouseList.LoadListData();

                });

            </script>
        </asp:View>
        <asp:View ID="FormContent" runat="server">
            <div class="visitHouseForm" style="padding: 15px 45px;">
                <form>
                    <div class="board">
                        <h1 style="text-align: center; font-size: 3em;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131242") %></h1>
                        <div class="board-inner">
                            <ul class="nav nav-tabs">
                                <div class="liner"></div>
                                <li class="active">
                                    <a href="#user" data-toggle="tab" title="welcome">
                                        <span class="round-tabs one">
                                            <i class="glyphicon glyphicon-user fa fa-users"></i>
                                        </span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#smile" data-toggle="tab" title="profile">
                                        <span class="round-tabs two">
                                            <i class="glyphicon glyphicon-smile fa fa-smile-o"></i>
                                        </span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#picture" data-toggle="tab" title="bootsnipp goodies">
                                        <span class="round-tabs three">
                                            <i class="glyphicon glyphicon-picture fa fa-camera"></i>
                                        </span>
                                    </a>
                                </li>
                            </ul>
                        </div>
                        <div class="tab-content">
                            <div class="tab-pane fade in active" id="user">
                                <h2 class="head text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131243") %></h2>

                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12 form-inline">
                                        <span>1.1 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02107") %></span>
                                        <select id="sltTimeTogether" name="sltTimeTogether[]" class="form-control" required>
                                            <option value="1">1</option>
                                            <option value="2">2</option>
                                            <option value="3">3</option>
                                            <option value="4">4</option>
                                            <option value="5">5</option>
                                        </select>
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131244") %></span>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12">
                                        <span>1.2 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00297") %></span>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-3 mb-3">
                                        <span class="tab2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %></span>
                                    </div>
                                    <div class="col-md-9 mb-9 form-inline">
                                        <select id="sltFatherRelationsLevel" name="sltFatherRelationsLevel[]"
                                            class="form-control" required>
                                            <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305059") %></option>
                                            <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305060") %></option>
                                            <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305056") %></option>
                                            <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305062") %></option>
                                            <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-3 mb-3">
                                        <span class="tab2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %></span>
                                    </div>
                                    <div class="col-md-9 mb-9 form-inline">
                                        <select id="sltMotherRelationsLevel" name="sltMotherRelationsLevel[]"
                                            class="form-control" required>
                                            <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305059") %></option>
                                            <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305060") %></option>
                                            <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305056") %></option>
                                            <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305062") %></option>
                                            <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-3 mb-3">
                                        <span class="tab2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305063") %></span>
                                    </div>
                                    <div class="col-md-9 mb-9 form-inline">
                                        <select id="sltBrotherRelationsLevel" name="sltBrotherRelationsLevel[]"
                                            class="form-control" required>
                                            <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305059") %></option>
                                            <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305060") %></option>
                                            <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305056") %></option>
                                            <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305062") %></option>
                                            <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-3 mb-3">
                                        <span class="tab2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305064") %></span>
                                    </div>
                                    <div class="col-md-9 mb-9 form-inline">
                                        <select id="sltSistersRelationsLevel" name="sltSistersRelationsLevel[]"
                                            class="form-control" required>
                                            <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305059") %></option>
                                            <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305060") %></option>
                                            <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305056") %></option>
                                            <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305062") %></option>
                                            <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-3 mb-3">
                                        <span class="tab2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %></span>
                                    </div>
                                    <div class="col-md-9 mb-9 form-inline">
                                        <select id="sltRelativeRelationsLevel"
                                            name="sltRelativeRelationsLevel[]" class="form-control" required>
                                            <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305059") %></option>
                                            <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305060") %></option>
                                            <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305056") %></option>
                                            <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305062") %></option>
                                            <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %></option>
                                        </select>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12">
                                        <span>1.3 ก<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ณีที่<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305076") %>
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133522") %></span>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12 tab2">
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoTakeCareChildren" value="1"
                                                    checked>
                                                <span class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoTakeCareChildren" value="2">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305077") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoTakeCareChildren" value="3">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00834") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoTakeCareChildren" value="4">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206502") %></span>
                                            </label>
                                            <input id="iptTakeCareChildrenOther" class="input-bottom-dot"
                                                style="display: none;">
                                        </div>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12">
                                        <span>1.4 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02790") %>ค<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ัวเ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ือน<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206284") %>ต่อ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> (<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405031") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02790") %>ค<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ัวเ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ือน
                                                    หา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ด้วย<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131292") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>)</span>
                                        <input id="iptHouseholdIncome" class="input-bottom-dot">
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></span>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12">
                                        <span>1.5
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305086") %></span><input id="iptExpensesFrom" class="input-bottom-dot">
                                        <span>นักเรียนทำงานหารายได้ อาชีพ</span><input id="iptExtraWork" class="input-bottom-dot">
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12 tab2">
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01734") %></span><input id="iptExtraWorkIncome" class="input-bottom-dot"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></span>
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00816") %></span><input id="iptCarryMoneySchool"
                                            class="input-bottom-dot"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></span>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12">
                                        <span>1.6 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02166") %></span>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12 tab2">
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoHelpFromSchool" value="1" checked>
                                                <span class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131141") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoHelpFromSchool" value="2">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305112") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoHelpFromSchool" value="3">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131245") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoHelpFromSchool" value="4">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206502") %></span>
                                            </label>
                                            <input id="iptHelpFromSchoolOther" class="input-bottom-dot"
                                                style="display: none;">
                                        </div>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12">
                                        <span>1.7
                                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00278") %></span>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12 tab2">
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoHelpFamilyReceived" value="1"
                                                    checked>
                                                <span class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131246") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoHelpFamilyReceived" value="2">
                                                <span class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131247") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoHelpFamilyReceived" value="3">
                                                <span class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206502") %></span>
                                            </label>
                                            <input id="iptHelpFamilyReceivedOther" class="input-bottom-dot"
                                                style="display: none;">
                                        </div>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12">
                                        <span>1.8 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00207") %></span>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12 tab2">
                                        <textarea id="iptParentsConcerns" class="form-control" rows="5"></textarea>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left" style="padding-top: 40px;">
                                    <div class="col-md-12 mb-12 text-center">
                                        <button id="btnUserSave" class="btn btn-warning form-control"
                                            style="/*white-space: inherit ! important; font-size: xx-large; margin: 5px; */">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00906") %>
                                        </button>
                                        <button id="btnUserNext" class="btn btn-success form-control"
                                            style="/*white-space: inherit ! important; font-size: xx-large; margin: 5px; */">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %>
                                            <span class="pull-right">
                                                <span class="glyphicon fa fa-angle-right"></span>
                                            </span>
                                        </button>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane fade" id="smile">
                                <h2 class="head text-left">2. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01176") %></h2>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12">
                                        <span>2.1 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305116") %></span>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12 tab2">
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoHealth" value="1" checked>
                                                <span class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305117") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoHealth" value="2">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305118") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoHealth" value="3">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305119") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoHealth" value="4">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305120") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoHealth" value="5">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305121") %></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12">
                                        <span>2.2 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02141") %></span>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12 tab2">
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoWelfare" value="1" checked>
                                                <span class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305125") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoWelfare" value="2">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01335") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoWelfare" value="3">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305129") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoWelfare" value="4">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305131") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoWelfare" value="5">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305133") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoWelfare" value="6">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305135") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoWelfare" value="7">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00796") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoWelfare" value="8">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305128") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoWelfare" value="9">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305130") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoWelfare" value="10">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305132") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoWelfare" value="11">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305134") %></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12">
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131248") %></span><input id="iptDistanceSchool"
                                            class="input-bottom-dot"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00121") %></span>
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305143") %></span><input id="iptTimeSchoolHour"
                                            class="input-bottom-dot"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00513") %></span><input id="iptTimeSchoolMinute"
                                                class="input-bottom-dot"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00836") %></span>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12 tab2">
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoTravelMethod" value="1" checked>
                                                <span class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131297") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoTravelMethod" value="2">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305151") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoTravelMethod" value="3">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131249") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoTravelMethod" value="4">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305153") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoTravelMethod" value="5">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131250") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoTravelMethod" value="6">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132161") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoTravelMethod" value="7">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305155") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoTravelMethod" value="8">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206502") %></span>
                                            </label>
                                            <input id="iptTravelMethodOther" class="input-bottom-dot"
                                                style="display: none;">
                                        </div>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12">
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131253") %></span>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12 tab2">
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoLivingConditions" value="1"
                                                    checked>
                                                <span class="label-text">สภา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131216") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M404053") %>ท<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ุดโท<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ม <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101286") %>
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131216") %>ทำ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204038") %>วัสดุ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ื้น<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131216") %> เช่น ไม้ไ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206418") %>่
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132968") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204038") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101286") %>วัสดุเหลือใช้</span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoLivingConditions" value="2">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131255") %></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12">
                                        <span>2.5 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305164") %></span>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12 tab2">
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoStudentWorkFamily" value="1"
                                                    checked>
                                                <span class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305165") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoStudentWorkFamily" value="2">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305167") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoStudentWorkFamily" value="3">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131256") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoStudentWorkFamily" value="4">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131238") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoStudentWorkFamily" value="5">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305168") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoStudentWorkFamily" value="6">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206502") %></span>
                                            </label>
                                            <input id="iptStudentWorkFamilyOther" class="input-bottom-dot"
                                                style="display: none;">
                                        </div>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12">
                                        <span>2.6 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305171") %></span>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12 tab2">
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoHobby" value="1" checked>
                                                <span class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131257") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoHobby" value="2">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131258") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoHobby" value="3">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305177") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoHobby" value="4">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132162") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoHobby" value="5">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131259") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoHobby" value="6">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131260") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoHobby" value="7">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305179") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoHobby" value="8">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305176") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoHobby" value="9">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206502") %></span>
                                            </label>
                                            <input id="iptHobbyOther" class="input-bottom-dot"
                                                style="display: none;">
                                        </div>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12">
                                        <span>2.7 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305182") %></span>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12 tab2">
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoSubstanceAbuseBehavior" value="1"
                                                    checked>
                                                <span class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131261") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoSubstanceAbuseBehavior" value="2">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131302") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoSubstanceAbuseBehavior" value="3">
                                                <span class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305186") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoSubstanceAbuseBehavior" value="4">
                                                <span class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305187") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoSubstanceAbuseBehavior" value="5">
                                                <span class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01080") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206418") %>ู้ติดบุห<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ี่ สุ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>า
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101286") %>กา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ใช้สา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>เส<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ติด<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12">
                                        <span>2.8 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305190") %></span>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12 tab2">
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoViolenceBehavior" value="1"
                                                    checked>
                                                <span class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305192") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoViolenceBehavior" value="2">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305193") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoViolenceBehavior" value="3">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305194") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoViolenceBehavior" value="4">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305195") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoViolenceBehavior" value="5">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305196") %></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12">
                                        <span>2.9 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305198") %></span>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12 tab2">
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoSexualBehavior" value="1" checked>
                                                <span class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305200") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoSexualBehavior" value="2">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305201") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoSexualBehavior" value="3">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305202") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoSexualBehavior" value="4">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305203") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoSexualBehavior" value="5">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305204") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoSexualBehavior" value="6">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305205") %></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12">
                                        <span>2.10 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305207") %></span>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12 tab2">
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoGameAddiction" value="1" checked>
                                                <span class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131263") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoGameAddiction" value="2">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305209") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoGameAddiction" value="3">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305210") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoGameAddiction" value="4">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305211") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoGameAddiction" value="5">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305212") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoGameAddiction" value="6">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01609") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoGameAddiction" value="7">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305213") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoGameAddiction" value="8">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305214") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoGameAddiction" value="9">
                                                <span
                                                    class="label-text">ใช้เงินสิ้นเ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %>ลือง โกหก
                                                            ลัก<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>โมยเงินเ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ื่อเล่นเกม</span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoGameAddiction" value="10">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206502") %></span>
                                            </label>
                                            <input id="iptGameAddictionOther" class="input-bottom-dot"
                                                style="display: none;">
                                        </div>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12">
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131265") %></span>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12 tab2">
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoInternetAccess" value="1" checked>
                                                <span class="label-text">สามา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ถ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133256") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %> Internet
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132249") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204038") %>ที่<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131216") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoInternetAccess" value="2">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01355") %>สามา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ถ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133256") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105086") %> Internet
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132249") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204038") %>ที่<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131216") %></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12">
                                        <span>2.12 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305222") %></span>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12 tab2">
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoUsingElectronicTools" value="1"
                                                    checked>
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131268") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoUsingElectronicTools" value="2">
                                                <span class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133256") %>ใช้ LINE, Facebook, Twitter <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101286") %>
                                                            chat (เกิน<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %>ละ 1 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00513") %>)</span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoUsingElectronicTools" value="3">
                                                <span class="label-text">ใช้<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105084") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701042") %>ใน<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ะหว่าง<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105070") %> 2 - 3
                                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00513") %> / <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoUsingElectronicTools" value="4">
                                                <span class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133256") %>ใช้ LINE, Facebook, Twitter <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101286") %>
                                                            chat (เกิน<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102216") %>ละ 2 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00513") %>)</span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <h2 class="head text-left"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01158") %></h2>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12">
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoInformant" value="1" checked>
                                                <span class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoInformant" value="2">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoInformant" value="3">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01221") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoInformant" value="4">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01225") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoInformant" value="5">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00835") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoInformant" value="6">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoInformant" value="7">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01058") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoInformant" value="8">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01862") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoInformant" value="9">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %>ู</span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoInformant" value="10">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01457") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoInformant" value="11">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00734") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoInformant" value="12">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01458") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoInformant" value="13">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00771") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoInformant" value="14">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131273") %></span>
                                            </label>
                                        </div>
                                        <div class="form-check radio-inline">
                                            <label>
                                                <input type="radio" name="rdoInformant" value="15">
                                                <span
                                                    class="label-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131274") %></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left" style="padding-top: 40px;">
                                    <div class="col-md-12 mb-12 text-center">
                                        <button id="btnSmileSave" class="btn btn-warning form-control"
                                            style="/*white-space: inherit ! important; font-size: xx-large; margin: 5px; */">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00906") %>
                                        </button>
                                        <button id="btnSmileBack" class="btn btn-success form-control"
                                            style="/*white-space: inherit ! important; font-size: xx-large; margin: 5px; */">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00070") %>
                                                    <span class="pull-right">
                                                        <span class="glyphicon fa fa-angle-left"></span>
                                                    </span>
                                        </button>
                                        <button id="btnSmileNext" class="btn btn-success form-control"
                                            style="/*white-space: inherit ! important; font-size: xx-large; margin: 5px; */">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %>
                                                    <span class="pull-right">
                                                        <span class="glyphicon fa fa-angle-right"></span>
                                                    </span>
                                        </button>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane fade" id="picture">
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12 tab2">
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132139") %></span>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-4 mb-4 tab2">
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131309") %></span>
                                    </div>
                                    <div class="col-md-8 mb-8 tab2">
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131240") %></span><br />
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00916") %></span><br />
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131216") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101286") %>ที่<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ัก<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106002") %> วัด มูลนิธิ หอ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ัก โ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>งงาน
                                                    อยู่<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132955") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121039") %>จ้าง</span><br />
                                        <span>ภา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %>และ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01058") %>ย<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131060") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105073") %>ถ่ายภา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131216") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01355") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132249") %>
                                                    เ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>าะ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131216") %>อยู่ต่าง<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104030") %>/</span><br />
                                        <span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131241") %></span><br />
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12 text-center">
                                        <h2 class="head text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131277") %></h2>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12 text-center">
                                        <div id="divPhotosOutsideHome" class="picture-frame" style="border: 1px solid; height: 400px; width: 100%; background-image: url(/Assets/images/background-image-r.gif);">
                                            <img class="img-photo" src="data:image/gif;base64,R0lGODlhAQABAAD/ACwAAAAAAQABAAACADs%3D"
                                                style="width: 100%; height: 100%;" />
                                            <div class="btn btn-default form-control div-browse"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206358") %><i class="glyphicon fa fa-upload pull-right"></i></div>
                                            <div class="progress" style="height: 5px; margin-right: 0px; margin-top: 0px; position: absolute; width: 100%; display: none;">
                                                <div class="progress-bar" role="progressbar" style="width: 25%;" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                            <div style='height: 0px; width: 0px; overflow: hidden;'>
                                                <input type="file" value="upload" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12 text-center">
                                        <h2 class="head text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131278") %></h2>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12 text-center">
                                        <div id="divPhotosInsideHome" class="picture-frame" style="border: 1px solid; height: 400px; width: 100%; background-image: url(/Assets/images/background-image-r.gif);">
                                            <img class="img-photo" src="data:image/gif;base64,R0lGODlhAQABAAD/ACwAAAAAAQABAAACADs%3D"
                                                style="width: 100%; height: 100%;" />
                                            <div class="btn btn-default form-control div-browse"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206358") %><i class="glyphicon fa fa-upload pull-right"></i></div>
                                            <div class="progress" style="height: 5px; margin-right: 0px; margin-top: 0px; position: absolute; width: 100%; display: none;">
                                                <div class="progress-bar" role="progressbar" style="width: 25%;" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                            <div style='height: 0px; width: 0px; overflow: hidden;'>
                                                <input type="file" value="upload" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12 text-center">
                                        <h2 class="head text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131279") %></h2>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left">
                                    <div class="col-md-12 mb-12 text-center">
                                        <div id="map" style="height: 400px; width: 100%;"></div>
                                        <script type='text/javascript' src="/scripts/map-function.js"></script>
                                        <script async defer
                                            src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBlXrha-w3Nc6LbnmPn3s16be4TP59T0Os&callback=initMap">
                                        </script>
                                    </div>
                                </div>
                                <div class="row div-row-padding padding-left" style="padding-top: 40px;">
                                    <div class="col-md-12 mb-12 text-center">
                                        <button id="btnPictureSave" class="btn btn-info form-control"
                                            style="/*white-space: inherit ! important; font-size: xx-large; margin: 5px;*/">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>
                                                    <span class="pull-right">
                                                        <span class="glyphicon fa fa-save"></span>
                                                    </span>
                                        </button>
                                        <button id="btnPictureBack" class="btn btn-success form-control"
                                            style="/*white-space: inherit ! important; font-size: xx-large; margin: 5px;*/">
                                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00070") %>
                                                    <span class="pull-right">
                                                        <span class="glyphicon fa fa-angle-left"></span>
                                                    </span>
                                        </button>
                                    </div>
                                </div>
                            </div>

                            <div class="clearfix"></div>
                        </div>

                    </div>
                </form>
            </div>
            <script type="text/javascript">

                var visitHouseForm = {
                    GetItem: function (year, vid, sid, term) {
                        $.ajax({
                            type: "POST",
                            url: "VisitHouseData.aspx/GetItem",
                            data: '{year: ' + year + ', vid: ' + vid + ', sid: ' + sid + ', term: ' + term + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: visitHouseForm.OnSuccessGet,
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    OnSuccessGet: function (response) {
                        if (response.d == "error") {

                            $("#modalNotifyOnlyClose").find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>');
                            $("#modalNotifyOnlyClose").find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111025") %>');
                            $("#modalNotifyOnlyClose").modal('show');

                        } else if (response.d == "new") {

                        } else {
                            var xmlDoc = $.parseXML(response.d);
                            var xml = $(xmlDoc);
                            var infors = xml.find("Table1");

                            $.each(infors, function (index) {
                                var infor = $(this);

                                $("#sltTimeTogether").val($(this).find("F1").text());
                                $("#sltFatherRelationsLevel").val($(this).find("F2").text());
                                $("#sltMotherRelationsLevel").val($(this).find("F3").text());
                                $("#sltBrotherRelationsLevel").val($(this).find("F4").text());
                                $("#sltSistersRelationsLevel").val($(this).find("F5").text());
                                $("#sltRelativeRelationsLevel").val($(this).find("F6").text());
                                $('input:radio[name=rdoTakeCareChildren][value=' + $(this).find("F7").text() + ']').click();
                                $("#iptTakeCareChildrenOther").val($(this).find("F8").text());
                                $("#iptHouseholdIncome").val($(this).find("F9").text());
                                $("#iptExpensesFrom").val($(this).find("F10").text());
                                $("#iptExtraWork").val($(this).find("F11").text());
                                $("#iptExtraWorkIncome").val($(this).find("F12").text());
                                $("#iptCarryMoneySchool").val($(this).find("F13").text());
                                $('input:radio[name=rdoHelpFromSchool][value=' + $(this).find("F14").text() + ']').click();
                                $("#iptHelpFromSchoolOther").val($(this).find("F15").text());
                                $('input:radio[name=rdoHelpFamilyReceived][value=' + $(this).find("F16").text() + ']').click();
                                $("#iptHelpFamilyReceivedOther").val($(this).find("F17").text());
                                $("#iptParentsConcerns").val($(this).find("F18").text());

                                $('input:radio[name=rdoHealth][value=' + $(this).find("F19").text() + ']').click();
                                $('input:radio[name=rdoWelfare][value=' + $(this).find("F20").text() + ']').click();
                                $("#iptDistanceSchool").val($(this).find("F21").text());
                                $("#iptTimeSchoolHour").val($(this).find("F22").text());
                                $("#iptTimeSchoolMinute").val($(this).find("F23").text());
                                $('input:radio[name=rdoTravelMethod][value=' + $(this).find("F24").text() + ']').click();
                                $("#iptTravelMethodOther").val($(this).find("F25").text());
                                $('input:radio[name=rdoLivingConditions][value=' + $(this).find("F26").text() + ']').click();
                                $('input:radio[name=rdoStudentWorkFamily][value=' + $(this).find("F27").text() + ']').click();
                                $("#iptStudentWorkFamilyOther").val($(this).find("F28").text());
                                $('input:radio[name=rdoHobby][value=' + $(this).find("F29").text() + ']').click();
                                $("#iptHobbyOther").val($(this).find("F30").text());
                                $('input:radio[name=rdoSubstanceAbuseBehavior][value=' + $(this).find("F31").text() + ']').click();
                                $('input:radio[name=rdoViolenceBehavior][value=' + $(this).find("F32").text() + ']').click();
                                $('input:radio[name=rdoSexualBehavior][value=' + $(this).find("F33").text() + ']').click();
                                $('input:radio[name=rdoGameAddiction][value=' + $(this).find("F34").text() + ']').click();
                                $("#iptGameAddictionOther").val($(this).find("F35").text());
                                $('input:radio[name=rdoInternetAccess][value=' + $(this).find("F36").text() + ']').click();
                                $('input:radio[name=rdoUsingElectronicTools][value=' + $(this).find("F37").text() + ']').click();
                                $('input:radio[name=rdoInformant][value=' + $(this).find("F38").text() + ']').click();

                                if (!$.isBlank($(this).find("F39").text())) {
                                    $("#divPhotosOutsideHome .img-photo").attr("src", "/Handles/GetImageHandler.ashx?cl=visit-out&im=" + $(this).find("F39").text() + "&year=<%=Request.QueryString["year"]%>&id=<%=Request.QueryString["vid"]%>");
                                }
                                if (!$.isBlank($(this).find("F40").text())) {
                                    $("#divPhotosInsideHome .img-photo").attr("src", "/Handles/GetImageHandler.ashx?cl=visit-in&im=" + $(this).find("F40").text() + "&year=<%=Request.QueryString["year"]%>&id=<%=Request.QueryString["vid"]%>");
                                }
                                if (!$.isBlank($(this).find("F41").text()) && !$.isBlank($(this).find("F42").text())) {
                                    if (marker) {
                                        var lat = parseFloat($(this).find("F41").text());
                                        var lng = parseFloat($(this).find("F42").text());

                                        placeMarker({ lat: lat, lng: lng });
                                        map.setCenter({ lat: lat, lng: lng });
                                    } else {
                                        gpsVariable.Latitude = parseFloat($(this).find("F41").text());
                                        gpsVariable.Longitude = parseFloat($(this).find("F42").text());
                                    }
                                }
                                else {
                                    gpsVariable.Latitude = null;
                                    gpsVariable.Longitude = null;
                                }

                            });

                        }
                    },
                    SaveItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "VisitHouseData.aspx/SaveItem",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: visitHouseForm.OnSuccessSave,
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    OnSuccessSave: function (response) {
                        var title = "";
                        var body = "";

                        switch (response.d) {
                            case "complete":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';

                                $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').on('click', function () {

                                    // Close modal
                                    visitHouseForm.ClearSession(function () {
                                        modalForm.hideForm();

                                        visitHouseList.ReloadListData();
                                    });

                                });

                                break;
                            case "warning":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111026") %>';

                                break;
                            case "error":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111027") %>';

                                break;
                            default: break;
                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);

                        $('#modalNotifyOnlyClose').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                        $("#modalNotifyOnlyClose").modal('show');
                    },
                    ClearSession: function (callbackRedirect) {
                        $.ajax({
                            type: "POST",
                            url: "VisitHouseData.aspx/ClearSessionID",
                            data: '{}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                callbackRedirect();
                            },
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    }
                }

                $(document).ready(function () {

                    $(".visitHouseForm #save").bind({
                        click: function () {

                            $('#modalNotifyConfirmSave').css('z-index', parseInt($('#modalShowForm').css('z-index')) + 1);
                            $('#modalNotifyConfirmSave').modal('show');

                            return false;
                        }
                    });

                    $(".visitHouseForm #cancel").bind({
                        click: function () {

                            // Close modal
                            visitHouseForm.ClearSession(function () {
                                modalForm.hideForm();
                            });

                            return false;
                        }
                    });

                    // Modal Section
                    $('#modalNotifyConfirmSave').on('show.bs.modal', function (e) {
                        $(this).find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101166") %>');
                        $(this).find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101167") %>');
                    });
                    $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').on('click', function () {
                        $('#modalNotifyConfirmSave').modal('hide');

                        $('#modalWaitDialog').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                        $("#modalWaitDialog").modal('show');

                        // Save command
                        var data = new Array();
                        data[0] = "0";
                        data[1] = $("#sltTimeTogether").children("option:selected").val();
                        data[2] = $("#sltFatherRelationsLevel").children("option:selected").val();
                        data[3] = $("#sltMotherRelationsLevel").children("option:selected").val();
                        data[4] = $("#sltBrotherRelationsLevel").children("option:selected").val();
                        data[5] = $("#sltSistersRelationsLevel").children("option:selected").val();
                        data[6] = $("#sltRelativeRelationsLevel").children("option:selected").val();
                        data[7] = $("input[name='rdoTakeCareChildren']:checked").val();
                        data[8] = $("#iptTakeCareChildrenOther").val();
                        data[9] = $("#iptHouseholdIncome").val();
                        data[10] = $("#iptExpensesFrom").val();
                        data[11] = $("#iptExtraWork").val();
                        data[12] = $("#iptExtraWorkIncome").val();
                        data[13] = $("#iptCarryMoneySchool").val();
                        data[14] = $("input[name='rdoHelpFromSchool']:checked").val();
                        data[15] = $("#iptHelpFromSchoolOther").val();
                        data[16] = $("input[name='rdoHelpFamilyReceived']:checked").val();
                        data[17] = $("#iptHelpFamilyReceivedOther").val();
                        data[18] = $("#iptParentsConcerns").val();

                        data[19] = $("input[name='rdoHealth']:checked").val();
                        data[20] = $("input[name='rdoWelfare']:checked").val();
                        data[21] = $("#iptDistanceSchool").val();
                        data[22] = $("#iptTimeSchoolHour").val();
                        data[23] = $("#iptTimeSchoolMinute").val();
                        data[24] = $("input[name='rdoTravelMethod']:checked").val();
                        data[25] = $("#iptTravelMethodOther").val();
                        data[26] = $("input[name='rdoLivingConditions']:checked").val();
                        data[27] = $("input[name='rdoStudentWorkFamily']:checked").val();
                        data[28] = $("#iptStudentWorkFamilyOther").val();
                        data[29] = $("input[name='rdoHobby']:checked").val();
                        data[30] = $("#iptHobbyOther").val();
                        data[31] = $("input[name='rdoSubstanceAbuseBehavior']:checked").val();
                        data[32] = $("input[name='rdoViolenceBehavior']:checked").val();
                        data[33] = $("input[name='rdoSexualBehavior']:checked").val();
                        data[34] = $("input[name='rdoGameAddiction']:checked").val();
                        data[35] = $("#iptGameAddictionOther").val();
                        data[36] = $("input[name='rdoInternetAccess']:checked").val();
                        data[37] = $("input[name='rdoUsingElectronicTools']:checked").val();
                        data[38] = $("input[name='rdoInformant']:checked").val();

                        data[39] = $("#divPhotosOutsideHome").attr("data-filename");
                        data[40] = $("#divPhotosInsideHome").attr("data-filename");
                        data[41] = marker.getPosition().lat();
                        data[42] = marker.getPosition().lng();


                        visitHouseForm.SaveItem(data);

                    });

                    $(".visitHouseForm input[name='rdoTakeCareChildren']").change(function () {
                        if ($("input[name='rdoTakeCareChildren']:checked").val() == "4") {
                            $('#iptTakeCareChildrenOther').show();
                        }
                        else {
                            $('#iptTakeCareChildrenOther').hide();
                        }
                    });

                    $(".visitHouseForm input[name='rdoHelpFromSchool']").change(function () {
                        if ($("input[name='rdoHelpFromSchool']:checked").val() == "4") {
                            $('#iptHelpFromSchoolOther').show();
                        }
                        else {
                            $('#iptHelpFromSchoolOther').hide();
                        }
                    });

                    $(".visitHouseForm input[name='rdoHelpFamilyReceived']").change(function () {
                        if ($("input[name='rdoHelpFamilyReceived']:checked").val() == "3") {
                            $('#iptHelpFamilyReceivedOther').show();
                        }
                        else {
                            $('#iptHelpFamilyReceivedOther').hide();
                        }
                    });

                    $(".visitHouseForm #btnUserSave").click(function () {

                        $('#modalNotifyConfirmSave').css('z-index', parseInt($('#modalShowForm').css('z-index')) + 1);
                        $('#modalNotifyConfirmSave').modal('show');

                        return false;
                    });
                    $(".visitHouseForm #btnUserNext").click(function () {

                        $('.visitHouseForm a[href="#smile"]').click();

                        return false;
                    });

                    $(".visitHouseForm input[name='rdoTravelMethod']").change(function () {
                        if ($("input[name='rdoTravelMethod']:checked").val() == "8") {
                            $('#iptTravelMethodOther').show();
                        }
                        else {
                            $('#iptTravelMethodOther').hide();
                        }
                    });

                    $(".visitHouseForm input[name='rdoStudentWorkFamily']").change(function () {
                        if ($("input[name='rdoStudentWorkFamily']:checked").val() == "6") {
                            $('#iptStudentWorkFamilyOther').show();
                        }
                        else {
                            $('#iptStudentWorkFamilyOther').hide();
                        }
                    });

                    $(".visitHouseForm input[name='rdoHobby']").change(function () {
                        if ($("input[name='rdoHobby']:checked").val() == "9") {
                            $('#iptHobbyOther').show();
                        }
                        else {
                            $('#iptHobbyOther').hide();
                        }
                    });

                    $(".visitHouseForm input[name='rdoGameAddiction']").change(function () {
                        if ($("input[name='rdoGameAddiction']:checked").val() == "10") {
                            $('#iptGameAddictionOther').show();
                        }
                        else {
                            $('#iptGameAddictionOther').hide();
                        }
                    });

                    $(".visitHouseForm #btnSmileSave").click(function () {

                        $('#modalNotifyConfirmSave').css('z-index', parseInt($('#modalShowForm').css('z-index')) + 1);
                        $('#modalNotifyConfirmSave').modal('show');

                        return false;
                    });
                    $(".visitHouseForm #btnSmileBack").click(function () {

                        $('.visitHouseForm a[href="#user"]').click();

                        return false;
                    });
                    $(".visitHouseForm #btnSmileNext").click(function () {

                        $('.visitHouseForm a[href="#picture"]').click();

                        return false;
                    });

                    $(".visitHouseForm #btnPictureSave").click(function () {

                        $('#modalNotifyConfirmSave').css('z-index', parseInt($('#modalShowForm').css('z-index')) + 1);
                        $('#modalNotifyConfirmSave').modal('show');

                        return false;
                    });
                    $(".visitHouseForm #btnPictureBack").click(function () {

                        $('.visitHouseForm a[href="#smile"]').click();

                        return false;
                    });

                    $(".visitHouseForm #divPhotosOutsideHome .div-browse").bind({
                        click: function () {

                            $('.visitHouseForm #divPhotosOutsideHome input[type="file"]').trigger('click');

                            return false;
                        }
                    });

                    $('.visitHouseForm #divPhotosOutsideHome input[type="file"]').change(function () {

                        UploadFile('.visitHouseForm #divPhotosOutsideHome', 'file_visit_out_', 'tmp-visit-out');

                    });

                    $(".visitHouseForm #divPhotosInsideHome .div-browse").bind({
                        click: function () {

                            $('.visitHouseForm #divPhotosInsideHome input[type="file"]').trigger('click');

                            return false;
                        }
                    });

                    $('.visitHouseForm #divPhotosInsideHome input[type="file"]').change(function () {

                        UploadFile('.visitHouseForm #divPhotosInsideHome', 'file_visit_in_', 'tmp-visit-in');

                    });

                    // Initial data


                    // Load info command
                    visitHouseForm.GetItem(<%=Request.QueryString["year"]%>, <%=string.IsNullOrEmpty(Request.QueryString["vid"])?"0":Request.QueryString["vid"]%>, <%=Request.QueryString["sid"]%>, <%=Request.QueryString["term"]%>);

                });

            </script>
        </asp:View>
        <asp:View ID="ViewContent" runat="server">
            <div class="visitHouseView" style="padding: 15px;">
                <div class="row">
                    <div class="col-md-12 mb-12 text-center" style="padding-bottom: 10px;">
                        <button id="btnPrint" class="btn btn-info"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106076") %></button>
                    </div>
                </div>
                <iframe id="iPrint" name="iPrint" src="VisitHousePrint.aspx?year=<%=Request.QueryString["year"]%>&vid=<%=Request.QueryString["vid"]%>" style="height: 700px; width: 100%; border: none;"></iframe>
            </div>
            <script type="text/javascript">

                $(document).ready(function () {

                    $('.visitHouseView #btnPrint').click(function () {

                        window.frames["iPrint"].focus();
                        window.frames["iPrint"].print();

                        return false;
                    });

                });

            </script>
        </asp:View>
        <asp:View ID="FormScheduleContent" runat="server">
            <div class="visitHouseFormSchedule" style="padding: 15px;">
                <form class="form-padding">
                    <div class="row div-row-padding">
                        <div class="col-md-3 mb-3 text-align-end">
                            <label for="divStampDate"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %></label>
                        </div>
                        <div class="col-md-9 mb-9">
                            <div class='input-group date' id='divStampDate'>
                                <input type='text' class="form-control"
                                    placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>" />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row div-row-padding">
                        <div class="col-md-3 mb-3 text-align-end">
                            <label for="divStampTime"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %></label>
                        </div>
                        <div class="col-md-9 mb-9">
                            <div class='input-group date' id='divStampTime'>
                                <input type="text" class="form-control" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105024") %>" />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-time"></span>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row text-center">
                        <button id="save" type="submit" class="btn btn-primary"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                        <button id="cancel" type="button"
                            class="btn btn-danger" data-dismiss="modal">
                            <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></button>
                    </div>
                </form>
            </div>
            <script type="text/javascript">

                var visitHouseFormSchedule = {
                    GetItem: function (year, vid) {
                        $.ajax({
                            type: "POST",
                            url: "VisitHouseData.aspx/GetItemSchedule",
                            data: '{year: ' + year + ', vid: ' + vid + '}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: visitHouseFormSchedule.OnSuccessGet,
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    OnSuccessGet: function (response) {
                        if (response.d == "error") {

                            $("#modalNotifyOnlyClose").find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>');
                            $("#modalNotifyOnlyClose").find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111025") %>');
                            $("#modalNotifyOnlyClose").modal('show');

                        } else if (response.d == "new") {

                        } else {
                            var xmlDoc = $.parseXML(response.d);
                            var xml = $(xmlDoc);
                            var infors = xml.find("Table1");

                            $.each(infors, function (index) {
                                var infor = $(this);

                                $("#divStampDate > :input").val($(this).find("F1").text());
                                $("#divStampTime > :input").val($(this).find("F2").text());

                            });

                        }
                    },
                    SaveItem: function (data) {
                        $.ajax({
                            type: "POST",
                            url: "VisitHouseData.aspx/SaveItemSchedule",
                            data: JSON.stringify({ data: data }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: visitHouseFormSchedule.OnSuccessSave,
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    },
                    OnSuccessSave: function (response) {
                        var title = "";
                        var body = "";

                        switch (response.d) {
                            case "complete":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701015") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101168") %>';

                                $('#modalNotifyOnlyClose').find('.modal-footer #modalClose').on('click', function () {

                                    // Close modal
                                    visitHouseFormSchedule.ClearSession(function () {
                                        modalForm.hideForm();

                                        visitHouseList.ReloadListData();
                                    });

                                });

                                break;
                            case "warning":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00229") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111026") %>';

                                break;
                            case "error":
                                title = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101242") %>';
                                body = '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111027") %>';

                                break;
                            default: break;
                        }

                        $("#modalWaitDialog").modal('hide');

                        $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                        $("#modalNotifyOnlyClose").find('.modal-body p').text(body);

                        $('#modalNotifyOnlyClose').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                        $("#modalNotifyOnlyClose").modal('show');
                    },
                    ClearSession: function (callbackRedirect) {
                        $.ajax({
                            type: "POST",
                            url: "VisitHouseData.aspx/ClearSessionID",
                            data: '{}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                callbackRedirect();
                            },
                            failure: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111003") %>');

                                $("#modalWaitDialog").modal('hide');
                            },
                            error: function (response) {
                                alert('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111004") %>');

                                $("#modalWaitDialog").modal('hide');
                            }
                        });
                    }
                }

                $(document).ready(function () {

                    $(".visitHouseFormSchedule #save").bind({
                        click: function () {

                            $('#modalNotifyConfirmSave').css('z-index', parseInt($('#modalShowForm').css('z-index')) + 1);
                            $('#modalNotifyConfirmSave').modal('show');

                            return false;
                        }
                    });

                    $(".visitHouseFormSchedule #cancel").bind({
                        click: function () {

                            // Close modal
                            visitHouseFormSchedule.ClearSession(function () {
                                modalForm.hideForm();
                            });

                            return false;
                        }
                    });

                    // Modal Section
                    $('#modalNotifyConfirmSave').on('show.bs.modal', function (e) {
                        $(this).find('.modal-title').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101166") %>');
                        $(this).find('.modal-body p').text('<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101167") %>');
                    });
                    $('#modalNotifyConfirmSave').find('.modal-footer #modalConfirmSave').on('click', function () {
                        $('#modalNotifyConfirmSave').modal('hide');

                        $('#modalWaitDialog').css('z-index', parseInt($('#modalNotifyConfirmSave').css('z-index')) + 1);
                        $("#modalWaitDialog").modal('show');

                        // Save command
                        var data = new Array();
                        data[0] = "0";
                        data[1] = $("#divStampDate > :input").val();
                        data[2] = $("#divStampTime > :input").val();

                        visitHouseFormSchedule.SaveItem(data);

                    });

                    // Initial data



                    $('#divStampDate').datetimepicker({
                        format: 'DD/MM/YYYY-BE',
                        locale: 'th'
                    });
                    $('#divStampTime').datetimepicker({
                        format: 'HH:mm'
                    });

                    // Load info command
                    visitHouseFormSchedule.GetItem(<%=Request.QueryString["year"]%>, <%=string.IsNullOrEmpty(Request.QueryString["vid"])?"0":Request.QueryString["vid"]%>);

                });

            </script>
        </asp:View>
    </asp:MultiView>
</body>
</html>
