using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    public class ChangeProductWithNamesDto
    {
        [Required]
        public string Product { get; set; }

        public string Name { get; set; }

        public float? Price { get; set; }

        public string Category { get; set; }

        public PossibleValuesOfDimensionDto NewHeightDimensions { get; set; }

        public PossibleValuesOfDimensionDto NewWidthDimensions { get; set; }

        public PossibleValuesOfDimensionDto NewDepthDimensions { get; set; }

        public ICollection<string> MaterialsAndFinishes { get; set; }
    }
}