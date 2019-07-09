class SubstanceCounter {
    Etalon: Substance;
    Substances: Array<Substance>;
    private prefix: string;
    constructor(substanceObj: SubstancesObject) {
        console.log("SubstanceCounter.constructor", substanceObj);
        this.prefix = "";
        //this.setProperties();
        this.Substances = [];
        if (substanceObj != null && substanceObj != undefined) {
            console.log("SubstanceCounter.constructor", "Попадание в условие");
            this.Etalon = substanceObj.Etalon;
            this.Substances = substanceObj.Substances;
        }
        this.DrawTable();
    }
    ClearTable(): void {
        document.getElementById(`${this.prefix}.tbodySubstances`).innerHTML = "";
    }
    DrawEtalon(): void {
        var tr = document.createElement("tr");
        var html = `
                <td>
                    <input class="form-control etalon-field" data-prefix="${this.prefix}" name="${this.prefix}.etalon.Name" type="text" value="">
                </td>

                <td>
                    <input class="form-control etalon-field" data-prefix="${this.prefix}" name="${this.prefix}.etalon.Massa" type="number" value="">
                </td>

                <td>
                    <input class="form-control etalon-field" data-prefix="${this.prefix}" name="${this.prefix}.etalon.MolarMassa" type="number" value="">
                </td>

                <td>
                    <input class="form-control etalon-field" data-prefix="${this.prefix}" name="${this.prefix}.etalon.Koef" type="number" value="">
                </td>

                <td>

                </td>`;
        tr.innerHTML = html;
        document.getElementById(`${this.prefix}.tbodySubstances`).appendChild(tr);
    }
    DrawSubstance(count: number): void {
        var tr = document.createElement("tr");
        var html = `
                    <td>
                        <input class="form-control substance-name" name="${this.prefix}.Name[${count}]" data-count="${count}" type="text" value="">
                    </td>

                    <td>
                        <input class="form-control substance-massa" name="${this.prefix}.Massa[${count}]" data-count="${count}" data-prefix="${this.prefix}" type="number" value="">
                    </td>

                    <td>
                        <input class="form-control substance-mollar-massa" data-count="${count}" data-prefix="${this.prefix}" name="${this.prefix}.MolarMassa[${count}]" type="number" value="">
                    </td>

                    <td>
                        <input class="form-control substance-koef" data-count="${count}" data-prefix="${this.prefix}" name="${this.prefix}.Koef[${count}]" type="number" value="">
                    </td>

                    <td>
                        <button class="btn btn-danger substance-remove" data-count="${count}" data-prefix="${this.prefix}">
                            <i class="fas fa-trash">

                            </i>
                        </button>
                    </td>`;
        tr.innerHTML = html;
        document.getElementById(`${this.prefix}.tbodySubstances`).appendChild(tr);
    }
    DrawTable(): void {
        if (this.Etalon == null) {
            ToastrWorker.ShowError("Не указано эталонное вещество");
            this.Etalon = { Name: "", Koef: 1, Massa: 1, MolarMassa: 1 };
        }
        this.DrawEtalon();
        this.setValueToName(`${this.prefix}.etalon.Name`, this.Etalon.Name);
        this.setValueToName(`${this.prefix}.etalon.Massa`, this.Etalon.Massa.toString());
        this.setValueToName(`${this.prefix}.etalon.MolarMassa`, this.Etalon.MolarMassa.toString());
        this.setValueToName(`${this.prefix}.etalon.Koef`, this.Etalon.Koef.toString());
        for (let i = 0; i < this.Substances.length; i++) {
            this.DrawSubstance(i);
            this.setValuesToSubstance(i);
        }
        SubstanceEventSetter.InitHandlers();
    }
    AddSubstance(): void {
        var count = SubstanceStaticHandlers.substance.Substances.length;
        this.DrawSubstance(count);
        this.Substances.push({
            Koef: 1,
            Massa: 1,
            MolarMassa: 1,
            Name: ""
        });
    }
    RecalcSubstances(): void {
        for (let i = 0; i < this.Substances.length; i++) {
            SubstanceStaticHandlers.ChangeMassa(i, this.prefix);
        }
    }
    setValuesToSubstance(count) {

        var sub = this.Substances[count];

        this.setValueToName(`${this.prefix}.Name[${count}]`, sub.Name);
        this.setValueToName(`${this.prefix}.Massa[${count}]`, sub.Massa != null ? sub.Massa.toString() : "");
        this.setValueToName(`${this.prefix}.MolarMassa[${count}]`, sub.MolarMassa != null ? sub.MolarMassa.toString() : "");
        this.setValueToName(`${this.prefix}.Koef[${count}]`, sub.Koef != null ? sub.Koef.toString() : "");
    }
    setValueToName(name: string, value: string): void {
        (document.getElementsByName(name)[0] as HTMLInputElement).value = value;
    }
    setProperties(): void {
        this.Etalon = {
            Name: (document.getElementsByName(`${this.prefix}.etalon.Name`)[0] as HTMLInputElement).value,
            Massa: +(document.getElementsByName(`${this.prefix}.etalon.Massa`)[0] as HTMLInputElement).value,
            MolarMassa: +(document.getElementsByName(`${this.prefix}.etalon.MolarMassa`)[0] as HTMLInputElement).value,
            Koef: +(document.getElementsByName(`${this.prefix}.etalon.Koef`)[0] as HTMLInputElement).value
        };
        this.RecalcSubstances();
    }
    getJSON() {
        return { Etalon: this.Etalon, Substances: this.Substances };
    }
    getTotalKoef(): number {
        return (this.Etalon.Massa / this.Etalon.MolarMassa / this.Etalon.Koef);
    }
}
