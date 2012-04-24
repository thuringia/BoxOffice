using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoxOffice.Exceptions
{
    [Serializable]
    class RequestFailedException : Exception
    {
        override string Message
        {
            get
            {
                return "request failed";
            }
        }
    }
}
