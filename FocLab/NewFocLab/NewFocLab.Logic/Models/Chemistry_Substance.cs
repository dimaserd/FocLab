namespace FocLab.Logic.Models
{
    /// <summary>
    /// Химическое вещество
    /// </summary>
    public class Chemistry_Substance
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Масса
        /// </summary>
        public string Massa { get; set; }

        /// <summary>
        /// Молярная масса
        /// </summary>
        public string MolarMassa { get; set; }

        /// <summary>
        /// Коефициент
        /// </summary>
        // ReSharper disable once IdentifierTypo
        public string Koef { get; set; }
    }
}