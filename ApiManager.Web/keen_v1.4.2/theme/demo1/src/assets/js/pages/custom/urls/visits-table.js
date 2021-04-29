"use strict";
// Class definition

var KTUrlVisitsDatatable = function () {

    // variables
    var datatable, urlId, period;

    // init
    var init = function () {

        // init the params
        period = $("#Period").val();
        urlId = $('#kt_form_url').val();

        // init the datatables. Learn more: https://keenthemes.com/keen/?page=docs&section=datatable
        datatable = $('#kt_url_visits_datatable').KTDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: {
                    read: {
                        url: '/api/url/visits',
                        params: {
                            urlId: urlId,
                            period: period
                        }
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

            // columns definition
            columns: [{
                field: "username",
                title: "Username"
            }, {
                field: 'latest_visit_date',
                title: 'Latest Visit'
            }, {
                field: 'avg_duration',
                title: 'Avg. Duration',
                    template: function (row) {
                        return (Math.round(row.avg_duration * 100) / 100).toFixed(2);
                }
            }, {
                field: 'quantity_visited_urls',
                title: 'Visited #'
            }, {
                field: "Actions",
                width: 80,
                title: "Actions",
                sortable: false,
                autoHide: false,
                    overflow: 'visible',


                template: function (row) {
                    return '<div class="dropdown">\
                                <a href="#" class="btn btn-hover-brand btn-icon btn-pill" data-toggle="dropdown">\
                                    <i class="la la-ellipsis-h"></i>\
                                </a>\
                                <div class="dropdown-menu dropdown-menu-right">\
                                    <a href="/Statistics/UserVisits?userId=' + row.user_id + '&period=' + $("#Period").val() + '" class="kt-nav__link">\
											<i class="kt-nav__link-icon flaticon2-contract"></i>\
											<span class="kt-nav__link-text">User Visits</span>\
									</a>\
                                </div>\
                            </div>';
                }
            }]
        });
    };

    var processAjaxData = function (response, urlPath) {
        document.getElementById("content").innerHTML = response.html;
        document.title = response.pageTitle;
        window.history.pushState({ "html": response.html, "pageTitle": response.pageTitle }, "", urlPath);
    };

    // search
    var search = function () {

       

        $('#kt_form_url').on('changed.bs.select', function (e, clickedIndex, isSelected, previousValue) {
            

            datatable.setDataSourceParam('urlId', $(this).val());
            datatable.load();

            window.history.replaceState({ period: period }, "Url Visits", "?urlId=" + $(this).val() + "&period=" + $("#Period").val());
        });

        $(".period-button").click(function (e) {
            e.preventDefault();

            var button = $(this);

            $('.period-button').removeClass('btn-outline-primary').addClass('btn-primary');
            button.toggleClass('btn-outline-primary btn-primary');
            
            var period = button.data('period');
            $("#Period").val(period);

            datatable.setDataSourceParam('period', button.data('period'));
            datatable.load();

            window.history.replaceState({period: period}, "Url Visits", "?urlId=" + $('#kt_form_url').val() + "&period=" + period);
        });
    };

    // selection
    var selection = function () {
        // init form controls
        //$('#kt_form_status, #kt_form_type').selectpicker();
        $('#kt_form_url').selectpicker();


    };


    return {
        // public functions
        init: function () {
            init();
            search();
            selection();
        }
    };
}();

// On document ready
KTUtil.ready(function () {
    KTUrlVisitsDatatable.init();
});