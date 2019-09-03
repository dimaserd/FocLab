declare class FormDrawFactory {
    static DictionaryImplementations: Dictionary<Func<GenerateGenericUserInterfaceModel, IFormDraw>>;
    static GetImplementation(buildModel: GenerateGenericUserInterfaceModel, key: string): IFormDraw;
}
