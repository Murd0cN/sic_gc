using System.Collections.Generic;
using Arqsi_1160752_1161361_3DF.Dtos;
using Arqsi_1160752_1161361_3DF.Models;

namespace Arqsi_1160752_1161361_3DF.services
{
    public class ValidateRestriction
    {
        public bool CheckRestriction(ItemDto parent, ItemDto child, ICollection<Restriction> list){

            foreach (Restriction r in list)
            {
                if(r is PercentageRestriction) {
                    PercentageRestriction p1 = (PercentageRestriction) r;
                    if(!ValidatePercentage(p1, parent, child)) {
                        return false;
                    }
                }

                if(r is MaterialRestriction) {
                    MaterialRestriction p2 = (MaterialRestriction) r;
                    if(!ValidateMaterial(p2,parent, child)) {
                        return false;
                    }
                }

                if(r is DimensionsRestriction) {
                    DimensionsRestriction p3 = (DimensionsRestriction) r;
                    if(!ValidateDimensions(p3, child)) {
                        return false;
                    }
                }

            }

            return true;
        }

        private bool ValidateMaterial(MaterialRestriction p, ItemDto parent, ItemDto child) {
            if(parent.material != child.material) {
                return false;
            }   

            return true;
        }

        private bool ValidatePercentage(PercentageRestriction p, ItemDto parent, ItemDto child) {
            
            int widthRatio = child.width * 100 / parent.width;

            if(widthRatio > p.MaxWidthPercentage || widthRatio < p.MinWidthPercentage) {
                return false;
            }

            int height = child.height * 100 / parent.height;

            if(height > p.MaxHeightPercentage || height < p.MinHeightPercentage) {
                return false;
            }

            int depth = child.depth * 100 / parent.depth;

            if(depth > p.MaxDepthPercentage || depth < p.MinDepthPercentage) {
                return false;
            }

            return true;
        }

        private bool ValidateDimensions(DimensionsRestriction p, ItemDto child) {

            if(child.width < p.MinWidthValue || child.width > p.MaxWidthValue) {
                return false;
            }

            if(child.height < p.MinHeightValue || child.height > p.MaxHeightValue) {
                return false;
            }

            if(child.depth < p.MinDepthValue || child.depth > p.MaxDepthValue) {
                return false;
            }

            return true;
        }
    }
}