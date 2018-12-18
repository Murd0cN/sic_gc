using System.ComponentModel.DataAnnotations;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    public class ItemDto
    {
        [Required]
        public int productId { get; set; }

        public int width { get; set; }

        public int height { get; set; }

        public int depth { get; set; }

        public string material { get; set; }
    }
}