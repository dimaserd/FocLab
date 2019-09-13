class FormTypeDataGetter {

    _typeDescription: CrocoTypeDescription;

    constructor(data: CrocoTypeDescription) {

        if (!data.IsClass) {
            const mes = "Тип не являющийся классом не поддерживается";
            alert(mes);
            throw Error(mes);
        }

        this._typeDescription = data;
    }

    private BuildObject() : object {
        const data = {};

        for (let i = 0; i < this._typeDescription.Properties.length; i++) {

            const prop = this._typeDescription.Properties[i];

            data[prop.PropertyName] = "";
        }

        return data;
    }

    public GetData(modelPrefix: string): object {

        const initData = FormDataHelper.CollectDataByPrefix(this.BuildObject(), modelPrefix);

        for (let i = 0; i < this._typeDescription.Properties.length; i++) {

            const prop = this._typeDescription.Properties[i];

            switch (prop.TypeName) {
                case CSharpType.Decimal.toString():
                    initData[prop.PropertyName] = Number((initData[prop.PropertyName] as string).replace(/,/g, '.'));
                    break;
                case CSharpType.Boolean.toString():
                    initData[prop.PropertyName] = (initData[prop.PropertyName] as string).toLowerCase() === "true";
                    break;
            }
        }

        return initData;
    }
}