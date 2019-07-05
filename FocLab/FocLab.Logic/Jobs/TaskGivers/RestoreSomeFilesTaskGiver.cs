using CrocoShop.Logic.Jobs.TaskGivers;
using FocLab.Logic.Workers;
using System;
using System.Threading.Tasks;

namespace FocLab.Logic.Jobs.TaskGivers
{
    public class RestoreSomeFilesTaskGiver : BaseTaskGiver
    {
        int _countSetting = 5;

        public override void ExecuteTask()
        {
            GetTask().GetAwaiter().GetResult();
        }

        public async Task GetTask()
        {
            using (var contextWrapper = GetSystemPrincipalContextWrapper())
            {
                var fileWorker = new DbFileWorker(contextWrapper);

                await fileWorker.BaseManager.MakeLocalCopies(_countSetting);
            }
        }
    }
}
