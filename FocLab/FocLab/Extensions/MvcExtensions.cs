using System;
using System.Collections.Generic;
using FocLab.Logic.EntityDtos.Users.Default;
using Zoo.GenericUserInterface.Extensions;
using Zoo.GenericUserInterface.Models;

namespace FocLab.Extensions
{
    public static class MvcExtensions
    {
        public static List<MySelectListItem> GetEnumDropdownList(Type type)
        {
            return MySelectListItemExtensions.GetEnumDropDownList(type);
        }

        public static IEnumerable<MySelectListItem> GetSexesSelectList(ApplicationUserDto applicationUser)
        {
            yield return new MySelectListItem
            {
                Text = "Мужской",
                Value = true.ToString(),
                Selected = applicationUser.Sex == true
            };

            yield return new MySelectListItem
            {
                Text = "Женский",
                Value = false.ToString(),

                Selected = applicationUser.Sex == false
            };

            yield return new MySelectListItem
            {
                Text = "Не указано",
                Value = "",
                Selected = applicationUser.Sex == null
            };
        }
    }
}
