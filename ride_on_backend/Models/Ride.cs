namespace ride_on_backend.Models
{
    public class Ride
    {
        public int ID { get; set; }
        public int PassengerID { get; set; }
        public int DriverID { get; set; }
        public string Pickup {  get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
    }
}
