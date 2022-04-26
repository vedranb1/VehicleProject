using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Service2.Models
{
    [NotMapped]
    public class Paging
    {

        public int PageSize { get; set; }
        public int Page { get; set; }

    }
}
