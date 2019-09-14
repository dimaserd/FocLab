using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Abstractions;
using Croco.Core.Common.Models;
using Croco.Core.Logic.Models.Files;
using Croco.Core.Logic.Workers;
using FocLab.Logic.EntityDtos;
using FocLab.Logic.Extensions;
using FocLab.Logic.Settings.Statics;
using FocLab.Model.Entities;
using FocLab.Model.Enumerations;
using Hangfire;
using Croco.Core.Search.Models;
using Croco.Core.Implementations.AmbientContext;

namespace FocLab.Logic.Workers
{
    public class DbFileWorker : BaseCrocoWorker
    {
        public ApplicationFileManager BaseManager => new ApplicationFileManager(AmbientContext.RepositoryFactory);

        public DbFileWorker(ICrocoAmbientContext context) : base(context)
        {

        }

        public Task<GetListResult<DbFileDto>> GetFiles(GetListSearchModel model)
        {
            var initQuery = GetRepository<DbFile>().Query().OrderByDescending(x => x.CreatedOn);

            return GetListResult<DbFileDto>.GetAsync(model, initQuery, DbFileDto.SelectExpression);
        }

        public async Task<BaseApiResponse> ReloadFileAsync(int fileId, IFileData httpFile)
        {
            if (!User.IsAdmin() && !User.HasRight(UserRight.Developer))
            {
                return new BaseApiResponse(false, "Чтобы перезагружать файлы вы должны обладать правами администратора или разработчика");
            }

            return await TryExecuteCodeAndReturnSuccessfulResultAsync(async () =>
            {
                await BaseManager.ReloadFileAsync(fileId, httpFile);

                return new BaseApiResponse(true, "Содержимое файла заменено");
            });
        }

        public async Task<BaseApiResponse<int[]>> UploadFilesAsync(IEnumerable<IFileData> httpFiles)
        {
            //Добавляем функцию отложенной загрузки файлов
            var arrayOfIds = await BaseManager.UploadFilesAsync(httpFiles.Where(x => x.IsGoodFile()), x => Task.CompletedTask);

            //Отложенно делаем копии файлов
            BackgroundJob.Enqueue(() => GetJobToMakeLocalCopies(arrayOfIds.Length));

            if (arrayOfIds.Length == 0)
            {
                return new BaseApiResponse<int[]>(false, "Файлы не выбраны");
            }

            return new BaseApiResponse<int[]>(true, "Файлы загружены на сервер", arrayOfIds);
        }

        public static Task GetJobToMakeLocalCopies(int filesCount)
        {
            var wrapper = new SystemCrocoAmbientContext();

            return new ApplicationFileManager(wrapper.RepositoryFactory).MakeLocalCopies(filesCount);
        }

        public Task<DbFileIntIdModelNoData[]> GetFilesThatAreNotOnLocalMachineAsync()
        {
            return BaseManager.GetFilesThatAreNotOnLocalMachine();
        }
    }
}
