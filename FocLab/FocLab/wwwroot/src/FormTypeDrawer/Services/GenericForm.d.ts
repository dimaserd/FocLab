interface IGenericFormOptions {
    FormDrawFactory: FormDrawFactory;
}
declare class GenericForm {
    _formDrawFactory: FormDrawFactory;
    constructor(opts: IGenericFormOptions);
    _genericInterfaces: Array<RenderGenericInterface>;
    static UnWrapModel(model: GenerateGenericUserInterfaceModel, drawer: FormTypeDrawer): string;
    static ThrowError(mes: string): void;
    DrawForms(): void;
    FindFormAndSave(elem: Element): void;
    DrawForm(renderModel: RenderGenericInterface): void;
}
