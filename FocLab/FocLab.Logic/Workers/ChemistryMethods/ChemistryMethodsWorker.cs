using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Abstractions.ContextWrappers;
using Croco.Core.Common.Models;
using FocLab.Logic.Models.Methods;
using FocLab.Model.Contexts;
using FocLab.Model.Entities;
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
        public Task<ChemistryMethodFileModel> GetMethodAsync(string id)
        {
            return  Context.ChemistryMethodFiles.Select(ChemistryMethodFileModel.SelectExpression).FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Загрузить метод решения задачи
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> UploadMethodAsync(CreateChemistryMethod model)
        {
            var validation = ValidateModel(model);

            if(!validation.IsSucceeded)
            {
                return validation;
            }

            var repo = GetRepository<ChemistryMethodFile>();

            if (await repo.Query().AnyAsync(x => x.Name == model.Name))
            {
                return new BaseApiResponse(false, $"Метод решения с именем {model.Name} уже существует");
            }

            if(!await GetRepository<DbFile>().Query().AnyAsync(x => x.Id == model.FileId))
            {
                return new BaseApiResponse(false, "Файл не найден по указанному идентификатору");
            }
            
            var methodFile = new ChemistryMethodFile
            {
                CreationDate = DateTime.Now,
                FileId = model.FileId,
                Name = model.Name
            };

            repo.CreateHandled(methodFile);

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