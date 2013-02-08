using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ArduinoController
{
    class Program
    {
        static Arduino ard;
        static void Main(string[] args)
        {
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
            
            while (true)
            {
                if (ard.getAvailableLength() > 0)
                {
                    Console.Write(ard.readString());
                }
            }
        }
    }
}
