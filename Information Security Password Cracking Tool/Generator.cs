using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace Generator {
    public static class Generator {
        private static Random randomSalt = new Random();
        private static string part1Regex = "^([a-z]){2,5}$";
        private static string part2Regex = "^([a-z]|[A-Z]){2,5}$";
        private static string part3Regex = "^([a-z]|[A-Z]|[0-9]){2,5}$";
        private static string part4Regex = "^.{2,5}$";
        private static string getSalt(int length) {
            var salt = "";
            for (int i = 0; i < length; i++) {
                char c = Convert.ToChar(randomSalt.Next() % 2 == 0 ? randomSalt.Next(65, 90) : randomSalt.Next(97, 122));
                salt += c;
            }
            return salt;
        }
        public static string hash(string text) {
            using (var hasher = SHA256.Create())
                return BitConverter.ToString(hasher.ComputeHash(Encoding.ASCII.GetBytes(text))).Replace("-", "").ToLower();
        }
        public static void generateAndWrite(string _part, string username, string password) {

#if DEBUG
            Console.WriteLine($"Part: {_part} username: {username} password: {password}");
#endif
            try {
                int part = Int32.Parse(_part);
                switch (part) {
                    case 1:
                        if (!Regex.IsMatch(password, part1Regex)) {
                            throw new AhlFormatException("only lowercase allowed.");
                        }
                        break;
                    case 2:
                        if (!Regex.IsMatch(password, part2Regex)) {
                            throw new AhlFormatException("only uppercase and lowercase are allowed.");
                        }
                        break;
                    case 3:
                        if (!Regex.IsMatch(password, part3Regex)) {
                            throw new AhlFormatException("only uppercase, lowercase, and numbers are allowed.");
                        }
                        break;
                    case 4:
                        if (!Regex.IsMatch(password, part4Regex)) {
                            throw new AhlFormatException("only uppercase, lowercase, numbers, and special are allowed.");
                        }
                        break;
                    default:
                        throw new FormatException("Part number is invalid");
                }
                var salt = getSalt(32);
                var hash = Generator.hash(password + salt);
                var result = $"[{username}, {salt}, {hash}]";
                File.WriteAllText($"./part{part}.txt", result.ToString());
            } catch (IndexOutOfRangeException e) {
                Console.WriteLine("args should be <part number> <username> <password>");
            } catch (AhlFormatException e) {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            } catch (FormatException e) {
                Console.WriteLine("Part number not in correct format");
            }
        }
        static void Main(String[] args) {
            string part = args[0];
            string username = args[1]; 
            string password = args[2];
            generateAndWrite(part, username, password);
        }
    }
}
