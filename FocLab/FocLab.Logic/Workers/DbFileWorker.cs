﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FocLab.Logic.EntityDtos;
using FocLab.Logic.Extensions;
using FocLab.Logic.Settings.Statics;
using FocLab.Model.Entities;
using FocLab.Model.Enumerations;
using FocLab.Logic.Implementations;
using Croco.Core.Contract.Models.Search;
using Croco.Core.Contract;
using Croco.Core.Contract.Application;
using Croco.Core.Logic.Files.Abstractions;
using Croco.Core.Search.Extensions;
using Croco.Core.Contract.Files;
using Croco.Core.Contract.Models;
using Croco.Core.Logic.Files.Models;

namespace FocLab.Logic.Workers
{
    public class DbFileWorker : FocLabWorker
    {
        IDbFileManager FileManager { get; }

        public DbFileWorker(ICrocoAmbientContextAccessor context,
            ICrocoApplication application,
            IDbFileManager fileManager) : base(context, application)
        {
            FileManager = fileManager;
        }

        public Task<GetListResult<DbFileDto>> GetFiles(GetListSearchModel model)
        {
            var initQuery = GetRepository<DbFile>().Query().OrderByDescending(x => x.CreatedOn);

            return EFCoreExtensions.GetAsync(model, initQuery, DbFileDto.SelectExpression);
        }

        public async Task<BaseApiResponse> ReloadFileAsync(int fileId, IFileData httpFile)
        {
            if (!User.IsAdmin() && !User.HasRight(UserRight.Developer))
            {
                return new BaseApiResponse(false, "Чтобы перезагружать файлы вы должны обладать правами администратора или разработчика");
            }

            return await TryExecuteCodeAndReturnSuccessfulResultAsync(async () =>
            {
                await FileManager.ReloadFileAsync(fileId, httpFile);

                return new BaseApiResponse(true, "Содержимое файла заменено");
            });
        }

        public async Task<BaseApiResponse<int[]>> UploadFilesAsync(IEnumerable<IFileData> httpFiles)
        {
            //Добавляем функцию отложенной загрузки файлов
            var arrayOfIds = await FileManager.UploadFilesAsync(httpFiles.Where(x => x.IsGoodFile()));

            if (arrayOfIds.Length == 0)
            {
                return new BaseApiResponse<int[]>(false, "Файлы не выбраны");
            }

            return new BaseApiResponse<int[]>(true, "Файлы загружены на сервер", arrayOfIds);
        }
    }
}