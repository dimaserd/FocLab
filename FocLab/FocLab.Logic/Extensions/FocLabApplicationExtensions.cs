using Croco.Core.Application;
using FocLab.Logic.Implementations;
using FocLab.Model.Contexts;

namespace FocLab.Logic.Extensions
{
    public static class FocLabApplicationExtensions
    {
        public static ChemistryDbContext GetChemistryDbContext(this CrocoApplication application)
        {
            return application.GetDbContext() as ChemistryDbContext;
        }

        public static bool IsDevelopment(this CrocoApplication application)
        {
            return ((FocLabWebApplication)application).IsDevelopment;
        }
    }
}
