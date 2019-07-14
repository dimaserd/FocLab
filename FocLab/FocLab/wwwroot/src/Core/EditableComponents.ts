interface EditableElement {
    ElementId: string;
    Value: string;
    OnValueChangedHandler: Function
}

class EditableComponents {

    static Editables: Array<EditableElement> = [];

    static InitEditable(element: HTMLInputElement, onValueChangedHandler: Function, isTextArea = false) {

        //Получить уникальный идентификатор
        const elementId = Math.random().toString(36);

        //Скрываю настоящий элемент
        element.style.display = "none";
        element.setAttribute("data-editable-real-input", elementId);


        //Добавляю идентификатор элемента, чтобы потом отлавливать его и изменения его значений
        EditableComponents.Editables.push({
            ElementId: elementId,
            Value: element.value,
            OnValueChangedHandler: onValueChangedHandler
        });

        // Получаю фейковый элемент который редактируем
        const newNode = EditableComponents.GetFakeElement(element, elementId, isTextArea);

        // Вставляю новый элемент перед скрытым старым
        element.before(newNode);

        this.InitEditableInner(elementId);
        this.InitLiasoningOnChange(element, elementId);
    }    

    static InitLiasoningOnChange(element: HTMLInputElement, elementId) {
        element.addEventListener("change", function () {

            console.log("Обработчик реального измененного элемента");
            let elemId = elementId;
            const fakeElement = document.querySelectorAll(`[data-editable-input="${elemId}"]`)[0] as HTMLInputElement;

            fakeElement.value = this.value;    

        });

        const fakeElement = document.querySelectorAll(`[data-editable-input="${elementId}"]`)[0];

        fakeElement.addEventListener("change", x => {

            let elemId = elementId;
            const realElement = document.querySelectorAll(`[data-editable-real-input="${elemId}"]`)[0] as HTMLInputElement;

            realElement.value = (x.target as HTMLInputElement).value;
        });    
    }

    //Создать фейковый элемент 
    static GetFakeElement(element, elementId: string, isTextArea): HTMLDivElement {

        const div = document.createElement("div");

        const html = `<div class="form-group editable" data-editable-form="${elementId}">
        <div class="editable-backdrop" data-editable-backdrop="${elementId}"></div>
        ${EditableComponents.GetFakeInputElement(element, elementId, isTextArea)}
        <div class="editable-buttons">
            <button class="btn btn-sm btn-editable" data-editable-check="${elementId}"><i class="fas fa-check"></i></button>
            <button class="btn btn-sm btn-editable" data-editable-cancel="${elementId}"><i class="fas fa-times"></i></button>
        </div>
        </div>`;

        div.innerHTML = html;

        return div;   
    }

    static GetFakeInputElement(element: HTMLInputElement, elementId: string, isTextArea: boolean): string {

        if (!isTextArea) {
            return `<input type="text" class="${element.className} editable-input" name="${element.id}" data-editable-input="${elementId}"
                    data-editable-input-id="${elementId}" value="${element.value}">`;
        }

        return `<textarea class="${element.className} editable-input" name="${element.id}" data-editable-input="${elementId}"
                    data-editable-input-id="${elementId}">${element.value}</textarea>`;
    }    

        //Сюда передаётся фейковый элемент
    static InitEditableInner(elementId : string) {

        const element = document.querySelectorAll(`[data-editable-input="${elementId}"]`)[0] as HTMLInputElement;

        element.addEventListener("click", x => this.OnInputClickHandler(x.target as HTMLElement), false);

        //<button class="btn btn-sm btn-editable"><i class="fas fa-check"></i></button>
        const btnCheck = document.querySelectorAll(`[data-editable-check="${elementId}"]`)[0];

        btnCheck.addEventListener("click", (function () { this.OnBtnCheckClickHandler(elementId) }).bind(this));

        //<button class="btn btn-sm btn-editable"><i class="fas fa-times"></i></button>
        const btnBack = document.querySelectorAll(`[data-editable-cancel="${elementId}"]`)[0];

        btnBack.addEventListener("click", (function () { this.OnBtnCancelClickHandler(elementId) }).bind(this));

        const backDrop = document.querySelectorAll(`[data-editable-backdrop="${elementId}"]`)[0];

        backDrop.addEventListener("click", (function () { this.BackDropClickHandler(elementId) }).bind(this));    
    }

    static CheckValueChanged(elementId: string) {
        //Получаю инпут
        const input = document.querySelectorAll(`[data-editable-input="${elementId}"]`)[0] as HTMLInputElement;

        const record = EditableComponents.Editables.find(x => x.ElementId === elementId);

        const newValue = input.value;

        //значит значение изменилось
        if (record.Value !== newValue) {

            //Вызываю обработчик события для измененного значения
            if (record.OnValueChangedHandler) {
                record.OnValueChangedHandler(newValue);
            }
            record.Value = newValue;
            EditableComponents.Editables = this.Editables.filter(x => x.ElementId !== elementId);
            EditableComponents.Editables.push(record);
        }
    }    

    static BackDropClickHandler(elementId : string) {

        const form = document.querySelectorAll(`[data-editable-form="${elementId}"]`)[0];

        form.classList.remove("editable--active");

        //Получаю инпут
        const input = document.querySelectorAll(`[data-editable-input="${elementId}"]`)[0];

        input.removeAttribute("editable");

        EditableComponents.CheckValueChanged(elementId);
    }    

    //Клик на редактируемый инпут
    static OnInputClickHandler(x: HTMLElement) {

        const elementId = x.getAttribute("data-editable-input-id");

        //Получаю инпут
        const input = document.querySelectorAll(`[data-editable-input="${elementId}"]`)[0];

        input.setAttribute("editable", "editable");

        //Получаю форму
        const form = document.querySelectorAll(`[data-editable-form="${elementId}"]`)[0];

        form.classList.add("editable--active");
    }    

    static OnBtnCheckClickHandler(elementId: string) {

        const input = document.querySelectorAll(`[data-editable-input="${elementId}"]`)[0];
        input.removeAttribute("editable");

        const form = document.querySelectorAll(`[data-editable-form="${elementId}"]`)[0];
        form.classList.remove("editable--active");


        EditableComponents.CheckValueChanged(elementId);
    }

    static OnBtnCancelClickHandler(elementId: string) {

        const input = document.querySelectorAll(`[data-editable-input="${elementId}"]`)[0] as HTMLInputElement;
        input.removeAttribute("editable");

        const form = document.querySelectorAll(`[data-editable-form="${elementId}"]`)[0];
        form.classList.remove("editable--active");

        const record = EditableComponents.Editables.find(x => x.ElementId === elementId);

        //возращение к первичному значению
        input.value = record.Value;
    }    
}