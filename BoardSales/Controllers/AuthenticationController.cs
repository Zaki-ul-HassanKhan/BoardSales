using Domain.DtoModels;
using Domain.Repository.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BoardSales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthenticationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = _unitOfWork.UserRepository.GetByUserNamePassword(loginModel);
            if (user != null)
            {
                return Ok(user);

            }
            return NotFound();


        }
    }
}
