using Croco.Core.Implementations.AmbientContext;
using FocLab.Logic.Workers;
using System.Threading.Tasks;

namespace FocLab.Logic.Jobs.TaskGivers
{
    public class RestoreSomeFilesTaskGiver : BaseTaskGiver
    {
        int _countSetting = 15;

        public override void ExecuteTask()
        {
            GetTask().GetAwaiter().GetResult();
        }

        public async Task GetTask()
        {
            var ambientContext = new SystemCrocoAmbientContext();

            var fileWorker = new DbFileWorker(ambientContext);

            await fileWorker.BaseManager.MakeLocalCopies(_countSetting);
        }
    }
}
