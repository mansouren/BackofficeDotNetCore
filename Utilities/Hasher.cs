
namespace Utilities
{
    public static class Hasher
    {
        public static string ToHash(this string s)
        {
            try
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(s);
                var hashbytes = System.Security.Cryptography.SHA1.Create().ComputeHash(bytes);
                var result = new System.Text.StringBuilder();
                foreach (var item in hashbytes)
                {
                    result.Append(item.ToString("x2"));
                }
                return result.ToString();
                //return HashHandler.ComputeHash(s, Enumerations.HashMode.SHA1, new byte[8]);
            }
            catch
            {
                return null;
            }
        }
    }
}
