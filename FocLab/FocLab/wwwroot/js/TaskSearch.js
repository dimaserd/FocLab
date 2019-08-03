var TaskFilterMethods = /** @class */ (function () {
    function TaskFilterMethods() {
    }
    TaskFilterMethods.ApplyTextFilter = function (tasks, q) {
        q = q.toLowerCase();
        var result = tasks.filter(function (x) {
            return (x.Title != null && x.Title.toLowerCase().includes(q));
        });
        return result;
    };
    TaskFilterMethods.ApplyStatusFilter = function (tasks, isPerformed) {
        if (isPerformed == null) {
            return tasks;
        }
        var result = tasks.filter(function (x) { return x.IsPerformed == isPerformed; });
        return result;
    };
    TaskFilterMethods.ParseBoolean = function (s) {
        if (s === "" || s === null) {
            return null;
        }
        return s.toLowerCase() === "true";
    };
    return TaskFilterMethods;
}());


var IndexPageTaskDrawer = /** @class */ (function () {
    function IndexPageTaskDrawer() {
    }
    IndexPageTaskDrawer.GetNotFoundElement = function (colspan) {
        var tr = document.createElement("tr");
        var innerHtml = "\n        <td colspan=\"" + colspan + "\">\n            <i class=\"far fa-frown\"></i>\n            \u041D\u0435 \u043D\u0430\u0439\u0434\u0435\u043D\u043E\n        </td>";
        tr.innerHTML = innerHtml;
        return tr;
    };
    IndexPageTaskDrawer.GetTaskElementForPerformedTr = function (task) {
        var _class = task.IsPerformedInTime ? "success" : "danger";
        var tr = document.createElement("tr");
        tr.classList.add(_class);
        tr.classList.add("performed-chemistry-task");
        var innerHtml = "\n\n                        <td>\n                            <a href=\"/Chemistry/Tasks/Task/" + task.Id + "\">\n                                " + task.Title + "\n                            </a>\n                        </td>\n\n                        <td>\n                            <a href=\"/Chemistry/Tasks?id=" + task.PerformerUser.Id + "\">\n                                " + task.PerformerUser.Name + " " + task.PerformerUser.Email + "\n                            </a>\n                        </td>\n\n                        <td>\n                            " + moment(task.DeadLineDate).locale('ru').format('DD MMMM YYYY, hh:mm:ss') + "\n                        </td>\n\n                        <td>\n                            " + moment(task.PerformedDate).locale('ru').format('DD MMMM YYYY, hh:mm:ss') + "\n                        </td>\n\n                        <td>\n                            " + (task.AdminQuantity != null ? task.AdminQuantity : '') + "\n                        </td>\n\n                        <td>\n                            " + (task.PerformerQuantity != null ? task.PerformerQuantity : '') + "\n                        </td>\n\n                        <td>\n                            " + (task.AdminQuality != null ? task.AdminQuality : '') + "\n                        </td>\n\n                        <td>\n                            " + (task.PerformerQuality != null ? task.PerformerQuality : '') + "\n                        </td>";
        tr.innerHTML = innerHtml;
        return tr;
    };
    IndexPageTaskDrawer.GetTaskElementForNotPerformedTr = function (task) {
        var dateNow = new Date();
        var _class = task.DeadLineDate > dateNow ? "success" : "danger";
        var tr = document.createElement("tr");
        tr.classList.add(_class);
        var innerHtml = "\n\n                        <td>\n                            <a href=\"/Chemistry/Tasks/Task/" + task.Id + "\">\n                                " + task.Title + "\n                            </a>\n                        </td>\n\n                        <td>\n                            <a href=\"/Chemistry/Chemistry?id=" + task.PerformerUser.Id + "\">\n                                " + task.PerformerUser.Name + " " + task.PerformerUser.Email + "\n                            </a>\n                        </td>\n\n                        <td>\n                            " + moment(task.DeadLineDate).locale('ru').format('DD MMMM YYYY, hh:mm:ss') + "\n                        </td>\n\n                        <td>";
        if (task.ChemistryMethodFile != null) {
            innerHtml += "\n                                <a href=\"/Files/GetDbFileById?id=" + task.ChemistryMethodFile.FileId + "\" class=\"btn btn-primary\">\n                                    " + task.ChemistryMethodFile.Name + "\n                                </a>";
        }
        else {
            innerHtml += "[Метод не указан]";
        }
        innerHtml += "\n\n                        </td>\n\n                        <td>\n                            " + (task.AdminQuality != null ? task.AdminQuality : "") + "\n                        </td>\n\n                        <td>\n                            " + (task.AdminQuantity != null ? task.AdminQuantity : "") + "\n                        </td>";
        tr.innerHTML = innerHtml;
        return tr;
    };
    return IndexPageTaskDrawer;
}());

var TasksFilter = /** @class */ (function () {
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
            // a должно быть равным b
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
            // a должно быть равным b
            return 0;
        });
        return result;
    };
    TasksFilter.FilterPrefix = "Search";
    TasksFilter.ShowAllString = "ShowAll";
    return TasksFilter;
}());