﻿class CreateExperiment {

    static _modelPrefix = "create.";

    static Create(): void {

        const data = TryForm.GetDataForFormByModelPrefix(CreateExperiment._modelPrefix);

        Requester.SendPostRequestWithAnimation('/Api/Chemistry/Experiments/Create', data, (x: BaseApiResponse) => {
            if (x.IsSucceeded) {
                setTimeout(() => { location.href = "/Chemistry/Experiments/Index" }, 1500);
            }
        }, null);
    }

    static SetHandlers(): void {
        EventSetter.SetHandlerForClass("btn-exp-create", "click", () => CreateExperimentHandlers.Create());
        CreateExperiment.AfterDrawHandler();
    }

    static AfterDrawHandler(): void {

        Requester.SendAjaxGet("/Api/Chemistry/Tasks/GetAll", null, (resp: Array<{Id: string, Title: string}>) => {

            let idOfExp = window.location.href.split("/").reverse()[0];

            idOfExp = idOfExp === "Create" ? null : idOfExp;

            const selList: Array<SelectListItem> = resp.map(t => {
                return {
                    Value: t.Id,
                    Text: t.Title,
                    Selected: t.Id === idOfExp
                }
            });

            const taskIdPropName = "TaskId";

            FormTypeAfterDrawnDrawer.SetSelectListForProperty(taskIdPropName, CreateExperiment._modelPrefix, selList);

            $(FormDrawHelper.GetPropertySelector(taskIdPropName, CreateExperiment._modelPrefix)).select2({
                placeholder: "Выберите задачу",

                language: {
                    "noResults"() {
                        return "Задача не найдена.";
                    }
                },
                escapeMarkup(markup) {
                    return markup;
                }
            });

        }, null);
    }
}

CreateExperiment.SetHandlers();