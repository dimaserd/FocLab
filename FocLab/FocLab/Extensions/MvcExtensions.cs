using System;
using System.Collections.Generic;
using System.Linq;
using Croco.Core.Logic.Workers.Documentation;
using FocLab.Logic.EntityDtos.Users.Default;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FocLab.Extensions
{
    public static class MvcExtensions
    {
        public static IEnumerable<SelectListItem> GetEnumDropdownList(Type type)
        {
            if (!type.IsEnum)
            {
                throw new ApplicationException($"Тип {type.FullName} не является перечислением");
            }

            var descr = ClassModelDescriptor.GetDocumentationForClass(type);

            return descr.EnumValues.Select(x => new SelectListItem
            {
                Text = x.DisplayName,
                Value = x.StringRepresentation
            });
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
