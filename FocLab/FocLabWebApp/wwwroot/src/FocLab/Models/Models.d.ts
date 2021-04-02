interface UserModelBase {
    Id: string;
    Name: string;
    Email: string;
}
interface ChemistryReagentNameAndIdModel {
    Id: string;
    Name: string;
}
interface Chemistry_Substance {
    Name: string;
    Massa: string;
    MolarMassa: string;
    Koef: string;
}
interface ChemistryTaskExperimentSimpleModel {
    Id: string;
    Title: string;
    Performer: UserModelBase;
    CreationDate: Date;
    Deleted: boolean;
}
declare enum ChemistryTaskDbFileType {
    ReactionSchemaImage,
    File1,
    File2,
    File3,
    File4
}
interface ChemistryTaskModel {
    Id: string;
    Title: string;
    DeadLineDate: Date;
    IsPerformed: boolean;
    IsPerformedInTime: boolean;
    SubstanceCounter: Chemistry_SubstanceCounter;
    PerformedDate: Date;
    CreationDate: Date;
    ReactionSchemaImage: ChemistryTaskFileModel;
    HasReactionSchemaImage: boolean;
    AdminUser: UserModelBase;
    PerformerUser: UserModelBase;
    ChemistryMethodFile: ChemistryMethodFileModel;
    Files: Array<ChemistryTaskFileModel>;
    Experiments: Array<ChemistryTaskExperimentSimpleModel>;
    Reagents: Array<ChemistryTaskReagentModel>;
    AdminQuantity: string;
    AdminQuality: string;
    PerformerQuantity: string;
    PerformerQuality: string;
    PerformerText: string;
    SubstanceCounterJson: string;
    Deleted: boolean;
}
interface ChemistryTaskReagentModel {
    Reagent: ChemistryReagentNameAndIdModel;
    TakenQuantity: number;
    ReturnedQuantity: number;
}
interface ChemistryTaskFileModel {
    FileId: number;
    Type: ChemistryTaskDbFileType;
}
interface ChemistryMethodFileModel {
    Id: string;
    Name: string;
    FileId: number;
    CreationDate: Date;
}
interface ChemistryTaskFileModel {
    FileId: number;
    Type: ChemistryTaskDbFileType;
}
interface Chemistry_SubstanceCounter {
    Etalon: Chemistry_Substance;
    Substances: Array<Chemistry_Substance>;
}
