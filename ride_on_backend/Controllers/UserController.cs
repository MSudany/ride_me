using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ride_on_backend.Models;

namespace ride_on_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("registration")]
        public Response register(User user)
        {
            Response response = new Response();
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ride_on").ToString());
            response = dal.register(user, connection);
            return response;
        }

        [HttpPost]
        [Route("login")]
        public Response login(User user)
        {
            Response response = new Response();
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ride_on").ToString());
            response = dal.login(user, connection);
            return response;
        }

        [HttpPost]
        [Route("viewUser")]
        public Response viewUser(User user) { 
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ride_on").ToString());
            Response response = dal.viewUser(user, connection);
            return response;
        }

        [HttpPost]
        [Route("updateProfile")]
        public Response updateProfile(User user) {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ride_on").ToString());
            Response response = dal.updateProfile(user, connection);
            return response;
        }
    }
}
