using System.ComponentModel.DataAnnotations;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    public class FinishDto
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}