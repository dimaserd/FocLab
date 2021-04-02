using Clt.Contract.Models.Users;
using Croco.Core.Common.Enumerations;

namespace FocLab.App.Logic.Extensions
{
    public static class ClientExtensions
    {
        public static string GetAvatarLink(this ClientModel model, ImageSizeType imageSizeType)
        {
            return $"/Files/ImageCopies/{model.AvatarFileId}.jpg";
        }
    }
}
