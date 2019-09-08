var TasksFilter = (function () {
    function TasksFilter() {
    }
    TasksFilter.prototype.SetTasks = function (tasks) {
        this.Tasks = tasks;
    };
    TasksFilter.prototype.ApplyFilter = function () {
        if (this.Tasks == null) {
            alert("Не установлены названия");
        }
        var tasks = this.Tasks;
        tasks = this.SortTasks(tasks);
        this.ShowAndHideTasks(tasks);
    };
    TasksFilter.prototype.ClearFilter = function () {
        document.getElementsByName("SearchTasks.Q")[0].value = "";
    };
    TasksFilter.prototype.SearchAndSortTasks = function (tasks) {
        var q = document.getElementsByName("SearchTasks.Q")[0].value;
        var applyTextSearch = q !== null && q.length > 0;
        if (applyTextSearch) {
            return TaskFilterMethods.ApplyTextFilter(tasks, q);
        }
        var status = document.getElementsByName("SearchTasks.TaskStatus")[0].value;
        tasks = TaskFilterMethods.ApplyStatusFilter(tasks, TaskFilterMethods.ParseBoolean(status));
        var userId = document.getElementsByName("SearchTasks.User")[0].value;
        if (userId != null && userId !== TasksFilter.ShowAllString) {
            tasks = tasks.filter(function (x) { return x.PerformerUser.Id == userId; });
        }
        return tasks;
    };
    TasksFilter.prototype.ShowAndHideTasks = function (tasks) {
        tasks = this.SearchAndSortTasks(tasks);
        this.DrawPerformed(tasks);
        this.DrawNotPerformed(tasks);
    };
    TasksFilter.prototype.DrawPerformed = function (tasks) {
        var tBody = document.getElementById("performed-tbody");
        tBody.innerHTML = "";
        var performed = tasks.filter(function (x) { return x.IsPerformed; });
        for (var i = 0; i < performed.length; i++) {
            tBody.appendChild(IndexPageTaskDrawer.GetTaskElementForPerformedTr(performed[i]));
        }
        if (tasks.length === 0) {
            tBody.appendChild(IndexPageTaskDrawer.GetNotFoundElement(8));
        }
    };
    TasksFilter.prototype.DrawNotPerformed = function (tasks) {
        var tBody = document.getElementById("not-performed-body");
        tBody.innerHTML = "";
        var notPerformed = tasks.filter(function (x) { return !x.IsPerformed; });
        for (var i = 0; i < notPerformed.length; i++) {
            tBody.appendChild(IndexPageTaskDrawer.GetTaskElementForNotPerformedTr(notPerformed[i]));
        }
        if (tasks.length == 0) {
            tBody.appendChild(IndexPageTaskDrawer.GetNotFoundElement(6));
        }
    };
    TasksFilter.prototype.SortTasks = function (tasks) {
        var sort = document.getElementsByName("SearchTasks.Sort")[0].value;
        if (sort === '1') {
            tasks = this.SortByPerformedDate(tasks);
            return tasks;
        }
        else if (sort === '2') {
            tasks = this.SortByPerformerName(tasks);
            return tasks;
        }
        return tasks;
    };
    TasksFilter.prototype.SortByPerformedDate = function (tasks) {
        var result = tasks.sort(function (a, b) {
            if (a.PerformedDate == null && b.PerformedDate == null) {
                return 0;
            }
            if (a.PerformedDate != null && b.PerformedDate == null) {
                return 1;
            }
            if (a.PerformedDate == null && b.PerformedDate != null) {
                return -1;
            }
            var aDate = a.PerformedDate;
            var bDate = b.PerformedDate;
            if (aDate > bDate) {
                return 1;
            }
            if (aDate < bDate) {
                return -1;
            }
            return 0;
        });
        return result;
    };
    TasksFilter.prototype.SortByPerformerName = function (tasks) {
        var result = tasks.sort(function (a, b) {
            if (a.PerformerUser.Name > b.PerformerUser.Name) {
                return 1;
            }
            if (a.PerformerUser.Name < b.PerformerUser.Name) {
                return -1;
            }
            return 0;
        });
        return result;
    };
    TasksFilter.FilterPrefix = "Search";
    TasksFilter.ShowAllString = "ShowAll";
    return TasksFilter;
}());
