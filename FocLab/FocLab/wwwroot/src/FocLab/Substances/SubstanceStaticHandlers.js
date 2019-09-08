var SubstanceStaticHandlers = (function () {
    function SubstanceStaticHandlers() {
    }
    SubstanceStaticHandlers.RemoveSubstanceHandler = function (count) {
        var copy = Object.assign(SubstanceStaticHandlers.substance);
        console.log("RemoveSubstanceHandler{" + count + ") BeforeRemove", copy);
        SubstanceStaticHandlers.substance.Substances.splice(count);
        console.log("RemoveSubstanceHandler AfterRemove", SubstanceStaticHandlers.substance);
        SubstanceStaticHandlers.substance.ClearTable();
        SubstanceStaticHandlers.substance.DrawTable();
    };
    SubstanceStaticHandlers.EtalonChangedHandler = function (prefix) {
        if (prefix == "") {
            SubstanceStaticHandlers.substance.setProperties();
            console.log(SubstanceStaticHandlers.substance);
        }
    };
    SubstanceStaticHandlers.ChangeMassa = function (count, prefix) {
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
