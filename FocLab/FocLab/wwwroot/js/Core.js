var Dictionary = /** @class */ (function () {
    function Dictionary(init) {
        this._keys = [];
        this._values = [];
        if (init) {
            for (var x = 0; x < init.length; x++) {
                this[init[x].key] = init[x].value;
                this._keys.push(init[x].key);
                this._values.push(init[x].value);
            }
        }
    }
    Dictionary.prototype.getByKey = function (key) {
        var index = this._keys.indexOf(key, 0);
        if (index > 0) {
            return this._values[index];
        }
        return null;
    };
    Dictionary.prototype.add = function (key, value) {
        if (this.containsKey(key)) {
            throw new DOMException("\u041A\u043B\u044E\u0447 " + key + " \u0443\u0436\u0435 \u0441\u0443\u0449\u0435\u0441\u0442\u0432\u0443\u0435\u0442 \u0432 \u0434\u0430\u043D\u043D\u043E\u043C \u0441\u043B\u043E\u0432\u0430\u0440\u0435");
        }
        this[key] = value;
        this._keys.push(key);
        this._values.push(value);
    };
    Dictionary.prototype.remove = function (key) {
        var index = this._keys.indexOf(key, 0);
        this._keys.splice(index, 1);
        this._values.splice(index, 1);
        delete this[key];
    };
    Dictionary.prototype.keys = function () {
        return this._keys;
    };
    Dictionary.prototype.values = function () {
        return this._values;
    };
    Dictionary.prototype.containsKey = function (key) {
        if (typeof this[key] === "undefined") {
            return false;
        }
        return true;
    };
    Dictionary.prototype.toLookup = function () {
        return this;
    };
    return Dictionary;
}());

var CrocoJsApplication = (function () {
    function CrocoJsApplication() {
    }
    return CrocoJsApplication;
}());

