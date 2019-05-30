using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Croco.Core.Abstractions;
using Croco.Core.Abstractions.ContextWrappers;
using Croco.Core.Common.Models;
using FocLab.Model.Contexts;
using FocLab.Model.Entities.Chemistry;
using Microsoft.EntityFrameworkCore;

namespace FocLab.Logic.Workers.ChemistryMethods
{
    /// <summary>
    /// Рабочий класс для методов решения химических задач
    /// </summary>
    public class ChemistryMethodsWorker : BaseChemistryWorker
    {
        /// <summary>
        /// Получить метод по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ChemistryMethodFile> GetMethodAsync(string id)
        {
            return  Context.ChemistryMethodFiles.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Загрузить метод решения задачи
        /// </summary>
        /// <param name="model"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> UploadMethodAsync(IFileData model, string methodName)
        {
            if (await Context.ChemistryMethodFiles.AnyAsync(x => x.Name == methodName))
            {
                return new BaseApiResponse(false, $"Метод решения с именем {methodName} уже существует");
            }

            if (model == null)
            {
                return new BaseApiResponse(false, "Файл не подан");
            }

            var fileWorker = new DbFileWorker(ApplicationContextWrapper);

            var fileUploadResult = await fileWorker.UploadUserFileAsync(model);

            if (!fileUploadResult.IsSucceeded)
            {
                return new BaseApiResponse(fileUploadResult.IsSucceeded, fileUploadResult.Message);
            }

            
            var methodFile = new ChemistryMethodFile
            {
                CreationDate = DateTime.Now,
                FileId = fileUploadResult.ResponseObject.Id,
                Id = Guid.NewGuid().ToString(),
                Name = methodName
            };

            Context.ChemistryMethodFiles.Add(methodFile);

            return await TrySaveChangesAndReturnResultAsync("Метод решения химической задачи создан");
        }

        /// <summary>
        /// Получить методы решения заданий
        /// </summary>
        /// <returns></returns>
        public Task<List<ChemistryMethodFile>> GetTaskMethodsAsync()
        {
            return Context.ChemistryMethodFiles.ToListAsync();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="contextWrapper"></param>
        public ChemistryMethodsWorker(IUserContextWrapper<ChemistryDbContext> contextWrapper) : base(contextWrapper)
        {
        }
    }
}