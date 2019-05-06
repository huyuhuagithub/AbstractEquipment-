using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractEquipment.RS232Equipment
{
    public abstract class AbstractRS232
    {
        public bool IsOpen { get; set; }
        public abstract SerialPort initializeRS232(string portName, int BaudRatio, string NewLine);
        public abstract string Read(SerialPort serialPort);
        public abstract void WriteCommand(SerialPort serialPort, string command);
        public abstract string ReadQuery(SerialPort serialPort, string command);
        public abstract void CancelSerialPort(SerialPort serialPort);
    }
}
