var FormTypeDrawerModelBuilder = (function () {
    function FormTypeDrawerModelBuilder(model) {
        this._model = model;
    }
    FormTypeDrawerModelBuilder.prototype.SetMultipleDropDownListFor = function (propertyName, selectListItems) {
        var block = this.GetPropertyBlockByName(propertyName);
        block.InterfaceType = UserInterfaceType.MultipleDropDownList;
        block.SelectList = selectListItems;
        this.ResetBlock(block);
        return this;
    };
    FormTypeDrawerModelBuilder.prototype.SetDropDownListFor = function (propertyName, selectListItems) {
        var block = this.GetPropertyBlockByName(propertyName);
        block.InterfaceType = UserInterfaceType.DropDownList;
        block.SelectList = selectListItems;
        this.ResetBlock(block);
        return this;
    };
    FormTypeDrawerModelBuilder.prototype.SetTextAreaFor = function (propertyName) {
        var block = this.GetPropertyBlockByName(propertyName);
        if (block.InterfaceType != UserInterfaceType.TextBox) {
            throw new Error("\u0422\u043E\u043B\u044C\u043A\u043E \u044D\u043B\u0435\u043C\u0435\u043D\u0442\u044B \u0441 \u0442\u0438\u043F\u043E\u043C " + UserInterfaceType.TextBox + " \u043C\u043E\u0436\u043D\u043E \u043F\u0435\u0440\u0435\u043A\u043B\u044E\u0447\u0430\u0442\u044C \u043D\u0430 " + UserInterfaceType.TextArea);
        }
        block.InterfaceType = UserInterfaceType.TextArea;
        this.ResetBlock(block);
        return this;
    };
    FormTypeDrawerModelBuilder.prototype.SetHiddenFor = function (propertyName) {
        var block = this.GetPropertyBlockByName(propertyName);
        block.InterfaceType = UserInterfaceType.Hidden;
        this.ResetBlock(block);
        return this;
    };
    FormTypeDrawerModelBuilder.prototype.ResetBlock = function (block) {
        var index = this._model.Blocks.findIndex(function (x) { return x.PropertyName == block.PropertyName; });
        this._model.Blocks[index] = block;
    };
    FormTypeDrawerModelBuilder.prototype.GetPropertyBlockByName = function (propertyName) {
        var block = this._model.Blocks.find(function (x) { return x.PropertyName == propertyName; });
        if (block == null) {
            throw new Error("\u0411\u043B\u043E\u043A \u043D\u0435 \u043D\u0430\u0439\u0434\u0435\u043D \u043F\u043E \u0443\u043A\u0430\u0437\u0430\u043D\u043D\u043E\u043C\u0443 \u0438\u043C\u0435\u043D\u0438 " + propertyName);
        }
        return block;
    };
    return FormTypeDrawerModelBuilder;
}());
