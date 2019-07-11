var ExperimentPageHandlers = /** @class */ (function () {
    function ExperimentPageHandlers() {
    }
    ExperimentPageHandlers.UpdateFileByType = function (fileType) {
        GenericUtil.GenericUpdateFileByType(fileType, '/Api/Chemistry/Experiments/ChangeFile', {
            ExperimentId: ExperimentPageHandlers.ExperimentId
        });
    };
    ExperimentPageHandlers.UpdateExperiment = function () {
        console.log("ExperimentPageHandlers.UpdateExperiment()");
        var toCollect = {
            Title: "",
            PerformerText: "",
        };
        toCollect = FormDataHelper.CollectDataByPrefix(toCollect, "Experiment.");
        var data = {
            Id: ExperimentPageHandlers.ExperimentId,
            Title: toCollect.Title,
            PerformerText: toCollect.PerformerText,
            SubstanceCounterJson: JSON.stringify(SubstanceStaticHandlers.substance.getJSON())
        };
        Requester.SendPostRequestWithAnimation("/Api/Chemistry/Experiments/Update", data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    };
    ExperimentPageHandlers.SetHandlers = function () {
        var btn = document.getElementById("exp-update-btn");
        EventSetter.SetHandlerForClass("exp-update-btn", "click", function () { return ExperimentPageHandlers.UpdateExperiment(); });
    };
    return ExperimentPageHandlers;
}());
ExperimentPageHandlers.SetHandlers();
