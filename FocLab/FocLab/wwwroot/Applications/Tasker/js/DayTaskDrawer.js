class DayTaskDrawer {
    constructor() {
        this.setHandlers();
    }

    setHandlers() {

        this.DrawTasks = function (tasks, isAdmin) {

            this.ClearTasks();

            for (let i = 0; i < tasks.length; i++) {

                const task = tasks[i];

                this.AddTaskToDate(task);

            }

            if (isAdmin) {
                this.AddAdminActions();
            }
        }

        this.AddTaskToDate = function (task) {
            const dateTrailed = moment(task.TaskDate).format("DD.MM.YYYY");

            console.log(dateTrailed);

            const elem = document.querySelector(`[data-date='${dateTrailed}']`);


            $(elem).children(".no-tasks-text").remove();

            console.log(elem, task);

            const toAdd = document.createElement("div");

            toAdd.innerHTML = `<a class="event d-block p-1 pl-2 pr-2 mb-1 rounded text-truncate small bg-primary text-white" onclick="ShowDayTaskModal('${task.Id}')" title="${task.TaskTitle}">
                                        ${task.TaskTitle}
                                        <span class="float-right" data-toggle="tooltip" data-placement="top" title="${task.AssigneeUser.Email}">
                                            ${ColorAvatarInitor.InitColorForAvatar(task)}
                                        </span>
                                   </a>`;

            elem.appendChild(toAdd);
        }

        this.AddAdminActions = function () {

            const elem = document.getElementById("createTaskBtn");
            elem.innerHTML = '';
            const toAdd = document.createElement("div");

            toAdd.innerHTML = `<a class="btn float-right pl-2 pr-2 mb-1 rounded text-truncate bg-success text-white d-none d-lg-inline" onclick="ShowCreateTaskModal()">
                        <i class="fas fa-plus-circle fa-fw" style="font-size: 0.8rem;"></i> Создать задание
                    </a>
                    <a class="btn float-right pl-2 pr-2 mb-1 rounded text-truncate bg-success text-white  d-inline d-lg-none" onclick="ShowCreateTaskModal()">
                        <i class="fas fa-plus-circle fa-fw" style="font-size: 0.8rem;"></i> Создать
                    </a>`;

            elem.appendChild(toAdd);

        }
        //Удаляет все нарисованные задания на календаре
        this.ClearTasks = function () {
            const paras = document.getElementsByClassName("event");

            while (paras[0]) {
                paras[0].parentNode.removeChild(paras[0]);
            }
        }
    }
}