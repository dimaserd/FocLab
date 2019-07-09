class CreateExperimentHandlers {
    static Create(): void {
        var data = {
            TaskId: "",
            Title: ""
        };

        Requester.SendPostRequestWithAnimation('/Api/Chemistry/Experiments/Create', FormDataHelper.CollectData(data), x => {
            if (x.IsSucceeded) {
                setTimeout(() => { location.href = "/Chemistry/Experiments/Index" }, 1500);
            }
        }, null);
    }

    static SetHandlers() {
        EventSetter.SetHandlerForClass("btn-exp-create", "click", () => CreateExperimentHandlers.Create());
    }
}

CreateExperimentHandlers.SetHandlers();