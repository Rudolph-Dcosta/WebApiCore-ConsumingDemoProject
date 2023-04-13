using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiCoreDemoProject.Models.Domain;
using WebApiCoreDemoProject.Repositories.IRepository;

namespace WebApiCoreDemoProject.Repositories
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration configuration;
        public TokenHandler(IConfiguration configuration)
        {
            this.configuration=configuration;
        }
        public Task<string> CreateTokenAsync(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var cliams = new List<Claim>();
            cliams.Add(new Claim(ClaimTypes.GivenName, user.FirstName));
            cliams.Add(new Claim(ClaimTypes.Surname, user.LastName));
            cliams.Add(new Claim(ClaimTypes.Email, user.EmailAddress));

            //lopp using  foreach for roles

            user.Roles.ForEach(role =>
            {
                cliams.Add(new Claim(ClaimTypes.Role, role));
            });

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                cliams,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );
            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
