class EventSetter {
    static SetHandlerForClass(className: string, eventName: string, handlerFunction: EventListenerOrEventListenerObject) {
        var classNameElems = document.getElementsByClassName(className);

        Array.from(classNameElems).forEach(x => x.addEventListener(eventName, handlerFunction, false));
    }
}