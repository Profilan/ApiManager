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
/******/ 	return __webpack_require__(__webpack_require__.s = "../src/assets/js/pages/custom/monitor/api-task-scheduler-table.js");
/******/ })
/************************************************************************/
/******/ ({

/***/ "../src/assets/js/pages/custom/monitor/api-task-scheduler-table.js":
/*!*************************************************************************!*\
  !*** ../src/assets/js/pages/custom/monitor/api-task-scheduler-table.js ***!
  \*************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
eval("﻿\r\n\r\nvar ApiTaskSchedulerDatatable = function () {\r\n\r\n    // variables\r\n    var datatable;\r\n    var scheduler;\r\n\r\n    var panel = KTUtil.get('kt_quick_panel');\r\n    var notificationPanel = $('.kt_quick_panel_tab_notifications');\r\n\r\n    // init\r\n    var init = function () {\r\n        datatable = $('#api_tasks_list').KTDatatable({\r\n            // datasource definition\r\n            data: {\r\n                type: 'remote',\r\n                source: {\r\n                    read: {\r\n                        url: '/api/monitor/api-task-scheduler'\r\n                    }\r\n                }\r\n            },\r\n\r\n            // layout definition\r\n            layout: {\r\n                scroll: false, //disable datatable scroll, both horizontal and vertical\r\n                footer: false // hide footer\r\n            },\r\n\r\n            sortable: false,\r\n\r\n            pagination: false,\r\n\r\n            // rows definition\r\n            rows: {\r\n                afterTemplate: function (row, data, index) {\r\n                    // console.log(row);\r\n                    // $('tr[data-field=' + index + ']').data('record-id', data.id);\r\n                    row.attr('id', data.id);\r\n                }\r\n            },\r\n\r\n            // columns definition\r\n            columns: [\r\n                {\r\n                    field: 'title',\r\n                    title: 'Task'\r\n                },\r\n                {\r\n                    field: 'enabled',\r\n                    title: 'Enabled',\r\n                    template: function (row) {\r\n                        var status = {\r\n                            true: { 'title': 'Enabled', 'class': 'kt-badge--success', 'enabled': 1 },\r\n                            false: { 'title': 'Disabled', 'class': 'kt-badge--danger', 'enabled': 0 }\r\n                        };\r\n\r\n                        return '<span class=\"enable-task kt-badge ' + status[row.enabled].class + ' kt-badge--inline kt-badge--pill\" data-enabled=\"' + status[row.enabled].enabled + '\" data-record-id=\"' + row.id + '\">' + status[row.enabled].title + '</span>';\r\n\r\n                    }\r\n                },\r\n                {\r\n                    field: 'queued',\r\n                    title: '#Queued',\r\n                    template: function (row) {\r\n                        var color = \"secondary\";\r\n                        if (row.queued > 0) {\r\n                            color = \"primary\";\r\n                        }\r\n                        return '<a href=\"/monitor/apiqueue?taskId=' + row.id + '\"><span class=\"queued text-' + color + '\">' + row.queued + '</span></a>';\r\n                    }\r\n                },\r\n                {\r\n                    field: 'last_run_time',\r\n                    title: 'Last Run Time',\r\n                    template: function (row) {\r\n                        return '<span class=\"last-run-time\">' + row.last_run_time + '</span>';\r\n                    }\r\n                },\r\n                {\r\n                    field: 'last_run_result',\r\n                    title: 'Last run Result',\r\n                    template: function (row) {\r\n                        var color = 'success';\r\n                        var statusCode = parseInt(row.last_run_result.split(\" \")[0]);\r\n                        if (statusCode >= 400) {\r\n                            color = 'danger';\r\n                        }\r\n                        return '<span class=\"last-run-result kt-font-' + color + '\">' + row.last_run_result + '</span>';\r\n                    }\r\n                },\r\n                {\r\n                    field: 'Actions',\r\n                    title: 'Actions',\r\n                    width: 130,\r\n                    overflow: 'visible',\r\n                    textAlign: 'center',\r\n                    template: function (row, index, datatable) {\r\n                        var dropup = (datatable.getPageSize() - index) <= 4 ? 'dropup' : '';\r\n                        return '<div class=\"dropdown ' + dropup + '\">\\\r\n                                    <a href=\"#\" class=\"btn btn-hover-brand btn-icon btn-pill\" data-toggle=\"dropdown\">\\\r\n                                        <i class=\"la la-ellipsis-h\"></i>\\\r\n                                    </a>\\\r\n                                    <div class=\"dropdown-menu dropdown-menu-right\">\\\r\n                                        <a id=\"runNowLink-' + row.id + '\" data-record-id=\"' + row.id + '\" class=\"dropdown-item btn-runnow\" href=\"#\"><i class=\"la la-cogs\"></i> Run Now</a>\\\r\n                                        <a class=\"dropdown-item btn-reset\" href=\"#\" data-record-id=\"' + row.id + '\"><i class=\"la la-power-off\"></i> Reset</a>\\\r\n                                    </div>\\\r\n                                </div>\\\r\n                                <a href=\"#\" class=\"btn btn-hover-brand btn-icon btn-pill btn-details\" data-record-id=\"' + row.id + '\" data-toggle=\"modal\" data-target=\"#detailsModal\" title=\"Details\">\\\r\n                                    <i class=\"la la-book\"></i>\\\r\n                                </a>';\r\n                    }\r\n                }\r\n            ]\r\n        });\r\n\r\n        datatable.on('kt-datatable--on-layout-updated', function (e, args) {\r\n            $(\".enable-task\").click(function () {\r\n\r\n                if ($(this).data(\"enabled\")) {\r\n\r\n                    scheduler.server.disableTask($(this).data(\"record-id\"));\r\n\r\n                } else {\r\n\r\n                    scheduler.server.enableTask($(this).data(\"record-id\"));\r\n\r\n                }\r\n            });\r\n\r\n            $(\".btn-runnow\").click(function (e) {\r\n                e.preventDefault();\r\n\r\n                var id = $(this).data(\"record-id\");\r\n\r\n                if (!$(\"#runNowLink-\" + id).hasClass('disabled')) {\r\n                    console.log('Run Now');\r\n                    $(\"#runNowLink-\" + id).addClass('disabled');\r\n                    scheduler.server.runTask($(this).data(\"record-id\"));\r\n                }\r\n            });\r\n\r\n            $('.btn-reset').click(function (e) {\r\n                e.preventDefault();\r\n\r\n                console.log('reset');\r\n\r\n                var $this = $(this);\r\n                $(this).addClass('disabled');\r\n                $.post('/api/task/reset/' + $(this).data(\"record-id\"), function (result) {\r\n                    $this.removeClass(\"disabled\");\r\n                });\r\n            });\r\n\r\n            refreshTasks();\r\n            setInterval(refreshTasks, 60 * 1000);\r\n        });\r\n\r\n        var modal = $('#detailsModal');\r\n\r\n        modal.on('show.bs.modal', function (e) {\r\n            var link = $(e.relatedTarget);\r\n            var id = link.data('record-id');\r\n\r\n            $.getJSON('/api/task/' + id)\r\n                .done(function (data) {\r\n                    modal.find('.modal-title').text(data.title);\r\n                    modal.find('.modal-body > p').text(data.last_run_details);\r\n                });\r\n        });\r\n\r\n    };\r\n\r\n    var refreshTasks = function () {\r\n        $.getJSON(\"/api/task\")\r\n            .done(function (data) {\r\n                $.each(data, function (idx, task) {\r\n                    var color = 'secondary';\r\n                    if (task.queued > 0) {\r\n                        color = 'primary';\r\n                    }\r\n                    $(\"#\" + task.id).find(\".queued\").removeClass('text-primary text-secondary').addClass('text-' + color).text(task.queued);\r\n                });\r\n            });\r\n    };\r\n\r\n    var refreshTask = function (id) {\r\n        $.getJSON(\"/api/task/\" + id)\r\n            .done(function (data) {\r\n                var color = 'secondary';\r\n                if (data.queued > 0) {\r\n                    color = 'primary';\r\n                }\r\n                $(\"#\" + id).find(\".queued\").removeClass('text-primary text-secondary').addClass('text-' + color).text(data.queued);\r\n            });\r\n    };\r\n\r\n    var updatePanel = function (timestamp, message, type, source) {\r\n\r\n        var timelineItem = $('.kt-timeline__item.prototype').clone().removeClass('prototype kt-hidden');\r\n\r\n        timelineItem.find('.kt-timeline__item-datetime').text(timestamp);\r\n        timelineItem.find('.kt-timeline__item-text').text(message);\r\n        timelineItem.find('.kt-timeline__item-info').text(source);\r\n        timelineItem.addClass(' kt-timeline__item--' + type);\r\n\r\n        $('.kt-timeline').prepend(timelineItem);\r\n\r\n\r\n        // KTUtil.scrollUpdate(notificationPanel);\r\n    };\r\n\r\n    var showNotification = function (title, message, type) {\r\n\r\n        /*\r\n        $.notify({\r\n            title: title,\r\n            message: message\r\n        },\r\n            {\r\n                type: type,\r\n                template: `\r\n                    <div class=\"alert alert-custom alert-notice alert-{0} fade show\" role=\"alert\">\r\n                        <div class=\"alert-text\">\r\n                            <span data-notify=\"title\">{1}</span><br>\r\n                            <span data-notify=\"message\">{2}</span>\r\n                        </div>\r\n                        <div class=\"alert-close\">\r\n                            <button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">\r\n                                <span aria-hidden=\"true\"><i class=\"ki ki-close\"></i></span>\r\n                            </button>\r\n                        </div>\r\n                    </div>\r\n                `\r\n            });\r\n        */\r\n        var color = \"green\";\r\n        if (type == \"danger\") {\r\n            color = \"red\";\r\n        }\r\n\r\n        var template = `\r\n              <div class=\"toast fade\" role=\"alert\" aria-live=\"assertive\" aria-atomic=\"true\" data-delay=\"5000\">\r\n                <div class=\"toast-header\">\r\n                  <svg class=\"bd-placeholder-img rounded mr-2\" width=\"20\" height=\"20\" xmlns=\"http://www.w3.org/2000/svg\" role=\"img\" aria-label=\" :  \" preserveAspectRatio=\"xMidYMid slice\" focusable=\"false\"><title> </title><rect width=\"100%\" height=\"100%\" fill=\"${color}\"></rect><text x=\"50%\" y=\"50%\" fill=\"#dee2e6\" dy=\".3em\"> </text></svg>\r\n                  <strong class=\"mr-auto\">${title}</strong>\r\n                   <button type=\"button\" class=\"ml-2 mb-1 close\" data-dismiss=\"toast\" aria-label=\"Close\">\r\n                    <span aria-hidden=\"true\">&times;</span>\r\n                  </button>\r\n                </div>\r\n                <div class=\"toast-body\">\r\n                  ${message}\r\n                </div>\r\n              </div>\r\n        `;\r\n\r\n        var message = $(template);\r\n        message.toast();\r\n        message.on('hidden.bs.toast', function () {\r\n            $(this).remove();\r\n        });\r\n\r\n        $('#notifications').prepend(message);\r\n\r\n        message.toast('show');\r\n    }\r\n\r\n    var initSignalr = function () {\r\n\r\n\r\n        // Define url where the api task management service is running\r\n        $.connection.hub.url = \"http://ap-eek-zwd-01:8082/signalr\";\r\n\r\n        // Connect to signalR hub\r\n        scheduler = $.connection.taskSchedulerHub;\r\n\r\n        // Receive task statusses\r\n        scheduler.client.updateTask = function (id, title, active, lastRunTime, lastRunResult, enabled) {\r\n\r\n            var statusCode = parseInt(lastRunResult.split(\" \")[0]);\r\n            var notifyType = \"success\";\r\n\r\n            if (statusCode >= 400) {\r\n                notifyType = \"danger\";\r\n            }\r\n\r\n            if (!active) {\r\n                $('#runNowLink-' + id).removeClass('disabled');\r\n\r\n                updatePanel(lastRunTime, \"Task \" + title + \" has completed\", notifyType, 'API Task Service');\r\n\r\n                // $('tr[id=\"' + id + '\"]').effect(\"highlight\", { color: 'LightBlue' }, 1500);\r\n                showNotification('<b>' + lastRunResult + ':</b> ', 'Task <b>' + title + '</b> has completed', notifyType);\r\n            } else {\r\n                $('#runNowLink-' + id).addClass('disabled');\r\n\r\n                updatePanel(lastRunTime, \"Task \" + title + \" has started\", notifyType, 'API Task Service');\r\n                // $('tr[id=\"' + id + '\"]').effect(\"highlight\", { color: 'LightGreen' }, 1500);\r\n                showNotification('<b>' + lastRunResult + ':</b> ', 'Task <b>' + title + '</b> has started', notifyType);\r\n            }\r\n\r\n            $('tr[id=\"' + id + '\"]').find('.last-run-time').text(lastRunTime);\r\n            $('tr[id=\"' + id + '\"]').find('.last-run-result').text(lastRunResult).removeClass('kt-font-danger kt-font-success').addClass('kt-font-' + notifyType);\r\n\r\n            refreshTask(id);\r\n\r\n        };\r\n\r\n        scheduler.client.updateTaskStatus = function (id, title, enabled) {\r\n\r\n            console.log('updateTaskStatus: ' + id);\r\n            \r\n            if (enabled) {\r\n                $('span[data-record-id=\"' + id + '\"]').data(\"enabled\", 1).toggleClass('kt-badge--success kt-badge--danger');\r\n\r\n            } else {\r\n                $('span[data-record-id=\"' + id + '\"]').data(\"enabled\", 0).toggleClass('kt-badge--success kt-badge--danger');\r\n            }\r\n\r\n        };\r\n        $.connection.hub.start().done(function () {\r\n\r\n        });\r\n    };\r\n\r\n    return {\r\n        init: function () {\r\n            init();\r\n            initSignalr();\r\n        }\r\n    };\r\n}();\r\n\r\nKTUtil.ready(function () {\r\n    ApiTaskSchedulerDatatable.init();\r\n});\r\n\n\n//# sourceURL=webpack:///../src/assets/js/pages/custom/monitor/api-task-scheduler-table.js?");

/***/ })

/******/ });