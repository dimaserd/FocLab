class TaskModalWorker {

    public static ShowDayTaskModal(task: DayTaskModel) {
        TaskModalWorker.InitTask(task, AccountWorker.User.Id);

        FormDataHelper.FillDataByPrefix(task, "task.");

        Utils.SetDatePicker("input[name='task.TaskDate']");

        ModalWorker.ShowModal("dayTaskModal");
    }

    public static InitTask = function (task: DayTaskModel, accountId: string) {

        task.TaskDate = moment(new Date(task.TaskDate)).format("DD/MM/YYYY");

        TaskModalWorker.ClearContent();

        document.getElementById("dayTaskModalTitle").innerHTML = task.TaskTitle;

        let avatar: string = ColorAvatarInitor.InitColorForAvatar(task);

        document.getElementById("Author").innerHTML = `<a class="media-left tms-profile-link" href="#" data-task-author-id="${task.Author.Id}">${avatar}</a>
                <a  href="#" data-task-author-id="${task.Author.Id}" class="tms-profile-link text-semibold media-heading box-inline ml-1 mb-1">
                    ${task.Author.Name} ${task.Author.Email}
                </a>`;

        (<HTMLInputElement>document.getElementsByName('DayTaskId')[0]).value = task.Id;

        $("#usersSelect1").val(task.AssigneeUser.Id).trigger('change.select2');

        TaskModalWorker.DrawComments("Comments", accountId, task);
        $("#usersSelect1").select2({
            width: '100%'
        });
    }

    public static DrawComments = function (divId: string, userId: string, task: DayTaskModel) {
        TaskModalWorker.ClearContent();

        let avatar: string = ColorAvatarInitor.InitColorForAvatar(task);

        let html: string = "<div>";
        for (var comment in task.Comments) {
            html += `
          <div class="media-block">
            <div class="media-body">
                <div class="form-group m-form__group row m--margin-top-10 d-flex justify-content-between align-items-center">
                        <div>
                            <a href="#" class="btn-link btn cursor-pointer tms-profile-link" data-task-author-id="${task.Author.Id}">
                                ${avatar}
                            </a>
                            <a href="#" data-task-author-id="${task.Author.Id}" class="text-semibold tms-profile-link">
                                ${task.Comments[comment].Author.Name}
                            </a>
                        </div>`;

            if (task.Comments[comment].Author.Id == userId) {
                html += `<div>
                            <button style='height:30px; width:30px' data-editable-name="btnEditComment" data-id="${task.Comments[comment].Id}" class="float-right bg-white border-0" onclick="TaskModalWorker.MakeCommentFieldEditable('${task.Comments[comment].Id}')">
                                <i class="fa fa-pencil-square-o" aria-hidden="true"></i>
                            </button>
                        </div>
                        </div>`;
            } else {
                html += `</div>`;
            }

            html += `<div id="${task.Comments[comment].Id}">${task.Comments[comment].Comment}</div>
                        
                    </div>
                  </div>`;
        };

        html += "</div>";

        document.getElementById(divId).innerHTML = html;
    }

    public static ClearContent = (): void => {
        (<HTMLInputElement>document.getElementsByName("Comment")[0]).value = "";
        document.getElementById("dayTaskModalTitle").innerHTML = "";

        const paras = document.getElementsByClassName("media-block");

        while (paras[0]) {
            paras[0].remove();
        }
    }


    public static MakeCommentFieldEditable = (commentId: string): void => {
        const text = document.getElementById(commentId).innerHTML;
        
        $("[data-editable-name='btnEditComment']").attr('hidden', 'hidden');

            document.getElementById(commentId).innerHTML = `<textarea class="form-control" name="edit.Comment" rows="2">${text}</textarea>
                <button class="btn btn-sm btn-editable float-right m-1" data-editable-cancel="${commentId}" 
                        onclick="TaskModalWorker.ResetCommentChanges('${commentId}','${text}')">
                    <i class="fas fa-times"></i>
                </button>

                <button class="btn btn-sm btn-editable float-right mt-1 tms-update-comment-btn" data-comment-id="${commentId}">
                    <i class="fas fa-check"></i>
                </button>`;
            document.querySelector(`[data-id="${commentId}"]`).setAttribute('hidden', 'hidden');
    }

    public static ResetCommentChanges = (commentId: string, text: string): void => {
        document.getElementById(`${commentId}`).innerHTML = `${text}`;
        $("[data-editable-name='btnEditComment']").removeAttr('hidden');
    }
}
