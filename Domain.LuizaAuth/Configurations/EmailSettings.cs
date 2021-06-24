using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.LuizaAuth.Configurations
{
    public class EmailSettings
    {
        public string PrimaryDomain { get; set; }
        public int PrimaryPort { get; set;  }
        public string UsernameEmail { get; set; }
        public string UsernamePassword { get; set; }
        public string FromEmail { get; set; }
    }
}
