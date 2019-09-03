var FormTypeDataGetter = /** @class */ (function () {
    function FormTypeDataGetter(data) {
        if (!data.IsClass) {
            var mes = "Тип не являющийся классом не поддерживается";
            alert(mes);
            throw Error(mes);
        }
        this._typeDescription = data;
    }
    FormTypeDataGetter.prototype.BuildObject = function () {
        var data = {};
        for (var i = 0; i < this._typeDescription.Properties.length; i++) {
            var prop = this._typeDescription.Properties[i];
            data[prop.PropertyName] = "";
        }
        return data;
    };
    FormTypeDataGetter.prototype.GetData = function (modelPrefix) {
        var initData = FormDataHelper.CollectDataByPrefix(this.BuildObject(), modelPrefix);
        for (var i = 0; i < this._typeDescription.Properties.length; i++) {
            var prop = this._typeDescription.Properties[i];
            switch (prop.TypeName) {
                case CSharpType.Decimal.toString():
                    initData[prop.PropertyName] = Number(initData[prop.PropertyName].replace(/,/g, '.'));
                    break;
                case CSharpType.Boolean.toString():
                    initData[prop.PropertyName] = initData[prop.PropertyName].toLowerCase() == "true";
                    break;
            }
        }
        return initData;
    };
    return FormTypeDataGetter;
}());
