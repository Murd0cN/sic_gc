using System.ComponentModel.DataAnnotations;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    public class ChangeCategoryWithNamesDto
    {
        [Required]
        public string category { get; set; }

        [Required]
        public string parentCagetory { get; set; }
    }
}