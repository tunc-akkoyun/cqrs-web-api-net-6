using System.Security.Cryptography;
using System.Text;

namespace Store.Application.Utilities;

public static class PasswordGenerator
{
    /// <summary>
    /// Creates a SHA512 password
    /// </summary>
    public static string Create(string strData)
    {
        if (string.IsNullOrEmpty(strData))
            return null;

        var message = Encoding.UTF8.GetBytes(strData);
        using (var alg = SHA512.Create())
        {
            string hex = "";

            var hashValue = alg.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += string.Format("{0:x2}", x);
            }
            return hex;
        }
    }
}