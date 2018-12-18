using System.Collections.Generic;
using System.Linq;
using Arqsi_1160752_1161361_3DF.Dtos;
using Arqsi_1160752_1161361_3DF.Models;

namespace Arqsi_1160752_1161361_3DF.Data.Factory
{
    public class PossibleValuesFactory : IPossibleValuesFactory
    {
        /*
            Creates a new instance of PossibleValues base on the specified instance of NewDimensionsDto.
        */
        public PossibleValues CreateNewPossibleValuesForDimension(PossibleValuesOfDimensionDto dimensionDto)
        {
            if (dimensionDto.IsDiscrete)
            {
                DiscretePossibleValues discreteValues = new DiscretePossibleValues();
                discreteValues.PossibleValues = new HashSet<Float>();

                foreach (float value in dimensionDto.Values)
                {
                    Float newFloat = new Arqsi_1160752_1161361_3DF.Models.Float() { FloatValue = value };
                    discreteValues.PossibleValues.Add(newFloat);
                }

                return discreteValues;
            }

            ContinuousPossibleValues continuousValues = new ContinuousPossibleValues();
            List<float> values = TransformIcollection(dimensionDto.Values);

            if (values.Count != 2)
            {
                return null;
            }

            values.Sort();
            continuousValues.MinValue = values[0];
            continuousValues.MaxValue = values[1];

            return continuousValues;
        }

        /*
            Returns a List of floats with the same elements as the specified collection of floats.
        */
        private List<float> TransformIcollection(ICollection<float> collection)
        {
            List<float> floatList = new List<float>();

            foreach (float f in collection)
            {
                floatList.Add(f);
            }

            return floatList;
        }
    }
}
