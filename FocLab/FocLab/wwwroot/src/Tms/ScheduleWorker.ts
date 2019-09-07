interface ScheduleWorkerFilter {
    UserIds: Array<string>
}

class ScheduleWorker {

    static filter: ScheduleWorkerFilter
    static Users: Array<any>;

    static Constructor(filter: ScheduleWorkerFilter) {
        ScheduleWorker.filter = filter;
        ScheduleWorker.SetUsersSelect();
        ScheduleWorker.Users = [];
    }

    static formatStateSelection(state) {
        if (!state.id) {
            return state.text;
        }

        var img = "";

        let showAvatar = false;

        if (state.avatarId) {
            const baseUrl = `/FileCopies/Images/Icon/${state.avatarId}.jpg`;
            img = showAvatar? `<img src="${baseUrl}" class="img-max-50" />` : "";
        }


        const $state = $(
            `<span>${img} ${state.text}<span>&nbsp;</span></span>`
        );
        return $state;
    }

    static formatStateResult(state) {
        if (!state.id) {
            return state.text;
        }

        var img = "";

        let showAvatar = false;

        if (state.avatarId) {
            const baseUrl = `/FileCopies/Images/Icon/${state.avatarId}.jpg`;
            img = showAvatar? `<img src="${baseUrl}" class="img-max-50" />` : "";
        }


        const $state = $(
            `<span>${img} ${state.text}<span>&nbsp;</span></span>`
        );
        return $state;
    };

    static SetUsersSelect() {

        Requester.SendAjaxPost("/Api/User/Get", { Count: null, OffSet: 0 },
            x => {
                
                ScheduleWorker.Users = x.List;

                $("#usersSelect").select2({
                    placeholder: "Выберите пользователя",

                    language: {
                        "noResults": function () {
                            return "Пользователь не найден.";
                        }
                    },

                    data: x.List.map(t => ({
                        id: t.Id,
                        text: t.Email,
                        avatarId: t.AvatarFileId
                    })),

                    templateSelection: ScheduleWorker.formatStateSelection,
                    templateResult: ScheduleWorker.formatStateResult,

                    escapeMarkup: function (markup) {
                        return markup;
                    }
                });

                FormDataHelper.FillDataByPrefix({
                    UserIds: ScheduleWorker.filter.UserIds
                }, "filter.");

                $("#usersSelect").val(this.filter.UserIds).trigger('change.select2');
                $('.select2-selection__rendered img').addClass('m--img-rounded m--marginless m--img-centered');

            }, null, false);
    }
    
}
