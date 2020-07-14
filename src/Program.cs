using System;
using System.Configuration;

namespace DotNetConsoleApp {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            string stage = Environment.GetEnvironmentVariable("DNCA_STAGE");
            Console.WriteLine($"stage: {stage}");
        }
    }
}