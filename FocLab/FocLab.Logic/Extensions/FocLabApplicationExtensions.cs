using Croco.Core.Abstractions;
using FocLab.Logic.Implementations;
using FocLab.Model.Contexts;

namespace FocLab.Logic.Extensions
{
    public static class FocLabApplicationExtensions
    {
        public static ChemistryDbContext GetChemistryDbContext(this ICrocoApplication application)
        {
            return application.GetDbContext() as ChemistryDbContext;
        }

        public static bool IsDevelopment(this ICrocoApplication application)
        {
            return ((FocLabWebApplication)application).IsDevelopment;
        }
    }
}
