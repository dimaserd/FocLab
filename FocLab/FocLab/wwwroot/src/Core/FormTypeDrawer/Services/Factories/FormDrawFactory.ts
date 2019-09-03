class FormDrawFactory {

    static DictionaryImplementations = new Dictionary<Func<GenerateGenericUserInterfaceModel, IFormDraw>>([
        { key: "Default", value: x => new FormDrawImplementation(x) },
        { key: "Tab", value: x => new TabFormDrawImplementation(x) }
    ]);


    static GetImplementation(buildModel: GenerateGenericUserInterfaceModel, key: string): IFormDraw {

        var func = FormDrawFactory.DictionaryImplementations.getByKey(key);

        if (func == null) {
            return new FormDrawImplementation(buildModel);
        }

        return func(buildModel);
    }
}