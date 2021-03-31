using NewFocLab.Logic.Models.Users;

namespace FocLab.Logic.Models.Tasks
{
    public class ChemistryTaskSimpleModel
    {
        public string Id { get; set; }

        /// <summary>
        /// Текст задачи
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Исполнитель задания
        /// </summary>
        public UserModelBase PerformerUser { get; set; }
    }
}
