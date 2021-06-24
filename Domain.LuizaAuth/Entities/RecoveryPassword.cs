using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.LuizaAuth.Entities
{
    public class RecoveryPassword
    {
        public Guid ID { get; set; }
        public long AccountId { get; set; }
        public DateTime Created { get; set; }
    }
}
