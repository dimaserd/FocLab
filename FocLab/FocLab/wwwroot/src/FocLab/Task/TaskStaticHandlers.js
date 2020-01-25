var TaskStaticHandlers = (function () {
    function TaskStaticHandlers() {
    }
    TaskStaticHandlers.UpdateBtnClickHandler = function () {
        var data = {
            Id: TaskStaticHandlers.TaskId,
            PerformerQuality: document.getElementsByName("Quality")[0].value,
            PerformerQuantity: document.getElementsByName("Quantity")[0].value,
            PerformerText: document.getElementsByName("Text")[0].value,
            SubstanceCounterJSON: JSON.stringify(SubstanceStaticHandlers.substance.getJSON()),
        };
        CrocoAppCore.Application.Requester.SendPostRequestWithAnimation('/Api/Chemistry/Tasks/Performer/Update', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    };
    TaskStaticHandlers.UpdateFileByType = function (fileType) {
        GenericUtil.GenericUpdateFileByType(fileType, '/Api/Chemistry/Tasks/ChangeFileForTask', {
            TaskId: TaskStaticHandlers.TaskId
        });
    };
    TaskStaticHandlers.RemoveTask = function (id) {
        var data = { Id: id, Flag: false };
        CrocoAppCore.Application.Requester.SendPostRequestWithAnimation('/Chemistry/Chemistry/RemoveOrCancelRemoving', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    };
    TaskStaticHandlers.CancelRemoving = function (id) {
        var data = { Id: id, Flag: true };
        CrocoAppCore.Application.Requester.SendPostRequestWithAnimation('/Chemistry/Chemistry/RemoveOrCancelRemoving', data, DefaultHandlers.IfSuccessReloadPageAfter1500MSecs, null);
    };
    return TaskStaticHandlers;
}());
