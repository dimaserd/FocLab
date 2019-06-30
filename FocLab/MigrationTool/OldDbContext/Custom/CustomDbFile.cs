using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationTool.OldDbContext.Custom
{
    public class CustomDbFile
    {
        public CustomDbFile()
        {

        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        public string FileName { get; set; }
    }

    public class CustomDbFileDto
    {
        public CustomDbFileDto()
        {

        }

        public string Id { get; set; }

        public string FileName { get; set; }
    }
}