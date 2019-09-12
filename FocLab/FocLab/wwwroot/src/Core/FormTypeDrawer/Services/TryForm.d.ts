declare class TryForm {
    static _genericInterfaces: Array<GenerateGenericUserInterfaceModel>;
    static _beforeDrawInterfaceHandlers: Dictionary<Func<GenerateGenericUserInterfaceModel, GenerateGenericUserInterfaceModel>>;
    static _afterDrawInterfaceHandlers: Dictionary<Action<GenerateGenericUserInterfaceModel>>;
    static UnWrapModel(model: GenerateGenericUserInterfaceModel, drawer: FormTypeDrawer): string;
    static ThrowError(mes: string): void;
    static SetBeforeDrawingHandler(modelPrefix: string, func: Func<GenerateGenericUserInterfaceModel, GenerateGenericUserInterfaceModel>): void;
    static SetAfterDrawingHandler(modelPrefix: string, func: Action<GenerateGenericUserInterfaceModel>): void;
    static GetForms(): void;
    static GetDataForFormByModelPrefix(modelPrefix: string): object;
    static GetDataForFirstForm(): object;
    static GetDataForForm(buildModel: GenerateGenericUserInterfaceModel): object;
    static AddBuildModel(buildModel: GenerateGenericUserInterfaceModel): void;
    static GetForm(elem: Element): void;
}
