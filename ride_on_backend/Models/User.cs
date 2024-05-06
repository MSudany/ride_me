namespace ride_on_backend.Models
{
    public enum UserType
    {
        Admin,
        Driver,
        Passenger
    }
    public class User
    {
        public int ID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Pass { get; set; }
        public string Mail { get; set; }
        public UserType Type { get; set; }
        public bool Available { get; set; }
        public DateTime CreatedOn { get; set; }
        public String CarUrl { get; set; }
        public double Rating {  get; set; }
    }
}
