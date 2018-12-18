using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    public class PossibleValuesOfDimensionDto
    {
        [Required]
        public bool IsDiscrete { get; set; }

        [Required]
        public ICollection<float> Values { get; set; }
    }
}