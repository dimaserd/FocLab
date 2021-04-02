interface ScheduleWorkerFilter {
    UserIds: Array<string>
}

class ScheduleWorker_Resx {
    SelectUser: string = "Выберите пользователя";
    UserNotFound: string = "Пользователь не найден.";
}

class ScheduleWorker {

    static filter: ScheduleWorkerFilter
    static Users: Array<any> = [];
    static Resources: ScheduleWorker_Resx = new ScheduleWorker_Resx();

    static Constructor(filter: ScheduleWorkerFilter) {
        ScheduleWorker.filter = filter;
        ScheduleWorker.SetUsersSelect();
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

        CrocoAppCore.Application.Requester.Post("/Api/User/Get", { Count: null, OffSet: 0 },
            (x : any) => {
                
                ScheduleWorker.Users = x.List as Array<ApplicationUserModel>;

                $("#usersSelect").select2({
                    placeholder: ScheduleWorker.Resources.SelectUser,

                    language: {
                        "noResults": function () {
                            return ScheduleWorker.Resources.UserNotFound;
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

                CrocoAppCore.Application.FormDataHelper.FillDataByPrefix({
                    UserIds: ScheduleWorker.filter.UserIds
                }, "filter.");

                $("#usersSelect").val(this.filter.UserIds).trigger('change.select2');
                $('.select2-selection__rendered img').addClass('m--img-rounded m--marginless m--img-centered');

            }, null);
    }
}