using ApiManager.Logic.Common;
using ApiManager.Logic.Models;
using ApiManager.Logic.Repositories;
using ApiManager.Web.Models;
using ApiManager.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Runtime.Remoting.Channels;
using System.Web.Http;

namespace ApiManager.Web.Controllers.Api
{
    public class TaskController : ApiController
    {
        private readonly TaskRepository taskRepository = new TaskRepository();
        private readonly QueueRepository queueRepository = new QueueRepository();
        private readonly UrlRepository urlRepository = new UrlRepository();
        private readonly ShareRepository shareRepository = new ShareRepository();
        private readonly LogRepository logRepository = new LogRepository();

        [Route("api/Task")]
        [HttpPost]
        public IHttpActionResult Get(FormDataCollection data)
        {
            string searchString = data["query[generalsearch]"];
            string sortOrder = data["sort[field]"] + "." + data["sort[sort]"];

            var items = taskRepository.List(sortOrder, searchString);

            List<TaskApiModel> tasks = new List<TaskApiModel>();
            foreach (var item in items)
            {
                tasks.Add(new TaskApiModel()
                {
                    Id = item.Id,
                    Title = item.Title,
                    Type = item.TaskType.ToString(),
                    Queued = queueRepository.List().Where(x => x.Task.Id == item.Id).Count()
                });
            }

            return Ok(tasks);
        }

