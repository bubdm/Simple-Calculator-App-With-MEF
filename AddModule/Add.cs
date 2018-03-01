using NijuCalculator;
using System.ComponentModel.Composition;

namespace AddModule
{
    [Export(typeof(NijuCalculator.IOperation))]
    [ExportMetadata("Symbol", '+')]
    public class Add : IOperation
    {
        public int Operate(int left, int right)
        {
            return left + right;
        }
    }
}
