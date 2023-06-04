using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace PracticeWebAPI.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(LoginAuthentication authenticationUser)
        {
            PetStoreUser? user = ValidateUser(authenticationUser);

            if (user == null)
            {
                return Unauthorized();
            }

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretKey"]));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.UserId));
            //claimsForToken.Add(new Claim("iss", _configuration["Authentication:Issuer"]));
            //claimsForToken.Add(new Claim("aud", _configuration["Authentication:Audience"]));
            claimsForToken.Add(new Claim("given_name", user.Fullname));
            claimsForToken.Add(new Claim("email", user.Email));
            claimsForToken.Add(new Claim("phone_number", user.Phone));
            claimsForToken.Add(new Claim("address", user.Address));
            claimsForToken.Add(new Claim("address", user.Address));

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials
            );

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Ok(token);
        }

        public PetStoreUser? ValidateUser(LoginAuthentication authenticationUser)
        {
            // Let's imagine we have check user credentials and they were correct, and return the user object*
            if (authenticationUser.UserName == "yassine-klilich" && authenticationUser.Password == "123")
            {
                return new PetStoreUser()
                {
                    UserId = "user_123",
                    UserName = "yassine-klilich",
                    Fullname = "Yassine Klilich",
                    Email = "y.klilich@email.com",
                    CIN = "K000000",
                    Gender = "Male",
                    Phone = "0600000000",
                    Address = "Branes 1",
                    City = "Tangier",
                };

            }

            return null;
        }

        public class LoginAuthentication
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class PetStoreUser
        {
            public string UserId { get; set; } = string.Empty;
            public string UserName { get; set; } = string.Empty;
            public string Fullname { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string CIN { get; set; } = string.Empty;
            public string Gender { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;
            public string City { get; set; } = string.Empty;
        }
    }
}
