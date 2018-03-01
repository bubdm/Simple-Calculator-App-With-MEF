# Calculator console application using .NET MEF

Managed Extensibility Framework or MEF is a library for creating lightweight, extensible applications. It allows application developers to discover and use extensions with no configuration required.

### Intro 

The core of the MEF composition model is the composition container, which contains all the parts available and performs composition. (That is, the matching up of imports to exports.) The most common type of composition container is CompositionContainer, and thats what I am using here.

In order to discover the parts available to it, the composition containers makes use of a catalog. A catalog is an object that makes available parts discovered from some source. MEF provides catalogs to discover parts from a provided type, an assembly, or a directory. Application developers can easily create new catalogs to discover parts from other sources, such as a Web service. 

### Basic analogy - from Code Project.
```
There are three basic parts to MEF in your application. If we continue on with our laptop analogy, we can visualize these three parts as the USB port on the laptop, an external hard drive with a USB connector, and the hand that plugs the USB connector into the port. In MEF terms, the port is defined as an [Import] statement. This statement is placed above a property to tell the system that something gets plugged in here. The USB cable is defined as an [Export] statement. This statement is placed above a class, method, or property to indicate that this is an item to be plugged in somewhere. An application could (and probably will) have a lot of these exports and imports. The job of the third piece is to figure out which ports we have and thus which cables we need to plug into them. This is the job of the CompositionContainer. It acts like the hand, plugging in the cables that match up to the appropriate ports. Those cables that don’t match up with a port are ignored.
```

### Points to note
	1. I have 6 projects here.
	2. I have the main application i.e NijuCalculator and 6 other class libraries (add, subtract, divide, multiply and mod).
	3. Each operation i.e "add" is a module by itself.
	4. I have a "Plugins" folder where all module dlls are kept for discovery by NijuCalculator.
	5. Here is how "add" exports its stuff
	```
	[Export(typeof(NijuCalculator.IOperation))]
    [ExportMetadata("Symbol", '+')]
    public class Add : IOperation
    {
        public int Operate(int left, int right)
        {
            return left + right;
        }
    }
	```
	6. And here is how Calculator imports stuff from "add" module
	```
	[Export(typeof(NijuCalculator.ICalculator))]
    public class Calculator : ICalculator
    {
        [ImportMany]
        IEnumerable<Lazy<IOperation, IOperationData>> operations;

        public String Calculate(String input)
        {
            int left;
            int right;
            Char operation;
            int fn = FindFirstNonDigit(input); //finds the operator  
            if (fn < 0) return "Could not parse command.";

            try
            {
                //separate out the operands  
                left = int.Parse(input.Substring(0, fn));
                right = int.Parse(input.Substring(fn + 1));
            }
            catch
            {
                return "Could not parse command.";
            }

            operation = input[fn];

            foreach (Lazy<IOperation, IOperationData> i in operations)
            {
                if (i.Metadata.Symbol.Equals(operation)) return i.Value.Operate(left, right).ToString();
            }
            return "Operation Not Found!";
        }
	}
	```
	7. Notice Calculator itself is exporting its stuff to main program therefore it can be a module on itself too. Here the Main program gets the calculator import.
	```
	  [Import(typeof(NijuCalculator.ICalculator))]
       public ICalculator calculator;
	```
	
	8. If you happen to add your own module don't forget to change build path to the "Plugins" folder in NijuCalculator project.
	
#### The code is well commented and feel free to contact me in case of any problem.


### Happy Coding!!!!!!!!!!!!!!! Osu!