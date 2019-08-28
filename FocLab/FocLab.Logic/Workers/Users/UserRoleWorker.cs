using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Abstractions;
using Croco.Core.Common.Models;
using Croco.Core.Extensions.Enumerations;
using FocLab.Logic.Models.Users;
using FocLab.Logic.Resources;
using FocLab.Logic.Services;
using FocLab.Model.Entities.Users.Default;
using FocLab.Model.Enumerations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FocLab.Logic.Workers.Users
{
    public class UserRoleWorker : BaseChemistryWorker
    {
        public static int GetHighRoleOfUser(IList<string> roles)
        {
            if (roles.Contains(UserRight.Root.ToString()))
            {
                return (int)UserRight.Root;
            }
            if (roles.Contains(UserRight.SuperAdmin.ToString()))
            {
                return (int)UserRight.SuperAdmin;
            }
            if (roles.Contains(UserRight.Admin.ToString()))
            {
                return (int)UserRight.Admin;
            }

            return int.MaxValue;
        }

        public async Task<BaseApiResponse> AddUserToRoleAsync(UserIdAndRole userIdAndRole, UserManager<ApplicationUser> userManager)
        {
            if (!IsAuthenticated)
            {
                return new BaseApiResponse(false, "Вы не авторизованы");
            }
            var role = await Context.Roles.FirstOrDefaultAsync(x => x.Name == userIdAndRole.Role.ToString());

            if (role == null)
            {
                return new BaseApiResponse(false, "Право не найдено (Возможно оно еще не создано)");
            }

            var userRepo = GetRepository<ApplicationUser>();

            var user = await userRepo.Query().FirstOrDefaultAsync(x => x.Id == userIdAndRole.UserId);
            if (user == null)
            {
                return new BaseApiResponse(false, "Изменяемый пользователь не найден");
            }

            var userEditorId = UserId;
            var userEditor = await userRepo.Query().FirstOrDefaultAsync(x => x.Id == userEditorId);
            if (userEditor == null)
            {
                return new BaseApiResponse(false, "Пользователь не найден");
            }
            
            var rolesOfEditUser= await userManager.GetRolesAsync(userEditor);
            var rolesOfUserEditor= await userManager.GetRolesAsync(userEditor);

            var roleOfEditUser = GetHighRoleOfUser(rolesOfEditUser);
            var roleOfUserEditor = GetHighRoleOfUser(rolesOfUserEditor);

            if (roleOfUserEditor >= roleOfEditUser)
            {
                return new BaseApiResponse(false, "Вы не имеете прав изменять роль данного пользователя");
            }

            if (roleOfUserEditor >= (int)userIdAndRole.Role)
            {
                return new BaseApiResponse(false, "Вы не имеете прав добавлять роль равную или выше вашей");
            }

            if (user.Roles.Any(x => x.RoleId == role.Id))
            {
                return new BaseApiResponse(false, "У пользователя уже есть данное право");
            }

            var addingToRoleResult = await userManager.AddToRoleAsync(userEditor, role.Name);

            if (!addingToRoleResult.Succeeded)
            {
                return new BaseApiResponse(false, addingToRoleResult.Errors.First().Description);
            }

            return new BaseApiResponse(true, $"Право {userIdAndRole.Role.ToDisplayName()} добавлено пользователю {user.Name}"); ;
        }


        public async Task<List<string>> GetRolesAsync()
        {
            return await Context.Roles.Select(x => x.Name).ToListAsync();
        }


        public async Task<BaseApiResponse> RemoveRoleFromUserAsync(UserIdAndRole userIdAndRole, UserManager<ApplicationUser> userManager)
        {
            if (!IsAuthenticated)
            {
                return new BaseApiResponse(false, ValidationMessages.YouAreNotAuthorized);
            }

            var role = await Context.Roles.FirstOrDefaultAsync(x => x.Name == userIdAndRole.Role.ToString());

            if (role == null)
            {
                return new BaseApiResponse(false, "Право не найдено (Возможно оно еще не создано)");
            }

            var userRepo = GetRepository<ApplicationUser>();

            var user = await userRepo.Query().FirstOrDefaultAsync(x => x.Id == userIdAndRole.UserId);
            if (user == null)
            {
                return new BaseApiResponse(false, "Изменяемый пользователь не найден");
            }

            var userEditorId = UserId;
            var userEditor = await userRepo.Query().FirstOrDefaultAsync(x => x.Id == userEditorId);
            if (userEditor == null)
            {
                return new BaseApiResponse(false, "Пользователь не найден");
            }

            var rolesOfEditUser = await userManager.GetRolesAsync(user);
            var rolesOfUserEditor = await userManager.GetRolesAsync(userEditor);

            var roleOfEditUser = GetHighRoleOfUser(rolesOfEditUser);
            var roleOfUserEditor = GetHighRoleOfUser(rolesOfUserEditor);

            if (roleOfUserEditor >= roleOfEditUser)
            {
                return new BaseApiResponse(false, "Вы не имеете прав изменять роль данного пользователя");
            }

            if (roleOfUserEditor >= (int)userIdAndRole.Role)
            {
                return new BaseApiResponse(false, "Вы не имеете прав удалять роль равную или выше вашей");
            }

            if (user.Roles.All(x => x.RoleId != role.Id))
            {
                return new BaseApiResponse(false, "У пользователя уже отсутствует данное право");
            }

            var removingToRoleResult = await userManager.RemoveFromRoleAsync(userEditor, role.Name);

            if (!removingToRoleResult.Succeeded)
            {
                return new BaseApiResponse(false, removingToRoleResult.Errors.First().Description);
            }

            return new BaseApiResponse(true, $"Право {userIdAndRole.Role.ToDisplayName()} удалено у пользователя {user.Name}"); ;
        }

        public UserRoleWorker(ICrocoAmbientContext context) : base(context)
        {
        }
    }
}
