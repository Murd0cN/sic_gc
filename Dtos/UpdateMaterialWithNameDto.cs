using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    public class UpdateMaterialWithNameDto
    {
        /*
            Name of the material.
        */
        [Required]
        public string Name { get; set; }

        public string NewName { get; set; }
        /*
            Collection of DTO's for finishes that weren't already part of the material.
        */
        [Required]
        public ICollection<string> NewFinishes { get; set; }
    }
}