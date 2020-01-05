using System;
using System.IO;
using System.Net;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

                    if(args[i] == "install")
                    {
                        if (CheckRuntime() == true)
                        {
                            if (args.Length >= 2)
                            {
                                var file = new FileInfo("app.hax");
                                if(file.Exists)
                                {
                                    if (args[i + 1].Contains("file:///") || args[i + 1].Contains("file://") || args[i + 1].Contains("https://") || args[i + 1].Contains("http://"))
                                    {
                                        Console.WriteLine("The argument {name} doesn't take in a URL object.");
                                    }
                                    else
                                    {
                                        var name = args[i + 1];

                                        WebClient client1 = new WebClient();
                                        Stream stream1 = client1.OpenRead("https://alamshafil.github.io/HaxPro/store/data/package.json");
                                        StreamReader reader1 = new StreamReader(stream1);
                                        string content1 = reader1.ReadToEnd();
                                        dynamic data1 = JObject.Parse(content1);

                                        WebClient client2 = new WebClient();
                                        Stream stream2 = client2.OpenRead("https://alamshafil.github.io/HaxPro/store/data/package.hax");
                                        StreamReader reader2 = new StreamReader(stream2);
                                        string content2 = reader2.ReadToEnd();
                                        Array data2 = content2.Split("::");

                                        if (Array.IndexOf(data2, name) >= 0)
                                        {
                                            for (i = 0; i < data1["apps"].Count; i++)
                                            {
                                                string packName = data1["apps"][i].name;
                                                if(packName == name)
                                                {
                                                    string url = data1["apps"][i].path;
                                                    string ext = data1["apps"][i].name + ".js";


                                                    WebClient client3 = new WebClient();
                                                    Stream stream3 = client3.OpenRead(url);
                                                    StreamReader reader3 = new StreamReader(stream3);
                                                    string dat = reader3.ReadToEnd();

                                                    var current = Directory.GetCurrentDirectory();

                                                    using (StreamWriter e = new StreamWriter(current + "/files/" + ext, false))
                                                    {
                                                        e.Write(dat);
                                                    }

                                                    break;
                                                }
                                            }
                                                
                                        } else
                                        {
                                            Console.WriteLine("Package " + name + " was not found.");
                                        }
                                    }

                                } else
                                {
                                    Console.WriteLine("This is not a vaild project. If you used 'hax init (name of project)' then use 'cd (name of project)' to enter your project.");
                                }     
                            }
                            else
                            {
                                Console.WriteLine("Enter a name");
                            }

                        }
                        else if (CheckRuntime() == false)
                        {
                            Console.WriteLine("Runtime packages are missing.");
                            GetRuntime();
                        }
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
