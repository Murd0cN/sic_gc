using System.Collections.Generic;
using Arqsi_1160752_1161361_3DF.Dtos;
using Arqsi_1160752_1161361_3DF.Models;

namespace Arqsi_1160752_1161361_3DF.Data.Factory
{
    public interface IRestrictionFactory
    {
        /*
            Builds a DTO for a GET request of a restriction according to the specified restriction and its type.
        */
        GetRestrictionDto CreatesRestrictionDto(Restriction restriction);

        /*
           Creates a mew instance of DimensionsRestriction using the minimum and maximum values of the
           specified parent product for each dimension.
           If the specified child product doesn't fit inside the specified parent product, the return
           value is null.
        */
        DimensionsRestriction CreateDimensionsRestriction(Product parentProduct, Product childProduct);

        /*
            Takes the collections of the ProductMaterialRelationship's of the parent product and of the child product and
            checks if the child has any of the materials of the parent. If so, then a material restriction is returned.
            If not, the child cannot satisfy the restriction and the return is null.
        */
        MaterialRestriction CreateMaterialRestriction(ICollection<ProductMaterialRelationship> parentProductMaterialRelats,
            ICollection<ProductMaterialRelationship> childProductMaterialRelats);

        /*
            Creates a new instance of PercentageRestriction based on the specified DTO. Checks (optimistically) if the child product
            fits in the parent product with the percentages specified in the DTO, meaning that, for instance, if the greatest possible
            value for the child's height is bigger than the minimum possible value for the parent's height multiplied by the minimum
            percentage for the height, the minimum percentage for the height is possible. If any of the restriction's percentages
            doesn't fit, the return is null.
        */
        PercentageRestriction CreatePercentageRestriction(NewPercentageRestrictionDto dto,
            Product parentProduct, Product childProduct);
    }
}