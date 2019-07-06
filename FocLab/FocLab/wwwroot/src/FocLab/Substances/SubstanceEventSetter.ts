class SubstanceEventSetter {
    static SetHandlerForClass(className: string, eventName: string, handlerFunction: EventListenerOrEventListenerObject) {
        var classNameElems = document.getElementsByClassName(className);

        console.log("SubstanceStaticHandlers.SetHandlerForClass", classNameElems)


        Array.from(classNameElems).forEach(x => x.addEventListener(eventName, handlerFunction, false));
    }

    static InitHandlers(): void {

        SubstanceEventSetter.SetHandlerForClass("substance-name", "change", x => {
            var count = +$(x.target).data("count");
            SubstanceStaticHandlers.ChangeName(count, $(x.target).val().toString());
        })

        SubstanceEventSetter.SetHandlerForClass("substance-massa", "change", x => {
            var count = +$(x.target).data("count");
            SubstanceStaticHandlers.WriteMassa(+$(x.target).val(), count);
        });

        var evHandler = x => {
            var count = +$(x.target).data("count");
            var prefix = $(x.target).data("prefix").toString();
            SubstanceStaticHandlers.ChangeMassa(count, prefix);
        };

        for (let elem in ["substance-mollar-massa", "substance-koef"]) {

            SubstanceEventSetter.SetHandlerForClass(elem, "change", evHandler);
            SubstanceEventSetter.SetHandlerForClass(elem, "input", evHandler);
        }

        SubstanceEventSetter.SetHandlerForClass("substance-remove", "click", x => {
            var count = +$(x.target).data("count");
            SubstanceStaticHandlers.RemoveSubstanceHandler(count);
        });

        var etalonEvHandler = x => {

            var prefix = $(x.target).data("prefix").toString();
            SubstanceStaticHandlers.EtalonChangedHandler(prefix);
        };

        SubstanceEventSetter.SetHandlerForClass("etalon-field", "change", etalonEvHandler);
        SubstanceEventSetter.SetHandlerForClass("etalon-field", "input", etalonEvHandler);
    }
}