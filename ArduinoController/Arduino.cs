using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoController
{
    class Arduino
    {
        private string ComPort;
        private string DeviceVersion;
        private string DeviceName;
        private byte PHandlerID;
        private int Baud;
        private SerialPort serial;
        private bool isInit;

        //ReadOnly Properties
        public string getCOM() { return this.ComPort; }
        public string getVersion() { return this.DeviceVersion; }
        public string getName() { return this.DeviceName; }
        public byte getPacketHandlerID() { return this.PHandlerID; }
        public int getBaudRate() { return this.Baud; }
        public bool getIsInit() { return this.isInit; }
        public int getInitPacketLength() { return 2 + DeviceName.Length; }
        public int getAvailableLength() { return serial.BytesToRead; }
        //Constructor & Initialization
        public Arduino(string COM, string DeviceVersion = "Arduino x", string DeviceName = "My Arduino", byte PHandlerID = 0, int Baud = 9600)
        {
            this.ComPort = COM;
            this.DeviceVersion = DeviceVersion;
            this.DeviceName = DeviceName;
            this.PHandlerID = PHandlerID;
            this.Baud = Baud;
            initSerial();
            sendInitPacket();
        }

        enum PacketHeaders
        {
            Init = 0x2F,
            Pause = 0x3F,
            UnPause = 0x4F,
            Ping = 0x2E,
            PingR = 0x20,
        }

        private void initSerial()
        {
            serial = new SerialPort(this.ComPort, this.Baud);
            serial.Open();
        }

        public void killSerial()
        {
            serial.Close();
        }

        private void sendInitPacket()
        {
            
            byte[] data = new byte[2 + DeviceName.Length];
            data[0] = (byte)PacketHeaders.Init;
            data[1] = getPacketHandlerID();
            for (int i = 0; i < DeviceName.Length - 1; i++)
            {
                data[i + 2] = (byte) DeviceName[i];
            }
            serial.Write(data, 0, data.Length);
            isInit = true;
        }

        public void pauseDevice()
        {
            this.sendPacket((byte)PacketHeaders.Pause);
        }

        public void unpauseDevice()
        {
            this.sendPacket((byte)PacketHeaders.UnPause); ;
        }

        public void sendPacket(byte[] data)
        {
            serial.Write(data, 0, data.Length);
        }

        public void sendPacket(byte data)
        {
            byte[] d = new byte[1] { data };
            this.sendPacket(d);
        }

        public string readString(int length = 0)
        {
            if (length == 0) { length = this.getAvailableLength(); }
            return System.Text.Encoding.UTF8.GetString(readPacket(length));
        }

        public int ping()
        {
            this.sendPacket((byte)PacketHeaders.Ping);
            int i=0;
            while (!(readPacket(1)[0] == (byte)PacketHeaders.PingR))
            {
                System.Threading.Thread.Sleep(1);
                i++;
                if (i > 1000) { return -1; }
            }
            return i;
        }

        public byte[] readPacket(int length = 0)
        {
            if (length == 0) { length = this.getAvailableLength(); }
            byte[] buffer = new byte[length];
            serial.Read(buffer, 0, length);
            return buffer;
        }
    }
}
