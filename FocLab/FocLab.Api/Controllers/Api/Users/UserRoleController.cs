using System.Threading.Tasks;
using Croco.Core.Common.Models;
using FocLab.Api.Controllers.Base;
using FocLab.Logic.Models.Users;
using FocLab.Logic.Services;
using FocLab.Logic.Workers.Users;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Api.Controllers.Api.Users
{
    /// <inheritdoc />
    /// <summary>
    /// Контроллер предоставляющий методы для работы c ролями пользователей 
    /// </summary>
    [Route("Api/User/Role")]
    public class UserRoleController: BaseApiController
    {
        /// <inheritdoc />
        public UserRoleController(ChemistryDbContext context, ApplicationSignInManager signInManager, ApplicationUserManager userManager, IHttpContextAccessor httpContextAccessor) : base(context, signInManager, userManager, httpContextAccessor)
        {
        }

        private UserRoleWorker UserRoleWorker => new UserRoleWorker(AmbientContext);

        /// <summary>
        /// Добавление роли
        /// </summary>
        [HttpPost(nameof(Add))]
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        public Task<BaseApiResponse> Add([FromForm]UserIdAndRole model)
        {
            return UserRoleWorker.AddUserToRoleAsync(model, UserManager);
        }

        /// <summary>
        /// Удаление роли
        /// </summary>
        [HttpPost(nameof(Remove))]
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        public Task<BaseApiResponse> Remove([FromForm]UserIdAndRole model)
        {
            return UserRoleWorker.RemoveRoleFromUserAsync(model, UserManager);
        }
    }
}
