﻿using Croco.Core.Contract.Models.Search;
using Croco.Core.Search.Extensions;
using System;
using System.Collections.Generic;

namespace Zoo.Core
{
    public class WebAppRequestContextLogsSearch : GetListSearchModel
    {
        public GenericRange<DateTime> StartedOn { get; set; }

        public string UserId { get; set; }

        public IEnumerable<SearchQueryCriteria<WebAppRequestContextLog>> GetCriterias()
        {
            yield return StartedOn.GetCriteriaForDatePropertyWithNoTime<WebAppRequestContextLog>(x => x.StartedOn);

            yield return UserId.MapString(x => new SearchQueryCriteria<WebAppRequestContextLog>(t => t.UserId == x));
        }
    }
}