using Microsoft.IdentityModel.Tokens;
using modelLayer;
using RepostoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RepostoryLayer.Interfaces;

namespace RepostoryLayer.Services
{
    public class AdminRepo:IAdminRepo

    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;
        public AdminRepo(DataContext context, IConfiguration _config)
        {
            _context = context;
            this._config = _config;
        }
        private string GenerateToken(long UserId, string userEmail)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim("Email",userEmail),
                new Claim("UserId", UserId.ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(1440),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        ///login

        public LoginToken Login(LoginModel loginModel)
        {
            using (var conn = _context.CreateConnection())
            {
                AdminEntity user = new AdminEntity();
                SqlCommand cmd = new SqlCommand("spLoginAdmin", (SqlConnection)conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmailId", loginModel.EmailId);
                cmd.Parameters.AddWithValue("@Password", loginModel.Password);
                conn.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    user.Id = Convert.ToInt32(dataReader["Id"]);
                    user.FullName = dataReader["FullName"].ToString();
                    user.EmailId = dataReader["EmailId"].ToString();
                    user.Password = dataReader["Password"].ToString();
                    user.Mobile = dataReader["Mobile"].ToString();
                }
                if (user.EmailId == loginModel.EmailId && user.Password == loginModel.Password)
                {
                    LoginToken login = new LoginToken();
                    var token = GenerateToken(user.Id, user.EmailId);
                    login.Token = token;

                    return login;
                }
                return null;


            }
        }
    }
}
