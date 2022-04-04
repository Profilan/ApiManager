"use strict";
// Class definition

var KTLogsListDatatable = function() {

	// variables
    var datatable;

    var datatableOptions = {
        // datasource definition
        data: {
            type: 'remote',
            source: {
                read: {
                    url: '/api/log',
                    timeout: 300000,
                }
            },
            pageSize: 20, // display 20 records per page
            serverPaging: true,
            serverFiltering: true,
            serverSorting: true,
            saveState: false,
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
            field: "TimeStamp",
            title: "Date",
            template: function (row) {
                return row.TimeStamp;
            }
        }, {
            field: 'Message',
            title: 'Message'
        }, {
            field: 'PriorityName',
            title: 'Status',
            template: function (row) {
                var color = "green";
                if (row.PriorityName === "ERR") {
                    color = "red";
                }
                if (row.PriorityName === "ALERT") {
                    color = "purple";
                }

                return '<span style="color: ' + color + ';">' + row.PriorityName + '</span>';
            }
        }, {
            field: 'Url',
            title: 'Destination'
        }, {
            field: 'UserName',
            title: 'Username'
        }, {
            field: 'Duration',
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
							<a href="#" class="btn btn-hover-brand btn-icon btn-pill btn-details" data-record-id="' + row.Id + '" data-toggle="modal" data-target="#detailsModal" title="Details">\
                                    <i class="la la-book"></i>\
                            </a>\
						';
            }
        }]
    };

	// init
    var init = function () {
        // init the datatables. Learn more: https://keenthemes.com/keen/?page=docs&section=datatable
        datatable = $('#kt_apps_log_list_datatable').KTDatatable(datatableOptions);

        var modal = $('#detailsModal');

        modal.on('show.bs.modal', function (e) {
            var link = $(e.relatedTarget);
            var id = link.data('record-id');

            $.getJSON('/api/log/' + id)
                .done(function (data) {
                    
                    modal.find('.modal-body > p').text(data.User.username + ': ' + data.Detail);
                });
        });
    };

	// search
    var search = function () {
        //$('#kt_form_user').on('change', function () {
        //   datatable.search($(this).val().toLowerCase(), 'UserId');
        //});
        $('#kt_form_user').on('changed.bs.select', function (e, clickedIndex, isSelected, previousValue) {
            console.log($(this).val());
            datatable.search($(this).val(), 'UserId');
        });

        $('#kt_form_task').on('changed.bs.select', function (e, clickedIndex, isSelected, previousValue) {
            console.log($(this).val());
            datatable.search($(this).val(), 'TaskId');
        });

        //$('#kt_form_error_type').on('change', function () {
        //    datatable.search($(this).val().toLowerCase(), 'Type');
        //});
        $('#kt_form_error_type').on('changed.bs.select', function (e, clickedIndex, isSelected, previousValue) {
            datatable.search($(this).val(), 'Type');
        });

        addDateRangeSelector();

        $('#clearFilterBtn').click(function (e) {
            e.preventDefault();

            //$("#generalSearch").val("");
            //$("#kt_form_user").selectpicker('val', '');
            //$('#kt_form_user').selectpicker('render'); 
            //$("#kt_form_error_type").selectpicker('val', '');
            //$("#kt_form_error_type").selectpicker('render');

            // datatable.reload();
            //$('#kt_form_user').selectpicker('refresh'); 


            //datatable.load();

            location.reload(true);
        });

    };

    var addDateRangeSelector = function () {
        $('#DateRange').daterangepicker({
            opens: 'left',
            startDate: moment().subtract(3, "days"),
            endDate: moment(),
            locale: {
                format: 'DD-MM-YYYY'
            }
        }, function (start, end, label) {
            datatable.search(start.format('YYYY-MM-DD 00:00:00') + ';' + end.format('YYYY-MM-DD 23:59:59'), 'daterange');
        });
    }

	// selection
    var selection = function () {
        // init form controls
        //$('#kt_form_status, #kt_form_type').selectpicker();
        $('#kt_form_user, #kt_form_error_type').selectpicker();
        $('#kt_form_task, #kt_form_error_type').selectpicker();
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