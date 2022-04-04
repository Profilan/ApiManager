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

    var showApiTypeSection = function (el) {
        if (el.val() == 0) {
            $('#restSection').hide();
            $('#graphqlSection').hide();
        }
        if (el.val() == 1) { // Rest
            $('#restSection').show();
        } else {
            $('#restSection').hide();
        }

        if (el.val() == 2) { // GraphQL
            $('#graphqlSection').show();
        } else {
            $('#graphqlSection').hide();
        }
    };

    var showTab = function (el) {
        $('#apiTab').parent().addClass('d-none');
        $('#ftpTab').parent().addClass('d-none');
        $('#diskTab').parent().addClass('d-none');
        $('#mailTab').parent().addClass('d-none');
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
            $('#apiTab').parent().removeClass('d-none');
            $('#diskTab').parent().removeClass('d-none');
            $('#mailTab').parent().removeClass('d-none');
        } else {
            if (el.val() != 1) {
                $('#apiTab').parent().addClass('d-none');
            }
            $('#diskTab').parent().addClass('d-none');
            $('#mailTab').parent().addClass('d-none');
        }
    };

    var initTabs = function () {
        showAuthSection($('#AuthenticationViewModel_AuthenticationType'));
        showTab($('#TaskType'));
        showApiTypeSection($('#ApiViewModel_ApiType'));

        $('#AuthenticationViewModel_AuthenticationType').change(function (e) {
            showAuthSection($(this));
        });

        $('#TaskType').change(function (e) {
            showTab($(this));
        });

        $('#ApiViewModel_ApiType').change(function (e) {
            showApiTypeSection($(this));
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
                    url: '/api/task/create',
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

    };

    return {
        // public functions
        init: function () {
            formEl = $('#kt_task_form');

            initTabs();
            initRepeaters();
            initValidation();
            initSubmit();
        }
    };
}();

jQuery(document).ready(function () {
    KTTasksEdit.init();
});