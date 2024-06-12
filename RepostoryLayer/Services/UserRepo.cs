using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using modelLayer;
using RepostoryLayer.Interfaces;
using RepostoryLayer.Entity;
using System.Reflection;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Channels;

namespace RepostoryLayer.Services
{
    public class UserRepo : IUserRepo
    {
        
        private readonly DataContext _context;
        private readonly IConfiguration _config;
        public UserRepo(DataContext context, IConfiguration _config)
        {
            _context = context;
            this._config = _config;
        }

        public UserModel RegisterUser(UserModel model)
        {
            using (var conn = _context.CreateConnection())
            {
                var cmd = new SqlCommand("spRegisterUser", (SqlConnection)conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@FullName", model.FullName);
                cmd.Parameters.AddWithValue("@EmailId", model.EmailId);
                cmd.Parameters.AddWithValue("@Password", model.Password);
                cmd.Parameters.AddWithValue("@Mobile", model.Mobile);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                conn.Close();

                return result < 0 ? model : null;
            }

        }
        ///generate token 
        private string GenerateToken(long UserId, string userEmail)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
               new Claim(ClaimTypes.Role,"User"),
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
                UserEntity user = new UserEntity();
                SqlCommand cmd = new SqlCommand("spLoginUser", (SqlConnection) conn);
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
                    login.EmailId = user.EmailId;
                    login.FullName = user.FullName;
                    login.Password = user.Password;
                    login.Mobile = user.Mobile;
                    login.userid = user.Id;
               
                    return login;
                }
                    return null; 


            }
        }

        //forgot

        public ForgotPasswordModel ForgotPassword(string email)
        {

            using (var conn = _context.CreateConnection())
            {
                try
                {
                    UserEntity user = new UserEntity();

                    SqlCommand cmd = new SqlCommand("spForgotPassword", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmailId", email);
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
                    if (email == user.EmailId)
                    {
                        ForgotPasswordModel model = new ForgotPasswordModel();

                        model.EmailId = user.EmailId;
                        model.Id = user.Id;
                        model.token = GenerateToken(user.Id, user.EmailId);
                        return model;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;
            }
        }



        //

        public bool ResetPassword(string email, string password)
        {
            // using (SqlConnection conn = new SqlConnection(connectionstring))
            using (var conn = _context.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spResetPassword", (SqlConnection) conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailId", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    return true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return false;
            }
        }
        
    }
}
