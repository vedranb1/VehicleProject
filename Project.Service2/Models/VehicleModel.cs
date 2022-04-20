using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Service.Models
{
    public class VehicleModel
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public int MakeId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Abrv { get; set; }
        [ForeignKey("MakeId")]
        public VehicleMake VehicleMake { get; set; }

    }
}
