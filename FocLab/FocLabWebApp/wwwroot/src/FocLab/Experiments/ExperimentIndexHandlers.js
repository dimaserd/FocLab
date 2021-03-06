﻿var ExperimentIndexHandlers = (function () {
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
