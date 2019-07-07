namespace FocLab.Logic.Models.Reagents
{
    public class CreateOrUpdateTaskReagent : TaskReagentIdModel
    {
        /// <summary>
        /// Взятое кол-во
        /// </summary>
        public decimal TakenQuantity { get; set; }

        /// <summary>
        /// Колличество которое вернули
        /// </summary>
        public decimal ReturnedQuantity { get; set; }
    }
}
