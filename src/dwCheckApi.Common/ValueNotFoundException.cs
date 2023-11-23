using System;

namespace dwCheckApi.Common
{
    public class ValueNotFoundException : Exception
    {
        public ValueNotFoundException(string message) : base(message)
        {

        }
    }
}