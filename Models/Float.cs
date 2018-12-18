using System;

namespace Arqsi_1160752_1161361_3DF.Models
{
    /*
        Wrapper entity for a float value.
    */
    public class Float
    {
        /*
            ID of the float wrapper.
        */
        public int ID { get; set; }

        /*
            Value of the dimension.
        */
        public float FloatValue { get; set; }

        /*
            Override of the Equals method for the Float class. Uses the stored
            float value to make the comparison.
        */
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Float floatValue = (Float)obj;
                return (FloatValue == floatValue.FloatValue);
            }
        }

        /*
            Override of the GetHashCode function.
        */
        public override int GetHashCode() => HashCode.Combine(FloatValue);
    }
}