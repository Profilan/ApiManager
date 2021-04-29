
using ApiManager.Logic.Models;
using ApiManager.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiManager.Web.Services
{
    public static class DashboardService
    {
        private static List<DataPoint> _dataPoints;

        public static ChartDataApiModel GetTopFiveUrlsData(IEnumerable<UrlStatistics> urls)
        {
            var labels = new List<string>();
            var datasets = new List<ChartDataset>();
            var data = new List<int>();
            var backgroundColor = new List<string>();

            backgroundColor.Add("#fe3995");
            backgroundColor.Add("#f6aa33");
            backgroundColor.Add("#6e4ff5");
            backgroundColor.Add("#2abe81");
            backgroundColor.Add("#c7d2e7");

            var index = 1;
            foreach (var url in urls)
            {
                if (url.Total > 0)
                {
                    labels.Add(url.Name);

                    data.Add(url.Total);

                }
            }

            datasets.Add(new ChartDataset()
            {
                Label = "Dataset " + index,
                Data = data,
                Colors = backgroundColor
            });

            var model = new ChartDataApiModel()
            { 
                Datasets = datasets,
                Labels = labels
            };

            return model;
        }

        public static ChartDataApiModel GetTopFiveUsersData(IEnumerable<UserStatistics> users)
        {
            var labels = new List<string>();
            var datasets = new List<ChartDataset>();
            var data = new List<int>();
            var backgroundColor = new List<string>();

            backgroundColor.Add("#fe3995");
            backgroundColor.Add("#f6aa33");
            backgroundColor.Add("#6e4ff5");
            backgroundColor.Add("#2abe81");
            backgroundColor.Add("#c7d2e7");

            var index = 1;
            foreach (var user in users)
            {
                if (user.Total > 0)
                {
                    labels.Add(user.Username);

                    data.Add(user.Total);

                }
            }

            datasets.Add(new ChartDataset()
            {
                Label = "Dataset " + index,
                Data = data,
                Colors = backgroundColor
            });

            var model = new ChartDataApiModel()
            {
                Datasets = datasets,
                Labels = labels
            };

            return model;
        }

        public static ChartDataApiModel GetUrlUsageData(IEnumerable<UrlStatistics> urls)
        {
            var labels = new List<string>();
            var datasets = new List<ChartDataset>();
            var data = new List<int>();
            var backgroundColor = new List<string>();

            backgroundColor.Add("#fe3995");
            backgroundColor.Add("#f6aa33");
            backgroundColor.Add("#6e4ff5");
            backgroundColor.Add("#2abe81");
            backgroundColor.Add("#c7d2e7");

            var index = 1;
            foreach (var url in urls)
            {
                if (url.Total > 0)
                {
                    labels.Add(url.Name);

                    data.Add(url.Total);

                }
            }

            datasets.Add(new ChartDataset()
            {
                Label = "Dataset " + index,
                Data = data,
                Colors = backgroundColor
            });

            var model = new ChartDataApiModel()
            {
                Datasets = datasets,
                Labels = labels
            };

            return model;
        }
    }
}