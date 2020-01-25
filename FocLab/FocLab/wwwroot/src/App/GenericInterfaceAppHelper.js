var GenericInterfaceAppHelper = (function () {
    function GenericInterfaceAppHelper() {
        this.FormHelper = new GenericForm({ FormDrawFactory: CrocoAppCore.GetFormDrawFactory() });
    }
    GenericInterfaceAppHelper.prototype.GetUserInterfaceModel = function (typeName, modelPrefix, callBack) {
        var data = { typeName: typeName, modelPrefix: modelPrefix };
        CrocoAppCore.Application.Requester.Post("/Api/Documentation/GenericInterface", data, function (x) {
            if (x == null) {
                alert("\u041E\u0431\u043E\u0431\u0449\u0435\u043D\u043D\u044B\u0439 \u0438\u043D\u0442\u0435\u0440\u0444\u0435\u0439\u0441 \u0441 \u043D\u0430\u0437\u0432\u0430\u043D\u0438\u0435\u043C '" + typeName + "' \u043D\u0435 \u043F\u0440\u0438\u0448\u0451\u043B \u0441 \u0441\u0435\u0440\u0432\u0435\u0440\u0430");
                return;
            }
            callBack(x);
        }, function () {
            var mes = "\u041F\u0440\u043E\u0438\u0437\u043E\u0448\u043B\u0430 \u043E\u0448\u0438\u0431\u043A\u0430 \u043F\u0440\u0438 \u043F\u043E\u0438\u0441\u043A\u0435 \u043E\u0431\u043E\u0431\u0449\u0435\u043D\u043D\u043E\u0433\u043E \u0438\u043D\u0442\u0435\u0440\u0444\u0435\u0439\u0441\u0430 \u043F\u043E \u0442\u0438\u043F\u0443 " + typeName;
            alert(mes);
            CrocoAppCore.Application.Logger.LogAction(mes, "", "GenericInterfaceAppHelper.GetUserInterfaceModel.ErrorOnRequest", JSON.stringify(data));
        });
    };
    GenericInterfaceAppHelper.prototype.GetEnumModel = function (enumTypeName, callBack) {
        var data = { typeName: enumTypeName };
        CrocoAppCore.Application.Requester.Post("/Api/Documentation/EnumType", data, function (x) {
            if (x == null) {
                alert("\u041F\u0435\u0440\u0435\u0447\u0438\u0441\u043B\u0435\u043D\u0438\u0435 \u0441 \u043D\u0430\u0437\u0432\u0430\u043D\u0438\u0435\u043C '" + enumTypeName + "' \u043D\u0435 \u043F\u0440\u0438\u0448\u043B\u043E \u0441 \u0441\u0435\u0440\u0432\u0435\u0440\u0430");
                return;
            }
            callBack(x);
        }, function () {
            var mes = "\u041F\u0440\u043E\u0438\u0437\u043E\u0448\u043B\u0430 \u043E\u0448\u0438\u0431\u043A\u0430 \u043F\u0440\u0438 \u043F\u043E\u0438\u0441\u043A\u0435 \u043F\u0435\u0440\u0435\u0447\u0435\u0438\u0441\u043B\u0435\u043D\u0438\u044F \u0441 \u043D\u0430\u0437\u0432\u0430\u043D\u0438\u0435\u043C " + enumTypeName;
            alert(mes);
            CrocoAppCore.Application.Logger.LogAction(mes, "", "GenericInterfaceAppHelper.GetEnumModel.ErrorOnRequest", JSON.stringify(data));
        });
    };
    return GenericInterfaceAppHelper;
}());
