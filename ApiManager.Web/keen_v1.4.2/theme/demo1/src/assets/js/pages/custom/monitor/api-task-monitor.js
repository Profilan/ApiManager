

"use strict";

var ApiTaskMonitor = function () {

    // variables
    var datatable;
    var scheduler;
    var currentPage = 1;
    var length = 10;
    var pageTotal = 0;
    var detailsModal;
    var paused = true;
    var pageScroller;
    var tasks = [];

    var status = {
        true: { 'title': 'Enabled', 'class': 'kt-badge--success', 'enabled': 1 },
        false: { 'title': 'Disabled', 'class': 'kt-badge--danger', 'enabled': 0 }
    };

    var panel = KTUtil.get('kt_quick_panel');
    var notificationPanel = $('.kt_quick_panel_tab_notifications');

    var initDatatable = function () {

        var hostName = $('#HostName option:selected').text().toLowerCase();
        initSignalr(hostName);

        datatable = $('#apiTaskMonitorTable');

        $('.btn-next').click(function (e) {

            ++currentPage;
            show(currentPage);
        });

        $('.btn-previous').click(function (e) {

            --currentPage;
            show(currentPage);
        });

        $.get('/api/task', { schedulerId: $('#HostName').val() }, function (response) {

            tasks = response;
            pageTotal = Math.ceil(tasks.length / length);
            $('.page-total').text(pageTotal);

            show(currentPage);
        });
    }

    var show = function (page) {
        datatable.find('tbody').empty();

        $('.page-number').text(currentPage);

        if (page >= 1 && page < pageTotal) {
            $('.btn-next').removeClass('invisible');
        } else {
            $('.btn-next').addClass('invisible');
        }

        if (page <= pageTotal && page > 1) {
            $('.btn-previous').removeClass('invisible');
        } else {
            $('.btn-previous').addClass('invisible');
        }
        for (var i = 0; i < length; i++) {
            addTask(tasks[i + ((page - 1) * length)]);
        }
   }

    // init
    var init = function () {


        $('.btn-play').click(function (e) {
            if (paused) {
                $('.btn-play > i').removeClass('la-play').addClass('la-pause');
                pageScroller = setInterval(function () {
                    if (currentPage == pageTotal) {
                        currentPage = 1;
                    } else {
                        ++currentPage;
                    }

                    show(currentPage);
                }, 20 * 1000);
                paused = false;
            } else {
                $('.btn-play > i').removeClass('la-pause').addClass('la-play');
                clearInterval(pageScroller);
                paused = true;
            }

        });

        $('#HostName').change(function (e) {
            initDatatable();

        });

        detailsModal = $('#detailsModal');
    };

    var addTask = function (task) {

        var row = $('<tr/>').data('record-id', task.id).attr('id', task.id).addClass('task');
        $('<td/>').data('field', 'title').text(task.title).appendTo(row);
        var btnEnable = $('<span class="enable-task kt-badge ' + status[task.enabled].class + ' kt-badge--inline kt-badge--pill" data-enabled="' + status[task.enabled].enabled + '" data-record-id="' + task.id + '">' + status[task.enabled].title + '</span>')
            .click(function () {

                console.log($(this).data("enabled"))

                if ($(this).data('enabled')) {

                    scheduler.server.disableTask($(this).data('record-id'));

                } else {

                    scheduler.server.enableTask($(this).data('record-id'));

                }
            });
        $('<td/>').data('field', 'enabled')
            .append(btnEnable).appendTo(row);
        var linkQueue = $('<a/>').addClass('queued').attr('href', '/monitor/apiqueue?taskId=' + task.id).text(task.queued);
        if (task.queued > 0) {
            linkQueue.addClass('text-primary');
        } else {
            linkQueue.addClass('text-secondary');
        }
        $('<td/>').data('field', 'queued').append(linkQueue).appendTo(row);
        var lastRunTime = $('<span/>').addClass('last-run-time').text(task.last_run_time);
        $('<td/>').data('field', 'last_run_time').append(lastRunTime).appendTo(row);
        var color = 'success';
        var statusCode = parseInt(task.last_run_result.split(' ')[0]);
        if (statusCode >= 400) {
            color = 'danger';
        }
        var lastRunResult = $('<span/>').addClass('last-run-result kt-font-' + color).text(task.last_run_result);
        $('<td/>').data('field', 'last_run_result').append(lastRunResult).appendTo(row);
        var colActions = $('<td/>').data('field', 'actions').appendTo(row);
        var btnRun = $('<a/>').addClass('btn btn-sm btn-outline-secondary btn-runnow')
            .data('record-id', task.id)
            .attr('title', 'Run Now')
            .text('Run Now').appendTo(colActions)
            .click(function (e) {
                e.preventDefault();

                if (!$(this).hasClass('disabled')) {
                    console.log('Run Now');
                    $(this).addClass('disabled');
                    $(this).toggleClass('btn-outline-secondary btn-secondary');
                    scheduler.server.runTask(task.id);
                }
            });
        
        if (task.active) {
            btnRun.addClass('disabled');
        } else {
            btnRun.removeClass('disabled');
        }
        $('<a/>').addClass('btn btn-sm btn-outline-secondary btn-reset')
            .data('record-id', task.id)
            .attr('title', 'Reset')
            .text('Reset').appendTo(colActions)
            .click(function (e) {
                e.preventDefault();

                var $this = $(this);
                $(this).addClass('disabled');
                $.post('/api/task/reset/' + task.id, function (result) {
                    $this.removeClass("disabled");
                    btnRun.removeClass('disabled');
                    btnRun.toggleClass('btn-outline-secondary btn-secondary');
                });
            });
        $('<a/>').addClass('btn btn-sm btn-link btn-details')
            .data('record-id', task.id).data('toggle', 'modal').data('target', '#detailsModal').attr('title', 'Details')
            .attr('title', 'Details')
            .text('Details').appendTo(colActions)
            .click(function (e) {
                e.preventDefault();

                $.getJSON('/api/task/' + task.id)
                    .done(function (data) {
                        detailsModal.find('.modal-title').text(data.title);
                        detailsModal.find('.modal-body > p').text(data.last_run_details);
                    });
                detailsModal.modal('show');
            });

        datatable.find('tbody').append(row);
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

                $('tr[id="' + id + '"]').find(".queued").removeClass('text-primary text-secondary').addClass('text-' + color).text(data.queued);
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

    var initSignalr = function (hostName) {


        // Define url where the api task management service is running
        // $.connection.hub.url = hubConnectionUrl;
        var connectionUrl = 'http://' + hostName + ':8082/';
        console.log(connectionUrl);
        $.connection.hub.url = connectionUrl;

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
                $('tr[id="' + id + '"] .btn-runnow').removeClass('disabled');
                $('tr[id="' + id + '"] .btn-runnow').toggleClass('btn-outline-secondary btn-secondary');

                updatePanel(lastRunTime, "Task " + title + " has completed", notifyType, 'API Task Service');

                // $('tr[id="' + id + '"]').effect("highlight", { color: 'LightBlue' }, 1500);
                showNotification('<b>' + lastRunResult + ':</b> ', 'Task <b>' + title + '</b> has completed', notifyType);
            } else {
                $('tr[id="' + id + '"] .btn-runnow').addClass('disabled');
                $('tr[id="' + id + '"] .btn-runnow').toggleClass('btn-outline-secondary btn-secondary');

                updatePanel(lastRunTime, "Task " + title + " has started", notifyType, 'API Task Service');
                // $('tr[id="' + id + '"]').effect("highlight", { color: 'LightGreen' }, 1500);
                showNotification('<b>' + lastRunResult + ':</b> ', 'Task <b>' + title + '</b> has started', notifyType);
            }

            refreshTask(id);

            $('tr[id="' + id + '"]').find('.last-run-time').text(lastRunTime);
            $('tr[id="' + id + '"]').find('.last-run-result').text(lastRunResult).removeClass('kt-font-danger kt-font-success').addClass('kt-font-' + notifyType);

            var task = tasks.find(x => x.id === id);
            task.last_run_time = lastRunTime;
            task.last_run_result = lastRunResult;
        };

        scheduler.client.updateTaskStatus = function (id, enabled) {

            console.log('updateTaskStatus: ' + id);
            console.log('enabled: ' + enabled);
            
            if (enabled) {
                $('span[data-record-id="' + id + '"]').data("enabled", 1).toggleClass('kt-badge--success kt-badge--danger').text('Enabled');

            } else {
                $('span[data-record-id="' + id + '"]').data("enabled", 0).toggleClass('kt-badge--success kt-badge--danger').text('Disabled');
            }

            var task = tasks.find(x => x.id === id);
            task.enabled = enabled;

            console.log(task);

        };
        $.connection.hub.start().done(function () {

        });
    };

    return {
        init: function () {
            
            init();
            initDatatable();

        }
    };
}();

KTUtil.ready(function () {
    ApiTaskMonitor.init();
});
