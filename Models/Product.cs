using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arqsi_1160752_1161361_3DF.Models
{
    public class Product
    {
        /*
            ID of the product.
        */
        public int ID { get; set; }

        /*
            Name of the product.
        */
        public string Name { get; set; }

        /*
            Price of the product.
        */
        public float Price { get; set; }

        /*
            ID of the category of the product.
        */
        [ForeignKey("ProductCategory")]
        public int ProductCategoryID { get; set; }

        /*
            ID of the possible dimensions of the product.
        */
        [ForeignKey("PossibleDimensions")]
        public int PossibleDimensionsID { get; set; }

        /*
            Category of the product.
        */
        public Category ProductCategory { get; set; }

        /*
            Possible values for all the dimensions of the product.
        */
        public PossibleDimensions PossibleDimensions { get; set; }
    }
}