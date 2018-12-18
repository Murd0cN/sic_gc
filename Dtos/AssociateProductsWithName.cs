using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    public class AssociateProductsWithName
    {
        [Required]
        public string Parent { get; set; }

        [Required]
        public string Child { get; set; }

        [Required]
        public bool IsMandatory { get; set; }

        [Required]
        public bool RestrictMaterials { get; set; }

        [Required]
        public IList<float> PercentageRestrictions { get; set; }
    }
}