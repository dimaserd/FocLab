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
    InitHandlers(): void;
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
