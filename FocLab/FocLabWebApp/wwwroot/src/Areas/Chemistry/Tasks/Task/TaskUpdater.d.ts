declare class TaskUpdater {
    TaskId: string;
    constructor(taskId: string);
    UploadUserFile(fileType: string): void;
}
