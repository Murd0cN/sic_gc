using System;

namespace Arqsi_1160752_1161361_3DF.Exceptions
{
    public class EntityNotFoundException : Exception
    {

        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
}