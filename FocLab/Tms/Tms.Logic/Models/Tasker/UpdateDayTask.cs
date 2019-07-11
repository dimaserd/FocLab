namespace Tms.Logic.Models.Tasker
{
    public class UpdateDayTask : CreateDayTask
    {
        public string Id { get; set; }

        /// <summary>
        /// Оценка исполнителя
        /// </summary>
        public int EstimationSeconds { get; set; }

    }
}
