declare var moment: Function;
declare class DayTaskDrawer {
    DrawTasks(tasks: Array<DayTaskModel>): void;
    AddTaskToDate(task: DayTaskModel): void;
    AddAdminActions(): void;
    ClearTasks(): void;
}
