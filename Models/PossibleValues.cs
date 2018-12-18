namespace Arqsi_1160752_1161361_3DF.Models
{
    /*
        Interface for the representation of possible values.
    */
    public abstract class PossibleValues
    {
        /*
            ID of the set of values.
        */
        public int ID { get; set; }

        /*
            Builds a string that represents the possible values.
        */
        public abstract string DescribePossibleValues();
    }
}