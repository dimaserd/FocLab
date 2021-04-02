using Croco.Core.Application;
using Croco.Core.Common.Enumerations;
using Croco.Core.Logic.Files.Entites;
using Croco.Core.Logic.Files.Models;
using FocLab.App.Logic.EntityDtos;
using FocLab.App.Logic.Implementations;

namespace FocLab.App.Logic.Extensions
{
    public static class DbFileExtensions
    {
        public static bool IsImage(this DbFileIntIdModelNoData model)
        {
            return FocLabWebApplication.IsImage(model.FileName);
        }

        public static bool IsImage(this DbFile file)
        {
            return FocLabWebApplication.IsImage(file.FileName);
        }

        public static bool IsImage(this DbFileDto file)
        {
            return FocLabWebApplication.IsImage(file.FileName);
        }

        public static string GetLinkToDownload(this int fileId, ImageSizeType sizeType = ImageSizeType.Original)
        {
            return $"/Files/GetDbFileById?id={fileId}&type={sizeType}";
        }

        public static string GetUnSafeImgLink(this DbFileDto file, ImageSizeType sizeType = ImageSizeType.Original)
        {
            return CrocoApp.Application.FileCopyWorker.GetVirtualResizedImageLocalPath(file.Id, sizeType);
        }

        public static string GetLinkToDownload(this DbFile file, ImageSizeType sizeType = ImageSizeType.Original)
        {
            return GetLinkToDownload(file.Id, sizeType);
        }

        public static string GetLinkToDownload(this DbFileDto file, ImageSizeType sizeType = ImageSizeType.Original)
        {
            return file.Id.GetLinkToDownload(sizeType);
        }
    }
}