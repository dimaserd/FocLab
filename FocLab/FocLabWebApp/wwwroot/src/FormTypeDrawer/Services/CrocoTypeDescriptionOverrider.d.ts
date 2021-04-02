declare class CrocoTypeDescriptionOverrider {
    static FindUserInterfacePropBlockByPropertyName(model: GenerateGenericUserInterfaceModel, propertyName: string): UserInterfaceBlock;
    static SetUserInterfaceTypeForProperty(model: GenerateGenericUserInterfaceModel, propertyName: string, interfaceType: UserInterfaceType): void;
    static RemoveProperty(model: GenerateGenericUserInterfaceModel, propertyName: string): void;
    static SetLabelText(model: GenerateGenericUserInterfaceModel, propertyName: string, labelText: string): void;
    static SetHidden(model: GenerateGenericUserInterfaceModel, propertyName: string): void;
    static SetTextArea(model: GenerateGenericUserInterfaceModel, propertyName: string): void;
    static SetDropDownList(model: GenerateGenericUserInterfaceModel, propertyName: string, selectList: SelectListItem[]): void;
}
