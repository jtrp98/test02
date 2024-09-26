
// Education Zone Module
var ez = {
    activePageComplete: function (pid) {
        $.ajax({
            url: '/PreRegister/Ashx/ActivePageComplete.ashx',
            data: '{pid: ' + pid + ' }',
            dataType: 'json',
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                if (result.redirect) {
                    window.location.replace(result.url);
                }
            },
            error: function (response) {
                console.log(response);
            }
        });
    },
    activeLeftMenu: function () {
        var path = window.location.pathname;
        var ca = path.split("/");
        var controller = ca[1].toLowerCase();
        var action = '';
        if (typeof (ca[2]) != "undefined") {
            action = ca[2].toLowerCase() || "index";
        }

        var menuIndex = '';
        var subMenuIndex = 0;

        switch (controller) {
            case "academicmanage": menuIndex = '01';
                switch (action) {
                    case "calendar": subMenuIndex = 0; break;
                    case "locallearning": subMenuIndex = 1; break;
                    case "supervisory": subMenuIndex = 2; break;
                    case "qualityreport": subMenuIndex = 3; break;
                }
                break;
            case "budgetmanage": menuIndex = '02';
                switch (action) {
                    case "budgetallocation": subMenuIndex = 0; break;
                }
                break;
            case "personnelmanage": menuIndex = '03';
                switch (action) {
                    case "personnelinformation": subMenuIndex = 0; break;
                    case "personnelplan": subMenuIndex = 1; break;
                }
                break;
            case "generalmanage": menuIndex = '04';
                switch (action) {
                    case "receivemail": subMenuIndex = 0; break;
                    case "publicrelations": subMenuIndex = 1; break;
                }
                break;
            case "report": menuIndex = '05';
                switch (action) {
                    case "study":
                    case "studydetail": subMenuIndex = 0; break;
                    case "work":
                    case "workdetail": subMenuIndex = 1; break;
                    case "usage": subMenuIndex = 2; break;
                }
                break;
            case "setting": menuIndex = '06';
                switch (action) {
                    case "agency": subMenuIndex = 0; break;
                    case "users": subMenuIndex = 1; break;
                    case "title": subMenuIndex = 2; break;
                    case "position": subMenuIndex = 3; break;
                }
                break;
        }

        $('.nav #menu' + menuIndex).prev().addClass('collapsed');
        $('.nav #menu' + menuIndex).addClass('collapse show');
        $('.nav #menu' + menuIndex).parent().addClass('active');
        $('.nav #menu' + menuIndex + ' > .nav').children('.nav-item').eq(subMenuIndex).addClass('active');
    },
    showNotification: function (from, align, type, icon, message, timer) {
        // type: success, info, warning, danger
        // icon: done, info, notifications, error
        $.notify({ icon: icon, message: message },
            {
                type: type,
                timer: timer,
                placement: {
                    from: from,
                    align: align
                }
            });
    },
    showSweetAlert: function (type, callback) {

        switch (type) {
            case 'confirm-remove':
                Swal.fire({
                    title: "Are you sure?",
                    text: "But you will still be able to retrieve this data.",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonClass: 'btn btn-success',
                    cancelButtonClass: 'btn btn-danger',
                    confirmButtonText: 'Yes, delete it!',
                    cancelButtonText: "No, cancel please!",
                    buttonsStyling: false
                }).then((result) => {
                    if (result.value) {
                        callback(); // confirm to remove item when user press yes
                    }
                    else {
                        //swal("Cancelled", "Your data is safe :)", "error");
                    }
                });
                break;
            case 'removed':
                Swal.fire({
                    title: 'Deleted!',
                    text: 'Your data has been deleted.',
                    type: 'success',
                    confirmButtonClass: "btn btn-success",
                    buttonsStyling: false
                });
                break;
            case 'done':
                Swal.fire({
                    title: 'Done!',
                    text: 'Complete your process.',
                    type: 'success',
                    showConfirmButton: false
                });
                break;
        }

    },
    initFormExtendedDatetimepickers: function (objID) {
        $(objID).datetimepicker({
            keepOpen: false,
            debug: false,
            format: 'DD/MM/YYYY-BE',
            locale: 'th',
            icons: {
                time: "fa fa-clock-o",
                date: "fa fa-calendar",
                up: "fa fa-chevron-up",
                down: "fa fa-chevron-down",
                previous: 'fa fa-chevron-left',
                next: 'fa fa-chevron-right',
                today: 'fa fa-screenshot',
                clear: 'fa fa-trash',
                close: 'fa fa-remove'
            }
        });
    },
    initFormExtendedDatetimepickersViewMode: function (objID, viewMode, format) {
        $(objID).datetimepicker({
            keepOpen: false,
            debug: false,
            viewMode: viewMode,
            format: format,
            locale: 'th',
            icons: {
                time: "fa fa-clock-o",
                date: "fa fa-calendar",
                up: "fa fa-chevron-up",
                down: "fa fa-chevron-down",
                previous: 'fa fa-chevron-left',
                next: 'fa fa-chevron-right',
                today: 'fa fa-screenshot',
                clear: 'fa fa-trash',
                close: 'fa fa-remove'
            }
        });
    },
    initFormExtendedDatetimepickersDisableFutureDate: function (objID) {
        $(objID).datetimepicker({
            keepOpen: false,
            debug: false,
            format: 'DD/MM/YYYY-BE',
            locale: 'th',
            maxDate: moment().add(-1, 'days').format(),
            icons: {
                time: "fa fa-clock-o",
                date: "fa fa-calendar",
                up: "fa fa-chevron-up",
                down: "fa fa-chevron-down",
                previous: 'fa fa-chevron-left',
                next: 'fa fa-chevron-right',
                today: 'fa fa-screenshot',
                clear: 'fa fa-trash',
                close: 'fa fa-remove'
            }
        });
    },
    activateBootstrapSelect: function () {
        if ($(".selectpicker").length != 0) {
            $(".selectpicker").selectpicker();
        }
    },
    refreshBootstrapSelect: function () {
        if ($(".selectpicker").length != 0) {
            $('.selectpicker').selectpicker('refresh');
        }
    },
    setValueBootstrapSelect: function (objID, value) {
        $(objID).selectpicker('val', value);
    },
    ajaxHandleFailure: function (callback) {
        ez.showNotification('top', 'right', 'danger', 'error', 'แจ้งข่าวสาร<br/>การตอบสนองล้มเหลว!', 3000);

        if (callback && typeof (callback) === "function") callback();

    },
    ajaxHandleError: function (callback) {
        ez.showNotification('top', 'right', 'danger', 'error', 'แจ้งข่าวสาร<br/>การตอบสนองผิดพลาด!', 3000);

        if (callback && typeof (callback) === "function") callback();

    },
    initFullCalendar: function (objID, onViewRender) {

        $(objID).fullCalendar({
            //height: 650,
            viewRender: function (view, element) {
                // We make sure that we activate the perfect scrollbar when the view isn't on Month
                if (view.name != 'month') {
                    $(element).find('.fc-scroller').perfectScrollbar();
                }
            },
            header: {
                left: 'title',
                center: '', // month,agendaWeek,agendaDay
                right: 'prev,next,today'
            },
            buttonText: {
                today: 'วันนี้',
                //month: 'รายเดือน',
                //week: 'รายสัปดาห์',
                //agendaDay: 'รายวัน'
            },
            locale: 'th',
            defaultDate: new Date(),
            selectable: true,
            selectHelper: true,
            views: {
                month: {
                    titleFormat: 'MMMM YYYY'
                },
                week: {
                    titleFormat: " MMMM D YYYY"
                },
                day: {
                    titleFormat: 'D MMM, YYYY'
                }
            },
            editable: false,
            eventLimit: true, // allow "more" link when too many events
            viewRender: onViewRender
        });
    },
    initPieChart: function (objID, data) {

        /* **************** Public Preferences - Pie Chart ******************** */
        var options = {
            donut: true,
            donutWidth: 70,
            donutSolid: true,
            startAngle: 90,
            showLabel: true,
            height: '350px'
        };

        Chartist.Pie(objID, data, options);

    },
    initBarChart: function (objID, data) {

        var options = {
            height: '400px',
            seriesBarDistance: 32,
            axisY: {
                offset: 100,
                labelInterpolationFnc: function (value) {
                    return value.toLocaleString();
                }
            },
            plugins: [
                Chartist.plugins.ctBarLabels({
                    position: {
                        y: function (data) {
                            return data.y2 - 5;
                        }
                    },
                    labelInterpolationFnc: function (text) {
                        return text.toLocaleString();
                    }
                })
            ]
        };

        var responsiveOptions = [
            ['screen and (min-width: 641px) and (max-width: 1024px)', {
                seriesBarDistance: 10,
                axisX: {
                    labelInterpolationFnc: function (value) {
                        return value;
                    }
                }
            }],
            ['screen and (max-width: 640px)', {
                seriesBarDistance: 5,
                axisX: {
                    labelInterpolationFnc: function (value) {
                        return value[0];
                    }
                }
            }]
        ];

        var viewsChart = Chartist.Bar(objID, data, options, responsiveOptions);

        //start animation for the Emails Subscription Chart
        ez.startAnimationForBarChart(viewsChart);

    },
    initBarChartMillionAxisY: function (objID, data) {

        var options = {
            height: '400px',
            seriesBarDistance: 32,
            high: Math.max(...data.series[0]) + 500000,
            axisY: {
                offset: 100,
                labelInterpolationFnc: function (value) {
                    return (value / 1000000).toLocaleString() + 'M';
                }
            },
            plugins: [
                Chartist.plugins.ctBarLabels({
                    position: {
                        y: function (data) {
                            return data.y2 - 5;
                        }
                    },
                    labelInterpolationFnc: function (text) {
                        return text.toLocaleString();
                    }
                })
            ]
        };

        var responsiveOptions = [
            ['screen and (min-width: 641px) and (max-width: 1024px)', {
                seriesBarDistance: 10,
                axisX: {
                    labelInterpolationFnc: function (value) {
                        return value;
                    }
                }
            }],
            ['screen and (max-width: 640px)', {
                seriesBarDistance: 5,
                axisX: {
                    labelInterpolationFnc: function (value) {
                        return value[0];
                    }
                }
            }]
        ];

        var viewsChart = Chartist.Bar(objID, data, options, responsiveOptions);

        //start animation for the Emails Subscription Chart
        ez.startAnimationForBarChart(viewsChart);

    },
    startAnimationForBarChart: function (chart) {

        var seq2 = 0;

        chart.on('draw', function (data) {
            if (data.type === 'bar') {
                seq2++;
                data.element.animate({
                    opacity: {
                        begin: seq2 * delays2,
                        dur: durations2,
                        from: 0,
                        to: 1,
                        easing: 'ease'
                    }
                });
            }
        });

        seq2 = 0;
    },
    checkFullPageBackgroundImage: function () {
        $page = $('.full-page');
        image_src = $page.data('image');

        if (image_src !== undefined) {
            image_container = '<div class="full-page-background" style="background-image: url(' + image_src + ') "/>'
            $page.append(image_container);
        }
    }

}

var modalForm = {
    showForm: function (modalElement, urlAction, title, callShow) {

        $ele = $(modalElement);

        // Insert loading
        $ele.find('.modal-body')
            .attr('text-align:', '-webkit-center')
            .html('<div class="loader"></div>');

        // Get form data
        $.get(urlAction, {},
            function (data) {
                $ele.find('.modal-body')
                    .css('text-align', '')
                    .html(data);
            }
        );
        $ele.find('.modal-title').text(title);

        // Force show modal
        if (callShow) {
            $ele.modal('show');
        }
    },
    hideForm: function () {

        $('#modalForm').modal('hide');
        $('#modalForm').find('.modal-body').html('');

    }
}

$(document).ready(function () {

    // Event handle modal
    $('#modalForm').on('show.bs.modal', function (e) {

        $urlAction = $(e.relatedTarget).attr('url-action');
        $modalTitle = $(e.relatedTarget).attr('modal-title');

        modalForm.showForm('#modalForm', $urlAction, $modalTitle, false);

    });

    ez.activeLeftMenu();

});