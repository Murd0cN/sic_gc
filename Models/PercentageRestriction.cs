namespace Arqsi_1160752_1161361_3DF.Models
{
    public class PercentageRestriction : Restriction
    {
        /*
            Minimum ocupation percentage for the height.
        */
        public float MinHeightPercentage { get; set; }

        /*
            Maximum ocupation percentage for the height.
        */
        public float MaxHeightPercentage { get; set; }

        /*
            Minimum ocupation percentage for the width.
        */
        public float MinWidthPercentage { get; set; }

        /*
            Maximum ocupation percentage for the width.
        */
        public float MaxWidthPercentage { get; set; }

        /*
            Minimum ocupation percentage for the depth.
        */
        public float MinDepthPercentage { get; set; }

        /*
            Maximum ocupation percentage for the depth.
        */
        public float MaxDepthPercentage { get; set; }
    }
}