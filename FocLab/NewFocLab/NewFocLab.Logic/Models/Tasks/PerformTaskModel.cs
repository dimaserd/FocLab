namespace FocLab.Logic.Models.Tasks
{
    /// <summary>
    /// Класс описыващий модель для завершения или отмены завершенности задания
    /// </summary>
    public class PerformTaskModel
    {
        /// <summary>
        /// Идентификатор задания
        /// </summary>
        public string TaskId { get; set; }

        /// <summary>
        /// Флаг завершенности
        /// </summary>
        public bool IsPerformed { get; set; }
    }
}