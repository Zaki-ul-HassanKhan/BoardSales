using DataAccess.Context;
using DataAccess.Presistence.Generic;
using Domain.DtoModels;
using Domain.Repository.UnitOfWork;
using Domain.Repository.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Presistence.User
{
    public class UserRepository : GenericRepository<Domain.EntityModels.User>, IUserRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BoardSalesDbContext _boardSalesDbContext;
        public UserRepository(BoardSalesDbContext boardSalesDbContext) : base(boardSalesDbContext)
        {
            _boardSalesDbContext = boardSalesDbContext;
        }

        Domain.DtoModels.User IUserRepository.GetByUserNamePassword(LoginModel loginModel)
        {
            var user = new Domain.DtoModels.User();
            user = _boardSalesDbContext.User.Where(x => x.UserName == loginModel.UserName).Select(x => new Domain.DtoModels.User
            {
                UserName = x.UserName
            }).FirstOrDefault();

            return user;
        }
    }
}
