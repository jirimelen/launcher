using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;

namespace WpfApp4
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    { 
        public static 
        //List<TextBlock> Items = new List<TextBlock>();
        //List<string> usedNames = new List<string>();
        List<FileInfo> ItemsInfo = new List<FileInfo>();
        string activePath = String.Empty;

        string selectedPath = new PathSelection().GetPath("Select folder with projects");
        private ItemsManager itemsManager = new ItemsManager();
        private ProjectsManager projectsManager = new ProjectsManager();

        public MainWindow()
        {
            InitializeComponent();

            DisplayFiles();
            
        }

        public void DisplayFiles(FileFilter filter = FileFilter.update)
        {
            //ItemsInfo = new List<FileInfo>();
            ItemsInfo = itemsManager.GetExes(selectedPath, filter);
            
            files.ItemsSource = ItemsInfo;
        }

        public void FilterFiles(object sender, EventArgs e)
        {
            FileFilter filter = (FileFilter)int.Parse(((Button)sender).Tag.ToString());
            DisplayFiles(filter);
        }

        public void RunFile(object sender, RoutedEventArgs e)
        {
            Process proc = new Process();
            Button btn = (Button)sender;
            string directory = activePath;

            if (!directory.Equals(String.Empty))
            {
                proc.StartInfo = new ProcessStartInfo(directory);
                proc.Start();
            }
                
        }

        private void listView_select(object sender, EventArgs e)
        {
            if ((sender as ListView).SelectedItems.Count != 0)
            {
                UpdateInfo((FileInfo)(sender as ListView).SelectedItems[0]);
            }
        }

        public void UpdateInfo(FileInfo info)
        {
            fileOptions.Children.Clear();
            TextBlock child;
            Button childButton = new Button();
            Button childButton1 = new Button();
            Button childButton2 = new Button();

            child = new TextBlock();
            child.Text = "path:";
            child.FontWeight = FontWeights.Bold;
            fileOptions.Children.Add(child);
            child = new TextBlock();
            child.Text = activePath = info.Directory + "\\" + info.Name;
            fileOptions.Children.Add(child);

            child = new TextBlock();
            child.Text = "last update:";
            child.FontWeight = FontWeights.Bold;
            fileOptions.Children.Add(child);
            child = new TextBlock();
            child.Text = info.LastWriteTime.ToString();
            fileOptions.Children.Add(child);

            childButton.Content = "run";
            childButton.Click += RunFile;
            fileOptions.Children.Add(childButton);

            childButton1.Content = "delete";
            childButton1.Click += DeleteProject;
            fileOptions.Children.Add(childButton1);
            
            childButton2.Content = "relocate";
            childButton2.Click += RelocateProject;
            fileOptions.Children.Add(childButton2);
        }

        private void ChangeDirectory(object sender, RoutedEventArgs e)
        {
            selectedPath = new PathSelection().GetPath("Select folder with projects");
            DisplayFiles();
        }

        private void RelocateProject(object sender, RoutedEventArgs e)
        {
            projectsManager.ChangeDir(activePath, new PathSelection().GetPath("Select folder with projects"));
            DisplayFiles();
        }

        private void DeleteProject(object sender, RoutedEventArgs e)
        {
            projectsManager.DeleteProject(activePath);
            DisplayFiles();
        }
    }
}
