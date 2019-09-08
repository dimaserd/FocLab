declare class TaskStaticHandlers {
    static TaskId: string;
    static UpdateBtnClickHandler(): void;
    static UpdateFileByType(fileType: string): void;
    static RemoveTask(id: string): void;
    static CancelRemoving(id: any): void;
}
