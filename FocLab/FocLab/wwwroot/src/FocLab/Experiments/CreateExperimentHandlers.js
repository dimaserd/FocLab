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
