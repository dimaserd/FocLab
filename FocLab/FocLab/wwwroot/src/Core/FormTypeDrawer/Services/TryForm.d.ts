declare class TryForm {
    static _genericInterfaces: Array<GenerateGenericUserInterfaceModel>;
    static UnWrapModel(model: GenerateGenericUserInterfaceModel, drawer: FormTypeDrawer): string;
    static ThrowError(mes: string): void;
    static GetForms(): void;
    static GetDataForFormByModelPrefix(modelPrefix: string): object;
    static GetDataForFirstForm(): object;
    static GetDataForForm(buildModel: GenerateGenericUserInterfaceModel): object;
    static AddBuildModel(buildModel: GenerateGenericUserInterfaceModel): void;
    static GetForm(elem: Element): void;
}
