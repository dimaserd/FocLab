class TaskReagentWorker {

    TaskId: string;

    constructor(taskId: string) {
        this.TaskId = taskId;
    }

    public GetValueByName(name: string) {
        return (document.getElementsByName(name)[0] as HTMLInputElement).value;
    }

    public CreateTaskReagent() {

        var data = {
            TaskId: this.TaskId,
            TakenQuantity: this.GetValueByName("Create.TakenQuantity"),
            ReturnedQuantity: this.GetValueByName("Create.ReturnedQuantity"),
            ReagentId: this.GetValueByName("Create.ReagentId")
        };

        CrocoAppCore.Application.Requester.SendPostRequestWithAnimation('/Api/Chemistry/Reagents/ForTask/CreateOrUpdate', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null)
    }    

    public EditTaskReagent(reagentId: string) {

        console.log("TaskReagentWorker.EditTaskReagent", reagentId);

        var data = {
            TaskId: this.TaskId,
            TakenQuantity: this.GetValueByName(`Edit.TakenQuantity.${reagentId}`),
            ReturnedQuantity: this.GetValueByName(`Edit.ReturnedQuantity.${reagentId}`),
            ReagentId: reagentId
        };

        CrocoAppCore.Application.Requester.SendPostRequestWithAnimation('/Api/Chemistry/Reagents/ForTask/CreateOrUpdate', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null)
    }    

    public RemoveTaskReagent(reagentId: string) {

        console.log("TaskReagentWorker.RemoveTaskReagent", reagentId);

        var data = {
            TaskId: this.TaskId,
            ReagentId: reagentId
        };

        CrocoAppCore.Application.Requester.SendPostRequestWithAnimation('/Api/Chemistry/Reagents/ForTask/Remove', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null)
    }    
}