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
        Requester.SendPostRequestWithAnimation('/Api/Chemistry/Reagents/ForTask/CreateOrUpdate', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    };
    TaskReagentWorker.prototype.EditTaskReagent = function (reagentId) {
        console.log("TaskReagentWorker.EditTaskReagent", reagentId);
        var data = {
            TaskId: this.TaskId,
            TakenQuantity: this.GetValueByName("Edit.TakenQuantity." + reagentId),
            ReturnedQuantity: this.GetValueByName("Edit.ReturnedQuantity." + reagentId),
            ReagentId: reagentId
        };
        Requester.SendPostRequestWithAnimation('/Api/Chemistry/Reagents/ForTask/CreateOrUpdate', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    };
    TaskReagentWorker.prototype.RemoveTaskReagent = function (reagentId) {
        console.log("TaskReagentWorker.RemoveTaskReagent", reagentId);
        var data = {
            TaskId: this.TaskId,
            ReagentId: reagentId
        };
        Requester.SendPostRequestWithAnimation('/Api/Chemistry/Reagents/ForTask/Remove', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    };
    return TaskReagentWorker;
}());
