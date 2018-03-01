using NijuCalculator;
using System.ComponentModel.Composition;

namespace SubtractModule
{
    [Export(typeof(NijuCalculator.IOperation))]
    [ExportMetadata("Symbol", '-')]
    public class Subtract: IOperation
    {
        public int Operate(int left, int right)
        {
            return left - right;
        }
    }
}
