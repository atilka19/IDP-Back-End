using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDP_Back_End.Models;
using IDP_Back_End.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDP_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        // Nothing is needed inside code block cause Authorize tag will trow "Unauthorized" error if no token header is present
        // The catch error should also never happen because of this, only kept it there because it might catch other error such as hosting or cors
        [Authorize]
        public ActionResult<bool> Get()
        {
            try
            {
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult Login([FromBody] LoginInputModel model)
        {
            //Find the user that wants to log in
            var user = _userRepo.GetUserByUserName(model.Username);

            //Username check, if the given username does not exist
            if (user == null)
                return StatusCode(401, "User Was not found!");
            //Password check
            if (!_userRepo.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return StatusCode(401, "Username or Password incorrect!");

            return Ok(new
            {
                username = user.UserName,
                token = _userRepo.GenerateToken(user),
                isAdmin = user.Admin
            });

        }

        [HttpPost]
        public ActionResult<LoginInputModel> Post([FromBody] LoginInputModel model)
        {
            try
            {
                var check = _userRepo.GetUserByUserName(model.Username);
                if (check != null)
                {
                    return StatusCode(401, "Username is taken!");
                }
                else
                {
                    var user = _userRepo.RegisterUser(model);
                }
                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
