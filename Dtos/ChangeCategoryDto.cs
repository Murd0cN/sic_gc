using System.ComponentModel.DataAnnotations;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    public class ChangeCategoryDto
    {
        [Required]
        public int CategoryID { get; set; }

        [Required]
        public int ParentCategoryID { get; set; }
    }
}