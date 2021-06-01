using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Security.Cryptography;

namespace SQLITEFTS
{
    class TMD5
    {

        public static string ComputeFilesMD5(string path)
        {
            using (FileStream fs = File.OpenRead(path))
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] filebytes = new byte[fs.Length];
                fs.Read(filebytes, 0, (int)fs.Length);
                byte[] Sum = md5.ComputeHash(filebytes);
                string result = BitConverter.ToString(Sum).Replace("-", String.Empty);
                return result;
            }
        }

        public static string ComputeStringMD5Hash(string instr)
        {
            string strHash = string.Empty;

            foreach (byte b in new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(instr)))
            {
                strHash += b.ToString("X2");
            }
            return strHash;
        }
    }
}
