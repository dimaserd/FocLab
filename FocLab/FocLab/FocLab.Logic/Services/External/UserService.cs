using Croco.Core.Contract;
using Croco.Core.Contract.Application;
using Croco.Core.Contract.Models;
using Microsoft.EntityFrameworkCore;
using FocLab.Logic.Models.Users;
using FocLab.Model.External;
using System.Threading.Tasks;

namespace FocLab.Logic.Services.External
{
    /// <summary>
    /// Сервис для работы с пользователями
    /// </summary>
    public class UserService : FocLabService
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context"></param>
        /// <param name="application"></param>
        public UserService(ICrocoAmbientContextAccessor context, ICrocoApplication application) : base(context, application)
        {
        }

        /// <summary>
        /// Создать пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> CreateUser(UserModelBase model)
        {
            var repo = GetRepository<FocLabUser>();

            if (await repo.Query().AnyAsync(x => x.Id == model.Id))
            {
                return new BaseApiResponse(false, "Пользователь с таким идентификатором уже существует");
            }

            repo.CreateHandled(new FocLabUser
            {
                Id = model.Id,
                Email = model.Email,
                Name = model.Name
            });

            return await TrySaveChangesAndReturnResultAsync("Пользователь успешно добавлен");
        }

        /// <summary>
        /// Обновить пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> UpdateUser(UserModelBase model)
        {
            var repo = GetRepository<FocLabUser>();

            var user = await repo.Query().FirstOrDefaultAsync(x => x.Id == model.Id);

            if (user == null)
            {
                return new BaseApiResponse(false, "Пользователь не найден с таким идентификатором");
            }

            user.Email = model.Email;
            user.Name = model.Name;

            repo.UpdateHandled(user);

            return await TrySaveChangesAndReturnResultAsync("Пользователь успешно добавлен");
        }
    }
}