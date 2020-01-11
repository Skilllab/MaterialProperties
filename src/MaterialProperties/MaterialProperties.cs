using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml;

namespace Material
{
    public class MaterialProperties
    {
        public ErrorFlags GetMaterialPropertiesFromActiveDoc(out string currentMaterial, out List<ConfigProperties> configPropertieses)
        {
            configPropertieses = new List<ConfigProperties>();
            currentMaterial = string.Empty;

            var activeSolidworksObject = Marshal.GetActiveObject("SldWorks.Application");
            if (activeSolidworksObject == null)
            {
                return ErrorFlags.ActiveSWNotFound;
            }

            var activeDoc = activeSolidworksObject.GetType().InvokeMember("ActiveDoc", BindingFlags.GetProperty, null, activeSolidworksObject, null);
            //progType - Тот объект, который будет модифицироваться. В случае если тип переменной object, требуется .GetType()
            //"ActiveDoc" - имя того, что посылаем
            //BindingFlags.GetProperty - получение свойства из целевого объекта
            //sw - целевой объект
            if (activeDoc == null)
            {
                return ErrorFlags.ActiveDocNull;
            }

            var docType = (int)activeDoc.GetType().InvokeMember("GetType", BindingFlags.InvokeMethod, null, activeDoc, null);

            //Если документ не деталь
            if (docType != 1)
            {
                return ErrorFlags.ActiveDocNotPart;
            }

           
            var matIDName = (string)activeDoc.GetType().InvokeMember("MaterialIdName", BindingFlags.GetProperty, null, activeDoc, null);

            if (string.IsNullOrEmpty(matIDName))
            {
                return ErrorFlags.MaterialNotFound;
            }

            var baseName = matIDName.Substring(0, matIDName.IndexOf("|"));

            currentMaterial = baseName;
            var matDB = (string[])activeSolidworksObject.GetType()
                .InvokeMember("GetMaterialDatabases", BindingFlags.InvokeMethod, null, activeSolidworksObject, null);
            if (matDB == null || matDB.Length<=0)
            {
                return ErrorFlags.MaterialDBNotFound;
            }

            var pathToBase = "";
            foreach (var current in matDB)
            {
                var IIII = current.Substring(current.LastIndexOf("\\") + 1);
                var LLL = IIII.Remove(IIII.LastIndexOf(".")).ToLower();
                if (LLL == baseName.ToLower())
                {
                    pathToBase = current;
                    break;
                }
            }
            var propName = matIDName.Substring(matIDName.LastIndexOf("|")+1, matIDName.Length - matIDName.LastIndexOf("|")-1);
            var doc = new XmlDocument();
            doc.Load(pathToBase);


            var root = doc.DocumentElement;
            var nodes = root.SelectNodes("//material");
            XmlNode rightNode = null;
            foreach (XmlNode node in nodes)
            {
                var employeeName = node.Attributes;
                foreach (XmlAttribute xmlAttribute in employeeName)
                {
                    if (xmlAttribute.Value == propName)
                    {
                        rightNode = node;
                    }
                }
            }
            try
            {
                if (rightNode != null)
                {
                    foreach (XmlNode childNode in rightNode.ChildNodes)
                    {
                        if (childNode.Name.ToLower() == "custom")
                        {
                            foreach (XmlNode node in childNode)
                            {
                                var nodeName = node.Attributes[0].Value; //name


                                var nodeDescription = node.Attributes[1].Value; //description


                                var nodeValue = node.Attributes[2].Value; //value


                                var nodeUnits = node.Attributes[3].Value; //units

                                var currentProperties = new ConfigProperties(nodeName, nodeDescription, nodeValue, nodeUnits);
                                configPropertieses.Add(currentProperties);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
            Marshal.ReleaseComObject(activeSolidworksObject);
            Marshal.ReleaseComObject(activeDoc);
            activeSolidworksObject = null;
            activeDoc = null;

            return ErrorFlags.AllOK;
            
        }
       
    }
   
}
