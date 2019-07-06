using Croco.Core.Abstractions.ContextWrappers;
using Croco.Core.Common.Models;
using Croco.Core.Logic.Workers;
using Croco.Core.Model.Entities.Store;
using FocLab.Model.Contexts;
using FocLab.Model.Entities.Chemistry;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MigrationTool.Tools
{
    public class AddSnaphotsForEntities : BaseCrocoWorker
    {
        public AddSnaphotsForEntities(IUserRequestWithRepositoryFactory context) : base(context)
        {
        }

        public async Task<BaseApiResponse> Execute()
        {
            var snapShotRepo = GetRepository<Snapshot>();

            var repo = GetRepository<ChemistryMethodFile>().Query().ToListAsync();

            return new BaseApiResponse(true, "Завершено");
        }
    }
}
