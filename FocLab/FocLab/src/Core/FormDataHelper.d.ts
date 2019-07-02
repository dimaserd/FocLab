declare class FormDataHelper {
    static FillData: (object: Object) => void;
    static FillDataByPrefix: (object: Object, prefix: string) => void;
    static CollectData: (object: Object) => Object;
    static CollectDataByPrefix(object: Object, prefix: string): Object;
}
