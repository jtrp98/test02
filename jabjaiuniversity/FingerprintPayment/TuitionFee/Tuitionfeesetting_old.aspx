<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="Tuitionfeesetting_old.aspx.cs" Inherits="FingerprintPayment.TuitionFee.Tuitionfeesetting_old" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-validation-bootstrap-tooltip-master/jquery-validate.bootstrap-tooltip.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap-dialog.js" type="text/javascript"></script>
    <link href="../Content/bootstrap-dialog.css" rel="stylesheet" />
    <style type="text/css">
        table.tableSection {
            display: table;
            width: 100%;
        }

            table.tableSection thead,
            table.tableSection tbody {
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
                height: 420px;
            }

            table.tableSection tr {
                width: 100%;
                display: table;
                /* Keeping the texts of both thead and tbody in same alignment */
                text-align: left;
            }

            table.tableSection th,
            table.tableSection td {
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
                padding-right: 18px;
                /* 18px is approx. value of width of scroll bar */
                /*width: calc(100% - 20px);*/
            }

        .datepicker {
            padding: 0;
        }

        .fa-times {
            color: red;
            cursor: pointer;
        }

        .glyphicon-plus {
            cursor: pointer;
        }

        select {
            text-align: center;
            text-align-last: center;
            /* webkit*/
        }

        option {
            text-align: left;
            /* reset to left*/
        }
    </style>
    <script src="../javascript/jquery-number/jquery.number.js" type="text/javascript"></script>
    <script type="text/javascript">
        var sublevel_data = [],
            class_data = [];
        var group_data = [];
        var invoices = [];
        var i = 0;
        var data_completed = 0,
            data_length = 0;
        let savedata = false;
        var invoice_data = [];

    <%--    var _lblMsg = '<%=lblMsg.ClientID%>';
        $(document).ready(function () {
            var downloadComplete = false;
            var intervalListener = window.setInterval(function () {
                if (!downloadComplete)
                    CallCheckDownload();
            }, 1000);

            function CallCheckDownload() {
                $.ajax({
                    type: "POST",
                    url: "/TuitionFee/Tuitionfeesetting.aspx/CheckDownload",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        updateStatus('completed', r.d);
                        if (r.d.indexOf('000ABORT_CHECK000') > -1) {
                            downloadComplete = true;
                            // Write further code per your requirement  
                        }
                    },
                    error: function (r) {
                        console.log('Check error : ' + r.responseText);
                    },
                    failure: function (r) {
                        console.log('Check failure : ' + r.responseText);
                    }
                });
                if (downloadComplete)
                    window.clearInterval(intervalListener);
            }

            function updateStatus(status, msg) {
                document.getElementById(_lblMsg).innerHTML = msg;
            }
        });
        var intervalListener;
        function StartProgress() {
            var InvoicesGroup_Id = [];
            $.each($("#tableAddRow tbody tr"), function (index, values) {
                if ($(this).find("input[type=checkbox]:checked").prop("checked")) {
                    InvoicesGroup_Id.push(parseInt($(this).find("input[id*=item_id]").val()));
                }
            });

            PageMethods.StartProgress(parseInt($("#year_id").val()), $("#trem_id").val(), InvoicesGroup_Id, function () {

            });

            intervalListener = window.setInterval(function () {
                CallCheckDownload();
            }, 500);
        }

        function CallCheckDownload() {
            PageMethods.CheckDownload(function (result) {
                if (result.indexOf("Completed Progress") > -1) window.clearInterval(intervalListener);
                document.getElementById('<%=lblMsg.ClientID%>').append(result + "<br/>");
                result = $.parseJSON("[" + result + "]");
                console.log(result);
            });
        }--%>

        $(function () {
            addTableRow();
            $("#modalpopup-data .btn-danger").hide();
            PageMethods.getyear(
                function (response) {
                    response = $.parseJSON(response);
                    select_settingdata($("#year_id"), response.data, "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101312") %>", "year_id",
                        "year_name", "");
                },
                function () { });

            $("#year_id").change(function () {
                PageMethods.gettrem($(this).val(),
                    function (response) {
                        getTuitionfeesetting($("#year_id").val(), $("#trem_id").val());
                        response = $.parseJSON(response);
                        //if (response.data == []) addTableRow();
                        select_settingdata($("#trem_id"), response.data, "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202002") %>", "trem_id",
                            "trem_name", "");
                    },
                    function () {

                    });
            });

            $("#modalpopup-data-submit").click(function () {
                $("#modalpopup-data").modal("hide");
            });

            $("#trem_id").change(function () {
                //if ($("#trem_id").val() === "") addTableRow();
                //else
                $("#tableAddRow thead input[type=checkbox]").prop("checked", true);
                getTuitionfeesetting($("#year_id").val(), $("#trem_id").val());
            });

            $("#submit").click(function (e) {
                SaveDataToDataBase();
            });

            $("#submitpeak").click(function () {
                //alert("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133367") %>");
                if (savedata === false) {
                    SaveDataToDataBase();
                    SaveComplete();
                } else {
                    invoice_data = [];
                    getInvoices();
                    //var invoices_List = getInvoicesList();
                    //loading_popup(1, invoices_List.length);
                    //setpeakengine(invoices_List);
                }
            });
        });

        $(document).ready(function () {
            $(".datepicker").datepicker();
            $('#aspnetForm').on('submit', function (event) {

                // adding rules for inputs with class 'comment'
                $($("#tableAddRow tbody").children("tr")).each(function () {
                    addRules($(this).find("[id*=sublevel]").attr("id"),
                        "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133368") %>");
                    addRules($(this).find("[id*=sublevel]").attr("id"),
                        "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203077") %>");
                    addRules($(this).find("[id*=group]").attr("id"),
                        "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133369") %>");
                    addRules($(this).find("[id*=issuedDate]").attr("id"),
                        "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133370") %>");
                    addRules($(this).find("[id*=dueDate]").attr("id"),
                        "กรุณาเลือกวันที่ครบกำหนดชำระเงิน");
                    console.log($(this).find("[id*=sublevel]").attr("id"));
                });

                // prevent default submit action         
                event.preventDefault();

                // test if form is valid 
                if ($('#aspnetForm').valid()) {
                    console.log("validates");
                } else {
                    console.log("does not validate");
                }
            });

            $("#tableAddRow").hide();
            $('.addBtn').on('click', function () {
                addTableRow();
                //$("#submitpeak").hide();
            });

            $('.addBtnRemove').click(function () {
                $(this).closest('tr').remove();
            });

            //Get Group Data
            PageMethods.getgroup(
                function (response) {
                    group_data = $.parseJSON(response).data;
                    if (group_data !== [] && sublevel_data !== []) $("#tableAddRow").show();
                },
                function () { });

            //Get SubLevel Data
            PageMethods.getsublevel(
                function (response) {
                    sublevel_data = $.parseJSON(response).data;
                    if (group_data !== [] && sublevel_data !== []) $("#tableAddRow").show();
                },
                function () {

                });

            //Get Class Data
            PageMethods.getclass(
                function (response) {
                    response = $.parseJSON(response);
                    class_data = response;
                },
                function () {

                });

            $("#aspnetForm").validate({
                rules: {
                    "year_id": "required",
                    "trem_id": "required"
                },
                messages: {
                    "year_id": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133368") %>",
                    "trem_id": "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133368") %>"
                },
                tooltip_options: {
                    "year_id": {
                        placement: 'top',
                        trigger: 'focus'
                    },
                    "trem_id": {
                        placement: 'top',
                        trigger: 'focus'
                    }
                },
                submitHandler: function (e) { }
            });
        });

        //Send Data
        function SaveComplete() {
            setTimeout(function () {
                if (savedata === false) {
                    SaveComplete();
                } else {
                    var invoices_List = getInvoicesList(false);
                    loading_popup();
                    setpeakengine(invoices_List);
                }
            }, 500);
        }

        var index_list = 0;

        function getInvoicesList(rows_all) {
            var tr = $("#tableAddRow tbody").children("tr");
            var invoices_List = [];
            $.each(tr, function (s, e) {
                var row_last = $(e);
                var group_select = row_last.find("[id*=group]");
                var checkbox = row_last.find("input[type=checkbox]");
                var sublevel_select = row_last.find("[id*=sublevel]");
                var class_select = row_last.find("[id*=class]");
                var price_label = row_last.find("[id*=label]");
                var id = row_last.find("[id*=item_id]");
                sublevel_select.prop('disabled', 'disabled');
                class_select.prop('disabled', 'disabled');

                if (checkbox.prop('checked') === true || rows_all) {
                    invoices_List.push({
                        group_id: parseInt(group_select.val()),
                        sublevel_id: parseInt(sublevel_select.val()),
                        class_id: parseInt(class_select.val()),
                        dueDate: $.datepicker.formatDate('mm/dd/yy', $(row_last.find(
                            "[id*=dueDate]")).datepicker(
                                'getDate')),
                        issuedDate: $.datepicker.formatDate('mm/dd/yy', $(row_last.find(
                            "[id*=issuedDate]")).datepicker('getDate')),
                        id: parseInt(id.val())
                    });
                }

            });

            console.log(invoices_List);
            return invoices_List;
        }

        function loading_popup(index, length) {
            $("body").mLoading({
                "text": '<p class="h4" style="color:black;"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133371") %> <br/><br/> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133372") %> <br/><br/> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133374") %> <br/><br/> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133373") %> ( ' +
                    index + ' / ' + length + ' )</p>',
                html: true,
                icon: "/scripts/blockUI/ProgressGreen.gif"
            });
        }

        function loading_popupMessage(message) {
            $("body").mLoading({
                "text": message,
                html: true,
                icon: "/scripts/blockUI/ProgressGreen.gif"
            });
        }

        function loading_sendinvoices_popup(index, length, time) {
            $("body").mLoading({
                "text": '<p class="h4" style="color:black;"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133371") %> <br/><br/> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133375") %>  ' +
                    time +
                    ' <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00836") %> <br/><br/> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133374") %> <br/><br/> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133376") %> <span id="loading_counter"> ( ' +
                    index + ' / ' + length + ' ) </span></p>' +
                    '<br/><p style="color:red;font-size:15px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133377") %></p><p style="color:red;font-size:15px;">ข้อมูลนักเรียน/นักศึกษาที่ได้ทำการแก้ไขในระบบ Peak อาทิเช่น แก้ไขวันที่ครบกำหนดชำระเงิน <br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133380") %></p>',
                html: true,
                icon: "/scripts/blockUI/ProgressGreen.gif"
            });
        }

        function getInvoices() {
            data_completed = 1;
            var InvoicesGroup_Id = [];
            $.each($("#tableAddRow tbody tr"), function (index, values) {
                if ($(this).find("input[type=checkbox]:checked").prop("checked")) {
                    InvoicesGroup_Id.push(parseInt($(this).find("input[id*=item_id]").val()));
                }
            });

            PageMethods.getInvoices(parseInt($("#year_id").val()), $("#trem_id").val(), InvoicesGroup_Id,
                function (e) {
                    e = $.parseJSON(e);
                    if (e.length === 0) {
                        getVoid();
                    } else {
                        data_length = 0;
                        $.each(e, function (Key, Value) {
                            data_length += Value.invoicesStudents.length;
                        });
                        //loading_popup(1, data_length);    
                        var tolaltime = 2 * data_length;
                        var millisecond = tolaltime % 60;
                        var minute = (tolaltime - (tolaltime % 60)) / 60;
                        loading_sendinvoices_popup(data_completed, data_length, minute + "." + millisecond +
                            " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00836") %> - " + (minute + 5) + "." + millisecond);
                        invoice_data = e;
                        StudentLength = 0;
                        InvoiceLength = invoice_data.length;
                        StudentIndex = 0;
                        InvoiceIndex = 0;
                        GetInvoicesData(InvoiceIndex);
                    }
                });
        }

        var JSon_Void = [];
        var index;

        function getVoid() {
            //data_completed = 0;
            $("body").mLoading("hide");
            $("#modalpopup-data").modal("show").find("#message").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133381") %>");
            console.log("Send Void Completed !!");

            //$("#modalpopup-data").modal("show").find("#message").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133381") %>");

            //loading_popupMessage("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133382") %>");
            //PageMethods.getInvoicesVoid(parseInt($("#year_id").val()), $("#trem_id").val(),
            //    function (response) {
            //        JSon_Void = $.parseJSON(response);
            //        data_length = JSon_Void.length;
            //        data_completed = 0;
            //        console.log(JSon_Void);
            //        $("body").mLoading("hide");
            //        if (JSon_Void.length === 0) {
            //            $("#modalpopup-data").modal("show").find("#message").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133381") %>");
            //            console.log("Send Void Completed !!")
            //        } else {
            //            BootstrapDialog.show({
            //                title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01468") %>',
            //                closable: false,
            //                message: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101304") %> Void <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01107") %> Peak',
            //                buttons: [{
            //                    label: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101165") %>',
            //                    cssClass: 'btn-primary',
            //                    action: function (dialog) {
            //                        dialog.close();
            //                        SendVoid(data_completed);
            //                        //dialog.setTitle('Title 1');
            //                    }
            //                },
            //                {
            //                    label: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %>',
            //                    action: function (dialog) {
            //                        dialog.close();
            //                    }
            //                }
            //                ]
            //            });
            //        }
            //    },
            //    function (e) {

            //    });
        }

        function SendVoid(index) {
            PageMethods.SendVoid(JSon_Void[index].Invoices_Id,
                function (e) {
                    console.log(e);
                    data_completed += 1;
                    if (data_completed < data_length) {
                        SendVoid(data_completed);
                        loading_popupMessage("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133383") %> ( " + data_completed + " / " +
                            data_length + " )");
                    } else {
                        $("body").mLoading("hide");
                        $("#modalpopup-data").modal("show").find("#message").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133381") %>");
                        console.log("Send Void Completed !!");
                    }
                },
                function (e) { });
        }

        var StudentLength = 0;
        var InvoiceLength = 0;
        var StudentIndex = 0;
        var InvoiceIndex = 0;
        var DataTmp = [];

        function GetInvoicesData(rowsIndex) {
            DataTmp = invoice_data[rowsIndex];
            StudentLength = DataTmp.invoicesStudents.length;
            GetStudentData(StudentIndex);
            console.log(InvoiceIndex + " / " + InvoiceLength);
        }

        function GetStudentData(rowsIndex) {
            //console.log(rowsIndex + " " + StudentLength)
            if (rowsIndex < StudentLength) {
                var Value1 = invoice_data[InvoiceIndex].invoicesStudents[rowsIndex];
                var data = {
                    "GroupId": invoice_data[InvoiceIndex].GroupId,
                    "Id": invoice_data[InvoiceIndex].Id,
                    "Name": invoice_data[InvoiceIndex].Name,
                    "Update": new Date(parseInt(invoice_data[InvoiceIndex].Update.substring(6))),
                    "School_id": invoice_data[InvoiceIndex].School_id,
                    "invoicesStudents": [{
                        "school_id": Value1.School_id,
                        "issuedDate": new Date(parseInt(Value1.IssuedDate.substring(6))),
                        "dueDate": new Date(parseInt(Value1.DueDate.substring(6))),
                        "tuitionfeeDetail_id": Value1.Id,
                        "contactId": Value1.ContactId,
                        "student_id": Value1.Student_Id,
                        "Id": Value1.Id
                    }],
                    "items": invoice_data[InvoiceIndex].items
                };
                //console.log(data)
                SendPeakEngine(data);
                //console.log((data_completed++) + " / " + data_length);
                //loading_popup(data_completed++, data_length);

            } else {
                InvoiceIndex += 1;
                if (InvoiceIndex < InvoiceLength) {
                    StudentIndex = 0;
                    GetInvoicesData(InvoiceIndex);
                } else {
                    console.log("Send Invoices completed");
                    console.log("completed : " + data_completed + " / " + data_length);
                    //$("#loading_counter").html("( " + data_completed + " / " + data_length + " )");
                    getVoid();
                }
            }
        }

        function SendPeakEngine(Invoices) {
            PageMethods.SetupInvoicess(Invoices,
                function (e) {
                    StudentIndex += 1;
                    if (data_completed > 1) $("#loading_counter").html("( " + data_completed + " / " + data_length + " )");
                    data_completed += 1;
                    GetStudentData(StudentIndex);
                    console.log(e);
                },
                function () {

                });

        }

        function setpeakengine(invoices_List) {
            var data = invoices_List[index_list];
            PageMethods.getinvoice_student({
                year_id: parseInt($("#year_id").val()),
                trem_id: $("#trem_id").val(),
                "Invoices_List": [invoices_List[index_list]]
            },
                function (response) {
                    if (response !== null) {
                        $.each(response, function (index, data) {
                            invoice_data.push(data);
                        });
                    }
                    index_list += 1;
                    if (index_list < invoices_List.length) {
                        loading_popup(index_list, invoices_List.length);
                        setpeakengine(invoices_List);
                    } else {
                        index_list = 0;
                        if (invoice_data.length === 0) {
                            $("body").mLoading("hide");
                            $("#modalpopup-data").modal("show").find("#message").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133381") %>");
                        } else {
                            var tolaltime = 2 * invoice_data.length;
                            var millisecond = tolaltime % 60;
                            var minute = (tolaltime - (tolaltime % 60)) / 60;
                            loading_sendinvoices_popup(index_list + 1, invoice_data.length, (minute + "." +
                                millisecond) + " <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00836") %> - " + (minute + 5) + "." + millisecond);
                            sendinvoices_peak(index_list);
                        }
                    }
                },
                function (e) {
                    console.log(e.StackTrace);
                }
            );
        }

        function sendinvoices_peak(invoices_data_index) {
            console.log(invoice_data[invoices_data_index].name);
            PageMethods.sendinvoice_student(invoice_data[invoices_data_index],
                function (response) {
                    index_list += 1;
                    if (index_list < invoice_data.length) {
                        console.log(response);
                        //$("body").mLoading({ "text": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133384") %> ( ' + (index_list + 1) + ' / ' + invoices_List.length + ' )' });
                        //loading_sendinvoices_popup(index_list, );
                        $("#loading_counter").html("( " + index_list + " / " + invoice_data.length + " )");
                        sendinvoices_peak(index_list);
                    } else {
                        console.log(response);
                        $("body").mLoading("hide");
                        $("#modalpopup-data").modal("show").find("#message").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133381") %>");
                        getTuitionfeesetting($("#year_id").val(), $("#trem_id").val());
                        index_list = 0;
                    }
                },
                function (e) {
                    console.log(e.StackTrace);
                }
            );
        }

        function SaveDataToDataBase() {
            //e.preventDefault();
            var invoices_List = getInvoicesList(true);
            if ($('form').valid() === false) return;
            if (invoices_List.length === 0) {
                $("#modalpopup-data").modal("show").find("#message").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133385") %>");
                return;
            }
            //$('.spinner').show();
            $("body").mLoading({
                "text": '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133386") %>'
            });

            PageMethods.saveData({
                year_id: parseInt($("#year_id").val()),
                trem_id: $("#trem_id").val(),
                invoices_List: invoices_List
            },
                function (response) {
                    //$("#submitpeak").show();
                    setTimeout($("body").mLoading("hide"), 1000);
                    $("#modalpopup-data").modal("show").find("#message").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133387") %>");
                    savedata = true;
                },
                function (e) {
                    setTimeout($("body").mLoading("hide"), 1000);
                    $("#modalpopup-data").modal("show").find("#message").html(
                        "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133388") %>");
                    console.log(e.StackTrace);
                }
            );
        }

        function addRules(control, messages) {
            $("#" + control).rules("add", {
                required: true,
                messages: {
                    required: messages
                }
            });
        }
        //Setting Select In Table Rows 
        function setting_select() {
            var row_last = $("#tableAddRow tbody tr:last");
            var group_select = row_last.find("[id*=group]");
            var sublevel_select = row_last.find("[id*=sublevel]");
            var class_select = row_last.find("[id*=class]");
            var price_label = row_last.find("[id*=label]");

            select_settingdata(group_select, group_data, "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133389") %>", "group_id", "group_name", "");
            select_settingdata(sublevel_select, sublevel_data, "เลือกขั้นปี", "sublevel_id", "sublevel_name", "");

            sublevel_select.on("change", function () {
                getclass(sublevel_select, class_select, "0");
            });

            if ($("#tableAddRow tbody")[0].scrollHeight > 420) $("#tableAddRow tbody").scrollTop($(
                "#tableAddRow tbody")[0].scrollHeight);

            group_select.on("change", function () {
                var data = getObjects(group_data, "group_id", $(this).val());
                if ($(this).val() !== "" && data.length > 0) {
                    price_label.number(data[0]["price"], 2);
                } else {
                    price_label.number(0, 2);
                }
            });

            getclass(sublevel_select, class_select);
            row_last.find(".addBtnRemove").on("click", function () {
                $(this).closest('tr').remove();
            });
        }

        //Setting Class Select Control
        function getclass(sublevel_select, class_select, class_vlaues) {
            if ($(sublevel_select).val() === "") {
                $("#" + $(class_select).attr("id") + " option").remove();

                $(class_select).append($("<option></option>")
                    .attr("value", "0")
                    .text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01941") %>"));
                $(class_select).siblings().hide();
                $(class_select).show();
            } else {
                $(class_select).siblings('').show();
                $(class_select).hide();

                select_settingdata(class_select, getObjects(class_data, "level_id", $(sublevel_select).val()),
                    "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01941") %>", "class_id", "class_name", "0");
                $(class_select).val(class_vlaues);
                data_completed += 1;
            }
        }

        //Setting Select 
        function select_settingdata(control, data, text_default, display_value, display_text, value_default) {
            $("#" + $(control).attr("id") + " option").remove();
            if (text_default !== "") {
                $(control).append($("<option></option>")
                    .attr("value", value_default)
                    .text(text_default));
            }
            $.each(data, function (s, e) {
                $(control).append($("<option></option>")
                    .attr("value", data[s][display_value])
                    .text(data[s][display_text]));
            });

            $(control).siblings().css("display", "none");
            $(control).show();
        }

        function loadingdata() {
            if (data_length > data_completed) {
                setTimeout(loadingdata, 500);
            } else {
                console.log("completed");
            }
        }

        //Add Table Rows
        function addTableRow() {
            var tempTr = $(
                '<tr><td style="width: 5%" class="center"><input type="checkbox" value="1" class="form-check-input" checked /></td>' +
                '<td style="width: 15%"><select name="sublevel_' + i + '" id="sublevel_' + i +
                '" class="form-control" ><option value="">Loading . . .</option></select></td>' +
                '<td style="width: 15%" class="center"><i class="fa fa-refresh fa-spin"></i><select name="class_' +
                i + '" id="class_' + i +
                '" class="form-control" style="display:none;" ><option>Loading . . .</option></select></td>' +
                '<td style="width: 15%"><select name="group_' + i + '" id="group_' + i +
                '" class="form-control" ><option>Loading . . .</option></select></td>' +
                '<td class="right" style="width: 10%"><label id="label_' + i + '">0.00</label> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></td>' +
                '<td class="right" style="width: 15%"><input type="text" autocomplete="off" name="issuedDate_' +
                i +
                '" id="issuedDate_' + i + '" class="form-control datepicker" required /></td>' +
                '<td class="right" style="width: 15%"><input type="text" autocomplete="off" name="dueDate_' + i +
                '" id="dueDate_' +
                i + '" class="form-control datepicker" required /></td>' +
                '<td class="center" style="width: 10%; vertical-align: middle;"><span class="fa fa-times addBtnRemove" id="addBtn_' +
                i + '"></span>' +
                '<input name="item_id_' + i + '" id="item_id_' + i + '" value="0" type="hidden"/></td></tr>')
            $("#tableAddRow").append(tempTr);
            setting_select();
            i++;
            $(".datepicker").datepicker({
                minDate: new Date()
            });
            $(tempTr).find("input[type=checkbox]").change(function () {
                $("#tableAddRow thead input[type=checkbox]").prop("checked", $("#tableAddRow tbody input[type=checkbox]:checked").length === $("#tableAddRow tbody tr").length);
            });
            return tempTr;
        }

        //Get Data Tuitionfeesetting
        function getTuitionfeesetting(year_id, trem_id) {
            PageMethods.getdata(year_id, trem_id, function (response) {
                response = $.parseJSON(response).data;
                $("#tableAddRow tbody tr").remove();
                i = 0;
                if (response.length > 0) {
                    data_length = response.length;
                    data_completed = 0;
                    loadingdata();
                    $.each(response, function (index, data) {
                        savedata = true;
                        var tr = addTableRow();
                        tr.find("[id*=sublevel]").val(data.level_id);
                        tr.find("[id*=group]").val(data.paygroup_id);
                        tr.find("[id*=dueDate]").val(data.dueDate);
                        tr.find("[id*=issuedDate]").val(data.issuedDate);
                        tr.find("[id*=label]").number(data.price, 2);
                        tr.find("[id*=sublevel]").prop('disabled', 'disabled');
                        tr.find("[id*=class]").prop('disabled', 'disabled');
                        tr.find("[id*=item_id]").val(data.id);
                        getclass(tr.find("[id*=sublevel]"), tr.find("[id*=class]"), data.class_id);
                    });
                } else {
                    savedata = false;
                    //$("#submitpeak").hide();
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        <Services>
            <asp:ServiceReference Path="~/App_Logic/WSDataService.asmx" />
        </Services>
    </asp:ScriptManager>
    <div class="full-card box-content employeeslist-container group-list">
        <div class="row">
            <div class="col-xs-12 col-md-12">
                <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00110") %></h3>
                <div class="row">
                    <div class="col-xs-12 col-md-3">
                        <select id="year_id" name="year_id" class="chosen-container form-control bottom">
                            <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101312") %></option>
                        </select>
                    </div>
                    <div class="col-xs-12 col-md-3">
                        <select id="trem_id" name="trem_id" class="chosen-container form-control">
                            <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202002") %></option>
                        </select>
                    </div>
                    <div class="col-xs-12 col-md-6"></div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-md-12">
                <div class="row">
                    <div class="col-xs-12 col-md-6 disabled"></div>
                </div>
            </div>
        </div>
        <div class="row--space"></div>
        <div class="row">
            <div class="col-xs-12 col-md-12">
                <h3><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00111") %></h3>
            </div>
        </div>
        <div class="row--space"></div>
        <div class="row">
            <div class="col-lg-12">
                <table id="tableAddRow" class="table table-bordered table-hover tableSection" style="margin-bottom: 0px; border-top: 0px; border-right: 0px; font-size: 18px;">
                    <thead>
                        <tr style="border-top: 1px solid #ddd; background-color: #337AB7; color: white;">
                            <th style="width: 5%" class="center">
                                <input type="checkbox" value="1" class="form-check-input" checked onchange="$('#tableAddRow tbody input[type=checkbox]').prop('checked',$(this).prop('checked'))" />
                            </th>
                            <th style="width: 15%" class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101212") %></th>
                            <th style="width: 15%" class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210458") %></th>
                            <th style="width: 15%" class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107039") %></th>
                            <th style="width: 10%" class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M502004") %></th>
                            <th style="width: 15%" class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01985") %></th>
                            <th style="width: 15%" class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01964") %></th>
                            <th style="width: 10%; vertical-align: middle;" class="center">
                                <span class="btn btn-success glyphicon glyphicon-plus addBtn" id="addBtn_0" style="font-size: 15px;"></span>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="row--space"></div>
        <div class="row">
            <div class="col-xs-12 col-md-6">
                <button type="submit" class="btn btn-success" id="submit"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %></button>
                <div class="btn btn-danger"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101127") %></div>
            </div>
            <div class="col-xs-12 col-md-6 right">
                <button type="submit" class="btn btn-primary" id="submitpeak" title="กรุณากดปุ่มบันทึกก่อนทำการส่งให้ระบบัญชี Peak" data-toggle="tooltip">ส่งให้ Peak</button>
            </div>
        </div>
    </div>
    <%--    <div class="spinner">
        <h1 class="message"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133386") %></h1>
        <div class="rect1"></div>
        <div class="rect2"></div>
        <div class="rect3"></div>
        <div class="rect4"></div>
        <div class="rect5"></div>
    </div>--%>
    <%-- <div id="dialog-confirm" class="modal fade alertBoxInfo" title="Empty the recycle bin?" style="display: none;">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 12px 12px 20px 0;"></span>These items will be permanently deleted and cannot be recovered. Are you sure?</p>
    </div>--%>
    <asp:Label ID="lblMsg" runat="server" />
    <div class="modal fade" id="dialog-confirm">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">Modal title</h4>
                </div>
                <div class="modal-body">
                    <p>One fine body…</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="modalpopup" runat="server">
    <div class="row">
        <div class="col-md-12 col-xs-12 center">
            <label id="message"></label>
        </div>
    </div>
</asp:Content>
