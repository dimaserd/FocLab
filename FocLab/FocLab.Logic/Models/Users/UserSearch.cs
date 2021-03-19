using System;
using System.Collections.Generic;
using Croco.Core.Contract.Models.Search;
using Croco.Core.Search.Extensions;
using FocLab.Model.Entities.Users.Default;

namespace FocLab.Logic.Models.Users
{
    public class UserSearch : GetListSearchModel
    {
        public string Q { get; set; }

        public bool? Deactivated { get; set; }

        public GenericRange<DateTime> RegistrationDate { get; set; }

        public bool SearchSex { get; set; }

        public bool? Sex { get; set; }

        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        public static UserSearch GetAllUsers => new UserSearch
        {
            Count = null,
            OffSet = 0
        };
        

        public IEnumerable<SearchQueryCriteria<ApplicationUser>> GetCriterias()
        {
            if(!string.IsNullOrWhiteSpace(Q))
            {
                yield return new SearchQueryCriteria<ApplicationUser>(x => x.Email.Contains(Q) || x.PhoneNumber.Contains(Q) || x.Name.Contains(Q));
            }

            if (Deactivated.HasValue)
            {
                yield return new SearchQueryCriteria<ApplicationUser>(x => x.DeActivated == Deactivated.Value);
            }

            yield return RegistrationDate.GetSearchCriteriaFromGenericRange<ApplicationUser, DateTime>(x => x.CreatedOn);

            if (SearchSex)
            {
                yield return new SearchQueryCriteria<ApplicationUser>(x => x.Sex == Sex);
            }
        }
    }
}
