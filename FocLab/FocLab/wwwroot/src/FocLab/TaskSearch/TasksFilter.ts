class TasksFilter {
    static FilterPrefix: string = "Search";
    DefaultFilterState: object;
    Tasks: Array<ChemistryTaskModel>;
    static ShowAllString: string = "ShowAll";
    SetTasks(tasks): void {
        this.Tasks = tasks;
    }
    ApplyFilter(): void {
        if (this.Tasks == null) {
            alert("Не установлены названия");
        }
        var tasks = this.Tasks;
        tasks = this.SortTasks(tasks);
        this.ShowAndHideTasks(tasks);
    }
    ClearFilter(): void {
        (document.getElementsByName("SearchTasks.Q")[0] as HTMLInputElement).value = "";
    }
    SearchAndSortTasks(tasks: Array<ChemistryTaskModel>): Array<ChemistryTaskModel> {
        var q = (document.getElementsByName("SearchTasks.Q")[0] as HTMLInputElement).value;
        let applyTextSearch = q !== null && q.length > 0;
        if (applyTextSearch) {
            return TaskFilterMethods.ApplyTextFilter(tasks, q);
        }
        var status = (document.getElementsByName("SearchTasks.TaskStatus")[0] as HTMLInputElement).value;
        tasks = TaskFilterMethods.ApplyStatusFilter(tasks, TaskFilterMethods.ParseBoolean(status));
        var userId = (document.getElementsByName("SearchTasks.User")[0] as HTMLInputElement).value;
        if (userId != null && userId !== TasksFilter.ShowAllString) {
            tasks = tasks.filter(x => x.PerformerUser.Id == userId);
        }
        return tasks;
    }
    ShowAndHideTasks(tasks: Array<ChemistryTaskModel>): void {
        tasks = this.SearchAndSortTasks(tasks);
        this.DrawPerformed(tasks);
        this.DrawNotPerformed(tasks);
    }
    DrawPerformed(tasks: Array<ChemistryTaskModel>): void {
        var tBody = document.getElementById("performed-tbody");
        tBody.innerHTML = "";
        var performed = tasks.filter(x => x.IsPerformed);
        for (var i = 0; i < performed.length; i++) {
            tBody.appendChild(IndexPageTaskDrawer.GetTaskElementForPerformedTr(performed[i]));
        }
        if (tasks.length === 0) {
            tBody.appendChild(IndexPageTaskDrawer.GetNotFoundElement(8));
        }
    }
    DrawNotPerformed(tasks: Array<ChemistryTaskModel>): void {
        var tBody = document.getElementById("not-performed-body");
        tBody.innerHTML = "";
        var notPerformed = tasks.filter(x => !x.IsPerformed);
        for (let i = 0; i < notPerformed.length; i++) {
            tBody.appendChild(IndexPageTaskDrawer.GetTaskElementForNotPerformedTr(notPerformed[i]));
        }
        if (tasks.length == 0) {
            tBody.appendChild(IndexPageTaskDrawer.GetNotFoundElement(6));
        }
    }
    SortTasks(tasks: Array<ChemistryTaskModel>): Array<ChemistryTaskModel> {
        var sort = (document.getElementsByName("SearchTasks.Sort")[0] as HTMLInputElement).value;
        if (sort === '1') {
            tasks = this.SortByPerformedDate(tasks);
            return tasks;
        }
        else if (sort === '2') {
            tasks = this.SortByPerformerName(tasks);
            return tasks;
        }
        return tasks;
    }
    SortByPerformedDate(tasks: Array<ChemistryTaskModel>): Array<ChemistryTaskModel> {
        var result = tasks.sort(function(a, b) {
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
    }
    SortByPerformerName(tasks: Array<ChemistryTaskModel>): Array<ChemistryTaskModel> {
        var result = tasks.sort(function(a, b) {
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
    }
}
