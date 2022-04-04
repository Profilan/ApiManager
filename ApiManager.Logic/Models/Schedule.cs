using ApiManager.Logic.Common;
using APIManager.Logic.Enums;
using System;

namespace ApiManager.Logic.Models
{
    public abstract class Schedule : Entity<Guid>
    {
        /// <summary>
        /// Gets or sets a Boolean value that indicates whether the schedule is enabled.
        /// </summary>
        public virtual bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the trigger is activated.
        /// </summary>
        public virtual DateTime StartBoundery { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the trigger is deactivated. The trigger cannot start the task after it is deactivated.
        /// </summary>
        public virtual DateTime EndBoundery { get; set; }

        /// <summary>
        /// Gets or sets the maximum amount of time that the task launched by this trigger is allowed to run. 
        /// </summary>
        public virtual TimeSpan ExecutionTimeLimit { get; set; }

        public virtual bool Repeat { get; set; }

        /// <summary>
        /// Gets a RepetitionPattern instance that indicates how often the task is run and how long the repetition pattern is repeated after the task is started.
        /// </summary>
        public virtual RepetitionPattern Repetition { get; set; }

        /// <summary>
        /// Gets or sets the task to be scheduled
        /// </summary>
        public virtual SchedulerTask Task { get; set; }

        public virtual void SetTask(SchedulerTask newTask)
        {
            var prevTask = Task;

            if (newTask == prevTask)
                return;

            Task = newTask;

            if (prevTask != null)
                prevTask.RemoveSchedule(this);

            if (newTask != null)
                newTask.AddSchedule(this);

        }
    }
}
