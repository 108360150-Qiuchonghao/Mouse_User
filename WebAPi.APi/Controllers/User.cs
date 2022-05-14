using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPi.APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(string username, string password, string email)
        {
            string connstring = "server=localhost;port=3306;user=root;password=qchqdd725327831;database=itcast;Charset=utf8;";
            Console.WriteLine("生成连接");
            try
            {
                using var conn = new MySqlConnection(connstring);
               
                var sql = "INSERT INTO mouseuser(username, email, password) VALUES(@username, @email, @password);";
                
                var command = new MySqlCommand(sql, conn);
                command.Parameters.Add("@username",MySqlDbType.String);
                command.Parameters["@username"].Value = username;
                command.Parameters.Add("@email", MySqlDbType.String);
                command.Parameters["@email"].Value = email;
                command.Parameters.Add("@password", MySqlDbType.String);
                command.Parameters["@password"].Value = password;
                conn.Open();
                
                var row = command.ExecuteNonQuery();
                if (row == 1)
                { 
                    return StatusCode(201);
                }
                else
                {
                    Console.WriteLine("连接失败");
                    return UnprocessableEntity();
                }
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        
        }
    }
}
