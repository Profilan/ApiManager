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
/******/ 	return __webpack_require__(__webpack_require__.s = "../src/assets/js/pages/custom/users/visits-table.js");
/******/ })
/************************************************************************/
/******/ ({

/***/ "../src/assets/js/pages/custom/users/visits-table.js":
/*!***********************************************************!*\
  !*** ../src/assets/js/pages/custom/users/visits-table.js ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
eval("﻿\r\n// Class definition\r\n\r\nvar KTUserVisitsDatatable = function () {\r\n\r\n    // variables\r\n    var datatable, userId, period;\r\n\r\n    // init\r\n    var init = function () {\r\n\r\n        // init the params\r\n        period = $(\"#Period\").val();\r\n        userId = $('#kt_form_user').val();\r\n\r\n        // init the datatables. Learn more: https://keenthemes.com/keen/?page=docs&section=datatable\r\n        datatable = $('#kt_user_visits_datatable').KTDatatable({\r\n            // datasource definition\r\n            data: {\r\n                type: 'remote',\r\n                source: {\r\n                    read: {\r\n                        url: '/api/user/visits',\r\n                        params: {\r\n                            userId: userId,\r\n                            period: period\r\n                        }\r\n                    }\r\n                },\r\n                pageSize: 10, // display 20 records per page\r\n                serverPaging: true,\r\n                serverFiltering: true,\r\n                serverSorting: true\r\n            },\r\n\r\n            // layout definition\r\n            layout: {\r\n                scroll: false, // enable/disable datatable scroll both horizontal and vertical when needed.\r\n                footer: false // display/hide footer\r\n            },\r\n\r\n            // column sorting\r\n            sortable: true,\r\n\r\n            pagination: true,\r\n\r\n            // columns definition\r\n            columns: [{\r\n                field: \"name\",\r\n                title: \"Url Name\"\r\n            }, {\r\n                field: 'latest_visit_date',\r\n                title: 'Latest Visit'\r\n            }, {\r\n                field: 'avg_duration',\r\n                title: 'Avg. Duration',\r\n                    template: function (row) {\r\n                        return (Math.round(row.avg_duration * 100) / 100).toFixed(2);\r\n                }\r\n            }, {\r\n                field: 'hits',\r\n                title: 'Visited #'\r\n            }, {\r\n                field: \"Actions\",\r\n                width: 80,\r\n                title: \"Actions\",\r\n                sortable: false,\r\n                autoHide: false,\r\n                    overflow: 'visible',\r\n\r\n\r\n                template: function (row) {\r\n                    return '<div class=\"dropdown\">\\\r\n                                <a href=\"#\" class=\"btn btn-hover-brand btn-icon btn-pill\" data-toggle=\"dropdown\">\\\r\n                                    <i class=\"la la-ellipsis-h\"></i>\\\r\n                                </a>\\\r\n                                <div class=\"dropdown-menu dropdown-menu-right\">\\\r\n                                    <a href=\"/Statistics/UrlVisits?urlId=' + row.id + '&period=' + $(\"#Period\").val() + '\" class=\"kt-nav__link\">\\\r\n\t\t\t\t\t\t\t\t\t\t\t<i class=\"kt-nav__link-icon flaticon2-contract\"></i>\\\r\n\t\t\t\t\t\t\t\t\t\t\t<span class=\"kt-nav__link-text\">Url Visits</span>\\\r\n\t\t\t\t\t\t\t\t\t</a>\\\r\n                                </div>\\\r\n                            </div>';\r\n                }\r\n            }]\r\n        });\r\n    };\r\n\r\n    // search\r\n    var search = function () {\r\n\r\n       \r\n\r\n        $('#kt_form_user').on('changed.bs.select', function (e, clickedIndex, isSelected, previousValue) {\r\n            \r\n\r\n            datatable.setDataSourceParam('userId', $(this).val());\r\n            datatable.load();\r\n\r\n            window.history.replaceState({ period: period }, \"User Visits\", \"?userId=\" + $(this).val() + \"&period=\" + $(\"#Period\").val());\r\n        });\r\n\r\n        $(\".period-button\").click(function (e) {\r\n            e.preventDefault();\r\n\r\n            var button = $(this);\r\n\r\n            $('.period-button').removeClass('btn-outline-primary').addClass('btn-primary');\r\n            button.toggleClass('btn-outline-primary btn-primary');\r\n            \r\n            var period = button.data('period');\r\n            $(\"#Period\").val(period);\r\n\r\n            datatable.setDataSourceParam('period', button.data('period'));\r\n            datatable.load();\r\n\r\n            window.history.replaceState({period: period}, \"User Visits\", \"?userId=\" + $('#kt_form_user').val() + \"&period=\" + period);\r\n        });\r\n    };\r\n\r\n    // selection\r\n    var selection = function () {\r\n        // init form controls\r\n        //$('#kt_form_status, #kt_form_type').selectpicker();\r\n        $('#kt_form_user').selectpicker();\r\n\r\n\r\n    };\r\n\r\n\r\n    return {\r\n        // public functions\r\n        init: function () {\r\n            init();\r\n            search();\r\n            selection();\r\n        }\r\n    };\r\n}();\r\n\r\n// On document ready\r\nKTUtil.ready(function () {\r\n    KTUserVisitsDatatable.init();\r\n});\n\n//# sourceURL=webpack:///../src/assets/js/pages/custom/users/visits-table.js?");

/***/ })

/******/ });