using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arqsi_1160752_1161361_3DF.Models
{
    /*
        Describes the relationship between two products. Specifies which one of them is the parent and which is the child.
        Also hold the restriction for the relationship.
    */
    public class ProductRelationship
    {
        /*
            ID for the relationship.
        */
        public int ID { get; set; }

        /*
            ID of the parent product.
        */
        [ForeignKey("ParentProduct")]
        public int ParentProductID { get; set; }

        /*
            ID of the child product.
        */
        [ForeignKey("ChildProduct")]
        public int ChildProductID { get; set; }

        /*
            Parent product.
        */
        public Product ParentProduct { get; set; }

        /*
            Child product.
        */
        public Product ChildProduct { get; set; }

        /*
            Defines if the parent product must always have the child product or if it's optional.
        */
        public bool IsMandatory { get; set; }

        /*
            Restrictions for the relationship.
        */
        public ICollection<Restriction> Restrictions { get; set; }

        /*
            Verifies if the product relationship is material restricted.
        */
        public bool HasMaterialRestriction()
        {
            bool hasMaterialRestriction = false;

            foreach (Restriction r in Restrictions)
            {
                if (r is MaterialRestriction)
                {
                    hasMaterialRestriction = true;
                    break;
                }
            }

            return hasMaterialRestriction;
        }

        /*
            Removes the material restriction of the product relationship (if there
            is any, there should only be one). Returns true if the material restriction
            is successfully removed and false otherwise.
        */
        public bool RemoveMaterialRestriction()
        {
            bool wasRemoved = false;

            foreach (Restriction r in Restrictions)
            {
                if (r is MaterialRestriction)
                {
                    wasRemoved = Restrictions.Remove(r);
                    break;
                }
            }

            return wasRemoved;
        }

        /*
            Removes the percentage restriction of the product relationship (if there
            is any, there should only be one). Returns true if the percentage restriction
            is successfully removed and false otherwise.
        */
        public bool RemovePercentageRestriction()
        {
            bool wasRemoved = false;

            foreach (Restriction r in Restrictions)
            {
                if (r is PercentageRestriction)
                {
                    wasRemoved = Restrictions.Remove(r);
                    break;
                }
            }

            return wasRemoved;
        }

        /*
            Finds the material restriction of the relationship, if there is one.
            If there isn't, null is returned.
        */
        public MaterialRestriction GetMaterialRestriction()
        {
            MaterialRestriction materialRestriction = null;

            foreach (Restriction r in Restrictions)
            {
                if (r is MaterialRestriction)
                {
                    materialRestriction = (MaterialRestriction)r;
                    break;
                }
            }

            return materialRestriction;
        }

        /*
            Finds the percentage restriction of the relationship, if there is one.
            If there isn't, null is returned.
        */
        public PercentageRestriction GetPercentageRestriction()
        {
            PercentageRestriction percentageRestriction = null;

            foreach (Restriction r in Restrictions)
            {
                if (r is PercentageRestriction)
                {
                    percentageRestriction = (PercentageRestriction)r;
                    break;
                }
            }

            return percentageRestriction;
        }
    }
}