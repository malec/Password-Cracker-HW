using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace Information_Security_Password_Cracking_Tool {
    static class Generator {
        private static Random randomSalt = new Random();
        public static void generateAndWrite(string username, string password, string path) {
            var salt = getSalt(32);
            var hash = getHash(password + salt);
            var result = $"[{username}, {salt}, {hash}]";
            File.WriteAllText(path, result.ToString());
        }
        private static string getSalt(int length) {
            var salt = "";
            for(int i = 0; i < length; i++) {
                char c = Convert.ToChar(randomSalt.Next() % 2 == 0 ? randomSalt.Next(65, 90) : randomSalt.Next(97, 122));
                salt += c;
            }
            return salt;
        }
        private static string getHash(string text) {
            using (var hasher = SHA256.Create())
                return BitConverter.ToString(hasher.ComputeHash(Encoding.ASCII.GetBytes(text))).Replace("-", "").ToLower();
        }
    }
}
