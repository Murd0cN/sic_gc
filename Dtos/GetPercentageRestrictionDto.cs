using System.ComponentModel.DataAnnotations;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    public class GetPercentageRestrictionDto : GetRestrictionDto
    {
        /*
           Minimum ocupation percentage for the height.
       */
        [Required]
        public float MinHeightPercentage { get; set; }

        /*
            Maximum ocupation percentage for the height.
        */
        [Required]
        public float MaxHeightPercentage { get; set; }

        /*
            Minimum ocupation percentage for the width.
        */
        [Required]
        public float MinWidthPercentage { get; set; }

        /*
            Maximum ocupation percentage for the width.
        */
        [Required]
        public float MaxWidthPercentage { get; set; }

        /*
            Minimum ocupation percentage for the depth.
        */
        [Required]
        public float MinDepthPercentage { get; set; }

        /*
            Maximum ocupation percentage for the depth.
        */
        [Required]
        public float MaxDepthPercentage { get; set; }
    }
}