using Domain.DtoModels.UserBoards;
using Domain.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.UserBoard
{
    public interface IUserBoardRepository : IGenericRepository<UserBoards>
    {
        Task<UserBoardsResponse> AddUpdateUserBoard(AddUpdateUserBoard userBoardsResponse, string path);
    }
}
