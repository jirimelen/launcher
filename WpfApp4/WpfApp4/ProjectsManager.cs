using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4
{
    class ProjectsManager
    {


        public void ChangeDir(string originPath, string destPath)
        {
            string dirname = string.Empty;
            string path = string.Empty;
            List<string> originPathParts = originPath.Split('\\').ToList();
            for (int i = 0; i < 3; i++)
            {
                originPathParts.Remove(originPathParts.Last());
            }
            dirname = originPathParts.Last();
            originPathParts.Remove(originPathParts.Last());
            foreach (var item in originPathParts)
            {
                path += item + "\\";
            }
            Directory.Move(path, destPath + "\\" + dirname);
        }

        public void DeleteProject(string originPath)
        {
            string dirname = string.Empty;
            string path = string.Empty;
            List<string> originPathParts = originPath.Split('\\').ToList();
            for (int i = 0; i < 3; i++)
            {
                originPathParts.Remove(originPathParts.Last());
            }
            originPathParts.Remove(originPathParts.Last());
            foreach (var item in originPathParts)
            {
                path += item + "\\";
            }
            Directory.Delete(path, true);
        }
    }
}
