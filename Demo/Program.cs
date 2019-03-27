using System;

namespace Demo {
    class Program {
        static void Main(string[] args) {
            int part;
            string username;
            string password;
#if DEBUG
            Console.WriteLine($"Part: {args[0]} username: {args[1]} password: {args[2]}");
#endif
            part = Int32.Parse(args[0]);
            username = args[1];
            password = args[2];
            Generator.Generator.generateAndWrite(part.ToString(), username, password);
            Cracker.Cracker cracker = new Cracker.Cracker();
            cracker.Crack(args[0]);
        }
    }
}
