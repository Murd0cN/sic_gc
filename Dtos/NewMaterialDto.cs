using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    /*
        DTO for the creation of a new material.
    */
    public class NewMaterialDto
    {
        /*
            Name of the material.
        */
        [Required]
        public string MaterialName { get; set; }

        /*
            Available finishes for the material.
        */
        [Required]
        public ICollection<NewFinishDto> Finishes { get; set; }
    }
}