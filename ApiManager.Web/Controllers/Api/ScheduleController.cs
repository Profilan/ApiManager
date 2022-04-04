using ApiManager.Logic.Common;
using ApiManager.Logic.Models;
using ApiManager.Logic.Repositories;
using ApiManager.Web.Models;
using ApiManager.Web.Models.Api;
using APIManager.Logic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace ApiManager.Web.Controllers.Api
{
    public class ScheduleController : ApiController
    {
        private readonly ScheduleRepository scheduleRepository = new ScheduleRepository();
        private readonly TaskRepository taskRepository = new TaskRepository();

        [HttpPost]
        [Route("api/schedule")]
        public IHttpActionResult Get(FormDataCollection data)
        {
            string searchString = data["query[generalsearch]"];
            string sortOrder = data["sort[field]"] + "." + data["sort[sort]"];

            IEnumerable<Schedule> items = scheduleRepository.ListByTask(new Guid(data["query[taskId]"]), sortOrder);

            var schedules = new List<ScheduleApiModel>();
            foreach (var item in items)
            {
                schedules.Add(new ScheduleApiModel
                {
                    Id = item.Id,
                    TaskId = item.Task.Id,
                    StartBoundery = item.StartBoundery.ToString("dd-MM-yyyy HH:mm"),
                    EndBoundery = item.EndBoundery.ToString("dd-MM-yyyy HH:mm"),
                    Type = item.GetType().Name,
                    Enabled = item.Enabled
                }); ;
            }
            return Ok(schedules);
        }

        [Route("api/schedule/create")]
        [HttpPost]
        public IHttpActionResult Create(ScheduleViewModel data)
        {
            try
            {
                var task = taskRepository.GetById(data.TaskId);

                Schedule schedule = null;

                TimeSpan duration = new TimeSpan(0, 5, 0);
                if (data.Repeat)
                {
                    switch (data.RepeatUnit)
                    {
                        case Unit.Hours:
                            duration = new TimeSpan(data.RepeatAmount, 0, 0);
                            break;
                        case Unit.Minutes:
                            duration = new TimeSpan(0, data.RepeatAmount, 0);
                            break;
                        case Unit.Seconds:
                            duration = new TimeSpan(0, 0, data.RepeatAmount);
                            break;
                    }
                }

                switch (data.Type)
                {
                    case ScheduleType.Once:
                        schedule = new TimeSchedule
                        {
                            StartBoundery = DateTime.Parse(data.StartBoundery),
                            Enabled = data.Enabled,
                            Repeat = data.Repeat,
                            Repetition = new RepetitionPattern
                            {
                                Interval = duration,
                            },
                            EndBoundery = string.IsNullOrEmpty(data.EndBoundery) ? DateTime.MaxValue : DateTime.Parse(data.EndBoundery)
                        };
                        break;
                    case ScheduleType.Daily:
                        schedule = new DailySchedule
                        {
                            StartBoundery = DateTime.Parse(data.StartBoundery),
                            Enabled = data.Enabled,
                            Repeat = data.Repeat,
                            Repetition = new RepetitionPattern
                            {
                                Interval = duration,
                            },
                            DaysInterval = data.Interval,
                            EndBoundery = string.IsNullOrEmpty(data.EndBoundery) ? DateTime.MaxValue : DateTime.Parse(data.EndBoundery)
                        };
                        break;
                    case ScheduleType.Weekly:
                        schedule = new WeeklySchedule
                        {
                            StartBoundery = DateTime.Parse(data.StartBoundery),
                            Enabled = data.Enabled,
                            Repeat = data.Repeat,
                            Repetition = new RepetitionPattern
                            {
                                Interval = duration,
                            },
                            WeeksInterval = data.Interval,
                            DaysOfWeek = ToDaysOfTheWeek(data.Days),
                            EndBoundery = string.IsNullOrEmpty(data.EndBoundery) ? DateTime.MaxValue : DateTime.Parse(data.EndBoundery)
                        };
                        break;
                }

                task.AddSchedule(schedule);

                taskRepository.Update(task);

                return Ok();

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [Route("api/schedule/edit")]
        [HttpPost]
        public IHttpActionResult CreateUrl(ScheduleViewModel data)
        {
            try
            {
                var schedule = scheduleRepository.GetById(data.Id);

                TimeSpan duration = new TimeSpan(0, 5, 0);
                if (data.Repeat)
                {
                    switch (data.RepeatUnit)
                    {
                        case Unit.Hours:
                            duration = new TimeSpan(data.RepeatAmount, 0, 0);
                            break;
                        case Unit.Minutes:
                            duration = new TimeSpan(0, data.RepeatAmount, 0);
                            break;
                        case Unit.Seconds:
                            duration = new TimeSpan(0, 0, data.RepeatAmount);
                            break;
                    }
                }


                schedule.StartBoundery = DateTime.Parse(data.StartBoundery);
                schedule.EndBoundery = DateTime.Parse(data.EndBoundery);
                schedule.Enabled = data.Enabled;
                schedule.Repeat = data.Repeat;
                schedule.Repetition = new RepetitionPattern
                {
                    Interval = duration
                };
                schedule.Enabled = data.Enabled;

                switch (data.Type)
                {
                    case ScheduleType.Daily:
                        ((DailySchedule)schedule).DaysInterval = data.Interval;
                        break;
                    case ScheduleType.Weekly:
                        ((WeeklySchedule)schedule).WeeksInterval = data.Interval;
                        ((WeeklySchedule)schedule).DaysOfWeek = ToDaysOfTheWeek(data.Days);
                        break;
                }

                scheduleRepository.Update(schedule);

                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("api/schedule/delete/{id}")]
        [HttpPost]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                scheduleRepository.Delete(id);

                return Content(HttpStatusCode.NoContent, "Schedule deleted");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private DaysOfTheWeek ToDaysOfTheWeek(IList<int> days)
        {
            DaysOfTheWeek daysOfTheWeek = 0;
            foreach (var day in days)
            {
                daysOfTheWeek |= (DaysOfTheWeek)day;
            }

            return daysOfTheWeek;
        }
    }
}
