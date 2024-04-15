using Domain.DtoModels;
using Domain.Repository.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
        public ActionResult UpdateUser(Domain.DtoModels.User value)
        {
            var fileWithPath = "";
            if (value.ProfilePicture != null) {

                var contentPath = _environment.ContentRootPath;
                var newFileName = value.UserId + ".jpg";
                var path = Path.Combine(contentPath, "Uploads/ProfilePictures/"+value.UserId);
                fileWithPath = Path.Combine(path, newFileName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            var users = _unitOfWork.UserRepository.UpdateUser(value, fileWithPath);
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
