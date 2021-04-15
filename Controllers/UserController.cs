using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NodeCMBAPI.Services;
using NodeCMBAPI.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NodeCMBAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            var records = _userService.GetUsers();

            foreach (var item in records)
            {
                item.passwordHash = null;
                item.passwordSalt = null;
            }

            if (records != null)
                if (records.Count > 0)
                {
                    dic.Add("status", "1");
                    dic.Add("message", "Successful");
                    dic.Add("data", records);
                    return Ok(dic);
                }

            dic.Add("status", "0");
            dic.Add("message", "Unsuccessful");
            dic.Add("data", null);
            return Ok(dic);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            var records = _userService.GetById(id);
            
            if (records != null)
            {
                records.passwordHash = null;
                records.passwordSalt = null;
                dic.Add("status", "1");
                dic.Add("message", "Successful");
                dic.Add("data", records);
                return Ok(dic);
            }

            dic.Add("status", "0");
            dic.Add("message", "Unsuccessful");
            dic.Add("data", null);
            return Ok(dic);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }



        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public ActionResult<string> Authenticate(User userDto)
        {
            var user = _userService.Authenticate(userDto.UserName, userDto.Password);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (user == null)
            {
                dic.Add("status", "0");
                dic.Add("message", "Username or password is incorrect");
                dic.Add("data", null);
                return Ok(dic);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.ID.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);


            Dictionary<string, object> dicData = new Dictionary<string, object>();
            dicData.Add("UserName", user.UserName);
            dicData.Add("FirstName", user.FirstName);
            dicData.Add("LastName", user.LastName);
            dicData.Add("Mobile", user.Mobile);
            dicData.Add("ID", user.ID);
            dicData.Add("Token ", tokenString);

            dic.Add("status", "1");
            dic.Add("message", "Successful");
            dic.Add("data", dicData);
            // return basic user info (without password) and token to store client side
            return Ok(dic);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult<string> Register(User user)
        {
            var status = _userService.Register(user);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (status.Equals("1"))
            {
                dic.Add("status", "1");
                dic.Add("message", "Successful");
                dic.Add("data", null);
                return Ok(dic);
            }
            else
            {
                dic.Add("status", "0");
                dic.Add("message", status);
                dic.Add("data", null);
                return Ok(dic);
            }
        }

        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        public ActionResult<string> ResetPassword(User user)
        {
            var status = _userService.ResetPassword(user);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (status.Equals("1"))
            {
                dic.Add("status", "1");
                dic.Add("message", "Successful");
                dic.Add("data", null);
                return Ok(dic);
            }
            else
            {
                dic.Add("status", "0");
                dic.Add("message", status);
                dic.Add("data", null);
                return Ok(dic);
            }
        }

        [HttpPost("UpdateUser")]
        public ActionResult<string> UpdateUser(User user)
        {
            var status = _userService.UpdateUser(user);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (status.Equals("1"))
            {
                var updatedUser = _userService.GetById(user.ID);
                dic.Add("status", "1");
                dic.Add("message", "Successful");
                dic.Add("data", updatedUser);
                return Ok(dic);
            }
            else
            {
                dic.Add("status", "0");
                dic.Add("message", status);
                dic.Add("data", null);
                return Ok(dic);
            }
        }

  

    }
}
