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
            SubstanceCounterJSON: JSON.stringify(SubstanceStaticHandlers.substance.getJSON()),
        };
        CrocoAppCore.Application.Requester.SendPostRequestWithAnimation('/Api/Chemistry/Tasks/Performer/Update', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    }

    static UpdateFileByType(fileType: string) : void {
        GenericUtil.GenericUpdateFileByType(fileType, '/Api/Chemistry/Tasks/ChangeFileForTask', {
            TaskId: TaskStaticHandlers.TaskId
        });
    }

    static RemoveTask(id: string) {

        var data = { Id: id, Flag: false };

        CrocoAppCore.Application.Requester.SendPostRequestWithAnimation('/Chemistry/Chemistry/RemoveOrCancelRemoving', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    }

    static CancelRemoving(id) {

        var data = { Id: id, Flag: true };

        CrocoAppCore.Application.Requester.SendPostRequestWithAnimation('/Chemistry/Chemistry/RemoveOrCancelRemoving', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    }
}