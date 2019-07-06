declare class TaskStaticHandlers {
    static TaskId: string;
    /**
     * Обработчик клика на кнопку обновить
     * */
    static UpdateBtnClickHandler(): void;
    static RemoveTask(id: string): void;
    static CancelRemoving(id: any): void;
}
