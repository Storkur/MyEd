using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace MyEd
{
	/// <summary>
	///     Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public delegate void FilePathChangedHandler(String filePath);

		public delegate void FileSavedHandler(bool isFileSaved);

		public event FilePathChangedHandler FilePathChanged;
		public event FileSavedHandler SetFileSaveStatus;

		private const double Pt = 96 / 72.0;
		private bool isFileSaved;

		public MainWindow()
		{
			InitializeComponent();

			var userPrefs = new UserPreferences();

			Height = userPrefs.WindowHeight;
			Width = userPrefs.WindowWidth;
			Top = userPrefs.WindowTop;
			Left = userPrefs.WindowLeft;
			WindowState = userPrefs.WindowState;
			StartupFileLoading(userPrefs);
			FilePathChanged += ChangeTitle;
			SetFileSaveStatus += UpdateFileSaveStatus;
		}

		private string FilePath
		{
			get
			{
				if (Application.Current.Properties["CurrentFile"] is string)
				{
					return (string)Application.Current.Properties["CurrentFile"];
				}
				else return "";
			}
			set { Application.Current.Properties["CurrentFile"] = value; }
		}

		private FlowDocument Document
		{
			get { return EdBox.Document; }
			set
			{
				if (value != null)
					EdBox.Document = value;
				else 
					EdBox.Document = new FlowDocument();
			}
		}

		#region Open, Save, New, Open last file or from command line

		private void StartupFileLoading(UserPreferences userPrefs)
		{
			if (App.Args.Length > 0)
			{
				String filePath = App.Args[0];
				Document = FileOperations.OpenFile(filePath);
			}
			else
			{
				if (userPrefs.LastFilePath != "")
					Document = FileOperations.OpenFile(userPrefs.LastFilePath);
			}
			FilePath = userPrefs.LastFilePath;
		}


		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}


		private void Save_Click(object sender, RoutedEventArgs e)
		{
			if (FilePath != "")
			{
				FileOperations.SaveFile(EdBox.Document, FilePath);
				if (FilePathChanged != null)
					FilePathChanged(FilePath);
			}
			else
			{
				FileOperations.SaveFile(EdBox.Document, Dialogs.SaveAsXmlDialog());
			}
		}


		private void SaveAs_Click(object sender, RoutedEventArgs e)
		{
			FileOperations.SaveFile(EdBox.Document, Dialogs.SaveAsXmlDialog());
		}


		private void New_Click(object sender, RoutedEventArgs e)
		{
			FilePath = "";
			EdBox.Document = new FlowDocument();
		}

		#endregion



		private void UpdateFileSaveStatus(bool isFileSaved)
		{
			this.isFileSaved = isFileSaved;
		}

		private void MyWindow_Closing(object sender, CancelEventArgs e)
		{
			var userPrefs = new UserPreferences();

			userPrefs.WindowHeight = Height;
			userPrefs.WindowWidth = Width;
			userPrefs.WindowTop = Top;
			userPrefs.WindowLeft = Left;
			userPrefs.WindowState = WindowState;
			userPrefs.LastFilePath = FilePath;

			userPrefs.Save();
		}

		private void EdBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if ((e.Changes.Count < 2) && (e.Changes.Count > 0) && (IsInitialized))
				if (FilePathChanged != null)
					FilePathChanged(FilePath);
		}

		private void ChangeTitle(string filepath)
		{
			string title = "MyED";
			if (!isFileSaved) title += " *";
			
			if (filepath != "") title += " " + filepath;
			Title = title; //TODO медленно работает
		}

		private void OpenCmdExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			OpenFileCommand();
		}

		/// <summary>
		/// Invokes on Open File command from context menu or Ctrl+O, display some dialogs
		/// </summary>
		private void OpenFileCommand()
		{
			MessageBoxResult result = Dialogs.SaveBeforeOpenMessageBoxResult();
			if (result == MessageBoxResult.Yes)
			{
				bool fileSaveResult = FileOperations.SaveFile(Document, Dialogs.SaveAsXmlDialog());
				if (fileSaveResult)
				{
					TryOpenNewDocument();
				}
			}
			else if (result == MessageBoxResult.No)
			{
				TryOpenNewDocument();
			}
		}

		/// <summary>
		/// Display open file dialog and try to open document
		/// </summary>
		private void TryOpenNewDocument()
		{
			var newFilePath = Dialogs.OpenXmlDialog();
			var document = FileOperations.OpenFile(newFilePath);
			if (document != null)
			{
				Document = document; 
				UpdateFileSaveStatus(true);
				FilePathChanged(newFilePath);
			}
		}

		private void SaveCmdExecuted(object sender, ExecutedRoutedEventArgs e)
		{
		}

		private void SaveAsCmdExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void CloseCmdExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}