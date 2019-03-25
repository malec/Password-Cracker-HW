using System;

namespace Information_Security_Password_Cracking_Tool {
    class Program {
        static void Main(string[] args) {
            // Part 1
            Generator.generateAndWrite("alec", "password", "./output.txt");
        }
    }
}
