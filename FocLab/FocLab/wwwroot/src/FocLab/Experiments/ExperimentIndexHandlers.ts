class ExperimentIndexHandlers {

    
    

    static PerformExperiment(id: string, flag: boolean): void {

        var data = {
            ExperimentId: id,
            Performed: flag
        };

        Requester.SendPostRequestWithAnimation('/Api/Chemistry/Experiments/Perform', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    }

    static RemoveExperiment(id: string) : void {
        Requester.SendPostRequestWithAnimation(`/Api/Chemistry/Experiments/Remove?id=${id}`, {}, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    }

    static CancelRemoveExperiment(id: string) : void {
        Requester.SendPostRequestWithAnimation(`/Api/Chemistry/Experiments/CancelRemove?id=${id}`, {}, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    }

    static SetHandlers() {
        EventSetter.SetHandlerForClass("exp-perform-btn", "click", x => {
            var id = $(x.target).data("id").toString();
            ExperimentIndexHandlers.PerformExperiment(id, true);
        });

        EventSetter.SetHandlerForClass("exp-cancel-remove-btn", "click", x => {
            var id = $(x.target).data("id").toString();
            ExperimentIndexHandlers.CancelRemoveExperiment(id);
        });

        EventSetter.SetHandlerForClass("exp-remove-btn", "click", x => {
            var id = $(x.target).data("id").toString();
            ExperimentIndexHandlers.RemoveExperiment(id);
        });

        EventSetter.SetHandlerForClass("exp-cancel-perform-btn", "click", x => {
            var id = $(x.target).data("id").toString();

            ExperimentIndexHandlers.PerformExperiment(id, false);
        })
    }
}

ExperimentIndexHandlers.SetHandlers();