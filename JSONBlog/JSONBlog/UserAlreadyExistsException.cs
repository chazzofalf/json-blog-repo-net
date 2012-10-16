using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSONBlog
{
    class UserAlreadyExistsException : Exception
    {
        public override string Message
        {
            get
            {
                return "This user already exists";
            }
        }
    }
}
