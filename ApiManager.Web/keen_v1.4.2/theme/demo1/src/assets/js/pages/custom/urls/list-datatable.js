"use strict";
// Class definition

var KTUrlsListDatatable = function() {

	// variables
	var datatable;

	// init
    var init = function () {
        // init the datatables. Learn more: https://keenthemes.com/keen/?page=docs&section=datatable
        datatable = $('#kt_apps_url_list_datatable').KTDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: {
                    read: {
                        url: '/api/url'
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
                field: "name",
                title: "Name"
            }, {
                field: "access_type",
                title: "Access Type",
                template: function (row) {
                    var status = {
                        2: { 'title': 'Outbound', 'state': 'primary' },
                        1: { 'title': 'Inbound', 'state': 'danger' }
                    };
                    return '<span class="kt-badge kt-badge--' + status[row.access_type].state + ' kt-badge--dot"></span>&nbsp;<span class="kt-font-bold kt-font-' + status[row.access_type].state + '">' +
                        status[row.access_type].title + '</span>';
                }
            }, {
                field: 'address',
                title: 'Address'
            }, {
                field: 'hits',
                title: 'Hits'
            }, {
                field: "Actions",
                width: 120,
                title: "Actions",
                sortable: false,
                autoHide: false,
                overflow: 'visible',
                template: function (row) {
                    return '\
                            <a href="/url/edit/' + row.id + '" class="btn btn-sm btn-clean btn-icon btn-icon-sm" title="Edit Details">\
							    <i class="flaticon2-paper"></i>\
							</a>\
                            <a href="/url/delete/' + row.id + '" class="btn btn-sm btn-clean btn-icon btn-icon-sm" title="Delete">\
							    <i class="flaticon2-trash"></i>\
							</a>\
                             <a href="/statistics/urlvisits?urlid=' + row.id + '" class="btn btn-sm btn-clean btn-icon btn-icon-sm" title="Delete">\
							    <i class="flaticon2-analytics"></i>\
							</a>\
                       ';
                }
            }]
        });
    };

	// search
    var search = function () {
        $('#kt_form_access_type').on('change', function () {
            datatable.search($(this).val().toLowerCase(), 'AccessType');
        });
    };

	// selection
    var selection = function () {
        // init form controls
        $('#kt_form_access_type').selectpicker();
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
	KTUrlsListDatatable.init();
});