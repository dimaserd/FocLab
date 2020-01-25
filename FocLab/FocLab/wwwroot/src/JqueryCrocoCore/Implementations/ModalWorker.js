var ModalWorker = (function () {
    function ModalWorker() {
    }
    ModalWorker.prototype.ShowModal = function (modalId) {
        if (modalId === "" || modalId == null || modalId == undefined) {
            modalId = ModalWorker.LoadingModal;
        }
        $("#" + modalId).modal('show');
    };
    ModalWorker.prototype.ShowLoadingModal = function () {
        this.ShowModal(ModalWorker.LoadingModal);
    };
    ModalWorker.prototype.HideModals = function () {
        $('.modal').modal('hide');
        $(".modal-backdrop.fade").remove();
        $('.modal').on('shown.bs.modal', function () {
        });
    };
    ModalWorker.prototype.HideModal = function (modalId) {
        $("#" + modalId).modal('hide');
    };
    ModalWorker.LoadingModal = "loadingModal";
    return ModalWorker;
}());
