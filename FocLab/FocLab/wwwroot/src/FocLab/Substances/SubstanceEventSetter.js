var SubstanceEventSetter = /** @class */ (function () {
    function SubstanceEventSetter() {
    }
    SubstanceEventSetter.SetHandlerForClass = function (className, eventName, handlerFunction) {
        var classNameElems = document.getElementsByClassName(className);
        Array.from(classNameElems).forEach(function (x) { return x.addEventListener(eventName, handlerFunction, false); });
    };
    SubstanceEventSetter.InitHandlers = function () {
        SubstanceEventSetter.SetHandlerForClass("substance-name", "change", function (x) {
            var count = +$(x.target).data("count");
            SubstanceStaticHandlers.ChangeName(count, $(x.target).val().toString());
        });
        SubstanceEventSetter.SetHandlerForClass("substance-massa", "change", function (x) {
            var count = +$(x.target).data("count");
            SubstanceStaticHandlers.WriteMassa(+$(x.target).val(), count);
        });
        SubstanceEventSetter.SetHandlerForClass("add-substance-btn", "click", function (x) {
            var prefix = $(x.target).data("prefix").toString();
            SubstanceStaticHandlers.AddSubstance(prefix);
        });
        SubstanceEventSetter.SetHandlerForClass("substance-mollar-massa", "change", function (x) {
            var count = +$(x.target).data("count");
            var prefix = $(x.target).data("prefix").toString();
            SubstanceStaticHandlers.ChangeMassa(count, prefix);
        });
        SubstanceEventSetter.SetHandlerForClass("substance-mollar-massa", "input", function (x) {
            var count = +$(x.target).data("count");
            var prefix = $(x.target).data("prefix").toString();
            SubstanceStaticHandlers.ChangeMassa(count, prefix);
        });
        SubstanceEventSetter.SetHandlerForClass("substance-koef", "change", function (x) {
            var count = +$(x.target).data("count");
            var prefix = $(x.target).data("prefix").toString();
            SubstanceStaticHandlers.ChangeMassa(count, prefix);
        });
        SubstanceEventSetter.SetHandlerForClass("substance-koef", "input", function (x) {
            var count = +$(x.target).data("count");
            var prefix = $(x.target).data("prefix").toString();
            SubstanceStaticHandlers.ChangeMassa(count, prefix);
        });
        SubstanceEventSetter.SetHandlerForClass("substance-remove", "click", function (x) {
            var count = +$(x.target).data("count");
            SubstanceStaticHandlers.RemoveSubstanceHandler(count);
        });
        var etalonEvHandler = function (x) {
            var prefix = $(x.target).data("prefix").toString();
            SubstanceStaticHandlers.EtalonChangedHandler(prefix);
        };
        SubstanceEventSetter.SetHandlerForClass("etalon-field", "change", etalonEvHandler);
        SubstanceEventSetter.SetHandlerForClass("etalon-field", "input", etalonEvHandler);
    };
    return SubstanceEventSetter;
}());
