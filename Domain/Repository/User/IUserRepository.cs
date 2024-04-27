using Domain.DtoModels;
using Domain.DtoModels.DeleteAccount;
using Domain.DtoModels.Lookups;
using Domain.DtoModels.UpdateUser;
using Domain.DtoModels.UserRegisteration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.User
{
    public interface IUserRepository:IGenericRepository<EntityModels.User>
    {
        Task<DtoModels.User> GetByUserNamePassword(LoginModel loginModel);
        Task<DtoModels.User> AddUser(RegisterUserRequest registerUserRequest);
        Task<DtoModels.User> UpdateUser(UpdateUserRequest registerUserRequest, string path);
        Task<UpdatePasswordReponse> UpdatePassword(UpdatePasswordRequest updatePasswordRequest);
        Task<DeleteAccountResponse> DeleteAccount(DeleteAccountRequest updatePasswordRequest);
        Task<DtoModels.User> GetUser(int userId);
        Task<LooksResponse> GetLookups();
    }
}
