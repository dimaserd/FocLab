using Croco.Core.Common.Enumerations;
using Croco.Core.Contract;
using Croco.Core.Contract.Application;
using Croco.Core.Contract.Models;
using Doc.Logic.Word.Abstractions;
using Doc.Logic.Word.Models;
using FocLab.Logic.Implementations;
using FocLab.Logic.Models;
using FocLab.Logic.Models.Doc;
using FocLab.Model.Entities.Chemistry;
using FocLab.Model.Enumerations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FocLab.Implementations.Doc
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
                    Chemistry_SubstanceCounter.GetSubstanceDocumentTable(substanceCounterJson),
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