using System.ComponentModel.DataAnnotations;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    public class NewCategoryDto
    {
        [Required]
        public string CategoryName { get; set; }

        public string ParentCategoryName { get; set; }
    }
}