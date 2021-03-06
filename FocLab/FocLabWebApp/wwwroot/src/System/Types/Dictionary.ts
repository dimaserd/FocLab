﻿interface IDictionary<T> {
    add(key: string, value: T): void;
    remove(key: string): void;
    containsKey(key: string): boolean;
    keys(): string[];
    values(): T[];
}

class Dictionary<T> implements IDictionary<T> {

    _keys: string[] = [];
    _values: T[] = [];

    constructor(init?: { key: string; value: T; }[]) {
        if (init) {
            for (var x = 0; x < init.length; x++) {
                this[init[x].key] = init[x].value;
                this._keys.push(init[x].key);
                this._values.push(init[x].value);
            }
        }
    }

    getByKey(key: string): T {
        var index = this._keys.indexOf(key, 0);

        if (index > 0) {
            return this._values[index];
        }

        return null;
    }

    add(key: string, value: T): void {

        if (this.containsKey(key)) {
            throw new DOMException(`Key ${key} is already exists`);
        }

        this[key] = value;
        this._keys.push(key);
        this._values.push(value);
    }

    remove(key: string) : void {
        var index = this._keys.indexOf(key, 0);
        this._keys.splice(index, 1);
        this._values.splice(index, 1);

        delete this[key];
    }

    keys(): string[] {
        return this._keys;
    }

    values(): T[] {
        return this._values;
    }

    containsKey(key: string): boolean {
        if (typeof this[key] === "undefined") {
            return false;
        }

        return true;
    }

    toLookup(): IDictionary<T> {
        return this;
    }
}