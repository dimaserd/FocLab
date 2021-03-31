using System.ComponentModel.DataAnnotations;

namespace FocLab.Logic.Models.Experiments
{
    public class CreateExperiment
    {
        [Display(Name = "Задание")]
        public string TaskId { get; set; }

        [Display(Name = "Название эксперимента")]
        public string Title { get; set; }
    }
}
