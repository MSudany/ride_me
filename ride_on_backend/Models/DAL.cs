using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;


namespace ride_on_backend.Models
{
    public class DAL
    {
        public Response register(User user, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_register", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FName", user.FName);
            cmd.Parameters.AddWithValue("@LName", user.LName);
            cmd.Parameters.AddWithValue("@Pass", user.Pass);
            cmd.Parameters.AddWithValue("@Mail", user.Mail);
            cmd.Parameters.AddWithValue("@Type", user.Type);
            cmd.Parameters.AddWithValue("@Available", true);
            cmd.Parameters.AddWithValue("@CarUrl", user.CarUrl);
            // Rating and CreatedOn are mentioned in procedures only
            
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            
            connection.Close();
            if (i > 0) {
                response.StatusCode = 200;
                response.StatusMessage = "User registerd successfully :)";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User registeration failed :c";
            }
            
            return response;
        }

        public Response login(User user, SqlConnection connection)
        {
            Response response = new Response();
            SqlDataAdapter adapter = new SqlDataAdapter("sp_login", connection);
            DataTable dataTable = new DataTable();
            User returnUser = new User();
            
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Mail", user.Mail);
            adapter.SelectCommand.Parameters.AddWithValue("@Pass", user.Pass);

            adapter.Fill(dataTable);
            
            if (dataTable.Rows.Count > 0)
            {

                returnUser.ID = Convert.ToInt32(dataTable.Rows[0]["ID"]);
                returnUser.FName = Convert.ToString(dataTable.Rows[0]["FName"]);
                returnUser.LName = Convert.ToString(dataTable.Rows[0]["LName"]);
                returnUser.Mail = Convert.ToString(dataTable.Rows[0]["Mail"]);
                returnUser.Type = (UserType)Enum.Parse(typeof(UserType), Convert.ToString(dataTable.Rows[0]["Type"]));

                response.StatusCode = 200;
                response.StatusMessage = "Logged in!";
                response.user = user;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Invalid user!";
                response.user = null;
            }

            return response;
        }
        public Response viewUser(User user, SqlConnection connection)
        {
            Response response = new Response();
            SqlDataAdapter adapter = new SqlDataAdapter("p_viewUser", connection);
            DataTable dataTable = new DataTable();
            User returnUser = new User();

            adapter.SelectCommand.CommandType= CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@ID", user.ID);
            adapter.Fill(dataTable);
            
            if (dataTable.Rows.Count > 0)
            {
                returnUser.ID = Convert.ToInt32(dataTable.Rows[0]["ID"]);
                returnUser.FName = Convert.ToString(dataTable.Rows[0]["FName"]);
                returnUser.LName = Convert.ToString(dataTable.Rows[0]["LName"]);
                returnUser.Pass = Convert.ToString(dataTable.Rows[0]["Pass"]);
                returnUser.Mail = Convert.ToString(dataTable.Rows[0]["Mail"]);
                returnUser.Type = (UserType)Enum.Parse(typeof(UserType), Convert.ToString(dataTable.Rows[0]["Type"]));
                returnUser.CreatedOn = Convert.ToDateTime(dataTable.Rows[0]["CreatedOn"]);

                response.StatusCode = 200;
                response.StatusMessage = "Exists";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Invalid credentials";
                response.user = returnUser;
            }

            return response;
        }

        public Response updateProfile(User user, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_updateProfile", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FName", user.FName);
            cmd.Parameters.AddWithValue("@LName", user.LName);
            cmd.Parameters.AddWithValue("@Pass", user.Pass);
            cmd.Parameters.AddWithValue("@Mail", user.Mail);
            cmd.Parameters.AddWithValue("@Available", user.Available);

            connection.Open();
            int i = cmd.ExecuteNonQuery();

            connection.Close();
            if(i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Updated successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Update failed";
            }

            return response;
        }

        public Response rideSelection(Ride ride, SqlConnection connection) {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_rideRequest", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DriverID", ride.DriverID);
            cmd.Parameters.AddWithValue("@PassengerID", ride.PassengerID);
            cmd.Parameters.AddWithValue("@Pickup", ride.Pickup);
            cmd.Parameters.AddWithValue("@Destination", ride.Destination);
            cmd.Parameters.AddWithValue("@Price", ride.Price);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            if(i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Request sent. Waiting for Response ...";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Failed to send the request. Please try again.";
            }

            return response;
        }

        public Response submitRequest(User user, SqlConnection connection) {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_submitRequest", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", user.ID);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Request sent. Waiting for Response ...";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Failed to send the request. Please try again.";
            }

            return response;
        }

        public Response rideHistory(User user, SqlConnection connection) {
            Response response = new Response();
            List<Ride> list = new List<Ride>();
            SqlDataAdapter adapter = new SqlDataAdapter("sp_rideHistory", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Type", user.Type.ToString());
            adapter.SelectCommand.Parameters.AddWithValue("@ID", user.ID);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            if(dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Ride ride = new Ride();
                    ride.ID = Convert.ToInt32(dataTable.Rows[i]["ID"]);
                    ride.DriverID = Convert.ToInt32(dataTable.Rows[i]["DriverID"]);
                    ride.Pickup = Convert.ToString(dataTable.Rows[i]["Pickup"]);
                    ride.Destination = Convert.ToString(dataTable.Rows[i]["Destination"]);
                    ride.Price = Convert.ToDouble(dataTable.Rows[i]["Price"]);

                    list.Add(ride);
                }
                if (list.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "fetched";
                    response.rides = list;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "failed to fetch";
                    response.rides = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "failed to fetch";
                response.rides = null;
            }
            return response;
        }

        public Response userList(SqlConnection connection)
        {
            Response response = new Response();
            List<User> list = new List<User>();
            SqlDataAdapter adapter = new SqlDataAdapter("sp_usersList", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    User tempUser = new User();
                    tempUser.ID = Convert.ToInt32(dataTable.Rows[i]["ID"]);
                    tempUser.FName = Convert.ToString(dataTable.Rows[i]["FName"]);
                    tempUser.LName = Convert.ToString(dataTable.Rows[i]["LName"]);
                    tempUser.Pass = Convert.ToString(dataTable.Rows[i]["Pass"]);
                    tempUser.Mail = Convert.ToString(dataTable.Rows[i]["Mail"]);
                    tempUser.Available = Convert.ToBoolean(dataTable.Rows[i]["Available"]);
                    tempUser.CreatedOn = Convert.ToDateTime(dataTable.Rows[i]["CreatedOn"]);
                    tempUser.CarUrl = Convert.ToString(dataTable.Rows[i]["CarUrl"]);
                    tempUser.Rating = Convert.ToDouble(dataTable.Rows[i]["Rating"]);

                    list.Add(tempUser);
                }
                if (list.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "fetched";
                    response.users = list;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "failed to fetch";
                    response.users = null;
                }
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "failed to fetch";
                response.users = null;
            }
            return response;
        }
    }
}
