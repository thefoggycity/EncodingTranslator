using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EncodingTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            String InputPath = "", OutputPath = "";
            int encode = 0, OutEnc = 0;

            if (args.GetLength(0) == 0)
            {
                prninst();
                Console.WriteLine("\nDrag file to this program to interact, or add \"[codepage index]_\" before file name to auto convert.");
                Console.ReadKey();
                return;
            }
            else if (args.GetLength(0) == 1)         //Drag-and-drop case
            {
                if (File.Exists(args[0]))       //Is a path
                {
                    InputPath = Path.GetFullPath(args[0]);
                    String fname;                    
                    fname = Path.GetFileNameWithoutExtension(InputPath);      //get name for auto mode

                    if (fname.IndexOf("_") == -1 || fname.IndexOf("_") == 0)  //Manual mode
                    {
                        String enter;
                        Console.WriteLine("Code page index of source file (default UTF):");
                        bool redo = true;
                        while (redo)
                        {
                            enter = Console.ReadLine();
                            if (String.IsNullOrWhiteSpace(enter))
                                break;
                            try
                            {
                                encode = Convert.ToInt32(enter);
                                redo = false;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invaild input. Please retry:");
                                redo = true;
                            }
                        }
                        Console.WriteLine("Code page index of destination file (default UTF):");
                        redo = true;
                        while (redo)
                        {
                            enter = Console.ReadLine();
                            if (String.IsNullOrWhiteSpace(enter))
                                break;
                            try
                            {
                                OutEnc = Convert.ToInt32(enter);
                                redo = false;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invaild input. Please retry:");
                                redo = true;
                            }
                        }
                        String outfname = Path.GetFileNameWithoutExtension(InputPath) + "_conv" + Path.GetExtension(InputPath);
                        Console.WriteLine("Output file destination: ({0})",outfname);
                        Console.Write(Path.GetDirectoryName(InputPath) + "\\");
                        enter = Console.ReadLine();
                        if (!String.IsNullOrWhiteSpace(enter))
                            outfname = enter;
                        OutputPath = Path.GetDirectoryName(InputPath) + "\\" + outfname;
                    }
                    else                                                    //Auto mode
                    {
                        bool manmode = false;
                        try
                        {
                            encode = Convert.ToInt32(fname.Substring(0, fname.IndexOf("_")));
                            OutputPath = Path.GetDirectoryName(InputPath) + "\\" + Path.GetFileName(InputPath).Substring(fname.IndexOf("_") + 1);
                            manmode = false;
                        }
                        catch (FormatException)                             //Not auto mode
                        {
                            manmode = true;
                            goto MANMODE;
                        }
                        catch (OverflowException)
                        {
                            manmode = true;
                            goto MANMODE;
                        }
                    MANMODE:
                        if (manmode)
                        {
                            String enter;
                            Console.WriteLine("Code page index of source file (default UTF):");
                            bool redo = true;
                            while (redo)
                            {
                                enter = Console.ReadLine();
                                if (String.IsNullOrWhiteSpace(enter))
                                    break;
                                try
                                {
                                    encode = Convert.ToInt32(enter);
                                    redo = false;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Invaild input. Please retry:");
                                    redo = true;
                                }
                            }
                            Console.WriteLine("Code page index of destination file (default UTF):");
                            redo = true;
                            while (redo)
                            {
                                enter = Console.ReadLine();
                                if (String.IsNullOrWhiteSpace(enter))
                                    break;
                                try
                                {
                                    OutEnc = Convert.ToInt32(enter);
                                    redo = false;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Invaild input. Please retry:");
                                    redo = true;
                                }
                            }
                            String outfname = Path.GetFileNameWithoutExtension(InputPath) + "_conv" + Path.GetExtension(InputPath);
                            Console.WriteLine("Output file destination: ({0})", outfname);
                            Console.Write(Path.GetDirectoryName(InputPath) + "\\");
                            enter = Console.ReadLine();
                            if (!String.IsNullOrWhiteSpace(enter))
                                outfname = enter;
                            OutputPath = Path.GetDirectoryName(InputPath) + "\\" + outfname;
                        }
                    }
                }
                else
                {
                    prninst();                  //Not a path, then wrongly entered params.
                    Console.ReadKey();
                    return;
                }                
            }
            else if (args.GetLength(0) == 3)
            {
                try
                {
                    encode = Convert.ToInt32(args[0]);
                }
                catch (FormatException)
                {
                    prninst();
                    return;
                }
                InputPath = args[1];
                OutputPath = args[2];
            }
            else if (args.GetLength(0) == 4)
            {
                try
                {
                    encode = Convert.ToInt32(args[0]);
                    OutEnc = Convert.ToInt32(args[3]);
                }
                catch (FormatException)
                {
                    prninst();
                    return;
                }
                InputPath = args[1];
                OutputPath = args[2];

            }
            else
            {
                prninst();
                return;
            }

            try 
            {
                StreamReader ipf;
                if (encode == 0)
                {
                    ipf = new StreamReader(InputPath);
                }
                else
                {
                    ipf = new StreamReader(InputPath, Encoding.GetEncoding(encode));
                }
                StreamWriter opf;
                if (OutEnc == 0)
                {
                    opf = new StreamWriter(OutputPath);
                }
                else
                {
                    Stream stm = new FileStream(OutputPath,FileMode.Create);
                    opf = new StreamWriter(stm, Encoding.GetEncoding(OutEnc));
                }
                while (!ipf.EndOfStream)
                {
                        opf.WriteLine(ipf.ReadLine());
                }
                ipf.Dispose();
                opf.Dispose();
                Console.WriteLine("File written: {0}", OutputPath );
            }
            catch
            {
                Console.WriteLine("Failed to open specified file.");
            }
            //Console.ReadKey();
            return;
        }

        private static void prninst()
        {
            Console.WriteLine("Encoding Translator by tfc");
            Console.WriteLine("Convert text file of specified codepage.");
            Console.WriteLine("\nUsage:\nEncodingTranslator.exe <Codepage Index> <Input File> <Output File> [Output Codepage Index]");
            Console.WriteLine("\nSome codepage indexes:");
            Console.WriteLine("932 Shift-JIS\t936 GB2312\t949 EUC-KR\t950 Big5");
        }

    }
}
