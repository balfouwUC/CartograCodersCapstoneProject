using System.ComponentModel.DataAnnotations;

namespace MapMaker.Models
{
    public class Map
    {        
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreatedBy { get; set; }
        public string Data {  get; set; }

    }
}
