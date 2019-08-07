using AbstractEquipment.RS232Equipment;
using BaseModule.Helper.ConvertFrom;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace AbstractEquipment
{
    public class ES4600AT : AbstractRS232
    {
        public override void CancelSerialPort(SerialPort serialPort)
        {
            serialPort.Close();
        }

        public override SerialPort initializeRS232(string portName, int BaudRatio, string NewLine)
        {
            SerialPort serialPort = new SerialPort() { PortName = portName, BaudRate = BaudRatio, DataBits = 8, StopBits = StopBits.One, Parity = Parity.None, NewLine = NewLine };
            serialPort.ReadTimeout = 2000;
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                }
                else
                {
                    serialPort.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e); ;
            }
            return serialPort;
        }

        public override string Read(SerialPort serialPort)
        {
            byte[] tempdata = new byte[serialPort.ReadBufferSize];
            int i = serialPort.Read(tempdata, 0, tempdata.Length);
            return ConvertFrom.ByteArrayToString(tempdata, Encoding.ASCII).Replace("\0", "").Replace("\r\n", "");
        }

        public override string ReadQuery(SerialPort serialPort, string command)
        {
            WriteCommand(serialPort, command);
            Thread.Sleep(800);
            return Read(serialPort);
        }

        public override void WriteCommand(SerialPort serialPort, string command)
        {
            if (serialPort.IsOpen)
            {
                List<byte> list = ConvertFrom.HexstringToBytesArray(command);
                serialPort.Write(list.ToArray(), 0, list.Count);
            }

        }
    }
}
