interface ScheduleWorkerFilter {
    UserIds: Array<string>;
}
declare class ScheduleWorker_Resx {
    SelectUser: string;
    UserNotFound: string;
}
declare class ScheduleWorker {
    static filter: ScheduleWorkerFilter;
    static Users: Array<any>;
    static Resources: ScheduleWorker_Resx;
    static Constructor(filter: ScheduleWorkerFilter): void;
    static formatStateSelection(state: any): any;
    static formatStateResult(state: any): any;
    static SetUsersSelect(): void;
}
