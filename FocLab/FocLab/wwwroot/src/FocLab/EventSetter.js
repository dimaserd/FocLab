var EventSetter = (function () {
    function EventSetter() {
    }
    EventSetter.SetHandlerForClass = function (className, eventName, handlerFunction) {
        var classNameElems = document.getElementsByClassName(className);
        Array.from(classNameElems).forEach(function (x) { return x.addEventListener(eventName, handlerFunction, false); });
    };
    return EventSetter;
}());
