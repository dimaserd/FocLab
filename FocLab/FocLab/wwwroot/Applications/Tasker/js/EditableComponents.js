class EditableComponents {
    constructor() {
        this.setHandlers();

        this.Editables = [];
    }

    setHandlers() {
        this.InitEditable = function (element, onValueChangedHandler, isTextArea = false) {

            //Получить уникальный идентификатор
            const elementId = Math.random().toString(36);

            //Скрываю настоящий элемент
            element.style.display = "none";
            element.setAttribute("data-editable-real-input", elementId);
            
            
            //Добавляю идентификатор элемента, чтобы потом отлавливать его и изменения его значений
            this.Editables.push({
                ElementId: elementId,
                Value: element.value,
                OnValueChangedHandler: onValueChangedHandler
            });
            
            // Получаю фейковый элемент который редактируем
            const newNode = this.GetFakeElement(element, elementId, isTextArea);

            // Вставляю новый элемент перед скрытым старым
            element.before(newNode);

            this.InitEditableInner(elementId);
            this.InitLiasoningOnChange(element, elementId);
        }

        this.InitLiasoningOnChange = function (element, elementId) {
            element.addEventListener("change", function() {

                console.log("Обработчик реального измененного элемента");
                let elemId = elementId;
                const fakeElement = document.querySelectorAll(`[data-editable-input="${elemId}"]`)[0];

                fakeElement.value = this.value;

            });

            const fakeElement = document.querySelectorAll(`[data-editable-input="${elementId}"]`)[0];

            fakeElement.addEventListener("change", function () {
                
                let elemId = elementId;
                const realElement = document.querySelectorAll(`[data-editable-real-input="${elemId}"]`)[0];

                realElement.value = this.value;
            });
        }

        //Создать фейковый элемент 
        this.GetFakeElement = function (element, elementId, isTextArea) {

            const div = document.createElement("div");

            const html = `<div class="form-group editable" data-editable-form="${elementId}">
        <div class="editable-backdrop" data-editable-backdrop="${elementId}"></div>
        ${this.GetFakeInputElement(element, elementId, isTextArea)}
        <div class="editable-buttons">
            <button class="btn btn-sm btn-editable" data-editable-check="${elementId}"><i class="fas fa-check"></i></button>
            <button class="btn btn-sm btn-editable" data-editable-cancel="${elementId}"><i class="fas fa-times"></i></button>
        </div>
        </div>`;

            div.innerHTML = html;

            return div;
        }

        this.GetFakeInputElement = function (element, elementId, isTextArea) {

            if (!isTextArea) {
                return `<input type="text" class="${element.className} editable-input" name="${element.id}" data-editable-input="${elementId}"
                    data-editable-input-id="${elementId}" value="${element.value}">`;
            }

            return `<textarea class="${element.className} editable-input" name="${element.id}" data-editable-input="${elementId}"
                    data-editable-input-id="${elementId}">${element.value}</textarea>`;
        }

        //Сюда передаётся фейковый элемент
        this.InitEditableInner = function (elementId) {

            const element = document.querySelectorAll(`[data-editable-input="${elementId}"]`)[0];

            //Устанавливаю на сам инпут
            element.onclick = this.OnInputClickHandler;

            //<button class="btn btn-sm btn-editable"><i class="fas fa-check"></i></button>
            const btnCheck = document.querySelectorAll(`[data-editable-check="${elementId}"]`)[0];

            btnCheck.addEventListener("click", (function () { this.OnBtnCheckClickHandler(elementId) }).bind(this));

            //<button class="btn btn-sm btn-editable"><i class="fas fa-times"></i></button>
            const btnBack = document.querySelectorAll(`[data-editable-cancel="${elementId}"]`)[0];

            btnBack.addEventListener("click", (function () { this.OnBtnCancelClickHandler(elementId) }).bind(this));

            const backDrop = document.querySelectorAll(`[data-editable-backdrop="${elementId}"]`)[0];

            backDrop.addEventListener("click", (function () { this.BackDropClickHandler(elementId) }).bind(this));
        }

        this.CheckValueChanged = function(elementId) {
            //Получаю инпут
            const input = document.querySelectorAll(`[data-editable-input="${elementId}"]`)[0];

            const record = this.Editables.find(x => x.ElementId === elementId);

            const newValue = input.value;

            //значит значение изменилось
            if (record.Value !== newValue) {

                //Вызываю обработчик события для измененного значения
                if (record.OnValueChangedHandler) {
                    record.OnValueChangedHandler(newValue);
                }
                record.Value = newValue;
                this.Editables = this.Editables.filter(x => x.ElementId !== elementId);
                this.Editables.push(record);
            }
        }

        this.BackDropClickHandler = function (elementId) {

            const form = document.querySelectorAll(`[data-editable-form="${elementId}"]`)[0];

            form.classList.remove("editable--active");

            //Получаю инпут
            const input = document.querySelectorAll(`[data-editable-input="${elementId}"]`)[0];

            input.removeAttribute("editable");

            this.CheckValueChanged(elementId);
        }

        //Клик на редактируемый инпут
        this.OnInputClickHandler = function () {

            const elementId = this.getAttribute("data-editable-input-id");

            //Получаю инпут
            const input = document.querySelectorAll(`[data-editable-input="${elementId}"]`)[0];

            input.setAttribute("editable", "editable");

            //Получаю форму
            const form = document.querySelectorAll(`[data-editable-form="${elementId}"]`)[0];
            
            form.classList.add("editable--active");
        }

        this.OnBtnCheckClickHandler = function (elementId) {

            const input = document.querySelectorAll(`[data-editable-input="${elementId}"]`)[0];
            input.removeAttribute("editable");

            const form = document.querySelectorAll(`[data-editable-form="${elementId}"]`)[0];
            form.classList.remove("editable--active");


            this.CheckValueChanged(elementId);
        }

        this.OnBtnCancelClickHandler = function (elementId) {

            const input = document.querySelectorAll(`[data-editable-input="${elementId}"]`)[0];
            input.removeAttribute("editable");

            const form = document.querySelectorAll(`[data-editable-form="${elementId}"]`)[0];
            form.classList.remove("editable--active");

            const record = this.Editables.find(x => x.ElementId === elementId);

            //возращение к первичному значению
            input.value = record.Value;
        }
    }
}

const editableComponents = new EditableComponents();