using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Abstractions.ContextWrappers;
using Croco.Core.Application;
using Croco.Core.Common.Models;
using Croco.Core.Utils;
using FocLab.Logic.Abstractions;
using FocLab.Logic.EntityDtos;
using FocLab.Logic.Extensions;
using FocLab.Logic.Implementations;
using FocLab.Logic.Models;
using FocLab.Logic.Models.Experiments;
using FocLab.Logic.Settings.Statics;
using FocLab.Logic.Workers.Users;
using FocLab.Model.Contexts;
using FocLab.Model.Entities.Chemistry;
using Microsoft.EntityFrameworkCore;

namespace FocLab.Logic.Workers.ChemistryTaskExperiments
{
    /// <summary>
    /// Статический класс предоставляющий методы для работы с экспериментами
    /// </summary>
    public class ChemistryTaskExperimentsWorker : BaseChemistryWorker
    {
        /// <summary>
        /// Завершение эксперимента
        /// </summary>
        /// <param name="model"></param>
        /// <param name="mailSender"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> PerformExperimentAsync(PerformExperimentModel model, IUserMailSender mailSender)
        {
            if (model == null)
            {
                return new BaseApiResponse(false, "Вы подали пустую модель");
            }

            var experiment = await GetRepository<ChemistryTaskExperiment>().Query().FirstOrDefaultAsync(x => x.Id == model.ExperimentId);

            if (experiment == null)
            {
                return new BaseApiResponse(false, "Эксперимент не найден по указанному идентификатору");
            }

            if (experiment.PerformerId != UserId)
            {
                return new BaseApiResponse(false, "Вы не являетесь исполнителем данного эксперимента");
            }

            if (experiment.PerformedDate.HasValue && model.Performed)
            {
                return new BaseApiResponse(false, "Эксперимент уже является исполненным");
            }

            if (!experiment.PerformedDate.HasValue && !model.Performed)
            {
                return new BaseApiResponse(false, "Эксперимент и так не является исполненным");
            }

            Context.Entry(experiment).State = EntityState.Modified;

            var userMeId = UserId;

            var userMe = await Context.Users.FirstOrDefaultAsync(x => x.Id == userMeId);

            if (model.Performed)
            {
                experiment.PerformedDate = DateTime.Now;

                await Context.SaveChangesAsync();

                var domainName = ((FocLabWebApplication)CrocoApp.Application).DomainName;

                var searcher = new UserSearcher(ApplicationContextWrapper);

                var user = await searcher.GetUserByEmailAsync(ChemistryAdminSettings.AdminEmail);

                await mailSender.SendMailUnSafeAsync(new SendMailMessage
                {
                    Body = $"<p>Пользователь {userMe.Email} завершил <a href='{domainName}/Chemistry/Experiments/Experiment/{experiment.Id}'>эксперимент</a>.</p>",
                    UserId = user.Id,
                    Subject = "Эксперимент завершен"
                });

                return new BaseApiResponse(true, "Вы завершили эксперимент");
            }

            experiment.PerformedDate = null;

            return await TrySaveChangesAndReturnResultAsync("Вы отменили заверешение эксперимента");
        }

        
        /// <summary>
        /// Обновить заголовок эксперимента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> UpdateExperimentTitleAsync(ChemistryTaskExperimentDto model)
        {
            if(model == null)
            {
                return new BaseApiResponse(false, "Вы подали пустую модель");
            }

            var experiment = await GetRepository<ChemistryTaskExperiment>().Query()
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if(experiment == null)
            {
                return new BaseApiResponse(false, "Эксперимент не найден по указанному идентификатору");
            }

            Context.Entry(experiment).State = EntityState.Modified;
            experiment.Title = model.Title;

            return await TrySaveChangesAndReturnResultAsync("Название эксперимента изменено");
        }

