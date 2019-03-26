using System;
using System.Collections.Generic;
using System.IO;

namespace Cracker {
    public class Cracker {
        Cracker() {
            part1Keys.Sort();
            part2Keys.Sort();
            part3Keys.Sort();
            part4Keys.Sort();
        }
        static void Main(String[] args) {
            string part = args[0];
            Cracker cracker = new Cracker();
            cracker.Crack(part);
        }
        private int tries = 0;
        public void Crack(string _part) {
            tries = 0;
            try {
                int part;
                if (!Int32.TryParse(_part, out part)) {
                    throw new Exception("Part number is invalid");
                }
                string fileName = $"part{part}";
                string[] fileContents = File.ReadAllText(fileName).Trim('[', ']').Split(',');
                string salt = fileContents[1];
                string hash = fileContents[2];
                string password = "";
                while (Generator.Generator.hash(password + salt) != hash) {
                    password = getNewPassword(part);
                }
            } catch (IndexOutOfRangeException e) {
                Console.WriteLine("Provide a file crack via command line.");
                Console.WriteLine("Format is <filename>");
            } catch (FormatException) {
                throw new FormatException("Part number is invalid");
            } catch (ArgumentNullException e) {
                throw new FormatException("Part number is invalid");
            } catch (OverflowException e) {
                throw new FormatException("Part number is invalid");
            }
        }
        private static string upperCaseString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static string lowerCaseString = upperCaseString.ToLower();
        private static string numbers = "0123456789";
        private static string specials = "$#%&*()";
        private static List<char> part1Keys= new List<char>(lowerCaseString.ToCharArray());
        private static List<char> part2Keys = new List<char>((upperCaseString + lowerCaseString).ToCharArray());
        private static List<char> part3Keys = new List<char>(numbers + upperCaseString + lowerCaseString);
        private static List<char> part4Keys = new List<char>(specials + numbers + upperCaseString + lowerCaseString);

        public string crackLowerCasePassword(string keys) {
        }
        private static string getNewPassword(string password, int part, int stop) {
            if (password[password.Length] == ) {

            }
            switch (part) {
                case 1: {
                        break;
                    }
                case 2: {
                        break;
                    }
                case 3: {
                        break;
                    }
                case 4: {
                        break;
                    }
                default:
                    return null;
            }
        }
    }
}