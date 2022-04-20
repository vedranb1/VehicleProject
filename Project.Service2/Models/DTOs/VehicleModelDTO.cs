using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models.DTOs
{
    public class VehicleModelDTO
    {

        public int Id { get; set; }
        [Required]
        public int MakeId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Abrv { get; set; }
        public VehicleMakeDTO VehicleMake { get; set; }

    }
}
