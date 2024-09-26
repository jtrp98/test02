<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true"
    CodeBehind="app-permissions.aspx.cs" Inherits="FingerprintPayment.app_permissions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/bootstrap-select.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-select.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap-toggle.js" type="text/javascript"> </script>
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <link href="/Content/bootstrap-toggle.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="/shop/createTable.js" type="text/javascript"></script>
    <link href="/Content/bootstrap-checkbox.css" rel="stylesheet" />
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>

    <style type="text/css">
        table.tableSection {
            display: table;
            width: 100%;
        }

            table.tableSection thead, table.tableSection tbody {
                float: left;
                width: 100%;
            }

                table.tableSection thead th {
                    vertical-align: top;
                }

            table.tableSection tbody {
                overflow-y: scroll;
                /* Giving height to make the tbody scroll */
                /* Giving height dynamically is recommended */
                height: 200px;
            }

            table.tableSection tr {
                width: 100%;
                display: table;
                /* Keeping the texts of both thead and tbody in same alignment */
                text-align: left;
            }

            table.tableSection th, table.tableSection td {
                width: 33%;
            }

            table.tableSection tr > td:last-child {
                /* removing fraction of width i.e 2% to align the tbody columns with thead columns. */
                /* It is must as we need to consider the tbody scroll width too */
                /* if the width is in pixels, then (width - 18px) would be enough */
                width: 31%;
            }
            /** for older browsers (IE8), if you know number of columns in your table **/
            table.tableSection tr > td:first-child + td + td {
                width: 31%;
            }

            table.tableSection thead {
                padding-right: 18px; /* 18px is approx. value of width of scroll bar */
                /*width: calc(100% - 20px);*/
            }

        .ui-autocomplete {
            z-index: 9999;
        }

        .nav-tabs > li.active > a, .nav-tabs > li.active > a:hover, .nav-tabs > li.active > a:focus {
            background-color: #337ab7;
            color: white;
        }

        /* The container */
        .container {
            display: block;
            position: relative;
            padding-left: 35px;
            margin-bottom: 12px;
            cursor: pointer;
            font-size: 22px;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
        }

            /* Hide the browser's default radio button */
            .container input {
                position: absolute;
                opacity: 0;
                cursor: pointer;
            }

        /* Create a custom radio button */
        .checkmark {
            position: absolute;
            top: 0;
            left: 0;
            height: 25px;
            width: 25px;
            background-color: #eee;
            border-radius: 50%;
        }

        /* On mouse-over, add a grey background color */
        .container:hover input ~ .checkmark {
            background-color: #ccc;
        }

        /* When the radio button is checked, add a blue background */
        .container input:checked ~ .checkmark {
            background-color: #2196F3;
        }

        /* Create the indicator (the dot/circle - hidden when not checked) */
        .checkmark:after {
            content: "";
            position: absolute;
            display: none;
        }

        /* Show the indicator (dot/circle) when checked */
        .container input:checked ~ .checkmark:after {
            display: block;
        }

        /* Style the indicator (dot/circle) */
        .container .checkmark:after {
            top: 9px;
            left: 9px;
            width: 8px;
            height: 8px;
            border-radius: 50%;
            background: white;
        }

        .dropdown-toggle {
            border-radius: 0%;
        }

        .input-group-addon {
            font-size: 22px;
        }
    </style>
    <script type="text/javascript">
        var pageSize = 20, pageNumber = 1;
        var wording = "", user_type = "";
        var table_emp = [];
        $user_id = 0;
        var load_data = <%= returnlist(new Search { pageNumber = 1, pageSize = 20, wording = "" ,user_type = ""})  %>;
        var temp = new TemplateTable();
        var availableValueEmployees = [];
        $(document).ready(function () {

            temp.PageSetting({ 'pageSize': pageSize, 'pageNumber': pageNumber });
            temp.TemplateSetting({ template_id: "#tmpl-mustache", target_name: "#target" });
            temp.RenderRows(load_data);

            $("#pageNumber").change(function () {
                pageNumber = parseInt($(this).val());
                loaddata();
                document.documentElement.scrollTo(0, 0);
            });

            $("#pageSize").change(function () {
                pageNumber = 1;
                pageSize = parseInt($(this).val());
                loaddata();
                document.documentElement.scrollTo(0, 0);
            });

            $("#txtSearch").keypress(function (e) {
                if (e.keyCode === 13) {
                    e.preventDefault();
                    pageNumber = 1;
                    user_type = $("#select_type option:selected").val();
                    wording = $("#txtSearch").val();
                    loaddata();
                }
            })

            $("#divsearch").click(function () {
                user_type = $("#select_type option:selected").val();
                pageNumber = 1;
                wording = $("#txtSearch").val();
                loaddata();
            });

            $("#btncancel").click(function () {
                $(".employeeslist-container").show();
                $(".permission-container").addClass("hidden");
            });

            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=teacher",
                dataType: "json",
                success: function (objjson) {
                    $.each(objjson, function (index) {
                        var newObject = {
                            label: objjson[index].sName + ' ' + objjson[index].sLastname,
                            value: objjson[index].sEmp,
                            "nDepartmentId": objjson[index].nDepartmentId,
                            "cType": objjson[index].cType
                        };
                        availableValueEmployees[index] = newObject;
                    });
                }
            });

            PageMethods.getEmployeeTypes(function (response) {
                $("#select_type option").remove();
                $("#select_type").append("<option value=\"\"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>");
                $.each(response, function (e, s) {
                    $("#select_type").append("<option value=\"" + s.nTypeId + " \"> " + s.Title);
                });
                $("#select_type").selectpicker('refresh');
            });

            $('#txtSearch').autocomplete({
                width: 300,
                max: 10,
                delay: 100,
                minLength: 1,
                autoFocus: true,
                cacheLength: 1,
                scroll: true,
                highlight: false,
                source: function (request, response) {
                    var data = availableValueEmployees;
                    if ($("#select_type option:selected").val() !== "") data = getObjects(availableValueEmployees, "cType", $("#select_type option:selected").val());
                    //if ($("#select_department option:selected").val() !== "") data = getObjects(availableValueEmployees, "nDepartmentId", $("#select_department option:selected").val());
                    results = $.ui.autocomplete.filter(data, request.term);
                    response(results.slice(0, 10));
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $("#txtSearch").val(ui.item.label);
                },
                focus: function (event, ui) {
                    event.preventDefault();
                }
            });
        })

        function select_all(group_id, values) {
            var _select = $("#" + group_id + " select");
            $.each(_select, function (e, s) {
                if ($(s).attr("menuid") != undefined) {
                    if ($(s).children("option[value=" + values + "]").length > 0) {
                        $(s).val(values);
                    } else if ($(s).children("option[value=1]").length > 0) {
                        $(s).val(1);
                    }
                }
            });
        }

        function selectGroup_all(group_id, values) {
            var _select = $("#groupmenu_" + group_id + " .col-lg-4 select");
            $.each(_select, function (e, s) {
                if ($(s).attr("menuid") != undefined) {
                    if ($(s).children("option[value=" + values + "]").length > 0) {
                        $(s).val(values);
                    } else if ($(s).children("option[value=1]").length > 0) {
                        $(s).val(1);
                    }
                }
            });
        }

        function UpdatePermission() {
            $("body").mLoading();
            var select_web = $("#tab1primary select");
            var web_permission = [];
            $.each(select_web, function (e, s) {
                if ($(s).attr("menuid") != undefined)
                    web_permission.push({
                        "menu_id": $(s).attr("menuid"),
                        "status": $(s).val() === null ? 1 : $(s).val(),
                    });
            });

            var select_mobile = $("#tab2primary select");
            var mobile_permission = [];
            $.each(select_mobile, function (e, s) {
                if ($(s).attr("menuid") != undefined)
                    mobile_permission.push({
                        "menu_id": $(s).attr("menuid"),
                        "status": $(s).val() === null ? 1 : $(s).val()
                    });
            });

            console.log(web_permission);
            console.log(mobile_permission);
            PageMethods.UpdatePermission(
                {
                    "user_id": $user_id,
                    "Web_menu": web_permission,
                    "Mobile_menu": mobile_permission
                },
                function (e) {
                    console.log(e);
                    $(".employeeslist-container").show();
                    $(".permission-container").hide();
                    $("body").mLoading('hide');
                },
                function (e) {
                    console.log(e);
                });
        }

        function loaddata() {
            var data = { "wording": wording, "pageSize": pageSize, "pageNumber": pageNumber, "user_type": user_type }
            $('#target').html('<tr><td colspan="7" class="text-center"><br/><i class="fa fa-spin fa-refresh fa-3x"></i><h1>Loading</h1><br/></td></tr>');
            PageMethods.returnlist(data,
                function (respones) {
                    respones = $.parseJSON(respones);
                    console.log(respones);
                    temp.PageSetting({ 'pageSize': pageSize, 'pageNumber': pageNumber });
                    temp.TemplateSetting({ template_id: "#tmpl-mustache", target_name: "#target" });
                    temp.RenderRows(respones);
                },
                function (respones) {
                    console.log(respones);
                }
            )
        }

        function showpermission(user_id) {
            $("body").mLoading();
            $user_id = user_id;
            PageMethods.get_permission(user_id, function (result) {
                result = $.parseJSON(result);
                console.log(result);
                var result_web = result.web;
                var result_mobile = result.mobile;
                $(this).removeClass("disabled");
                $(".employeeslist-container").hide();
                $(".permission-container").show();
                $("#btnsave").removeClass("disabled");
                $("#label_name").html(result_web.employessname);

                var template = $('#template').html();
                var template_mobile = $('#template-mobile').html();
                var data = [];

                //Mustache.parse(template);   // optional, speeds up future uses
                var rendered = Mustache.render(template, result_web);
                $(".permission-container").removeClass("hidden");
                $('#tab1primary').html(rendered);

                //Mustache.parse(template_mobile);   // optional, speeds up future uses
                var rendered_mobile = Mustache.render(template_mobile, result);
                $('#tab2primary').html(rendered_mobile);

                $("#btnsave").click(function () {
                    $(this).addClass("disabled");
                    UpdatePermission();
                    this.preventDefault();
                    //console.log(menu);
                });

                $("body").mLoading('hide');
            }, function () {

            });
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />

    <div class="full-card box-content employeeslist-container">
        <div class="row">
            <div class="col-md-6 col-lg-6">
                <div class="input-group my-group">
                    <div class="input-group-addon h2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102162") %></div>
                    <select id="select_type" class="selectpicker" style="width: 20%; border-radius: unset;">
                        <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                        <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102165") %></option>
                        <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131042") %></option>
                    </select>
                    <input type="text" id="txtSearch" class='form-control' placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %>" style="height: 48px;" />
                    <div class="input-group-addon btn" id="divsearch"><i class="fa fa-search"></i>&nbsp; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %></div>
                </div>
                <%--               <div class="input-append">
                    <span class=" btn-group">
                        <select id="select_type" class="form-control btn">
                            <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %></option>
                            <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102165") %></option>
                            <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131042") %></option>
                        </select>
                    </span>
                    <input type="text" id="txtSearch" class='span2' placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M601005") %>" />
                    <button class="btn btn-group" onclick="Search();">
                        <i class="fa fa-search"></i>
                    </button>
                </div>--%>
            </div>
        </div>

        <script id="tmpl-mustache" type="x-tmpl-mustache">
        {{#DATA}}
            <tr class="itemCell" style="font-weight: normal; font-style: normal; text-decoration: none;">
                <td class="centertext">
                    {{index}}
                </td>
                <td class="paymentgroup_name center">{{emp_type}}</td>
                <td class="paymentgroup_name center">{{emp_name}}</td>
                <td class="paymentgroup_name center">{{emp_lastname}}</td>
                <td class="paymentgroup_name center">{{phone}}</td>
                <td class="paymentgroup_name center">{{birth}}</td>
                <td class="centertext center">
                     <div class="checkbox checkbox-primary">
                        <a class="btn btn-warning col-md-5 col-xs-11 pull-right" style="cursor: pointer;" onclick="showpermission({{emp_id}})"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701028") %></a>
                    </div>
                </td>
            </tr>
        {{/DATA}}
        </script>
        <div class="row mini--space__top">
            <div class="col-xs-12">
                <div class="wrapper-table">
                    <table class="cool-table" cellspacing="0" cellpadding="2" border="0"
                        style="font-weight: normal; font-style: normal; text-decoration: none; width: 100%; border-collapse: collapse;">
                        <thead>
                            <tr class="headerCell" style="font-weight: bold; font-style: normal; text-decoration: none;">
                                <th class="center" scope="col" style="width: 5%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101014") %>
                                </th>
                                <th class="center" scope="col" style="width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131044") %></th>
                                <th class="center" scope="col" style="width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101018") %></th>
                                <th class="center" scope="col" style="width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></th>
                                <th class="center" scope="col" style="width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></th>
                                <th class="center" scope="col" style="width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101075") %></th>
                                <th class="centertext" scope="col" style="width: 20%;"></th>
                            </tr>
                        </thead>
                        <tbody id="target">
                            <tr>
                                <td colspan="7" class="text-center"><i class="fa fa-spin fa-refresh"></i>
                                    <h1>Loading</h1>
                                </td>
                            </tr>
                        </tbody>
                        <tfoot>
                            <tr style="color: #337AB7; background-color: #337AB7; border-color: #337AB7;">
                                <td colspan="7">
                                    <table width="100%" class="tab" style="border-collapse: separate;">
                                        <tr>
                                            <td style="width: 40%">
                                                <div class="row">
                                                    <div class="col-md-8 col-xs-12" style="margin-top: 10px;">
                                                        <span style="color: White; border-color: #337AB7;" class="pull-right"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102242") %>:</span>
                                                    </div>
                                                    <div class="col-md-4 col-xs-12">
                                                        <select id="pageSize" class="selectpicker form-control">
                                                            <option selected="selected" value="20">20</option>
                                                            <option value="50">50</option>
                                                            <option value="100">100</option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </td>
                                            <td style="width: 30%">
                                                <div class="row">
                                                    <div class="col-md-4 col-xs-12">
                                                        <div id="backbutton" style="color: White; border-color: #337AB7; margin-top: 10px;" onclick="changePage(-1)">
                                                            <span style="cursor: pointer;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101378") %></span>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 col-xs-12">
                                                        <select id="pageNumber" class="selectpicker form-control" data-size="10">
                                                            <option selected="selected" value="1">1</option>
                                                            <option value="2">2</option>
                                                        </select>
                                                    </div>
                                                    <div class="col-md-4 col-xs-12">
                                                        <div id="nextbutton" style="color: White; border-color: #337AB7; margin-top: 10px;" onclick="changePage(1)">
                                                            <span style="cursor: pointer;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td style="width: 30%; text-align: right">
                                                <span id="spane_pageNumber" style="color: White;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M301036") %></span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tfoot>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div class="full-card box-content permission-container hidden">
        <div class="row">
            <label class="col-lg-2 col-md-12"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101343") %> : </label>
            <label class="col-lg-10 col-md-12" id="label_name"></label>
        </div>
        <div class="row tab1primary">
            <div class="col-lg-12 col-md-12">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131032") %></label>
                <p>
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131036") %></label>
                </p>
                <p>
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131034") %></label>
                </p>
                <p>
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131035") %></label>
                </p>
            </div>
        </div>
        <div class="row tab2primary" style="display: none;">
            <div class="col-lg-12 col-md-12">
                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131032") %></label>
                <p>
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131036") %></label>
                </p>
                <p>
                    <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131037") %></label>
                </p>
            </div>
        </div>
        <div class="row">
            <div class="panel with-nav-tabs panel-default">
                <div class="panel-heading" style="padding: 0px 0px;">
                    <ul class="nav nav-tabs" style="border-bottom: 0px solid #ddd;">
                        <li class="active">
                            <a href="#tab1primary" data-toggle="tab" onclick="$('.tab1primary').show(); $('.tab2primary').hide();">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131046") %></label>
                            </a>
                        </li>
                        <li>
                            <a href="#tab2primary" data-toggle="tab" onclick="$('.tab2primary').show(); $('.tab1primary').hide();">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131045") %></label>
                            </a>
                        </li>
                        <li class="pull-right" style="padding: 5px 5px;">
                            <div class="btn btn-success" id="btnsave"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></div>
                            <div class="btn btn-danger" id="btncancel"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></div>
                        </li>
                    </ul>
                </div>
                <div class="panel-body">
                    <div class="tab-content">
                        <div class="tab-pane fade in active" id="tab1primary">Primary 1</div>
                        <div class="tab-pane fade" id="tab2primary">Primary 2</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script id="template" type="x-tmpl-mustache">
          <div class="row">
                <label class="container col-lg-1">
                </label>
                <label class="container col-lg-2">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131038") %>
                    <input type="radio" name="radio" onclick="select_all('tab1primary',2)">
                    <span class="checkmark"></span>
                </label>
                <label class="container col-lg-2">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131039") %>
                    <input type="radio" name="radio" onclick="select_all('tab1primary',1)">
                    <span class="checkmark"></span>
                </label>
                <label class="container col-lg-3">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131040") %>
                    <input type="radio" name="radio" onclick="select_all('tab1primary',0)">
                    <span class="checkmark"></span>
                </label>
          </div>
          <div class="row--space">
          </div>
          <div class="row">
                <div class="col-lg-12">
                    <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                    {{#groupmenu}}
                        <div class="panel panel-default">
                            <div class="panel-heading" role="tab" id="headingOne">
                                <h4 role="button" class="panel-title" data-toggle="collapse" data-parent="#accordion" 
                                    href="#groupmenu_{{groupmenuid}}" aria-expanded="true" aria-controls="{{groupmenuid}}">
                                    <span class='h2'><span class="fa arrow"></span>{{groupmenu}}</span>
                                </h4>
                            </div>
                            <div id="groupmenu_{{groupmenuid}}" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingOne">
                                <div class="panel-body">
                                    <div class="row bottom">
                                        <div class="col-lg-9">
                                            <div class="row bottom">
                                                <div class="col-lg-8">
                                                    <span class='h2'></span><br/>
                                                </div>
                                                <div class="col-lg-4">
                                                </div>
                                            </div>
                                        {{#menu}}
                                            <div class="row bottom">
                                                <div class="col-lg-8">
                                                    <span class='h2'>{{menuname}}</span><br/>
                                                </div>
                                                <div class="col-lg-4">
                                                    <select menuid="{{menuid}}" class="form-control h3 menu-control" style="margin-top:0px;">
                                                        {{#option}}
                                                        <option value="{{value}}" {{selected}}>{{text}}</option>
                                                        {{/option}}
                                                    </select>
                                                </div>
                                            </div>
                                        {{/menu}}   
                                        </div>
                                        <div class="col-lg-3">
                                             <select class="form-control h3 menu-control" style="margin-top:0px;" onchange="selectGroup_all({{groupmenuid}},$(this).val())">
                                                    <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131048") %></option>
                                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131049") %></option>
                                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131050") %></option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    {{/groupmenu}}
                    </div>
                </div>
            </div>
    </script>

    <script id="template-mobile" type="x-tmpl-mustache">
          <div class="row">
                <label class="container col-lg-1">
                </label>
                <label class="container col-lg-2">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131038") %>
                    <input type="radio" name="radio" onclick="select_all('tab2primary',1)">
                    <span class="checkmark"></span>
                </label>
                <label class="container col-lg-2">
                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131041") %>
                    <input type="radio" name="radio" onclick="select_all('tab2primary',0)">
                    <span class="checkmark"></span>
                </label>
          </div>
          <div class="row--space">
          </div>
          <div class="row">
                <div class="col-lg-12">
                    <div class="panel-group" id="accordion_mobile" role="tablist" aria-multiselectable="true">
                    {{#mobile}}
                        <div class="panel panel-default">
                            <div class="panel-heading" role="tab" id="headingOne">
                                <h4 class="panel-title" role="button" data-toggle="collapse" data-parent="#accordion_mobile" 
                                        href="#mobile_groupmenu_{{menu_id}}" >
                                    <span class='h2'><span class="fa arrow"></span>{{menu_name}}</span>
                                </h4>
                            </div>
                            <div id="mobile_groupmenu_{{menu_id}}" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingOne">
                                <div class="panel-body">
                                    <div class="row bottom">
                                        <div class="col-lg-9">
                                            <div class="row bottom">
                                                <div class="col-lg-8">
                                                    <span class='h2'></span><br/>
                                                </div>
                                                <div class="col-lg-4">
                                                </div>
                                            </div>
                                        {{#submenu}}
                                            <div class="row bottom">
                                                <div class="col-lg-8">
                                                    <span class='h2'>{{submenu_name}}</span><br/>
                                                </div>
                                                <div class="col-lg-4">
                                                    <select menuid="{{submenu_id}}" class="form-control h3 menu-control" style="margin-top:0px;">
                                                        {{#option}}
                                                        <option value="{{value}}" {{selected}}>{{text}}</option>
                                                        {{/option}}
                                                    </select>
                                                </div>
                                            </div>
                                        {{/submenu}}   
                                        </div>
                                        <div class="col-lg-3">
                                             <select class="form-control h3 menu-control" style="margin-top:0px;" onchange="$('#mobile_groupmenu_{{menu_id}} .col-lg-4 select').val($(this).val())">
                                                    <option value="0"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131047") %></option>
                                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131050") %></option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    {{/mobile}}
                    </div>
                </div>
            </div>
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
</asp:Content>
