using ApiManager.Logic.Models;
using ApiManager.Logic.Repositories;
using ApiManager.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace ApiManager.Web.Controllers.Api
{
    public class MonitorController : ApiController
    {
        private readonly TaskRepository taskRepository = new TaskRepository();
        private readonly QueueRepository queueRepository = new QueueRepository();

        [Route("api/monitor/api-task-scheduler")]
        [HttpPost]
        public IHttpActionResult APITaskScheduler(FormDataCollection data)
        {
            string searchString = data["query[generalsearch]"];
            string sortOrder = data["sort[field]"] + "." + data["sort[sort]"];

            var items = taskRepository.List(sortOrder, searchString);

            List<TaskApiModel> tasks = new List<TaskApiModel>();
            foreach (var item in items)
            {
                var queued = queueRepository.List().Where(x => x.Task.Id == item.Id).Count();

                tasks.Add(new TaskApiModel()
                {
                    Id = item.Id,
                    Title = item.Title,
                    Type = item.TaskType.ToString(),
                    Enabled = item.Enabled,
                    LastRunTime = item.LastRunTime.ToString("dd-MM-yyyy hh:mm:ss"),
                    LastRunResult = item.LastRunResult,
                    LastRunDetails = item.LastRunDetails,
                    Queued = queued
                });
            }

            return Ok(tasks);
        }

        [Route("api/monitor/api-queue")]
        [HttpPost]
        public IHttpActionResult APIQueue(FormDataCollection data)
        {
            IEnumerable<Queue> items;
            if (data["taskId"] != "00000000-0000-0000-0000-000000000000")
            {
                items = queueRepository.ListByTask(new Guid(data["taskId"]));
            }
            else
            {
                items = queueRepository.List();
            }
            

            List<QueueApiModel> queueItems = new List<QueueApiModel>();
            foreach (var item in items)
            {
                queueItems.Add(new QueueApiModel()
                {
                    Id = item.Id,
                    TaskTitle = item.Task.Title,
                    Created = item.SysCreated.ToString("dd-MM-yyyy hh:mm:ss"),
                    TryCount = item.TryCount,
                    Key = item.Key,
                    Error = item.Error
                });
            }

            return Ok(queueItems);
        }
    }
}
