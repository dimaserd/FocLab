﻿using Croco.Core.Contract;
using Croco.Core.Contract.Application;
using Croco.Core.Contract.Models.Search;
using Croco.Core.Logic.Models.Users;
using Croco.Core.Logic.Services;
using Croco.Core.Model.Abstractions.Entity;
using Croco.Core.Search.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Zoo.Core
{
    public class WebAppRequestContextSearcher<TDbContext> : BaseCrocoService<TDbContext> where TDbContext : DbContext
    {
        public WebAppRequestContextSearcher(ICrocoAmbientContextAccessor context, ICrocoApplication application) : base(context, application)
        {
        }

        public async Task<GetListResult<WebAppRequestContextLogWithUserModel>> GetLogs<TUser>(WebAppRequestContextLogsSearch model) where TUser : class, ICrocoUser
        {
            var startDate = DateTime.Now;

            var query = Query<WebAppRequestContextLog>()
                .BuildQuery(model.GetCriterias())
                .OrderBy(x => x.StartedOn);

            var step1 = (DateTime.Now - startDate).TotalMilliseconds;

            var joinedQuery = from log in query
                              join user in Query<TUser>() on log.UserId equals user.Id into joinedUser
                              from userOrEmpty in joinedUser.DefaultIfEmpty()
                              select new
                              {
                                  Log = log,
                                  UserOrEmpty = userOrEmpty,
                              };

            var res = await EFCoreExtensions.GetAsync(model, joinedQuery.OrderBy(x => x.Log.StartedOn), a => new WebAppRequestContextLogWithUserModel
            {
                Log = new WebAppRequestContextLogModel
                {
                    Id = a.Log.Id,
                    FinishedOn = a.Log.FinishedOn,
                    ParentRequestId = a.Log.ParentRequestId,
                    RequestId = a.Log.RequestId,
                    StartedOn = a.Log.StartedOn,
                    Uri = a.Log.Uri,
                    UserId = a.Log.UserId
                },
                User = a.UserOrEmpty != null ? new UserNameAndEmailModel
                {
                    Id = a.UserOrEmpty.Id,
                    Email = a.UserOrEmpty.Email,
                    Name = a.UserOrEmpty.Name
                } : null
            });

            var step3 = (DateTime.Now - startDate).TotalMilliseconds;

            return res;
        }
    }
}