        [Route("api/task")]
        [HttpGet]
        public IHttpActionResult GetTasks(int schedulerId = 1)
        {
            try
            {
                var items = taskRepository.ListByScheduler(schedulerId);

                List<TaskApiModel> tasks = new List<TaskApiModel>();
                foreach (var item in items)
                {
                    //int errors = logRepository.ListByTypeAndTask(Period.Week, ErrorType.ERR, item).Count();

                    tasks.Add(new TaskApiModel()
                    {
                        Id = item.Id,
                        Title = item.Title,
                        Type = item.TaskType.ToString(),
                        Queued = queueRepository.List().Where(x => x.Task.Id == item.Id).Count(),
                        Enabled = item.Enabled,
                        LastRunTime = item.LastRunTime.ToString("dd-MM-yyyy hh:mm:ss"),
                        LastRunResult = item.LastRunResult,
                        LastRunDetails = item.LastRunDetails,
                        Active = item.Active
                    });
                }

                return Ok(tasks);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/task/{id}")]
        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            var item = taskRepository.GetById(id);

            var task = new TaskApiModel()
            { 
                Id = item.Id,
                Title = item.Title,
                LastRunDetails = item.LastRunDetails,
                Queued = queueRepository.List().Where(x => x.Task.Id == item.Id).Count()
        };


            return Ok(task);
        }

        [HttpPost]
        [Route("api/task/reset/{id}")]
        public IHttpActionResult Reset(Guid id)
        {
            var task = taskRepository.GetById(id);
            if (task != null)
            {
                taskRepository.Reset(id);
            }
            else
            {
                return NotFound();
            }

            return Ok(new { message = task.Title + " succesfully reset" });
        }

        [HttpPost]
        [Route("api/task/edit")]
        public IHttpActionResult Post(TaskViewModel data)
        {
            try
            {

                var task = taskRepository.GetById(data.Id);


                if (task.TaskType == TaskType.API || task.TaskType == TaskType.RECEIVESEND)
                {
                    task.HttpMethod = data.ApiViewModel.HttpMethod;

                    string spLogger;
                    try
                    {
                        spLogger = data.SPLogger;
                    }
                    catch (Exception)
                    {
                        spLogger = "";
                    }
                    task.SPLogger = spLogger;

                    var url = urlRepository.GetById(Convert.ToInt32(data.ApiViewModel.UrlId));
                    task.Url = url;

                    try
                    {
                        task.HttpHeaders.Clear();
                        foreach (var header in data.ApiViewModel.Headers)
                        {
                            if (!string.IsNullOrEmpty(header.Name))
                            {
                                task.AddHeader(header);
                            }
                        }
                    }
                    catch
                    {

                    }

                    task.ApiType = data.ApiViewModel.ApiType;
                    task.HttpMethod = data.ApiViewModel.HttpMethod;
                    task.GraphQLMethod = data.ApiViewModel.GraphQLMethod;
                }

                if (task.TaskType == TaskType.FILE || task.TaskType == TaskType.RECEIVESEND)
                {
                    if (data.FileViewModel != null)
                    {
                        var selectedShares = data.FileViewModel.SelectedShares;
                        if (data.FileViewModel.SelectedShares != null)
                        {
                            foreach (var shareId in selectedShares)
                            {
                                var share = shareRepository.GetById(Convert.ToInt32(shareId));
                                task.Shares.Add(share);
                            }
                        }
                    }

                    task.ContentFormats = String.Join(";", data.FileViewModel.SelectedFormats);
                }

                if (task.TaskType == TaskType.MAIL || task.TaskType == TaskType.RECEIVESEND)
                {
                    task.MailSender = data.MailViewModel.MailSender;
                    task.MailRecipient = data.MailViewModel.MailRecipient;
                }

                /*
                Interval interval = new Interval(
                     Convert.ToInt32(data.ScheduleViewModel.Amount), data.ScheduleViewModel.Unit);
                */

                /*
                var scheduleEnd = DateTime.MaxValue;
                if (!string.IsNullOrEmpty(data.ScheduleViewModel.ScheduleEnd))
                    scheduleEnd = DateTime.Parse(data.ScheduleViewModel.ScheduleEnd);
                */

                // TODO: Add multiple schedules

                //Schedule schedule = new Schedule(ScheduleType.Daily, DateTime.Now, DateTime.MaxValue, data.ScheduleViewModel.ScheduleDays, 
                //    Convert.ToInt32(data.ScheduleViewModel.ScheduleRecurrence), interval);

                // task.Schedule = schedule;

                Authentication authentication = new Authentication(data.AuthenticationViewModel.Username,
                    data.AuthenticationViewModel.Password,
                    data.AuthenticationViewModel.AuthenticationType,
                    data.AuthenticationViewModel.Scope,
                    data.AuthenticationViewModel.GrantType,
                    data.AuthenticationViewModel.OAuthUrl,
                    data.AuthenticationViewModel.OAuthAudience);
                authentication.ApiKey = data.AuthenticationViewModel.ApiKey;

                task.Authentication = authentication;
                task.TaskType = data.TaskType;
                
                task.Classname = data.Classname;
                task.Title = data.Title;

                task.MaxErrors = Convert.ToInt32(data.MaxErrors);
                task.TotalProcessedItems = Convert.ToInt32(data.TotalProcessedItems);

                taskRepository.Update(task);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        
        public IHttpActionResult Create(FormDataCollection data)
        {
            return Ok();
        }

        [HttpPost]
        [Route("api/task/create")]
        public IHttpActionResult CreateTask(TaskViewModel data)
        {
            try
            {
                TaskType taskType = data.TaskType;

                //Interval interval = new Interval(
                //     Convert.ToInt32(data.ScheduleViewModel.Amount), data.ScheduleViewModel.Unit);

                // TODO: Add multiple schedules

                //Schedule schedule = new Schedule(ScheduleType.Daily, DateTime.Now, DateTime.MaxValue, data.ScheduleViewModel.ScheduleDays, 
                //    Convert.ToInt32(data.ScheduleViewModel.ScheduleRecurrence), interval);

                Authentication authentication = new Authentication(data.AuthenticationViewModel.Username,
                    data.AuthenticationViewModel.Password,
                    data.AuthenticationViewModel.AuthenticationType,
                    data.AuthenticationViewModel.Scope,
                    data.AuthenticationViewModel.GrantType,
                    data.AuthenticationViewModel.OAuthUrl,
                    data.AuthenticationViewModel.OAuthAudience);
                authentication.ApiKey = data.AuthenticationViewModel.ApiKey;


                SchedulerTask task = null;
                switch (taskType)
                {
                    case TaskType.API:
                        task = new APITask(data.Title,
                            authentication,
                            false);
                        break;
                    case TaskType.RECEIVESEND:
                        task = new RECEIVESENDTask(data.Title,
                            authentication,
                            false);

                        break;
                    case TaskType.FILE:
                        task = new FILETask(data.Title,
                            authentication,
                            false);
                        break;
                    case TaskType.FTP:
                        break;
                    case TaskType.MAIL:
                        task = new MAILTask(data.Title,
                            authentication,
                            false);
                        break;
                }
                task.TaskType = data.TaskType;
                task.ContentFormats = "1";

                if (task.TaskType == TaskType.FILE || task.TaskType == TaskType.RECEIVESEND)
                {
                    try
                    {
                        if (data.FileViewModel != null)
                        {
                            var selectedShares = data.FileViewModel.SelectedShares;
                            foreach (var shareId in selectedShares)
                            {
                                var share = shareRepository.GetById(Convert.ToInt32(shareId));
                                task.Shares.Add(share);
                            }
                            task.ContentFormats = String.Join(";", data.FileViewModel.SelectedFormats);
                        }
                    }
                    catch (Exception)
                    {

                    }
                    
                }

                if (task.TaskType == TaskType.MAIL || task.TaskType == TaskType.RECEIVESEND)
                {
                    if (data.MailViewModel != null)
                    {
                        task.MailSender = data.MailViewModel.MailSender;
                        task.MailRecipient = data.MailViewModel.MailRecipient;
                    }
                }

                if (task.TaskType == TaskType.API || task.TaskType == TaskType.RECEIVESEND)
                {
                    var headers = data.ApiViewModel.Headers;
                    try
                    {
                        foreach (var header in data.ApiViewModel.Headers)
                        {
                            if (!string.IsNullOrEmpty(header.Name))
                            {
                                task.AddHeader(header);
                            }
                        }
                    }
                    catch
                    {

                    }
                    var url = urlRepository.GetById(Convert.ToInt32(data.ApiViewModel.UrlId));
                    task.Url = url;

                    task.ApiType = data.ApiViewModel.ApiType;
                    task.HttpMethod = data.ApiViewModel.HttpMethod;
                    task.GraphQLMethod = data.ApiViewModel.GraphQLMethod;
                }

                string spLogger;
                try
                {
                    spLogger = data.SPLogger;
                }
                catch (Exception)
                {
                    spLogger = "";
                }
                task.SPLogger = spLogger;

                task.MaxErrors = Convert.ToInt32(data.MaxErrors);
                task.TotalProcessedItems = Convert.ToInt32(data.TotalProcessedItems);
               
                task.Classname = data.Classname;
                task.LastRunTime = DateTime.Now;
                task.LastRunResult = "0 Not Run";

                taskRepository.Insert(task);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
