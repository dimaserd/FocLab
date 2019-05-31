using System;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Abstractions.ContextWrappers;
using Croco.Core.Common.Models;
using FocLab.Logic.Extensions;
using FocLab.Logic.Models.Users;
using FocLab.Logic.Resources;
using FocLab.Logic.Settings;
using FocLab.Logic.Settings.Statics;
using FocLab.Model.Contexts;
using FocLab.Model.Entities.Users.Default;
using Microsoft.EntityFrameworkCore;

namespace FocLab.Logic.Workers.Users
{
    public class ClientWorker : BaseChemistryWorker
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

            var fileManager = new ApplicationFileManager(ApplicationContextWrapper);

            var file = await fileManager.GetFilesQueryable().FirstOrDefaultAsync(x => x.Id == fileId);

            if (file == null)
            {
                return new BaseApiResponse(false, DbFileValidationMessages.FileIsNotFoundById);
            }

            if (!file.IsImage())
            {
                return new BaseApiResponse(false, DbFileValidationMessages.FileIsNotImage);
            }

            userToEditEntity.AvatarFileId = fileId;

            return await TryExecuteCodeAndReturnSuccessfulResultAsync(async () =>
            {
                await ContextWrapper.SaveChangesAsync();
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

            var userRepo = ContextWrapper.GetRepository<ApplicationUser>();

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
                await ContextWrapper.SaveChangesAsync();

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

        public ClientWorker(IUserContextWrapper<ChemistryDbContext> contextWrapper, Func<ApplicationUser, Task> refreshUserDataFunc) : base(contextWrapper)
        {
            _refreshUserDataFunc = refreshUserDataFunc;
        }
    }
}
