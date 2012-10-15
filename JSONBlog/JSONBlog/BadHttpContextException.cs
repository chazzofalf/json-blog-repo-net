using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSONBlog
{
    class BadHttpContextException : Exception
    {
        public override string Message
        {
            get
            {
                return "No Current HTTP Context Exists";
            }
        }
    }
}
