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

		/// <summary>
		/// Save xml document in file to disk
		/// </summary>
		/// <param name="document">Document content</param>
		/// <param name="FilePath">Filepath to save document</param>
		/// <returns></returns>
        public static bool SaveFile(FlowDocument document, string FilePath)
        {
	        try
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
	        catch (Exception e)
	        {
				Dialogs.CommonExceprionMsg(e.Message);
		        return false;
	        }
        }
	}
}
