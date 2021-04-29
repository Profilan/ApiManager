// Class definition
var KTServicesAdd = function () {
    // Base elements
    var formEl;
    var validator;
    
    var showHtpasswdSection = function (el) {
        if (el.val() == 1) {
            $('#HtpasswdSection').show();
        } else {
            $('#HtpasswdSection').hide();
        }
    };

    var initValidation = function () {
        
        validator = formEl.validate({
            // Validate only visible fields
            ignore: ":hidden",

            // Validation rules
            rules: {

                Code: {
                    required: true
                },
                HashingId: {
                    required: true
                }
            },

            // Validation messages
            messages: {
                'HashingId': {
                    required: 'You must select at least one hashing option'
                }
            },

            // Display error  
            invalidHandler: function (event, validator) {
                KTUtil.scrollTop();

                swal.fire({
                    "title": "",
                    "text": "An error was encountered while saving the service.",
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
                    url: '/api/service/create',
                    success: function () {
                        KTApp.unprogress(btn);
                        //KTApp.unblock(formEl);

                        swal.fire({
                            "title": "",
                            "text": "Service saved.",
                            "type": "success",
                            "confirmButtonClass": "btn btn-secondary"
                        }).then((result) => {
                            if (result.value) {
                                window.location = '/service';
                            }
                        });
                    }
                });
            }
        });
    };

    var initSections = function () {

        showHtpasswdSection($('#HashingId'));
        $("#HashingId").on('change', function (e) {
            showHtpasswdSection($(this));
        });
    };

    return {
        // public functions
        init: function() {
            formEl = $('#kt_service_form');

            initValidation();
            initSubmit();
            initSections();
        }
    };
}();

jQuery(document).ready(function() {    
    KTServicesAdd.init();
});