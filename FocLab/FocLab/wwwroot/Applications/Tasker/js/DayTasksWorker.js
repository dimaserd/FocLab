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