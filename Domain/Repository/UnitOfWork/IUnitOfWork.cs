using Domain.Repository.BoardType;
using Domain.Repository.FinSetup;
using Domain.Repository.FinSystem;
using Domain.Repository.Location;
using Domain.Repository.Product;
using Domain.Repository.Shapers;
using Domain.Repository.User;
using Domain.Repository.UserBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IProductRepository ProductRepository { get; }
        ILocationRepository LocationRepository { get; }
        IBoardTypeRepository BoardTypeRepository { get; }
        IUserBoardRepository UserBoardRepository { get; }
        IShapersRepository ShapersRepository { get; }
        IFinSystemRepository FinSystemRepository { get; }
        IFinSetupRepository FinSetupRepository { get; }
        int Save();
    }
}
