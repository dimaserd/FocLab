interface AdminDayTaskCreatorProps {
    AssigneeUserId: string;
}

class AdminDayTaskCreator {
    AssigneeUserId: string;
    TaskDate: string;

    constructor(props: AdminDayTaskCreatorProps) {
        this.AssigneeUserId = props.AssigneeUserId;
        this.TaskDate = "";

    }

    SetDate(date: string): void {
        const a = date.split(".");

        date = `${a[1]}.${a[0]}.${a[2]}`;

        this.TaskDate = date;
    }

    ProccessData(data) {
        data.AssigneeUserId = this.AssigneeUserId;
        data.TaskDate = this.TaskDate;

        return data;
    }



    CreateDayTask(data) : void {

        data = this.ProccessData(data);

        Requester.SendPostRequestWithAnimation("/Api/DayTask/Create", data, x => {
            if (x.IsSucceeded) {
                DayTasksWorker.GetTasks();
            }
        }, null);
    }

    EditDayTask(data): void {

        Requester.SendPostRequestWithAnimation("/Api/DayTask/Update", data, x => {
            if (x.IsSucceeded) {
                DayTasksWorker.GetTasks();
            }
        }, null);
    }

}