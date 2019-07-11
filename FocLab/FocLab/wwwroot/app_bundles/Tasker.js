class AdminDayTaskCreator {
    constructor(props) {
        this.AssigneeUserId = props.AssigneeUserId;
        this.TaskDate = "";
        this.setHandlers();

        this.IsAjaxGoing = false;
    }

    setHandlers() {

        this.SetDate = function (date) {
            const a = date.split(".");

            date = `${a[1]}.${a[0]}.${a[2]}`;

            this.TaskDate = date;
        }

        this.ProccessData = function (data) {
            data.AssigneeUserId = this.AssigneeUserId;
            data.TaskDate = this.TaskDate;

            return data;
        }



        this.CreateDayTask = function (data) {

            data = this.ProccessData(data);

            Requester.SendPostRequestWithAnimation("/Api/DayTask/Create", data, x => {
                if (x.IsSucceeded) {
                    dayTasksWorker.GetTasks();
                }
            });
        }

        this.EditDayTask = function (data) {

            Requester.SendPostRequestWithAnimation("/Api/DayTask/Update", data, x => {
                if (x.IsSucceeded) {
                    dayTasksWorker.GetTasks();
                }
            });
        }
    }
}
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
class DayTaskEditor {
    constructor() {
        this.setHandlers();
    }

    setHandlers() {

        this.UpdateHtmlProperties = function (data) {

            ModalWorker.ShowModal("loadingModal");

            Requester.SendPostRequestWithAnimation('/Api/DayTask/Update', data, x => {
                if (x.IsSucceeded) {
                    dayTasksWorker.GetTasks();
                }
            });
        }
    }
}
class DayTasksWorker {
    constructor(props) {
        this.Tasks = props.Tasks;
        this.IsAdmin = props.IsAdmin;
        this.User = props.User;
        this.SearchModel = props.SearchModel;

        this.OpenTaskId = props.OpenTaskId;

        this.MyUserId = props.MyUserId;

        this.setHandlers();

        this.CurrentTaskId = null;
        this.CurrentTask = null;

        this.IsAjaxGoing = false;
        this.Drawer = new DayTaskDrawer();
    }

    setHandlers() {

        this.OpenTaskById = function () {

            var taskId = this.OpenTaskId;

            const task = this.Tasks.filter(x => x.Id === taskId)[0];

            if (task != null) {
                //открываю модал по заданию полученному из ссылки
                ShowDayTaskModal(task.Id);
            }
        }

        this.SetCurrentTaskId = function (taskId) {
            this.CurrentTaskId = taskId;

            this.CurrentTask = this.Tasks.filter(x => x.Id === taskId)[0];
        }

        this.SendNotificationToAdmin = function () {

            ModalWorker.ShowModal("loadingModal");

            Requester.SendPostRequestWithAnimation("/Api/DayTask/SendToAdmin",
                { Id: this.CurrentTaskId });
        }

        this.GetTasks = function () {

            Requester.SendAjaxPost("/Api/DayTask/GetTasks", this.SearchModel, x => {
                this.Tasks = x;
                this.Drawer.DrawTasks(this.Tasks, true);
                this.OpenTaskById();
            });
        }
        
        this.GetTaskById = function (taskId) {
            return this.Tasks.filter(function (x) { return x.Id === taskId })[0];
        }
    }
}
class EditableComponents {
    constructor() {
        this.setHandlers();

        this.Editables = [];
    }

