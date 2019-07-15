interface ScheduleWorkerFilter {
    UserIds: Array<string>;
}
declare class ScheduleWorker {
    static filter: ScheduleWorkerFilter;
    static Users: Array<any>;
    static Constructor(filter: ScheduleWorkerFilter): void;
    static formatStateSelection(state: any): any;
    static formatStateResult(state: any): any;
    static SetUsersSelect(): void;
}
