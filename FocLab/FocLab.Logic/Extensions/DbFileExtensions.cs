using System.IO;
using Croco.Core.Application;
using Croco.Core.Common.Enumerations;
using FocLab.Logic.EntityDtos;
using FocLab.Logic.Implementations;
using FocLab.Logic.Settings.Statics;
using FocLab.Model.Entities;

namespace FocLab.Logic.Extensions
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

        public static BaseApiResponse CheckFile(this IFileData file)
        {
            if (file.IsGoodFile())
            {
                return new BaseApiResponse(true, "Файл допущен к загрузке");
            }

            var ext = Path.GetExtension(file.FileName);

            return new BaseApiResponse(false, $"Файл с разрешением {ext} не допущен к загрузке");
        }

        public static BaseApiResponse<DbFile> ToDbFile(this IFileData file)
        {
            var checkResult = CheckFile(file);

            if (!checkResult.IsSucceeded)
            {
                return new BaseApiResponse<DbFile>(checkResult);
            }

            return new BaseApiResponse<DbFile>(checkResult.IsSucceeded, checkResult.Message, new DbFile
            {
                Id = 0,
                Data = file.Data,
                FileName = file.FileName,
                FilePath = ""
            });
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
