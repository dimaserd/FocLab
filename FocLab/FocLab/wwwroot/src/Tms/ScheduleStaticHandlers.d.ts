interface UpdateDayTaskComment {
    DayTaskCommentId: string;
    Comment: string;
}
interface AddComment {
    DayTaskId: string;
    Comment: string;
}
interface ShowUserSchedule {
    UserIds: Array<string>;
}
interface UpdateDayTask {
    TaskText: string;
    TaskTitle: string;
    AssigneeUserId: string;
    Id: string;
    TaskDate: string;
}
interface CreateDayTask {
    TaskText: string;
    TaskTitle: string;
    AssigneeUserId: string;
    TaskDate: string;
}
declare class ScheduleStaticHandlers {
    static ShowUserSchedule(): void;
    static ShowDayTaskModal(taskId: string): void;
    static ShowCreateTaskModal: () => void;
    static updateComment(commentId: string): void;
    static addComment(): void;
    static updateDayTask(): void;
    static createDayTask(): void;
    static hideCreateModal(): void;
    static redirectToProfile: (profileId: string) => void;
}
