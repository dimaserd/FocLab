using Croco.Core.Contract;
using Croco.Core.Contract.Application;
using FocLab.Logic.Implementations;
using FocLab.Model.Entities.Chemistry;
using FocLab.Model.Entities.Tasker;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MigrationTool.Tools
{
    public class ExportDayTaskToTmsService : FocLabWorker
    {
        private readonly int BatchSize = 50;

        public ExportDayTaskToTmsService(ICrocoAmbientContextAccessor context, ICrocoApplication application) : base(context, application)
        {
        }

        public async Task<int> PasteData()
        {
            var db = this.AmbientContext.DataConnection.ConnectionContext;

            var set = db.DayTasks;

            var count = await Query<ChemistryDayTask>().CountAsync();

            var setting = Application.SettingsFactory.GetSetting<ExportDayTaskState>();

            int counter = setting.Count;

            var skip = counter * BatchSize;

            var data = await Query<ChemistryDayTask>()
                    .OrderBy(x => x.CreationDate)
                    .Skip(skip)
                    .Take(BatchSize)
                    .ToListAsync();

            if (data.Count == 0)
            {
                return 0;
            }

            var dataToAdd = data
                .Select(ToDayTask)
                .ToList();

            set.AddRange(dataToAdd);

            await db.SaveChangesAsync();

            counter++;
            setting.Count = counter;
            await Application.SettingsFactory.UpdateSettingAsync(setting);

            return count - (counter + 1) * BatchSize;
        }

        public class ExportDayTaskState
        {
            public int Count { get; set; }
        }

        private ApplicationDayTask ToDayTask(ChemistryDayTask task)
        {
            return new ApplicationDayTask
            {
                AssigneeUserId = task.AssigneeUserId,
                AuthorId = task.AdminId,
                CompletionSeconds = 0,
                TaskComment = task.TaskCommentHtml,
                EstimationSeconds = 0,
                FinishDate = task.FinishDate,
                TaskReview = task.TaskReviewHtml,
                CreatedOn = task.CreationDate,
                CreatedBy = task.AdminId,
                TaskTitle = task.TaskTitle,
                Id = task.Id,
                TaskText = task.TaskText,
                TaskTarget = task.TaskTargetHtml,
                TaskDate = task.TaskDate,
                Seconds = 0,
            };
        }
    }
}