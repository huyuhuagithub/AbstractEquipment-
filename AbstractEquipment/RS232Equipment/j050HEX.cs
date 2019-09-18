using AbstractEquipment.RS232Equipment;
using BaseModule.Helper.ConvertFrom;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbstractEquipment
{
    public class j050HEX : AbstractRS232
    {
        public override void CancelSerialPort(SerialPort serialPort)
        {
            serialPort.Close();
        }

        public override SerialPort initializeRS232(string portName, int BaudRatio, string NewLine)
        {
            SerialPort serialPort = new SerialPort() { PortName = portName, BaudRate = BaudRatio, DataBits = 8, StopBits = StopBits.One, Parity = Parity.None, NewLine = NewLine };
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
            int tempdatalenth = serialPort.BytesToRead;
            byte[] tempbytes = new byte[tempdatalenth];
            serialPort.Read(tempbytes, 0, tempdatalenth);
            return ConvertFrom.ToHexString(tempbytes);
        }

        public override string ReadQuery(SerialPort serialPort, string command)
        {
            WriteCommand(serialPort, command);
            Thread.Sleep(400);
            return Read(serialPort);
        }

        public override void WriteCommand(SerialPort serialPort, string command)
        {
            if (serialPort.IsOpen)
            {
                List<byte> list = ConvertFrom.HexstringToBytesArray(command);
                serialPort.Write(list.ToArray(), 0, list.Count);
            }
            else
            {
                serialPort.WriteLine(command);
            }

        }
    }
}
