class ModalWorker {
    
    /** Показать модальное окно по идентификатору. */
    public static ShowModal = function (modalId: string): void {

        if (modalId == "" || modalId == null || modalId == undefined) {
            modalId = "loadingModal";
        }
        else {
            $('#' + modalId).modal('show');
        }
    }

    public static ChangeModalValues = function (modalId: string, modalHeader: string, modalContent: string): void {
        if (modalHeader != null) {
            document.getElementById(`header${modalId}`).innerText = modalHeader;
        }

        if (modalContent != null) {
            document.getElementById("body" + modalId).innerHTML = modalContent;
        }
    }

    public static ShowModalWithValues = function (modalId: string, modalHeader: string, modalContent: string): void {

        this.ChangeModalValues(modalId, modalHeader, modalContent);

        this.ShowModal(modalId);
    }
    
    public static HideModals = function (): void {

        $('.modal').modal('hide');

        $(".modal-backdrop.fade").remove();

        $('.modal').on('shown.bs.modal', function () {
        })
    }

    public static HideModal = function (modalId: string): void {
        $(`#${modalId}`).modal('hide');
    }
}