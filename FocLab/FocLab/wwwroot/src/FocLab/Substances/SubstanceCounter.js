var SubstanceCounter = /** @class */ (function () {
    function SubstanceCounter(substanceObj) {
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
    SubstanceCounter.prototype.ClearTable = function () {
        document.getElementById(this.prefix + ".tbodySubstances").innerHTML = "";
    };
    SubstanceCounter.prototype.DrawEtalon = function () {
        var tr = document.createElement("tr");
        var html = "\n                <td>\n                    <input class=\"form-control etalon-field\" data-prefix=\"" + this.prefix + "\" name=\"" + this.prefix + ".etalon.Name\" type=\"text\" value=\"\">\n                </td>\n\n                <td>\n                    <input class=\"form-control etalon-field\" data-prefix=\"" + this.prefix + "\" name=\"" + this.prefix + ".etalon.Massa\" type=\"number\" value=\"\">\n                </td>\n\n                <td>\n                    <input class=\"form-control etalon-field\" data-prefix=\"" + this.prefix + "\" name=\"" + this.prefix + ".etalon.MolarMassa\" type=\"number\" value=\"\">\n                </td>\n\n                <td>\n                    <input class=\"form-control etalon-field\" data-prefix=\"" + this.prefix + "\" name=\"" + this.prefix + ".etalon.Koef\" type=\"number\" value=\"\">\n                </td>\n\n                <td>\n\n                </td>";
        tr.innerHTML = html;
        document.getElementById(this.prefix + ".tbodySubstances").appendChild(tr);
    };
    SubstanceCounter.prototype.DrawSubstance = function (count) {
        var tr = document.createElement("tr");
        var html = "\n                    <td>\n                        <input class=\"form-control substance-name\" name=\"" + this.prefix + ".Name[" + count + "]\" data-count=\"" + count + "\" type=\"text\" value=\"\">\n                    </td>\n\n                    <td>\n                        <input class=\"form-control substance-massa\" name=\"" + this.prefix + ".Massa[" + count + "]\" data-count=\"" + count + "\" data-prefix=\"" + this.prefix + "\" type=\"number\" value=\"\">\n                    </td>\n\n                    <td>\n                        <input class=\"form-control substance-mollar-massa\" data-count=\"" + count + "\" data-prefix=\"" + this.prefix + "\" name=\"" + this.prefix + ".MolarMassa[" + count + "]\" type=\"number\" value=\"\">\n                    </td>\n\n                    <td>\n                        <input class=\"form-control substance-koef\" data-count=\"" + count + "\" data-prefix=\"" + this.prefix + "\" name=\"" + this.prefix + ".Koef[" + count + "]\" type=\"number\" value=\"\">\n                    </td>\n\n                    <td>\n                        <button class=\"btn btn-danger substance-remove\" data-count=\"" + count + "\" data-prefix=\"" + this.prefix + "\">\n                            <i class=\"fas fa-trash\">\n\n                            </i>\n                        </button>\n                    </td>";
        tr.innerHTML = html;
        document.getElementById(this.prefix + ".tbodySubstances").appendChild(tr);
    };
    SubstanceCounter.prototype.DrawTable = function () {
        if (this.Etalon == null) {
            ToastrWorker.ShowError("Не указано эталонное вещество");
            this.Etalon = { Name: "", Koef: 1, Massa: 1, MolarMassa: 1 };
        }
        this.DrawEtalon();
        this.setValueToName(this.prefix + ".etalon.Name", this.Etalon.Name);
        this.setValueToName(this.prefix + ".etalon.Massa", this.Etalon.Massa.toString());
        this.setValueToName(this.prefix + ".etalon.MolarMassa", this.Etalon.MolarMassa.toString());
        this.setValueToName(this.prefix + ".etalon.Koef", this.Etalon.Koef.toString());
        for (var i = 0; i < this.Substances.length; i++) {
            this.DrawSubstance(i);
            this.setValuesToSubstance(i);
        }
        SubstanceEventSetter.InitHandlers();
    };
    SubstanceCounter.prototype.AddSubstance = function () {
        var count = SubstanceStaticHandlers.substance.Substances.length;
        this.DrawSubstance(count);
        this.Substances.push({
            Koef: 1,
            Massa: 1,
            MolarMassa: 1,
            Name: ""
        });
    };
    SubstanceCounter.prototype.RecalcSubstances = function () {
        for (var i = 0; i < this.Substances.length; i++) {
            SubstanceStaticHandlers.ChangeMassa(i, this.prefix);
        }
    };
    SubstanceCounter.prototype.setValuesToSubstance = function (count) {
        var sub = this.Substances[count];
        this.setValueToName(this.prefix + ".Name[" + count + "]", sub.Name);
        this.setValueToName(this.prefix + ".Massa[" + count + "]", sub.Massa != null ? sub.Massa.toString() : "");
        this.setValueToName(this.prefix + ".MolarMassa[" + count + "]", sub.MolarMassa != null ? sub.MolarMassa.toString() : "");
        this.setValueToName(this.prefix + ".Koef[" + count + "]", sub.Koef != null ? sub.Koef.toString() : "");
    };
    SubstanceCounter.prototype.setValueToName = function (name, value) {
        document.getElementsByName(name)[0].value = value;
    };
    SubstanceCounter.prototype.setProperties = function () {
        this.Etalon = {
            Name: document.getElementsByName(this.prefix + ".etalon.Name")[0].value,
            Massa: +document.getElementsByName(this.prefix + ".etalon.Massa")[0].value,
            MolarMassa: +document.getElementsByName(this.prefix + ".etalon.MolarMassa")[0].value,
            Koef: +document.getElementsByName(this.prefix + ".etalon.Koef")[0].value
        };
        this.RecalcSubstances();
    };
    SubstanceCounter.prototype.getJSON = function () {
        return { Etalon: this.Etalon, Substances: this.Substances };
    };
    SubstanceCounter.prototype.getTotalKoef = function () {
        return (this.Etalon.Massa / this.Etalon.MolarMassa / this.Etalon.Koef);
    };
    return SubstanceCounter;
}());