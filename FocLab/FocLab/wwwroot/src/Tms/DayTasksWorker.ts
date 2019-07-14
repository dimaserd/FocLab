interface DayTasksWorkerProps {
    Tasks: Array<DayTaskModel>;
    IsAdmin: boolean;
    User: any;
    SearchModel: any;
    OpenTaskId: string;
    MyUserId: string;
}

class DayTasksWorker {

    static Tasks: Array<DayTaskModel>;
    static IsAdmin: boolean;
    static User: any;
    static SearchModel: any;
    static OpenTaskId: string;
    static MyUserId: string;
    static CurrentTaskId: string;
    static CurrentTask: DayTaskModel;

    static Drawer: DayTaskDrawer;

    static Constructor(props: DayTasksWorkerProps) {
        this.Tasks = props.Tasks;
        this.IsAdmin = props.IsAdmin;
        this.User = props.User;
        this.SearchModel = props.SearchModel;

        this.OpenTaskId = props.OpenTaskId;

        this.MyUserId = props.MyUserId;

        this.CurrentTaskId = null;
        this.CurrentTask = null;

        this.Drawer = new DayTaskDrawer();
    }

    static OpenTaskById() : void {

        var taskId = this.OpenTaskId;

        const task = this.Tasks.find(x => x.Id === taskId);

        if (task != null) {
            //открываю модал по заданию полученному из ссылки
            ScheduleStaticHandlers.ShowDayTaskModal(task.Id);
        }
    }

    static SetCurrentTaskId(taskId: string) {
        this.CurrentTaskId = taskId;

        this.CurrentTask = this.Tasks.find(x => x.Id === taskId);
    }

    static SendNotificationToAdmin() {

        ModalWorker.ShowModal("loadingModal");

        Requester.SendPostRequestWithAnimation("/Api/DayTask/SendToAdmin", { Id: this.CurrentTaskId }, x => alert(x), null);
    }

    static GetTasks(): void {

        Requester.SendAjaxPost("/Api/DayTask/GetTasks", this.SearchModel, x => {
            this.Tasks = x as Array<DayTaskModel>;
            this.Drawer.DrawTasks(this.Tasks, true);
            this.OpenTaskById();
        }, null, false);
    }

    static GetTaskById(taskId: string): DayTaskModel {
        return this.Tasks.find(x => x.Id === taskId);
    }
}