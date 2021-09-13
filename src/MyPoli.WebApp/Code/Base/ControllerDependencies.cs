using MyPoli.Common.DTOs;

namespace MyPoli.WebApp.Code.Base
{
    public class ControllerDependencies
    {
        public CurrentUserDto CurrentUser { get; set; }

        public ControllerDependencies(CurrentUserDto currentUser)
        {
            this.CurrentUser = currentUser;
        }
    }
}
