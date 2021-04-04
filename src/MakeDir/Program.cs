using System;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace MakeDir
{
    class Program
    {
        static int Main(string[] args)
        {
            using var runSpace = RunspaceFactory.CreateRunspace();
            try
            {
                var powershell = PowerShell.Create();
                runSpace.Open();
                powershell.Runspace = runSpace;

                powershell.AddScript("Set-ExecutionPolicy Unrestricted -Scope CurrentUser ");
                powershell.Invoke();

                powershell.AddCommand(Path.Combine(Directory.GetCurrentDirectory(), "MakeDir.ps1"));
                powershell.AddParameter("dirName", "_MyPowershellDir_");

                var results = powershell.Invoke();
                if (powershell.Streams.Error.Count > 0)
                {
                    Console.WriteLine("Failed");
                    foreach (var error in powershell.Streams.Error)
                    {
                        Console.WriteLine(error.ErrorDetails.Message);
                    }

                    return -1;
                }

                powershell.Commands.Clear();
                powershell.AddScript("Set-ExecutionPolicy Restricted -Scope CurrentUser");
                powershell.Invoke();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRORS OCCURRED! " + ex.Message);
                return -2;
            }


            Console.WriteLine("Done");
            return 200;
        }
    }
}