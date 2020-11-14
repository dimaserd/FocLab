var AllTasksForSingleDayModalService = (function () {
    function AllTasksForSingleDayModalService() {
    }
    AllTasksForSingleDayModalService.DrawTasksOnModal = function (tasks) {
        var html = "";
        html += '<div class="container">';
        for (var i = 0; i < tasks.length; i++) {
            var task = tasks[i];
            html +=
                "<div class=\"row bg-primary rounded text-white\">\n                <a style=\"cursor:pointer\" class=\"col-md-12 tms-show-task-modal-big\" data-task-id=\"" + task.Id + "\" title=\"" + task.TaskTitle + "\">\n                    " + task.TaskTitle + "\n                    <span class=\"float-right\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"" + task.AssigneeUser.Email + "\">\n                        " + ColorAvatarInitor.InitColorForAvatar(task) + "\n                    </span>\n                </a>\n            </div>";
            html += "<div class=\"row\" style=\"height:5px\"></div>";
        }
        html += '</div>';
        document.getElementById("show-day-tasks-modal-body").innerHTML = html;
    };
    return AllTasksForSingleDayModalService;
}());
