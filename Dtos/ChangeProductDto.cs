using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    public class ChangeProductDto
    {
        [Required]
        public int ProductId { get; set; }

        public string Name { get; set; }

        public float? Price { get; set; }

        public int? CategoryId { get; set; }

        public PossibleValuesOfDimensionDto NewHeightDimensions { get; set; }

        public PossibleValuesOfDimensionDto NewWidthDimensions { get; set; }

        public PossibleValuesOfDimensionDto NewDepthDimensions { get; set; }

        public ICollection<int> MaterialsAndFinishes { get; set; }
    }
}