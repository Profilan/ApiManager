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
/******/ 	return __webpack_require__(__webpack_require__.s = "../src/assets/js/pages/custom/home/dashboard.js");
/******/ })
/************************************************************************/
/******/ ({

/***/ "../src/assets/js/pages/custom/home/dashboard.js":
/*!*******************************************************!*\
  !*** ../src/assets/js/pages/custom/home/dashboard.js ***!
  \*******************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

eval("﻿var APIDashboard = function () {\r\n\r\n    var period;\r\n    var top5UsersChart;\r\n    var top5UrlsChart;\r\n\r\n    var init = function () {\r\n        period = $('#Period').val();\r\n    };\r\n\r\n    var topFiveUrlsChart = function () {\r\n\r\n        var ctx = $('#top-five-urls-pie-chart');\r\n\r\n        if (ctx.length == 0) {\r\n            return;\r\n        }\r\n\r\n        var onClickUrl = function (e) {\r\n            window.location.href = \"/Url/Details/\" + e.dataPoint.id + \"?period=\";\r\n        };\r\n\r\n        $.get('/api/url/top5', { period: period }, function (chartData) {\r\n                        \r\n            top5UrlsChart = new Chart(ctx, {\r\n                type: 'pie',\r\n                data: chartData,\r\n                options: {\r\n                    responsive: true,\r\n                    legend: {\r\n                        position: 'right'\r\n                    },\r\n                    animation: {\r\n                        animateScale: true,\r\n                        animateRotate: true\r\n                    },\r\n                    onClick: (e) => {\r\n\r\n                    }\r\n                }\r\n            });\r\n        });\r\n       \r\n    };\r\n\r\n    var topFiveUsersChart = function () {\r\n\r\n        var ctx = $('#top-five-users-pie-chart');\r\n\r\n        if (ctx.length == 0) {\r\n            return;\r\n        }\r\n\r\n        $.get('/api/user/top5', { period: period }, function (chartData) {\r\n\r\n            top5UsersChart = new Chart(ctx, {\r\n                type: 'pie',\r\n                data: chartData,\r\n                options: {\r\n                    responsive: true,\r\n                    legend: {\r\n                        position: 'right'\r\n                    },\r\n                    animation: {\r\n                        animateScale: true,\r\n                        animateRotate: true\r\n                    }\r\n                }\r\n            });\r\n        });\r\n\r\n    };\r\n\r\n    var addLatestErrors = function (data) {\r\n        $('#latest-errors li').not('.prototype').remove();\r\n        $.each(data, function (key, value) {\r\n            var el = $('#latest-errors li.prototype').clone();\r\n            el.removeClass('prototype d-none');\r\n            el.find('span').text(value.url_name + \" (\" + value.error_count + \")\");\r\n            $('#latest-errors ul').append(el);\r\n        });\r\n\r\n        setTimeout(updateLatestErrors, 3000);\r\n    };\r\n\r\n\r\n    var updateLatestErrors = function () {\r\n        $.get('/api/log/latest-errors', { period: period }, addLatestErrors);\r\n    };\r\n\r\n   var latestErrors = function () {\r\n        var ctx = $('#latest-errors');\r\n\r\n\r\n        updateLatestErrors();\r\n    };\r\n\r\n    var addData = function (chart, data) {\r\n        chart.data.labels = data.labels;\r\n        chart.data.datasets = data.datasets;\r\n        chart.update();\r\n    };\r\n\r\n    var removeData = function (chart) {\r\n\r\n        chart.data.labels = [];\r\n        chart.data.datasets = [];\r\n        chart.update();\r\n    };\r\n\r\n    var search = function () {\r\n\r\n        $(\".period-button\").click(function (e) {\r\n            e.preventDefault();\r\n\r\n            var button = $(this);\r\n\r\n            $('.period-button').removeClass('btn-dark').addClass('btn-primary');\r\n            button.toggleClass('btn-dark btn-primary');\r\n\r\n            period = button.data('period');\r\n            $(\"#Period\").val(period);\r\n\r\n            removeData(top5UsersChart);\r\n            removeData(top5UrlsChart);\r\n            $('#latest-errors li').not('.prototype').remove();\r\n\r\n            $.get('/api/url/top5', { period: period }, function (chartData) {\r\n                addData(top5UrlsChart, chartData);\r\n            });\r\n\r\n            $.get('/api/user/top5', { period: period }, function (chartData) {\r\n                addData(top5UsersChart, chartData);\r\n            });\r\n\r\n            updateLatestErrors();\r\n\r\n            window.history.replaceState({ period: period }, \"Dashboard\", \"?period=\" + period);\r\n        });\r\n    };\r\n\r\n    return {\r\n        init: function () {\r\n            init();\r\n            search();\r\n            topFiveUrlsChart();\r\n            topFiveUsersChart();\r\n            latestErrors();\r\n        }\r\n    };\r\n\r\n}();\r\n\r\njQuery(document).ready(function () {\r\n    APIDashboard.init();\r\n});\n\n//# sourceURL=webpack:///../src/assets/js/pages/custom/home/dashboard.js?");

/***/ })

/******/ });