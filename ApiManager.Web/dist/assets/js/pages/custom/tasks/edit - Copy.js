// Class definition
var KTUrlsEdit = function () {
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

    var showTab = function (el) {
        $('#apiTab').hide();
        $('#ftpTab').hide();
        $('#diskTab').hide();
        $('#mailTab').hide();
        if (el.val() == 1) {
            $('#apiTab').show();
        } else {
            $('#apiTab').hide();
        }
        if (el.val() == 2) {
            $('#ftpTab').show();
        } else {
            $('#ftpTab').hide();
        }
        if (el.val() == 3) {
            $('#diskTab').show();
        } else {
            $('#diskTab').hide();
        }
        if (el.val() == 4) {
            $('#mailTab').show();
        } else {
            $('#mailTab').hide();
        }
    };


    var initTabs = function () {
        showAuthSection($('#AuthenticationType'));
        showTab($('#TaskType'));

        $('#taskTabs a').click(function (e) {
            e.preventDefault();
            $(this).tab('show');
        });

        $('#AuthenticationType').change(function (e) {
            showAuthSection($(this));
        });

        $('#TaskType').change(function (e) {
            showTab($(this));
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
    KTUrlsEdit.init();
});