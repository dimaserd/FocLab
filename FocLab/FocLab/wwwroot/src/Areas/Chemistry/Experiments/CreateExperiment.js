var CreateExperiment = (function () {
    function CreateExperiment() {
    }
    CreateExperiment.Create = function () {
        console.log("CreateExperiment.Click");
        var data = CrocoAppCore.GenericInterfaceHelper
            .FormHelper._genericInterfaces
            .find(function (x) { return x.FormModel.Prefix === CreateExperiment._modelPrefix; });
        CrocoAppCore.Application.Requester.SendPostRequestWithAnimation('/Api/Chemistry/Experiments/Create', data, function (x) {
            if (x.IsSucceeded) {
                setTimeout(function () { location.href = "/Chemistry/Experiments/Index"; }, 1500);
            }
        }, null);
    };
    CreateExperiment.SetHandlers = function () {
        EventSetter.SetHandlerForClass("btn-exp-create", "click", function () { return CreateExperiment.Create(); });
        CreateExperiment.AfterDrawHandler();
    };
    CreateExperiment.AfterDrawHandler = function () {
        CrocoAppCore.Application.Requester.Get("/Api/Chemistry/Tasks/GetAll", null, function (resp) {
            var idOfExp = window.location.href.split("/").reverse()[0];
            idOfExp = idOfExp === "Create" ? null : idOfExp;
            var selList = resp.map(function (t) {
                return {
                    Value: t.Id,
                    Text: t.Title,
                    Selected: t.Id === idOfExp
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
