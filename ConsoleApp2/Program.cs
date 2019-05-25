using AbstractEquipment;
using AbstractEquipment.RS232Equipment;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseModule.Helper.ConvertFrom;
namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            AbstractRS232 bt001 = new ES4600AT();
            SerialPort serialPortbt = bt001.initializeRS232("COM7", 9600, "\r\n");
            string d = bt001.ReadQuery(serialPortbt, "16 54 0d");
            
            Console.WriteLine(d);
            Console.ReadLine();
        }
    }
}
