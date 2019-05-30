using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Abstractions.ContextWrappers;
using Croco.Core.Application;
using Croco.Core.Common.Models;
using FocLab.Logic.Abstractions;
using FocLab.Logic.EntityDtos;
using FocLab.Logic.EntityDtos.Users.Default;
using FocLab.Logic.Extensions;
using FocLab.Logic.Implementations;
using FocLab.Logic.Models;
using FocLab.Logic.Settings;
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

            var experiment = await Context.ChemistryTaskExperiments.FirstOrDefaultAsync(x => x.Id == model.ExperimentId);

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
        /// Получить все эксперименты вместе с пользователями
        /// </summary>
        /// <returns></returns>
        public async Task<List<IGrouping<ChemistryTaskDto, ChemistryTaskExperimentDto>>> GetAllExperimentsWithUsers()
        {
            var query = Context.ChemistryTaskExperiments.Select(x => new ChemistryTaskExperimentDto
            {
                Id = x.Id,
                Title = x.Title,
                ChemistryTask = new ChemistryTaskDto
                {
                    Id = x.ChemistryTask.Id,
                    Title = x.ChemistryTask.Title,
                },
                CreationDate = x.CreationDate,
                PerformedDate = x.PerformedDate,
                Performer = new ApplicationUserDto
                {
                    Email = x.Performer.Email,
                    Id = x.Performer.Id,
                    Name = x.Performer.Name
                }
            }).GroupBy(x => x.ChemistryTask);

            return await query.ToListAsync();
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

            var experiment = await Context.ChemistryTaskExperiments
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
        public async Task<BaseApiResponse> LoadOrReloadFileForExperimentAsync(Chemistry_ChangeFileForExperiment model)
        {
            if (model == null)
            {
                return new BaseApiResponse(false, "Вы подали пустую модель");
            }

            if (model.ExperimentFile == null)
            {
                return new BaseApiResponse(false, "Вы не подали модель файла");
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

            if (model.File == null)
            {
                return new BaseApiResponse(false, "Файл не подан");
            }

            var existedFile = await Context.ChemistryTaskExperimentFiles.FirstOrDefaultAsync(x => x.ChemistryTaskExperimentId == experiment.Id && x.Type == model.ExperimentFile.FileType);

            var fileWorker = new DbFileWorker(ApplicationContextWrapper);

            //загружаю файл
            var fileUploadResult = await fileWorker.UploadUserFileAsync(model.File);
            
            var file = fileUploadResult.ResponseObject;

           

            ChemistryTaskExperimentFile experimentFile;
            //Если файла пока не существует
            if (existedFile == null)
            {
                experimentFile = new ChemistryTaskExperimentFile
                {
                    FileId = file.Id,
                    Type = model.ExperimentFile.FileType,
                    ChemistryTaskExperimentId = experiment.Id
                };

                Context.ChemistryTaskExperimentFiles.Add(experimentFile);

                await Context.SaveChangesAsync();

                return new BaseApiResponse(true, "Файл загружен");
            }
            else
            {
                Context.ChemistryTaskExperimentFiles.Remove(existedFile);

                experimentFile = new ChemistryTaskExperimentFile
                {
                    FileId = file.Id,
                    Type = model.ExperimentFile.FileType,
                    ChemistryTaskExperimentId = experiment.Id
                };

                Context.ChemistryTaskExperimentFiles.Add(experimentFile);

                await Context.SaveChangesAsync();

                return new BaseApiResponse(true, "Файл загружен и обновлен");

            }
            
        }

        /// <summary>
        /// Создать эксперимент к задаче
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Context"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse<ChemistryTaskExperiment>> CreateExperimentForTaskAsync(ChemistryTaskExperimentDto model)
        {
            if(model == null)
            {
                return new BaseApiResponse<ChemistryTaskExperiment>(false, "Вы подали пустую модель");
            }

            if (!IsAuthenticated)
            {
                return new BaseApiResponse<ChemistryTaskExperiment>(false, "Вы не авторизованы");
            }

            if(!await Context.ChemistryTasks.AnyAsync(x => x.Id == model.ChemistryTaskId))
            {
                return new BaseApiResponse<ChemistryTaskExperiment>(false, "Задача к которой создается эксперимент не найдена по указанному идентификатору");
            }

            var experiment = new ChemistryTaskExperiment
            {
                Id = Guid.NewGuid().ToString(),
                Title = model.Title,
                ChemistryTaskId = model.ChemistryTaskId,
                CreationDate = DateTime.Now,
                Deleted = false,
                PerformerId = UserId,
                SubstanceCounterJson = Newtonsoft.Json.JsonConvert.SerializeObject(Chemistry_SubstanceCounter.GetDefaultCounter()),
            };

            Context.ChemistryTaskExperiments.Add(experiment);

            await Context.SaveChangesAsync();

            return new BaseApiResponse<ChemistryTaskExperiment>(true, "Создан эксперимент для химической задачи", experiment);
        }

        /// <summary>
        /// Получить эксперимент
        /// </summary>
        /// <param name="experimentId"></param>
        /// <returns></returns>
        public async Task<ChemistryTaskExperiment> GetExperimentAsync(string experimentId)
        {
            if (!IsAuthenticated)
            {
                return null;
            }
            
            var experiment = await Context.ChemistryTaskExperiments
                .Include(x => x.Files)
                .Include(x => x.ChemistryTask)
                .FirstOrDefaultAsync(x => x.Id == experimentId);

            return experiment;
        }

        /// <summary>
        /// Получить эксперименты
        /// </summary>
        /// <param name="experimentId"></param>
        /// <returns></returns>
        public async Task<ChemistryTaskExperimentDto> GetExperimentDtoAsync(string experimentId)
        {
            if (!IsAuthenticated)
            {
                return null;
            }

            var experiment = await Context.ChemistryTaskExperiments
                .Select(x => new ChemistryTaskExperimentDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Deleted = x.Deleted,
                    CreationDate = x.CreationDate,
                    ChemistryTaskId = x.ChemistryTaskId,
                    PerformerId = x.PerformerId,
                    PerformerText = x.PerformerText,
                    SubstanceCounterJson = x.SubstanceCounterJson,

                    Performer = new ApplicationUserDto
                    {
                        Id = x.Performer.Id,
                        Name = x.Performer.Name,
                        Email = x.Performer.Email,
                    },

                    ChemistryTask = new ChemistryTaskDto
                    {
                        Id = x.ChemistryTask.Id,
                        Title = x.ChemistryTask.Title,
                        
                    },
                    
                    Files = x.Files.Select(t => new ChemistryTaskExperimentFileDto
                    {
                        FileId = t.FileId,
                        Type = t.Type,
                        ChemistryTaskExperimentId = t.ChemistryTaskExperimentId
                    }).ToList()
                    
                })
                .FirstOrDefaultAsync(x => x.Id == experimentId);

            return experiment;
        }

        /// <summary>
        /// Получить все эксперименты
        /// </summary>
        /// <param name="Context"></param>
        /// <returns></returns>
        public async Task<List<ChemistryTaskExperiment>> GetAllExperimentsAsync()
        {
            if (!IsAuthenticated)
            {
                return null;
            }
            
            return await Context.ChemistryTaskExperiments.Include(x => x.Performer).ToListAsync();
        }

        /// <summary>
        /// Получить мои эксперименты
        /// </summary>
        /// <returns></returns>
        public async Task<List<ChemistryTaskExperiment>> GetMyExperimentsAsync()
        {
            if (!IsAuthenticated)
            {
                return null;
            }

            var userId = UserId;

            return await Context.ChemistryTaskExperiments.Include(x => x.ChemistryTask).Where(x => x.PerformerId == userId).ToListAsync();
        }

        /// <summary>
        /// Удалить эксперимент асинхронно
        /// </summary>
        /// <param name="experimentId"></param>
        /// <param name="Context"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> RemoveExperimentAsync(string experimentId)
        {
            var experiment = await GetExperimentAsync(experimentId);

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

            Context.Entry(experiment).State = EntityState.Modified;

            experiment.Deleted = true;

            return await TrySaveChangesAndReturnResultAsync("Эксперимент отправлен в удаленные");
        }

        /// <summary>
        /// Отменить удаление эксперимента
        /// </summary>
        /// <param name="experimentId"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> CancelRemovingExperimentAsync(string experimentId)
        {
            var experiment = await GetExperimentAsync(experimentId);

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

            Context.Entry(experiment).State = EntityState.Modified;

            experiment.Deleted = false;

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