using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    public class GetProductRestrictionsDto
    {
        [Required]
        public int ProductID { get; set; }

        [Required]
        public ICollection<int> ProductRestrictions { get; set; }
    }
}