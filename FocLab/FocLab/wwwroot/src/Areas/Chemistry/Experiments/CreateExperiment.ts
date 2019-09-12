interface 

class CreateExperiment {

    static _modelPrefix: string = "create.";

    static Create(): void {

        let data = TryForm.GetDataForFormByModelPrefix(CreateExperiment._modelPrefix);

        Requester.SendPostRequestWithAnimation('/Api/Chemistry/Experiments/Create', data, (x: BaseApiResponse) => {
            if (x.IsSucceeded) {
                setTimeout(() => { location.href = "/Chemistry/Experiments/Index" }, 1500);
            }
        }, null);
    }

    static SetHandlers(): void {
        EventSetter.SetHandlerForClass("btn-exp-create", "click", () => CreateExperimentHandlers.Create());
    }

    static AfterDrawHandler(modelPrefix: string): void {

        Requester.SendAjaxPost("/Api/User/Get", { Count: null, OffSet: 0 }, (resp: GetListResut<ApplicationUser>) => {

            let t = TryForm._genericInterfaces.find(x => x.Prefix == modelPrefix);

            let prop = t.TypeDescription.Properties.find(x => x.PropertyName == "Email")

            let drawer: FormDrawImplementation = new FormDrawImplementation(t);

            let selList: Array<SelectListItem> = resp.List.map(t => {
                return {
                    Value: t.Id,
                    Text: t.Email,
                    Selected: false
                }
            });

            let html = drawer.RenderDropDownList(prop, selList, false);

            FormTypeAfterDrawnDrawer.SetInnerHtmlForProperty(prop.PropertyName, modelPrefix, html);

            drawer.AfterFormDrawing();

        }, null, false)
    }
}

CreateExperiment.SetHandlers();