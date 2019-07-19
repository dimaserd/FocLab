///<reference path="../../../node_modules/moment/moment.d.ts"/>

declare var moment: Function;

class DayTaskDrawer {

    DrawTasks(tasks: Array<DayTaskModel>, isAdmin: boolean) {

        this.ClearTasks();
        for (let i = 0; i < tasks.length; i++) {

            const task = tasks[i];

            this.AddTaskToDate(task);

        }

        if (isAdmin) {
            this.AddAdminActions();
        }


        ScheduleStaticHandlers.SetHandlers();
    }       

    AddTaskToDate(task: DayTaskModel) {
        const dateTrailed = moment(task.TaskDate).format("DD.MM.YYYY");

        const elem = document.querySelector(`[data-date='${dateTrailed}']`);


        $(elem).children(".no-tasks-text").hide();

        const toAdd = document.createElement("div");

        toAdd.innerHTML = `<a class="event d-block p-1 pl-2 pr-2 mb-1 rounded text-truncate small bg-primary text-white tms-show-task-modal" data-task-id="${task.Id}" title="${task.TaskTitle}">
                                        ${task.TaskTitle}
                                        <span class="float-right" data-toggle="tooltip" data-placement="top" title="${task.AssigneeUser.Email}">
                                            ${ColorAvatarInitor.InitColorForAvatar(task)}
                                        </span>
                                   </a>`;

        elem.appendChild(toAdd);
    }    

    AddAdminActions() {

        const elem = document.getElementById("createTaskBtn");
        elem.innerHTML = '';
        const toAdd = document.createElement("div");

        toAdd.innerHTML = `<a class="btn float-right pl-2 pr-2 mb-1 rounded text-truncate bg-success text-white d-none d-lg-inline tms-btn-create-task">
                        <i class="fas fa-plus-circle fa-fw" style="font-size: 0.8rem;"></i> Создать задание
                    </a>
                    <a class="btn float-right pl-2 pr-2 mb-1 rounded text-truncate bg-success text-white  d-inline d-lg-none tms-btn-create-task">
                        <i class="fas fa-plus-circle fa-fw" style="font-size: 0.8rem;"></i> Создать
                    </a>`;

        elem.appendChild(toAdd);
    }    
        
    //Удаляет все нарисованные задания на календаре
    ClearTasks() {
        const paras = document.getElementsByClassName("event");

        while (paras[0]) {
            paras[0].parentNode.removeChild(paras[0]);
        }
    }
}