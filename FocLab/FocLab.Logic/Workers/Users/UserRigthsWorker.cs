using System.Linq;
using System.Security.Principal;
using Croco.Core.Common.Models;
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

            if (userDto.Rights.Any(x => x == UserRight.Root))
            {
                return new BaseApiResponse(false, "Вы не можете редактировать пользователя Root");
            }

            var isSuperAdmin = userPrincipal.HasRight(UserRight.SuperAdmin);

            if (isSuperAdmin && userDto.Rights.Any(x => x == UserRight.SuperAdmin))
            {
                return new BaseApiResponse(false, "Вы не можете редактировать пользователя, который является Супер-Администратором");
            }

            if (isSuperAdmin)
            {
                return new BaseApiResponse(true, "Вы можете редактировать пользователя, который является Супер-Администратором");
            }

            var isAdmin = userPrincipal.HasRight(UserRight.Admin);

            if (isAdmin && userDto.Rights.Any(x => x == UserRight.Admin))
            {
                return new BaseApiResponse(false, "Вы не можете редактировать пользователя, который является Администратором, для этого нужны права Супер-Администратора");
            }

            if (isAdmin)
            {
                return new BaseApiResponse(true, "Вы не можете редактировать пользователя, который является Администратором, для этого нужны права Супер-Администратора");
            }

            return new BaseApiResponse(true, "Вы не можете редактировать пользователя, так как у вас недостаточно прав");
        }


        public static BaseApiResponse CheckRightsForManagingRight(UserRight right, IPrincipal userPrincipal)
        {
            #region Проверка на root
            if (right == UserRight.Root)
            {
                return new BaseApiResponse(false, "Право доступа Root запрещено к редактированию");
            }

            bool isRoot = userPrincipal.HasRight(UserRight.Root);

            if (isRoot)
            {
                return new BaseApiResponse(true, "Root может делать все что угодно кроме добавления права Root");
            }
            #endregion

            #region Супер Админ
            bool isSuperAdmin = userPrincipal.HasRight(UserRight.SuperAdmin);

            if (isSuperAdmin && right == UserRight.SuperAdmin)
            {
                return new BaseApiResponse(false, "Супер-Администратор не может редактировать права пользователя Супер-Администратор");
                    //new BaseApiResponse(true, "Супер-Администратор может редактировать права пользователя Супер-Администратор");
            }
            #endregion


            var isAdmin = userPrincipal.HasRight(UserRight.Admin);

            if (isAdmin && right == UserRight.SuperAdmin)
            {
                return new BaseApiResponse(false, "Администратор не может редактировать пользователей с правами Супер-Администратора");
            }

            if(isAdmin && right == UserRight.Admin)
            {
                return new BaseApiResponse(false, "Администратор не может редактировать права пользователя Администратор");
                    
            }



            if ( (isAdmin || isSuperAdmin) && (right != UserRight.Admin && right != UserRight.SuperAdmin))
            {
                return new BaseApiResponse(true, "Вы можете назначить или удалить данное право у пользователя");
            }

            return new BaseApiResponse(false, "Вы не имеете прав для редактирования прав пользователям");
        }

    }
}
