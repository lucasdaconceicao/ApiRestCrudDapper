using System;
using System.Security.Cryptography;
using System.Text;

namespace ApiRestUsuarios.Util
{
    public class Criptografia
    {
        public string RetornarMD5(string senha){
            MD5 md5Hash= MD5.Create();
            return RetornarHash(md5Hash,senha);
        }

        private string RetornarHash(MD5 md5Hash, string senha)
        {
            byte[] data=md5Hash.ComputeHash(Encoding.UTF8.GetBytes(senha));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
             return sBuilder.ToString();
        }
    }
}