using System;
using System.IO;

namespace BrainFuckInterpeter
{
    class Program
    {
        static enum EErrorCode
        {
            NONE = 0,
            NO_BRAINFUCK_SYMBOL
        }

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                //Read the source from the brainf*ck file.
                byte[] brainFuckContent = File.ReadAllBytes(@"" + args[0]);
                byte[] memoryByte = new byte[30000];
                int bytePointer = 0;

                //Loop through the brainf*ck code and filter the symbols that it comes across.
                for (int i = 0; i < brainFuckContent.Length; i++)
                {
                    switch (brainFuckContent[i])
                    {
                        case 43:    //+
                            memoryByte[bytePointer] += 1;
                            continue;
                        case 45:    //-
                            memoryByte[bytePointer] -= 1;
                            continue;
                        case 60:    //<
                            bytePointer -= 1;
                            continue;
                        case 62:    //>
                            bytePointer += 1;
                            continue;
                        case 91:    //[
                            if (memoryByte[bytePointer] == 0)
                            {
                                int loops = 1;
                                while (loops > 0)
                                {
                                    i += 1;
                                    if (brainFuckContent[i] == 91)      loops += 1;
                                    else if (brainFuckContent[i] == 93) loops -= 1;
                                }
                            }
                            continue;
                        case 93:    //]
                            if (memoryByte[bytePointer] != 0)
                            {
                                int loops = 1;
                                while (loops > 0)
                                {
                                    i -= 1;
                                    if (brainFuckContent[i] == 91)      loops -= 1;
                                    else if (brainFuckContent[i] == 93) loops += 1;
                                }
                            }
                            continue;
                        case 46:    //.
                            Console.Write((char)memoryByte[bytePointer]);
                            continue;
                        case 44:    //,
                            memoryByte[bytePointer] = (byte)Console.ReadKey().KeyChar;
                            continue;
                        case default:
                            ExitProgram(NO_BRAINFUCK_SYMBOL);
                    }
                }
                ExitProgram();
            }
            ExitProgram();
        }

        private static void ExitProgram(EErrorCode errorCode = 0)
        {
            if (System.Windows.Forms.Application.MessageLoop && errorCode == EErrorCode.NONE)
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                switch (errorCode)
                {
                    case EErrorCode.NO_BRAINFUCK_SYMBOL:
                        Console.WriteLine("\n Error: this symbol is not part of the brainf*ck.");
                        break;
                    default:
                        Console.WriteLine("\n Error: Unknown.");
                        break;
                }

                Console.ReadKey();
                System.Environment.Exit(1);
            }
        }
    }
}