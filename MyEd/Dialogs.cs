using System.Windows.Documents;
using Microsoft.Win32;
using System.Windows;

namespace MyEd
{
	public class Dialogs
	{
		public Dialogs()
		{

		}

		public static MessageBoxResult SaveBeforeOpenMessageBoxResult()
		{
			return MessageBox.Show("Сохранить изменения в файл?",
			                       "Выход без сохранения!", MessageBoxButton.YesNoCancel,
			                       MessageBoxImage.Warning);
		}

		//public bool SaveAndOpenDlg()
		//{
		//	var result = NotSavingMessageBoxResult();
		//	if (result == MessageBoxResult.Yes)
		//	{
		//		if (fileIsNotSaved))
		//		{
		//			SaveFileDialog();
		//		}
					
		//	}
		//	else if (result == MessageBoxResult.No)
		//	{
		//		return true;
		//	}
		//	return false;
		//}


		public static string SaveAsXmlDialog()
		{
			// Configure save file dialog box
			var dlg = new SaveFileDialog();
			dlg.FileName = "Document"; // Default file name
			dlg.DefaultExt = ".xml"; // Default file extension
			dlg.Filter = "XML documents (.xml)|*.xml"; // Filter files by extension 
			bool? result = dlg.ShowDialog(); 
			if (result == true)
			{
				return dlg.FileName;
			}
			return "";
		}

		public static string OpenXmlDialog()
		{
			var dlg = new OpenFileDialog();
			dlg.DefaultExt = ".xml"; // Default file extension
			dlg.Filter = "XML documents (.xml)|*.xml"; // Filter files by extension 

			// Show open file dialog box
			bool? result = dlg.ShowDialog();

			// Process open file dialog box results 
			if (result == true)
			{
				return dlg.FileName;
			}
			return "";
		}
	}
}