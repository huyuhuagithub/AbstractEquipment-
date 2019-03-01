using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
namespace AbstractEquipment
{
    public abstract class AbstractRS232
    {
        public bool IsOpen { get; set; }
        public abstract SerialPort initializeRS232(string portName, int BaudRatio, string NewLine);
        public abstract void WriteCommand(SerialPort serialPort, string command);
        public abstract string ReadCommand(SerialPort serialPort);
        public abstract void CancelSerialPort(SerialPort serialPort);
    }
}
