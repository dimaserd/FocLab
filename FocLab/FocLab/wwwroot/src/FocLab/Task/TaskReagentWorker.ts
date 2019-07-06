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

        Requester.SendPostRequestWithAnimation('/Chemistry/Reagent/AddTaskReagent', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null)
    }    

    public EditTaskReagent(reagentId: string) {
        var data = {
            TaskId: this.TaskId,
            TakenQuantity: this.GetValueByName(`Edit.TakenQuantity.${reagentId}`),
            ReturnedQuantity: this.GetValueByName(`Edit.ReturnedQuantity.${reagentId}`),
            ReagentId: reagentId
        };


        Requester.SendPostRequestWithAnimation('/Chemistry/Reagent/EditTaskReagent', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null)
    }    

    public RemoveTaskReagent(reagentId: string) {
        var data = {
            TaskId: this.TaskId,
            ReagentId: reagentId
        };

        Requester.SendPostRequestWithAnimation('/Chemistry/Reagent/RemoveTaskReagent', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null)
    }    
}