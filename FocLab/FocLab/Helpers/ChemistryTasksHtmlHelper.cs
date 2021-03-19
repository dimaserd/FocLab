using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Contract;
using Croco.Core.Contract.Application;
using Croco.Core.Logic.Workers;
using FocLab.Logic.Models.Users;
using FocLab.Logic.Workers.ChemistryMethods;
using FocLab.Logic.Workers.Users;
using FocLab.Model.Contexts;
using FocLab.Model.Enumerations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FocLab.Helpers
{
    public class ChemistryTasksHtmlHelper : BaseCrocoWorker<ChemistryDbContext>
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
            var users = await UserSearcher.SearchUsersAsync(UserSearch.GetAllUsers);

            var usersSelectList = users.List.Where(x => !x.HasRight(UserRight.Admin) && !x.HasRight(UserRight.SuperAdmin))
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
