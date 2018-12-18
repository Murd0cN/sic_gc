using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    public class GetProductDto
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public PossibleValuesOfDimensionDto HeightPossibleValues { get; set; }

        [Required]
        public PossibleValuesOfDimensionDto WidthPossibleValues { get; set; }

        [Required]
        public PossibleValuesOfDimensionDto DepthPossibleValues { get; set; }

        [Required]
        public ICollection<int> MaterialsAndFinishes { get; set; }
    }
}