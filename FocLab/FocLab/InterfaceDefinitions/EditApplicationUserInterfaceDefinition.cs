using Clt.Contract.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zoo.GenericUserInterface.Models.Definition;
using Zoo.GenericUserInterface.Services;

namespace FocLab.InterfaceDefinitions
{
    public class EditApplicationUserInterfaceDefinition : UserInterfaceDefinition<EditApplicationUser>
    {
        public override Task OverrideInterfaceAsync(GenericUserInterfaceBag bag, GenericUserInterfaceModelBuilder<EditApplicationUser> overrider)
        {
            overrider
                .GetBlockBuilder(x => x.Id)
                .SetHidden();

            overrider.GetBlockBuilder(x => x.ObjectJson)
                .SetHidden();

            overrider.GetBlockBuilder(x => x.Sex)
                .SetDropDownList(GetSexesSelectList());

            return Task.CompletedTask;
        }

        public static List<SelectListItemData<bool?>> GetSexesSelectList()
        {
            return new List<SelectListItemData<bool?>>
            {
                new SelectListItemData<bool?>
                {
                    Text = "Мужской",
                    Value = true,
                    Selected = false
                },
                new SelectListItemData<bool?>
                {
                    Text = "Женский",
                    Value = false,

                    Selected = false
                },
                new SelectListItemData<bool?>
                {
                    Text = "Не указано",
                    Value = null,
                    Selected = true
                }
            };
        }
    }
}