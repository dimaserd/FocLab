declare class SubstanceStaticHandlers {
    static substance: SubstanceCounter;
    static RemoveSubstanceHandler(count: number): void;
    static EtalonChangedHandler(prefix: any): void;
    static ChangeMassa(count: number, prefix: string): void;
    static WriteMassa(value: number, count: number): void;
    static ChangeName(count: number, value: string): void;
    static AddSubstance(prefix: string): void;
}
