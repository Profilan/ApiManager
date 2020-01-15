"use strict";
// Class definition

var KTLogsListDatatable = function() {

	// variables
    var datatable;
    var currentStartDate;
    var currentEndDate;
    var arrows;
    if (KTUtil.isRTL()) {
        arrows = {
            leftArrow: '<i class="la la-angle-right"></i>',
            rightArrow: '<i class="la la-angle-left"></i>'
        };
    } else {
        arrows = {
            leftArrow: '<i class="la la-angle-left"></i>',
            rightArrow: '<i class="la la-angle-right"></i>'
        };
    }

	// init
    var init = function () {
        // init the datatables. Learn more: https://keenthemes.com/keen/?page=docs&section=datatable
        datatable = $('#kt_apps_log_list_datatable').KTDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: {
                    read: {
                        url: '/api/log',
                        timeout: 300000
                    }
                },
                pageSize: 10, // display 20 records per page
                serverPaging: true,
                serverFiltering: true,
                serverSorting: true
            },

            // layout definition
            layout: {
                scroll: false, // enable/disable datatable scroll both horizontal and vertical when needed.
                footer: false // display/hide footer
            },

            // column sorting
            sortable: true,

            pagination: true,

            search: {
                input: $('#generalSearch'),
                delay: 400
            },

            // columns definition
            columns: [{
                field: "timestamp",
                title: "Date",
                type: 'date',
                format: 'DD-MM-YYYY hh:mm:ss'
            }, {
                field: 'message',
                title: 'Message'
            }, {
                field: 'status',
                title: 'Status',
                    template: function (row) {
                        var color = "green";
                        if (row.priority_name === "ERR") {
                            color = "red";
                        }

                        return '<span style="color: ' + color + ';">' + row.priority_name + '</span>';
                    } 
            }, {
                field: 'url',
                title: 'Destination'
            }, {
                field: 'duration',
                title: 'Duration (ms)'
            }, {
                field: "Actions",
                width: 80,
                title: "Actions",
                sortable: false,
                autoHide: false,
                overflow: 'visible',
                template: function (row) {
                    return '\
							<div class="dropdown">\
								<a href="javascript:;" class="btn btn-sm btn-clean btn-icon btn-icon-md" data-toggle="dropdown">\
									<i class="flaticon-more-1"></i>\
								</a>\
								<div class="dropdown-menu dropdown-menu-right">\
									<ul class="kt-nav">\
										<li class="kt-nav__item">\
											<a href="/log/details/' + row.id + '" class="kt-nav__link">\
												<i class="kt-nav__link-icon flaticon2-expand"></i>\
												<span class="kt-nav__link-text">Details</span>\
											</a>\
										</li>\
									</ul>\
								</div>\
							</div>\
						';
                }
            }]
        });
    };

    var dateRangeValid = function () {
        var startDate = new Date($("#kt_form_start_date").val());
        var endDate = new Date($("#kt_form_end_date").val());

        if (startDate >= endDate) {
            return false;
        } else {
            return true;
        }
    };

	// search
    var search = function () {
        $('#kt_form_user').on('change', function () {
            datatable.search($(this).val().toLowerCase(), 'UserId');
        });

        $('#kt_form_error_type').on('change', function () {
            datatable.search($(this).val().toLowerCase(), 'Type');
        });


        $("#kt_form_start_date").change(function (e) {
            if (dateRangeValid()) {
                currentStartDate = $(this).val();
                //datatable.search($("#kt_form_start_date").val().toLowerCase(), 'StartDate');
                //datatable.search($("#kt_form_end_date").val().toLowerCase(), 'EndDate');
                datatable.setDataSourceParam('StartDate', $("#kt_form_start_date").val().toLowerCase());
                datatable.setDataSourceParam('EndDate', $("#kt_form_end_date").val().toLowerCase());
                datatable.load();
                
            } else {
                $("#kt_form_start_date").val(currentStartDate);
                swal.fire({
                    "title": "",
                    "text": "Start Date should be less than End Date.",
                    "type": "error",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                });
            }
        });

        $("#kt_form_end_date").change(function (e) {
            if (dateRangeValid()) {
                currentEndDate = $(this).val();
                datatable.setDataSourceParam('StartDate', $("#kt_form_start_date").val().toLowerCase());
                datatable.setDataSourceParam('EndDate', $("#kt_form_end_date").val().toLowerCase());
                datatable.load();
                
            } else {
                $("#kt_form_end_date").val(currentEndDate);
                swal.fire({
                    "title": "",
                    "text": "End Date should be greater than Start Date.",
                    "type": "error",
                    "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                });
            }
        });

    };

	// selection
    var selection = function () {
        // init form controls
        //$('#kt_form_status, #kt_form_type').selectpicker();
        $('#kt_form_user, #kt_form_error_type').selectpicker();

        $('#kt_form_start_date, #kt_form_end_date').datepicker({
            rtl: KTUtil.isRTL(),
            todayHighlight: true,
            orientation: "bottom left",
            templates: arrows,
            format: 'dd-mm-yyyy'
        });

        currentStartDate = $("#kt_form_start_date").val();
        currentEndDate = $("#kt_form_end_date").val();

    };

	return {
		// public functions
		init: function() {
			init();
			search();
			selection();
		}
	};
}();

// On document ready
KTUtil.ready(function() {
	KTLogsListDatatable.init();
});