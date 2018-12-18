using System.Collections.Generic;
using System.Linq;
using Arqsi_1160752_1161361_3DF.Dtos;
using Arqsi_1160752_1161361_3DF.Models;

namespace Arqsi_1160752_1161361_3DF.Data.Factory
{
    public class RestrictionFactory : IRestrictionFactory
    {
        /*
            String to identify the restriction as a dimensions restriction.
        */
        private const string DimensionsRestrictionTypeString = "Dimensions Restriction";
        /*
            String to identify the restriction as a material restriction.
        */
        private const string MaterialRestrictionTypeString = "Material Restriction";

        private const string PercentageRestrictionTypeString = "Percentage Restriction";

        /*
            Builds a DTO for a GET request of a restriction according to the specified restriction and its type.
        */
        public GetRestrictionDto CreatesRestrictionDto(Restriction restriction)
        {
            GetRestrictionDto dto;

            if (restriction is DimensionsRestriction)
            {
                DimensionsRestriction dimsRestriction = (DimensionsRestriction)restriction;

                dto = new GetDimensionsRestrictionDto
                {
                    RestrictionType = DimensionsRestrictionTypeString,
                    MinHeightValue = dimsRestriction.MinHeightValue,
                    MaxHeightValue = dimsRestriction.MaxHeightValue,
                    MinWidthValue = dimsRestriction.MinWidthValue,
                    MaxWidthValue = dimsRestriction.MaxWidthValue,
                    MinDepthValue = dimsRestriction.MinDepthValue,
                    MaxDepthValue = dimsRestriction.MaxDepthValue
                };
            }
            else
            {
                if (restriction is MaterialRestriction)
                {
                    MaterialRestriction matRestriction = (MaterialRestriction)restriction;

                    dto = new GetMaterialRestrictionDto
                    {
                        RestrictionType = MaterialRestrictionTypeString
                    };
                }
                else
                {
                    PercentageRestriction percentRestriction = (PercentageRestriction)restriction;

                    dto = new GetPercentageRestrictionDto
                    {
                        RestrictionType = PercentageRestrictionTypeString,
                        MinHeightPercentage = percentRestriction.MinHeightPercentage,
                        MaxHeightPercentage = percentRestriction.MaxHeightPercentage,
                        MinWidthPercentage = percentRestriction.MinWidthPercentage,
                        MaxWidthPercentage = percentRestriction.MaxWidthPercentage,
                        MinDepthPercentage = percentRestriction.MinDepthPercentage,
                        MaxDepthPercentage = percentRestriction.MaxDepthPercentage
                    };
                }
            }

            dto.ID = restriction.ID;

            return dto;
        }

        /*
           Creates a mew instance of DimensionsRestriction using the minimum and maximum values of the
           specified parent product for each dimension.
           If the specified child product doesn't fit inside the specified parent product, the return
           value is null.
        */
        public DimensionsRestriction CreateDimensionsRestriction(Product parentProduct, Product childProduct)
        {
            PossibleValues parentHeightPossibleValues = parentProduct.PossibleDimensions.HeightPossibleValues;
            PossibleValues parentWidthPossibleValues = parentProduct.PossibleDimensions.WidthPossibleValues;
            PossibleValues parentDepthPossibleValues = parentProduct.PossibleDimensions.DepthPossibleValues;

            float parentMinHeight = FindMinValueOfPossibleValues(parentHeightPossibleValues);
            float parentMaxHeight = FindMaxValueOfPossibleValues(parentHeightPossibleValues);
            float parentMinWidth = FindMinValueOfPossibleValues(parentWidthPossibleValues);
            float parentMaxWidth = FindMaxValueOfPossibleValues(parentWidthPossibleValues);
            float parentMinDepth = FindMinValueOfPossibleValues(parentDepthPossibleValues);
            float parentMaxDepth = FindMaxValueOfPossibleValues(parentDepthPossibleValues);

            PossibleValues childHeightPossibleValues = childProduct.PossibleDimensions.HeightPossibleValues;
            PossibleValues childWidthPossibleValues = childProduct.PossibleDimensions.WidthPossibleValues;
            PossibleValues childDepthPossibleValues = childProduct.PossibleDimensions.DepthPossibleValues;

            float childMinHeight = FindMinValueOfPossibleValues(childHeightPossibleValues);
            float childMaxHeight = FindMaxValueOfPossibleValues(childHeightPossibleValues);
            float childMinWidth = FindMinValueOfPossibleValues(childWidthPossibleValues);
            float childMaxWidth = FindMaxValueOfPossibleValues(childWidthPossibleValues);
            float childMinDepth = FindMinValueOfPossibleValues(childDepthPossibleValues);
            float childMaxDepth = FindMaxValueOfPossibleValues(childDepthPossibleValues);

            DimensionsRestriction newDimensionsRestriction = null;

            if (childMinHeight <= parentMaxHeight &&
                childMinWidth <= parentMaxWidth &&
                childMinDepth <= parentMaxDepth)
            {
                newDimensionsRestriction = new DimensionsRestriction
                {
                    MinHeightValue = childMinHeight,
                    MaxHeightValue = parentMaxHeight,
                    MinWidthValue = childMinWidth,
                    MaxWidthValue = parentMaxWidth,
                    MinDepthValue = childMinDepth,
                    MaxDepthValue = parentMaxDepth
                };
            }


            return newDimensionsRestriction;
        }

