using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;

namespace ArduinoController
{
    class Program
    {
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private delegate bool EventHandler(CtrlType sig);
        static EventHandler _handler;

        enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }
        private static bool Handler(CtrlType sig)
        {
            switch (sig)
            {
                case CtrlType.CTRL_CLOSE_EVENT:
                    ard.killDevice();
                    return true;
                default: return true;
            }
        }

        static Arduino ard;
        static void Main(string[] args)
        {
            _handler += new EventHandler(Handler);
            SetConsoleCtrlHandler(_handler, true);

            string com;
            if (args.Length == 0)
            {
                Console.Write("Enter COM port (Press Enter for COM3): ");
                com = Console.ReadLine();
                if (com == "") com = "COM3";
            }
            else
            {
                com = args[0];
            }
            ard = new Arduino(COM: com, DeviceVersion: "Arduino Uno Rev.3", DeviceName: "SammkosArduino");
            Root.root(ard);
        }
    }
}
