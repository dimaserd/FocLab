declare class IndexPageTaskDrawer {
    static GetNotFoundElement(colspan: number): HTMLTableRowElement;
    static GetTaskElementForPerformedTr(task: ChemistryTaskModel): HTMLTableRowElement;
    static GetTaskElementForNotPerformedTr(task: ChemistryTaskModel): HTMLTableRowElement;
}
