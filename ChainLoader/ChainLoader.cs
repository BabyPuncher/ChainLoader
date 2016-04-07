﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var iniFile = new IniFile("ChainLoader.ini");
                var target = iniFile.Read("Target", "ChainLoader");

                if (String.IsNullOrEmpty(target))
                {
                    throw new MissingFieldException("Key 'target' not found");
                }
                Console.WriteLine("Executing \"" + target + "\"");
                System.Diagnostics.Process.Start(target);
            }
            catch (Exception)
            {
                Console.WriteLine("Bad or nonexistent config.ini\n"
                                    +"config.ini needs \"Target\" key pointing to the .exe you want to load.\n"
                                    +"Press any key to continue...");
            }
        }
    }
}
