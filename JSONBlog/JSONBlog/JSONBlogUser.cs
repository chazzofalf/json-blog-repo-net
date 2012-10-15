using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
namespace JSONBlog
{
    [Serializable()]
    class JSONBlogUser
    {
        private const String INFO_FILE = "userInfo.json";       
        private DirectoryInfo userDirectory;
        [Serializable()]
        private string username;
        [Serializable()]
        private string passHash;
        [Serializable()]
        private int position;
        public string Password
        {
            get
            {
            }
            set
            {

            }
        }
        private string passwordToHash(string password)
        {
            SHA512Managed sha512 = new SHA512Managed();
             

        }

        public string Username
        {
            get
            {
                return username;
            }
        }
        public JSONBlogUser(DirectoryInfo userDirectory)
        {
            this.userDirectory = userDirectory;
            
        }        
        public JSONBlogUser(DirectoryInfo userDirectory,string username,string password)
        {

        }
    }
}
