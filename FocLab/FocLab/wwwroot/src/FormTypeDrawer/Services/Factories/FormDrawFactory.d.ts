interface FormDrawFactoryOptions {
    DefaultImplementation: (t: GenerateGenericUserInterfaceModel) => IFormDraw;
    Implementations: Map<string, (t: GenerateGenericUserInterfaceModel) => IFormDraw>;
}
declare class FormDrawFactory {
    _defaultImplementation: (t: GenerateGenericUserInterfaceModel) => IFormDraw;
    _implementations: Map<string, (t: GenerateGenericUserInterfaceModel) => IFormDraw>;
    constructor(opts: FormDrawFactoryOptions);
    GetImplementation(buildModel: GenerateGenericUserInterfaceModel, key: string): IFormDraw;
}
