declare class TaskFilterMethods {
    static ApplyTextFilter(tasks: Array<ChemistryTaskModel>, q: string): Array<ChemistryTaskModel>;
    static ApplyStatusFilter(tasks: Array<ChemistryTaskModel>, isPerformed: boolean): Array<ChemistryTaskModel>;
    static ParseBoolean(s: string): boolean;
}