        /// <summary>
        /// Обновить эксперимент
        /// </summary>
        /// <param name="experiment"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> UpdateExperimentAsync(ChemistryTaskExperimentDto experiment)
        {
            if (experiment == null)
            {
                return new BaseApiResponse(false, "Вы подали пустую модель");
            }

            var dbExperiment = await GetExperimentAsync(experiment.Id);

            if (dbExperiment == null)
            {
                return new BaseApiResponse(false, "Экперимент не найден по указанному идентификатору");
            }

            var userId = UserId;

            if (dbExperiment.PerformerId != userId)
            {
                return new BaseApiResponse(false, "Вы не являетесь исполнителем данного эксперимента");
            }

            Context.Entry(dbExperiment).State = EntityState.Modified;

            dbExperiment.PerformerText = experiment.PerformerText;
            dbExperiment.SubstanceCounterJson = experiment.SubstanceCounterJson;

            await Context.SaveChangesAsync();
            

            return new BaseApiResponse(true, "Данные эксперимента обновлены");
        }

        /// <summary>
        /// Загрузить или перезагрузить файл для эксперимента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> LoadOrReloadFileForExperimentAsync(ChemistryChangeFileForExperiment model)
        {
            if (model == null)
            {
                return new BaseApiResponse(false, "Вы подали пустую модель");
            }

            var experiment = await GetExperimentAsync(model.ExperimentId);

            if (experiment == null)
            {
                return new BaseApiResponse(false, "Эксперимент не найден по указанному идентификатору");
            }

            var userId = UserId;

            if (experiment.PerformerId != userId)
            {
                return new BaseApiResponse(false, "Вы не имеете прав для редактирования эксперимента. Так как вы не являетесь экспериментатором.");
            }

            if (experiment.Deleted)
            {
                return new BaseApiResponse(false, "Эксперимент является удаленным");
            }

            var repo = GetRepository<ChemistryTaskExperimentFile>();

            
            var existedFile = await repo.Query().FirstOrDefaultAsync(x => x.ChemistryTaskExperimentId == experiment.Id && x.Type == model.FileType);

            ChemistryTaskExperimentFile experimentFile;


            //Если файла пока не существует
            if (existedFile != null)
            {
                experimentFile = new ChemistryTaskExperimentFile
                {
                    FileId = model.FileId,
                    Type = model.FileType,
                    ChemistryTaskExperimentId = experiment.Id
                };

                repo.CreateHandled(experimentFile);
                
                return await TrySaveChangesAndReturnResultAsync("Файл обновлен");
            }
            else
            {
                repo.DeleteHandled(existedFile);

                experimentFile = new ChemistryTaskExperimentFile
                {
                    FileId = model.FileId,
                    Type = model.FileType,
                    ChemistryTaskExperimentId = experiment.Id
                };

                repo.CreateHandled(experimentFile);

                return await TrySaveChangesAndReturnResultAsync("Файл обновлен");

            }
            
        }

        /// <summary>
        /// Создать эксперимент к задаче
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> CreateExperimentForTaskAsync(CreateExperiment model)
        {
            if(model == null)
            {
                return new BaseApiResponse(false, "Вы подали пустую модель");
            }

            if (!IsAuthenticated)
            {
                return new BaseApiResponse(false, "Вы не авторизованы");
            }

            if(!await Context.ChemistryTasks.AnyAsync(x => x.Id == model.TaskId))
            {
                return new BaseApiResponse(false, "Задача к которой создается эксперимент не найдена по указанному идентификатору");
            }

            var experiment = new ChemistryTaskExperiment
            {
                Title = model.Title,
                ChemistryTaskId = model.TaskId,
                CreationDate = DateTime.Now,
                Deleted = false,
                PerformerId = UserId,
                SubstanceCounterJson = Tool.JsonConverter.Serialize(Chemistry_SubstanceCounter.GetDefaultCounter()),
            };

            var repo = GetRepository<ChemistryTaskExperiment>();

            repo.CreateHandled(experiment);

            return await TrySaveChangesAndReturnResultAsync("Создан эксперимент для химической задачи");
        }

