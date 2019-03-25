using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace Information_Security_Password_Cracking_Tool {
    static class Generator {
        private static Random randomSalt = new Random();
        private static string part1Regex = "^([a-z]){2,5}$";
        private static string part2Regex = "^([a-z]|[A-Z]){2,5}$";
        private static string part3Regex = "^([a-z]|[A-Z]|[0-9]){2,5}$";
        private static string part4Regex = "^.{2,5}$";
        public static void generateAndWrite(string username, string password, string path) {
            var salt = getSalt(32);
            var hash = getHash(password + salt);
            var result = $"[{username}, {salt}, {hash}]";
            File.WriteAllText(path, result.ToString());
        }
        private static string getSalt(int length) {
            var salt = "";
            for (int i = 0; i < length; i++) {
                char c = Convert.ToChar(randomSalt.Next() % 2 == 0 ? randomSalt.Next(65, 90) : randomSalt.Next(97, 122));
                salt += c;
            }
            return salt;
        }
        private static string getHash(string text) {
            using (var hasher = SHA256.Create())
                return BitConverter.ToString(hasher.ComputeHash(Encoding.ASCII.GetBytes(text))).Replace("-", "").ToLower();
        }
        static void Main(String[] args) {
            int part;
            string username;
            string password;

#if DEBUG
            Console.WriteLine($"Part: {args[0]} username: {args[1]} password: {args[2]}");
#endif
            try {
                Int32.TryParse(args[0], out part);
                username = args[1];
                password = args[2];
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
                Generator.generateAndWrite(username, password, $"./output{part}.txt");
            } catch (IndexOutOfRangeException e) {
                Console.WriteLine("args should be <part number> <username> <password>");
            } catch (AhlFormatException e) {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}
