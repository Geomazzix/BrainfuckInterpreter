using System;
using System.IO;

namespace BrainFuckInterpeter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                //All the brainFuck functions.
                byte[] brainFuckContent = File.ReadAllBytes(@"" + args[0]);
                byte[] memoryByte = new byte[30000];
                int bytePointer = 0;

                //Loop through the chars of the brainFuck document and check if it is one of the functions of brainfuck, if not ignore them.
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

                                    if (brainFuckContent[i] == 91)
                                    {
                                        loops += 1;
                                    }
                                    else if (brainFuckContent[i] == 93)
                                    {
                                        loops -= 1;
                                    }
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

                                    if (brainFuckContent[i] == 91)
                                    {
                                        loops -= 1;
                                    }
                                    else if (brainFuckContent[i] == 93)
                                    {
                                        loops += 1;
                                    }
                                }
                            }
                            continue;
                        case 46:    //.
                            Console.Write((char)memoryByte[bytePointer]);
                            continue;
                        case 44:    //,
                            memoryByte[bytePointer] = (byte)Console.ReadKey().KeyChar;
                            continue;
                    }
                }

                //Make sure to exit the programm.
                ExitProgram();
            }

            ExitProgram();
        }



        //Make sure to check on which platform the user works.
        private static void ExitProgram()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            if (System.Windows.Forms.Application.MessageLoop)
            {
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                System.Environment.Exit(1);
            }
        }
    }
}