﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Clt.Contract.Models.Users;
using Clt.Logic.Services.Users;
using Croco.Core.Application;
using Croco.Core.Contract;
using FocLab.Consts;
using FocLab.Controllers.Base;
using FocLab.Logic.Extensions;
using FocLab.Logic.Models.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FocLab.Logic.Services.ChemistryReagents;
using FocLab.Logic.Services.ChemistryTasks;
using FocLab.App.Logic.Services.Doc;
using FocLab.App.Logic.Models.Doc;
using FocLabWebApp.Helpers;

namespace FocLab.Areas.Chemistry.Controllers.Mvc
{
    [Area(AreaConsts.Chemistry), Authorize]
    public class TasksController : BaseController
    {
        private ChemistryTasksWorker ChemistryTasksWorker { get; }

        private ChemistryReagentsWorker ChemistryReagentsWorker { get; }

        private ChemistryTasksHtmlHelper ChemistryTasksHtmlHelper { get; }

        private ChemistryTaskDocumentProccessor FocLabDocumentProcessor { get; }
        private UserSearcher UserSearcher { get; }

        public TasksController(ChemistryTasksWorker chemistryTasksWorker, 
            ChemistryReagentsWorker chemistryReagentsWorker,
            ChemistryTasksHtmlHelper chemistryTasksHtmlHelper,
            ChemistryTaskDocumentProccessor chemistryTaskDocumentProccessor,
            UserSearcher userSearcher,
            ICrocoRequestContextAccessor requestContextAccessor) : base(requestContextAccessor)
        {
            ChemistryTasksWorker = chemistryTasksWorker;
            ChemistryReagentsWorker = chemistryReagentsWorker;
            ChemistryTasksHtmlHelper = chemistryTasksHtmlHelper;
            FocLabDocumentProcessor = chemistryTaskDocumentProccessor;
            UserSearcher = userSearcher;
        }

        public async Task<FileResult> Print(string id)
        {
            var fileName = $"Task.docx";

            var filePath = CrocoApp.Application.MapPath($"~/wwwroot/Docs/{fileName}");

            var t = await FocLabDocumentProcessor.RanderByTaskIdAsync(new RenderChemistryTaskDocument
            {
                TaskId = id,
                DocSaveFileName = filePath
            });

            if(!t.IsSucceeded)
            {
                throw new ApplicationException(t.Message);
            }

            return PhysicalFileWithMimeType(filePath, fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Index(string id)
        {
            var user = await UserSearcher.GetUserByIdAsync(id);

            var usersList = await UserSearcher.GetUsersAsync(UserSearch.GetAllUsers);

            var usersSelectList = usersList.List.Select(x => new SelectListItem { Value = x.Id, Text = $"{x.Name} {x.Email}" }).ToList();

            usersSelectList.Add(new SelectListItem
            {
                Selected = true,
                Text = "Показывать всех",
                Value = "ShowAll"
            });

            ViewData["usersSelectList"] = usersSelectList;

            ViewData["User"] = user;

            var model = await ChemistryTasksWorker.GetNotDeletedTasksAsync();

            var tasksSelectList = model.Select(x => new SelectListItem { Text = x.Title, Value = x.Title }).ToList();

            tasksSelectList.Add(new SelectListItem { Text = "Не указано", Value = "", Selected = true });

            ViewData["tasksSelectList"] = tasksSelectList;

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Task(string id)
        {
            var task = await ChemistryTasksWorker.GetChemistryTaskByIdAsync(id);

            var reagents = await ChemistryReagentsWorker.GetReagentsAsync();

            var reagentSelectList = reagents
                .Where(x => !task.Reagents.Any(t => t.Reagent.Id == x.Id))
                .Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id
            }).ToList();

            ViewData["reagentSelectList"] = reagentSelectList;

            if (task == null)
            {
                return RedirectToAction("Index");
            }

            ViewData["task"] = task;
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
            if (!User.IsAdmin())
            {
                return RedirectToAction("Index");
            }

            var task = await ChemistryTasksWorker.GetChemistryTaskByIdAsync(id);

            if (task == null)
            {
                return RedirectToAction("Index");
            }
            var model = EditChemistryTask.ToEditChemistryTask(task);

            ViewData["model"] = task;
            ViewData["fileMethodsSelectList"] = await ChemistryTasksHtmlHelper.GetMethodsSelectListAsync();

            ViewData["usersSelectList"] = await ChemistryTasksHtmlHelper.GetUsersSelectListAsync();

            return View(model);
        }


        /// <summary>
        /// Создание задания
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> CreateTask()
        {
            var model = new ChemistryCreateTask
            {
                AdminId = UserId,
            };

            ViewData["fileMethodsSelectList"] = await ChemistryTasksHtmlHelper.GetMethodsSelectListAsync();
            ViewData["usersSelectList"] = await ChemistryTasksHtmlHelper.GetUsersSelectListAsync();

            return View(model);
        }
    }
}