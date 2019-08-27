using System;
using System.Security.Cryptography;
using System.Text;

namespace ApiRestUsuarios.Util
{
    public class Criptografia
    {
        public string RetornarMD5(string senha){
            using ( MD5 md5Hash= MD5.Create()){
                return RetornarHash(md5Hash,senha);
            }
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
        public bool ComparaMD5(string senhaParaCriptografar, string Senha_MD5)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                var senha = RetornarMD5(senhaParaCriptografar);
                if (VerificarHash(md5Hash, Senha_MD5, senha))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private bool VerificarHash(MD5 md5Hash, string Senha_MD5, string senha)
        {
            StringComparer compara = StringComparer.OrdinalIgnoreCase;

            if (0 == compara.Compare(Senha_MD5, senha))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}