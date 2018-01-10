using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ContactsWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ContactsWebApi.Controllers {

    [Authorize]
    [Route("api/user")]
    public class UserController : Controller {
        private readonly IUserService _userService;
        public UserController(IUserService _userService) {
            this._userService = _userService;
        }

        // GET api/user
        [HttpGet]
        public IActionResult Get() {
            var Claims = User.Claims.ToList();
            string firstName = ""; string lastName = "";
            foreach (Claim claim in Claims)  {
                if (claim.Type.IndexOf("claims/givenname") != -1) {
                    firstName = claim.Value;
                }
                if (claim.Type.IndexOf("claims/surname") != -1) {
                    lastName = claim.Value;
                }
            }
            firstName = firstName != null ? firstName : "Tim";
            lastName = lastName != null ? lastName : "Tester";
            return Ok(this._userService.GetUser(firstName, lastName));
        }
    }
}
