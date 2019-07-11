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