using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Abstractions;
using Croco.Core.Models;
using FocLab.Logic.Extensions;
using FocLab.Logic.Implementations;
using FocLab.Logic.Models.Methods;
using FocLab.Model.Entities;
using FocLab.Model.Entities.Chemistry;
using Microsoft.EntityFrameworkCore;

namespace FocLab.Logic.Workers.ChemistryMethods
{
    /// <summary>
    /// Рабочий класс для методов решения химических задач
    /// </summary>
    public class ChemistryMethodsWorker : FocLabWorker
    {
        /// <summary>
        /// Получить метод по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ChemistryMethodFileModel> GetMethodAsync(string id)
        {
            return Query<ChemistryMethodFile>().Select(ChemistryMethodFileModel.SelectExpression).FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Получить все методы
        /// </summary>
        /// <returns></returns>
        public Task<List<ChemistryMethodFileModel>> GetMethodsAsync()
        {
            return GetRepository<ChemistryMethodFile>().Query()
                .OrderByDescending(x => x.CreationDate)
                .Select(ChemistryMethodFileModel.SelectExpression)
                .ToListAsync();
        }

        /// <summary>
        /// Загрузить метод решения задачи
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> CreateMethodAsync(CreateChemistryMethod model)
        {
            var validation = ValidateModel(model);

            if(!validation.IsSucceeded)
            {
                return validation;
            }

            if(!User.IsAdmin())
            {
                return new BaseApiResponse(false, "Только администраторы имеют право создавать и редактирвать методы");
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

        public async Task<BaseApiResponse> EditMethodAsync(EditChemistryMethod model)
        {
            var validation = ValidateModel(model);

            if (!validation.IsSucceeded)
            {
                return validation;
            }

            if (!User.IsAdmin())
            {
                return new BaseApiResponse(false, "Только администраторы имеют право создавать и редактирвать методы");
            }

            var repo = GetRepository<ChemistryMethodFile>();

            var method = await repo.Query().FirstOrDefaultAsync(x => x.Id == model.Id);

            if(method == null)
            {
                return new BaseApiResponse(false, "Метод не найден по указанному идентифиатору");
            }

            method.Name = model.Name;

            repo.UpdateHandled(method);

            return await TrySaveChangesAndReturnResultAsync("Название метода изменено");
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="contextWrapper"></param>
        public ChemistryMethodsWorker(ICrocoAmbientContext context) : base(context)
        {
        }
    }
}