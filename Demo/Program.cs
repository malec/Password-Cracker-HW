using System;
using System.Text.RegularExpressions;

namespace Demo {
    class Program {
        static void Main(string[] args) {
            int part;
            string username;
            string password;
#if DEBUG
            Console.WriteLine($"Part: {args[0]} username: {args[1]} password: {args[2]}");
#endif
            try {
                part = Int32.Parse(args[0]);
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
            }
    }
}
