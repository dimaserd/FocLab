declare class ModalWorker {
    /** Показать модальное окно по идентификатору. */
    static ShowModal: (modalId: string) => void;
    static ChangeModalValues: (modalId: string, modalHeader: string, modalContent: string) => void;
    static ShowModalWithValues: (modalId: string, modalHeader: string, modalContent: string) => void;
    static HideModals: () => void;
    static HideModal: (modalId: string) => void;
}
