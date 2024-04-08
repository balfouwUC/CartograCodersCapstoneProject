using System.ComponentModel.DataAnnotations;

namespace MapMaker.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Map> Maps { get; set; }

    }
}
