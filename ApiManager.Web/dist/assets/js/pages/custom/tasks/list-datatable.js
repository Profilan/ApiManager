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
/******/ 	return __webpack_require__(__webpack_require__.s = "../src/assets/js/pages/custom/tasks/list-datatable.js");
/******/ })
/************************************************************************/
/******/ ({

/***/ "../src/assets/js/pages/custom/tasks/list-datatable.js":
/*!*************************************************************!*\
  !*** ../src/assets/js/pages/custom/tasks/list-datatable.js ***!
  \*************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
eval("\r\n// Class definition\r\n\r\nvar KTTasksListDatatable = function() {\r\n\r\n\t// variables\r\n    var datatable;\r\n    var scheduleTable;\r\n\r\n\t// init\r\n    var init = function () {\r\n        // init the datatables. Learn more: https://keenthemes.com/keen/?page=docs&section=datatable\r\n        datatable = $('#kt_apps_task_list_datatable').KTDatatable({\r\n            // datasource definition\r\n            data: {\r\n                type: 'remote',\r\n                source: {\r\n                    read: {\r\n                        url: '/api/Task'\r\n                    }\r\n                },\r\n                pageSize: 10, // display 20 records per page\r\n                serverPaging: true,\r\n                serverFiltering: true,\r\n                serverSorting: true\r\n            },\r\n\r\n            // layout definition\r\n            layout: {\r\n                scroll: false, // enable/disable datatable scroll both horizontal and vertical when needed.\r\n                footer: false // display/hide footer\r\n            },\r\n\r\n            // column sorting\r\n            sortable: true,\r\n\r\n            pagination: true,\r\n\r\n            detail: {\r\n                title: 'Load schedules',\r\n                content: scheduleTableInit\r\n            },\r\n\r\n            search: {\r\n                input: $('#generalSearch'),\r\n                delay: 400\r\n            },\r\n\r\n            // columns definition\r\n            columns: [{\r\n                field: 'id',\r\n                title: '',\r\n                sortable: false,\r\n                width: 30,\r\n                textAlign: 'center'\r\n            }, {\r\n                field: \"title\",\r\n                title: \"Title\",\r\n                template: function (row) {\r\n                    return `\r\n                        ${row.title}<br><small>${row.id}</small>\r\n                    `;\r\n                }\r\n            }, {\r\n                field: \"type\",\r\n                title: \"Type\"\r\n            }, {\r\n                field: \"Actions\",\r\n                width: 120,\r\n                title: \"Actions\",\r\n                sortable: false,\r\n                autoHide: false,\r\n                overflow: 'visible',\r\n                template: function (row) {\r\n                    return '\\\r\n                                <a href=\"/task/edit/' + row.id + '\" class=\"btn btn-sm btn-clean btn-icon btn-icon-sm\" title=\"Edit Details\">\\\r\n                                    <i class=\"flaticon-list-1\"></i>\\\r\n                                </a>\\\r\n                                <a href=\"/api/task/delete/' + row.id + '\" class=\"btn-delete btn btn-sm btn-clean btn-icon btn-icon-sm\" title=\"Delete\">\\\r\n                                    <i class=\"flaticon-delete\"></i>\\\r\n                                </a>\\\r\n                                <a href=\"/schedule/create?taskId=' + row.id + '\" class=\"btn btn-sm btn-clean btn-icon btn-icon-sm\" title=\"Add Schedule\">\\\r\n                                    <i class=\"flaticon-time-2\"></i>\\\r\n                                </a>\\\r\n                            ';\r\n                }\r\n            }]\r\n        }).on('kt-datatable--on-layout-updated', function (e, options) {\r\n            console.log('datatable-on-layout-updated');\r\n\r\n            $('.btn-delete').click(function (event) {\r\n                event.preventDefault();\r\n\r\n                var btn = $(this);\r\n\r\n                var url = btn.attr('href');\r\n\r\n                $('#deleteModal .btn-ok').data('url', url);\r\n\r\n                $('#deleteModal').modal('show');\r\n\r\n            });\r\n        });\r\n\r\n        $('#deleteModal .btn-ok').click(function (event) {\r\n            event.preventDefault();\r\n\r\n            var url = $(this).data('url');\r\n\r\n            $.post(url, function () {\r\n                datatable.reload();\r\n                scheduleTable.reload();\r\n                $('#deleteModal').modal('hide');\r\n\r\n                Swal.fire(\"Schedule deleted!\");\r\n            });\r\n        });\r\n    };\r\n\r\n    var scheduleTableInit = function (e) {\r\n        scheduleTable = $('<div/>').attr('id', 'child_data_ajax_' + e.data.id).appendTo(e.detailCell).KTDatatable({\r\n            data: {\r\n                type: 'remote',\r\n                source: {\r\n                    read: {\r\n                        url: '/api/schedule',\r\n                        params: {\r\n                            // custom query params\r\n                            query: {\r\n                                generalSearch: '',\r\n                                taskId: e.data.id,\r\n                            },\r\n                        },\r\n                    },\r\n                },\r\n                pageSize: 5,\r\n                serverPaging: true,\r\n                serverFiltering: false,\r\n                serverSorting: true,\r\n            },\r\n\r\n            layout: {\r\n                scroll: false,\r\n                footer: false,\r\n            },\r\n\r\n            sortable: true,\r\n\r\n            columns: [\r\n                {\r\n                    field: 'start',\r\n                    title: 'Start',\r\n                },\r\n                {\r\n                    field: 'type',\r\n                    title: 'Type'\r\n                },\r\n                {\r\n                    field: 'enabled',\r\n                    title: 'State',\r\n                    // callback function support for column rendering\r\n                    template: function (row) {\r\n                        var state = {\r\n                            false: { 'title': 'Disabled', 'class': 'label-light-danger' },\r\n                            true: { 'title': 'Enabled', 'class': ' label-light-primary' },\r\n                        };\r\n                        return '<span class=\"label ' + state[row.enabled].class + ' label-inline label-bold\">' + state[row.enabled].title + '</span>';\r\n                    },\r\n                },\r\n                {\r\n                    field: \"Actions\",\r\n                    width: 80,\r\n                    title: \"Actions\",\r\n                    sortable: false,\r\n                    autoHide: false,\r\n                    overflow: 'visible',\r\n                    template: function (row) {\r\n                        return '\\\r\n                                <a href=\"/schedule/edit/' + row.id + '\" class=\"btn btn-sm btn-clean btn-icon btn-icon-sm\" title=\"Edit Details\">\\\r\n                                    <i class=\"flaticon-list-1\"></i>\\\r\n                                </a>\\\r\n                                <a href=\"/api/schedule/delete/' + row.id + '\" class=\"btn-schedule-delete btn btn-sm btn-clean btn-icon btn-icon-sm\" title=\"Delete\">\\\r\n                                    <i class=\"flaticon-delete\"></i>\\\r\n                                </a>\\\r\n                            ';\r\n                    }\r\n                }\r\n\r\n            ]\r\n        }).on('kt-datatable--on-layout-updated', function (e, options) {\r\n            console.log('datatable-on-layout-updated');\r\n            $('.btn-schedule-delete').click(function (event) {\r\n                event.preventDefault();\r\n\r\n                var btn = $(this);\r\n\r\n                var url = btn.attr('href');\r\n\r\n                $('#deleteModal .btn-ok').data('url', url);\r\n\r\n                $('#deleteModal').modal('show');\r\n            });\r\n        });\r\n\r\n    };\r\n\r\n\t// search\r\n    var search = function () {\r\n        $('#kt_form_status').on('change', function () {\r\n            datatable.search($(this).val().toLowerCase(), 'Status');\r\n        });\r\n    };\r\n\r\n\t// selection\r\n    var selection = function () {\r\n        // init form controls\r\n        //$('#kt_form_status, #kt_form_type').selectpicker();\r\n\r\n        // event handler on check and uncheck on records\r\n        datatable.on('kt-datatable--on-check kt-datatable--on-uncheck kt-datatable--on-layout-updated', function (e) {\r\n            var checkedNodes = datatable.rows('.kt-datatable__row--active').nodes(); // get selected records\r\n            var count = checkedNodes.length; // selected records count\r\n\r\n            $('#kt_subheader_group_selected_rows').html(count);\r\n\r\n            if (count > 0) {\r\n                $('#kt_subheader_search').addClass('kt-hidden');\r\n                $('#kt_subheader_group_actions').removeClass('kt-hidden');\r\n            } else {\r\n                $('#kt_subheader_search').removeClass('kt-hidden');\r\n                $('#kt_subheader_group_actions').addClass('kt-hidden');\r\n            }\r\n        });\r\n    };\r\n\r\n\t// fetch selected records\r\n\tvar selectedFetch = function() {\r\n\t\t// event handler on selected records fetch modal launch\r\n\t\t$('#kt_datatable_records_fetch_modal').on('show.bs.modal', function(e) {\r\n\t\t\t// show loading dialog\r\n            var loading = new KTDialog({'type': 'loader', 'placement': 'top center', 'message': 'Loading ...'});\r\n            loading.show();\r\n\r\n            setTimeout(function() {\r\n                loading.hide();\r\n\t\t\t}, 1000);\r\n\t\t\t\r\n\t\t\t// fetch selected IDs\r\n\t\t\tvar ids = datatable.rows('.kt-datatable__row--active').nodes().find('.kt-checkbox--single > [type=\"checkbox\"]').map(function(i, chk) {\r\n\t\t\t\treturn $(chk).val();\r\n\t\t\t});\r\n\r\n\t\t\t// populate selected IDs\r\n\t\t\tvar c = document.createDocumentFragment();\r\n\t\t\t\t\r\n\t\t\tfor (var i = 0; i < ids.length; i++) {\r\n\t\t\t\tvar li = document.createElement('li');\r\n\t\t\t\tli.setAttribute('data-id', ids[i]);\r\n\t\t\t\tli.innerHTML = 'Selected record ID: ' + ids[i];\r\n\t\t\t\tc.appendChild(li);\r\n\t\t\t}\r\n\r\n\t\t\t$(e.target).find('#kt_apps_user_fetch_records_selected').append(c);\r\n\t\t}).on('hide.bs.modal', function(e) {\r\n\t\t\t$(e.target).find('#kt_apps_user_fetch_records_selected').empty();\r\n\t\t});\r\n\t};\r\n\r\n\t// selected records status update\r\n    var selectedStatusUpdate = function () {\r\n        $('#kt_subheader_group_actions_status_change').on('click', \"[data-toggle='status-change']\", function () {\r\n            var status = $(this).find(\".kt-nav__link-text\").html();\r\n\r\n            // fetch selected IDs\r\n            var ids = datatable.rows('.kt-datatable__row--active').nodes().find('.kt-checkbox--single > [type=\"checkbox\"]').map(function (i, chk) {\r\n                return $(chk).val();\r\n            });\r\n\r\n            if (ids.length > 0) {\r\n                // learn more: https://sweetalert2.github.io/\r\n                swal.fire({\r\n                    buttonsStyling: false,\r\n\r\n                    html: \"Are you sure to update \" + ids.length + \" selected records status to \" + status + \" ?\",\r\n                    type: \"info\",\r\n\r\n                    confirmButtonText: \"Yes, update!\",\r\n                    confirmButtonClass: \"btn btn-sm btn-bold btn-brand\",\r\n\r\n                    showCancelButton: true,\r\n                    cancelButtonText: \"No, cancel\",\r\n                    cancelButtonClass: \"btn btn-sm btn-bold btn-default\"\r\n                }).then(function (result) {\r\n                    if (result.value) {\r\n                        swal.fire({\r\n                            title: 'Deleted!',\r\n                            text: 'Your selected records statuses have been updated!',\r\n                            type: 'success',\r\n                            buttonsStyling: false,\r\n                            confirmButtonText: \"OK\",\r\n                            confirmButtonClass: \"btn btn-sm btn-bold btn-brand\"\r\n                        });\r\n                        // result.dismiss can be 'cancel', 'overlay',\r\n                        // 'close', and 'timer'\r\n                    } else if (result.dismiss === 'cancel') {\r\n                        swal.fire({\r\n                            title: 'Cancelled',\r\n                            text: 'You selected records statuses have not been updated!',\r\n                            type: 'error',\r\n                            buttonsStyling: false,\r\n                            confirmButtonText: \"OK\",\r\n                            confirmButtonClass: \"btn btn-sm btn-bold btn-brand\"\r\n                        });\r\n                    }\r\n                });\r\n            }\r\n        });\r\n    };\r\n\r\n\t// selected records delete\r\n    var selectedDelete = function () {\r\n        $('#kt_subheader_group_actions_delete_all').on('click', function () {\r\n            // fetch selected IDs\r\n            var ids = datatable.rows('.kt-datatable__row--active').nodes().find('.kt-checkbox--single > [type=\"checkbox\"]').map(function (i, chk) {\r\n                return $(chk).val();\r\n            });\r\n\r\n            if (ids.length > 0) {\r\n                // learn more: https://sweetalert2.github.io/\r\n                swal.fire({\r\n                    buttonsStyling: false,\r\n\r\n                    text: \"Are you sure to delete \" + ids.length + \" selected records ?\",\r\n                    type: \"danger\",\r\n\r\n                    confirmButtonText: \"Yes, delete!\",\r\n                    confirmButtonClass: \"btn btn-sm btn-bold btn-danger\",\r\n\r\n                    showCancelButton: true,\r\n                    cancelButtonText: \"No, cancel\",\r\n                    cancelButtonClass: \"btn btn-sm btn-bold btn-brand\"\r\n                }).then(function (result) {\r\n                    if (result.value) {\r\n                        swal.fire({\r\n                            title: 'Deleted!',\r\n                            text: 'Your selected records have been deleted! :(',\r\n                            type: 'success',\r\n                            buttonsStyling: false,\r\n                            confirmButtonText: \"OK\",\r\n                            confirmButtonClass: \"btn btn-sm btn-bold btn-brand\",\r\n                        });\r\n                        // result.dismiss can be 'cancel', 'overlay',\r\n                        // 'close', and 'timer'\r\n                    } else if (result.dismiss === 'cancel') {\r\n                        swal.fire({\r\n                            title: 'Cancelled',\r\n                            text: 'You selected records have not been deleted! :)',\r\n                            type: 'error',\r\n                            buttonsStyling: false,\r\n                            confirmButtonText: \"OK\",\r\n                            confirmButtonClass: \"btn btn-sm btn-bold btn-brand\",\r\n                        });\r\n                    }\r\n                });\r\n            }\r\n        });\r\n    };\r\n\r\n\tvar updateTotal = function() {\r\n\t\tdatatable.on('kt-datatable--on-layout-updated', function () {\r\n\t\t\t//$('#kt_subheader_total').html(datatable.getTotalRows() + ' Total');\r\n\t\t});\r\n\t};\r\n\r\n\treturn {\r\n\t\t// public functions\r\n\t\tinit: function() {\r\n\t\t\tinit();\r\n\t\t\tsearch();\r\n\t\t\tselection();\r\n\t\t\tselectedFetch();\r\n\t\t\tselectedStatusUpdate();\r\n\t\t\tselectedDelete();\r\n\t\t\tupdateTotal();\r\n\t\t}\r\n\t};\r\n}();\r\n\r\n// On document ready\r\nKTUtil.ready(function() {\r\n\tKTTasksListDatatable.init();\r\n});\n\n//# sourceURL=webpack:///../src/assets/js/pages/custom/tasks/list-datatable.js?");

/***/ })

/******/ });