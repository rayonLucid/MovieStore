using System.ComponentModel.DataAnnotations;

namespace MovieStore.Models
{
    public class Customers
    {
        [Key]
        public int ID { get; set; }
        public int StoreID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Active { get; set; }
        public int MoviesRented { get; set; }
    }
}