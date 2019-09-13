var CreateExperiment = (function () {
    function CreateExperiment() {
    }
    CreateExperiment.Create = function () {
        var data = TryForm.GetDataForFormByModelPrefix(CreateExperiment._modelPrefix);
        Requester.SendPostRequestWithAnimation('/Api/Chemistry/Experiments/Create', data, function (x) {
            if (x.IsSucceeded) {
                setTimeout(function () { location.href = "/Chemistry/Experiments/Index"; }, 1500);
            }
        }, null);
    };
    CreateExperiment.SetHandlers = function () {
        EventSetter.SetHandlerForClass("btn-exp-create", "click", function () { return CreateExperimentHandlers.Create(); });
        CreateExperiment.AfterDrawHandler();
    };
    CreateExperiment.AfterDrawHandler = function () {
        Requester.SendAjaxGet("/Api/Chemistry/Tasks/GetAll", null, function (resp) {
            var selList = resp.map(function (t) {
                return {
                    Value: t.Id,
                    Text: t.Title,
                    Selected: false
                };
            });
            var taskIdPropName = "TaskId";
            FormTypeAfterDrawnDrawer.SetSelectListForProperty(taskIdPropName, CreateExperiment._modelPrefix, selList);
            $(FormDrawHelper.GetPropertySelector(taskIdPropName, CreateExperiment._modelPrefix)).select2({
                placeholder: "Выберите задачу",
                language: {
                    "noResults": function () {
                        return "Задача не найдена.";
                    }
                },
                escapeMarkup: function (markup) {
                    return markup;
                }
            });
        }, null);
    };
    CreateExperiment._modelPrefix = "create.";
    return CreateExperiment;
}());
CreateExperiment.SetHandlers();
