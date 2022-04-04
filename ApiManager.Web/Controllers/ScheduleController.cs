using ApiManager.Logic.Common;
using ApiManager.Logic.Models;
using ApiManager.Logic.Repositories;
using ApiManager.Web.Models;
using APIManager.Logic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApiManager.Web.Controllers
{
    [Authorize]
    public class ScheduleController : Controller
    {
        private readonly ScheduleRepository scheduleRepository = new ScheduleRepository();
        private readonly TaskRepository messageRepository = new TaskRepository();

        public ActionResult Edit(Guid id)
        {
            var schedule = scheduleRepository.GetById(id);

            // Determine repetition
            int repeatAmount = schedule.Repetition.Interval.Hours;
            Unit repeatUnit = Unit.Hours;
            if (schedule.Repeat)
            {
                if (schedule.Repetition.Interval.Minutes > 0 && schedule.Repetition.Interval.Hours == 0)
                {
                    repeatAmount = schedule.Repetition.Interval.Minutes;
                    repeatUnit = Unit.Minutes;
                }
                if (schedule.Repetition.Interval.Seconds > 0 && schedule.Repetition.Interval.Minutes == 0)
                {
                    repeatAmount = schedule.Repetition.Interval.Seconds;
                    repeatUnit = Unit.Seconds;
                }
            }

            // Determine schedule type and 
            // Determine recurrency interval (daily or weekly)
            // Determine days of the week
            ScheduleType scheduleType = ScheduleType.Once;
            short interval = 0;
            IList<int> days = new List<int>();
            switch (schedule.GetType().Name)
            {
                case "DailySchedule":
                    scheduleType = ScheduleType.Daily;
                    interval = ((DailySchedule)schedule).DaysInterval;
                    break;
                case "WeeklySchedule":
                    scheduleType = ScheduleType.Weekly;
                    interval = ((WeeklySchedule)schedule).WeeksInterval;
                    days = ToDays(((WeeklySchedule)schedule).DaysOfWeek);
                    break;
            }



            ScheduleViewModel model = new ScheduleViewModel
            {
                Id = id,
                TaskId = schedule.Task.Id,
                Repeat = schedule.Repeat,
                RepeatAmount = repeatAmount,
                RepeatUnit = repeatUnit,
                StartBoundery = schedule.StartBoundery.ToString("yyyy-MM-ddTHH:mm"),
                EndBoundery = schedule.EndBoundery.ToString("yyyy-MM-ddTHH:mm"),
                Type = scheduleType,
                Enabled = schedule.Enabled,
                Interval = interval,
                Days = days
            };

            return View(model);
        }

        public ActionResult Create(Guid taskId)
        {
            ScheduleViewModel model = new ScheduleViewModel()
            {
                TaskId = taskId,
                Repeat = false,
                RepeatAmount = 1,
                RepeatUnit = Unit.Hours,
                StartBoundery = DateTime.Now.ToString("yyyy-MM-ddTHH:mm"),
                Type = ScheduleType.Once,
                Enabled = true,
                Interval = 1
            };

            return View(model);
        }

        private IList<int> ToDays(DaysOfTheWeek daysOfTheWeek)
        {
            IList<int> days = new List<int>();
            if (((int)daysOfTheWeek & (int)DaysOfTheWeek.Sunday) == 1)
            {
                days.Add((int)DaysOfTheWeek.Sunday);
            }

            if (((int)daysOfTheWeek & (int)DaysOfTheWeek.Monday) == 2)
            {
                days.Add((int)DaysOfTheWeek.Monday);
            }
            if (((int)daysOfTheWeek & (int)DaysOfTheWeek.Tuesday) == 4)
            {
                days.Add((int)DaysOfTheWeek.Tuesday);
            }
            if (((int)daysOfTheWeek & (int)DaysOfTheWeek.Wednesday) == 8)
            {
                days.Add((int)DaysOfTheWeek.Wednesday);
            }
            if (((int)daysOfTheWeek & (int)DaysOfTheWeek.Thursday) == 16)
            {
                days.Add((int)DaysOfTheWeek.Thursday);
            }
            if (((int)daysOfTheWeek & (int)DaysOfTheWeek.Friday) == 32)
            {
                days.Add((int)DaysOfTheWeek.Friday);
            }
            if (((int)daysOfTheWeek & (int)DaysOfTheWeek.Saturday) == 64)
            {
                days.Add((int)DaysOfTheWeek.Saturday);
            }

            return days;
        }

    }
}