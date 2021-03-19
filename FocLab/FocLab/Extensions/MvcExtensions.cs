using System;
using System.Collections.Generic;
using FocLab.Logic.EntityDtos.Users.Default;
using Zoo.GenericUserInterface.Models;

namespace FocLab.Extensions
{
    public static class MvcExtensions
    {
        public static List<SelectListItem> GetEnumDropdownList(Type type)
        {
            return SelectListItemExtensions.GetEnumDropDownList(type);
        }

        public static IEnumerable<SelectListItem> GetSexesSelectList(ApplicationUserDto applicationUser)
        {
            yield return new SelectListItem
            {
                Text = "Мужской",
                Value = true.ToString(),
                Selected = applicationUser.Sex == true
            };

            yield return new SelectListItem
            {
                Text = "Женский",
                Value = false.ToString(),

                Selected = applicationUser.Sex == false
            };

            yield return new SelectListItem
            {
                Text = "Не указано",
                Value = "",
                Selected = applicationUser.Sex == null
            };
        }
    }
}