namespace Zoo.Core
{
    public class WebAppRequestContextLogWithUserModel
    {
        public WebAppRequestContextLogModel Log { get; set; }

        public UserNameAndEmailModel User { get; set; }
    }
}