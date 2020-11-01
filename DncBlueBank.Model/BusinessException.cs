using System;
using System.Collections.Generic;
using System.Text;

namespace DncBlueBank.Model
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {

        }
    }
}
