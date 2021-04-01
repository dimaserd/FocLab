using Clt.Contract.Models.Users;
using Clt.Logic.Services.Users;
using Croco.Core.Contract;
using Croco.Core.Contract.Cache;

namespace FocLab.Logic.Services
{
    public class ClientCacheService
    {
        ICrocoCacheManager CacheManager { get; }
        ClientQueryService ClientQueryService { get; }
        string UserId { get; }

        public ClientCacheService(ICrocoCacheManager cacheManager, 
            ICrocoRequestContextAccessor requestContextAccessor,
            ClientQueryService clientWorker)
        {
            CacheManager = cacheManager;
            ClientQueryService = clientWorker;
            UserId = requestContextAccessor.GetRequestContext().UserPrincipal.UserId;
        }

        public ClientModel GetCachedUser()
        {
            var value = CacheManager.GetOrAddValue($"{GetType().FullName}_{UserId}", () => ClientQueryService.GetClientById(UserId));

            return value.ResponseObject;
        }
    }
}
