var IndexPageTaskDrawer = /** @class */ (function () {
    function IndexPageTaskDrawer() {
    }
    IndexPageTaskDrawer.prototype.GetTaskElementForPerformedTr = function (task) {
        var _class = task.IsPerformedInTime ? "success" : "danger";
        var tr = document.createElement("tr");
        tr.classList.add(_class);
        tr.classList.add("performed-chemistry-task");
        var innerHtml = "\n\n                        <td>\n                            <a href=\"/Chemistry/Tasks/Task/" + task.Id + "\">\n                                " + task.Title + "\n                            </a>\n                        </td>\n\n                        <td>\n                            <a href=\"/Chemistry/Chemistry?id=" + task.PerformerUser.UserId + "\">\n                                " + task.PerformerUser.Name + " " + task.PerformerUser.Email + "\n                            </a>\n                        </td>\n\n                        <td>\n                            " + moment(task.DeadLineDate).locale('ru').format('DD MMMM YYYY, hh:mm:ss') + "\n                        </td>\n\n                        <td>\n                            " + moment(task.PerformedDate).locale('ru').format('DD MMMM YYYY, hh:mm:ss') + "\n                        </td>\n\n                        <td>\n                            " + (task.AdminQuantity != null ? task.AdminQuantity : '') + "\n                        </td>\n\n                        <td>\n                            " + (task.PerformerQuantity != null ? task.PerformerQuantity : '') + "\n                        </td>\n\n                        <td>\n                            " + (task.AdminQuality != null ? task.AdminQuality : '') + "\n                        </td>\n\n                        <td>\n                            " + (task.PerformerQuality != null ? task.PerformerQuality : '') + "\n                        </td>";
        tr.innerHTML = innerHtml;
        return tr;
    };
    IndexPageTaskDrawer.prototype.GetTaskElementForNotPerformedTr = function (task) {
        var dateNow = new Date();
        var _class = task.DeadLineDate > dateNow ? "success" : "danger";
        var tr = document.createElement("tr");
        tr.classList.add(_class);
        var innerHtml = "\n\n\n                        <td>\n                            <a href=\"/Chemistry/Tasks/Task/" + task.Id + "\">\n                                " + task.Title + "\n                            </a>\n                        </td>\n\n                        <td>\n                            <a href=\"/Chemistry/Chemistry?id=" + task.PerformerUser.UserId + "\">\n                                " + task.PerformerUser.Name + " " + task.PerformerUser.Email + "\n                            </a>\n                        </td>\n\n                        <td>\n                            " + moment(task.DeadLineDate).locale('ru').format('DD MMMM YYYY, hh:mm:ss') + "\n                        </td>\n\n                        <td>";
        if (task.MethodFile != null) {
            innerHtml += "\n                                <a href=\"/Files/GetDbFileById?id=" + task.MethodFile.FileId + "\" class=\"btn btn-primary\">\n                                    " + task.MethodFile.Name + "\n                                </a>";
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
