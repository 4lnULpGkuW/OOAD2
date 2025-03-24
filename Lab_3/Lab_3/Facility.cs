using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3
{
    public abstract class Facility
    {
        public bool IsOccupied { get; protected set; }

        public bool Reserve()
        {
            if (IsOccupied)
                return false;

            IsOccupied = true;
            return true;
        }

        public bool Free()
        {
            if (!IsOccupied)
                return false;

            IsOccupied = false;
            return true;
        }
    }

    public class Runway : Facility
    {

    }

    public class Terminal : Facility
    {

    }
}
