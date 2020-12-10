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
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public AuthController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        // I was unable to get this god forsaken tag working... I would just get 401 with the correct headers no matter how I sent them
        // I'm pretty sure implementation is correct, but it's probably for some different version of ASP.net than what we're using.....
        [Authorize]
        public ActionResult<bool> Get()
        {
            // Nothing is needed inside code block cause Authorize tag will trow "Unauthorized" error if no token header is present
            // The catch error should also never happen because of this, only kept it there because it might catch other error such as hosting or cors
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
    }
}
