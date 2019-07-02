using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FocLab.Logic.Workers.ChemistryTasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FocLab.Helpers
{
    public class ChemistryTasksHtmlHelper
    {
        private readonly ChemistryTasksWorker _tasksWorker;

        public ChemistryTasksHtmlHelper(ChemistryTasksWorker tasksWorker)
        {
            _tasksWorker = tasksWorker;
        }

        public async Task<List<SelectListItem>> GetMethodsSelectListAsync()
        {
            var model = await _tasksWorker.GetNotDeletedTasksAsync();

            var tasksSelectList = model.Select(x => new SelectListItem { Text = x.Title, Value = x.Title }).ToList();

            tasksSelectList.Add(new SelectListItem { Text = "Не указано", Value = "", Selected = true });

            return tasksSelectList;
        }
    }
}
