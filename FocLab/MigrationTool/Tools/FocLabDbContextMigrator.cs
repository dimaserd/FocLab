using Croco.Core.Contract;
using Croco.Core.Contract.Application;
using FocLab.Logic.Implementations;
using FocLab.Model.Contexts;
using FocLab.Model.Entities;
using NewFocLab.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationTool.Tools
{
    public class FocLabDbContextMigrator : FocLabWorker
    {
        public int BatchSize = 10;

        public FocLabDbContext FocLabDbContext { get; }

        public ChemistryDbContext ChemistryDb { get; }

        public FocLabDbContextMigrator(ICrocoAmbientContextAccessor context, ICrocoApplication application) : base(context, application)
        {
            FocLabDbContext = context.GetAmbientContext<FocLabDbContext>()
                .DataConnection
                .ConnectionContext;

            ChemistryDb = AmbientContext.DataConnection.ConnectionContext;
        }


        public Task CopyData()
        {
            return Task.CompletedTask;
        }
    }
}
