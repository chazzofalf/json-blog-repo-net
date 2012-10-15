
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
namespace JSONBlog
{
    /// <summary>
    /// Represents a registered user of this blog system.
    /// 
    /// </summary>
    /// 
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
        public bool LoginSuccessful(string password)
        {
            byte[] hash = passwordToHash(password);
            byte[] passHash = Convert.FromBase64String(this.passHash);
            byte[] storedhash = new byte[hash.Length];
            if (position < hash.Length)
            {
                Array.Copy(passHash, position, storedhash, 0, storedhash.Length);
            }
            else
            {

            }
        }
        private byte[] passwordToHash(string password)
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
            byte[] salt = new byte[1024/8];
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
                int firstPartLength = position - hash.Length;
                int lastPartLength = hash.Length - firstPartLength;
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
        public JSONBlogUser(DirectoryInfo userDirectory)
        {
            this.userDirectory = userDirectory;
            
        }        
        public JSONBlogUser(DirectoryInfo userDirectory,string username,string password)
        {

        }
    }
}
