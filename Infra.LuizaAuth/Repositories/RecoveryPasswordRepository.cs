using Domain.LuizaAuth.Entities;
using Domain.LuizaAuth.Interfaces;
using Infra.LuizaAuth.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.LuizaAuth.Repositories
{
    public class RecoveryPasswordRepository : Repository<RecoveryPassword>, IRecoveryPasswordRepository
    {
        public RecoveryPasswordRepository(LuizaAuthContext context) : base(context) { }
    }
}
