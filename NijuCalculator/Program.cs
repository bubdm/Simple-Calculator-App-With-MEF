using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NijuCalculator
{
    class Program
    {
        private CompositionContainer _container;

        //import is like a jig-saw for other programs to "hook" to your system
        [Import(typeof(NijuCalculator.ICalculator))]
        public ICalculator calculator;
        private Program()
        {
            //An aggregate catalog that combines multiple catalogs  
            var catalog = new AggregateCatalog();

            //Adds all the parts found in the same assembly as the Program class : TO FIND CALCULATOR!!!!! 
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));

            //Adds all the parts found in this project folder  : Plugins
            catalog.Catalogs.Add(new DirectoryCatalog(@"D:\Projects\NijuCalculator\NijuCalculator\Plugins"));

            //Create the CompositionContainer with the parts in the catalog  
            _container = new CompositionContainer(catalog);

            //Fill the imports of this object  
            try
            {
                this._container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }
        static void Main(string[] args)
        {
            Program p = new Program(); //Composition is performed in the constructor  
            String s;
            Console.WriteLine("Enter Command:");
            while (true)
            {
                s = Console.ReadLine();
                try
                {
                    Console.WriteLine(p.calculator.Calculate(s));
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.InnerException.ToString());
                }
            }
        }
    }
}
