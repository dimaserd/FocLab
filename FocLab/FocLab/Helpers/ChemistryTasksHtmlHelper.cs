using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Abstractions;
using FocLab.Logic.Models.Users;
using FocLab.Logic.Workers.ChemistryMethods;
using FocLab.Logic.Workers.Users;
using FocLab.Model.Enumerations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FocLab.Helpers
{
    public class ChemistryTasksHtmlHelper
    {
        private readonly ChemistryMethodsWorker _methodsWorker;

        public ChemistryTasksHtmlHelper(ChemistryMethodsWorker methodsWorker)
        {
            _methodsWorker = methodsWorker;
        }

        public async Task<List<SelectListItem>> GetMethodsSelectListAsync()
        {
            var model = await _methodsWorker.GetMethodsAsync();

            var tasksSelectList = model.Select(x => new SelectListItem { Text = x.Name, Value = x.Id }).ToList();

            tasksSelectList.Add(new SelectListItem { Text = "Не указано", Value = "", Selected = true });

            return tasksSelectList;
        }

        public async Task<List<SelectListItem>> GetUsersSelectListAsync(ICrocoAmbientContext context)
        {
            var userId = context.RequestContext.UserPrincipal.UserId;

            var searcher = new UserSearcher(context);

            var users = await searcher.SearchUsersAsync(UserSearch.GetAllUsers);

            var usersSelectList = users.List.Where(x => !x.HasRight(UserRight.Admin) && !x.HasRight(UserRight.SuperAdmin))
                .Where(x => x.Id != userId)
                .Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = $"{x.Name} {x.Email}"
                }).ToList();

            return usersSelectList;
        }
    }
}