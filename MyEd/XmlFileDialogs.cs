using Microsoft.Win32;

namespace MyEd
{
	public class XmlFileDialogs
	{
		private MainWindow _mainWindow;

		public XmlFileDialogs(MainWindow mainWindow)
		{
			_mainWindow = mainWindow;
		}

		private void OpenXmlFileDialog()
		{
			var dlg = new OpenFileDialog();
			//dlg.FileName = "Document"; // Default file name
			dlg.DefaultExt = ".xml"; // Default file extension
			dlg.Filter = "XML documents (.xml)|*.xml"; // Filter files by extension 

			// Show open file dialog box
			bool? result = dlg.ShowDialog();

			// Process open file dialog box results 
			if (result == true)
			{
				// Open document 
				FileOperations.OpenFile(dlg.FileName);
				_mainWindow.DisplayDocument(FileOperations.OpenFile(dlg.FileName));
				_mainWindow.FilePath = dlg.FileName;
				if (_mainWindow.FileChanged != null)
					_mainWindow.FileChanged(_mainWindow.FilePath, true);
			}
		}

		private void SaveAsXmlFileDialog()
		{
			// Configure save file dialog box
			var dlg = new SaveFileDialog();
			dlg.FileName = "Document"; // Default file name
			dlg.DefaultExt = ".xml"; // Default file extension
			dlg.Filter = "XML documents (.xml)|*.xml"; // Filter files by extension 

			// Show save file dialog box
			bool? result = dlg.ShowDialog();

			// Process save file dialog box results 
			if (result == true)
			{
				// Save document 
				FileOperations.SaveFile(_mainWindow.EdBox.Document, dlg.FileName);
				_mainWindow.FilePath = dlg.FileName;
			}
		}
	}
}