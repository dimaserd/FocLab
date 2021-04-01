using Clt.Model.Entities;
using Croco.Core.Contract;
using Croco.Core.Contract.Application;
using Croco.Core.Contract.Cache;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tms.Logic.Abstractions;
using Tms.Logic.Models;

namespace FocLab.Logic.Implementations
{
    public class TmsUsersStorage : FocLabWorker, IUsersStorage
    {
        ICrocoCacheManager CacheManager { get; }

        public TmsUsersStorage(ICrocoAmbientContextAccessor contextAccessor, 
            ICrocoApplication application) : base(contextAccessor, application)
        {
            CacheManager = application.CacheManager;
        }

        public Task<Dictionary<string, UserFullNameEmailAndAvatarModel>> GetUsersDictionary()
        {
            return CacheManager.GetOrAddValueAsync($"{GetType().FullName}.users", async () =>
            {
                var result = await Query<Client>().Select(SelectExpression).ToListAsync();

                return result.ToDictionary(x => x.Id);

            }, DateTime.Now.AddMinutes(10));
        }

        public Task<UserFullNameEmailAndAvatarModel> GetUserById(string userId)
        {
            return Query<Client>().Select(SelectExpression).FirstOrDefaultAsync(x => x.Id == userId);
        }

        public static readonly Expression<Func<Client, UserFullNameEmailAndAvatarModel>> SelectExpression = x => new UserFullNameEmailAndAvatarModel
        {
            Id = x.Id,
            Email = x.Email,
            AvatarFileId = x.AvatarFileId,
            Name = x.Name,
            Patronymic = x.Patronymic,
            Surname = x.Surname
        };
    }
}