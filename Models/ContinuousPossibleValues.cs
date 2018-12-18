using System;
using System.Text;

namespace Arqsi_1160752_1161361_3DF.Models
{
    /*
        This class represents a continuous interval of floating point values. 
    */
    public class ContinuousPossibleValues : PossibleValues
    {
        /*
            Minimum value.
        */
        public float MinValue { get; set; }

        /*
            Maximum value.
        */
        public float MaxValue { get; set; }

        /*
            Builds a string with all the possible values.
        */
        public override string DescribePossibleValues()
        {
            StringBuilder valuesString = new StringBuilder();
            valuesString.AppendFormat("Min value = {0}; Max value = {1}", MinValue, MaxValue);

            return valuesString.ToString();
        }
    }
}