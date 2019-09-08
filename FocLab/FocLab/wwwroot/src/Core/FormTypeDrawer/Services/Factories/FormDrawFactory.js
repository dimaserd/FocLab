var FormDrawFactory = (function () {
    function FormDrawFactory() {
    }
    FormDrawFactory.GetImplementation = function (buildModel, key) {
        var func = FormDrawFactory.DictionaryImplementations.getByKey(key);
        if (func == null) {
            return new FormDrawImplementation(buildModel);
        }
        return func(buildModel);
    };
    FormDrawFactory.DictionaryImplementations = new Dictionary([
        { key: "Default", value: function (x) { return new FormDrawImplementation(x); } },
        { key: "Tab", value: function (x) { return new TabFormDrawImplementation(x); } }
    ]);
    return FormDrawFactory;
}());
