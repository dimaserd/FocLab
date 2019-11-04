using Croco.Core.Utils;
using System.Collections.Generic;
using System.Linq;
using Zoo.Doc.WordGen.Models;

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

        public static DocumentTable GetSubstanceDocumentTable(string substanceJson)
        {
            var substanceCounter = Tool.JsonConverter.Deserialize<Chemistry_SubstanceCounter>(substanceJson);

            if (substanceCounter == null)
            {
                substanceCounter = GetDefaultCounter();
            }

            substanceCounter.Substances.Insert(0, substanceCounter.Etalon);

            substanceCounter.Substances.ForEach(x =>
            {
                if (x.Koef == null)
                {
                    x.Koef = "";
                }

                if (x.Name == null)
                {
                    x.Name = "";
                }

                if (x.MolarMassa == null)
                {
                    x.MolarMassa = "";
                }

                if (x.Massa == null)
                {
                    x.Massa = "";
                }
            });

            return new DocumentTable
            {
                PlacingText = "{SubstancesTablePlace}",

                Header = new List<string>
                {
                    "Название вещества",
                    "Масса вещества (г)",
                    "Молярная масса (г / моль)",
                    "Коэфициент"
                },

                Data = substanceCounter.Substances.Select(x => new List<string>
                {
                    x.Name,
                    x.Massa,
                    x.MolarMassa,
                    x.Koef
                }).ToList()
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