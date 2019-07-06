class TaskStaticHandlers {

    static TaskId: string;

    /**
     * Обработчик клика на кнопку обновить
     * */
    static UpdateBtnClickHandler() : void {
        var data = {
            Id: TaskStaticHandlers.TaskId,
            PerformerQuality: (document.getElementsByName("Quality")[0] as HTMLInputElement).value,
            PerformerQuantity: (document.getElementsByName("Quantity")[0] as HTMLInputElement).value,
            PerformerText: (document.getElementsByName("Text")[0] as HTMLInputElement).value,
            SubstanceCounterJSON: JSON.stringify(substance.getJSON()),
        };
        Requester.SendPostRequestWithAnimation('/Api/Chemistry/Tasks/Performer/Update', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    }

    static RemoveTask(id: string) {

        var data = { Id: id, Flag: false };

        Requester.SendPostRequestWithAnimation('/Chemistry/Chemistry/RemoveOrCancelRemoving', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    }

    static CancelRemoving(id) {

        var data = { Id: id, Flag: true };

        Requester.SendPostRequestWithAnimation('/Chemistry/Chemistry/RemoveOrCancelRemoving', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    }
}