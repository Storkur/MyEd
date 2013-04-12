using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Microsoft.Win32;

namespace MyEd
{
	/// <summary>
	///     Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public delegate void FileChangedHandler (String filePath, bool isFileSaved);

		public event FileChangedHandler FileChanged;

		private const double Pt = 96/72.0;

		private bool isFileSaved = false;
		private readonly Dialogs _dialogs;

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
			FileChanged += ChangeTitle;
			FileChanged += UpdateFileSaveStatus;
			_dialogs = new Dialogs();
		}

		void UpdateFileSaveStatus(string filePath, bool isFileSaved)
		{
			this.isFileSaved = isFileSaved;
		}

		private string FilePath
		{
			get
			{
				if (Application.Current.Properties["CurrentFile"] is string)
				{
					return (string) Application.Current.Properties["CurrentFile"];
				}
				else return "";
			}
			set { Application.Current.Properties["CurrentFile"] = value; }
		}


		#region Open, Save, New, Open last file or from command line

		private void StartupFileLoading(UserPreferences userPrefs)
		{
			if (App.Args.Length > 0)
			{
				String filePath = App.Args[0];
				DisplayDocument(FileOperations.OpenFile(filePath));
			}
			else
			{
				if (userPrefs.LastFilePath != "")
					DisplayDocument(FileOperations.OpenFile(userPrefs.LastFilePath));
			}
			FilePath = userPrefs.LastFilePath;
		}


		private void Open_Click(object sender, RoutedEventArgs e)
		{
			var document = FileOperations.OpenFile(Dialogs.OpenXmlDialog());
			DisplayDocument(document);
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
				if (FileChanged != null)
					FileChanged(FilePath, true);
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



		/// <summary>
		/// Display new document
		/// </summary>
		/// <param name="document"></param>
		private void DisplayDocument(FlowDocument document)
		{
			EdBox.Document = document;
			if (FileChanged != null)
				FileChanged(FilePath, true);
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
			if ((e.Changes.Count < 2) && (e.Changes.Count > 0) && (this.IsInitialized))
				if (FileChanged != null)
					FileChanged(FilePath, false);
			
		}

		private void ChangeTitle(string filepath, bool isFileSaved)
		{
			string title = "MyED";
			if (!isFileSaved) title += " *";
			title += " " + filepath;
			this.Title = title;
		}


		private void LineHeight1_Click(object sender, RoutedEventArgs e)
		{
			EdBox.Document.LineHeight = 1;
		}

		private void LineHeight2_Click(object sender, RoutedEventArgs e)
		{
			EdBox.Document.LineHeight = 4;
		}

		private void LineHeight3_Click(object sender, RoutedEventArgs e)
		{
			EdBox.Document.LineHeight = 8;
		}

		private void OpenCmdExecuted(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
		{
			FileOperations.OpenFile(Dialogs.OpenXmlDialog());
		}

		private void OpenCmdCanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
		{
			if (!this.isFileSaved)
			{


			}

			else e.CanExecute = true;
		}

		private void SaveCmdExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void SaveCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void SaveAsCmdExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void SaveAsCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void CloseCmdExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void CloseCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}