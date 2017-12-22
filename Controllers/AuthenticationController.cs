using Microsoft.AspNetCore.Mvc;
using ContactsWebApi.Models;
using ContactsWebApi.Services;

namespace ContactsWebApi.Controllers
{
    [Route("api/authenticate")]
    public class AuthenticationController : Controller
    {
        [HttpPost]
        public IActionResult Post([FromBody] LoginCredentials loginCredentials)
        {
            if (loginCredentials == null) {
              return BadRequest();
            } else {
              var tokenService = new TokenService();
              var token = tokenService.GetToken(loginCredentials);
              if (token.Result == null) {
                 return StatusCode(401); 
              } else {
                 return new JsonResult(token.Result);
              }
            }
        }

    }
}
