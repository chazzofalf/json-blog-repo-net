using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSONBlog
{
    class FailedToCreateUserInfoFileException :Exception
    {
        public override string Message
        {
            get
            {
                return "Failed to create user info file.";
            }
        }
    }
}
