var CrocoTypeDescriptionOverrider = (function () {
    function CrocoTypeDescriptionOverrider() {
    }
    CrocoTypeDescriptionOverrider.FindUserInterfacePropBlockByPropertyName = function (model, propertyName) {
        var prop = model.Blocks.find(function (x) { return x.PropertyName === propertyName; });
        if (prop == null) {
            alert("\u0421\u0432\u043E\u0439\u0441\u0442\u0432\u043E '" + propertyName + "' \u043D\u0435 \u043D\u0430\u0439\u0434\u0435\u043D\u043E \u0432 \u043C\u043E\u0434\u0435\u043B\u0438 \u043E\u0431\u043E\u0431\u0449\u0435\u043D\u043D\u043E\u0433\u043E \u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u043B\u0435\u044C\u0441\u043A\u043E\u0433\u043E \u0438\u043D\u0442\u0435\u0440\u0444\u0435\u0439\u0441\u0430");
            return;
        }
        return prop;
    };
    CrocoTypeDescriptionOverrider.SetUserInterfaceTypeForProperty = function (model, propertyName, interfaceType) {
        var prop = CrocoTypeDescriptionOverrider.FindUserInterfacePropBlockByPropertyName(model, propertyName);
        prop.InterfaceType = interfaceType;
    };
    CrocoTypeDescriptionOverrider.RemoveProperty = function (model, propertyName) {
        model.Blocks = model.Blocks.filter(function (x) { return x.PropertyName !== propertyName; });
    };
    CrocoTypeDescriptionOverrider.SetLabelText = function (model, propertyName, labelText) {
        CrocoTypeDescriptionOverrider.FindUserInterfacePropBlockByPropertyName(model, propertyName).LabelText = labelText;
    };
    CrocoTypeDescriptionOverrider.SetHidden = function (model, propertyName) {
        CrocoTypeDescriptionOverrider.SetUserInterfaceTypeForProperty(model, propertyName, UserInterfaceType.Hidden);
    };
    CrocoTypeDescriptionOverrider.SetTextArea = function (model, propertyName) {
        CrocoTypeDescriptionOverrider.SetUserInterfaceTypeForProperty(model, propertyName, UserInterfaceType.TextArea);
    };
    CrocoTypeDescriptionOverrider.SetDropDownList = function (model, propertyName, selectList) {
        var prop = CrocoTypeDescriptionOverrider.FindUserInterfacePropBlockByPropertyName(model, propertyName);
        prop.InterfaceType = UserInterfaceType.DropDownList;
        prop.SelectList = selectList;
    };
    return CrocoTypeDescriptionOverrider;
}());
