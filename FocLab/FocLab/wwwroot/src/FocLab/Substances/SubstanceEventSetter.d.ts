declare class SubstanceEventSetter {
    static SetHandlerForClass(className: string, eventName: string, handlerFunction: EventListenerOrEventListenerObject): void;
    static InitHandlers(): void;
}
