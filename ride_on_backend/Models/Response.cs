namespace ride_on_backend.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public List<User> users { get; set; } // list of users
        public User user { get; set; } // particular user
        public List<Ride> rides { get; set; } // list of rides
        public Ride ride { get; set; } // particular ride
        public List<Request> requests { get; set; } // list of requests
        public Request request { get; set; } // particular request
    }
}
