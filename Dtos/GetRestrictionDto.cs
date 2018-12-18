using System.ComponentModel.DataAnnotations;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    public abstract class GetRestrictionDto
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string RestrictionType { get; set; }
    }
}