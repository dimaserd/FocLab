var DefaultHandlers = /** @class */ (function () {
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

var EventSetter = /** @class */ (function () {
    function EventSetter() {
    }
    EventSetter.SetHandlerForClass = function (className, eventName, handlerFunction) {
        var classNameElems = document.getElementsByClassName(className);
        Array.from(classNameElems).forEach(function (x) { return x.addEventListener(eventName, handlerFunction, false); });
    };
    return EventSetter;
}());