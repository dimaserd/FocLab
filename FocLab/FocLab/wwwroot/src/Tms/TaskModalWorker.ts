class TaskModalWorker {

    public static InitTask = function (task: any, accountId: string) {

        task.TaskDate = task.TaskDate.split('T')[0].split('-').reverse().join('/');

        TaskModalWorker.ClearContent();

        document.getElementById("dayTaskModalTitle").innerHTML = task.TaskTitle;

        let avatar: string = ColorAvatarInitor.InitColorForAvatar(task);

        document.getElementById("Author").innerHTML = `<a class="media-left" onClick="redirectToProfile('${task.Author.Id}')">${avatar}</a>
                <a onClick="redirectToProfile('${task.Author.Id}')" class="text-semibold media-heading box-inline ml-1 mb-1">
                    ${task.Author.Name} ${task.Author.Email}
                </a>`;

        (<HTMLInputElement>document.getElementsByName('DayTaskId')[0]).value = task.Id;

        $("#usersSelect1").val(task.AssigneeUser.Id).trigger('change.select2');

        TaskModalWorker.DrawComments("Comments", accountId, task);
        (<any>$)("#usersSelect1").select2({
            width: '90%'
        });
    }

    public static DrawComments = function (divId: string, userId: string, task: any) {
        TaskModalWorker.ClearContent();

        let avatar: string = ColorAvatarInitor.InitColorForAvatar(task);

        let html: string = "";
        for (var comment in task.Comments) {
            html += `
          <div class="media-block">
            <div class="media-body">
                <div class="form-group m-form__group row m--margin-top-10 d-flex justify-content-between align-items-center">
                        <div class="btn">
                            <a class="btn-link btn cursor-pointer" onClick="redirectToProfile('${task.Author.Id}')">
                                ${avatar}
                            </a>
                            <a onClick="redirectToProfile('${task.Author.Id}')" class="text-semibold">
                                ${task.Comments[comment].Author.Name}
                            </a>
                        </div>`;

            if (task.Comments[comment].Author.Id == userId) {
                html += `<div class="btn">
                                    <button style='height:30px; width:30px' data-editable-name="btnEditComment" data-id="${task.Comments[comment].Id}"
                                    class="float-right bg-white border-0" onclick="TaskModalWorker.MakeCommentFieldEditable('${task.Comments[comment].Id}')">
                                        <i class="fa fa-pencil-square-o" aria-hidden="true"></i>
                                    </button>
                            </div>
                        </div>`;
            } else {
                html += `</div>`;
            }

            html += `<div id="${task.Comments[comment].Id}" class="col-lg-12">${task.Comments[comment].Comment}</div>
                        <hr>
                    </div>
                  </div>`;
        };

        document.getElementById(divId).innerHTML = html;
    }

    public static ClearContent = ():void => {
        (<HTMLInputElement>document.getElementsByName("Comment")[0]).value = "";
        document.getElementById("dayTaskModalTitle").innerHTML = "";

        const paras = document.getElementsByClassName("media-block");

        while (paras[0]) {
            paras[0].remove();
        }
    }


    public static MakeCommentFieldEditable = (commentId: string): void => {
        const text = document.getElementById(`${commentId}`).innerHTML;
        
        $("[data-editable-name='btnEditComment']").attr('hidden', 'hidden');

            document.getElementById(`${commentId}`).innerHTML = `<textarea class="form-control" name="edit.Comment" rows="2">${text}</textarea>
                <button class="btn btn-sm btn-editable float-right m-1" data-editable-cancel="${commentId}" 
                        onclick="TaskModalWorker.ResetCommentChanges('${commentId}','${text}')">
                    <i class="fas fa-times"></i>
                </button>

                <button id="updateComment" class="btn btn-sm btn-editable float-right mt-1" onClick="updateComment('${commentId}')">
                    <i class="fas fa-check"></i>
                </button>`;
            document.querySelector(`[data-id="${commentId}"]`).setAttribute('hidden', 'hidden');
    }

    public static ResetCommentChanges = (commentId: string, text: string): void => {
        document.getElementById(`${commentId}`).innerHTML = `${text}`;
        $("[data-editable-name='btnEditComment']").removeAttr('hidden');
    }
}
