using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbstractEquipment.GPIB488Equipment;
using Ivi.Visa.Interop;

namespace AbstractEquipment.GPIB488Equipment
{
    public class JUNGJIN_SG1501B : GPIBAbstract
    {

        public override FormattedIO488 initializeGPIB(string GPIBAddr)
        {
            FormattedIO488 visaGPIB = new FormattedIO488();
            ResourceManager grm = new ResourceManager();
            visaGPIB.IO = (IMessage)grm.Open(GPIBAddr);
            return visaGPIB;
        }

        public override string Visa_GPIBCmd(FormattedIO488 iO488, string cmdstr)
        {
            iO488.WriteString(cmdstr, true);
            return "ok";
        }

        public override string Visa_GPIBQuery(FormattedIO488 iO488, string cmdstr)
        {
            string VisaStr = string.Empty;
            iO488.WriteString(cmdstr, true);
            VisaStr = iO488.ReadString();
            return VisaStr;
        }

        public override void Visa_GPIBClose(FormattedIO488 iO488)
        {
            iO488.IO.Close();
            iO488.IO = null;
            GC.Collect();
        }
    }
}
