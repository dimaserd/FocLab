interface EditableElement {
    ElementId: string;
    Value: string;
    OnValueChangedHandler: Function;
}
declare class EditableComponents {
    static Editables: Array<EditableElement>;
    static InitEditable(element: HTMLInputElement, onValueChangedHandler: Function, isTextArea?: boolean): void;
    static InitLiasoningOnChange(element: HTMLInputElement, elementId: any): void;
    static GetFakeElement(element: any, elementId: string, isTextArea: any): HTMLDivElement;
    static GetFakeInputElement(element: HTMLInputElement, elementId: string, isTextArea: boolean): string;
    static InitEditableInner(elementId: string): void;
    static CheckValueChanged(elementId: string): void;
    static BackDropClickHandler(elementId: string): void;
    static OnInputClickHandler(x: HTMLElement): void;
    static OnBtnCheckClickHandler(elementId: string): void;
    static OnBtnCancelClickHandler(elementId: string): void;
}
