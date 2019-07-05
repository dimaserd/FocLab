using FocLab.Logic.Jobs.TaskGivers;
using Hangfire;
using Hangfire.Common;
using System.Collections.Generic;

namespace FocLab.Logic.Jobs
{
    public class ApplicationJobManager
    {
        private static IEnumerable<ApplicationJob> GetJobs()
        {
            return new List<ApplicationJob>
            {
               new ApplicationJob
               {
                   Id = "restoreFiles",
                   Job = Job.FromExpression(() => new RestoreSomeFilesTaskGiver().ExecuteTask()),
                   CronExpression = Cron.Minutely()
               }
            };
        }

        public static void UpdateJobs()
        {
            var manager = new RecurringJobManager();

            var jobs = GetJobs();

            foreach (var job in jobs)
            {
                manager.AddOrUpdate(job.Id, job.Job, job.CronExpression);
            }
        }
    }
}
