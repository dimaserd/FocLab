declare class TaskModalWorker {
    static ShowDayTaskModal(task: DayTaskModel): void;
    static InitTask(task: DayTaskModel): void;
    static DrawComments(divId: string, task: DayTaskModel): void;
    static ClearContent(): void;
    static MakeCommentFieldEditable(commentId: string): void;
    static ResetCommentChanges(commentId: string, text: string): void;
}
