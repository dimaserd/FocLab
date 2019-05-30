using Croco.Core.Abstractions.RepositoryFactory;
using Croco.Core.Logic.Workers.Files;
using FocLab.Model.Entities;

namespace FocLab.Logic.Workers
{
    public class ApplicationFileManager : DbFileManager<DbFile, ApplicationDbFileHistory>
    {
        public ApplicationFileManager(IRepositoryFactory repositoryFactory) : base(repositoryFactory)
        {
        }
    }
}
