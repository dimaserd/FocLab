using CrocoShop.Logic.Jobs.TaskGivers;
using System.Threading.Tasks;

namespace FocLab.Logic.Jobs.TaskGivers
{
    /// <summary>
    /// Здесь нужно назодить исключения возникшие как на сервере так и на UI и сообщать о них разработчику
    /// </summary>
    public class ErrorAuditTaskGiver : BaseTaskGiver
    {
        public override void ExecuteTask()
        {
            AsyncTask().GetAwaiter().GetResult();
        }

        public Task AsyncTask()
        {
            return GetTask();
        }

        public Task GetTask()
        {
            return Task.CompletedTask;
        }
    }
}
