using System;

namespace Demo {
    class Program {
        static void Main(string[] args) {
            int part;
            string username;
            string password;
            part = Int32.Parse(args[0]);
            username = args[1];
            password = args[2];
            Generator.Generator.generateAndWrite(part.ToString(), username, password);
            Cracker.Cracker cracker = new Cracker.Cracker();
            Console.WriteLine("Cracking...");
            var crackerResult = cracker.Crack(args[0], args[3]);
            Console.WriteLine("Done.");
            Console.WriteLine($"Password: {crackerResult.password}");
            Console.WriteLine($"Number of tries: {crackerResult.tries}.");
            Console.WriteLine($"Time Elapsed: {crackerResult.timeSpan.ToString()}");
#if DEBUG
            Console.ReadKey();
#endif
        }
    }
}
