using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ride_on_backend.Models;


namespace ride_on_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RideController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public RideController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("rideSelection")]
        public Response rideSelection(Ride ride)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ride_on").ToString());
            Response response = dal.rideSelection(ride, connection);
            return response;
        }

        [HttpPost]
        [Route("submitRequest")]
        public Response submitRequest(User user)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ride_on").ToString());
            Response response = dal.submitRequest(user, connection);
            return response;
        }

        [HttpPost]
        [Route("rideHistory")]
        public Response rideHistory(User user)
        {
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ride_on").ToString());
            Response response = dal.rideHistory(user, connection);
            return response;
        }
    }
}
