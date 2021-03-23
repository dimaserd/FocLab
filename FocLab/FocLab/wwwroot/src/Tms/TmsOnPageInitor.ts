class TmsOnPageInitor {
    static Init() {
        var filter = window["tms-filter"];
        console.log("TmsOnPageInitor.Init", filter);
        ScheduleWorker.Constructor(filter);

        var props: DayTasksWorkerProps = {
            Tasks: null,
            User: null,
            IsAdmin: false,
            MyUserId: AccountWorker.User?.Id,
            OpenTaskId: null,
            SearchModel: filter
        };

        DayTasksWorker.Constructor(props);
        ScheduleStaticHandlers.Filter = filter;

        document.addEventListener("DOMContentLoaded", () => {

            DayTasksWorker.GetTasks();

            $("#usersSelect").select2().on("change", () => { ScheduleStaticHandlers.OnUsersSelectChanged(); });
        });

        $("body").tooltip({ selector: '[data-toggle=tooltip]' });
    }
}

TmsOnPageInitor.Init();