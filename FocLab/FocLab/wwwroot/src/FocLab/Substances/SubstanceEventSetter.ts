class SubstanceEventSetter {
    

    static InitHandlers(): void {

        EventSetter.SetHandlerForClass("substance-name", "change", x => {
            var count = +$(x.target).data("count");
            SubstanceStaticHandlers.ChangeName(count, $(x.target).val().toString());
        })

        EventSetter.SetHandlerForClass("substance-massa", "change", x => {
            var count = +$(x.target).data("count");
            SubstanceStaticHandlers.WriteMassa(+$(x.target).val(), count);
        });


        EventSetter.SetHandlerForClass("add-substance-btn", "click", x => {
            var prefix = $(x.target).data("prefix").toString();
            SubstanceStaticHandlers.AddSubstance(prefix);
        });

        EventSetter.SetHandlerForClass("substance-mollar-massa", "change", x => {
                var count = +$(x.target).data("count");
                var prefix = $(x.target).data("prefix").toString();
                SubstanceStaticHandlers.ChangeMassa(count, prefix);
            });
        EventSetter.SetHandlerForClass("substance-mollar-massa", "input", x => {
                var count = +$(x.target).data("count");
                var prefix = $(x.target).data("prefix").toString();
                SubstanceStaticHandlers.ChangeMassa(count, prefix);
            });
        EventSetter.SetHandlerForClass("substance-koef", "change", x => {
            var count = +$(x.target).data("count");
            var prefix = $(x.target).data("prefix").toString();
            SubstanceStaticHandlers.ChangeMassa(count, prefix);
        });
        EventSetter.SetHandlerForClass("substance-koef", "input", x => {
            var count = +$(x.target).data("count");
            var prefix = $(x.target).data("prefix").toString();
            SubstanceStaticHandlers.ChangeMassa(count, prefix);
        });                                                    

        EventSetter.SetHandlerForClass("substance-remove", "click", x => {

            var target = x.srcElement;
            console.log(".substance-remove clicked", x.target);
            var count = +$(target).data("count");
            SubstanceStaticHandlers.RemoveSubstanceHandler(count);
        });

        var etalonEvHandler = x => {

            var prefix = $(x.target).data("prefix").toString();
            SubstanceStaticHandlers.EtalonChangedHandler(prefix);
        };

        EventSetter.SetHandlerForClass("etalon-field", "change", etalonEvHandler);
        EventSetter.SetHandlerForClass("etalon-field", "input", etalonEvHandler);
    }
}