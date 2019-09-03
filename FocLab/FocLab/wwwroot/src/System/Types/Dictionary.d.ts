interface IDictionary<T> {
    add(key: string, value: T): void;
    remove(key: string): void;
    containsKey(key: string): boolean;
    keys(): string[];
    values(): T[];
}
declare class Dictionary<T> implements IDictionary<T> {
    _keys: string[];
    _values: T[];
    constructor(init?: {
        key: string;
        value: T;
    }[]);
    getByKey(key: string): T;
    add(key: string, value: T): void;
    remove(key: string): void;
    keys(): string[];
    values(): T[];
    containsKey(key: string): boolean;
    toLookup(): IDictionary<T>;
}
