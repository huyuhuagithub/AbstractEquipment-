using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AbstractEquipment;
using AbstractEquipment.CANEquipment;
using AbstractEquipment.RS232Equipment;
using BaseModule.Helper;
using BaseModule.Helper.ConvertFrom;

namespace 东峻Dome
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool cancelflag = false;
        private void Sendbut_Click(object sender, EventArgs e)
        {

            CANAbstract USBCAN2I = new USBCAN_2I();
            CANG(USBCAN2I, Datatb.Text);
            #region 123
            //List<byte> sbytet = new List<byte> { 160, 151, 12, 12, 19, 255, 01, 00, 02, 03 };
            //Displytb.Text = ConvertFrom.ByteArrayToHexString(sbytet.ToArray());
            //Displytb.Text = ConvertFrom.ByteArrayToString(new byte[] { 48, 49, 50, 51, 52, 53, 65, 66, 67, 68 }, Encoding.ASCII);
            //Displytb.Text = ConvertFrom.HexStringToString("ec a0 45 2d 43 61 72 78", Encoding.ASCII);
            //string kk = ConvertFrom.HexStringToString(Datatb.Text, Encoding.ASCII);
            //Datatb.Text = kk;
            //string ss = ConvertFrom.StringToHexString(kk, Encoding.ASCII);
            ////dataGridView1.Rows.Add(10);
            ////dataGridView1.Columns.Add(10);
            //Action<int> action = new Action<int>(s => dataGridView1.Rows[s].Cells[0].Value = s);
            ///* dataGridView1.Rows[0].Cells[3].Value = */
            //new int[] { 0, 1, 2, 3, 4 }.ToList().ForEach(s =>
            //{
            //    dataGridView1.Rows.Add();
            //    dataGridView1.Rows[s].Cells[3].Value = kk;
            //});
            ////DataGridViewRowCollection sss= dataGridView1.Rows;

            //Displytb.Text = ConvertFrom.HexStringToString(ss, Encoding.ASCII);
            #endregion
        }

        public string CANG<T>(T t, uint deviceType = 4, uint deviceIndex = 0, uint cANIndex = 0) where T : CANAbstract
        {
            string dataResult = "";
            test test = new test();
            test = TestobjectFactor.Create<test>();
            if (t.initializeCAN(deviceType, deviceIndex, cANIndex, 0X1C))
            {
                t.Query("ec a0 45 2d 43 61 72 78", 0x77B, deviceType, deviceIndex, cANIndex, 0x77a);
                int i = 0;
                foreach (var item in test.TestItem)
                {
                    i++;
                    string key = item.Key;
                    string value = item.Value;
                    dataResult = t.Query(value, 0x77B, deviceType, deviceIndex, cANIndex, 0x77a);
                    updatedataGridViewUI(dataGridView1, dataResult, i - 1, 3);
                    WriteLog.WriteLogFile(key + value + "\r\n" + dataResult + "\r\n");
                    updateTextBoxUI(Displytb, key + value + "\r\n" + dataResult + "\r\n\r\n");
                }
                t.CancelCAN(deviceType, cANIndex, cANIndex);
                return dataResult;
            }

            else
            {
                return "初始化失败!";
            }
        }
        public void CANG<T>(T t, string data, uint deviceType = 4, uint deviceIndex = 0, uint cANIndex = 0) where T : CANAbstract
        {
            string dataResult = "";
            if (t.initializeCAN(deviceType, deviceIndex, cANIndex, 0X1C))
            {
                t.Query("ec a0 45 2d 43 61 72 78", 0x77B, deviceType, deviceIndex, cANIndex, 0x77a);
                Thread.Sleep(100);
                dataResult = t.Query(data, 0x77B, deviceType, deviceIndex, cANIndex, 0x77a);
                WriteLog.WriteLogFile(data + "\r\n" + dataResult + "\r\n");
                updateTextBoxUI(Displytb, data + "\r\n" + dataResult + "\r\n");
            }
            t.CancelCAN(deviceType, cANIndex, cANIndex);
            //else
            //{
            //    return "初始化失败!";
            //}
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Displytb.Text = "";
            cancelflag = false;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Action action = new Action(NewMethod);
            action.BeginInvoke(callback =>
            {
                action.EndInvoke(callback);
                stopwatch.Stop();
                long time = stopwatch.ElapsedMilliseconds;
                updateLabelUI(label1, time.ToString() + "ms");
                button1.Invoke(new Action(() => { button1.Enabled = true; }));
            }, null);
            button1.Enabled = false;
        }

        private void NewMethod()
        {
            var cancelSource = new CancellationTokenSource();

            for (int i = 0; i < 100; i++)
            {
                if (cancelflag == false)
                {
                    CANAbstract USBCAN2I = new USBCAN_2I();
                    CANG(USBCAN2I);
                }
                else
                {
                    cancelSource.Cancel();
                    cancelflag = true;
                    break;
                }
            }
            #region 旧文件
            //updateTextBoxUI(Displytb, "查询开机完成:11 B5 00 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 B5 00 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（ACC电压)信息:11 C0 00 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 C0 00 00 00 00 00 00") + "\r\n");

            //updateTextBoxUI(Displytb, "设置（音量)信息:11 83 02 01 14 00 00 00\r\n" + CANG(USBCAN2I, "11 83 02 01 14 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（私有CAN)信息:11 BD 01 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 BD 01 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询(IHU MCU)版本信息:11 12 01 02 00 00 00 00\r\n" + CANG(USBCAN2I, "11 12 01 02 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（软件MPU)版本信息:11 12 01 03 00 00 00 00\r\n" + CANG(USBCAN2I, "11 12 01 03 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（硬件)版本信息:11 12 01 01 00 00 00 00\r\n" + CANG(USBCAN2I, "11 12 01 01 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（高德地图包)版本信息:11 12 01 04 00 00 00 00\r\n" + CANG(USBCAN2I, "11 12 01 04 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（MCUboot)版本信息:11 12 01 0D 00 00 00 00\r\n" + CANG(USBCAN2I, "11 12 01 0D 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（倒车Flash)版本信息:11 12 01 0E 00 00 00 00\r\n" + CANG(USBCAN2I, "11 12 01 0E 00 00 00 00") + "\r\n");

            //updateTextBoxUI(Displytb, "查询（ICCID)版本信息:11 13 01 02 00 00 00 00\r\n" + CANG(USBCAN2I, "11 13 01 02 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（IMSI)版本信息:11 13 01 03 00 00 00 00\r\n" + CANG(USBCAN2I, "11 13 01 03 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（MSISND)版本信息:11 13 01 04 00 00 00 00\r\n" + CANG(USBCAN2I, "11 13 01 04 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（XDSN)版本信息:11 A2 02 03 01 00 00 00\r\n" + CANG(USBCAN2I, "11 A2 02 03 01 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（IHUID)版本信息:11 A2 02 03 04 00 00 00\r\n" + CANG(USBCAN2I, "11 A2 02 03 04 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（UUID 配置字)信息:11 A2 02 03 06 00 00 00\r\n" + CANG(USBCAN2I, "11 A2 02 03 06 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（打开蓝牙)信息:11 60 01 01 00 00 00 00\r\n" + CANG(USBCAN2I, "11 60 01 01 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（蓝牙MAC地址)信息:11 62 00 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 62 00 00 00 00 00 00") + "\r\n");

            //updateTextBoxUI(Displytb, "查询（进入USB界面)信息:11 50 01 09 00 00 00 00\r\n" + CANG(USBCAN2I, "11 50 01 09 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "设置（播放第一曲USB歌曲)信息:11 54 04 01 00 01 00 00\r\n" + CANG(USBCAN2I, "11 54 04 01 00 01 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（切换到收音状态)信息:11 50 01 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 50 01 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（FM97.5MHz)信息:11 40 03 03 16 26 00 00\r\n" + CANG(USBCAN2I, "11 40 03 03 16 26 00 00") + "\r\n");

            //updateTextBoxUI(Displytb, "查询（4G信号强度)信息:11 31 00 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 31 00 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（4G自诊断)信息:11 32 00 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 32 00 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（清除DTC故障码)信息:11 B2 00 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 B2 00 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（DTC查询)信息:11 B2 01 01 00 00 00 00\r\n" + CANG(USBCAN2I, "11 B2 01 01 00 00 00 00") + "\r\n");

            //updateTextBoxUI(Displytb, "查询（获取陀螺仪GYRO ID)信息:11 BC 00 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 BC 00 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（打开WIFI测试)信息:11 20 01 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 20 01 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（WIFI测试)信息:11 21 01 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 21 01 00 00 00 00 00") + "\r\n");

            //updateTextBoxUI(Displytb, "查询（GPS 信息)信息:11 70 01 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 70 01 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（GPS 搜星)信息:11 71 00 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 71 00 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（查询第一路方控)信息:11 BA 01 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 BA 01 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（查询第二路方控)信息:11 BA 01 01 00 00 00 00\r\n" + CANG(USBCAN2I, "11 BA 01 01 00 00 00 00") + "\r\n");

            //updateTextBoxUI(Displytb, "查询（Lin通信)信息:11 C6 00 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 C6 00 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（雷达串口通信)信息:11 C7 00 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 C7 00 00 00 00 00 00") + "\r\n");

            //updateTextBoxUI(Displytb, "查询（USB过流)信息:11 CA 01 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 CA 01 00 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（音量值)信息:11 83 01 02 00 00 00 00\r\n" + CANG(USBCAN2I, "11 83 01 02 00 00 00 00") + "\r\n");

            //updateTextBoxUI(Displytb, "查询（IO测试 CAR_ACC_DET)信息:11 C8 01 04 00 00 00 00\r\n" + CANG(USBCAN2I, "11 C8 01 04 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（IO测试 MTK_ACC_DET)信息:11 C8 01 01 00 00 00 00\r\n" + CANG(USBCAN2I, "11 C8 01 01 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（IO测试 MCU_PULES_DET)信息:11 C8 01 03 00 00 00 00\r\n" + CANG(USBCAN2I, "11 C8 01 03 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（IO测试 MTK_PULES_DET)信息:11 C8 01 02 00 00 00 00\r\n" + CANG(USBCAN2I, "11 C8 01 02 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（IO测试 MTK_VCC_DET)信息:11 C8 01 05 00 00 00 00\r\n" + CANG(USBCAN2I, "11 C8 01 05 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（IO测试 CSD_ACC-EN OFF)信息:11 C9 02 03 00 00 00 00\r\n" + CANG(USBCAN2I, "11 C9 01 03 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（IO测试 CSD_ACC-EN ON)信息:11 C9 02 03 01 00 00 00\r\n" + CANG(USBCAN2I, "11 C9 02 03 01 00 00 00") + "\r\n");

            //updateTextBoxUI(Displytb, "查询（关闭蓝牙)信息:11 60 01 02 00 00 00 00\r\n" + CANG(USBCAN2I, "11 60 01 02 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（关闭WIFI)信息:11 20 01 01 00 00 00 00\r\n" + CANG(USBCAN2I, "11 20 01 01 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（清除蓝牙连接)信息:11 6B 01 01 00 00 00 00\r\n" + CANG(USBCAN2I, "11 6B 01 01 00 00 00 00") + "\r\n");
            //updateTextBoxUI(Displytb, "查询（睡眠电流)信息:11 90 01 00 00 00 00 00\r\n" + CANG(USBCAN2I, "11 90 01 00 00 00 00 00") + "\r\n");

            #endregion
        }
        public void updateTextBoxUI(TextBox textBox, string text)
        {
            textBox.Invoke(new Action(() =>
            {
                textBox.Text += text;
                textBox.Focus();//获取焦点
                textBox.Select(textBox.TextLength, 0);//光标定位到文本最后
                textBox.ScrollToCaret();//滚动到光标处
            }));
        }
        public void updateLabelUI(Label textBox, string text)
        {
            textBox.Invoke(new Action(() =>
            {
                textBox.Text = text;
            }));
        }

        public void updatedataGridViewUI(DataGridView datedataGridView, string text, int row, int cell)
        {
            datedataGridView.Invoke(new Action(() =>
            {
                dataGridView1.Rows[row].Cells[cell].Value = text;
            }));
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            CANAbstract USBCAN2I = new USBCAN_2I();
            USBCAN2I.CancelCAN(4, 0, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(10);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cancelflag = true;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Action action = scan;
            action.BeginInvoke(null, null);

        }

        private void scan()
        {
            AbstractRS232 bt001 = new ES4600AT();
            SerialPort serialPortbt = bt001.initializeRS232("COM7", 9600, "\r\n");
            try
            {
                string data = bt001.ReadQuery(serialPortbt, "16 54 0d");
                bt001.CancelSerialPort(serialPortbt);
                label2.Invoke(new Action(() => { label2.Text = data; }));
                WriteLog.WriteLogFile(data + "\r\n");
            }
            catch (TimeoutException ex)
            {
                bt001.CancelSerialPort(serialPortbt);
                Button3_Click(1, new EventArgs());

            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            string value = "02 25 03 14 05 2B 06 2B 09 27 0C 26 11 2E 13 2A 17 28 1C 25 43 00 44 00 00 00 00 00";
            bool b = Satellite(value);
        }

        private static bool Satellite(string value)
        {
            List<byte> listvar = ConvertFrom.HexstringToBytesArray(value);
            List<byte> Satelliteindex = new List<byte>();
            List<int> Strength = new List<int>();
            var newlist = listvar.Select((b, index) => new { index, b });//投影出index
            foreach (var item in newlist)
            {
                if (item.index % 2 == 1)
                {
                    Strength.Add(item.b);
                }
                else
                {
                    Satelliteindex.Add(item.b);
                }
            }
            return Strength.Where(i => i > 21).Count() >= 7;
        }
    }
}
