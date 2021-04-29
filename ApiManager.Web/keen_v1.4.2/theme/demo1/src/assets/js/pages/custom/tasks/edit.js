// Class definition
var KTTasksEdit = function () {
    // Base elements
    var formEl;
    var validator;

    var initValidation = function () {
        validator = formEl.validate({
            // Validate only visible fields
            ignore: ":hidden",

            // Validation rules
            rules: {
                
                Title: {
                    required: true
                },
            },

            // Validation messages
            messages: {
                'ServiceId': {
                    required: 'You must select at least one service'
                }
            },

            // Display error  
            invalidHandler: function (event, validator) {
                KTUtil.scrollTop();

                swal.fire({
                    "title": "",
                    "text": "An error was encountered while saving the task.",
                    "type": "error",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                });
            },

            // Submit valid form
            submitHandler: function (form) {

            }
        });
    };

    var showAuthSection = function (el) {
        if (el.val() == 1 || el.val() == 2 || el.val() == 3 || el.val() == 4) {
            $('#basicSection').show();
        } else {
            $('#basicSection').hide();
        }
        if (el.val() == 2) {
            $('#oAuthSection').show();
        } else {
            $('#oAuthSection').hide();
        }
        if (el.val() == 4) {
            $('#apiKeySection').show();
        } else {
            $('#apiKeySection').hide();
        }
    };

    var showReceiveAuthSection = function (el) {
        if (el.val() == 1 || el.val() == 2 || el.val() == 3 || el.val() == 4) {
            $('#basicReceiveSection').show();
        } else {
            $('#basicReceiveSection').hide();
        }
        if (el.val() == 2) {
            $('#oAuthReceiveSection').show();
        } else {
            $('#oAuthReceiveSection').hide();
        }
        if (el.val() == 4) {
            $('#apiKeyReceiveSection').show();
        } else {
            $('#apiKeyReceiveSection').hide();
        }
    };

    var showSendAuthSection = function (el) {
        if (el.val() == 1 || el.val() == 2 || el.val() == 3 || el.val() == 4) {
            $('#basicSendSection').show();
        } else {
            $('#basicSendSection').hide();
        }
        if (el.val() == 2) {
            $('#oAuthSendSection').show();
        } else {
            $('#oAuthSendSection').hide();
        }
        if (el.val() == 4) {
            $('#apiKeySendSection').show();
        } else {
            $('#apiKeySendSection').hide();
        }
    };

    var showReceiveApiTypeSection = function (el) {
        if (el.val() == 0) {
            $('#restReceiveSection').hide();
            $('#graphqlReceiveSection').hide();
        }
        if (el.val() == 1) { // Rest
            $('#restReceiveSection').show();
        } else {
            $('#restReceiveSection').hide();
        }

        if (el.val() == 2) { // GraphQL
            $('#graphqlReceiveSection').show();
        } else {
            $('#graphqlReceiveSection').hide();
        }
    };

    var showSendApiTypeSection = function (el) {
        if (el.val() == 0) {
            $('#restSendSection').hide();
            $('#graphqlSendSection').hide();
        }
        if (el.val() == 1) { // Rest
            $('#restSendSection').show();
        } else {
            $('#restSendSection').hide();
        }

        if (el.val() == 2) { // GraphQL
            $('#graphqlSendSection').show();
        } else {
            $('#graphqlSendSection').hide();
        }
    };

    var showTab = function (el) {
        $('#apiTab').parent().addClass('d-none');
        $('#ftpTab').parent().addClass('d-none');
        $('#diskTab').parent().addClass('d-none');
        $('#mailTab').parent().addClass('d-none');
        $('#receivesendTab').parent().addClass('d-none');
        $('#authenticationTab').parent().removeClass('d-none');
        if (el.val() == 1) {
            $('#apiTab').parent().removeClass('d-none');
        } else {
            $('#apiTab').parent().addClass('d-none');
        }
        if (el.val() == 2) {
            $('#ftpTab').parent().removeClass('d-none');
        } else {
            $('#ftpTab').parent().addClass('d-none');
        }
        if (el.val() == 3) {
            $('#diskTab').parent().removeClass('d-none');
        } else {
            $('#diskTab').parent().addClass('d-none');
        }
        if (el.val() == 4) {
            $('#mailTab').parent().removeClass('d-none');
        } else {
            $('#mailTab').parent().addClass('d-none');
        }
        if (el.val() == 5) {
            $('#receivesendTab').parent().removeClass('d-none');
            $('#authenticationTab').parent().addClass('d-none');
        } else {
            $('#receivesendTab').parent().addClass('d-none');
        }
    };

    var showReceiveTab = function (el) {
        console.log(el.val());
        $('#apiReceiveTab').parent().addClass('d-none');
        $('#ftpReceiveTab').parent().addClass('d-none');
        $('#diskReceiveTab').parent().addClass('d-none');
        $('#mailReceiveTab').parent().addClass('d-none');
        $('#sqlReceiveTab').parent().addClass('d-none');
        $('#authenticationReceiveTab').parent().removeClass('d-none');
        if (el.val() == 1) {
            $('#apiReceiveTab').parent().removeClass('d-none');
            $('#apiReceiveTab').tab('show');
        } else {
            $('#apiReceiveTab').parent().addClass('d-none');
        }
        if (el.val() == 2) {
            $('#ftpReceiveTab').parent().removeClass('d-none');
            $('#ftpReceiveTab').tab('show');
        } else {
            $('#ftpReceiveTab').parent().addClass('d-none');
        }
        if (el.val() == 3) {
            $('#diskReceiveTab').parent().removeClass('d-none');
            $('#diskReceiveTab').tab('show');
        } else {
            $('#diskReceiveTab').parent().addClass('d-none');
        }
        if (el.val() == 4) {
            $('#mailReceiveTab').parent().removeClass('d-none');
            $('#mailReceiveTab').tab('show');
        } else {
            $('#mailReceiveTab').parent().addClass('d-none');
        }
        if (el.val() == 5) {
            $('#sqlReceiveTab').parent().removeClass('d-none');
            $('#sqlReceiveTab').tab('show');
        } else {
            $('#sqlReceiveTab').parent().addClass('d-none');
        }
    };

    var showSendTab = function (el) {
        console.log(el.val());
        $('#apiSendTab').parent().addClass('d-none');
        $('#ftpSendTab').parent().addClass('d-none');
        $('#diskSendTab').parent().addClass('d-none');
        $('#mailSendTab').parent().addClass('d-none');
        $('#sqlSendTab').parent().addClass('d-none');
        $('#authenticationSendTab').parent().removeClass('d-none');
        if (el.val() == 1) {
            $('#apiSendTab').parent().removeClass('d-none');
            $('#apiSendTab').tab('show');
        } else {
            $('#apiSendTab').parent().addClass('d-none');
        }
        if (el.val() == 2) {
            $('#ftpSendTab').parent().removeClass('d-none');
            $('#ftpSendTab').tab('show');
        } else {
            $('#ftpSendTab').parent().addClass('d-none');
        }
        if (el.val() == 3) {
            $('#diskSendTab').parent().removeClass('d-none');
            $('#diskSendTab').tab('show');
        } else {
            $('#diskSendTab').parent().addClass('d-none');
        }
        if (el.val() == 4) {
            $('#mailSendTab').parent().removeClass('d-none');
            $('#mailSendTab').tab('show');
        } else {
            $('#mailSendTab').parent().addClass('d-none');
        }
        if (el.val() == 5) {
            $('#sqlSendTab').parent().removeClass('d-none');
            $('sqlSendTab').tab('show');
        } else {
            $('#sqlSendTab').parent().addClass('d-none');
        }
    };


    var initTabs = function () {
        showAuthSection($('#AuthenticationType'));
        showTab($('#TaskType'));

        $('#AuthenticationType').change(function (e) {
            showAuthSection($(this));
        });

        $('#TaskType').change(function (e) {
            showTab($(this));
        });

        // Receive tabs
        showReceiveAuthSection($('#ReceiverViewModel_AuthenticationViewModel_AuthenticationType'));
        showReceiveApiTypeSection($('#ReceiveApiType'));
        showReceiveTab($('#ReceiveType'));

        $('#ReceiverViewModel_AuthenticationViewModel_AuthenticationType').change(function (e) {
            showReceiveAuthSection($(this));
        });

        $('#ReceiverViewModel_ApiViewModel_ApiType').change(function (e) {
            showReceiveApiTypeSection($(this));
        });

        $('#ReceiverViewModel_Type').change(function (e) {
            showReceiveTab($(this));
        });

        // Send Tabs
        showSendAuthSection($('#SenderViewModel_AuthenticationViewModel_AuthenticationType'));
        showSendApiTypeSection($('#SendApiType'));
        showSendTab($('#SendType'));

        $('#SenderViewModel_AuthenticationViewModel_AuthenticationType').change(function (e) {
            showSendAuthSection($(this));
        });

        $('#SenderViewModel_ApiViewModel_ApiType').change(function (e) {
            showSendApiTypeSection($(this));
        });

        $('#SenderViewModel_Type').change(function (e) {
            showSendTab($(this));
        });

        // Schedule tab
        $('input:radio[name=ScheduleType]').change(function () {
            if ($(this).is(':checked')) {
                if ($(this).val() == 1) { // Daily
                    $('#daysSection').hide();
                    $('#recurrenceType').text('days');
                }
                if ($(this).val() == 2) { // Weekly
                    $('#daysSection').show();
                    $('#recurrenceType').text('weeks');
                }
            }
        });
    };

    var initSubmit = function () {
        var btn = formEl.find('[data-ktwizard-action="action-submit"]');

        btn.on('click', function (e) {
            e.preventDefault();

            if (validator.form()) {
                // See: src\js\framework\base\app.js
                KTApp.progress(btn);
                //KTApp.block(formEl);

                // See: http://malsup.com/jquery/form/#ajaxSubmit
                formEl.ajaxSubmit({
                    url: '/api/task/edit',
                    success: function () {
                        KTApp.unprogress(btn);
                        //KTApp.unblock(formEl);

                        swal.fire({
                            "title": "",
                            "text": "Task saved.",
                            "type": "success",
                            "confirmButtonClass": "btn btn-secondary"
                        }).then((result) => {
                            if (result.value) {
                                window.location = '/task';
                            }
                        });
                    }
                });
            }
        });
    };

    var initRepeaters = function () {

        $('#HeaderRepeater').repeater({
            show: function () {
                $(this).slideDown();
            },
            // Enable the option below to have a 2-step remove button
            /*
            hide: function (deleteElement) {
                if(confirm('Are you sure you want to delete this element?')) {
                    $(this).slideUp(deleteElement);
                }
            },
            */
            isFirstItemUndeletable: true
        });

        $('#ReceiveHeaderRepeater').repeater({
            show: function () {
                $(this).slideDown();
            },
            isFirstItemUndeletable: true
        });

        $('#SendHeaderRepeater').repeater({
            show: function () {
                $(this).slideDown();
            },
            isFirstItemUndeletable: true
        });

    };

    return {
        // public functions
        init: function() {
            formEl = $('#kt_task_form');

            initTabs();
            initRepeaters();
            initValidation();
            initSubmit();
        }
    };
}();

jQuery(document).ready(function() {    
    KTTasksEdit.init();
});