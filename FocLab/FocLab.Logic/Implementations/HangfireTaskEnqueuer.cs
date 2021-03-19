using Hangfire;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FocLab.Logic.Implementations
{
    public class HangfireTaskEnqueuer : ICrocoTaskEnqueuer
    {
        public void Enqueue(Expression<Func<Task>> methodCall)
        {
            BackgroundJob.Enqueue(methodCall);
        }
    }
}
