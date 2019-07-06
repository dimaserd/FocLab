var TaskStaticHandlers = /** @class */ (function () {
    function TaskStaticHandlers() {
    }
    /**
     * Обработчик клика на кнопку обновить
     * */
    TaskStaticHandlers.UpdateBtnClickHandler = function () {
        var data = {
            Id: TaskStaticHandlers.TaskId,
            PerformerQuality: document.getElementsByName("Quality")[0].value,
            PerformerQuantity: document.getElementsByName("Quantity")[0].value,
            PerformerText: document.getElementsByName("Text")[0].value,
            SubstanceCounterJSON: JSON.stringify(SubstanceStaticHandlers.substance.getJSON()),
        };
        Requester.SendPostRequestWithAnimation('/Api/Chemistry/Tasks/Performer/Update', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    };
    TaskStaticHandlers.RemoveTask = function (id) {
        var data = { Id: id, Flag: false };
        Requester.SendPostRequestWithAnimation('/Chemistry/Chemistry/RemoveOrCancelRemoving', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    };
    TaskStaticHandlers.CancelRemoving = function (id) {
        var data = { Id: id, Flag: true };
        Requester.SendPostRequestWithAnimation('/Chemistry/Chemistry/RemoveOrCancelRemoving', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    };
    return TaskStaticHandlers;
}());
