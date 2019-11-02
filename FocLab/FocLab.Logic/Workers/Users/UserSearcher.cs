using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Croco.Core.Abstractions;
using Croco.Core.Logic.Workers;
using Croco.Core.Search.Extensions;
using Croco.Core.Search.Models;
using FocLab.Logic.EntityDtos.Users.Default;
using FocLab.Logic.Models.Users;
using FocLab.Logic.Settings.Statics;
using FocLab.Model.Entities.Users.Default;
using Microsoft.EntityFrameworkCore;

namespace FocLab.Logic.Workers.Users
{
    /// <summary>
    /// Класс предоставляющий методы для поиска пользователей
    /// </summary>
    public class UserSearcher : BaseCrocoWorker
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

        public Task<ApplicationUserDto> GetUserByEmailAsync(string email)
        {
            return GetUserByPredicateExpression(x => x.Email == email);
        }

        private Task<ApplicationUserDto> GetUserByPredicateExpression(Expression<Func<ApplicationUserDto, bool>> predicate)
        {
            var usersRepo = GetRepository<ApplicationUser>();

            return usersRepo.Query().Select(ApplicationUserDto.SelectExpression).FirstOrDefaultAsync(predicate);
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

        public Task<GetListResult<ApplicationUserDto>> SearchUsersAsync(UserSearch model)
        {
            var userRepo = GetRepository<ApplicationUser>();

            var initQuery = userRepo.Query()
                .Where(x => x.Email != RightsSettings.RootEmail);
            
            initQuery = initQuery.BuildQuery(model.GetCriterias());

            return GetListResult<ApplicationUserDto>.GetAsync(model, initQuery.OrderByDescending(x => x.CreatedOn), ApplicationUserDto.SelectExpression);
        }

        #endregion

        public UserSearcher(ICrocoAmbientContext context) : base(context)
        {
        }
    }
}
