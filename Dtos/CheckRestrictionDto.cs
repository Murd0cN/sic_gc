using System.ComponentModel.DataAnnotations;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    public class CheckRestrictionDto
    {
        [Required]
        public ItemDto parent { get; set; }
        [Required]
        public ItemDto child { get; set; }
    }
}