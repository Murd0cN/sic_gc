using System;
using System.Collections.Generic;
using System.Text;

namespace Arqsi_1160752_1161361_3DF.Models
{
    /*
        Class that represents a discrete set of floating point values.
    */
    public class DiscretePossibleValues : PossibleValues
    {
        /*
            List of floating point values.
        */
        public ICollection<Float> PossibleValues { get; set; }

        /*
            Builds a string with all the possible values.
        */
        public override string DescribePossibleValues()
        {
            StringBuilder valuesString = new StringBuilder();
            int cont = 0;
            foreach (Float f in PossibleValues)
            {
                if (cont == 0)
                {
                    valuesString.AppendFormat(" {0}", f.FloatValue);
                }
                else
                {
                    valuesString.AppendFormat("; {0}", f.FloatValue);
                }

                cont++;
            }

            return valuesString.ToString();
        }
    }
}