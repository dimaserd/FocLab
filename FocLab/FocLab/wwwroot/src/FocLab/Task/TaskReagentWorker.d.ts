declare class TaskReagentWorker {
    TaskId: string;
    constructor(taskId: string);
    GetValueByName(name: string): string;
    CreateTaskReagent(): void;
    EditTaskReagent(reagentId: string): void;
    RemoveTaskReagent(reagentId: string): void;
}
