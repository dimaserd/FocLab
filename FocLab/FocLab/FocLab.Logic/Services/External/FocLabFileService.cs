using Croco.Core.Contract;
using Croco.Core.Contract.Application;
using Croco.Core.Contract.Models;
using FocLab.Model.External;
using System.Linq;
using System.Threading.Tasks;

namespace FocLab.Logic.Services.External
{
    /// <summary>
    /// Сервис для работы с файлами
    /// </summary>
    public class FocLabFileService : FocLabService
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context"></param>
        /// <param name="application"></param>
        public FocLabFileService(ICrocoAmbientContextAccessor context, ICrocoApplication application) : base(context, application)
        {
        }

        /// <summary>
        /// Создать файлы
        /// </summary>
        /// <param name="fileIds"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> CreateFiles(int[] fileIds)
        {
            if (fileIds == null)
            {
                return new BaseApiResponse(false, "fileIds is null");
            }

            var files = fileIds.Select(x => new FocLabDbFile
            {
                Id = x
            });

            CreateHandled(files);

            return await TrySaveChangesAndReturnResultAsync("Ok");
        }
    }
}
