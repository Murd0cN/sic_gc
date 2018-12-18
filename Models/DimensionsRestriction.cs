using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Arqsi_1160752_1161361_3DF.Models
{
    /*
        Restricton regarding the dimensions (describes a maximum and a minimum value for each dimension).
    */
    public class DimensionsRestriction : Restriction
    {
        /*
            Minimum allowed value for the height.
        */
        public float MinHeightValue { get; set; }

        /*
            Maximum allowed value for the height.
        */
        public float MaxHeightValue { get; set; }

        /*
            Minimum allowed value for the width.
        */
        public float MinWidthValue { get; set; }

        /*
            Maximum allowed value for the width.
        */
        public float MaxWidthValue { get; set; }

        /*
            Minimum allowed value for the depth.
        */
        public float MinDepthValue { get; set; }

        /*
            Maximum allowed value for the depth.
        */
        public float MaxDepthValue { get; set; }
    }
}