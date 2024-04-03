using Domain.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.User
{
    public interface IUserRepository:IGenericRepository<EntityModels.User>
    {
        Domain.DtoModels.User GetByUserNamePassword(LoginModel loginModel);
    }
}
