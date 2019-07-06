interface Substance {
    Name: string;
    Massa: number;
    MolarMassa: number;
    Koef: number;
}
interface SubstancesObject {
    Etalon: Substance;
    Substances: Array<Substance>;
}
declare class SubstanceCounter {
    Etalon: Substance;
    Substances: Array<Substance>;
    private prefix;
    constructor(substanceObj: SubstancesObject);
    ClearTable(): void;
    DrawEtalon(): void;
    DrawSubstance(count: number): void;
    DrawTable(): void;
    AddSubstance(): void;
    RecalcSubstances(): void;
    setValuesToSubstance(count: any): void;
    setValueToName(name: string, value: string): void;
    setProperties(): void;
    getJSON(): {
        Etalon: Substance;
        Substances: Substance[];
    };
    getTotalKoef(): number;
}
declare var substance: SubstanceCounter;
declare class SubstanceStaticHandlers {
    static RemoveSubstanceHandler(count: number): void;
    static EtalonChangedHandler(prefix: any): void;
    static ChangeMassa(count: number, prefix: string): void;
    static WriteMassa(value: number, count: number): void;
    static ChangeName(count: number, value: string): void;
    static AddSubstance(prefix: string): void;
}
