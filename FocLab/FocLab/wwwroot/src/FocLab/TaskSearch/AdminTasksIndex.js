var TaskFilterMethods = /** @class */ (function () {
    function TaskFilterMethods() {
    }
    TaskFilterMethods.ApplyTextFilter = function (tasks, q) {
        q = q.toLowerCase();
        var result = tasks.filter(function (x) {
            return (x.Title != null && x.Title.toLowerCase().includes(q));
        });
        return result;
    };
    TaskFilterMethods.ApplyStatusFilter = function (tasks, isPerformed) {
        if (isPerformed == null) {
            return tasks;
        }
        var result = tasks.filter(function (x) { return x.IsPerformed == isPerformed; });
        return result;
    };
    TaskFilterMethods.ParseBoolean = function (s) {
        if (s === "" || s === null) {
            return null;
        }
        return s.toLowerCase() === "true";
    };
    return TaskFilterMethods;
}());
