using System;
using System.Collections.Generic;
using System.Text;

namespace Labs
{
    public static class LabUtils
    {
        public static void RunStep(string msg, Action action)
        {
            int len = 80;

            int totalPadding = len - msg.Length - 2;
            int leftPadding = totalPadding / 2;
            int rightPadding = totalPadding - leftPadding;

            Console.WriteLine(
                $"{new string('=', leftPadding)} {msg} {new string('=', rightPadding)}"
            );

            action();

            Console.WriteLine($"{new string('=', len)}");
            Console.WriteLine();
        }
    }
}
