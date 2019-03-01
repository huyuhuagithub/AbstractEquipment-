using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractEquipment
{
    public class M2 : AbstractRS232
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

        public override string ReadCommand(SerialPort serialPort)
        {
            return serialPort.ReadExisting();
        }

        public override void WriteCommand(SerialPort serialPort, string command)
        {
            if (!serialPort.IsOpen)
            {
                serialPort.Open();
            }
            else
            {
                serialPort.WriteLine(command);
            }

        }
    }
}
