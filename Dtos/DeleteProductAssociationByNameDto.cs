using System.ComponentModel.DataAnnotations;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    public class DeleteProductAssociationByNameDto
    {
        [Required]
        public string ParentName { get; set; }

        [Required]
        public string ChildName { get; set; }
    }
}