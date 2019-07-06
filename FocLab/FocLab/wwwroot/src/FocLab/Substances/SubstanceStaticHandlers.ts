class SubstanceStaticHandlers {
    static substance: SubstanceCounter;
    
    static RemoveSubstanceHandler(count: number) {
        console.log("RemoveSubstanceHandler", count);
        var substanceToDelete = SubstanceStaticHandlers.substance.Substances[count];
        SubstanceStaticHandlers.substance.Substances = SubstanceStaticHandlers.substance.Substances.filter(x => x != substanceToDelete);
        ;
        SubstanceStaticHandlers.substance.ClearTable();
        SubstanceStaticHandlers.substance.DrawTable();
        TaskStaticHandlers.UpdateBtnClickHandler();
    }
    static EtalonChangedHandler(prefix) {
        if (prefix == "") {
            SubstanceStaticHandlers.substance.setProperties();
            console.log(SubstanceStaticHandlers.substance);
        }
    }
    static ChangeMassa(count: number, prefix: string) {
        var molarMassa = +(document.getElementsByName(`${prefix}.MolarMassa[${count}]`)[0] as HTMLInputElement).value;
        var koef = +(document.getElementsByName(`${prefix}.Koef[${count}]`)[0] as HTMLInputElement).value;
        var massa = SubstanceStaticHandlers.substance.getTotalKoef() * koef * molarMassa;

        massa = +massa.toFixed(2);
        SubstanceStaticHandlers.substance.Substances[count].MolarMassa = molarMassa;
        SubstanceStaticHandlers.substance.Substances[count].Massa = massa;
        SubstanceStaticHandlers.substance.Substances[count].Koef = koef;
        (document.getElementsByName(`${prefix}.Massa[${count}]`)[0] as HTMLInputElement).value = massa.toFixed(2);
    }
    static WriteMassa(value: number, count: number) {
        SubstanceStaticHandlers.substance.Substances[count].Massa = value;
    }
    static ChangeName(count: number, value: string) {
        console.log("SubstanceStaticHandlers.ChangeName");
        SubstanceStaticHandlers.substance.Substances[count].Name = value;
    }
    static AddSubstance(prefix: string) {
        if (prefix === "") {
            SubstanceStaticHandlers.substance.AddSubstance();
        }
    }
}
