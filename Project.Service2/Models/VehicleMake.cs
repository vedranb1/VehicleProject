using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class VehicleMake
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Abrv { get; set; }

    }
}
