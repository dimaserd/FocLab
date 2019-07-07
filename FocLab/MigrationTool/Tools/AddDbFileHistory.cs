using Croco.Core.Abstractions.ContextWrappers;
using Croco.Core.Common.Models;
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
        public AddDbFileHistory(IUserContextWrapper<ChemistryDbContext> context)
        {
            Context = context;
        }

        public IUserContextWrapper<ChemistryDbContext> Context { get; }

        public async Task<BaseApiResponse> Execute()
        {
            int count = 10;

            var doneBatchesCount = await ExecuteBatch(count);

            return new BaseApiResponse(true, $"Выполнено преобразований {doneBatchesCount}");
        }

        public Task<int> GetCountLeftAsync()
        {
            return Context.DbContext.DbFiles.Where(x => !x.History.Any()).CountAsync();
        }

        public async Task<int> ExecuteBatch(int count)
        {
            var filesWithNoHistory = Context.DbContext.DbFiles.Where(x => !x.History.Any())
                .OrderBy(x => x.Id)
                .Take(count);
            
            var histories = filesWithNoHistory.Select(x => new ApplicationDbFileHistory
            {
                Id = Guid.NewGuid().ToString(),
                FileData = x.FileData,
                FileName = x.FileName,
                FilePath = x.FilePath,
                ParentId = x.Id
            }).ToList();

            var repo = Context.GetRepository<ApplicationDbFileHistory>();

            repo.CreateHandled(histories);
            await Context.SaveChangesAsync();

            return histories.Count;
        }
    }
}
