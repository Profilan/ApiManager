// Class definition
var KTUsersEdit = function () {
    // Base elements
    var formEl;
    var validator;

    var initValidation = function () {
        validator = formEl.validate({
            // Validate only visible fields
            ignore: ":hidden",

            // Validation rules
            rules: {
                
                Username: {
                    required: true
                },
                DisplayName: {
                    required: true
                },
                Email: {
                    required: true,
                    email: true
                },
                Apikey: {
                    required: true
                },
                Role: {
                    required: true
                },
                AllowedIP: {
                    required: true
                }
            },

            // Validation messages
            messages: {
                'account_communication[]': {
                    required: 'You must select at least one communication option'
                },
                accept: {
                    required: "You must accept the Terms and Conditions agreement!"
                }
            },

            // Display error  
            invalidHandler: function (event, validator) {
                KTUtil.scrollTop();

                swal.fire({
                    "title": "",
                    "text": "An error was encountered while saving the user.",
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
                    url: '/api/user/edit',
                    success: function () {
                        KTApp.unprogress(btn);
                        //KTApp.unblock(formEl);

                        swal.fire({
                            "title": "",
                            "text": "User saved.",
                            "type": "success",
                            "confirmButtonClass": "btn btn-secondary"
                        });
                    }
                });
            }
        });
    };

    var initApikeyGenerator = function () {
        var btn = $('#generate-apikey-btn');

        btn.on('click', function (e) {
            e.preventDefault();

            sarr = new Array("abcdefghijkmnopqrstuvwxyz", "ABCDEFGHJKLMNPQRSTUVWXYZ",
                "23456789", "~!#$%^&*()_+-=\[]{};:,./<>?");
            s = new String();
            pw = new String();
            s = sarr[0] + sarr[1] + sarr[2];
            for (var i = 0; i < 40; i++) {
                pw += s.charAt(Math.floor(Math.random() * s.length));
            }
            $('#Apikey').val(pw);
        });
    };

    return {
        // public functions
        init: function() {
            formEl = $('#kt_user_form');

            initValidation();
            initSubmit();
            initApikeyGenerator();
        }
    };
}();

jQuery(document).ready(function() {    
    KTUsersEdit.init();
});