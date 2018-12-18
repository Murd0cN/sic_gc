using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Arqsi_1160752_1161361_3DF.Models
{
    /*
        This class represents the possible values for the three dimensions (height, width and depth) of something.
    */
    public class PossibleDimensions
    {
        /*
            ID of the possible dimensions.
        */
        public int ID { get; set; }

        /*
            ID of the possible values for the height.
        */
        [ForeignKey("HeightPossibleValues")]
        public int HeightPossibleValuesID { get; set; }

        /*
            ID of the possible values for the width.
        */
        [ForeignKey("WidthPossibleValues")]
        public int WidthPossibleValuesID { get; set; }

        /*
            ID of the possible value for the depth.
        */
        [ForeignKey("DepthPossibleValues")]
        public int DepthPossibleValuesID { get; set; }

        /*
            Possible values for the height.
        */
        public PossibleValues HeightPossibleValues { get; set; }

        /*
            Possible values for the width.
        */
        public PossibleValues WidthPossibleValues { get; set; }

        /*
            Possible values for the depth.
        */
        public PossibleValues DepthPossibleValues { get; set; }

        /*
            Creates a string with the description of the possible values of every dimension.
        */
        public string DescribePossibleDimensions()
        {
            StringBuilder dimensionsString = new StringBuilder();

            dimensionsString.AppendFormat("Height -> {0}", HeightPossibleValues.DescribePossibleValues());
            dimensionsString.AppendFormat(" - Width -> {0}", WidthPossibleValues.DescribePossibleValues());
            dimensionsString.AppendFormat(" - Depth -> {0}", DepthPossibleValues.DescribePossibleValues());

            return null;
        }
    }
}