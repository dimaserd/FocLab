declare class FormDataHelper {
    static FillData(object: Object): void;
    static FillDataByPrefix(object: Object, prefix: string): void;
    static CollectData(object: object): object;
    static CollectDataByPrefix(object: object, prefix: string): object;
}
