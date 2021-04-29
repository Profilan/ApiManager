"use strict";

var ApiQueueDatatable = function () {

    // variables
    var datatable;

    // init
    var init = function () {
        var queryString = window.location.search;
        var urlParams = new URLSearchParams(queryString);
        var taskId = urlParams.get('taskId');

        datatable = $('#api_queue_list').KTDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: {
                    read: {
                        url: '/api/monitor/api-queue',
                        params: {
                            'taskId': taskId
                        }
                    }
                }
            },

            // layout definition
            layout: {
                scroll: true,
                footer: false
            },

            sortable: false,

            pagination: false,

            // coluns definition
            columns: [
                {
                    field: 'key',
                    title: 'Key'
                },
                {
                    field: 'title',
                    title: 'Task'
                },
                {
                    field: 'created',
                    title: 'Creation Date'
                },
                {
                    field: 'try_count',
                    title: 'Try Count'
                },
                {
                    field: 'actions',
                    title: 'Actions',
                    width: 130,
                    overflow: 'visible',
                    textAlign: 'center',
                    template: function (row, index, datatable) {
                        return '<a href="/api/queue/delete/' + row.id + '" class="btn-delete btn btn-hover-brand btn-icon btn-pill" title="Delete">\
                                    <i class="la la-trash"></i>\
                                </a>';

                    }
                }
            ]
        }).on('kt-datatable--on-layout-updated', function (e, options) {

            var totalRows = $('#api_queue_list tbody > tr').length;

            $('.queue-total').text(totalRows);
            
            $('.btn-delete').click(function (event) {
                event.preventDefault();

                console.log(this);

                var btn = $(this);

                var url = btn.attr('href');

                $('#deleteModal .btn-ok').data('url', url);

                $('#deleteModal').modal('show');
            });
        });

        $('#deleteModal .btn-ok').click(function (event) {
            event.preventDefault();

            var url = $(this).data('url');

            $.post(url, function () {
                datatable.reload();
                $('#deleteModal').modal('hide');

                Swal.fire("Queue item deleted!");
            });
        });

        setInterval(refresh, 10 * 1000);
    };

    var refresh = function () {
        // console.log('Refresh');
        datatable.reload();
        $('.queue-total').text(datatable.getTotalRows());
    }

    return {
        init: function () {
            init();
        }
    };

}();

KTUtil.ready(function () {
    ApiQueueDatatable.init();
});