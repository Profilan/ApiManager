"use strict";

var ApiTaskSchedulerDatatable = function () {

    // variables
    var datatable;
    var scheduler;

    var panel = KTUtil.get('kt_quick_panel');
    var notificationPanel = $('.kt_quick_panel_tab_notifications');

    // init
    var init = function () {
        datatable = $('#api_tasks_list').KTDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: {
                    read: {
                        url: '/api/monitor/api-task-scheduler'
                    }
                }
            },

            // layout definition
            layout: {
                scroll: false, //disable datatable scroll, both horizontal and vertical
                footer: false // hide footer
            },

            sortable: false,

            pagination: false,

            // rows definition
            rows: {
                afterTemplate: function (row, data, index) {
                    // console.log(row);
                    // $('tr[data-field=' + index + ']').data('record-id', data.id);
                    row.attr('id', data.id);
                }
            },

            // columns definition
            columns: [
                {
                    field: 'title',
                    title: 'Task'
                },
                {
                    field: 'enabled',
                    title: 'Enabled',
                    template: function (row) {
                        var status = {
                            true: { 'title': 'Enabled', 'class': 'kt-badge--success', 'enabled': 1 },
                            false: { 'title': 'Disabled', 'class': 'kt-badge--danger', 'enabled': 0 }
                        };

                        return '<span class="enable-task kt-badge ' + status[row.enabled].class + ' kt-badge--inline kt-badge--pill" data-enabled="' + status[row.enabled].enabled + '" data-record-id="' + row.id + '">' + status[row.enabled].title + '</span>';

                    }
                },
                {
                    field: 'queued',
                    title: '#Queued',
                    template: function (row) {
                        var color = "secondary";
                        if (row.queued > 0) {
                            color = "primary";
                        }
                        return '<a href="/monitor/apiqueue?taskId=' + row.id + '"><span class="queued text-' + color + '">' + row.queued + '</span></a>';
                    }
                },
                {
                    field: 'last_run_time',
                    title: 'Last Run Time',
                    template: function (row) {
                        return '<span class="last-run-time">' + row.last_run_time + '</span>';
                    }
                },
                {
                    field: 'last_run_result',
                    title: 'Last run Result',
                    template: function (row) {
                        var color = 'success';
                        var statusCode = parseInt(row.last_run_result.split(" ")[0]);
                        if (statusCode >= 400) {
                            color = 'danger';
                        }
                        return '<span class="last-run-result kt-font-' + color + '">' + row.last_run_result + '</span>';
                    }
                },
                {
                    field: 'Actions',
                    title: 'Actions',
                    width: 130,
                    overflow: 'visible',
                    textAlign: 'center',
                    template: function (row, index, datatable) {
                        var dropup = (datatable.getPageSize() - index) <= 4 ? 'dropup' : '';
                        return '<div class="dropdown ' + dropup + '">\
                                    <a href="#" class="btn btn-hover-brand btn-icon btn-pill" data-toggle="dropdown">\
                                        <i class="la la-ellipsis-h"></i>\
                                    </a>\
                                    <div class="dropdown-menu dropdown-menu-right">\
                                        <a id="runNowLink-' + row.id + '" data-record-id="' + row.id + '" class="dropdown-item btn-runnow" href="#"><i class="la la-cogs"></i> Run Now</a>\
                                        <a class="dropdown-item btn-reset" href="#" data-record-id="' + row.id + '"><i class="la la-power-off"></i> Reset</a>\
                                    </div>\
                                </div>\
                                <a href="#" class="btn btn-hover-brand btn-icon btn-pill btn-details" data-record-id="' + row.id + '" data-toggle="modal" data-target="#detailsModal" title="Details">\
                                    <i class="la la-book"></i>\
                                </a>';
                    }
                }
            ]
        });

        datatable.on('kt-datatable--on-layout-updated', function (e, args) {
            $(".enable-task").click(function () {

                if ($(this).data("enabled")) {

                    scheduler.server.disableTask($(this).data("record-id"));

                } else {

                    scheduler.server.enableTask($(this).data("record-id"));

                }
            });

            $(".btn-runnow").click(function (e) {
                e.preventDefault();

                var id = $(this).data("record-id");

                if (!$("#runNowLink-" + id).hasClass('disabled')) {
                    console.log('Run Now');
                    $("#runNowLink-" + id).addClass('disabled');
                    scheduler.server.runTask($(this).data("record-id"));
                }
            });

            $('.btn-reset').click(function (e) {
                e.preventDefault();

                console.log('reset');

                var $this = $(this);
                $(this).addClass('disabled');
                $.post('/api/task/reset/' + $(this).data("record-id"), function (result) {
                    $this.removeClass("disabled");
                });
            });

            refreshTasks();
            setInterval(refreshTasks, 60 * 1000);
        });

        var modal = $('#detailsModal');

        modal.on('show.bs.modal', function (e) {
            var link = $(e.relatedTarget);
            var id = link.data('record-id');

            $.getJSON('/api/task/' + id)
                .done(function (data) {
                    modal.find('.modal-title').text(data.title);
                    modal.find('.modal-body > p').text(data.last_run_details);
                });
        });

    };

    var refreshTasks = function () {
        $.getJSON("/api/task")
            .done(function (data) {
                $.each(data, function (idx, task) {
                    var color = 'secondary';
                    if (task.queued > 0) {
                        color = 'primary';
                    }
                    $("#" + task.id).find(".queued").removeClass('text-primary text-secondary').addClass('text-' + color).text(task.queued);
                });
            });
    };

    var refreshTask = function (id) {
        $.getJSON("/api/task/" + id)
            .done(function (data) {
                var color = 'secondary';
                if (data.queued > 0) {
                    color = 'primary';
                }
                $("#" + id).find(".queued").removeClass('text-primary text-secondary').addClass('text-' + color).text(data.queued);
            });
    };

    var updatePanel = function (timestamp, message, type, source) {

        var timelineItem = $('.kt-timeline__item.prototype').clone().removeClass('prototype kt-hidden');

        timelineItem.find('.kt-timeline__item-datetime').text(timestamp);
        timelineItem.find('.kt-timeline__item-text').text(message);
        timelineItem.find('.kt-timeline__item-info').text(source);
        timelineItem.addClass(' kt-timeline__item--' + type);

        $('.kt-timeline').prepend(timelineItem);


        // KTUtil.scrollUpdate(notificationPanel);
    };

    var showNotification = function (title, message, type) {

        /*
        $.notify({
            title: title,
            message: message
        },
            {
                type: type,
                template: `
                    <div class="alert alert-custom alert-notice alert-{0} fade show" role="alert">
                        <div class="alert-text">
                            <span data-notify="title">{1}</span><br>
                            <span data-notify="message">{2}</span>
                        </div>
                        <div class="alert-close">
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true"><i class="ki ki-close"></i></span>
                            </button>
                        </div>
                    </div>
                `
            });
        */
        var color = "green";
        if (type == "danger") {
            color = "red";
        }

        var template = `
              <div class="toast fade" role="alert" aria-live="assertive" aria-atomic="true" data-delay="5000">
                <div class="toast-header">
                  <svg class="bd-placeholder-img rounded mr-2" width="20" height="20" xmlns="http://www.w3.org/2000/svg" role="img" aria-label=" :  " preserveAspectRatio="xMidYMid slice" focusable="false"><title> </title><rect width="100%" height="100%" fill="${color}"></rect><text x="50%" y="50%" fill="#dee2e6" dy=".3em"> </text></svg>
                  <strong class="mr-auto">${title}</strong>
                   <button type="button" class="ml-2 mb-1 close" data-dismiss="toast" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="toast-body">
                  ${message}
                </div>
              </div>
        `;

        var message = $(template);
        message.toast();
        message.on('hidden.bs.toast', function () {
            $(this).remove();
        });

        $('#notifications').prepend(message);

        message.toast('show');
    }

    var initSignalr = function () {


        // Define url where the api task management service is running
        $.connection.hub.url = "http://ap-eek-zwd-01:8082/signalr";

        // Connect to signalR hub
        scheduler = $.connection.taskSchedulerHub;

        // Receive task statusses
        scheduler.client.updateTask = function (id, title, active, lastRunTime, lastRunResult, enabled) {

            var statusCode = parseInt(lastRunResult.split(" ")[0]);
            var notifyType = "success";

            if (statusCode >= 400) {
                notifyType = "danger";
            }

            if (!active) {
                $('#runNowLink-' + id).removeClass('disabled');

                updatePanel(lastRunTime, "Task " + title + " has completed", notifyType, 'API Task Service');

                // $('tr[id="' + id + '"]').effect("highlight", { color: 'LightBlue' }, 1500);
                showNotification('<b>' + lastRunResult + ':</b> ', 'Task <b>' + title + '</b> has completed', notifyType);
            } else {
                $('#runNowLink-' + id).addClass('disabled');

                updatePanel(lastRunTime, "Task " + title + " has started", notifyType, 'API Task Service');
                // $('tr[id="' + id + '"]').effect("highlight", { color: 'LightGreen' }, 1500);
                showNotification('<b>' + lastRunResult + ':</b> ', 'Task <b>' + title + '</b> has started', notifyType);
            }

            $('tr[id="' + id + '"]').find('.last-run-time').text(lastRunTime);
            $('tr[id="' + id + '"]').find('.last-run-result').text(lastRunResult).removeClass('kt-font-danger kt-font-success').addClass('kt-font-' + notifyType);

            refreshTask(id);

        };

        scheduler.client.updateTaskStatus = function (id, title, enabled) {

            console.log('updateTaskStatus: ' + id);
            
            if (enabled) {
                $('span[data-record-id="' + id + '"]').data("enabled", 1).toggleClass('kt-badge--success kt-badge--danger');

            } else {
                $('span[data-record-id="' + id + '"]').data("enabled", 0).toggleClass('kt-badge--success kt-badge--danger');
            }

        };
        $.connection.hub.start().done(function () {

        });
    };

    return {
        init: function () {
            init();
            initSignalr();
        }
    };
}();

KTUtil.ready(function () {
    ApiTaskSchedulerDatatable.init();
});
