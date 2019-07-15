var ScheduleWorker = /** @class */ (function () {
    function ScheduleWorker() {
    }
    ScheduleWorker.Constructor = function (filter) {
        ScheduleWorker.filter = filter;
        ScheduleWorker.SetUsersSelect();
        ScheduleWorker.Users = [];
    };
    ScheduleWorker.formatStateSelection = function (state) {
        if (!state.id) {
            return state.text;
        }
        var img = "";
        if (state.avatarId) {
            var baseUrl = "/FileCopies/Images/Icon/" + state.avatarId + ".jpg";
            img = "<img src=\"" + baseUrl + "\" class=\"img-max-50\" />";
        }
        var $state = $("<span>" + img + " " + state.text + "<span>&nbsp;</span></span>");
        return $state;
    };
    ScheduleWorker.formatStateResult = function (state) {
        if (!state.id) {
            return state.text;
        }
        var img = "";
        if (state.avatarId) {
            var baseUrl = "/FileCopies/Images/Icon/" + state.avatarId + ".jpg";
            img = "<img src=\"" + baseUrl + "\" class=\"img-max-50\" />";
        }
        var $state = $("<span>" + img + " " + state.text + "<span>&nbsp;</span></span>");
        return $state;
    };
    ;
    ScheduleWorker.SetUsersSelect = function () {
        var _this = this;
        Requester.SendAjaxPost("/Api/User/Get", { Count: null, OffSet: 0 }, function (x) {
            console.log("/Api/User/Get", x);
            ScheduleWorker.Users = x.List;
            $(".usersSelect").select2({
                placeholder: "Выберите пользователя",
                language: {
                    "noResults": function () {
                        return "Пользователь не найден.";
                    }
                },
                data: x.List.map(function (t) { return ({
                    id: t.Id,
                    text: t.Name + " " + t.Email,
                    avatarId: t.AvatarFileId
                }); }),
                templateSelection: ScheduleWorker.formatStateSelection,
                templateResult: ScheduleWorker.formatStateResult,
                escapeMarkup: function (markup) {
                    return markup;
                }
            });
            FormDataHelper.FillDataByPrefix({
                UserIds: ScheduleWorker.filter.UserIds
            }, "filter.");
            $("#usersSelect").val(_this.filter.UserIds).trigger('change.select2');
            $('.select2-selection__rendered img').addClass('m--img-rounded m--marginless m--img-centered');
        }, null, false);
    };
    return ScheduleWorker;
}());
