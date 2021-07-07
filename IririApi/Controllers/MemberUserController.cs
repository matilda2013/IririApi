using AutoMapper;
using IririApi.Libs.Helpers;
using IririApi.Libs.Model;
using IririApi.Libs.Model.IService;
using IririApi.Libs.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberUserController : ControllerBase
    {
        private UserManager<MemberRegistrationUser> _userManager;
        private readonly AuthenticationContext _DbContext;
        private readonly IUserAccountService _userAccountService;
        // EmailServiceController _emailService;


        public MemberUserController(AuthenticationContext Db, UserManager<MemberRegistrationUser> userManager, IUserAccountService userAccountService)
        {
            _userManager = userManager;
            _DbContext = Db;
            _userAccountService = userAccountService;

        }



        [HttpPost]
       [Route("RegisterAdmin")]
  
        public async Task PostAdminUser([FromBody]MemberUserViewModel model)
        {

            ArgumentGuard.NotNullOrEmpty(model.FirstName, nameof(model.FirstName));

            ArgumentGuard.NotNullOrEmpty(model.LastName, nameof(model.LastName));

            ArgumentGuard.NotNullOrEmpty(model.MemberEmail, nameof(model.MemberEmail));


            await _userAccountService.RegisterAdminUserAsync(model);
            

        }

        [HttpGet]
        [Route("ViewMemberById")]
        public async Task GetMembersById(string id)
        {
             await _userAccountService.ViewMembersByIdAsync(id);
        }

        [HttpPost]
        [Route("RegisterMember")]
       
        public async Task PostMemberUser([FromBody]MemberUserViewModel model)
        {
            model.Role = "Member";


            ArgumentGuard.NotNullOrEmpty(model.FirstName, nameof(model.FirstName));

            ArgumentGuard.NotNullOrEmpty(model.LastName, nameof(model.LastName));

            ArgumentGuard.NotNullOrEmpty(model.MemberEmail, nameof(model.MemberEmail));

            await _userAccountService.RegisterMemberUserAsync(model);


        }



        [HttpPost]
        [Route("Login")]
       
        public async Task<IActionResult> Login(LoginModel model)
        {

            MemberRegistrationUser user = await _userManager.FindByEmailAsync(model.UserName);


            if (user == null)
            {


                return BadRequest(new { message = "You need to Register and confirm your email " });
            }


            //if (!await _userManager.IsEmailConfirmedAsync(user))
            //{

            //    return BadRequest(new { message = "You need to Register and confirm your email " });

            //}


            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var role = await _userManager.GetRolesAsync(user);
                IdentityOptions _options = new IdentityOptions();

                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {

                         new Claim("UserID", user.Id.ToString()),
                       new Claim(_options.ClaimsIdentity.RoleClaimType,role.FirstOrDefault())

                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                   
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890123456")), SecurityAlgorithms.HmacSha256Signature)

                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);


                return Ok(new { token });


            }
            else

                return BadRequest(new { message = " Invalid Login Credentials " });
        }



    }
}
