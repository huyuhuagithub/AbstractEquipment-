using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ivi.Visa.Interop;

namespace AbstractEquipment.GPIB488Equipment
{
    public class IO488Resource
    {
        private FormattedIO488 visaGPIB = new FormattedIO488();

        public void Visa_GPIBOpen(string GPIBAddr)
        {
            ResourceManager grm = new ResourceManager();
            visaGPIB.IO = (IMessage)grm.Open(GPIBAddr);
        }

        internal void Visa_GPIBClose()
        {
            visaGPIB.IO.Close();
            visaGPIB.IO = null;
            GC.Collect();
        }

        internal void Visa_GPIBCmd(string cmdstr)
        {
            visaGPIB.WriteString(cmdstr, true);
        }

        internal string Visa_GPIBQuery(string cmdstr)
        {
            string VisaStr = string.Empty;
            visaGPIB.WriteString(cmdstr, true );
            VisaStr = visaGPIB.ReadString();
            return VisaStr;
        }
    }
}