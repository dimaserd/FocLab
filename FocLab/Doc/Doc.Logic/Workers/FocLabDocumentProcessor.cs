using Croco.Core.Abstractions.ContextWrappers;
using Croco.Core.Application;
using Croco.Core.Common.Models;
using Croco.Core.Logic.Workers.Files;
using Croco.Core.Utils;
using Doc.Logic.Entities;
using Doc.Logic.Implementations;
using Doc.Logic.Models;
using FocLab.Logic.Models;
using FocLab.Logic.Workers;
using FocLab.Model.Contexts;
using FocLab.Model.Entities.Chemistry;
using FocLab.Model.Enumerations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doc.Logic.Workers
{

    /// <summary>
    /// Класс для процессинга документов
    /// </summary>
    public class FocLabDocumentProcessor : BaseChemistryWorker
    {
        public FocLabDocumentProcessor(IUserContextWrapper<ChemistryDbContext> contextWrapper) : base(contextWrapper)
        {
        }

        private string GetDocTemplateFilePath()
        {
            return CrocoApp.Application.MapPath("~/DocTemplates/Document.docx").Replace("/", "\\");
        }

        /// <summary>
        /// Срендерить документ для задания
        /// </summary>
        /// <param name="id">Идентификатор задания</param>
        /// <returns></returns>
        public async Task<BaseApiResponse> RanderByTaskIdAsync(RenderChemistryTaskDocument model)
        {
            var task = await GetRepository<ChemistryTask>().Query()
                .Include(x => x.Files)
                .Include(x => x.PerformerUser)
                .FirstOrDefaultAsync(x => x.Id == model.TaskId);

            if (task == null)
            {
                return new BaseApiResponse(false, "Задание не найдено по указанному идентификатору");
            }

            return RenderInner(task, GetDocTemplateFilePath(), model.DocSaveFileName);
        }

        /// <summary>
        /// Тест
        /// </summary>
        /// <param name="model"></param>
        private static BaseApiResponse RenderInner(ChemistryTask model, string docTemplateFileName, string docSaveFileName)
        {
            var substanceCounter = Tool.JsonConverter.Deserialize<Chemistry_SubstanceCounter>(model.SubstanceCounterJson);

            if (substanceCounter == null)
            {
                substanceCounter = Chemistry_SubstanceCounter.GetDefaultCounter();
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

            var file = model.Files.FirstOrDefault(x => x.Type == ChemistryTaskDbFileType.ReactionSchemaImage);

            var imgPath = file != null ? CrocoApp.Application.MapPath($"~/FileCopies/Images/Original/{file.FileId}.{FileCopyWorker.DefaultImgExtension}") : "";

            var docModel = new DocXDocumentObjectModel
            {
                Replaces = new Dictionary<string, string>
                {
                    { "{Title}", model.Title },
                    { "{PerformerQuality}", model.PerformerQuality },
                    { "{PerformerQuantity}", model.PerformerQuantity },
                    { "{AdminQuality}", model.AdminQuality },
                    { "{AdminQuantity}", model.AdminQuantity },
                    { "{PerformerName}", model.PerformerUser.Name },
                    { "{PerformerText}", model.PerformerText },
                },

                Tables = new List<DocumentTable>
                {
                    new DocumentTable
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
                    }
                },

                DocumentFileName = docTemplateFileName,

                ToReplaceImages = file != null ? new Dictionary<string, string>
                {
                    { "{PicturePlace}", imgPath }
                } : new Dictionary<string, string>(),

                DocumentSaveFileName = docSaveFileName,
            };

            if (file == null)
            {
                docModel.Replaces.Add("{PicturePlace}", "");
            }

            var proccessor = new WordDocumentProcessor(new WordDocumentProcessorOptions {
                Engine = new DocOpenFormatWordEngine()
            });
            return proccessor.RenderDocument(docModel);
        }
    }
}
