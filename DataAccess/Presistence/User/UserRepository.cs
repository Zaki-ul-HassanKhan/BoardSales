using DataAccess.Context;
using DataAccess.Presistence.Generic;
using Domain.DtoModels;
using Domain.EntityModels;
using Domain.Repository.UnitOfWork;
using Domain.Repository.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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
                        Location = "",
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

        public async Task<Domain.DtoModels.User> UpdateUser(Domain.DtoModels.User registerUserRequest, string path)
        {
            var user = new Domain.DtoModels.User();
            try
            {
                string mystr = registerUserRequest.ProfilePicture.Replace("data:image/jpeg;base64,", string.Empty);
                var testb = Convert.FromBase64String(mystr);
                //var path = Path.Combine("@D:/Images","testb");
                System.IO.File.WriteAllBytes(path, testb);
            }
            catch (Exception ex)
            {

            }
            return user;
        }

        Domain.DtoModels.User IUserRepository.GetByUserNamePassword(LoginModel loginModel)
        {   
            var user = new Domain.DtoModels.User();
            var Dbuser = _boardSalesDbContext.User.Where(x => x.UserName == loginModel.UserName).Select(x => new Domain.DtoModels.User
            {
                UserId = x.UserId,
                UserName = x.UserName,
                Name = x.Name,
                LocationName = x.LocationName,
                Location = x.Location,
                ProfilePicture = x.ProfilePicture

            }).FirstOrDefault();

            if (Dbuser != null)
            {
                var identity = _boardSalesDbContext.Identity.Where(x => x.UserId == Dbuser.UserId).FirstOrDefault();
                if (identity.Adapter == "App")
                {
                    if (VerifyPassword(loginModel.Password, identity?.Hash, identity?.Salt))
                    {
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
                user.Message = "User is not registered";
                return user;
            }
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
                ProfilePicture = user.ProfilePicture

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
        #endregion
    }
}
