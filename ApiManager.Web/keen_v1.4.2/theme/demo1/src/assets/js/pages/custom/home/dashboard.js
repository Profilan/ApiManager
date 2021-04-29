var APIDashboard = function () {

    var period;
    var top5UsersChart;
    var top5UrlsChart;

    var init = function () {
        period = $('#Period').val();
    };

    var topFiveUrlsChart = function () {

        var ctx = $('#top-five-urls-pie-chart');

        if (ctx.length == 0) {
            return;
        }

        var onClickUrl = function (e) {
            window.location.href = "/Url/Details/" + e.dataPoint.id + "?period=";
        };

        $.get('/api/url/top5', { period: period }, function (chartData) {
                        
            top5UrlsChart = new Chart(ctx, {
                type: 'pie',
                data: chartData,
                options: {
                    responsive: true,
                    legend: {
                        position: 'right'
                    },
                    animation: {
                        animateScale: true,
                        animateRotate: true
                    },
                    onClick: (e) => {

                    }
                }
            });
        });
       
    };

    var topFiveUsersChart = function () {

        var ctx = $('#top-five-users-pie-chart');

        if (ctx.length == 0) {
            return;
        }

        $.get('/api/user/top5', { period: period }, function (chartData) {

            top5UsersChart = new Chart(ctx, {
                type: 'pie',
                data: chartData,
                options: {
                    responsive: true,
                    legend: {
                        position: 'right'
                    },
                    animation: {
                        animateScale: true,
                        animateRotate: true
                    }
                }
            });
        });

    };

    var addLatestErrors = function (data) {
        $('#latest-errors li').not('.prototype').remove();
        $.each(data, function (key, value) {
            var el = $('#latest-errors li.prototype').clone();
            el.removeClass('prototype d-none');
            el.find('span').text(value.url_name + " (" + value.error_count + ")");
            $('#latest-errors ul').append(el);
        });

        setTimeout(updateLatestErrors, 3000);
    };


    var updateLatestErrors = function () {
        $.get('/api/log/latest-errors', { period: period }, addLatestErrors);
    };

   var latestErrors = function () {
        var ctx = $('#latest-errors');


        updateLatestErrors();
    };

    var addData = function (chart, data) {
        chart.data.labels = data.labels;
        chart.data.datasets = data.datasets;
        chart.update();
    };

    var removeData = function (chart) {

        chart.data.labels = [];
        chart.data.datasets = [];
        chart.update();
    };

    var search = function () {

        $(".period-button").click(function (e) {
            e.preventDefault();

            var button = $(this);

            $('.period-button').removeClass('btn-dark').addClass('btn-primary');
            button.toggleClass('btn-dark btn-primary');

            period = button.data('period');
            $("#Period").val(period);

            removeData(top5UsersChart);
            removeData(top5UrlsChart);
            $('#latest-errors li').not('.prototype').remove();

            $.get('/api/url/top5', { period: period }, function (chartData) {
                addData(top5UrlsChart, chartData);
            });

            $.get('/api/user/top5', { period: period }, function (chartData) {
                addData(top5UsersChart, chartData);
            });

            updateLatestErrors();

            window.history.replaceState({ period: period }, "Dashboard", "?period=" + period);
        });
    };

    return {
        init: function () {
            init();
            search();
            topFiveUrlsChart();
            topFiveUsersChart();
            latestErrors();
        }
    };

}();

jQuery(document).ready(function () {
    APIDashboard.init();
});