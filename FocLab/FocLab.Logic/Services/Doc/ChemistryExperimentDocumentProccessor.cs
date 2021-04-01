using Croco.Core.Common.Enumerations;
using Croco.Core.Contract;
using Croco.Core.Contract.Application;
using Croco.Core.Contract.Models;
using Croco.Core.Utils;
using Doc.Logic.Word.Abstractions;
using Doc.Logic.Word.Models;
using FocLab.Logic.Implementations;
using FocLab.Logic.Models;
using FocLab.Logic.Models.Doc;
using Microsoft.EntityFrameworkCore;
using NewFocLab.Model.Entities;
using NewFocLab.Model.Enumerations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FocLab.Logic.Services.Doc
{
    public class ChemistryExperimentDocumentProccessor : FocLabWorker
    {
        IWordProccessorEngine WordProccessorEngine { get; }

        public ChemistryExperimentDocumentProccessor(ICrocoAmbientContextAccessor context,
            ICrocoApplication application, IWordProccessorEngine wordProccessorEngine) : base(context, application)
        {
            WordProccessorEngine = wordProccessorEngine;
        }

        private string GetDocTemplateFilePath()
        {
            return Application.MapPath("~/wwwroot/DocTemplates/ExperimentTemplate.docx");
        }

        /// <summary>
        /// Срендерить документ для задания
        /// </summary>
        /// <param name="id">Идентификатор задания</param>
        /// <returns></returns>
        public async Task<BaseApiResponse> RanderExperimentByIdAsync(RenderChemistryExperimentDocument model)
        {
            var task = await Query<ChemistryTaskExperiment>()
                .Include(x => x.Files)
                .Include(x => x.Performer)
                .Include(x => x.ChemistryTask)
                .FirstOrDefaultAsync(x => x.Id == model.ExperimentId);

            if (task == null)
            {
                return new BaseApiResponse(false, "Эксперимент не найден по указанному идентификатору");
            }

            return RenderInner(task, model.DocSaveFileName);
        }

        /// <summary>
        /// Тест
        /// </summary>
        /// <param name="model"></param>
        private BaseApiResponse RenderInner(ChemistryTaskExperiment model, string docSaveFileName)
        {
            var file = model.Files.FirstOrDefault(x => x.Type == ChemistryTaskDbFileType.ReactionSchemaImage);

            var docModel = GetDocumentObjectModel(docSaveFileName,
                model.SubstanceCounterJson, GetDocumentReplacesDicitonaryByExperiment(model), file);

            return WordProccessorEngine.ProccessTemplate(docModel);
        }


        private DocXDocumentObjectModel GetDocumentObjectModel(string docSaveFileName, string substanceCounterJson, Dictionary<string, string> replaceDict, ChemistryTaskExperimentFile file)
        {
            var res = new DocXDocumentObjectModel
            {
                Replaces = replaceDict,

                Tables = new List<DocumentTable>
                {
                    GetSubstanceDocumentTable(substanceCounterJson),
                },

                DocumentTemplateFileName = GetDocTemplateFilePath(),

                ToReplaceImages = file != null ? new List<DocxImageReplace>
                {
                    new DocxImageReplace
                    {
                        TextToReplace = "{PicturePlace}",
                        ImageFilePath = Application.FileCopyWorker.GetResizedImageLocalPath(file.FileId, ImageSizeType.Original),
                    }
                } : new List<DocxImageReplace>(),

                DocumentSaveFileName = docSaveFileName,
            };

            if (file == null)
            {
                res.Replaces.Add("{PicturePlace}", "");
            }

            return res;
        }

        public static DocumentTable GetSubstanceDocumentTable(string substanceJson)
        {
            var substanceCounter = Tool.JsonConverter.Deserialize<Chemistry_SubstanceCounter>(substanceJson);

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


        private static Dictionary<string, string> GetDocumentReplacesDicitonaryByExperiment(ChemistryTaskExperiment model)
        {
            return new Dictionary<string, string>
            {
                ["{Title}"] = model.Title ?? "[Без названия]",
                ["{TaskTitle}"] = model.ChemistryTask.Title,
                ["{PerformerName}"] = model.Performer.Name,
                ["{PerformerText}"] = model.PerformerText
            };
        }
    }
}