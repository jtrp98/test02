<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="StudentGraduateDetail.aspx.cs" Inherits="FingerprintPayment.StudentInfo.StudentGraduateDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <link rel="stylesheet" href="/styles/style-std.css?v=<%=DateTime.Now.Ticks%>" />

    <link rel="stylesheet" href="/Styles/style-upload.css?v=<%=DateTime.Now.Ticks%>" />

    <!-- bootstrap-toggle -->
    <link rel="stylesheet" href="/assets/plugins/bootstrap-toggle/2.2.2/css/bootstrap-toggle.min.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101001") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %> &rsaquo; <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101338") %>
            </p>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="card stdProfile">
                <div class="card-header card-header-warning card-header-icon">
                    <div class="card-icon">
                        <i class="material-icons">account_circle</i>
                    </div>
                    <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101338") %></h4>
                </div>
                <div class="card-body">
                    <div class="toolbar">
                        <!-- Here you can write extra buttons/actions for the toolbar -->
                    </div>
                    <div style="text-align: center;">
                        <div id="divProfileImage" class="picture-frame">
                            <img class="img-photo" src="/Assets/images/avatar.png"
                                style="width: 100%; height: 100%;" />
                            <div class="div-browse"><i class="glyphicon fa fa-camera"></i></div>
                            <div class="div-picture-remove"><i class="glyphicon fa fa-times"></i></div>
                            <div class="progress" style="height: 5px; margin-right: 0px; margin-top: 0px; position: absolute; width: 100%; display: none;">
                                <div class="progress-bar" role="progressbar" style="width: 25%;" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                            </div>
                            <div style='height: 0px; width: 0px; overflow: hidden;'>
                                <input type="file" value="upload" />
                            </div>
                        </div>
                    </div>
                    <div class="panel with-nav-tabs panel-default">
                        <div class="panel-heading">
                            <ul class="nav nav-pills nav-justified" role="tablist" id="studentTabs">
                                <li class="nav-item active"><a class="nav-link active show" href="#tab1" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101003") %></a></li>
                                <li class="nav-item"><a class="nav-link" href="#tab2" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101170") %></a></li>
                                <li class="nav-item"><a class="nav-link" href="#tab3" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101179") %></a></li>
                                <li class="nav-item"><a class="nav-link" href="#tab4" data-toggle="tab"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101209") %></a></li>
                            </ul>
                        </div>
                        <div class="panel-body" style="padding: 0px;">
                            <div class="tab-content">
                                <div class="tab-pane fade in active show" id="tab1">
                                    <div class="content1"></div>
                                    <div class="content2"></div>
                                </div>
                                <div class="tab-pane fade" id="tab2">
                                    <div class="content1"></div>
                                    <div class="content2"></div>
                                </div>
                                <div class="tab-pane fade" id="tab3">
                                    <div class="content1"></div>
                                    <div class="content2"></div>
                                </div>
                                <div class="tab-pane fade" id="tab4">
                                    <div class="content1"></div>
                                    <div class="content2"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end content-->
            </div>
            <!--  end card  -->
        </div>
        <!-- end col-md-12 -->
    </div>
    <!-- end row -->

    <% Response.WriteFile("~/Employees/modal-emp.inc"); %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script type='text/javascript' src="/Scripts/init-function.js"></script>
    <script type='text/javascript' src="/Scripts/prototype.js"></script>

    <!-- bootstrap-toggle -->
    <script type='text/javascript' src="/assets/plugins/bootstrap-toggle/2.2.2/js/bootstrap-toggle.min.js"></script>

    <script type='text/javascript' src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.2/Chart.min.js"></script>

    <script type='text/javascript' src="/scripts/upload-function.js"></script>

    <script src="../javascript/jquery-number/jquery.number.js" type="text/javascript"></script>

    <script type='text/javascript'>

        var xStdKey = { "sid": "<%=Request.QueryString["sid"]%>", "tid": "<%=Request.QueryString["tid"]%>", "fmlid": "0", "hltid": "0" };
        var StudentDetail_NextSID = '<%=NextSID%>';
        var flagTab = [0, 0, 0, 0];
        var urlTab = [['StdProfile.aspx', 'StdAddress.aspx'],
        ['StdEducation.aspx'],
        ['StdFamily.aspx'],
        ['StdHealth.aspx']];

        var modalForm = {
            showForm: function (formPage, title, callShow, v, sid, tid, id) {

                $.get(formPage, { "v": v, "sid": sid, "tid": tid, "id": id },
                    function (data) {
                        $('#modalShowForm').find('.modal-body').html(data);
                    }
                );
                $('#modalShowForm').find('.modal-title').text(title);

                if (callShow) {
                    $('#modalShowForm').modal('show');
                }

            },
            hideForm: function () {

                $('#modalShowForm').modal('hide');
                $('#modalShowForm').find('.modal-body').html('');

            }
        }

        function LoadDistrict(obj, provinceID) {
            if (provinceID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "StudentDetail.aspx/LoadDistrict",
                    data: '{provinceID: ' + provinceID + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var list = response.d;

                        $(obj).empty();

                        var options = '<option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101138") %></option>';
                        $(list).each(function () {

                            options += '<option value="' + this.id + '">' + this.name + '</option>';

                        });

                        $(obj).html(options);
                        $(obj).selectpicker('refresh');
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

        function LoadSubDistrict(obj, districtID) {
            if (districtID) {
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "StudentDetail.aspx/LoadSubDistrict",
                    data: '{districtID: ' + districtID + ' }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var plans = response.d;

                        $(obj).empty();

                        var options = '<option selected="selected" value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101140") %></option>';
                        $(plans).each(function () {

                            options += '<option value="' + this.id + '">' + this.name + '</option>';

                        });

                        $(obj).html(options);
                        $(obj).selectpicker('refresh');
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

        $(function () {

            $.ajaxSetup({
                statusCode: {
                    500: function () {
                        window.location.replace("/Default.aspx");
                    }
                }
            });

            if (jQuery().dataTable) {
                $.fn.dataTable.ext.errMode = 'none';
            }

            $.validator.addMethod("thaiDate",
                function (value, element) {
                    if (!value) return true; // when set required: false
                    return value.match(/^(0?[1-9]|[12][0-9]|3[0-1])[/., -](0?[1-9]|1[<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305069") %>])[/., -](24|25)?\d{2}$/);
                },
                "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111051") %>"
            );

            $.validator.addMethod("email2",
                function (value, element) {
                    if (!value) return true; // when set required: false
                    return value.match(/^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@(?:\S{1,63})$|-/);
                },
                "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111052") %>"
            );

            // initial first tab content
            flagTab[0] = 1;
            $('#tab1').prepend('<div class="loader"></div>');
            $.each(urlTab[0], function (index, url) {
                $.get(url, { "v": "view", "sid": <%=Request.QueryString["sid"]%>, "tid": "<%=Request.QueryString["tid"]%>" },
                    function (data) {
                        $('#tab1 .content' + (index + 1)).html(data);

                        $('#tab1 .loader').remove();
                    }
                );
            });

            // tab event click
            $('#studentTabs li:not(.dropdown) a').click(function (e) {

                // find index tabs is active
                var tabIndex = parseInt($(this).attr('href').replace('#tab', '')) - 1;
                if (flagTab[tabIndex] == 0) {

                    // already first click
                    flagTab[tabIndex] = 1;

                    $('#tab' + (tabIndex + 1)).prepend('<div class="loader"></div>');
                    $.each(urlTab[tabIndex], function (index, url) {
                        $.get(url, { "v": "view", "sid": <%=Request.QueryString["sid"]%>, "tid": "<%=Request.QueryString["tid"]%>" },
                            function (data) {
                                $('#tab' + (tabIndex + 1) + ' .content' + (index + 1)).html(data).resize();

                                $('#tab' + (tabIndex + 1) + ' .loader').remove();
                            }
                        );
                    });
                }
                else {
                    // force resize
                    $.each(urlTab[tabIndex], function (index, url) {
                        $('#tab' + (tabIndex + 1) + ' .content' + (index + 1)).resize();
                    });
                }
            });

            // Modal section
            $('#modalShowForm').on('show.bs.modal', function (e) {

                $formName = $(e.relatedTarget).attr('form-name');
                $formTitle = $(e.relatedTarget).attr('form-title');
                $pid = $(e.relatedTarget).attr('pid');
                if (typeof $pid === 'undefined') {
                    $pid = 0;
                }

                modalForm.showForm($formName, $formTitle, false, <%=Request.QueryString["v"]%>, <%=Request.QueryString["sid"]%>, "<%=Request.QueryString["tid"]%>", $pid);

            });

            $('#modalShowForm').on('hidden.bs.modal', function (e) {

                $(this).removeData();

            });

            // Upload function
            function readURL(input) {
                if (input.files && input.files[0]) {
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        $('.stdProfile img.img-photo').attr('src', e.target.result);
                    }

                    reader.readAsDataURL(input.files[0]);
                }
            }

            $('.stdProfile #divProfileImage input[type="file"]').change(function () {

                readURL(this);

                // Set flag remove old picture
                $(".stdProfile #divProfileImage").attr("data-remove-old-picture", false);

            });

        });

    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
