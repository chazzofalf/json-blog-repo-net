using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSONBlog
{
    class InvalidLoginException : Exception
    {
        public override string Message
        {
            get
            {
                return "You have performed an invalid login";
            }
        }
    }
}
