using System;
using System.IO;
using System.Linq;

namespace ChainLoader
{
    class ChainLoader
    {
        static void Main(string[] args)
        {
            if (args.Count() > 0)
            {
                Console.Write("Arguments passed: ");

                foreach (var arg in args)
                {
                    Console.Write(arg + " ");
                }

                Console.WriteLine("\n(ignoring)");
            }

            try
            {
                var target = GetTarget();
                Console.WriteLine("Executing \"" + target + "\"");
                System.Diagnostics.Process.Start(target);
            }
            catch (MissingFieldException)
            {
                Console.WriteLine("Bad or nonexistent config.ini\n"
                                    + "config.ini needs \"Target\" key pointing to the .exe you want to load.\n"
                                    + "Press any key to continue...");
                Console.ReadKey();
            }
        }

        private static string GetTarget()
        {
            var assemblyDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            var iniFile = (File.Exists("ChainLoader.ini"))
                ? new IniFile("ChainLoader.ini") : new IniFile(assemblyDirectory + "\\ChainLoader.ini");

            var target = iniFile.Read("Target", "ChainLoader");

            if (String.IsNullOrEmpty(target))
            {
                throw new MissingFieldException("Key 'Target' not found");
            }

            return target;
        }
    }
}
