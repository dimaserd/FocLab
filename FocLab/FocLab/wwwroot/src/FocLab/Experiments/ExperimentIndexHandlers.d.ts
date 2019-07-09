declare class ExperimentIndexHandlers {
    static PerformExperiment(id: string, flag: boolean): void;
    static RemoveExperiment(id: string): void;
    static CancelRemoveExperiment(id: string): void;
    static SetHandlers(): void;
}
