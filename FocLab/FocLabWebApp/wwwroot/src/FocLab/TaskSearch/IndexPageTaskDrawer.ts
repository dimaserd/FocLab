class IndexPageTaskDrawer {
    static GetNotFoundElement(colspan: number): HTMLTableRowElement {
        var tr = document.createElement("tr");
        var innerHtml = `
        <td colspan="${colspan}">
            <i class="far fa-frown"></i>
            Не найдено
        </td>`;
        tr.innerHTML = innerHtml;
        return tr;
    }
    static GetTaskElementForPerformedTr(task: ChemistryTaskModel): HTMLTableRowElement {
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
                            <a href="/Chemistry/Tasks?id=${task.PerformerUser.Id}">
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
