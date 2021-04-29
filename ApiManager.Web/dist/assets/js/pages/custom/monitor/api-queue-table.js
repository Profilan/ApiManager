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
/******/ 	return __webpack_require__(__webpack_require__.s = "../src/assets/js/pages/custom/monitor/api-queue-table.js");
/******/ })
/************************************************************************/
/******/ ({

/***/ "../src/assets/js/pages/custom/monitor/api-queue-table.js":
/*!****************************************************************!*\
  !*** ../src/assets/js/pages/custom/monitor/api-queue-table.js ***!
  \****************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
eval("﻿\r\n\r\nvar ApiQueueDatatable = function () {\r\n\r\n    // variables\r\n    var datatable;\r\n\r\n    // init\r\n    var init = function () {\r\n        var queryString = window.location.search;\r\n        var urlParams = new URLSearchParams(queryString);\r\n        var taskId = urlParams.get('taskId');\r\n\r\n        datatable = $('#api_queue_list').KTDatatable({\r\n            // datasource definition\r\n            data: {\r\n                type: 'remote',\r\n                source: {\r\n                    read: {\r\n                        url: '/api/monitor/api-queue',\r\n                        params: {\r\n                            'taskId': taskId\r\n                        }\r\n                    }\r\n                }\r\n            },\r\n\r\n            // layout definition\r\n            layout: {\r\n                scroll: true,\r\n                footer: false\r\n            },\r\n\r\n            sortable: false,\r\n\r\n            pagination: false,\r\n\r\n            // coluns definition\r\n            columns: [\r\n                {\r\n                    field: 'key',\r\n                    title: 'Key'\r\n                },\r\n                {\r\n                    field: 'title',\r\n                    title: 'Task'\r\n                },\r\n                {\r\n                    field: 'created',\r\n                    title: 'Creation Date'\r\n                },\r\n                {\r\n                    field: 'try_count',\r\n                    title: 'Try Count'\r\n                },\r\n                {\r\n                    field: 'actions',\r\n                    title: 'Actions',\r\n                    width: 130,\r\n                    overflow: 'visible',\r\n                    textAlign: 'center',\r\n                    template: function (row, index, datatable) {\r\n                        return '<a href=\"/api/queue/delete/' + row.id + '\" class=\"btn-delete btn btn-hover-brand btn-icon btn-pill\" title=\"Delete\">\\\r\n                                    <i class=\"la la-trash\"></i>\\\r\n                                </a>';\r\n\r\n                    }\r\n                }\r\n            ]\r\n        }).on('kt-datatable--on-layout-updated', function (e, options) {\r\n\r\n            var totalRows = $('#api_queue_list tbody > tr').length;\r\n\r\n            $('.queue-total').text(totalRows);\r\n            \r\n            $('.btn-delete').click(function (event) {\r\n                event.preventDefault();\r\n\r\n                console.log(this);\r\n\r\n                var btn = $(this);\r\n\r\n                var url = btn.attr('href');\r\n\r\n                $('#deleteModal .btn-ok').data('url', url);\r\n\r\n                $('#deleteModal').modal('show');\r\n            });\r\n        });\r\n\r\n        $('#deleteModal .btn-ok').click(function (event) {\r\n            event.preventDefault();\r\n\r\n            var url = $(this).data('url');\r\n\r\n            $.post(url, function () {\r\n                datatable.reload();\r\n                $('#deleteModal').modal('hide');\r\n\r\n                Swal.fire(\"Queue item deleted!\");\r\n            });\r\n        });\r\n\r\n        setInterval(refresh, 10 * 1000);\r\n    };\r\n\r\n    var refresh = function () {\r\n        // console.log('Refresh');\r\n        datatable.reload();\r\n        $('.queue-total').text(datatable.getTotalRows());\r\n    }\r\n\r\n    return {\r\n        init: function () {\r\n            init();\r\n        }\r\n    };\r\n\r\n}();\r\n\r\nKTUtil.ready(function () {\r\n    ApiQueueDatatable.init();\r\n});\n\n//# sourceURL=webpack:///../src/assets/js/pages/custom/monitor/api-queue-table.js?");

/***/ })

/******/ });