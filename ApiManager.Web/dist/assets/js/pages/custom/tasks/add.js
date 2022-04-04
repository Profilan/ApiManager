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
/******/ 	return __webpack_require__(__webpack_require__.s = "../src/assets/js/pages/custom/tasks/add.js");
/******/ })
/************************************************************************/
/******/ ({

/***/ "../src/assets/js/pages/custom/tasks/add.js":
/*!**************************************************!*\
  !*** ../src/assets/js/pages/custom/tasks/add.js ***!
  \**************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

eval("// Class definition\r\nvar KTTasksEdit = function () {\r\n    // Base elements\r\n    var formEl;\r\n    var validator;\r\n\r\n    var initValidation = function () {\r\n        validator = formEl.validate({\r\n            // Validate only visible fields\r\n            ignore: \":hidden\",\r\n\r\n            // Validation rules\r\n            rules: {\r\n\r\n                Title: {\r\n                    required: true\r\n                },\r\n            },\r\n\r\n            // Validation messages\r\n            messages: {\r\n                'ServiceId': {\r\n                    required: 'You must select at least one service'\r\n                }\r\n            },\r\n\r\n            // Display error  \r\n            invalidHandler: function (event, validator) {\r\n                KTUtil.scrollTop();\r\n\r\n                swal.fire({\r\n                    \"title\": \"\",\r\n                    \"text\": \"An error was encountered while saving the task.\",\r\n                    \"type\": \"error\",\r\n                    \"confirmButtonClass\": \"btn btn-secondary m-btn m-btn--wide\"\r\n                });\r\n            },\r\n\r\n            // Submit valid form\r\n            submitHandler: function (form) {\r\n\r\n            }\r\n        });\r\n    };\r\n\r\n    var showAuthSection = function (el) {\r\n        if (el.val() == 1 || el.val() == 2 || el.val() == 3 || el.val() == 4) {\r\n            $('#basicSection').show();\r\n        } else {\r\n            $('#basicSection').hide();\r\n        }\r\n        if (el.val() == 2) {\r\n            $('#oAuthSection').show();\r\n        } else {\r\n            $('#oAuthSection').hide();\r\n        }\r\n        if (el.val() == 4) {\r\n            $('#apiKeySection').show();\r\n        } else {\r\n            $('#apiKeySection').hide();\r\n        }\r\n    };\r\n\r\n    var showApiTypeSection = function (el) {\r\n        if (el.val() == 0) {\r\n            $('#restSection').hide();\r\n            $('#graphqlSection').hide();\r\n        }\r\n        if (el.val() == 1) { // Rest\r\n            $('#restSection').show();\r\n        } else {\r\n            $('#restSection').hide();\r\n        }\r\n\r\n        if (el.val() == 2) { // GraphQL\r\n            $('#graphqlSection').show();\r\n        } else {\r\n            $('#graphqlSection').hide();\r\n        }\r\n    };\r\n\r\n    var showTab = function (el) {\r\n        $('#apiTab').parent().addClass('d-none');\r\n        $('#ftpTab').parent().addClass('d-none');\r\n        $('#diskTab').parent().addClass('d-none');\r\n        $('#mailTab').parent().addClass('d-none');\r\n        $('#authenticationTab').parent().removeClass('d-none');\r\n        if (el.val() == 1) {\r\n            $('#apiTab').parent().removeClass('d-none');\r\n        } else {\r\n            $('#apiTab').parent().addClass('d-none');\r\n        }\r\n        if (el.val() == 2) {\r\n            $('#ftpTab').parent().removeClass('d-none');\r\n        } else {\r\n            $('#ftpTab').parent().addClass('d-none');\r\n        }\r\n        if (el.val() == 3) {\r\n            $('#diskTab').parent().removeClass('d-none');\r\n        } else {\r\n            $('#diskTab').parent().addClass('d-none');\r\n        }\r\n        if (el.val() == 4) {\r\n            $('#mailTab').parent().removeClass('d-none');\r\n        } else {\r\n            $('#mailTab').parent().addClass('d-none');\r\n        }\r\n        if (el.val() == 5) {\r\n            $('#apiTab').parent().removeClass('d-none');\r\n            $('#diskTab').parent().removeClass('d-none');\r\n            $('#mailTab').parent().removeClass('d-none');\r\n        } else {\r\n            if (el.val() != 1) {\r\n                $('#apiTab').parent().addClass('d-none');\r\n            }\r\n            $('#diskTab').parent().addClass('d-none');\r\n            $('#mailTab').parent().addClass('d-none');\r\n        }\r\n    };\r\n\r\n    var initTabs = function () {\r\n        showAuthSection($('#AuthenticationViewModel_AuthenticationType'));\r\n        showTab($('#TaskType'));\r\n        showApiTypeSection($('#ApiViewModel_ApiType'));\r\n\r\n        $('#AuthenticationViewModel_AuthenticationType').change(function (e) {\r\n            showAuthSection($(this));\r\n        });\r\n\r\n        $('#TaskType').change(function (e) {\r\n            showTab($(this));\r\n        });\r\n\r\n        $('#ApiViewModel_ApiType').change(function (e) {\r\n            showApiTypeSection($(this));\r\n        });\r\n\r\n        // Schedule tab\r\n        $('input:radio[name=ScheduleType]').change(function () {\r\n            if ($(this).is(':checked')) {\r\n                if ($(this).val() == 1) { // Daily\r\n                    $('#daysSection').hide();\r\n                    $('#recurrenceType').text('days');\r\n                }\r\n                if ($(this).val() == 2) { // Weekly\r\n                    $('#daysSection').show();\r\n                    $('#recurrenceType').text('weeks');\r\n                }\r\n            }\r\n        });\r\n    };\r\n\r\n    var initSubmit = function () {\r\n        var btn = formEl.find('[data-ktwizard-action=\"action-submit\"]');\r\n\r\n        btn.on('click', function (e) {\r\n            e.preventDefault();\r\n\r\n            if (validator.form()) {\r\n                // See: src\\js\\framework\\base\\app.js\r\n                KTApp.progress(btn);\r\n                //KTApp.block(formEl);\r\n\r\n                // See: http://malsup.com/jquery/form/#ajaxSubmit\r\n                formEl.ajaxSubmit({\r\n                    url: '/api/task/create',\r\n                    success: function () {\r\n                        KTApp.unprogress(btn);\r\n                        //KTApp.unblock(formEl);\r\n\r\n                        swal.fire({\r\n                            \"title\": \"\",\r\n                            \"text\": \"Task saved.\",\r\n                            \"type\": \"success\",\r\n                            \"confirmButtonClass\": \"btn btn-secondary\"\r\n                        }).then((result) => {\r\n                            if (result.value) {\r\n                                window.location = '/task';\r\n                            }\r\n                        });\r\n                    }\r\n                });\r\n            }\r\n        });\r\n    };\r\n\r\n    var initRepeaters = function () {\r\n\r\n        $('#HeaderRepeater').repeater({\r\n            show: function () {\r\n                $(this).slideDown();\r\n            },\r\n            // Enable the option below to have a 2-step remove button\r\n            /*\r\n            hide: function (deleteElement) {\r\n                if(confirm('Are you sure you want to delete this element?')) {\r\n                    $(this).slideUp(deleteElement);\r\n                }\r\n            },\r\n            */\r\n            isFirstItemUndeletable: true\r\n        });\r\n\r\n    };\r\n\r\n    return {\r\n        // public functions\r\n        init: function () {\r\n            formEl = $('#kt_task_form');\r\n\r\n            initTabs();\r\n            initRepeaters();\r\n            initValidation();\r\n            initSubmit();\r\n        }\r\n    };\r\n}();\r\n\r\njQuery(document).ready(function () {\r\n    KTTasksEdit.init();\r\n});\n\n//# sourceURL=webpack:///../src/assets/js/pages/custom/tasks/add.js?");

/***/ })

/******/ });