        /*
            Finds the minimum value of the specified values.
        */
        public float FindMinValueOfPossibleValues(PossibleValues possibleValues)
        {
            float min;

            if (possibleValues is DiscretePossibleValues)
            {
                DiscretePossibleValues discreteValues = (DiscretePossibleValues)possibleValues;
                min = discreteValues.PossibleValues.Min(f => f.FloatValue);
            }
            else
            {
                ContinuousPossibleValues continuousValues = (ContinuousPossibleValues)possibleValues;
                min = continuousValues.MinValue;
            }

            return min;
        }

        /*
            Finds the maximum value of the specified values.
        */
        public float FindMaxValueOfPossibleValues(PossibleValues dimensionValues)
        {
            float max;

            if (dimensionValues is DiscretePossibleValues)
            {
                DiscretePossibleValues discreteValues = (DiscretePossibleValues)dimensionValues;
                max = discreteValues.PossibleValues.Max(f => f.FloatValue);
            }
            else
            {
                ContinuousPossibleValues continuousValues = (ContinuousPossibleValues)dimensionValues;
                max = continuousValues.MaxValue;
            }

            return max;
        }

        /*
            Checks if the specified material is in the specified collection of ProductMaterialRelationship.
        */
        private bool productHasMaterial(Material material, ICollection<ProductMaterialRelationship> productMaterialRelationships)
        {
            bool boolReturn = false;

            foreach (ProductMaterialRelationship relat in productMaterialRelationships)
            {
                if (material.Equals(relat.Material))
                {
                    boolReturn = true;
                    break;
                }
            }

            return boolReturn;
        }

        /*
            Takes the collections of the ProductMaterialRelationship's of the parent product and of the child product and
            checks if the child has any of the materials of the parent. If so, then a material restriction is returned.
            If not, the child cannot satisfy the restriction and the return is null.
        */
        public MaterialRestriction CreateMaterialRestriction(ICollection<ProductMaterialRelationship> parentProductMaterialRelats,
            ICollection<ProductMaterialRelationship> childProductMaterialRelats)
        {
            MaterialRestriction materialRestriction = null;

            foreach (ProductMaterialRelationship relat in parentProductMaterialRelats)
            {
                if (productHasMaterial(relat.Material, childProductMaterialRelats))
                {
                    materialRestriction = new MaterialRestriction();
                    break;
                }
            }

            return materialRestriction;
        }

        /*
            Creates a new instance of PercentageRestriction based on the specified DTO. Checks (optimistically) if the child product
            fits in the parent product with the percentages specified in the DTO, meaning that, for instance, if the greatest possible
            value for the child's height is bigger than the minimum possible value for the parent's height multiplied by the minimum
            percentage for the height, the minimum percentage for the height is possible. If any of the restriction's percentages
            doesn't fit, the return is null.
        */
        public PercentageRestriction CreatePercentageRestriction(NewPercentageRestrictionDto dto,
            Product parentProduct, Product childProduct)
        {
            PercentageRestriction restriction = null;

            if (dto.MinHeightPercentage <= dto.MaxHeightPercentage && dto.MinWidthPercentage <= dto.MaxWidthPercentage
                && dto.MinDepthPercentage <= dto.MaxDepthPercentage)
            {
                float minParentHeight = FindMinValueOfPossibleValues(parentProduct.PossibleDimensions.HeightPossibleValues);
                float maxParentHeight = FindMaxValueOfPossibleValues(parentProduct.PossibleDimensions.HeightPossibleValues);
                float minParentWidth = FindMinValueOfPossibleValues(parentProduct.PossibleDimensions.WidthPossibleValues);
                float maxParentWidth = FindMaxValueOfPossibleValues(parentProduct.PossibleDimensions.WidthPossibleValues);
                float minParentDepth = FindMinValueOfPossibleValues(parentProduct.PossibleDimensions.DepthPossibleValues);
                float maxParentDepth = FindMaxValueOfPossibleValues(parentProduct.PossibleDimensions.DepthPossibleValues);

                float minChildHeight = FindMinValueOfPossibleValues(childProduct.PossibleDimensions.HeightPossibleValues);
                float maxChildHeight = FindMaxValueOfPossibleValues(childProduct.PossibleDimensions.HeightPossibleValues);
                float minChildWidth = FindMinValueOfPossibleValues(childProduct.PossibleDimensions.WidthPossibleValues);
                float maxChildWidth = FindMaxValueOfPossibleValues(childProduct.PossibleDimensions.WidthPossibleValues);
                float minChildDepth = FindMinValueOfPossibleValues(childProduct.PossibleDimensions.DepthPossibleValues);
                float maxChildDepth = FindMaxValueOfPossibleValues(childProduct.PossibleDimensions.DepthPossibleValues);

                // verifies if the height restriction is possible
                if ((maxChildHeight >= minParentHeight * dto.MinHeightPercentage) && (minChildHeight <= maxParentHeight * dto.MaxHeightPercentage))
                {
                    // verifies if the width restriction is possible
                    if ((maxChildWidth >= minParentWidth * dto.MinWidthPercentage) && (minChildWidth <= maxParentWidth * dto.MaxWidthPercentage))
                    {
                        // verifies if the depth restriction is possible
                        if ((maxChildDepth >= minParentDepth * dto.MinDepthPercentage) && (minChildDepth <= maxParentDepth * dto.MaxDepthPercentage))
                        {
                            restriction = new PercentageRestriction
                            {
                                MinHeightPercentage = dto.MinHeightPercentage,
                                MaxHeightPercentage = dto.MaxHeightPercentage,
                                MinWidthPercentage = dto.MinWidthPercentage,
                                MaxWidthPercentage = dto.MaxWidthPercentage,
                                MinDepthPercentage = dto.MinDepthPercentage,
                                MaxDepthPercentage = dto.MaxDepthPercentage
                            };
                        }
                    }
                }
            }

            return restriction;
        }
    }
}