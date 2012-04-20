using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoxOffice.Controllers
{
    class AddToQueueFailedException : Exception
    {
        public override string Message
        {
            get
            {
                return "adding to queue failed";
            }
        }
    }
}
