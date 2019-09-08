declare class TasksFilter {
    static FilterPrefix: string;
    DefaultFilterState: object;
    Tasks: Array<ChemistryTaskModel>;
    static ShowAllString: string;
    SetTasks(tasks: any): void;
    ApplyFilter(): void;
    ClearFilter(): void;
    SearchAndSortTasks(tasks: Array<ChemistryTaskModel>): Array<ChemistryTaskModel>;
    ShowAndHideTasks(tasks: Array<ChemistryTaskModel>): void;
    DrawPerformed(tasks: Array<ChemistryTaskModel>): void;
    DrawNotPerformed(tasks: Array<ChemistryTaskModel>): void;
    SortTasks(tasks: Array<ChemistryTaskModel>): Array<ChemistryTaskModel>;
    SortByPerformedDate(tasks: Array<ChemistryTaskModel>): Array<ChemistryTaskModel>;
    SortByPerformerName(tasks: Array<ChemistryTaskModel>): Array<ChemistryTaskModel>;
}
