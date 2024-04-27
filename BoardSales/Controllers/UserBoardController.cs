using Domain.DtoModels.UserBoards;
using Domain.Repository.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BoardSales.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserBoardController : ControllerBase
    {

        private IWebHostEnvironment _environment;
        private readonly IUnitOfWork _unitOfWork;
        public UserBoardController( IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _environment = webHostEnvironment;
        }
        // GET: api/<UserBoardController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserBoardController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserBoardController>
        [Route("AddUpdateUserBoard")]
        [HttpPost]
        public ActionResult Post(AddUpdateUserBoard value)
        {
            var contentPath = "";
            if (value.ImagesPath != null && value.ImagesPath.Count>0)
            {

                contentPath = _environment.WebRootPath + "/Uploads/UserBoardPictures/";
                value.FileName = value.UserId + ".jpg";
                //path = Path.Combine(contentPath, "ProfilePictures/");
                //  path = Path.Combine(path, value.FileName);
                if (!Directory.Exists(contentPath))
                {
                    Directory.CreateDirectory(contentPath);
                }
                contentPath = Path.Combine(contentPath, value.FileName);
            }

            var users = _unitOfWork.UserBoardRepository.AddUpdateUserBoard(value, contentPath);
            return Ok(users.Result);
        }

        // PUT api/<UserBoardController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserBoardController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
