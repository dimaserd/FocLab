class IndexPageTaskDrawer {
    static GetTaskElementForPerformedTr(task: ChemistryTaskModel) {

            var _class = task.IsPerformedInTime ? "success" : "danger";

            var tr = document.createElement("tr");

            tr.classList.add(_class);
            tr.classList.add("performed-chemistry-task");

            var innerHtml = `

                        <td>
                            <a href="/Chemistry/Tasks/Task/${task.Id}">
                                ${task.Title}
                            </a>
                        </td>

                        <td>
                            <a href="/Chemistry/Chemistry?id=${task.PerformerUser.Id}">
                                ${task.PerformerUser.Name} ${task.PerformerUser.Email}
                            </a>
                        </td>

                        <td>
                            ${moment(task.DeadLineDate).locale('ru').format('DD MMMM YYYY, hh:mm:ss')}
                        </td>

                        <td>
                            ${moment(task.PerformedDate).locale('ru').format('DD MMMM YYYY, hh:mm:ss')}
                        </td>

                        <td>
                            ${task.AdminQuantity != null ? task.AdminQuantity : ''}
                        </td>

                        <td>
                            ${task.PerformerQuantity != null ? task.PerformerQuantity : ''}
                        </td>

                        <td>
                            ${task.AdminQuality != null ? task.AdminQuality : ''}
                        </td>

                        <td>
                            ${task.PerformerQuality != null ? task.PerformerQuality : ''}
                        </td>`;

            tr.innerHTML = innerHtml;

            return tr;
        }

    static GetTaskElementForNotPerformedTr(task: ChemistryTaskModel) {

            var dateNow = new Date();

            var _class = task.DeadLineDate > dateNow ? "success" : "danger";

            var tr = document.createElement("tr");

            tr.classList.add(_class);

            var innerHtml = `

                        <td>
                            <a href="/Chemistry/Tasks/Task/${task.Id}">
                                ${task.Title}
                            </a>
                        </td>

                        <td>
                            <a href="/Chemistry/Chemistry?id=${task.PerformerUser.Id}">
                                ${task.PerformerUser.Name} ${task.PerformerUser.Email}
                            </a>
                        </td>

                        <td>
                            ${moment(task.DeadLineDate).locale('ru').format('DD MMMM YYYY, hh:mm:ss')}
                        </td>

                        <td>`;

        if (task.ChemistryMethodFile != null) {
                innerHtml += `
                                <a href="/Files/GetDbFileById?id=${task.ChemistryMethodFile.FileId}" class="btn btn-primary">
                                    ${task.ChemistryMethodFile.Name}
                                </a>`;
            }
            else {
                innerHtml += "[Метод не указан]";
            }

            innerHtml += `

                        </td>

                        <td>
                            ${task.AdminQuality != null ? task.AdminQuality : ""}
                        </td>

                        <td>
                            ${task.AdminQuantity != null ? task.AdminQuantity : ""}
                        </td>`;

            tr.innerHTML = innerHtml;

            return tr;
        }
}

class TasksFilter {

    Tasks: Array<ChemistryTaskModel>;

    constructor() {
        this.Tasks = null;
    }


        SetTasks(tasks) {
            this.Tasks = tasks;
        }

        ApplyFilter() {

            var days = (document.getElementsByName("days")[0] as HTMLInputElement).value;

            if (this.Tasks == null) {
                alert("Не установлены названия")
            }

            var tasks = days == null || days == "" ? this.Tasks : this.ApplyDaysFilter(this.Tasks, days);

            var q = (document.getElementsByName("SearchTasks.Q")[0] as HTMLInputElement).value;

            console.log(`ApplyFilterBeforeText q = ${q}`, tasks);

            tasks = this.ApplyTextFilter(tasks, q);

            console.log("ApplyFilterAfterText", tasks);

            tasks = this.SortTasks(tasks);

            this.ShowAndHideTasks(tasks);
        }

        
    ApplyDaysFilter(tasks: Array<ChemistryTaskModel>, n) {

            console.log("ApplyDaysFilter", tasks, n);

            var dateNow = Date.now();

        var result = tasks.filter(function (x) {
            var date = Date.parse(x.CreationDate.toDateString());

                var timeDiff = Math.abs(dateNow - date);

                var diffDays = Math.abs(Math.ceil(timeDiff / (1000 * 3600 * 24)));

                return diffDays <= n;
            });

            return result;
        }

    ApplyTextFilter(tasks: Array<ChemistryTaskModel>, q): Array<ChemistryTaskModel> {
            if (q == null || q === "") {
                return tasks;
            }

            var result = tasks.filter(function (x) {
                //if (x.PerformerText != null && x.PerformerText.includes(q)) {
                //    return true;
                //}

                return (x.Title != null && x.Title.toLowerCase().includes(q.toLowerCase()));
            });

            return result;
        }

    ShowAndHideTasks(tasks: Array<ChemistryTaskModel>) {

            var tBody = document.getElementById("performed-tbody");

            tBody.innerHTML = "";

            var performed = tasks.filter(x => x.IsPerformed);

            for (var i = 0; i < performed.length; i++) {
                tBody.appendChild(IndexPageTaskDrawer.GetTaskElementForPerformedTr(performed[i]));
            }

            tBody = document.getElementById("not-performed-body");

            tBody.innerHTML = "";

            var notPerformed = tasks.filter(x => !x.IsPerformed);

            for (let i = 0; i < notPerformed.length; i++) {
                tBody.appendChild(IndexPageTaskDrawer.GetTaskElementForNotPerformedTr(notPerformed[i]));
            }
        }

    SortTasks(tasks: Array<ChemistryTaskModel>) {

            var sort = (document.getElementsByName("sort")[0] as HTMLInputElement).value;

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


    SortByPerformedDate(tasks: Array<ChemistryTaskModel>) {

            var result = tasks.sort(function (a, b) {

                var aDate = Date.parse(a.PerformedDate.toDateString());

                var bDate = Date.parse(b.PerformedDate.toDateString());

                if (aDate > bDate) {
                    return 1;
                }
                if (aDate < bDate) {
                    return -1;
                }
                // a должно быть равным b
                return 0;
            })

            return result;
        }

    SortByPerformerName(tasks: Array<ChemistryTaskModel>): Array<ChemistryTaskModel> {

            var result = tasks.sort(function (a, b) {



                if (a.PerformerUser.Name > b.PerformerUser.Name) {
                    return 1;
                }
                if (a.PerformerUser.Name < b.PerformerUser.Name) {
                    return -1;
                }
                // a должно быть равным b
                return 0;
            })

            return result;

        }
}