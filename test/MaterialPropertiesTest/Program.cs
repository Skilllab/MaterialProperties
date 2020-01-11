using System;
using Material;

namespace MaterialPropertiesTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MaterialProperties myCP = new MaterialProperties();

            if (myCP.GetMaterialPropertiesFromActiveDoc(out var matName, out var matProps) == ErrorFlags.AllOK)
            {
                Console.WriteLine($"Для материала \"{matName}\" найдены следующие свойства: " );
                Console.WriteLine("");
                foreach (ConfigProperties configProperties in matProps)
                {
                    Console.WriteLine($"Наименование свойства: {configProperties.PropertyName}");
                    Console.WriteLine($"Описание свойства: {configProperties.PropertyDescription}");
                    Console.WriteLine($"Значение свойства: {configProperties.PropertyValue}");
                    Console.WriteLine($"Единица измерения: {configProperties.PropertyUnits}");
                    Console.WriteLine("--------------------------------");
                }

                Console.ReadLine();
            }

            

        }
    }
}
