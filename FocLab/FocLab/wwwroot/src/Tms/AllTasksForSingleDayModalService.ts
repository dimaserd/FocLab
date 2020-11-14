class AllTasksForSingleDayModalService {
    public static DrawTasksOnModal(tasks: DayTaskModel[]): void {
        let html = "";

        html += '<div class="container">'

        for (var i = 0; i < tasks.length; i++) {
            let task = tasks[i];

            html += 
            `<div class="row bg-primary rounded text-white">
                <a style="cursor:pointer" class="col-md-12 tms-show-task-modal-big" data-task-id="${task.Id}" title="${task.TaskTitle}">
                    ${task.TaskTitle}
                    <span class="float-right" data-toggle="tooltip" data-placement="top" title="${task.AssigneeUser.Email}">
                        ${ColorAvatarInitor.InitColorForAvatar(task)}
                    </span>
                </a>
            </div>`;

            html += `<div class="row" style="height:5px"></div>`;
        }

        html += '</div>';

        document.getElementById("show-day-tasks-modal-body").innerHTML = html;
    }
}