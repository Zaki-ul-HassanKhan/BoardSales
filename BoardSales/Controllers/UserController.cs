using Domain.DtoModels;
using Domain.DtoModels.DeleteAccount;
using Domain.DtoModels.Location;
using Domain.DtoModels.Lookups;
using Domain.DtoModels.UpdateUser;
using Domain.DtoModels.UserRegisteration;
using Domain.Repository.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BoardSales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IWebHostEnvironment _environment;
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _environment = env;
        }
        // GET: api/<UserController>
        [HttpGet]

        public ActionResult Get()
        {
            var users = _unitOfWork.UserRepository.GetAll();
            return Ok(users);
        }

        [Route("GetLookups")]
        [HttpGet]
        public ActionResult GetLookups()
        {
            var lookups = new LooksResponse();
            lookups.Locations = new List<ListItem<int, string>>(
                _unitOfWork.LocationRepository.GetAll().Select(x => new ListItem<int, string>
                {
                    Key = x.Id,
                    Value = x.LocationName,
                }));
            lookups.BoardTypes = new List<ListItem<int, string>>(
                _unitOfWork.BoardTypeRepository.GetAll().Select(x => new ListItem<int, string>
                {
                    Key = x.Id,
                    Value = x.BoardTypeName
                }
                ));
                    lookups.Shapers = new List<ListItem<int, string>>(
            _unitOfWork.ShapersRepository.GetAll().Select(x => new ListItem<int, string>
            {
                Key = x.Id,
                Value = x.ShaperName
            }
            ));
                    lookups.FinSetup = new List<ListItem<int, string>>(
            _unitOfWork.FinSetupRepository.GetAll().Select(x => new ListItem<int, string>
            {
                Key = x.Id,
                Value = x.FinSteupName
            }
            )); lookups.FinSystem = new List<ListItem<int, string>>(
            _unitOfWork.FinSystemRepository.GetAll().Select(x => new ListItem<int, string>
            {
                Key = x.Id,
                Value = x.FinSystemName
            }
            ));
            return Ok(lookups);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var users = _unitOfWork.UserRepository.GetUser(id);
            return Ok(users);
        }

        // POST api/<UserController>
        [HttpPost]
        public ActionResult AddUser(RegisterUserRequest value)
        {
            var users = _unitOfWork.UserRepository.AddUser(value);
            return Ok(users.Result);
        }
        [Route("UpdateUser")]
        [HttpPost]
        public ActionResult UpdateUser(UpdateUserRequest value)
        {
            var contentPath = "";
            if (value.ProfilePicture != null && value.ProfilePicture != "")
            {

                contentPath = _environment.WebRootPath + "/Uploads/ProfilePicture/";
                value.FileName = value.UserId + ".jpg";
                //path = Path.Combine(contentPath, "ProfilePictures/");
                //  path = Path.Combine(path, value.FileName);
                if (!Directory.Exists(contentPath))
                {
                    Directory.CreateDirectory(contentPath);
                }
                contentPath = Path.Combine(contentPath, value.FileName);
            }
            var users = _unitOfWork.UserRepository.UpdateUser(value, contentPath);
            return Ok(users.Result);
        }

        [Route("UpdatePassword")]
        [HttpPost]
        public ActionResult UpdatePassword(UpdatePasswordRequest value)
        {

            var users = _unitOfWork.UserRepository.UpdatePassword(value);
            return Ok(users.Result);
        }

        [Route("DeleteAccount")]
        [HttpPost]
        public ActionResult DeleteAccount(DeleteAccountRequest value)
        {

            var users = _unitOfWork.UserRepository.DeleteAccount(value);
            return Ok(users.Result);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
