declare class TaskModalWorker {
    static InitTask: (task: DayTaskModel, accountId: string) => void;
    static DrawComments: (divId: string, userId: string, task: DayTaskModel) => void;
    static ClearContent: () => void;
    static MakeCommentFieldEditable: (commentId: string) => void;
    static ResetCommentChanges: (commentId: string, text: string) => void;
}
