using System;
using System.ComponentModel.DataAnnotations;

namespace MovieStore.Models
{
    public class Stores
    {
        [Key]
        public int ID { get; set; }
        public int CountryID { get; set; }
        public string location { get; set; }
        public Nullable<int> AvailableMovies { get; set; }
    }
}