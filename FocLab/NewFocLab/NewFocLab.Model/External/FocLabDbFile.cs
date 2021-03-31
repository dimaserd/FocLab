using Microsoft.EntityFrameworkCore;

namespace NewFocLab.Model.External
{
    /// <summary>
    /// Класс описывающий файл который находится в базе данных
    /// </summary>
    public class FocLabDbFile
    {
        public int Id { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FocLabDbFile>().Property(x => x.Id).ValueGeneratedNever();
        }
    }
}