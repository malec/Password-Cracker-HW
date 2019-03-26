using System;
using System.Collections.Generic;
using System.Text;

namespace Information_Security_Password_Cracking_Tool {
    static class Cracker {
        static void Main(String[] args) {
            try {
                string fileName = args[0];
                int part = Int32.Parse(args[1]);
                Cracker.Crack(fileName, part);
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
        public static void Crack(string FileName, int part) {

        }
    }
}