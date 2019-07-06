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


class SubstanceCounter {

    Etalon: Substance; 
    Substances: Array<Substance>;
    private prefix: string;

    constructor(substanceObj: SubstancesObject) {

        this.prefix = "";
        //this.setProperties();
        this.Substances = [];

        if (substanceObj != null && substanceObj != undefined) {
            this.Etalon = substanceObj.Etalon;
            this.Substances = substanceObj.Substances;

        }
        
        this.DrawTable();
    }

    
        ClearTable() : void {
            document.getElementById(`${this.prefix}.tbodySubstances`).innerHTML = "";
        }

        DrawEtalon() : void {
            var tr = document.createElement("tr");

            var html = `
                <td>
                    <input class="form-control" name="${this.prefix}.etalon.Name" onchange="EtalonChangedHandler('${this.prefix}')" oninput="EtalonChangedHandler('${this.prefix}')" type="text" value="">
                </td>

                <td>
                    <input class="form-control" name="${this.prefix}.etalon.Massa" onchange="EtalonChangedHandler('${this.prefix}')" oninput="EtalonChangedHandler('${this.prefix}')" type="number" value="">
                </td>

                <td>
                    <input class="form-control" name="${this.prefix}.etalon.MolarMassa" onchange="EtalonChangedHandler('${this.prefix}')" oninput="EtalonChangedHandler('${this.prefix}')" type="number" value="">
                </td>

                <td>
                    <input class="form-control" name="${this.prefix}.etalon.Koef" onchange="EtalonChangedHandler('${this.prefix}')" oninput="EtalonChangedHandler('${this.prefix}')" type="number" value="">
                </td>

                <td>

                </td>`;

            tr.innerHTML = html;

            document.getElementById(`${this.prefix}.tbodySubstances`).appendChild(tr);
        }

        DrawSubstance(count : number) : void {
            var tr = document.createElement("tr");

            var html =
                `
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

            $(".substance-name").on("change", () => {
                var count = +$(this).data("count");
                SubstanceStaticHandlers.ChangeName(count, $(this).val().toString());
            });

            $(".substance-massa").on("change", () => {
                var count = +$(this).data("count");
                SubstanceStaticHandlers.WriteMassa(+$(this).val(), count);
            });

            for (let elem in [".substance-mollar-massa", ".substance-koef"]) {
                $(elem).on("change", () => {
                    var count = +$(this).data("count");
                    var prefix = $(this).data("prefix").toString();
                    SubstanceStaticHandlers.ChangeMassa(count, prefix);
                }).on("input", () => {
                    var count = +$(this).data("count");
                    var prefix = $(this).data("prefix").toString();
                    SubstanceStaticHandlers.ChangeMassa(count, prefix);
                });
            }

            $(".substance-remove").on("click", () => {
                var count = +$(this).data("count");
                SubstanceStaticHandlers.RemoveSubstanceHandler(count);
            })
        }

        DrawTable() : void {
            if (this.Etalon == null) {
                alert("Не указано эталонное вещество");
                this.Etalon = {
                    Name: "",
                    Koef: 1,
                    Massa: 1,
                    MolarMassa: 1
                };
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
        }
    



    AddSubstance() : void {

        var count = window['substance'].Substances.length;

        this.DrawSubstance(count);

        this.Substances.push({
            Koef: 1,
            Massa: 1,
            MolarMassa: 1,
            Name: ""
        });
    }

    RecalcSubstances() : void {

        for (let i = 0; i < this.Substances.length; i++) {
            SubstanceStaticHandlers.ChangeMassa(i, this.prefix);
        }
    }

    setValuesToSubstance(count) {
        this.setValueToName(`${this.prefix}.Name[${count}]`, this.Substances[count].Name);
        this.setValueToName(`${this.prefix}.Massa[${count}]`, this.Substances[count].Massa.toString());
        this.setValueToName(`${this.prefix}.MolarMassa[${count}]`, this.Substances[count].MolarMassa.toString());
        this.setValueToName(`${this.prefix}.Koef[${count}]`, this.Substances[count].Koef.toString());
    }

    setValueToName(name: string, value: string): void {
        (document.getElementsByName(name)[0] as HTMLInputElement).value = value;
    }

    setProperties() : void {
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

    getTotalKoef() : number {
        return (this.Etalon.Massa / this.Etalon.MolarMassa / this.Etalon.Koef);
    }
}

var substance = new SubstanceCounter(window['substanceObject']);

class SubstanceStaticHandlers {
    static RemoveSubstanceHandler(count: number) {

        console.log("RemoveSubstanceHandler", count);

        var substanceToDelete = substance.Substances[count];

        substance.Substances = substance.Substances.filter(x => x != substanceToDelete);;
        substance.ClearTable();
        substance.DrawTable();
        TaskStaticHandlers.UpdateBtnClickHandler();
    }

    static EtalonChangedHandler(prefix) {

        if (prefix == "") {
            substance.setProperties();
            console.log(substance);
        }
    }


    static ChangeMassa(count: number, prefix: string) {

        var molarMassa = +(document.getElementsByName(`${prefix}.MolarMassa[${count}]`)[0] as HTMLInputElement).value;

        var koef = +(document.getElementsByName(`${prefix}.Koef[${count}]`)[0] as HTMLInputElement).value;

        var massa = substance.getTotalKoef() * koef * molarMassa;

        massa = +massa.toFixed(2);
        substance.Substances[count].MolarMassa = molarMassa;
        substance.Substances[count].Massa = massa;
        substance.Substances[count].Koef = koef;

        (document.getElementsByName(`${prefix}.Massa[${count}]`)[0] as HTMLInputElement).value = massa.toFixed(2);
    }

    static WriteMassa(value: number, count: number) {
        substance.Substances[count].Massa = value;
    }

    static ChangeName(count: number, value: string) {
        substance.Substances[count].Name = value;
    }

    static AddSubstance(prefix: string) {
        if (prefix === "") {
            substance.AddSubstance();
        }
    }
}