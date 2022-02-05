using System.ComponentModel.DataAnnotations;

namespace MovieStore.Models
{
    public class Countries
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}