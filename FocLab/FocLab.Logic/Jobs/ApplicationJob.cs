using Hangfire.Common;

namespace FocLab.Logic.Jobs
{
    public class ApplicationJob
    {
        public string Id { get; set; }

        public Job Job { get; set; }

        public string CronExpression { get; set; }
    }
}
