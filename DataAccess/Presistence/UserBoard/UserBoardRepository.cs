using DataAccess.Context;
using DataAccess.Presistence.Generic;
using Domain.DtoModels.UpdateUser;
using Domain.DtoModels.UserBoards;
using Domain.EntityModels;
using Domain.Repository.UserBoard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Presistence.UserBoard
{
    public class UserBoardRepository : GenericRepository<UserBoards>, IUserBoardRepository
    {
        private readonly BoardSalesDbContext _boardSalesDbContext;
        public UserBoardRepository(BoardSalesDbContext boardSalesDbContext) : base(boardSalesDbContext)
        {
            _boardSalesDbContext = boardSalesDbContext;
        }

        public async Task<UserBoardsResponse> AddUpdateUserBoard(AddUpdateUserBoard request)
        {
            var response = new UserBoardsResponse();
            var userBoard = new UserBoards();
            try
            {
                for (int i = 0; i < request?.ImagesPath.Count(); i++)
                {
                    userBoard.ImagesPath += i.ToString() + ",";
                    string mystr = request.ImagesPath[i].Replace("data:image/jpeg;base64,", string.Empty);
                    var testb = Convert.FromBase64String(mystr);
                    var filename = Path.Combine(i.ToString(), request.FileName);
                    System.IO.File.WriteAllBytes(filename, testb);
                }
                userBoard.Title = request?.Title;
                userBoard.Condition = request?.Condition;
                userBoard.BoardType = request?.BoardType;
                userBoard.BoardShapers = request?.BoardShapers;
                userBoard.FinSystem = request?.FinSystem;
                userBoard.FinSetup = request?.FinSetup;
                userBoard.SurfCraftType = request?.SurfCraftType;
                userBoard.SurfCraftWeight = request?.SurfCraftWeight;
                userBoard.Length = request?.Length;
                userBoard.Width = request?.Width;
                userBoard.Thickness = request?.Thickness;
                userBoard.Volume = request?.Volume;
                userBoard.Description = request?.Description;
                userBoard.Price = request?.Price;
                userBoard.ConsiderSwap = request?.ConsiderSwap;
                userBoard.Location = request?.Location;
                userBoard.IsFeatured = request?.IsFeatured;
                userBoard.TeamBoard = request?.TeeamBoard;
                userBoard.Vintage = request?.Vintage;
                userBoard.DateAdded = DateTime.Now;
                userBoard.IsPosted = request?.IsPosted;
                userBoard.IsSold = false;
                userBoard.Active = true;
                userBoard.UserId = request.UserId;
                _boardSalesDbContext.UserBoards.Add(userBoard);
                _boardSalesDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                response.Code = "400";
                response.Message = ex.Message;
            }
            finally
            {
                response.Code = "200";
                response.Message = "Data Saved Successfully";
            }
            return response;
        }
    }
}
