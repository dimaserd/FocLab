using System;
using System.Collections.Generic;
using System.Linq;
using Croco.Core.Application;
using Croco.Core.Common.Enumerations;
using FocLab.Logic.EntityDtos.Users.Default;
using FocLab.Logic.Enumerations;
using FocLab.Logic.Settings.Statics;
using FocLab.Model.Entities.Users.Default;
using FocLab.Model.Enumerations;

namespace FocLab.Logic.Extensions
{
    public static class ApplicationUserExtensions
    {
        public static string GetAvatarLink(this ApplicationUser user, ImageSizeType imageSizeType)
        {
            var imageId = user?.AvatarFileId;

            return imageId.HasValue ? CrocoApp.Application.FileCopyWorker.GetVirtualResizedImageLocalPath(imageId.Value, imageSizeType) : null;
        }

        public static UserDifferenceAction GetComparingAction(ApplicationUserDto userFromCookie, ApplicationUserDto userFromDb)
        {
            if (!userFromCookie.EmailConfirmed && userFromCookie.Email != RightsSettings.RootEmail && !AccountSettings.IsLoginEnabledForUsersWhoDidNotConfirmEmail)
            {
                return UserDifferenceAction.Logout;
            }

            if (userFromDb.DeActivated || userFromDb.PasswordHash != userFromCookie.PasswordHash)
            {
                return UserDifferenceAction.Logout;
            }

            var compareResult = Compare(userFromCookie, userFromDb);

            return !compareResult ? UserDifferenceAction.AutoReLogin : UserDifferenceAction.None;
        }


        /// <summary>
        /// Из модели DTO в сущность
        /// </summary>
        /// <returns></returns>
        public static ApplicationUser ToEntity(this ApplicationUserDto model)
        {
            return new ApplicationUser
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname,
                Patronymic = model.Patronymic,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                EmailConfirmed = model.EmailConfirmed,
                SecurityStamp = model.SecurityStamp,
                CreatedOn = model.CreatedOn,
                AvatarFileId = model.AvatarFileId,
                BirthDate = model.BirthDate,
                Sex = model.Sex,
                Balance = model.Balance
            };
        }

        /// <summary>
        /// Из сущности в модель DTO
        /// </summary>
        /// <returns></returns>
        public static ApplicationUserDto ToDto(this ApplicationUser model)
        {
            return new ApplicationUserDto
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname,
                Patronymic = model.Patronymic,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                EmailConfirmed = model.EmailConfirmed,
                SecurityStamp = model.SecurityStamp,
                CreatedOn = model.CreatedOn,
                AvatarFileId = model.AvatarFileId,
                BirthDate = model.BirthDate,
                Sex = model.Sex,
                Balance = model.Balance

            };
        }


        public static bool Compare(ApplicationUserDto user1, ApplicationUserDto user2)
        {
            var rightsChanged = user1.Roles.Count != user2.Roles.Count;

            if (!rightsChanged)
            {
                for (var i = 0; i < user1.Roles.Count; i++)
                {
                    if (user1.Roles.OrderBy(x => x).ToList()[i] == user2.Roles.OrderBy(x => x).ToList()[i])
                    {
                        continue;
                    }
                    rightsChanged = true;
                    break;
                }
            }

            return user1.Id == user2.Id &&
                !rightsChanged &&
                user1.Name == user2.Name &&
                user1.AvatarFileId == user2.AvatarFileId &&
                string.IsNullOrEmpty(user1.ObjectJson) == string.IsNullOrEmpty(user2.ObjectJson) &&
                string.IsNullOrEmpty(user1.PhoneNumber) == string.IsNullOrEmpty(user2.PhoneNumber);
        }

        public static List<UserRight> ToUserRights(List<Tuple<ApplicationRole, string, UserRight>> rightsTupleList, List<ApplicationUserRoleDto> roles)
        {
            var rights = new List<UserRight>();

            foreach (var role in roles)
            {
                var rightTuple = rightsTupleList.FirstOrDefault(x => x.Item1.Id == role.RoleId);

                if (rightTuple != null)
                {
                    rights.Add(rightTuple.Item3);
                }
            }

            return rights;
        }
    }
}
