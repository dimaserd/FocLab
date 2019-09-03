declare class TryForm {
    static _genericInterfaces: Array<GenerateGenericUserInterfaceModel>;
    static UnWrapModel(model: GenerateGenericUserInterfaceModel, drawer: FormTypeDrawer): string;
    static ThrowError(mes: string): void;
    static GetForms(): void;
    /**
     * Получить объект данных с первой попавшейся формы на странице
     */
    static GetDataForFormByModelPrefix(modelPrefix: string): object;
    /**
     * Получить объект данных с первой попавшейся формы на странице
     */
    static GetDataForFirstForm(): object;
    /**
     * Получить данные с формы приведенные к описанному типу данных
     * @param buildModel Тип данных
     */
    static GetDataForForm(buildModel: GenerateGenericUserInterfaceModel): object;
    static AddBuildModel(buildModel: GenerateGenericUserInterfaceModel): void;
    static GetForm(elem: Element): void;
}
