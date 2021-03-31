using System.Linq;
using System.Threading.Tasks;
using Clt.Logic.Models.Users;
using Clt.Logic.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tms.Logic.Models;
using Tms.Logic.Services;
using Tms.Model;
using Tms.Model.Entities;
using Zoo.GenericUserInterface.Models;
using Zoo.GenericUserInterface.Services;

namespace FocLab.Controllers.Mvc
{
    public class ScheduleController : Controller
    {
        private UserSearcher UserSearcher { get; }
        private DayTasksService TasksService { get; }
        private GenericUserInterfaceBag InterfacesBag { get; }
        public TmsDbContext TmsDb { get; }

        public ScheduleController(UserSearcher userSearcher,
            DayTasksService dayTasksService, 
            GenericUserInterfaceBag interfacesBag,
            TmsDbContext tmsDb)
        {
            TasksService = dayTasksService;
            InterfacesBag = interfacesBag;
            TmsDb = tmsDb;
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

            var task = await TmsDb.Set<DayTask>().FirstOrDefaultAsync(x => x.Id == id);

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