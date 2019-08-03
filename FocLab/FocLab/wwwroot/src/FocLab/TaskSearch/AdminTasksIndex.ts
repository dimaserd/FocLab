class TaskFilterMethods {
    static ApplyTextFilter(tasks: Array<ChemistryTaskModel>, q: string): Array<ChemistryTaskModel> {

        q = q.toLowerCase();

        var result = tasks.filter(function (x) {

            return (x.Title != null && x.Title.toLowerCase().includes(q));
        });

        return result;
    }

    static ApplyStatusFilter(tasks: Array<ChemistryTaskModel>, isPerformed: boolean): Array<ChemistryTaskModel> {

        if (isPerformed == null) {
            return tasks;
        }

        var result = tasks.filter(x => x.IsPerformed == isPerformed);

        return result;
    } 

    static ParseBoolean(s: string): boolean {
        if (s === "" || s === null) {
            return null;
        }

        return s.toLowerCase() === "true";
    }
}



