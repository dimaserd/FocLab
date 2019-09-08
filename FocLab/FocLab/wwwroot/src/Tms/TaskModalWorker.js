var TaskModalWorker = (function () {
    function TaskModalWorker() {
    }
    TaskModalWorker.ShowDayTaskModal = function (task) {
        TaskModalWorker.InitTask(task);
        FormDataHelper.FillDataByPrefix(task, "task.");
        Utils.SetDatePicker("input[name='task.TaskDate']");
        ModalWorker.ShowModal("dayTaskModal");
    };
    TaskModalWorker.InitTask = function (task) {
        task.TaskDate = moment(new Date(task.TaskDate)).format("DD/MM/YYYY");
        TaskModalWorker.ClearContent();
        document.getElementById("dayTaskModalTitle").innerHTML = task.TaskTitle;
        var avatar = ColorAvatarInitor.InitColorForAvatar(task);
        document.getElementById("Author").innerHTML = "<a class=\"media-left tms-profile-link\" href=\"#\" data-task-author-id=\"" + task.Author.Id + "\">" + avatar + "</a>\n                <a  href=\"#\" data-task-author-id=\"" + task.Author.Id + "\" class=\"tms-profile-link text-semibold media-heading box-inline ml-1 mb-1\">\n                    " + task.Author.Name + " " + task.Author.Email + "\n                </a>";
        document.getElementsByName('DayTaskId')[0].value = task.Id;
        $("#usersSelect1").val(task.AssigneeUser.Id).trigger('change.select2');
        TaskModalWorker.DrawComments("Comments", task);
        $("#usersSelect1").select2({
            width: '100%'
        });
    };
    TaskModalWorker.DrawComments = function (divId, task) {
        TaskModalWorker.ClearContent();
        var userId = AccountWorker.User.Id;
        var avatar = ColorAvatarInitor.InitColorForAvatar(task);
        var html = "<div>";
        for (var comment in task.Comments) {
            html += "\n          <div class=\"media-block\">\n            <div class=\"media-body\">\n                <div class=\"form-group m-form__group row m--margin-top-10 d-flex justify-content-between align-items-center\">\n                        <div>\n                            <a href=\"#\" class=\"btn-link btn cursor-pointer tms-profile-link\" data-task-author-id=\"" + task.Author.Id + "\">\n                                " + avatar + "\n                            </a>\n                            <a href=\"#\" data-task-author-id=\"" + task.Author.Id + "\" class=\"text-semibold tms-profile-link\">\n                                " + task.Comments[comment].Author.Name + "\n                            </a>\n                        </div>";
            if (task.Comments[comment].Author.Id == userId) {
                html += "<div>\n                            <button style='height:30px; width:30px' data-editable-name=\"btnEditComment\" data-id=\"" + task.Comments[comment].Id + "\" class=\"float-right bg-white border-0\" onclick=\"TaskModalWorker.MakeCommentFieldEditable('" + task.Comments[comment].Id + "')\">\n                                <i class=\"fa fa-pencil-square-o\" aria-hidden=\"true\"></i>\n                            </button>\n                        </div>\n                        </div>";
            }
            else {
                html += "</div>";
            }
            html += "<div id=\"" + task.Comments[comment].Id + "\">" + task.Comments[comment].Comment + "</div>\n                        \n                    </div>\n                  </div>";
        }
        ;
        html += "</div>";
        document.getElementById(divId).innerHTML = html;
    };
    TaskModalWorker.ClearContent = function () {
        document.getElementsByName("Comment")[0].value = "";
        document.getElementById("dayTaskModalTitle").innerHTML = "";
        var paras = document.getElementsByClassName("media-block");
        while (paras[0]) {
            paras[0].remove();
        }
    };
    TaskModalWorker.MakeCommentFieldEditable = function (commentId) {
        var text = document.getElementById(commentId).innerHTML;
        $("[data-editable-name='btnEditComment']").attr('hidden', 'hidden');
        document.getElementById(commentId).innerHTML = "<textarea class=\"form-control\" name=\"edit.Comment\" rows=\"2\">" + text + "</textarea>\n                <button class=\"btn btn-sm btn-editable float-right m-1\" data-editable-cancel=\"" + commentId + "\" \n                        onclick=\"TaskModalWorker.ResetCommentChanges('" + commentId + "','" + text + "')\">\n                    <i class=\"fas fa-times\"></i>\n                </button>\n\n                <button class=\"btn btn-sm btn-editable float-right mt-1 tms-update-comment-btn\" data-comment-id=\"" + commentId + "\">\n                    <i class=\"fas fa-check\"></i>\n                </button>";
        document.querySelector("[data-id=\"" + commentId + "\"]").setAttribute('hidden', 'hidden');
    };
    TaskModalWorker.ResetCommentChanges = function (commentId, text) {
        document.getElementById("" + commentId).innerHTML = "" + text;
        $("[data-editable-name='btnEditComment']").removeAttr('hidden');
    };
    return TaskModalWorker;
}());
