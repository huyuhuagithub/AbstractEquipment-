using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractEquipment;
using System.IO.Ports;
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //AbstractRS232 m2 = new M2();

            //SerialPort serialPortm2 = m2.initializeRS232("COM10", 115200, "\r\n");
            //m2.WriteCommand(serialPortm2, "Power_DC_OUT 3 1");
            //m2.CancelSerialPort(serialPortm2);

            //AbstractRS232 bt001 = new BT001();
            //SerialPort serialPortbt = bt001.initializeRS232("COM10", 9600, "\r\n");
            //bt001.WriteCommand(serialPortbt, "Power_DC_OUT 3 1");
            //bt001.CancelSerialPort(serialPortbt);
            AbstractRS232 m2 = new M2();
            AbstractRS232 BT001 = new BT001();
            //rs2322(m2);
            rs2322G(BT001);
        }
        public static void rs2322(AbstractRS232 abstractRS232)
        {
            SerialPort serialPortm2=  abstractRS232.initializeRS232("COM10", 9600, "\r\n");
            abstractRS232.WriteCommand(serialPortm2, "Power_DC_OUT 3 1");
            abstractRS232.CancelSerialPort(serialPortm2);
        }

        public static void rs2322G<T>(T t) where T : AbstractRS232
        {
            SerialPort serialPortm2 = t.initializeRS232("COM10", 115200, "\r\n");
            t.WriteCommand(serialPortm2, "Power_DC_OUT 3 1");
            t.CancelSerialPort(serialPortm2);
        }
    }
}
