using System.Collections.Generic;

namespace FocLab.Logic.Models
{
    /// <summary>
    /// Счетчик веществ
    /// </summary>
    public class Chemistry_SubstanceCounter
    {
        /// <summary>
        /// Получить счетчик по-умолчанию
        /// </summary>
        /// <returns></returns>
        public static Chemistry_SubstanceCounter GetDefaultCounter()
        {
            return new Chemistry_SubstanceCounter
            {
                Etalon = new Chemistry_Substance
                {
                    Koef = 1.ToString(),
                    Massa = 1.ToString(),
                    MolarMassa = 1.ToString(),
                    Name = "Эталонное вещество"
                },

                Substances = new List<Chemistry_Substance>()
            };
        }

        /// <summary>
        /// Эталонное вещество
        /// </summary>
        // ReSharper disable once IdentifierTypo
        public Chemistry_Substance Etalon { get; set; }

        /// <summary>
        /// Вещества
        /// </summary>
        public List<Chemistry_Substance> Substances { get; set; }
    }
}