namespace FocLab.Logic.Models.Tasks
{
    /// <summary>
    /// Модель для удаления химического задания
    /// </summary>
    public class ChemistryTaskRemoveOrCancelModel
    {
        /// <summary>
        /// Идентификатор задания
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Флаг удаленности
        /// </summary>
        public bool Flag { get; set; }
    }

}