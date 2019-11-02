using Croco.Core.Abstractions;
using Croco.Core.Common.Enumerations;
using Croco.Core.Logic.Workers;
using Croco.Core.Models;
using Croco.Core.Utils;
using Doc.Logic.Entities;
using Doc.Logic.Models;
using FocLab.Logic.Models;
using FocLab.Model.Entities.Chemistry;
using FocLab.Model.Enumerations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zoo.Doc.WordGen.Implementations;
using Zoo.Doc.WordGen.Models;
using Zoo.Doc.WordGen.Workers;

namespace Doc.Logic.Workers
{
    public class ChemistryTaskDocumentProccessor : BaseCrocoWorker
    {
        public ChemistryTaskDocumentProccessor(ICrocoAmbientContext context) : base(context)
        {
        }

        private string GetDocTemplateFilePath()
        {
            return Application.MapPath("~/wwwroot/DocTemplates/Document.docx");
        }

        /// <summary>
        /// Срендерить документ для задания
        /// </summary>
        /// <param name="id">Идентификатор задания</param>
        /// <returns></returns>
        public async Task<BaseApiResponse> RanderByTaskIdAsync(RenderChemistryTaskDocument model)
        {
            var task = await Query<ChemistryTask>()
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
        private BaseApiResponse RenderInner(ChemistryTask model, string docTemplateFileName, string docSaveFileName)
        {
            var file = model.Files.FirstOrDefault(x => x.Type == ChemistryTaskDbFileType.ReactionSchemaImage);

            var docModel = GetDocumentObjectModel(docTemplateFileName, docSaveFileName,
                model.SubstanceCounterJson, GetDocumentReplacesDicitonaryByTask(model), file);

            var proccessor = new WordDocumentProcessor(new WordDocumentProcessorOptions
            {
                Engine = new DocOpenFormatWordEngine()
            });

            return proccessor.RenderDocument(docModel);
        }


        private DocXDocumentObjectModel GetDocumentObjectModel(string docTemplateFileName, string docSaveFileName, string substanceCounterJson, Dictionary<string, string> replaceDict, ChemistryTaskDbFile file)
        {
            var res = new DocXDocumentObjectModel
            {
                Replaces = replaceDict,

                Tables = new List<DocumentTable>
                {
                    GetSubstanceDocumentTable(substanceCounterJson)
                },

                DocumentTemplateFileName = docTemplateFileName,

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

        private static DocumentTable GetSubstanceDocumentTable(string substanceJson)
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

        private static Dictionary<string, string> GetDocumentReplacesDicitonaryByTask(ChemistryTask model)
        {
            return new Dictionary<string, string>
            {
                ["{Title}"] = model.Title,
                ["{PerformerQuality}"] = model.PerformerQuality,
                ["{PerformerQuantity}"] = model.PerformerQuantity,
                ["{AdminQuality}"] = model.AdminQuality,
                ["{AdminQuantity}"] = model.AdminQuantity,
                ["{PerformerName}"] = model.PerformerUser.Name,
                ["{PerformerText}"] = model.PerformerText
            };
        }
    }
}
