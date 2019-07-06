var SubstanceStaticHandlers = /** @class */ (function () {
    function SubstanceStaticHandlers() {
    }
    SubstanceStaticHandlers.SetHandlerForClass = function (className, eventName, handlerFunction) {
        var classNameElems = document.getElementsByClassName(className);
        console.log("SubstanceStaticHandlers.SetHandlerForClass", classNameElems);
        Array.from(classNameElems).forEach(function (x) { return x.addEventListener(eventName, handlerFunction, false); });
    };
    SubstanceStaticHandlers.RemoveSubstanceHandler = function (count) {
        console.log("RemoveSubstanceHandler", count);
        var substanceToDelete = SubstanceStaticHandlers.substance.Substances[count];
        SubstanceStaticHandlers.substance.Substances = SubstanceStaticHandlers.substance.Substances.filter(function (x) { return x != substanceToDelete; });
        ;
        SubstanceStaticHandlers.substance.ClearTable();
        SubstanceStaticHandlers.substance.DrawTable();
        TaskStaticHandlers.UpdateBtnClickHandler();
    };
    SubstanceStaticHandlers.EtalonChangedHandler = function (prefix) {
        if (prefix == "") {
            SubstanceStaticHandlers.substance.setProperties();
            console.log(SubstanceStaticHandlers.substance);
        }
    };
    SubstanceStaticHandlers.ChangeMassa = function (count, prefix) {
        console.log("SubstanceStaticHandlers.ChangeMassa", count, prefix);
        var molarMassa = +document.getElementsByName(prefix + ".MolarMassa[" + count + "]")[0].value;
        var koef = +document.getElementsByName(prefix + ".Koef[" + count + "]")[0].value;
        var massa = SubstanceStaticHandlers.substance.getTotalKoef() * koef * molarMassa;
        massa = +massa.toFixed(2);
        SubstanceStaticHandlers.substance.Substances[count].MolarMassa = molarMassa;
        SubstanceStaticHandlers.substance.Substances[count].Massa = massa;
        SubstanceStaticHandlers.substance.Substances[count].Koef = koef;
        document.getElementsByName(prefix + ".Massa[" + count + "]")[0].value = massa.toFixed(2);
    };
    SubstanceStaticHandlers.WriteMassa = function (value, count) {
        SubstanceStaticHandlers.substance.Substances[count].Massa = value;
    };
    SubstanceStaticHandlers.ChangeName = function (count, value) {
        console.log("SubstanceStaticHandlers.ChangeName");
        SubstanceStaticHandlers.substance.Substances[count].Name = value;
    };
    SubstanceStaticHandlers.AddSubstance = function (prefix) {
        if (prefix === "") {
            SubstanceStaticHandlers.substance.AddSubstance();
        }
    };
    return SubstanceStaticHandlers;
}());
