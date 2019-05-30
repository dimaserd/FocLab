namespace FocLab.Logic.Workers.ChemistryTaskExperiments
{
    /// <summary>
    /// Модель завершения эксперимента
    /// </summary>
    public class PerformExperimentModel
    {
        /// <summary>
        /// Идентификатор эксперимента
        /// </summary>
        public string ExperimentId { get; set; }

        /// <summary>
        /// Флаг завершенности
        /// </summary>
        public bool Performed { get; set; }
    }
}