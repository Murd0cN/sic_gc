using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    /*
        Dto with data to modify a material: contains both a collection of previously existent
        finishes (DTO's with ID's) and a collection of new finishes (DTO's without ID's).
    */
    public class PutMaterialDto
    {
        /*
            ID of the material.
        */
        [Required]
        public int ID { get; set; }

        /*
            Name of the material.
        */
        [Required]
        public string Name { get; set; }

        /*
            Collection of DTO's for finishes that were already part of the material.
        */
        [Required]
        public ICollection<FinishDto> AlreadyExistentFinishes { get; set; }

        /*
            Collection of DTO's for finishes that weren't already part of the material.
        */
        [Required]
        public ICollection<NewFinishDto> NewFinishes { get; set; }
    }
}