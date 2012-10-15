//Created By: Charles Montgomery (2012.10.15 14:26)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;
namespace JSONBlog
{
    /// <summary>
    /// Represents a the state of a registered user of this blog system.
    /// 
    /// </summary>
    /// 
    [Serializable()]
    public class JSONBlogUserState
    {
        
        public JSONBlogUserState(string username)
        {
            this.username = username;
        }
        private string username;
        
        private string passHash;
        
        private int position;
        public int Position
        {
            get
            {
                return position;
            }
        }
        public string Password
        {
            get
            {
                return passHash;
            }
            set
            {
                byte[] salt = generateSalt();
                byte[] passw = passwordToHash(value);
                int pos = randomPosition();
                placeHashInSalt(passw, salt, pos);
                position = pos;
                passHash = Convert.ToBase64String(salt);
            }
        }
        public byte[] passwordToHash(string password)
        {
            SHA512Managed sha512 = new SHA512Managed();
            MemoryStream passwordStore = new MemoryStream();
            TextWriter passwordWriter = new StreamWriter(passwordStore, Encoding.UTF32);
            passwordWriter.Write(password);
            byte[] hash = sha512.ComputeHash(passwordStore.ToArray());
            return hash;
        }
        private byte[] generateSalt()
        {
            byte[] salt = new byte[1024 / 8];
            RNGCryptoServiceProvider.Create().GetBytes(salt);
            return salt;
        }
        private int randomPosition()
        {
            byte[] intPick = new byte[4];
            RNGCryptoServiceProvider.Create().GetBytes(intPick);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(intPick);
            }
            int pick = BitConverter.ToInt32(intPick, 0);
            pick %= 128;
            if (pick < 0)
                pick += 128;
            return pick;
        }
        private void placeHashInSalt(byte[] hash, byte[] salt, int position)
        {
            if (position > salt.Length)
            {
                throw new IndexOutOfRangeException();
            }
            else if (position < 0)
            {
                throw new IndexOutOfRangeException();
            }
            else if (position > hash.Length)
            {

                int lastPartLength = position - hash.Length;
                int firstPartLength = hash.Length - lastPartLength;
                Array.Copy(hash, 0, salt, position, firstPartLength);
                Array.Copy(hash, firstPartLength, salt, 0, lastPartLength);
            }
            else
            {
                Array.Copy(hash, 0, salt, 0, hash.Length);
            }
        }
        public string Username
        {
            get
            {
                return username;
            }
        }
    }
    public class JSONBlogUser
    {
        private JSONBlogUserState state;
        private const String INFO_FILE = "userInfo.json";       
        private DirectoryInfo userDirectory;        
        public string Password
        {
            get
            {
                return state.Password;
            }
            set
            {
                state.Password = value;
                writeDataToInfoFile();
            }
        }
        public string User
        {
            get
            {
                return state.Username;
            }
        }
        public bool LoginSuccessful(string password)
        {
            byte[] hash = state.passwordToHash(password);
            byte[] passHash = Convert.FromBase64String(state.Password);
            byte[] storedhash = new byte[hash.Length];
            if (state.Position < hash.Length)
            {
                Array.Copy(passHash, state.Position, storedhash, 0, storedhash.Length);
            }
            else
            {
                int lastPartLength = state.Position - hash.Length;
                int firstPartLength = hash.Length - lastPartLength;
                Array.Copy(passHash, state.Position, storedhash, 0, firstPartLength);
                Array.Copy(passHash, 0, storedhash, firstPartLength, lastPartLength);
            }
            String compare = Convert.ToBase64String(storedhash);
            return compare.CompareTo(hash) == 0;
        }
        
        public JSONBlogUser(DirectoryInfo userDirectory)
        {
            this.userDirectory = userDirectory;
            readDataFromInfoFile();
        }        
        public JSONBlogUser(DirectoryInfo userDirectory,string username,string password)
        {
            this.userDirectory = userDirectory;
            state = new JSONBlogUserState(username);
            Password = password;
        }
        private FileInfo findInfoFile()
        {
            foreach (FileInfo file in userDirectory.EnumerateFiles())
            {
                if (file.Name.CompareTo(INFO_FILE) == 0)
                {
                    return file;
                }
            }
            return null;
        }
        private void readDataFromInfoFile()
        {
            Stream infoStream = new FileStream(InfoFileInfo.FullName, FileMode.Open);
            TextReader reader =  new StreamReader(infoStream,Encoding.UTF32);
            string serialized = reader.ReadToEnd();
            reader.Close();
            state = JsonConvert.DeserializeObject<JSONBlogUserState>(serialized);        
        }
        private void writeDataToInfoFile()
        {
            Stream infoStream = new FileStream(InfoFileInfo.FullName, FileMode.Create);
            writeDataToInfoStream(infoStream);
            infoStream.Close();
        }
        private void writeDataToInfoStream(Stream info)
        {
            TextWriter textWriter = new StreamWriter(info, Encoding.UTF32);
            string serialized = JsonConvert.SerializeObject(state);
            textWriter.Write(serialized);
            textWriter.Close();
        }
        private void createInfoFile()
        {
            String infoPath = Path.Combine(this.userDirectory.FullName, INFO_FILE);
            FileInfo infoFileInfo = new FileInfo(infoPath);
            Stream infoStream = infoFileInfo.Create();
            writeDataToInfoStream(infoStream);
            infoStream.Close();
        }
        public FileInfo InfoFileInfo
        {
            get
            {
                FileInfo infoFileInfo = findInfoFile();
                if (infoFileInfo == null)
                {
                    createInfoFile();
                }
                infoFileInfo = findInfoFile();
                if (infoFileInfo == null)
                {
                    throw new FailedToCreateUserInfoFileException();
                }
                return infoFileInfo;
            }
        }
    }
}
