using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Service2.Models
{
    [NotMapped]
    public class Filtering
    {

        public string SearchText { get; set; }

    }
}
