﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3
{
    public interface IControlTower
    {
        Task ProcessRequest(Aircraft aircraft);
        void RemoveAircraft(Aircraft aircraft);
    }
}
