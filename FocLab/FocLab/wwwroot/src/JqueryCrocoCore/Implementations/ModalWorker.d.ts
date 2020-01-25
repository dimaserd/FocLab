declare class ModalWorker implements IModalWorker {
    static LoadingModal: string;
    ShowModal(modalId: string): void;
    ShowLoadingModal(): void;
    HideModals(): void;
    HideModal(modalId: string): void;
}
