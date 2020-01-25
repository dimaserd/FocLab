interface UpdateExperiment {
    Id: string;

    Title: string;

    PerformerText: string;

    SubstanceCounterJson: string;
}

class ExperimentPageHandlers {

    static ExperimentId: string;

    static UpdateFileByType(fileType: string) : void {

        GenericUtil.GenericUpdateFileByType(fileType, '/Api/Chemistry/Experiments/ChangeFile', {
            ExperimentId: ExperimentPageHandlers.ExperimentId
        });
    }

    static UpdateExperiment(): void {

        console.log("ExperimentPageHandlers.UpdateExperiment()")

        var toCollect = {
            Title: "",
            PerformerText: "",
        };

        CrocoAppCore.Application.FormDataHelper.CollectDataByPrefix(toCollect, "Experiment.");

        var data: UpdateExperiment = {
            Id: ExperimentPageHandlers.ExperimentId,
            Title: toCollect.Title,
            PerformerText: toCollect.PerformerText,
            SubstanceCounterJson: JSON.stringify(SubstanceStaticHandlers.substance.getJSON())
        }

        CrocoAppCore.Application.Requester.SendPostRequestWithAnimation("/Api/Chemistry/Experiments/Update", data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    }

    static SetHandlers(): void {
        EventSetter.SetHandlerForClass("exp-update-btn", "click", () => ExperimentPageHandlers.UpdateExperiment());
    }
}

ExperimentPageHandlers.SetHandlers();