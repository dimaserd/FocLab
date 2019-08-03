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
