var CSharpType;
(function (CSharpType) {
    CSharpType[CSharpType["String"] = "String"] = "String";
    CSharpType[CSharpType["Int"] = "Int"] = "Int";
    CSharpType[CSharpType["Decimal"] = "Decimal"] = "Decimal";
    CSharpType[CSharpType["Boolean"] = "Boolean"] = "Boolean";
    CSharpType[CSharpType["DateTime"] = "DateTime"] = "DateTime";
})(CSharpType || (CSharpType = {}));

var CookieWorker = (function () {
    function CookieWorker() {
    }
    CookieWorker.prototype.setCookie = function (name, value, days) {
        var expires = "";
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toUTCString();
        }
        document.cookie = name + "=" + (value || "") + expires + "; path=/";
    };
    CookieWorker.prototype.getCookie = function (name) {
        var nameEq = name + "=";
        var ca = document.cookie.split(";");
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) === " ")
                c = c.substring(1, c.length);
            if (c.indexOf(nameEq) === 0)
                return c.substring(nameEq.length, c.length);
        }
        return null;
    };
    CookieWorker.prototype.eraseCookie = function (name) {
        document.cookie = name + "=; Max-Age=-99999999;";
    };
    return CookieWorker;
}());

var CrocoJsApplication = (function () {
    function CrocoJsApplication() {
    }
    return CrocoJsApplication;
}());

var FormDataHelper = (function () {
    function FormDataHelper() {
        this.NullValue = "VALUE_NULL";
        this.DataTypeAttributeName = "data-type";
    }
    FormDataHelper.prototype.FillDataByPrefix = function (object, prefix) {
        for (var index in object) {
            var valueOfProp = object[index];
            if (valueOfProp === null || valueOfProp === undefined) {
                continue;
            }
            var name_1 = prefix + index;
            var element = document.getElementsByName(name_1)[0];
            if (element === null || element === undefined) {
                continue;
            }
            if (Array.isArray(valueOfProp)) {
                if (element.type !== "select-multiple") {
                    alert("An attempt to set an array to HTMLInputElement which is not a select with multiple attribute");
                }
                var select = element;
                var _loop_1 = function (i) {
                    var opt = select.options[i];
                    var value = valueOfProp.filter(function (x) { return opt.value === x; }).length > 0;
                    opt.selected = value;
                };
                for (var i = 0; i < select.options.length; i++) {
                    _loop_1(i);
                }
                var event_1 = new Event("change");
                element.dispatchEvent(event_1);
                continue;
            }
            if (element.type === "checkbox") {
                element.checked = valueOfProp;
            }
            else if (element.type === "radio") {
                document.querySelector("input[name=" + name_1 + "][value=" + valueOfProp + "]").checked = true;
            }
            else {
                element.value = valueOfProp;
            }
            var event_2 = new Event("change");
            element.dispatchEvent(event_2);
        }
    };
    FormDataHelper.prototype.CollectDataByPrefix = function (object, prefix) {
        for (var index in object) {
            if (object.hasOwnProperty(index)) {
                var name_2 = prefix + index;
                var element = document.getElementsByName(name_2)[0];
                if (element == null) {
                    alert("\u042D\u043B\u0435\u043C\u0435\u043D\u0442 \u043D\u0435 \u043D\u0430\u0439\u0434\u0435\u043D \u043F\u043E \u0443\u043A\u0430\u0437\u0430\u043D\u043D\u043E\u043C\u0443 \u0438\u043C\u0435\u043D\u0438 " + name_2);
                    return;
                }
                var rawValue = this.GetRawValueFromElement(element);
                object[index] = this.ValueMapper(rawValue, element.getAttribute(this.DataTypeAttributeName));
            }
        }
    };
    FormDataHelper.prototype.GetRawValueFromElement = function (htmlElement) {
        if (htmlElement.type === "select-multiple") {
            return Array.apply(null, htmlElement.options)
                .filter(function (option) { return option.selected; })
                .map(function (option) { return option.value; });
        }
        if (htmlElement.type === "radio") {
            var flag = document.querySelector("input[name=\"" + name + "\"]:checked") != null;
            if (flag) {
                var elem = document.querySelector("input[name=\"" + name + "\"]:checked");
                return elem.value;
            }
            return null;
        }
        return htmlElement.type === "checkbox" ? htmlElement.checked : htmlElement.value;
    };
    FormDataHelper.prototype.CollectDataByPrefixWithTypeMatching = function (modelPrefix, typeDescription) {
        this.CheckData(typeDescription);
        var initData = this.BuildObject(typeDescription);
        this.CollectDataByPrefix(initData, modelPrefix);
        for (var i = 0; i < typeDescription.Properties.length; i++) {
            var prop = typeDescription.Properties[i];
            var initValue = this.GetInitValue(initData[prop.PropertyDescription.PropertyName]);
            initData[prop.PropertyDescription.PropertyName] = this.ValueMapper(initValue, prop.TypeName);
        }
        return initData;
    };
    FormDataHelper.prototype.ValueMapper = function (rawValue, dataType) {
        if (rawValue === this.NullValue) {
            return null;
        }
        switch (dataType) {
            case CSharpType.DateTime.toString():
                return new Date(rawValue);
            case CSharpType.Decimal.toString():
                return (rawValue !== null) ? Number((rawValue).replace(/,/g, '.')) : null;
            case CSharpType.Boolean.toString():
                return (rawValue !== null) ? rawValue.toLowerCase() === "true" : null;
        }
        return rawValue;
    };
    FormDataHelper.prototype.GetInitValue = function (propValue) {
        var strValue = propValue;
        return strValue === this.NullValue ? null : strValue;
    };
    FormDataHelper.prototype.CheckData = function (typeDescription) {
        if (!typeDescription.IsClass) {
            var mes = "Тип не являющийся классом не поддерживается";
            alert(mes);
            throw Error(mes);
        }
    };
    FormDataHelper.prototype.BuildObject = function (typeDescription) {
        var data = {};
        for (var i = 0; i < typeDescription.Properties.length; i++) {
            var prop = typeDescription.Properties[i];
            data[prop.PropertyDescription.PropertyName] = "";
        }
        return data;
    };
    return FormDataHelper;
}());






