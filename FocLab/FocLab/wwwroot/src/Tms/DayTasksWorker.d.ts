interface DayTasksWorkerProps {
    Tasks: Array<DayTaskModel>;
    IsAdmin: boolean;
    User: any;
    SearchModel: any;
    OpenTaskId: string;
    MyUserId: string;
}
declare class DayTasksWorker {
    static Tasks: DayTaskModel[];
    static IsAdmin: boolean;
    static User: any;
    static SearchModel: any;
    static OpenTaskId: string;
    static MyUserId: string;
    static CurrentTaskId: string;
    static CurrentTask: DayTaskModel;
    static Drawer: DayTaskDrawer;
    static Constructor(props: DayTasksWorkerProps): void;
    static OpenTaskById(): void;
    static SetCurrentTaskId(taskId: string): void;
    static SendNotificationToAdmin(): void;
    static GetTasks(): void;
    static GetTaskById(taskId: string): DayTaskModel;
}
