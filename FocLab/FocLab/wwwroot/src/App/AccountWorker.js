var AccountWorker = (function () {
    function AccountWorker() {
    }
    AccountWorker.CheckUser = function () {
        if (!this.IsAuthenticated() || true) {
            return;
        }
    };
    AccountWorker.IsAuthenticated = function () {
        var value = this.User != null;
        console.log("AccountWorker.IsAuthenticated()=" + value);
        return value;
    };
    AccountWorker.User = null;
    return AccountWorker;
}());
document.addEventListener("DOMContentLoaded", function () {
    setTimeout(function () { AccountWorker.CheckUser(); }, 1100);
});
