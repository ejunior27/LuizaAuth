using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.LuizaAuth.Exceptions
{
    [Serializable]
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {

        }
    }
}
