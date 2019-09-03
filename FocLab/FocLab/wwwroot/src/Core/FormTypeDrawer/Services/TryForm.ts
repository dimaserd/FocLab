class TryForm {

    static _genericInterfaces: Array<GenerateGenericUserInterfaceModel> = [];

    static UnWrapModel(model: GenerateGenericUserInterfaceModel, drawer: FormTypeDrawer) : string {

        let html = "";

        for (let i = 0; i < model.Blocks.length; i++) {
            let block = model.Blocks[i];

            switch (block.InterfaceType) {
                case UserInterfaceType.TextBox:
                    html += drawer.TextBoxFor(block.PropertyName);
                    break;
                case UserInterfaceType.TextArea:
                    html += drawer.TextAreaFor(block.PropertyName);
                    break;
                case UserInterfaceType.DropDownList:
                    html += drawer.DropDownFor(block.PropertyName, block.SelectList);
                    break;
                case UserInterfaceType.Hidden:
                    html += drawer.HiddenFor(block.PropertyName);
                    break;
                case UserInterfaceType.DatePicker:
                    html += drawer.DatePickerFor(block.PropertyName);
                    break;
                default:
                    console.log("Данный блок не реализован", block);
                    throw new Error("Не реализовано");
            }
        }

        return html;
    }

    static ThrowError(mes: string) {
        alert(mes);
        throw Error(mes);
    }

    static GetForms() : void {

        var elems = document.getElementsByClassName("generic-user-interface");

        for (let i = 0; i < elems.length; i++) {
            let elem = elems[i];

            TryForm.GetForm(elem);
        }
    }

    /**
     * Получить объект данных с первой попавшейся формы на странице
     */
    static GetDataForFormByModelPrefix(modelPrefix: string): object {
        var model = TryForm._genericInterfaces.find(x => x.Prefix == modelPrefix);

        if (model == null) {

        }

        return TryForm.GetDataForForm(model);
    }

    /**
     * Получить объект данных с первой попавшейся формы на странице
     */
    static GetDataForFirstForm(): object {
        if (TryForm._genericInterfaces.length == 0) {
            TryForm.ThrowError("На странице не объявлено ни одной формы");
        }

        var model = TryForm._genericInterfaces[0];

        return TryForm.GetDataForForm(model);
    }

    /**
     * Получить данные с формы приведенные к описанному типу данных
     * @param buildModel Тип данных
     */
    static GetDataForForm(buildModel: GenerateGenericUserInterfaceModel): object {
        let getter = new FormTypeDataGetter(buildModel.TypeDescription);

        return getter.GetData(buildModel.Prefix);
    }


    static AddBuildModel(buildModel: GenerateGenericUserInterfaceModel): void {
        let elem = TryForm._genericInterfaces.find(x => x.Prefix == buildModel.Prefix);

        if (elem != null) {
            TryForm.ThrowError(`На странице уже объявлена форма с префиксом ${buildModel.Prefix}`);
        }

        TryForm._genericInterfaces.push(buildModel);
    }

    static GetForm(elem: Element): void {
        var id = elem.getAttribute("data-id");

        var formDrawKey = elem.getAttribute("data-form-draw");

        var buildModel = window[id] as GenerateGenericUserInterfaceModel;

        TryForm.AddBuildModel(buildModel);

        var drawImpl = FormDrawFactory.GetImplementation(buildModel, formDrawKey);

        var drawer = new FormTypeDrawer(drawImpl, buildModel.TypeDescription);

        drawer.BeforeFormDrawing();

        elem.innerHTML = TryForm.UnWrapModel(buildModel, drawer);

        drawer.AfterFormDrawing();
    }
}

TryForm.GetForms();