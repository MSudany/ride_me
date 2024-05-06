namespace ride_on_backend.Models
{
    public enum RequestStatus
    {
        Pending,
        Accepted,
        Cancelled
    }
    public class Request
    {
        public int ID { get; set; }
        public int RideID { get; set; }
        public int PassengerID { get; set; }
        public RequestStatus Status { get; set; }
    }
}
