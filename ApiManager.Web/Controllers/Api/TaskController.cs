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

        [Route("api/Task")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            var items = taskRepository.List();

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
                });
            }

            return Ok(tasks);
        }

        [Route("api/task/{id}")]
        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            var item = taskRepository.GetById(id);

            var task = new TaskApiModel()
            { 
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

                task.Title = data.Title;


                if (task.TaskType == TaskType.API)
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

                    task.MaxErrors = Convert.ToInt32(data.MaxErrors);
                    task.TotalProcessedItems = Convert.ToInt32(data.TotalProcessedItems);

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
                }

                if (task.TaskType == TaskType.FILE)
                {
                    try
                    {
                        task.Shares.Clear();
                        var selectedShares = data.FileViewModel.SelectedShares;
                        foreach (var shareId in selectedShares)
                        {
                            var share = shareRepository.GetById(Convert.ToInt32(shareId));
                            task.Shares.Add(share);
                        }
                    }
                    catch (Exception)
                    {

                    }

                    task.ContentFormats = String.Join(";", data.FileViewModel.SelectedFormats);
                }

                if (task.TaskType == TaskType.MAIL)
                {
                    task.MailSender = data.MailViewModel.MailSender;
                    task.MailRecipient = data.MailViewModel.MailRecipient;
                }

                Interval interval = new Interval(
                     Convert.ToInt32(data.ScheduleViewModel.Amount), data.ScheduleViewModel.Unit);

                var scheduleEnd = DateTime.MaxValue;
                if (!string.IsNullOrEmpty(data.ScheduleViewModel.ScheduleEnd))
                    scheduleEnd = DateTime.Parse(data.ScheduleViewModel.ScheduleEnd);

                Schedule schedule = new Schedule(ScheduleType.Daily, DateTime.Now, DateTime.MaxValue, data.ScheduleViewModel.ScheduleDays, 
                    Convert.ToInt32(data.ScheduleViewModel.ScheduleRecurrence), interval);

                task.Schedule = schedule;


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
                // task.Disk.UNCPath = collection.DiskUNCPath;
                task.Classname = data.Classname;

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

                Interval interval = new Interval(
                     Convert.ToInt32(data.ScheduleViewModel.Amount), data.ScheduleViewModel.Unit);

                Schedule schedule = new Schedule(ScheduleType.Daily, DateTime.Now, DateTime.MaxValue, data.ScheduleViewModel.ScheduleDays, 
                    Convert.ToInt32(data.ScheduleViewModel.ScheduleRecurrence), interval);

                Authentication authentication = new Authentication(data.AuthenticationViewModel.Username,
                    data.AuthenticationViewModel.Password,
                    data.AuthenticationViewModel.AuthenticationType,
                    data.AuthenticationViewModel.Scope,
                    data.AuthenticationViewModel.GrantType,
                    data.AuthenticationViewModel.OAuthUrl,
                    data.AuthenticationViewModel.OAuthAudience);
                authentication.ApiKey = data.AuthenticationViewModel.ApiKey;

                if (taskType != TaskType.RECEIVESEND)
                {
                    SchedulerTask task = null;
                    switch (taskType)
                    {
                        case TaskType.API:
                            task = new APITask(data.Title,
                                schedule,
                                authentication,
                                false);
                            break;
                        case TaskType.FILE:
                            task = new FILETask(data.Title,
                                schedule,
                                authentication,
                                false);
                            break;
                        case TaskType.FTP:
                            break;
                        case TaskType.MAIL:
                            task = new MAILTask(data.Title,
                                schedule,
                                authentication,
                                false);
                            break;
                    }

                    var url = urlRepository.GetById(Convert.ToInt32(data.ApiViewModel.UrlId));
                    task.Url = url;

                    task.HttpMethod = data.ApiViewModel.HttpMethod;
                    task.MaxErrors = Convert.ToInt32(data.MaxErrors);
                    task.TotalProcessedItems = Convert.ToInt32(data.TotalProcessedItems);
                    task.TaskType = data.TaskType;
                    // task.Disk.UNCPath = collection.DiskUNCPath;
                    task.Classname = data.Classname;
                    task.ContentFormats = String.Join(";", data.FileViewModel.SelectedFormats);

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

                    if (task.TaskType == TaskType.FILE)
                    {
                        try
                        {
                            task.Shares.Clear();
                            var selectedShares = data.FileViewModel.SelectedShares;
                            foreach (var shareId in selectedShares)
                            {
                                var share = shareRepository.GetById(Convert.ToInt32(shareId));
                                task.Shares.Add(share);
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }

                    if (task.TaskType == TaskType.MAIL)
                    {
                        task.MailSender = data.MailViewModel.MailSender;
                        task.MailRecipient = data.MailViewModel.MailRecipient;
                    }

                    if (task.TaskType == TaskType.API)
                    {
                        var headers = data.ApiViewModel.Headers;
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
                    }

                    task.LastRunTime = DateTime.Now;
                    task.LastRunResult = "0 Not Run";

                    taskRepository.Insert(task);
                }
                else // TaskType == RECEIVESEND
                {
                    ReceiveSendTask task = new ReceiveSendTask();
                    

                    Authentication receiveAuthentication = new Authentication(data.ReceiverViewModel.AuthenticationViewModel.Username,
                        data.ReceiverViewModel.AuthenticationViewModel.Password,
                        data.ReceiverViewModel.AuthenticationViewModel.AuthenticationType,
                        data.ReceiverViewModel.AuthenticationViewModel.Scope,
                        data.ReceiverViewModel.AuthenticationViewModel.GrantType,
                        data.ReceiverViewModel.AuthenticationViewModel.OAuthUrl,
                        data.ReceiverViewModel.AuthenticationViewModel.OAuthAudience);
                    receiveAuthentication.ApiKey = data.ReceiverViewModel.AuthenticationViewModel.ApiKey;

                    TaskReceiver receiver = new TaskReceiver();
                    receiver.ActionType = data.ReceiverViewModel.Type;
                    receiver.Authentication = receiveAuthentication;
                    
                    if (data.ReceiverViewModel.Type == ActionType.API)
                    {
                        foreach (var header in data.ReceiverViewModel.ApiViewModel.ActionHeaders)
                        {
                            if (!string.IsNullOrEmpty(header.Name))
                            {
                                receiver.HttpHeaders.Add(header);
                            }
                        }
                        receiver.ApiType = data.ReceiverViewModel.ApiViewModel.ApiType;
                        receiver.HttpMethod = data.ReceiverViewModel.ApiViewModel.HttpMethod;
                        receiver.ActionType = data.ReceiverViewModel.Type;
                        
                        var receiveUrl = urlRepository.GetById(Convert.ToInt32(data.ReceiverViewModel.ApiViewModel.UrlId));
                        receiver.Url = receiveUrl;
                    }

                    if (data.ReceiverViewModel.Type == ActionType.MAIL)
                    {
                        receiver.MailRecipient = data.ReceiverViewModel.MailViewModel.MailRecipient;
                        receiver.MailSender = data.ReceiverViewModel.MailViewModel.MailSender;
                    }
                   
                    if (data.ReceiverViewModel.Type == ActionType.FILE)
                    {
                        var selectedReceiveShares = data.ReceiverViewModel.FileViewModel.SelectedShares;
                        foreach (var shareId in selectedReceiveShares)
                        {
                            var share = shareRepository.GetById(Convert.ToInt32(shareId));
                            receiver.Shares.Add(share);
                        }
                    }

                    Authentication sendAuthentication = new Authentication(data.SenderViewModel.AuthenticationViewModel.Username,
                        data.SenderViewModel.AuthenticationViewModel.Password,
                        data.SenderViewModel.AuthenticationViewModel.AuthenticationType,
                        data.SenderViewModel.AuthenticationViewModel.Scope,
                        data.SenderViewModel.AuthenticationViewModel.GrantType,
                        data.SenderViewModel.AuthenticationViewModel.OAuthUrl,
                        data.SenderViewModel.AuthenticationViewModel.OAuthAudience);
                    sendAuthentication.ApiKey = data.SenderViewModel.AuthenticationViewModel.ApiKey;

                    TaskSender sender = new TaskSender();
                    sender.ActionType = data.SenderViewModel.Type;
                    sender.Authentication = sendAuthentication;

                    if (data.SenderViewModel.Type == ActionType.API)
                    {
                        foreach (var header in data.SenderViewModel.ApiViewModel.ActionHeaders)
                        {
                            if (!string.IsNullOrEmpty(header.Name))
                            {
                                sender.HttpHeaders.Add(header);
                            }
                        }
                        sender.ApiType = data.SenderViewModel.ApiViewModel.ApiType;
                        sender.HttpMethod = data.SenderViewModel.ApiViewModel.HttpMethod;
                        sender.GraphQLMethod = data.SenderViewModel.ApiViewModel.GraphQLMethod;

                        var sendUrl = urlRepository.GetById(Convert.ToInt32(data.SenderViewModel.ApiViewModel.UrlId));
                        sender.Url = sendUrl;
                    }

                    if (data.SenderViewModel.Type == ActionType.MAIL)
                    {
                        sender.MailRecipient = data.SenderViewModel.MailViewModel.MailRecipient;
                        sender.MailSender = data.SenderViewModel.MailViewModel.MailSender;
                    }

                    if (data.SenderViewModel.Type == ActionType.FILE)
                    {
                        var selectedSendShares = data.SenderViewModel.FileViewModel.SelectedShares;
                        foreach (var shareId in selectedSendShares)
                        {
                            var share = shareRepository.GetById(Convert.ToInt32(shareId));
                            sender.Shares.Add(share);
                        }
                    }

                    task.Title = data.Title;
                    task.TaskType = data.TaskType;
                    task.SPLogger = data.SPLogger;
                    task.Schedule = schedule;
                    task.MaxErrors = Convert.ToInt32(data.MaxErrors);
                    task.TotalProcessedItems = Convert.ToInt32(data.TotalProcessedItems);
                    if (!string.IsNullOrEmpty(data.Classname))
                    {
                        task.Classname = data.Classname;
                    }
                    else
                    {

                    }
                    
                    task.Authentication = authentication;
                    task.Receiver = receiver;
                    receiver.Task = task;
                    task.Sender = sender;
                    sender.Task = task;

                    task.LastRunTime = DateTime.Now;
                    task.LastRunResult = "0 Not Run";

                    taskRepository.Insert(task);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
