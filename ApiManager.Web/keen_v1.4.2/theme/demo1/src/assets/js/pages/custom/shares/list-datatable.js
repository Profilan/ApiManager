"use strict";
// Class definition

var KTSharesListDatatable = function() {

	// variables
	var datatable;

	// init
    var init = function () {
        // init the datatables. Learn more: https://keenthemes.com/keen/?page=docs&section=datatable
        datatable = $('#kt_share_list').KTDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: {
                    read: {
                        url: '/api/share'
                    }
                },
                pageSize: 10, // display 20 records per page
                serverPaging: true,
                serverFiltering: true,
                serverSorting: true,
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
                field: "unc_path",
                title: "UNC Path"
            }, {
                field: 'monitor_inactivity',
                title: 'Monitor Inactivity'
            }, {
                field: "Actions",
                width: 80,
                title: "Actions",
                sortable: false,
                autoHide: false,
                overflow: 'visible',
                template: function (row) {
                    return '\
                            <a href="/share/edit/' + row.id + '" class="btn btn-sm btn-clean btn-icon btn-icon-sm" title="Edit Details">\
							    <i class="flaticon2-paper"></i>\
							</a>\
                            <a href="/share/delete/' + row.id + '" class="btn btn-sm btn-clean btn-icon btn-icon-sm" title="Delete">\
							    <i class="flaticon2-trash"></i>\
							</a>\
                        ';
                }
            }]
        });
    };

	// search
    var search = function () {
        
    };


	return {
		// public functions
		init: function() {
			init();
			search();
		}
	};
}();

// On document ready
KTUtil.ready(function() {
	KTSharesListDatatable.init();
});