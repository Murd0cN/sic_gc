using System.ComponentModel.DataAnnotations.Schema;

namespace Arqsi_1160752_1161361_3DF.Models
{
    public class ProductMaterialRelationship
    {
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("Material")]
        public int MaterialId { get; set; }

        public Product Product { get; set; }

        public Material Material { get; set; }
    }
}