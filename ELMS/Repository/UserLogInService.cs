using ELearningWeb.DbContexts;
using ELearningWeb.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.Extensions.Options;
using ELearningWeb.Helper;
using ELearningWeb.IRepository;

namespace ELearningWeb.Repository
{
    public class UserLogInService : IUserLogInService
    {
        private IOptions<Audience> _settings;
        private readonly ReadDbContext _contextR;
        private readonly WriteDbContext _contextW;
        public UserLogInService(ReadDbContext _contextr, WriteDbContext _contextw,IOptions<Audience> settings)
        {
            _contextR = _contextr;
            _contextW = _contextw;
            _settings = settings;
        }

        public async Task<AuthDTO> LogIn(string Email, string Password)
        {
            try
            {
                if (Email == null)
                {
                    throw new Exception("Email must be given");
                }
                if (Password == null)
                {
                    throw new Exception("Password must be given");
                }

                var ValidUser = await Task.FromResult(_contextW.Users.Where(y => y.Email == Email && y.Password == Password).FirstOrDefault());
                if (ValidUser == null)
                {
                    throw new Exception("User name or password invalid");
                }

                var token = await GenerateToken(Email);
                return token;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<AuthDTO> GenerateToken(string Email)
        {
            var now = DateTime.UtcNow;
            try
            {
                if(Email == null)
                {
                    throw new Exception("Email should not be null");
                }

                var claims = new Claim[]
               {

                    new Claim(ClaimTypes.Role,"test"),
                    new Claim("enroll",AesOperation.EncryptString("b14ca5898a4e4133bbce2ea2315a1916","test")),
                    new Claim("terantId",AesOperation.EncryptString("b14ca5898a4e4133bbce2ea2315a1916", "Test")),
                    new Claim("Email",Email),
                    new Claim(JwtRegisteredClaimNames.Sub, "test"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
                };

                //_env.IsProduction()? Configuration.GetSection("REACT_APP_SECRET_VALUE").Value.Trim():

                var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.Value.Secret));

                var jwt = new JwtSecurityToken(
                    issuer: _settings.Value.Iss,
                    audience: _settings.Value.Aud,
                    claims: claims,
                    //notBefore: now,
                    // expires: now.Add(TimeSpan.FromMinutes(60 * 24 * 1)), //expires after 1day
                    expires: now.Add(TimeSpan.FromMinutes(60 * 24 * 1)), //expires after 1day
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);


                return new AuthDTO
                {
                    Success = true,
                    Token = encodedJwt,
                    RefreshToken = encodedJwt,
                    expires_in = (int)TimeSpan.FromMinutes(1).TotalSeconds,
                    ActionTime = DateTime.Now//actionTime.ToString("yyyy-MM-dd hh:mm:ss.ff")
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    }
