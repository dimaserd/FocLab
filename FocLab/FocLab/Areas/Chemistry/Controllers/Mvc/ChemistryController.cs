using System;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Abstractions;
using Croco.Core.Common.Models;
using FocLab.Areas.Chemistry.Controllers.Base;
using FocLab.Logic.EntityDtos;
using FocLab.Logic.Extensions;
using FocLab.Logic.Models;
using FocLab.Logic.Models.ChemistryTasks;
using FocLab.Logic.Models.Users;
using FocLab.Logic.Services;
using FocLab.Logic.Workers.ChemistryMethods;
using FocLab.Logic.Workers.ChemistryReagents;
using FocLab.Logic.Workers.ChemistryTaskExperiments;
using FocLab.Logic.Workers.ChemistryTasks;
using FocLab.Model.Contexts;
using FocLab.Model.Enumerations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FocLab.Areas.Chemistry.Controllers.Mvc
{

    /// <inheritdoc />
    /// <summary>
    /// Контроллер для работы с химическими заданиями
    /// </summary>
    [Area("Chemistry")]
    [Authorize]
    public class ChemistryController : BaseChemistryCustomController
    {
        private ChemistryMethodsWorker ChemistryMethodsWorker => new ChemistryMethodsWorker(ContextWrapper);

        private ChemistryTasksWorker ChemistryTasksWorker => new ChemistryTasksWorker(ContextWrapper);

        private ChemistryTaskExperimentsWorker ChemistryTaskExperimentsWorker =>
            new ChemistryTaskExperimentsWorker(ContextWrapper);

        private ChemistryReagentsWorker ChemistryReagentsWorker => new ChemistryReagentsWorker(ContextWrapper);

        private PerformerChemistryTasksWorker PerformerChemistryTasksWorker =>
            new PerformerChemistryTasksWorker(ContextWrapper);

        private AdminChemistryTasksWorker AdminChemistryTasksWorker => new AdminChemistryTasksWorker(ContextWrapper);
        

        #region Http Обработчики
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Index(string id)
        {
            var user = await UserSearcher.GetUserByIdAsync(id);
            
            var usersList = await UserSearcher.SearchUsersAsync(UserSearch.GetAllUsers);

            var usersSelectList = usersList.List.Select(x => new SelectListItem { Value = x.Id, Text = $"{x.Name} {x.Email}" }).ToList();

            ViewData["usersSelectList"] = usersSelectList;

            ViewData["User"] = user;

            var model = await ChemistryTasksWorker.GetNotDeletedTasksAsync();

            var tasksSelectList = model.Select(x => new SelectListItem { Text = x.Title, Value = x.Title }).ToList();

            tasksSelectList.Add(new SelectListItem { Text = "Не указано", Value = "", Selected = true });

            ViewData["tasksSelectList"] = tasksSelectList;

            return View(model);
        }

        #region Задания

        #region Просмотр и редактирование задания
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Task(string id)
        {
            var task = await ChemistryTasksWorker.GetChemistryTaskByIdAsync(id);

            var reagents = await ChemistryReagentsWorker.GetReagentsAsync();

            var reagentSelectList = reagents.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id
            }).ToList();

            ViewData["reagentSelectList"] = reagentSelectList;

            if(task == null)
            {
                return RedirectToAction("Index");
            }

            if (!User.HasRight(UserRight.Admin) && !User.HasRight(UserRight.SuperAdmin) && task.PerformerUser.UserId != UserId)
            {
                return RedirectToAction("Index");
            }

            return View(task);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> EditTask(string id)
        {
            if(!User.IsAdmin())
            {
                return RedirectToAction("Index");
            }

            var task = await ChemistryTasksWorker.GetChemistryTaskByIdAsync(id);

            if (task == null)
            {
                return RedirectToAction("Index");
            }

            
            ViewData["model"] = task;
            ViewData["fileMethodsSelectList"] = await AdminChemistryTasksWorker.GetFileMethodsSelectListAsync();

            ViewData["usersSelectList"] = await AdminChemistryTasksWorker.GetUsersSelectListAsync();

            return View(task);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="metaEntityId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> EditTask(Chemistry_CreateTask model, string metaEntityId)
        {
            var result = await AdminChemistryTasksWorker.EditTaskAsync(new ChemistryTaskDto
            {
                Id = metaEntityId,
                DeadLineDate = model.DeadLineDate,
                AdminQuality = model.Quality,
                AdminQuantity = model.Quantity,
                Title = model.Title,
                MethodFileId = model.FileMethodId.ToString(),
                
            });

            return Json(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> UploadTaskFile(Chemistry_ChangeFileForTask model)
        {
            var result = await ChemistryTasksWorker.LoadOrReloadFileForTaskAsync(model);

            return Json(result);
        }

        /// <summary>
        /// Обновить задание
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> UpdateTaskByPerformer(ChemistryTaskDto model)
        {
            var result = await PerformerChemistryTasksWorker.UpdateTaskAsync(model);

            return Json(result);
        }
        #region Методы завершения

        /// <summary>
        /// Выполнить задание
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> PerformTask(PerformTaskModel model)
        {
            try
            {
                var result = await PerformerChemistryTasksWorker.PerformTaskAsync(model, MailSender);

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

            
        }
        
        #endregion


        #endregion

        #region Создание 

        /// <summary>
        /// Создание задания
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> CreateTask()
        {
            var model = new Chemistry_CreateTask
            {
                AdminId = UserId,
            };

            ViewData["fileMethodsSelectList"] = await AdminChemistryTasksWorker.GetFileMethodsSelectListAsync();

            ViewData["usersSelectList"] = await AdminChemistryTasksWorker.GetUsersSelectListAsync();

            return View(model);
        }
        
        #endregion

        #region Методы удаления
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> RemoveOrCancelRemoving(ChemistryTaskRemoveOrCancelModel model)
        {
            var resp = model.Flag ? 
                await AdminChemistryTasksWorker.CancelRemoveTaskAsync(new ChemistryTaskDto { Id = model.Id })
                : await AdminChemistryTasksWorker.RemoveTaskAsync(new ChemistryTaskDto { Id = model.Id });

            return Json(resp);
        }
        #endregion

        #endregion

        #region Методы
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin,SuperAdmin,Root")]
        public async Task<ActionResult> Methods()
        {
            var model = await ChemistryMethodsWorker.GetTaskMethodsAsync();

            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,SuperAdmin,Root")]
        public async Task<JsonResult> UploadMethod(IFileData model, string methodName)
        {
            //TODO Переделать загрузку файлов на единичный случай
            return Json(await ChemistryMethodsWorker.UploadMethodAsync(model, methodName));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> EditMethod(string id)
        {
            var method = await ChemistryMethodsWorker.GetMethodAsync(id);

            return View(method);
        }
        #endregion

        #region Эксперименты
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> LoadOrReloadFileForExperiment(ChemistryChangeFileForExperiment model)
        {
            try
            {
                var t = await ChemistryTaskExperimentsWorker.LoadOrReloadFileForExperimentAsync(model);

                return Json(t);
            }
            catch (Exception ex)
            {
                return Json(new BaseApiResponse(false, ex.Message));
            }
            
        }

        #region Удаление 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BaseApiResponse> RemoveExperiment(string id)
        {
            return await ChemistryTaskExperimentsWorker.RemoveExperimentAsync(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BaseApiResponse> CancelRemovingExperiment(string id)
        {
            return await ChemistryTaskExperimentsWorker.CancelRemovingExperimentAsync(id);
        }
        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BaseApiResponse> UpdateExperiment(ChemistryTaskExperimentDto model)
        {
            return await ChemistryTaskExperimentsWorker.UpdateExperimentAsync(model);
        }

        #endregion

        #region Документы

        /// <summary>
        /// Распечатать задание
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ActionResult> PrintTask(string id)
        {
            //var t = await ChemistryDocumentProcessor.TestById(id);

            //const string fileName = "PrintDocument.docx";

            //var filePath = HostingEnvironment.MapPath($"~/Files/{fileName}");

            //return File(System.IO.File.ReadAllBytes(filePath), MimeMapping.GetMimeMapping(filePath), fileName);

            return null;
        }
        #endregion

        #endregion

        public ChemistryController(ChemistryDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(context, userManager, signInManager)
        {
        }
    }
}