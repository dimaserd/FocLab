var TaskReagentWorker = /** @class */ (function () {
    function TaskReagentWorker(taskId) {
        this.TaskId = taskId;
    }
    TaskReagentWorker.prototype.GetValueByName = function (name) {
        return document.getElementsByName(name)[0].value;
    };
    TaskReagentWorker.prototype.CreateTaskReagent = function () {
        var data = {
            TaskId: this.TaskId,
            TakenQuantity: this.GetValueByName("Create.TakenQuantity"),
            ReturnedQuantity: this.GetValueByName("Create.ReturnedQuantity"),
            ReagentId: this.GetValueByName("Create.ReagentId")
        };
        Requester.SendPostRequestWithAnimation('/Chemistry/Reagent/AddTaskReagent', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    };
    TaskReagentWorker.prototype.EditTaskReagent = function (reagentId) {
        var data = {
            TaskId: this.TaskId,
            TakenQuantity: this.GetValueByName("Edit.TakenQuantity." + reagentId),
            ReturnedQuantity: this.GetValueByName("Edit.ReturnedQuantity." + reagentId),
            ReagentId: reagentId
        };
        Requester.SendPostRequestWithAnimation('/Chemistry/Reagent/EditTaskReagent', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    };
    TaskReagentWorker.prototype.RemoveTaskReagent = function (reagentId) {
        var data = {
            TaskId: this.TaskId,
            ReagentId: reagentId
        };
        Requester.SendPostRequestWithAnimation('/Chemistry/Reagent/RemoveTaskReagent', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    };
    return TaskReagentWorker;
}());