var FormDataUtils = (function () {
    function FormDataUtils() {
    }
    FormDataUtils.prototype.GetStartUrlNoParams = function (startUrl) {
        if (startUrl === void 0) { startUrl = null; }
        startUrl = startUrl == null ? window.location.href : startUrl;
        if (!startUrl.includes('?')) {
            return startUrl;
        }
        return startUrl.split('?')[0];
    };
    FormDataUtils.prototype.GetUrlParamsObject = function (startUrl) {
        if (startUrl === void 0) { startUrl = null; }
        startUrl = startUrl == null ? window.location.href : startUrl;
        var url = unescape(startUrl);
        var obj = {};
        if (!url.includes('?')) {
            return obj;
        }
        var paramsUrl = url.split('?')[1].split('&');
        for (var i = 0; i < paramsUrl.length; i++) {
            var para = paramsUrl[i];
            if (!para.includes('=')) {
                continue;
            }
            var bits = paramsUrl[i].split('=');
            obj[bits[0]] = bits[1];
        }
        return obj;
    };
    FormDataUtils.prototype.ProccessStringPropertiesAsDateTime = function (obj, propNames) {
        var _this = this;
        if (Array.isArray(obj)) {
            return obj.map(function (x) { return _this.ProccessStringPropertiesAsDateTime(x, propNames); });
        }
        for (var i in obj) {
            var oldValue = obj[i];
            if (Array.isArray(oldValue)) {
                obj[i] = oldValue.map(function (x) { return _this.ProccessStringPropertiesAsDateTime(x, propNames); });
                continue;
            }
            if (oldValue instanceof Object && oldValue.constructor === Object) {
                obj[i] = this.ProccessStringPropertiesAsDateTime(oldValue, propNames);
                continue;
            }
            if (propNames.findIndex(function (t) { return t === i; }) > -1 && obj[i] != null) {
                obj[i] = new Date(oldValue);
            }
        }
        return obj;
    };
    FormDataUtils.prototype.ProccessAllDateTimePropertiesAsString = function (obj) {
        for (var i in obj) {
            if (obj[i] instanceof Object && obj[i].constructor === Object) {
                obj[i] = this.ProccessAllDateTimePropertiesAsString(obj[i]);
                continue;
            }
            if (Object.prototype.toString.call(obj[i]) === '[object Date]') {
                obj[i] = obj[i].toISOString();
            }
        }
        return obj;
    };
    FormDataUtils.prototype.ProccessAllNumberPropertiesAsString = function (obj) {
        for (var i in obj) {
            if (obj[i] instanceof Object && obj[i].constructor === Object) {
                obj[i] = this.ProccessAllNumberPropertiesAsString(obj[i]);
                continue;
            }
            if (typeof obj[i] == 'number') {
                obj[i] = obj[i].toString().replace('.', ',');
            }
        }
        return obj;
    };
    return FormDataUtils;
}());

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