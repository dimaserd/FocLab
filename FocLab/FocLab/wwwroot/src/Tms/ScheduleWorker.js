class ScheduleWorker {
    
    constructor(filter) {
        this.filter = filter;
        this.setHandlers();
        this.SetUsersSelect();
        this.Users = [];
    }

    setHandlers() {
        this.SetUsersSelect = function () {
            
            Requester.SendAjaxPost("/Api/User/Get",
                { Count: null, OffSet: 0 },
                (x => {
                    console.log("/Api/User/Get", x);

                    this.Users = x.List;

                    $(".usersSelect").select2({
                        placeholder: "Выберите пользователя",

                        language: {
                            "noResults": function () {
                                return "Пользователь не найден.";
                            }
                        },

                        data: x.List.map(t => ({
                            id: t.Id,
                            text: `${t.Name} ${t.Email}`,
                            avatarId: t.AvatarFileId
                        })),

                        templateSelection: formatStateSelection,
                        templateResult: formatStateResult,

                        escapeMarkup: function (markup) {
                            return markup;
                        }
                    });

                    FormDataHelper.FillDataByPrefix("filter.", {
                        UserIds: this.filter.UserIds
                    });

                    $("#usersSelect").val(this.filter.UserIds).trigger('change.select2');
                    $('.select2-selection__rendered img').addClass('m--img-rounded m--marginless m--img-centered');

                }).bind(this));
        }
    }
}