        /// <summary>
        /// Получить эксперимент
        /// </summary>
        /// <param name="experimentId"></param>
        /// <returns></returns>
        public async Task<ChemistryTaskExperimentModel> GetExperimentAsync(string experimentId)
        {
            if (!IsAuthenticated)
            {
                return null;
            }
            return await GetRepository<ChemistryTaskExperiment>().Query()
                .Select(ChemistryTaskExperimentModel.SelectExpression)
                .FirstOrDefaultAsync(x => x.Id == experimentId);
        }

        
        /// <summary>
        /// Получить все эксперименты
        /// </summary>
        /// <param name="Context"></param>
        /// <returns></returns>
        public async Task<List<ChemistryTaskExperimentModel>> GetAllExperimentsAsync()
        {
            if (!IsAuthenticated)
            {
                return null;
            }
            
            return await GetRepository<ChemistryTaskExperiment>().Query()
                .Select(ChemistryTaskExperimentModel.SelectExpression)
                .ToListAsync();
        }

        /// <summary>
        /// Получить мои эксперименты
        /// </summary>
        /// <returns></returns>
        public async Task<List<ChemistryTaskExperimentModel>> GetMyExperimentsAsync()
        {
            if (!IsAuthenticated)
            {
                return null;
            }

            var userId = UserId;

            return await GetRepository<ChemistryTaskExperiment>().Query()
                .Where(x => x.PerformerId == userId)
                .Select(ChemistryTaskExperimentModel.SelectExpression).ToListAsync();
        }

        /// <summary>
        /// Удалить эксперимент асинхронно
        /// </summary>
        /// <param name="experimentId"></param>
        /// <param name="Context"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> RemoveExperimentAsync(string experimentId)
        {
            var repo = GetRepository<ChemistryTaskExperiment>();

            var experiment = await repo.Query().FirstOrDefaultAsync(x => x.Id == experimentId);

            if (experiment == null)
            {
                return new BaseApiResponse(false, "Эксперимент не найден по указанному идентификатору");
            }

            var userId = UserId;

            if (!User.IsAdmin() && experiment.PerformerId != userId)
            {
                return new BaseApiResponse(false, "У вас недостаточно прав для удаления эксперимента. Вы либо не администратор либо не создатель эксперимента.");
            }

            if(experiment.Deleted)
            {
                return new BaseApiResponse(false, "Эксперимент уже является удаленным");
            }

            experiment.Deleted = true;
            repo.UpdateHandled(experiment);

            return await TrySaveChangesAndReturnResultAsync("Эксперимент отправлен в удаленные");
        }

        /// <summary>
        /// Отменить удаление эксперимента
        /// </summary>
        /// <param name="experimentId"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> CancelRemovingExperimentAsync(string experimentId)
        {
            var repo = GetRepository<ChemistryTaskExperiment>();

            var experiment = await repo.Query().FirstOrDefaultAsync(x => x.Id == experimentId);

            if (experiment == null)
            {
                return new BaseApiResponse(false, "Эксперимент не найден по указанному идентификатору");
            }

            var userId = UserId;

            if (!User.IsAdmin() && experiment.PerformerId != userId)
            {
                return new BaseApiResponse(false, "У вас недостаточно прав для удаления эксперимента. Вы либо не администратор либо не создатель эксперимента.");
            }

            if (!experiment.Deleted)
            {
                return new BaseApiResponse(false, "Эксперимент уже является востановленным");
            }

            
            experiment.Deleted = false;
            repo.UpdateHandled(experiment);
            
            return await TrySaveChangesAndReturnResultAsync("Эксперимент востановлен");
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="contextWrapper"></param>
        public ChemistryTaskExperimentsWorker(IUserContextWrapper<ChemistryDbContext> contextWrapper) : base(contextWrapper)
        {
        }
    }
}