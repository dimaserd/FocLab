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
interface CreateOrUpdateDayTask {
    Id: string;
    TaskText: string;
    TaskTitle: string;
    AssigneeUserId: string;
    TaskDate: Date;
    TaskTarget: string;
    TaskReview: string;
    TaskComment: string;
}
interface CreateDayTask {
    TaskText: string;
    TaskTitle: string;
    AssigneeUserId: string;
    TaskDate: string;
}
interface UserScheduleSearchModel {
    MonthShift: number;
    UserIds: Array<string>;
    ShowTasksWithNoAssignee: boolean;
}
declare class ScheduleConsts {
    static FilterPrefix: string;
}
declare class ScheduleStaticHandlers {
    static Filter: UserScheduleSearchModel;
    static _countOfChanges: number;
    static SetHandlers(): void;
    static GetQueryParams(isNextMonth: boolean): string;
    static ApplyFilter(isNextMonth: boolean): void;
    static OnUsersSelectChanged(): void;
    static ShowUserSchedule(): void;
    static ShowDayTaskModal(taskId: string): void;
    static ShowCreateTaskModal(): void;
    static updateComment(commentId: string): void;
    static addComment(): void;
    static redirectToFullVersion(): void;
    static updateDayTask(): void;
    static createDayTask(): void;
    static hideCreateModal(): void;
    static redirectToProfile(profileId: string): void;
}
