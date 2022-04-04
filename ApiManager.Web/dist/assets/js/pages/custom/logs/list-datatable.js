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
/******/ 	return __webpack_require__(__webpack_require__.s = "../src/assets/js/pages/custom/logs/list-datatable.js");
/******/ })
/************************************************************************/
/******/ ({

/***/ "../src/assets/js/pages/custom/logs/list-datatable.js":
/*!************************************************************!*\
  !*** ../src/assets/js/pages/custom/logs/list-datatable.js ***!
  \************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
eval("\r\n// Class definition\r\n\r\nvar KTLogsListDatatable = function() {\r\n\r\n\t// variables\r\n    var datatable;\r\n\r\n    var datatableOptions = {\r\n        // datasource definition\r\n        data: {\r\n            type: 'remote',\r\n            source: {\r\n                read: {\r\n                    url: '/api/log',\r\n                    timeout: 300000,\r\n                }\r\n            },\r\n            pageSize: 20, // display 20 records per page\r\n            serverPaging: true,\r\n            serverFiltering: true,\r\n            serverSorting: true,\r\n            saveState: false,\r\n        },\r\n\r\n        // layout definition\r\n        layout: {\r\n            scroll: false, // enable/disable datatable scroll both horizontal and vertical when needed.\r\n            footer: false // display/hide footer\r\n        },\r\n\r\n        // column sorting\r\n        sortable: true,\r\n\r\n        pagination: true,\r\n\r\n        search: {\r\n            input: $('#generalSearch'),\r\n            delay: 400\r\n        },\r\n\r\n        // columns definition\r\n        columns: [{\r\n            field: \"TimeStamp\",\r\n            title: \"Date\",\r\n            template: function (row) {\r\n                return row.TimeStamp;\r\n            }\r\n        }, {\r\n            field: 'Message',\r\n            title: 'Message'\r\n        }, {\r\n            field: 'PriorityName',\r\n            title: 'Status',\r\n            template: function (row) {\r\n                var color = \"green\";\r\n                if (row.PriorityName === \"ERR\") {\r\n                    color = \"red\";\r\n                }\r\n                if (row.PriorityName === \"ALERT\") {\r\n                    color = \"purple\";\r\n                }\r\n\r\n                return '<span style=\"color: ' + color + ';\">' + row.PriorityName + '</span>';\r\n            }\r\n        }, {\r\n            field: 'Url',\r\n            title: 'Destination'\r\n        }, {\r\n            field: 'UserName',\r\n            title: 'Username'\r\n        }, {\r\n            field: 'Duration',\r\n            title: 'Duration (ms)'\r\n        }, {\r\n            field: \"Actions\",\r\n            width: 80,\r\n            title: \"Actions\",\r\n            sortable: false,\r\n            autoHide: false,\r\n            overflow: 'visible',\r\n            template: function (row) {\r\n                return '\\\r\n\t\t\t\t\t\t\t<a href=\"#\" class=\"btn btn-hover-brand btn-icon btn-pill btn-details\" data-record-id=\"' + row.Id + '\" data-toggle=\"modal\" data-target=\"#detailsModal\" title=\"Details\">\\\r\n                                    <i class=\"la la-book\"></i>\\\r\n                            </a>\\\r\n\t\t\t\t\t\t';\r\n            }\r\n        }]\r\n    };\r\n\r\n\t// init\r\n    var init = function () {\r\n        // init the datatables. Learn more: https://keenthemes.com/keen/?page=docs&section=datatable\r\n        datatable = $('#kt_apps_log_list_datatable').KTDatatable(datatableOptions);\r\n\r\n        var modal = $('#detailsModal');\r\n\r\n        modal.on('show.bs.modal', function (e) {\r\n            var link = $(e.relatedTarget);\r\n            var id = link.data('record-id');\r\n\r\n            $.getJSON('/api/log/' + id)\r\n                .done(function (data) {\r\n                    \r\n                    modal.find('.modal-body > p').text(data.User.username + ': ' + data.Detail);\r\n                });\r\n        });\r\n    };\r\n\r\n\t// search\r\n    var search = function () {\r\n        //$('#kt_form_user').on('change', function () {\r\n        //   datatable.search($(this).val().toLowerCase(), 'UserId');\r\n        //});\r\n        $('#kt_form_user').on('changed.bs.select', function (e, clickedIndex, isSelected, previousValue) {\r\n            console.log($(this).val());\r\n            datatable.search($(this).val(), 'UserId');\r\n        });\r\n\r\n        $('#kt_form_task').on('changed.bs.select', function (e, clickedIndex, isSelected, previousValue) {\r\n            console.log($(this).val());\r\n            datatable.search($(this).val(), 'TaskId');\r\n        });\r\n\r\n        //$('#kt_form_error_type').on('change', function () {\r\n        //    datatable.search($(this).val().toLowerCase(), 'Type');\r\n        //});\r\n        $('#kt_form_error_type').on('changed.bs.select', function (e, clickedIndex, isSelected, previousValue) {\r\n            datatable.search($(this).val(), 'Type');\r\n        });\r\n\r\n        addDateRangeSelector();\r\n\r\n        $('#clearFilterBtn').click(function (e) {\r\n            e.preventDefault();\r\n\r\n            //$(\"#generalSearch\").val(\"\");\r\n            //$(\"#kt_form_user\").selectpicker('val', '');\r\n            //$('#kt_form_user').selectpicker('render'); \r\n            //$(\"#kt_form_error_type\").selectpicker('val', '');\r\n            //$(\"#kt_form_error_type\").selectpicker('render');\r\n\r\n            // datatable.reload();\r\n            //$('#kt_form_user').selectpicker('refresh'); \r\n\r\n\r\n            //datatable.load();\r\n\r\n            location.reload(true);\r\n        });\r\n\r\n    };\r\n\r\n    var addDateRangeSelector = function () {\r\n        $('#DateRange').daterangepicker({\r\n            opens: 'left',\r\n            startDate: moment().subtract(3, \"days\"),\r\n            endDate: moment(),\r\n            locale: {\r\n                format: 'DD-MM-YYYY'\r\n            }\r\n        }, function (start, end, label) {\r\n            datatable.search(start.format('YYYY-MM-DD 00:00:00') + ';' + end.format('YYYY-MM-DD 23:59:59'), 'daterange');\r\n        });\r\n    }\r\n\r\n\t// selection\r\n    var selection = function () {\r\n        // init form controls\r\n        //$('#kt_form_status, #kt_form_type').selectpicker();\r\n        $('#kt_form_user, #kt_form_error_type').selectpicker();\r\n        $('#kt_form_task, #kt_form_error_type').selectpicker();\r\n    };\r\n\r\n\treturn {\r\n\t\t// public functions\r\n\t\tinit: function() {\r\n\t\t\tinit();\r\n\t\t\tsearch();\r\n\t\t\tselection();\r\n\t\t}\r\n\t};\r\n}();\r\n\r\n// On document ready\r\nKTUtil.ready(function() {\r\n\tKTLogsListDatatable.init();\r\n});\n\n//# sourceURL=webpack:///../src/assets/js/pages/custom/logs/list-datatable.js?");

/***/ })

/******/ });