    setHandlers() {
        this.InitEditable = function (element, onValueChangedHandler, isTextArea = false) {

            //Получить уникальный идентификатор
            const elementId = Math.random().toString(36);

            //Скрываю настоящий элемент
            element.style.display = "none";
            element.setAttribute("data-editable-real-input", elementId);
            
            
            //Добавляю идентификатор элемента, чтобы потом отлавливать его и изменения его значений
            this.Editables.push({
                ElementId: elementId,
                Value: element.value,
                OnValueChangedHandler: onValueChangedHandler
            });
            
            // Получаю фейковый элемент который редактируем
            const newNode = this.GetFakeElement(element, elementId, isTextArea);

            // Вставляю новый элемент перед скрытым старым
            element.before(newNode);

            this.InitEditableInner(elementId);
            this.InitLiasoningOnChange(element, elementId);
        }

        this.InitLiasoningOnChange = function (element, elementId) {
            element.addEventListener("change", function() {

                console.log("Обработчик реального измененного элемента");
                let elemId = elementId;
                const fakeElement = document.querySelectorAll(`[data-editable-input="${elemId}"]`)[0];

                fakeElement.value = this.value;

            });

            const fakeElement = document.querySelectorAll(`[data-editable-input="${elementId}"]`)[0];

            fakeElement.addEventListener("change", function () {
                
                let elemId = elementId;
                const realElement = document.querySelectorAll(`[data-editable-real-input="${elemId}"]`)[0];

                realElement.value = this.value;
            });
        }

        //Создать фейковый элемент 
        this.GetFakeElement = function (element, elementId, isTextArea) {

            const div = document.createElement("div");

            const html = `<div class="form-group editable" data-editable-form="${elementId}">
        <div class="editable-backdrop" data-editable-backdrop="${elementId}"></div>
        ${this.GetFakeInputElement(element, elementId, isTextArea)}
        <div class="editable-buttons">
            <button class="btn btn-sm btn-editable" data-editable-check="${elementId}"><i class="fas fa-check"></i></button>
            <button class="btn btn-sm btn-editable" data-editable-cancel="${elementId}"><i class="fas fa-times"></i></button>
        </div>
        </div>`;

            div.innerHTML = html;

            return div;
        }

        this.GetFakeInputElement = function (element, elementId, isTextArea) {

            if (!isTextArea) {
                return `<input type="text" class="${element.className} editable-input" name="${element.id}" data-editable-input="${elementId}"
                    data-editable-input-id="${elementId}" value="${element.value}">`;
            }

            return `<textarea class="${element.className} editable-input" name="${element.id}" data-editable-input="${elementId}"
                    data-editable-input-id="${elementId}">${element.value}</textarea>`;
        }

        //Сюда передаётся фейковый элемент
        this.InitEditableInner = function (elementId) {

            const element = document.querySelectorAll(`[data-editable-input="${elementId}"]`)[0];

            //Устанавливаю на сам инпут
            element.onclick = this.OnInputClickHandler;

            //<button class="btn btn-sm btn-editable"><i class="fas fa-check"></i></button>
            const btnCheck = document.querySelectorAll(`[data-editable-check="${elementId}"]`)[0];

            btnCheck.addEventListener("click", (function () { this.OnBtnCheckClickHandler(elementId) }).bind(this));

            //<button class="btn btn-sm btn-editable"><i class="fas fa-times"></i></button>
            const btnBack = document.querySelectorAll(`[data-editable-cancel="${elementId}"]`)[0];

            btnBack.addEventListener("click", (function () { this.OnBtnCancelClickHandler(elementId) }).bind(this));

            const backDrop = document.querySelectorAll(`[data-editable-backdrop="${elementId}"]`)[0];

            backDrop.addEventListener("click", (function () { this.BackDropClickHandler(elementId) }).bind(this));
        }

        this.CheckValueChanged = function(elementId) {
            //Получаю инпут
            const input = document.querySelectorAll(`[data-editable-input="${elementId}"]`)[0];

            const record = this.Editables.find(x => x.ElementId === elementId);

            const newValue = input.value;

            //значит значение изменилось
            if (record.Value !== newValue) {

                //Вызываю обработчик события для измененного значения
                if (record.OnValueChangedHandler) {
                    record.OnValueChangedHandler(newValue);
                }
                record.Value = newValue;
                this.Editables = this.Editables.filter(x => x.ElementId !== elementId);
                this.Editables.push(record);
            }
        }

        this.BackDropClickHandler = function (elementId) {

            const form = document.querySelectorAll(`[data-editable-form="${elementId}"]`)[0];

            form.classList.remove("editable--active");

            //Получаю инпут
            const input = document.querySelectorAll(`[data-editable-input="${elementId}"]`)[0];

            input.removeAttribute("editable");

            this.CheckValueChanged(elementId);
        }

        //Клик на редактируемый инпут
        this.OnInputClickHandler = function () {

            const elementId = this.getAttribute("data-editable-input-id");

            //Получаю инпут
            const input = document.querySelectorAll(`[data-editable-input="${elementId}"]`)[0];

            input.setAttribute("editable", "editable");

            //Получаю форму
            const form = document.querySelectorAll(`[data-editable-form="${elementId}"]`)[0];
            
            form.classList.add("editable--active");
        }

        this.OnBtnCheckClickHandler = function (elementId) {

            const input = document.querySelectorAll(`[data-editable-input="${elementId}"]`)[0];
            input.removeAttribute("editable");

            const form = document.querySelectorAll(`[data-editable-form="${elementId}"]`)[0];
            form.classList.remove("editable--active");


            this.CheckValueChanged(elementId);
        }

        this.OnBtnCancelClickHandler = function (elementId) {

            const input = document.querySelectorAll(`[data-editable-input="${elementId}"]`)[0];
            input.removeAttribute("editable");

            const form = document.querySelectorAll(`[data-editable-form="${elementId}"]`)[0];
            form.classList.remove("editable--active");

            const record = this.Editables.find(x => x.ElementId === elementId);

            //возращение к первичному значению
            input.value = record.Value;
        }
    }
}

const editableComponents = new EditableComponents();
class EditDayTaskModalWorker {

    constructor() {
        this.setHandlers();
    }

    setHandlers() {

    }
}


class ScheduleWorker {
    
    constructor(filter) {
        this.filter = filter;
        this.setHandlers();
        this.SetUsersSelect();
        this.Users = [];
    }

    setHandlers() {
        this.SetUsersSelect = function () {
            
            Requester.SendAjaxPost("/Api/User/Get",
                { Count: null, OffSet: 0 },
                (x => {
                    console.log("/Api/User/Get", x);

                    this.Users = x.List;

                    $(".usersSelect").select2({
                        placeholder: "Выберите пользователя",

                        language: {
                            "noResults": function () {
                                return "Пользователь не найден.";
                            }
                        },

                        data: x.List.map(t => ({
                            id: t.Id,
                            text: `${t.Name} ${t.Email}`,
                            avatarId: t.AvatarFileId
                        })),

                        templateSelection: formatStateSelection,
                        templateResult: formatStateResult,

                        escapeMarkup: function (markup) {
                            return markup;
                        }
                    });

                    FormDataHelper.FillDataByPrefix("filter.", {
                        UserIds: this.filter.UserIds
                    });

                    $("#usersSelect").val(this.filter.UserIds).trigger('change.select2');
                    $('.select2-selection__rendered img').addClass('m--img-rounded m--marginless m--img-centered');

                }).bind(this));
        }
    }
}