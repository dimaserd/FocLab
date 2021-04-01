using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clt.Contract.Models.Users;
using Clt.Logic.Services.Users;
using Croco.Core.Contract;
using Croco.Core.Contract.Application;
using Croco.Core.Logic.Services;
using FocLab.Logic.Workers.ChemistryMethods;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewFocLab.Model;

namespace FocLab.Helpers
{
    public class ChemistryTasksHtmlHelper : BaseCrocoService<FocLabDbContext>
    {
        private readonly ChemistryMethodsWorker _methodsWorker;

        UserSearcher UserSearcher { get; }

        public ChemistryTasksHtmlHelper(
            ICrocoAmbientContextAccessor contextAccessor,
            ICrocoApplication application,
            ChemistryMethodsWorker methodsWorker, 
            UserSearcher userSearcher) : base(contextAccessor, application)
        {
            _methodsWorker = methodsWorker;
            UserSearcher = userSearcher;
        }

        
        public async Task<List<SelectListItem>> GetMethodsSelectListAsync()
        {
            var model = await _methodsWorker.GetMethodsAsync();

            var tasksSelectList = model.Select(x => new SelectListItem { Text = x.Name, Value = x.Id }).ToList();

            tasksSelectList.Add(new SelectListItem { Text = "Не указано", Value = "", Selected = true });

            return tasksSelectList;
        }

        public async Task<List<SelectListItem>> GetUsersSelectListAsync()
        {
            var users = await UserSearcher.GetUsersAsync(UserSearch.GetAllUsers);

            var usersSelectList = users.List.Where(x => !x.RoleNames.Contains("Admin"))
                .Where(x => x.Id != UserId)
                .Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = $"{x.Name} {x.Email}"
                }).ToList();

            return usersSelectList;
        }
    }
}