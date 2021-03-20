using Croco.Core.Contract;
using Croco.Core.Contract.Models;
using FocLab.Model.Contexts;
using FocLab.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MigrationTool.Tools
{
    public class AddDbFileHistory
    {
        public AddDbFileHistory(ICrocoAmbientContextAccessor contextAccessor)
        {
            Context = contextAccessor.GetAmbientContext<ChemistryDbContext>();
        }

        public ICrocoAmbientContext<ChemistryDbContext> Context { get; }

        public async Task<BaseApiResponse> Execute()
        {
            var count = 10;

            var doneBatchesCount = await ExecuteBatch(count);

            return new BaseApiResponse(true, $"Выполнено преобразований {doneBatchesCount}");
        }

        public Task<int> GetCountLeftAsync()
        {
            return Context.DataConnection
                .GetRepositoryFactory()
                .GetRepository<DbFile>().Query()
                .Where(x => !x.History.Any()).CountAsync();
        }

        public async Task<int> ExecuteBatch(int count)
        {
            var repoFactory = Context
                .DataConnection
                .GetRepositoryFactory();

            var filesWithNoHistory = repoFactory
                .GetRepository<DbFile>().Query().Where(x => !x.History.Any())
                .OrderBy(x => x.Id)
                .Take(count);
            
            var histories = filesWithNoHistory.Select(x => new ApplicationDbFileHistory
            {
                Id = Guid.NewGuid().ToString(),
                FileData = x.FileData,
                FileName = x.FileName,
                ParentId = x.Id
            }).ToList();

            var repo = repoFactory.GetRepository<ApplicationDbFileHistory>();

            repo.CreateHandled(histories);
            await repoFactory.SaveChangesAsync();

            return histories.Count;
        }
    }
}
