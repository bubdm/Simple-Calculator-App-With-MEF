﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NijuCalculator
{
    public interface IOperation
    {
        int Operate(int left, int right);
    }
}
