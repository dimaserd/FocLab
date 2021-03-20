using Croco.Core.Logic.Files.Abstractions;
using System.Threading.Tasks;

namespace FocLab.Logic.Jobs.TaskGivers
{
    public class RestoreSomeFilesTaskGiver
    {
        readonly int _countSetting = 15;

        IDbFileManager FileManager { get; }

        public RestoreSomeFilesTaskGiver(IDbFileManager fileManager)
        {
            FileManager = fileManager;
        }
        
        public Task GetTask()
        {
            return FileManager.LocalStorageService.MakeLocalCopies(_countSetting, true);
        }
    }
}