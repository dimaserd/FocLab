using FocLab.Logic.Models.Reagents;

namespace FocLab.Logic.Models.Tasks
{
    public class ChemistryTaskReagentModel
    {
        /// <summary>
        /// Химический реагент
        /// </summary>
        public ChemistryReagentNameAndIdModel Reagent { get; set; }

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
