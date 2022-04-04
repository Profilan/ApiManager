using ApiManager.Logic.Common;
using ApiManager.Logic.Models;
using ApiManager.Logic.Repositories;
using ApiManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApiManager.Web.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly TaskRepository taskRepository = new TaskRepository();
        private readonly UrlRepository urlRepository = new UrlRepository();
        private readonly ShareRepository shareRepository = new ShareRepository();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var urls = urlRepository.List();
            var shares = shareRepository.List();

            var formats = new[]
            {
                new SelectListItem { Value = "1", Text = "JSON" },
                new SelectListItem { Value = "2", Text = "XML" },
                new SelectListItem { Value = "3", Text = "TXT" }
            };

            var taskViewModel = new TaskViewModel()
            {
                ApiViewModel = new TaskApiViewModel
                {
                    Urls = urls,
                    Headers = new List<HttpHeader>()
                },
                FileViewModel = new TaskFileViewModel
                {
                    Shares = shares,
                    Formats = formats,
                    SelectedShares = new HashSet<int>()
                },
                ScheduleViewModel = new TaskScheduleViewModel
                {
                    Amount = 5,
                    Unit = Unit.Minutes
                },
                IsEdit = false,
                MaxErrors = 4,
                TotalProcessedItems = 100,
                SPLogger = "EEK_sp_APIQUEUE"
            };

            return View(taskViewModel);
        }

        public ActionResult Edit(Guid id)
        {
            var task = taskRepository.GetById(id);

            TaskFileViewModel fileViewModel = null;
            TaskApiViewModel apiViewModel = null;
            TaskMailViewModel mailViewModel = null; 

            if (task.GetType() == typeof(FILETask) || task.GetType() == typeof(RECEIVESENDTask))
            {
                var formats = new[]
                {
                new SelectListItem { Value = "1", Text = "JSON" },
                new SelectListItem { Value = "2", Text = "XML" },
                new SelectListItem { Value = "3", Text = "TXT" }
                };

                ICollection<int> selectedShares = new HashSet<int>();
                foreach (var selectedShare in task.Shares)
                {
                    selectedShares.Add(selectedShare.Id);
                }

                fileViewModel = new TaskFileViewModel
                {
                    SelectedShares = selectedShares,
                    Shares = shareRepository.List(),
                    Formats = formats,
                    SelectedFormats = GetFormats(task.ContentFormats)
                };
            }

            if (task.GetType() == typeof(APITask) || task.GetType() == typeof(RECEIVESENDTask))
            {
                apiViewModel = new TaskApiViewModel
                {
                    HttpMethod = task.HttpMethod,
                    ApiType = task.ApiType,
                    Headers = task.HttpHeaders,
                    Urls = urlRepository.List(),
                    UrlId = task.Url != null ? task.Url.Id : 0,
                    Url = task.Url != null ? task.Url.Address : ""
                };
            }

            if (task.GetType() == typeof(MAILTask) || task.GetType() == typeof(RECEIVESENDTask))
            {
                mailViewModel = new TaskMailViewModel
                {
                    MailSender = task.MailSender,
                    MailRecipient = task.MailRecipient
                };
            }

            string classname;
            try
            {
                classname = task.Classname;
            }
            catch (Exception)
            {
                classname = "";
            }

            // var scheduleEnd = task.Schedule.End.ToString("yyyy-MM-ddTHH:mm");
            // TODO: Add multiple schedules

            var taskViewModel = new TaskViewModel
            {
                Id = task.Id,
                Title = task.Title,
                SPLogger = task.SPLogger,
                ApiViewModel = apiViewModel,
                AuthenticationViewModel = new TaskAuthenticationViewModel
                {
                    Username = task.Authentication.Username,
                    Password = task.Authentication.Password,
                    OAuthUrl = task.Authentication.OAuthUrl,
                    OAuthAudience = task.Authentication.OAuthAudience,
                    GrantType = task.Authentication.GrantType,
                    AuthenticationType = task.Authentication.AuthenticationType,
                    Scope = task.Authentication.Scope,
                    ApiKey = task.Authentication.ApiKey
                },
                TaskType = task.TaskType,
                // TODO: Add multiple schedules

                //ScheduleViewModel = new TaskScheduleViewModel
                //{
                //    Amount = task.Schedule.Interval.Amount,
                //    Unit = task.Schedule.Interval.Unit,
                //    ScheduleType = task.Schedule.Type,
                //    ScheduleStart = task.Schedule.Start.ToString("yyyy-MM-ddTHH:mm"),
                //    ScheduleEnd = scheduleEnd == "0001-01-01T00:00" ? DateTime.MaxValue.ToString("yyyy-MM-ddTHH:mm") : scheduleEnd,
                //    ScheduleDays = task.Schedule.Days,
                //    ScheduleRecurrence = task.Schedule.Recurrence,
                //},
                MaxErrors = task.MaxErrors,
                Classname = classname,
                FileViewModel = fileViewModel,
                TotalProcessedItems = task.TotalProcessedItems,
                MailViewModel = mailViewModel,
                IsEdit = true
            };

            return View(taskViewModel);
        }

        private int[] GetFormats(string formats)
        {
            List<int> selectedFormats = new List<int>();
            var contentFormats = formats.Split(';');

            foreach (var contentFormat in contentFormats)
            {
                selectedFormats.Add(Convert.ToInt32(contentFormat));
            }
            return selectedFormats.ToArray();
        }
    }
}