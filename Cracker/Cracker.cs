using System;
using System.Collections.Generic;
using System.IO;
using Generator;

namespace Cracker {
    public class Cracker {
        static void Main(String[] args) {
            string part = args[0];
            Cracker cracker = new Cracker();
            Console.WriteLine("Cracking...");
            var crackerResult = cracker.Crack(part, args[1]);
            Console.WriteLine("Done.");
            Console.WriteLine($"Password: {crackerResult.password}");
            Console.WriteLine($"Number of tries: {crackerResult.tries}.");
            Console.WriteLine($"Time Elapsed: {crackerResult.timeSpan.ToString()}");
#if DEBUG
            Console.ReadKey();
#endif
        }
        private int tries = 0;
        public class CrackResult {
            public String kind { get; }
            public int tries { get; }
            public String password { get; }
            public TimeSpan timeSpan { get;  }
            public CrackResult(String _kind, int _tries, String _password, TimeSpan _timeSpan) {
                this.kind = _kind;
                this.tries = _tries;
                this.password = _password;
                this.timeSpan = _timeSpan;
            }
        }
        public CrackResult Crack(string _part, String path) {
            var startTime = DateTime.Now;
            tries = 0;
            try {
                int part;
                if (!Int32.TryParse(_part, out part)) {
                    throw new Exception("Part number is invalid");
                }
                // string fileName = $"../../../../Generator/bin/Debug/netcoreapp2.1/part{part}.txt";
                string[] fileContents = File.ReadAllText(path).Trim('[', ']').Split(',');

                string salt = fileContents[1].Trim();
                string hash = fileContents[2].Trim();
                string password = "";

                int minLength = 2;
                int maxLength = 5;
                int maxCount = computeLength(charSet[part - 1].Length, minLength, maxLength);

                List<int> currentIndicies = new List<int>();
                currentIndicies.Add(0);
                string computedHash = "";
                bool found = false;
                while (!found) {
                    computedHash = Generator.Generator.hash(password + salt);
                    if (computedHash == hash) {
                        found = true;
                    } else {
                        if (tries > maxCount) {
                            throw new Exception("Couldn't crack the password with the characters provided.");
                        }
                        string nextPassword = getNewPassword(password, part, currentIndicies);
                        if (nextPassword == password) {
                            // nothing has changed, add another digit
                            int length = currentIndicies.Count;
                            currentIndicies = new List<int>();
                            for (int i = 0; i <= length; i++) {
                                currentIndicies.Add(0);
                            }
                            nextPassword = "";
                        }
                        password = nextPassword;
                        tries++;
                    }
                }
                var endTime = DateTime.Now;
                var timeDifference = endTime - startTime;
                return new CrackResult("Success", tries, password, timeDifference);
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
            return new CrackResult("Failure", 0, null, new TimeSpan());
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
            if (currentPassword == "") {
                return computePasswordFromIndicies(part, currentIndicies);
            } else {
                return recurseIncrement(currentPassword, part, currentIndicies, currentIndicies.Count - 1);
            }
        }
        private string recurseIncrement(string currentPassword, int part, List<int> currentIndicies, int index) {
            int lastCharIndex = charSet[part - 1].Length - 1;
            char lastChar = charSet[part - 1][lastCharIndex];
            // If we don't need to carry
            if (currentPassword[index] != lastChar) {
                currentIndicies[index]++;
                return computePasswordFromIndicies(part, currentIndicies);
            } else {
                if (index == 0) {
                    return currentPassword;
                } else {
                    currentIndicies[index] = 0;
                    index--;
                    return recurseIncrement(currentPassword, part, currentIndicies, index);
                }
            }
        }
        private string computePasswordFromIndicies(int part, List<int> currentIndicies) {
            string result = "";
            foreach (int i in currentIndicies) {
                result += charSet[part - 1][i];
            }
            return result;
        }
        private static int computeLength(int keysLength, int min, int max) {
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