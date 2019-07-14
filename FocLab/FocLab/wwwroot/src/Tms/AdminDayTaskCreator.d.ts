interface AdminDayTaskCreatorProps {
    AssigneeUserId: string;
}
declare class AdminDayTaskCreator {
    AssigneeUserId: string;
    TaskDate: string;
    constructor(props: AdminDayTaskCreatorProps);
    SetDate(date: string): void;
    ProccessData(data: any): any;
    CreateDayTask(data: any): void;
    EditDayTask(data: any): void;
}
