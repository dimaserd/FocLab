﻿using System.Linq;
using System.Threading.Tasks;
using FocLab.Areas.Chemistry.Controllers.Base;
using FocLab.Logic.Extensions;
using FocLab.Logic.Models.Tasks;
using FocLab.Logic.Models.Users;
using FocLab.Logic.Services;
using FocLab.Logic.Workers.ChemistryReagents;
using FocLab.Logic.Workers.ChemistryTasks;
using FocLab.Model.Contexts;
using FocLab.Model.Enumerations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FocLab.Areas.Chemistry.Controllers.Mvc
{
    [Area("Chemistry")]
    [Authorize]
    public class TasksController : BaseChemistryCustomController
    {
        public TasksController(ChemistryDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(context, userManager, signInManager)
        {
        }

        private ChemistryTasksWorker ChemistryTasksWorker => new ChemistryTasksWorker(ContextWrapper);

        private ChemistryReagentsWorker ChemistryReagentsWorker => new ChemistryReagentsWorker(ContextWrapper);

        private AdminChemistryTasksWorker AdminChemistryTasksWorker => new AdminChemistryTasksWorker(ContextWrapper);
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

            if (task == null)
            {
                return RedirectToAction("Index");
            }

            if (!User.HasRight(UserRight.Admin) && !User.HasRight(UserRight.SuperAdmin) && task.PerformerUser.UserId != UserId)
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


            ViewData["model"] = task;
            ViewData["fileMethodsSelectList"] = await AdminChemistryTasksWorker.GetFileMethodsSelectListAsync();

            ViewData["usersSelectList"] = await AdminChemistryTasksWorker.GetUsersSelectListAsync();

            return View(task);
        }


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
    }
}
