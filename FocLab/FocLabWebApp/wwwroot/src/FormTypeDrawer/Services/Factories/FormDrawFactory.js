var FormDrawFactory = (function () {
    function FormDrawFactory(opts) {
        this._defaultImplementation = opts.DefaultImplementation;
        this._implementations = opts.Implementations;
    }
    FormDrawFactory.prototype.GetImplementation = function (buildModel, key) {
        var func = this._implementations.get(key);
        if (func == null) {
            return this._defaultImplementation(buildModel);
        }
        return func(buildModel);
    };
    return FormDrawFactory;
}());
