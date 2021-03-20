using System.Security.Principal;
using Croco.Core.Contract.Models;
using FocLab.Logic.EntityDtos.Users.Default;
using FocLab.Logic.Extensions;
using FocLab.Model.Enumerations;

namespace FocLab.Logic.Workers.Users
{
    public static class UserRightsWorker
    {
        public static BaseApiResponse HasRightToEditUser(ApplicationUserDto userDto, IPrincipal userPrincipal)
        {
            if (userPrincipal.HasRight(UserRight.Root))
            {
                return new BaseApiResponse(true, "Root может делать все что угодно");
            }

            if (userDto.HasRight(UserRight.Root))
            {
                return new BaseApiResponse(false, "Вы не можете редактировать пользователя Root");
            }

            var isSuperAdmin = userPrincipal.HasRight(UserRight.SuperAdmin);

            if (isSuperAdmin && userDto.HasRight(UserRight.SuperAdmin))
            {
                return new BaseApiResponse(false, "Вы не можете редактировать пользователя, который является Супер-Администратором");
            }

            if (isSuperAdmin)
            {
                return new BaseApiResponse(true, "Вы можете редактировать пользователя, который является Супер-Администратором");
            }

            var isAdmin = userPrincipal.HasRight(UserRight.Admin);

            if (isAdmin && userDto.HasRight(UserRight.Admin))
            {
                return new BaseApiResponse(false, "Вы не можете редактировать пользователя, который является Администратором, для этого нужны права Супер-Администратора");
            }

            if (isAdmin)
            {
                return new BaseApiResponse(true, "Вы не можете редактировать пользователя, который является Администратором, для этого нужны права Супер-Администратора");
            }

            return new BaseApiResponse(true, "Вы не можете редактировать пользователя, так как у вас недостаточно прав");
        }
    }
}