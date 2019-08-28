using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Croco.Core.Abstractions;
using Croco.Core.Extensions.Enumerations;
using Croco.Core.Search.Extensions;
using Croco.Core.Search.Models;
using FocLab.Logic.EntityDtos.Users.Default;
using FocLab.Logic.Extensions;
using FocLab.Logic.Models.Users;
using FocLab.Logic.Settings.Statics;
using FocLab.Model.Contexts;
using FocLab.Model.Entities.Users.Default;
using FocLab.Model.Enumerations;
using Microsoft.EntityFrameworkCore;

namespace FocLab.Logic.Workers.Users
{
    /// <summary>
    /// Класс предоставляющий методы для поиска пользователей
    /// </summary>
    public class UserSearcher : BaseChemistryWorker
    {
        public async Task<List<Tuple<ApplicationRole, string, UserRight>>> GetRightsAndRolesTupleAsync()
        {
            var rolesRepo = GetRepository<ApplicationRole>();

            var roles = await rolesRepo.Query().ToListAsync();

            var tuplesRoleAndRight = new List<Tuple<ApplicationRole, string, UserRight>>();

            foreach (var role in roles)
            {
                var right = ((UserRight)Enum.Parse(typeof(UserRight), role.Name));

                tuplesRoleAndRight.Add(new Tuple<ApplicationRole, string, UserRight>(role, right.ToDisplayName(), right));
            }

            return tuplesRoleAndRight;
        }

        #region Методы получения одного пользователя


        public Task<ApplicationUserDto> GetUserByPhoneNumberAsync(string phoneNumber)
        {
            return GetUserByPredicateExpression(x => x.PhoneNumber == phoneNumber);
        }

        public Task<ApplicationUserDto> GetUserByIdAsync(string userId)
        {   
            return GetUserByPredicateExpression(x => x.Id == userId);
        }

        public Task<ApplicationUserDto> GetUserByEmailAsync(string email)
        {
            return GetUserByPredicateExpression(x => x.Email == email);
        }

        private async Task<ApplicationUserDto> GetUserByPredicateExpression(Expression<Func<ApplicationUserDto, bool>> predicate)
        {
            var usersRepo = GetRepository<ApplicationUser>();

            var user = await usersRepo.Query().Select(ApplicationUserDto.SelectExpression).FirstOrDefaultAsync(predicate);

            if (user == null)
            {
                return null;
            }

            var rightsList = await GetRightsAndRolesTupleAsync();

            user.Rights = new List<UserRight>();

            foreach (var role in user.Roles)
            {
                var rightTuple = rightsList.FirstOrDefault(x => x.Item1.Id == role.RoleId);

                if (rightTuple != null)
                {
                    user.Rights.Add(rightTuple.Item3);
                }
            }

            return user;
        }

        #endregion

        #region Метод получения списка пользователей

        public Task<GetListResult<ApplicationUserBaseModel>> GetUsersAsync(UserSearch model)
        {
            var userRepo = GetRepository<ApplicationUser>();

            var query = userRepo.Query().Where(x => x.Email != RightsSettings.RootEmail)
                .BuildQuery(model.GetCriterias())
                .OrderByDescending(x => x.CreatedOn);

            return GetListResult<ApplicationUserBaseModel>.GetAsync(model, query, ApplicationUserBaseModel.SelectExpression);
        }

        public async Task<GetListResult<ApplicationUserDto>> SearchUsersAsync(UserSearch model)
        {
            
            var rightsList = await GetRightsAndRolesTupleAsync();

            var userRepo = GetRepository<ApplicationUser>();

            var initQuery = userRepo.Query()
                .Where(x => x.Email != RightsSettings.RootEmail);
            
            initQuery = initQuery.BuildQuery(model.GetCriterias());

            var result = new GetListResult<ApplicationUserDto>();
            
            await result.GetResultAsync(model, initQuery.OrderByDescending(x => x.CreatedOn), ApplicationUserDto.SelectExpression);

            result.List.ForEach(x => x.Rights = ApplicationUserExtensions.ToUserRights(rightsList, x.Roles));
            
            return result;
        }

        #endregion

        public UserSearcher(ICrocoAmbientContext context) : base(context)
        {
        }
    }
}
