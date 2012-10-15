using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Security.Cryptography;
namespace JSONBlog
{
    public class JSONBlogUserCollection
    {
        
        public const String DIRECTORY_NAME = "Users";
        
        private DirectoryInfo UserDirectory
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
        private  DirectoryInfo[] UserDirectories
        {
            get
            {
                return UserDirectory.GetDirectories();
            }
        }
        private  JSONBlogUser[] Users
        {
            
            get
            {
                List<JSONBlogUser> userList = new List<JSONBlogUser>();
                foreach (DirectoryInfo info in UserDirectories)
                {
                    userList.Add(new JSONBlogUser(info));
                }
                return userList.ToArray();
            }
        }
        public  void addUser(string userName, string password)
        {
            DirectoryInfo userDirectory = createRandomDirectory();
            JSONBlogUser user = new JSONBlogUser(userDirectory, userName, password);
        }
        private DirectoryInfo createRandomDirectory()
        {
            string randomName = createRandomName();
            string userDirectoryPath = UserDirectory.FullName;
            string fullpath = Path.Combine(userDirectoryPath, randomName);
            DirectoryInfo info = new DirectoryInfo(fullpath);
            info.Create();
            return info;
        }
        private string createRandomName()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < 32; i++)
            {
                builder.Append(createRandomDirectoryNameCharacter());
            }
            return builder.ToString();
        }
        private char createRandomDirectoryNameCharacter()
        {
            int pickValue = randomInt(0, 36);
            if (inRange(pickValue, 0, 10))
            {
                return (char)('0' + pickValue);
            }
            else if (inRange(pickValue, 10, 36))
            {
                return (char)('a' + pickValue - 10);
            }
            else
            {
                throw new Exception("Bad Character");
            }

        }
        private bool inRange(int me,int low, int high)
        {
            return me >= low && me < high;
        }
        private int randomInt()
        {
            byte[] intpick = new byte[4];
            RNGCryptoServiceProvider.Create().GetBytes(intpick);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(intpick);
            }
            int picked = BitConverter.ToInt32(intpick, 0);
            return picked;
        }
        private int randomInt(int low, int high)
        {
            int range = high - low;
            int pick = randomInt() % range;
            if (pick < 0)
            {
                pick += 36;
            }
            pick += low;
            return pick;
        }
    }

}
