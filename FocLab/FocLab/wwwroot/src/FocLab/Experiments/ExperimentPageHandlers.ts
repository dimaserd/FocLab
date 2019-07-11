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

        toCollect = FormDataHelper.CollectDataByPrefix(toCollect, "Experiment.") as {
            Title: string;
            PerformerText: string;
        };

        var data: UpdateExperiment = {
            Id: ExperimentPageHandlers.ExperimentId,
            Title: toCollect.Title,
            PerformerText: toCollect.PerformerText,
            SubstanceCounterJson: JSON.stringify(SubstanceStaticHandlers.substance.getJSON())
        }

        Requester.SendPostRequestWithAnimation("/Api/Chemistry/Experiments/Update", data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    }

    static SetHandlers(): void {
        var btn = document.getElementById("exp-update-btn");
        EventSetter.SetHandlerForClass("exp-update-btn", "click", () => ExperimentPageHandlers.UpdateExperiment());
    }
}

ExperimentPageHandlers.SetHandlers();