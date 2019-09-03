using System;
using System.Collections.Generic;
using System.Linq;
using Croco.Core.Logic.Workers.Documentation;
using FocLab.Logic.EntityDtos.Users.Default;
using Microsoft.AspNetCore.Mvc.Rendering;
using Zoo.GenericUserInterface.Models;

namespace FocLab.Extensions
{
    public static class MvcExtensions
    {
        public static List<SelectListItem> GetEnumDropdownList(Type type)
        {
            if (!type.IsEnum)
            {
                throw new ApplicationException($"Тип {type.FullName} не является перечислением");
            }

            var descr = CrocoTypeDescriptor.GetDocumentationForClass(type);

            return descr.EnumValues.Select(x => new SelectListItem
            {
                Text = x.DisplayName,
                Value = x.StringRepresentation
            }).ToList();
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