var CookieWorker = /** @class */ (function () {
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

var FormDataHelper = /** @class */ (function () {
    function FormDataHelper() {
        /*
        * Константа для обозначения null значений и вычленения их из строки
        * */
        this.NullValue = "VALUE_NULL";
        /*
        * Константа для имени аттрибута содержащего тип данных
        * */
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
            //Выбрасываю событие об изменении значения
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
        //Чекбоксы нужно проверять отдельно потому что у них свойство не value а почему-то checked
        return htmlElement.type === "checkbox" ? htmlElement.checked : htmlElement.value;
    };
    /**
     * Собрать данные с сопоставлением типов
     * @param modelPrefix префикс модели
     * @param typeDescription описание типа
     */
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

var CrocoJsApplication = /** @class */ (function () {
    function CrocoJsApplication() {
    }
    return CrocoJsApplication;
}());






var FormDataUtils = /** @class */ (function () {
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
    /*
     * Получить объект, который будет содержать все поля
     * */
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

var CSharpType;
(function (CSharpType) {
    CSharpType[CSharpType["String"] = "String"] = "String";
    CSharpType[CSharpType["Int"] = "Int"] = "Int";
    CSharpType[CSharpType["Decimal"] = "Decimal"] = "Decimal";
    CSharpType[CSharpType["Boolean"] = "Boolean"] = "Boolean";
    CSharpType[CSharpType["DateTime"] = "DateTime"] = "DateTime";
})(CSharpType || (CSharpType = {}));
var CookieWorker=function(){function n(){}return n.prototype.setCookie=function(n,t,i){var u="",r;i&&(r=new Date,r.setTime(r.getTime()+i*864e5),u="; expires="+r.toUTCString());document.cookie=n+"="+(t||"")+u+"; path=/"},n.prototype.getCookie=function(n){for(var t,r=n+"=",u=document.cookie.split(";"),i=0;i<u.length;i++){for(t=u[i];t.charAt(0)===" ";)t=t.substring(1,t.length);if(t.indexOf(r)===0)return t.substring(r.length,t.length)}return null},n.prototype.eraseCookie=function(n){document.cookie=n+"=; Max-Age=-99999999;"},n}(),FormDataHelper=function(){function n(){this.NullValue="VALUE_NULL";this.DataTypeAttributeName="data-type"}return n.prototype.FillDataByPrefix=function(n,t){var f,r,e,i,o,s,u,h,c;for(f in n)if((r=n[f],r!==null&&r!==undefined)&&(e=t+f,i=document.getElementsByName(e)[0],i!==null&&i!==undefined)){if(Array.isArray(r)){for(i.type!=="select-multiple"&&alert("An attempt to set an array to HTMLInputElement which is not a select with multiple attribute"),o=i,s=function(n){var t=o.options[n],i=r.filter(function(n){return t.value===n}).length>0;t.selected=i},u=0;u<o.options.length;u++)s(u);h=new Event("change");i.dispatchEvent(h);continue}i.type==="checkbox"?i.checked=r:i.type==="radio"?document.querySelector("input[name="+e+"][value="+r+"]").checked=!0:i.value=r;c=new Event("change");i.dispatchEvent(c)}},n.prototype.CollectDataByPrefix=function(n,t){var i,u,r,f;for(i in n)if(n.hasOwnProperty(i)){if(u=t+i,r=document.getElementsByName(u)[0],r==null){alert("Элемент не найден по указанному имени "+u);return}f=this.GetRawValueFromElement(r);n[i]=this.ValueMapper(f,r.getAttribute(this.DataTypeAttributeName))}},n.prototype.GetRawValueFromElement=function(n){var t,i;return n.type==="select-multiple"?Array.apply(null,n.options).filter(function(n){return n.selected}).map(function(n){return n.value}):n.type==="radio"?(t=document.querySelector('input[name="'+name+'"]:checked')!=null,t)?(i=document.querySelector('input[name="'+name+'"]:checked'),i.value):null:n.type==="checkbox"?n.checked:n.value},n.prototype.CollectDataByPrefixWithTypeMatching=function(n,t){var i,r,u,f;for(this.CheckData(t),i=this.BuildObject(t),this.CollectDataByPrefix(i,n),r=0;r<t.Properties.length;r++)u=t.Properties[r],f=this.GetInitValue(i[u.PropertyDescription.PropertyName]),i[u.PropertyDescription.PropertyName]=this.ValueMapper(f,u.TypeName);return i},n.prototype.ValueMapper=function(n,t){if(n===this.NullValue)return null;switch(t){case CSharpType.DateTime.toString():return new Date(n);case CSharpType.Decimal.toString():return n!==null?Number(n.replace(/,/g,".")):null;case CSharpType.Boolean.toString():return n!==null?n.toLowerCase()==="true":null}return n},n.prototype.GetInitValue=function(n){var t=n;return t===this.NullValue?null:t},n.prototype.CheckData=function(n){if(!n.IsClass){var t="Тип не являющийся классом не поддерживается";alert(t);throw Error(t);}},n.prototype.BuildObject=function(n){for(var r,i={},t=0;t<n.Properties.length;t++)r=n.Properties[t],i[r.PropertyDescription.PropertyName]="";return i},n}(),CrocoJsApplication=function(){function n(){}return n}(),FormDataUtils=function(){function n(){}return n.prototype.GetStartUrlNoParams=function(n){return(n===void 0&&(n=null),n=n==null?window.location.href:n,!n.includes("?"))?n:n.split("?")[0]},n.prototype.GetUrlParamsObject=function(n){var u,i,r,t,e,f;if(n===void 0&&(n=null),n=n==null?window.location.href:n,u=unescape(n),i={},!u.includes("?"))return i;for(r=u.split("?")[1].split("&"),t=0;t<r.length;t++)(e=r[t],e.includes("="))&&(f=r[t].split("="),i[f[0]]=f[1]);return i},n.prototype.ProccessStringPropertiesAsDateTime=function(n,t){var u=this,i,r;if(Array.isArray(n))return n.map(function(n){return u.ProccessStringPropertiesAsDateTime(n,t)});for(i in n){if(r=n[i],Array.isArray(r)){n[i]=r.map(function(n){return u.ProccessStringPropertiesAsDateTime(n,t)});continue}if(r instanceof Object&&r.constructor===Object){n[i]=this.ProccessStringPropertiesAsDateTime(r,t);continue}t.findIndex(function(n){return n===i})>-1&&n[i]!=null&&(n[i]=new Date(r))}return n},n.prototype.ProccessAllDateTimePropertiesAsString=function(n){for(var t in n){if(n[t]instanceof Object&&n[t].constructor===Object){n[t]=this.ProccessAllDateTimePropertiesAsString(n[t]);continue}Object.prototype.toString.call(n[t])==="[object Date]"&&(n[t]=n[t].toISOString())}return n},n.prototype.ProccessAllNumberPropertiesAsString=function(n){for(var t in n){if(n[t]instanceof Object&&n[t].constructor===Object){n[t]=this.ProccessAllNumberPropertiesAsString(n[t]);continue}typeof n[t]=="number"&&(n[t]=n[t].toString().replace(".",","))}return n},n}(),CSharpType;(function(n){n[n.String="String"]="String";n[n.Int="Int"]="Int";n[n.Decimal="Decimal"]="Decimal";n[n.Boolean="Boolean"]="Boolean";n[n.DateTime="DateTime"]="DateTime"})(CSharpType||(CSharpType={}));