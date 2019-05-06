using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbstractEquipment.CANEquipment
{
   public abstract class CANAbstract
    {
        public bool IsOpen { get; set; }
        public abstract bool initializeCAN(uint DeviceType, uint DeviceIndex, uint CANIndex,byte baudratio);
        public abstract string Read(uint command, uint deviceType, uint deviceIndex, uint cANIndex);
        public abstract void Write( string command, uint deviceType, uint deviceIndex, uint cANIndex, uint frameid);
        public abstract string Query(string command, uint filterID, uint deviceType, uint deviceIndex, uint cANIndex, uint frameid);
        public abstract void CancelCAN(uint DeviceType, uint DeviceIndex, uint CANIndex);
    }
}
