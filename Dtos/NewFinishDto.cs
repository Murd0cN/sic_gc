using System.ComponentModel.DataAnnotations;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    /*
        DTO for the creation of a new finish.
    */
    public class NewFinishDto
    {
        /*
            Name of the finish.
        */
        [Required]
        public string Name { get; set; }
    }
}