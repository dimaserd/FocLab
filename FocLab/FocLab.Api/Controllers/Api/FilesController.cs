using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Common.Models;
using Croco.Core.Logic.Models.Files;
using FocLab.Api.Controllers.Base;
using FocLab.Api.Models;
using FocLab.Logic.Extensions;
using FocLab.Logic.Services;
using FocLab.Logic.Workers;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Api.Controllers.Api
{
    /// <inheritdoc />
    /// <summary>
    /// Апи-контроллер предоставляющий методы для работы с локальными файлами
    /// </summary>
    [Route("Api/FilesDirectory")]
    public class FilesController : BaseApiController
    {
        /// <inheritdoc />
        public FilesController(ChemistryDbContext context, ApplicationSignInManager signInManager, ApplicationUserManager userManager, IHttpContextAccessor httpContextAccessor) : base(context, signInManager, userManager, httpContextAccessor)
        {
        }

        private DbFileWorker FileWorker => new DbFileWorker(ContextWrapper);


        /// <summary>
        /// Перезагрузить содержимое файла
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(BaseApiResponse<int[]>))]
        [HttpPost(nameof(ReloadFileData))]
        public Task<BaseApiResponse> ReloadFileData(ReloadFileData model)
        {
            var data = model.FileData ?? Request.Form.Files.FirstOrDefault();

            return FileWorker.ReloadFileAsync(model.FileId, data.ToFileData());
        }

        /// <summary>
        /// Загрузка файла на сервер
        /// </summary>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(BaseApiResponse<int[]>))]
        [HttpPost("UploadFiles")]
        public async Task<BaseApiResponse<int[]>> UploadFiles()
        {
            try
            {
                var files = new List<IFormFile>();

                foreach (var file in Request.Form.Files)
                {
                    files.Add(file);
                }

                //TODO Implement Files Upload
                return await FileWorker.UploadFilesAsync(files.Select(x => x.ToFileData()));
            }
            catch(Exception ex)
            {
                return new BaseApiResponse<int[]>(false, ex.Message);
            }
        }

        /// <summary>
        /// Получить файлы которых нет на локальной машине
        /// </summary>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(DbFileIntIdModelNoData[]))]
        [HttpPost("Local/GetThatAreNotOnMachine")]
        public Task<DbFileIntIdModelNoData[]> GetNotCopiedFileIds()
        {
            return FileWorker.GetFilesThatAreNotOnLocalMachineAsync();
        }

        /// <summary>
        /// Запустить несколько операций копирования
        /// </summary>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        [HttpPost("Local/CopySomeFiles")]
        public Task<BaseApiResponse> CopyFilesThatAreNotOnMachine(int count)
        {
            return FileWorker.BaseManager.MakeLocalCopies(count);
        }
    }
}
