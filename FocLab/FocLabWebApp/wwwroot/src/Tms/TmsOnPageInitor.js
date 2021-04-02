var TmsOnPageInitor = (function () {
    function TmsOnPageInitor() {
    }
    TmsOnPageInitor.Init = function () {
        var _a;
        var filter = window["tms-filter"];
        console.log("TmsOnPageInitor.Init", filter);
        ScheduleWorker.Constructor(filter);
        var props = {
            Tasks: null,
            User: null,
            IsAdmin: false,
            MyUserId: (_a = AccountWorker.User) === null || _a === void 0 ? void 0 : _a.Id,
            OpenTaskId: null,
            SearchModel: filter
        };
        DayTasksWorker.Constructor(props);
        ScheduleStaticHandlers.Filter = filter;
        document.addEventListener("DOMContentLoaded", function () {
            DayTasksWorker.GetTasks();
            $("#usersSelect").select2().on("change", function () { ScheduleStaticHandlers.OnUsersSelectChanged(); });
        });
        $("body").tooltip({ selector: '[data-toggle=tooltip]' });
    };
    return TmsOnPageInitor;
}());
TmsOnPageInitor.Init();
