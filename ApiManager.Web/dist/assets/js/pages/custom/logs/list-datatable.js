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
eval("\r\n// Class definition\r\n\r\nvar KTLogsListDatatable = function() {\r\n\r\n\t// variables\r\n    var datatable;\r\n    var currentStartDate;\r\n    var currentEndDate;\r\n    var arrows;\r\n    if (KTUtil.isRTL()) {\r\n        arrows = {\r\n            leftArrow: '<i class=\"la la-angle-right\"></i>',\r\n            rightArrow: '<i class=\"la la-angle-left\"></i>'\r\n        };\r\n    } else {\r\n        arrows = {\r\n            leftArrow: '<i class=\"la la-angle-left\"></i>',\r\n            rightArrow: '<i class=\"la la-angle-right\"></i>'\r\n        };\r\n    }\r\n\r\n\t// init\r\n    var init = function () {\r\n        // init the datatables. Learn more: https://keenthemes.com/keen/?page=docs&section=datatable\r\n        datatable = $('#kt_apps_log_list_datatable').KTDatatable({\r\n            // datasource definition\r\n            data: {\r\n                type: 'remote',\r\n                source: {\r\n                    read: {\r\n                        url: '/api/log',\r\n                        timeout: 300000\r\n                    }\r\n                },\r\n                pageSize: 10, // display 20 records per page\r\n                serverPaging: true,\r\n                serverFiltering: true,\r\n                serverSorting: true\r\n            },\r\n\r\n            // layout definition\r\n            layout: {\r\n                scroll: false, // enable/disable datatable scroll both horizontal and vertical when needed.\r\n                footer: false // display/hide footer\r\n            },\r\n\r\n            // column sorting\r\n            sortable: true,\r\n\r\n            pagination: true,\r\n\r\n            search: {\r\n                input: $('#generalSearch'),\r\n                delay: 400\r\n            },\r\n\r\n            // columns definition\r\n            columns: [{\r\n                field: \"timestamp\",\r\n                title: \"Date\",\r\n                type: 'date',\r\n                format: 'DD-MM-YYYY hh:mm:ss'\r\n            }, {\r\n                field: 'message',\r\n                title: 'Message'\r\n            }, {\r\n                field: 'status',\r\n                title: 'Status',\r\n                    template: function (row) {\r\n                        var color = \"green\";\r\n                        if (row.priority_name === \"ERR\") {\r\n                            color = \"red\";\r\n                        }\r\n\r\n                        return '<span style=\"color: ' + color + ';\">' + row.priority_name + '</span>';\r\n                    } \r\n            }, {\r\n                field: 'url',\r\n                title: 'Destination'\r\n            }, {\r\n                field: 'duration',\r\n                title: 'Duration (ms)'\r\n            }, {\r\n                field: \"Actions\",\r\n                width: 80,\r\n                title: \"Actions\",\r\n                sortable: false,\r\n                autoHide: false,\r\n                overflow: 'visible',\r\n                template: function (row) {\r\n                    return '\\\r\n\t\t\t\t\t\t\t<div class=\"dropdown\">\\\r\n\t\t\t\t\t\t\t\t<a href=\"javascript:;\" class=\"btn btn-sm btn-clean btn-icon btn-icon-md\" data-toggle=\"dropdown\">\\\r\n\t\t\t\t\t\t\t\t\t<i class=\"flaticon-more-1\"></i>\\\r\n\t\t\t\t\t\t\t\t</a>\\\r\n\t\t\t\t\t\t\t\t<div class=\"dropdown-menu dropdown-menu-right\">\\\r\n\t\t\t\t\t\t\t\t\t<ul class=\"kt-nav\">\\\r\n\t\t\t\t\t\t\t\t\t\t<li class=\"kt-nav__item\">\\\r\n\t\t\t\t\t\t\t\t\t\t\t<a href=\"/log/details/' + row.id + '\" class=\"kt-nav__link\">\\\r\n\t\t\t\t\t\t\t\t\t\t\t\t<i class=\"kt-nav__link-icon flaticon2-expand\"></i>\\\r\n\t\t\t\t\t\t\t\t\t\t\t\t<span class=\"kt-nav__link-text\">Details</span>\\\r\n\t\t\t\t\t\t\t\t\t\t\t</a>\\\r\n\t\t\t\t\t\t\t\t\t\t</li>\\\r\n\t\t\t\t\t\t\t\t\t</ul>\\\r\n\t\t\t\t\t\t\t\t</div>\\\r\n\t\t\t\t\t\t\t</div>\\\r\n\t\t\t\t\t\t';\r\n                }\r\n            }]\r\n        });\r\n    };\r\n\r\n    var dateRangeValid = function () {\r\n        var startDate = new Date($(\"#kt_form_start_date\").val());\r\n        var endDate = new Date($(\"#kt_form_end_date\").val());\r\n\r\n        if (startDate >= endDate) {\r\n            return false;\r\n        } else {\r\n            return true;\r\n        }\r\n    };\r\n\r\n\t// search\r\n    var search = function () {\r\n        $('#kt_form_user').on('change', function () {\r\n            datatable.search($(this).val().toLowerCase(), 'UserId');\r\n        });\r\n\r\n        $('#kt_form_error_type').on('change', function () {\r\n            datatable.search($(this).val().toLowerCase(), 'Type');\r\n        });\r\n\r\n\r\n        $(\"#kt_form_start_date\").change(function (e) {\r\n            if (dateRangeValid()) {\r\n                currentStartDate = $(this).val();\r\n                //datatable.search($(\"#kt_form_start_date\").val().toLowerCase(), 'StartDate');\r\n                //datatable.search($(\"#kt_form_end_date\").val().toLowerCase(), 'EndDate');\r\n                datatable.setDataSourceParam('StartDate', $(\"#kt_form_start_date\").val().toLowerCase());\r\n                datatable.setDataSourceParam('EndDate', $(\"#kt_form_end_date\").val().toLowerCase());\r\n                datatable.load();\r\n                \r\n            } else {\r\n                $(\"#kt_form_start_date\").val(currentStartDate);\r\n                swal.fire({\r\n                    \"title\": \"\",\r\n                    \"text\": \"Start Date should be less than End Date.\",\r\n                    \"type\": \"error\",\r\n                    \"confirmButtonClass\": \"btn btn-secondary m-btn m-btn--wide\"\r\n                });\r\n            }\r\n        });\r\n\r\n        $(\"#kt_form_end_date\").change(function (e) {\r\n            if (dateRangeValid()) {\r\n                currentEndDate = $(this).val();\r\n                datatable.setDataSourceParam('StartDate', $(\"#kt_form_start_date\").val().toLowerCase());\r\n                datatable.setDataSourceParam('EndDate', $(\"#kt_form_end_date\").val().toLowerCase());\r\n                datatable.load();\r\n                \r\n            } else {\r\n                $(\"#kt_form_end_date\").val(currentEndDate);\r\n                swal.fire({\r\n                    \"title\": \"\",\r\n                    \"text\": \"End Date should be greater than Start Date.\",\r\n                    \"type\": \"error\",\r\n                    \"confirmButtonClass\": \"btn btn-secondary m-btn m-btn--wide\"\r\n                });\r\n            }\r\n        });\r\n\r\n    };\r\n\r\n\t// selection\r\n    var selection = function () {\r\n        // init form controls\r\n        //$('#kt_form_status, #kt_form_type').selectpicker();\r\n        $('#kt_form_user, #kt_form_error_type').selectpicker();\r\n\r\n        $('#kt_form_start_date, #kt_form_end_date').datepicker({\r\n            rtl: KTUtil.isRTL(),\r\n            todayHighlight: true,\r\n            orientation: \"bottom left\",\r\n            templates: arrows,\r\n            format: 'dd-mm-yyyy'\r\n        });\r\n\r\n        currentStartDate = $(\"#kt_form_start_date\").val();\r\n        currentEndDate = $(\"#kt_form_end_date\").val();\r\n\r\n    };\r\n\r\n\treturn {\r\n\t\t// public functions\r\n\t\tinit: function() {\r\n\t\t\tinit();\r\n\t\t\tsearch();\r\n\t\t\tselection();\r\n\t\t}\r\n\t};\r\n}();\r\n\r\n// On document ready\r\nKTUtil.ready(function() {\r\n\tKTLogsListDatatable.init();\r\n});\n\n//# sourceURL=webpack:///../src/assets/js/pages/custom/logs/list-datatable.js?");

/***/ })

/******/ });