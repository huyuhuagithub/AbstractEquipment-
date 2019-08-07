using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractEquipment;
using System.IO.Ports;
using AbstractEquipment.CANEquipment;
using System.Threading;
using AbstractEquipment.RS232Equipment;
using BaseModule.Helper;
using AbstractEquipment.GPIB488Equipment;

using System.IO;
using AbstractEquipment.ABTECAudioAnalyzeEquipment;

namespace ConsoleApp11
{
    class Program
    {
        static void Main(string[] args)
        {
            #region RS232Dome
            //AbstractRS232 m2 = new M2();

            //SerialPort serialPortm2 = m2.initializeRS232("COM10", 115200, "\r\n");
            //m2.WriteCommand(serialPortm2, "Power_DC_OUT 3 1");
            //m2.CancelSerialPort(serialPortm2);

            AbstractRS232 bt001 = new ES4600AT();
            SerialPort serialPortbt = bt001.initializeRS232("COM7", 9600, "\r\n");

            string d = bt001.ReadQuery(serialPortbt, "16 54 0d");
            //d += bt001.ReadQuery(serialPortbt, "AT+SCON=000D1909A543");
            //d += bt001.ReadQuery(serialPortbt, "AT+SCON=000D1909A543");
            //d += bt001.ReadQuery(serialPortbt, "AT+SCON=000D1909A543");
            //d += bt001.ReadQuery(serialPortbt, "AT+SCON=000D1909A543");
            //d += bt001.ReadQuery(serialPortbt, "AT+SCON=000D1909A543");
            //d += bt001.ReadQuery(serialPortbt, "AT+SCON=000D1909A543");
            Console.WriteLine(d);
            //bt001.CancelSerialPort(serialPortbt);

            //AbstractRS232 m2 = new M2();

            //AbstractRS232 BT001 = new BT001();
            ////rs2322(m2);
            //rs2322G(BT001);
            #endregion

            #region GPIBDome
            //GPIBAbstract gPIBAbstractSG1501B = new JUNGJIN_SG1501B();
            //GPIBAbstract gPIBAbstract66319D = new Agilent_66319D();
            //GPIBG(gPIBAbstractSG1501B, "*IND?", "");
            #endregion

            #region canDome
            //CANAbstract USBCAN2I = new USBCAN_2I();

            //WriteLog.ConsoleWritelog("查询开机完成\r\n" + CANG(USBCAN2I, "11 B5 00 00 00 00 00 00"));
            //WriteLog.ConsoleWritelog("查询硬件版本信息\r\n" + CANG(USBCAN2I, "11 12 01 01 00 00 00 00"));

            //WriteLog.ConsoleWritelog("查询软件版本信息\r\n" + CANG(USBCAN2I, "11 12 01 03 00 00 00 00"));
            //WriteLog.ConsoleWritelog("查询MCU版本信息\r\n" + CANG(USBCAN2I, "11 12 01 02 00 00 00 00"));

            //WriteLog.ConsoleWritelog("查询VIN码信息\r\n" + CANG(USBCAN2I, "11 13 01 00 00 00 00 00"));
            //WriteLog.ConsoleWritelog("查询TBOX/TEMID码信息\r\n" + CANG(USBCAN2I, "11 13 01 01 00 00 00 00"));

            //WriteLog.ConsoleWritelog("查询ICCID码信息\r\n" + CANG(USBCAN2I, "11 13 01 02 00 00 00 00"));
            //WriteLog.ConsoleWritelog("查询IMSI码信息\r\n" + CANG(USBCAN2I, "11 13 01 03 00 00 00 00"));

            //WriteLog.ConsoleWritelog("查询MSISND(ASCII)码信息\r\n" + CANG(USBCAN2I, "11 13 01 04 00 00 00 00"));
            //WriteLog.ConsoleWritelog("查询BT地址信息\r\n" + CANG(USBCAN2I, "11 62 00 00 00 00 00 00"));

            //WriteLog.ConsoleWritelog("开启BT\r\n" + CANG(USBCAN2I, "11 60 01 01 00 00 00 00"));

            #endregion

            #region JsonDome
            //atcdata atcdata1 = new atcdata()
            //{
            //    yValue = new double[] { 12.01, 12.10, 12.10, 12.10, 12.10, 12.10, 12.10, 12.10, 12.10, 12.10 },
            //    xValue = new double[] { 13.01, 13.10, 13.10, 13.10, 13.10, 13.10, 13.10, 13.10, 13.10, 13.10 },
            //    dateTime = DateTime.Now,
            //};

            //string s = JsonHelper.ObjToJsonString(atcdata1);
            //WriteLog.WriteLogFile(s);
            //atcdata atcdata12= Create<atcdata>();
            #endregion

            #region ATCDome
            //ATCAbstract aTC = new ATCAbstract();
            
            #endregion
            Console.ReadLine();

        }
        public static void rs2322(AbstractRS232 abstractRS232)
        {
            SerialPort serialPortm2 = abstractRS232.initializeRS232("COM10", 9600, "\r\n");
            abstractRS232.WriteCommand(serialPortm2, "Power_DC_OUT 3 1");
            abstractRS232.CancelSerialPort(serialPortm2);
        }

        public static void rs2322G<T>(T t) where T : AbstractRS232
        {
            SerialPort serialPort = t.initializeRS232("COM10", 115200, "\r\n");
            t.WriteCommand(serialPort, "Power_DC_OUT 3 1");
            t.CancelSerialPort(serialPort);
        }

        //public static string CANG<T>(T t, string data, uint deviceType = 4, uint deviceIndex = 0, uint cANIndex = 0) where T : CANAbstract
        //{
        //    string dataResult;
        //    t.initializeCAN(deviceType, deviceIndex, cANIndex, 0X1C);
        //    t.Query("ec a0 45 2d 43 61 72 78", 0x77B, deviceType, deviceIndex, cANIndex);
        //    Thread.Sleep(50);
        //    dataResult = t.Query(data, 0x77B, deviceType, deviceIndex, cANIndex);

        //    //WriteLog.WriteLogFile(dataResult);
        //    //t.ResetCAN(4, 0, 0);
        //    t.CancelCAN(deviceType, cANIndex);

        //    return dataResult;
        //}

        //public static string GPIBG<T>(T t, string data, string addr) where T : GPIBAbstract
        //{
        //    string dataResult;
        //    FormattedIO488 formattedIO488 = t.initializeGPIB(addr);
        //    dataResult = t.Visa_GPIBQuery(formattedIO488, data);
        //    t.Visa_GPIBClose(formattedIO488);
        //    return dataResult;
        //}


       // public static T Create<T>() where T : atcdata
       // {
       //     string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
       //         $"ConfigFiles\\{typeof(T).Name}.json");

       //     string info = File.ReadAllText(path);
       //     T model = JsonHelper.JsonToObj<T>(info);
       //     return model;
       // }
       //public class atcdata
       // {
       //     public DateTime dateTime { get; set; }
       //     public double [] xValue { get; set; }
       //     public double [] yValue { get; set; }
           
       // }
    }
}
