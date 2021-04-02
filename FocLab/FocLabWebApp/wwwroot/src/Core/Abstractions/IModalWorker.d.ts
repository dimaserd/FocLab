interface IModalWorker {
    ShowModal(modalId: string): void;
    ShowLoadingModal(): void;
    HideModal(modalId: string): void;
    HideModals(): void;
}
