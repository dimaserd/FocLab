using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Croco.Core.Abstractions;
using Croco.Core.Search.Extensions;
using Croco.Core.Search.Models;
using FocLab.Logic.EntityDtos.Users.Default;
using FocLab.Logic.Implementations;
using FocLab.Logic.Models.Users;
using FocLab.Model.Entities.Users.Default;
using Microsoft.EntityFrameworkCore;

namespace FocLab.Logic.Workers.Users
{
    /// <summary>
    /// Класс предоставляющий методы для поиска пользователей
    /// </summary>
    public class UserSearcher : FocLabWorker
    {
        #region Методы получения одного пользователя

        public Task<ApplicationUserDto> GetUserByPhoneNumberAsync(string phoneNumber)
        {
            return GetUserByPredicateExpression(x => x.PhoneNumber == phoneNumber);
        }

        public Task<ApplicationUserDto> GetUserByIdAsync(string userId)
        {   
            return GetUserByPredicateExpression(x => x.Id == userId);
        }

        public Task<List<ApplicationUserDto>> GetUsersByIdsAsync(List<string> userIds)
        {
            return GetUsersByPredicateExpression(x => userIds.Contains(x.Id));
        }

        public Task<ApplicationUserDto> GetUserByEmailAsync(string email)
        {
            return GetUserByPredicateExpression(x => x.Email == email);
        }

        private Task<ApplicationUserDto> GetUserByPredicateExpression(Expression<Func<ApplicationUserDto, bool>> predicate)
        {
            return Query<ApplicationUser>()
                .Select(ApplicationUserDto.SelectExpression)
                .FirstOrDefaultAsync(predicate);
        }

        private Task<List<ApplicationUserDto>> GetUsersByPredicateExpression(Expression<Func<ApplicationUserDto, bool>> predicate)
        {
            return Query<ApplicationUser>()
                .Select(ApplicationUserDto.SelectExpression)
                .Where(predicate)
                .ToListAsync();
        }

        #endregion

        #region Метод получения списка пользователей

        public Task<GetListResult<ApplicationUserBaseModel>> GetUsersAsync(UserSearch model)
        {
            var query = Query<ApplicationUser>()
                .BuildQuery(model.GetCriterias())
                .OrderByDescending(x => x.CreatedOn);

            return GetListResult<ApplicationUserBaseModel>.GetAsync(model, query, ApplicationUserBaseModel.SelectExpression);
        }

        public Task<GetListResult<ApplicationUserDto>> SearchUsersAsync(UserSearch model)
        {
            var initQuery = Query<ApplicationUser>()
                .BuildQuery(model.GetCriterias())
                .OrderByDescending(x => x.CreatedOn);

            return GetListResult<ApplicationUserDto>.GetAsync(model, initQuery, ApplicationUserDto.SelectExpression);
        }

        #endregion

        public UserSearcher(ICrocoAmbientContext context) : base(context)
        {
        }
    }
}