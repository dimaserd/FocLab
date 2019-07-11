var SubstanceEventSetter = /** @class */ (function () {
    function SubstanceEventSetter() {
    }
    SubstanceEventSetter.InitHandlers = function () {
        EventSetter.SetHandlerForClass("substance-name", "change", function (x) {
            var count = +$(x.target).data("count");
            SubstanceStaticHandlers.ChangeName(count, $(x.target).val().toString());
        });
        EventSetter.SetHandlerForClass("substance-massa", "change", function (x) {
            var count = +$(x.target).data("count");
            SubstanceStaticHandlers.WriteMassa(+$(x.target).val(), count);
        });
        EventSetter.SetHandlerForClass("add-substance-btn", "click", function (x) {
            var prefix = $(x.target).data("prefix").toString();
            SubstanceStaticHandlers.AddSubstance(prefix);
        });
        EventSetter.SetHandlerForClass("substance-mollar-massa", "change", function (x) {
            var count = +$(x.target).data("count");
            var prefix = $(x.target).data("prefix").toString();
            SubstanceStaticHandlers.ChangeMassa(count, prefix);
        });
        EventSetter.SetHandlerForClass("substance-mollar-massa", "input", function (x) {
            var count = +$(x.target).data("count");
            var prefix = $(x.target).data("prefix").toString();
            SubstanceStaticHandlers.ChangeMassa(count, prefix);
        });
        EventSetter.SetHandlerForClass("substance-koef", "change", function (x) {
            var count = +$(x.target).data("count");
            var prefix = $(x.target).data("prefix").toString();
            SubstanceStaticHandlers.ChangeMassa(count, prefix);
        });
        EventSetter.SetHandlerForClass("substance-koef", "input", function (x) {
            var count = +$(x.target).data("count");
            var prefix = $(x.target).data("prefix").toString();
            SubstanceStaticHandlers.ChangeMassa(count, prefix);
        });
        EventSetter.SetHandlerForClass("substance-remove", "click", function (x) {
            var target = x.srcElement;
            console.log(".substance-remove clicked", x.target);
            var count = +$(target).data("count");
            SubstanceStaticHandlers.RemoveSubstanceHandler(count);
        });
        var etalonEvHandler = function (x) {
            var prefix = $(x.target).data("prefix").toString();
            SubstanceStaticHandlers.EtalonChangedHandler(prefix);
        };
        EventSetter.SetHandlerForClass("etalon-field", "change", etalonEvHandler);
        EventSetter.SetHandlerForClass("etalon-field", "input", etalonEvHandler);
    };
    return SubstanceEventSetter;
}());
