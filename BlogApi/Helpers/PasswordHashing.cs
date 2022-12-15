using System.Security.Cryptography;

namespace BlogApi.Helpers
{
    public class PasswordHashing
    {
        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer;
            if (password == null) throw new ArgumentNullException("password");
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 16, 10000))
            {
                salt = bytes.Salt;
                buffer = bytes.GetBytes(32);
            }
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(buffer, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (hashedPassword == null) return false;
            if (password == null) throw new ArgumentNullException("password");
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            if (hashBytes.Length != 36) return false;
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i]) throw new UnauthorizedAccessException();
            }
            return true;
        }
    }
}
