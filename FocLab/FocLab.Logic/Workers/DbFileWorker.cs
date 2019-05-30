using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Abstractions;
using Croco.Core.Abstractions.ContextWrappers;
using Croco.Core.Application;
using Croco.Core.Common.Models;
using Croco.Core.ContextWrappers;
using Croco.Core.Logic.Models.Files;
using Croco.Core.Logic.Workers;
using Croco.Core.Principal;
using Croco.Core.Search;
using FocLab.Logic.Extensions;
using FocLab.Logic.Settings;
using FocLab.Model.Contexts;
using FocLab.Model.Entities;
using FocLab.Model.Enumerations;
using Hangfire;

namespace FocLab.Logic.Workers
{
    public class DbFileWorker : BaseCrocoWorker
    {
        public ApplicationFileManager BaseManager => new ApplicationFileManager(ContextWrapper);

        public DbFileWorker(IUserRequestWithRepositoryFactory contextWrapper) : base(contextWrapper)
        {

        }

        public async Task<GetListResult<DbFileDto>> GetFiles(GetListSearchModel model)
        {
            if (!User.IsAdmin())
            {
                return null;
            }

            var initQuery = GetRepository<DbFile>().Query().OrderByDescending(x => x.CreatedOn);

            var result = new GetListResult<DbFileDto>();

            await result.GetResultAsync(model, initQuery, DbFileDto.SelectExpression);

            return result;
        }

        public async Task<BaseApiResponse<DbFileIntIdModelNoData>> UploadUserFileAsync(IFileData model)
        {
            if (!IsAuthenticated)
            {
                return new BaseApiResponse<DbFileIntIdModelNoData>(false, "Не авторизованные пользователи не могут загружать файлы");
            }

            var fileWithResult = model.ToDbFile();

            if (!fileWithResult.IsSucceeded)
            {
                return new BaseApiResponse<DbFileIntIdModelNoData>(false, fileWithResult.Message);
            }

            var fileIds = await BaseManager.UploadFilesAsync(new[] { model });

            return await TrySaveChangesAndReturnResultAsync($"Пользовательский файл создан и ему присвоен №{fileIds.First()}", new DbFileIntIdModelNoData { Id = fileIds.First() });
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
            var systemPrincipal = new SystemPrincipal();

            var wrapper = new UserContextWrapper<ChemistryDbContext>(systemPrincipal, CrocoApp.Application.GetChemistryDbContext(), SystemPrincipal.GetSystemId);

            return new ApplicationFileManager(wrapper).MakeLocalCopies(filesCount);
        }

        public Task<BaseApiResponse<int[]>> UploadFilesAsImagesAsync(IEnumerable<IFileData> httpFiles)
        {
            return UploadFilesAsync(httpFiles.Where(x => x.IsImage()));
        }

        public Task<DbFileIntIdModelNoData[]> GetFilesThatAreNotOnLocalMachineAsync()
        {
            return BaseManager.GetFilesThatAreNotOnLocalMachine();
        }

        
    }
}
