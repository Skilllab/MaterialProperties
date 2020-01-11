### MaterialProperties

Данная библиотека предназначена для чтения свойств материала у детали Solidworks.

- Работает с версии Solidworks 2001;
- Создавалась и тестировалась с версией .NET Framework 3.5

![](https://img.shields.io/github/release/Skilllab/MaterialProperties.svg)
![](https://img.shields.io/github/issues/Skilllab/MaterialProperties.svg)


**Применение**
```C#
MaterialProperties myCP = new MaterialProperties();

if (myCP.GetMaterialPropertiesFromActiveDoc(out var matName, out var matProps) == ErrorFlags.AllOK) {
    Console.WriteLine($ "Для материала \"{matName}\" найдены следующие свойства: ");
    Console.WriteLine("");
    foreach (ConfigProperties configProperties in matProps) {
        Console.WriteLine($ "Наименование свойства: {configProperties.PropertyName}");
        Console.WriteLine($ "Описание свойства: {configProperties.PropertyDescription}");
        Console.WriteLine($ "Значение свойства: {configProperties.PropertyValue}");
        Console.WriteLine($ "Единица измерения: {configProperties.PropertyUnits}");
        Console.WriteLine("--------------------------------");
    }

    Console.ReadLine();
}
```
