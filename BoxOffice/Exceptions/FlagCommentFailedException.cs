using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoxOffice.Controllers
{
    class FlagCommentFailedException : Exception
    {
        public override string Message
        {
            get
            {
                return "flagging of comment failed";
            }
        }
    }
}
