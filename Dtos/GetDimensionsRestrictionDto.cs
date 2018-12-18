using System.ComponentModel.DataAnnotations;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    public class GetDimensionsRestrictionDto : GetRestrictionDto
    {
        [Required]
        public float MinHeightValue { get; set; }

        [Required]
        public float MaxHeightValue { get; set; }

        [Required]
        public float MinWidthValue { get; set; }

        [Required]
        public float MaxWidthValue { get; set; }

        [Required]
        public float MinDepthValue { get; set; }

        [Required]
        public float MaxDepthValue { get; set; }
    }
}