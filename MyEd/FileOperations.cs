using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Xml;

namespace MyEd
{
    class FileOperations
    {
        public static FlowDocument OpenFile(string FilePath)
        {
			if (File.Exists(FilePath))
			{
				Stream stream = File.OpenRead(FilePath);
				StreamReader sr = new StreamReader(stream);
				String data = sr.ReadLine();
				sr.Close();
				stream.Close();
				var stringReader = new StringReader(data);
				var xmlTextReader = new XmlTextReader(stringReader);
				return (FlowDocument)XamlReader.Load(xmlTextReader);
			}
			else return null;
        }


        public static bool SaveFile(FlowDocument document, string FilePath)
        {
	        if (FilePath == "") 
				return false;
            var data = XamlWriter.Save(document);

            Stream stream = File.Open(FilePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(stream);
            sw.Write(data);
            sw.Close();
            stream.Close();

	        return true;
        }
    }
}
