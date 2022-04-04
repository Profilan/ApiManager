/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, { enumerable: true, get: getter });
/******/ 		}
/******/ 	};
/******/
/******/ 	// define __esModule on exports
/******/ 	__webpack_require__.r = function(exports) {
/******/ 		if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 			Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 		}
/******/ 		Object.defineProperty(exports, '__esModule', { value: true });
/******/ 	};
/******/
/******/ 	// create a fake namespace object
/******/ 	// mode & 1: value is a module id, require it
/******/ 	// mode & 2: merge all properties of value into the ns
/******/ 	// mode & 4: return value when already ns object
/******/ 	// mode & 8|1: behave like require
/******/ 	__webpack_require__.t = function(value, mode) {
/******/ 		if(mode & 1) value = __webpack_require__(value);
/******/ 		if(mode & 8) return value;
/******/ 		if((mode & 4) && typeof value === 'object' && value && value.__esModule) return value;
/******/ 		var ns = Object.create(null);
/******/ 		__webpack_require__.r(ns);
/******/ 		Object.defineProperty(ns, 'default', { enumerable: true, value: value });
/******/ 		if(mode & 2 && typeof value != 'string') for(var key in value) __webpack_require__.d(ns, key, function(key) { return value[key]; }.bind(null, key));
/******/ 		return ns;
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = "../src/assets/js/pages/custom/monitor/api-task-monitor.js");
/******/ })
/************************************************************************/
/******/ ({

/***/ "../src/assets/js/pages/custom/monitor/api-task-monitor.js":
/*!*****************************************************************!*\
  !*** ../src/assets/js/pages/custom/monitor/api-task-monitor.js ***!
  \*****************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
eval("﻿\r\n\r\n\r\n\r\nvar ApiTaskMonitor = function () {\r\n\r\n    // variables\r\n    var datatable;\r\n    var scheduler;\r\n    var currentPage = 1;\r\n    var length = 10;\r\n    var pageTotal = 0;\r\n    var detailsModal;\r\n    var paused = true;\r\n    var pageScroller;\r\n    var tasks = [];\r\n\r\n    var status = {\r\n        true: { 'title': 'Enabled', 'class': 'kt-badge--success', 'enabled': 1 },\r\n        false: { 'title': 'Disabled', 'class': 'kt-badge--danger', 'enabled': 0 }\r\n    };\r\n\r\n    var panel = KTUtil.get('kt_quick_panel');\r\n    var notificationPanel = $('.kt_quick_panel_tab_notifications');\r\n\r\n    var initDatatable = function () {\r\n\r\n        var hostName = $('#HostName option:selected').text().toLowerCase();\r\n        initSignalr(hostName);\r\n\r\n        datatable = $('#apiTaskMonitorTable');\r\n\r\n        $('.btn-next').click(function (e) {\r\n\r\n            ++currentPage;\r\n            show(currentPage);\r\n        });\r\n\r\n        $('.btn-previous').click(function (e) {\r\n\r\n            --currentPage;\r\n            show(currentPage);\r\n        });\r\n\r\n        $.get('/api/task', { schedulerId: $('#HostName').val() }, function (response) {\r\n\r\n            tasks = response;\r\n            pageTotal = Math.ceil(tasks.length / length);\r\n            $('.page-total').text(pageTotal);\r\n\r\n            show(currentPage);\r\n        });\r\n    }\r\n\r\n    var show = function (page) {\r\n        datatable.find('tbody').empty();\r\n\r\n        $('.page-number').text(currentPage);\r\n\r\n        if (page >= 1 && page < pageTotal) {\r\n            $('.btn-next').removeClass('invisible');\r\n        } else {\r\n            $('.btn-next').addClass('invisible');\r\n        }\r\n\r\n        if (page <= pageTotal && page > 1) {\r\n            $('.btn-previous').removeClass('invisible');\r\n        } else {\r\n            $('.btn-previous').addClass('invisible');\r\n        }\r\n        for (var i = 0; i < length; i++) {\r\n            addTask(tasks[i + ((page - 1) * length)]);\r\n        }\r\n   }\r\n\r\n    // init\r\n    var init = function () {\r\n\r\n\r\n        $('.btn-play').click(function (e) {\r\n            if (paused) {\r\n                $('.btn-play > i').removeClass('la-play').addClass('la-pause');\r\n                pageScroller = setInterval(function () {\r\n                    if (currentPage == pageTotal) {\r\n                        currentPage = 1;\r\n                    } else {\r\n                        ++currentPage;\r\n                    }\r\n\r\n                    show(currentPage);\r\n                }, 20 * 1000);\r\n                paused = false;\r\n            } else {\r\n                $('.btn-play > i').removeClass('la-pause').addClass('la-play');\r\n                clearInterval(pageScroller);\r\n                paused = true;\r\n            }\r\n\r\n        });\r\n\r\n        $('#HostName').change(function (e) {\r\n            initDatatable();\r\n\r\n        });\r\n\r\n        detailsModal = $('#detailsModal');\r\n    };\r\n\r\n    var addTask = function (task) {\r\n\r\n        var row = $('<tr/>').data('record-id', task.id).attr('id', task.id).addClass('task');\r\n        $('<td/>').data('field', 'title').text(task.title).appendTo(row);\r\n        var btnEnable = $('<span class=\"enable-task kt-badge ' + status[task.enabled].class + ' kt-badge--inline kt-badge--pill\" data-enabled=\"' + status[task.enabled].enabled + '\" data-record-id=\"' + task.id + '\">' + status[task.enabled].title + '</span>')\r\n            .click(function () {\r\n\r\n                console.log($(this).data(\"enabled\"))\r\n\r\n                if ($(this).data('enabled')) {\r\n\r\n                    scheduler.server.disableTask($(this).data('record-id'));\r\n\r\n                } else {\r\n\r\n                    scheduler.server.enableTask($(this).data('record-id'));\r\n\r\n                }\r\n            });\r\n        $('<td/>').data('field', 'enabled')\r\n            .append(btnEnable).appendTo(row);\r\n        var linkQueue = $('<a/>').addClass('queued').attr('href', '/monitor/apiqueue?taskId=' + task.id).text(task.queued);\r\n        if (task.queued > 0) {\r\n            linkQueue.addClass('text-primary');\r\n        } else {\r\n            linkQueue.addClass('text-secondary');\r\n        }\r\n        $('<td/>').data('field', 'queued').append(linkQueue).appendTo(row);\r\n        var lastRunTime = $('<span/>').addClass('last-run-time').text(task.last_run_time);\r\n        $('<td/>').data('field', 'last_run_time').append(lastRunTime).appendTo(row);\r\n        var color = 'success';\r\n        var statusCode = parseInt(task.last_run_result.split(' ')[0]);\r\n        if (statusCode >= 400) {\r\n            color = 'danger';\r\n        }\r\n        var lastRunResult = $('<span/>').addClass('last-run-result kt-font-' + color).text(task.last_run_result);\r\n        $('<td/>').data('field', 'last_run_result').append(lastRunResult).appendTo(row);\r\n        var colActions = $('<td/>').data('field', 'actions').appendTo(row);\r\n        var btnRun = $('<a/>').addClass('btn btn-sm btn-outline-secondary btn-runnow')\r\n            .data('record-id', task.id)\r\n            .attr('title', 'Run Now')\r\n            .text('Run Now').appendTo(colActions)\r\n            .click(function (e) {\r\n                e.preventDefault();\r\n\r\n                if (!$(this).hasClass('disabled')) {\r\n                    console.log('Run Now');\r\n                    $(this).addClass('disabled');\r\n                    $(this).toggleClass('btn-outline-secondary btn-secondary');\r\n                    scheduler.server.runTask(task.id);\r\n                }\r\n            });\r\n        \r\n        if (task.active) {\r\n            btnRun.addClass('disabled');\r\n        } else {\r\n            btnRun.removeClass('disabled');\r\n        }\r\n        $('<a/>').addClass('btn btn-sm btn-outline-secondary btn-reset')\r\n            .data('record-id', task.id)\r\n            .attr('title', 'Reset')\r\n            .text('Reset').appendTo(colActions)\r\n            .click(function (e) {\r\n                e.preventDefault();\r\n\r\n                var $this = $(this);\r\n                $(this).addClass('disabled');\r\n                $.post('/api/task/reset/' + task.id, function (result) {\r\n                    $this.removeClass(\"disabled\");\r\n                    btnRun.removeClass('disabled');\r\n                    btnRun.toggleClass('btn-outline-secondary btn-secondary');\r\n                });\r\n            });\r\n        $('<a/>').addClass('btn btn-sm btn-link btn-details')\r\n            .data('record-id', task.id).data('toggle', 'modal').data('target', '#detailsModal').attr('title', 'Details')\r\n            .attr('title', 'Details')\r\n            .text('Details').appendTo(colActions)\r\n            .click(function (e) {\r\n                e.preventDefault();\r\n\r\n                $.getJSON('/api/task/' + task.id)\r\n                    .done(function (data) {\r\n                        detailsModal.find('.modal-title').text(data.title);\r\n                        detailsModal.find('.modal-body > p').text(data.last_run_details);\r\n                    });\r\n                detailsModal.modal('show');\r\n            });\r\n\r\n        datatable.find('tbody').append(row);\r\n    };\r\n\r\n    var refreshTasks = function () {\r\n        $.getJSON(\"/api/task\")\r\n            .done(function (data) {\r\n\r\n                $.each(data, function (idx, task) {\r\n                    var color = 'secondary';\r\n                    if (task.queued > 0) {\r\n                        color = 'primary';\r\n                    }\r\n                    $(\"#\" + task.id).find(\".queued\").removeClass('text-primary text-secondary').addClass('text-' + color).text(task.queued);\r\n                });\r\n            });\r\n    };\r\n\r\n    var refreshTask = function (id) {\r\n        $.getJSON(\"/api/task/\" + id)\r\n            .done(function (data) {\r\n                var color = 'secondary';\r\n                if (data.queued > 0) {\r\n                    color = 'primary';\r\n                }\r\n\r\n                $('tr[id=\"' + id + '\"]').find(\".queued\").removeClass('text-primary text-secondary').addClass('text-' + color).text(data.queued);\r\n            });\r\n    };\r\n\r\n    var updatePanel = function (timestamp, message, type, source) {\r\n\r\n        var timelineItem = $('.kt-timeline__item.prototype').clone().removeClass('prototype kt-hidden');\r\n\r\n        timelineItem.find('.kt-timeline__item-datetime').text(timestamp);\r\n        timelineItem.find('.kt-timeline__item-text').text(message);\r\n        timelineItem.find('.kt-timeline__item-info').text(source);\r\n        timelineItem.addClass(' kt-timeline__item--' + type);\r\n\r\n        $('.kt-timeline').prepend(timelineItem);\r\n\r\n\r\n        // KTUtil.scrollUpdate(notificationPanel);\r\n    };\r\n\r\n    var showNotification = function (title, message, type) {\r\n        var color = \"green\";\r\n        if (type == \"danger\") {\r\n            color = \"red\";\r\n        }\r\n\r\n        var template = `\r\n              <div class=\"toast fade\" role=\"alert\" aria-live=\"assertive\" aria-atomic=\"true\" data-delay=\"5000\">\r\n                <div class=\"toast-header\">\r\n                  <svg class=\"bd-placeholder-img rounded mr-2\" width=\"20\" height=\"20\" xmlns=\"http://www.w3.org/2000/svg\" role=\"img\" aria-label=\" :  \" preserveAspectRatio=\"xMidYMid slice\" focusable=\"false\"><title> </title><rect width=\"100%\" height=\"100%\" fill=\"${color}\"></rect><text x=\"50%\" y=\"50%\" fill=\"#dee2e6\" dy=\".3em\"> </text></svg>\r\n                  <strong class=\"mr-auto\">${title}</strong>\r\n                   <button type=\"button\" class=\"ml-2 mb-1 close\" data-dismiss=\"toast\" aria-label=\"Close\">\r\n                    <span aria-hidden=\"true\">&times;</span>\r\n                  </button>\r\n                </div>\r\n                <div class=\"toast-body\">\r\n                  ${message}\r\n                </div>\r\n              </div>\r\n        `;\r\n\r\n        var message = $(template);\r\n        message.toast();\r\n        message.on('hidden.bs.toast', function () {\r\n            $(this).remove();\r\n        });\r\n\r\n        $('#notifications').prepend(message);\r\n\r\n        message.toast('show');\r\n    }\r\n\r\n    var initSignalr = function (hostName) {\r\n\r\n\r\n        // Define url where the api task management service is running\r\n        // $.connection.hub.url = hubConnectionUrl;\r\n        var connectionUrl = 'http://' + hostName + ':8082/';\r\n        console.log(connectionUrl);\r\n        $.connection.hub.url = connectionUrl;\r\n\r\n        // Connect to signalR hub\r\n        scheduler = $.connection.taskSchedulerHub;\r\n\r\n        // Receive task statusses\r\n        scheduler.client.updateTask = function (id, title, active, lastRunTime, lastRunResult, enabled) {\r\n\r\n            var statusCode = parseInt(lastRunResult.split(\" \")[0]);\r\n            var notifyType = \"success\";\r\n\r\n            if (statusCode >= 400) {\r\n                notifyType = \"danger\";\r\n            }\r\n\r\n            if (!active) {\r\n                $('tr[id=\"' + id + '\"] .btn-runnow').removeClass('disabled');\r\n                $('tr[id=\"' + id + '\"] .btn-runnow').toggleClass('btn-outline-secondary btn-secondary');\r\n\r\n                updatePanel(lastRunTime, \"Task \" + title + \" has completed\", notifyType, 'API Task Service');\r\n\r\n                // $('tr[id=\"' + id + '\"]').effect(\"highlight\", { color: 'LightBlue' }, 1500);\r\n                showNotification('<b>' + lastRunResult + ':</b> ', 'Task <b>' + title + '</b> has completed', notifyType);\r\n            } else {\r\n                $('tr[id=\"' + id + '\"] .btn-runnow').addClass('disabled');\r\n                $('tr[id=\"' + id + '\"] .btn-runnow').toggleClass('btn-outline-secondary btn-secondary');\r\n\r\n                updatePanel(lastRunTime, \"Task \" + title + \" has started\", notifyType, 'API Task Service');\r\n                // $('tr[id=\"' + id + '\"]').effect(\"highlight\", { color: 'LightGreen' }, 1500);\r\n                showNotification('<b>' + lastRunResult + ':</b> ', 'Task <b>' + title + '</b> has started', notifyType);\r\n            }\r\n\r\n            refreshTask(id);\r\n\r\n            $('tr[id=\"' + id + '\"]').find('.last-run-time').text(lastRunTime);\r\n            $('tr[id=\"' + id + '\"]').find('.last-run-result').text(lastRunResult).removeClass('kt-font-danger kt-font-success').addClass('kt-font-' + notifyType);\r\n\r\n            var task = tasks.find(x => x.id === id);\r\n            task.last_run_time = lastRunTime;\r\n            task.last_run_result = lastRunResult;\r\n        };\r\n\r\n        scheduler.client.updateTaskStatus = function (id, enabled) {\r\n\r\n            console.log('updateTaskStatus: ' + id);\r\n            console.log('enabled: ' + enabled);\r\n            \r\n            if (enabled) {\r\n                $('span[data-record-id=\"' + id + '\"]').data(\"enabled\", 1).toggleClass('kt-badge--success kt-badge--danger').text('Enabled');\r\n\r\n            } else {\r\n                $('span[data-record-id=\"' + id + '\"]').data(\"enabled\", 0).toggleClass('kt-badge--success kt-badge--danger').text('Disabled');\r\n            }\r\n\r\n            var task = tasks.find(x => x.id === id);\r\n            task.enabled = enabled;\r\n\r\n            console.log(task);\r\n\r\n        };\r\n        $.connection.hub.start().done(function () {\r\n\r\n        });\r\n    };\r\n\r\n    return {\r\n        init: function () {\r\n            \r\n            init();\r\n            initDatatable();\r\n\r\n        }\r\n    };\r\n}();\r\n\r\nKTUtil.ready(function () {\r\n    ApiTaskMonitor.init();\r\n});\r\n\n\n//# sourceURL=webpack:///../src/assets/js/pages/custom/monitor/api-task-monitor.js?");

/***/ })

/******/ });