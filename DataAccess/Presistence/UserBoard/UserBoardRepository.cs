using DataAccess.Context;
using DataAccess.Presistence.Generic;
using Domain.DtoModels.UserBoards;
using Domain.EntityModels;
using Domain.Repository.UserBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Presistence.UserBoard
{
    public class UserBoardRepository : GenericRepository<UserBoards>, IUserBoardRepository
    {
        public UserBoardRepository(BoardSalesDbContext boardSalesDbContext) : base(boardSalesDbContext)
        {
        }

        public Task<UserBoardsResponse> AddUpdateUserBoard(AddUpdateUserBoard userBoardsResponse, string path)
        {
            throw new NotImplementedException();
        }
    }
}
