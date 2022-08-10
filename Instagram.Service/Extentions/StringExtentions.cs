using System.Security.Cryptography;
using System.Text;

namespace Instagram.Service.Extentions
{
    public static class StringExtentions
    {

        public static string GetHash(this string password)
        {
            using var md5Hash = MD5.Create();

            var sourceBytes = Encoding.UTF32.GetBytes(password);

            var hashBytes = md5Hash.ComputeHash(sourceBytes);

            return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
        }
    }
}
