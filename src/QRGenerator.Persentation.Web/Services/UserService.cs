using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QRGenerator.Business.Interface;
using QRGenerator.DataAccess.Models;
using QRGenerator.Persentation.Web.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QRGenerator.Persentation.Web.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public AuthenticationTicket Login(LoginViewModel vm)
        {
            var user = _userRepository.GetAccountByUsername(vm.Username)
               ?? throw new Exception("Incorrect username or password");
            //proses verifikasi antara password di database dan password yang dimasukan database
            bool iscorectPassword = false;
            if (vm.Password == user.Password) { 
                iscorectPassword = true;
            }
            if (!iscorectPassword)
            {
                throw new Exception("Incorrect username or password");
            }
            ClaimsPrincipal principal = GetPrincipal(user);
            return GetAuthenticationTicket(principal);
        }
        private ClaimsPrincipal GetPrincipal(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("username",user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }
        private AuthenticationTicket GetAuthenticationTicket(ClaimsPrincipal principal)
        {
            AuthenticationProperties authenticationProperties = new AuthenticationProperties
            {
                IssuedUtc = DateTime.Now,
                ExpiresUtc = DateTime.Now.AddMinutes(60),
                AllowRefresh = false,
            };
            AuthenticationTicket authenticationTicket = new AuthenticationTicket(principal, authenticationProperties,
                                                                                            CookieAuthenticationDefaults.AuthenticationScheme);
            return authenticationTicket;
        }
        
    }
}
