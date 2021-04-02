var ExperimentIndexHandlers = (function () {
    function ExperimentIndexHandlers() {
    }
    ExperimentIndexHandlers.PerformExperiment = function (id, flag) {
        var data = {
            ExperimentId: id,
            Performed: flag
        };
        CrocoAppCore.Application.Requester.SendPostRequestWithAnimation('/Api/Chemistry/Experiments/Perform', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    };
    ExperimentIndexHandlers.RemoveExperiment = function (id) {
        CrocoAppCore.Application.Requester.SendPostRequestWithAnimation("/Api/Chemistry/Experiments/Remove?id=" + id, {}, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    };
    ExperimentIndexHandlers.CancelRemoveExperiment = function (id) {
        CrocoAppCore.Application.Requester.SendPostRequestWithAnimation("/Api/Chemistry/Experiments/CancelRemove?id=" + id, {}, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    };
    ExperimentIndexHandlers.SetHandlers = function () {
        EventSetter.SetHandlerForClass("exp-perform-btn", "click", function (x) {
            var id = $(x.target).data("id").toString();
            ExperimentIndexHandlers.PerformExperiment(id, true);
        });
        EventSetter.SetHandlerForClass("exp-cancel-remove-btn", "click", function (x) {
            var id = $(x.target).data("id").toString();
            ExperimentIndexHandlers.CancelRemoveExperiment(id);
        });
        EventSetter.SetHandlerForClass("exp-remove-btn", "click", function (x) {
            var id = $(x.target).data("id").toString();
            ExperimentIndexHandlers.RemoveExperiment(id);
        });
        EventSetter.SetHandlerForClass("exp-cancel-perform-btn", "click", function (x) {
            var id = $(x.target).data("id").toString();
            ExperimentIndexHandlers.PerformExperiment(id, false);
        });
    };
    return ExperimentIndexHandlers;
}());
ExperimentIndexHandlers.SetHandlers();

var ExperimentPageHandlers = (function () {
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
        CrocoAppCore.Application.FormDataHelper.CollectDataByPrefix(toCollect, "Experiment.");
        var data = {
            Id: ExperimentPageHandlers.ExperimentId,
            Title: toCollect.Title,
            PerformerText: toCollect.PerformerText,
            SubstanceCounterJson: JSON.stringify(SubstanceStaticHandlers.substance.getJSON())
        };
        CrocoAppCore.Application.Requester.SendPostRequestWithAnimation("/Api/Chemistry/Experiments/Update", data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    };
    ExperimentPageHandlers.SetHandlers = function () {
        EventSetter.SetHandlerForClass("exp-update-btn", "click", function () { return ExperimentPageHandlers.UpdateExperiment(); });
    };
    return ExperimentPageHandlers;
}());
ExperimentPageHandlers.SetHandlers();