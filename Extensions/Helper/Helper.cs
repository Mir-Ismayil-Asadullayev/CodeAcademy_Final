using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Extensions.Helper
{
    public class Helper
    {
        public static void DeleteImg(string root, string folder, string ImageName)
        {
            string fullpath = Path.Combine(root, folder, ImageName);
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }
        }
    }
}
