using NijuCalculator;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivivsionModule
{
    [Export(typeof(NijuCalculator.IOperation))]
    [ExportMetadata("Symbol",'/')]
    public class Divide: IOperation
    {
        public int Operate(int left, int right)
        {
            return left / right;
        }
    }
}
