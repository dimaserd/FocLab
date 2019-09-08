var ModalWorker = (function () {
    function ModalWorker() {
    }
    ModalWorker.ShowModal = function (modalId) {
        if (modalId == "" || modalId == null || modalId == undefined) {
            modalId = "loadingModal";
        }
        else {
            $('#' + modalId).modal('show');
        }
    };
    ModalWorker.ChangeModalValues = function (modalId, modalHeader, modalContent) {
        if (modalHeader != null) {
            document.getElementById("header" + modalId).innerText = modalHeader;
        }
        if (modalContent != null) {
            document.getElementById("body" + modalId).innerHTML = modalContent;
        }
    };
    ModalWorker.ShowModalWithValues = function (modalId, modalHeader, modalContent) {
        this.ChangeModalValues(modalId, modalHeader, modalContent);
        this.ShowModal(modalId);
    };
    ModalWorker.HideModals = function () {
        $('.modal').modal('hide');
        $(".modal-backdrop.fade").remove();
        $('.modal').on('shown.bs.modal', function () {
        });
    };
    ModalWorker.HideModal = function (modalId) {
        $("#" + modalId).modal('hide');
    };
    return ModalWorker;
}());
