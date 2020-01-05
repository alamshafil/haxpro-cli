using System;
using System.IO;

namespace hax
{
    class Program
    {

        static void Main(string[] args)
        {
            bool CheckRuntime()
            {
                var dir = new DirectoryInfo("c:\\HaxPro\\bin");

                if (dir.Exists)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            void GetRuntime()
            {
                Console.WriteLine("Installing haxor packages...");

                Directory.CreateDirectory("C:\\HaxPro\\bin\\");
                using (StreamWriter file = new StreamWriter("C:\\HaxPro\\bin\\dat.hax", false))
                {
                    file.WriteLine("use: haxpro.utills.data");
                    file.WriteLine("dispatch.syscall: data.serialize: data.stream.utf16: <this.syscall={package.Downloaded}>");
                }

                Console.WriteLine("Done!");
            }

            if (args.Length == 0)
            {
                Console.WriteLine("args is null");
            }
            else
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if(args[i] == "version")
                    {
                        /* if(args.Length >= 2 && args[i+1] == "hax")
                        {
                            Console.WriteLine("stop");
                        } */

                        Console.WriteLine("v0.1-beta1");
                    }

                    if(args[i] == "init")
                    {
                        if(CheckRuntime() == true)
                        {
                            if(args.Length >= 2)
                            {
                                var name = args[i + 1];
                                var current = Directory.GetCurrentDirectory();
                                var dir = Path.Join(current, "/" + name + "/");

                                Directory.CreateDirectory(dir);
                                Directory.CreateDirectory(Path.Join(dir, "/builds/"));
                                Directory.CreateDirectory(Path.Join(dir, "/code/"));
                                Directory.CreateDirectory(Path.Join(dir, "/files/"));

                                using (StreamWriter file = new StreamWriter(dir + "/app.json", false))
                                {
                                    file.WriteLine(name);
                                }

                                using (StreamWriter file = new StreamWriter(dir + "/app.hax", false))
                                {
                                    file.WriteLine("use: haxpro.utills.data");
                                    file.WriteLine("dispatch.syscall: data.serialize: data.stream.utf16: <this.syscall={app.Load(" + name + ")}>");
                                }

                                using (StreamWriter file = new StreamWriter(dir + "/code/main.hax", false))
                                {
                                    file.WriteLine("use: haxpro.main");
                                    file.WriteLine("use: haxpro.ui");
                                    file.WriteLine("use: haxpro.cli.package.loader");
                                    file.WriteLine("");
                                    file.WriteLine("Main<>");
                                    file.WriteLine("");
                                }

                            } else
                            {
                                Console.WriteLine("Enter a name");
                            }

                        } else if(CheckRuntime() == false)
                        {
                            Console.WriteLine("Runtime packages are missing.");
                            GetRuntime();
                        }
                    }
                }
            }
        }   
    }
}
