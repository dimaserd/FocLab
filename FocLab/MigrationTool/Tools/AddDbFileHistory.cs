using Croco.Core.Abstractions;
using Croco.Core.Models;
using FocLab.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MigrationTool.Tools
{
    public class AddDbFileHistory
    {
        public AddDbFileHistory(ICrocoAmbientContext context)
        {
            Context = context;
        }

        public ICrocoAmbientContext Context { get; }

        public async Task<BaseApiResponse> Execute()
        {
            var count = 10;

            var doneBatchesCount = await ExecuteBatch(count);

            return new BaseApiResponse(true, $"Выполнено преобразований {doneBatchesCount}");
        }

        public Task<int> GetCountLeftAsync()
        {
            return Context.RepositoryFactory
                .GetRepository<DbFile>().Query()
                .Where(x => !x.History.Any()).CountAsync();
        }

        public async Task<int> ExecuteBatch(int count)
        {
            var filesWithNoHistory = Context.RepositoryFactory.GetRepository<DbFile>().Query().Where(x => !x.History.Any())
                .OrderBy(x => x.Id)
                .Take(count);
            
            var histories = filesWithNoHistory.Select(x => new ApplicationDbFileHistory
            {
                Id = Guid.NewGuid().ToString(),
                Data = x.Data,
                FileName = x.FileName,
                FilePath = x.FilePath,
                ParentId = x.Id
            }).ToList();

            var repo = Context.RepositoryFactory.GetRepository<ApplicationDbFileHistory>();

            repo.CreateHandled(histories);
            await Context.RepositoryFactory.SaveChangesAsync();

            return histories.Count;
        }
    }
}
