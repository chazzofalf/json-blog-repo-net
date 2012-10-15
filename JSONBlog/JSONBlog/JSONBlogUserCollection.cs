using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
namespace JSONBlog
{
    class JSONBlogUserCollection
    {
        
        public const String DIRECTORY_NAME = "Users";     
        private static DirectoryInfo UserDirectory
        {
            get
            {
                DirectoryInfo[] receiver = new DirectoryInfo[1];
                if (AppData.FindDirectory(DIRECTORY_NAME, receiver))
                {
                    return receiver[0];
                }
                else
                {
                    return AppData.CreateAppDataDirectory(DIRECTORY_NAME);
                }
            }
        }
        private static DirectoryInfo[] UserDirectories
        {
            get
            {
                return UserDirectory.GetDirectories();
            }
        }
        private static JSONBlogUser[] Users
        {

        }
        public static void addUser(string userName, string password)
        {
        }     
    }

}
