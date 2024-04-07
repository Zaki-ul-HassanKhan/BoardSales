using DataAccess.Context;
using DataAccess.Presistence.Generic;
using Domain.DtoModels;
using Domain.EntityModels;
using Domain.Repository.UnitOfWork;
using Domain.Repository.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Presistence.User
{
    public class UserRepository : GenericRepository<Domain.EntityModels.User>, IUserRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BoardSalesDbContext _boardSalesDbContext;
        public UserRepository(BoardSalesDbContext boardSalesDbContext, IUnitOfWork unitOfWork) : base(boardSalesDbContext)
        {
            _boardSalesDbContext = boardSalesDbContext;
            _unitOfWork = unitOfWork;
        }
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        public async Task<RegisterUserRequest> AddUser(RegisterUserRequest registerUserRequest)
        {
            var user = new RegisterUserRequest();
            byte[] salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
        Encoding.UTF8.GetBytes(registerUserRequest.Password),
        salt,
        iterations,
        hashAlgorithm,
        keySize);
          string  HashString = Convert.ToHexString(hash);
            var username = registerUserRequest.UserName;
            var updateuser = new Domain.EntityModels.User()
            {
                Active = true,
                DateAdded = DateTime.Now,
                UserName = username,
                Name = "",
                LocationName = "",
                Location = "",
                ProfilePicture = "",
                Verified = true,
                VerificationCode = "",
                LastLogin = DateTime.Now,

            };

            _boardSalesDbContext.User.Add(updateuser);
            _boardSalesDbContext.SaveChanges();
            int id = updateuser.UserId;
            var identity = new Identity()
            {
                UserId = id,
                Adapter = "App",
                Hash = HashString,
                Salt = salt,
                Status = true,
                DateAdded = DateTime.Now
            };
            _boardSalesDbContext.Identity.Add(identity);
            _boardSalesDbContext.SaveChanges();
            return user;
        }

        bool IUserRepository.GetByUserNamePassword(LoginModel loginModel)
        {
            var user = new Domain.DtoModels.User();
            user = _boardSalesDbContext.User.Where(x => x.UserName == loginModel.UserName).Select(x => new Domain.DtoModels.User
            {
                UserName = x.UserName,
                UserId = x.UserId
            }).FirstOrDefault();

            if (user != null)
            {
                var identity = _boardSalesDbContext.Identity.Where(x => x.UserId == user.UserId).FirstOrDefault();
                if (identity.Adapter == "App")
                {
                    if (VerifyPassword(loginModel.Password, identity?.Hash, identity?.Salt))
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {

                return false;
            }
        }
        bool VerifyPassword(string password, string? hash, byte[]? salt)
        {
            if (hash == null || salt == null)
            {
                return false;
            }
            else
            {
                var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
                return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
            }
        }
    }
}
