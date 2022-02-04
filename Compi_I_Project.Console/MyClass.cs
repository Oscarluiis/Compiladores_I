using System;
using System.IO;

namespace Compi_I_Project.Console
{
    public class MyClass
    {
        public MyClass()
        {
            var fileContent = File.ReadAllText("test.txt");
        }
    }
}
