// Class definition
var ScheduleEdit = function () {
    // Base elements
    var validator;
    var formEl;

    var _init = function () {
        $('#Type').change(function (e) {
            var type = $(this).val();
            
            refresh(type);
        });

        refresh($('#Type').val());

    };


    var initValidation = function () {
        validator = formEl.validate({
            // Validate only visible fields
            ignore: ":hidden",

            // Validation rules
            rules: {

                StartBoundery: {
                    required: true
                }            },

            // Validation messages
            messages: {
                'StartBoundery': {
                    required: 'Start is required'
                }
            },

            // Display error  
            invalidHandler: function (event, validator) {
                KTUtil.scrollTop();

                swal.fire({
                    "title": "",
                    "text": "An error was encountered while saving the schedule.",
                    "type": "error",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                });
            },

            // Submit valid form
            submitHandler: function (form) {

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
                    url: '/api/schedule/edit',
                    success: function () {
                        KTApp.unprogress(btn);
                        //KTApp.unblock(formEl);

                        swal.fire({
                            "title": "",
                            "text": "Schedule saved.",
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

    var refresh = function (type) {

        console.log(type);
        switch (type) {
            case '0': // Once
                $('#schedule-section').show();
                $('#daysSection').hide();
                $('#recurrenceSection').hide();
                break;
            case '1': // Daily
                $('#schedule-section').show();
                $('#recurrenceSection').show();
                $('#recurrenceType').text('days');
                $('#daysSection').hide();
                break;
            case '2': // Weekly
                $('#schedule-section').show();
                $('#recurrenceSection').show();
                $('#recurrenceType').text('weeks on:');
                $('#daysSection').show();
                break;
        }
    };

    return {
        // public functions
        init: function () {
            formEl = $('#schedule_form');

            _init();
            initValidation();
            initSubmit();
        }
    };
}();

jQuery(document).ready(function () {
    ScheduleEdit.init();
});