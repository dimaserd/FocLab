using System.Linq;
using System.Threading.Tasks;
using FocLab.Logic.Models.Users;
using FocLab.Logic.Workers.Users;
using FocLab.Model.Contexts;
using FocLab.Model.Entities.Tasker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tms.Logic.Models;
using Tms.Logic.Services;
using Zoo.GenericUserInterface.Models;
using Zoo.GenericUserInterface.Services;

namespace FocLab.Controllers.Mvc
{
    public class ScheduleController : Controller
    {
        private UserSearcher UserSearcher { get; }
        private DayTasksService TasksService { get; }
        private GenericUserInterfaceBag InterfacesBag { get; }
        private ChemistryDbContext ChemistryDb { get; }

        public ScheduleController(UserSearcher userSearcher,
            DayTasksService dayTasksService, 
            GenericUserInterfaceBag interfacesBag,
            ChemistryDbContext chemistryDb)
        {
            TasksService = dayTasksService;
            InterfacesBag = interfacesBag;
            ChemistryDb = chemistryDb;
            UserSearcher = userSearcher;
        }


        /// <summary>
        /// Показать задания пользователей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResult Index(UserScheduleSearchModel model)
        {
            ViewData["searchModel"] = model;

            var viewModel = new CalendarMonthViewModel(model.MonthShift);

            return View(viewModel);
        }

        public async Task<IActionResult> Task(string id)
        {
            var model = await TasksService.GetDayTaskByIdAsync(id);

            var task = await ChemistryDb.Set<ApplicationDayTask>().FirstOrDefaultAsync(x => x.Id == id);

            var users = await UserSearcher.GetUsersAsync(UserSearch.GetAllUsers);

            var userSelectList = users.List.Select(x => new SelectListItem
            {
                Selected = x.Id == task.AssigneeUserId,
                Text = x.Email,
                Value = x.Id
            });

            var interfaceModel = await InterfacesBag.GetDefaultInterface<CreateOrUpdateDayTask>();
            interfaceModel.Interface.Prefix = "update.";
            ViewData["interfaceModel"] = interfaceModel;
            
            return View(model);
        }
    }
}