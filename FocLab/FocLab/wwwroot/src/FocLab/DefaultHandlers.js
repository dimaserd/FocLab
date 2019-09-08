var DefaultHandlers = (function () {
    function DefaultHandlers() {
    }
    DefaultHandlers.IfSuccessReloadPageAfter1500MSecs = function (x) {
        if (x.IsSucceeded) {
            setTimeout(function () { return location.reload(); }, 1500);
        }
    };
    DefaultHandlers.NoHandler = function (x) {
    };
    return DefaultHandlers;
}());
