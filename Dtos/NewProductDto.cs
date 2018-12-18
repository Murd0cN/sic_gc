using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    public class NewProductDto
    {
        [Required]
        public string ProductName { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public PossibleValuesOfDimensionDto NewHeightDimensions { get; set; }

        [Required]
        public PossibleValuesOfDimensionDto NewWidthDimensions { get; set; }

        [Required]
        public PossibleValuesOfDimensionDto NewDepthDimensions { get; set; }

        [Required]
        public ICollection<int> Materials { get; set; }
    }
}