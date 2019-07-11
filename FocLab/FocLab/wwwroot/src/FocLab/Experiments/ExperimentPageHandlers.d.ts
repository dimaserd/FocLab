interface UpdateExperiment {
    Id: string;
    Title: string;
    PerformerText: string;
    SubstanceCounterJson: string;
}
declare class ExperimentPageHandlers {
    static ExperimentId: string;
    static UpdateFileByType(fileType: string): void;
    static UpdateExperiment(): void;
    static SetHandlers(): void;
}
