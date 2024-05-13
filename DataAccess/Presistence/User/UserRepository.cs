using DataAccess.Context;
using DataAccess.Presistence.Generic;
using Domain.DtoModels;
using Domain.DtoModels.DeleteAccount;
using Domain.DtoModels.Lookups;
using Domain.DtoModels.UserRegisteration;
using Domain.EntityModels;
using Domain.Repository.UnitOfWork;
using Domain.Repository.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Presistence.User
{
    public class UserRepository : GenericRepository<Domain.EntityModels.User>, IUserRepository
    {
        private readonly BoardSalesDbContext _boardSalesDbContext;
        private readonly IConfiguration _configuration;
        public UserRepository(BoardSalesDbContext boardSalesDbContext, IConfiguration configuration) : base(boardSalesDbContext)
        {
            _boardSalesDbContext = boardSalesDbContext;
            _configuration = configuration;
        }
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        public async Task<Domain.DtoModels.User> AddUser(RegisterUserRequest registerUserRequest)
        {
            var user = new Domain.DtoModels.User();
            try
            {
                string HashString = "";
                byte[] salt = new byte[keySize];

                var isUserExsist = _boardSalesDbContext.User.Where(x => x.UserName == registerUserRequest.UserName).FirstOrDefault();
                if (isUserExsist == null)
                {
                    if (registerUserRequest.Platform == "App")
                    {
                       salt = RandomNumberGenerator.GetBytes(keySize);
                        var hash = Rfc2898DeriveBytes.Pbkdf2(
                    Encoding.UTF8.GetBytes(registerUserRequest.Password),
                    salt,
                    iterations,
                    hashAlgorithm,
                    keySize);
                        HashString = Convert.ToHexString(hash);
                    }
                    var updateuser = new Domain.EntityModels.User()
                    {
                        Active = true,
                        DateAdded = DateTime.Now,
                        UserName = registerUserRequest.UserName,
                        Name = registerUserRequest.Name,
                        LocationName = "",
                        Location = 0,
                        ProfilePicture = registerUserRequest.ProfilePicture,
                        Verified = true,
                        VerificationCode = "",
                        LastLogin = DateTime.Now,

                    };

                    _boardSalesDbContext.User.Add(updateuser);
                    _boardSalesDbContext.SaveChanges();
                    var identity = new Identity()
                    {
                        UserId = updateuser.UserId,
                        Adapter = registerUserRequest.Platform,
                        Hash = HashString,
                        Salt = salt,
                        Status = true,
                        DateAdded = DateTime.Now
                    };
                    _boardSalesDbContext.Identity.Add(identity);
                    _boardSalesDbContext.SaveChanges();
                   user = UserModelResponse(updateuser);
                    user.Token = TokenGenerator(updateuser.UserName);
                }
                else
                {
                    user = UserModelResponse(isUserExsist);

                    user.Token = TokenGenerator(isUserExsist.UserName);
                }
            }
            catch (Exception ex)
            {

            }
            return user;
        }

        public async Task<Domain.DtoModels.User> UpdateUser(Domain.DtoModels.UpdateUser.UpdateUserRequest updateUserRequest, string path)
        {
            var user = new Domain.DtoModels.User();
            try
            {
               var dbUser = _boardSalesDbContext.User.Where(x => x.UserId == updateUserRequest.UserId).FirstOrDefault();
                var identity = _boardSalesDbContext.Identity.Where(x => x.UserId == dbUser.UserId).FirstOrDefault();
                if (updateUserRequest.FileName != null && updateUserRequest.FileName != "" && updateUserRequest.FileName != dbUser.ProfilePicture)
                {
                    string mystr = updateUserRequest.ProfilePicture.Replace("data:image/jpeg;base64,", string.Empty);
                    var testb = Convert.FromBase64String(mystr);
                    //var path = Path.Combine("@D:/Images","testb");
                    System.IO.File.WriteAllBytes(path, testb);
                }
                dbUser.Name = updateUserRequest.Name;
                dbUser.Location = updateUserRequest.Location;
                dbUser.Distance =Convert.ToInt32(updateUserRequest.Distance);
                dbUser.BoardType = updateUserRequest.BoardType;
                dbUser.BoardLength = updateUserRequest.BoardLength;
                dbUser.GetStartedCompleted = updateUserRequest.GetStartedCompleted;
                dbUser.ProfilePicture = updateUserRequest.FileName == null ? dbUser.ProfilePicture : updateUserRequest.FileName;
                user = UserModelResponse(dbUser);
                //if (identity.Adapter == "App")
                //{
                //    user.ProfilePicture = user.ProfilePicture != "" ? updateUserRequest.FileName + user.ProfilePicture : "";
                //}
                _boardSalesDbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                user.Code = "400";
                user.Message = ex.Message;
                return user;
            }
            return user;
        }

        public async Task<Domain.DtoModels.User> GetByUserNamePassword(LoginModel loginModel)
        {   
            var user = new Domain.DtoModels.User();
            var Dbuser = _boardSalesDbContext.User.Where(x => x.UserName == loginModel.UserName).FirstOrDefault();

            if (Dbuser != null)
            {

                if (Dbuser.Active)
                {
                    var identity = _boardSalesDbContext.Identity.Where(x => x.UserId == Dbuser.UserId).FirstOrDefault();
                    if (identity.Adapter == "App")
                    {
                        if (VerifyPassword(loginModel.Password, identity?.Hash, identity?.Salt))
                        {
                            user = UserModelResponse(Dbuser);
                            user.Token = TokenGenerator(Dbuser.UserName);
                            return user;
                        }
                        else
                        {
                            user.Code = "400";
                            user.Message = "Password didn't matched";
                        }
                    }
                    user.Token = TokenGenerator(Dbuser.UserName);
                    return user;
                }
                else
                {
                    user.Code = "400";
                    user.Message = "User is not active";
                    return user;
                }
            }
            else
            {
                var usrr = _boardSalesDbContext.User.ToList();
                user.Code = "400";
                user.Message = "User is not registered";
                return user;
            }
        }

        public async Task<Domain.DtoModels.User> GetUser(int userId)
        {
            var user = new Domain.DtoModels.User();
            var Dbuser = _boardSalesDbContext.User.Where(x => x.UserId == userId).FirstOrDefault();

            if (Dbuser != null)
            {
                if (Dbuser.Active)
                {
                   
                            user = UserModelResponse(Dbuser);
                            user.Token = TokenGenerator(Dbuser.UserName);
                            return user;
                }
                else
                {
                    user.Code = "400";
                    user.Message = "User is not active";
                    return user;
                }
            }
            else
            {
                user.Code = "400";
                user.Message = "User is not registered";
                return user;
            }
        }
        public async Task<UpdatePasswordReponse> UpdatePassword(UpdatePasswordRequest updatePasswordRequest)
        {
            var updatePasswordReponse = new UpdatePasswordReponse();
            try
            {
                var queryUser =
                    from user in _boardSalesDbContext.User
                    join identity in _boardSalesDbContext.Identity on user.UserId equals identity.UserId
                    where identity.UserId == updatePasswordRequest.UserId
                    select identity;
                var identityUser = queryUser.FirstOrDefault();
               // var user = _boardSalesDbContext.User.Where(x => x.UserId == updatePasswordRequest.UserId).FirstOrDefault();
                if (queryUser != null && identityUser?.Adapter == "App")
                {

                    byte[] salt = RandomNumberGenerator.GetBytes(keySize);
                    var hash = Rfc2898DeriveBytes.Pbkdf2(
                    Encoding.UTF8.GetBytes(updatePasswordRequest.Password),
                    salt,
                    iterations,
                    hashAlgorithm,
                    keySize);
                    string HashString = Convert.ToHexString(hash);
                    identityUser.Salt = salt;
                    identityUser.Hash = HashString;
                    _boardSalesDbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
            return updatePasswordReponse;
        }

        public async Task<DeleteAccountResponse> DeleteAccount(DeleteAccountRequest updatePasswordRequest)
        {
            var updatePasswordReponse = new DeleteAccountResponse();
            try
            {
               
                 var user = _boardSalesDbContext.User.Where(x => x.UserId == updatePasswordRequest.UserId).FirstOrDefault();
                if (user != null)
                {
                    user.Active = false;
                    _boardSalesDbContext.SaveChanges();
                    updatePasswordReponse.Code = "200";
                    updatePasswordReponse.Message = "Account Deleted Successfully";
                }
            }
            catch (Exception ex)
            {
                updatePasswordReponse.Code = "400";
                updatePasswordReponse.Message = ex.Message;
            }
            return updatePasswordReponse;
        }
        #region private
        private Domain.DtoModels.User UserModelResponse(Domain.EntityModels.User user)
        {
            return new Domain.DtoModels.User {
                UserId = user.UserId,
                UserName = user.UserName,
                Name = user.Name,
                LocationName = user.LocationName,
                Location = user.Location,
                ProfilePicture = user.ProfilePicture,
                GetStartedCompleted = user.GetStartedCompleted,
                Distance = user.Distance,
                BoardType = user.BoardType,
                BoardLength = user.BoardLength

            };

        }

        private Domain.EntityModels.User DtoModelToEntityModel(Domain.DtoModels.User user)
        {
            return new Domain.EntityModels.User
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Name = user.Name,
                LocationName = user.LocationName,
                Location = user.Location,
                ProfilePicture = user.ProfilePicture,
                GetStartedCompleted = user.GetStartedCompleted,
                Distance = user.Distance,

            };

        }
        private bool VerifyPassword(string password, string? hash, byte[]? salt)
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
        private string TokenGenerator(string user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var sigingkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var signingCredentials = new SigningCredentials(sigingkey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issue"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Task<LooksResponse> GetLookups()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
