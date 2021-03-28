using Croco.Core.Contract;
using Croco.Core.Contract.Application;
using Croco.Core.Logic.Services;
using FocLab.Model.Contexts;
using FocLab.Model.Entities.Tasker;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Tms.Model;

namespace MigrationTool.Tools
{
    public class ExportOldTmsDataToTmsService : BaseCrocoService<ChemistryDbContext>
    {
        private readonly int BatchSize = 50;

        TmsDbContext TmsDbContext { get; }

        public ExportOldTmsDataToTmsService(ICrocoAmbientContextAccessor context, 
            ICrocoApplication application, TmsDbContext tmsDbContext) : base(context, application)
        {
            TmsDbContext = tmsDbContext;
        }

        public async Task<CurrentState> GetState()
        {
            return new CurrentState
            {
                NewTmsServiceTasksCount = await TmsDbContext.DayTasks.CountAsync(),
                OldTmsServiceTasksCount = await Query<ApplicationDayTask>().CountAsync()
            };
        }

        public async Task<int> PasteData()
        {
            var set = TmsDbContext.DayTasks;

            var count = await Query<ApplicationDayTask>().CountAsync();

            var setting = Application.SettingsFactory.GetSetting<ExportOldTmsDataToTmsServiceState>();

            int counter = setting.Count;

            var skip = counter * BatchSize;

            var data = await Query<ApplicationDayTask>()
                    .OrderBy(x => x.CreatedOn)
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

            await TmsDbContext.SaveChangesAsync();

            counter++;
            setting.Count = counter;
            await Application.SettingsFactory.UpdateSettingAsync(setting);

            return count - (counter + 1) * BatchSize;
        }

        public class CurrentState
        {
            public int NewTmsServiceTasksCount { get; set; }
            public int OldTmsServiceTasksCount { get; set; }
        }

        public class ExportOldTmsDataToTmsServiceState
        {
            public int Count { get; set; }
        }

        private Tms.Model.Entities.DayTask ToDayTask(ApplicationDayTask task)
        {
            return new Tms.Model.Entities.DayTask
            {
                AssigneeUserId = task.AssigneeUserId,
                AuthorId = task.AuthorId,
                CompletionSeconds = 0,
                TaskComment = task.TaskComment,
                EstimationSeconds = 0,
                FinishDate = task.FinishDate,
                TaskReview = task.TaskReview,
                CreatedOn = task.CreatedOn,
                CreatedBy = task.CreatedBy,
                TaskTitle = task.TaskTitle,
                Id = task.Id,
                TaskText = task.TaskText,
                TaskTarget = task.TaskTarget,
                TaskDate = task.TaskDate
            };
        }
    }
}