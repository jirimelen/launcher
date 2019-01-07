using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WpfApp4
{
    class ItemsManager
    {
        List<FileInfo> projectFiles = new List<FileInfo>();
        List<FileInfo> exeFiles = new List<FileInfo>();

        public void Search(string searchPath)
        {

        }
        // use FileFilter enum update / obj / bin
        public List<FileInfo> GetProjects(string Path)
        {
            foreach (string dirPath in Directory.GetFiles(Path, "*.csproj", SearchOption.AllDirectories))
            {
                FileInfo infoNew = new FileInfo(dirPath);
                Boolean Equals = false;
                Boolean Newer = false;
                // check if already in list
                foreach (var item in projectFiles)
                {
                    if (item.Name.Equals(infoNew.Name)) Equals = true;
                    if (Equals)
                    {
                        if (DateTime.Compare(infoNew.LastWriteTime, item.LastWriteTime) > 0)
                        {
                            Newer = true;
                            projectFiles.Remove(item);
                            break;
                        }
                    }
                }
                // display the newer one 
                if (Equals && Newer)
                {
                    projectFiles.Add(infoNew);
                }
                else if (!Equals)
                {
                    projectFiles.Add(infoNew);
                }
            }

            return projectFiles;
        }

        public List<FileInfo> GetExes(string Path)
        {
            exeFiles.Clear();
            foreach (FileInfo project in GetProjects(Path))
            {
                foreach (string dirPath in Directory.GetFiles(project.Directory.FullName, "*.exe", SearchOption.AllDirectories))
                {
                    FileInfo infoNew = new FileInfo(dirPath);
                    Boolean Equals = false;
                    Boolean Newer = false;
                    // check if already in list
                    foreach (var item in exeFiles)
                    {
                        if (item.Name.Equals(infoNew.Name)) Equals = true;
                        if (Equals)
                        {
                            if (DateTime.Compare(infoNew.LastWriteTime, item.LastWriteTime) > 0)
                            {
                                Newer = true;
                                exeFiles.Remove(item);
                                break;
                            }
                        }
                    }
                    // display the newer one 
                    if (Equals && Newer)
                    {
                        exeFiles.Add(infoNew);
                    }
                    else if (!Equals)
                    {
                        exeFiles.Add(infoNew);
                    }
                }
            }

            return exeFiles;
        }
    }
}
