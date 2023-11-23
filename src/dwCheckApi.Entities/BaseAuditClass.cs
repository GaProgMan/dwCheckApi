using System;

namespace dwCheckApi.Entities
{
    public class BaseAuditClass 
    {
        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}