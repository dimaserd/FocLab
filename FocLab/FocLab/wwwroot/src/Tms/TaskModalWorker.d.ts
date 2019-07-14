declare class TaskModalWorker {
    static InitTask: (task: any, accountId: string) => void;
    static DrawComments: (divId: string, userId: string, task: any) => void;
    static ClearContent: () => void;
    static MakeCommentFieldEditable: (commentId: string) => void;
    static ResetCommentChanges: (commentId: string, text: string) => void;
}
