var ExperimentPageHandlers = /** @class */ (function () {
    function ExperimentPageHandlers() {
    }
    ExperimentPageHandlers.UpdateFileByType = function (fileType) {
        GenericUtil.GenericUpdateFileByType(fileType, '/Api/Chemistry/Tasks/ChangeFileForTask', {
            TaskId: TaskStaticHandlers.TaskId,
            FileType: fileType
        });
    };
    ExperimentPageHandlers.SetHandlers = function () {
        //EventSetter.SetHandlerForClass("")
    };
    return ExperimentPageHandlers;
}());
