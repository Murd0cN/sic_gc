using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    public class GetProductPartsDto
    {
        [Required]
        public int ParentID { get; set; }

        [Required]
        public ICollection<int> PartsIDs { get; set; }
    }
}