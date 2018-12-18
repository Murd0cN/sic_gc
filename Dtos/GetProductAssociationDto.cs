using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    /*
        DTO for displaying a relationship between two products.
    */
    public class GetProductAssociationDto
    {
        [Required]
        public int AssociationId { get; set; }

        [Required]
        public int ParentId { get; set; }

        [Required]
        public int ChildId { get; set; }

        [Required]
        public bool IsMandatory { get; set; }

        [Required]
        public bool RestrictMaterials { get; set; }

        [Required]
        public IList<float> PercentageRestrictions { get; set; }
    }
}