class ExperimentPageHandlers {

    static ExperimentId: string;

    static UpdateFileByType(fileType: string) : void {

        GenericUtil.GenericUpdateFileByType(fileType, '/Api/Chemistry/Experiments/ChangeFile', {
            ExperimentId: ExperimentPageHandlers.ExperimentId
        });
    }
}