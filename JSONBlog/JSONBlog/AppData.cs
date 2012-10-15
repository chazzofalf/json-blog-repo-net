using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
namespace JSONBlog
{
    class AppData
    {
        public static HttpContext CurrentContext
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    return HttpContext.Current;
                }
                throw new BadHttpContextException();
            }
        }
        public static String AppDataPath
        {
            get
            {
                return CurrentContext.Request.MapPath("~/App_Data");
            }
        }
        public static DirectoryInfo AppDataDirectory
        {
            get
            {
                return new DirectoryInfo(AppDataPath);
            }
        }
        public static DirectoryInfo CreateAppDataDirectory(string name)
        {
            return AppDataDirectory.CreateSubdirectory(name);
        }
        public static DirectoryInfo GetAppDataDirectory(string name)
        {
            foreach (DirectoryInfo subdir in AppDataDirectory.GetDirectories())
            {
                if (subdir.Name.CompareTo(name) == 0)
                {
                    return subdir;
                }
            }
            return null;
        }
        public static bool FindDirectory(string name,params DirectoryInfo[][] receiver)
        {
            DirectoryInfo gotDirectory = GetAppDataDirectory(name);
            if (gotDirectory != null)
            {
                if (receiver.Length == 1 && receiver[0].Length == 1)
                {
                    receiver[0][0] = gotDirectory;
                }
                return true;
            }
            return false;
        }
    }
}
