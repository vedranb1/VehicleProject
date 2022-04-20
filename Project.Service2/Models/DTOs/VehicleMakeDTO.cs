using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models.DTOs
{
    public class VehicleMakeDTO
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Abrv { get; set; }

    }
}
