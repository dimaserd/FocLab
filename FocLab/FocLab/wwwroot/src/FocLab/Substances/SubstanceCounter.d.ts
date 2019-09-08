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
