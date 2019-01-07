using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WpfApp4
{
    public partial class PathSelection : Form
    {
        public string GetPath(string des)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            //fbd.RootFolder = Environment.SpecialFolder.Desktop;
            fbd.SelectedPath = @"C:\Users\admin\source\repos";
            fbd.Description = des;
            fbd.ShowNewFolderButton = false;

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                return fbd.SelectedPath;
            }
            return "";
        }
    }
}
