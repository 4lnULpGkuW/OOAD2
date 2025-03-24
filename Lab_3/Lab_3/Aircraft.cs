using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3
{
    public class Aircraft
    {
        public string RegCode { get; }
        private IControlTower controlTower;
        public Aircraft(string regCode, IControlTower controlTower)
        {
            RegCode = regCode;
            this.controlTower = controlTower;
        }
        public void Request()
        {
            controlTower.ProcessRequest(this);
        }
        public void Leave()
        {
            controlTower.RemoveAircraft(this);
        }
    }
}
