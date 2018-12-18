using Arqsi_1160752_1161361_3DF.Dtos;
using Arqsi_1160752_1161361_3DF.Models;

namespace Arqsi_1160752_1161361_3DF.Data.Factory
{
    public interface IPossibleValuesFactory
    {
        PossibleValues CreateNewPossibleValuesForDimension(PossibleValuesOfDimensionDto newDimensionsDto);
    }
}