using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace myFunctionlib
{
    public class MathFuncions
    {
        [DllExport("Add", CallingConvention = CallingConvention.Cdecl)]
        public static int Add(int a, int b)
        {
            return a + b;
        }

        [DllExport("GetMessage", CallingConvention = CallingConvention.Cdecl)]
        public static IntPtr GetMessage()
        {
            string message = "GetMessage";
            return Marshal.StringToHGlobalAnsi(message);
        }
    }
}
