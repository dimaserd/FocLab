var CreateTaskScripts = (function () {
    function CreateTaskScripts() {
        CreateTaskScripts._NotSelectedValue = window["NotSelectedValue"];
    }
    CreateTaskScripts.prototype.Init = function () {
        $("#FileMethodsSelect").select2({
            placeholder: "Выберите метод решения",
            "language": {
                "noResults": function () {
                    return "Метод решения не найден.";
                }
            },
            escapeMarkup: function (markup) {
                return markup;
            }
        });
        $("#PerformerSelect").select2({
            placeholder: "Выберите исполнителя",
            "language": {
                "noResults": function () {
                    return "Исполнитель не найден.";
                }
            },
            escapeMarkup: function (markup) {
                return markup;
            }
        });
        DatePickerUtils.SetDatePicker("DeadLineDatePicker", "DeadLineDate");
        $(".form-action-btn").on("click", function () { return CreateTask(); });
        function CreateTask() {
            var data = {
                AdminId: "",
                FileMethodId: "",
                Title: "",
                DeadLineDate: new Date(),
                PerformerId: "",
                Quality: "",
                Quantity: ""
            };
            CrocoAppCore.Application.FormDataHelper.CollectDataByPrefix(data, "");
            console.log("Собраные данные", data);
            data.DeadLineDate = DatePickerUtils.GetDateFromDatePicker("DeadLineDate");
            if (data.FileMethodId === CreateTaskScripts._NotSelectedValue) {
                data.FileMethodId = null;
            }
            CrocoAppCore.Application.Requester.SendPostRequestWithAnimation("/Api/Chemistry/Tasks/Admin/Create", data, function (x) {
                if (x.IsSucceeded) {
                    setTimeout(function () { return location.href = "/Chemistry/Tasks/Index"; }, 1500);
                }
            }, null);
        }
    };
    return CreateTaskScripts;
}());
new CreateTaskScripts();
