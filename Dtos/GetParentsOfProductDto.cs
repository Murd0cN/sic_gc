using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    public class GetParentsOfProductDto
    {
        [Required]
        public int ChildID { get; set; }

        [Required]
        public ICollection<int> ParentsIDs { get; set; }
    }
}