var EditableComponents = (function () {
    function EditableComponents() {
    }
    EditableComponents.InitEditable = function (element, onValueChangedHandler, isTextArea) {
        if (isTextArea === void 0) { isTextArea = false; }
        var elementId = Math.random().toString(36);
        element.style.display = "none";
        element.setAttribute("data-editable-real-input", elementId);
        EditableComponents.Editables.push({
            ElementId: elementId,
            Value: element.value,
            OnValueChangedHandler: onValueChangedHandler
        });
        var newNode = EditableComponents.GetFakeElement(element, elementId, isTextArea);
        element.before(newNode);
        this.InitEditableInner(elementId);
        this.InitLiasoningOnChange(element, elementId);
    };
    EditableComponents.InitLiasoningOnChange = function (element, elementId) {
        element.addEventListener("change", function (e) {
            var elemId = elementId;
            var fakeElement = document.querySelectorAll("[data-editable-input=\"" + elemId + "\"]")[0];
            fakeElement.value = e.target.value;
        });
        var fakeElement = document.querySelectorAll("[data-editable-input=\"" + elementId + "\"]")[0];
        fakeElement.addEventListener("change", function (x) {
            var elemId = elementId;
            var realElement = document.querySelectorAll("[data-editable-real-input=\"" + elemId + "\"]")[0];
            realElement.value = x.target.value;
        });
    };
    EditableComponents.GetFakeElement = function (element, elementId, isTextArea) {
        var div = document.createElement("div");
        var html = "<div class=\"form-group editable\" data-editable-form=\"" + elementId + "\">\n        <div class=\"editable-backdrop\" data-editable-backdrop=\"" + elementId + "\"></div>\n        " + EditableComponents.GetFakeInputElement(element, elementId, isTextArea) + "\n        <div class=\"editable-buttons\">\n            <button class=\"btn btn-sm btn-editable\" data-editable-check=\"" + elementId + "\"><i class=\"fas fa-check\"></i></button>\n            <button class=\"btn btn-sm btn-editable\" data-editable-cancel=\"" + elementId + "\"><i class=\"fas fa-times\"></i></button>\n        </div>\n        </div>";
        div.innerHTML = html;
        return div;
    };
    EditableComponents.GetFakeInputElement = function (element, elementId, isTextArea) {
        if (!isTextArea) {
            return "<input type=\"text\" class=\"" + element.className + " editable-input\" name=\"" + element.id + "\" data-editable-input=\"" + elementId + "\"\n                    data-editable-input-id=\"" + elementId + "\" value=\"" + element.value + "\">";
        }
        return "<textarea class=\"" + element.className + " editable-input\" name=\"" + element.id + "\" data-editable-input=\"" + elementId + "\"\n                    data-editable-input-id=\"" + elementId + "\">" + element.value + "</textarea>";
    };
    EditableComponents.InitEditableInner = function (elementId) {
        var _this = this;
        var element = document.querySelectorAll("[data-editable-input=\"" + elementId + "\"]")[0];
        element.addEventListener("click", function (x) { return _this.OnInputClickHandler(x.target); }, false);
        var btnCheck = document.querySelectorAll("[data-editable-check=\"" + elementId + "\"]")[0];
        btnCheck.addEventListener("click", (function () { this.OnBtnCheckClickHandler(elementId); }).bind(this));
        var btnBack = document.querySelectorAll("[data-editable-cancel=\"" + elementId + "\"]")[0];
        btnBack.addEventListener("click", (function () { this.OnBtnCancelClickHandler(elementId); }).bind(this));
        var backDrop = document.querySelectorAll("[data-editable-backdrop=\"" + elementId + "\"]")[0];
        backDrop.addEventListener("click", (function () { this.BackDropClickHandler(elementId); }).bind(this));
    };
    EditableComponents.CheckValueChanged = function (elementId) {
        var input = document.querySelectorAll("[data-editable-input=\"" + elementId + "\"]")[0];
        var record = EditableComponents.Editables.filter(function (x) { return x.ElementId === elementId; })[0];
        var newValue = input.value;
        if (record.Value !== newValue) {
            if (record.OnValueChangedHandler) {
                record.OnValueChangedHandler(newValue);
            }
            record.Value = newValue;
            EditableComponents.Editables = this.Editables.filter(function (x) { return x.ElementId !== elementId; });
            EditableComponents.Editables.push(record);
        }
    };
    EditableComponents.BackDropClickHandler = function (elementId) {
        var form = document.querySelectorAll("[data-editable-form=\"" + elementId + "\"]")[0];
        form.classList.remove("editable--active");
        var input = document.querySelectorAll("[data-editable-input=\"" + elementId + "\"]")[0];
        input.removeAttribute("editable");
        EditableComponents.CheckValueChanged(elementId);
    };
    EditableComponents.OnInputClickHandler = function (x) {
        var elementId = x.getAttribute("data-editable-input-id");
        var input = document.querySelectorAll("[data-editable-input=\"" + elementId + "\"]")[0];
        input.setAttribute("editable", "editable");
        var form = document.querySelectorAll("[data-editable-form=\"" + elementId + "\"]")[0];
        form.classList.add("editable--active");
    };
    EditableComponents.OnBtnCheckClickHandler = function (elementId) {
        var input = document.querySelectorAll("[data-editable-input=\"" + elementId + "\"]")[0];
        input.removeAttribute("editable");
        var form = document.querySelectorAll("[data-editable-form=\"" + elementId + "\"]")[0];
        form.classList.remove("editable--active");
        EditableComponents.CheckValueChanged(elementId);
    };
    EditableComponents.OnBtnCancelClickHandler = function (elementId) {
        var input = document.querySelectorAll("[data-editable-input=\"" + elementId + "\"]")[0];
        input.removeAttribute("editable");
        var form = document.querySelectorAll("[data-editable-form=\"" + elementId + "\"]")[0];
        form.classList.remove("editable--active");
        var record = EditableComponents.Editables.find(function (x) { return x.ElementId === elementId; });
        input.value = record.Value;
    };
    EditableComponents.Editables = [];
    return EditableComponents;
}());
