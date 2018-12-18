using System;

namespace Arqsi_1160752_1161361_3DF.Models
{
    /*
        This class represents a finish.
    */
    public class Finish
    {
        /*
            ID of the finish.
        */
        public int ID { get; set; }

        /*
            Name of the finish.
        */
        public string FinishName { get; set; }

        /*
            Override of the Equals function. Uses the name of the finishes to make the comparison.
        */
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Finish finish = (Finish)obj;
                return (FinishName == finish.FinishName);
            }
        }

        /*
            Override of the GetHashCode function.
        */
        public override int GetHashCode() => HashCode.Combine(FinishName);
    }
}