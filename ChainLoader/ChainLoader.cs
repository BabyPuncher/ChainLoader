using System;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace BabyPuncher.ChainLoader
{
    class ChainLoader
    {
        private static readonly string iniFileName = "ChainLoader.ini";
        private static readonly string iniSectionName = "ChainLoader";
        private static readonly string iniTargetKeyName = "Target";

        public static void Main(string[] args)
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

            string target = String.Empty;

            try
            {
                target = getTarget();
                Console.WriteLine("Executing \"" + target + "\"");
                System.Diagnostics.Process.Start(target);
            }
            catch (InvalidDataException)
            {
                Console.WriteLine("Bad or nonexistent config.ini\n"
                                    + "config.ini needs \""
                                    + iniTargetKeyName 
                                    + "\" key pointing to the .exe you want to load.\n"
                                    + "Press any key to continue...");
                Console.ReadKey();
            }
            catch (Win32Exception e)
            {
                Console.WriteLine(e.Message
                                    + ": "
                                    + target
                                    + "\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        private static string getTarget()
        {
            var assemblyDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            var iniFile = (File.Exists(iniFileName))
                ? new IniFile(iniFileName) : new IniFile(assemblyDirectory + "\\" + iniFileName);

            var target = iniFile.Read(iniTargetKeyName, iniSectionName);

            if (String.IsNullOrEmpty(target))
            {
                throw new InvalidDataException("Key " + iniTargetKeyName + " not found");
            }

            return target;
        }
    }
}
