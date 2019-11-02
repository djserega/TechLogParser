using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TechLogParser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            PathLogs = @"C:\Users\djserega\Documents\logs";
        }


        public string PathLogs
        {
            get { return (string)GetValue(PathLogsProperty); }
            set { SetValue(PathLogsProperty, value); }
        }
        public static readonly DependencyProperty PathLogsProperty =
            DependencyProperty.Register("PathLogs", typeof(string), typeof(MainWindow));

        public ObservableCollection<Model.DirectoryLog> ListLog
        {
            get { return (ObservableCollection<Model.DirectoryLog>)GetValue(ListLogProperty); }
            set { SetValue(ListLogProperty, value); }
        }
        public static readonly DependencyProperty ListLogProperty =
            DependencyProperty.Register("ListLog", typeof(ObservableCollection<Model.DirectoryLog>), typeof(MainWindow));

        public Model.DirectoryLog ListLogSelectedItem
        {
            get { return (Model.DirectoryLog)GetValue(ListLogSelectedItemProperty); }
            set
            {
                SetValue(ListLogSelectedItemProperty, value);
                if (value != null)
                    ListLogSelectedItemFileLogs = new ObservableCollection<Model.FileLog>(value.FileLogs);

                ListLogSelectedItemFileLogsDataLogs = null;
            }
        }
        public static readonly DependencyProperty ListLogSelectedItemProperty =
            DependencyProperty.Register("ListLogSelectedItem", typeof(Model.DirectoryLog), typeof(Model.DirectoryLog));

        public ObservableCollection<Model.FileLog> ListLogSelectedItemFileLogs
        {
            get { return (ObservableCollection<Model.FileLog>)GetValue(ListLogSelectedItemFileLogsProperty); }
            set { SetValue(ListLogSelectedItemFileLogsProperty, value); }
        }
        public static readonly DependencyProperty ListLogSelectedItemFileLogsProperty =
            DependencyProperty.Register("ListLogSelectedItemFileLogs", typeof(ObservableCollection<Model.FileLog>), typeof(MainWindow));

        public Model.FileLog ListLogSelectedItemFileLogsSelectedItem
        {
            get { return (Model.FileLog)GetValue(ListLogSelectedItemFileLogsSelectedItemProperty); }
            set
            {
                SetValue(ListLogSelectedItemFileLogsSelectedItemProperty, value);
                if (value != null)
                    ListLogSelectedItemFileLogsDataLogs = new ObservableCollection<Model.DataLog>(value.DataLogs);
            }
        }
        public static readonly DependencyProperty ListLogSelectedItemFileLogsSelectedItemProperty =
            DependencyProperty.Register("ListLogSelectedItemFileLogsSelectedItem", typeof(Model.FileLog), typeof(Model.FileLog));

        public ObservableCollection<Model.DataLog> ListLogSelectedItemFileLogsDataLogs
        {
            get { return (ObservableCollection<Model.DataLog>)GetValue(ListLogSelectedItemFileLogsDataLogsProperty); }
            set { SetValue(ListLogSelectedItemFileLogsDataLogsProperty, value); }
        }
        public static readonly DependencyProperty ListLogSelectedItemFileLogsDataLogsProperty =
            DependencyProperty.Register("ListLogSelectedItemFileLogsDataLogs", typeof(ObservableCollection<Model.DataLog>), typeof(MainWindow));
        

        private void ButtonSelectPath_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog()
            {
                Description = "Каталог логов техжурнала:",
                ShowNewFolderButton = false
            };
            DialogResult result = folderDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                PathLogs = folderDialog.SelectedPath;
            }
        }

        private void ButtonReadPathLog_Click(object sender, RoutedEventArgs e)
        {
            List<Model.DirectoryLog> listLog = new List<Model.DirectoryLog>();

            if (string.IsNullOrEmpty(PathLogs))
                return;

            string[] dirLogs = Directory.GetDirectories(PathLogs);

            foreach (string pathDir in dirLogs)
            {
                Model.DirectoryLog directoryLog = new Model.DirectoryLog(pathDir);

                string[] files = Directory.GetFiles(pathDir);

                foreach (string pathFile in files)
                {
                    Model.FileLog fileLog = new Model.FileLog(pathFile);
                    fileLog.ParseTextFileLog();

                    if (!fileLog.DataLogsIsEmpty)
                        directoryLog.FileLogs.Add(fileLog);
                }

                if (!directoryLog.FileLogsIsEmpty)
                    listLog.Add(directoryLog);
            }

            if (listLog.Count > 0)
                ListLog = new ObservableCollection<Model.DirectoryLog>(listLog);
        }
    }
}