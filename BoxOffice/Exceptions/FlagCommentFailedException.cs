using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoxOffice.Exceptions
{
    [Serializable]
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
