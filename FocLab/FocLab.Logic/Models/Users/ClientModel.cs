using Croco.Core.Application;
using Croco.Core.Common.Enumerations;

namespace FocLab.Logic.Models.Users
{
    public class ClientModel : EditClient
    {
        public string Email { get; set; }

        public int? AvatarFileId { get; set; }

        public string GetImgLink(ImageSizeType sizeType)
        {
            if (!AvatarFileId.HasValue)
            {
                return null;
            }

            return CrocoApp.Application.FileCopyWorker.GetVirtualResizedImageLocalPath(AvatarFileId.Value, sizeType);
        }
    }
}