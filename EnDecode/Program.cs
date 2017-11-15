using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnDecode
{
    /// <summary>
    /// This program show encode and decode string using ascii (range between 32 to 126)
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Again:
            Console.WriteLine("Enter your choice: Encode (E) OR Decode (D) OR Exit (X)");
            ConsoleKeyInfo consoleResponse = Console.ReadKey(true);
            string result = string.Empty;

            switch (consoleResponse.Key)
            {
                case ConsoleKey.E:
                    EnDecoding.Encoding();
                    break;
                case ConsoleKey.D:
                    EnDecoding.Decoding();
                    break;
                case ConsoleKey.X:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Wrong Choice!!!");
                    System.Threading.Thread.Sleep(1000);
                    Environment.Exit(0);
                    break;
            }

            goto Again;
        }
    }

    public class EnDecoding
    {
        public static int[] allAscii = Enumerable.Range(32, 95).ToArray();
        public static bool isValid = true;

        /// <summary>
        /// To encode the given value with secure manner as to reverse and store/show
        /// </summary>
        public static void Encoding()
        {
            Start:
            Console.WriteLine("Enter value to encode");
            string ToEncode = Console.ReadLine();
            string sEncoded = string.Empty;
            string encodeResult = string.Empty;
            List<string> lstRevEncode = new List<string>();
            isValid = true;

            foreach (var item in ToEncode)
            {
                int val = Convert.ToInt32(item);
                if (allAscii.Contains(val))
                    sEncoded += +val;
                else
                {
                    isValid = false;
                    goto exit;
                }
            }
            if (isValid)
            {
                char[] charEncode = sEncoded.ToCharArray();
                for (int i = charEncode.Length - 1; i >= 0; i--)
                {
                    lstRevEncode.Add(charEncode[i].ToString());
                }

                encodeResult = string.Join("", lstRevEncode.ToArray());
                Console.WriteLine("Resulting encoding is: " + encodeResult);
            }

            exit:
            if (!isValid)
            {
                Console.WriteLine("Only between 32 to 126 ascii range to encode!");
                goto Start;
            }

        }

        /// <summary>
        /// To decode the given secure reverse value and store/show
        /// </summary>
        public static void Decoding()
        {
            Again:
            Console.WriteLine("Enter value to decode");
            string Todecode = Console.ReadLine();
            string sdecode = string.Empty;
            string decodeResult = string.Empty;
            bool isNumeric = true;
            isValid = true;

            char[] charDecode = Todecode.ToCharArray();
            for (int i = charDecode.Length - 1; i >= 0; i--)
            {
                sdecode += charDecode[i];
                if (charDecode[i] < '0' || charDecode[i] > '9')
                    isNumeric = false;
            }
            
            if (isNumeric)
            {
                int idecode = 0;
                isValid = true;

                while (sdecode.Length > 1)
                {
                    idecode = Convert.ToInt32(sdecode.Substring(0, 2));

                    if (idecode < 32 && sdecode.Length > 2)
                        idecode = Convert.ToInt32(sdecode.Substring(0, 3));

                    if (!allAscii.Contains(idecode))
                    {
                        isValid = false;
                        goto exit;
                    }

                    if (isValid)
                    {
                        decodeResult += Convert.ToString(Convert.ToChar(idecode));

                        sdecode = sdecode.Substring(Convert.ToString(idecode).Length, sdecode.Length - Convert.ToString(idecode).Length);
                    }
                }
                if (sdecode.Length == 1 || decodeResult == "")
                    isValid = false;

                exit:
                if (!isValid)
                {
                    Console.WriteLine("Only between 32 to 126 ascii range to decode!");
                    goto Again;
                }
                else
                    Console.WriteLine("Resulting decoding is: " + decodeResult);
            }
            else
            {
                Console.WriteLine("Only numeric value to decode!");
                goto Again;
            }
        }
    }

}
