using System;
using System.Collections.Generic;
using System.IO;
using Generator;

namespace Cracker {
    public class Cracker {
        public Cracker() {
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
                string fileName = $"../../part{part}.txt";
                string[] fileContents = File.ReadAllText(fileName).Trim('[', ']').Split(',');
                string salt = fileContents[1];
                string hash = fileContents[2];
                string password = "";
                int maxCount = 0;
                int minLength = 2;
                int maxLength = 5;
                List<int> currentIndicies = new List<int>();
                maxCount = computeLength(charSet[part].Length, minLength, maxLength);
                foreach (int key in charSet[part]) {
                    currentIndicies.Add(0);
                }
                while ((Generator.Generator.hash(password + salt) != hash) || (tries <= maxCount)) {
                    password = getNewPassword(password, part, currentIndicies);
                    tries++;
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
        private static char[] part1Keys = lowerCaseString.ToCharArray();
        private static char[] part2Keys = (upperCaseString + lowerCaseString).ToCharArray();
        private static char[] part3Keys = (numbers + upperCaseString + lowerCaseString).ToCharArray();
        private static char[] part4Keys = (specials + numbers + upperCaseString + lowerCaseString).ToCharArray();
        private static char[][] charSet = { part1Keys, part2Keys, part3Keys, part4Keys };
        private string getNewPassword(string currentPassword, int part, List<int> currentIndicies) {
            return recurseIncrement(currentPassword, part, currentIndicies, charSet[part - 1].Length - 1);
        }
        private string recurseIncrement(string currentPassword, int part, List<int> currentIndicies, int index) {
            if (currentPassword[currentIndicies[index]] != charSet[part - 1][charSet[part - 1].Length - 1]) {
                currentIndicies[index]++;
                string result = "";
                foreach (int i in currentIndicies) {
                    result += charSet[part - 1][index];
                }
                return result;
            } else {
                if (index == 0) {
                    throw new OverflowException("Password " + currentPassword + " is overflowing");
                } else {
                    currentIndicies[index] = 0;
                    index--;
                    return recurseIncrement(currentPassword, part, currentIndicies, index);
                }
            }
        }
        private static int computeLength(int keysLength, int max, int min) {
            int maxCount = 0;
            for (int i = min; i <= max; i++) {
                maxCount += (int)Math.Pow(keysLength, i);
            }
            return maxCount;
        }
        static int Fact(int n) {
            if (n <= 1)
                return 1;
            return n * Fact(n - 1);

        }
    }
}