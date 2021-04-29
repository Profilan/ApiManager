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
/******/ 	return __webpack_require__(__webpack_require__.s = "../src/assets/js/pages/custom/shares/list-datatable.js");
/******/ })
/************************************************************************/
/******/ ({

/***/ "../src/assets/js/pages/custom/shares/list-datatable.js":
/*!**************************************************************!*\
  !*** ../src/assets/js/pages/custom/shares/list-datatable.js ***!
  \**************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
eval("\r\n// Class definition\r\n\r\nvar KTSharesListDatatable = function() {\r\n\r\n\t// variables\r\n\tvar datatable;\r\n\r\n\t// init\r\n    var init = function () {\r\n        // init the datatables. Learn more: https://keenthemes.com/keen/?page=docs&section=datatable\r\n        datatable = $('#kt_share_list').KTDatatable({\r\n            // datasource definition\r\n            data: {\r\n                type: 'remote',\r\n                source: {\r\n                    read: {\r\n                        url: '/api/share'\r\n                    }\r\n                },\r\n                pageSize: 10, // display 20 records per page\r\n                serverPaging: true,\r\n                serverFiltering: true,\r\n                serverSorting: true,\r\n            },\r\n\r\n            // layout definition\r\n            layout: {\r\n                scroll: false, // enable/disable datatable scroll both horizontal and vertical when needed.\r\n                footer: false // display/hide footer\r\n            },\r\n\r\n            // column sorting\r\n            sortable: true,\r\n\r\n            pagination: true,\r\n\r\n            search: {\r\n                input: $('#generalSearch'),\r\n                delay: 400\r\n            },\r\n\r\n            // columns definition\r\n            columns: [{\r\n                field: \"name\",\r\n                title: \"Name\"\r\n            }, {\r\n                field: \"unc_path\",\r\n                title: \"UNC Path\"\r\n            }, {\r\n                field: 'monitor_inactivity',\r\n                title: 'Monitor Inactivity'\r\n            }, {\r\n                field: \"Actions\",\r\n                width: 80,\r\n                title: \"Actions\",\r\n                sortable: false,\r\n                autoHide: false,\r\n                overflow: 'visible',\r\n                template: function (row) {\r\n                    return '\\\r\n                            <a href=\"/share/edit/' + row.id + '\" class=\"btn btn-sm btn-clean btn-icon btn-icon-sm\" title=\"Edit Details\">\\\r\n\t\t\t\t\t\t\t    <i class=\"flaticon2-paper\"></i>\\\r\n\t\t\t\t\t\t\t</a>\\\r\n                            <a href=\"/share/delete/' + row.id + '\" class=\"btn btn-sm btn-clean btn-icon btn-icon-sm\" title=\"Delete\">\\\r\n\t\t\t\t\t\t\t    <i class=\"flaticon2-trash\"></i>\\\r\n\t\t\t\t\t\t\t</a>\\\r\n                        ';\r\n                }\r\n            }]\r\n        });\r\n    };\r\n\r\n\t// search\r\n    var search = function () {\r\n        \r\n    };\r\n\r\n\r\n\treturn {\r\n\t\t// public functions\r\n\t\tinit: function() {\r\n\t\t\tinit();\r\n\t\t\tsearch();\r\n\t\t}\r\n\t};\r\n}();\r\n\r\n// On document ready\r\nKTUtil.ready(function() {\r\n\tKTSharesListDatatable.init();\r\n});\n\n//# sourceURL=webpack:///../src/assets/js/pages/custom/shares/list-datatable.js?");

/***/ })

/******/ });