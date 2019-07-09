class ExperimentPageHandlers {

    static UpdateFileByType(fileType: string) : void {

        GenericUtil.GenericUpdateFileByType(fileType, '/Api/Chemistry/Tasks/ChangeFileForTask', {
            TaskId: TaskStaticHandlers.TaskId,
            FileType: fileType
        });
    }

    static SetHandlers(): void {
        //EventSetter.SetHandlerForClass("")
    }
}