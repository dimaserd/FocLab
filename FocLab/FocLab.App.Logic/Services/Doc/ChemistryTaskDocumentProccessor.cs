﻿using Croco.Core.Common.Enumerations;
using Croco.Core.Contract;
using Croco.Core.Contract.Application;
using Croco.Core.Contract.Models;
using Doc.Logic.Word.Abstractions;
using Doc.Logic.Word.Models;
using FocLab.App.Logic.Implementations;
using FocLab.App.Logic.Models.Doc;
using Microsoft.EntityFrameworkCore;
using FocLab.Model.Entities;
using FocLab.Model.Enumerations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FocLab.App.Logic.Services.Doc
{
    public class ChemistryTaskDocumentProccessor : FocLabWorker
    {
        IWordProccessorEngine ProccessorEngine { get; }

        public ChemistryTaskDocumentProccessor(ICrocoAmbientContextAccessor context,
            ICrocoApplication app,
            IWordProccessorEngine proccessorEngine) : base(context, app)
        {
            ProccessorEngine = proccessorEngine;
        }

        private string GetDocTemplateFilePath()
        {
            return Application.MapPath("~/wwwroot/DocTemplates/TaskTemplate.docx");
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

            return RenderInner(task, model.DocSaveFileName);
        }

        /// <summary>
        /// Тест
        /// </summary>
        /// <param name="model"></param>
        private BaseApiResponse RenderInner(ChemistryTask model, string docSaveFileName)
        {
            var file = model.Files.FirstOrDefault(x => x.Type == ChemistryTaskDbFileType.ReactionSchemaImage);

            var docModel = GetDocumentObjectModel(docSaveFileName,
                model.SubstanceCounterJson, GetDocumentReplacesDicitonaryByTask(model), file);

            return ProccessorEngine.ProccessTemplate(docModel);
        }

        private DocXDocumentObjectModel GetDocumentObjectModel(string docSaveFileName, string substanceCounterJson, Dictionary<string, string> replaceDict, ChemistryTaskDbFile file)
        {
            var res = new DocXDocumentObjectModel
            {
                Replaces = replaceDict,

                Tables = new List<DocumentTable>
                {
                    ChemistryExperimentDocumentProccessor.GetSubstanceDocumentTable(substanceCounterJson)
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