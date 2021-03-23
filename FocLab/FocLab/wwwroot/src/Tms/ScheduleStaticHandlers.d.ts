interface ApplicationUserModel {
    Id: string;
    Email: string;
    AvatarFileId: number;
}
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
}
declare class ScheduleConsts {
    static FilterPrefix: string;
}
declare class ScheduleStaticHandlers {
    static Filter: UserScheduleSearchModel;
    static SetInnerHandlers(): void;
    static SetHandlers(): void;
    static OnUsersSelectChanged(): void;
    static RedirectToNewUserSchedule(monthShift: number): void;
    static ShowDayTaskModal(taskId: string): void;
    static ShowCreateTaskModal(): void;
    static InitUserSelect(selector: string): void;
    static redirectToFullVersion(): void;
    static updateDayTask(): void;
    static createDayTask(): void;
    static hideCreateModal(): void;
    static redirectToProfile(profileId: string): void;
}
