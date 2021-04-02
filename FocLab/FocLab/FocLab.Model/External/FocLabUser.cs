using Microsoft.EntityFrameworkCore;

namespace FocLab.Model.External
{
    public class FocLabUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FocLabUser>().Property(x => x.Id).ValueGeneratedNever();
        }
    }
}