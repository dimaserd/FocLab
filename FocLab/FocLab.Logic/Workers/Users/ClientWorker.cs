using System;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Abstractions;
using Croco.Core.Models;
using FocLab.Logic.Implementations;
using FocLab.Logic.Models.Users;
using FocLab.Logic.Resources;
using FocLab.Logic.Settings.Statics;
using FocLab.Model.Entities.Users.Default;
using Microsoft.EntityFrameworkCore;

namespace FocLab.Logic.Workers.Users
{
    public class ClientWorker : FocLabWorker
    {
        private readonly Func<ApplicationUser, Task> _refreshUserDataFunc;

        public async Task<BaseApiResponse> UpdateClientPhotoAsync(int fileId)
        {
            if (!IsAuthenticated)
            {
                return new BaseApiResponse(false, ValidationMessages.YouAreNotAuthorized);
            }

            var id = UserId;

            var userRepo = GetRepository<ApplicationUser>();
            
            var userToEditEntity = await userRepo.Query().FirstOrDefaultAsync(x => x.Id == id);

            if (userToEditEntity == null)
            {
                return new BaseApiResponse(false, ValidationMessages.UserNotFound);
            }

            var fileManager = new ApplicationFileManager(AmbientContext.RepositoryFactory);

            var file = await fileManager.LocalStorageService.GetFilesQueryable().Select(x => new { x.Id, x.FileName })
                .FirstOrDefaultAsync(x => x.Id == fileId);

            if (file == null)
            {
                return new BaseApiResponse(false, DbFileValidationMessages.FileIsNotFoundById);
            }

            if (!FocLabWebApplication.IsImage(file.FileName))
            {
                return new BaseApiResponse(false, DbFileValidationMessages.FileIsNotImage);
            }

            userToEditEntity.AvatarFileId = fileId;

            userRepo.UpdateHandled(userToEditEntity);

            return await TryExecuteCodeAndReturnSuccessfulResultAsync(async () =>
            {
                await RepositoryFactory.SaveChangesAsync();
                await _refreshUserDataFunc(userToEditEntity);

                return new BaseApiResponse(true, ClientResource.ClientAvatarUpdated);
            });
        }

        public async Task<BaseApiResponse> EditUserAsync(EditClient model)
        {
            if (!IsAuthenticated)
            {
                return new BaseApiResponse(false, ValidationMessages.YouAreNotAuthorized);
            }

            var validation = ValidateModel(model);

            if (!validation.IsSucceeded)
            {
                return validation;
            }

            model.PhoneNumber = new string(model.PhoneNumber.Where(char.IsDigit).ToArray());
            
            var id = UserId;

            var userRepo = GetRepository<ApplicationUser>();

            if (await userRepo.Query().AnyAsync(x => x.Id != id && x.PhoneNumber == model.PhoneNumber))
            {
                return new BaseApiResponse(false, ValidationMessages.ThisPhoneNumberIsAlreadyTakenByOtherUser);
            }

            var userToEditEntity = await userRepo.Query().FirstOrDefaultAsync(x => x.Id == id);

            if (userToEditEntity == null)
            {
                return new BaseApiResponse(false, ValidationMessages.UserNotFound);
            }

            if (userToEditEntity.Email == RightsSettings.RootEmail)
            {
                return new BaseApiResponse(false, "Root не может редактировать сам себя");
            }

            
            userToEditEntity.Name = model.Name;
            userToEditEntity.Surname = model.Surname;
            userToEditEntity.Patronymic = model.Patronymic;
            userToEditEntity.Sex = model.Sex;
            userToEditEntity.PhoneNumber = model.PhoneNumber;
            userToEditEntity.BirthDate = model.BirthDate;

            userRepo.UpdateHandled(userToEditEntity);

            return await TryExecuteCodeAndReturnSuccessfulResultAsync(async () =>
            {
                await SaveChangesAsync();

                await _refreshUserDataFunc(userToEditEntity);

                return new BaseApiResponse(true, ClientResource.ClientDataRenewed);
            });
        }
        
        public async Task<BaseApiResponse<ClientModel>> GetUserAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return new BaseApiResponse<ClientModel>(false, ValidationMessages.YouAreNotAuthorized);
            }

            return await GetClientByIdAsync(UserId);
        }

        public async Task<BaseApiResponse<ClientModel>> GetClientByIdAsync(string id)
        {
            var repo = GetRepository<ApplicationUser>();

            var model = await repo.Query().FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
            {
                return new BaseApiResponse<ClientModel>(false, ValidationMessages.UserNotFound);
            }

            return new BaseApiResponse<ClientModel>(true, ValidationMessages.UserFound, new ClientModel
            {
                Email = model.Email,
                Name = model.Name,
                Surname = model.Surname,
                Patronymic = model.Patronymic,
                Sex = model.Sex,
                PhoneNumber = model.PhoneNumber,
                BirthDate = model.BirthDate,
                AvatarFileId = model.AvatarFileId
            });
        }

        public ClientWorker(ICrocoAmbientContext context, Func<ApplicationUser, Task> refreshUserDataFunc) : base(context)
        {
            _refreshUserDataFunc = refreshUserDataFunc;
        }
    }
}