using System;
using System.Collections.Generic;

namespace Arqsi_1160752_1161361_3DF.Models
{
    /*
        Class that represents a material.
    */
    public class Material
    {
        /*
            ID of the material.
        */
        public int ID { get; set; }

        /*
            Name of the material.
        */
        public string MaterialName { get; set; }

        /*
            Available finishes for the material.
        */
        public ICollection<Finish> AvailableFinishes { get; set; }

        /*
            Checks if the material as a finish with the specified ID.
        */
        public bool HasFinishWithId(int id)
        {
            bool r = false;

            foreach (Finish f in AvailableFinishes)
            {
                if (f.ID == id)
                {
                    r = true;
                    break;
                }
            }

            return r;
        }

        /*
            Override of the Equals function. Uses the names of the materials to make the comparison.
        */
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Material material = (Material)obj;
                return (MaterialName == material.MaterialName);
            }
        }

        /*
            Override of the GetHashCode function.
        */
        public override int GetHashCode() => HashCode.Combine(MaterialName);
    }
}