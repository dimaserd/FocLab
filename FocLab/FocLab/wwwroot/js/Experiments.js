var CreateExperimentHandlers = /** @class */ (function () {
    function CreateExperimentHandlers() {
    }
    CreateExperimentHandlers.Create = function () {
        var data = {
            TaskId: "",
            Title: ""
        };
        Requester.SendPostRequestWithAnimation('/Api/Chemistry/Experiments/Create', FormDataHelper.CollectData(data), function (x) {
            if (x.IsSucceeded) {
                setTimeout(function () { location.href = "/Chemistry/Experiments/Index"; }, 1500);
            }
        }, null);
    };
    CreateExperimentHandlers.SetHandlers = function () {
        EventSetter.SetHandlerForClass("btn-exp-create", "click", function () { return CreateExperimentHandlers.Create(); });
    };
    return CreateExperimentHandlers;
}());
CreateExperimentHandlers.SetHandlers();

var ExperimentIndexHandlers = /** @class */ (function () {
    function ExperimentIndexHandlers() {
    }
    ExperimentIndexHandlers.PerformExperiment = function (id, flag) {
        var data = {
            ExperimentId: id,
            Performed: flag
        };
        Requester.SendPostRequestWithAnimation('/Api/Chemistry/Experiments/Perform', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    };
    ExperimentIndexHandlers.RemoveExperiment = function (id) {
        Requester.SendPostRequestWithAnimation("/Api/Chemistry/Experiments/Remove?id=" + id, {}, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    };
    ExperimentIndexHandlers.CancelRemoveExperiment = function (id) {
        Requester.SendPostRequestWithAnimation("/Api/Chemistry/Experiments/CancelRemove?id=" + id, {}, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
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

var ExperimentPageHandlers = /** @class */ (function () {
    function ExperimentPageHandlers() {
    }
    ExperimentPageHandlers.UpdateFileByType = function (fileType) {
        GenericUtil.GenericUpdateFileByType(fileType, '/Api/Chemistry/Experiments/ChangeFile', {
            ExperimentId: ExperimentPageHandlers.ExperimentId
        });
    };
    return ExperimentPageHandlers;
}());